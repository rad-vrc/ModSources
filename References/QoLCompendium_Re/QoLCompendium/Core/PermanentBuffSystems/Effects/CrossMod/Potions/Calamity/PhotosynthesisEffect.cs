using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Calamity
{
	// Token: 0x0200031F RID: 799
	public class PhotosynthesisEffect : IPermanentModdedBuff
	{
		// Token: 0x0600126B RID: 4715 RVA: 0x0008B384 File Offset: 0x00089584
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "PhotosynthesisBuff")] && !PermanentBuffPlayer.PermanentCalamityBuffsBools[18])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "PhotosynthesisBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "PhotosynthesisBuff")] = true;
			}
		}
	}
}
