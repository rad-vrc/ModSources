using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x02000321 RID: 801
	internal class SaetharEyeGlowDrawLayer : PlayerDrawLayer
	{
		// Token: 0x06002EC9 RID: 11977 RVA: 0x005326E3 File Offset: 0x005308E3
		[NullableContext(1)]
		public override PlayerDrawLayer.Position GetDefaultPosition()
		{
			return new PlayerDrawLayer.AfterParent(PlayerDrawLayers.Head);
		}

		// Token: 0x06002ECA RID: 11978 RVA: 0x005326F0 File Offset: 0x005308F0
		public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
		{
			SaetharSetEffectPlayer modPlayer;
			return drawInfo.drawPlayer.TryGetModPlayer<SaetharSetEffectPlayer>(out modPlayer) && modPlayer.IsActive;
		}

		// Token: 0x06002ECB RID: 11979 RVA: 0x00532714 File Offset: 0x00530914
		protected override void Draw(ref PlayerDrawSet drawInfo)
		{
			if (drawInfo.shadow != 0f)
			{
				return;
			}
			if (this.textureAsset == null)
			{
				this.textureAsset = ModContent.Request<Texture2D>("ModLoader/Patreon.Saethar_Head_Glow", 2);
			}
			Asset<Texture2D> asset = this.textureAsset;
			if (asset != null && asset.IsLoaded)
			{
				Texture2D texture = asset.Value;
				if (texture != null)
				{
					Vector2 headOrigin = drawInfo.headVect;
					Player player = drawInfo.drawPlayer;
					Vector2 position = player.headPosition + drawInfo.headVect + new Vector2((float)((int)(drawInfo.Position.X + (float)player.width / 2f - (float)player.bodyFrame.Width / 2f - Main.screenPosition.X)), (float)((int)(drawInfo.Position.Y + (float)player.height - (float)player.bodyFrame.Height + 4f - Main.screenPosition.Y)));
					drawInfo.DrawDataCache.Add(new DrawData(texture, position, new Rectangle?(player.bodyFrame), Color.White, player.headRotation, headOrigin, 1f, drawInfo.playerEffect, 0f));
					return;
				}
			}
		}

		// Token: 0x04001C82 RID: 7298
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private Asset<Texture2D> textureAsset;
	}
}
