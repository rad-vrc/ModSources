using DragonLens.Core.Loaders.UILoading;
using DragonLens.Core.Systems.ThemeSystem;
using DragonLens.Helpers;
using System;

namespace DragonLens.Content.GUI
{
	/// <summary>
	/// A simple button used for on/off states
	/// </summary>
	internal class ToggleButton : SmartUIElement
	{
		/// <summary>
		/// The texture of the icon to draw on the button
		/// </summary>
		public string iconTexture;
		/// <summary>
		/// How the button should determine if it is 'on' or not. While on, it will draw a colored outline around itself.
		/// </summary>
		public Func<bool> isOn;
		/// <summary>
		/// What this button should say when hovered over
		/// </summary>
		public string tooltip;
		/// <summary>
		/// Custom sub tooltip getter if relevant
		/// </summary>
		public Func<string> getInfo;

		/// <summary>
		///
		/// </summary>
		/// <param name="iconTexture">The texture of the icon to draw on the button</param>
		/// <param name="isOn">How the button should determine if it is 'on' or not. While on, it will draw a colored outline around itself.</param>
		/// <param name="tooltip">What this button should say when hovered over</param>
		public ToggleButton(string iconTexture, Func<bool> isOn, string tooltip = "", Func<string> getInfo = null)
		{
			this.iconTexture = iconTexture;
			this.isOn = isOn;
			Width.Set(32, 0);
			Height.Set(32, 0);
			this.tooltip = tooltip;
			this.getInfo = getInfo;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			GUIHelper.DrawBox(spriteBatch, GetDimensions().ToRectangle(), ThemeHandler.ButtonColor);

			Texture2D tex = ModContent.Request<Texture2D>(iconTexture).Value;
			// ドロップシャドウで視認性アップ
			var center = GetDimensions().Center();
			var origin = tex.Size() / 2f;
			spriteBatch.Draw(tex, center + new Vector2(1, 1), null, Color.Black * 0.35f, 0, origin, 1f, 0, 0);
			spriteBatch.Draw(tex, center, null, Color.White, 0, origin, 1f, 0, 0);

			// 常時枠描画＋ON時は反転色で強調
			var rect = GetDimensions().ToRectangle();
			GUIHelper.DrawOutline(spriteBatch, rect, ThemeHandler.currentColorProvider.SafeOutlineColor);
			if (isOn())
				GUIHelper.DrawOutline(spriteBatch, rect, ThemeHandler.ButtonColor.InvertColor());

			if (IsMouseHovering && tooltip != "")
			{
				Tooltip.SetName(tooltip);
				Tooltip.SetTooltip(getInfo?.Invoke() ?? LocalizationHelper.GetGUIText($"ToggleButton.{(isOn() ? "On" : "Off")}"));
			}
		}
	}
}
