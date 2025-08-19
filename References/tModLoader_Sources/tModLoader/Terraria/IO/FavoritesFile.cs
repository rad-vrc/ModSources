using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Terraria.UI;
using Terraria.Utilities;

namespace Terraria.IO
{
	// Token: 0x020003DB RID: 987
	public class FavoritesFile
	{
		// Token: 0x060033E6 RID: 13286 RVA: 0x00555F59 File Offset: 0x00554159
		public FavoritesFile(string path, bool isCloud)
		{
			this.Path = path;
			this.IsCloudSave = isCloud;
		}

		// Token: 0x060033E7 RID: 13287 RVA: 0x00555F88 File Offset: 0x00554188
		public void SaveFavorite(FileData fileData)
		{
			if (!this._data.ContainsKey(fileData.Type))
			{
				this._data.Add(fileData.Type, new Dictionary<string, bool>());
			}
			this._data[fileData.Type][fileData.GetFileName(true)] = fileData.IsFavorite;
			this.Save();
		}

		// Token: 0x060033E8 RID: 13288 RVA: 0x00555FE7 File Offset: 0x005541E7
		public void ClearEntry(FileData fileData)
		{
			if (this._data.ContainsKey(fileData.Type))
			{
				this._data[fileData.Type].Remove(fileData.GetFileName(true));
				this.Save();
			}
		}

		// Token: 0x060033E9 RID: 13289 RVA: 0x00556020 File Offset: 0x00554220
		public bool IsFavorite(FileData fileData)
		{
			if (!this._data.ContainsKey(fileData.Type))
			{
				return false;
			}
			string fileName = fileData.GetFileName(true);
			bool value;
			return this._data[fileData.Type].TryGetValue(fileName, out value) && value;
		}

		// Token: 0x060033EA RID: 13290 RVA: 0x00556068 File Offset: 0x00554268
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

		// Token: 0x060033EB RID: 13291 RVA: 0x005560C0 File Offset: 0x005542C0
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

		// Token: 0x04001E5E RID: 7774
		public readonly string Path;

		// Token: 0x04001E5F RID: 7775
		public readonly bool IsCloudSave;

		// Token: 0x04001E60 RID: 7776
		private Dictionary<string, Dictionary<string, bool>> _data = new Dictionary<string, Dictionary<string, bool>>();

		// Token: 0x04001E61 RID: 7777
		private UTF8Encoding _ourEncoder = new UTF8Encoding(true, true);
	}
}
