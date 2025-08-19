using System;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.GameContent
{
	// Token: 0x020004A3 RID: 1187
	public interface ITownNPCProfile
	{
		// Token: 0x06003951 RID: 14673
		int RollVariation();

		// Token: 0x06003952 RID: 14674
		string GetNameForVariant(NPC npc);

		// Token: 0x06003953 RID: 14675
		Asset<Texture2D> GetTextureNPCShouldUse(NPC npc);

		// Token: 0x06003954 RID: 14676
		int GetHeadTextureIndex(NPC npc);
	}
}
