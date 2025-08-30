using DragonLens.Core.Systems.ThemeSystem;
using System;

namespace DragonLens.Content.Themes.BoxProviders
{
	internal class SimpleBoxes : ThemeBoxProvider
	{
		public override string NameKey => "Simple";

		public override void DrawBox(SpriteBatch spriteBatch, Rectangle target, Color color)
		{
			Texture2D tex = Assets.Themes.BoxProviders.SimpleBoxes.Box.Value;

			// フォールバック: テクスチャ未生成（MagicPixel）の場合は単純塗り + 1px 枠で区画を可視化
			if (tex == Terraria.GameContent.TextureAssets.MagicPixel.Value)
			{
				Color fill = color == default ? ThemeHandler.ButtonColor * 0.6f : color;
				spriteBatch.Draw(Terraria.GameContent.TextureAssets.MagicPixel.Value, target, fill);
				var edge = Terraria.GameContent.TextureAssets.MagicPixel.Value;
				Color outline = new Color(255, 255, 255, 160);
				// top/bottom/left/right
				spriteBatch.Draw(edge, new Rectangle(target.X, target.Y, target.Width, 1), outline);
				spriteBatch.Draw(edge, new Rectangle(target.X, target.Bottom - 1, target.Width, 1), outline);
				spriteBatch.Draw(edge, new Rectangle(target.X, target.Y, 1, target.Height), outline);
				spriteBatch.Draw(edge, new Rectangle(target.Right - 1, target.Y, 1, target.Height), outline);
				return;
			}

			if (color == default)
				color = new Color(49, 84, 141) * 0.9f;

			var sourceCorner = new Rectangle(0, 0, 6, 6);
			var sourceEdge = new Rectangle(6, 0, 4, 6);
			var sourceCenter = new Rectangle(6, 6, 4, 4);

			Rectangle inner = target;
			inner.Inflate(-6, -6);

			spriteBatch.Draw(tex, inner, sourceCenter, color);

			spriteBatch.Draw(tex, new Rectangle(target.X + 6, target.Y, target.Width - 12, 6), sourceEdge, color, 0, Vector2.Zero, 0, 0);
			spriteBatch.Draw(tex, new Rectangle(target.X, target.Y - 6 + target.Height, target.Height - 12, 6), sourceEdge, color, -(float)Math.PI * 0.5f, Vector2.Zero, 0, 0);
			spriteBatch.Draw(tex, new Rectangle(target.X - 6 + target.Width, target.Y + target.Height, target.Width - 12, 6), sourceEdge, color, (float)Math.PI, Vector2.Zero, 0, 0);
			spriteBatch.Draw(tex, new Rectangle(target.X + target.Width, target.Y + 6, target.Height - 12, 6), sourceEdge, color, (float)Math.PI * 0.5f, Vector2.Zero, 0, 0);

			spriteBatch.Draw(tex, new Rectangle(target.X, target.Y, 6, 6), sourceCorner, color, 0, Vector2.Zero, 0, 0);
			spriteBatch.Draw(tex, new Rectangle(target.X + target.Width, target.Y, 6, 6), sourceCorner, color, (float)Math.PI * 0.5f, Vector2.Zero, 0, 0);
			spriteBatch.Draw(tex, new Rectangle(target.X + target.Width, target.Y + target.Height, 6, 6), sourceCorner, color, (float)Math.PI, Vector2.Zero, 0, 0);
			spriteBatch.Draw(tex, new Rectangle(target.X, target.Y + target.Height, 6, 6), sourceCorner, color, (float)Math.PI * 1.5f, Vector2.Zero, 0, 0);
		}

		public override void DrawBoxFancy(SpriteBatch spriteBatch, Rectangle target, Color color)
		{
			DrawBox(spriteBatch, target, color);
		}

		public override void DrawBoxSmall(SpriteBatch spriteBatch, Rectangle target, Color color)
		{
			DrawBox(spriteBatch, target, color);
		}

		public override void DrawOutline(SpriteBatch spriteBatch, Rectangle target, Color color)
		{
			Texture2D tex = Assets.Themes.BoxProviders.SimpleBoxes.Box.Value;

			if (color == default)
				color = new Color(49, 84, 141) * 0.9f;

			var sourceCorner = new Rectangle(0, 0, 6, 6);
			var sourceEdge = new Rectangle(6, 0, 4, 6);
			var sourceCenter = new Rectangle(6, 6, 4, 4);

			spriteBatch.Draw(tex, new Rectangle(target.X + 6, target.Y, target.Width - 12, 6), sourceEdge, color, 0, Vector2.Zero, 0, 0);
			spriteBatch.Draw(tex, new Rectangle(target.X, target.Y - 6 + target.Height, target.Height - 12, 6), sourceEdge, color, -(float)Math.PI * 0.5f, Vector2.Zero, 0, 0);
			spriteBatch.Draw(tex, new Rectangle(target.X - 6 + target.Width, target.Y + target.Height, target.Width - 12, 6), sourceEdge, color, (float)Math.PI, Vector2.Zero, 0, 0);
			spriteBatch.Draw(tex, new Rectangle(target.X + target.Width, target.Y + 6, target.Height - 12, 6), sourceEdge, color, (float)Math.PI * 0.5f, Vector2.Zero, 0, 0);

			spriteBatch.Draw(tex, new Rectangle(target.X, target.Y, 6, 6), sourceCorner, color, 0, Vector2.Zero, 0, 0);
			spriteBatch.Draw(tex, new Rectangle(target.X + target.Width, target.Y, 6, 6), sourceCorner, color, (float)Math.PI * 0.5f, Vector2.Zero, 0, 0);
			spriteBatch.Draw(tex, new Rectangle(target.X + target.Width, target.Y + target.Height, 6, 6), sourceCorner, color, (float)Math.PI, Vector2.Zero, 0, 0);
			spriteBatch.Draw(tex, new Rectangle(target.X, target.Y + target.Height, 6, 6), sourceCorner, color, (float)Math.PI * 1.5f, Vector2.Zero, 0, 0);
		}
	}
}
