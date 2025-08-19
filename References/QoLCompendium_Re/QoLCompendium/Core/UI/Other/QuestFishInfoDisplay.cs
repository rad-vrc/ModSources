using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Core.UI.Other
{
	// Token: 0x0200028A RID: 650
	public class QuestFishInfoDisplay : InfoDisplay
	{
		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060010D9 RID: 4313 RVA: 0x00085DAC File Offset: 0x00083FAC
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/InfoDisplayIcons/QuestFishInfoDisplay";
			}
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x00085DB3 File Offset: 0x00083FB3
		public override bool Active()
		{
			return Main.LocalPlayer.GetModPlayer<InfoPlayer>().anglerRadar;
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x00085DC4 File Offset: 0x00083FC4
		public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
		{
			if (Main.anglerQuestFinished || Main.anglerQuest == -1 || Main.anglerQuest >= Main.anglerQuestItemNetIDs.Length || !NPC.AnyNPCs(369))
			{
				displayColor = InfoDisplay.InactiveInfoTextColor;
				return Language.GetTextValue("Mods.QoLCompendium.InfoDisplayText.QuestFinished");
			}
			return Lang.GetItemNameValue(Main.anglerQuestItemNetIDs[Main.anglerQuest]);
		}
	}
}
