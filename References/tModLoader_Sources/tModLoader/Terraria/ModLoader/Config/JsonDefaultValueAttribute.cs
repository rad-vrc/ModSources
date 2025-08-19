using System;

namespace Terraria.ModLoader.Config
{
	/// <summary>
	/// Similar to DefaultValueAttribute but for reference types. It uses a json string that will be used populate this element when initialized. Defines the default value, expressed as json, to be used to populate an object with the NullAllowed attribute. Modders should only use this in conjunction with NullAllowed, as simply initializing the field with a default value is preferred.
	/// </summary>
	// Token: 0x02000377 RID: 887
	public class JsonDefaultValueAttribute : Attribute
	{
		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x0600309D RID: 12445 RVA: 0x0053D05A File Offset: 0x0053B25A
		public string Json { get; }

		// Token: 0x0600309E RID: 12446 RVA: 0x0053D062 File Offset: 0x0053B262
		public JsonDefaultValueAttribute(string json)
		{
			this.Json = json;
		}
	}
}
