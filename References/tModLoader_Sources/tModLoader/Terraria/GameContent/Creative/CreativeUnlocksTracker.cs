using System;
using System.IO;

namespace Terraria.GameContent.Creative
{
	// Token: 0x02000646 RID: 1606
	public class CreativeUnlocksTracker : IPersistentPerWorldContent, IOnPlayerJoining
	{
		// Token: 0x06004689 RID: 18057 RVA: 0x006322FD File Offset: 0x006304FD
		public void Save(BinaryWriter writer)
		{
			this.ItemSacrifices.Save(writer);
		}

		// Token: 0x0600468A RID: 18058 RVA: 0x0063230B File Offset: 0x0063050B
		public void Load(BinaryReader reader, int gameVersionSaveWasMadeOn)
		{
			this.ItemSacrifices.Load(reader, gameVersionSaveWasMadeOn);
		}

		// Token: 0x0600468B RID: 18059 RVA: 0x0063231A File Offset: 0x0063051A
		public void ValidateWorld(BinaryReader reader, int gameVersionSaveWasMadeOn)
		{
			this.ValidateWorld(reader, gameVersionSaveWasMadeOn);
		}

		// Token: 0x0600468C RID: 18060 RVA: 0x00632324 File Offset: 0x00630524
		public void Reset()
		{
			this.ItemSacrifices.Reset();
		}

		// Token: 0x0600468D RID: 18061 RVA: 0x00632331 File Offset: 0x00630531
		public void OnPlayerJoining(int playerIndex)
		{
			this.ItemSacrifices.OnPlayerJoining(playerIndex);
		}

		// Token: 0x04005B83 RID: 23427
		public ItemsSacrificedUnlocksTracker ItemSacrifices = new ItemsSacrificedUnlocksTracker();
	}
}
