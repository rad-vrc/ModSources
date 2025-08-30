# PowerShell script to translate ALL_TEXT_ja.txt using TranslateTest2 infrastructure
# Run this from the project root directory

param(
    [string]$ApiKey = "",
    [string]$InputFile = "References\ALL_TEXT_ja.txt",
    [string]$OutputFile = "References\ALL_TEXT_ja_translated.txt"
)

Write-Host "=== TranslateTest2 Large Text Translation Script ===" -ForegroundColor Cyan
Write-Host

# Check if input file exists
if (-not (Test-Path $InputFile)) {
    Write-Host "ERROR: Input file not found: $InputFile" -ForegroundColor Red
    Write-Host "Please ensure you're running from the project root directory."
    exit 1
}

# Get API key if not provided
if ([string]::IsNullOrWhiteSpace($ApiKey)) {
    $ApiKey = Read-Host "Please enter your DeepL API key (or press Enter to skip)"
    if ([string]::IsNullOrWhiteSpace($ApiKey)) {
        Write-Host "No API key provided. Translation cannot proceed." -ForegroundColor Red
        exit 1
    }
}

# Get file info
$fileInfo = Get-Item $InputFile
$fileSizeMB = [math]::Round($fileInfo.Length / 1024 / 1024, 2)
$lines = Get-Content $InputFile
$lineCount = $lines.Count

Write-Host "File Information:" -ForegroundColor Yellow
Write-Host "  Input file: $InputFile"
Write-Host "  Output file: $OutputFile"
Write-Host "  File size: $($fileInfo.Length.ToString('N0')) bytes ($fileSizeMB MB)"
Write-Host "  Total lines: $($lineCount.ToString('N0'))"
Write-Host

# Analyze content to estimate translation workload
$translateableLines = 0
$alreadyJapanese = 0
$codeLines = 0
$emptyLines = 0

Write-Host "Analyzing content..." -ForegroundColor Yellow

for ($i = 0; $i -lt [Math]::Min($lines.Count, 10000); $i++) {
    $line = $lines[$i].Trim()
    
    if ([string]::IsNullOrWhiteSpace($line)) {
        $emptyLines++
    }
    elseif ($line -match '[\u3040-\u309F\u30A0-\u30FF\u4E00-\u9FFF]') {
        $alreadyJapanese++
    }
    elseif ($line -match '^(virtual |public |private |class |enum |struct |namespace )' -or 
            $line.Contains('()') -or $line.Contains('{}') -or $line.Contains('[]')) {
        $codeLines++
    }
    elseif ($line -match '[a-zA-Z]' -and $line.Length -ge 3) {
        $translateableLines++
    }
}

# Estimate based on sample
$sampleSize = [Math]::Min($lines.Count, 10000)
$estimatedTranslateable = [Math]::Round($translateableLines * $lines.Count / $sampleSize)

Write-Host "Content Analysis (based on first $sampleSize lines):" -ForegroundColor Green
Write-Host "  Empty lines: ~$($emptyLines * $lines.Count / $sampleSize)"
Write-Host "  Already Japanese: ~$($alreadyJapanese * $lines.Count / $sampleSize)"
Write-Host "  Code-like lines: ~$($codeLines * $lines.Count / $sampleSize)"
Write-Host "  Translatable lines: ~$estimatedTranslateable"
Write-Host

# Cost estimation
$estimatedChars = $estimatedTranslateable * 50  # Average 50 chars per line
$estimatedCostUSD = [Math]::Round($estimatedChars * 20 / 1000000, 2)  # $20 per 1M chars for DeepL

Write-Host "Estimated Translation Cost:" -ForegroundColor Magenta
Write-Host "  Characters to translate: ~$($estimatedChars.ToString('N0'))"
Write-Host "  Estimated DeepL cost: ~`$$estimatedCostUSD USD"
Write-Host

Write-Host "WARNING: This process may take several hours and consume DeepL API quota." -ForegroundColor Red
$confirm = Read-Host "Do you want to proceed? (y/N)"

if ($confirm -notmatch '^[Yy]') {
    Write-Host "Translation cancelled." -ForegroundColor Yellow
    exit 0
}

Write-Host
Write-Host "Starting translation process..." -ForegroundColor Green
Write-Host "Progress will be saved incrementally to: $OutputFile.progress"
Write-Host

# Create the C# translation code inline
$translatorCode = @'
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

