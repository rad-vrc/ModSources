// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.KeybindPlayer
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using QoLCompendium.Content.Items.Tools.Mirrors;
using QoLCompendium.Core.Changes.PlayerChanges;
using QoLCompendium.Core.UI.Panels;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ObjectData;

#nullable disable
namespace QoLCompendium.Core;

public class KeybindPlayer : ModPlayer
{
  public int originalSelectedItem;
  public bool autoRevertSelectedItem;
  public int dashTimeMod;
  public static byte timeout;

  public virtual void ProcessTriggers(TriggersSet triggersSet)
  {
    if (KeybindSystem.SendNPCsHome.JustPressed)
    {
      foreach (NPC npc in ((IEnumerable<NPC>) Main.npc).Where<NPC>((Func<NPC, bool>) (n => n != null && ((Entity) n).active && n.townNPC && !n.homeless)))
        QoLCompendium.QoLCompendium.TownEntitiesTeleportToHome(npc, npc.homeTileX, npc.homeTileY);
      Main.NewText(Language.GetTextValue("Mods.QoLCompendium.Messages.TeleportNPCsHome"), byte.MaxValue, byte.MaxValue, byte.MaxValue);
    }
    if (KeybindSystem.QuickRecall.JustPressed)
      this.AutoUseMirror();
    if (KeybindSystem.QuickMosaicMirror.JustPressed)
      this.AutoUseMosaicMirror();
    if (KeybindSystem.QuickRod.JustPressed)
      this.AutoUseRod();
    if (KeybindSystem.Dash.JustPressed)
    {
      this.Player.dashTime = 30;
      if (this.Player.GetModPlayer<DashPlayer>().LeftLastPressed || ((Entity) this.Player).direction == -1 && (double) ((Entity) this.Player).velocity.X == 0.0)
      {
        this.Player.controlLeft = true;
        this.Player.releaseLeft = true;
        for (int index = 0; index < 10; ++index)
        {
          if (index >= 9)
          {
            this.Player.controlLeft = true;
            this.Player.releaseLeft = true;
          }
        }
      }
      if (this.Player.GetModPlayer<DashPlayer>().RightLastPressed || ((Entity) this.Player).direction == 1 && (double) ((Entity) this.Player).velocity.X == 0.0)
      {
        this.Player.controlRight = true;
        this.Player.releaseRight = true;
        for (int index = 0; index < 10; ++index)
        {
          if (index >= 9)
          {
            this.Player.controlRight = true;
            this.Player.releaseRight = true;
          }
        }
      }
    }
    if (Main.netMode == 0 && KeybindPlayer.timeout > (byte) 0)
      --KeybindPlayer.timeout;
    if (KeybindSystem.AddTileToWhitelist.JustPressed)
    {
      Tile tile = ((Tilemap) ref Main.tile)[Player.tileTargetX, Player.tileTargetY];
      TileLoader.GetTile((int) ((Tile) ref tile).TileType);
      int tileStyle = TileObjectData.GetTileStyle(tile);
      if (((Tile) ref tile).HasTile)
      {
        Common.UpdateWhitelist((int) ((Tile) ref tile).TileType, Common.GetFullNameById((int) ((Tile) ref tile).TileType, tileStyle), tileStyle);
        Main.NewText($"{Language.GetTextValue("Mods.QoLCompendium.TileStuff.Whitelisted")} {((EntityDefinition) new TileDefinition((int) ((Tile) ref tile).TileType)).Name}", byte.MaxValue, byte.MaxValue, byte.MaxValue);
      }
    }
    if (KeybindSystem.RemoveTileFromWhitelist.JustPressed)
    {
      Tile tile = ((Tilemap) ref Main.tile)[Player.tileTargetX, Player.tileTargetY];
      TileLoader.GetTile((int) ((Tile) ref tile).TileType);
      int tileStyle = TileObjectData.GetTileStyle(tile);
      if (((Tile) ref tile).HasTile)
      {
        Common.UpdateWhitelist((int) ((Tile) ref tile).TileType, Common.GetFullNameById((int) ((Tile) ref tile).TileType, tileStyle), tileStyle, true);
        Main.NewText($"{Language.GetTextValue("Mods.QoLCompendium.TileStuff.Removed")} {((EntityDefinition) new TileDefinition((int) ((Tile) ref tile).TileType)).Name}", byte.MaxValue, byte.MaxValue, byte.MaxValue);
      }
    }
    if (!KeybindSystem.PermanentBuffUIToggle.JustPressed)
      return;
    PermanentBuffSelectorUI.timeStart = Main.GameUpdateCount;
    PermanentBuffSelectorUI.visible = !PermanentBuffSelectorUI.visible;
    if (PermanentBuffSelectorUI.visible)
      SoundEngine.PlaySound(ref SoundID.MenuOpen, new Vector2?(((Entity) Main.LocalPlayer).position), (SoundUpdateCallback) null);
    else
      SoundEngine.PlaySound(ref SoundID.MenuClose, new Vector2?(((Entity) Main.LocalPlayer).position), (SoundUpdateCallback) null);
    if (PermanentBuffSelectorUI.visible)
      return;
    PermanentBuffUI.visible = false;
    PermanentCalamityBuffUI.visible = false;
    PermanentMartinsOrderBuffUI.visible = false;
    PermanentSOTSBuffUI.visible = false;
    PermanentSpiritClassicBuffUI.visible = false;
    PermanentThoriumBuffUI.visible = false;
  }

