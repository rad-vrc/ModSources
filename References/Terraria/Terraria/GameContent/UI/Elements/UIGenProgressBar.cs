using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000396 RID: 918
	public class UIGenProgressBar : UIElement
	{
		// Token: 0x0600297E RID: 10622 RVA: 0x00593614 File Offset: 0x00591814
		public UIGenProgressBar()
		{
			if (Main.netMode != 2)
			{
				this._texOuterCorrupt = Main.Assets.Request<Texture2D>("Images/UI/WorldGen/Outer_Corrupt", 1);
				this._texOuterCrimson = Main.Assets.Request<Texture2D>("Images/UI/WorldGen/Outer_Crimson", 1);
				this._texOuterLower = Main.Assets.Request<Texture2D>("Images/UI/WorldGen/Outer_Lower", 1);
			}
			this.Recalculate();
		}

		// Token: 0x0600297F RID: 10623 RVA: 0x00593690 File Offset: 0x00591890
		public override void Recalculate()
		{
			this.Width.Precent = 0f;
			this.Height.Precent = 0f;
			this.Width.Pixels = 612f;
			this.Height.Pixels = 70f;
			base.Recalculate();
		}

		// Token: 0x06002980 RID: 10624 RVA: 0x005936E3 File Offset: 0x005918E3
		public void SetProgress(float overallProgress, float currentProgress)
		{
			this._targetCurrentProgress = currentProgress;
			this._targetOverallProgress = overallProgress;
		}

		// Token: 0x06002981 RID: 10625 RVA: 0x005936F4 File Offset: 0x005918F4
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (!this._texOuterCorrupt.IsLoaded || !this._texOuterCrimson.IsLoaded || !this._texOuterLower.IsLoaded)
			{
				return;
			}
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
			Vector2 value = new Vector2(dimensions.X, dimensions.Y);
			Color color = default(Color);
			color.PackedValue = (flag ? 4286836223U : 4283888223U);
			this.DrawFilling2(spriteBatch, value + new Vector2(20f, 40f), 16, completedWidth, this._longBarWidth, color, Color.Lerp(color, Color.Black, 0.5f), new Color(48, 48, 48));
			color.PackedValue = 4290947159U;
			this.DrawFilling2(spriteBatch, value + new Vector2(50f, 60f), 8, completedWidth2, this._smallBarWidth, color, Color.Lerp(color, Color.Black, 0.5f), new Color(33, 33, 33));
			Rectangle r = base.GetDimensions().ToRectangle();
			r.X -= 8;
			spriteBatch.Draw(flag ? this._texOuterCrimson.Value : this._texOuterCorrupt.Value, r.TopLeft(), Color.White);
			spriteBatch.Draw(this._texOuterLower.Value, r.TopLeft() + new Vector2(44f, 60f), Color.White);
		}

		// Token: 0x06002982 RID: 10626 RVA: 0x005938C8 File Offset: 0x00591AC8
		private void DrawFilling(SpriteBatch spritebatch, Texture2D tex, Texture2D texShadow, Vector2 topLeft, int completedWidth, int totalWidth, Color separator, Color empty)
		{
			if (completedWidth % 2 != 0)
			{
				completedWidth--;
			}
			Vector2 position = topLeft + (float)completedWidth * Vector2.UnitX;
			int i = completedWidth;
			Rectangle rectangle = tex.Frame(1, 1, 0, 0, 0, 0);
			while (i > 0)
			{
				if (rectangle.Width > i)
				{
					rectangle.X += rectangle.Width - i;
					rectangle.Width = i;
				}
				spritebatch.Draw(tex, position, new Rectangle?(rectangle), Color.White, 0f, new Vector2((float)rectangle.Width, 0f), 1f, SpriteEffects.None, 0f);
				position.X -= (float)rectangle.Width;
				i -= rectangle.Width;
			}
			if (texShadow != null)
			{
				spritebatch.Draw(texShadow, topLeft, new Rectangle?(new Rectangle(0, 0, completedWidth, texShadow.Height)), Color.White);
			}
			spritebatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle((int)topLeft.X + completedWidth, (int)topLeft.Y, totalWidth - completedWidth, tex.Height), new Rectangle?(new Rectangle(0, 0, 1, 1)), empty);
			spritebatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle((int)topLeft.X + completedWidth - 2, (int)topLeft.Y, 2, tex.Height), new Rectangle?(new Rectangle(0, 0, 1, 1)), separator);
		}

		// Token: 0x06002983 RID: 10627 RVA: 0x00593A28 File Offset: 0x00591C28
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

		// Token: 0x04004C8D RID: 19597
		private Asset<Texture2D> _texOuterCrimson;

		// Token: 0x04004C8E RID: 19598
		private Asset<Texture2D> _texOuterCorrupt;

		// Token: 0x04004C8F RID: 19599
		private Asset<Texture2D> _texOuterLower;

		// Token: 0x04004C90 RID: 19600
		private float _visualOverallProgress;

		// Token: 0x04004C91 RID: 19601
		private float _targetOverallProgress;

		// Token: 0x04004C92 RID: 19602
		private float _visualCurrentProgress;

		// Token: 0x04004C93 RID: 19603
		private float _targetCurrentProgress;

		// Token: 0x04004C94 RID: 19604
		private int _smallBarWidth = 508;

		// Token: 0x04004C95 RID: 19605
		private int _longBarWidth = 570;
	}
}
