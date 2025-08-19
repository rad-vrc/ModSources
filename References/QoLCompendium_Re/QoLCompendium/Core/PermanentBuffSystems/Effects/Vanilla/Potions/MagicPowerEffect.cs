using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions
{
	// Token: 0x0200037A RID: 890
	public class MagicPowerEffect : IPermanentBuff
	{
		// Token: 0x06001321 RID: 4897 RVA: 0x0008DEB4 File Offset: 0x0008C0B4
		internal unsafe override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[7] && !PermanentBuffPlayer.PermanentBuffsBools[32])
			{
				*player.Player.GetDamage(DamageClass.Magic) += 0.2f;
				player.Player.buffImmune[7] = true;
			}
		}
	}
}
