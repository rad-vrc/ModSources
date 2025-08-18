// Decompiled with JetBrains decompiler
// Type: SummonerPrefix.Utils
// Assembly: SummonerPrefix, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8925F543-424F-4302-A9BE-42DFABB73BB2
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModReader\SummonerPrefix\SummonerPrefix.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace SummonerPrefix;

public static class Utils
{
  public static float getDistance(Vector2 v1, Vector2 v2)
  {
    return (float) Math.Sqrt(Math.Pow((double) v2.X - (double) v1.X, 2.0) + Math.Pow((double) v2.Y - (double) v1.Y, 2.0));
  }

  public static void UseBlendState(this SpriteBatch sb, BlendState blend, SamplerState s = null)
  {
    sb.End();
    sb.Begin((SpriteSortMode) 1, blend, s == null ? Main.DefaultSamplerState : s, DepthStencilState.None, Main.Rasterizer, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
  }

  public static bool LineThroughRect(
    Vector2 start,
    Vector2 end,
    Rectangle rect,
    int lineWidth = 4,
    int checkDistance = 8)
  {
    float num = 0.0f;
    return Collision.CheckAABBvLineCollision(Utils.TopLeft(rect), Utils.Size(rect), start, end, (float) lineWidth, ref num);
  }

  public static PrefixGlobalItem global(this Item item)
  {
    PrefixGlobalItem prefixGlobalItem;
    return item.TryGetGlobalItem<PrefixGlobalItem>(ref prefixGlobalItem) ? item.GetGlobalItem<PrefixGlobalItem>() : new PrefixGlobalItem();
  }

  public static void AddTag(this NPC npc, WhipTag tag)
  {
    WhipTagGNPC globalNpc = npc.GetGlobalNPC<WhipTagGNPC>();
    foreach (WhipTag tag1 in globalNpc.tags)
    {
      if (tag1.ID == tag.ID)
      {
        tag1.TimeLeft = tag.TimeLeft;
        tag1.TagDamage = tag.TagDamage;
        tag1.TagDamageMult = tag.TagDamageMult;
        tag1.CritAdd = tag.CritAdd;
        return;
      }
    }
    globalNpc.tags.Add(tag);
  }

  public static bool isMinionSummonItem(this Item item, bool sentry = true)
  {
    if (item.shoot != 0)
    {
      Projectile projectile1 = new Projectile();
      projectile1.SetDefaults(item.shoot);
      Projectile projectile2;
      if (projectile1.minion || projectile1.sentry & sentry)
      {
        projectile2 = (Projectile) null;
        return true;
      }
      projectile2 = (Projectile) null;
    }
    return false;
  }

  public static void Replace(this List<TooltipLine> tooltips, string targetStr, string to)
  {
    if (Main.dedServ)
      return;
    tooltips.FindAndReplace(targetStr, to);
  }

  public static void Replace(this List<TooltipLine> tooltips, string targetStr, int to)
  {
    if (Main.dedServ)
      return;
    tooltips.FindAndReplace(targetStr, to.ToString());
  }

  public static void Replace(this List<TooltipLine> tooltips, string targetStr, float to)
  {
    if (Main.dedServ)
      return;
    tooltips.FindAndReplace(targetStr, to.ToString());
  }

  public static int ToPercent(this float f) => (int) Math.Round((double) f * 100.0);

  public static void Replace(this TooltipLine tl, string targetStr, string to)
  {
    tl.Text = tl.Text.Replace(targetStr, to);
  }

  public static void FindAndReplace(
    this List<TooltipLine> tooltips,
    string replacedKey,
    string newKey)
  {
    TooltipLine tooltipLine = tooltips.FirstOrDefault<TooltipLine>((Func<TooltipLine, bool>) (x => x.Mod == "Terraria" && x.Text.Contains(replacedKey)));
    if (tooltipLine == null)
      return;
    tooltipLine.Text = tooltipLine.Text.Replace(replacedKey, newKey);
  }
}
