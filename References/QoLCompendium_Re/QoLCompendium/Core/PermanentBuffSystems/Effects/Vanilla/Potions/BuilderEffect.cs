using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions
{
	// Token: 0x02000368 RID: 872
	public class BuilderEffect : IPermanentBuff
	{
		// Token: 0x060012FD RID: 4861 RVA: 0x0008D6BC File Offset: 0x0008B8BC
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[107] && !PermanentBuffPlayer.PermanentBuffsBools[14])
			{
				player.Player.tileSpeed += 0.25f;
				player.Player.wallSpeed += 0.25f;
				player.Player.blockRange++;
				player.Player.buffImmune[107] = true;
			}
		}
	}
}
