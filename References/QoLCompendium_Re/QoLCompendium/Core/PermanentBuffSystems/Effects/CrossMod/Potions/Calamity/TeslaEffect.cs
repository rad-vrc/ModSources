using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Calamity
{
	// Token: 0x02000323 RID: 803
	public class TeslaEffect : IPermanentModdedBuff
	{
		// Token: 0x06001273 RID: 4723 RVA: 0x0008B5B4 File Offset: 0x000897B4
		internal override void ApplyEffect(PermanentBuffPlayer player)
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			if (!player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "TeslaBuff")] && !PermanentBuffPlayer.PermanentCalamityBuffsBools[22])
			{
				this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "TeslaBuff"));
				this.buffToApply.Update(player.Player, ref this.index);
				player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "TeslaBuff")] = true;
				if (player.Player.ownedProjectileCounts[Common.GetModProjectile(ModConditions.calamityMod, "TeslaAura")] < 1)
				{
					int damage = (int)player.Player.GetBestClassDamage().ApplyTo(10f);
					Projectile.NewProjectile(player.Player.GetSource_FromAI(null), player.Player.Center, Vector2.Zero, Common.GetModProjectile(ModConditions.calamityMod, "TeslaAura"), damage, 0f, player.Player.whoAmI, 0f, 0f, 0f);
				}
			}
		}
	}
}
