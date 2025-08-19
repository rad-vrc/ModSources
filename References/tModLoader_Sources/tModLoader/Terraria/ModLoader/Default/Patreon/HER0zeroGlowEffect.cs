using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x02000307 RID: 775
	internal class HER0zeroGlowEffect : PlayerDrawLayer
	{
		// Token: 0x06002E7D RID: 11901 RVA: 0x00531C41 File Offset: 0x0052FE41
		public override PlayerDrawLayer.Position GetDefaultPosition()
		{
			return new PlayerDrawLayer.BeforeParent(PlayerDrawLayers.JimsCloak);
		}

		// Token: 0x06002E7E RID: 11902 RVA: 0x00531C50 File Offset: 0x0052FE50
		public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
		{
			HER0zeroPlayer modPlayer;
			return drawInfo.drawPlayer.TryGetModPlayer<HER0zeroPlayer>(out modPlayer) && modPlayer.glowEffect;
		}

		// Token: 0x06002E7F RID: 11903 RVA: 0x00531C74 File Offset: 0x0052FE74
		protected override void Draw(ref PlayerDrawSet drawInfo)
		{
			if (drawInfo.shadow != 0f)
			{
				return;
			}
			if (this.textureAsset == null)
			{
				this.textureAsset = ModContent.Request<Texture2D>("ModLoader/Patreon.HER0zero_Effect", 2);
			}
			if (this.textureAsset.IsLoaded)
			{
				Texture2D texture = this.textureAsset.Value;
				if (texture != null)
				{
					Player player = drawInfo.drawPlayer;
					int frameSize = texture.Height / 4;
					int frame = player.miscCounter % 40 / 10;
					float alpha = 0.5f;
					Vector2 position = (drawInfo.Position + player.Size * 0.5f - Main.screenPosition).ToPoint().ToVector2();
					Rectangle srcRect;
					srcRect..ctor(0, frameSize * frame, texture.Width, frameSize);
					drawInfo.DrawDataCache.Add(new DrawData(texture, position, new Rectangle?(srcRect), Color.White * alpha, 0f, new Vector2((float)texture.Width, (float)frameSize) * 0.5f, 1f, drawInfo.playerEffect, 0f));
					return;
				}
			}
		}

		// Token: 0x04001C7D RID: 7293
		[Nullable(new byte[]
		{
			2,
			0
		})]
		private Asset<Texture2D> textureAsset;
	}
}
