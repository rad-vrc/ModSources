using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions
{
	// Token: 0x02000381 RID: 897
	public class RegenerationEffect : IPermanentBuff
	{
		// Token: 0x0600132F RID: 4911 RVA: 0x0008E193 File Offset: 0x0008C393
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[2] && !PermanentBuffPlayer.PermanentBuffsBools[39])
			{
				player.Player.lifeRegen += 4;
				player.Player.buffImmune[2] = true;
			}
		}
	}
}
