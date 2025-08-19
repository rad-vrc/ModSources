using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Thorium
{
	// Token: 0x020002DE RID: 734
	public class WarmongerEffect : IPermanentModdedBuff
	{
		// Token: 0x060011E9 RID: 4585 RVA: 0x0008916C File Offset: 0x0008736C
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.thoriumLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "WarmongerBuff")] && !PermanentBuffPlayer.PermanentThoriumBuffsBools[17])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "WarmongerBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "WarmongerBuff")] = true;
			}
		}
	}
}
