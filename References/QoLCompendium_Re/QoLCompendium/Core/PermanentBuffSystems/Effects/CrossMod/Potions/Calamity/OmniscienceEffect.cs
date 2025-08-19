using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Calamity
{
	// Token: 0x0200031E RID: 798
	public class OmniscienceEffect : IPermanentModdedBuff
	{
		// Token: 0x06001269 RID: 4713 RVA: 0x0008B2F8 File Offset: 0x000894F8
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "Omniscience")] && !PermanentBuffPlayer.PermanentCalamityBuffsBools[17])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "Omniscience"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "Omniscience")] = true;
			}
		}
	}
}
