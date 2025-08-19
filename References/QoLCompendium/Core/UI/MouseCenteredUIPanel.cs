// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.UI.MouseCenteredUIPanel
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

#nullable disable
namespace QoLCompendium.Core.UI;

public class MouseCenteredUIPanel : UIPanel
{
  private bool centeredOnMouse;

  public virtual void OnActivate()
  {
    ((UIElement) this).OnActivate();
    this.centeredOnMouse = true;
  }

  public virtual void Update(GameTime gameTime)
  {
    ((UIElement) this).Update(gameTime);
    if (!this.centeredOnMouse)
      return;
    this.centeredOnMouse = false;
    ((StyleDimension) ref ((UIElement) this).Left).Set((float) Main.mouseX - ((UIElement) this).Width.Pixels / 2f, 0.0f);
    ((StyleDimension) ref ((UIElement) this).Top).Set((float) Main.mouseY - ((UIElement) this).Height.Pixels / 2f, 0.0f);
    ((UIElement) this).Recalculate();
  }

  protected virtual void DrawChildren(SpriteBatch spriteBatch)
  {
    if (this.centeredOnMouse)
      return;
    ((UIElement) this).DrawChildren(spriteBatch);
  }
}
