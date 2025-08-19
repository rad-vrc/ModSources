// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.UI.Buttons.ShopButtons
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using QoLCompendium.Core.UI.Other;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.UI;

#nullable disable
namespace QoLCompendium.Core.UI.Buttons;

public class ShopButtons : GameInterfaceLayer
{
  public ShopButtons()
    : base("QoLCompendium: Shop Buttons", (InterfaceScaleType) 1)
  {
  }

  protected virtual bool DrawSelf()
  {
    if (Main.npcShop <= 0)
      return true;
    ShopExpander instance = ShopExpander.instance;
    float num1 = 0.755f;
    Vector2 vector2 = Vector2.op_Multiply(Utils.Size(TextureAssets.InventoryBack6), num1);
    int num2 = Utils.Width(TextureAssets.CraftUpButton);
    int num3 = Utils.Height(TextureAssets.CraftUpButton);
    int width = TextureAssets.ScrollLeftButton.Value.Width;
    int num4 = (int) (73.0 + 560.0 * (double) Main.inventoryScale);
    int invBottom = Main.instance.invBottom;
    int num5 = (int) ((double) Main.instance.invBottom + 168.0 * (double) Main.inventoryScale + (double) vector2.Y - (double) num3);
    bool flag = false;
    if (instance.page > 0)
    {
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(num4, num5 - num3 - 4, num2, num3);
      Color color = Color.op_Multiply(Color.White, 0.8f);
      if (((Rectangle) ref rectangle).Contains(Utils.ToPoint(Main.MouseScreen)))
      {
        Main.LocalPlayer.mouseInterface = true;
        if (Main.mouseLeft && Main.mouseLeftRelease)
        {
          --instance.page;
          flag = true;
        }
        color = Color.White;
      }
      Main.spriteBatch.Draw(TextureAssets.CraftUpButton.Value, rectangle, color);
    }
    if (instance.page < instance.pageCount - 1)
    {
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(num4, num5, num2, num3);
      Color color = Color.op_Multiply(Color.White, 0.8f);
      if (((Rectangle) ref rectangle).Contains(Utils.ToPoint(Main.MouseScreen)))
      {
        Main.LocalPlayer.mouseInterface = true;
        if (Main.mouseLeft && Main.mouseLeftRelease)
        {
          ++instance.page;
          flag = true;
        }
        color = Color.White;
      }
      Main.spriteBatch.Draw(TextureAssets.CraftDownButton.Value, rectangle, color);
    }
    if (flag)
    {
      instance.Refresh();
      SoundEngine.PlaySound(ref SoundID.MenuTick, new Vector2?(), (SoundUpdateCallback) null);
    }
    return true;
  }
}
