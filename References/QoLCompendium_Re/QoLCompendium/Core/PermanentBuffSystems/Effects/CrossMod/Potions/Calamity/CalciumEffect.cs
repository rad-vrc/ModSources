using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Calamity
{
	// Token: 0x0200031B RID: 795
	public class CalciumEffect : IPermanentModdedBuff
	{
		// Token: 0x06001263 RID: 4707 RVA: 0x0008B154 File Offset: 0x00089354
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "CalciumBuff")] && !PermanentBuffPlayer.PermanentCalamityBuffsBools[14])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "CalciumBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "CalciumBuff")] = true;
			}
		}
	}
}
