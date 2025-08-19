using System;
using System.IO;

namespace Terraria.ModLoader.Default
{
	// Token: 0x020002C9 RID: 713
	internal abstract class NetHandler
	{
		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06002DAD RID: 11693 RVA: 0x0052F92D File Offset: 0x0052DB2D
		// (set) Token: 0x06002DAE RID: 11694 RVA: 0x0052F935 File Offset: 0x0052DB35
		internal byte HandlerType { get; set; }

		// Token: 0x06002DAF RID: 11695
		public abstract void HandlePacket(BinaryReader r, int fromWho);

		// Token: 0x06002DB0 RID: 11696 RVA: 0x0052F93E File Offset: 0x0052DB3E
		protected NetHandler(byte handlerType)
		{
			this.HandlerType = handlerType;
		}

		// Token: 0x06002DB1 RID: 11697 RVA: 0x0052F950 File Offset: 0x0052DB50
		protected ModPacket GetPacket(byte packetType, int fromWho)
		{
			ModPacket p = ModContent.GetInstance<ModLoaderMod>().GetPacket(256);
			p.Write(this.HandlerType);
			p.Write(packetType);
			if (Main.netMode == 2)
			{
				p.Write((byte)fromWho);
			}
			return p;
		}
	}
}
