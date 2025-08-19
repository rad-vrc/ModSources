using System;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000302 RID: 770
	public class CritterUICollectionInfoProvider : IBestiaryUICollectionInfoProvider
	{
		// Token: 0x060023CC RID: 9164 RVA: 0x005580E1 File Offset: 0x005562E1
		public CritterUICollectionInfoProvider(string persistentId)
		{
			this._persistentIdentifierToCheck = persistentId;
		}

		// Token: 0x060023CD RID: 9165 RVA: 0x005580F0 File Offset: 0x005562F0
		public BestiaryUICollectionInfo GetEntryUICollectionInfo()
		{
			return new BestiaryUICollectionInfo
			{
				UnlockState = (Main.BestiaryTracker.Sights.GetWasNearbyBefore(this._persistentIdentifierToCheck) ? BestiaryEntryUnlockState.CanShowDropsWithDropRates_4 : BestiaryEntryUnlockState.NotKnownAtAll_0)
			};
		}

		// Token: 0x060023CE RID: 9166 RVA: 0x0006A9EF File Offset: 0x00068BEF
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			return null;
		}

		// Token: 0x0400484F RID: 18511
		private string _persistentIdentifierToCheck;
	}
}
