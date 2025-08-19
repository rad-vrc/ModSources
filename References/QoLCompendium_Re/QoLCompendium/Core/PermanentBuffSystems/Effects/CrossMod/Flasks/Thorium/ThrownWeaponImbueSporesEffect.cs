using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Flasks.Thorium
{
	// Token: 0x0200033F RID: 831
	public class ThrownWeaponImbueSporesEffect : IPermanentModdedBuff
	{
		// Token: 0x060012AB RID: 4779 RVA: 0x0008C488 File Offset: 0x0008A688
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.thoriumLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "SporeCoatingBuff")])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "SporeCoatingBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "SporeCoatingBuff")] = true;
				Common.HandleCoatingBuffs(player.Player);
			}
		}
	}
}
