using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Core.UI.ItemSlots
{
	// Token: 0x0200028E RID: 654
	public class ExpertSlot : ModAccessorySlot
	{
		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060010F4 RID: 4340 RVA: 0x00086356 File Offset: 0x00084556
		public override string FunctionalTexture
		{
			get
			{
				return "QoLCompendium/Assets/Slots/Expert";
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060010F5 RID: 4341 RVA: 0x00086356 File Offset: 0x00084556
		public override string VanityTexture
		{
			get
			{
				return "QoLCompendium/Assets/Slots/Expert";
			}
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x0008635D File Offset: 0x0008455D
		public override bool CanAcceptItem(Item checkItem, AccessorySlotType context)
		{
			return checkItem.expert && checkItem.accessory;
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x0008635D File Offset: 0x0008455D
		public override bool ModifyDefaultSwapSlot(Item item, int accSlotToSwapTo)
		{
			return item.expert && item.accessory;
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x00086372 File Offset: 0x00084572
		public override bool IsEnabled()
		{
			return QoLCompendium.mainConfig.ExpertSlot && Main.expertMode;
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x0000404D File Offset: 0x0000224D
		public override bool IsVisibleWhenNotEnabled()
		{
			return false;
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x0008638C File Offset: 0x0008458C
		public override void OnMouseHover(AccessorySlotType context)
		{
			switch (context)
			{
			case AccessorySlotType.FunctionalSlot:
				Main.hoverItemName = Language.GetTextValue("Mods.QoLCompendium.ExpertSlot.Expert");
				return;
			case AccessorySlotType.VanitySlot:
				Main.hoverItemName = Language.GetTextValue("Mods.QoLCompendium.ExpertSlot.SocialExpert");
				return;
			case AccessorySlotType.DyeSlot:
				Main.hoverItemName = Language.GetTextValue("Mods.QoLCompendium.ExpertSlot.Dye");
				return;
			default:
				return;
			}
		}
	}
}
