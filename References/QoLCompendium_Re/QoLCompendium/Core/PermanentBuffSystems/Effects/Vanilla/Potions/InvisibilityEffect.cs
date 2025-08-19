using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions
{
	// Token: 0x02000376 RID: 886
	public class InvisibilityEffect : IPermanentBuff
	{
		// Token: 0x06001319 RID: 4889 RVA: 0x0008DD85 File Offset: 0x0008BF85
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[10] && !PermanentBuffPlayer.PermanentBuffsBools[28])
			{
				player.Player.invis = true;
				player.Player.buffImmune[10] = true;
			}
		}
	}
}
