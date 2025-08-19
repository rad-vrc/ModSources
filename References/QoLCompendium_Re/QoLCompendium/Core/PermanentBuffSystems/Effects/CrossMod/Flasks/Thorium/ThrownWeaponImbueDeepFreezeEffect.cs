using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Flasks.Thorium
{
	// Token: 0x0200033C RID: 828
	public class ThrownWeaponImbueDeepFreezeEffect : IPermanentModdedBuff
	{
		// Token: 0x060012A5 RID: 4773 RVA: 0x0008C2E4 File Offset: 0x0008A4E4
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.thoriumLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "DeepFreezeCoatingBuff")])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "DeepFreezeCoatingBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "DeepFreezeCoatingBuff")] = true;
				Common.HandleCoatingBuffs(player.Player);
			}
		}
	}
}
