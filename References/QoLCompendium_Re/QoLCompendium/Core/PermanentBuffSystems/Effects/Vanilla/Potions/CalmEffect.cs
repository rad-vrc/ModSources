using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions
{
	// Token: 0x02000369 RID: 873
	public class CalmEffect : IPermanentBuff
	{
		// Token: 0x060012FF RID: 4863 RVA: 0x0008D733 File Offset: 0x0008B933
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[106] && !PermanentBuffPlayer.PermanentBuffsBools[15])
			{
				player.Player.calmed = true;
				player.Player.buffImmune[106] = true;
			}
		}
	}
}
