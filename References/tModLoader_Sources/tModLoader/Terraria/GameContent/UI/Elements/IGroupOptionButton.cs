using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020004FE RID: 1278
	public interface IGroupOptionButton
	{
		// Token: 0x06003DBA RID: 15802
		void SetColorsBasedOnSelectionState(Color pickedColor, Color unpickedColor, float opacityPicked, float opacityNotPicked);

		// Token: 0x06003DBB RID: 15803
		void SetBorderColor(Color color);
	}
}
