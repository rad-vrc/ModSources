using System;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000305 RID: 773
	public class HighestOfMultipleUICollectionInfoProvider : IBestiaryUICollectionInfoProvider
	{
		// Token: 0x060023D8 RID: 9176 RVA: 0x00558289 File Offset: 0x00556489
		public HighestOfMultipleUICollectionInfoProvider(params IBestiaryUICollectionInfoProvider[] providers)
		{
			this._providers = providers;
			this._mainProviderIndex = 0;
		}

		// Token: 0x060023D9 RID: 9177 RVA: 0x005582A0 File Offset: 0x005564A0
		public BestiaryUICollectionInfo GetEntryUICollectionInfo()
		{
			BestiaryUICollectionInfo entryUICollectionInfo = this._providers[this._mainProviderIndex].GetEntryUICollectionInfo();
			BestiaryEntryUnlockState unlockState = entryUICollectionInfo.UnlockState;
			for (int i = 0; i < this._providers.Length; i++)
			{
				BestiaryUICollectionInfo entryUICollectionInfo2 = this._providers[i].GetEntryUICollectionInfo();
				if (unlockState < entryUICollectionInfo2.UnlockState)
				{
					unlockState = entryUICollectionInfo2.UnlockState;
				}
			}
			entryUICollectionInfo.UnlockState = unlockState;
			return entryUICollectionInfo;
		}

		// Token: 0x060023DA RID: 9178 RVA: 0x0006A9EF File Offset: 0x00068BEF
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			return null;
		}

		// Token: 0x04004854 RID: 18516
		private IBestiaryUICollectionInfoProvider[] _providers;

		// Token: 0x04004855 RID: 18517
		private int _mainProviderIndex;
	}
}
