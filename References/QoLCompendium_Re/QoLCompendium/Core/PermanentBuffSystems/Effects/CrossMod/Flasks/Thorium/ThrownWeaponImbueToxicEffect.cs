using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Flasks.Thorium
{
	// Token: 0x02000340 RID: 832
	public class ThrownWeaponImbueToxicEffect : IPermanentModdedBuff
	{
		// Token: 0x060012AD RID: 4781 RVA: 0x0008C514 File Offset: 0x0008A714
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.thoriumLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "ToxicCoatingBuff")])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.thoriumMod, "ToxicCoatingBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.thoriumMod, "ToxicCoatingBuff")] = true;
				Common.HandleCoatingBuffs(player.Player);
			}
		}
	}
}
