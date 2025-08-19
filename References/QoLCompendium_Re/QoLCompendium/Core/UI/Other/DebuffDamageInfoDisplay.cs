using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Core.UI.Other
{
	// Token: 0x02000289 RID: 649
	public class DebuffDamageInfoDisplay : InfoDisplay
	{
		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060010D4 RID: 4308 RVA: 0x00085CEE File Offset: 0x00083EEE
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/InfoDisplayIcons/DebuffDamageInfoDisplay";
			}
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x00085CF5 File Offset: 0x00083EF5
		public override bool Active()
		{
			return Main.LocalPlayer.GetModPlayer<InfoPlayer>().deteriorationDisplay;
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x00085D08 File Offset: 0x00083F08
		public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
		{
			int dps = DebuffDamageInfoDisplay.GetDPS(Main.myPlayer);
			if (dps > 0)
			{
				return Language.GetTextValue("Mods.QoLCompendium.InfoDisplayText.DDPS", dps);
			}
			displayColor = InfoDisplay.InactiveInfoTextColor;
			return Language.GetTextValue("Mods.QoLCompendium.InfoDisplayText.NoDPS");
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x00085D4C File Offset: 0x00083F4C
		private static int GetDPS(int player)
		{
			double dps = 0.0;
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc = Main.npc[i];
				if (npc.active && npc.lifeRegen < 0 && npc.playerInteraction[player])
				{
					dps -= (double)npc.lifeRegen / 2.0;
				}
			}
			return (int)dps;
		}
	}
}
