using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions
{
	// Token: 0x0200037C RID: 892
	public class MiningEffect : IPermanentBuff
	{
		// Token: 0x06001325 RID: 4901 RVA: 0x0008DF44 File Offset: 0x0008C144
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[104] && !PermanentBuffPlayer.PermanentBuffsBools[34])
			{
				player.Player.pickSpeed -= 0.25f;
				player.Player.buffImmune[104] = true;
			}
		}
	}
}
