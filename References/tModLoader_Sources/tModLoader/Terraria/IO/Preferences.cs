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
	// Token: 0x020003E1 RID: 993
	public class Preferences
	{
		// Token: 0x14000058 RID: 88
		// (add) Token: 0x06003413 RID: 13331 RVA: 0x005568E0 File Offset: 0x00554AE0
		// (remove) Token: 0x06003414 RID: 13332 RVA: 0x00556918 File Offset: 0x00554B18
		public event Action<Preferences> OnSave;

		// Token: 0x14000059 RID: 89
		// (add) Token: 0x06003415 RID: 13333 RVA: 0x00556950 File Offset: 0x00554B50
		// (remove) Token: 0x06003416 RID: 13334 RVA: 0x00556988 File Offset: 0x00554B88
		public event Action<Preferences> OnLoad;

		// Token: 0x1400005A RID: 90
		// (add) Token: 0x06003417 RID: 13335 RVA: 0x005569C0 File Offset: 0x00554BC0
		// (remove) Token: 0x06003418 RID: 13336 RVA: 0x005569F8 File Offset: 0x00554BF8
		public event Preferences.TextProcessAction OnProcessText;

		// Token: 0x06003419 RID: 13337 RVA: 0x00556A30 File Offset: 0x00554C30
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

		// Token: 0x0600341A RID: 13338 RVA: 0x00556AA0 File Offset: 0x00554CA0
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
							string value = File.ReadAllText(this._path);
							this._data = JsonConvert.DeserializeObject<Dictionary<string, object>>(value, this._serializerSettings);
						}
						else
						{
							using (FileStream stream = File.OpenRead(this._path))
							{
								using (BsonReader reader = new BsonReader(stream))
								{
									JsonSerializer jsonSerializer = JsonSerializer.Create(this._serializerSettings);
									this._data = jsonSerializer.Deserialize<Dictionary<string, object>>(reader);
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

		// Token: 0x0600341B RID: 13339 RVA: 0x00556BB8 File Offset: 0x00554DB8
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
						string text = JsonConvert.SerializeObject(this._data, this._serializerSettings);
						if (this.OnProcessText != null)
						{
							this.OnProcessText(ref text);
						}
						string text2 = this._path + ".bak";
						File.WriteAllText(text2, text);
						File.Move(text2, this._path, true);
						File.Delete(text2);
						File.SetAttributes(this._path, FileAttributes.Normal);
					}
					else
					{
						using (FileStream stream = File.Create(this._path))
						{
							using (BsonWriter jsonWriter = new BsonWriter(stream))
							{
								File.SetAttributes(this._path, FileAttributes.Normal);
								JsonSerializer.Create(this._serializerSettings).Serialize(jsonWriter, this._data);
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

		// Token: 0x0600341C RID: 13340 RVA: 0x00556D8C File Offset: 0x00554F8C
		public void Clear()
		{
			this._data.Clear();
		}

		// Token: 0x0600341D RID: 13341 RVA: 0x00556D9C File Offset: 0x00554F9C
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

		// Token: 0x0600341E RID: 13342 RVA: 0x00556DF4 File Offset: 0x00554FF4
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

		// Token: 0x0600341F RID: 13343 RVA: 0x00556E3C File Offset: 0x0055503C
		public T Get<T>(string name, T defaultValue)
		{
			object @lock = this._lock;
			T result;
			lock (@lock)
			{
				try
				{
					object value;
					if (this._data.TryGetValue(name, out value))
					{
						if (value is T)
						{
							result = (T)((object)value);
						}
						else if (value is JObject)
						{
							result = JsonConvert.DeserializeObject<T>(((JObject)value).ToString());
						}
						else
						{
							result = (T)((object)Convert.ChangeType(value, typeof(T)));
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

		// Token: 0x06003420 RID: 13344 RVA: 0x00556EE0 File Offset: 0x005550E0
		public void Get<T>(string name, ref T currentValue)
		{
			currentValue = this.Get<T>(name, currentValue);
		}

		// Token: 0x06003421 RID: 13345 RVA: 0x00556EF5 File Offset: 0x005550F5
		public List<string> GetAllKeys()
		{
			return this._data.Keys.ToList<string>();
		}

		// Token: 0x04001E79 RID: 7801
		private Dictionary<string, object> _data = new Dictionary<string, object>();

		// Token: 0x04001E7A RID: 7802
		private readonly string _path;

		// Token: 0x04001E7B RID: 7803
		private readonly JsonSerializerSettings _serializerSettings;

		// Token: 0x04001E7C RID: 7804
		public readonly bool UseBson;

		// Token: 0x04001E7D RID: 7805
		private readonly object _lock = new object();

		// Token: 0x04001E7E RID: 7806
		public bool AutoSave;

		// Token: 0x02000B24 RID: 2852
		// (Invoke) Token: 0x06005B79 RID: 23417
		public delegate void TextProcessAction(ref string text);
	}
}
