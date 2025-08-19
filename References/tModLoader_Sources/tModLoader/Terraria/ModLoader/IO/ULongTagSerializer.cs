using System;

namespace Terraria.ModLoader.IO
{
	// Token: 0x02000290 RID: 656
	public class ULongTagSerializer : TagSerializer<ulong, long>
	{
		// Token: 0x06002C6C RID: 11372 RVA: 0x0052784C File Offset: 0x00525A4C
		public override long Serialize(ulong value)
		{
			return (long)value;
		}

		// Token: 0x06002C6D RID: 11373 RVA: 0x0052784F File Offset: 0x00525A4F
		public override ulong Deserialize(long tag)
		{
			return (ulong)tag;
		}
	}
}
