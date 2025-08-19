using System;

namespace Terraria.DataStructures
{
	// Token: 0x0200070B RID: 1803
	public struct FishingAttempt
	{
		// Token: 0x04005ED9 RID: 24281
		public PlayerFishingConditions playerFishingConditions;

		/// <summary>
		/// The x-coordinate of tile this bobber is on, in tile coordinates.
		/// </summary>
		// Token: 0x04005EDA RID: 24282
		public int X;

		/// <summary>
		/// The y-coordinate of tile this bobber is on, in tile coordinates.
		/// </summary>
		// Token: 0x04005EDB RID: 24283
		public int Y;

		/// <summary>
		/// The projectile type (<see cref="F:Terraria.Projectile.type" />) of this bobber.
		/// </summary>
		// Token: 0x04005EDC RID: 24284
		public int bobberType;

		/// <summary>
		/// If <see langword="true" />, this fishing attempt can catch common items.
		/// </summary>
		// Token: 0x04005EDD RID: 24285
		public bool common;

		/// <summary>
		/// If <see langword="true" />, this fishing attempt can catch uncommon items.
		/// </summary>
		// Token: 0x04005EDE RID: 24286
		public bool uncommon;

		/// <summary>
		/// If <see langword="true" />, this fishing attempt can catch rare items.
		/// </summary>
		// Token: 0x04005EDF RID: 24287
		public bool rare;

		/// <summary>
		/// If <see langword="true" />, this fishing attempt can catch very rare items.
		/// </summary>
		// Token: 0x04005EE0 RID: 24288
		public bool veryrare;

		/// <summary>
		/// If <see langword="true" />, this fishing attempt can catch legendary items.
		/// </summary>
		// Token: 0x04005EE1 RID: 24289
		public bool legendary;

		/// <summary>
		/// If <see langword="true" />, this fishing attempt can catch crates.
		/// </summary>
		// Token: 0x04005EE2 RID: 24290
		public bool crate;

		/// <summary>
		/// If <see langword="true" />, this fishing attempt is in lava.
		/// </summary>
		// Token: 0x04005EE3 RID: 24291
		public bool inLava;

		/// <summary>
		/// If <see langword="true" />, this fishing attempt is in honey.
		/// </summary>
		// Token: 0x04005EE4 RID: 24292
		public bool inHoney;

		/// <summary>
		/// The number of liquid tiles counted for this fishing attempt.
		/// </summary>
		// Token: 0x04005EE5 RID: 24293
		public int waterTilesCount;

		/// <summary>
		/// The number of liquid tiles needed for proper fishing. If <see cref="F:Terraria.DataStructures.FishingAttempt.waterTilesCount" /> is less than this, then the player will receive a <see cref="F:Terraria.DataStructures.FishingAttempt.waterQuality" /> percent debuff to their fishing power.
		/// <br /> This debuff is automatically applied to <see cref="F:Terraria.DataStructures.FishingAttempt.fishingLevel" />.
		/// </summary>
		// Token: 0x04005EE6 RID: 24294
		public int waterNeededToFish;

		/// <summary>
		/// If positive, the percent decrease in fishing power this attempt has from missing liquid tiles.
		/// <br /> This is <strong>not</strong> how full the body of liquid is.
		/// </summary>
		// Token: 0x04005EE7 RID: 24295
		public float waterQuality;

		/// <summary>
		/// The number of chums applied to this attempt. Fishing power from chum is automatically added to <see cref="F:Terraria.DataStructures.FishingAttempt.fishingLevel" />.
		/// </summary>
		// Token: 0x04005EE8 RID: 24296
		public int chumsInWater;

		/// <summary>
		/// The fishing power of this attempt after all modifications. The higher this number, the better the attempt will go.
		/// </summary>
		// Token: 0x04005EE9 RID: 24297
		public int fishingLevel;

		/// <summary>
		/// If <see langword="true" />, then this attempt can succeed if it is <see cref="F:Terraria.DataStructures.FishingAttempt.inLava" />.
		/// </summary>
		// Token: 0x04005EEA RID: 24298
		public bool CanFishInLava;

		/// <summary>
		/// How high in the sky this attempt takes place, in the range [<c>0.25f</c>, <c>1f</c>]. Any value below <c>1f</c> takes place approximately in the top 10% of the world.
		/// <br /> The lower this value, the smaller <see cref="F:Terraria.DataStructures.FishingAttempt.waterNeededToFish" /> will be, which is automatically applied.
		/// </summary>
		// Token: 0x04005EEB RID: 24299
		public float atmo;

		/// <summary>
		/// The item type (<see cref="F:Terraria.Item.type" />) of the quest fish the Angler wants, or <c>-1</c> if this player can't catch that fish today.
		/// </summary>
		// Token: 0x04005EEC RID: 24300
		public int questFish;

		/// <summary>
		/// A representation of the current height.
		/// <br /> <c>0</c> is space-level (50% of <see cref="F:Terraria.Main.worldSurface" /> or higher).
		/// <br /> <c>1</c> is the surface (<see cref="P:Terraria.Player.ZoneOverworldHeight" />).
		/// <br /> <c>2</c> is underground (<see cref="P:Terraria.Player.ZoneDirtLayerHeight" />).
		/// <br /> <c>3</c> is the caverns (<see cref="P:Terraria.Player.ZoneRockLayerHeight" />).
		/// <br /> <c>4</c> is the underworld (<see cref="P:Terraria.Player.ZoneUnderworldHeight" />).
		/// </summary>
		// Token: 0x04005EED RID: 24301
		public int heightLevel;

		/// <summary>
		/// The item type (<see cref="F:Terraria.Item.type" />) of the caught item.
		/// </summary>
		// Token: 0x04005EEE RID: 24302
		public int rolledItemDrop;

		/// <summary>
		/// The item type (<see cref="F:Terraria.Item.type" />) of the caught NPC.
		/// </summary>
		// Token: 0x04005EEF RID: 24303
		public int rolledEnemySpawn;
	}
}
