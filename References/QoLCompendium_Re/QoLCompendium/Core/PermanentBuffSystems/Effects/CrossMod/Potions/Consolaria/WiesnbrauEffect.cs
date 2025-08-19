using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Consolaria
{
	// Token: 0x0200030D RID: 781
	public class WiesnbrauEffect : IPermanentModdedBuff
	{
		// Token: 0x06001247 RID: 4679 RVA: 0x0008AA4C File Offset: 0x00088C4C
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.consolariaLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.consolariaMod, "Drunk")])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.consolariaMod, "Drunk"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.consolariaMod, "Drunk")] = true;
			}
		}
	}
}
