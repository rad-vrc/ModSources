using System;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000303 RID: 771
	public class TownNPCUICollectionInfoProvider : IBestiaryUICollectionInfoProvider
	{
		// Token: 0x060023CF RID: 9167 RVA: 0x00558128 File Offset: 0x00556328
		public TownNPCUICollectionInfoProvider(string persistentId)
		{
			this._persistentIdentifierToCheck = persistentId;
		}

		// Token: 0x060023D0 RID: 9168 RVA: 0x00558138 File Offset: 0x00556338
		public BestiaryUICollectionInfo GetEntryUICollectionInfo()
		{
			return new BestiaryUICollectionInfo
			{
				UnlockState = (Main.BestiaryTracker.Chats.GetWasChatWith(this._persistentIdentifierToCheck) ? BestiaryEntryUnlockState.CanShowDropsWithDropRates_4 : BestiaryEntryUnlockState.NotKnownAtAll_0)
			};
		}

		// Token: 0x060023D1 RID: 9169 RVA: 0x0006A9EF File Offset: 0x00068BEF
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			return null;
		}

		// Token: 0x04004850 RID: 18512
		private string _persistentIdentifierToCheck;
	}
}
