using DragonLens.Helpers;

namespace DragonLens.Core.Systems.ThemeSystem
{
	internal class ThemeColorProvider
	{
		public Color backgroundColor = new(49, 84, 141);
		public Color buttonColor = new(49, 84, 141);

		public Color SafeOutlineColor => buttonColor.A == 0 ? new Color(255, 255, 255, 255) : buttonColor.InvertColor();
	}
}
