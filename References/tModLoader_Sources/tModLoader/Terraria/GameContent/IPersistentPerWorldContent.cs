using System;
using System.IO;

namespace Terraria.GameContent
{
	// Token: 0x020004A0 RID: 1184
	public interface IPersistentPerWorldContent
	{
		// Token: 0x06003940 RID: 14656
		void Save(BinaryWriter writer);

		// Token: 0x06003941 RID: 14657
		void Load(BinaryReader reader, int gameVersionSaveWasMadeOn);

		// Token: 0x06003942 RID: 14658
		void ValidateWorld(BinaryReader reader, int gameVersionSaveWasMadeOn);

		// Token: 0x06003943 RID: 14659
		void Reset();
	}
}
