using System;
using Terraria.Utilities;

namespace Terraria.IO
{
	// Token: 0x020000D7 RID: 215
	public abstract class FileData
	{
		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060014D2 RID: 5330 RVA: 0x004AE8F4 File Offset: 0x004ACAF4
		public string Path
		{
			get
			{
				return this._path;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060014D3 RID: 5331 RVA: 0x004AE8FC File Offset: 0x004ACAFC
		public bool IsCloudSave
		{
			get
			{
				return this._isCloudSave;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060014D4 RID: 5332 RVA: 0x004AE904 File Offset: 0x004ACB04
		public bool IsFavorite
		{
			get
			{
				return this._isFavorite;
			}
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x004AE90C File Offset: 0x004ACB0C
		protected FileData(string type)
		{
			this.Type = type;
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x004AE91B File Offset: 0x004ACB1B
		protected FileData(string type, string path, bool isCloud)
		{
			this.Type = type;
			this._path = path;
			this._isCloudSave = isCloud;
			this._isFavorite = (isCloud ? Main.CloudFavoritesData : Main.LocalFavoriteData).IsFavorite(this);
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x004AE953 File Offset: 0x004ACB53
		public void ToggleFavorite()
		{
			this.SetFavorite(!this.IsFavorite, true);
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x004AE965 File Offset: 0x004ACB65
		public string GetFileName(bool includeExtension = true)
		{
			return FileUtilities.GetFileName(this.Path, includeExtension);
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x004AE973 File Offset: 0x004ACB73
		public void SetFavorite(bool favorite, bool saveChanges = true)
		{
			this._isFavorite = favorite;
			if (saveChanges)
			{
				(this.IsCloudSave ? Main.CloudFavoritesData : Main.LocalFavoriteData).SaveFavorite(this);
			}
		}

		// Token: 0x060014DA RID: 5338
		public abstract void SetAsActive();

		// Token: 0x060014DB RID: 5339
		public abstract void MoveToCloud();

		// Token: 0x060014DC RID: 5340
		public abstract void MoveToLocal();

		// Token: 0x04001265 RID: 4709
		protected string _path;

		// Token: 0x04001266 RID: 4710
		protected bool _isCloudSave;

		// Token: 0x04001267 RID: 4711
		public FileMetadata Metadata;

		// Token: 0x04001268 RID: 4712
		public string Name;

		// Token: 0x04001269 RID: 4713
		public readonly string Type;

		// Token: 0x0400126A RID: 4714
		protected bool _isFavorite;
	}
}
