using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Calamity
{
	// Token: 0x0200031A RID: 794
	public class BoundingEffect : IPermanentModdedBuff
	{
		// Token: 0x06001261 RID: 4705 RVA: 0x0008B0C8 File Offset: 0x000892C8
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "BoundingBuff")] && !PermanentBuffPlayer.PermanentCalamityBuffsBools[13])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "BoundingBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "BoundingBuff")] = true;
			}
		}
	}
}
