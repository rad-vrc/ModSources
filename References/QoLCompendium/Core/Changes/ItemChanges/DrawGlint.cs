// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.ItemChanges.DrawGlint
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.ItemChanges;

public class DrawGlint : GlobalItem
{
  public virtual bool PreDrawInInventory(
    Item item,
    SpriteBatch sb,
    Vector2 position,
    Rectangle frame,
    Color drawColor,
    Color itemColor,
    Vector2 origin,
    float scale)
  {
    if (!Common.CheckToActivateGlintEffect(item))
      return true;
    Asset<Texture2D> asset = Common.GetAsset("Effects", "Glint_", (int) QoLCompendium.QoLCompendium.mainClientConfig.GlintColor);
    Effect effect = ModContent.Request<Effect>("QoLCompendium/Assets/Effects/Transform", (AssetRequestMode) 1).Value;
    effect.Parameters["uTime"].SetValue(Main.GlobalTimeWrappedHourly * 0.2f);
    effect.CurrentTechnique.Passes["EnchantedPass"].Apply();
    ((Game) Main.instance).GraphicsDevice.Textures[1] = (Texture) asset.Value;
    sb.End();
    sb.Begin((SpriteSortMode) 0, ((GraphicsResource) sb).GraphicsDevice.BlendState, ((GraphicsResource) sb).GraphicsDevice.SamplerStates[0], ((GraphicsResource) sb).GraphicsDevice.DepthStencilState, ((GraphicsResource) sb).GraphicsDevice.RasterizerState, effect, Main.UIScaleMatrix);
    return true;
  }

  public virtual void PostDrawInInventory(
    Item item,
    SpriteBatch sb,
    Vector2 position,
    Rectangle frame,
    Color drawColor,
    Color itemColor,
    Vector2 origin,
    float scale)
  {
    if (!Common.CheckToActivateGlintEffect(item))
      return;
    sb.End();
    sb.Begin((SpriteSortMode) 0, ((GraphicsResource) sb).GraphicsDevice.BlendState, ((GraphicsResource) sb).GraphicsDevice.SamplerStates[0], ((GraphicsResource) sb).GraphicsDevice.DepthStencilState, ((GraphicsResource) sb).GraphicsDevice.RasterizerState, (Effect) null, Main.UIScaleMatrix);
  }

  public virtual bool PreDrawInWorld(
    Item item,
    SpriteBatch sb,
    Color lightColor,
    Color alphaColor,
    ref float rotation,
    ref float scale,
    int whoAmI)
  {
    if (!Common.CheckToActivateGlintEffect(item))
      return true;
    Asset<Texture2D> asset = Common.GetAsset("Effects", "Glint_", (int) QoLCompendium.QoLCompendium.mainClientConfig.GlintColor);
    Effect effect = ModContent.Request<Effect>("QoLCompendium/Assets/Effects/Transform", (AssetRequestMode) 1).Value;
    effect.Parameters["uTime"].SetValue(Main.GlobalTimeWrappedHourly * 0.2f);
    effect.CurrentTechnique.Passes["EnchantedPass"].Apply();
    ((Game) Main.instance).GraphicsDevice.Textures[1] = (Texture) asset.Value;
    sb.End();
    sb.Begin((SpriteSortMode) 0, ((GraphicsResource) Main.spriteBatch).GraphicsDevice.BlendState, ((GraphicsResource) sb).GraphicsDevice.SamplerStates[0], ((GraphicsResource) Main.spriteBatch).GraphicsDevice.DepthStencilState, ((GraphicsResource) sb).GraphicsDevice.RasterizerState, effect, Main.GameViewMatrix.TransformationMatrix);
    return true;
  }

  public virtual void PostDrawInWorld(
    Item item,
    SpriteBatch sb,
    Color lightColor,
    Color alphaColor,
    float rotation,
    float scale,
    int whoAmI)
  {
    if (!Common.CheckToActivateGlintEffect(item))
      return;
    sb.End();
    sb.Begin((SpriteSortMode) 0, ((GraphicsResource) sb).GraphicsDevice.BlendState, ((GraphicsResource) sb).GraphicsDevice.SamplerStates[0], ((GraphicsResource) sb).GraphicsDevice.DepthStencilState, ((GraphicsResource) sb).GraphicsDevice.RasterizerState, (Effect) null, Main.GameViewMatrix.TransformationMatrix);
  }
}
