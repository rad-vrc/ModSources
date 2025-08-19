using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Clamity
{
	// Token: 0x02000310 RID: 784
	public class SupremeLuckEffect : IPermanentModdedBuff
	{
		// Token: 0x0600124D RID: 4685 RVA: 0x0008ABA8 File Offset: 0x00088DA8
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.clamityAddonLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.clamityAddonMod, "SupremeLucky")] && !PermanentBuffPlayer.PermanentCalamityBuffsBools[28])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.clamityAddonMod, "SupremeLucky"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.clamityAddonMod, "SupremeLucky")] = true;
			}
		}
	}
}
