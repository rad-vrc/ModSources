// Decompiled with JetBrains decompiler
// Type: InventoryDrag.Compatability.AndroLibPlayer
// Assembly: InventoryDrag, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0AEBC7B1-9360-4E40-BFA4-A14C9A81EEA1
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\InventoryDrag\InventoryDrag.dll

using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace InventoryDrag.Compatability;

[Autoload(false)]
public class AndroLibPlayer : ModPlayer
{
  internal int slotIdCache;
  internal int slotIndexCache;
  internal bool didSlotChange;

  public void UpdateSlotChange(int slotId, int slotIndex)
  {
    if (((Entity) Main.LocalPlayer).whoAmI != ((Entity) this.Player).whoAmI)
      return;
    this.didSlotChange = slotId != this.slotIdCache || slotIndex != this.slotIndexCache;
    this.slotIdCache = slotId;
    this.slotIndexCache = slotIndex;
  }
}
