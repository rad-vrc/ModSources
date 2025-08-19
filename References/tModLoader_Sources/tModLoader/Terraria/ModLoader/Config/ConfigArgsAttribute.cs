using System;

namespace Terraria.ModLoader.Config
{
	// Token: 0x0200036D RID: 877
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public abstract class ConfigArgsAttribute : Attribute
	{
		// Token: 0x0600308C RID: 12428 RVA: 0x0053CECA File Offset: 0x0053B0CA
		public ConfigArgsAttribute(params object[] args)
		{
			this.args = args;
		}

		// Token: 0x04001D18 RID: 7448
		internal readonly object[] args;
	}
}
