using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.MartinsOrder
{
	// Token: 0x020002FE RID: 766
	public class DefenderEffect : IPermanentModdedBuff
	{
		// Token: 0x06001229 RID: 4649 RVA: 0x0008A234 File Offset: 0x00088434
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.martainsOrderLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.martainsOrderMod, "TurretBuff")] && !PermanentBuffPlayer.PermanentMartinsOrderBuffsBools[2])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.martainsOrderMod, "TurretBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.martainsOrderMod, "TurretBuff")] = true;
			}
		}
	}
}
