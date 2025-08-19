// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Projectiles.Explosives.AutoHouserProj
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using QoLCompendium.Core;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Projectiles.Explosives;

public class AutoHouserProj : ModProjectile
{
  public virtual string Texture => "QoLCompendium/Assets/Projectiles/Invisible";

  public virtual void SetDefaults()
  {
    ((Entity) this.Projectile).width = 1;
    ((Entity) this.Projectile).height = 1;
    this.Projectile.timeLeft = 1;
  }

  public static void PlaceHouse(int x, int y, Vector2 position, int side, Player player)
  {
    int x1 = (int) ((double) (side * -1 + x) + (double) position.X / 16.0);
    int y1 = (int) ((double) y + (double) position.Y / 16.0);
    Tile tile = ((Tilemap) ref Main.tile)[x1, y1];
    if (!CheckDestruction.OkayToDestroyTileAt(x1, y1))
      return;
    int wallID1 = 4;
    int tileID1 = 30;
    int platformID1 = 0;
    AutoHouserProj.GetHouseStyle(player, ref wallID1, ref tileID1, ref platformID1);
    int wallID2 = 0;
    int tileID2 = -1;
    int platformID2 = -1;
    bool inModdedBiome = false;
    AutoHouserProj.GetModdedHouseStyle(player, ref wallID2, ref tileID2, ref platformID2, ref inModdedBiome);
    if (x == 10 * side || x == side)
    {
      if (y == -5 && (int) ((Tile) ref tile).TileType == tileID1 || (y == -4 || y == 0) && (int) ((Tile) ref tile).TileType == tileID1 || (y == -1 || y == -2 || y == -3) && (((Tile) ref tile).TileType == (ushort) 10 || ((Tile) ref tile).TileType == (ushort) 11))
        return;
    }
    else if (y == -5 && (((Tile) ref tile).TileType == (ushort) 19 || (int) ((Tile) ref tile).TileType == tileID1 || (int) ((Tile) ref tile).TileType == platformID2) || y == 0 && (((Tile) ref tile).TileType == (ushort) 19 || (int) ((Tile) ref tile).TileType == tileID1 || (int) ((Tile) ref tile).TileType == platformID2))
      return;
    if (x != 9 * side && x != 2 * side || y != -1 && y != -2 && y != -3 || ((Tile) ref tile).TileType != (ushort) 11)
      Destruction.ClearEverything(x1, y1);
    if (y != -5 && y != 0 && x != 10 * side && x != side)
    {
      if (inModdedBiome)
      {
        WorldGen.PlaceWall(x1, y1, wallID2, false);
        if (Main.netMode == 2)
          NetMessage.SendTileSquare(-1, x1, y1, 1, (TileChangeType) 0);
      }
      else
      {
        WorldGen.PlaceWall(x1, y1, wallID1, false);
        if (Main.netMode == 2)
          NetMessage.SendTileSquare(-1, x1, y1, 1, (TileChangeType) 0);
      }
    }
    if (y == -5 && Math.Abs(x) >= 3 && Math.Abs(x) <= 5)
    {
      if (inModdedBiome)
      {
        WorldGen.PlaceTile(x1, y1, platformID2, false, false, -1, 0);
        if (Main.netMode != 2)
          return;
        NetMessage.SendData(17, -1, -1, (NetworkText) null, 1, (float) x1, (float) y1, (float) platformID2, 0, 0, 0);
      }
      else
      {
        WorldGen.PlaceTile(x1, y1, 19, false, false, -1, platformID1);
        if (Main.netMode != 2)
          return;
        NetMessage.SendData(17, -1, -1, (NetworkText) null, 1, (float) x1, (float) y1, 19f, platformID1, 0, 0);
      }
    }
    else
    {
      if (y != -5 && y != 0 && x != 10 * side && (x != side || y != -4))
        return;
      if (inModdedBiome)
      {
        WorldGen.PlaceTile(x1, y1, tileID2, false, false, -1, 0);
        if (Main.netMode != 2)
          return;
        NetMessage.SendTileSquare(-1, x1, y1, 1, (TileChangeType) 0);
      }
      else
      {
        WorldGen.PlaceTile(x1, y1, tileID1, false, false, -1, 0);
        if (Main.netMode != 2)
          return;
        NetMessage.SendTileSquare(-1, x1, y1, 1, (TileChangeType) 0);
      }
    }
  }

