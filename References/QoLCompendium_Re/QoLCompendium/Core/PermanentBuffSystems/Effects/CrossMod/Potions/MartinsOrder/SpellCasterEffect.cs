using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.MartinsOrder
{
	// Token: 0x02000307 RID: 775
	public class SpellCasterEffect : IPermanentModdedBuff
	{
		// Token: 0x0600123B RID: 4667 RVA: 0x0008A704 File Offset: 0x00088904
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.martainsOrderLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.martainsOrderMod, "CasterBuff")] && !PermanentBuffPlayer.PermanentMartinsOrderBuffsBools[11])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.martainsOrderMod, "CasterBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.martainsOrderMod, "CasterBuff")] = true;
			}
		}
	}
}
