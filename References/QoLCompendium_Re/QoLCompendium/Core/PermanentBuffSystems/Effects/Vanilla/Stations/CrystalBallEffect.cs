using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Stations
{
	// Token: 0x02000360 RID: 864
	public class CrystalBallEffect : IPermanentBuff
	{
		// Token: 0x060012ED RID: 4845 RVA: 0x0008D3F8 File Offset: 0x0008B5F8
		internal unsafe override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[29] && !PermanentBuffPlayer.PermanentBuffsBools[54])
			{
				*player.Player.GetDamage(DamageClass.Magic) += 0.05f;
				*player.Player.GetCritChance(DamageClass.Magic) += 2f;
				player.Player.statManaMax2 += 20;
				player.Player.manaCost -= 0.02f;
				player.Player.buffImmune[29] = true;
			}
		}
	}
}
