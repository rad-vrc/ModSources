using System;

namespace Terraria.ID
{
	// Token: 0x0200019F RID: 415
	public static class PlayerItemSlotID
	{
		// Token: 0x06001B7C RID: 7036 RVA: 0x004E8690 File Offset: 0x004E6890
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

		// Token: 0x06001B7D RID: 7037 RVA: 0x004E8784 File Offset: 0x004E6984
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

		// Token: 0x04001705 RID: 5893
		public static readonly int Inventory0;

		// Token: 0x04001706 RID: 5894
		public static readonly int InventoryMouseItem;

		// Token: 0x04001707 RID: 5895
		public static readonly int Armor0;

		// Token: 0x04001708 RID: 5896
		public static readonly int Dye0;

		// Token: 0x04001709 RID: 5897
		public static readonly int Misc0;

		// Token: 0x0400170A RID: 5898
		public static readonly int MiscDye0;

		// Token: 0x0400170B RID: 5899
		public static readonly int Bank1_0;

		// Token: 0x0400170C RID: 5900
		public static readonly int Bank2_0;

		// Token: 0x0400170D RID: 5901
		public static readonly int TrashItem;

		// Token: 0x0400170E RID: 5902
		public static readonly int Bank3_0;

		// Token: 0x0400170F RID: 5903
		public static readonly int Bank4_0;

		// Token: 0x04001710 RID: 5904
		public static readonly int Loadout1_Armor_0;

		// Token: 0x04001711 RID: 5905
		public static readonly int Loadout1_Dye_0;

		// Token: 0x04001712 RID: 5906
		public static readonly int Loadout2_Armor_0;

		// Token: 0x04001713 RID: 5907
		public static readonly int Loadout2_Dye_0;

		// Token: 0x04001714 RID: 5908
		public static readonly int Loadout3_Armor_0;

		// Token: 0x04001715 RID: 5909
		public static readonly int Loadout3_Dye_0;

		// Token: 0x04001716 RID: 5910
		public static bool[] CanRelay = new bool[0];

		// Token: 0x04001717 RID: 5911
		private static int _nextSlotId;
	}
}
