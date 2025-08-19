using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions
{
	// Token: 0x02000374 RID: 884
	public class HunterEffect : IPermanentBuff
	{
		// Token: 0x06001315 RID: 4885 RVA: 0x0008DABD File Offset: 0x0008BCBD
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[17] && !PermanentBuffPlayer.PermanentBuffsBools[26])
			{
				player.Player.detectCreature = true;
				player.Player.buffImmune[17] = true;
			}
		}
	}
}
