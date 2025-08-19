using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.ModLoader.UI
{
	// Token: 0x0200024B RID: 587
	internal sealed class UILoaderAnimatedImage : UIElement
	{
		// Token: 0x060029B3 RID: 10675 RVA: 0x00513F1C File Offset: 0x0051211C
		public UILoaderAnimatedImage(float left, float top, float scale = 1f)
		{
			this._scale = scale;
			this.Width.Pixels = 200f * scale;
			this.Height.Pixels = 200f * scale;
			this.HAlign = left;
			this.VAlign = top;
		}

		// Token: 0x060029B4 RID: 10676 RVA: 0x00513F68 File Offset: 0x00512168
		public override void OnInitialize()
		{
			this._backgroundTexture = UICommon.LoaderBgTexture;
			this._loaderTexture = UICommon.LoaderTexture;
		}

		// Token: 0x060029B5 RID: 10677 RVA: 0x00513F80 File Offset: 0x00512180
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			int num = this.FrameTick + 1;
			this.FrameTick = num;
			if (num >= 5)
			{
				this.FrameTick = 0;
				num = this.Frame + 1;
				this.Frame = num;
				if (num >= 16)
				{
					this.Frame = 0;
				}
			}
			CalculatedStyle dimensions = base.GetDimensions();
			if (this.WithBackground)
			{
				spriteBatch.Draw(this._backgroundTexture.Value, new Vector2((float)((int)dimensions.X), (float)((int)dimensions.Y)), new Rectangle?(new Rectangle(0, 0, 200, 200)), Color.White, 0f, new Vector2(0f, 0f), this._scale, 0, 0f);
			}
			spriteBatch.Draw(this._loaderTexture.Value, new Vector2((float)((int)dimensions.X), (float)((int)dimensions.Y)), new Rectangle?(new Rectangle(200 * (this.Frame / 8), 200 * (this.Frame % 8), 200, 200)), Color.White, 0f, new Vector2(0f, 0f), this._scale, 0, 0f);
		}

		// Token: 0x04001A7C RID: 6780
		public const int MAX_FRAMES = 16;

		// Token: 0x04001A7D RID: 6781
		public const int MAX_DELAY = 5;

		// Token: 0x04001A7E RID: 6782
		public bool WithBackground;

		// Token: 0x04001A7F RID: 6783
		public int FrameTick;

		// Token: 0x04001A80 RID: 6784
		public int Frame;

		// Token: 0x04001A81 RID: 6785
		private readonly float _scale;

		// Token: 0x04001A82 RID: 6786
		private Asset<Texture2D> _backgroundTexture;

		// Token: 0x04001A83 RID: 6787
		private Asset<Texture2D> _loaderTexture;
	}
}
