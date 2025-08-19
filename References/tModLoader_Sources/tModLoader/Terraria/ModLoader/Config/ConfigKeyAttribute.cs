using System;

namespace Terraria.ModLoader.Config
{
	// Token: 0x0200036C RID: 876
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field)]
	public abstract class ConfigKeyAttribute : Attribute
	{
		// Token: 0x0600308B RID: 12427 RVA: 0x0053CE99 File Offset: 0x0053B099
		public ConfigKeyAttribute(string key)
		{
			if (!key.StartsWith("$"))
			{
				this.malformed = true;
				this.key = key;
				return;
			}
			this.key = key.Substring(1);
		}

		// Token: 0x04001D16 RID: 7446
		internal readonly string key;

		// Token: 0x04001D17 RID: 7447
		internal readonly bool malformed;
	}
}
