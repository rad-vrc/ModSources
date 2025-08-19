using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Calamity
{
	// Token: 0x0200031C RID: 796
	public class CeaselessHungerEffect : IPermanentModdedBuff
	{
		// Token: 0x06001265 RID: 4709 RVA: 0x0008B1E0 File Offset: 0x000893E0
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "CeaselessHunger")] && !PermanentBuffPlayer.PermanentCalamityBuffsBools[15])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "CeaselessHunger"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "CeaselessHunger")] = true;
			}
		}
	}
}
