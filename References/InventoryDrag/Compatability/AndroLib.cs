// Decompiled with JetBrains decompiler
// Type: InventoryDrag.Compatability.AndroLib
// Assembly: InventoryDrag, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0AEBC7B1-9360-4E40-BFA4-A14C9A81EEA1
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\InventoryDrag\InventoryDrag.dll

using androLib.UI;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace InventoryDrag.Compatability;

public static class AndroLib
{
  internal const string androLib = "androLib";
  public static Mod Instance = (Mod) null;
  public static bool Enabled = Terraria.ModLoader.ModLoader.TryGetMod(nameof (androLib), ref AndroLib.Instance);

  public static bool PreventDoubleClickInJourneyMode(int context, bool overrideShiftLeftClick)
  {
    return AndroLib.Enabled && context == 29 & overrideShiftLeftClick;
  }

  public static void Load(Mod mod)
  {
    if (!AndroLib.Enabled)
      return;
    AndroLibReference.Load();
    mod.AddContent<AndroLibPlayer>();
  }

  public static void Unload(Mod mod) => AndroLibReference.Unload();

  [JITWhenModsEnabled(new string[] {"androLib"})]
  public static bool DidBagSlotChange()
  {
    AndroLibPlayer androLibPlayer;
    return AndroLib.Enabled && !AndroLib.NoBagsHovered && Main.LocalPlayer.TryGetModPlayer<AndroLibPlayer>(ref androLibPlayer) && androLibPlayer.didSlotChange;
  }

  [JITWhenModsEnabled(new string[] {"androLib"})]
  public static bool NoBagsHovered => MasterUIManager.NoUIBeingHovered;

  [JITWhenModsEnabled(new string[] {"androLib"})]
  public static int HoverId => MasterUIManager.UIBeingHovered;

  [JITWhenModsEnabled(new string[] {"androLib"})]
  public static void UpdateBagSlotCache()
  {
    if (!AndroLib.Enabled)
      return;
    AndroLibReference.UpdateItemSlot();
  }
}
