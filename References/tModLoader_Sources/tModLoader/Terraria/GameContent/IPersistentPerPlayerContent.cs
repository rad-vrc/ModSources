using System;
using System.IO;

namespace Terraria.GameContent
{
	// Token: 0x0200049F RID: 1183
	public interface IPersistentPerPlayerContent
	{
		// Token: 0x0600393B RID: 14651
		void Save(Player player, BinaryWriter writer);

		// Token: 0x0600393C RID: 14652
		void Load(Player player, BinaryReader reader, int gameVersionSaveWasMadeOn);

		// Token: 0x0600393D RID: 14653
		void ApplyLoadedDataToOutOfPlayerFields(Player player);

		// Token: 0x0600393E RID: 14654
		void ResetDataForNewPlayer(Player player);

		// Token: 0x0600393F RID: 14655
		void Reset();
	}
}
