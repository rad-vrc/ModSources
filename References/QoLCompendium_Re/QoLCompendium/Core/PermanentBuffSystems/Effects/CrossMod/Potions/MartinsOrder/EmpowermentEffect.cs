using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.MartinsOrder
{
	// Token: 0x020002FF RID: 767
	public class EmpowermentEffect : IPermanentModdedBuff
	{
		// Token: 0x0600122B RID: 4651 RVA: 0x0008A2BC File Offset: 0x000884BC
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.martainsOrderLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.martainsOrderMod, "EmpowermentBuff")] && !PermanentBuffPlayer.PermanentMartinsOrderBuffsBools[3])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.martainsOrderMod, "EmpowermentBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.martainsOrderMod, "EmpowermentBuff")] = true;
			}
		}
	}
}
