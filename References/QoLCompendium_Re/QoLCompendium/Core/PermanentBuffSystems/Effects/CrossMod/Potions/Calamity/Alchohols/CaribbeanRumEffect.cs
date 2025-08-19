using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Calamity.Alchohols
{
	// Token: 0x02000327 RID: 807
	public class CaribbeanRumEffect : IPermanentModdedBuff
	{
		// Token: 0x0600127B RID: 4731 RVA: 0x0008B864 File Offset: 0x00089A64
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "CaribbeanRumBuff")])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "CaribbeanRumBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "CaribbeanRumBuff")] = true;
			}
		}
	}
}
