using System;
using System.Collections.Generic;
using System.Linq;
using ReLogic.Content;
using ReLogic.Content.Sources;
using Terraria.Audio;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terraria.GameContent
{
	// Token: 0x0200048D RID: 1165
	public class AssetSourceController
	{
		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x060038CD RID: 14541 RVA: 0x005947E2 File Offset: 0x005929E2
		internal IContentSource StaticSource
		{
			get
			{
				return this._staticSources.Single<IContentSource>();
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x060038CE RID: 14542 RVA: 0x005947EF File Offset: 0x005929EF
		// (set) Token: 0x060038CF RID: 14543 RVA: 0x005947F7 File Offset: 0x005929F7
		public ResourcePackList ActiveResourcePackList { get; private set; }

		// Token: 0x1400005F RID: 95
		// (add) Token: 0x060038D0 RID: 14544 RVA: 0x00594800 File Offset: 0x00592A00
		// (remove) Token: 0x060038D1 RID: 14545 RVA: 0x00594838 File Offset: 0x00592A38
		public event Action<ResourcePackList> OnResourcePackChange;

		// Token: 0x060038D2 RID: 14546 RVA: 0x0059486D File Offset: 0x00592A6D
		public AssetSourceController(IAssetRepository assetRepository, IEnumerable<IContentSource> staticSources)
		{
			this._assetRepository = assetRepository;
			this._staticSources = staticSources.ToList<IContentSource>();
			this.UseResourcePacks(new ResourcePackList());
		}

		// Token: 0x060038D3 RID: 14547 RVA: 0x00594894 File Offset: 0x00592A94
		public void Refresh()
		{
			foreach (ResourcePack resourcePack in this.ActiveResourcePackList.AllPacks)
			{
				resourcePack.Refresh();
			}
			this.UseResourcePacks(this.ActiveResourcePackList);
		}

		// Token: 0x060038D4 RID: 14548 RVA: 0x005948F0 File Offset: 0x00592AF0
		public void UseResourcePacks(ResourcePackList resourcePacks)
		{
			if (this.OnResourcePackChange != null)
			{
				this.OnResourcePackChange(resourcePacks);
			}
			if (resourcePacks.EnabledPacks.Any<ResourcePack>())
			{
				Logging.tML.Info("Loading the following resource packs: " + string.Join(", ", from x in resourcePacks.EnabledPacks
				select x.Name));
			}
			this.ActiveResourcePackList = resourcePacks;
			List<IContentSource> list = new List<IContentSource>(from pack in resourcePacks.EnabledPacks
			orderby pack.SortingOrder
			select pack.GetContentSource());
			list.AddRange(this._staticSources);
			foreach (IContentSource contentSource in list)
			{
				contentSource.Rejections.Clear();
			}
			List<IContentSource> list2 = new List<IContentSource>();
			for (int num = list.Count - 1; num >= 0; num--)
			{
				list2.Add(list[num]);
			}
			this._assetRepository.SetSources(list, 1);
			LanguageManager.Instance.UseSources(list2);
			Main.audioSystem.UseSources(list2);
			SoundEngine.Reload();
			Main.changeTheTitle = true;
		}

		// Token: 0x0400520C RID: 21004
		private readonly List<IContentSource> _staticSources;

		// Token: 0x0400520D RID: 21005
		private readonly IAssetRepository _assetRepository;
	}
}
