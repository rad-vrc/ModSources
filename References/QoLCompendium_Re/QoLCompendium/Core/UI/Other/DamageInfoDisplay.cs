using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Core.UI.Other
{
	// Token: 0x02000285 RID: 645
	public class DamageInfoDisplay : InfoDisplay
	{
		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060010C4 RID: 4292 RVA: 0x00085BAB File Offset: 0x00083DAB
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/InfoDisplayIcons/DamageInfoDisplay";
			}
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x00085BB2 File Offset: 0x00083DB2
		public override bool Active()
		{
			return Main.LocalPlayer.GetModPlayer<InfoPlayer>().harmInducer;
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x00085BC4 File Offset: 0x00083DC4
		public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
		{
			float damageStat = Main.LocalPlayer.GetModPlayer<InfoPlayer>().damageStat;
			return damageStat.ToString("N2") + Language.GetTextValue("Mods.QoLCompendium.InfoDisplayText.DamageMultiplier");
		}
	}
}
