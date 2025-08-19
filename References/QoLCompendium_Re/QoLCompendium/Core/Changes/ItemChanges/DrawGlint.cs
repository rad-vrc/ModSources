using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.ItemChanges
{
	// Token: 0x0200025C RID: 604
	public class DrawGlint : GlobalItem
	{
		// Token: 0x06000E10 RID: 3600 RVA: 0x00070C68 File Offset: 0x0006EE68
		public override bool PreDrawInInventory(Item item, SpriteBatch sb, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			if (!Common.CheckToActivateGlintEffect(item))
			{
				return true;
			}
			Asset<Texture2D> texture = Common.GetAsset("Effects", "Glint_", (int)QoLCompendium.mainClientConfig.GlintColor);
			Effect shader = ModContent.Request<Effect>("QoLCompendium/Assets/Effects/Transform", 1).Value;
			shader.Parameters["uTime"].SetValue(Main.GlobalTimeWrappedHourly * 0.2f);
			shader.CurrentTechnique.Passes["EnchantedPass"].Apply();
			Main.instance.GraphicsDevice.Textures[1] = texture.Value;
			sb.End();
			sb.Begin(0, sb.GraphicsDevice.BlendState, sb.GraphicsDevice.SamplerStates[0], sb.GraphicsDevice.DepthStencilState, sb.GraphicsDevice.RasterizerState, shader, Main.UIScaleMatrix);
			return true;
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x00070D48 File Offset: 0x0006EF48
		public override void PostDrawInInventory(Item item, SpriteBatch sb, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			if (!Common.CheckToActivateGlintEffect(item))
			{
				return;
			}
			sb.End();
			sb.Begin(0, sb.GraphicsDevice.BlendState, sb.GraphicsDevice.SamplerStates[0], sb.GraphicsDevice.DepthStencilState, sb.GraphicsDevice.RasterizerState, null, Main.UIScaleMatrix);
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x00070DA4 File Offset: 0x0006EFA4
		public override bool PreDrawInWorld(Item item, SpriteBatch sb, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{
			if (!Common.CheckToActivateGlintEffect(item))
			{
				return true;
			}
			Asset<Texture2D> texture = Common.GetAsset("Effects", "Glint_", (int)QoLCompendium.mainClientConfig.GlintColor);
			Effect shader = ModContent.Request<Effect>("QoLCompendium/Assets/Effects/Transform", 1).Value;
			shader.Parameters["uTime"].SetValue(Main.GlobalTimeWrappedHourly * 0.2f);
			shader.CurrentTechnique.Passes["EnchantedPass"].Apply();
			Main.instance.GraphicsDevice.Textures[1] = texture.Value;
			sb.End();
			sb.Begin(0, Main.spriteBatch.GraphicsDevice.BlendState, sb.GraphicsDevice.SamplerStates[0], Main.spriteBatch.GraphicsDevice.DepthStencilState, sb.GraphicsDevice.RasterizerState, shader, Main.GameViewMatrix.TransformationMatrix);
			return true;
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x00070E90 File Offset: 0x0006F090
		public override void PostDrawInWorld(Item item, SpriteBatch sb, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			if (!Common.CheckToActivateGlintEffect(item))
			{
				return;
			}
			sb.End();
			sb.Begin(0, sb.GraphicsDevice.BlendState, sb.GraphicsDevice.SamplerStates[0], sb.GraphicsDevice.DepthStencilState, sb.GraphicsDevice.RasterizerState, null, Main.GameViewMatrix.TransformationMatrix);
		}
	}
}
