using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Thorium
{
	// Token: 0x020002CD RID: 717
	public class BatRepellentEffect : IPermanentModdedBuff
	{
		// Token: 0x060011C7 RID: 4551 RVA: 0x0008883C File Offset: 0x00086A3C
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.thoriumLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "BatRepellentBuff")] && !PermanentBuffPlayer.PermanentThoriumBuffsBools[18])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "BatRepellentBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "BatRepellentBuff")] = true;
			}
		}
	}
}
