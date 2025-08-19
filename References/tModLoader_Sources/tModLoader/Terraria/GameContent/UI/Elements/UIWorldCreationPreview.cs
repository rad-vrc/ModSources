using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x0200053B RID: 1339
	public class UIWorldCreationPreview : UIElement
	{
		// Token: 0x06003FBE RID: 16318 RVA: 0x005DB580 File Offset: 0x005D9780
		public UIWorldCreationPreview()
		{
			this._BorderTexture = Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewBorder");
			this._BackgroundNormalTexture = Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewDifficultyNormal1");
			this._BackgroundExpertTexture = Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewDifficultyExpert1");
			this._BackgroundMasterTexture = Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewDifficultyMaster1");
			this._BunnyNormalTexture = Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewDifficultyNormal2");
			this._BunnyExpertTexture = Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewDifficultyExpert2");
			this._BunnyCreativeTexture = Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewDifficultyCreative2");
			this._BunnyMasterTexture = Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewDifficultyMaster2");
			this._EvilRandomTexture = Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewEvilRandom");
			this._EvilCorruptionTexture = Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewEvilCorruption");
			this._EvilCrimsonTexture = Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewEvilCrimson");
			this._SizeSmallTexture = Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewSizeSmall");
			this._SizeMediumTexture = Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewSizeMedium");
			this._SizeLargeTexture = Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewSizeLarge");
			this.Width.Set((float)this._BackgroundExpertTexture.Width(), 0f);
			this.Height.Set((float)this._BackgroundExpertTexture.Height(), 0f);
		}

		// Token: 0x06003FBF RID: 16319 RVA: 0x005DB6F1 File Offset: 0x005D98F1
		public void UpdateOption(byte difficulty, byte evil, byte size)
		{
			this._difficulty = difficulty;
			this._evil = evil;
			this._size = size;
		}

		// Token: 0x06003FC0 RID: 16320 RVA: 0x005DB708 File Offset: 0x005D9908
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			Vector2 position;
			position..ctor(dimensions.X + 4f, dimensions.Y + 4f);
			Color color = Color.White;
			switch (this._difficulty)
			{
			case 0:
			case 3:
				spriteBatch.Draw(this._BackgroundNormalTexture.Value, position, Color.White);
				color = Color.White;
				break;
			case 1:
				spriteBatch.Draw(this._BackgroundExpertTexture.Value, position, Color.White);
				color = Color.DarkGray;
				break;
			case 2:
				spriteBatch.Draw(this._BackgroundMasterTexture.Value, position, Color.White);
				color = Color.DarkGray;
				break;
			}
			switch (this._size)
			{
			case 0:
				spriteBatch.Draw(this._SizeSmallTexture.Value, position, color);
				break;
			case 1:
				spriteBatch.Draw(this._SizeMediumTexture.Value, position, color);
				break;
			case 2:
				spriteBatch.Draw(this._SizeLargeTexture.Value, position, color);
				break;
			}
			switch (this._evil)
			{
			case 0:
				spriteBatch.Draw(this._EvilRandomTexture.Value, position, color);
				break;
			case 1:
				spriteBatch.Draw(this._EvilCorruptionTexture.Value, position, color);
				break;
			case 2:
				spriteBatch.Draw(this._EvilCrimsonTexture.Value, position, color);
				break;
			}
			switch (this._difficulty)
			{
			case 0:
				spriteBatch.Draw(this._BunnyNormalTexture.Value, position, color);
				break;
			case 1:
				spriteBatch.Draw(this._BunnyExpertTexture.Value, position, color);
				break;
			case 2:
				spriteBatch.Draw(this._BunnyMasterTexture.Value, position, color * 1.2f);
				break;
			case 3:
				spriteBatch.Draw(this._BunnyCreativeTexture.Value, position, color);
				break;
			}
			spriteBatch.Draw(this._BorderTexture.Value, new Vector2(dimensions.X, dimensions.Y), Color.White);
		}

		// Token: 0x0400580D RID: 22541
		private readonly Asset<Texture2D> _BorderTexture;

		// Token: 0x0400580E RID: 22542
		private readonly Asset<Texture2D> _BackgroundExpertTexture;

		// Token: 0x0400580F RID: 22543
		private readonly Asset<Texture2D> _BackgroundNormalTexture;

		// Token: 0x04005810 RID: 22544
		private readonly Asset<Texture2D> _BackgroundMasterTexture;

		// Token: 0x04005811 RID: 22545
		private readonly Asset<Texture2D> _BunnyExpertTexture;

		// Token: 0x04005812 RID: 22546
		private readonly Asset<Texture2D> _BunnyNormalTexture;

		// Token: 0x04005813 RID: 22547
		private readonly Asset<Texture2D> _BunnyCreativeTexture;

		// Token: 0x04005814 RID: 22548
		private readonly Asset<Texture2D> _BunnyMasterTexture;

		// Token: 0x04005815 RID: 22549
		private readonly Asset<Texture2D> _EvilRandomTexture;

		// Token: 0x04005816 RID: 22550
		private readonly Asset<Texture2D> _EvilCorruptionTexture;

		// Token: 0x04005817 RID: 22551
		private readonly Asset<Texture2D> _EvilCrimsonTexture;

		// Token: 0x04005818 RID: 22552
		private readonly Asset<Texture2D> _SizeSmallTexture;

		// Token: 0x04005819 RID: 22553
		private readonly Asset<Texture2D> _SizeMediumTexture;

		// Token: 0x0400581A RID: 22554
		private readonly Asset<Texture2D> _SizeLargeTexture;

		// Token: 0x0400581B RID: 22555
		private byte _difficulty;

		// Token: 0x0400581C RID: 22556
		private byte _evil;

		// Token: 0x0400581D RID: 22557
		private byte _size;
	}
}
