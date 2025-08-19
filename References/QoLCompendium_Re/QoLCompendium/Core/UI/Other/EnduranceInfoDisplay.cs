using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Core.UI.Other
{
	// Token: 0x02000283 RID: 643
	public class EnduranceInfoDisplay : InfoDisplay
	{
		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060010BC RID: 4284 RVA: 0x00085AFF File Offset: 0x00083CFF
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/InfoDisplayIcons/EnduranceInfoDisplay";
			}
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x00085B06 File Offset: 0x00083D06
		public override bool Active()
		{
			return Main.LocalPlayer.GetModPlayer<InfoPlayer>().reinforcedPanel;
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x00085B18 File Offset: 0x00083D18
		public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
		{
			return (100f * Main.LocalPlayer.endurance).ToString("N0") + Language.GetTextValue("Mods.QoLCompendium.InfoDisplayText.Endurance");
		}
	}
}
