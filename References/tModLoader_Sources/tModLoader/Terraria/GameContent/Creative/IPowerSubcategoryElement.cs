using System;
using Terraria.GameContent.UI.Elements;

namespace Terraria.GameContent.Creative
{
	// Token: 0x0200064A RID: 1610
	public interface IPowerSubcategoryElement
	{
		// Token: 0x0600469A RID: 18074
		GroupOptionButton<int> GetOptionButton(CreativePowerUIElementRequestInfo info, int optionIndex, int currentOptionIndex);
	}
}
