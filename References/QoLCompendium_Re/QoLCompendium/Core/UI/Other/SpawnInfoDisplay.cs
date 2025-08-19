using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Core.UI.Other
{
	// Token: 0x0200027F RID: 639
	public class SpawnInfoDisplay : InfoDisplay
	{
		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060010AC RID: 4268 RVA: 0x0008590E File Offset: 0x00083B0E
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/InfoDisplayIcons/SpawnInfoDisplay";
			}
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x00085915 File Offset: 0x00083B15
		public override bool Active()
		{
			return Main.LocalPlayer.GetModPlayer<InfoPlayer>().headCounter;
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x00085928 File Offset: 0x00083B28
		public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
		{
			int spawnRateRaw = Main.LocalPlayer.GetModPlayer<QoLCPlayer>().spawnRate;
			if (spawnRateRaw == 0)
			{
				return Language.GetTextValue("Mods.QoLCompendium.InfoDisplayText.SpawnsDisabled");
			}
			if (Main.LocalPlayer.GetModPlayer<QoLCPlayer>().noSpawns)
			{
				return Language.GetTextValue("Mods.QoLCompendium.InfoDisplayText.SpawnsDisabled");
			}
			float spawnRateAdapted = 60f / (float)spawnRateRaw;
			return ((float)Math.Round((double)spawnRateAdapted, 2)).ToString() + " " + Language.GetTextValue("Mods.QoLCompendium.InfoDisplayText.SpawnRate");
		}
	}
}
