using System;
using System.IO;

namespace Terraria.GameContent.Creative
{
	// Token: 0x020002C4 RID: 708
	public class CreativeUnlocksTracker : IPersistentPerWorldContent, IOnPlayerJoining
	{
		// Token: 0x0600226A RID: 8810 RVA: 0x00542D74 File Offset: 0x00540F74
		public void Save(BinaryWriter writer)
		{
			this.ItemSacrifices.Save(writer);
		}

		// Token: 0x0600226B RID: 8811 RVA: 0x00542D82 File Offset: 0x00540F82
		public void Load(BinaryReader reader, int gameVersionSaveWasMadeOn)
		{
			this.ItemSacrifices.Load(reader, gameVersionSaveWasMadeOn);
		}

		// Token: 0x0600226C RID: 8812 RVA: 0x00542D91 File Offset: 0x00540F91
		public void ValidateWorld(BinaryReader reader, int gameVersionSaveWasMadeOn)
		{
			this.ValidateWorld(reader, gameVersionSaveWasMadeOn);
		}

		// Token: 0x0600226D RID: 8813 RVA: 0x00542D9B File Offset: 0x00540F9B
		public void Reset()
		{
			this.ItemSacrifices.Reset();
		}

		// Token: 0x0600226E RID: 8814 RVA: 0x00542DA8 File Offset: 0x00540FA8
		public void OnPlayerJoining(int playerIndex)
		{
			this.ItemSacrifices.OnPlayerJoining(playerIndex);
		}

		// Token: 0x040047D2 RID: 18386
		public ItemsSacrificedUnlocksTracker ItemSacrifices = new ItemsSacrificedUnlocksTracker();
	}
}
