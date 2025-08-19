using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions
{
	// Token: 0x02000377 RID: 887
	public class IronskinEffect : IPermanentBuff
	{
		// Token: 0x0600131B RID: 4891 RVA: 0x0008DDBC File Offset: 0x0008BFBC
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[5] && !PermanentBuffPlayer.PermanentBuffsBools[29])
			{
				player.Player.statDefense += 8;
				player.Player.buffImmune[5] = true;
			}
		}
	}
}
