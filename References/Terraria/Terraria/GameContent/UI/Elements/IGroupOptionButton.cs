using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000374 RID: 884
	public interface IGroupOptionButton
	{
		// Token: 0x06002866 RID: 10342
		void SetColorsBasedOnSelectionState(Color pickedColor, Color unpickedColor, float opacityPicked, float opacityNotPicked);

		// Token: 0x06002867 RID: 10343
		void SetBorderColor(Color color);
	}
}
