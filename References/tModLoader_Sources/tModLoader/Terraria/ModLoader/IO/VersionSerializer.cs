using System;
using System.Runtime.CompilerServices;

namespace Terraria.ModLoader.IO
{
	// Token: 0x02000298 RID: 664
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1,
		1
	})]
	public class VersionSerializer : TagSerializer<Version, string>
	{
		// Token: 0x06002C84 RID: 11396 RVA: 0x00527ADD File Offset: 0x00525CDD
		public override string Serialize(Version value)
		{
			return value.ToString();
		}

		// Token: 0x06002C85 RID: 11397 RVA: 0x00527AE5 File Offset: 0x00525CE5
		public override Version Deserialize(string tag)
		{
			return new Version(tag);
		}
	}
}
