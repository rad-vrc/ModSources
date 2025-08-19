using System;

namespace Terraria.ModLoader.Config
{
	/// <summary>
	/// Similar to DefaultListValueAttribute but for reference types. It uses a json string that will be used populate new instances list elements. Defines the default value, expressed as json, to be added when using the ModConfig UI to add elements to a Collection (List, Set, or Dictionary value).
	/// </summary>
	// Token: 0x0200037A RID: 890
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class JsonDefaultListValueAttribute : Attribute
	{
		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x060030BD RID: 12477 RVA: 0x0053D393 File Offset: 0x0053B593
		public string Json { get; }

		// Token: 0x060030BE RID: 12478 RVA: 0x0053D39B File Offset: 0x0053B59B
		public JsonDefaultListValueAttribute(string json)
		{
			this.Json = json;
		}
	}
}
