using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Calamity
{
	// Token: 0x0200031D RID: 797
	public class GravityNormalizerEffect : IPermanentModdedBuff
	{
		// Token: 0x06001267 RID: 4711 RVA: 0x0008B26C File Offset: 0x0008946C
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "GravityNormalizerBuff")] && !PermanentBuffPlayer.PermanentCalamityBuffsBools[16])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "GravityNormalizerBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "GravityNormalizerBuff")] = true;
			}
		}
	}
}
