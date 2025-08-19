using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.MartinsOrder
{
	// Token: 0x02000306 RID: 774
	public class SoulEffect : IPermanentModdedBuff
	{
		// Token: 0x06001239 RID: 4665 RVA: 0x0008A678 File Offset: 0x00088878
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.martainsOrderLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.martainsOrderMod, "SoulBuff")] && !PermanentBuffPlayer.PermanentMartinsOrderBuffsBools[10])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.martainsOrderMod, "SoulBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.martainsOrderMod, "SoulBuff")] = true;
			}
		}
	}
}
