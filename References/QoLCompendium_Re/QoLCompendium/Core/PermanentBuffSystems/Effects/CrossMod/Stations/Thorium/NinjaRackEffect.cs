using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Stations.Thorium
{
	// Token: 0x020002C2 RID: 706
	public class NinjaRackEffect : IPermanentModdedBuff
	{
		// Token: 0x060011B1 RID: 4529 RVA: 0x0008827C File Offset: 0x0008647C
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.thoriumLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "NinjaBuff")] && !PermanentBuffPlayer.PermanentThoriumBuffsBools[25])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "NinjaBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "NinjaBuff")] = true;
			}
		}
	}
}
