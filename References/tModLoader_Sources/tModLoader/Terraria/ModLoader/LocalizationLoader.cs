using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Hjson;
using log4net;
using Newtonsoft.Json.Linq;
using Terraria.Localization;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.Utilities;

namespace Terraria.ModLoader
{
	// Token: 0x02000191 RID: 401
	public static class LocalizationLoader
	{
		// Token: 0x06001EF8 RID: 7928 RVA: 0x004DD530 File Offset: 0x004DB730
		internal static void Autoload(Mod mod)
		{
			LanguageManager lang = LanguageManager.Instance;
			string gameTipPrefix = "Mods." + mod.Name + ".GameTips.";
			foreach (ValueTuple<string, string> valueTuple in LocalizationLoader.LoadTranslations(mod, GameCulture.DefaultCulture))
			{
				string key = valueTuple.Item1;
				LocalizedText text = lang.GetOrRegister(key, null);
				if (key.StartsWith(gameTipPrefix))
				{
					Main.gameTips.allTips.Add(new GameTipData(text, mod));
				}
			}
		}

		// Token: 0x06001EF9 RID: 7929 RVA: 0x004DD5CC File Offset: 0x004DB7CC
		public static void LoadModTranslations(GameCulture culture)
		{
			LanguageManager lang = LanguageManager.Instance;
			Mod[] mods = ModLoader.Mods;
			for (int i = 0; i < mods.Length; i++)
			{
				foreach (ValueTuple<string, string> valueTuple in LocalizationLoader.LoadTranslations(mods[i], culture))
				{
					string key = valueTuple.Item1;
					string value = valueTuple.Item2;
					lang.GetText(key).SetValue(value);
				}
			}
		}

		// Token: 0x06001EFA RID: 7930 RVA: 0x004DD654 File Offset: 0x004DB854
		internal static void UpgradeLangFile(string langFile, string modName)
		{
			string[] array = File.ReadAllLines(langFile, Encoding.UTF8);
			JObject modObject = new JObject();
			JObject jobject = new JObject();
			jobject.Add(modName, modObject);
			JObject modsObject = jobject;
			JObject jobject2 = new JObject();
			jobject2.Add("Mods", modsObject);
			JObject rootObject = jobject2;
			foreach (string line in array)
			{
				if (!line.Trim().StartsWith("#"))
				{
					int split = line.IndexOf('=');
					if (split >= 0)
					{
						string key = line.Substring(0, split).Trim().Replace(" ", "_");
						string value = line.Substring(split + 1);
						if (value.Length != 0)
						{
							value = value.Replace("\\n", "\n");
							string[] splitKey = key.Split(".", StringSplitOptions.None);
							JObject curObj = modObject;
							foreach (string i in splitKey.SkipLast(1))
							{
								if (!curObj.ContainsKey(i))
								{
									curObj.Add(i, new JObject());
								}
								JToken existingVal = curObj.GetValue(i);
								if (existingVal.Type == 1)
								{
									curObj = (JObject)existingVal;
								}
								else
								{
									curObj[i] = new JObject();
									curObj = (JObject)curObj.GetValue(i);
									curObj["$parentVal"] = existingVal;
								}
							}
							string lastKey = splitKey.Last<string>();
							if (curObj.ContainsKey(splitKey.Last<string>()) && curObj[lastKey] is JObject)
							{
								((JObject)curObj[lastKey]).Add("$parentValue", value);
							}
							curObj.Add(splitKey.Last<string>(), value);
						}
					}
				}
			}
			string path = Path.ChangeExtension(langFile, "hjson");
			string hjsonContents = JsonValue.Parse(rootObject.ToString()).ToFancyHjsonString(null);
			File.WriteAllText(path, hjsonContents);
			File.Move(langFile, langFile + ".legacy", true);
		}

		// Token: 0x06001EFB RID: 7931 RVA: 0x004DD87C File Offset: 0x004DBA7C
		[Obsolete("Use $TryGetCultureAndPrefixFromPath instead.", true)]
		[return: TupleElementNames(new string[]
		{
			"culture",
			"prefix"
		})]
		public static ValueTuple<GameCulture, string> GetCultureAndPrefixFromPath(string path)
		{
			GameCulture culture;
			string prefix;
			if (LocalizationLoader.TryGetCultureAndPrefixFromPath(path, out culture, out prefix))
			{
				return new ValueTuple<GameCulture, string>(culture, prefix);
			}
			return new ValueTuple<GameCulture, string>(GameCulture.DefaultCulture, string.Empty);
		}

		/// <summary>
		/// Derives a culture and shared prefix from a localization file path. Prefix will be found after culture, either separated by an underscore or nested in the folder.
		/// <br /> Some examples:<code>
		/// Localization/en-US_Mods.ExampleMod.hjson
		/// Localization/en-US/Mods.ExampleMod.hjson
		/// en-US_Mods.ExampleMod.hjson
		/// en-US/Mods.ExampleMod.hjson
		/// </code>
		/// </summary>
		/// <param name="path"></param>
		/// <param name="culture"></param>
		/// <param name="prefix"></param>
		/// <returns></returns>
		// Token: 0x06001EFC RID: 7932 RVA: 0x004DD8AC File Offset: 0x004DBAAC
		[NullableContext(2)]
		public static bool TryGetCultureAndPrefixFromPath([Nullable(1)] string path, [NotNullWhen(true)] out GameCulture culture, [NotNullWhen(true)] out string prefix)
		{
			path = Path.ChangeExtension(path, null);
			path = path.Replace("\\", "/");
			culture2 = null;
			prefix = null;
			string[] array = path.Split("/", StringSplitOptions.None);
			for (int i = 0; i < array.Length; i++)
			{
				string[] splitByUnderscore = array[i].Split("_", StringSplitOptions.None);
				for (int underscoreSplitIndex = 0; underscoreSplitIndex < splitByUnderscore.Length; underscoreSplitIndex++)
				{
					string underscorePart = splitByUnderscore[underscoreSplitIndex];
					GameCulture parsedCulture = GameCulture.KnownCultures.FirstOrDefault((GameCulture culture) => culture.Name == underscorePart);
					if (parsedCulture != null)
					{
						culture2 = parsedCulture;
					}
					else if (parsedCulture == null && culture2 != null)
					{
						prefix = string.Join("_", splitByUnderscore.Skip(underscoreSplitIndex));
						return true;
					}
				}
			}
			if (culture2 != null)
			{
				prefix = string.Empty;
				return true;
			}
			return false;
		}