public class SimpleTranslator
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly Dictionary<string, string> _cache = new();
    
    public SimpleTranslator(string apiKey)
    {
        _apiKey = apiKey;
        _httpClient = new HttpClient();
        _httpClient.Timeout = TimeSpan.FromSeconds(30);
    }
    
    public async Task<string> TranslateText(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return text;
        if (_cache.TryGetValue(text, out var cached)) return cached;
        
        try
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("auth_key", _apiKey),
                new KeyValuePair<string, string>("text", text),
                new KeyValuePair<string, string>("target_lang", "JA"),
                new KeyValuePair<string, string>("source_lang", "EN")
            });
            
            var response = await _httpClient.PostAsync("https://api-free.deepl.com/v2/translate", content);
            
            if (!response.IsSuccessStatusCode)
            {
                // Try paid endpoint
                response = await _httpClient.PostAsync("https://api.deepl.com/v2/translate", content);
            }
            
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);
                
                if (doc.RootElement.TryGetProperty("translations", out var translations) && 
                    translations.GetArrayLength() > 0)
                {
                    var translated = translations[0].GetProperty("text").GetString();
                    _cache[text] = translated;
                    return translated;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Translation error: {ex.Message}");
        }
        
        return text; // Return original if translation fails
    }
    
    public bool ShouldTranslate(string line)
    {
        if (string.IsNullOrWhiteSpace(line)) return false;
        var trimmed = line.Trim();
        
        // Skip short lines
        if (trimmed.Length < 3) return false;
        
        // Skip if already contains Japanese
        if (Regex.IsMatch(trimmed, @"[\u3040-\u309F\u30A0-\u30FF\u4E00-\u9FFF]")) return false;
        
        // Skip code-like patterns
        if (Regex.IsMatch(trimmed, @"^(virtual |public |private |class |enum |struct |namespace )") ||
            trimmed.Contains("()") || trimmed.Contains("{}") || trimmed.Contains("[]")) return false;
        
        // Must contain English letters
        return Regex.IsMatch(trimmed, @"[a-zA-Z]");
    }
    
    public void Dispose()
    {
        _httpClient?.Dispose();
    }
}
'@

# Compile and run the translator
Add-Type -TypeDefinition $translatorCode -ReferencedAssemblies System.Net.Http,System.Text.Json

$translator = New-Object SimpleTranslator($ApiKey)
$startTime = Get-Date
$processedLines = 0
$translatedCount = 0

try {
    $outputLines = @()
    $outputLines += $lines
    
    Write-Host "Processing $($lines.Count) lines..."
    
    for ($i = 0; $i -lt $lines.Count; $i++) {
        $line = $lines[$i]
        
        if ($translator.ShouldTranslate($line)) {
            try {
                $translated = $translator.TranslateText($line).GetAwaiter().GetResult()
                if ($translated -ne $line) {
                    $outputLines[$i] = $translated
                    $translatedCount++
                }
            }
            catch {
                Write-Host "Translation failed for line $($i + 1): $_" -ForegroundColor Red
            }
            
            # Brief delay to respect rate limits
            Start-Sleep -Milliseconds 200
        }
        
        $processedLines++
        
        # Progress report every 100 lines
        if ($processedLines % 100 -eq 0) {
            $elapsed = (Get-Date) - $startTime
            $percent = [Math]::Round($processedLines * 100.0 / $lines.Count, 1)
            $eta = if ($processedLines -gt 0) { 
                [TimeSpan]::FromTicks($elapsed.Ticks * ($lines.Count - $processedLines) / $processedLines)
            } else { 
                [TimeSpan]::Zero 
            }
            
            Write-Host "Progress: $processedLines/$($lines.Count) ($percent%) | Translated: $translatedCount | Elapsed: $($elapsed.ToString('mm\:ss')) | ETA: $($eta.ToString('mm\:ss'))"
            
            # Save progress every 1000 lines
            if ($processedLines % 1000 -eq 0) {
                $outputLines | Out-File -FilePath "$OutputFile.progress" -Encoding utf8
            }
        }
    }
    
    # Save final result
    $outputLines | Out-File -FilePath $OutputFile -Encoding utf8
    
    $totalTime = (Get-Date) - $startTime
    Write-Host
    Write-Host "=== Translation Complete ===" -ForegroundColor Green
    Write-Host "Total time: $($totalTime.ToString('hh\:mm\:ss'))"
    Write-Host "Lines processed: $($processedLines.ToString('N0'))"
    Write-Host "Lines translated: $($translatedCount.ToString('N0'))"
    Write-Host "Output saved to: $OutputFile"
    
    # Clean up progress file
    if (Test-Path "$OutputFile.progress") {
        Remove-Item "$OutputFile.progress"
    }
}
catch {
    Write-Host
    Write-Host "ERROR: Translation failed: $_" -ForegroundColor Red
    Write-Host "Partial progress may be saved in: $OutputFile.progress"
}
finally {
    $translator.Dispose()
}

Write-Host
Write-Host "Press any key to exit..."
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")