using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Calamity.Alchohols
{
	// Token: 0x0200032C RID: 812
	public class GrapeBeerEffect : IPermanentModdedBuff
	{
		// Token: 0x06001285 RID: 4741 RVA: 0x0008BAE4 File Offset: 0x00089CE4
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "GrapeBeerBuff")])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "GrapeBeerBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "GrapeBeerBuff")] = true;
			}
		}
	}
}
