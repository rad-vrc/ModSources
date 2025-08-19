using System;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x0200067E RID: 1662
	public class CritterUICollectionInfoProvider : IBestiaryUICollectionInfoProvider
	{
		// Token: 0x060047CB RID: 18379 RVA: 0x006466D6 File Offset: 0x006448D6
		public CritterUICollectionInfoProvider(string persistentId)
		{
			this._persistentIdentifierToCheck = persistentId;
		}

		// Token: 0x060047CC RID: 18380 RVA: 0x006466E8 File Offset: 0x006448E8
		public BestiaryUICollectionInfo GetEntryUICollectionInfo()
		{
			return new BestiaryUICollectionInfo
			{
				UnlockState = (Main.BestiaryTracker.Sights.GetWasNearbyBefore(this._persistentIdentifierToCheck) ? BestiaryEntryUnlockState.CanShowDropsWithDropRates_4 : BestiaryEntryUnlockState.NotKnownAtAll_0)
			};
		}

		// Token: 0x060047CD RID: 18381 RVA: 0x00646720 File Offset: 0x00644920
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			return null;
		}

		// Token: 0x04005BFA RID: 23546
		private string _persistentIdentifierToCheck;
	}
}
