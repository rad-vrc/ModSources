using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions
{
	// Token: 0x02000365 RID: 869
	public class ArcheryEffect : IPermanentBuff
	{
		// Token: 0x060012F7 RID: 4855 RVA: 0x0008D5E8 File Offset: 0x0008B7E8
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[16] && !PermanentBuffPlayer.PermanentBuffsBools[11])
			{
				player.Player.archery = true;
				player.Player.arrowDamage *= 1.1f;
				player.Player.buffImmune[16] = true;
			}
		}
	}
}
