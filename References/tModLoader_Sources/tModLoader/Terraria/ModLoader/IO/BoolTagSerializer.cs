using System;

namespace Terraria.ModLoader.IO
{
	// Token: 0x02000291 RID: 657
	public class BoolTagSerializer : TagSerializer<bool, byte>
	{
		// Token: 0x06002C6F RID: 11375 RVA: 0x0052785A File Offset: 0x00525A5A
		public override byte Serialize(bool value)
		{
			return (value > false) ? 1 : 0;
		}

		// Token: 0x06002C70 RID: 11376 RVA: 0x00527861 File Offset: 0x00525A61
		public override bool Deserialize(byte tag)
		{
			return tag > 0;
		}
	}
}
