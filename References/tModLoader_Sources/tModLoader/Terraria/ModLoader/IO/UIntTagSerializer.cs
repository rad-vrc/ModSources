using System;

namespace Terraria.ModLoader.IO
{
	// Token: 0x0200028F RID: 655
	public class UIntTagSerializer : TagSerializer<uint, int>
	{
		// Token: 0x06002C69 RID: 11369 RVA: 0x0052783E File Offset: 0x00525A3E
		public override int Serialize(uint value)
		{
			return (int)value;
		}

		// Token: 0x06002C6A RID: 11370 RVA: 0x00527841 File Offset: 0x00525A41
		public override uint Deserialize(int tag)
		{
			return (uint)tag;
		}
	}
}
