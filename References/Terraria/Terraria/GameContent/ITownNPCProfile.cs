using System;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.GameContent
{
	// Token: 0x020001EF RID: 495
	public interface ITownNPCProfile
	{
		// Token: 0x06001CD9 RID: 7385
		int RollVariation();

		// Token: 0x06001CDA RID: 7386
		string GetNameForVariant(NPC npc);

		// Token: 0x06001CDB RID: 7387
		Asset<Texture2D> GetTextureNPCShouldUse(NPC npc);

		// Token: 0x06001CDC RID: 7388
		int GetHeadTextureIndex(NPC npc);
	}
}
