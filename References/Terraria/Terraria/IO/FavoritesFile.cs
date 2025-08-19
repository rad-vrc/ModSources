using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Terraria.UI;
using Terraria.Utilities;

namespace Terraria.IO
{
	// Token: 0x020000D6 RID: 214
	public class FavoritesFile
	{
		// Token: 0x060014CC RID: 5324 RVA: 0x004AE6C3 File Offset: 0x004AC8C3
		public FavoritesFile(string path, bool isCloud)
		{
			this.Path = path;
			this.IsCloudSave = isCloud;
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x004AE6F4 File Offset: 0x004AC8F4
		public void SaveFavorite(FileData fileData)
		{
			if (!this._data.ContainsKey(fileData.Type))
			{
				this._data.Add(fileData.Type, new Dictionary<string, bool>());
			}
			this._data[fileData.Type][fileData.GetFileName(true)] = fileData.IsFavorite;
			this.Save();
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x004AE753 File Offset: 0x004AC953
		public void ClearEntry(FileData fileData)
		{
			if (!this._data.ContainsKey(fileData.Type))
			{
				return;
			}
			this._data[fileData.Type].Remove(fileData.GetFileName(true));
			this.Save();
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x004AE790 File Offset: 0x004AC990
		public bool IsFavorite(FileData fileData)
		{
			if (!this._data.ContainsKey(fileData.Type))
			{
				return false;
			}
			string fileName = fileData.GetFileName(true);
			bool flag;
			return this._data[fileData.Type].TryGetValue(fileName, out flag) && flag;
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x004AE7D8 File Offset: 0x004AC9D8
		public void Save()
		{
			try
			{
				string s = JsonConvert.SerializeObject(this._data, 1);
				byte[] bytes = this._ourEncoder.GetBytes(s);
				FileUtilities.WriteAllBytes(this.Path, bytes, this.IsCloudSave);
			}
			catch (Exception exception)
			{
				FancyErrorPrinter.ShowFileSavingFailError(exception, this.Path);
				throw;
			}
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x004AE830 File Offset: 0x004ACA30
		public void Load()
		{
			if (!FileUtilities.Exists(this.Path, this.IsCloudSave))
			{
				this._data.Clear();
				return;
			}
			try
			{
				byte[] bytes = FileUtilities.ReadAllBytes(this.Path, this.IsCloudSave);
				string @string;
				try
				{
					@string = this._ourEncoder.GetString(bytes);
				}
				catch
				{
					@string = Encoding.ASCII.GetString(bytes);
				}
				this._data = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, bool>>>(@string);
				if (this._data == null)
				{
					this._data = new Dictionary<string, Dictionary<string, bool>>();
				}
			}
			catch (Exception)
			{
				Console.WriteLine("Unable to load favorites.json file ({0} : {1})", this.Path, this.IsCloudSave ? "Cloud Save" : "Local Save");
			}
		}

		// Token: 0x04001261 RID: 4705
		public readonly string Path;

		// Token: 0x04001262 RID: 4706
		public readonly bool IsCloudSave;

		// Token: 0x04001263 RID: 4707
		private Dictionary<string, Dictionary<string, bool>> _data = new Dictionary<string, Dictionary<string, bool>>();

		// Token: 0x04001264 RID: 4708
		private UTF8Encoding _ourEncoder = new UTF8Encoding(true, true);
	}
}
