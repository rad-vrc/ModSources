using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions
{
	// Token: 0x02000383 RID: 899
	public class SonarEffect : IPermanentBuff
	{
		// Token: 0x06001333 RID: 4915 RVA: 0x0008E260 File Offset: 0x0008C460
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[122] && !PermanentBuffPlayer.PermanentBuffsBools[41])
			{
				player.Player.sonarPotion = true;
				player.Player.buffImmune[122] = true;
			}
		}
	}
}
