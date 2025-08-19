using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions
{
	// Token: 0x02000379 RID: 889
	public class LuckyEffect : IPermanentBuff
	{
		// Token: 0x0600131F RID: 4895 RVA: 0x0008DE60 File Offset: 0x0008C060
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[257] && !PermanentBuffPlayer.PermanentBuffsBools[31])
			{
				player.Player.luck += 0.3f;
				player.Player.buffImmune[257] = true;
			}
		}
	}
}
