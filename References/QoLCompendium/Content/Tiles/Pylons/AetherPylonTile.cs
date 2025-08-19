// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Tiles.Pylons.AetherPylonTile
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QoLCompendium.Content.Items.Placeables.Pylons;
using QoLCompendium.Core;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Map;
using Terraria.ModLoader;
using Terraria.ModLoader.Default;
using Terraria.ObjectData;

#nullable disable
namespace QoLCompendium.Content.Tiles.Pylons;

public class AetherPylonTile : ModPylon
{
  public int CrystalVerticalFrameCount = 8;
  public Asset<Texture2D> crystalTexture;
  public Asset<Texture2D> crystalHighlightTexture;
  public Asset<Texture2D> mapIcon;

  public virtual void Load()
  {
    this.crystalTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/PylonCrystals/AetherPylonTile_Crystal", (AssetRequestMode) 2);
    this.crystalHighlightTexture = ModContent.Request<Texture2D>("QoLCompendium/Assets/Highlights/PylonCrystal_Highlight", (AssetRequestMode) 2);
    this.mapIcon = ModContent.Request<Texture2D>("QoLCompendium/Assets/MapIcons/AetherPylonTile_MapIcon", (AssetRequestMode) 2);
  }

  public virtual void SetStaticDefaults()
  {
    Main.tileLighted[(int) ((ModBlockType) this).Type] = true;
    Main.tileFrameImportant[(int) ((ModBlockType) this).Type] = true;
    TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
    TileObjectData.newTile.LavaDeath = false;
    TileObjectData.newTile.DrawYOffset = 2;
    TileObjectData.newTile.StyleHorizontal = true;
    TEModdedPylon instance = (TEModdedPylon) ModContent.GetInstance<AetherPylonTileEntity>();
    TileObjectData.newTile.HookCheckIfCanPlace = new PlacementHook(new Func<int, int, int, int, int, int, int>(instance.PlacementPreviewHook_CheckIfCanPlace), 1, 0, true);
    TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(new Func<int, int, int, int, int, int, int>(((ModTileEntity) instance).Hook_AfterPlacement), -1, 0, false);
    TileObjectData.addTile((int) ((ModBlockType) this).Type);
    TileID.Sets.InteractibleByNPCs[(int) ((ModBlockType) this).Type] = true;
    TileID.Sets.PreventsSandfall[(int) ((ModBlockType) this).Type] = true;
    TileID.Sets.AvoidedByMeteorLanding[(int) ((ModBlockType) this).Type] = true;
    ((ModTile) this).AddToArray(ref TileID.Sets.CountsAsPylon);
    ((ModTile) this).AddMapEntry(Color.MediumOrchid, ((ModBlockType) this).CreateMapEntryName());
    ((ModBlockType) this).DustType = -1;
  }

  public virtual NPCShop.Entry GetNPCShopEntry()
  {
    if (!QoLCompendium.QoLCompendium.itemConfig.Pylons)
      return (NPCShop.Entry) null;
    return new NPCShop.Entry(ModContent.ItemType<AetherPylon>(), new Condition[1]
    {
      Condition.InAether
    });
  }

  public virtual void MouseOver(int i, int j)
  {
    Main.LocalPlayer.cursorItemIconEnabled = true;
    Main.LocalPlayer.cursorItemIconID = ModContent.ItemType<AetherPylon>();
  }

  public virtual void KillMultiTile(int i, int j, int frameX, int frameY)
  {
    ModContent.GetInstance<AetherPylonTileEntity>().Kill(i, j);
  }

  public virtual bool ValidTeleportCheck_NPCCount(
    TeleportPylonInfo pylonInfo,
    int defaultNecessaryNPCCount)
  {
    return true;
  }

  public virtual bool ValidTeleportCheck_BiomeRequirements(
    TeleportPylonInfo pylonInfo,
    SceneMetrics sceneData)
  {
    return sceneData.EnoughTilesForShimmer;
  }

  public virtual void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
  {
    r = g = b = 0.75f;
  }

  public virtual void SpecialDraw(int i, int j, SpriteBatch spriteBatch)
  {
    this.DefaultDrawPylonCrystal(spriteBatch, i, j, this.crystalTexture, this.crystalHighlightTexture, new Vector2(-1f, -12f), Color.op_Multiply(Color.White, 0.1f), Common.ColorSwap(Color.MediumOrchid, Color.MediumSlateBlue, 1.5f), 4, this.CrystalVerticalFrameCount);
  }

  public virtual void DrawMapIcon(
    ref MapOverlayDrawContext context,
    ref string mouseOverText,
    TeleportPylonInfo pylonInfo,
    bool isNearPylon,
    Color drawColor,
    float deselectedScale,
    float selectedScale)
  {
    this.DefaultMapClickHandle(this.DefaultDrawMapIcon(ref context, this.mapIcon, Vector2.op_Addition(Utils.ToVector2(pylonInfo.PositionInTiles), new Vector2(1.5f, 2f)), drawColor, deselectedScale, selectedScale), pylonInfo, ModContent.GetInstance<AetherPylon>().DisplayName.Key, ref mouseOverText);
  }
}
