// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.UI.ItemSlots.WingSlot
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.UI.ItemSlots;

public class WingSlot : ModAccessorySlot
{
  public virtual string FunctionalTexture => "QoLCompendium/Assets/Slots/Wings";

  public virtual string VanityTexture => "QoLCompendium/Assets/Slots/Wings";

  public virtual bool CanAcceptItem(Item checkItem, AccessorySlotType context)
  {
    return checkItem.wingSlot > 0;
  }

  public virtual bool ModifyDefaultSwapSlot(Item item, int accSlotToSwapTo) => item.wingSlot > 0;

  public virtual bool IsEnabled() => QoLCompendium.QoLCompendium.mainConfig.WingSlot;

  public virtual bool IsVisibleWhenNotEnabled() => false;

  public virtual void OnMouseHover(AccessorySlotType context)
  {
    switch (context - 10)
    {
      case 0:
        Main.hoverItemName = Language.GetTextValue("Mods.QoLCompendium.WingSlot.Wings");
        break;
      case 1:
        Main.hoverItemName = Language.GetTextValue("Mods.QoLCompendium.WingSlot.SocialWings");
        break;
      case 2:
        Main.hoverItemName = Language.GetTextValue("Mods.QoLCompendium.WingSlot.Dye");
        break;
    }
  }
}
