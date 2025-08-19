using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions
{
	// Token: 0x02000373 RID: 883
	public class HeartreachEffect : IPermanentBuff
	{
		// Token: 0x06001313 RID: 4883 RVA: 0x0008DA86 File Offset: 0x0008BC86
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[105] && !PermanentBuffPlayer.PermanentBuffsBools[25])
			{
				player.Player.lifeMagnet = true;
				player.Player.buffImmune[105] = true;
			}
		}
	}
}
