using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Core.UI.Other
{
	// Token: 0x02000286 RID: 646
	public class CritInfoDisplay : InfoDisplay
	{
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060010C8 RID: 4296 RVA: 0x00085BFC File Offset: 0x00083DFC
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/InfoDisplayIcons/CritInfoDisplay";
			}
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x00085C03 File Offset: 0x00083E03
		public override bool Active()
		{
			return Main.LocalPlayer.GetModPlayer<InfoPlayer>().luckyDie;
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x00085C14 File Offset: 0x00083E14
		public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
		{
			float critChanceStat = Main.LocalPlayer.GetModPlayer<InfoPlayer>().critChanceStat;
			return critChanceStat.ToString() + Language.GetTextValue("Mods.QoLCompendium.InfoDisplayText.CriticalStrikeChance");
		}
	}
}
