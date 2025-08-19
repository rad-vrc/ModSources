using System;
using Microsoft.Xna.Framework;

namespace Terraria.Graphics.Light
{
	// Token: 0x02000119 RID: 281
	public interface ILightingEngine
	{
		// Token: 0x060016F4 RID: 5876
		void Rebuild();

		// Token: 0x060016F5 RID: 5877
		void AddLight(int x, int y, Vector3 color);

		// Token: 0x060016F6 RID: 5878
		void ProcessArea(Rectangle area);

		// Token: 0x060016F7 RID: 5879
		Vector3 GetColor(int x, int y);

		// Token: 0x060016F8 RID: 5880
		void Clear();
	}
}
