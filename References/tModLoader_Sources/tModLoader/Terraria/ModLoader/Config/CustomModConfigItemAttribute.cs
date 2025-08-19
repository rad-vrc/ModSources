using System;

namespace Terraria.ModLoader.Config
{
	/// <summary>
	/// Use this attribute to specify a custom UI element to be used for the annotated property, field, or class in the ModConfig UI.
	/// </summary>
	// Token: 0x02000376 RID: 886
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Property | AttributeTargets.Field)]
	public class CustomModConfigItemAttribute : Attribute
	{
		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x0600309B RID: 12443 RVA: 0x0053D043 File Offset: 0x0053B243
		public Type Type { get; }

		// Token: 0x0600309C RID: 12444 RVA: 0x0053D04B File Offset: 0x0053B24B
		public CustomModConfigItemAttribute(Type type)
		{
			this.Type = type;
		}
	}
}
