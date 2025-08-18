// Decompiled with JetBrains decompiler
// Type: SummonerPrefix.WhipTagGNPC
// Assembly: SummonerPrefix, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8925F543-424F-4302-A9BE-42DFABB73BB2
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModReader\SummonerPrefix\SummonerPrefix.dll

using Microsoft.Xna.Framework;
using SummonerPrefix.Prefixes;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace SummonerPrefix;

public class WhipTagGNPC : GlobalNPC
{
  public List<WhipTag> tags = new List<WhipTag>();

  public virtual bool InstancePerEntity => true;

  public virtual bool PreAI(NPC npc)
  {
    if (this.tags.Count > 0)
    {
      for (int index = this.tags.Count - 1; index >= 0; --index)
      {
        --this.tags[index].TimeLeft;
        if (this.tags[index].TimeLeft <= 0)
          this.tags.RemoveAt(index);
      }
    }
    bool flag = false;
    foreach (WhipTag tag in this.tags)
    {
      if (tag.SpecialType == "Devoted")
      {
        flag = true;
        break;
      }
    }
    if (flag)
    {
      for (float num = 0.0f; (double) num < 360.0; num += 2f)
      {
        Dust dust = Dust.NewDustDirect(Vector2.op_Addition(((Entity) npc).Center, Vector2.op_Multiply(Utils.ToRotationVector2(MathHelper.ToRadians(num)), 24f)), 1, 1, 14, 0.0f, 0.0f, 0, new Color(), 1f);
        dust.velocity = Vector2.Zero;
        dust.noGravity = true;
        dust.scale = 0.8f;
      }
    }
    return true;
  }

  public virtual void ModifyHitByProjectile(
    NPC npc,
    Projectile projectile,
    ref NPC.HitModifiers modifiers)
  {
    if (projectile.npcProj || projectile.trap || !projectile.IsMinionOrSentryRelated)
      return;
    bool flag = false;
    float num1 = ProjectileID.Sets.SummonTagDamageMultiplier[projectile.type];
    int num2 = 0;
    float num3 = 0.0f;
    float num4 = 1f;
    float num5 = num3 + projectile.GetGlobalProjectile<SPGlobalProj>().MinionCrit;
    foreach (WhipTag tag in this.tags)
    {
      if (tag.SpecialType == "Devoted")
        flag = true;
      num2 += tag.TagDamage;
      num5 += tag.CritAdd;
      num4 += (tag.TagDamageMult - 1f) * num1;
    }
    Player player;
    if (projectile.TryGetOwner(ref player))
    {
      Item heldItem = player.HeldItem;
      if (heldItem.shoot > 0 && ProjectileID.Sets.IsAWhip[heldItem.shoot] && heldItem.global().wTag.SpecialType == "Devoted")
      {
        if (flag)
          num4 += Devoted.Increase;
        else
          num4 -= Devoted.Decrease;
      }
    }
    ref AddableFloat local1 = ref modifiers.FlatBonusDamage;
    local1 = AddableFloat.op_Addition(local1, (float) num2 * num1);
    ref StatModifier local2 = ref modifiers.SourceDamage;
    local2 = StatModifier.op_Multiply(local2, num4);
    if ((double) Utils.NextFloat(Main.rand) >= (double) num5)
      return;
    ((NPC.HitModifiers) ref modifiers).SetCrit();
  }
}
