using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Core.UI.Other
{
	// Token: 0x02000287 RID: 647
	public class ArmorPenetrationInfoDisplay : InfoDisplay
	{
		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060010CC RID: 4300 RVA: 0x00085C47 File Offset: 0x00083E47
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/InfoDisplayIcons/ArmorPenetrationInfoDisplay";
			}
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x00085C4E File Offset: 0x00083E4E
		public override bool Active()
		{
			return Main.LocalPlayer.GetModPlayer<InfoPlayer>().plateCracker;
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x00085C60 File Offset: 0x00083E60
		public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
		{
			float armorPenetrationStat = Main.LocalPlayer.GetModPlayer<InfoPlayer>().armorPenetrationStat;
			return armorPenetrationStat.ToString() + " " + Language.GetTextValue("Mods.QoLCompendium.InfoDisplayText.ArmorPenetration");
		}
	}
}
