using System;

namespace Terraria.ModLoader.Config
{
	/// <summary>
	/// Affects whether this data will be presented as a slider or an input field. Add this attribute to use a slider. Currently only affects data of type int.
	/// </summary>
	// Token: 0x0200037F RID: 895
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class SliderAttribute : Attribute
	{
	}
}
