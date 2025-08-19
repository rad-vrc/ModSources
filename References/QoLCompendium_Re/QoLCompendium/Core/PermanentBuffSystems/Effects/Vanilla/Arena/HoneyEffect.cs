using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Arena
{
	// Token: 0x0200039A RID: 922
	public class HoneyEffect : IPermanentBuff
	{
		// Token: 0x06001361 RID: 4961 RVA: 0x0008E950 File Offset: 0x0008CB50
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[48] && !PermanentBuffPlayer.PermanentBuffsBools[4])
			{
				player.Player.lifeRegenTime += 2f;
				player.Player.lifeRegen += 2;
				player.Player.buffImmune[48] = true;
			}
		}
	}
}
