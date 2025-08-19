using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x020002FF RID: 767
	internal class GuildpackEyeGlowDrawLayer : PlayerDrawLayer
	{
		// Token: 0x06002E67 RID: 11879 RVA: 0x00531877 File Offset: 0x0052FA77
		[NullableContext(1)]
		public override PlayerDrawLayer.Position GetDefaultPosition()
		{
			return new PlayerDrawLayer.AfterParent(PlayerDrawLayers.Head);
		}

		// Token: 0x06002E68 RID: 11880 RVA: 0x00531884 File Offset: 0x0052FA84
		public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
		{
			GuildpackSetEffectPlayer modPlayer;
			return drawInfo.drawPlayer.TryGetModPlayer<GuildpackSetEffectPlayer>(out modPlayer) && modPlayer.IsActive;
		}

		// Token: 0x06002E69 RID: 11881 RVA: 0x005318A8 File Offset: 0x0052FAA8
		protected override void Draw(ref PlayerDrawSet drawInfo)
		{
			if (this.textureAsset == null)
			{
				this.textureAsset = ModContent.Request<Texture2D>("ModLoader/Patreon.Guildpack_Head_Glow", 2);
			}
			if (this.textureAsset.IsLoaded)
			{
				Texture2D texture = this.textureAsset.Value;
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

		// Token: 0x04001C7A RID: 7290
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private Asset<Texture2D> textureAsset;
	}
}
