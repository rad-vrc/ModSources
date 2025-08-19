using System;
using System.IO;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x020002F0 RID: 752
	public class BestiaryUnlocksTracker : IPersistentPerWorldContent, IOnPlayerJoining
	{
		// Token: 0x0600237F RID: 9087 RVA: 0x00556B2E File Offset: 0x00554D2E
		public void Save(BinaryWriter writer)
		{
			this.Kills.Save(writer);
			this.Sights.Save(writer);
			this.Chats.Save(writer);
		}

		// Token: 0x06002380 RID: 9088 RVA: 0x00556B54 File Offset: 0x00554D54
		public void Load(BinaryReader reader, int gameVersionSaveWasMadeOn)
		{
			this.Kills.Load(reader, gameVersionSaveWasMadeOn);
			this.Sights.Load(reader, gameVersionSaveWasMadeOn);
			this.Chats.Load(reader, gameVersionSaveWasMadeOn);
		}

		// Token: 0x06002381 RID: 9089 RVA: 0x00556B7D File Offset: 0x00554D7D
		public void ValidateWorld(BinaryReader reader, int gameVersionSaveWasMadeOn)
		{
			this.Kills.ValidateWorld(reader, gameVersionSaveWasMadeOn);
			this.Sights.ValidateWorld(reader, gameVersionSaveWasMadeOn);
			this.Chats.ValidateWorld(reader, gameVersionSaveWasMadeOn);
		}

		// Token: 0x06002382 RID: 9090 RVA: 0x00556BA6 File Offset: 0x00554DA6
		public void Reset()
		{
			this.Kills.Reset();
			this.Sights.Reset();
			this.Chats.Reset();
		}

		// Token: 0x06002383 RID: 9091 RVA: 0x00556BC9 File Offset: 0x00554DC9
		public void OnPlayerJoining(int playerIndex)
		{
			this.Kills.OnPlayerJoining(playerIndex);
			this.Sights.OnPlayerJoining(playerIndex);
			this.Chats.OnPlayerJoining(playerIndex);
		}

		// Token: 0x06002384 RID: 9092 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void FillBasedOnVersionBefore210()
		{
		}

		// Token: 0x0400482B RID: 18475
		public NPCKillsTracker Kills = new NPCKillsTracker();

		// Token: 0x0400482C RID: 18476
		public NPCWasNearPlayerTracker Sights = new NPCWasNearPlayerTracker();

		// Token: 0x0400482D RID: 18477
		public NPCWasChatWithTracker Chats = new NPCWasChatWithTracker();
	}
}
