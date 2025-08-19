using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions
{
	// Token: 0x0200038A RID: 906
	public class WarmthEffect : IPermanentBuff
	{
		// Token: 0x06001341 RID: 4929 RVA: 0x0008E48C File Offset: 0x0008C68C
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[124] && !PermanentBuffPlayer.PermanentBuffsBools[48])
			{
				player.Player.resistCold = true;
				player.Player.buffImmune[124] = true;
			}
		}
	}
}
