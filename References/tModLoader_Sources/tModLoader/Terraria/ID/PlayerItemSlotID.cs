using System;

namespace Terraria.ID
{
	// Token: 0x0200041F RID: 1055
	public static class PlayerItemSlotID
	{
		// Token: 0x06003547 RID: 13639 RVA: 0x005723B4 File Offset: 0x005705B4
		static PlayerItemSlotID()
		{
			PlayerItemSlotID.Inventory0 = PlayerItemSlotID.AllocateSlots(58, true);
			PlayerItemSlotID.InventoryMouseItem = PlayerItemSlotID.AllocateSlots(1, true);
			PlayerItemSlotID.Armor0 = PlayerItemSlotID.AllocateSlots(20, true);
			PlayerItemSlotID.Dye0 = PlayerItemSlotID.AllocateSlots(10, true);
			PlayerItemSlotID.Misc0 = PlayerItemSlotID.AllocateSlots(5, true);
			PlayerItemSlotID.MiscDye0 = PlayerItemSlotID.AllocateSlots(5, true);
			PlayerItemSlotID.Bank1_0 = PlayerItemSlotID.AllocateSlots(40, false);
			PlayerItemSlotID.Bank2_0 = PlayerItemSlotID.AllocateSlots(40, false);
			PlayerItemSlotID.TrashItem = PlayerItemSlotID.AllocateSlots(1, false);
			PlayerItemSlotID.Bank3_0 = PlayerItemSlotID.AllocateSlots(40, false);
			PlayerItemSlotID.Bank4_0 = PlayerItemSlotID.AllocateSlots(40, true);
			PlayerItemSlotID.Loadout1_Armor_0 = PlayerItemSlotID.AllocateSlots(20, true);
			PlayerItemSlotID.Loadout1_Dye_0 = PlayerItemSlotID.AllocateSlots(10, true);
			PlayerItemSlotID.Loadout2_Armor_0 = PlayerItemSlotID.AllocateSlots(20, true);
			PlayerItemSlotID.Loadout2_Dye_0 = PlayerItemSlotID.AllocateSlots(10, true);
			PlayerItemSlotID.Loadout3_Armor_0 = PlayerItemSlotID.AllocateSlots(20, true);
			PlayerItemSlotID.Loadout3_Dye_0 = PlayerItemSlotID.AllocateSlots(10, true);
		}

		// Token: 0x06003548 RID: 13640 RVA: 0x005724A8 File Offset: 0x005706A8
		private static int AllocateSlots(int amount, bool canNetRelay)
		{
			int nextSlotId = PlayerItemSlotID._nextSlotId;
			PlayerItemSlotID._nextSlotId += amount;
			int num = PlayerItemSlotID.CanRelay.Length;
			Array.Resize<bool>(ref PlayerItemSlotID.CanRelay, num + amount);
			for (int i = num; i < PlayerItemSlotID._nextSlotId; i++)
			{
				PlayerItemSlotID.CanRelay[i] = canNetRelay;
			}
			return nextSlotId;
		}

		// Token: 0x04004310 RID: 17168
		public static readonly int Inventory0;

		// Token: 0x04004311 RID: 17169
		public static readonly int InventoryMouseItem;

		// Token: 0x04004312 RID: 17170
		public static readonly int Armor0;

		// Token: 0x04004313 RID: 17171
		public static readonly int Dye0;

		// Token: 0x04004314 RID: 17172
		public static readonly int Misc0;

		// Token: 0x04004315 RID: 17173
		public static readonly int MiscDye0;

		// Token: 0x04004316 RID: 17174
		public static readonly int Bank1_0;

		// Token: 0x04004317 RID: 17175
		public static readonly int Bank2_0;

		// Token: 0x04004318 RID: 17176
		public static readonly int TrashItem;

		// Token: 0x04004319 RID: 17177
		public static readonly int Bank3_0;

		// Token: 0x0400431A RID: 17178
		public static readonly int Bank4_0;

		// Token: 0x0400431B RID: 17179
		public static readonly int Loadout1_Armor_0;

		// Token: 0x0400431C RID: 17180
		public static readonly int Loadout1_Dye_0;

		// Token: 0x0400431D RID: 17181
		public static readonly int Loadout2_Armor_0;

		// Token: 0x0400431E RID: 17182
		public static readonly int Loadout2_Dye_0;

		// Token: 0x0400431F RID: 17183
		public static readonly int Loadout3_Armor_0;

		// Token: 0x04004320 RID: 17184
		public static readonly int Loadout3_Dye_0;

		// Token: 0x04004321 RID: 17185
		public static bool[] CanRelay = new bool[0];

		// Token: 0x04004322 RID: 17186
		private static int _nextSlotId;
	}
}