  public virtual void PostUpdate()
  {
    if (!this.autoRevertSelectedItem || this.Player.itemTime != 0 || this.Player.itemAnimation != 0)
      return;
    this.Player.selectedItem = this.originalSelectedItem;
    this.autoRevertSelectedItem = false;
  }

  public void AutoUseRod()
  {
    if (this.Player.HasItem(5335))
      this.QuickUseItemAt(Common.GetSlotItemIsIn(new Item(5335, 1, 0), this.Player.inventory));
    if (!this.Player.HasItem(1326))
      return;
    this.QuickUseItemAt(Common.GetSlotItemIsIn(new Item(1326, 1, 0), this.Player.inventory));
  }

  public void AutoUseMirror()
  {
    int index1 = -1;
    int index2 = -1;
    int index3 = -1;
    for (int index4 = 0; index4 < this.Player.inventory.Length; ++index4)
    {
      switch (this.Player.inventory[index4].type)
      {
        case 50:
        case 3124:
        case 3199:
        case 5358:
          index3 = index4;
          break;
        case 2350:
          index2 = index4;
          break;
        case 4870:
          index1 = index4;
          break;
      }
    }
    if (index1 != -1)
      this.QuickUseItemAt(index1);
    else if (index2 != -1)
    {
      this.QuickUseItemAt(index2);
    }
    else
    {
      if (index3 == -1)
        return;
      this.QuickUseItemAt(index3);
    }
  }

  public void AutoUseMosaicMirror()
  {
    int index1 = -1;
    for (int index2 = 0; index2 < this.Player.inventory.Length; ++index2)
    {
      if (this.Player.inventory[index2].type == ModContent.ItemType<MosaicMirror>())
      {
        index1 = index2;
        break;
      }
    }
    if (index1 == -1)
      return;
    this.QuickUseItemAt(index1);
  }

  public void QuickUseItemAt(int index, bool use = true)
  {
    if (this.autoRevertSelectedItem || this.Player.selectedItem == index || this.Player.inventory[index].type == 0)
      return;
    this.originalSelectedItem = this.Player.selectedItem;
    this.autoRevertSelectedItem = true;
    this.Player.selectedItem = index;
    this.Player.controlUseItem = true;
    if (!use || !CombinedHooks.CanUseItem(this.Player, this.Player.inventory[this.Player.selectedItem]) || ((Entity) this.Player).whoAmI != Main.myPlayer)
      return;
    this.Player.ItemCheck();
  }
}
