using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions
{
	// Token: 0x0200036B RID: 875
	public class DangersenseEffect : IPermanentBuff
	{
		// Token: 0x06001303 RID: 4867 RVA: 0x0008D7A1 File Offset: 0x0008B9A1
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[111] && !PermanentBuffPlayer.PermanentBuffsBools[17])
			{
				player.Player.dangerSense = true;
				player.Player.buffImmune[111] = true;
			}
		}
	}
}
