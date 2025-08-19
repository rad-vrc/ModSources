using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Calamity.Alchohols
{
	// Token: 0x02000337 RID: 823
	public class TequilaSunriseEffect : IPermanentModdedBuff
	{
		// Token: 0x0600129B RID: 4763 RVA: 0x0008C064 File Offset: 0x0008A264
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "TequilaSunriseBuff")])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "TequilaSunriseBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "TequilaSunriseBuff")] = true;
			}
		}
	}
}
