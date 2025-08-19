// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.KeybindSystem
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Core.Changes.PlayerChanges;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core;

public class KeybindSystem : ModSystem
{
  public static ModKeybind SendNPCsHome { get; private set; }

  public static ModKeybind QuickRecall { get; private set; }

  public static ModKeybind QuickMosaicMirror { get; private set; }

  public static ModKeybind Dash { get; private set; }

  public static ModKeybind AddTileToWhitelist { get; private set; }

  public static ModKeybind RemoveTileFromWhitelist { get; private set; }

  public static ModKeybind PermanentBuffUIToggle { get; private set; }

  public static ModKeybind QuickRod { get; private set; }

  public virtual void Load()
  {
    KeybindSystem.SendNPCsHome = KeybindLoader.RegisterKeybind(((ModType) this).Mod, "SendNPCsHomeBind", "I");
    KeybindSystem.QuickRecall = KeybindLoader.RegisterKeybind(((ModType) this).Mod, "RecallBind", "K");
    KeybindSystem.QuickMosaicMirror = KeybindLoader.RegisterKeybind(((ModType) this).Mod, "MosaicMirrorBind", "L");
    KeybindSystem.Dash = KeybindLoader.RegisterKeybind(((ModType) this).Mod, "DashBind", "C");
    KeybindSystem.AddTileToWhitelist = KeybindLoader.RegisterKeybind(((ModType) this).Mod, "WhitelistTileBind", "O");
    KeybindSystem.RemoveTileFromWhitelist = KeybindLoader.RegisterKeybind(((ModType) this).Mod, "RemoveWhitelistedTileBind", "P");
    KeybindSystem.PermanentBuffUIToggle = KeybindLoader.RegisterKeybind(((ModType) this).Mod, "PermanentBuffUIToggleBind", "L");
    KeybindSystem.QuickRod = KeybindLoader.RegisterKeybind(((ModType) this).Mod, "QuickRodBind", "Z");
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    On_Player.DoCommonDashHandle += KeybindSystem.\u003C\u003EO.\u003C0\u003E__OnVanillaDash ?? (KeybindSystem.\u003C\u003EO.\u003C0\u003E__OnVanillaDash = new On_Player.hook_DoCommonDashHandle((object) null, __methodptr(OnVanillaDash)));
  }

  public virtual void Unload()
  {
    KeybindSystem.SendNPCsHome = (ModKeybind) null;
    KeybindSystem.QuickRecall = (ModKeybind) null;
    KeybindSystem.QuickMosaicMirror = (ModKeybind) null;
    KeybindSystem.Dash = (ModKeybind) null;
    KeybindSystem.AddTileToWhitelist = (ModKeybind) null;
    KeybindSystem.RemoveTileFromWhitelist = (ModKeybind) null;
    KeybindSystem.PermanentBuffUIToggle = (ModKeybind) null;
    KeybindSystem.QuickRod = (ModKeybind) null;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    On_Player.DoCommonDashHandle -= KeybindSystem.\u003C\u003EO.\u003C0\u003E__OnVanillaDash ?? (KeybindSystem.\u003C\u003EO.\u003C0\u003E__OnVanillaDash = new On_Player.hook_DoCommonDashHandle((object) null, __methodptr(OnVanillaDash)));
  }

  public static void OnVanillaDash(
    On_Player.orig_DoCommonDashHandle orig,
    Player player,
    out int dir,
    out bool dashing,
    Player.DashStartAction dashStartAction)
  {
    if (QoLCompendium.QoLCompendium.mainClientConfig.DisableDashDoubleTap)
      player.dashTime = 0;
    orig.Invoke(player, ref dir, ref dashing, dashStartAction);
    if (((Entity) player).whoAmI != Main.myPlayer || !KeybindSystem.Dash.JustPressed || player.CCed)
      return;
    DashPlayer modPlayer = player.GetModPlayer<DashPlayer>();
    if (player.controlRight && player.controlLeft)
      dir = modPlayer.latestXDirPressed;
    else if (player.controlRight)
      dir = 1;
    else if (player.controlLeft)
      dir = -1;
    if (dir == 0)
      return;
    ((Entity) player).direction = dir;
    dashing = true;
    if (player.dashTime > 0)
      --player.dashTime;
    if (player.dashTime < 0)
      ++player.dashTime;
    if (player.dashTime <= 0 && ((Entity) player).direction == -1 || player.dashTime >= 0 && ((Entity) player).direction == 1)
    {
      player.dashTime = 15;
    }
    else
    {
      dashing = true;
      player.dashTime = 0;
      player.timeSinceLastDashStarted = 0;
      if (dashStartAction == null || dashStartAction == null)
        return;
      dashStartAction.Invoke(dir);
    }
  }
}
