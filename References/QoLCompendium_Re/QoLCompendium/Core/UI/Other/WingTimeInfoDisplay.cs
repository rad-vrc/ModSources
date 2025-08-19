using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Core.UI.Other
{
	// Token: 0x02000282 RID: 642
	public class WingTimeInfoDisplay : InfoDisplay
	{
		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060010B8 RID: 4280 RVA: 0x00085A8E File Offset: 0x00083C8E
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/InfoDisplayIcons/WingTimeInfoDisplay";
			}
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x00085A95 File Offset: 0x00083C95
		public override bool Active()
		{
			return Main.LocalPlayer.GetModPlayer<InfoPlayer>().wingTimer;
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x00085AA8 File Offset: 0x00083CA8
		public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
		{
			float num = Main.LocalPlayer.wingTime / 60f;
			if (Main.LocalPlayer.empressBrooch && !ModConditions.calamityLoaded)
			{
				return Language.GetTextValue("Mods.QoLCompendium.InfoDisplayText.InfiniteWingTime");
			}
			return num.ToString("0.00") + Language.GetTextValue("Mods.QoLCompendium.InfoDisplayText.WingTime");
		}
	}
}
