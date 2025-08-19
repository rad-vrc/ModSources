using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.MartinsOrder
{
	// Token: 0x020002FC RID: 764
	public class BlackHoleEffect : IPermanentModdedBuff
	{
		// Token: 0x06001225 RID: 4645 RVA: 0x0008A124 File Offset: 0x00088324
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.martainsOrderLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.martainsOrderMod, "BlackHoleBuff")] && !PermanentBuffPlayer.PermanentMartinsOrderBuffsBools[0])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.martainsOrderMod, "BlackHoleBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.martainsOrderMod, "BlackHoleBuff")] = true;
			}
		}
	}
}