  public static void PlaceFurniture(int x, int y, Vector2 position, int side, Player player)
  {
    int num1 = (int) ((double) (side * -1 + x) + (double) position.X / 16.0);
    int num2 = (int) ((double) y + (double) position.Y / 16.0);
    if (!CheckDestruction.OkayToDestroyTile(((Tilemap) ref Main.tile)[num1, num2]))
      return;
    int tableTile = 1;
    int tableID1 = 0;
    int chairID1 = 0;
    int doorID1 = 0;
    int torchID1 = 0;
    AutoHouserProj.GetFurnitureStyle(player, ref tableID1, ref chairID1, ref doorID1, ref torchID1, ref tableTile);
    int tableID2 = -1;
    int chairID2 = -1;
    int doorID2 = -1;
    int torchID2 = -1;
    bool inModdedBiome = false;
    AutoHouserProj.GetModdedFurnitureStyle(player, ref tableID2, ref chairID2, ref doorID2, ref torchID2, ref inModdedBiome);
    if (y == -1)
    {
      if (Math.Abs(x) == 1)
      {
        if (inModdedBiome)
        {
          WorldGen.PlaceTile(num1, num2, doorID2, false, false, -1, 0);
          if (Main.netMode == 2)
            NetMessage.SendTileSquare(-1, num1, num2 - 2, 1, 3, (TileChangeType) 0);
        }
        else
        {
          WorldGen.PlaceTile(num1, num2, 10, false, false, -1, doorID1);
          if (Main.netMode == 2)
            NetMessage.SendTileSquare(-1, num1, num2 - 2, 1, 3, (TileChangeType) 0);
        }
      }
      if (x == 5 * side)
      {
        if (inModdedBiome)
        {
          if (side == -1 && chairID2 == Common.GetModTile(ModConditions.calamityMod, "AcidwoodChairTile"))
            ++num1;
          WorldGen.PlaceObject(num1, num2, chairID2, false, 0, 0, -1, side);
          if (Main.netMode == 2)
            NetMessage.SendData(17, -1, -1, (NetworkText) null, 1, (float) num1, (float) num2, (float) chairID2, 0, 0, 0);
        }
        else
        {
          int num3 = num1;
          int num4 = num2;
          int num5 = side;
          int num6 = chairID1;
          int num7 = num5;
          WorldGen.PlaceObject(num3, num4, 15, false, num6, 0, -1, num7);
          if (Main.netMode == 2)
            NetMessage.SendData(17, -1, -1, (NetworkText) null, 1, (float) num1, (float) num2, 15f, chairID1, 0, 0);
        }
      }
      if (x == 7 * side)
      {
        if (inModdedBiome)
        {
          WorldGen.PlaceTile(num1, num2, tableID2, false, false, -1, 0);
          if (Main.netMode == 2)
            NetMessage.SendData(17, -1, -1, (NetworkText) null, 1, (float) num1, (float) num2, (float) tableID2, 0, 0, 0);
        }
        else
        {
          if (tableTile == 1)
          {
            WorldGen.PlaceTile(num1, num2, 14, false, false, -1, tableID1);
            if (Main.netMode == 2)
              NetMessage.SendData(17, -1, -1, (NetworkText) null, 1, (float) num1, (float) num2, 14f, tableID1, 0, 0);
          }
          if (tableTile == 2)
          {
            WorldGen.PlaceTile(num1, num2, 469, false, false, -1, tableID1);
            if (Main.netMode == 2)
              NetMessage.SendData(17, -1, -1, (NetworkText) null, 1, (float) num1, (float) num2, 469f, tableID1, 0, 0);
          }
        }
      }
    }
    if (x != 7 * side || y != -4)
      return;
    if (inModdedBiome)
    {
      WorldGen.PlaceTile(num1, num2, torchID2, false, false, -1, 0);
      if (Main.netMode != 2)
        return;
      NetMessage.SendData(17, -1, -1, (NetworkText) null, 1, (float) num1, (float) num2, (float) torchID2, 0, 0, 0);
    }
    else
    {
      WorldGen.PlaceTile(num1, num2, 4, false, false, -1, torchID1);
      if (Main.netMode != 2)
        return;
      NetMessage.SendData(17, -1, -1, (NetworkText) null, 1, (float) num1, (float) num2, 4f, 0, 0, 0);
    }
  }

