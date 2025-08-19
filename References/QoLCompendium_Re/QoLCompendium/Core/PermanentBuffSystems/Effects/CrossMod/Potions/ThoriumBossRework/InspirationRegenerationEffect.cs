using System;
using Terraria.ModLoader;
using ThoriumRework;
using ThoriumRework.Buffs;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.ThoriumBossRework
{
	// Token: 0x020002C8 RID: 712
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumRework"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumRework"
	})]
	public class InspirationRegenerationEffect : IPermanentModdedBuff
	{
		// Token: 0x060011BD RID: 4541 RVA: 0x000885A4 File Offset: 0x000867A4
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.thoriumBossReworkLoaded)
			{
				return;
			}
			if (!ModContent.GetInstance<CompatConfig>().extraPotions)
			{
				return;
			}
			if (!player.Player.buffImmune[ModContent.BuffType<Inspired>()] && !PermanentBuffPlayer.PermanentThoriumBuffsBools[27])
			{
				this.buffToApply = BuffLoader.GetBuff(ModContent.BuffType<Inspired>());
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[ModContent.BuffType<Inspired>()] = true;
			}
		}
	}
}
