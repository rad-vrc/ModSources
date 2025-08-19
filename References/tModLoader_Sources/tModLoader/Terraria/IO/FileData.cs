using System;
using Terraria.Utilities;

namespace Terraria.IO
{
	// Token: 0x020003DC RID: 988
	public abstract class FileData
	{
		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x060033EC RID: 13292 RVA: 0x00556184 File Offset: 0x00554384
		public string Path
		{
			get
			{
				return this._path;
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x060033ED RID: 13293 RVA: 0x0055618C File Offset: 0x0055438C
		public bool IsCloudSave
		{
			get
			{
				return this._isCloudSave;
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x060033EE RID: 13294 RVA: 0x00556194 File Offset: 0x00554394
		public bool IsFavorite
		{
			get
			{
				return this._isFavorite;
			}
		}

		// Token: 0x060033EF RID: 13295 RVA: 0x0055619C File Offset: 0x0055439C
		protected FileData(string type)
		{
			this.Type = type;
		}

		// Token: 0x060033F0 RID: 13296 RVA: 0x005561AB File Offset: 0x005543AB
		protected FileData(string type, string path, bool isCloud)
		{
			this.Type = type;
			this._path = path;
			this._isCloudSave = isCloud;
			this._isFavorite = (isCloud ? Main.CloudFavoritesData : Main.LocalFavoriteData).IsFavorite(this);
		}

		// Token: 0x060033F1 RID: 13297 RVA: 0x005561E3 File Offset: 0x005543E3
		public void ToggleFavorite()
		{
			this.SetFavorite(!this.IsFavorite, true);
		}

		// Token: 0x060033F2 RID: 13298 RVA: 0x005561F5 File Offset: 0x005543F5
		public string GetFileName(bool includeExtension = true)
		{
			return FileUtilities.GetFileName(this.Path, includeExtension);
		}

		// Token: 0x060033F3 RID: 13299 RVA: 0x00556203 File Offset: 0x00554403
		public void SetFavorite(bool favorite, bool saveChanges = true)
		{
			this._isFavorite = favorite;
			if (saveChanges)
			{
				(this.IsCloudSave ? Main.CloudFavoritesData : Main.LocalFavoriteData).SaveFavorite(this);
			}
		}

		// Token: 0x060033F4 RID: 13300
		public abstract void SetAsActive();

		// Token: 0x060033F5 RID: 13301
		public abstract void MoveToCloud();

		// Token: 0x060033F6 RID: 13302
		public abstract void MoveToLocal();

		// Token: 0x04001E62 RID: 7778
		protected string _path;

		// Token: 0x04001E63 RID: 7779
		protected bool _isCloudSave;

		// Token: 0x04001E64 RID: 7780
		public FileMetadata Metadata;

		// Token: 0x04001E65 RID: 7781
		public string Name;

		// Token: 0x04001E66 RID: 7782
		public readonly string Type;

		// Token: 0x04001E67 RID: 7783
		protected bool _isFavorite;
	}
}
