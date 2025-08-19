using System;

namespace Terraria.Graphics.CameraModifiers
{
	// Token: 0x02000131 RID: 305
	public interface ICameraModifier
	{
		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060017A6 RID: 6054
		string UniqueIdentity { get; }

		// Token: 0x060017A7 RID: 6055
		void Update(ref CameraInfo cameraPosition);

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060017A8 RID: 6056
		bool Finished { get; }
	}
}
