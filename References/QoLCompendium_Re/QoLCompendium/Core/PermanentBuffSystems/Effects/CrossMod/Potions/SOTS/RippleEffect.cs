using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.SOTS
{
	// Token: 0x020002F8 RID: 760
	public class RippleEffect : IPermanentModdedBuff
	{
		// Token: 0x0600121D RID: 4637 RVA: 0x00089F30 File Offset: 0x00088130
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.secretsOfTheShadowsLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "RippleBuff")] && !PermanentBuffPlayer.PermanentSOTSBuffsBools[6])
			{
				player.Player.buffImmune[Common.GetModBuff(ModConditions.secretsOfTheShadowsMod, "RippleBuff")] = true;
			}
		}
	}
}
