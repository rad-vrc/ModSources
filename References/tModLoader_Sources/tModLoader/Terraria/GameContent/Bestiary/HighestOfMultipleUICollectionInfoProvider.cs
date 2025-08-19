using System;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000685 RID: 1669
	public class HighestOfMultipleUICollectionInfoProvider : IBestiaryUICollectionInfoProvider
	{
		// Token: 0x060047E8 RID: 18408 RVA: 0x00646D3E File Offset: 0x00644F3E
		public HighestOfMultipleUICollectionInfoProvider(params IBestiaryUICollectionInfoProvider[] providers)
		{
			this._providers = providers;
			this._mainProviderIndex = 0;
		}

		// Token: 0x060047E9 RID: 18409 RVA: 0x00646D54 File Offset: 0x00644F54
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

		// Token: 0x060047EA RID: 18410 RVA: 0x00646DB5 File Offset: 0x00644FB5
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			return null;
		}

		// Token: 0x04005C0B RID: 23563
		private IBestiaryUICollectionInfoProvider[] _providers;

		// Token: 0x04005C0C RID: 23564
		private int _mainProviderIndex;
	}
}
