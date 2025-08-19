// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.ModChanges.ModdedReplaceHerbs
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.ModChanges;

public class ModdedReplaceHerbs : ModPlayer
{
  public const int GrowthSize = 18;

  public virtual void PostItemCheck()
  {
    if (!QoLCompendium.QoLCompendium.mainConfig.RegrowthAutoReplant || !this.Player.controlUseItem || this.Player.HeldItem.type != 213 && this.Player.HeldItem.type != 5295 && (Main.mouseItem == null || Main.mouseItem.type != 213 && Main.mouseItem.type != 5295))
      return;
    ModdedReplaceHerbs.GetHerbDrops(((Tilemap) ref Main.tile)[Player.tileTargetX, Player.tileTargetY]);
  }

  public static void GetHerbDrops(Tile tile)
  {
    if (!((Tile) ref tile).HasTile)
      return;
    int stage = (int) ((Tile) ref tile).TileFrameX / 18;
    if (ModConditions.depthsLoaded && (int) ((Tile) ref tile).TileType == Common.GetModTile(ModConditions.depthsMod, "ShadowShrub"))
    {
      ModdedReplaceHerbs.DropItems(stage, Common.GetModItem(ModConditions.depthsMod, "ShadowShrub"), Common.GetModItem(ModConditions.depthsMod, "ShadowShrubSeeds"));
      ModdedReplaceHerbs.ResetTileFrame(tile);
    }
    if (ModConditions.redemptionLoaded && (int) ((Tile) ref tile).TileType == Common.GetModTile(ModConditions.redemptionMod, "NightshadeTile"))
    {
      ModdedReplaceHerbs.DropItems(stage, Common.GetModItem(ModConditions.redemptionMod, "Nightshade"), Common.GetModItem(ModConditions.redemptionMod, "NightshadeSeeds"));
      ModdedReplaceHerbs.ResetTileFrame(tile);
    }
    if (ModConditions.shadowsOfAbaddonLoaded)
    {
      if ((int) ((Tile) ref tile).TileType == Common.GetModTile(ModConditions.shadowsOfAbaddonMod, "Welkinbell"))
      {
        ModdedReplaceHerbs.DropItems(stage, Common.GetModItem(ModConditions.shadowsOfAbaddonMod, "Welkinbell"), Common.GetModItem(ModConditions.shadowsOfAbaddonMod, "WelkinbellSeeds"));
        ModdedReplaceHerbs.ResetTileFrame(tile);
      }
      if ((int) ((Tile) ref tile).TileType == Common.GetModTile(ModConditions.shadowsOfAbaddonMod, "Illumifern"))
      {
        ModdedReplaceHerbs.DropItems(stage, Common.GetModItem(ModConditions.shadowsOfAbaddonMod, "Illumifern"), Common.GetModItem(ModConditions.shadowsOfAbaddonMod, "IllumifernSeeds"));
        ModdedReplaceHerbs.ResetTileFrame(tile);
      }
      if ((int) ((Tile) ref tile).TileType == Common.GetModTile(ModConditions.shadowsOfAbaddonMod, "Enduflora"))
      {
        ModdedReplaceHerbs.DropItems(stage, Common.GetModItem(ModConditions.shadowsOfAbaddonMod, "Enduflora"), Common.GetModItem(ModConditions.shadowsOfAbaddonMod, "EndufloraSeeds"));
        ModdedReplaceHerbs.ResetTileFrame(tile);
      }
    }
    if (ModConditions.spiritLoaded)
    {
      if ((int) ((Tile) ref tile).TileType == Common.GetModTile(ModConditions.spiritMod, "Cloudstalk"))
      {
        ModdedReplaceHerbs.DropItems(stage, Common.GetModItem(ModConditions.spiritMod, "CloudstalkItem"), Common.GetModItem(ModConditions.spiritMod, "CloudstalkSeed"));
        ModdedReplaceHerbs.ResetTileFrame(tile);
      }
      if ((int) ((Tile) ref tile).TileType == Common.GetModTile(ModConditions.spiritMod, "SoulBloomTile"))
      {
        ModdedReplaceHerbs.DropItems(stage, Common.GetModItem(ModConditions.spiritMod, "SoulBloom"), Common.GetModItem(ModConditions.spiritMod, "SoulSeeds"));
        ModdedReplaceHerbs.ResetTileFrame(tile);
      }
    }
    if (!ModConditions.thoriumLoaded || (int) ((Tile) ref tile).TileType != Common.GetModTile(ModConditions.thoriumMod, "MarineKelp2"))
      return;
    ModdedReplaceHerbs.DropItems(stage, Common.GetModItem(ModConditions.thoriumMod, "MarineKelp"), Common.GetModItem(ModConditions.thoriumMod, "MarineKelpSeeds"));
    ModdedReplaceHerbs.ResetTileFrame(tile);
  }

  public static void DropItems(int stage, int herbID, int seedID)
  {
    if (stage < 1)
      return;
    int num1 = 0;
    int num2 = 0;
    if (stage == 1)
    {
      num1 = 1;
      num2 = Main.rand.Next(1, 3);
    }
    if (stage == 2)
    {
      num1 = Main.rand.Next(1, 3);
      num2 = Main.rand.Next(1, 6);
    }
    Item.NewItem((IEntitySource) new EntitySource_TileBreak(Player.tileTargetX, Player.tileTargetY, (string) null), new Vector2((float) (Player.tileTargetX * 16 /*0x10*/), (float) (Player.tileTargetY * 16 /*0x10*/)), 8, 8, herbID, num1, false, 0, false, false);
    Item.NewItem((IEntitySource) new EntitySource_TileBreak(Player.tileTargetX, Player.tileTargetY, (string) null), new Vector2((float) (Player.tileTargetX * 16 /*0x10*/), (float) (Player.tileTargetY * 16 /*0x10*/)), 8, 8, seedID, num2, false, 0, false, false);
  }

  public static void ResetTileFrame(Tile tile)
  {
    ((Tile) ref tile).TileFrameX = (short) 0;
    NetMessage.SendData(17, -1, -1, (NetworkText) null, 1, (float) Player.tileTargetX, (float) Player.tileTargetY, (float) ((Tile) ref tile).TileType, 0, 0, 0);
  }
}
