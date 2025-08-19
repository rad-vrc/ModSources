using System;
using System.IO;

namespace Terraria.GameContent
{
	// Token: 0x020001DF RID: 479
	public interface IPersistentPerPlayerContent
	{
		// Token: 0x06001C3F RID: 7231
		void Save(Player player, BinaryWriter writer);

		// Token: 0x06001C40 RID: 7232
		void Load(Player player, BinaryReader reader, int gameVersionSaveWasMadeOn);

		// Token: 0x06001C41 RID: 7233
		void ApplyLoadedDataToOutOfPlayerFields(Player player);

		// Token: 0x06001C42 RID: 7234
		void ResetDataForNewPlayer(Player player);

		// Token: 0x06001C43 RID: 7235
		void Reset();
	}
}
