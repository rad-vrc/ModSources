// Decompiled with JetBrains decompiler
// Type: QoLCompendium.QoLCompendium
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Core;
using QoLCompendium.Core.Changes.BuffChanges;
using QoLCompendium.Core.UI.Panels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium;

public class QoLCompendium : Mod
{
  public static Mod Instance;
  public static QoLCompendium.QoLCompendium instance;
  public static QoLCConfig mainConfig;
  public static QoLCConfig.MainClientConfig mainClientConfig;
  public static QoLCConfig.ItemConfig itemConfig;
  public static QoLCConfig.ShopConfig shopConfig;
  public static QoLCConfig.TooltipConfig tooltipConfig;
  public static int? LastOpenedBank;

  public virtual uint ExtraPlayerBuffSlots => (uint) QoLCompendium.QoLCompendium.mainConfig.ExtraBuffSlots;

  public virtual void PostSetupContent()
  {
    BuffSystem.DoBuffIntegration();
    Common.PostSetupTasks();
    LoadModSupport.PostSetupTasks();
    PermanentCalamityBuffUI.GetCalamityBuffData();
    PermanentMartinsOrderBuffUI.GetMartinsOrderBuffData();
    PermanentSOTSBuffUI.GetSOTSBuffData();
    PermanentSpiritClassicBuffUI.GetSpiritClassicBuffData();
    PermanentThoriumBuffUI.GetThoriumBuffData();
  }

  public virtual void Load()
  {
    // ISSUE: method pointer
    On_Player.HandleBeingInChestRange += new On_Player.hook_HandleBeingInChestRange((object) this, __methodptr(ChestRange));
    // ISSUE: method pointer
    On_WorldGen.moveRoom += new On_WorldGen.hook_moveRoom((object) this, __methodptr(WorldGen_moveRoom));
    QoLCompendium.QoLCompendium.instance = this;
    QoLCompendium.QoLCompendium.Instance = (Mod) this;
    ModConditions.LoadSupportedMods();
    LoadModSupport.LoadTasks();
  }

  public virtual void Unload()
  {
    QoLCompendium.QoLCompendium.instance = (QoLCompendium.QoLCompendium) null;
    QoLCompendium.QoLCompendium.Instance = (Mod) null;
    QoLCompendium.QoLCompendium.mainConfig = (QoLCConfig) null;
    QoLCompendium.QoLCompendium.mainClientConfig = (QoLCConfig.MainClientConfig) null;
    QoLCompendium.QoLCompendium.itemConfig = (QoLCConfig.ItemConfig) null;
    QoLCompendium.QoLCompendium.shopConfig = (QoLCConfig.ShopConfig) null;
    QoLCompendium.QoLCompendium.tooltipConfig = (QoLCConfig.TooltipConfig) null;
    // ISSUE: method pointer
    On_Player.HandleBeingInChestRange -= new On_Player.hook_HandleBeingInChestRange((object) this, __methodptr(ChestRange));
    // ISSUE: method pointer
    On_WorldGen.moveRoom -= new On_WorldGen.hook_moveRoom((object) this, __methodptr(WorldGen_moveRoom));
    Common.UnloadTasks();
    LoadModSupport.UnloadTasks();
  }

  public virtual void HandlePacket(BinaryReader reader, int whoAmI)
  {
    foreach (NPC npc in ((IEnumerable<NPC>) Main.npc).Where<NPC>((Func<NPC, bool>) (n => n != null && ((Entity) n).active && n.townNPC && !n.homeless)))
      QoLCompendium.QoLCompendium.TownEntitiesTeleportToHome(npc, npc.homeTileX, npc.homeTileY);
  }

  private void WorldGen_moveRoom(On_WorldGen.orig_moveRoom orig, int x, int y, int n)
  {
    orig.Invoke(x, y, n);
    if (!Utils.IndexInRange<NPC>(Main.npc, n) || Main.npc[n] == null)
      return;
    QoLCompendium.QoLCompendium.TownEntitiesTeleportToHome(Main.npc[n], Main.npc[n].homeTileX, Main.npc[n].homeTileY);
  }

  internal static void TownEntitiesTeleportToHome(NPC npc, int homeFloorX, int homeFloorY)
  {
    ((MethodBase) npc?.GetType().GetMethod("AI_007_TownEntities_TeleportToHome", (BindingFlags) 36, new Type[2]
    {
      typeof (int),
      typeof (int)
    }))?.Invoke((object) npc, new object[2]
    {
      (object) homeFloorX,
      (object) homeFloorY
    });
  }

  private void ChestRange(On_Player.orig_HandleBeingInChestRange orig, Player player)
  {
    int chest = player.chest;
    int? lastOpenedBank = QoLCompendium.QoLCompendium.LastOpenedBank;
    int valueOrDefault = lastOpenedBank.GetValueOrDefault();
    if (chest == valueOrDefault & lastOpenedBank.HasValue)
      return;
    if (QoLCompendium.QoLCompendium.LastOpenedBank.HasValue)
      QoLCompendium.QoLCompendium.LastOpenedBank = new int?();
    orig.Invoke(player);
  }
}
