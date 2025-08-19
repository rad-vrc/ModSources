using System;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace Terraria.ID
{
	/// <summary>
	/// AmmoID entries represent ammo types. Ammo items that share the same AmmoID value assigned to <see cref="F:Terraria.Item.ammo" /> can all be used as ammo for weapons using that same value for <see cref="F:Terraria.Item.useAmmo" />. AmmoID values are actually equivalent to the <see cref="T:Terraria.ID.ItemID" /> value of the iconic ammo item.<br />
	/// The <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-Ammo">Basic Ammo Guide</see> teaches more about ammo.
	/// </summary>
	// Token: 0x020003F5 RID: 1013
	public static class AmmoID
	{
		// Token: 0x04001EEE RID: 7918
		public static int None = 0;

		// Token: 0x04001EEF RID: 7919
		public static int Gel = 23;

		// Token: 0x04001EF0 RID: 7920
		public static int Arrow = 40;

		// Token: 0x04001EF1 RID: 7921
		public static int Coin = 71;

		// Token: 0x04001EF2 RID: 7922
		public static int FallenStar = 75;

		// Token: 0x04001EF3 RID: 7923
		public static int Bullet = 97;

		/// <summary> Items using this as <see cref="F:Terraria.Item.ammo" /> can use <see cref="F:Terraria.ID.ItemID.Sets.SandgunAmmoProjectileData" /> to customize the projectile and bonus damage of this ammo. </summary>
		// Token: 0x04001EF4 RID: 7924
		public static int Sand = 169;

		// Token: 0x04001EF5 RID: 7925
		public static int Dart = 283;

		// Token: 0x04001EF6 RID: 7926
		public static int Rocket = 771;

		// Token: 0x04001EF7 RID: 7927
		public static int Solution = 780;

		// Token: 0x04001EF8 RID: 7928
		public static int Flare = 931;

		// Token: 0x04001EF9 RID: 7929
		public static int Snowball = 949;

		// Token: 0x04001EFA RID: 7930
		public static int StyngerBolt = 1261;

		// Token: 0x04001EFB RID: 7931
		public static int CandyCorn = 1783;

		// Token: 0x04001EFC RID: 7932
		public static int JackOLantern = 1785;

		// Token: 0x04001EFD RID: 7933
		public static int Stake = 1836;

		// Token: 0x04001EFE RID: 7934
		public static int NailFriendly = 3108;

		// Token: 0x02000B3C RID: 2876
		public class Sets
		{
			/// <summary>
			/// Associates a weapon's item type (<see cref="F:Terraria.Item.type" />) and an ammo's item type (<see cref="F:Terraria.Item.type" />) to the projectile type (<see cref="F:Terraria.Projectile.type" />) they will shoot when used together. This is used in vanilla Terraria exclusively by items using rocket ammo but will work for any type of weapon that wants "variant projectiles" for specific ammo.
			/// <para /> For example, a <see cref="F:Terraria.ID.ItemID.SnowmanCannon" /> used with a <see cref="F:Terraria.ID.ItemID.MiniNukeI" /> will fire the <see cref="F:Terraria.ID.ProjectileID.MiniNukeSnowmanRocketI" />.
			/// <para /> New weapons using this system should also set <see cref="F:Terraria.ID.AmmoID.Sets.SpecificLauncherAmmoProjectileFallback" /> to use as a fallback for any ammo item types that are unaccounted for, such as ammo added by other mods.
			/// </summary>
			// Token: 0x0400703B RID: 28731
			public static Dictionary<int, Dictionary<int, int>> SpecificLauncherAmmoProjectileMatches = new Dictionary<int, Dictionary<int, int>>
			{
				{
					759,
					new Dictionary<int, int>
					{
						{
							771,
							134
						},
						{
							772,
							137
						},
						{
							773,
							140
						},
						{
							774,
							143
						},
						{
							4445,
							776
						},
						{
							4446,
							780
						},
						{
							4457,
							793
						},
						{
							4458,
							796
						},
						{
							4459,
							799
						},
						{
							4447,
							784
						},
						{
							4448,
							787
						},
						{
							4449,
							790
						}
					}
				},
				{
					758,
					new Dictionary<int, int>
					{
						{
							771,
							133
						},
						{
							772,
							136
						},
						{
							773,
							139
						},
						{
							774,
							142
						},
						{
							4445,
							777
						},
						{
							4446,
							781
						},
						{
							4457,
							794
						},
						{
							4458,
							797
						},
						{
							4459,
							800
						},
						{
							4447,
							785
						},
						{
							4448,
							788
						},
						{
							4449,
							791
						}
					}
				},
				{
					760,
					new Dictionary<int, int>
					{
						{
							771,
							135
						},
						{
							772,
							138
						},
						{
							773,
							141
						},
						{
							774,
							144
						},
						{
							4445,
							778
						},
						{
							4446,
							782
						},
						{
							4457,
							795
						},
						{
							4458,
							798
						},
						{
							4459,
							801
						},
						{
							4447,
							786
						},
						{
							4448,
							789
						},
						{
							4449,
							792
						}
					}
				},
				{
					1946,
					new Dictionary<int, int>
					{
						{
							771,
							338
						},
						{
							772,
							339
						},
						{
							773,
							340
						},
						{
							774,
							341
						},
						{
							4445,
							803
						},
						{
							4446,
							804
						},
						{
							4457,
							808
						},
						{
							4458,
							809
						},
						{
							4459,
							810
						},
						{
							4447,
							805
						},
						{
							4448,
							806
						},
						{
							4449,
							807
						}
					}
				},
				{
					3930,
					new Dictionary<int, int>
					{
						{
							771,
							715
						},
						{
							772,
							716
						},
						{
							773,
							717
						},
						{
							774,
							718
						},
						{
							4445,
							717
						},
						{
							4446,
							718
						},
						{
							4457,
							717
						},
						{
							4458,
							718
						},
						{
							4459,
							717
						},
						{
							4447,
							717
						},
						{
							4448,
							717
						},
						{
							4449,
							717
						}
					}
				}
			};

			// Token: 0x0400703C RID: 28732
			public static SetFactory Factory = new SetFactory(ItemLoader.ItemCount, "AmmoID", ItemID.Search);

			/// <summary>
			/// If <see langword="true" /> for a given item type (<see cref="F:Terraria.Item.type" />), then items of that type are counted as arrows for the purposes of <see cref="F:Terraria.Player.arrowDamage" /> and the <see cref="F:Terraria.ID.ItemID.MagicQuiver" />.
			/// <br /> Defaults to <see langword="false" />.
			/// </summary>
			// Token: 0x0400703D RID: 28733
			public static bool[] IsArrow = AmmoID.Sets.Factory.CreateBoolSet(false, new int[]
			{
				AmmoID.Arrow,
				AmmoID.Stake
			});

			/// <summary>
			/// If <see langword="true" /> for a given item type (<see cref="F:Terraria.Item.type" />), then items of that type are counted as bullets for the purposes of <see cref="F:Terraria.Player.bulletDamage" />.
			/// <br /> Defaults to <see langword="false" />.
			/// </summary>
			// Token: 0x0400703E RID: 28734
			public static bool[] IsBullet = AmmoID.Sets.Factory.CreateBoolSet(false, new int[]
			{
				AmmoID.Bullet,
				AmmoID.CandyCorn
			});

			/// <summary>
			/// If <see langword="true" /> for a given item type (<see cref="F:Terraria.Item.type" />), then items of that type are counted as specialist ammo for the purposes of <see cref="F:Terraria.Player.specialistDamage" />.
			/// <br /> Defaults to <see langword="false" />.
			/// </summary>
			// Token: 0x0400703F RID: 28735
			public static bool[] IsSpecialist = AmmoID.Sets.Factory.CreateBoolSet(false, new int[]
			{
				AmmoID.Rocket,
				AmmoID.StyngerBolt,
				AmmoID.JackOLantern,
				AmmoID.NailFriendly,
				AmmoID.Coin,
				AmmoID.Flare,
				AmmoID.Dart,
				AmmoID.Snowball,
				AmmoID.Sand,
				AmmoID.FallenStar,
				AmmoID.Gel
			});

			/// <summary>
			/// Maps a weapon item type (<see cref="F:Terraria.Item.type" />) to a fallback item type to use when determining weapon-specific projectiles for an ammo. If an entry is not found in <see cref="F:Terraria.ID.AmmoID.Sets.SpecificLauncherAmmoProjectileMatches" /> for the weapon item and ammo item pair, then the fallback item provided by this set will be used as a fallback query into <see cref="F:Terraria.ID.AmmoID.Sets.SpecificLauncherAmmoProjectileMatches" />.
			/// <para /> This enables weapons, most commonly weapons using rocket ammo, to properly use "variant projectiles" for specific ammo without tedious copy and paste or cross-mod integrations. For example, Mod A could add an upgraded <see cref="F:Terraria.ID.ItemID.SnowmanCannon" /> using this fallback mechanism. If Mod B adds a new rocket ammo item and associated snow themed variant projectile to <see cref="F:Terraria.ID.AmmoID.Sets.SpecificLauncherAmmoProjectileMatches" />, then Mod A's weapon would automatically use that variant projectile when using that ammo.
			/// <para /> Defaults to <c>-1</c>.
			/// </summary>
			// Token: 0x04007040 RID: 28736
			public static int[] SpecificLauncherAmmoProjectileFallback = AmmoID.Sets.Factory.CreateIntSet(Array.Empty<int>());
		}
	}
}
