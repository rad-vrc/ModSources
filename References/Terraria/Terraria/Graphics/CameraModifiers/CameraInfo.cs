using System;
using Microsoft.Xna.Framework;

namespace Terraria.Graphics.CameraModifiers
{
	// Token: 0x02000132 RID: 306
	public struct CameraInfo
	{
		// Token: 0x060017A9 RID: 6057 RVA: 0x004D503F File Offset: 0x004D323F
		public CameraInfo(Vector2 position)
		{
			this.OriginalCameraPosition = position;
			this.OriginalCameraCenter = position + Main.ScreenSize.ToVector2() / 2f;
			this.CameraPosition = this.OriginalCameraPosition;
		}

		// Token: 0x04001455 RID: 5205
		public Vector2 CameraPosition;

		// Token: 0x04001456 RID: 5206
		public Vector2 OriginalCameraCenter;

		// Token: 0x04001457 RID: 5207
		public Vector2 OriginalCameraPosition;
	}
}
