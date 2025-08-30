using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using DragonLens.Helpers;

namespace DragonLens.Content.GUI
{
	internal class CloseButton : UIImageButton
	{
		public CloseButton() : base(Assets.GUI.Remove) { }

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);

			// フォールバック: 枠線を必ず描画して視認性を確保
			var rect = GetDimensions().ToRectangle();
			GUIHelper.DrawOutline(spriteBatch, rect, Color.White);
		}
	}
}

