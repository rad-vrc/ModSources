using System;
using ClickerClass;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.Clicker;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems
{
	// Token: 0x020002A2 RID: 674
	[JITWhenModsEnabled(new string[]
	{
		"ClickerClass"
	})]
	[ExtendsFromMod(new string[]
	{
		"ClickerClass"
	})]
	public class ClickerClassInfluenceEffect : ModPlayer
	{
		// Token: 0x06001170 RID: 4464 RVA: 0x000878C8 File Offset: 0x00085AC8
		public override void PreUpdateBuffs()
		{
			if (base.Player.HasItemInAnyInventory(ModContent.ItemType<PermanentInfluence>()))
			{
				base.Player.GetModPlayer<ClickerPlayer>().clickerRadius += (float)(2 * this.RadiusIncrease) / 100f;
			}
		}

		// Token: 0x04000766 RID: 1894
		public readonly int RadiusIncrease = 20;
	}
}
