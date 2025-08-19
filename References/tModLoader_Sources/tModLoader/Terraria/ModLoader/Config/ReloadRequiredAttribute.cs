using System;

namespace Terraria.ModLoader.Config
{
	/// <summary>
	/// This attribute hints that changing the value of the annotated property or field will put the config in a state that requires a reload. An overridden ModConfig.NeedsReload can further validate if more complex logic is needed.
	/// </summary>
	// Token: 0x0200036B RID: 875
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class ReloadRequiredAttribute : Attribute
	{
	}
}
