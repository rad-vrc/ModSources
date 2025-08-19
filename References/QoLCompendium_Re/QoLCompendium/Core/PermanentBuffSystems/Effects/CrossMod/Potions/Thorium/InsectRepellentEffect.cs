using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Thorium
{
	// Token: 0x020002D9 RID: 729
	public class InsectRepellentEffect : IPermanentModdedBuff
	{
		// Token: 0x060011DF RID: 4575 RVA: 0x00088EBC File Offset: 0x000870BC
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.thoriumLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "InsectRepellentBuff")] && !PermanentBuffPlayer.PermanentThoriumBuffsBools[20])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "InsectRepellentBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "InsectRepellentBuff")] = true;
			}
		}
	}
}
