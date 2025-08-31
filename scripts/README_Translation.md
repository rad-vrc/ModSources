# ALL_TEXT_ja.txt Translation Guide

This guide explains how to efficiently translate the large `References/ALL_TEXT_ja.txt` file from English to Japanese using the provided translation tools.

## Overview

The `ALL_TEXT_ja.txt` file contains approximately 10.8 million characters (333,000+ lines) of English text that needs to be translated to Japanese. This guide provides several automated translation methods optimized for maximum efficiency.

## Prerequisites

### 1. DeepL API Key
- Sign up for a DeepL API account at [https://www.deepl.com/pro-api](https://www.deepl.com/pro-api)
- You can use either:
  - **DeepL API Free**: 500,000 characters/month free
  - **DeepL API Pro**: Pay-as-you-use pricing (~$20 per million characters)

### 2. Software Requirements
Choose one of the following based on your preference:

**Option A: Python (Recommended)**
- Python 3.7 or later
- No additional dependencies required (uses built-in libraries)

**Option B: PowerShell**
- Windows PowerShell 5.1+ or PowerShell Core 7+
- .NET Framework 4.7.2+ or .NET Core 3.1+

**Option C: C# Integration**
- .NET SDK 6.0+
- Visual Studio or VS Code (optional)

## Translation Methods

### Method 1: Python Script (Recommended)

**Advantages:**
- Cross-platform (Windows, Linux, macOS)
- No additional dependencies
- Built-in progress tracking and error recovery
- Efficient rate limiting and caching

**Usage:**
```bash
# From project root directory
python scripts/translate_all_text.py
```

**Windows users can also use:**
```cmd
scripts\run_translation.bat
```

### Method 2: PowerShell Script

**Advantages:**
- Native Windows integration
- Real-time progress display
- Built-in content analysis and cost estimation

**Usage:**
```powershell
# From project root directory
.\scripts\TranslateAllText.ps1
```

**With parameters:**
```powershell
.\scripts\TranslateAllText.ps1 -ApiKey "your-api-key" -InputFile "References\ALL_TEXT_ja.txt" -OutputFile "References\ALL_TEXT_ja_translated.txt"
```

### Method 3: C# Integration

**Advantages:**
- Integrates with existing TranslateTest2 infrastructure
- Uses the existing DeepL configuration and caching
- Batch processing optimization

**Setup:**
1. Add the LargeTextTranslator.cs to your TranslateTest2 project
2. Use the TranslateAllTextTool.cs for command-line execution

**Usage:**
```csharp
await LargeTextTranslator.TranslateFileAsync(
    inputPath: @"References\ALL_TEXT_ja.txt",
    outputPath: @"References\ALL_TEXT_ja_translated.txt",
    progressCallback: (processed, total) => 
        Console.WriteLine($"Progress: {processed}/{total}")
);
```

## Translation Process Features

### Smart Content Detection
All translation methods include intelligent content analysis to skip:
- Empty lines and whitespace-only lines
- Code snippets and programming constructs
- Lines already containing Japanese characters
- Line numbers and structural elements
- Comments and documentation markup

### Efficiency Optimizations
- **Caching**: Previously translated text is cached to avoid redundant API calls
- **Batch Processing**: Multiple texts processed together when possible
- **Rate Limiting**: Built-in delays to respect API rate limits
- **Progress Tracking**: Real-time progress updates and intermediate saves
- **Error Recovery**: Continues processing even if individual translations fail

### Progress Saving
- Progress is automatically saved every 1000 lines to `.progress` files
- If interrupted, you can resume from the progress file
- Final result overwrites the progress file upon completion

## Cost Estimation

Based on content analysis of the first 10,000 lines:

- **Total Characters**: ~10.8 million
- **Estimated Translatable Content**: ~30-40% of total
- **Estimated API Calls**: ~100,000-150,000
- **Estimated Cost**: $60-100 USD (DeepL Pro pricing)

**Note**: Actual costs will be lower due to:
- Content filtering (skips code, empty lines, etc.)
- Caching (repeated text translated once)
- Smart batching (reduces API overhead)

## Time Estimation

- **Translation Speed**: ~200-500 lines per minute (including delays)
- **Total Estimated Time**: 8-24 hours (depending on API performance and content complexity)

## Monitoring Progress

All methods provide real-time progress updates showing:
- Lines processed vs. total lines
- Number of successful translations
- Elapsed time and estimated time remaining
- API calls made and cache hits

Example output:
```
Progress: 15,000/333,250 (4.5%) | Translated: 4,200 | Elapsed: 45:30 | ETA: 12:30:00
```

## Output Files

- **Primary Output**: `References/ALL_TEXT_ja_translated.txt`
- **Progress File**: `References/ALL_TEXT_ja_translated.txt.progress` (deleted on completion)
- **Backup**: Original file remains unchanged

## Quality Assurance

After translation completion, the tools will display:
- Total lines processed and translated
- Percentage of lines containing Japanese characters
- File size comparison (original vs. translated)

## Troubleshooting

### Common Issues

1. **API Key Errors**
   - Verify your API key is correct
   - Check your DeepL account quota and billing status

2. **Rate Limiting**
   - Built-in delays should prevent this
   - If it occurs, the tools will automatically retry

3. **Network Timeouts**
   - Tools automatically retry failed requests
   - Increase timeout values if needed

4. **File Path Issues**
   - Ensure you're running from the project root directory
   - Use forward slashes (/) or escaped backslashes (\\\\) in paths

### Recovery from Interruption

If the translation process is interrupted:

1. Look for the `.progress` file in the same directory as the output
2. Rename it to the desired output filename to continue from that point
3. Or restart the process (it will skip already-translated content due to caching)

## Best Practices

1. **Test First**: Run the translation on a smaller sample file to verify everything works
2. **Monitor Costs**: Check your DeepL usage regularly during the process
3. **Backup**: Keep backups of both original and translated files
4. **Validate Results**: Spot-check translated content for quality
5. **Schedule Appropriately**: Plan for long running times (8-24 hours)

## Support

If you encounter issues:
1. Check the console output for specific error messages
2. Verify your API key and account status
3. Ensure you have sufficient DeepL API quota
4. Check network connectivity and firewall settings

For technical issues with the translation tools themselves, refer to the source code in:
- `TranslateTest2/Core/LargeTextTranslator.cs`
- `scripts/translate_all_text.py`
- `scripts/TranslateAllText.ps1`