using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000371 RID: 881
	public class UIWorldCreationPreview : UIElement
	{
		// Token: 0x06002859 RID: 10329 RVA: 0x00589FC8 File Offset: 0x005881C8
		public UIWorldCreationPreview()
		{
			this._BorderTexture = Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewBorder", 1);
			this._BackgroundNormalTexture = Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewDifficultyNormal1", 1);
			this._BackgroundExpertTexture = Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewDifficultyExpert1", 1);
			this._BackgroundMasterTexture = Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewDifficultyMaster1", 1);
			this._BunnyNormalTexture = Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewDifficultyNormal2", 1);
			this._BunnyExpertTexture = Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewDifficultyExpert2", 1);
			this._BunnyCreativeTexture = Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewDifficultyCreative2", 1);
			this._BunnyMasterTexture = Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewDifficultyMaster2", 1);
			this._EvilRandomTexture = Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewEvilRandom", 1);
			this._EvilCorruptionTexture = Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewEvilCorruption", 1);
			this._EvilCrimsonTexture = Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewEvilCrimson", 1);
			this._SizeSmallTexture = Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewSizeSmall", 1);
			this._SizeMediumTexture = Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewSizeMedium", 1);
			this._SizeLargeTexture = Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewSizeLarge", 1);
			this.Width.Set((float)this._BackgroundExpertTexture.Width(), 0f);
			this.Height.Set((float)this._BackgroundExpertTexture.Height(), 0f);
		}

		// Token: 0x0600285A RID: 10330 RVA: 0x0058A147 File Offset: 0x00588347
		public void UpdateOption(byte difficulty, byte evil, byte size)
		{
			this._difficulty = difficulty;
			this._evil = evil;
			this._size = size;
		}

		// Token: 0x0600285B RID: 10331 RVA: 0x0058A160 File Offset: 0x00588360
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			Vector2 position = new Vector2(dimensions.X + 4f, dimensions.Y + 4f);
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

		// Token: 0x04004B72 RID: 19314
		private readonly Asset<Texture2D> _BorderTexture;

		// Token: 0x04004B73 RID: 19315
		private readonly Asset<Texture2D> _BackgroundExpertTexture;

		// Token: 0x04004B74 RID: 19316
		private readonly Asset<Texture2D> _BackgroundNormalTexture;

		// Token: 0x04004B75 RID: 19317
		private readonly Asset<Texture2D> _BackgroundMasterTexture;

		// Token: 0x04004B76 RID: 19318
		private readonly Asset<Texture2D> _BunnyExpertTexture;

		// Token: 0x04004B77 RID: 19319
		private readonly Asset<Texture2D> _BunnyNormalTexture;

		// Token: 0x04004B78 RID: 19320
		private readonly Asset<Texture2D> _BunnyCreativeTexture;

		// Token: 0x04004B79 RID: 19321
		private readonly Asset<Texture2D> _BunnyMasterTexture;

		// Token: 0x04004B7A RID: 19322
		private readonly Asset<Texture2D> _EvilRandomTexture;

		// Token: 0x04004B7B RID: 19323
		private readonly Asset<Texture2D> _EvilCorruptionTexture;

		// Token: 0x04004B7C RID: 19324
		private readonly Asset<Texture2D> _EvilCrimsonTexture;

		// Token: 0x04004B7D RID: 19325
		private readonly Asset<Texture2D> _SizeSmallTexture;

		// Token: 0x04004B7E RID: 19326
		private readonly Asset<Texture2D> _SizeMediumTexture;

		// Token: 0x04004B7F RID: 19327
		private readonly Asset<Texture2D> _SizeLargeTexture;

		// Token: 0x04004B80 RID: 19328
		private byte _difficulty;

		// Token: 0x04004B81 RID: 19329
		private byte _evil;

		// Token: 0x04004B82 RID: 19330
		private byte _size;
	}
}
