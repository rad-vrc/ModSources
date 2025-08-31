#!/usr/bin/env python3
"""
Test the translation logic without actually calling DeepL API
Creates a small sample translation for verification
"""

import os
import re

def should_translate(line: str) -> bool:
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
        r'^\s*\d+\.\s*$',
        r'^[\s\|]+$'
    ]
    
    for pattern in code_patterns:
        if re.search(pattern, trimmed):
            return False
    
    # Must contain English letters
    return bool(re.search(r'[a-zA-Z]', trimmed))

def mock_translate(text: str) -> str:
    """Mock translation for testing purposes"""
    # Simple mock translations for common terms
    mock_translations = {
        "Public Member Functions": "パブリックメンバー関数",
        "Protected Member Functions": "プロテクテッドメンバー関数", 
        "Properties": "プロパティ",
        "List of all members": "全メンバーのリスト",
        "ModItem Class Reference": "ModItemクラスリファレンス",
        "This class serves as a place for you to place all your properties and hooks for each item.": "このクラスは、各アイテムのすべてのプロパティとフックを配置する場所として機能します。"
    }
    
    return mock_translations.get(text.strip(), f"[TRANSLATED] {text}")

def create_sample_translation():
    """Create a small sample translation to verify the process"""
    input_path = "References/ALL_TEXT_ja.txt"
    output_path = "References/ALL_TEXT_ja_sample.txt"
    
    if not os.path.exists(input_path):
        print(f"ERROR: Input file not found: {input_path}")
        return
    
    print("Creating sample translation of first 100 lines...")
    
    with open(input_path, 'r', encoding='utf-8', errors='ignore') as f:
        lines = f.readlines()
    
    translated_lines = []
    translated_count = 0
    
    # Process first 100 lines only
    for i, line in enumerate(lines[:100]):
        if should_translate(line):
            translated = mock_translate(line.strip())
            translated_lines.append(translated + '\n')
            translated_count += 1
            print(f"Line {i+1}: '{line.strip()}' → '{translated}'")
        else:
            translated_lines.append(line)
    
    # Save sample result
    with open(output_path, 'w', encoding='utf-8') as f:
        f.writelines(translated_lines)
    
    print(f"\nSample translation complete!")
    print(f"  Lines processed: 100")
    print(f"  Lines translated: {translated_count}")
    print(f"  Sample saved to: {output_path}")
    print(f"\nTo perform actual translation with DeepL API, use:")
    print(f"  python scripts/translate_all_text.py")

if __name__ == "__main__":
    create_sample_translation()