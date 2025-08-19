using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Calamity.Alchohols
{
	// Token: 0x0200032E RID: 814
	public class MoonshineEffect : IPermanentModdedBuff
	{
		// Token: 0x06001289 RID: 4745 RVA: 0x0008BBE4 File Offset: 0x00089DE4
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "MoonshineBuff")])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "MoonshineBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "MoonshineBuff")] = true;
			}
		}
	}
}
