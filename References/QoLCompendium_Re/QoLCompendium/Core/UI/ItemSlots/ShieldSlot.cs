using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Core.UI.ItemSlots
{
	// Token: 0x0200028F RID: 655
	public class ShieldSlot : ModAccessorySlot
	{
		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060010FC RID: 4348 RVA: 0x000863DE File Offset: 0x000845DE
		public override string FunctionalTexture
		{
			get
			{
				return "QoLCompendium/Assets/Slots/Shield";
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060010FD RID: 4349 RVA: 0x000863DE File Offset: 0x000845DE
		public override string VanityTexture
		{
			get
			{
				return "QoLCompendium/Assets/Slots/Shield";
			}
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x000863E5 File Offset: 0x000845E5
		public override bool CanAcceptItem(Item checkItem, AccessorySlotType context)
		{
			return checkItem.shieldSlot > 0;
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x000863E5 File Offset: 0x000845E5
		public override bool ModifyDefaultSwapSlot(Item item, int accSlotToSwapTo)
		{
			return item.shieldSlot > 0;
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x000863F3 File Offset: 0x000845F3
		public override bool IsEnabled()
		{
			return QoLCompendium.mainConfig.ShieldSlot;
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x0000404D File Offset: 0x0000224D
		public override bool IsVisibleWhenNotEnabled()
		{
			return false;
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x00086404 File Offset: 0x00084604
		public override void OnMouseHover(AccessorySlotType context)
		{
			switch (context)
			{
			case AccessorySlotType.FunctionalSlot:
				Main.hoverItemName = Language.GetTextValue("Mods.QoLCompendium.ShieldSlot.Shield");
				return;
			case AccessorySlotType.VanitySlot:
				Main.hoverItemName = Language.GetTextValue("Mods.QoLCompendium.ShieldSlot.SocialShield");
				return;
			case AccessorySlotType.DyeSlot:
				Main.hoverItemName = Language.GetTextValue("Mods.QoLCompendium.ShieldSlot.Dye");
				return;
			default:
				return;
			}
		}
	}
}
