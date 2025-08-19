using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions
{
	// Token: 0x02000389 RID: 905
	public class TitanEffect : IPermanentBuff
	{
		// Token: 0x0600133F RID: 4927 RVA: 0x0008E455 File Offset: 0x0008C655
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[108] && !PermanentBuffPlayer.PermanentBuffsBools[47])
			{
				player.Player.kbBuff = true;
				player.Player.buffImmune[108] = true;
			}
		}
	}
}
