using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Clicker
{
	// Token: 0x0200030E RID: 782
	public class InfluenceEffect : IPermanentModdedBuff
	{
		// Token: 0x06001249 RID: 4681 RVA: 0x0008AACC File Offset: 0x00088CCC
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.clickerClassLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.clickerClassMod, "InfluenceBuff")])
			{
				player.Player.buffImmune[Common.GetModBuff(ModConditions.clickerClassMod, "InfluenceBuff")] = true;
			}
		}
	}
}
