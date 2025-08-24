using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TranslateTest2.Common.GlobalProjectiles; // SPGlobalProj

namespace TranslateTest2.Common.Players
{
    public class SPModPlayer : ModPlayer
    {
        public int HealingCd;

        public bool HealMe(int amount, int Cooldown)
        {
            if (HealingCd > 0) return false;
            HealingCd = Cooldown;
            Player.Heal(amount);
            return true;
        }

        public override void PostUpdateMiscEffects()
        {
            HealingCd--;
            float blessingBonus = 0f;
            if (!Player.HeldItem.IsAir && Player.HeldItem.prefix == ModContent.PrefixType<Content.Prefixes.Focused_Whip>())
            {
                Player.GetDamage(DamageClass.Summon) += Content.Prefixes.Focused_Whip.SummonDmgAddition;
                Player.maxMinions -= Content.Prefixes.Focused_Whip.MinionSlotDecrease;
            }
            foreach (var proj in Main.ActiveProjectiles)
            {
                if ((proj.minion || proj.sentry) && proj.owner == Player.whoAmI && proj.GetGlobalProjectile<SPGlobalProj>().contract)
                    Player.statDefense -= Content.Prefixes.Contract.ArmorDecrease;
                if (proj.GetGlobalProjectile<SPGlobalProj>().blessing && proj.Distance(Player.Center) < Content.Prefixes.Blessing.range)
                {
                    blessingBonus += Content.Prefixes.Blessing.Bonus;
                    for (float num2 = 0f; num2 <= 1f; num2 += 0.01f)
                    {
                        Dust dust = Dust.NewDustDirect(Vector2.Lerp(Player.Center, proj.Center, num2), 1, 1, DustID.TerraBlade, 0f, 0f, 0, new Color(), 1f);
                        dust.velocity = Vector2.Zero;
                        dust.noGravity = true;
                        dust.scale = 0.4f;
                        dust.position = Vector2.Lerp(Player.Center, proj.Center, num2);
                    }
                }
            }
            if (blessingBonus > 0f)
            {
                Player.statDefense += (int)Math.Ceiling(blessingBonus * 3f);
                Player.lifeRegen += (int)Math.Ceiling(blessingBonus * 5f);
                Player.GetDamage(DamageClass.Generic) += blessingBonus * 0.02f;
            }
        }
    }
}
