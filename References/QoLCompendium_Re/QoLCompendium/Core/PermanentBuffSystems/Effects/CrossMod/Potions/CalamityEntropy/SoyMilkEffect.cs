using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.CalamityEntropy
{
	// Token: 0x02000314 RID: 788
	public class SoyMilkEffect : IPermanentModdedBuff
	{
		// Token: 0x06001255 RID: 4693 RVA: 0x0008AD80 File Offset: 0x00088F80
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.calamityEntropyLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityEntropyMod, "SoyMilkBuff")] && !PermanentBuffPlayer.PermanentCalamityBuffsBools[31])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityEntropyMod, "SoyMilkBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityEntropyMod, "SoyMilkBuff")] = true;
			}
		}
	}
}
