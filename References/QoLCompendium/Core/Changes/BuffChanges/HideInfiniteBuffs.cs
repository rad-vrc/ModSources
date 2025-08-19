// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.BuffChanges.HideInfiniteBuffs
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.BuffChanges;

public class HideInfiniteBuffs : GlobalBuff
{
  public virtual bool PreDraw(
    SpriteBatch spriteBatch,
    int type,
    int buffIndex,
    ref BuffDrawParams drawParams)
  {
    if (!QoLCompendium.QoLCompendium.mainConfig.HideBuffs)
      return base.PreDraw(spriteBatch, type, buffIndex, ref drawParams);
    if (!HideInfiniteBuffs.HideBuffTypes(type))
      return base.PreDraw(spriteBatch, type, buffIndex, ref drawParams);
    drawParams.TextPosition = new Vector2(-100f);
    drawParams.Position = new Vector2(-100f);
    drawParams.MouseRectangle = Rectangle.Empty;
    return false;
  }

  public static bool HideBuffTypes(int type)
  {
    return Main.LocalPlayer.GetModPlayer<QoLCPlayer>().activeBuffs.Contains(type);
  }
}
