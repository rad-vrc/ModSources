// Decompiled with JetBrains decompiler
// Type: SummonerPrefix.SPGlobalProj
// Assembly: SummonerPrefix, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8925F543-424F-4302-A9BE-42DFABB73BB2
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModReader\SummonerPrefix\SummonerPrefix.dll

using Microsoft.Xna.Framework;
using SummonerPrefix.Prefixes;
using SummonerPrefix.Projectiles;
using System;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace SummonerPrefix;

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

  public virtual bool InstancePerEntity => true;

  public virtual void SendExtraAI(
    Projectile projectile,
    BitWriter bitWriter,
    BinaryWriter binaryWriter)
  {
    binaryWriter.Write(this.blessing);
    binaryWriter.Write(this.MinionUpdateSpeed);
  }

  public virtual void ReceiveExtraAI(
    Projectile projectile,
    BitReader bitReader,
    BinaryReader binaryReader)
  {
    this.blessing = binaryReader.ReadBoolean();
    this.MinionUpdateSpeed = binaryReader.ReadSingle();
  }

  public virtual void OnSpawn(Projectile projectile, IEntitySource source)
  {
    if (source is EntitySource_ItemUse entitySourceItemUse && (projectile.minion || projectile.sentry) && entitySourceItemUse.Item.isMinionSummonItem())
    {
      projectile.minionSlots *= entitySourceItemUse.Item.global().MinionSlotMult;
      this.MultedMinionSlot = projectile.minionSlots;
      this.MinionUpdateSpeed = entitySourceItemUse.Item.global().MinionSpeedMult;
      projectile.scale *= entitySourceItemUse.Item.global().MinionScaleMult;
      projectile.knockBack *= entitySourceItemUse.Item.global().MinionKnockbackMult;
      this.LifeSteal = entitySourceItemUse.Item.global().MinionLifeSteal;
      this.MinionCrit = entitySourceItemUse.Item.global().MinionCritAdd;
      this.deviation = entitySourceItemUse.Item.prefix == ModContent.PrefixType<Deviation>();
      this.echo = entitySourceItemUse.Item.prefix == ModContent.PrefixType<Echo>();
      this.blessing = entitySourceItemUse.Item.prefix == ModContent.PrefixType<Blessing>();
      this.contract = entitySourceItemUse.Item.prefix == ModContent.PrefixType<Contract>();
      projectile.netUpdate = true;
      this.electrified = entitySourceItemUse.Item.global().electrified;
    }
    if (!(source is EntitySource_Parent entitySourceParent))
      return;
    if (entitySourceParent.Entity is Projectile entity1)
      this.LifeSteal = entity1.GetGlobalProjectile<SPGlobalProj>().LifeSteal;
    if (entitySourceParent.Entity is Projectile entity2 && (entity2.minion || entity2.sentry))
    {
      if (entity2.GetGlobalProjectile<SPGlobalProj>().deviation)
        ((Entity) projectile).velocity = Utils.RotateRandom(((Entity) projectile).velocity, 0.30000001192092896);
      if (entity2.GetGlobalProjectile<SPGlobalProj>().echo && SPGlobalProj.EchoDoubleProj && (double) Utils.NextFloat(Main.rand) < (double) Echo.DoubleChance)
      {
        SPGlobalProj.EchoDoubleProj = false;
        Projectile.NewProjectile(source, ((Entity) projectile).position, Utils.RotatedByRandom(((Entity) projectile).velocity, 0.059999998658895493), projectile.type, projectile.damage / 4, projectile.knockBack, projectile.owner, projectile.ai[0], projectile.ai[1], projectile.ai[2]);
        SPGlobalProj.EchoDoubleProj = true;
      }
    }
    projectile.netUpdate = true;
  }

  public virtual void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
  {
    Player player1;
    if (ProjectileID.Sets.IsAWhip[projectile.type] && projectile.TryGetOwner(ref player1) && player1.HeldItem.global().wTag != null && player1.HeldItem.global().CanGiveTag)
      target.AddTag(player1.HeldItem.global().wTag.Clone());
    Player player2;
    if ((double) this.LifeSteal <= 0.0 || !projectile.TryGetOwner(ref player2) || !player2.GetModPlayer<SPModPlayer>().HealMe((int) Math.Ceiling((double) player2.statLifeMax2 * (double) this.LifeSteal), 5))
      return;
    for (float num = 0.0f; (double) num <= 1.0; num += 0.005f)
    {
      Dust dust = Dust.NewDustDirect(Vector2.Lerp(((Entity) player2).Center, ((Entity) target).Center, num), 1, 1, 266, 0.0f, 0.0f, 0, new Color(), 1f);
      dust.velocity = Vector2.op_Addition(Vector2.op_Multiply(Utils.ToRotationVector2(Utils.NextFloat(Main.rand) * 6.28318548f), Utils.NextFloat(Main.rand, -1f, 1f)), Vector2.op_Multiply(Utils.SafeNormalize(Vector2.op_Subtraction(((Entity) player2).Center, ((Entity) target).Center), Vector2.Zero), 3.6f));
      dust.scale = 0.8f;
      dust.position = Vector2.Lerp(((Entity) player2).Center, ((Entity) target).Center, num);
    }
  }

  public virtual bool PreAI(Projectile projectile)
  {
    Player player;
    if (this.electrified && Main.GameUpdateCount % 12U == 0U && projectile.TryGetOwner(ref player))
      Projectile.NewProjectile(((Entity) projectile).GetSource_FromThis((string) null), ((Entity) projectile).Center, Vector2.Zero, ModContent.ProjectileType<ElectricLaser>(), (int) ((double) player.statLifeMax2 * (double) Electrified.DmgMultByPlayerMaxHealth), 0.0f, ((Entity) player).whoAmI, ((Entity) player).Center.X, ((Entity) player).Center.Y, 0.0f);
    return true;
  }
}
