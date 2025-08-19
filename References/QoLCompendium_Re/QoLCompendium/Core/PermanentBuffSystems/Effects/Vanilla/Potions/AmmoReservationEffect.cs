using System;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.Vanilla.Potions
{
	// Token: 0x02000364 RID: 868
	public class AmmoReservationEffect : IPermanentBuff
	{
		// Token: 0x060012F5 RID: 4853 RVA: 0x0008D5AF File Offset: 0x0008B7AF
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!player.Player.buffImmune[112] && !PermanentBuffPlayer.PermanentBuffsBools[10])
			{
				player.Player.ammoPotion = true;
				player.Player.buffImmune[112] = true;
			}
		}
	}
}
