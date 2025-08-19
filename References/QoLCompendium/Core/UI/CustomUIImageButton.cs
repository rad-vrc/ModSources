// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.UI.CustomUIImageButton
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.UI;

#nullable disable
namespace QoLCompendium.Core.UI;

public class CustomUIImageButton : SafeUIElement
{
  private Asset<Texture2D> _texture;
  private Asset<Texture2D> _borderTexture;

  public float VisibilityActive { get; private set; } = 1f;

  public float VisibilityInactive { get; private set; } = 1f;

  public CustomUIImageButton(Asset<Texture2D> texture)
  {
    this._texture = texture;
    ((StyleDimension) ref this.Width).Set((float) Utils.Width(this._texture), 0.0f);
    ((StyleDimension) ref this.Height).Set((float) Utils.Height(this._texture), 0.0f);
  }

  public void SetHoverImage(Asset<Texture2D> texture) => this._borderTexture = texture;

  public void SetImage(Asset<Texture2D> texture)
  {
    this._texture = texture;
    ((StyleDimension) ref this.Width).Set((float) Utils.Width(this._texture), 0.0f);
    ((StyleDimension) ref this.Height).Set((float) Utils.Height(this._texture), 0.0f);
  }

  protected virtual void DrawSelf(SpriteBatch spriteBatch)
  {
    CalculatedStyle dimensions = this.GetDimensions();
    spriteBatch.Draw(this._texture.Value, ((CalculatedStyle) ref dimensions).Position(), Color.op_Multiply(Color.White, this.IsMouseHovering ? this.VisibilityActive : this.VisibilityInactive));
    if (this._borderTexture == null || !this.IsMouseHovering)
      return;
    spriteBatch.Draw(this._borderTexture.Value, ((CalculatedStyle) ref dimensions).Position(), Color.White);
  }

  public override void MouseOver(UIMouseEvent evt)
  {
    base.MouseOver(evt);
    SoundEngine.PlaySound(ref SoundID.MenuTick, new Vector2?(), (SoundUpdateCallback) null);
  }

  public override void SafeUpdate(GameTime gameTime)
  {
    if (!this.ContainsPoint(Main.MouseScreen))
      return;
    Main.LocalPlayer.mouseInterface = true;
  }

  public void SetVisibility(float whenActive, float whenInactive)
  {
    this.VisibilityActive = MathHelper.Clamp(whenActive, 0.0f, 1f);
    this.VisibilityInactive = MathHelper.Clamp(whenInactive, 0.0f, 1f);
  }
}
