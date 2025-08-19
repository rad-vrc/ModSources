using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Core.UI.Other
{
	// Token: 0x02000280 RID: 640
	public class MinionInfoDisplay : InfoDisplay
	{
		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060010B0 RID: 4272 RVA: 0x0008599D File Offset: 0x00083B9D
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/InfoDisplayIcons/MinionInfoDisplay";
			}
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x000859A4 File Offset: 0x00083BA4
		public override bool Active()
		{
			return Main.LocalPlayer.GetModPlayer<InfoPlayer>().trackingDevice;
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x000859B8 File Offset: 0x00083BB8
		public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
		{
			return string.Concat(new string[]
			{
				Math.Round((double)Main.LocalPlayer.slotsMinions, 2).ToString(),
				"/",
				Main.LocalPlayer.maxMinions.ToString(),
				" ",
				Language.GetTextValue("Mods.QoLCompendium.InfoDisplayText.MinionSlots")
			});
		}
	}
}
