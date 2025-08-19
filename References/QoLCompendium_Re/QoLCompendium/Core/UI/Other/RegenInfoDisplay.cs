using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Core.UI.Other
{
	// Token: 0x0200027C RID: 636
	public class RegenInfoDisplay : InfoDisplay
	{
		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060010A0 RID: 4256 RVA: 0x000857C0 File Offset: 0x000839C0
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/InfoDisplayIcons/RegenInfoDisplay";
			}
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x000857C7 File Offset: 0x000839C7
		public override bool Active()
		{
			return Main.LocalPlayer.GetModPlayer<InfoPlayer>().regenerator;
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x000857D8 File Offset: 0x000839D8
		public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
		{
			float lifeRegen = (float)Main.LocalPlayer.lifeRegen;
			lifeRegen *= 0.5f;
			lifeRegen = (float)Math.Round((double)lifeRegen, 2);
			if (Main.LocalPlayer.statLife >= Main.LocalPlayer.statLifeMax2)
			{
				return Language.GetTextValue("Mods.QoLCompendium.InfoDisplayText.FullHealth");
			}
			return lifeRegen.ToString() + " " + Language.GetTextValue("Mods.QoLCompendium.InfoDisplayText.Regeneration");
		}
	}
}
