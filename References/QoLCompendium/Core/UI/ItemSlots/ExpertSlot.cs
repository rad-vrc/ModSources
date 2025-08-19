// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.UI.ItemSlots.ExpertSlot
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.UI.ItemSlots;

public class ExpertSlot : ModAccessorySlot
{
  public virtual string FunctionalTexture => "QoLCompendium/Assets/Slots/Expert";

  public virtual string VanityTexture => "QoLCompendium/Assets/Slots/Expert";

  public virtual bool CanAcceptItem(Item checkItem, AccessorySlotType context)
  {
    return checkItem.expert && checkItem.accessory;
  }

  public virtual bool ModifyDefaultSwapSlot(Item item, int accSlotToSwapTo)
  {
    return item.expert && item.accessory;
  }

  public virtual bool IsEnabled() => QoLCompendium.QoLCompendium.mainConfig.ExpertSlot && Main.expertMode;

  public virtual bool IsVisibleWhenNotEnabled() => false;

  public virtual void OnMouseHover(AccessorySlotType context)
  {
    switch (context - 10)
    {
      case 0:
        Main.hoverItemName = Language.GetTextValue("Mods.QoLCompendium.ExpertSlot.Expert");
        break;
      case 1:
        Main.hoverItemName = Language.GetTextValue("Mods.QoLCompendium.ExpertSlot.SocialExpert");
        break;
      case 2:
        Main.hoverItemName = Language.GetTextValue("Mods.QoLCompendium.ExpertSlot.Dye");
        break;
    }
  }
}
