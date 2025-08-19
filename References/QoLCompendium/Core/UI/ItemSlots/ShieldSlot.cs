// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.UI.ItemSlots.ShieldSlot
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.UI.ItemSlots;

public class ShieldSlot : ModAccessorySlot
{
  public virtual string FunctionalTexture => "QoLCompendium/Assets/Slots/Shield";

  public virtual string VanityTexture => "QoLCompendium/Assets/Slots/Shield";

  public virtual bool CanAcceptItem(Item checkItem, AccessorySlotType context)
  {
    return checkItem.shieldSlot > 0;
  }

  public virtual bool ModifyDefaultSwapSlot(Item item, int accSlotToSwapTo) => item.shieldSlot > 0;

  public virtual bool IsEnabled() => QoLCompendium.QoLCompendium.mainConfig.ShieldSlot;

  public virtual bool IsVisibleWhenNotEnabled() => false;

  public virtual void OnMouseHover(AccessorySlotType context)
  {
    switch (context - 10)
    {
      case 0:
        Main.hoverItemName = Language.GetTextValue("Mods.QoLCompendium.ShieldSlot.Shield");
        break;
      case 1:
        Main.hoverItemName = Language.GetTextValue("Mods.QoLCompendium.ShieldSlot.SocialShield");
        break;
      case 2:
        Main.hoverItemName = Language.GetTextValue("Mods.QoLCompendium.ShieldSlot.Dye");
        break;
    }
  }
}
