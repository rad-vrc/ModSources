// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.WorldChanges.MapTeleporting
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.WorldChanges;

public class MapTeleporting : ModSystem
{
  public static void TryToTeleportPlayerOnMap()
  {
    if (!Main.mouseRight || !((KeyboardState) ref Main.keyState).IsKeyUp((Keys) 162))
      return;
    Vector2 vector2_1;
    // ISSUE: explicit constructor call
    ((Vector2) ref vector2_1).\u002Ector((float) (Main.maxTilesX * 16 /*0x10*/), (float) (Main.maxTilesY * 16 /*0x10*/));
    Vector2 vector2_2;
    // ISSUE: explicit constructor call
    ((Vector2) ref vector2_2).\u002Ector((float) (Main.mouseX - Main.screenWidth / 2), (float) (Main.mouseY - Main.screenHeight / 2));
    Vector2 vector2_3 = Vector2.op_Multiply(Vector2.op_Addition(Main.mapFullscreenPos, Vector2.op_Multiply(Vector2.op_Division(vector2_2, 16f), 16f / Main.mapFullscreenScale)), 16f);
    vector2_3.Y -= (float) ((Entity) Main.LocalPlayer).height;
    if ((double) vector2_3.X < 0.0)
      vector2_3.X = 0.0f;
    else if ((double) vector2_3.X + (double) ((Entity) Main.LocalPlayer).width > (double) vector2_1.X)
      vector2_3.X = vector2_1.X - (float) ((Entity) Main.LocalPlayer).width;
    if ((double) vector2_3.Y < 0.0)
      vector2_3.Y = 0.0f;
    else if ((double) vector2_3.Y + (double) ((Entity) Main.LocalPlayer).height > (double) vector2_1.Y)
      vector2_3.Y = vector2_1.Y - (float) ((Entity) Main.LocalPlayer).height;
    if (!Vector2.op_Inequality(((Entity) Main.LocalPlayer).position, vector2_3))
      return;
    Main.LocalPlayer.Teleport(vector2_3, 1, 0);
    ((Entity) Main.LocalPlayer).position = vector2_3;
    ((Entity) Main.LocalPlayer).velocity = Vector2.Zero;
    Main.LocalPlayer.fallStart = (int) ((double) ((Entity) Main.LocalPlayer).position.Y / 16.0);
    NetMessage.SendData(65, -1, -1, (NetworkText) null, 0, (float) Main.myPlayer, vector2_3.X, vector2_3.Y, 1, 0, 0);
  }

  public virtual void PostDrawFullscreenMap(ref string mouseText)
  {
    if (!QoLCompendium.QoLCompendium.mainConfig.MapTeleporting)
      return;
    MapTeleporting.TryToTeleportPlayerOnMap();
  }
}
