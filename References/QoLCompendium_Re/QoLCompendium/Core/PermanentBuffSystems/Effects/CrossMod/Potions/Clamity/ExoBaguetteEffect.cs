using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Clamity
{
	// Token: 0x0200030F RID: 783
	public class ExoBaguetteEffect : IPermanentModdedBuff
	{
		// Token: 0x0600124B RID: 4683 RVA: 0x0008AB1C File Offset: 0x00088D1C
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.clamityAddonLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.clamityAddonMod, "ExoBaguetteBuff")] && !PermanentBuffPlayer.PermanentCalamityBuffsBools[27])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.clamityAddonMod, "ExoBaguetteBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.clamityAddonMod, "ExoBaguetteBuff")] = true;
			}
		}
	}
}
