using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Arena.Thorium
{
	// Token: 0x02000344 RID: 836
	public class MistletoeEffect : IPermanentModdedBuff
	{
		// Token: 0x060012B5 RID: 4789 RVA: 0x0008C744 File Offset: 0x0008A944
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.thoriumLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "MistletoeBuff")] && !PermanentBuffPlayer.PermanentThoriumBuffsBools[0])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "MistletoeBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "MistletoeBuff")] = true;
			}
		}
	}
}
