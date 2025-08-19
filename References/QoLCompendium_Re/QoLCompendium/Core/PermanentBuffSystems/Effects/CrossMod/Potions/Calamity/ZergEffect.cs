using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Calamity
{
	// Token: 0x02000325 RID: 805
	public class ZergEffect : IPermanentModdedBuff
	{
		// Token: 0x06001277 RID: 4727 RVA: 0x0008B758 File Offset: 0x00089958
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "Zerg")] && !PermanentBuffPlayer.PermanentCalamityBuffsBools[24])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "Zerg"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "Zerg")] = true;
			}
		}
	}
}
