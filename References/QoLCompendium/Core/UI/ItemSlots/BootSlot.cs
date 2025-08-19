// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.UI.ItemSlots.BootSlot
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.UI.ItemSlots;

public class BootSlot : ModAccessorySlot
{
  public virtual string FunctionalTexture => "QoLCompendium/Assets/Slots/Boots";

  public virtual string VanityTexture => "QoLCompendium/Assets/Slots/Boots";

  public virtual bool CanAcceptItem(Item checkItem, AccessorySlotType context)
  {
    return checkItem.shoeSlot > 0;
  }

  public virtual bool ModifyDefaultSwapSlot(Item item, int accSlotToSwapTo) => item.shoeSlot > 0;

  public virtual bool IsEnabled() => QoLCompendium.QoLCompendium.mainConfig.BootSlot;

  public virtual bool IsVisibleWhenNotEnabled() => false;

  public virtual void OnMouseHover(AccessorySlotType context)
  {
    switch (context - 10)
    {
      case 0:
        Main.hoverItemName = Language.GetTextValue("Mods.QoLCompendium.BootSlot.Boots");
        break;
      case 1:
        Main.hoverItemName = Language.GetTextValue("Mods.QoLCompendium.BootSlot.SocialBoots");
        break;
      case 2:
        Main.hoverItemName = Language.GetTextValue("Mods.QoLCompendium.BootSlot.Dye");
        break;
    }
  }
}
