using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Thorium
{
	// Token: 0x020002CB RID: 715
	public class ArtilleryEffect : IPermanentModdedBuff
	{
		// Token: 0x060011C3 RID: 4547 RVA: 0x0008872C File Offset: 0x0008692C
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.thoriumLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "ArtilleryBuff")] && !PermanentBuffPlayer.PermanentThoriumBuffsBools[3])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "ArtilleryBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "ArtilleryBuff")] = true;
			}
		}
	}
}
