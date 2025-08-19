using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions
{
	// Token: 0x02000366 RID: 870
	public class BattleEffect : IPermanentBuff
	{
		// Token: 0x060012F9 RID: 4857 RVA: 0x0008D645 File Offset: 0x0008B845
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[13] && !PermanentBuffPlayer.PermanentBuffsBools[12])
			{
				player.Player.enemySpawns = true;
				player.Player.buffImmune[13] = true;
			}
		}
	}
}
