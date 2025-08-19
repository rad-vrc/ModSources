using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions
{
	// Token: 0x0200037E RID: 894
	public class ObsidianSkinEffect : IPermanentBuff
	{
		// Token: 0x06001329 RID: 4905 RVA: 0x0008DFC8 File Offset: 0x0008C1C8
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[1] && !PermanentBuffPlayer.PermanentBuffsBools[36])
			{
				player.Player.lavaImmune = true;
				player.Player.fireWalk = true;
				player.Player.buffImmune[24] = true;
				player.Player.buffImmune[1] = true;
			}
		}
	}
}
