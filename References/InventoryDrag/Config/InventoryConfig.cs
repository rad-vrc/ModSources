// Decompiled with JetBrains decompiler
// Type: InventoryDrag.Config.InventoryConfig
// Assembly: InventoryDrag, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0AEBC7B1-9360-4E40-BFA4-A14C9A81EEA1
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\InventoryDrag\InventoryDrag.dll

using Terraria.ModLoader;
using Terraria.ModLoader.Config;

#nullable disable
namespace InventoryDrag.Config;

public class InventoryConfig : ModConfig
{
  [Header("MainFeatures")]
  [Expand(false)]
  public LeftMouseOptions LeftMouse = new LeftMouseOptions();
  [Expand(false)]
  public RightMouseOptions RightMouse = new RightMouseOptions();
  [Header("ExtraFeatures")]
  [Expand(false)]
  public SplittableGrabBags SplittableGrabBags = new SplittableGrabBags();
  [Expand(false)]
  public ThrowDragging ThrowDragging = new ThrowDragging();
  public bool DebugMessages;

  public static InventoryConfig Instance => ModContent.GetInstance<InventoryConfig>();

  public virtual ConfigScope Mode => (ConfigScope) 1;
}
