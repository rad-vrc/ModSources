// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.Magnets.MagnetGlobals
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Core;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Items.Tools.Magnets;

public class MagnetGlobals : GlobalItem
{
  public const int BaseMagnetRange = 8400;
  public const int HellstoneMagnetRange = 16800;
  public const int SoulMagnetMagnetRange = 33600;
  public const int SpectreMagnetRange = 67200;
  public const int LunarMagnetRange = 268800;

  public virtual void Update(Item item, ref float gravity, ref float maxFallSpeed)
  {
    MagnetPlayer modPlayer = Main.LocalPlayer.GetModPlayer<MagnetPlayer>();
    if (!((Entity) item).active || ((Entity) Main.LocalPlayer).whoAmI != Main.myPlayer || Common.PowerUpItems.Contains(item.type) || item.noGrabDelay != 0 || item.playerIndexTheItemIsReservedFor != ((Entity) Main.LocalPlayer).whoAmI)
      return;
    if ((double) ((Entity) Main.LocalPlayer).Distance(((Entity) item).Center) <= 8400.0 && modPlayer.BaseMagnet)
    {
      item.beingGrabbed = true;
      ((Entity) item).Center = ((Entity) Main.LocalPlayer).Center;
      if (Main.netMode != 0)
        NetMessage.SendData(21, -1, -1, (NetworkText) null, ((Entity) item).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
    }
    if ((double) ((Entity) Main.LocalPlayer).Distance(((Entity) item).Center) <= 16800.0 && modPlayer.HellstoneMagnet)
    {
      item.beingGrabbed = true;
      ((Entity) item).Center = ((Entity) Main.LocalPlayer).Center;
      if (Main.netMode != 0)
        NetMessage.SendData(21, -1, -1, (NetworkText) null, ((Entity) item).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
    }
    if ((double) ((Entity) Main.LocalPlayer).Distance(((Entity) item).Center) <= 33600.0 && modPlayer.SoulMagnet)
    {
      item.beingGrabbed = true;
      ((Entity) item).Center = ((Entity) Main.LocalPlayer).Center;
      if (Main.netMode != 0)
        NetMessage.SendData(21, -1, -1, (NetworkText) null, ((Entity) item).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
    }
    if ((double) ((Entity) Main.LocalPlayer).Distance(((Entity) item).Center) <= 67200.0 && modPlayer.SpectreMagnet)
    {
      item.beingGrabbed = true;
      ((Entity) item).Center = ((Entity) Main.LocalPlayer).Center;
      if (Main.netMode != 0)
        NetMessage.SendData(21, -1, -1, (NetworkText) null, ((Entity) item).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
    }
    if ((double) ((Entity) Main.LocalPlayer).Distance(((Entity) item).Center) > 268800.0 || !modPlayer.LunarMagnet)
      return;
    item.beingGrabbed = true;
    ((Entity) item).Center = ((Entity) Main.LocalPlayer).Center;
    if (Main.netMode == 0)
      return;
    NetMessage.SendData(21, -1, -1, (NetworkText) null, ((Entity) item).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
  }
}
