#!/usr/bin/env python3
"""
Test script to analyze ALL_TEXT_ja.txt content and estimate translation workload
"""

import os
import re
from pathlib import Path

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
        r'^\s*\d+\.\s*$',  # Line numbers only
        r'^[\s\|]+$'  # Only whitespace and pipes
    ]
    
    for pattern in code_patterns:
        if re.search(pattern, trimmed):
            return False
    
    # Must contain English letters
    return bool(re.search(r'[a-zA-Z]', trimmed))

def analyze_file(input_path: str):
    """Analyze file content and provide translation statistics"""
    print(f"=== Analysis of {input_path} ===\n")
    
    if not os.path.exists(input_path):
        print(f"ERROR: File not found: {input_path}")
        return
    
    # File size info
    file_size = os.path.getsize(input_path)
    print(f"File size: {file_size:,} bytes ({file_size/1024/1024:.2f} MB)")
    
    # Read and analyze lines
    with open(input_path, 'r', encoding='utf-8', errors='ignore') as f:
        lines = f.readlines()
    
    total_lines = len(lines)
    print(f"Total lines: {total_lines:,}")
    
    # Analyze first 10,000 lines for estimation
    sample_size = min(10000, total_lines)
    print(f"Analyzing first {sample_size:,} lines for estimation...\n")
    
    stats = {
        'empty_lines': 0,
        'japanese_lines': 0,
        'code_lines': 0,
        'translatable_lines': 0,
        'other_lines': 0,
        'total_chars_translatable': 0
    }
    
    for i, line in enumerate(lines[:sample_size]):
        trimmed = line.strip()
        
        if not trimmed:
            stats['empty_lines'] += 1
        elif re.search(r'[\u3040-\u309F\u30A0-\u30FF\u4E00-\u9FFF]', trimmed):
            stats['japanese_lines'] += 1
        elif should_translate(line):
            stats['translatable_lines'] += 1
            stats['total_chars_translatable'] += len(trimmed)
        else:
            # Check if it's code-like
            code_patterns = [
                r'^(virtual |public |private |class |enum |struct |namespace )',
                r'\(\)|\{\}|\[\]',
                r'^using\s+\w+',
                r'^\s*//.*',
            ]
            
            is_code = any(re.search(pattern, trimmed) for pattern in code_patterns)
            if is_code:
                stats['code_lines'] += 1
            else:
                stats['other_lines'] += 1
    
    # Scale up estimates based on sample
    scale_factor = total_lines / sample_size
    
    print("Content Analysis:")
    print(f"  Empty lines: {stats['empty_lines']:,} (~{int(stats['empty_lines'] * scale_factor):,} total)")
    print(f"  Japanese lines: {stats['japanese_lines']:,} (~{int(stats['japanese_lines'] * scale_factor):,} total)")
    print(f"  Code lines: {stats['code_lines']:,} (~{int(stats['code_lines'] * scale_factor):,} total)")
    print(f"  Translatable lines: {stats['translatable_lines']:,} (~{int(stats['translatable_lines'] * scale_factor):,} total)")
    print(f"  Other lines: {stats['other_lines']:,} (~{int(stats['other_lines'] * scale_factor):,} total)")
    print()
    
    # Translation estimates
    estimated_translatable = int(stats['translatable_lines'] * scale_factor)
    estimated_chars = int(stats['total_chars_translatable'] * scale_factor)
    
    print("Translation Estimates:")
    print(f"  Lines to translate: ~{estimated_translatable:,}")
    print(f"  Characters to translate: ~{estimated_chars:,}")
    print(f"  Estimated DeepL cost: ~${estimated_chars * 20 / 1000000:.2f} USD")
    print(f"  Estimated time (at 300 lines/hour): ~{estimated_translatable / 300:.1f} hours")
    print()
    
    # Show some sample translatable lines
    print("Sample translatable lines:")
    sample_count = 0
    for i, line in enumerate(lines[:sample_size]):
        if should_translate(line) and sample_count < 5:
            print(f"  Line {i+1}: {line.strip()}")
            sample_count += 1
    
    print("\nSample code/skipped lines:")
    sample_count = 0
    for i, line in enumerate(lines[:sample_size]):
        if not should_translate(line) and line.strip() and sample_count < 5:
            print(f"  Line {i+1}: {line.strip()}")
            sample_count += 1

if __name__ == "__main__":
    input_path = "References/ALL_TEXT_ja.txt"
    analyze_file(input_path)