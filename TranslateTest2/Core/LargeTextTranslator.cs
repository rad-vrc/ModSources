using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslateTest2.Config;

namespace TranslateTest2.Core
{
    /// <summary>
    /// Efficient translator for large text files like ALL_TEXT_ja.txt
    /// Uses batch processing and smart filtering for maximum efficiency
    /// </summary>
    public static class LargeTextTranslator
    {
        private static readonly Dictionary<string, string> _cache = new();
        private const int BATCH_SIZE = 50; // DeepL batch size limit
        private const int MAX_LINE_LENGTH = 4000; // DeepL text length limit
        private const int PROGRESS_INTERVAL = 100; // Report progress every N lines
        
        /// <summary>
        /// Translates a large text file from English to Japanese efficiently
        /// </summary>
        /// <param name="inputPath">Path to source file</param>
        /// <param name="outputPath">Path to output file</param>
        /// <param name="progressCallback">Optional progress callback</param>
        public static async Task TranslateFileAsync(string inputPath, string outputPath, Action<int, int> progressCallback = null)
        {
            if (!File.Exists(inputPath))
                throw new FileNotFoundException($"Input file not found: {inputPath}");
            
            // Read all lines
            var lines = await File.ReadAllLinesAsync(inputPath, Encoding.UTF8);
            var totalLines = lines.Length;
            
            // Create output directory if needed
            var outputDir = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
                Directory.CreateDirectory(outputDir);
            
            // Filter lines that need translation
            var translationBatches = new List<List<(int index, string text)>>();
            var currentBatch = new List<(int, string)>();
            
            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                
                // Skip empty lines, numbers-only, or already Japanese text
                if (ShouldTranslateLine(line))
                {
                    // Split very long lines
                    var chunks = SplitLongLine(line);
                    foreach (var chunk in chunks)
                    {
                        currentBatch.Add((i, chunk));
                        
                        if (currentBatch.Count >= BATCH_SIZE)
                        {
                            translationBatches.Add(currentBatch);
                            currentBatch = new List<(int, string)>();
                        }
                    }
                }
            }
            
            // Add remaining batch
            if (currentBatch.Count > 0)
                translationBatches.Add(currentBatch);
            
            // Process batches
            var translatedLines = new string[totalLines];
            Array.Copy(lines, translatedLines, totalLines); // Start with original lines
            
            int processedBatches = 0;
            foreach (var batch in translationBatches)
            {
                try
                {
                    await ProcessBatch(batch, translatedLines);
                    processedBatches++;
                    
                    if (processedBatches % PROGRESS_INTERVAL == 0 || processedBatches == translationBatches.Count)
                    {
                        progressCallback?.Invoke(processedBatches, translationBatches.Count);
                        
                        // Save intermediate progress
                        await SaveProgressAsync(outputPath + ".progress", translatedLines);
                    }
                    
                    // Brief delay between batches to respect rate limits
                    await Task.Delay(200);
                }
                catch (Exception ex)
                {
                    // Log error but continue with remaining batches
                    try 
                    { 
                        global::TranslateTest2.TranslateTest2.Instance?.Logger?.Error($"Batch translation error: {ex.Message}"); 
                    } 
                    catch { }
                }
            }
            
            // Save final result
            await File.WriteAllLinesAsync(outputPath, translatedLines, Encoding.UTF8);
            
            // Clean up progress file
            try
            {
                if (File.Exists(outputPath + ".progress"))
                    File.Delete(outputPath + ".progress");
            }
            catch { }
        }
        
        /// <summary>
        /// Determines if a line needs translation based on content analysis
        /// </summary>
        private static bool ShouldTranslateLine(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
                return false;
            
            var trimmed = line.Trim();
            
            // Skip very short lines
            if (trimmed.Length < 3)
                return false;
            
            // Skip lines that are mostly numbers or symbols
            if (IsNumericOrSymbolic(trimmed))
                return false;
            
            // Skip code-like patterns
            if (IsCodeLike(trimmed))
                return false;
            
            // Skip if already contains Japanese characters
            if (ContainsJapanese(trimmed))
                return false;
            
            // Use existing TextLangHelper if available
            try
            {
                return TextLangHelper.NeedsTranslation(trimmed);
            }
            catch
            {
                // Fallback to simple check
                return ContainsAlphabetic(trimmed);
            }
        }
        
