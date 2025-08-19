using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x020002FE RID: 766
	internal class GuildpackMiscEffectsDrawLayer : PlayerDrawLayer
	{
		// Token: 0x06002E63 RID: 11875 RVA: 0x00531738 File Offset: 0x0052F938
		[NullableContext(1)]
		public override PlayerDrawLayer.Position GetDefaultPosition()
		{
			return new PlayerDrawLayer.BeforeParent(PlayerDrawLayers.JimsCloak);
		}

		// Token: 0x06002E64 RID: 11876 RVA: 0x00531744 File Offset: 0x0052F944
		public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
		{
			GuildpackSetEffectPlayer modPlayer;
			return drawInfo.drawPlayer.TryGetModPlayer<GuildpackSetEffectPlayer>(out modPlayer) && modPlayer.IsActive;
		}

		// Token: 0x06002E65 RID: 11877 RVA: 0x00531768 File Offset: 0x0052F968
		protected override void Draw(ref PlayerDrawSet drawInfo)
		{
			if (drawInfo.shadow != 0f)
			{
				return;
			}
			if (this.textureAsset == null)
			{
				this.textureAsset = ModContent.Request<Texture2D>("ModLoader/Patreon.Guildpack_Aura", 2);
			}
			if (this.textureAsset.IsLoaded)
			{
				Texture2D texture = this.textureAsset.Value;
				if (texture != null)
				{
					Player player = drawInfo.drawPlayer;
					int frameSize = texture.Height / 3;
					int frame = DateTime.Now.Millisecond / 167 % 3;
					Vector2 position = (drawInfo.Position + player.Size * 0.5f - Main.screenPosition).ToPoint().ToVector2();
					Rectangle srcRect;
					srcRect..ctor(0, frameSize * frame, texture.Width, frameSize);
					drawInfo.DrawDataCache.Add(new DrawData(texture, position, new Rectangle?(srcRect), Color.White, 0f, new Vector2((float)texture.Width, (float)frameSize) * 0.5f, 1f, drawInfo.playerEffect, 0f));
					return;
				}
			}
		}

		// Token: 0x04001C79 RID: 7289
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private Asset<Texture2D> textureAsset;
	}
}
