using System;

namespace Terraria.ModLoader.IO
{
	// Token: 0x0200028E RID: 654
	public class UShortTagSerializer : TagSerializer<ushort, short>
	{
		// Token: 0x06002C66 RID: 11366 RVA: 0x0052782E File Offset: 0x00525A2E
		public override short Serialize(ushort value)
		{
			return (short)value;
		}

		// Token: 0x06002C67 RID: 11367 RVA: 0x00527832 File Offset: 0x00525A32
		public override ushort Deserialize(short tag)
		{
			return (ushort)tag;
		}
	}
}
