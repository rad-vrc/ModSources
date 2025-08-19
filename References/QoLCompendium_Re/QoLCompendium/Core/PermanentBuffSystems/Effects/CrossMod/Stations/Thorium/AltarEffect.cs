using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Stations.Thorium
{
	// Token: 0x020002C0 RID: 704
	public class AltarEffect : IPermanentModdedBuff
	{
		// Token: 0x060011AD RID: 4525 RVA: 0x00088164 File Offset: 0x00086364
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.thoriumLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "AltarBuff")] && !PermanentBuffPlayer.PermanentThoriumBuffsBools[23])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "AltarBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "AltarBuff")] = true;
			}
		}
	}
}
