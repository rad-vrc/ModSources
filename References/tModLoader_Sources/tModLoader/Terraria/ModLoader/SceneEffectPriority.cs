using System;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This enum dictates from low to high which SceneEffect selections take priority.
	/// Setting appropriate priority values is important so that your mod works well with other mods and vanilla selections.
	/// </summary>
	// Token: 0x020001F8 RID: 504
	public enum SceneEffectPriority
	{
		/// <summary>Represents no priority</summary>
		// Token: 0x040018AE RID: 6318
		None,
		/// <summary> Will override vanilla SceneEffect for Hallow, Ocean, Desert, Overworld, Night</summary>
		// Token: 0x040018AF RID: 6319
		BiomeLow,
		/// <summary> Will override vanilla SceneEffect for Meteor, Jungle, Graveyard, Snow</summary>
		// Token: 0x040018B0 RID: 6320
		BiomeMedium,
		/// <summary> Will override vanilla SceneEffect for Temple, Dungeon, Mushrooms, Corruption, Crimson</summary>
		// Token: 0x040018B1 RID: 6321
		BiomeHigh,
		/// <summary> Will override vanilla SceneEffect for Sandstorm, Hell, Above surface during Eclipse, Space, Shimmer</summary>
		// Token: 0x040018B2 RID: 6322
		Environment,
		/// <summary> Will override vanilla SceneEffect for Pirate Invasion, Goblin Invasion, Old Ones Army, Pumpkin Moon, Frost Moon</summary>
		// Token: 0x040018B3 RID: 6323
		Event,
		/// <summary>All other bosses and default modded boss priority</summary>
		// Token: 0x040018B4 RID: 6324
		BossLow,
		/// <summary>Will override vanilla SceneEffect for Martian Madness, Celestial Towers, Plantera, Mechdusa</summary>
		// Token: 0x040018B5 RID: 6325
		BossMedium,
		/// <summary>Will override SceneEffect of Moon Lord and Torch God</summary>
		// Token: 0x040018B6 RID: 6326
		BossHigh
	}
}
