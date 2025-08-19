using System;
using Microsoft.Xna.Framework;

namespace Terraria.DataStructures
{
	// Token: 0x020006D9 RID: 1753
	public struct ContainerTransferContext
	{
		// Token: 0x0600491D RID: 18717 RVA: 0x0064CAAD File Offset: 0x0064ACAD
		public static ContainerTransferContext FromProjectile(Projectile projectile)
		{
			return new ContainerTransferContext(projectile.Center);
		}

		// Token: 0x0600491E RID: 18718 RVA: 0x0064CABA File Offset: 0x0064ACBA
		public static ContainerTransferContext FromBlockPosition(int x, int y)
		{
			return new ContainerTransferContext(new Vector2((float)(x * 16 + 16), (float)(y * 16 + 16)));
		}

		// Token: 0x0600491F RID: 18719 RVA: 0x0064CAD8 File Offset: 0x0064ACD8
		public static ContainerTransferContext FromUnknown(Player player)
		{
			return new ContainerTransferContext
			{
				CanVisualizeTransfers = false
			};
		}

		// Token: 0x06004920 RID: 18720 RVA: 0x0064CAF6 File Offset: 0x0064ACF6
		public ContainerTransferContext(Vector2 position)
		{
			this._position = position;
			this.CanVisualizeTransfers = true;
		}

		// Token: 0x06004921 RID: 18721 RVA: 0x0064CB06 File Offset: 0x0064AD06
		public Vector2 GetContainerWorldPosition()
		{
			return this._position;
		}

		// Token: 0x04005E85 RID: 24197
		private Vector2 _position;

		// Token: 0x04005E86 RID: 24198
		public bool CanVisualizeTransfers;
	}
}
