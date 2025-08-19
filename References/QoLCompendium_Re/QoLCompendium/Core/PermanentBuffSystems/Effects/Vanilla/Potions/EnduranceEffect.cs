using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions
{
	// Token: 0x0200036C RID: 876
	public class EnduranceEffect : IPermanentBuff
	{
		// Token: 0x06001305 RID: 4869 RVA: 0x0008D7D8 File Offset: 0x0008B9D8
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[114] && !PermanentBuffPlayer.PermanentBuffsBools[18])
			{
				player.Player.endurance += 0.1f;
				player.Player.buffImmune[114] = true;
			}
		}
	}
}