  public static void UpdateWall(int x, int y, Vector2 position, int side, Player player)
  {
    int num1 = (int) ((double) (side * -1 + x) + (double) position.X / 16.0);
    int num2 = (int) ((double) y + (double) position.Y / 16.0);
    WorldGen.SquareWallFrame(num1, num2, true);
    if (Main.netMode != 2)
      return;
    NetMessage.SendTileSquare(-1, num1, num2, 1, (TileChangeType) 0);
  }

  public virtual void OnKill(int timeLeft)
  {
    Vector2 center = ((Entity) this.Projectile).Center;
    SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(center), (SoundUpdateCallback) null);
    Player player = Main.player[this.Projectile.owner];
    if (Main.netMode == 1)
      return;
    if ((double) ((Entity) player).Center.X < (double) center.X)
    {
      for (int index = 0; index < 3; ++index)
      {
        for (int x = 11; x > -1; --x)
        {
          if (index == 2 || x != 11 && x != 0)
          {
            for (int y = -6; y <= 1; ++y)
            {
              if (index == 2 || y != -6 && y != 1)
              {
                switch (index)
                {
                  case 0:
                    AutoHouserProj.PlaceHouse(x, y, center, 1, player);
                    continue;
                  case 1:
                    AutoHouserProj.PlaceFurniture(x, y, center, 1, player);
                    continue;
                  default:
                    AutoHouserProj.UpdateWall(x, y, center, 1, player);
                    continue;
                }
              }
            }
          }
        }
      }
    }
    else
    {
      for (int index = 0; index < 3; ++index)
      {
        for (int x = -11; x < 1; ++x)
        {
          if (index == 2 || x != -11 && x != 0)
          {
            for (int y = -6; y <= 1; ++y)
            {
              if (index == 2 || y != -6 && y != 1)
              {
                switch (index)
                {
                  case 0:
                    AutoHouserProj.PlaceHouse(x, y, center, -1, player);
                    continue;
                  case 1:
                    AutoHouserProj.PlaceFurniture(x, y, center, -1, player);
                    continue;
                  default:
                    continue;
                }
              }
            }
          }
        }
      }
    }
  }

  public static void GetHouseStyle(
    Player player,
    ref int wallID,
    ref int tileID,
    ref int platformID)
  {
    if (player.ZoneDesert && !player.ZoneBeach)
    {
      wallID = 235;
      tileID = 479;
      platformID = 42;
    }
    if (player.ZoneSnow)
    {
      wallID = 149;
      tileID = 321;
      platformID = 19;
    }
    if (player.ZoneJungle)
    {
      wallID = 42;
      tileID = 158;
      platformID = 2;
    }
    if (player.ZoneCorrupt)
    {
      wallID = 41;
      tileID = 157;
      platformID = 1;
    }
    if (player.ZoneCrimson)
    {
      wallID = 85;
      tileID = 208 /*0xD0*/;
      platformID = 5;
    }
    if (player.ZoneBeach)
    {
      wallID = 151;
      tileID = 322;
      platformID = 17;
    }
    if (player.ZoneHallow)
    {
      wallID = 43;
      tileID = 159;
      platformID = 3;
    }
    if (player.ZoneGlowshroom)
    {
      wallID = 74;
      tileID = 190;
      platformID = 18;
    }
    if (player.ZoneSkyHeight)
    {
      wallID = 82;
      tileID = 202;
      platformID = 22;
    }
    if (!player.ZoneUnderworldHeight)
      return;
    wallID = 20;
    tileID = 75;
    platformID = 13;
  }

  public static void GetFurnitureStyle(
    Player player,
    ref int tableID,
    ref int chairID,
    ref int doorID,
    ref int torchID,
    ref int tableTile)
  {
    if (player.ZoneDesert && !player.ZoneBeach)
    {
      tableTile = 2;
      tableID = 7;
      chairID = 43;
      doorID = 43;
      torchID = 16 /*0x10*/;
    }
    if (player.ZoneSnow)
    {
      tableID = 28;
      chairID = 30;
      doorID = 30;
      torchID = 9;
    }
    if (player.ZoneJungle)
    {
      tableID = 2;
      chairID = 3;
      doorID = 2;
      torchID = 21;
    }
    if (player.ZoneCorrupt)
    {
      tableID = 1;
      chairID = 2;
      doorID = 1;
      torchID = 18;
    }
    if (player.ZoneCrimson)
    {
      tableID = 8;
      chairID = 11;
      doorID = 10;
      torchID = 19;
    }
    if (player.ZoneBeach)
    {
      tableID = 26;
      chairID = 29;
      doorID = 29;
      torchID = 17;
    }
    if (player.ZoneHallow)
    {
      tableID = 3;
      chairID = 4;
      doorID = 3;
      torchID = 20;
    }
    if (player.ZoneGlowshroom)
    {
      tableID = 27;
      chairID = 9;
      doorID = 6;
      torchID = 22;
    }
    if (player.ZoneSkyHeight)
    {
      tableID = 7;
      chairID = 10;
      doorID = 9;
      torchID = 6;
    }
    if (!player.ZoneUnderworldHeight)
      return;
    tableID = 13;
    chairID = 16 /*0x10*/;
    doorID = 19;
    torchID = 7;
  }

  public static void GetModdedHouseStyle(
    Player player,
    ref int wallID,
    ref int tileID,
    ref int platformID,
    ref bool inModdedBiome)
  {
    if (!ModConditions.calamityLoaded)
      return;
    ModBiome modBiome1;
    if (ModConditions.calamityMod.TryFind<ModBiome>("AstralInfectionBiome", ref modBiome1) && Main.LocalPlayer.InModBiome(modBiome1))
    {
      wallID = Common.GetModWall(ModConditions.calamityMod, "AstralMonolithWall");
      tileID = Common.GetModTile(ModConditions.calamityMod, "AstralMonolith");
      platformID = Common.GetModTile(ModConditions.calamityMod, "MonolithPlatform");
      inModdedBiome = true;
    }
    ModBiome modBiome2;
    ModBiome modBiome3;
    ModBiome modBiome4;
    ModBiome modBiome5;
    if (ModConditions.calamityMod.TryFind<ModBiome>("AbyssLayer1Biome", ref modBiome2) && Main.LocalPlayer.InModBiome(modBiome2) || ModConditions.calamityMod.TryFind<ModBiome>("AbyssLayer2Biome", ref modBiome3) && Main.LocalPlayer.InModBiome(modBiome3) || ModConditions.calamityMod.TryFind<ModBiome>("AbyssLayer3Biome", ref modBiome4) && Main.LocalPlayer.InModBiome(modBiome4) || ModConditions.calamityMod.TryFind<ModBiome>("AbyssLayer4Biome", ref modBiome5) && Main.LocalPlayer.InModBiome(modBiome5))
    {
      wallID = Common.GetModWall(ModConditions.calamityMod, "SmoothAbyssGravelWall");
      tileID = Common.GetModTile(ModConditions.calamityMod, "SmoothAbyssGravel");
      platformID = Common.GetModTile(ModConditions.calamityMod, "SmoothAbyssGravelPlatform");
      inModdedBiome = true;
    }
    ModBiome modBiome6;
    if (ModConditions.calamityMod.TryFind<ModBiome>("BrimstoneCragsBiome", ref modBiome6) && Main.LocalPlayer.InModBiome(modBiome6))
    {
      wallID = Common.GetModWall(ModConditions.calamityMod, "SmoothBrimstoneSlagWall");
      tileID = Common.GetModTile(ModConditions.calamityMod, "SmoothBrimstoneSlag");
      platformID = Common.GetModTile(ModConditions.calamityMod, "AshenPlatform");
      inModdedBiome = true;
    }
    ModBiome modBiome7;
    if (ModConditions.calamityMod.TryFind<ModBiome>("SulphurousSeaBiome", ref modBiome7) && Main.LocalPlayer.InModBiome(modBiome7))
    {
      wallID = Common.GetModWall(ModConditions.calamityMod, "AcidwoodWall");
      tileID = Common.GetModTile(ModConditions.calamityMod, "AcidwoodTile");
      platformID = Common.GetModTile(ModConditions.calamityMod, "AcidwoodPlatformTile");
      inModdedBiome = true;
    }
    ModBiome modBiome8;
    if (!ModConditions.calamityMod.TryFind<ModBiome>("SunkenSeaBiome", ref modBiome8) || !Main.LocalPlayer.InModBiome(modBiome8))
      return;
    wallID = Common.GetModWall(ModConditions.calamityMod, "SmoothNavystoneWall");
    tileID = Common.GetModTile(ModConditions.calamityMod, "SmoothNavystone");
    platformID = Common.GetModTile(ModConditions.calamityMod, "EutrophicPlatform");
    inModdedBiome = true;
  }

  public static void GetModdedFurnitureStyle(
    Player player,
    ref int tableID,
    ref int chairID,
    ref int doorID,
    ref int torchID,
    ref bool inModdedBiome)
  {
    if (!ModConditions.calamityLoaded)
      return;
    ModBiome modBiome1;
    if (ModConditions.calamityMod.TryFind<ModBiome>("AstralInfectionBiome", ref modBiome1) && Main.LocalPlayer.InModBiome(modBiome1))
    {
      tableID = Common.GetModTile(ModConditions.calamityMod, "MonolithTable");
      chairID = Common.GetModTile(ModConditions.calamityMod, "MonolithChair");
      doorID = Common.GetModTile(ModConditions.calamityMod, "MonolithDoorClosed");
      torchID = Common.GetModTile(ModConditions.calamityMod, "AstralTorch");
      inModdedBiome = true;
    }
    ModBiome modBiome2;
    ModBiome modBiome3;
    ModBiome modBiome4;
    ModBiome modBiome5;
    if (ModConditions.calamityMod.TryFind<ModBiome>("AbyssLayer1Biome", ref modBiome2) && Main.LocalPlayer.InModBiome(modBiome2) || ModConditions.calamityMod.TryFind<ModBiome>("AbyssLayer2Biome", ref modBiome3) && Main.LocalPlayer.InModBiome(modBiome3) || ModConditions.calamityMod.TryFind<ModBiome>("AbyssLayer3Biome", ref modBiome4) && Main.LocalPlayer.InModBiome(modBiome4) || ModConditions.calamityMod.TryFind<ModBiome>("AbyssLayer4Biome", ref modBiome5) && Main.LocalPlayer.InModBiome(modBiome5))
    {
      tableID = Common.GetModTile(ModConditions.calamityMod, "AbyssTable");
      chairID = Common.GetModTile(ModConditions.calamityMod, "AbyssChair");
      doorID = Common.GetModTile(ModConditions.calamityMod, "AbyssDoorClosed");
      torchID = Common.GetModTile(ModConditions.calamityMod, "AbyssTorch");
      inModdedBiome = true;
    }
    ModBiome modBiome6;
    if (ModConditions.calamityMod.TryFind<ModBiome>("BrimstoneCragsBiome", ref modBiome6) && Main.LocalPlayer.InModBiome(modBiome6))
    {
      tableID = Common.GetModTile(ModConditions.calamityMod, "AshenTable");
      chairID = Common.GetModTile(ModConditions.calamityMod, "AshenChair");
      doorID = Common.GetModTile(ModConditions.calamityMod, "AshenDoorClosed");
      torchID = Common.GetModTile(ModConditions.calamityMod, "GloomTorch");
      inModdedBiome = true;
    }
    ModBiome modBiome7;
    if (ModConditions.calamityMod.TryFind<ModBiome>("SulphurousSeaBiome", ref modBiome7) && Main.LocalPlayer.InModBiome(modBiome7))
    {
      tableID = Common.GetModTile(ModConditions.calamityMod, "AcidwoodTableTile");
      chairID = Common.GetModTile(ModConditions.calamityMod, "AcidwoodChairTile");
      doorID = Common.GetModTile(ModConditions.calamityMod, "AcidwoodDoorClosed");
      torchID = Common.GetModTile(ModConditions.calamityMod, "SulphurousTorch");
      inModdedBiome = true;
    }
    ModBiome modBiome8;
    if (!ModConditions.calamityMod.TryFind<ModBiome>("SunkenSeaBiome", ref modBiome8) || !Main.LocalPlayer.InModBiome(modBiome8))
      return;
    tableID = Common.GetModTile(ModConditions.calamityMod, "EutrophicTable");
    chairID = Common.GetModTile(ModConditions.calamityMod, "EutrophicChair");
    doorID = Common.GetModTile(ModConditions.calamityMod, "EutrophicDoorClosed");
    torchID = Common.GetModTile(ModConditions.calamityMod, "NavyPrismTorch");
    inModdedBiome = true;
  }
}
