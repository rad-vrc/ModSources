using System;

namespace Terraria.ModLoader
{
	/// <summary>
	/// A struct that stores information regarding where an NPC is naturally spawning and the player it is spawning around. This serves to reduce the parameter count for ModNPC.CanSpawn and GlobalNPC.EditSpawnPool.
	/// </summary>
	// Token: 0x020001E5 RID: 485
	public struct NPCSpawnInfo
	{
		/// <summary>
		/// The x-coordinate of the tile the NPC will spawn above.
		/// </summary>
		// Token: 0x040017C8 RID: 6088
		public int SpawnTileX;

		/// <summary>
		/// The y-coordinate of the tile the NPC will spawn above.
		/// </summary>
		// Token: 0x040017C9 RID: 6089
		public int SpawnTileY;

		// Token: 0x040017CA RID: 6090
		public int SpawnTileType;

		/// <summary>
		/// The player that this NPC is spawning around.
		/// For convenience, here are the player zones, which are also useful for determining NPC spawn:
		/// (ZoneGranite, ZoneMarble, ZoneHive, ZoneGemCave are not actually proper spawning related checks, they are for visuals only (RGB), determined by the backwall type)
		/// <list type="bullet">
		/// <item><description>ZoneDungeon</description></item>
		/// <item><description>ZoneCorrupt</description></item>
		/// <item><description>ZoneHallow</description></item>
		/// <item><description>ZoneMeteor</description></item>
		/// <item><description>ZoneJungle</description></item>
		/// <item><description>ZoneSnow</description></item>
		/// <item><description>ZoneCrimson</description></item>
		/// <item><description>ZoneWaterCandle</description></item>
		/// <item><description>ZonePeaceCandle</description></item>
		/// <item><description>ZoneTowerSolar</description></item>
		/// <item><description>ZoneTowerVortex</description></item>
		/// <item><description>ZoneTowerNebula</description></item>
		/// <item><description>ZoneTowerStardust</description></item>
		/// <item><description>ZoneDesert</description></item>
		/// <item><description>ZoneGlowshroom</description></item>
		/// <item><description>ZoneUndergroundDesert</description></item>
		/// <item><description>ZoneSkyHeight</description></item>
		/// <item><description>ZoneOverworldHeight</description></item>
		/// <item><description>ZoneDirtLayerHeight</description></item>
		/// <item><description>ZoneRockLayerHeight</description></item>
		/// <item><description>ZoneUnderworldHeight</description></item>
		/// <item><description>ZoneBeach</description></item>
		/// <item><description>ZoneRain</description></item>
		/// <item><description>ZoneSandstorm</description></item>
		/// <item><description>ZoneOldOneArmy</description></item>
		/// <item><description>ZoneGraveyard</description></item>
		/// </list>
		/// </summary>
		// Token: 0x040017CB RID: 6091
		public Player Player;

		/// <summary>
		/// The x-coordinate of the tile the player is standing on.
		/// </summary>
		// Token: 0x040017CC RID: 6092
		public int PlayerFloorX;

		/// <summary>
		/// The y-coordinate of the tile the player is standing on.
		/// </summary>
		// Token: 0x040017CD RID: 6093
		public int PlayerFloorY;

		/// <summary>
		/// Whether or not the player is in the sky biome, where harpies and wyverns spawn.
		/// </summary>
		// Token: 0x040017CE RID: 6094
		public bool Sky;

		/// <summary>
		/// Whether or not the player is inside the jungle temple, where Lihzahrds spawn.
		/// </summary>
		// Token: 0x040017CF RID: 6095
		public bool Lihzahrd;

		/// <summary>
		/// Whether or not the player is in front of a player-placed wall or in a large town. If this is true, enemies that can attack through walls should not spawn (unless an invasion is in progress).
		/// </summary>
		// Token: 0x040017D0 RID: 6096
		public bool PlayerSafe;

		/// <summary>
		/// Whether or not there is an invasion going on and the player is near it.
		/// </summary>
		// Token: 0x040017D1 RID: 6097
		public bool Invasion;

		/// <summary>
		/// Whether or not the tile the NPC will spawn in contains water.
		/// </summary>
		// Token: 0x040017D2 RID: 6098
		public bool Water;

		/// <summary>
		/// Whether or not the NPC will spawn on a granite block or the player is near a granite biome.
		/// </summary>
		// Token: 0x040017D3 RID: 6099
		public bool Granite;

		/// <summary>
		/// Whether or not the NPC will spawn on a marble block or the player is near a marble biome.
		/// </summary>
		// Token: 0x040017D4 RID: 6100
		public bool Marble;

		/// <summary>
		/// Whether or not the player is in a spider cave or the NPC will spawn near one.
		/// </summary>
		// Token: 0x040017D5 RID: 6101
		public bool SpiderCave;

		/// <summary>
		/// Whether or not the player is in a town. This is used for spawning critters instead of monsters.
		/// </summary>
		// Token: 0x040017D6 RID: 6102
		public bool PlayerInTown;

		/// <summary>
		/// Whether or not the player is in front of a desert wall or the NPC will spawn near one.
		/// </summary>
		// Token: 0x040017D7 RID: 6103
		public bool DesertCave;

		/// <summary>
		/// Whether or not the NPC is horizontally within the range near the player in which NPCs cannot spawn. If this is true, it also means that it is vertically outside of the range near the player in which NPCs cannot spawn.
		/// </summary>
		// Token: 0x040017D8 RID: 6104
		public bool SafeRangeX;
	}
}
