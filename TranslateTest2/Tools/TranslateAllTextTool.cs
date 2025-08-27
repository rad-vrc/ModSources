using System;
using System.IO;
using System.Threading.Tasks;
using TranslateTest2.Core;
using TranslateTest2.Config;

namespace TranslateTest2.Tools
{
    /// <summary>
    /// Command-line tool for translating the ALL_TEXT_ja.txt file
    /// Usage: Run from TranslateTest2 project directory
    /// </summary>
    public static class TranslateAllTextTool
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("=== TranslateTest2 Large Text Translation Tool ===");
            Console.WriteLine();
            
            // Setup paths
            string projectRoot = Directory.GetCurrentDirectory();
            string inputPath = Path.Combine(projectRoot, "..", "References", "ALL_TEXT_ja.txt");
            string outputPath = Path.Combine(projectRoot, "..", "References", "ALL_TEXT_ja_translated.txt");
            
            // Check if input file exists
            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"ERROR: Input file not found: {inputPath}");
                Console.WriteLine("Please ensure you're running from the correct directory.");
                return;
            }
            
            // Initialize DeepL configuration
            Console.WriteLine("Please enter your DeepL API key (or press Enter to skip):");
            string apiKey = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                Console.WriteLine("No API key provided. Translation will not work without a valid DeepL API key.");
                return;
            }
            
            var config = new ClientConfig
            {
                DeepLApiKey = apiKey,
                TargetLang = "JA",
                UseDeepL = true,
                CacheDeepL = true,
                DeepLTimeoutMs = 5000,
                DeepLBatchIntervalMs = 300,
                DeepLBatchMax = 20,
                DeepLRetryCount = 3
            };
            
            DeepLTranslator.ApplyConfig(config);
            
            if (!DeepLTranslator.IsEnabled)
            {
                Console.WriteLine("ERROR: DeepL translator is not enabled. Please check your API key.");
                return;
            }
            
            Console.WriteLine("Configuration applied successfully.");
            Console.WriteLine($"Input file: {inputPath}");
            Console.WriteLine($"Output file: {outputPath}");
            Console.WriteLine();
            
            // Get file info
            var fileInfo = new FileInfo(inputPath);
            Console.WriteLine($"File size: {fileInfo.Length:N0} bytes ({fileInfo.Length / 1024.0 / 1024.0:F2} MB)");
            
            var lines = await File.ReadAllLinesAsync(inputPath);
            Console.WriteLine($"Total lines: {lines.Length:N0}");
            Console.WriteLine();
            
            Console.WriteLine("WARNING: This process may take several hours and consume DeepL API quota.");
            Console.Write("Do you want to proceed? (y/N): ");
            var confirm = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(confirm) || !confirm.Trim().ToLowerInvariant().StartsWith("y"))
            {
                Console.WriteLine("Translation cancelled.");
                return;
            }
            
            Console.WriteLine();
            Console.WriteLine("Starting translation process...");
            Console.WriteLine();
            
            // Track progress
            var startTime = DateTime.Now;
            int lastReportedBatch = 0;
            
            try
            {
                await LargeTextTranslator.TranslateFileAsync(inputPath, outputPath, (processed, total) =>
                {
                    if (processed > lastReportedBatch)
                    {
                        var elapsed = DateTime.Now - startTime;
                        var percent = (double)processed / total * 100;
                        var eta = processed > 0 ? TimeSpan.FromTicks((long)(elapsed.Ticks / (double)processed * (total - processed))) : TimeSpan.Zero;
                        
                        Console.WriteLine($"Progress: {processed:N0}/{total:N0} batches ({percent:F1}%) | " +
                                        $"Elapsed: {elapsed:mm\\:ss} | ETA: {eta:mm\\:ss}");
                        lastReportedBatch = processed;
                    }
                });
                
                var totalTime = DateTime.Now - startTime;
                Console.WriteLine();
                Console.WriteLine("=== Translation Complete ===");
                Console.WriteLine($"Total time: {totalTime:hh\\:mm\\:ss}");
                Console.WriteLine($"Output saved to: {outputPath}");
                
                // Show file comparison
                var outputInfo = new FileInfo(outputPath);
                Console.WriteLine($"Original size: {fileInfo.Length:N0} bytes");
                Console.WriteLine($"Translated size: {outputInfo.Length:N0} bytes");
                
                var translatedLines = await File.ReadAllLinesAsync(outputPath);
                int japaneseLines = 0;
                foreach (var line in translatedLines)
                {
                    if (ContainsJapanese(line))
                        japaneseLines++;
                }
                
                Console.WriteLine($"Lines containing Japanese characters: {japaneseLines:N0}/{translatedLines.Length:N0} ({(double)japaneseLines / translatedLines.Length * 100:F1}%)");
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine($"ERROR: Translation failed: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                
                // Check if partial progress was saved
                string progressFile = outputPath + ".progress";
                if (File.Exists(progressFile))
                {
                    Console.WriteLine($"Partial progress was saved to: {progressFile}");
                    Console.WriteLine("You can rename this file to continue from where it left off.");
                }
            }
            
            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
        
        private static bool ContainsJapanese(string text)
        {
            if (string.IsNullOrEmpty(text))
                return false;
            
            foreach (char c in text)
            {
                if ((c >= '\u3040' && c <= '\u309F') || // Hiragana
                    (c >= '\u30A0' && c <= '\u30FF') || // Katakana
                    (c >= '\u4E00' && c <= '\u9FFF'))   // CJK Unified Ideographs
                {
                    return true;
                }
            }
            return false;
        }
    }
}