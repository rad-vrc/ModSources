using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using CsvHelper;
using Newtonsoft.Json;
using ReLogic.Content;
using ReLogic.Content.Sources;
using ReLogic.Graphics;
using Terraria.GameContent;
using Terraria.Initializers;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Terraria.Localization
{
	// Token: 0x020003D7 RID: 983
	public class LanguageManager
	{
		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06003394 RID: 13204 RVA: 0x00554651 File Offset: 0x00552851
		// (set) Token: 0x06003395 RID: 13205 RVA: 0x00554659 File Offset: 0x00552859
		public GameCulture ActiveCulture { get; private set; }

		// Token: 0x14000056 RID: 86
		// (add) Token: 0x06003396 RID: 13206 RVA: 0x00554664 File Offset: 0x00552864
		// (remove) Token: 0x06003397 RID: 13207 RVA: 0x0055469C File Offset: 0x0055289C
		public event LanguageChangeCallback OnLanguageChanging;

		// Token: 0x14000057 RID: 87
		// (add) Token: 0x06003398 RID: 13208 RVA: 0x005546D4 File Offset: 0x005528D4
		// (remove) Token: 0x06003399 RID: 13209 RVA: 0x0055470C File Offset: 0x0055290C
		public event LanguageChangeCallback OnLanguageChanged;

		// Token: 0x0600339A RID: 13210 RVA: 0x00554744 File Offset: 0x00552944
		private LanguageManager()
		{
			this._localizedTexts[""] = LocalizedText.Empty;
		}

		// Token: 0x0600339B RID: 13211 RVA: 0x005547B9 File Offset: 0x005529B9
		public int GetCategorySize(string name)
		{
			if (this._categoryGroupedKeys.ContainsKey(name))
			{
				return this._categoryGroupedKeys[name].Count;
			}
			return 0;
		}

		// Token: 0x0600339C RID: 13212 RVA: 0x005547DC File Offset: 0x005529DC
		public void SetLanguage(int legacyId)
		{
			GameCulture language = GameCulture.FromLegacyId(legacyId);
			this.SetLanguage(language);
		}

		// Token: 0x0600339D RID: 13213 RVA: 0x005547F8 File Offset: 0x005529F8
		public void SetLanguage(string cultureName)
		{
			GameCulture language = GameCulture.FromName(cultureName);
			this.SetLanguage(language);
		}

		// Token: 0x0600339E RID: 13214 RVA: 0x00554814 File Offset: 0x00552A14
		public int EstimateWordCount()
		{
			int num = 0;
			foreach (string key in this._localizedTexts.Keys)
			{
				string textValue = this.GetTextValue(key);
				textValue.Replace(",", "").Replace(".", "").Replace("\"", "").Trim();
				string[] array = textValue.Split(' ', StringSplitOptions.None);
				string[] array2 = textValue.Split(' ', StringSplitOptions.None);
				if (array.Length != array2.Length)
				{
					return num;
				}
				foreach (string text in array)
				{
					if (!string.IsNullOrWhiteSpace(text) && text.Length >= 1)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x0600339F RID: 13215 RVA: 0x00554904 File Offset: 0x00552B04
		private void SetAllTextValuesToKeys()
		{
			foreach (KeyValuePair<string, LocalizedText> localizedText in this._localizedTexts)
			{
				localizedText.Value.SetValue(localizedText.Key);
			}
		}

		// Token: 0x060033A0 RID: 13216 RVA: 0x00554964 File Offset: 0x00552B64
		private string[] GetLanguageFilesForCulture(GameCulture culture)
		{
			Assembly.GetExecutingAssembly();
			return Array.FindAll<string>(typeof(Program).Assembly.GetManifestResourceNames(), (string element) => element.StartsWith("Terraria.Localization.Content." + culture.CultureInfo.Name.Replace('-', '_')) && element.EndsWith(".json"));
		}

		// Token: 0x060033A1 RID: 13217 RVA: 0x005549AC File Offset: 0x00552BAC
		public void SetLanguage(GameCulture culture)
		{
			if (this.ActiveCulture != culture)
			{
				this.ActiveCulture = culture;
				this.ReloadLanguage(true);
				Thread.CurrentThread.CurrentCulture = culture.CultureInfo;
				Thread.CurrentThread.CurrentUICulture = culture.CultureInfo;
				if (this.OnLanguageChanged != null)
				{
					this.OnLanguageChanged(this);
				}
				Asset<DynamicSpriteFont> deathText = FontAssets.DeathText;
			}
		}

		// Token: 0x060033A2 RID: 13218 RVA: 0x00554A0C File Offset: 0x00552C0C
		internal void ReloadLanguage(bool resetValuesToKeysFirst = true)
		{
			if (resetValuesToKeysFirst)
			{
				this.SetAllTextValuesToKeys();
			}
			this.LoadFilesForCulture(this._fallbackCulture);
			if (this.ActiveCulture != this._fallbackCulture)
			{
				this.LoadFilesForCulture(this.ActiveCulture);
			}
			this.LoadActiveCultureTranslationsFromSources();
			this.ProcessCopyCommandsInTexts();
			this.RecalculateBoundTextValues();
			ChatInitializer.PrepareAliases();
			SystemLoader.OnLocalizationsLoaded();
		}

		// Token: 0x060033A3 RID: 13219 RVA: 0x00554A64 File Offset: 0x00552C64
		private void LoadFilesForCulture(GameCulture culture)
		{
			foreach (string text in this.GetLanguageFilesForCulture(culture))
			{
				try
				{
					string text2 = Utils.ReadEmbeddedResource(text);
					if (text2 == null || text2.Length < 2)
					{
						throw new FormatException();
					}
					this.LoadLanguageFromFileTextJson(text2, true);
				}
				catch (Exception)
				{
					if (Debugger.IsAttached)
					{
						Debugger.Break();
					}
					Console.WriteLine("Failed to load language file: " + text);
					break;
				}
			}
			LocalizationLoader.LoadModTranslations(culture);
		}

		// Token: 0x060033A4 RID: 13220 RVA: 0x00554AE4 File Offset: 0x00552CE4
		public void UseSources(List<IContentSource> sourcesFromLowestToHighest)
		{
			if (this._contentSources.SequenceEqual(sourcesFromLowestToHighest))
			{
				return;
			}
			this._contentSources = sourcesFromLowestToHighest.ToArray();
			this.ReloadLanguage(true);
		}

		// Token: 0x060033A5 RID: 13221 RVA: 0x00554B08 File Offset: 0x00552D08
		private void LoadActiveCultureTranslationsFromSources()
		{
			IContentSource[] contentSources = this._contentSources;
			string name = this.ActiveCulture.Name;
			string assetNameStart = ("Localization" + Path.DirectorySeparatorChar.ToString() + name).ToLower();
			foreach (IContentSource item in contentSources)
			{
				foreach (string item2 in item.GetAllAssetsStartingWith(assetNameStart, true))
				{
					string extension = item.GetExtension(item2) ?? "";
					if (extension == ".json" || extension == ".csv")
					{
						try
						{
							using (Stream stream = item.OpenStream(item2))
							{
								using (StreamReader streamReader = new StreamReader(stream))
								{
									string fileText = streamReader.ReadToEnd();
									if (extension == ".json")
									{
										this.LoadLanguageFromFileTextJson(fileText, false);
									}
									if (extension == ".csv")
									{
										this.LoadLanguageFromFileTextCsv(fileText);
									}
								}
							}
						}
						catch
						{
							Logging.Terraria.Error("An error occurred loading the \"" + item2 + "\" file from one of the enabled resource packs. The language changes contained within will not take effect.");
							string basePath = null;
							FileSystemContentSource fileSystemContentSource = item as FileSystemContentSource;
							if (fileSystemContentSource != null)
							{
								basePath = (string)typeof(FileSystemContentSource).GetField("_basePath", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(fileSystemContentSource);
								ResourcePack resourcePack = Main.AssetSourceController.ActiveResourcePackList.EnabledPacks.FirstOrDefault((ResourcePack x) => basePath.Contains(x.FileName));
								if (resourcePack != null)
								{
									Logging.Terraria.Error("The resource pack that caused this error is \"" + resourcePack.Name + "\".");
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060033A6 RID: 13222 RVA: 0x00554D44 File Offset: 0x00552F44
		public void LoadLanguageFromFileTextCsv(string fileText)
		{
			using (TextReader reader = new StringReader(fileText))
			{
				using (CsvReader csvReader = new CsvReader(reader))
				{
					csvReader.Configuration.HasHeaderRecord = true;
					if (csvReader.ReadHeader())
					{
						string[] fieldHeaders = csvReader.FieldHeaders;
						int num = -1;
						int num2 = -1;
						for (int i = 0; i < fieldHeaders.Length; i++)
						{
							string a = fieldHeaders[i].ToLower();
							if (a == "translation")
							{
								num2 = i;
							}
							if (a == "key")
							{
								num = i;
							}
						}
						if (num != -1 && num2 != -1)
						{
							int num3 = Math.Max(num, num2) + 1;
							while (csvReader.Read())
							{
								string[] currentRecord = csvReader.CurrentRecord;
								if (currentRecord.Length >= num3)
								{
									string text2 = currentRecord[num];
									string value = currentRecord[num2];
									if (!string.IsNullOrWhiteSpace(text2) && !string.IsNullOrWhiteSpace(value) && this._localizedTexts.ContainsKey(text2))
									{
										this._localizedTexts[text2].SetValue(value);
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060033A7 RID: 13223 RVA: 0x00554E64 File Offset: 0x00553064
		public void LoadLanguageFromFileTextJson(string fileText, bool canCreateCategories)
		{
			foreach (KeyValuePair<string, Dictionary<string, string>> item in JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(fileText))
			{
				string key2 = item.Key;
				foreach (KeyValuePair<string, string> item2 in item.Value)
				{
					string key = item.Key + "." + item2.Key;
					if (this._localizedTexts.ContainsKey(key))
					{
						this._localizedTexts[key].SetValue(item2.Value);
					}
					else if (canCreateCategories)
					{
						this._localizedTexts.Add(key, new LocalizedText(key, item2.Value));
						if (!this._categoryGroupedKeys.ContainsKey(item.Key))
						{
							this._categoryGroupedKeys.Add(item.Key, new List<string>());
						}
						this._categoryGroupedKeys[item.Key].Add(item2.Key);
					}
				}
			}
		}

		// Token: 0x060033A8 RID: 13224 RVA: 0x00554FC4 File Offset: 0x005531C4
		[Conditional("DEBUG")]
		private void ValidateAllCharactersContainedInFont(DynamicSpriteFont font)
		{
			if (font == null)
			{
				return;
			}
			string text = "";
			foreach (LocalizedText value2 in this._localizedTexts.Values)
			{
				foreach (char c in value2.Value)
				{
					if (!font.IsCharacterSupported(c))
					{
						string[] array = new string[7];
						array[0] = text;
						array[1] = value2.Key;
						array[2] = ", ";
						array[3] = c.ToString();
						array[4] = ", ";
						int num = 5;
						int num2 = (int)c;
						array[num] = num2.ToString();
						array[6] = "\n";
						text = string.Concat(array);
					}
				}
			}
		}

		// Token: 0x060033A9 RID: 13225 RVA: 0x005550A0 File Offset: 0x005532A0
		public LocalizedText[] FindAll(Regex regex)
		{
			int num = 0;
			foreach (KeyValuePair<string, LocalizedText> localizedText in this._localizedTexts)
			{
				if (regex.IsMatch(localizedText.Key))
				{
					num++;
				}
			}
			LocalizedText[] array = new LocalizedText[num];
			int num2 = 0;
			foreach (KeyValuePair<string, LocalizedText> localizedText2 in this._localizedTexts)
			{
				if (regex.IsMatch(localizedText2.Key))
				{
					array[num2] = localizedText2.Value;
					num2++;
				}
			}
			return array;
		}

		// Token: 0x060033AA RID: 13226 RVA: 0x00555168 File Offset: 0x00553368
		public LocalizedText[] FindAll(LanguageSearchFilter filter)
		{
			LinkedList<LocalizedText> linkedList = new LinkedList<LocalizedText>();
			foreach (KeyValuePair<string, LocalizedText> localizedText in this._localizedTexts)
			{
				if (filter(localizedText.Key, localizedText.Value))
				{
					linkedList.AddLast(localizedText.Value);
				}
			}
			return linkedList.ToArray<LocalizedText>();
		}

		// Token: 0x060033AB RID: 13227 RVA: 0x005551E4 File Offset: 0x005533E4
		public LocalizedText SelectRandom(LanguageSearchFilter filter, UnifiedRandom random = null)
		{
			int num = 0;
			foreach (KeyValuePair<string, LocalizedText> localizedText in this._localizedTexts)
			{
				if (filter(localizedText.Key, localizedText.Value))
				{
					num++;
				}
			}
			int num2 = (random ?? Main.rand).Next(num);
			foreach (KeyValuePair<string, LocalizedText> localizedText2 in this._localizedTexts)
			{
				if (filter(localizedText2.Key, localizedText2.Value) && --num == num2)
				{
					return localizedText2.Value;
				}
			}
			return LocalizedText.Empty;
		}

		// Token: 0x060033AC RID: 13228 RVA: 0x005552CC File Offset: 0x005534CC
		public LocalizedText RandomFromCategory(string categoryName, UnifiedRandom random = null)
		{
			if (!this._categoryGroupedKeys.ContainsKey(categoryName))
			{
				return new LocalizedText(categoryName + ".RANDOM", categoryName + ".RANDOM");
			}
			List<string> list = this.GetKeysInCategory(categoryName);
			return this.GetText(categoryName + "." + list[(random ?? Main.rand).Next(list.Count)]);
		}

		// Token: 0x060033AD RID: 13229 RVA: 0x00555338 File Offset: 0x00553538
		public LocalizedText IndexedFromCategory(string categoryName, int index)
		{
			if (!this._categoryGroupedKeys.ContainsKey(categoryName))
			{
				return new LocalizedText(categoryName + ".INDEXED", categoryName + ".INDEXED");
			}
			List<string> list = this.GetKeysInCategory(categoryName);
			int index2 = index % list.Count;
			return this.GetText(categoryName + "." + list[index2]);
		}

		// Token: 0x060033AE RID: 13230 RVA: 0x00555398 File Offset: 0x00553598
		public bool Exists(string key)
		{
			return this._localizedTexts.ContainsKey(key);
		}

		// Token: 0x060033AF RID: 13231 RVA: 0x005553A8 File Offset: 0x005535A8
		public LocalizedText GetText(string key)
		{
			LocalizedText text;
			if (!this._localizedTexts.TryGetValue(key, out text))
			{
				return new LocalizedText(key, key);
			}
			return text;
		}

		// Token: 0x060033B0 RID: 13232 RVA: 0x005553D0 File Offset: 0x005535D0
		public LocalizedText GetOrRegister(string key, Func<string> makeDefaultValue = null)
		{
			LocalizedText text;
			if (!this._localizedTexts.TryGetValue(key, out text))
			{
				this._moddedKeys.Add(key);
				text = (this._localizedTexts[key] = new LocalizedText(key, ((makeDefaultValue != null) ? makeDefaultValue() : null) ?? key));
			}
			return text;
		}

		// Token: 0x060033B1 RID: 13233 RVA: 0x00555420 File Offset: 0x00553620
		public string GetTextValue(string key)
		{
			if (this._localizedTexts.ContainsKey(key))
			{
				return this._localizedTexts[key].Value;
			}
			return key;
		}

		// Token: 0x060033B2 RID: 13234 RVA: 0x00555443 File Offset: 0x00553643
		public string GetTextValue(string key, object arg0)
		{
			if (this._localizedTexts.ContainsKey(key))
			{
				return this._localizedTexts[key].Format(new object[]
				{
					arg0
				});
			}
			return key;
		}

		// Token: 0x060033B3 RID: 13235 RVA: 0x00555470 File Offset: 0x00553670
		public string GetTextValue(string key, object arg0, object arg1)
		{
			if (this._localizedTexts.ContainsKey(key))
			{
				return this._localizedTexts[key].Format(new object[]
				{
					arg0,
					arg1
				});
			}
			return key;
		}

		// Token: 0x060033B4 RID: 13236 RVA: 0x005554A1 File Offset: 0x005536A1
		public string GetTextValue(string key, object arg0, object arg1, object arg2)
		{
			if (this._localizedTexts.ContainsKey(key))
			{
				return this._localizedTexts[key].Format(new object[]
				{
					arg0,
					arg1,
					arg2
				});
			}
			return key;
		}

		// Token: 0x060033B5 RID: 13237 RVA: 0x005554D7 File Offset: 0x005536D7
		public string GetTextValue(string key, params object[] args)
		{
			if (this._localizedTexts.ContainsKey(key))
			{
				return this._localizedTexts[key].Format(args);
			}
			return key;
		}

		// Token: 0x060033B6 RID: 13238 RVA: 0x005554FB File Offset: 0x005536FB
		public void SetFallbackCulture(GameCulture culture)
		{
			this._fallbackCulture = culture;
		}

		// Token: 0x060033B7 RID: 13239 RVA: 0x00555504 File Offset: 0x00553704
		public List<string> GetKeysInCategory(string categoryName)
		{
			return this._categoryGroupedKeys[categoryName];
		}

		// Token: 0x060033B8 RID: 13240 RVA: 0x00555514 File Offset: 0x00553714
		public List<string> GetLocalizedEntriesInCategory(string categoryName)
		{
			List<string> keysInCategory = this.GetKeysInCategory(categoryName);
			List<string> localizedList = new List<string>();
			foreach (string key in keysInCategory)
			{
				localizedList.Add(this.GetText(categoryName + "." + key).Value);
			}
			return localizedList;
		}

		// Token: 0x060033B9 RID: 13241 RVA: 0x00555588 File Offset: 0x00553788
		internal void UnloadModdedEntries()
		{
			foreach (string key in this._moddedKeys)
			{
				this._localizedTexts.Remove(key);
			}
			this._moddedKeys.Clear();
			this.ResetBoundTexts();
			this.ReloadLanguage(true);
		}

		// Token: 0x060033BA RID: 13242 RVA: 0x005555FC File Offset: 0x005537FC
		private void ProcessCopyCommandsInTexts()
		{
			LanguageManager.<>c__DisplayClass48_0 CS$<>8__locals1 = new LanguageManager.<>c__DisplayClass48_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.referenceRegex = new Regex("{\\$([\\w\\.]+)(?:@(\\d+))?}", RegexOptions.Compiled);
			CS$<>8__locals1.argRemappingRegex = new Regex("(?<={\\^?)(\\d+)(?=(?::[^\\r\\n]+?)?})", RegexOptions.Compiled);
			CS$<>8__locals1.processed = new HashSet<LocalizedText>();
			foreach (LocalizedText text in this._localizedTexts.Values)
			{
				CS$<>8__locals1.<ProcessCopyCommandsInTexts>g__Process|0(text);
			}
		}

		// Token: 0x060033BB RID: 13243 RVA: 0x00555690 File Offset: 0x00553890
		internal void ResetBoundTexts()
		{
			this.boundTextCache.Clear();
			this.boundTexts.Clear();
		}

		// Token: 0x060033BC RID: 13244 RVA: 0x005556A8 File Offset: 0x005538A8
		internal LocalizedText BindFormatArgs(string key, object[] args)
		{
			LanguageManager.TextBinding binding = new LanguageManager.TextBinding(key, args);
			LocalizedText text;
			if (this.boundTextCache.TryGetValue(binding, out text))
			{
				return text;
			}
			text = new LocalizedText(key, this.GetTextValue(key));
			text.BindArgs(args);
			this.boundTextCache[binding] = text;
			this.boundTexts.Add(text);
			return text;
		}

		// Token: 0x060033BD RID: 13245 RVA: 0x00555700 File Offset: 0x00553900
		internal void RecalculateBoundTextValues()
		{
			foreach (LocalizedText text in this.boundTexts)
			{
				object[] args = text.BoundArgs;
				text.SetValue(this.GetTextValue(text.Key));
				text.BindArgs(args);
			}
		}

		// Token: 0x04001E48 RID: 7752
		public static LanguageManager Instance = new LanguageManager();

		// Token: 0x04001E49 RID: 7753
		internal readonly Dictionary<string, LocalizedText> _localizedTexts = new Dictionary<string, LocalizedText>();

		// Token: 0x04001E4A RID: 7754
		private readonly Dictionary<string, List<string>> _categoryGroupedKeys = new Dictionary<string, List<string>>();

		// Token: 0x04001E4B RID: 7755
		private GameCulture _fallbackCulture = GameCulture.DefaultCulture;

		// Token: 0x04001E4F RID: 7759
		private IContentSource[] _contentSources = Array.Empty<IContentSource>();

		// Token: 0x04001E50 RID: 7760
		private HashSet<string> _moddedKeys = new HashSet<string>();

		// Token: 0x04001E51 RID: 7761
		private Dictionary<LanguageManager.TextBinding, LocalizedText> boundTextCache = new Dictionary<LanguageManager.TextBinding, LocalizedText>();

		// Token: 0x04001E52 RID: 7762
		private List<LocalizedText> boundTexts = new List<LocalizedText>();

		// Token: 0x02000B1A RID: 2842
		private struct TextBinding : IEquatable<LanguageManager.TextBinding>
		{
			// Token: 0x06005B63 RID: 23395 RVA: 0x006A5B13 File Offset: 0x006A3D13
			public TextBinding(string key, object[] args)
			{
				this.key = key;
				this.args = args;
			}

			// Token: 0x06005B64 RID: 23396 RVA: 0x006A5B23 File Offset: 0x006A3D23
			public bool Equals(LanguageManager.TextBinding other)
			{
				return this.key == other.key && this.args.SequenceEqual(other.args);
			}

			// Token: 0x06005B65 RID: 23397 RVA: 0x006A5B4B File Offset: 0x006A3D4B
			public override bool Equals(object obj)
			{
				return obj is LanguageManager.TextBinding && this.Equals((LanguageManager.TextBinding)obj);
			}

			// Token: 0x06005B66 RID: 23398 RVA: 0x006A5B64 File Offset: 0x006A3D64
			public override int GetHashCode()
			{
				HashCode hash = default(HashCode);
				hash.Add<string>(this.key);
				foreach (object arg in this.args)
				{
					hash.Add<object>(arg);
				}
				return hash.ToHashCode();
			}

			// Token: 0x04006F08 RID: 28424
			public readonly string key;

			// Token: 0x04006F09 RID: 28425
			public readonly object[] args;
		}
	}
}
