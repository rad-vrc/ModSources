﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent
{
	// Token: 0x020001D9 RID: 473
	public class PlayerQueenSlimeMountTextureContent : ARenderTargetContentByRequest
	{
		// Token: 0x06001C24 RID: 7204 RVA: 0x004F19C0 File Offset: 0x004EFBC0
		protected override void HandleUseReqest(GraphicsDevice device, SpriteBatch spriteBatch)
		{
			Asset<Texture2D> asset = TextureAssets.Extra[204];
			base.PrepareARenderTarget_AndListenToEvents(ref this._target, device, asset.Width(), asset.Height(), RenderTargetUsage.PreserveContents);
			device.SetRenderTarget(this._target);
			device.Clear(Color.Transparent);
			DrawData value = new DrawData(asset.Value, Vector2.Zero, Color.White);
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
			GameShaders.Misc["QueenSlime"].Apply(new DrawData?(value));
			value.Draw(spriteBatch);
			spriteBatch.End();
			device.SetRenderTarget(null);
			this._wasPrepared = true;
		}
	}
}
