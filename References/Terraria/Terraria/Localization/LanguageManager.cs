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
using Terraria.Utilities;

namespace Terraria.Localization
{
	// Token: 0x020000AD RID: 173
	public class LanguageManager
	{
		// Token: 0x14000028 RID: 40
		// (add) Token: 0x060013A3 RID: 5027 RVA: 0x0049FEB8 File Offset: 0x0049E0B8
		// (remove) Token: 0x060013A4 RID: 5028 RVA: 0x0049FEF0 File Offset: 0x0049E0F0
		public event LanguageChangeCallback OnLanguageChanging;

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x060013A5 RID: 5029 RVA: 0x0049FF28 File Offset: 0x0049E128
		// (remove) Token: 0x060013A6 RID: 5030 RVA: 0x0049FF60 File Offset: 0x0049E160
		public event LanguageChangeCallback OnLanguageChanged;

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060013A7 RID: 5031 RVA: 0x0049FF95 File Offset: 0x0049E195
		// (set) Token: 0x060013A8 RID: 5032 RVA: 0x0049FF9D File Offset: 0x0049E19D
		public GameCulture ActiveCulture { get; private set; }

		// Token: 0x060013A9 RID: 5033 RVA: 0x0049FFA6 File Offset: 0x0049E1A6
		private LanguageManager()
		{
			this._localizedTexts[""] = LocalizedText.Empty;
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x0049FFE4 File Offset: 0x0049E1E4
		public int GetCategorySize(string name)
		{
			if (this._categoryGroupedKeys.ContainsKey(name))
			{
				return this._categoryGroupedKeys[name].Count;
			}
			return 0;
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x004A0008 File Offset: 0x0049E208
		public void SetLanguage(int legacyId)
		{
			GameCulture language = GameCulture.FromLegacyId(legacyId);
			this.SetLanguage(language);
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x004A0024 File Offset: 0x0049E224
		public void SetLanguage(string cultureName)
		{
			GameCulture language = GameCulture.FromName(cultureName);
			this.SetLanguage(language);
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x004A0040 File Offset: 0x0049E240
		public int EstimateWordCount()
		{
			int num = 0;
			foreach (string key in this._localizedTexts.Keys)
			{
				string textValue = this.GetTextValue(key);
				textValue.Replace(",", "").Replace(".", "").Replace("\"", "").Trim();
				string[] array = textValue.Split(new char[]
				{
					' '
				});
				string[] array2 = textValue.Split(new char[]
				{
					' '
				});
				if (array.Length != array2.Length)
				{
					break;
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

		// Token: 0x060013AE RID: 5038 RVA: 0x004A0140 File Offset: 0x0049E340
		private void SetAllTextValuesToKeys()
		{
			foreach (KeyValuePair<string, LocalizedText> keyValuePair in this._localizedTexts)
			{
				keyValuePair.Value.SetValue(keyValuePair.Key);
			}
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x004A01A0 File Offset: 0x0049E3A0
		private string[] GetLanguageFilesForCulture(GameCulture culture)
		{
			Assembly.GetExecutingAssembly();
			return Array.FindAll<string>(typeof(Program).Assembly.GetManifestResourceNames(), (string element) => element.StartsWith("Terraria.Localization.Content." + culture.CultureInfo.Name) && element.EndsWith(".json"));
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x004A01E8 File Offset: 0x0049E3E8
		public void SetLanguage(GameCulture culture)
		{
			if (this.ActiveCulture == culture)
			{
				return;
			}
			if (culture != this._fallbackCulture && this.ActiveCulture != this._fallbackCulture)
			{
				this.SetAllTextValuesToKeys();
				this.LoadLanguage(this._fallbackCulture, true);
			}
			this.LoadLanguage(culture, true);
			this.ActiveCulture = culture;
			Thread.CurrentThread.CurrentCulture = culture.CultureInfo;
			Thread.CurrentThread.CurrentUICulture = culture.CultureInfo;
			if (this.OnLanguageChanged != null)
			{
				this.OnLanguageChanged(this);
			}
			Asset<DynamicSpriteFont> deathText = FontAssets.DeathText;
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x004A0272 File Offset: 0x0049E472
		private void LoadLanguage(GameCulture culture, bool processCopyCommands = true)
		{
			this.LoadFilesForCulture(culture);
			if (this.OnLanguageChanging != null)
			{
				this.OnLanguageChanging(this);
			}
			if (processCopyCommands)
			{
				this.ProcessCopyCommandsInTexts();
			}
			ChatInitializer.PrepareAliases();
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x004A02A0 File Offset: 0x0049E4A0
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
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x004A031C File Offset: 0x0049E51C
		private void ProcessCopyCommandsInTexts()
		{
			Regex regex = new Regex("{\\$(\\w+\\.\\w+)}", RegexOptions.Compiled);
			foreach (KeyValuePair<string, LocalizedText> keyValuePair in this._localizedTexts)
			{
				LocalizedText value = keyValuePair.Value;
				for (int i = 0; i < 100; i++)
				{
					string text = regex.Replace(value.Value, (Match match) => this.GetTextValue(match.Groups[1].ToString()));
					if (text == value.Value)
					{
						break;
					}
					value.SetValue(text);
				}
			}
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x004A03C0 File Offset: 0x0049E5C0
		public void UseSources(List<IContentSource> sourcesFromLowestToHighest)
		{
			string name = this.ActiveCulture.Name;
			string text = ("Localization" + Path.DirectorySeparatorChar.ToString() + name).ToLower();
			this.LoadLanguage(this.ActiveCulture, false);
			foreach (IContentSource contentSource in sourcesFromLowestToHighest)
			{
				foreach (string text2 in contentSource.GetAllAssetsStartingWith(text))
				{
					string extension = contentSource.GetExtension(text2);
					if (extension == ".json" || extension == ".csv")
					{
						using (Stream stream = contentSource.OpenStream(text2))
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
				}
			}
			this.ProcessCopyCommandsInTexts();
			ChatInitializer.PrepareAliases();
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x004A053C File Offset: 0x0049E73C
		public void LoadLanguageFromFileTextCsv(string fileText)
		{
			using (TextReader textReader = new StringReader(fileText))
			{
				using (CsvReader csvReader = new CsvReader(textReader))
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
									string text = currentRecord[num];
									string value = currentRecord[num2];
									if (!string.IsNullOrWhiteSpace(text) && !string.IsNullOrWhiteSpace(value) && this._localizedTexts.ContainsKey(text))
									{
										this._localizedTexts[text].SetValue(value);
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x004A065C File Offset: 0x0049E85C
		public void LoadLanguageFromFileTextJson(string fileText, bool canCreateCategories)
		{
			foreach (KeyValuePair<string, Dictionary<string, string>> keyValuePair in JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(fileText))
			{
				string key = keyValuePair.Key;
				foreach (KeyValuePair<string, string> keyValuePair2 in keyValuePair.Value)
				{
					string key2 = keyValuePair.Key + "." + keyValuePair2.Key;
					if (this._localizedTexts.ContainsKey(key2))
					{
						this._localizedTexts[key2].SetValue(keyValuePair2.Value);
					}
					else if (canCreateCategories)
					{
						this._localizedTexts.Add(key2, new LocalizedText(key2, keyValuePair2.Value));
						if (!this._categoryGroupedKeys.ContainsKey(keyValuePair.Key))
						{
							this._categoryGroupedKeys.Add(keyValuePair.Key, new List<string>());
						}
						this._categoryGroupedKeys[keyValuePair.Key].Add(keyValuePair2.Key);
					}
				}
			}
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x004A07BC File Offset: 0x0049E9BC
		[Conditional("DEBUG")]
		private void ValidateAllCharactersContainedInFont(DynamicSpriteFont font)
		{
			if (font == null)
			{
				return;
			}
			string text = "";
			foreach (LocalizedText localizedText in this._localizedTexts.Values)
			{
				foreach (char c in localizedText.Value)
				{
					if (!font.IsCharacterSupported(c))
					{
						text = string.Concat(new object[]
						{
							text,
							localizedText.Key,
							", ",
							c.ToString(),
							", ",
							(int)c,
							"\n"
						});
					}
				}
			}
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x004A0890 File Offset: 0x0049EA90
		public LocalizedText[] FindAll(Regex regex)
		{
			int num = 0;
			foreach (KeyValuePair<string, LocalizedText> keyValuePair in this._localizedTexts)
			{
				if (regex.IsMatch(keyValuePair.Key))
				{
					num++;
				}
			}
			LocalizedText[] array = new LocalizedText[num];
			int num2 = 0;
			foreach (KeyValuePair<string, LocalizedText> keyValuePair2 in this._localizedTexts)
			{
				if (regex.IsMatch(keyValuePair2.Key))
				{
					array[num2] = keyValuePair2.Value;
					num2++;
				}
			}
			return array;
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x004A0958 File Offset: 0x0049EB58
		public LocalizedText[] FindAll(LanguageSearchFilter filter)
		{
			LinkedList<LocalizedText> linkedList = new LinkedList<LocalizedText>();
			foreach (KeyValuePair<string, LocalizedText> keyValuePair in this._localizedTexts)
			{
				if (filter(keyValuePair.Key, keyValuePair.Value))
				{
					linkedList.AddLast(keyValuePair.Value);
				}
			}
			return linkedList.ToArray<LocalizedText>();
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x004A09D4 File Offset: 0x0049EBD4
		public LocalizedText SelectRandom(LanguageSearchFilter filter, UnifiedRandom random = null)
		{
			int num = 0;
			foreach (KeyValuePair<string, LocalizedText> keyValuePair in this._localizedTexts)
			{
				if (filter(keyValuePair.Key, keyValuePair.Value))
				{
					num++;
				}
			}
			int num2 = (random ?? Main.rand).Next(num);
			foreach (KeyValuePair<string, LocalizedText> keyValuePair2 in this._localizedTexts)
			{
				if (filter(keyValuePair2.Key, keyValuePair2.Value) && --num == num2)
				{
					return keyValuePair2.Value;
				}
			}
			return LocalizedText.Empty;
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x004A0ABC File Offset: 0x0049ECBC
		public LocalizedText RandomFromCategory(string categoryName, UnifiedRandom random = null)
		{
			if (!this._categoryGroupedKeys.ContainsKey(categoryName))
			{
				return new LocalizedText(categoryName + ".RANDOM", categoryName + ".RANDOM");
			}
			List<string> list = this._categoryGroupedKeys[categoryName];
			return this.GetText(categoryName + "." + list[(random ?? Main.rand).Next(list.Count)]);
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x004A0B2C File Offset: 0x0049ED2C
		public LocalizedText IndexedFromCategory(string categoryName, int index)
		{
			if (!this._categoryGroupedKeys.ContainsKey(categoryName))
			{
				return new LocalizedText(categoryName + ".INDEXED", categoryName + ".INDEXED");
			}
			List<string> list = this._categoryGroupedKeys[categoryName];
			int index2 = index % list.Count;
			return this.GetText(categoryName + "." + list[index2]);
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x004A0B91 File Offset: 0x0049ED91
		public bool Exists(string key)
		{
			return this._localizedTexts.ContainsKey(key);
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x004A0B9F File Offset: 0x0049ED9F
		public LocalizedText GetText(string key)
		{
			if (!this._localizedTexts.ContainsKey(key))
			{
				return new LocalizedText(key, key);
			}
			return this._localizedTexts[key];
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x004A0BC3 File Offset: 0x0049EDC3
		public string GetTextValue(string key)
		{
			if (this._localizedTexts.ContainsKey(key))
			{
				return this._localizedTexts[key].Value;
			}
			return key;
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x004A0BE6 File Offset: 0x0049EDE6
		public string GetTextValue(string key, object arg0)
		{
			if (this._localizedTexts.ContainsKey(key))
			{
				return this._localizedTexts[key].Format(arg0);
			}
			return key;
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x004A0C0A File Offset: 0x0049EE0A
		public string GetTextValue(string key, object arg0, object arg1)
		{
			if (this._localizedTexts.ContainsKey(key))
			{
				return this._localizedTexts[key].Format(arg0, arg1);
			}
			return key;
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x004A0C2F File Offset: 0x0049EE2F
		public string GetTextValue(string key, object arg0, object arg1, object arg2)
		{
			if (this._localizedTexts.ContainsKey(key))
			{
				return this._localizedTexts[key].Format(arg0, arg1, arg2);
			}
			return key;
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x004A0C56 File Offset: 0x0049EE56
		public string GetTextValue(string key, params object[] args)
		{
			if (this._localizedTexts.ContainsKey(key))
			{
				return this._localizedTexts[key].Format(args);
			}
			return key;
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x004A0C7A File Offset: 0x0049EE7A
		public void SetFallbackCulture(GameCulture culture)
		{
			this._fallbackCulture = culture;
		}

		// Token: 0x040011B6 RID: 4534
		public static LanguageManager Instance = new LanguageManager();

		// Token: 0x040011BA RID: 4538
		private readonly Dictionary<string, LocalizedText> _localizedTexts = new Dictionary<string, LocalizedText>();

		// Token: 0x040011BB RID: 4539
		private readonly Dictionary<string, List<string>> _categoryGroupedKeys = new Dictionary<string, List<string>>();

		// Token: 0x040011BC RID: 4540
		private GameCulture _fallbackCulture = GameCulture.DefaultCulture;
	}
}
