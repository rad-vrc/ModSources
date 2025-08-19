using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Thorium
{
	// Token: 0x020002D8 RID: 728
	public class HydrationEffect : IPermanentModdedBuff
	{
		// Token: 0x060011DD RID: 4573 RVA: 0x00088E30 File Offset: 0x00087030
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.thoriumLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "HydrationBuff")] && !PermanentBuffPlayer.PermanentThoriumBuffsBools[14])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "HydrationBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "HydrationBuff")] = true;
			}
		}
	}
}
