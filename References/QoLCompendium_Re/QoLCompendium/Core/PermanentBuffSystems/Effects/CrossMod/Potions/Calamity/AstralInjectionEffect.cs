using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Calamity
{
	// Token: 0x02000317 RID: 791
	public class AstralInjectionEffect : IPermanentModdedBuff
	{
		// Token: 0x0600125B RID: 4699 RVA: 0x0008AF24 File Offset: 0x00089124
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "AstralInjectionBuff")] && !PermanentBuffPlayer.PermanentCalamityBuffsBools[10])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "AstralInjectionBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "AstralInjectionBuff")] = true;
			}
		}
	}
}
