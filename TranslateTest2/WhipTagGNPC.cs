using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TranslateTest2
{
    public class WhipTagGNPC : GlobalNPC
    {
        public List<WhipTag> tags = new List<WhipTag>();
        public override bool InstancePerEntity => true;

        public override bool PreAI(NPC npc)
        {
            if (tags.Count > 0)
            {
                for (int i = tags.Count - 1; i >= 0; i--)
                {
                    tags[i].TimeLeft--;
                    if (tags[i].TimeLeft <= 0)
                        tags.RemoveAt(i);
                }
            }
            bool hasDevoted = false;
            foreach (var tag in tags)
            {
                if (tag.SpecialType == "Devoted") { hasDevoted = true; break; }
            }
            if (hasDevoted)
            {
                for (float num = 0f; num < 360f; num += 2f)
                {
                    Dust dust = Dust.NewDustDirect(npc.Center + Utils.ToRotationVector2(MathHelper.ToRadians(num)) * 24f, 1, 1, DustID.Demonite, 0f, 0f, 0, new Color(), 1f);
                    dust.velocity = Vector2.Zero;
                    dust.noGravity = true;
                    dust.scale = 0.8f;
                }
            }
            return true;
        }

        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if (projectile.npcProj || projectile.trap || !projectile.IsMinionOrSentryRelated) return;
            bool hasDevoted = false;
            float tagDamageMultBase = ProjectileID.Sets.SummonTagDamageMultiplier[projectile.type];
            int flatTagDamage = 0;
            float critAdd = 0f;
            float sourceDamageMult = 1f;
            critAdd += projectile.GetGlobalProjectile<SPGlobalProj>().MinionCrit;
            foreach (var tag in tags)
            {
                if (tag.SpecialType == "Devoted") hasDevoted = true;
                flatTagDamage += tag.TagDamage;
                critAdd += tag.CritAdd;
                sourceDamageMult += (tag.TagDamageMult - 1f) * tagDamageMultBase;
            }
            if (projectile.TryGetOwner(out Player player))
            {
                var item = player.HeldItem;
                if (item.shoot > 0 && ProjectileID.Sets.IsAWhip[item.shoot] && item.global().wTag.SpecialType == "Devoted")
                {
                    if (hasDevoted) sourceDamageMult += Prefixes.Devoted.Increase; else sourceDamageMult -= Prefixes.Devoted.Decrease;
                }
            }
            modifiers.FlatBonusDamage += flatTagDamage * tagDamageMultBase;
            modifiers.SourceDamage *= sourceDamageMult;
            if (Utils.NextFloat(Main.rand) < critAdd) modifiers.SetCrit();
        }
    }
}