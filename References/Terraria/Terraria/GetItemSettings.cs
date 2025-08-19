using System;

namespace Terraria
{
	// Token: 0x0200003E RID: 62
	public struct GetItemSettings
	{
		// Token: 0x06000668 RID: 1640 RVA: 0x002C057C File Offset: 0x002BE77C
		public GetItemSettings(bool LongText = false, bool NoText = false, bool CanGoIntoVoidVault = false, Action<Item> StepAfterHandlingSlotNormally = null)
		{
			this.LongText = LongText;
			this.NoText = NoText;
			this.CanGoIntoVoidVault = CanGoIntoVoidVault;
			this.StepAfterHandlingSlotNormally = StepAfterHandlingSlotNormally;
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x002C059B File Offset: 0x002BE79B
		public void HandlePostAction(Item item)
		{
			if (this.StepAfterHandlingSlotNormally != null)
			{
				this.StepAfterHandlingSlotNormally(item);
			}
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x002C05B1 File Offset: 0x002BE7B1
		private static void MakeNewAndShiny(Item item)
		{
			item.newAndShiny = true;
		}

		// Token: 0x0400048E RID: 1166
		public static GetItemSettings InventoryEntityToPlayerInventorySettings = new GetItemSettings(false, true, false, null);

		// Token: 0x0400048F RID: 1167
		public static GetItemSettings NPCEntityToPlayerInventorySettings = new GetItemSettings(true, false, false, null);

		// Token: 0x04000490 RID: 1168
		public static GetItemSettings LootAllSettings = default(GetItemSettings);

		// Token: 0x04000491 RID: 1169
		public static GetItemSettings LootAllSettingsRegularChest = new GetItemSettings(false, false, true, null);

		// Token: 0x04000492 RID: 1170
		public static GetItemSettings PickupItemFromWorld = new GetItemSettings(false, false, true, null);

		// Token: 0x04000493 RID: 1171
		public static GetItemSettings GetItemInDropItemCheck = new GetItemSettings(false, true, false, null);

		// Token: 0x04000494 RID: 1172
		public static GetItemSettings InventoryUIToInventorySettings = default(GetItemSettings);

		// Token: 0x04000495 RID: 1173
		public static GetItemSettings InventoryUIToInventorySettingsShowAsNew = new GetItemSettings(false, true, false, new Action<Item>(GetItemSettings.MakeNewAndShiny));

		// Token: 0x04000496 RID: 1174
		public static GetItemSettings ItemCreatedFromItemUsage = default(GetItemSettings);

		// Token: 0x04000497 RID: 1175
		public readonly bool LongText;

		// Token: 0x04000498 RID: 1176
		public readonly bool NoText;

		// Token: 0x04000499 RID: 1177
		public readonly bool CanGoIntoVoidVault;

		// Token: 0x0400049A RID: 1178
		public readonly Action<Item> StepAfterHandlingSlotNormally;
	}
}
