// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.UI.Buttons.SpawnModifierButton
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

#nullable disable
namespace QoLCompendium.Core.UI.Buttons;

public class SpawnModifierButton : CustomUIImageButton
{
  private readonly Asset<Texture2D> faceTexture;
  public static int modifierTexture;
  public static int backgroundTexture;

  public LocalizedText Tooltip { get; set; }

  public SpawnModifierButton(Asset<Texture2D> faceTexture)
    : base(ModContent.Request<Texture2D>("QoLCompendium/Assets/Backgrounds/Background_" + SpawnModifierButton.backgroundTexture.ToString(), (AssetRequestMode) 2))
  {
    this.faceTexture = faceTexture;
    this.SetHoverImage(ModContent.Request<Texture2D>("QoLCompendium/Assets/SpawnModifiers/Modifier_Hover_" + SpawnModifierButton.modifierTexture.ToString(), (AssetRequestMode) 2));
  }

  protected override void DrawSelf(SpriteBatch spriteBatch)
  {
    base.DrawSelf(spriteBatch);
    CalculatedStyle dimensions = this.GetDimensions();
    spriteBatch.Draw(this.faceTexture.Value, ((CalculatedStyle) ref dimensions).Position(), Color.op_Multiply(Color.White, this.IsMouseHovering ? this.VisibilityActive : this.VisibilityInactive));
  }

  public virtual void Draw(SpriteBatch spriteBatch)
  {
    base.Draw(spriteBatch);
    if (!this.IsMouseHovering)
      return;
    Main.hoverItemName = this.Tooltip.Value;
    Main.mouseText = true;
  }
}
