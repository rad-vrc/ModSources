using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Core.UI.ItemSlots
{
	// Token: 0x0200028D RID: 653
	public class BootSlot : ModAccessorySlot
	{
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060010EC RID: 4332 RVA: 0x000862D6 File Offset: 0x000844D6
		public override string FunctionalTexture
		{
			get
			{
				return "QoLCompendium/Assets/Slots/Boots";
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060010ED RID: 4333 RVA: 0x000862D6 File Offset: 0x000844D6
		public override string VanityTexture
		{
			get
			{
				return "QoLCompendium/Assets/Slots/Boots";
			}
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x000862DD File Offset: 0x000844DD
		public override bool CanAcceptItem(Item checkItem, AccessorySlotType context)
		{
			return checkItem.shoeSlot > 0;
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x000862DD File Offset: 0x000844DD
		public override bool ModifyDefaultSwapSlot(Item item, int accSlotToSwapTo)
		{
			return item.shoeSlot > 0;
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x000862EB File Offset: 0x000844EB
		public override bool IsEnabled()
		{
			return QoLCompendium.mainConfig.BootSlot;
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x0000404D File Offset: 0x0000224D
		public override bool IsVisibleWhenNotEnabled()
		{
			return false;
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x000862FC File Offset: 0x000844FC
		public override void OnMouseHover(AccessorySlotType context)
		{
			switch (context)
			{
			case AccessorySlotType.FunctionalSlot:
				Main.hoverItemName = Language.GetTextValue("Mods.QoLCompendium.BootSlot.Boots");
				return;
			case AccessorySlotType.VanitySlot:
				Main.hoverItemName = Language.GetTextValue("Mods.QoLCompendium.BootSlot.SocialBoots");
				return;
			case AccessorySlotType.DyeSlot:
				Main.hoverItemName = Language.GetTextValue("Mods.QoLCompendium.BootSlot.Dye");
				return;
			default:
				return;
			}
		}
	}
}
