using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Linq;
using Terraria.Localization;

namespace Terraria.IO
{
	// Token: 0x020000DC RID: 220
	public class Preferences
	{
		// Token: 0x1400002A RID: 42
		// (add) Token: 0x0600150E RID: 5390 RVA: 0x004AF490 File Offset: 0x004AD690
		// (remove) Token: 0x0600150F RID: 5391 RVA: 0x004AF4C8 File Offset: 0x004AD6C8
		public event Action<Preferences> OnSave;

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x06001510 RID: 5392 RVA: 0x004AF500 File Offset: 0x004AD700
		// (remove) Token: 0x06001511 RID: 5393 RVA: 0x004AF538 File Offset: 0x004AD738
		public event Action<Preferences> OnLoad;

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x06001512 RID: 5394 RVA: 0x004AF570 File Offset: 0x004AD770
		// (remove) Token: 0x06001513 RID: 5395 RVA: 0x004AF5A8 File Offset: 0x004AD7A8
		public event Preferences.TextProcessAction OnProcessText;

		// Token: 0x06001514 RID: 5396 RVA: 0x004AF5E0 File Offset: 0x004AD7E0
		public Preferences(string path, bool parseAllTypes = false, bool useBson = false)
		{
			this._path = path;
			this.UseBson = useBson;
			if (parseAllTypes)
			{
				this._serializerSettings = new JsonSerializerSettings
				{
					TypeNameHandling = 4,
					MetadataPropertyHandling = 1,
					Formatting = 1
				};
				return;
			}
			this._serializerSettings = new JsonSerializerSettings
			{
				Formatting = 1
			};
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x004AF650 File Offset: 0x004AD850
		public bool Load()
		{
			object @lock = this._lock;
			bool result;
			lock (@lock)
			{
				if (!File.Exists(this._path))
				{
					result = false;
				}
				else
				{
					try
					{
						if (!this.UseBson)
						{
							string text = File.ReadAllText(this._path);
							this._data = JsonConvert.DeserializeObject<Dictionary<string, object>>(text, this._serializerSettings);
						}
						else
						{
							using (FileStream fileStream = File.OpenRead(this._path))
							{
								using (BsonReader bsonReader = new BsonReader(fileStream))
								{
									JsonSerializer jsonSerializer = JsonSerializer.Create(this._serializerSettings);
									this._data = jsonSerializer.Deserialize<Dictionary<string, object>>(bsonReader);
								}
							}
						}
						if (this._data == null)
						{
							this._data = new Dictionary<string, object>();
						}
						if (this.OnLoad != null)
						{
							this.OnLoad(this);
						}
						result = true;
					}
					catch (Exception)
					{
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x004AF768 File Offset: 0x004AD968
		public bool Save(bool canCreateFile = true)
		{
			object @lock = this._lock;
			bool result;
			lock (@lock)
			{
				try
				{
					if (this.OnSave != null)
					{
						this.OnSave(this);
					}
					if (!canCreateFile && !File.Exists(this._path))
					{
						return false;
					}
					Directory.GetParent(this._path).Create();
					if (File.Exists(this._path))
					{
						File.SetAttributes(this._path, FileAttributes.Normal);
					}
					if (!this.UseBson)
					{
						string contents = JsonConvert.SerializeObject(this._data, this._serializerSettings);
						if (this.OnProcessText != null)
						{
							this.OnProcessText(ref contents);
						}
						File.WriteAllText(this._path, contents);
						File.SetAttributes(this._path, FileAttributes.Normal);
					}
					else
					{
						using (FileStream fileStream = File.Create(this._path))
						{
							using (BsonWriter bsonWriter = new BsonWriter(fileStream))
							{
								File.SetAttributes(this._path, FileAttributes.Normal);
								JsonSerializer.Create(this._serializerSettings).Serialize(bsonWriter, this._data);
							}
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(Language.GetTextValue("Error.UnableToWritePreferences", this._path));
					Console.WriteLine(ex.ToString());
					return false;
				}
				result = true;
			}
			return result;
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x004AF920 File Offset: 0x004ADB20
		public void Clear()
		{
			this._data.Clear();
		}

		// Token: 0x06001518 RID: 5400 RVA: 0x004AF930 File Offset: 0x004ADB30
		public void Put(string name, object value)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				this._data[name] = value;
				if (this.AutoSave)
				{
					this.Save(true);
				}
			}
		}

		// Token: 0x06001519 RID: 5401 RVA: 0x004AF988 File Offset: 0x004ADB88
		public bool Contains(string name)
		{
			object @lock = this._lock;
			bool result;
			lock (@lock)
			{
				result = this._data.ContainsKey(name);
			}
			return result;
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x004AF9D0 File Offset: 0x004ADBD0
		public T Get<T>(string name, T defaultValue)
		{
			object @lock = this._lock;
			T result;
			lock (@lock)
			{
				try
				{
					object obj;
					if (this._data.TryGetValue(name, out obj))
					{
						if (obj is T)
						{
							result = (T)((object)obj);
						}
						else if (obj is JObject)
						{
							result = JsonConvert.DeserializeObject<T>(((JObject)obj).ToString());
						}
						else
						{
							result = (T)((object)Convert.ChangeType(obj, typeof(T)));
						}
					}
					else
					{
						result = defaultValue;
					}
				}
				catch
				{
					result = defaultValue;
				}
			}
			return result;
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x004AFA74 File Offset: 0x004ADC74
		public void Get<T>(string name, ref T currentValue)
		{
			currentValue = this.Get<T>(name, currentValue);
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x004AFA89 File Offset: 0x004ADC89
		public List<string> GetAllKeys()
		{
			return this._data.Keys.ToList<string>();
		}

		// Token: 0x04001290 RID: 4752
		private Dictionary<string, object> _data = new Dictionary<string, object>();

		// Token: 0x04001291 RID: 4753
		private readonly string _path;

		// Token: 0x04001292 RID: 4754
		private readonly JsonSerializerSettings _serializerSettings;

		// Token: 0x04001293 RID: 4755
		public readonly bool UseBson;

		// Token: 0x04001294 RID: 4756
		private readonly object _lock = new object();

		// Token: 0x04001295 RID: 4757
		public bool AutoSave;

		// Token: 0x0200056B RID: 1387
		// (Invoke) Token: 0x06003153 RID: 12627
		public delegate void TextProcessAction(ref string text);
	}
}
