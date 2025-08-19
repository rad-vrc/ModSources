using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000517 RID: 1303
	public class UIGenProgressBar : UIElement
	{
		// Token: 0x06003EAD RID: 16045 RVA: 0x005D45B8 File Offset: 0x005D27B8
		public UIGenProgressBar()
		{
			if (Main.netMode != 2)
			{
				this._texOuterCorrupt = Main.Assets.Request<Texture2D>("Images/UI/WorldGen/Outer_Corrupt");
				this._texOuterCrimson = Main.Assets.Request<Texture2D>("Images/UI/WorldGen/Outer_Crimson");
				this._texOuterLower = Main.Assets.Request<Texture2D>("Images/UI/WorldGen/Outer_Lower");
			}
			this.Recalculate();
		}

		// Token: 0x06003EAE RID: 16046 RVA: 0x005D4630 File Offset: 0x005D2830
		public override void Recalculate()
		{
			this.Width.Precent = 0f;
			this.Height.Precent = 0f;
			this.Width.Pixels = 612f;
			this.Height.Pixels = 70f;
			base.Recalculate();
		}

		// Token: 0x06003EAF RID: 16047 RVA: 0x005D4683 File Offset: 0x005D2883
		public void SetProgress(float overallProgress, float currentProgress)
		{
			this._targetCurrentProgress = currentProgress;
			this._targetOverallProgress = overallProgress;
		}

		// Token: 0x06003EB0 RID: 16048 RVA: 0x005D4694 File Offset: 0x005D2894
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (this._texOuterCorrupt.IsLoaded && this._texOuterCrimson.IsLoaded && this._texOuterLower.IsLoaded)
			{
				bool flag = WorldGen.crimson;
				if (WorldGen.drunkWorldGen && Main.rand.Next(2) == 0)
				{
					flag = !flag;
				}
				this._visualOverallProgress = this._targetOverallProgress;
				this._visualCurrentProgress = this._targetCurrentProgress;
				CalculatedStyle dimensions = base.GetDimensions();
				int completedWidth = (int)(this._visualOverallProgress * (float)this._longBarWidth);
				int completedWidth2 = (int)(this._visualCurrentProgress * (float)this._smallBarWidth);
				Vector2 vector;
				vector..ctor(dimensions.X, dimensions.Y);
				Color color = default(Color);
				color.PackedValue = (flag ? 4286836223U : 4283888223U);
				this.DrawFilling2(spriteBatch, vector + new Vector2(20f, 40f), 16, completedWidth, this._longBarWidth, color, Color.Lerp(color, Color.Black, 0.5f), new Color(48, 48, 48));
				color.PackedValue = 4290947159U;
				this.DrawFilling2(spriteBatch, vector + new Vector2(50f, 60f), 8, completedWidth2, this._smallBarWidth, color, Color.Lerp(color, Color.Black, 0.5f), new Color(33, 33, 33));
				Rectangle r = base.GetDimensions().ToRectangle();
				r.X -= 8;
				spriteBatch.Draw(flag ? this._texOuterCrimson.Value : this._texOuterCorrupt.Value, r.TopLeft(), Color.White);
				spriteBatch.Draw(this._texOuterLower.Value, r.TopLeft() + new Vector2(44f, 60f), Color.White);
			}
		}

		// Token: 0x06003EB1 RID: 16049 RVA: 0x005D4870 File Offset: 0x005D2A70
		private void DrawFilling(SpriteBatch spritebatch, Texture2D tex, Texture2D texShadow, Vector2 topLeft, int completedWidth, int totalWidth, Color separator, Color empty)
		{
			if (completedWidth % 2 != 0)
			{
				completedWidth--;
			}
			Vector2 position = topLeft + (float)completedWidth * Vector2.UnitX;
			int num = completedWidth;
			Rectangle value = tex.Frame(1, 1, 0, 0, 0, 0);
			while (num > 0)
			{
				if (value.Width > num)
				{
					value.X += value.Width - num;
					value.Width = num;
				}
				spritebatch.Draw(tex, position, new Rectangle?(value), Color.White, 0f, new Vector2((float)value.Width, 0f), 1f, 0, 0f);
				position.X -= (float)value.Width;
				num -= value.Width;
			}
			if (texShadow != null)
			{
				spritebatch.Draw(texShadow, topLeft, new Rectangle?(new Rectangle(0, 0, completedWidth, texShadow.Height)), Color.White);
			}
			spritebatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle((int)topLeft.X + completedWidth, (int)topLeft.Y, totalWidth - completedWidth, tex.Height), new Rectangle?(new Rectangle(0, 0, 1, 1)), empty);
			spritebatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle((int)topLeft.X + completedWidth - 2, (int)topLeft.Y, 2, tex.Height), new Rectangle?(new Rectangle(0, 0, 1, 1)), separator);
		}

		// Token: 0x06003EB2 RID: 16050 RVA: 0x005D49D0 File Offset: 0x005D2BD0
		private void DrawFilling2(SpriteBatch spritebatch, Vector2 topLeft, int height, int completedWidth, int totalWidth, Color filled, Color separator, Color empty)
		{
			if (completedWidth % 2 != 0)
			{
				completedWidth--;
			}
			spritebatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle((int)topLeft.X, (int)topLeft.Y, completedWidth, height), new Rectangle?(new Rectangle(0, 0, 1, 1)), filled);
			spritebatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle((int)topLeft.X + completedWidth, (int)topLeft.Y, totalWidth - completedWidth, height), new Rectangle?(new Rectangle(0, 0, 1, 1)), empty);
			spritebatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle((int)topLeft.X + completedWidth - 2, (int)topLeft.Y, 2, height), new Rectangle?(new Rectangle(0, 0, 1, 1)), separator);
		}

		// Token: 0x04005737 RID: 22327
		private Asset<Texture2D> _texOuterCrimson;

		// Token: 0x04005738 RID: 22328
		private Asset<Texture2D> _texOuterCorrupt;

		// Token: 0x04005739 RID: 22329
		private Asset<Texture2D> _texOuterLower;

		// Token: 0x0400573A RID: 22330
		private float _visualOverallProgress;

		// Token: 0x0400573B RID: 22331
		private float _targetOverallProgress;

		// Token: 0x0400573C RID: 22332
		private float _visualCurrentProgress;

		// Token: 0x0400573D RID: 22333
		private float _targetCurrentProgress;

		// Token: 0x0400573E RID: 22334
		private int _smallBarWidth = 508;

		// Token: 0x0400573F RID: 22335
		private int _longBarWidth = 570;
	}
}
