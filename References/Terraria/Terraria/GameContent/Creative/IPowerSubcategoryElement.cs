using System;
using Terraria.GameContent.UI.Elements;

namespace Terraria.GameContent.Creative
{
	// Token: 0x020002C1 RID: 705
	public interface IPowerSubcategoryElement
	{
		// Token: 0x06002267 RID: 8807
		GroupOptionButton<int> GetOptionButton(CreativePowerUIElementRequestInfo info, int optionIndex, int currentOptionIndex);
	}
}
