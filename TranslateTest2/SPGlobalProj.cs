using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace TranslateTest2
{
    public class SPGlobalProj : GlobalProjectile
    {
        public float MultedMinionSlot = 1f;
        public float UpdateCounter;
        public float MinionUpdateSpeed = 1f;
        public bool deviation;
        public bool echo;
        public bool blessing;
        public float LifeSteal;
        public bool contract;
        public float MinionCrit;
        public bool electrified;
        
        public static bool EchoDoubleProj = true;

        public override bool InstancePerEntity => true;

        public override void SendExtraAI(
            Projectile projectile,
            BitWriter bitWriter,
            BinaryWriter binaryWriter)
        {
            binaryWriter.Write(blessing);
            binaryWriter.Write(MinionUpdateSpeed);
        }

        public override void ReceiveExtraAI(
            Projectile projectile,
            BitReader bitReader,
            BinaryReader binaryReader)
        {
            blessing = binaryReader.ReadBoolean();
            var speed = binaryReader.ReadSingle();
            // Sanity clamp to avoid runaway AI iterations
            if (!float.IsFinite(speed) || speed <= 0f) speed = 1f;
            MinionUpdateSpeed = Math.Clamp(speed, 0.1f, 8f);
        }

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (source is EntitySource_ItemUse itemUse && (projectile.minion || projectile.sentry) && itemUse.Item.isMinionSummonItem())
            {
                projectile.minionSlots *= itemUse.Item.global().MinionSlotMult;
                MultedMinionSlot = projectile.minionSlots;
                var speed = itemUse.Item.global().MinionSpeedMult;
                if (!float.IsFinite(speed) || speed <= 0f) speed = 1f;
                MinionUpdateSpeed = Math.Clamp(speed, 0.1f, 8f);
                projectile.scale *= itemUse.Item.global().MinionScaleMult;
                projectile.knockBack *= itemUse.Item.global().MinionKnockbackMult;
                LifeSteal = itemUse.Item.global().MinionLifeSteal;
                MinionCrit = itemUse.Item.global().MinionCritAdd;
                deviation = itemUse.Item.prefix == ModContent.PrefixType<Prefixes.Deviation>();
                echo = itemUse.Item.prefix == ModContent.PrefixType<Prefixes.Echo>();
                blessing = itemUse.Item.prefix == ModContent.PrefixType<Prefixes.Blessing>();
                contract = itemUse.Item.prefix == ModContent.PrefixType<Prefixes.Contract>();
                UpdateCounter = 0f;
                projectile.netUpdate = true;
                electrified = itemUse.Item.global().electrified;
            }
            if (source is EntitySource_Parent parent)
            {
                if (parent.Entity is Projectile p1)
                    LifeSteal = p1.GetGlobalProjectile<SPGlobalProj>().LifeSteal;
                if (parent.Entity is Projectile p2 && (p2.minion || p2.sentry))
                {
                    var pg = p2.GetGlobalProjectile<SPGlobalProj>();
                    if (pg.deviation)
                        projectile.velocity = projectile.velocity.RotatedByRandom(0.3);
                    if (pg.echo && EchoDoubleProj && Utils.NextFloat(Main.rand) < Prefixes.Echo.DoubleChance)
                    {
                        EchoDoubleProj = false;
                        Projectile.NewProjectile(source, projectile.position, projectile.velocity.RotatedByRandom(0.06), projectile.type, projectile.damage / 4, projectile.knockBack, projectile.owner, projectile.ai[0], projectile.ai[1], projectile.ai[2]);
                        EchoDoubleProj = true;
                    }
                }
                projectile.netUpdate = true;
            }
        }

    // 原典では Electrified の追加平坦ダメージ修正は存在しないため削除

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            // Whipタグ処理
            if (ProjectileID.Sets.IsAWhip[projectile.type] && projectile.TryGetOwner(out Player whipPlayer) && whipPlayer.HeldItem.global().wTag != null && whipPlayer.HeldItem.global().CanGiveTag)
                target.AddTag(whipPlayer.HeldItem.global().wTag.Clone());
                
            // LifeSteal効果
            if (LifeSteal > 0f && projectile.TryGetOwner(out Player lifeStealPlayer) && lifeStealPlayer.GetModPlayer<SPModPlayer>().HealMe((int)Math.Ceiling(lifeStealPlayer.statLifeMax2 * LifeSteal), 5))
            {
                if (!Main.dedServ)
                {
                    for (float num = 0f; num <= 1f; num += 0.005f)
                    {
                        Dust dust = Dust.NewDustDirect(Vector2.Lerp(lifeStealPlayer.Center, target.Center, num), 1, 1, DustID.SomethingRed, 0f, 0f, 0, new Color(), 1f);
                        dust.velocity = Utils.ToRotationVector2(Utils.NextFloat(Main.rand) * MathHelper.TwoPi) * Utils.NextFloat(Main.rand, -1f, 1f) + Utils.SafeNormalize(lifeStealPlayer.Center - target.Center, Vector2.Zero) * 3.6f;
                        dust.scale = 0.8f;
                        dust.position = Vector2.Lerp(lifeStealPlayer.Center, target.Center, num);
                    }
                }
            }
        }

        public override bool PreAI(Projectile projectile)
        {
            // Electrified効果: 定期的な電撃レーザー生成
            if (electrified && Main.GameUpdateCount % 12U == 0U && projectile.TryGetOwner(out Player electrifiedPlayer))
            {
                int laserDamage = (int)(electrifiedPlayer.statLifeMax2 * Prefixes.Electrified.DmgMultByPlayerMaxHealth);
                Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, Vector2.Zero, ModContent.ProjectileType<Projectiles.ElectricLaser>(), laserDamage, 0.0f, electrifiedPlayer.whoAmI, electrifiedPlayer.Center.X, electrifiedPlayer.Center.Y, 0.0f);
            }

            return true;
        }
    }
}