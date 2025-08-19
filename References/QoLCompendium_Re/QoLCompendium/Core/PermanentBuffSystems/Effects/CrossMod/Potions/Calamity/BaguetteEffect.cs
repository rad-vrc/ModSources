using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Calamity
{
	// Token: 0x02000318 RID: 792
	public class BaguetteEffect : IPermanentModdedBuff
	{
		// Token: 0x0600125D RID: 4701 RVA: 0x0008AFB0 File Offset: 0x000891B0
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "BaguetteBuff")] && !PermanentBuffPlayer.PermanentCalamityBuffsBools[11])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "BaguetteBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "BaguetteBuff")] = true;
			}
		}
	}
}
