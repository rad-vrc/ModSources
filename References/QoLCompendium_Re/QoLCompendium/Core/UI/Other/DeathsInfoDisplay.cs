using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Core.UI.Other
{
	// Token: 0x02000288 RID: 648
	public class DeathsInfoDisplay : InfoDisplay
	{
		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060010D0 RID: 4304 RVA: 0x00085C98 File Offset: 0x00083E98
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/InfoDisplayIcons/DeathsInfoDisplay";
			}
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x00085C9F File Offset: 0x00083E9F
		public override bool Active()
		{
			return Main.LocalPlayer.GetModPlayer<InfoPlayer>().skullWatch;
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x00085CB0 File Offset: 0x00083EB0
		public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
		{
			return (Main.LocalPlayer.numberOfDeathsPVE + Main.LocalPlayer.numberOfDeathsPVP).ToString() + " " + Language.GetTextValue("Mods.QoLCompendium.InfoDisplayText.DeathCount");
		}
	}
}
