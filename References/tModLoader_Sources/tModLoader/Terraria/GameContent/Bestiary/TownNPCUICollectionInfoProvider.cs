using System;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x020006AA RID: 1706
	public class TownNPCUICollectionInfoProvider : IBestiaryUICollectionInfoProvider
	{
		// Token: 0x06004874 RID: 18548 RVA: 0x006491B8 File Offset: 0x006473B8
		public TownNPCUICollectionInfoProvider(string persistentId)
		{
			this._persistentIdentifierToCheck = persistentId;
		}

		// Token: 0x06004875 RID: 18549 RVA: 0x006491C8 File Offset: 0x006473C8
		public BestiaryUICollectionInfo GetEntryUICollectionInfo()
		{
			return new BestiaryUICollectionInfo
			{
				UnlockState = (Main.BestiaryTracker.Chats.GetWasChatWith(this._persistentIdentifierToCheck) ? BestiaryEntryUnlockState.CanShowDropsWithDropRates_4 : BestiaryEntryUnlockState.NotKnownAtAll_0)
			};
		}

		// Token: 0x06004876 RID: 18550 RVA: 0x00649200 File Offset: 0x00647400
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			return null;
		}

		// Token: 0x04005C38 RID: 23608
		private string _persistentIdentifierToCheck;
	}
}
