using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Thorium
{
	// Token: 0x020002DF RID: 735
	public class ZombieRepellentEffect : IPermanentModdedBuff
	{
		// Token: 0x060011EB RID: 4587 RVA: 0x000891F8 File Offset: 0x000873F8
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.thoriumLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "ZombieRepellentBuff")] && !PermanentBuffPlayer.PermanentThoriumBuffsBools[22])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "ZombieRepellentBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "ZombieRepellentBuff")] = true;
			}
		}
	}
}
