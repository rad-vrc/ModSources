using System;
using Microsoft.Xna.Framework;

namespace Terraria.Graphics.Light
{
	// Token: 0x0200045F RID: 1119
	public interface ILightingEngine
	{
		// Token: 0x060036C1 RID: 14017
		void Rebuild();

		// Token: 0x060036C2 RID: 14018
		void AddLight(int x, int y, Vector3 color);

		// Token: 0x060036C3 RID: 14019
		void ProcessArea(Rectangle area);

		// Token: 0x060036C4 RID: 14020
		Vector3 GetColor(int x, int y);

		// Token: 0x060036C5 RID: 14021
		void Clear();
	}
}
