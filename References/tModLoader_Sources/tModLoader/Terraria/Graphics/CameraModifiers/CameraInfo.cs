using System;
using Microsoft.Xna.Framework;

namespace Terraria.Graphics.CameraModifiers
{
	// Token: 0x0200047B RID: 1147
	public struct CameraInfo
	{
		// Token: 0x06003794 RID: 14228 RVA: 0x00589BAD File Offset: 0x00587DAD
		public CameraInfo(Vector2 position)
		{
			this.OriginalCameraPosition = position;
			this.OriginalCameraCenter = position + Main.ScreenSize.ToVector2() / 2f;
			this.CameraPosition = this.OriginalCameraPosition;
		}

		// Token: 0x04005146 RID: 20806
		public Vector2 CameraPosition;

		// Token: 0x04005147 RID: 20807
		public Vector2 OriginalCameraCenter;

		// Token: 0x04005148 RID: 20808
		public Vector2 OriginalCameraPosition;
	}
}
