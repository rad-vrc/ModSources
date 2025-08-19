using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.SpiritClassic
{
	// Token: 0x020002EA RID: 746
	public class ToxinEffect : IPermanentModdedBuff
	{
		// Token: 0x06001201 RID: 4609 RVA: 0x000897E8 File Offset: 0x000879E8
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.spiritLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.spiritMod, "BismitePotionBuff")] && !PermanentBuffPlayer.PermanentSpiritClassicBuffsBools[14])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.spiritMod, "BismitePotionBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.spiritMod, "BismitePotionBuff")] = true;
			}
		}
	}
}
