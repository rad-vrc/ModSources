using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Core.UI.ItemSlots
{
	// Token: 0x02000290 RID: 656
	public class WingSlot : ModAccessorySlot
	{
		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06001104 RID: 4356 RVA: 0x00086456 File Offset: 0x00084656
		public override string FunctionalTexture
		{
			get
			{
				return "QoLCompendium/Assets/Slots/Wings";
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06001105 RID: 4357 RVA: 0x00086456 File Offset: 0x00084656
		public override string VanityTexture
		{
			get
			{
				return "QoLCompendium/Assets/Slots/Wings";
			}
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x0008645D File Offset: 0x0008465D
		public override bool CanAcceptItem(Item checkItem, AccessorySlotType context)
		{
			return checkItem.wingSlot > 0;
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x0008645D File Offset: 0x0008465D
		public override bool ModifyDefaultSwapSlot(Item item, int accSlotToSwapTo)
		{
			return item.wingSlot > 0;
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x0008646B File Offset: 0x0008466B
		public override bool IsEnabled()
		{
			return QoLCompendium.mainConfig.WingSlot;
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x0000404D File Offset: 0x0000224D
		public override bool IsVisibleWhenNotEnabled()
		{
			return false;
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x0008647C File Offset: 0x0008467C
		public override void OnMouseHover(AccessorySlotType context)
		{
			switch (context)
			{
			case AccessorySlotType.FunctionalSlot:
				Main.hoverItemName = Language.GetTextValue("Mods.QoLCompendium.WingSlot.Wings");
				return;
			case AccessorySlotType.VanitySlot:
				Main.hoverItemName = Language.GetTextValue("Mods.QoLCompendium.WingSlot.SocialWings");
				return;
			case AccessorySlotType.DyeSlot:
				Main.hoverItemName = Language.GetTextValue("Mods.QoLCompendium.WingSlot.Dye");
				return;
			default:
				return;
			}
		}
	}
}
