using System;
using Terraria.DataStructures;

namespace Terraria.GameContent.UI.ResourceSets
{
	// Token: 0x020004F0 RID: 1264
	public interface IPlayerResourcesDisplaySet : IConfigKeyHolder
	{
		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06003D4F RID: 15695
		string DisplayedName { get; }

		// Token: 0x06003D50 RID: 15696
		void Draw();

		// Token: 0x06003D51 RID: 15697
		void TryToHover();
	}
}
