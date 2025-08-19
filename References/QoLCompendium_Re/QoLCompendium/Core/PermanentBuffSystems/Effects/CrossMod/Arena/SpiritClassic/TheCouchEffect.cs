using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Arena.SpiritClassic
{
	// Token: 0x02000348 RID: 840
	public class TheCouchEffect : IPermanentModdedBuff
	{
		// Token: 0x060012BD RID: 4797 RVA: 0x0008C964 File Offset: 0x0008AB64
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.spiritLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.spiritMod, "CouchPotato")] && !PermanentBuffPlayer.PermanentSpiritClassicBuffsBools[3])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.spiritMod, "CouchPotato"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.spiritMod, "CouchPotato")] = true;
			}
		}
	}
}
