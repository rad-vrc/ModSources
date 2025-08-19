using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Arena.Calamity
{
	// Token: 0x0200034D RID: 845
	public class EffigyOfDecayEffect : IPermanentModdedBuff
	{
		// Token: 0x060012C7 RID: 4807 RVA: 0x0008CC10 File Offset: 0x0008AE10
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "EffigyOfDecayBuff")] && !PermanentBuffPlayer.PermanentCalamityBuffsBools[3])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "EffigyOfDecayBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "EffigyOfDecayBuff")] = true;
			}
		}
	}
}
