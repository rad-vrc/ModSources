using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions
{
	// Token: 0x02000372 RID: 882
	public class GravitationEffect : IPermanentBuff
	{
		// Token: 0x06001311 RID: 4881 RVA: 0x0008DA4F File Offset: 0x0008BC4F
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[18] && !PermanentBuffPlayer.PermanentBuffsBools[24])
			{
				player.Player.gravControl = true;
				player.Player.buffImmune[18] = true;
			}
		}
	}
}
