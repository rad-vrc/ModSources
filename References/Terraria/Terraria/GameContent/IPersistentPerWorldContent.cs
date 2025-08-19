using System;
using System.IO;

namespace Terraria.GameContent
{
	// Token: 0x020001DE RID: 478
	public interface IPersistentPerWorldContent
	{
		// Token: 0x06001C3B RID: 7227
		void Save(BinaryWriter writer);

		// Token: 0x06001C3C RID: 7228
		void Load(BinaryReader reader, int gameVersionSaveWasMadeOn);

		// Token: 0x06001C3D RID: 7229
		void ValidateWorld(BinaryReader reader, int gameVersionSaveWasMadeOn);

		// Token: 0x06001C3E RID: 7230
		void Reset();
	}
}
