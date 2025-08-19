using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions
{
	// Token: 0x0200036A RID: 874
	public class CrateEffect : IPermanentBuff
	{
		// Token: 0x06001301 RID: 4865 RVA: 0x0008D76A File Offset: 0x0008B96A
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[123] && !PermanentBuffPlayer.PermanentBuffsBools[16])
			{
				player.Player.cratePotion = true;
				player.Player.buffImmune[123] = true;
			}
		}
	}
}
