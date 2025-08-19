using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Calamity
{
	// Token: 0x02000322 RID: 802
	public class SulphurskinEffect : IPermanentModdedBuff
	{
		// Token: 0x06001271 RID: 4721 RVA: 0x0008B528 File Offset: 0x00089728
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "SulphurskinBuff")] && !PermanentBuffPlayer.PermanentCalamityBuffsBools[21])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "SulphurskinBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "SulphurskinBuff")] = true;
			}
		}
	}
}
