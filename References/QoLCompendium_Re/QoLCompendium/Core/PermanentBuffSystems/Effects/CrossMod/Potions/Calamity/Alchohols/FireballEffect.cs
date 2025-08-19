using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Calamity.Alchohols
{
	// Token: 0x0200032B RID: 811
	public class FireballEffect : IPermanentModdedBuff
	{
		// Token: 0x06001283 RID: 4739 RVA: 0x0008BA64 File Offset: 0x00089C64
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "FireballBuff")])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "FireballBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "FireballBuff")] = true;
			}
		}
	}
}
