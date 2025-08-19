// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.UI.SafeUIElement
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using Terraria.UI;

#nullable disable
namespace QoLCompendium.Core.UI;

public class SafeUIElement : UIElement
{
  public virtual void SafeXButton1MouseUp(UIMouseEvent evt)
  {
  }

  public virtual void XButton1MouseUp(UIMouseEvent evt)
  {
    base.XButton1MouseUp(evt);
    this.SafeXButton1MouseUp(evt);
  }

  public virtual void SafeXButton1MouseDown(UIMouseEvent evt)
  {
  }

  public virtual void XButton1MouseDown(UIMouseEvent evt)
  {
    base.XButton1MouseDown(evt);
    this.SafeXButton1MouseDown(evt);
  }

  public virtual void SafeXButton1Click(UIMouseEvent evt)
  {
  }

  public virtual void XButton1Click(UIMouseEvent evt)
  {
    base.XButton1Click(evt);
    this.SafeXButton1Click(evt);
  }

  public virtual void SafeXButton1DoubleClick(UIMouseEvent evt)
  {
  }

  public virtual void XButton1DoubleClick(UIMouseEvent evt)
  {
    base.XButton1DoubleClick(evt);
    this.SafeXButton1DoubleClick(evt);
  }

  public virtual void SafeXButton2MouseUp(UIMouseEvent evt)
  {
  }

  public virtual void XButton2MouseUp(UIMouseEvent evt)
  {
    base.XButton2MouseUp(evt);
    this.SafeXButton2MouseUp(evt);
  }

  public virtual void SafeXButton2MouseDown(UIMouseEvent evt)
  {
  }

  public virtual void XButton2MouseDown(UIMouseEvent evt)
  {
    base.XButton2MouseDown(evt);
    this.SafeXButton2MouseDown(evt);
  }

  public virtual void SafeXButton2Click(UIMouseEvent evt)
  {
  }

  public virtual void XButton2Click(UIMouseEvent evt)
  {
    base.XButton2Click(evt);
    this.SafeXButton2Click(evt);
  }

  public virtual void SafeXButton2DoubleClick(UIMouseEvent evt)
  {
  }

  public virtual void XButton2DoubleClick(UIMouseEvent evt)
  {
    base.XButton2DoubleClick(evt);
    this.SafeXButton2DoubleClick(evt);
  }

  public virtual void SafeLeftMouseUp(UIMouseEvent evt)
  {
  }

  public virtual void LeftMouseUp(UIMouseEvent evt)
  {
    base.LeftMouseUp(evt);
    this.SafeLeftMouseUp(evt);
  }

  public virtual void SafeLeftMouseDown(UIMouseEvent evt)
  {
  }

  public virtual void LeftMouseDown(UIMouseEvent evt)
  {
    base.LeftMouseDown(evt);
    this.SafeLeftMouseDown(evt);
  }

  public virtual void SafeLeftClick(UIMouseEvent evt)
  {
  }

  public virtual void LeftClick(UIMouseEvent evt)
  {
    base.LeftClick(evt);
    this.SafeLeftClick(evt);
  }

  public virtual void SafeLeftDoubleClick(UIMouseEvent evt)
  {
  }

  public virtual void LeftDoubleClick(UIMouseEvent evt)
  {
    base.LeftDoubleClick(evt);
    this.SafeLeftDoubleClick(evt);
  }

  public virtual void SafeRightMouseUp(UIMouseEvent evt)
  {
  }

  public virtual void RightMouseUp(UIMouseEvent evt)
  {
    base.RightMouseUp(evt);
    this.SafeRightMouseUp(evt);
  }

  public virtual void SafeRightMouseDown(UIMouseEvent evt)
  {
  }

  public virtual void RightMouseDown(UIMouseEvent evt)
  {
    base.RightMouseDown(evt);
    this.SafeRightMouseDown(evt);
  }

  public virtual void SafeRightClick(UIMouseEvent evt)
  {
  }

  public virtual void RightClick(UIMouseEvent evt)
  {
    base.RightClick(evt);
    this.SafeRightClick(evt);
  }

  public virtual void SafeRightDoubleClick(UIMouseEvent evt)
  {
  }

  public virtual void RightDoubleClick(UIMouseEvent evt)
  {
    base.RightDoubleClick(evt);
    this.SafeRightDoubleClick(evt);
  }

  public virtual void SafeMiddleMouseUp(UIMouseEvent evt)
  {
  }

  public virtual void MiddleMouseUp(UIMouseEvent evt)
  {
    base.MiddleMouseUp(evt);
    this.SafeMiddleMouseUp(evt);
  }

  public virtual void SafeMiddleMouseDown(UIMouseEvent evt)
  {
  }

  public virtual void MiddleMouseDown(UIMouseEvent evt)
  {
    base.MiddleMouseDown(evt);
    this.SafeMiddleMouseDown(evt);
  }

  public virtual void SafeMiddleClick(UIMouseEvent evt)
  {
  }

  public virtual void MiddleClick(UIMouseEvent evt)
  {
    base.MiddleClick(evt);
    this.SafeMiddleClick(evt);
  }

  public virtual void SafeMiddleDoubleClick(UIMouseEvent evt)
  {
  }

  public virtual void MiddleDoubleClick(UIMouseEvent evt)
  {
    base.MiddleDoubleClick(evt);
    this.SafeMiddleDoubleClick(evt);
  }

  public virtual void SafeMouseOver(UIMouseEvent evt)
  {
  }

  public virtual void MouseOver(UIMouseEvent evt)
  {
    base.MouseOver(evt);
    this.SafeMouseOver(evt);
  }

  public virtual void SafeUpdate(GameTime gameTime)
  {
  }

  public virtual void Update(GameTime gameTime)
  {
    base.Update(gameTime);
    this.SafeUpdate(gameTime);
  }

  public virtual void SafeScrollWheel(UIScrollWheelEvent evt)
  {
  }

  public virtual void ScrollWheel(UIScrollWheelEvent evt)
  {
    base.ScrollWheel(evt);
    this.SafeScrollWheel(evt);
  }
}
