using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Calamity
{
	// Token: 0x02000319 RID: 793
	public class BloodfinEffect : IPermanentModdedBuff
	{
		// Token: 0x0600125F RID: 4703 RVA: 0x0008B03C File Offset: 0x0008923C
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "BloodfinBoost")] && !PermanentBuffPlayer.PermanentCalamityBuffsBools[12])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "BloodfinBoost"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "BloodfinBoost")] = true;
			}
		}
	}
}
