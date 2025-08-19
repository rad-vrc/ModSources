using System;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.All;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Potions.SOTS;
using QoLCompendium.Content.Items.Tools.PermanentBuffs.CrossMod.Upgraded.SOTS;
using SOTS;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems
{
	// Token: 0x020002A1 RID: 673
	[JITWhenModsEnabled(new string[]
	{
		"SOTS"
	})]
	[ExtendsFromMod(new string[]
	{
		"SOTS"
	})]
	public class SOTSRippleEffect : ModPlayer
	{
		// Token: 0x0600116E RID: 4462 RVA: 0x00087858 File Offset: 0x00085A58
		public override void PreUpdateBuffs()
		{
			if (base.Player.HasItemInAnyInventory(ModContent.ItemType<PermanentRipple>()) || base.Player.HasItemInAnyInventory(ModContent.ItemType<PermanentSecretsOfTheShadows>()) || base.Player.HasItemInAnyInventory(ModContent.ItemType<PermanentEverything>()))
			{
				SOTSPlayer sotsplayer = SOTSPlayer.ModPlayer(base.Player);
				sotsplayer.rippleBonusDamage += 2;
				sotsplayer.rippleEffect = true;
				sotsplayer.rippleTimer++;
			}
		}
	}
}
