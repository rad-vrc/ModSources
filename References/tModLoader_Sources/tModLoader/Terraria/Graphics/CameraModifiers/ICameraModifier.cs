using System;

namespace Terraria.Graphics.CameraModifiers
{
	// Token: 0x0200047D RID: 1149
	public interface ICameraModifier
	{
		/// <summary>
		/// Will cause any other active camera modifiers with the same value to be cleared when this camera modifier is added
		/// </summary>
		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x0600379A RID: 14234
		string UniqueIdentity { get; }

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x0600379B RID: 14235
		bool Finished { get; }

		// Token: 0x0600379C RID: 14236
		void Update(ref CameraInfo cameraPosition);
	}
}
