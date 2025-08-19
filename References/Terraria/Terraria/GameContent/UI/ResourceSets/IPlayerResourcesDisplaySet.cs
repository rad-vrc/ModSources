using System;
using Terraria.DataStructures;

namespace Terraria.GameContent.UI.ResourceSets
{
	// Token: 0x020003BE RID: 958
	public interface IPlayerResourcesDisplaySet : IConfigKeyHolder
	{
		// Token: 0x06002A4B RID: 10827
		void Draw();

		// Token: 0x06002A4C RID: 10828
		void TryToHover();
	}
}
