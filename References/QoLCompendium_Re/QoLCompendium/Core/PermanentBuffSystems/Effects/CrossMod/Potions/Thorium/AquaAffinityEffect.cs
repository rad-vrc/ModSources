using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Thorium
{
	// Token: 0x020002C9 RID: 713
	public class AquaAffinityEffect : IPermanentModdedBuff
	{
		// Token: 0x060011BF RID: 4543 RVA: 0x0008861C File Offset: 0x0008681C
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.thoriumLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "AquaAffinity")] && !PermanentBuffPlayer.PermanentThoriumBuffsBools[1])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "AquaAffinity"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "AquaAffinity")] = true;
			}
		}
	}
}
