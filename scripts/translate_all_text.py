#!/usr/bin/env python3
"""
Efficient translator for ALL_TEXT_ja.txt using DeepL API
Optimized for maximum efficiency with large files
"""

import os
import sys
import json
import re
import time
import urllib.request
import urllib.parse
from typing import List, Optional, Dict
from pathlib import Path

class EfficientTranslator:
    def __init__(self, api_key: str):
        self.api_key = api_key
        self.cache: Dict[str, str] = {}
        self.session_stats = {
            'total_lines': 0,
            'processed_lines': 0,
            'translated_lines': 0,
            'cached_hits': 0,
            'api_calls': 0,
            'skipped_lines': 0
        }
    
    def should_translate(self, line: str) -> bool:
        """Determine if a line needs translation"""
        if not line or len(line.strip()) < 3:
            return False
        
        trimmed = line.strip()
        
        # Skip if already contains Japanese characters
        if re.search(r'[\u3040-\u309F\u30A0-\u30FF\u4E00-\u9FFF]', trimmed):
            return False
        
        # Skip code-like patterns
        code_patterns = [
            r'^(virtual |public |private |class |enum |struct |namespace )',
            r'\(\)|\{\}|\[\]',
            r'^using\s+\w+',
            r'^\s*//.*',
            r'^\s*/\*.*\*/',
            r'^\s*\d+\.\s*$',  # Line numbers only
            r'^[\s\|]+$'  # Only whitespace and pipes
        ]
        
        for pattern in code_patterns:
            if re.search(pattern, trimmed):
                return False
        
        # Must contain English letters
        return bool(re.search(r'[a-zA-Z]', trimmed))
    
    async def translate_text(self, text: str) -> str:
        """Translate text using DeepL API with caching"""
        if not text.strip():
            return text
        
        # Check cache first
        if text in self.cache:
            self.session_stats['cached_hits'] += 1
            return self.cache[text]
        
        try:
            # Prepare request data
            data = {
                'auth_key': self.api_key,
                'text': text,
                'target_lang': 'JA',
                'source_lang': 'EN'
            }
            
            # Try free endpoint first, then paid
            endpoints = [
                'https://api-free.deepl.com/v2/translate',
                'https://api.deepl.com/v2/translate'
            ]
            
            for endpoint in endpoints:
                try:
                    encoded_data = urllib.parse.urlencode(data).encode('utf-8')
                    req = urllib.request.Request(endpoint, data=encoded_data, method='POST')
                    req.add_header('Content-Type', 'application/x-www-form-urlencoded')
                    
                    with urllib.request.urlopen(req, timeout=30) as response:
                        result = json.loads(response.read().decode('utf-8'))
                        
                        if 'translations' in result and len(result['translations']) > 0:
                            translated = result['translations'][0]['text']
                            self.cache[text] = translated
                            self.session_stats['api_calls'] += 1
                            return translated
                        
                except urllib.error.HTTPError as e:
                    if e.code == 403 or e.code == 429:
                        print(f"API error {e.code}, trying next endpoint...")
                        continue
                    else:
                        raise
                except Exception as e:
                    print(f"Error with endpoint {endpoint}: {e}")
                    continue
            
        except Exception as e:
            print(f"Translation error: {e}")
        
        # Return original text if translation fails
        return text
    
    def translate_file(self, input_path: str, output_path: str) -> None:
        """Translate entire file with progress tracking"""
        print(f"Reading file: {input_path}")
        
        with open(input_path, 'r', encoding='utf-8', errors='ignore') as f:
            lines = f.readlines()
        
        self.session_stats['total_lines'] = len(lines)
        
        print(f"Total lines: {len(lines):,}")
        
        # Analyze content first
        translatable_count = sum(1 for line in lines[:min(10000, len(lines))] if self.should_translate(line))
        estimated_translatable = int(translatable_count * len(lines) / min(10000, len(lines)))
        
        print(f"Estimated translatable lines: {estimated_translatable:,}")
        print(f"Estimated API calls: {estimated_translatable:,}")
        print(f"Estimated cost: ${estimated_translatable * 0.00002:.2f} USD (rough estimate)")
        print()
        
        confirm = input("Do you want to proceed? (y/N): ").strip().lower()
        if not confirm.startswith('y'):
            print("Translation cancelled.")
            return
        
        print("\nStarting translation...")
        start_time = time.time()
        translated_lines = []
        
        try:
            for i, line in enumerate(lines):
                if self.should_translate(line):
                    # This is a synchronous version - for async, you'd need asyncio
                    translated = self.translate_sync(line.strip())
                    if translated != line.strip():
                        translated_lines.append(translated + '\n')
                        self.session_stats['translated_lines'] += 1
                    else:
                        translated_lines.append(line)
                        self.session_stats['skipped_lines'] += 1
                    
                    # Rate limiting
                    time.sleep(0.2)  # 200ms delay between requests
                else:
                    translated_lines.append(line)
                    self.session_stats['skipped_lines'] += 1
                
                self.session_stats['processed_lines'] += 1
                
                # Progress report every 100 lines
                if (i + 1) % 100 == 0 or i == len(lines) - 1:
                    elapsed = time.time() - start_time
                    percent = (i + 1) * 100.0 / len(lines)
                    eta = elapsed * (len(lines) - i - 1) / (i + 1) if i > 0 else 0
                    
                    print(f"Progress: {i+1:,}/{len(lines):,} ({percent:.1f}%) | "
                          f"Translated: {self.session_stats['translated_lines']:,} | "
                          f"Elapsed: {elapsed/60:.1f}m | ETA: {eta/60:.1f}m")
                
                # Save progress every 1000 lines
                if (i + 1) % 1000 == 0:
                    with open(output_path + '.progress', 'w', encoding='utf-8') as f:
                        f.writelines(translated_lines)
            
            # Save final result
            with open(output_path, 'w', encoding='utf-8') as f:
                f.writelines(translated_lines)
            
            # Clean up progress file
            progress_file = output_path + '.progress'
            if os.path.exists(progress_file):
                os.remove(progress_file)
            
            elapsed = time.time() - start_time
            print(f"\n=== Translation Complete ===")
            print(f"Total time: {elapsed/3600:.2f} hours")
            print(f"Lines processed: {self.session_stats['processed_lines']:,}")
            print(f"Lines translated: {self.session_stats['translated_lines']:,}")
            print(f"API calls made: {self.session_stats['api_calls']:,}")
            print(f"Cache hits: {self.session_stats['cached_hits']:,}")
            print(f"Output saved to: {output_path}")
            
        except KeyboardInterrupt:
            print(f"\nTranslation interrupted by user.")
            print(f"Partial progress saved to: {output_path}.progress")
        except Exception as e:
            print(f"\nError: {e}")
            print(f"Partial progress may be saved to: {output_path}.progress")
    
    def translate_sync(self, text: str) -> str:
        """Synchronous version of translate_text"""
        if not text.strip():
            return text
        
        # Check cache first
        if text in self.cache:
            self.session_stats['cached_hits'] += 1
            return self.cache[text]
        
        try:
            data = {
                'auth_key': self.api_key,
                'text': text,
                'target_lang': 'JA',
                'source_lang': 'EN'
            }
            
            endpoints = [
                'https://api-free.deepl.com/v2/translate',
                'https://api.deepl.com/v2/translate'
            ]
            
            for endpoint in endpoints:
                try:
                    encoded_data = urllib.parse.urlencode(data).encode('utf-8')
                    req = urllib.request.Request(endpoint, data=encoded_data, method='POST')
                    req.add_header('Content-Type', 'application/x-www-form-urlencoded')
                    
                    with urllib.request.urlopen(req, timeout=30) as response:
                        result = json.loads(response.read().decode('utf-8'))
                        
                        if 'translations' in result and len(result['translations']) > 0:
                            translated = result['translations'][0]['text']
                            self.cache[text] = translated
                            self.session_stats['api_calls'] += 1
                            return translated
                        
                except urllib.error.HTTPError as e:
                    if e.code in (403, 429):
                        continue
                    else:
                        raise
                except Exception:
                    continue
        
        except Exception as e:
            print(f"Translation error: {e}")
        
        return text

def main():
    print("=== Efficient ALL_TEXT_ja.txt Translator ===\n")
    
    # Setup paths
    input_path = "References/ALL_TEXT_ja.txt"
    output_path = "References/ALL_TEXT_ja_translated.txt"
    
    if not os.path.exists(input_path):
        print(f"ERROR: Input file not found: {input_path}")
        print("Please ensure you're running from the project root directory.")
        return 1
    
    # Get API key
    api_key = input("Please enter your DeepL API key: ").strip()
    if not api_key:
        print("No API key provided. Translation cannot proceed.")
        return 1
    
    # File info
    file_size = os.path.getsize(input_path)
    print(f"\nFile Information:")
    print(f"  Input: {input_path}")
    print(f"  Output: {output_path}")
    print(f"  Size: {file_size:,} bytes ({file_size/1024/1024:.2f} MB)")
    
    # Create translator and run
    translator = EfficientTranslator(api_key)
    translator.translate_file(input_path, output_path)
    
    return 0

if __name__ == "__main__":
    sys.exit(main())