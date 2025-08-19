using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Thorium
{
	// Token: 0x020002D4 RID: 724
	public class FishRepellentEffect : IPermanentModdedBuff
	{
		// Token: 0x060011D5 RID: 4565 RVA: 0x00088C00 File Offset: 0x00086E00
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.thoriumLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "FishRepellentBuff")] && !PermanentBuffPlayer.PermanentThoriumBuffsBools[19])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "FishRepellentBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "FishRepellentBuff")] = true;
			}
		}
	}
}
