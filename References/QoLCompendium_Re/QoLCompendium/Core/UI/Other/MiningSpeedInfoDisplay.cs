using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Core.UI.Other
{
	// Token: 0x02000284 RID: 644
	public class MiningSpeedInfoDisplay : InfoDisplay
	{
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060010C0 RID: 4288 RVA: 0x00085B51 File Offset: 0x00083D51
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/InfoDisplayIcons/MiningSpeedInfoDisplay";
			}
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x00085B58 File Offset: 0x00083D58
		public override bool Active()
		{
			return Main.LocalPlayer.GetModPlayer<InfoPlayer>().kettlebell;
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x00085B6C File Offset: 0x00083D6C
		public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
		{
			return ((1f - Main.LocalPlayer.pickSpeed) * 100f).ToString("N0") + Language.GetTextValue("Mods.QoLCompendium.InfoDisplayText.MiningSpeed");
		}
	}
}