		// Token: 0x06001EFD RID: 7933 RVA: 0x004DD974 File Offset: 0x004DBB74
		[return: TupleElementNames(new string[]
		{
			"key",
			"value"
		})]
		private static List<ValueTuple<string, string>> LoadTranslations(Mod mod, GameCulture culture)
		{
			if (mod.File == null)
			{
				return new List<ValueTuple<string, string>>();
			}
			List<ValueTuple<string, string>> result;
			try
			{
				List<ValueTuple<string, string>> flattened = new List<ValueTuple<string, string>>();
				foreach (TmodFile.FileEntry translationFile in from entry in mod.File
				where Path.GetExtension(entry.Name) == ".hjson"
				select entry)
				{
					GameCulture fileCulture;
					string prefix;
					if (LocalizationLoader.TryGetCultureAndPrefixFromPath(translationFile.Name, out fileCulture, out prefix) && fileCulture == culture)
					{
						using (Stream stream = mod.File.GetStream(translationFile, false))
						{
							using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8, true))
							{
								string translationFileContents = streamReader.ReadToEnd();
								string modpath = Path.Combine(mod.Name, translationFile.Name).Replace('/', '\\');
								if (LocalizationLoader.changedFiles.Select(([TupleElementNames(new string[]
								{
									"Mod",
									"fileName"
								})] ValueTuple<string, string> x) => Path.Join(x.Item1, x.Item2)).Contains(modpath))
								{
									string path = Path.Combine(mod.SourceFolder, translationFile.Name);
									if (File.Exists(path))
									{
										try
										{
											translationFileContents = File.ReadAllText(path);
										}
										catch (Exception)
										{
										}
									}
								}
								string jsonString;
								try
								{
									jsonString = HjsonValue.Parse(translationFileContents).ToString();
								}
								catch (Exception e)
								{
									string additionalContext = "";
									DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
									if (e is ArgumentException)
									{
										Match match = Regex.Match(e.Message, "At line (\\d+),");
										int line;
										if (match != null && match.Success && int.TryParse(match.Groups[1].Value, out line))
										{
											string[] lines = translationFileContents.Replace("\r", "").Replace("\t", "    ").Split('\n', StringSplitOptions.None);
											int num = Math.Max(0, line - 4);
											int end = Math.Min(lines.Length, line + 3);
											StringBuilder linesOutput = new StringBuilder();
											for (int i = num; i < end; i++)
											{
												if (line - 1 == i)
												{
													StringBuilder stringBuilder = linesOutput;
													defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(12, 1);
													defaultInterpolatedStringHandler.AppendLiteral("\n");
													defaultInterpolatedStringHandler.AppendFormatted<int>(i + 1);
													defaultInterpolatedStringHandler.AppendLiteral("[c/ff0000:>");
													stringBuilder.Append(defaultInterpolatedStringHandler.ToStringAndClear() + lines[i] + "]");
												}
												else
												{
													StringBuilder stringBuilder2 = linesOutput;
													defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 1);
													defaultInterpolatedStringHandler.AppendLiteral("\n");
													defaultInterpolatedStringHandler.AppendFormatted<int>(i + 1);
													defaultInterpolatedStringHandler.AppendLiteral(":");
													stringBuilder2.Append(defaultInterpolatedStringHandler.ToStringAndClear() + lines[i]);
												}
											}
											additionalContext = "\nContext:" + linesOutput.ToString();
										}
									}
									defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(58, 2);
									defaultInterpolatedStringHandler.AppendLiteral("The localization file \"");
									defaultInterpolatedStringHandler.AppendFormatted(translationFile.Name);
									defaultInterpolatedStringHandler.AppendLiteral("\" is malformed and failed to load:");
									defaultInterpolatedStringHandler.AppendFormatted(additionalContext);
									defaultInterpolatedStringHandler.AppendLiteral(" ");
									throw new Exception(defaultInterpolatedStringHandler.ToStringAndClear(), e);
								}
								foreach (JToken t in JObject.Parse(jsonString).SelectTokens("$..*"))
								{
									if (!t.HasValues)
									{
										JObject obj = t as JObject;
										if (obj == null || obj.Count != 0)
										{
											string path2 = "";
											JToken current = t;
											for (JToken parent = t.Parent; parent != null; parent = parent.Parent)
											{
												JProperty property = parent as JProperty;
												string text;
												if (property == null)
												{
													JArray array = parent as JArray;
													if (array == null)
													{
														text = path2;
													}
													else
													{
														text = array.IndexOf(current).ToString() + ((path2 == string.Empty) ? string.Empty : ("." + path2));
													}
												}
												else
												{
													text = property.Name + ((path2 == string.Empty) ? string.Empty : ("." + path2));
												}
												path2 = text;
												current = parent;
											}
											path2 = path2.Replace(".$parentVal", "");
											if (!string.IsNullOrWhiteSpace(prefix))
											{
												path2 = prefix + "." + path2;
											}
											flattened.Add(new ValueTuple<string, string>(path2, t.ToString()));
										}
									}
								}
							}
						}
					}
				}
				result = flattened;
			}
			catch (Exception ex)
			{
				ex.Data["mod"] = mod.Name;
				throw;
			}
			return result;
		}

		// Token: 0x06001EFE RID: 7934 RVA: 0x004DDEBC File Offset: 0x004DC0BC
		internal static void FinishSetup()
		{
			LocalizationLoader.UpdateLocalizationFiles();
			LocalizationLoader.SetupFileWatchers();
		}

		// Token: 0x06001EFF RID: 7935 RVA: 0x004DDEC8 File Offset: 0x004DC0C8
		internal static void UpdateLocalizationFiles()
		{
			foreach (Mod mod in ModLoader.Mods)
			{
				try
				{
					LocalizationLoader.UpdateLocalizationFilesForMod(mod, null, null);
				}
				catch (Exception ex)
				{
					ex.Data["mod"] = mod.Name;
					throw;
				}
			}
		}

		// Token: 0x06001F00 RID: 7936 RVA: 0x004DDF20 File Offset: 0x004DC120
		private static void UpdateLocalizationFilesForMod(Mod mod, string outputPath = null, GameCulture specificCulture = null)
		{
			if (mod.File == null)
			{
				return;
			}
			HashSet<GameCulture> desiredCultures = new HashSet<GameCulture>();
			if (specificCulture != null)
			{
				desiredCultures.Add(specificCulture);
			}
			List<Mod> mods = new List<Mod>
			{
				mod
			};
			string sourceFolder = outputPath ?? mod.SourceFolder;
			if (!Directory.Exists(sourceFolder))
			{
				return;
			}
			string localBuiltTModFile = Path.Combine(ModLoader.ModPath, mod.Name + ".tmod");
			if (outputPath == null && !File.Exists(localBuiltTModFile))
			{
				return;
			}
			DateTime modLastModified = File.GetLastWriteTime(mod.File.path);
			if (mod.TranslationForMods != null)
			{
				foreach (string name2 in mod.TranslationForMods)
				{
					Mod otherMod;
					ModLoader.TryGetMod(name2, out otherMod);
					if (otherMod == null)
					{
						return;
					}
					mods.Add(otherMod);
					DateTime otherModLastModified = File.GetLastWriteTime(otherMod.File.path);
					if (otherModLastModified > modLastModified)
					{
						modLastModified = otherModLastModified;
					}
				}
			}
			Dictionary<GameCulture, List<LocalizationLoader.LocalizationFile>> localizationFilesByCulture = new Dictionary<GameCulture, List<LocalizationLoader.LocalizationFile>>();
			Dictionary<string, string> localizationFileContentsByPath = new Dictionary<string, string>();
			foreach (Mod inputMod in mods)
			{
				foreach (TmodFile.FileEntry translationFile in from entry in inputMod.File
				where Path.GetExtension(entry.Name) == ".hjson"
				select entry)
				{
					GameCulture culture4;
					string prefix;
					if (LocalizationLoader.TryGetCultureAndPrefixFromPath(translationFile.Name, out culture4, out prefix))
					{
						using (Stream stream = inputMod.File.GetStream(translationFile, false))
						{
							using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8, true))
							{
								string translationFileContents = streamReader.ReadToEnd();
								string fixedFileName = translationFile.Name;
								if (culture4 == GameCulture.DefaultCulture && !fixedFileName.Contains("en-US"))
								{
									fixedFileName = Path.Combine(Path.GetDirectoryName(fixedFileName), "en-US.hjson").Replace("\\", "/");
								}
								List<LocalizationLoader.LocalizationFile> fileList;
								if (!localizationFilesByCulture.TryGetValue(culture4, out fileList))
								{
									fileList = (localizationFilesByCulture[culture4] = new List<LocalizationLoader.LocalizationFile>());
								}
								if (inputMod == mod)
								{
									desiredCultures.Add(culture4);
									if (!localizationFileContentsByPath.ContainsKey(translationFile.Name))
									{
										localizationFileContentsByPath[translationFile.Name] = translationFileContents;
									}
								}
								JsonValue jsonValueEng;
								try
								{
									jsonValueEng = HjsonValue.Parse(translationFileContents, new HjsonOptions
									{
										KeepWsc = true
									});
								}
								catch (Exception e)
								{
									throw new Exception("The localization file \"" + translationFile.Name + "\" is malformed and failed to load: ", e);
								}
								List<LocalizationLoader.LocalizationEntry> entries = LocalizationLoader.ParseLocalizationEntries((WscJsonObject)jsonValueEng, prefix);
								if (!fileList.Any((LocalizationLoader.LocalizationFile x) => x.path == fixedFileName))
								{
									fileList.Add(new LocalizationLoader.LocalizationFile(fixedFileName, prefix, entries));
								}
								else
								{
									LocalizationLoader.LocalizationFile localizationFile = fileList.First((LocalizationLoader.LocalizationFile x) => x.path == fixedFileName);
									using (List<LocalizationLoader.LocalizationEntry>.Enumerator enumerator4 = entries.GetEnumerator())
									{
										while (enumerator4.MoveNext())
										{
											LocalizationLoader.LocalizationEntry entry = enumerator4.Current;
											if (!localizationFile.Entries.Exists((LocalizationLoader.LocalizationEntry x) => x.key == entry.key))
											{
												localizationFile.Entries.Add(entry);
											}
										}
									}
								}
							}
						}
					}
				}
			}
			List<LocalizationLoader.LocalizationFile> baseLocalizationFiles;
			if (!localizationFilesByCulture.TryGetValue(GameCulture.DefaultCulture, out baseLocalizationFiles))
			{
				localizationFilesByCulture[GameCulture.DefaultCulture] = (baseLocalizationFiles = new List<LocalizationLoader.LocalizationFile>());
				desiredCultures.Add(GameCulture.DefaultCulture);
				string prefix2 = "Mods." + mod.Name;
				string translationFileName = "Localization/en-US_" + prefix2 + ".hjson";
				baseLocalizationFiles.Add(new LocalizationLoader.LocalizationFile(translationFileName, prefix2, new List<LocalizationLoader.LocalizationEntry>()));
			}
			Dictionary<string, List<LocalizationLoader.LocalizationEntry>> duplicates = (from w in baseLocalizationFiles.SelectMany((LocalizationLoader.LocalizationFile f) => f.Entries)
			where w.type == 0
			select w into x
			group x by x.key into c
			where c.Count<LocalizationLoader.LocalizationEntry>() > 1
			select c).ToDictionary((IGrouping<string, LocalizationLoader.LocalizationEntry> g) => g.Key, (IGrouping<string, LocalizationLoader.LocalizationEntry> g) => g.ToList<LocalizationLoader.LocalizationEntry>());
			foreach (LocalizationLoader.LocalizationFile baseLocalizationFile in from x in baseLocalizationFiles
			orderby x.path.Length descending
			select x)
			{
				List<LocalizationLoader.LocalizationEntry> toRemove = new List<LocalizationLoader.LocalizationEntry>();
				foreach (LocalizationLoader.LocalizationEntry entry3 in baseLocalizationFile.Entries)
				{
					if (duplicates.ContainsKey(entry3.key))
					{
						duplicates.Remove(entry3.key);
						toRemove.Add(entry3);
					}
				}
				foreach (LocalizationLoader.LocalizationEntry entry2 in toRemove)
				{
					baseLocalizationFile.Entries.Remove(entry2);
				}
			}
			Exception e;
			HashSet<string> baseLocalizationKeys = baseLocalizationFiles.SelectMany((LocalizationLoader.LocalizationFile f) => from e in f.Entries
			select e.key).ToHashSet<string>();
			foreach (LocalizedText translation in LanguageManager.Instance._localizedTexts.Values)
			{
				if (translation.Key.StartsWith("Mods." + mod.Name + ".") && !baseLocalizationKeys.Contains(translation.Key))
				{
					LocalizationLoader.LocalizationEntry newEntry = new LocalizationLoader.LocalizationEntry(translation.Key, translation.Value, null, 0);
					LocalizationLoader.AddEntryToHJSON(LocalizationLoader.FindHJSONFileForKey(baseLocalizationFiles, newEntry.key), newEntry.key, newEntry.value, null);
				}
			}
			IEnumerable<GameCulture> targetCultures = desiredCultures.ToList<GameCulture>();
			if (specificCulture != null)
			{
				targetCultures = new GameCulture[]
				{
					specificCulture
				};
				List<LocalizationLoader.LocalizationFile> fileList2;
				if (!localizationFilesByCulture.TryGetValue(specificCulture, out fileList2))
				{
					fileList2 = (localizationFilesByCulture[specificCulture] = new List<LocalizationLoader.LocalizationFile>());
				}
			}
			foreach (GameCulture culture2 in targetCultures)
			{
				IEnumerable<LocalizationLoader.LocalizationEntry> enumerable = localizationFilesByCulture[culture2].SelectMany((LocalizationLoader.LocalizationFile f) => f.Entries);
				Dictionary<string, string> localizationsForCulture = new Dictionary<string, string>();
				foreach (LocalizationLoader.LocalizationEntry localizationEntry in enumerable)
				{
					if (localizationEntry.value != null)
					{
						string key = localizationEntry.key;
						if (key.EndsWith(".$parentVal"))
						{
							key = key.Replace(".$parentVal", "");
						}
						localizationsForCulture[key] = localizationEntry.value;
					}
				}
				foreach (LocalizationLoader.LocalizationFile localizationFile2 in baseLocalizationFiles)
				{
					string hjsonContents = LocalizationLoader.LocalizationFileToHjsonText(localizationFile2, localizationsForCulture).ReplaceLineEndings();
					string outputFileName = LocalizationLoader.GetPathForCulture(localizationFile2, culture2);
					string outputFilePath = Path.Combine(sourceFolder, outputFileName);
					DateTime dateTime = File.GetLastWriteTime(outputFilePath);
					string existingFileContents;
					if (!localizationFileContentsByPath.TryGetValue(outputFileName, out existingFileContents) || (existingFileContents.ReplaceLineEndings() != hjsonContents && dateTime < modLastModified) || specificCulture != null)
					{
						Directory.CreateDirectory(Path.GetDirectoryName(outputFilePath));
						File.WriteAllText(outputFilePath, hjsonContents);
						LocalizationLoader.changedMods.Add(mod.Name);
					}
				}
			}
			HashSet<string> outputPathsForAllLangs = localizationFilesByCulture.Keys.SelectMany((GameCulture culture) => from baseFile in baseLocalizationFiles
			select LocalizationLoader.GetPathForCulture(baseFile, culture)).ToHashSet<string>();
			foreach (string name in localizationFileContentsByPath.Keys.Except(outputPathsForAllLangs))
			{
				string originalPath = Path.Combine(sourceFolder, name);
				string newPath = originalPath + ".legacy";
				if (File.Exists(originalPath))
				{
					ILog tML = Logging.tML;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(320, 2);
					defaultInterpolatedStringHandler.AppendLiteral("The .hjson file \"");
					defaultInterpolatedStringHandler.AppendFormatted(originalPath);
					defaultInterpolatedStringHandler.AppendLiteral("\" was detected as a localization file but doesn't match the filename of any of the English template files. The file will be renamed to \"");
					defaultInterpolatedStringHandler.AppendFormatted(newPath);
					defaultInterpolatedStringHandler.AppendLiteral("\" and its contents will not be loaded. You should update the English template files or move these localization entries to a correctly named file to allow them to load.");
					tML.Warn(defaultInterpolatedStringHandler.ToStringAndClear());
					File.Move(originalPath, newPath);
				}
			}
			if (specificCulture == null)
			{
				Dictionary<GameCulture, int> localizationCounts = new Dictionary<GameCulture, int>();
				foreach (GameCulture culture3 in targetCultures)
				{
					int countNonTrivialEntries = (from x in localizationFilesByCulture[culture3].SelectMany((LocalizationLoader.LocalizationFile f) => f.Entries).ToList<LocalizationLoader.LocalizationEntry>()
					where LocalizationLoader.HasTextThatNeedsLocalization(x.value)
					select x).Count<LocalizationLoader.LocalizationEntry>();
					localizationCounts.Add(culture3, countNonTrivialEntries);
				}
				LocalizationLoader.localizationEntriesCounts[mod.Name] = localizationCounts;
				string translationsNeededPath = Path.Combine(sourceFolder, "Localization", "TranslationsNeeded.txt");
				if (File.Exists(translationsNeededPath))
				{
					int countMaxEntries = localizationCounts.DefaultIfEmpty<KeyValuePair<GameCulture, int>>().Max((KeyValuePair<GameCulture, int> x) => x.Value);
					string neededText = string.Join(Environment.NewLine, (from x in localizationCounts
					orderby x.Key.LegacyId
					select x).Select(delegate(KeyValuePair<GameCulture, int> x)
					{
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(15, 5);
						defaultInterpolatedStringHandler2.AppendFormatted(x.Key.Name);
						defaultInterpolatedStringHandler2.AppendLiteral(", ");
						defaultInterpolatedStringHandler2.AppendFormatted<int>(x.Value);
						defaultInterpolatedStringHandler2.AppendLiteral("/");
						defaultInterpolatedStringHandler2.AppendFormatted<int>(countMaxEntries);
						defaultInterpolatedStringHandler2.AppendLiteral(", ");
						defaultInterpolatedStringHandler2.AppendFormatted<float>((float)x.Value / (float)countMaxEntries, "0%");
						defaultInterpolatedStringHandler2.AppendLiteral(", missing ");
						defaultInterpolatedStringHandler2.AppendFormatted<int>(countMaxEntries - x.Value);
						return defaultInterpolatedStringHandler2.ToStringAndClear();
					})) + Environment.NewLine;
					if (File.ReadAllText(translationsNeededPath).ReplaceLineEndings() != neededText.ReplaceLineEndings())
					{
						File.WriteAllText(translationsNeededPath, neededText);
					}
				}
			}
		}

		// Token: 0x06001F01 RID: 7937 RVA: 0x004DEB64 File Offset: 0x004DCD64
		private static string GetPathForCulture(LocalizationLoader.LocalizationFile file, GameCulture culture)
		{
			return file.path.Replace("en-US", culture.CultureInfo.Name);
		}

		// Token: 0x06001F02 RID: 7938 RVA: 0x004DEB84 File Offset: 0x004DCD84
		private static string LocalizationFileToHjsonText(LocalizationLoader.LocalizationFile baseFile, Dictionary<string, string> localizationsForCulture)
		{
			Dictionary<string, int> prefixCounts = new Dictionary<string, int>();
			foreach (LocalizationLoader.LocalizationEntry entry in baseFile.Entries)
			{
				if (entry.type != 2)
				{
					string[] splitKey = LocalizationLoader.<LocalizationFileToHjsonText>g__GetKeyFromFilePrefixAndEntry|13_1(baseFile, entry).Split(".", StringSplitOptions.None);
					for (int i = 0; i < splitKey.Length; i++)
					{
						string partialKey = string.Join(".", splitKey.Take(i + 1));
						int count;
						prefixCounts.TryGetValue(partialKey, out count);
						prefixCounts[partialKey] = count + 1;
					}
				}
			}
			for (int j = baseFile.Entries.Count - 1; j >= 0; j--)
			{
				LocalizationLoader.LocalizationEntry entry2 = baseFile.Entries[j];
				if (entry2.type == 2)
				{
					string key = LocalizationLoader.<LocalizationFileToHjsonText>g__GetKeyFromFilePrefixAndEntry|13_1(baseFile, entry2);
					int count2;
					if (prefixCounts.TryGetValue(key, out count2) && count2 <= 1)
					{
						baseFile.Entries.RemoveAt(j);
					}
				}
				if (entry2.type == null)
				{
					string key2 = LocalizationLoader.<LocalizationFileToHjsonText>g__GetKeyFromFilePrefixAndEntry|13_1(baseFile, entry2);
					int count3;
					if (prefixCounts.TryGetValue(key2, out count3) && count3 > 1)
					{
						List<LocalizationLoader.LocalizationEntry> entries = baseFile.Entries;
						int index = j;
						LocalizationLoader.LocalizationEntry localizationEntry = entry2.<Clone>$();
						localizationEntry.key = entry2.key + ".$parentVal";
						entries[index] = localizationEntry;
					}
				}
			}
			LocalizationLoader.CommentedWscJsonObject rootObject = new LocalizationLoader.CommentedWscJsonObject();
			foreach (LocalizationLoader.LocalizationEntry entry3 in baseFile.Entries)
			{
				LocalizationLoader.CommentedWscJsonObject parent = rootObject;
				string[] splitKey2 = LocalizationLoader.<LocalizationFileToHjsonText>g__GetKeyFromFilePrefixAndEntry|13_1(baseFile, entry3).Split(".", StringSplitOptions.None);
				string[] array = splitKey2;
				string finalKey = array[array.Length - 1];
				for (int k = 0; k < splitKey2.Length - 1; k++)
				{
					string partialKey2 = string.Join(".", splitKey2.Take(k + 1));
					int count4;
					if (prefixCounts.TryGetValue(partialKey2, out count4) && count4 <= 1)
					{
						finalKey = string.Join(".", splitKey2.Skip(k));
						break;
					}
					string l = splitKey2[k];
					if (parent.ContainsKey(l))
					{
						parent = (LocalizationLoader.CommentedWscJsonObject)parent[l];
					}
					else
					{
						LocalizationLoader.CommentedWscJsonObject newParent = new LocalizationLoader.CommentedWscJsonObject();
						parent.Add(l, newParent);
						parent = newParent;
					}
				}
				if (entry3.value == null && entry3.type == 2)
				{
					JsonValue jsonValue = parent;
					string[] array2 = splitKey2;
					if (!jsonValue.ContainsKey(array2[array2.Length - 1]))
					{
						LocalizationLoader.<LocalizationFileToHjsonText>g__PlaceCommentAboveNewEntry|13_0(entry3, parent);
						JsonObject jsonObject = parent;
						string[] array3 = splitKey2;
						jsonObject.Add(array3[array3.Length - 1], new LocalizationLoader.CommentedWscJsonObject());
					}
				}
				else
				{
					string realKey = entry3.key.Replace(".$parentVal", "");
					string value;
					if (!localizationsForCulture.TryGetValue(realKey, out value))
					{
						parent.CommentedOut.Add(finalKey);
						value = entry3.value;
					}
					LocalizationLoader.<LocalizationFileToHjsonText>g__PlaceCommentAboveNewEntry|13_0(entry3, parent);
					parent.Add(finalKey, value);
				}
			}
			return rootObject.ToFancyHjsonString(null) + Environment.NewLine;
		}

		// Token: 0x06001F03 RID: 7939 RVA: 0x004DEEAC File Offset: 0x004DD0AC
		private static List<LocalizationLoader.LocalizationEntry> ParseLocalizationEntries(WscJsonObject jsonObjectEng, string prefix)
		{
			LocalizationLoader.<>c__DisplayClass14_0 CS$<>8__locals1;
			CS$<>8__locals1.existingKeys = new List<LocalizationLoader.LocalizationEntry>();
			LocalizationLoader.<ParseLocalizationEntries>g__RecurseThrough|14_0(jsonObjectEng, prefix, ref CS$<>8__locals1);
			return CS$<>8__locals1.existingKeys;
		}

		// Token: 0x06001F04 RID: 7940 RVA: 0x004DEED4 File Offset: 0x004DD0D4
		private static LocalizationLoader.LocalizationFile FindHJSONFileForKey(List<LocalizationLoader.LocalizationFile> files, string key)
		{
			int levelFound = -1;
			LocalizationLoader.LocalizationFile best = null;
			foreach (LocalizationLoader.LocalizationFile file in files)
			{
				if (string.IsNullOrWhiteSpace(file.prefix) || key.StartsWith(file.prefix))
				{
					int level = LocalizationLoader.LongestMatchingPrefix(file, key);
					if (level > levelFound)
					{
						levelFound = level;
						best = file;
					}
				}
			}
			if (best == null)
			{
				best = new LocalizationLoader.LocalizationFile("en-US.hjson", "", new List<LocalizationLoader.LocalizationEntry>());
				files.Add(best);
			}
			return best;
		}

		// Token: 0x06001F05 RID: 7941 RVA: 0x004DEF74 File Offset: 0x004DD174
		internal static int LongestMatchingPrefix(LocalizationLoader.LocalizationFile file, string key)
		{
			int num = string.IsNullOrWhiteSpace(file.prefix) ? 0 : file.prefix.Split(".", StringSplitOptions.None).Length;
			List<LocalizationLoader.LocalizationEntry> localizationEntries = file.Entries;
			string[] splitKey = key.Split(".", StringSplitOptions.None);
			for (int i = num; i < splitKey.Length; i++)
			{
				string text = splitKey[i];
				string partialKey = string.Join(".", splitKey.Take(i + 1));
				if (!localizationEntries.Any((LocalizationLoader.LocalizationEntry x) => x.key.StartsWith(partialKey)))
				{
					return i;
				}
			}
			return splitKey.Length;
		}

		// Token: 0x06001F06 RID: 7942 RVA: 0x004DF004 File Offset: 0x004DD204
		internal static void AddEntryToHJSON(LocalizationLoader.LocalizationFile file, string key, string value, string comment = null)
		{
			int index = 0;
			string[] splitKey = key.Split(".", StringSplitOptions.None);
			for (int i = 0; i < splitKey.Length - 1; i++)
			{
				string text = splitKey[i];
				string partialKey = string.Join(".", splitKey.Take(i + 1));
				int newIndex = file.Entries.FindLastIndex((LocalizationLoader.LocalizationEntry x) => x.key.StartsWith(partialKey));
				if (newIndex != -1)
				{
					index = newIndex;
				}
			}
			int placementIndex = (file.Entries.Count > 0) ? (index + 1) : 0;
			file.Entries.Insert(placementIndex, new LocalizationLoader.LocalizationEntry(key, value, comment, 0));
		}

		// Token: 0x06001F07 RID: 7943 RVA: 0x004DF0A4 File Offset: 0x004DD2A4
		internal static bool ExtractLocalizationFiles(string modName)
		{
			string dir = Path.Combine(Main.SavePath, "ModLocalization", modName);
			if (Directory.Exists(dir))
			{
				Directory.Delete(dir, true);
			}
			Directory.CreateDirectory(dir);
			Mod mod;
			ModLoader.TryGetMod(modName, out mod);
			if (mod == null)
			{
				Logging.tML.Error("Somehow " + modName + " was not loaded");
				return false;
			}
			LocalizationLoader.UpdateLocalizationFilesForMod(mod, dir, Language.ActiveCulture);
			Utils.OpenFolder(dir);
			return true;
		}

		// Token: 0x06001F08 RID: 7944 RVA: 0x004DF114 File Offset: 0x004DD314
		internal static Dictionary<GameCulture, int> GetLocalizationCounts(Mod mod)
		{
			Dictionary<GameCulture, int> results;
			if (LocalizationLoader.localizationEntriesCounts.TryGetValue(mod.Name, out results))
			{
				return results;
			}
			results = new Dictionary<GameCulture, int>();
			foreach (GameCulture culture in GameCulture.KnownCultures)
			{
				int countNonTrivialEntries = (from x in LocalizationLoader.LoadTranslations(mod, culture)
				where LocalizationLoader.HasTextThatNeedsLocalization(x.Item2)
				select x).Count<ValueTuple<string, string>>();
				results.Add(culture, countNonTrivialEntries);
			}
			LocalizationLoader.localizationEntriesCounts[mod.Name] = results;
			return results;
		}

		// Token: 0x06001F09 RID: 7945 RVA: 0x004DF1C0 File Offset: 0x004DD3C0
		private static bool HasTextThatNeedsLocalization(string value)
		{
			return !string.IsNullOrWhiteSpace(value) && !string.IsNullOrWhiteSpace(LocalizationLoader.referenceRegex.Replace(value, ""));
		}

		// Token: 0x06001F0A RID: 7946 RVA: 0x004DF1E8 File Offset: 0x004DD3E8
		private static void SetupFileWatchers()
		{
			Mod[] mods = ModLoader.Mods;
			for (int i = 0; i < mods.Length; i++)
			{
				Mod mod = mods[i];
				if (mod.File != null)
				{
					string path = mod.SourceFolder;
					if (Directory.Exists(path))
					{
						try
						{
							FileSystemWatcher localizationFileWatcher = new FileSystemWatcher();
							localizationFileWatcher.Path = path;
							localizationFileWatcher.NotifyFilter = (NotifyFilters.FileName | NotifyFilters.LastWrite);
							localizationFileWatcher.Filter = "*.hjson";
							localizationFileWatcher.IncludeSubdirectories = true;
							localizationFileWatcher.Changed += delegate(object a, FileSystemEventArgs b)
							{
								LocalizationLoader.HandleFileChangedOrRenamed(mod.Name, b.Name);
							};
							localizationFileWatcher.Renamed += delegate(object a, RenamedEventArgs b)
							{
								LocalizationLoader.HandleFileChangedOrRenamed(mod.Name, b.Name);
							};
							localizationFileWatcher.EnableRaisingEvents = true;
							LocalizationLoader.localizationFileWatchers[mod] = localizationFileWatcher;
						}
						catch (Exception)
						{
							throw;
						}
					}
				}
			}
		}

		// Token: 0x06001F0B RID: 7947 RVA: 0x004DF2C4 File Offset: 0x004DD4C4
		internal static void Unload()
		{
			LanguageManager.Instance.UnloadModdedEntries();
			LocalizationLoader.UnloadFileWatchers();
		}

		// Token: 0x06001F0C RID: 7948 RVA: 0x004DF2D8 File Offset: 0x004DD4D8
		private static void UnloadFileWatchers()
		{
			foreach (FileSystemWatcher fileSystemWatcher in LocalizationLoader.localizationFileWatchers.Values)
			{
				fileSystemWatcher.EnableRaisingEvents = false;
				fileSystemWatcher.Dispose();
			}
			LocalizationLoader.localizationFileWatchers.Clear();
		}

		// Token: 0x06001F0D RID: 7949 RVA: 0x004DF340 File Offset: 0x004DD540
		private static void HandleFileChangedOrRenamed(string modName, string fileName)
		{
			GameCulture gameCulture;
			string text;
			if (!LocalizationLoader.TryGetCultureAndPrefixFromPath(fileName, out gameCulture, out text))
			{
				return;
			}
			LocalizationLoader.watcherCooldown = 60;
			HashSet<ValueTuple<string, string>> obj = LocalizationLoader.pendingFiles;
			lock (obj)
			{
				LocalizationLoader.pendingFiles.Add(new ValueTuple<string, string>(modName, fileName));
			}
		}

		// Token: 0x06001F0E RID: 7950 RVA: 0x004DF3A0 File Offset: 0x004DD5A0
		internal static void HandleModBuilt(string modName)
		{
			LocalizationLoader.changedMods.Remove(modName);
			LocalizationLoader.changedFiles.RemoveWhere(([TupleElementNames(new string[]
			{
				"Mod",
				"fileName"
			})] ValueTuple<string, string> x) => x.Item1 == modName);
		}

		// Token: 0x06001F0F RID: 7951 RVA: 0x004DF3E4 File Offset: 0x004DD5E4
		internal static void Update()
		{
			if (LocalizationLoader.watcherCooldown <= 0)
			{
				return;
			}
			LocalizationLoader.watcherCooldown--;
			if (LocalizationLoader.watcherCooldown != 0)
			{
				return;
			}
			HashSet<ValueTuple<string, string>> obj = LocalizationLoader.pendingFiles;
			lock (obj)
			{
				Utils.LogAndChatAndConsoleInfoMessage(Language.GetTextValue("tModLoader.WatchLocalizationFileMessage", string.Join(", ", from x in LocalizationLoader.pendingFiles
				select Path.Join(x.Item1, x.Item2))));
			}
			obj = LocalizationLoader.pendingFiles;
			lock (obj)
			{
				LocalizationLoader.changedMods.UnionWith(from x in LocalizationLoader.pendingFiles
				select x.Item1);
				LocalizationLoader.changedFiles.UnionWith(LocalizationLoader.pendingFiles);
				LocalizationLoader.pendingFiles.Clear();
			}
			LanguageManager.Instance.ReloadLanguage(true);
		}

		// Token: 0x06001F11 RID: 7953 RVA: 0x004DF54C File Offset: 0x004DD74C
		[CompilerGenerated]
		internal static void <LocalizationFileToHjsonText>g__PlaceCommentAboveNewEntry|13_0(LocalizationLoader.LocalizationEntry entry, LocalizationLoader.CommentedWscJsonObject parent)
		{
			if (parent.Count == 0)
			{
				parent.Comments[""] = entry.comment;
				return;
			}
			string actualCommentKey = parent.Keys.Last<string>();
			parent.Comments[actualCommentKey] = entry.comment;
		}

		// Token: 0x06001F12 RID: 7954 RVA: 0x004DF598 File Offset: 0x004DD798
		[CompilerGenerated]
		internal static string <LocalizationFileToHjsonText>g__GetKeyFromFilePrefixAndEntry|13_1(LocalizationLoader.LocalizationFile baseLocalizationFileEntry, LocalizationLoader.LocalizationEntry entry)
		{
			string key = entry.key;
			if (!string.IsNullOrWhiteSpace(baseLocalizationFileEntry.prefix))
			{
				key = key.Substring(baseLocalizationFileEntry.prefix.Length + 1);
			}
			return key;
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x004DF5D0 File Offset: 0x004DD7D0
		[CompilerGenerated]
		internal static void <ParseLocalizationEntries>g__RecurseThrough|14_0(WscJsonObject original, string prefix, ref LocalizationLoader.<>c__DisplayClass14_0 A_2)
		{
			int index = 0;
			foreach (KeyValuePair<string, JsonValue> item in original)
			{
				if (item.Value.JsonType == 2)
				{
					JsonValue jsonValue = item.Value as WscJsonObject;
					string newPrefix = string.IsNullOrWhiteSpace(prefix) ? item.Key : (prefix + "." + item.Key);
					string comment = LocalizationLoader.<ParseLocalizationEntries>g__GetCommentFromIndex|14_1(index, original);
					A_2.existingKeys.Add(new LocalizationLoader.LocalizationEntry(newPrefix, null, comment, 2));
					LocalizationLoader.<ParseLocalizationEntries>g__RecurseThrough|14_0(JsonUtil.Qo(jsonValue) as WscJsonObject, newPrefix, ref A_2);
				}
				else if (item.Value.JsonType == null)
				{
					string localizationValue = JsonUtil.Qs(item.Value);
					string key = string.IsNullOrWhiteSpace(prefix) ? item.Key : (prefix + "." + item.Key);
					if (key.EndsWith(".$parentVal"))
					{
						key = key.Replace(".$parentVal", "");
					}
					string comment2 = LocalizationLoader.<ParseLocalizationEntries>g__GetCommentFromIndex|14_1(index, original);
					A_2.existingKeys.Add(new LocalizationLoader.LocalizationEntry(key, localizationValue, comment2, 0));
				}
				index++;
			}
		}

		// Token: 0x06001F14 RID: 7956 RVA: 0x004DF720 File Offset: 0x004DD920
		[CompilerGenerated]
		internal static string <ParseLocalizationEntries>g__GetCommentFromIndex|14_1(int index, WscJsonObject original)
		{
			int actualOrderIndex = index - 1;
			string actualCommentKey = (actualOrderIndex == -1) ? "" : original.Order[actualOrderIndex];
			return original.Comments[actualCommentKey];
		}

		// Token: 0x0400164F RID: 5711
		private static readonly Dictionary<string, Dictionary<GameCulture, int>> localizationEntriesCounts = new Dictionary<string, Dictionary<GameCulture, int>>();

		// Token: 0x04001650 RID: 5712
		private static Regex referenceRegex = new Regex("{\\$([\\w\\.]+)(?:@(\\d+))?}", RegexOptions.Compiled);

		// Token: 0x04001651 RID: 5713
		private const int defaultWatcherCooldown = 60;

		// Token: 0x04001652 RID: 5714
		private static readonly Dictionary<Mod, FileSystemWatcher> localizationFileWatchers = new Dictionary<Mod, FileSystemWatcher>();

		// Token: 0x04001653 RID: 5715
		[TupleElementNames(new string[]
		{
			"Mod",
			"fileName"
		})]
		private static readonly HashSet<ValueTuple<string, string>> changedFiles = new HashSet<ValueTuple<string, string>>();

		// Token: 0x04001654 RID: 5716
		[TupleElementNames(new string[]
		{
			"Mod",
			"fileName"
		})]
		private static readonly HashSet<ValueTuple<string, string>> pendingFiles = new HashSet<ValueTuple<string, string>>();

		// Token: 0x04001655 RID: 5717
		internal static readonly HashSet<string> changedMods = new HashSet<string>();

		// Token: 0x04001656 RID: 5718
		private static int watcherCooldown;

		// Token: 0x02000900 RID: 2304
		public class LocalizationFile : IEquatable<LocalizationLoader.LocalizationFile>
		{
			// Token: 0x06005325 RID: 21285 RVA: 0x00699393 File Offset: 0x00697593
			public LocalizationFile(string path, string prefix, List<LocalizationLoader.LocalizationEntry> Entries)
			{
				this.path = path;
				this.prefix = prefix;
				this.Entries = Entries;
				base..ctor();
			}

			// Token: 0x170008D4 RID: 2260
			// (get) Token: 0x06005326 RID: 21286 RVA: 0x006993B0 File Offset: 0x006975B0
			[Nullable(1)]
			[CompilerGenerated]
			protected virtual Type EqualityContract
			{
				[NullableContext(1)]
				[CompilerGenerated]
				get
				{
					return typeof(LocalizationLoader.LocalizationFile);
				}
			}

			// Token: 0x170008D5 RID: 2261
			// (get) Token: 0x06005327 RID: 21287 RVA: 0x006993BC File Offset: 0x006975BC
			// (set) Token: 0x06005328 RID: 21288 RVA: 0x006993C4 File Offset: 0x006975C4
			public string path { get; set; }

			// Token: 0x170008D6 RID: 2262
			// (get) Token: 0x06005329 RID: 21289 RVA: 0x006993CD File Offset: 0x006975CD
			// (set) Token: 0x0600532A RID: 21290 RVA: 0x006993D5 File Offset: 0x006975D5
			public string prefix { get; set; }

			// Token: 0x170008D7 RID: 2263
			// (get) Token: 0x0600532B RID: 21291 RVA: 0x006993DE File Offset: 0x006975DE
			// (set) Token: 0x0600532C RID: 21292 RVA: 0x006993E6 File Offset: 0x006975E6
			public List<LocalizationLoader.LocalizationEntry> Entries { get; set; }

			// Token: 0x0600532D RID: 21293 RVA: 0x006993F0 File Offset: 0x006975F0
			[NullableContext(1)]
			[CompilerGenerated]
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("LocalizationFile");
				stringBuilder.Append(" { ");
				if (this.PrintMembers(stringBuilder))
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append('}');
				return stringBuilder.ToString();
			}

			// Token: 0x0600532E RID: 21294 RVA: 0x0069943C File Offset: 0x0069763C
			[NullableContext(1)]
			[CompilerGenerated]
			protected virtual bool PrintMembers(StringBuilder builder)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				builder.Append("path = ");
				builder.Append(this.path);
				builder.Append(", prefix = ");
				builder.Append(this.prefix);
				builder.Append(", Entries = ");
				builder.Append(this.Entries);
				return true;
			}

			// Token: 0x0600532F RID: 21295 RVA: 0x0069949A File Offset: 0x0069769A
			[NullableContext(2)]
			[CompilerGenerated]
			public static bool operator !=(LocalizationLoader.LocalizationFile left, LocalizationLoader.LocalizationFile right)
			{
				return !(left == right);
			}

			// Token: 0x06005330 RID: 21296 RVA: 0x006994A6 File Offset: 0x006976A6
			[NullableContext(2)]
			[CompilerGenerated]
			public static bool operator ==(LocalizationLoader.LocalizationFile left, LocalizationLoader.LocalizationFile right)
			{
				return left == right || (left != null && left.Equals(right));
			}

			// Token: 0x06005331 RID: 21297 RVA: 0x006994BC File Offset: 0x006976BC
			[CompilerGenerated]
			public override int GetHashCode()
			{
				return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<path>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<prefix>k__BackingField)) * -1521134295 + EqualityComparer<List<LocalizationLoader.LocalizationEntry>>.Default.GetHashCode(this.<Entries>k__BackingField);
			}

			// Token: 0x06005332 RID: 21298 RVA: 0x0069951E File Offset: 0x0069771E
			[NullableContext(2)]
			[CompilerGenerated]
			public override bool Equals(object obj)
			{
				return this.Equals(obj as LocalizationLoader.LocalizationFile);
			}

			// Token: 0x06005333 RID: 21299 RVA: 0x0069952C File Offset: 0x0069772C
			[NullableContext(2)]
			[CompilerGenerated]
			public virtual bool Equals(LocalizationLoader.LocalizationFile other)
			{
				return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<path>k__BackingField, other.<path>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<prefix>k__BackingField, other.<prefix>k__BackingField) && EqualityComparer<List<LocalizationLoader.LocalizationEntry>>.Default.Equals(this.<Entries>k__BackingField, other.<Entries>k__BackingField));
			}

			// Token: 0x06005335 RID: 21301 RVA: 0x006995A5 File Offset: 0x006977A5
			[CompilerGenerated]
			protected LocalizationFile([Nullable(1)] LocalizationLoader.LocalizationFile original)
			{
				this.path = original.<path>k__BackingField;
				this.prefix = original.<prefix>k__BackingField;
				this.Entries = original.<Entries>k__BackingField;
			}

			// Token: 0x06005336 RID: 21302 RVA: 0x006995D1 File Offset: 0x006977D1
			[CompilerGenerated]
			public void Deconstruct(out string path, out string prefix, out List<LocalizationLoader.LocalizationEntry> Entries)
			{
				path = this.path;
				prefix = this.prefix;
				Entries = this.Entries;
			}
		}

		// Token: 0x02000901 RID: 2305
		public class LocalizationEntry : IEquatable<LocalizationLoader.LocalizationEntry>
		{
			// Token: 0x06005337 RID: 21303 RVA: 0x006995EB File Offset: 0x006977EB
			public LocalizationEntry(string key, string value, string comment, JsonType type = 0)
			{
				this.key = key;
				this.value = value;
				this.comment = comment;
				this.type = type;
				base..ctor();
			}

			// Token: 0x170008D8 RID: 2264
			// (get) Token: 0x06005338 RID: 21304 RVA: 0x00699610 File Offset: 0x00697810
			[Nullable(1)]
			[CompilerGenerated]
			protected virtual Type EqualityContract
			{
				[NullableContext(1)]
				[CompilerGenerated]
				get
				{
					return typeof(LocalizationLoader.LocalizationEntry);
				}
			}

			// Token: 0x170008D9 RID: 2265
			// (get) Token: 0x06005339 RID: 21305 RVA: 0x0069961C File Offset: 0x0069781C
			// (set) Token: 0x0600533A RID: 21306 RVA: 0x00699624 File Offset: 0x00697824
			public string key { get; set; }

			// Token: 0x170008DA RID: 2266
			// (get) Token: 0x0600533B RID: 21307 RVA: 0x0069962D File Offset: 0x0069782D
			// (set) Token: 0x0600533C RID: 21308 RVA: 0x00699635 File Offset: 0x00697835
			public string value { get; set; }

			// Token: 0x170008DB RID: 2267
			// (get) Token: 0x0600533D RID: 21309 RVA: 0x0069963E File Offset: 0x0069783E
			// (set) Token: 0x0600533E RID: 21310 RVA: 0x00699646 File Offset: 0x00697846
			public string comment { get; set; }

			// Token: 0x170008DC RID: 2268
			// (get) Token: 0x0600533F RID: 21311 RVA: 0x0069964F File Offset: 0x0069784F
			// (set) Token: 0x06005340 RID: 21312 RVA: 0x00699657 File Offset: 0x00697857
			public JsonType type { get; set; }

			// Token: 0x06005341 RID: 21313 RVA: 0x00699660 File Offset: 0x00697860
			[NullableContext(1)]
			[CompilerGenerated]
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("LocalizationEntry");
				stringBuilder.Append(" { ");
				if (this.PrintMembers(stringBuilder))
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append('}');
				return stringBuilder.ToString();
			}

			// Token: 0x06005342 RID: 21314 RVA: 0x006996AC File Offset: 0x006978AC
			[NullableContext(1)]
			[CompilerGenerated]
			protected virtual bool PrintMembers(StringBuilder builder)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				builder.Append("key = ");
				builder.Append(this.key);
				builder.Append(", value = ");
				builder.Append(this.value);
				builder.Append(", comment = ");
				builder.Append(this.comment);
				builder.Append(", type = ");
				builder.Append(this.type.ToString());
				return true;
			}

			// Token: 0x06005343 RID: 21315 RVA: 0x00699731 File Offset: 0x00697931
			[NullableContext(2)]
			[CompilerGenerated]
			public static bool operator !=(LocalizationLoader.LocalizationEntry left, LocalizationLoader.LocalizationEntry right)
			{
				return !(left == right);
			}

			// Token: 0x06005344 RID: 21316 RVA: 0x0069973D File Offset: 0x0069793D
			[NullableContext(2)]
			[CompilerGenerated]
			public static bool operator ==(LocalizationLoader.LocalizationEntry left, LocalizationLoader.LocalizationEntry right)
			{
				return left == right || (left != null && left.Equals(right));
			}

			// Token: 0x06005345 RID: 21317 RVA: 0x00699754 File Offset: 0x00697954
			[CompilerGenerated]
			public override int GetHashCode()
			{
				return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<key>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<value>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<comment>k__BackingField)) * -1521134295 + EqualityComparer<JsonType>.Default.GetHashCode(this.<type>k__BackingField);
			}

			// Token: 0x06005346 RID: 21318 RVA: 0x006997CD File Offset: 0x006979CD
			[NullableContext(2)]
			[CompilerGenerated]
			public override bool Equals(object obj)
			{
				return this.Equals(obj as LocalizationLoader.LocalizationEntry);
			}

			// Token: 0x06005347 RID: 21319 RVA: 0x006997DC File Offset: 0x006979DC
			[NullableContext(2)]
			[CompilerGenerated]
			public virtual bool Equals(LocalizationLoader.LocalizationEntry other)
			{
				return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<key>k__BackingField, other.<key>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<value>k__BackingField, other.<value>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<comment>k__BackingField, other.<comment>k__BackingField) && EqualityComparer<JsonType>.Default.Equals(this.<type>k__BackingField, other.<type>k__BackingField));
			}

			// Token: 0x06005349 RID: 21321 RVA: 0x0069986D File Offset: 0x00697A6D
			[CompilerGenerated]
			protected LocalizationEntry([Nullable(1)] LocalizationLoader.LocalizationEntry original)
			{
				this.key = original.<key>k__BackingField;
				this.value = original.<value>k__BackingField;
				this.comment = original.<comment>k__BackingField;
				this.type = original.<type>k__BackingField;
			}

			// Token: 0x0600534A RID: 21322 RVA: 0x006998A5 File Offset: 0x00697AA5
			[CompilerGenerated]
			public void Deconstruct(out string key, out string value, out string comment, out JsonType type)
			{
				key = this.key;
				value = this.value;
				comment = this.comment;
				type = this.type;
			}
		}

		// Token: 0x02000902 RID: 2306
		public class CommentedWscJsonObject : WscJsonObject
		{
			// Token: 0x170008DD RID: 2269
			// (get) Token: 0x0600534B RID: 21323 RVA: 0x006998C8 File Offset: 0x00697AC8
			// (set) Token: 0x0600534C RID: 21324 RVA: 0x006998D0 File Offset: 0x00697AD0
			public List<string> CommentedOut { get; private set; }

			// Token: 0x0600534D RID: 21325 RVA: 0x006998D9 File Offset: 0x00697AD9
			public CommentedWscJsonObject()
			{
				this.CommentedOut = new List<string>();
			}
		}
	}
}