        /// <summary>
        /// Splits very long lines into smaller chunks for DeepL processing
        /// </summary>
        private static List<string> SplitLongLine(string line)
        {
            if (line.Length <= MAX_LINE_LENGTH)
                return new List<string> { line };
            
            var chunks = new List<string>();
            var sentences = line.Split(new[] { '. ', '! ', '? ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            
            var currentChunk = new StringBuilder();
            foreach (var sentence in sentences)
            {
                if (currentChunk.Length + sentence.Length + 2 <= MAX_LINE_LENGTH)
                {
                    if (currentChunk.Length > 0)
                        currentChunk.Append(". ");
                    currentChunk.Append(sentence);
                }
                else
                {
                    if (currentChunk.Length > 0)
                    {
                        chunks.Add(currentChunk.ToString());
                        currentChunk.Clear();
                    }
                    
                    // If single sentence is still too long, split by words
                    if (sentence.Length > MAX_LINE_LENGTH)
                    {
                        var words = sentence.Split(' ');
                        var wordChunk = new StringBuilder();
                        
                        foreach (var word in words)
                        {
                            if (wordChunk.Length + word.Length + 1 <= MAX_LINE_LENGTH)
                            {
                                if (wordChunk.Length > 0)
                                    wordChunk.Append(' ');
                                wordChunk.Append(word);
                            }
                            else
                            {
                                if (wordChunk.Length > 0)
                                {
                                    chunks.Add(wordChunk.ToString());
                                    wordChunk.Clear();
                                }
                                wordChunk.Append(word);
                            }
                        }
                        
                        if (wordChunk.Length > 0)
                            currentChunk.Append(wordChunk.ToString());
                    }
                    else
                    {
                        currentChunk.Append(sentence);
                    }
                }
            }
            
            if (currentChunk.Length > 0)
                chunks.Add(currentChunk.ToString());
            
            return chunks;
        }
        
        /// <summary>
        /// Process a batch of translations using DeepL
        /// </summary>
        private static async Task ProcessBatch(List<(int index, string text)> batch, string[] outputLines)
        {
            var textsToTranslate = batch.Select(x => x.text).ToList();
            var uniqueTexts = textsToTranslate.Distinct().ToList();
            
            // Check cache first
            var translations = new Dictionary<string, string>();
            var needsTranslation = new List<string>();
            
            foreach (var text in uniqueTexts)
            {
                if (_cache.TryGetValue(text, out var cached))
                {
                    translations[text] = cached;
                }
                else if (DeepLTranslator.TryGetCached(text, out var deepLCached))
                {
                    translations[text] = deepLCached;
                    _cache[text] = deepLCached;
                }
                else
                {
                    needsTranslation.Add(text);
                }
            }
            
            // Translate uncached texts individually (using public API)
            foreach (var text in needsTranslation)
            {
                try
                {
                    var result = await DeepLTranslator.TranslateAsync(text);
                    if (!string.IsNullOrEmpty(result))
                    {
                        translations[text] = result;
                        _cache[text] = result;
                    }
                    else
                    {
                        translations[text] = text; // Keep original if translation fails
                    }
                    
                    // Brief delay between individual requests
                    await Task.Delay(100);
                }
                catch (Exception ex)
                {
                    try 
                    { 
                        global::TranslateTest2.TranslateTest2.Instance?.Logger?.Warn($"Translation failed for text: {ex.Message}"); 
                    } 
                    catch { }
                    
                    translations[text] = text; // Keep original
                }
            }
            
            // Apply translations to output
            foreach (var (index, text) in batch)
            {
                if (translations.TryGetValue(text, out var translated))
                {
                    outputLines[index] = translated;
                }
            }
        }
        
        /// <summary>
        /// Save intermediate progress to disk
        /// </summary>
        private static async Task SaveProgressAsync(string progressPath, string[] lines)
        {
            try
            {
                await File.WriteAllLinesAsync(progressPath, lines, Encoding.UTF8);
            }
            catch { } // Ignore progress save errors
        }
        
        #region Helper Methods
        
        private static bool IsNumericOrSymbolic(string text)
        {
            var alphaCount = text.Count(char.IsLetter);
            return alphaCount < text.Length * 0.3; // Less than 30% alphabetic
        }
        
        private static bool IsCodeLike(string text)
        {
            return text.Contains("()") || 
                   text.Contains("{}") || 
                   text.Contains("[]") ||
                   text.StartsWith("virtual ") ||
                   text.StartsWith("public ") ||
                   text.StartsWith("private ") ||
                   text.Contains(" = ") && text.Contains(";") ||
                   text.Contains("namespace ") ||
                   text.Contains("class ") ||
                   text.Contains("enum ") ||
                   text.Contains("struct ");
        }
        
        private static bool ContainsJapanese(string text)
        {
            try
            {
                return text.Any(c => TextLangHelper.IsJapaneseChar(c));
            }
            catch
            {
                // Fallback Japanese detection
                return text.Any(c => 
                    (c >= '\u3040' && c <= '\u309F') || // Hiragana
                    (c >= '\u30A0' && c <= '\u30FF') || // Katakana
                    (c >= '\u4E00' && c <= '\u9FFF'));  // CJK
            }
        }
        
        private static bool ContainsAlphabetic(string text)
        {
            return text.Any(c => char.IsLetter(c) && c < 128); // ASCII letters only
        }
        
        #endregion
    }
}