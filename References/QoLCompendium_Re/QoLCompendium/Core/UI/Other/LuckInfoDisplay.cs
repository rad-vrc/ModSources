using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Core.UI.Other
{
	// Token: 0x0200027E RID: 638
	public class LuckInfoDisplay : InfoDisplay
	{
		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060010A8 RID: 4264 RVA: 0x000858BB File Offset: 0x00083ABB
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/InfoDisplayIcons/LuckInfoDisplay";
			}
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x000858C2 File Offset: 0x00083AC2
		public override bool Active()
		{
			return Main.LocalPlayer.GetModPlayer<InfoPlayer>().metallicClover;
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x000858D4 File Offset: 0x00083AD4
		public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
		{
			return Math.Round((double)Main.LocalPlayer.luck, 3).ToString() + " " + Language.GetTextValue("Mods.QoLCompendium.InfoDisplayText.Luck");
		}
	}
}
