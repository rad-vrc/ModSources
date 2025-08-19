using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Core.UI.Other
{
	// Token: 0x0200027D RID: 637
	public class ManaRegenInfoDisplay : InfoDisplay
	{
		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060010A4 RID: 4260 RVA: 0x00085847 File Offset: 0x00083A47
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/InfoDisplayIcons/ManaRegenInfoDisplay";
			}
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x0008584E File Offset: 0x00083A4E
		public override bool Active()
		{
			return Main.LocalPlayer.GetModPlayer<InfoPlayer>().replenisher;
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x00085860 File Offset: 0x00083A60
		public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
		{
			if (Main.LocalPlayer.statMana >= Main.LocalPlayer.statManaMax2)
			{
				return Language.GetTextValue("Mods.QoLCompendium.InfoDisplayText.FullMana");
			}
			return ((float)Main.LocalPlayer.manaRegen / 2f).ToString() + " " + Language.GetTextValue("Mods.QoLCompendium.InfoDisplayText.ManaRegeneration");
		}
	}
}
