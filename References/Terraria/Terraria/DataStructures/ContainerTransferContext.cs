using System;
using Microsoft.Xna.Framework;

namespace Terraria.DataStructures
{
	// Token: 0x020003FC RID: 1020
	public struct ContainerTransferContext
	{
		// Token: 0x06002AEA RID: 10986 RVA: 0x0059D2F1 File Offset: 0x0059B4F1
		public static ContainerTransferContext FromProjectile(Projectile projectile)
		{
			return new ContainerTransferContext(projectile.Center);
		}

		// Token: 0x06002AEB RID: 10987 RVA: 0x0059D2FE File Offset: 0x0059B4FE
		public static ContainerTransferContext FromBlockPosition(int x, int y)
		{
			return new ContainerTransferContext(new Vector2((float)(x * 16 + 16), (float)(y * 16 + 16)));
		}

		// Token: 0x06002AEC RID: 10988 RVA: 0x0059D31C File Offset: 0x0059B51C
		public static ContainerTransferContext FromUnknown(Player player)
		{
			return new ContainerTransferContext
			{
				CanVisualizeTransfers = false
			};
		}

		// Token: 0x06002AED RID: 10989 RVA: 0x0059D33A File Offset: 0x0059B53A
		public ContainerTransferContext(Vector2 position)
		{
			this._position = position;
			this.CanVisualizeTransfers = true;
		}

		// Token: 0x06002AEE RID: 10990 RVA: 0x0059D34A File Offset: 0x0059B54A
		public Vector2 GetContainerWorldPosition()
		{
			return this._position;
		}

		// Token: 0x04004F32 RID: 20274
		private Vector2 _position;

		// Token: 0x04004F33 RID: 20275
		public bool CanVisualizeTransfers;
	}
}
