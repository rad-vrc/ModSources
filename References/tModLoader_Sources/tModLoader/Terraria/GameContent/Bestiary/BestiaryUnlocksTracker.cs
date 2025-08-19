using System;
using System.IO;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x0200067B RID: 1659
	public class BestiaryUnlocksTracker : IPersistentPerWorldContent, IOnPlayerJoining
	{
		// Token: 0x060047BB RID: 18363 RVA: 0x006464B7 File Offset: 0x006446B7
		public void Save(BinaryWriter writer)
		{
			this.Kills.Save(writer);
			this.Sights.Save(writer);
			this.Chats.Save(writer);
		}

		// Token: 0x060047BC RID: 18364 RVA: 0x006464DD File Offset: 0x006446DD
		public void Load(BinaryReader reader, int gameVersionSaveWasMadeOn)
		{
			this.Kills.Load(reader, gameVersionSaveWasMadeOn);
			this.Sights.Load(reader, gameVersionSaveWasMadeOn);
			this.Chats.Load(reader, gameVersionSaveWasMadeOn);
		}

		// Token: 0x060047BD RID: 18365 RVA: 0x00646506 File Offset: 0x00644706
		public void ValidateWorld(BinaryReader reader, int gameVersionSaveWasMadeOn)
		{
			this.Kills.ValidateWorld(reader, gameVersionSaveWasMadeOn);
			this.Sights.ValidateWorld(reader, gameVersionSaveWasMadeOn);
			this.Chats.ValidateWorld(reader, gameVersionSaveWasMadeOn);
		}

		// Token: 0x060047BE RID: 18366 RVA: 0x0064652F File Offset: 0x0064472F
		public void Reset()
		{
			this.Kills.Reset();
			this.Sights.Reset();
			this.Chats.Reset();
		}

		// Token: 0x060047BF RID: 18367 RVA: 0x00646552 File Offset: 0x00644752
		public void OnPlayerJoining(int playerIndex)
		{
			this.Kills.OnPlayerJoining(playerIndex);
			this.Sights.OnPlayerJoining(playerIndex);
			this.Chats.OnPlayerJoining(playerIndex);
		}

		// Token: 0x060047C0 RID: 18368 RVA: 0x00646578 File Offset: 0x00644778
		public void FillBasedOnVersionBefore210()
		{
		}

		// Token: 0x04005BF4 RID: 23540
		public NPCKillsTracker Kills = new NPCKillsTracker();

		// Token: 0x04005BF5 RID: 23541
		public NPCWasNearPlayerTracker Sights = new NPCWasNearPlayerTracker();

		// Token: 0x04005BF6 RID: 23542
		public NPCWasChatWithTracker Chats = new NPCWasChatWithTracker();
	}
}
