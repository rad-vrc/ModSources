using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Calamity
{
	// Token: 0x02000321 RID: 801
	public class SoaringEffect : IPermanentModdedBuff
	{
		// Token: 0x0600126F RID: 4719 RVA: 0x0008B49C File Offset: 0x0008969C
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "Soaring")] && !PermanentBuffPlayer.PermanentCalamityBuffsBools[20])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "Soaring"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "Soaring")] = true;
			}
		}
	}
}
