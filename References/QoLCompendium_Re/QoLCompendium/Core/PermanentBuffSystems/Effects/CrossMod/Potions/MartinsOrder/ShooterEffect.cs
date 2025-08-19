using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.MartinsOrder
{
	// Token: 0x02000305 RID: 773
	public class ShooterEffect : IPermanentModdedBuff
	{
		// Token: 0x06001237 RID: 4663 RVA: 0x0008A5EC File Offset: 0x000887EC
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.martainsOrderLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.martainsOrderMod, "ShooterBuff")] && !PermanentBuffPlayer.PermanentMartinsOrderBuffsBools[9])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.martainsOrderMod, "ShooterBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.martainsOrderMod, "ShooterBuff")] = true;
			}
		}
	}
}
