using System;

namespace Terraria.ModLoader.Config
{
	/// <summary>
	/// This specifies that the annotated item will appear as a button that leads to a separate page in the UI. Use this to organize hierarchies.
	/// </summary>
	// Token: 0x02000383 RID: 899
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field)]
	public class SeparatePageAttribute : Attribute
	{
	}
}
