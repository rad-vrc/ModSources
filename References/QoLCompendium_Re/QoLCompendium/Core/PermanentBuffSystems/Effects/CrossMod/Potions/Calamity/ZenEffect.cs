using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Calamity
{
	// Token: 0x02000324 RID: 804
	public class ZenEffect : IPermanentModdedBuff
	{
		// Token: 0x06001275 RID: 4725 RVA: 0x0008B6CC File Offset: 0x000898CC
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "Zen")] && !PermanentBuffPlayer.PermanentCalamityBuffsBools[23])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "Zen"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "Zen")] = true;
			}
		}
	}
}
