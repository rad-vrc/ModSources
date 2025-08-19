using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Calamity
{
	// Token: 0x02000316 RID: 790
	public class AnechoicCoatingEffect : IPermanentModdedBuff
	{
		// Token: 0x06001259 RID: 4697 RVA: 0x0008AE98 File Offset: 0x00089098
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "AnechoicCoatingBuff")] && !PermanentBuffPlayer.PermanentCalamityBuffsBools[9])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "AnechoicCoatingBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "AnechoicCoatingBuff")] = true;
			}
		}
	}
}
