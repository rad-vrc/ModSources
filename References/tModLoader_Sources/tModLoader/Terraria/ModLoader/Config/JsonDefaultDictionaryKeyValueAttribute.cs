using System;

namespace Terraria.ModLoader.Config
{
	/// <summary>
	/// Similar to JsonDefaultListValueAttribute, but for assigning to the Dictionary Key rather than the Value.
	/// </summary>
	// Token: 0x0200037B RID: 891
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class JsonDefaultDictionaryKeyValueAttribute : Attribute
	{
		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x060030BF RID: 12479 RVA: 0x0053D3AA File Offset: 0x0053B5AA
		public string Json { get; }

		// Token: 0x060030C0 RID: 12480 RVA: 0x0053D3B2 File Offset: 0x0053B5B2
		public JsonDefaultDictionaryKeyValueAttribute(string json)
		{
			this.Json = json;
		}
	}
}
