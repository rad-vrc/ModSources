using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Thorium
{
	// Token: 0x020002DD RID: 733
	public class SkeletonRepellentEffect : IPermanentModdedBuff
	{
		// Token: 0x060011E7 RID: 4583 RVA: 0x000890E0 File Offset: 0x000872E0
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.thoriumLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "SkeletonRepellentBuff")] && !PermanentBuffPlayer.PermanentThoriumBuffsBools[21])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "SkeletonRepellentBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "SkeletonRepellentBuff")] = true;
			}
		}
	}
}
