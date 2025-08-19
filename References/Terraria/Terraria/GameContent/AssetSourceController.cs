using System;
using System.Collections.Generic;
using System.Linq;
using ReLogic.Content;
using ReLogic.Content.Sources;
using Terraria.Audio;
using Terraria.IO;
using Terraria.Localization;

namespace Terraria.GameContent
{
	// Token: 0x020001F9 RID: 505
	public class AssetSourceController
	{
		// Token: 0x14000042 RID: 66
		// (add) Token: 0x06001D0D RID: 7437 RVA: 0x004FFCE8 File Offset: 0x004FDEE8
		// (remove) Token: 0x06001D0E RID: 7438 RVA: 0x004FFD20 File Offset: 0x004FDF20
		public event Action<ResourcePackList> OnResourcePackChange;

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06001D0F RID: 7439 RVA: 0x004FFD55 File Offset: 0x004FDF55
		// (set) Token: 0x06001D10 RID: 7440 RVA: 0x004FFD5D File Offset: 0x004FDF5D
		public ResourcePackList ActiveResourcePackList { get; private set; }

		// Token: 0x06001D11 RID: 7441 RVA: 0x004FFD66 File Offset: 0x004FDF66
		public AssetSourceController(IAssetRepository assetRepository, IEnumerable<IContentSource> staticSources)
		{
			this._assetRepository = assetRepository;
			this._staticSources = staticSources.ToList<IContentSource>();
			this.UseResourcePacks(new ResourcePackList());
		}

		// Token: 0x06001D12 RID: 7442 RVA: 0x004FFD8C File Offset: 0x004FDF8C
		public void Refresh()
		{
			foreach (ResourcePack resourcePack in this.ActiveResourcePackList.AllPacks)
			{
				resourcePack.Refresh();
			}
			this.UseResourcePacks(this.ActiveResourcePackList);
		}

		// Token: 0x06001D13 RID: 7443 RVA: 0x004FFDE8 File Offset: 0x004FDFE8
		public void UseResourcePacks(ResourcePackList resourcePacks)
		{
			if (this.OnResourcePackChange != null)
			{
				this.OnResourcePackChange(resourcePacks);
			}
			this.ActiveResourcePackList = resourcePacks;
			List<IContentSource> list = new List<IContentSource>(from pack in resourcePacks.EnabledPacks
			orderby pack.SortingOrder
			select pack.GetContentSource());
			list.AddRange(this._staticSources);
			foreach (IContentSource contentSource in list)
			{
				contentSource.ClearRejections();
			}
			List<IContentSource> list2 = new List<IContentSource>();
			for (int i = list.Count - 1; i >= 0; i--)
			{
				list2.Add(list[i]);
			}
			this._assetRepository.SetSources(list, 1);
			LanguageManager.Instance.UseSources(list2);
			Main.audioSystem.UseSources(list2);
			SoundEngine.Reload();
			Main.changeTheTitle = true;
		}

		// Token: 0x040043F6 RID: 17398
		private readonly List<IContentSource> _staticSources;

		// Token: 0x040043F7 RID: 17399
		private readonly IAssetRepository _assetRepository;
	}
}
