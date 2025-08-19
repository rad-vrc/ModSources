// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.UI.Buttons.PermanentBuffButton
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

public class PermanentBuffButton : CustomUIImageButton
{
  private readonly Asset<Texture2D> faceTexture;
  public Asset<Texture2D> drawTexture;
  public LocalizedText ModTooltip;
  public bool disabled;
  public bool moddedBuff;

  public string Tooltip { get; set; }

  public PermanentBuffButton(Asset<Texture2D> faceTexture)
    : base(ModContent.Request<Texture2D>("QoLCompendium/Assets/Items/PermanentBuff", (AssetRequestMode) 2))
  {
    this.faceTexture = faceTexture;
  }

  protected override void DrawSelf(SpriteBatch spriteBatch)
  {
    base.DrawSelf(spriteBatch);
    this.UpdateDraw(spriteBatch);
  }

  public virtual void Draw(SpriteBatch spriteBatch)
  {
    base.Draw(spriteBatch);
    this.UpdateDraw(spriteBatch);
  }

  private void UpdateDraw(SpriteBatch spriteBatch)
  {
    CalculatedStyle dimensions = this.GetDimensions();
    if (this.moddedBuff && this.drawTexture != null)
    {
      if (this.disabled)
        spriteBatch.Draw(this.drawTexture.Value, ((CalculatedStyle) ref dimensions).Position(), Color.op_Multiply(Color.Gray, this.IsMouseHovering ? this.VisibilityActive : this.VisibilityInactive));
      else
        spriteBatch.Draw(this.drawTexture.Value, ((CalculatedStyle) ref dimensions).Position(), Color.op_Multiply(Color.White, this.IsMouseHovering ? this.VisibilityActive : this.VisibilityInactive));
    }
    else if (this.disabled)
      spriteBatch.Draw(this.faceTexture.Value, ((CalculatedStyle) ref dimensions).Position(), Color.op_Multiply(Color.Gray, this.IsMouseHovering ? this.VisibilityActive : this.VisibilityInactive));
    else
      spriteBatch.Draw(this.faceTexture.Value, ((CalculatedStyle) ref dimensions).Position(), Color.op_Multiply(Color.White, this.IsMouseHovering ? this.VisibilityActive : this.VisibilityInactive));
    if (this.moddedBuff)
    {
      if (!this.IsMouseHovering)
        return;
      if (this.disabled)
      {
        Main.hoverItemName = $"{this.ModTooltip.Value} {Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.Disabled")}";
        Main.mouseText = true;
      }
      else
      {
        Main.hoverItemName = this.ModTooltip.Value;
        Main.mouseText = true;
      }
    }
    else
    {
      if (!this.IsMouseHovering)
        return;
      if (this.disabled)
      {
        Main.hoverItemName = $"{this.Tooltip} {Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.Disabled")}";
        Main.mouseText = true;
      }
      else
      {
        Main.hoverItemName = this.Tooltip;
        Main.mouseText = true;
      }
    }
  }
}
