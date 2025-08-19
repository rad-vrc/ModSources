using System;
using Terraria.ModLoader;
using ThoriumRework;
using ThoriumRework.Buffs;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.ThoriumBossRework
{
	// Token: 0x020002C7 RID: 711
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumRework"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumRework"
	})]
	public class DeathsingerEffect : IPermanentModdedBuff
	{
		// Token: 0x060011BB RID: 4539 RVA: 0x0008852C File Offset: 0x0008672C
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
			if (!player.Player.buffImmune[ModContent.BuffType<Deathsinger>()] && !PermanentBuffPlayer.PermanentThoriumBuffsBools[26])
			{
				this.buffToApply = BuffLoader.GetBuff(ModContent.BuffType<Deathsinger>());
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[ModContent.BuffType<Deathsinger>()] = true;
			}
		}
	}
}
