using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Terraria.Audio;

namespace Terraria.ID
{
	// Token: 0x02000429 RID: 1065
	public class SoundID
	{
		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06003572 RID: 13682 RVA: 0x0057353C File Offset: 0x0057173C
		internal static int TrackableLegacySoundCount
		{
			get
			{
				return SoundID._trackableLegacySoundPathList.Count;
			}
		}

		// Token: 0x06003573 RID: 13683 RVA: 0x00573548 File Offset: 0x00571748
		internal static string GetTrackableLegacySoundPath(int id)
		{
			return SoundID._trackableLegacySoundPathList[id];
		}

		// Token: 0x06003574 RID: 13684 RVA: 0x00573558 File Offset: 0x00571758
		private static SoundStyle CreateTrackable(string name, SoundID.SoundStyleDefaults defaults)
		{
			return SoundID.CreateTrackable(name, 1, defaults.Type).WithPitchVariance(defaults.PitchVariance).WithVolume(defaults.Volume);
		}

		// Token: 0x06003575 RID: 13685 RVA: 0x00573590 File Offset: 0x00571790
		private static SoundStyle CreateTrackable(string name, int variations, SoundID.SoundStyleDefaults defaults)
		{
			return SoundID.CreateTrackable(name, variations, defaults.Type).WithPitchVariance(defaults.PitchVariance).WithVolume(defaults.Volume);
		}

		// Token: 0x06003576 RID: 13686 RVA: 0x005735C6 File Offset: 0x005717C6
		private static SoundStyle CreateTrackable(string name, SoundType type = SoundType.Sound)
		{
			return SoundID.CreateTrackable(name, 1, type);
		}

		// Token: 0x06003577 RID: 13687 RVA: 0x005735D0 File Offset: 0x005717D0
		private static SoundStyle CreateTrackable(string name, int variations, SoundType type = SoundType.Sound)
		{
			if (SoundID._trackableLegacySoundPathList == null)
			{
				SoundID._trackableLegacySoundPathList = new List<string>();
			}
			int count = SoundID._trackableLegacySoundPathList.Count;
			if (variations == 1)
			{
				SoundID._trackableLegacySoundPathList.Add(name);
			}
			else
			{
				for (int i = 0; i < variations; i++)
				{
					SoundID._trackableLegacySoundPathList.Add(name + "_" + i.ToString());
				}
			}
			return new SoundStyle("Terraria/Sounds/Custom/" + name + ((variations > 1) ? "_" : null), 0, variations, type)
			{
				MaxInstances = 0
			};
		}

		// Token: 0x06003578 RID: 13688 RVA: 0x0057365C File Offset: 0x0057185C
		public static void FillAccessMap()
		{
			Dictionary<string, SoundStyle> ret = new Dictionary<string, SoundStyle>();
			Dictionary<string, ushort> ret2 = new Dictionary<string, ushort>();
			Dictionary<ushort, SoundStyle> ret3 = new Dictionary<ushort, SoundStyle>();
			ushort nextIndex = 0;
			List<FieldInfo> list = (from f in typeof(SoundID).GetFields(BindingFlags.Static | BindingFlags.Public)
			where f.FieldType == typeof(SoundStyle)
			select f).ToList<FieldInfo>();
			list.Sort((FieldInfo a, FieldInfo b) => string.Compare(a.Name, b.Name));
			list.ForEach(delegate(FieldInfo field)
			{
				ret[field.Name] = (SoundStyle)field.GetValue(null);
				ushort nextIndex;
				ret2[field.Name] = nextIndex;
				ret3[nextIndex] = (SoundStyle)field.GetValue(null);
				nextIndex = nextIndex;
				nextIndex += 1;
			});
			SoundID.SoundByName = ret;
			SoundID.IndexByName = ret2;
			SoundID.SoundByIndex = ret3;
		}

		// Token: 0x06003579 RID: 13689 RVA: 0x00573728 File Offset: 0x00571928
		unsafe static SoundID()
		{
			SoundStyle soundStyle = new SoundStyle("Terraria/Sounds/NPC_Killed_14", SoundType.Sound)
			{
				Volume = 0.5f
			};
			SoundID.DD2_GoblinBomb = soundStyle;
			SoundID.AchievementComplete = SoundID.CreateTrackable("achievement_complete", SoundType.Sound);
			SoundID.BlizzardInsideBuildingLoop = SoundID.CreateTrackable("blizzard_inside_building_loop", SoundType.Ambient);
			soundStyle = SoundID.CreateTrackable("blizzard_strong_loop", SoundType.Ambient);
			SoundID.BlizzardStrongLoop = soundStyle.WithVolume(0.5f);
			SoundID.LiquidsHoneyWater = SoundID.CreateTrackable("liquids_honey_water", 3, SoundType.Ambient);
			SoundID.LiquidsHoneyLava = SoundID.CreateTrackable("liquids_honey_lava", 3, SoundType.Ambient);
			SoundID.LiquidsWaterLava = SoundID.CreateTrackable("liquids_water_lava", 3, SoundType.Ambient);
			SoundID.DD2_BallistaTowerShot = SoundID.CreateTrackable("dd2_ballista_tower_shot", 3, SoundType.Sound);
			SoundID.DD2_ExplosiveTrapExplode = SoundID.CreateTrackable("dd2_explosive_trap_explode", 3, SoundType.Sound);
			SoundID.DD2_FlameburstTowerShot = SoundID.CreateTrackable("dd2_flameburst_tower_shot", 3, SoundType.Sound);
			SoundID.DD2_LightningAuraZap = SoundID.CreateTrackable("dd2_lightning_aura_zap", 4, SoundType.Sound);
			SoundID.DD2_DefenseTowerSpawn = SoundID.CreateTrackable("dd2_defense_tower_spawn", SoundType.Sound);
			SoundID.DD2_BetsyDeath = SoundID.CreateTrackable("dd2_betsy_death", 3, SoundType.Sound);
			SoundID.DD2_BetsyFireballShot = SoundID.CreateTrackable("dd2_betsy_fireball_shot", 3, SoundType.Sound);
			SoundID.DD2_BetsyFireballImpact = SoundID.CreateTrackable("dd2_betsy_fireball_impact", 3, SoundType.Sound);
			SoundID.DD2_BetsyFlameBreath = SoundID.CreateTrackable("dd2_betsy_flame_breath", SoundType.Sound);
			SoundID.DD2_BetsyFlyingCircleAttack = SoundID.CreateTrackable("dd2_betsy_flying_circle_attack", SoundType.Sound);
			SoundID.DD2_BetsyHurt = SoundID.CreateTrackable("dd2_betsy_hurt", 3, SoundType.Sound);
			SoundID.DD2_BetsyScream = SoundID.CreateTrackable("dd2_betsy_scream", SoundType.Sound);
			SoundID.DD2_BetsySummon = SoundID.CreateTrackable("dd2_betsy_summon", 3, SoundType.Sound);
			SoundID.DD2_BetsyWindAttack = SoundID.CreateTrackable("dd2_betsy_wind_attack", 3, SoundType.Sound);
			SoundID.DD2_DarkMageAttack = SoundID.CreateTrackable("dd2_dark_mage_attack", 3, SoundType.Sound);
			SoundID.DD2_DarkMageCastHeal = SoundID.CreateTrackable("dd2_dark_mage_cast_heal", 3, SoundType.Sound);
			SoundID.DD2_DarkMageDeath = SoundID.CreateTrackable("dd2_dark_mage_death", 3, SoundType.Sound);
			SoundID.DD2_DarkMageHealImpact = SoundID.CreateTrackable("dd2_dark_mage_heal_impact", 3, SoundType.Sound);
			SoundID.DD2_DarkMageHurt = SoundID.CreateTrackable("dd2_dark_mage_hurt", 3, SoundType.Sound);
			SoundID.DD2_DarkMageSummonSkeleton = SoundID.CreateTrackable("dd2_dark_mage_summon_skeleton", 3, SoundType.Sound);
			SoundID.DD2_DrakinBreathIn = SoundID.CreateTrackable("dd2_drakin_breath_in", 3, SoundType.Sound);
			SoundID.DD2_DrakinDeath = SoundID.CreateTrackable("dd2_drakin_death", 3, SoundType.Sound);
			SoundID.DD2_DrakinHurt = SoundID.CreateTrackable("dd2_drakin_hurt", 3, SoundType.Sound);
			SoundID.DD2_DrakinShot = SoundID.CreateTrackable("dd2_drakin_shot", 3, SoundType.Sound);
			SoundID.DD2_GoblinDeath = SoundID.CreateTrackable("dd2_goblin_death", 3, SoundType.Sound);
			SoundID.DD2_GoblinHurt = SoundID.CreateTrackable("dd2_goblin_hurt", 6, SoundType.Sound);
			SoundID.DD2_GoblinScream = SoundID.CreateTrackable("dd2_goblin_scream", 3, SoundType.Sound);
			SoundID.DD2_GoblinBomberDeath = SoundID.CreateTrackable("dd2_goblin_bomber_death", 3, SoundType.Sound);
			SoundID.DD2_GoblinBomberHurt = SoundID.CreateTrackable("dd2_goblin_bomber_hurt", 3, SoundType.Sound);
			SoundID.DD2_GoblinBomberScream = SoundID.CreateTrackable("dd2_goblin_bomber_scream", 3, SoundType.Sound);
			SoundID.DD2_GoblinBomberThrow = SoundID.CreateTrackable("dd2_goblin_bomber_throw", 3, SoundType.Sound);
			SoundID.DD2_JavelinThrowersAttack = SoundID.CreateTrackable("dd2_javelin_throwers_attack", 3, SoundType.Sound);
			SoundID.DD2_JavelinThrowersDeath = SoundID.CreateTrackable("dd2_javelin_throwers_death", 3, SoundType.Sound);
			SoundID.DD2_JavelinThrowersHurt = SoundID.CreateTrackable("dd2_javelin_throwers_hurt", 3, SoundType.Sound);
			SoundID.DD2_JavelinThrowersTaunt = SoundID.CreateTrackable("dd2_javelin_throwers_taunt", 3, SoundType.Sound);
			SoundID.DD2_KoboldDeath = SoundID.CreateTrackable("dd2_kobold_death", 3, SoundType.Sound);
			SoundID.DD2_KoboldExplosion = SoundID.CreateTrackable("dd2_kobold_explosion", 3, SoundType.Sound);
			SoundID.DD2_KoboldHurt = SoundID.CreateTrackable("dd2_kobold_hurt", 3, SoundType.Sound);
			SoundID.DD2_KoboldIgnite = SoundID.CreateTrackable("dd2_kobold_ignite", SoundType.Sound);
			SoundID.DD2_KoboldIgniteLoop = SoundID.CreateTrackable("dd2_kobold_ignite_loop", SoundType.Sound);
			SoundID.DD2_KoboldScreamChargeLoop = SoundID.CreateTrackable("dd2_kobold_scream_charge_loop", SoundType.Sound);
			SoundID.DD2_KoboldFlyerChargeScream = SoundID.CreateTrackable("dd2_kobold_flyer_charge_scream", 3, SoundType.Sound);
			SoundID.DD2_KoboldFlyerDeath = SoundID.CreateTrackable("dd2_kobold_flyer_death", 3, SoundType.Sound);
			SoundID.DD2_KoboldFlyerHurt = SoundID.CreateTrackable("dd2_kobold_flyer_hurt", 3, SoundType.Sound);
			SoundID.DD2_LightningBugDeath = SoundID.CreateTrackable("dd2_lightning_bug_death", 3, SoundType.Sound);
			SoundID.DD2_LightningBugHurt = SoundID.CreateTrackable("dd2_lightning_bug_hurt", 3, SoundType.Sound);
			SoundID.DD2_LightningBugZap = SoundID.CreateTrackable("dd2_lightning_bug_zap", 3, SoundType.Sound);
			SoundID.DD2_OgreAttack = SoundID.CreateTrackable("dd2_ogre_attack", 3, SoundType.Sound);
			SoundID.DD2_OgreDeath = SoundID.CreateTrackable("dd2_ogre_death", 3, SoundType.Sound);
			SoundID.DD2_OgreGroundPound = SoundID.CreateTrackable("dd2_ogre_ground_pound", SoundType.Sound);
			SoundID.DD2_OgreHurt = SoundID.CreateTrackable("dd2_ogre_hurt", 3, SoundType.Sound);
			SoundID.DD2_OgreRoar = SoundID.CreateTrackable("dd2_ogre_roar", 3, SoundType.Sound);
			SoundID.DD2_OgreSpit = SoundID.CreateTrackable("dd2_ogre_spit", SoundType.Sound);
			SoundID.DD2_SkeletonDeath = SoundID.CreateTrackable("dd2_skeleton_death", 3, SoundType.Sound);
			SoundID.DD2_SkeletonHurt = SoundID.CreateTrackable("dd2_skeleton_hurt", 3, SoundType.Sound);
			SoundID.DD2_SkeletonSummoned = SoundID.CreateTrackable("dd2_skeleton_summoned", SoundType.Sound);
			SoundID.DD2_WitherBeastAuraPulse = SoundID.CreateTrackable("dd2_wither_beast_aura_pulse", 2, SoundType.Sound);
			SoundID.DD2_WitherBeastCrystalImpact = SoundID.CreateTrackable("dd2_wither_beast_crystal_impact", 3, SoundType.Sound);
			SoundID.DD2_WitherBeastDeath = SoundID.CreateTrackable("dd2_wither_beast_death", 3, SoundType.Sound);
			SoundID.DD2_WitherBeastHurt = SoundID.CreateTrackable("dd2_wither_beast_hurt", 3, SoundType.Sound);
			SoundID.DD2_WyvernDeath = SoundID.CreateTrackable("dd2_wyvern_death", 3, SoundType.Sound);
			SoundID.DD2_WyvernHurt = SoundID.CreateTrackable("dd2_wyvern_hurt", 3, SoundType.Sound);
			SoundID.DD2_WyvernScream = SoundID.CreateTrackable("dd2_wyvern_scream", 3, SoundType.Sound);
			SoundID.DD2_WyvernDiveDown = SoundID.CreateTrackable("dd2_wyvern_dive_down", 3, SoundType.Sound);
			SoundID.DD2_EtherianPortalDryadTouch = SoundID.CreateTrackable("dd2_etherian_portal_dryad_touch", SoundType.Sound);
			SoundID.DD2_EtherianPortalIdleLoop = SoundID.CreateTrackable("dd2_etherian_portal_idle_loop", SoundType.Sound);
			SoundID.DD2_EtherianPortalOpen = SoundID.CreateTrackable("dd2_etherian_portal_open", SoundType.Sound);
			SoundID.DD2_EtherianPortalSpawnEnemy = SoundID.CreateTrackable("dd2_etherian_portal_spawn_enemy", 3, SoundType.Sound);
			SoundID.DD2_CrystalCartImpact = SoundID.CreateTrackable("dd2_crystal_cart_impact", 3, SoundType.Sound);
			SoundID.DD2_DefeatScene = SoundID.CreateTrackable("dd2_defeat_scene", SoundType.Sound);
			SoundID.DD2_WinScene = SoundID.CreateTrackable("dd2_win_scene", SoundType.Sound);
			SoundID.DD2_BetsysWrathShot = SoundID.DD2_BetsyFireballShot.WithVolume(0.4f);
			SoundID.DD2_BetsysWrathImpact = SoundID.DD2_BetsyFireballImpact.WithVolume(0.4f);
			SoundID.DD2_BookStaffCast = SoundID.CreateTrackable("dd2_book_staff_cast", 3, SoundType.Sound);
			SoundID.DD2_BookStaffTwisterLoop = SoundID.CreateTrackable("dd2_book_staff_twister_loop", SoundType.Sound);
			SoundID.DD2_GhastlyGlaiveImpactGhost = SoundID.CreateTrackable("dd2_ghastly_glaive_impact_ghost", 3, SoundType.Sound);
			SoundID.DD2_GhastlyGlaivePierce = SoundID.CreateTrackable("dd2_ghastly_glaive_pierce", 3, SoundType.Sound);
			SoundID.DD2_MonkStaffGroundImpact = SoundID.CreateTrackable("dd2_monk_staff_ground_impact", 3, SoundType.Sound);
			SoundID.DD2_MonkStaffGroundMiss = SoundID.CreateTrackable("dd2_monk_staff_ground_miss", 3, SoundType.Sound);
			SoundID.DD2_MonkStaffSwing = SoundID.CreateTrackable("dd2_monk_staff_swing", 4, SoundType.Sound);
			SoundID.DD2_PhantomPhoenixShot = SoundID.CreateTrackable("dd2_phantom_phoenix_shot", 3, SoundType.Sound);
			soundStyle = SoundID.CreateTrackable("dd2_sonic_boom_blade_slash", 3, SoundID.SoundStyleDefaults.ItemDefaults);
			SoundID.DD2_SonicBoomBladeSlash = soundStyle.WithVolume(0.5f);
			SoundID.DD2_SkyDragonsFuryCircle = SoundID.CreateTrackable("dd2_sky_dragons_fury_circle", 3, SoundType.Sound);
			SoundID.DD2_SkyDragonsFuryShot = SoundID.CreateTrackable("dd2_sky_dragons_fury_shot", 3, SoundType.Sound);
			SoundID.DD2_SkyDragonsFurySwing = SoundID.CreateTrackable("dd2_sky_dragons_fury_swing", 4, SoundType.Sound);
			soundStyle = SoundID.CreateTrackable("lucyaxe_talk", 5, SoundType.Sound);
			soundStyle = soundStyle.WithVolume(0.4f);
			SoundID.LucyTheAxeTalk = soundStyle.WithPitchVariance(0.1f);
			soundStyle = SoundID.CreateTrackable("deerclops_hit", 3, SoundType.Sound);
			SoundID.DeerclopsHit = soundStyle.WithVolume(0.3f);
			SoundID.DeerclopsDeath = SoundID.CreateTrackable("deerclops_death", SoundType.Sound);
			SoundID.DeerclopsScream = SoundID.CreateTrackable("deerclops_scream", 3, SoundType.Sound);
			soundStyle = SoundID.CreateTrackable("deerclops_ice_attack", 3, SoundType.Sound);
			SoundID.DeerclopsIceAttack = soundStyle.WithVolume(0.1f);
			soundStyle = SoundID.CreateTrackable("deerclops_rubble_attack", SoundType.Sound);
			SoundID.DeerclopsRubbleAttack = soundStyle.WithVolume(0.5f);
			soundStyle = SoundID.CreateTrackable("deerclops_step", SoundType.Sound);
			SoundID.DeerclopsStep = soundStyle.WithVolume(0.2f);
			SoundID.ChesterOpen = SoundID.CreateTrackable("chester_open", 2, SoundType.Sound);
			SoundID.ChesterClose = SoundID.CreateTrackable("chester_close", 2, SoundType.Sound);
			SoundID.AbigailSummon = SoundID.CreateTrackable("abigail_summon", SoundType.Sound);
			soundStyle = SoundID.CreateTrackable("abigail_cry", 3, SoundType.Sound);
			SoundID.AbigailCry = soundStyle.WithVolume(0.4f);
			soundStyle = SoundID.CreateTrackable("abigail_attack", SoundType.Sound);
			SoundID.AbigailAttack = soundStyle.WithVolume(0.35f);
			soundStyle = SoundID.CreateTrackable("abigail_upgrade", 3, SoundType.Sound);
			SoundID.AbigailUpgrade = soundStyle.WithVolume(0.5f);
			soundStyle = SoundID.CreateTrackable("glommer_bounce", 2, SoundType.Sound);
			SoundID.GlommerBounce = soundStyle.WithVolume(0.5f);
			soundStyle = SoundID.CreateTrackable("dst_male_hit", 3, SoundType.Sound);
			SoundID.DSTMaleHurt = soundStyle.WithVolume(0.1f);
			soundStyle = SoundID.CreateTrackable("dst_female_hit", 3, SoundType.Sound);
			SoundID.DSTFemaleHurt = soundStyle.WithVolume(0.1f);
			soundStyle = SoundID.CreateTrackable("Drone", SoundType.Sound);
			SoundID.JimsDrone = soundStyle.WithVolume(0.1f);
			SoundID.SoundByName = null;
			SoundID.IndexByName = null;
			SoundID.SoundByIndex = null;
			soundStyle = new SoundStyle("Terraria/Sounds/Dig_", 0, 3, SoundType.Sound)
			{
				PitchVariance = 0.2f,
				LimitsArePerVariant = true
			};
			SoundID.Dig = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Player_Hit_", 0, 3, SoundType.Sound)
			{
				LimitsArePerVariant = true
			};
			SoundID.PlayerHit = soundStyle;
			SoundID.Item = new SoundStyle("Terraria/Sounds/Item_", SoundType.Sound);
			SoundID.PlayerKilled = new SoundStyle("Terraria/Sounds/Player_Killed", SoundType.Sound);
			soundStyle = new SoundStyle("Terraria/Sounds/Grass", SoundType.Sound)
			{
				PitchVariance = 0.6f
			};
			SoundID.Grass = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Grab", SoundType.Sound)
			{
				PitchVariance = 0.2f
			};
			SoundID.Grab = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Door_Opened", SoundType.Sound)
			{
				PitchVariance = 0.4f
			};
			SoundID.DoorOpen = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Door_Closed", SoundType.Sound)
			{
				PitchVariance = 0.4f
			};
			SoundID.DoorClosed = soundStyle;
			SoundID.MenuOpen = new SoundStyle("Terraria/Sounds/Menu_Open", SoundType.Sound);
			SoundID.MenuClose = new SoundStyle("Terraria/Sounds/Menu_Close", SoundType.Sound);
			soundStyle = new SoundStyle("Terraria/Sounds/Menu_Tick", SoundType.Sound)
			{
				PlayOnlyIfFocused = true
			};
			SoundID.MenuTick = soundStyle;
			SoundID.Shatter = new SoundStyle("Terraria/Sounds/Shatter", SoundType.Sound);
			soundStyle = new SoundStyle("Terraria/Sounds/Zombie_", 0, 3, SoundType.Sound)
			{
				Identifier = "Terraria/ZombieMoan",
				Volume = 0.4f,
				MaxInstances = 0
			};
			SoundID.ZombieMoan = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Zombie_7", SoundType.Sound)
			{
				Volume = 0.4f,
				MaxInstances = 0
			};
			SoundID.SandShark = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Zombie_", 21, 3, SoundType.Sound)
			{
				Identifier = "Terraria/BloodZombie",
				Volume = 0.4f,
				MaxInstances = 0
			};
			SoundID.BloodZombie = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Roar_0", SoundType.Sound)
			{
				Identifier = "Terraria/Roar",
				SoundLimitBehavior = SoundLimitBehavior.IgnoreNew
			};
			SoundID.Roar = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Roar_1", SoundType.Sound)
			{
				SoundLimitBehavior = SoundLimitBehavior.IgnoreNew
			};
			SoundID.WormDig = soundStyle;
			soundStyle = SoundID.WormDig;
			soundStyle.Volume = 0.25f;
			SoundID.WormDigQuiet = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Roar_2", SoundType.Sound)
			{
				SoundLimitBehavior = SoundLimitBehavior.IgnoreNew
			};
			SoundID.ScaryScream = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Double_Jump", SoundType.Sound)
			{
				PitchVariance = 0.2f
			};
			SoundID.DoubleJump = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Run", SoundType.Sound)
			{
				PitchVariance = 0.2f
			};
			SoundID.Run = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Coins", SoundType.Sound)
			{
				MaxInstances = 0
			};
			SoundID.Coins = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Splash_0", SoundType.Sound)
			{
				PitchVariance = 0.2f,
				SoundLimitBehavior = SoundLimitBehavior.IgnoreNew
			};
			SoundID.Splash = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Splash_1", SoundType.Sound)
			{
				PitchVariance = 0.2f,
				SoundLimitBehavior = SoundLimitBehavior.IgnoreNew
			};
			SoundID.SplashWeak = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Splash_2", SoundType.Sound)
			{
				Volume = 0.75f,
				PitchVariance = 0.2f,
				SoundLimitBehavior = SoundLimitBehavior.IgnoreNew
			};
			SoundID.Shimmer1 = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Splash_3", SoundType.Sound)
			{
				Volume = 0.75f,
				PitchVariance = 0.2f,
				SoundLimitBehavior = SoundLimitBehavior.IgnoreNew
			};
			SoundID.Shimmer2 = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Splash_4", SoundType.Sound)
			{
				Identifier = "Terraria/ShimmerWeak",
				Volume = 0.75f,
				Pitch = -0.1f,
				PitchVariance = 0.2f,
				SoundLimitBehavior = SoundLimitBehavior.IgnoreNew
			};
			SoundID.ShimmerWeak1 = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Splash_5", SoundType.Sound)
			{
				Identifier = "Terraria/ShimmerWeak",
				Volume = 0.75f,
				Pitch = -0.1f,
				PitchVariance = 0.2f,
				SoundLimitBehavior = SoundLimitBehavior.IgnoreNew
			};
			SoundID.ShimmerWeak2 = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Female_Hit_", 0, 3, SoundType.Sound)
			{
				LimitsArePerVariant = true
			};
			SoundID.FemaleHit = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Tink_", 0, 3, SoundType.Sound)
			{
				LimitsArePerVariant = true
			};
			SoundID.Tink = soundStyle;
			SoundID.Unlock = new SoundStyle("Terraria/Sounds/Unlock", SoundType.Sound);
			SoundID.Drown = new SoundStyle("Terraria/Sounds/Drown", SoundType.Sound);
			soundStyle = new SoundStyle("Terraria/Sounds/Chat", SoundType.Sound)
			{
				MaxInstances = 0
			};
			SoundID.Chat = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/MaxMana", SoundType.Sound)
			{
				MaxInstances = 0
			};
			SoundID.MaxMana = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Zombie_", 3, 2, SoundType.Sound)
			{
				Identifier = "Terraria/Mummy",
				Volume = 0.9f,
				PitchVariance = 0.2f,
				MaxInstances = 0
			};
			SoundID.Mummy = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Pixie", SoundType.Sound)
			{
				PitchVariance = 0.2f,
				SoundLimitBehavior = SoundLimitBehavior.IgnoreNew
			};
			SoundID.Pixie = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Mech_0", SoundType.Sound)
			{
				PitchVariance = 0.2f,
				SoundLimitBehavior = SoundLimitBehavior.IgnoreNew
			};
			SoundID.Mech = soundStyle;
			IntPtr intPtr = stackalloc byte[checked(unchecked((UIntPtr)3) * (UIntPtr)sizeof(ValueTuple<int, float>))];
			*intPtr = new ValueTuple<int, float>(10, 0.49833333f);
			*(intPtr + (IntPtr)sizeof(ValueTuple<int, float>)) = new ValueTuple<int, float>(11, 0.49833333f);
			*(intPtr + (IntPtr)2 * (IntPtr)sizeof(ValueTuple<int, float>)) = new ValueTuple<int, float>(12, 0.0033333334f);
			Span<ValueTuple<int, float>> span = new Span<ValueTuple<int, float>>(intPtr, 3);
			soundStyle = new SoundStyle("Terraria/Sounds/Zombie_", span, SoundType.Ambient)
			{
				Identifier = "Terraria/Duck",
				Volume = 0.75f,
				PitchRange = new ValueTuple<float, float>(-0.7f, 0f),
				MaxInstances = 0
			};
			SoundID.Duck = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Zombie_13", SoundType.Ambient)
			{
				Volume = 0.35f,
				PitchRange = new ValueTuple<float, float>(-0.4f, 0.2f),
				MaxInstances = 0
			};
			SoundID.Frog = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Zombie_", RuntimeHelpers.CreateSpan<int>(fieldof(<PrivateImplementationDetails>.7D2E3A0910AEAFA5F4FC82B3E6E40983F1A771379656255C403F46E587AF55DB4).FieldHandle), SoundType.Ambient)
			{
				Identifier = "Terraria/Bird",
				Volume = 0.15f,
				PitchRange = new ValueTuple<float, float>(-0.7f, 0.26f),
				SoundLimitBehavior = SoundLimitBehavior.IgnoreNew,
				LimitsArePerVariant = true
			};
			SoundID.Bird = soundStyle;
			soundStyle = SoundID.Bird;
			soundStyle.Variants = RuntimeHelpers.CreateSpan<int>(fieldof(<PrivateImplementationDetails>.01B4F6BD5D6A06A7B74A8565CEB4F845AFE0AE96A0AC05CF5E86066BF7B538EC4).FieldHandle);
			SoundID.Bird14 = soundStyle;
			soundStyle = SoundID.Bird;
			soundStyle.Variants = RuntimeHelpers.CreateSpan<int>(fieldof(<PrivateImplementationDetails>.097328E8C957DE2428283954F6A1EE8FF7AD7DEF12E100A600178407F5DECF244).FieldHandle);
			SoundID.Bird16 = soundStyle;
			soundStyle = SoundID.Bird;
			soundStyle.Variants = RuntimeHelpers.CreateSpan<int>(fieldof(<PrivateImplementationDetails>.84FC05949DC1E486652A4ED316AFB6434E9437EB30B714594A1D0B42057766024).FieldHandle);
			SoundID.Bird17 = soundStyle;
			soundStyle = SoundID.Bird;
			soundStyle.Variants = RuntimeHelpers.CreateSpan<int>(fieldof(<PrivateImplementationDetails>.7D8E29FA389A36CCA29BC0F07A7892DDDD6F9070B9E33D12DCE8CE3569F818104).FieldHandle);
			SoundID.Bird18 = soundStyle;
			soundStyle = SoundID.Bird;
			soundStyle.Variants = RuntimeHelpers.CreateSpan<int>(fieldof(<PrivateImplementationDetails>.EBA09F2F48F209CFA2DFBF19FC678D755D05559671ECEDA0164F3E080CB497654).FieldHandle);
			SoundID.Bird19 = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Zombie_15", SoundType.Ambient)
			{
				Volume = 0.2f,
				PitchRange = new ValueTuple<float, float>(-0.1f, 0.3f),
				SoundLimitBehavior = SoundLimitBehavior.IgnoreNew
			};
			SoundID.Critter = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Liquid_0", SoundType.Ambient)
			{
				Volume = 0.2f,
				SoundLimitBehavior = SoundLimitBehavior.IgnoreNew
			};
			SoundID.Waterfall = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Liquid_1", SoundType.Ambient)
			{
				Volume = 0.65f,
				SoundLimitBehavior = SoundLimitBehavior.IgnoreNew
			};
			SoundID.Lavafall = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Roar_0", SoundType.Sound)
			{
				Identifier = "Terraria/Roar",
				MaxInstances = 0
			};
			SoundID.ForceRoar = soundStyle;
			soundStyle = SoundID.ForceRoar;
			soundStyle.Pitch = 0.6f;
			SoundID.ForceRoarPitched = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Item_", 57, 2, SoundType.Sound)
			{
				Identifier = "Terraria/Meowmere",
				PitchVariance = 0.8f,
				MaxInstances = 0
			};
			SoundID.Meowmere = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Coin_", 0, 5, SoundType.Sound)
			{
				PitchVariance = 0.16f,
				MaxInstances = 0
			};
			SoundID.CoinPickup = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Drip_", 0, 2, SoundType.Ambient)
			{
				Volume = 0.5f,
				PitchVariance = 0.6f,
				MaxInstances = 0
			};
			SoundID.Drip = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Drip_2", SoundType.Ambient)
			{
				Volume = 0.5f,
				PitchVariance = 0.6f,
				MaxInstances = 0
			};
			SoundID.DripSplash = soundStyle;
			SoundID.Camera = new SoundStyle("Terraria/Sounds/Camera", SoundType.Sound);
			soundStyle = new SoundStyle("Terraria/Sounds/NPC_Killed_10", SoundType.Sound)
			{
				PitchVariance = 0.2f,
				MaxInstances = 0
			};
			SoundID.MoonLord = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Thunder_", 0, 7, SoundType.Ambient)
			{
				PitchVariance = 0.2f,
				RerollAttempts = 6,
				LimitsArePerVariant = true
			};
			SoundID.Thunder = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Zombie_", 106, 3, SoundType.Ambient)
			{
				Identifier = "Terraria/Seagull",
				Volume = 0.2f,
				PitchRange = new ValueTuple<float, float>(-0.7f, 0f),
				MaxInstances = 0
			};
			SoundID.Seagull = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Zombie_109", SoundType.Ambient)
			{
				Volume = 0.3f,
				PitchVariance = 0.2f,
				SoundLimitBehavior = SoundLimitBehavior.IgnoreNew
			};
			SoundID.Dolphin = soundStyle;
			IntPtr intPtr2 = stackalloc byte[checked(unchecked((UIntPtr)5) * (UIntPtr)sizeof(ValueTuple<int, float>))];
			*intPtr2 = new ValueTuple<int, float>(110, 0.49833333f);
			*(intPtr2 + (IntPtr)sizeof(ValueTuple<int, float>)) = new ValueTuple<int, float>(111, 0.49833333f);
			*(intPtr2 + (IntPtr)2 * (IntPtr)sizeof(ValueTuple<int, float>)) = new ValueTuple<int, float>(112, 0.0011111111f);
			*(intPtr2 + (IntPtr)3 * (IntPtr)sizeof(ValueTuple<int, float>)) = new ValueTuple<int, float>(113, 0.0011111111f);
			*(intPtr2 + (IntPtr)4 * (IntPtr)sizeof(ValueTuple<int, float>)) = new ValueTuple<int, float>(114, 0.0011111111f);
			span = new Span<ValueTuple<int, float>>(intPtr2, 5);
			soundStyle = new SoundStyle("Terraria/Sounds/Zombie_", span, SoundType.Ambient)
			{
				Identifier = "Terraria/Owl",
				Volume = 0.9f,
				PitchVariance = 0.2f,
				SoundLimitBehavior = SoundLimitBehavior.IgnoreNew
			};
			SoundID.Owl = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Item_133", SoundType.Sound)
			{
				Volume = 0.45f,
				Identifier = "Terraria/Guitar"
			};
			SoundID.GuitarC = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Item_134", SoundType.Sound)
			{
				Volume = 0.45f,
				Identifier = "Terraria/Guitar"
			};
			SoundID.GuitarD = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Item_135", SoundType.Sound)
			{
				Volume = 0.45f,
				Identifier = "Terraria/Guitar"
			};
			SoundID.GuitarEm = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Item_136", SoundType.Sound)
			{
				Volume = 0.45f,
				Identifier = "Terraria/Guitar"
			};
			SoundID.GuitarG = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Item_137", SoundType.Sound)
			{
				Volume = 0.45f,
				Identifier = "Terraria/Guitar"
			};
			SoundID.GuitarBm = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Item_138", SoundType.Sound)
			{
				Volume = 0.45f,
				Identifier = "Terraria/Guitar"
			};
			SoundID.GuitarAm = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Item_139", SoundType.Sound)
			{
				Volume = 0.7f,
				Identifier = "Terraria/Drums"
			};
			SoundID.DrumHiHat = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Item_140", SoundType.Sound)
			{
				Volume = 0.7f,
				Identifier = "Terraria/Drums"
			};
			SoundID.DrumTomHigh = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Item_141", SoundType.Sound)
			{
				Volume = 0.7f,
				Identifier = "Terraria/Drums"
			};
			SoundID.DrumTomLow = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Item_142", SoundType.Sound)
			{
				Volume = 0.7f,
				Identifier = "Terraria/Drums"
			};
			SoundID.DrumTomMid = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Item_143", SoundType.Sound)
			{
				Volume = 0.7f,
				Identifier = "Terraria/Drums"
			};
			SoundID.DrumClosedHiHat = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Item_144", SoundType.Sound)
			{
				Volume = 0.7f,
				Identifier = "Terraria/Drums"
			};
			SoundID.DrumCymbal1 = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Item_145", SoundType.Sound)
			{
				Volume = 0.7f,
				Identifier = "Terraria/Drums"
			};
			SoundID.DrumCymbal2 = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Item_146", SoundType.Sound)
			{
				Volume = 0.7f,
				Identifier = "Terraria/Drums"
			};
			SoundID.DrumKick = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Item_147", SoundType.Sound)
			{
				Volume = 0.7f,
				Identifier = "Terraria/Drums"
			};
			SoundID.DrumTamaSnare = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Item_148", SoundType.Sound)
			{
				Volume = 0.7f,
				Identifier = "Terraria/Drums"
			};
			SoundID.DrumFloorTom = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Research_", 1, 3, SoundType.Sound)
			{
				LimitsArePerVariant = true
			};
			SoundID.Research = soundStyle;
			SoundID.ResearchComplete = new SoundStyle("Terraria/Sounds/Research_0", SoundType.Sound);
			soundStyle = new SoundStyle("Terraria/Sounds/Zombie_", 115, 3, SoundType.Sound)
			{
				Identifier = "Terraria/QueenSlime",
				Volume = 0.5f,
				SoundLimitBehavior = SoundLimitBehavior.IgnoreNew
			};
			SoundID.QueenSlime = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Zombie_", 121, 3, SoundType.Sound)
			{
				Identifier = "Terraria/Clown",
				Volume = 0.45f,
				PitchVariance = 0.3f,
				SoundLimitBehavior = SoundLimitBehavior.IgnoreNew
			};
			SoundID.Clown = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Zombie_", 118, 3, SoundType.Ambient)
			{
				Identifier = "Terraria/Cockatiel",
				Volume = 0.3f,
				PitchVariance = 0.1f,
				SoundLimitBehavior = SoundLimitBehavior.IgnoreNew,
				LimitsArePerVariant = true
			};
			SoundID.Cockatiel = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Zombie_", 126, 3, SoundType.Ambient)
			{
				Identifier = "Terraria/Macaw",
				Volume = 0.22f,
				PitchVariance = 0.1f,
				SoundLimitBehavior = SoundLimitBehavior.IgnoreNew,
				LimitsArePerVariant = true
			};
			SoundID.Macaw = soundStyle;
			soundStyle = new SoundStyle("Terraria/Sounds/Zombie_", 129, 2, SoundType.Ambient)
			{
				Identifier = "Terraria/Toucan",
				Volume = 0.2f,
				PitchVariance = 0.1f,
				SoundLimitBehavior = SoundLimitBehavior.IgnoreNew,
				LimitsArePerVariant = true
			};
			SoundID.Toucan = soundStyle;
			SoundID.NPCHit1 = SoundID.NPCHitSound(1);
			SoundID.NPCHit2 = SoundID.NPCHitSound(2);
			SoundID.NPCHit3 = SoundID.NPCHitSound(3);
			SoundID.NPCHit4 = SoundID.NPCHitSound(4);
			SoundID.NPCHit5 = SoundID.NPCHitSound(5);
			SoundID.NPCHit6 = SoundID.NPCHitSound(6);
			SoundID.NPCHit7 = SoundID.NPCHitSound(7);
			SoundID.NPCHit8 = SoundID.NPCHitSound(8);
			SoundID.NPCHit9 = SoundID.NPCHitSound(9);
			SoundID.NPCHit10 = SoundID.NPCHitSound(10);
			SoundID.NPCHit11 = SoundID.NPCHitSound(11);
			SoundID.NPCHit12 = SoundID.NPCHitSound(12);
			SoundID.NPCHit13 = SoundID.NPCHitSound(13);
			SoundID.NPCHit14 = SoundID.NPCHitSound(14);
			SoundID.NPCHit15 = SoundID.NPCHitSound(15);
			SoundID.NPCHit16 = SoundID.NPCHitSound(16);
			SoundID.NPCHit17 = SoundID.NPCHitSound(17);
			SoundID.NPCHit18 = SoundID.NPCHitSound(18);
			SoundID.NPCHit19 = SoundID.NPCHitSound(19);
			soundStyle = SoundID.NPCHitSound(20);
			soundStyle.Volume = 0.5f;
			SoundID.NPCHit20 = soundStyle;
			soundStyle = SoundID.NPCHitSound(21);
			soundStyle.Volume = 0.5f;
			SoundID.NPCHit21 = soundStyle;
			soundStyle = SoundID.NPCHitSound(22);
			soundStyle.Volume = 0.5f;
			SoundID.NPCHit22 = soundStyle;
			soundStyle = SoundID.NPCHitSound(23);
			soundStyle.Volume = 0.5f;
			SoundID.NPCHit23 = soundStyle;
			soundStyle = SoundID.NPCHitSound(24);
			soundStyle.Volume = 0.5f;
			SoundID.NPCHit24 = soundStyle;
			soundStyle = SoundID.NPCHitSound(25);
			soundStyle.Volume = 0.5f;
			SoundID.NPCHit25 = soundStyle;
			soundStyle = SoundID.NPCHitSound(26);
			soundStyle.Volume = 0.5f;
			SoundID.NPCHit26 = soundStyle;
			soundStyle = SoundID.NPCHitSound(27);
			soundStyle.Volume = 0.5f;
			SoundID.NPCHit27 = soundStyle;
			soundStyle = SoundID.NPCHitSound(28);
			soundStyle.Volume = 0.5f;
			SoundID.NPCHit28 = soundStyle;
			soundStyle = SoundID.NPCHitSound(29);
			soundStyle.Volume = 0.5f;
			SoundID.NPCHit29 = soundStyle;
			soundStyle = SoundID.NPCHitSound(30);
			soundStyle.Volume = 0.5f;
			SoundID.NPCHit30 = soundStyle;
			soundStyle = SoundID.NPCHitSound(31);
			soundStyle.Volume = 0.5f;
			SoundID.NPCHit31 = soundStyle;
			soundStyle = SoundID.NPCHitSound(32);
			soundStyle.Volume = 0.5f;
			SoundID.NPCHit32 = soundStyle;
			soundStyle = SoundID.NPCHitSound(33);
			soundStyle.Volume = 0.5f;
			SoundID.NPCHit33 = soundStyle;
			soundStyle = SoundID.NPCHitSound(34);
			soundStyle.Volume = 0.5f;
			SoundID.NPCHit34 = soundStyle;
			soundStyle = SoundID.NPCHitSound(35);
			soundStyle.Volume = 0.5f;
			SoundID.NPCHit35 = soundStyle;
			soundStyle = SoundID.NPCHitSound(36);
			soundStyle.Volume = 0.5f;
			SoundID.NPCHit36 = soundStyle;
			soundStyle = SoundID.NPCHitSound(37);
			soundStyle.Volume = 0.5f;
			SoundID.NPCHit37 = soundStyle;
			soundStyle = SoundID.NPCHitSound(38);
			soundStyle.Volume = 0.5f;
			SoundID.NPCHit38 = soundStyle;
			soundStyle = SoundID.NPCHitSound(39);
			soundStyle.Volume = 0.5f;
			SoundID.NPCHit39 = soundStyle;
			soundStyle = SoundID.NPCHitSound(40);
			soundStyle.Volume = 0.5f;
			SoundID.NPCHit40 = soundStyle;
			soundStyle = SoundID.NPCHitSound(41);
			soundStyle.Volume = 0.5f;
			SoundID.NPCHit41 = soundStyle;
			soundStyle = SoundID.NPCHitSound(42);
			soundStyle.Volume = 0.5f;
			SoundID.NPCHit42 = soundStyle;
			soundStyle = SoundID.NPCHitSound(43);
			soundStyle.Volume = 0.5f;
			SoundID.NPCHit43 = soundStyle;
			soundStyle = SoundID.NPCHitSound(44);
			soundStyle.Volume = 0.5f;
			SoundID.NPCHit44 = soundStyle;
			soundStyle = SoundID.NPCHitSound(45);
			soundStyle.Volume = 0.5f;
			SoundID.NPCHit45 = soundStyle;
			soundStyle = SoundID.NPCHitSound(46);
			soundStyle.Volume = 0.5f;
			SoundID.NPCHit46 = soundStyle;
			soundStyle = SoundID.NPCHitSound(47);
			soundStyle.Volume = 0.5f;
			SoundID.NPCHit47 = soundStyle;
			soundStyle = SoundID.NPCHitSound(48);
			soundStyle.Volume = 0.5f;
			SoundID.NPCHit48 = soundStyle;
			soundStyle = SoundID.NPCHitSound(49);
			soundStyle.Volume = 0.5f;
			SoundID.NPCHit49 = soundStyle;
			soundStyle = SoundID.NPCHitSound(50);
			soundStyle.Volume = 0.5f;
			SoundID.NPCHit50 = soundStyle;
			soundStyle = SoundID.NPCHitSound(51);
			soundStyle.Volume = 0.5f;
			SoundID.NPCHit51 = soundStyle;
			soundStyle = SoundID.NPCHitSound(52);
			soundStyle.Volume = 0.5f;
			SoundID.NPCHit52 = soundStyle;
			soundStyle = SoundID.NPCHitSound(53);
			soundStyle.Volume = 0.5f;
			SoundID.NPCHit53 = soundStyle;
			soundStyle = SoundID.NPCHitSound(54);
			soundStyle.Volume = 0.5f;
			SoundID.NPCHit54 = soundStyle;
			soundStyle = SoundID.NPCHitSound(55);
			soundStyle.Volume = 0.5f;
			SoundID.NPCHit55 = soundStyle;
			soundStyle = SoundID.NPCHitSound(56);
			soundStyle.Volume = 0.5f;
			SoundID.NPCHit56 = soundStyle;
			soundStyle = SoundID.NPCHitSound(57);
			soundStyle.Volume = 0.6f;
			soundStyle.SoundLimitBehavior = SoundLimitBehavior.IgnoreNew;
			SoundID.NPCHit57 = soundStyle;
			SoundID.NPCDeath1 = SoundID.NPCDeathSound(1);
			SoundID.NPCDeath2 = SoundID.NPCDeathSound(2);
			SoundID.NPCDeath3 = SoundID.NPCDeathSound(3);
			SoundID.NPCDeath4 = SoundID.NPCDeathSound(4);
			SoundID.NPCDeath5 = SoundID.NPCDeathSound(5);
			SoundID.NPCDeath6 = SoundID.NPCDeathSound(6);
			SoundID.NPCDeath7 = SoundID.NPCDeathSound(7);
			SoundID.NPCDeath8 = SoundID.NPCDeathSound(8);
			SoundID.NPCDeath9 = SoundID.NPCDeathSound(9);
			soundStyle = SoundID.NPCDeathSound(10);
			soundStyle.SoundLimitBehavior = SoundLimitBehavior.IgnoreNew;
			SoundID.NPCDeath10 = soundStyle;
			SoundID.NPCDeath11 = SoundID.NPCDeathSound(11);
			SoundID.NPCDeath12 = SoundID.NPCDeathSound(12);
			SoundID.NPCDeath13 = SoundID.NPCDeathSound(13);
			SoundID.NPCDeath14 = SoundID.NPCDeathSound(14);
			SoundID.NPCDeath15 = SoundID.NPCDeathSound(15);
			SoundID.NPCDeath16 = SoundID.NPCDeathSound(16);
			SoundID.NPCDeath17 = SoundID.NPCDeathSound(17);
			SoundID.NPCDeath18 = SoundID.NPCDeathSound(18);
			SoundID.NPCDeath19 = SoundID.NPCDeathSound(19);
			SoundID.NPCDeath20 = SoundID.NPCDeathSound(20);
			SoundID.NPCDeath21 = SoundID.NPCDeathSound(21);
			SoundID.NPCDeath22 = SoundID.NPCDeathSound(22);
			soundStyle = SoundID.NPCDeathSound(23);
			soundStyle.Volume = 0.5f;
			SoundID.NPCDeath23 = soundStyle;
			soundStyle = SoundID.NPCDeathSound(24);
			soundStyle.Volume = 0.5f;
			SoundID.NPCDeath24 = soundStyle;
			soundStyle = SoundID.NPCDeathSound(25);
			soundStyle.Volume = 0.5f;
			SoundID.NPCDeath25 = soundStyle;
			soundStyle = SoundID.NPCDeathSound(26);
			soundStyle.Volume = 0.5f;
			SoundID.NPCDeath26 = soundStyle;
			soundStyle = SoundID.NPCDeathSound(27);
			soundStyle.Volume = 0.5f;
			SoundID.NPCDeath27 = soundStyle;
			soundStyle = SoundID.NPCDeathSound(28);
			soundStyle.Volume = 0.5f;
			SoundID.NPCDeath28 = soundStyle;
			soundStyle = SoundID.NPCDeathSound(29);
			soundStyle.Volume = 0.5f;
			SoundID.NPCDeath29 = soundStyle;
			soundStyle = SoundID.NPCDeathSound(30);
			soundStyle.Volume = 0.5f;
			SoundID.NPCDeath30 = soundStyle;
			soundStyle = SoundID.NPCDeathSound(31);
			soundStyle.Volume = 0.5f;
			SoundID.NPCDeath31 = soundStyle;
			soundStyle = SoundID.NPCDeathSound(32);
			soundStyle.Volume = 0.5f;
			SoundID.NPCDeath32 = soundStyle;
			soundStyle = SoundID.NPCDeathSound(33);
			soundStyle.Volume = 0.5f;
			SoundID.NPCDeath33 = soundStyle;
			soundStyle = SoundID.NPCDeathSound(34);
			soundStyle.Volume = 0.5f;
			SoundID.NPCDeath34 = soundStyle;
			soundStyle = SoundID.NPCDeathSound(35);
			soundStyle.Volume = 0.5f;
			SoundID.NPCDeath35 = soundStyle;
			soundStyle = SoundID.NPCDeathSound(36);
			soundStyle.Volume = 0.5f;
			SoundID.NPCDeath36 = soundStyle;
			soundStyle = SoundID.NPCDeathSound(37);
			soundStyle.Volume = 0.5f;
			SoundID.NPCDeath37 = soundStyle;
			soundStyle = SoundID.NPCDeathSound(38);
			soundStyle.Volume = 0.5f;
			SoundID.NPCDeath38 = soundStyle;
			soundStyle = SoundID.NPCDeathSound(39);
			soundStyle.Volume = 0.5f;
			SoundID.NPCDeath39 = soundStyle;
			soundStyle = SoundID.NPCDeathSound(40);
			soundStyle.Volume = 0.5f;
			SoundID.NPCDeath40 = soundStyle;
			soundStyle = SoundID.NPCDeathSound(41);
			soundStyle.Volume = 0.5f;
			SoundID.NPCDeath41 = soundStyle;
			soundStyle = SoundID.NPCDeathSound(42);
			soundStyle.Volume = 0.5f;
			SoundID.NPCDeath42 = soundStyle;
			soundStyle = SoundID.NPCDeathSound(43);
			soundStyle.Volume = 0.5f;
			SoundID.NPCDeath43 = soundStyle;
			soundStyle = SoundID.NPCDeathSound(44);
			soundStyle.Volume = 0.5f;
			SoundID.NPCDeath44 = soundStyle;
			soundStyle = SoundID.NPCDeathSound(45);
			soundStyle.Volume = 0.5f;
			SoundID.NPCDeath45 = soundStyle;
			soundStyle = SoundID.NPCDeathSound(46);
			soundStyle.Volume = 0.5f;
			SoundID.NPCDeath46 = soundStyle;
			soundStyle = SoundID.NPCDeathSound(47);
			soundStyle.Volume = 0.5f;
			SoundID.NPCDeath47 = soundStyle;
			soundStyle = SoundID.NPCDeathSound(48);
			soundStyle.Volume = 0.5f;
			SoundID.NPCDeath48 = soundStyle;
			soundStyle = SoundID.NPCDeathSound(49);
			soundStyle.Volume = 0.5f;
			SoundID.NPCDeath49 = soundStyle;
			soundStyle = SoundID.NPCDeathSound(50);
			soundStyle.Volume = 0.5f;
			SoundID.NPCDeath50 = soundStyle;
			soundStyle = SoundID.NPCDeathSound(51);
			soundStyle.Volume = 0.5f;
			SoundID.NPCDeath51 = soundStyle;
			soundStyle = SoundID.NPCDeathSound(52);
			soundStyle.Volume = 0.5f;
			SoundID.NPCDeath52 = soundStyle;
			soundStyle = SoundID.NPCDeathSound(53);
			soundStyle.Volume = 0.5f;
			SoundID.NPCDeath53 = soundStyle;
			soundStyle = SoundID.NPCDeathSound(54);
			soundStyle.Volume = 0.5f;
			SoundID.NPCDeath54 = soundStyle;
			soundStyle = SoundID.NPCDeathSound(55);
			soundStyle.Volume = 0.5f;
			SoundID.NPCDeath55 = soundStyle;
			soundStyle = SoundID.NPCDeathSound(56);
			soundStyle.Volume = 0.5f;
			SoundID.NPCDeath56 = soundStyle;
			soundStyle = SoundID.NPCDeathSound(57);
			soundStyle.Volume = 0.5f;
			SoundID.NPCDeath57 = soundStyle;
			SoundID.NPCDeath58 = SoundID.NPCDeathSound(58);
			SoundID.NPCDeath59 = SoundID.NPCDeathSound(59);
			SoundID.NPCDeath60 = SoundID.NPCDeathSound(60);
			soundStyle = SoundID.NPCDeathSound(61);
			soundStyle.Volume = 0.6f;
			SoundID.NPCDeath61 = soundStyle;
			soundStyle = SoundID.NPCDeathSound(62);
			soundStyle.Volume = 0.6f;
			SoundID.NPCDeath62 = soundStyle;
			SoundID.NPCDeath63 = SoundID.NPCDeathSound(63);
			SoundID.NPCDeath64 = SoundID.NPCDeathSound(64);
			SoundID.NPCDeath65 = SoundID.NPCDeathSound(65);
			SoundID.NPCDeath66 = SoundID.NPCDeathSound(66);
			IntPtr intPtr3 = stackalloc byte[(UIntPtr)12];
			*intPtr3 = 1;
			*(intPtr3 + 4) = 18;
			*(intPtr3 + (IntPtr)2 * 4) = 19;
			SoundID.Item1 = SoundID.ItemSound(new Span<int>(intPtr3, 3));
			SoundID.Item2 = SoundID.ItemSound(2);
			SoundID.Item3 = SoundID.ItemSound(3);
			SoundID.Item4 = SoundID.ItemSound(4);
			SoundID.Item5 = SoundID.ItemSound(5);
			SoundID.Item6 = SoundID.ItemSound(6);
			SoundID.Item7 = SoundID.ItemSound(7);
			SoundID.Item8 = SoundID.ItemSound(8);
			soundStyle = SoundID.ItemSound(9);
			soundStyle.MaxInstances = 0;
			SoundID.Item9 = soundStyle;
			soundStyle = SoundID.ItemSound(10);
			soundStyle.MaxInstances = 0;
			SoundID.Item10 = soundStyle;
			SoundID.Item11 = SoundID.ItemSound(11);
			SoundID.Item12 = SoundID.ItemSound(12);
			SoundID.Item13 = SoundID.ItemSound(13);
			SoundID.Item14 = SoundID.ItemSound(14);
			SoundID.Item15 = SoundID.ItemSound(15);
			SoundID.Item16 = SoundID.ItemSound(16);
			SoundID.Item17 = SoundID.ItemSound(17);
			SoundID.Item18 = SoundID.ItemSound(18);
			SoundID.Item19 = SoundID.ItemSound(19);
			SoundID.Item20 = SoundID.ItemSound(20);
			SoundID.Item21 = SoundID.ItemSound(21);
			SoundID.Item22 = SoundID.ItemSound(22);
			SoundID.Item23 = SoundID.ItemSound(23);
			soundStyle = SoundID.ItemSound(24);
			soundStyle.MaxInstances = 0;
			SoundID.Item24 = soundStyle;
			SoundID.Item25 = SoundID.ItemSound(25);
			soundStyle = SoundID.ItemSound(26);
			soundStyle.Volume = 0.75f;
			soundStyle.PitchVariance = 0f;
			soundStyle.UsesMusicPitch = true;
			soundStyle.MaxInstances = 0;
			SoundID.Item26 = soundStyle;
			SoundID.Item27 = SoundID.ItemSound(27);
			SoundID.Item28 = SoundID.ItemSound(28);
			SoundID.Item29 = SoundID.ItemSound(29);
			SoundID.Item30 = SoundID.ItemSound(30);
			SoundID.Item31 = SoundID.ItemSound(31);
			SoundID.Item32 = SoundID.ItemSound(32);
			SoundID.Item33 = SoundID.ItemSound(33);
			soundStyle = SoundID.ItemSound(34);
			soundStyle.MaxInstances = 0;
			SoundID.Item34 = soundStyle;
			soundStyle = SoundID.ItemSound(35);
			soundStyle.Volume = 0.75f;
			soundStyle.PitchVariance = 0f;
			soundStyle.UsesMusicPitch = true;
			SoundID.Item35 = soundStyle;
			SoundID.Item36 = SoundID.ItemSound(36);
			soundStyle = SoundID.ItemSound(37);
			soundStyle.Volume = 0.5f;
			SoundID.Item37 = soundStyle;
			SoundID.Item38 = SoundID.ItemSound(38);
			SoundID.Item39 = SoundID.ItemSound(39);
			SoundID.Item40 = SoundID.ItemSound(40);
			SoundID.Item41 = SoundID.ItemSound(41);
			SoundID.Item42 = SoundID.ItemSound(42);
			soundStyle = SoundID.ItemSound(43);
			soundStyle.MaxInstances = 0;
			SoundID.Item43 = soundStyle;
			SoundID.Item44 = SoundID.ItemSound(44);
			SoundID.Item45 = SoundID.ItemSound(45);
			SoundID.Item46 = SoundID.ItemSound(46);
			soundStyle = SoundID.ItemSound(47);
			soundStyle.Volume = 0.75f;
			soundStyle.PitchVariance = 0f;
			soundStyle.UsesMusicPitch = true;
			SoundID.Item47 = soundStyle;
			SoundID.Item48 = SoundID.ItemSound(48);
			SoundID.Item49 = SoundID.ItemSound(49);
			SoundID.Item50 = SoundID.ItemSound(50);
			SoundID.Item51 = SoundID.ItemSound(51);
			soundStyle = SoundID.ItemSound(52);
			soundStyle.Volume = 0.35f;
			SoundID.Item52 = soundStyle;
			soundStyle = SoundID.ItemSound(53);
			soundStyle.Volume = 0.75f;
			soundStyle.PitchRange = new ValueTuple<float, float>(-0.4f, -0.2f);
			soundStyle.SoundLimitBehavior = SoundLimitBehavior.IgnoreNew;
			SoundID.Item53 = soundStyle;
			SoundID.Item54 = SoundID.ItemSound(54);
			soundStyle = SoundID.ItemSound(55);
			soundStyle.Volume = 0.5625f;
			soundStyle.PitchRange = new ValueTuple<float, float>(0.2f, 0.4f);
			soundStyle.SoundLimitBehavior = SoundLimitBehavior.IgnoreNew;
			SoundID.Item55 = soundStyle;
			SoundID.Item56 = SoundID.ItemSound(56);
			SoundID.Item57 = SoundID.ItemSound(57);
			SoundID.Item58 = SoundID.ItemSound(58);
			SoundID.Item59 = SoundID.ItemSound(59);
			SoundID.Item60 = SoundID.ItemSound(60);
			SoundID.Item61 = SoundID.ItemSound(61);
			SoundID.Item62 = SoundID.ItemSound(62);
			SoundID.Item63 = SoundID.ItemSound(63);
			SoundID.Item64 = SoundID.ItemSound(64);
			SoundID.Item65 = SoundID.ItemSound(65);
			SoundID.Item66 = SoundID.ItemSound(66);
			SoundID.Item67 = SoundID.ItemSound(67);
			SoundID.Item68 = SoundID.ItemSound(68);
			SoundID.Item69 = SoundID.ItemSound(69);
			SoundID.Item70 = SoundID.ItemSound(70);
			SoundID.Item71 = SoundID.ItemSound(71);
			SoundID.Item72 = SoundID.ItemSound(72);
			SoundID.Item73 = SoundID.ItemSound(73);
			SoundID.Item74 = SoundID.ItemSound(74);
			SoundID.Item75 = SoundID.ItemSound(75);
			SoundID.Item76 = SoundID.ItemSound(76);
			SoundID.Item77 = SoundID.ItemSound(77);
			SoundID.Item78 = SoundID.ItemSound(78);
			SoundID.Item79 = SoundID.ItemSound(79);
			SoundID.Item80 = SoundID.ItemSound(80);
			SoundID.Item81 = SoundID.ItemSound(81);
			SoundID.Item82 = SoundID.ItemSound(82);
			SoundID.Item83 = SoundID.ItemSound(83);
			SoundID.Item84 = SoundID.ItemSound(84);
			SoundID.Item85 = SoundID.ItemSound(85);
			SoundID.Item86 = SoundID.ItemSound(86);
			SoundID.Item87 = SoundID.ItemSound(87);
			SoundID.Item88 = SoundID.ItemSound(88);
			SoundID.Item89 = SoundID.ItemSound(89);
			SoundID.Item90 = SoundID.ItemSound(90);
			SoundID.Item91 = SoundID.ItemSound(91);
			SoundID.Item92 = SoundID.ItemSound(92);
			SoundID.Item93 = SoundID.ItemSound(93);
			SoundID.Item94 = SoundID.ItemSound(94);
			SoundID.Item95 = SoundID.ItemSound(95);
			SoundID.Item96 = SoundID.ItemSound(96);
			SoundID.Item97 = SoundID.ItemSound(97);
			SoundID.Item98 = SoundID.ItemSound(98);
			SoundID.Item99 = SoundID.ItemSound(99);
			SoundID.Item100 = SoundID.ItemSound(100);
			SoundID.Item101 = SoundID.ItemSound(101);
			SoundID.Item102 = SoundID.ItemSound(102);
			soundStyle = SoundID.ItemSound(103);
			soundStyle.MaxInstances = 0;
			SoundID.Item103 = soundStyle;
			SoundID.Item104 = SoundID.ItemSound(104);
			SoundID.Item105 = SoundID.ItemSound(105);
			SoundID.Item106 = SoundID.ItemSound(106);
			SoundID.Item107 = SoundID.ItemSound(107);
			SoundID.Item108 = SoundID.ItemSound(108);
			SoundID.Item109 = SoundID.ItemSound(109);
			SoundID.Item110 = SoundID.ItemSound(110);
			SoundID.Item111 = SoundID.ItemSound(111);
			SoundID.Item112 = SoundID.ItemSound(112);
			SoundID.Item113 = SoundID.ItemSound(113);
			SoundID.Item114 = SoundID.ItemSound(114);
			SoundID.Item115 = SoundID.ItemSound(115);
			soundStyle = SoundID.ItemSound(116);
			soundStyle.Volume = 0.5f;
			SoundID.Item116 = soundStyle;
			SoundID.Item117 = SoundID.ItemSound(117);
			SoundID.Item118 = SoundID.ItemSound(118);
			SoundID.Item119 = SoundID.ItemSound(119);
			SoundID.Item120 = SoundID.ItemSound(120);
			SoundID.Item121 = SoundID.ItemSound(121);
			SoundID.Item122 = SoundID.ItemSound(122);
			soundStyle = SoundID.ItemSound(123);
			soundStyle.Volume = 0.5f;
			SoundID.Item123 = soundStyle;
			soundStyle = SoundID.ItemSound(124);
			soundStyle.Volume = 0.65f;
			SoundID.Item124 = soundStyle;
			soundStyle = SoundID.ItemSound(125);
			soundStyle.Volume = 0.65f;
			SoundID.Item125 = soundStyle;
			SoundID.Item126 = SoundID.ItemSound(126);
			SoundID.Item127 = SoundID.ItemSound(127);
			SoundID.Item128 = SoundID.ItemSound(128);
			soundStyle = SoundID.ItemSound(129);
			soundStyle.Volume = 0.6f;
			SoundID.Item129 = soundStyle;
			SoundID.Item130 = SoundID.ItemSound(130);
			SoundID.Item131 = SoundID.ItemSound(131);
			soundStyle = SoundID.ItemSound(132);
			soundStyle.PitchVariance = 0.04f;
			SoundID.Item132 = soundStyle;
			SoundID.Item133 = SoundID.ItemSound(133);
			SoundID.Item134 = SoundID.ItemSound(134);
			SoundID.Item135 = SoundID.ItemSound(135);
			SoundID.Item136 = SoundID.ItemSound(136);
			SoundID.Item137 = SoundID.ItemSound(137);
			SoundID.Item138 = SoundID.ItemSound(138);
			SoundID.Item139 = SoundID.ItemSound(139);
			SoundID.Item140 = SoundID.ItemSound(140);
			SoundID.Item141 = SoundID.ItemSound(141);
			SoundID.Item142 = SoundID.ItemSound(142);
			SoundID.Item143 = SoundID.ItemSound(143);
			SoundID.Item144 = SoundID.ItemSound(144);
			SoundID.Item145 = SoundID.ItemSound(145);
			SoundID.Item146 = SoundID.ItemSound(146);
			SoundID.Item147 = SoundID.ItemSound(147);
			SoundID.Item148 = SoundID.ItemSound(148);
			SoundID.Item149 = SoundID.ItemSound(149);
			SoundID.Item150 = SoundID.ItemSound(150);
			SoundID.Item151 = SoundID.ItemSound(151);
			SoundID.Item152 = SoundID.ItemSound(152);
			soundStyle = SoundID.ItemSound(153);
			soundStyle.PitchVariance = 0.3f;
			SoundID.Item153 = soundStyle;
			SoundID.Item154 = SoundID.ItemSound(154);
			SoundID.Item155 = SoundID.ItemSound(155);
			soundStyle = SoundID.ItemSound(156);
			soundStyle.Volume = 0.6f;
			soundStyle.PitchVariance = 0.2f;
			soundStyle.MaxInstances = 0;
			SoundID.Item156 = soundStyle;
			soundStyle = SoundID.ItemSound(157);
			soundStyle.Volume = 0.7f;
			SoundID.Item157 = soundStyle;
			soundStyle = SoundID.ItemSound(158);
			soundStyle.Volume = 0.8f;
			SoundID.Item158 = soundStyle;
			soundStyle = SoundID.ItemSound(159);
			soundStyle.Volume = 0.75f;
			soundStyle.SoundLimitBehavior = SoundLimitBehavior.IgnoreNew;
			SoundID.Item159 = soundStyle;
			SoundID.Item160 = SoundID.ItemSound(160);
			SoundID.Item161 = SoundID.ItemSound(161);
			soundStyle = SoundID.ItemSound(162);
			soundStyle.MaxInstances = 0;
			SoundID.Item162 = soundStyle;
			SoundID.Item163 = SoundID.ItemSound(163);
			SoundID.Item164 = SoundID.ItemSound(164);
			SoundID.Item165 = SoundID.ItemSound(165);
			SoundID.Item166 = SoundID.ItemSound(166);
			SoundID.Item167 = SoundID.ItemSound(167);
			SoundID.Item168 = SoundID.ItemSound(168);
			soundStyle = SoundID.ItemSound(169);
			soundStyle.Pitch = -0.8f;
			SoundID.Item169 = soundStyle;
			SoundID.Item170 = SoundID.ItemSound(170);
			SoundID.Item171 = SoundID.ItemSound(171);
			SoundID.Item172 = SoundID.ItemSound(172);
			SoundID.Item173 = SoundID.ItemSound(173);
			SoundID.Item174 = SoundID.ItemSound(174);
			SoundID.Item175 = SoundID.ItemSound(175);
			soundStyle = SoundID.ItemSound(176);
			soundStyle.Volume = 0.9f;
			SoundID.Item176 = soundStyle;
			SoundID.Item177 = SoundID.ItemSound(177);
			SoundID.Item178 = SoundID.ItemSound(178);
			SoundID.Zombie1 = SoundID.ZombieSound(1);
			SoundID.Zombie2 = SoundID.ZombieSound(2);
			SoundID.Zombie3 = SoundID.ZombieSound(3);
			SoundID.Zombie4 = SoundID.ZombieSound(4);
			SoundID.Zombie5 = SoundID.ZombieSound(5);
			SoundID.Zombie6 = SoundID.ZombieSound(6);
			SoundID.Zombie7 = SoundID.ZombieSound(7);
			SoundID.Zombie8 = SoundID.ZombieSound(8);
			SoundID.Zombie9 = SoundID.ZombieSound(9);
			SoundID.Zombie10 = SoundID.ZombieSound(10);
			SoundID.Zombie11 = SoundID.ZombieSound(11);
			SoundID.Zombie12 = SoundID.ZombieSound(12);
			SoundID.Zombie13 = SoundID.ZombieSound(13);
			SoundID.Zombie14 = SoundID.ZombieSound(14);
			SoundID.Zombie15 = SoundID.ZombieSound(15);
			SoundID.Zombie16 = SoundID.ZombieSound(16);
			SoundID.Zombie17 = SoundID.ZombieSound(17);
			SoundID.Zombie18 = SoundID.ZombieSound(18);
			SoundID.Zombie19 = SoundID.ZombieSound(19);
			SoundID.Zombie20 = SoundID.ZombieSound(20);
			SoundID.Zombie21 = SoundID.ZombieSound(21);
			SoundID.Zombie22 = SoundID.ZombieSound(22);
			SoundID.Zombie23 = SoundID.ZombieSound(23);
			soundStyle = SoundID.ZombieSound(24);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie24 = soundStyle;
			soundStyle = SoundID.ZombieSound(25);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie25 = soundStyle;
			soundStyle = SoundID.ZombieSound(26);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie26 = soundStyle;
			soundStyle = SoundID.ZombieSound(27);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie27 = soundStyle;
			soundStyle = SoundID.ZombieSound(28);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie28 = soundStyle;
			soundStyle = SoundID.ZombieSound(29);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie29 = soundStyle;
			soundStyle = SoundID.ZombieSound(30);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie30 = soundStyle;
			soundStyle = SoundID.ZombieSound(31);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie31 = soundStyle;
			soundStyle = SoundID.ZombieSound(32);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie32 = soundStyle;
			soundStyle = SoundID.ZombieSound(33);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie33 = soundStyle;
			soundStyle = SoundID.ZombieSound(34);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie34 = soundStyle;
			soundStyle = SoundID.ZombieSound(35);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie35 = soundStyle;
			soundStyle = SoundID.ZombieSound(36);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie36 = soundStyle;
			soundStyle = SoundID.ZombieSound(37);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie37 = soundStyle;
			soundStyle = SoundID.ZombieSound(38);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie38 = soundStyle;
			soundStyle = SoundID.ZombieSound(39);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie39 = soundStyle;
			soundStyle = SoundID.ZombieSound(40);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie40 = soundStyle;
			soundStyle = SoundID.ZombieSound(41);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie41 = soundStyle;
			soundStyle = SoundID.ZombieSound(42);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie42 = soundStyle;
			soundStyle = SoundID.ZombieSound(43);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie43 = soundStyle;
			soundStyle = SoundID.ZombieSound(44);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie44 = soundStyle;
			soundStyle = SoundID.ZombieSound(45);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie45 = soundStyle;
			soundStyle = SoundID.ZombieSound(46);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie46 = soundStyle;
			soundStyle = SoundID.ZombieSound(47);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie47 = soundStyle;
			soundStyle = SoundID.ZombieSound(48);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie48 = soundStyle;
			soundStyle = SoundID.ZombieSound(49);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie49 = soundStyle;
			soundStyle = SoundID.ZombieSound(50);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie50 = soundStyle;
			soundStyle = SoundID.ZombieSound(51);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie51 = soundStyle;
			soundStyle = SoundID.ZombieSound(52);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie52 = soundStyle;
			soundStyle = SoundID.ZombieSound(53);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie53 = soundStyle;
			soundStyle = SoundID.ZombieSound(54);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie54 = soundStyle;
			soundStyle = SoundID.ZombieSound(55);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie55 = soundStyle;
			soundStyle = SoundID.ZombieSound(56);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie56 = soundStyle;
			soundStyle = SoundID.ZombieSound(57);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie57 = soundStyle;
			soundStyle = SoundID.ZombieSound(58);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie58 = soundStyle;
			soundStyle = SoundID.ZombieSound(59);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie59 = soundStyle;
			soundStyle = SoundID.ZombieSound(60);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie60 = soundStyle;
			soundStyle = SoundID.ZombieSound(61);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie61 = soundStyle;
			soundStyle = SoundID.ZombieSound(62);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie62 = soundStyle;
			soundStyle = SoundID.ZombieSound(63);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie63 = soundStyle;
			soundStyle = SoundID.ZombieSound(64);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie64 = soundStyle;
			soundStyle = SoundID.ZombieSound(65);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie65 = soundStyle;
			soundStyle = SoundID.ZombieSound(66);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie66 = soundStyle;
			soundStyle = SoundID.ZombieSound(67);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie67 = soundStyle;
			soundStyle = SoundID.ZombieSound(68);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie68 = soundStyle;
			soundStyle = SoundID.ZombieSound(69);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie69 = soundStyle;
			soundStyle = SoundID.ZombieSound(70);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie70 = soundStyle;
			soundStyle = SoundID.ZombieSound(71);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie71 = soundStyle;
			soundStyle = SoundID.ZombieSound(72);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie72 = soundStyle;
			soundStyle = SoundID.ZombieSound(73);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie73 = soundStyle;
			soundStyle = SoundID.ZombieSound(74);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie74 = soundStyle;
			soundStyle = SoundID.ZombieSound(75);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie75 = soundStyle;
			soundStyle = SoundID.ZombieSound(76);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie76 = soundStyle;
			soundStyle = SoundID.ZombieSound(77);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie77 = soundStyle;
			soundStyle = SoundID.ZombieSound(78);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie78 = soundStyle;
			soundStyle = SoundID.ZombieSound(79);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie79 = soundStyle;
			soundStyle = SoundID.ZombieSound(80);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie80 = soundStyle;
			soundStyle = SoundID.ZombieSound(81);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie81 = soundStyle;
			soundStyle = SoundID.ZombieSound(82);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie82 = soundStyle;
			soundStyle = SoundID.ZombieSound(83);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie83 = soundStyle;
			soundStyle = SoundID.ZombieSound(84);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie84 = soundStyle;
			soundStyle = SoundID.ZombieSound(85);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie85 = soundStyle;
			soundStyle = SoundID.ZombieSound(86);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie86 = soundStyle;
			soundStyle = SoundID.ZombieSound(87);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie87 = soundStyle;
			soundStyle = SoundID.ZombieSound(88);
			soundStyle.Volume = 0.7f;
			SoundID.Zombie88 = soundStyle;
			soundStyle = SoundID.ZombieSound(89);
			soundStyle.Volume = 0.7f;
			SoundID.Zombie89 = soundStyle;
			soundStyle = SoundID.ZombieSound(90);
			soundStyle.Volume = 0.7f;
			SoundID.Zombie90 = soundStyle;
			soundStyle = SoundID.ZombieSound(91);
			soundStyle.Volume = 0.7f;
			SoundID.Zombie91 = soundStyle;
			soundStyle = SoundID.ZombieSound(92);
			soundStyle.Volume = 0.5f;
			SoundID.Zombie92 = soundStyle;
			soundStyle = SoundID.ZombieSound(93);
			soundStyle.Volume = 0.4f;
			SoundID.Zombie93 = soundStyle;
			soundStyle = SoundID.ZombieSound(94);
			soundStyle.Volume = 0.4f;
			SoundID.Zombie94 = soundStyle;
			soundStyle = SoundID.ZombieSound(95);
			soundStyle.Volume = 0.4f;
			SoundID.Zombie95 = soundStyle;
			soundStyle = SoundID.ZombieSound(96);
			soundStyle.Volume = 0.4f;
			SoundID.Zombie96 = soundStyle;
			soundStyle = SoundID.ZombieSound(97);
			soundStyle.Volume = 0.4f;
			SoundID.Zombie97 = soundStyle;
			soundStyle = SoundID.ZombieSound(98);
			soundStyle.Volume = 0.4f;
			SoundID.Zombie98 = soundStyle;
			soundStyle = SoundID.ZombieSound(99);
			soundStyle.Volume = 0.4f;
			SoundID.Zombie99 = soundStyle;
			soundStyle = SoundID.ZombieSound(100);
			soundStyle.Volume = 0.25f;
			SoundID.Zombie100 = soundStyle;
			soundStyle = SoundID.ZombieSound(101);
			soundStyle.Volume = 0.25f;
			SoundID.Zombie101 = soundStyle;
			soundStyle = SoundID.ZombieSound(102);
			soundStyle.Volume = 0.4f;
			SoundID.Zombie102 = soundStyle;
			soundStyle = SoundID.ZombieSound(103);
			soundStyle.Volume = 0.4f;
			SoundID.Zombie103 = soundStyle;
			soundStyle = SoundID.ZombieSound(104);
			soundStyle.Volume = 0.55f;
			SoundID.Zombie104 = soundStyle;
			SoundID.Zombie105 = SoundID.ZombieSound(105);
			SoundID.Zombie106 = SoundID.ZombieSound(106);
			SoundID.Zombie107 = SoundID.ZombieSound(107);
			SoundID.Zombie108 = SoundID.ZombieSound(108);
			SoundID.Zombie109 = SoundID.ZombieSound(109);
			SoundID.Zombie110 = SoundID.ZombieSound(110);
			SoundID.Zombie111 = SoundID.ZombieSound(111);
			SoundID.Zombie112 = SoundID.ZombieSound(112);
			SoundID.Zombie113 = SoundID.ZombieSound(113);
			SoundID.Zombie114 = SoundID.ZombieSound(114);
			SoundID.Zombie115 = SoundID.ZombieSound(115);
			SoundID.Zombie116 = SoundID.ZombieSound(116);
			SoundID.Zombie117 = SoundID.ZombieSound(117);
			SoundID.Zombie118 = SoundID.ZombieSound(118);
			SoundID.Zombie119 = SoundID.ZombieSound(119);
			SoundID.Zombie120 = SoundID.ZombieSound(120);
			SoundID.Zombie121 = SoundID.ZombieSound(121);
			SoundID.Zombie122 = SoundID.ZombieSound(122);
			SoundID.Zombie123 = SoundID.ZombieSound(123);
			SoundID.Zombie124 = SoundID.ZombieSound(124);
			SoundID.Zombie125 = SoundID.ZombieSound(125);
			SoundID.Zombie126 = SoundID.ZombieSound(126);
			SoundID.Zombie127 = SoundID.ZombieSound(127);
			SoundID.Zombie128 = SoundID.ZombieSound(128);
			SoundID.Zombie129 = SoundID.ZombieSound(129);
			SoundID.Zombie130 = SoundID.ZombieSound(130);
			SoundID.ZombieSoundCount = 131;
			SoundID.legacyArrayedStylesMapping = new SoundStyle[LegacySoundIDs.Count][];
			SoundID.FillLegacyArrayedStylesMap();
		}

		// Token: 0x0600357A RID: 13690 RVA: 0x00576D34 File Offset: 0x00574F34
		internal static bool TryGetLegacyStyle(int type, int style, out SoundStyle result)
		{
			SoundStyle? tempResult = SoundID.GetLegacyStyle(type, style);
			if (tempResult != null)
			{
				result = tempResult.Value;
				return true;
			}
			result = default(SoundStyle);
			return false;
		}

		// Token: 0x0600357B RID: 13691 RVA: 0x00576D6C File Offset: 0x00574F6C
		private static void FillLegacyArrayedStylesMap()
		{
			SoundID.<FillLegacyArrayedStylesMap>g__AddNumberedStyles|645_0(2, "Item", 0, SoundID.ItemSoundCount);
			SoundID.<FillLegacyArrayedStylesMap>g__AddNumberedStyles|645_0(3, "NPCHit", 0, SoundID.NPCHitCount);
			SoundID.<FillLegacyArrayedStylesMap>g__AddNumberedStyles|645_0(4, "NPCDeath", 0, SoundID.NPCDeathCount);
			SoundID.<FillLegacyArrayedStylesMap>g__AddNumberedStyles|645_0(29, "Zombie", 0, SoundID.ZombieSoundCount);
			SoundID.<FillLegacyArrayedStylesMap>g__AddNumberedStyles|645_0(32, "Bird", 14, 6);
		}

		// Token: 0x0600357C RID: 13692 RVA: 0x00576DCD File Offset: 0x00574FCD
		private static SoundStyle SoundWithDefaults(SoundID.SoundStyleDefaults defaults, SoundStyle style)
		{
			defaults.Apply(ref style);
			return style;
		}

		// Token: 0x0600357D RID: 13693 RVA: 0x00576DDC File Offset: 0x00574FDC
		private static SoundStyle NPCHitSound(int soundStyle)
		{
			SoundID.SoundStyleDefaults npchitDefaults = SoundID.SoundStyleDefaults.NPCHitDefaults;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 2);
			defaultInterpolatedStringHandler.AppendFormatted("Terraria/Sounds/");
			defaultInterpolatedStringHandler.AppendLiteral("NPC_Hit_");
			defaultInterpolatedStringHandler.AppendFormatted<int>(soundStyle);
			return SoundID.SoundWithDefaults(npchitDefaults, new SoundStyle(defaultInterpolatedStringHandler.ToStringAndClear(), SoundType.Sound));
		}

		// Token: 0x0600357E RID: 13694 RVA: 0x00576E29 File Offset: 0x00575029
		private static SoundStyle NPCHitSound(ReadOnlySpan<int> soundStyles)
		{
			return SoundID.SoundWithDefaults(SoundID.SoundStyleDefaults.NPCHitDefaults, new SoundStyle("Terraria/Sounds/NPC_Hit_", soundStyles, SoundType.Sound));
		}

		// Token: 0x0600357F RID: 13695 RVA: 0x00576E44 File Offset: 0x00575044
		private static SoundStyle NPCDeathSound(int soundStyle)
		{
			SoundID.SoundStyleDefaults npcdeathDefaults = SoundID.SoundStyleDefaults.NPCDeathDefaults;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(11, 2);
			defaultInterpolatedStringHandler.AppendFormatted("Terraria/Sounds/");
			defaultInterpolatedStringHandler.AppendLiteral("NPC_Killed_");
			defaultInterpolatedStringHandler.AppendFormatted<int>(soundStyle);
			return SoundID.SoundWithDefaults(npcdeathDefaults, new SoundStyle(defaultInterpolatedStringHandler.ToStringAndClear(), SoundType.Sound));
		}

		// Token: 0x06003580 RID: 13696 RVA: 0x00576E92 File Offset: 0x00575092
		private static SoundStyle NPCDeathSound(ReadOnlySpan<int> soundStyles)
		{
			return SoundID.SoundWithDefaults(SoundID.SoundStyleDefaults.NPCDeathDefaults, new SoundStyle("Terraria/Sounds/NPC_Killed_", soundStyles, SoundType.Sound));
		}

		// Token: 0x06003581 RID: 13697 RVA: 0x00576EAC File Offset: 0x005750AC
		private static SoundStyle ItemSound(int soundStyle)
		{
			SoundID.SoundStyleDefaults itemDefaults = SoundID.SoundStyleDefaults.ItemDefaults;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 2);
			defaultInterpolatedStringHandler.AppendFormatted("Terraria/Sounds/");
			defaultInterpolatedStringHandler.AppendLiteral("Item_");
			defaultInterpolatedStringHandler.AppendFormatted<int>(soundStyle);
			return SoundID.SoundWithDefaults(itemDefaults, new SoundStyle(defaultInterpolatedStringHandler.ToStringAndClear(), SoundType.Sound));
		}

		// Token: 0x06003582 RID: 13698 RVA: 0x00576EF9 File Offset: 0x005750F9
		private static SoundStyle ItemSound(ReadOnlySpan<int> soundStyles)
		{
			return SoundID.SoundWithDefaults(SoundID.SoundStyleDefaults.ItemDefaults, new SoundStyle("Terraria/Sounds/Item_", soundStyles, SoundType.Sound));
		}

		// Token: 0x06003583 RID: 13699 RVA: 0x00576F14 File Offset: 0x00575114
		private static SoundStyle ZombieSound(int soundStyle)
		{
			SoundID.SoundStyleDefaults zombieDefaults = SoundID.SoundStyleDefaults.ZombieDefaults;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(7, 2);
			defaultInterpolatedStringHandler.AppendFormatted("Terraria/Sounds/");
			defaultInterpolatedStringHandler.AppendLiteral("Zombie_");
			defaultInterpolatedStringHandler.AppendFormatted<int>(soundStyle);
			SoundStyle result = SoundID.SoundWithDefaults(zombieDefaults, new SoundStyle(defaultInterpolatedStringHandler.ToStringAndClear(), SoundType.Sound));
			result.SoundLimitBehavior = SoundLimitBehavior.IgnoreNew;
			return result;
		}

		// Token: 0x06003584 RID: 13700 RVA: 0x00576F6C File Offset: 0x0057516C
		private static SoundStyle ZombieSound(ReadOnlySpan<int> soundStyles)
		{
			SoundStyle result = SoundID.SoundWithDefaults(SoundID.SoundStyleDefaults.ZombieDefaults, new SoundStyle("Terraria/Sounds/Zombie_", soundStyles, SoundType.Sound));
			result.SoundLimitBehavior = SoundLimitBehavior.IgnoreNew;
			return result;
		}

		// Token: 0x06003585 RID: 13701 RVA: 0x00576F9C File Offset: 0x0057519C
		internal static SoundStyle? GetLegacyStyle(int type, int style)
		{
			switch (type)
			{
			case 0:
				return new SoundStyle?(SoundID.Dig);
			case 1:
				return new SoundStyle?(SoundID.PlayerHit);
			case 2:
			case 3:
			case 4:
			case 29:
			case 32:
				return (style >= 1 && style < SoundID.legacyArrayedStylesMapping[type].Length) ? new SoundStyle?(SoundID.legacyArrayedStylesMapping[type][style]) : null;
			case 5:
				return new SoundStyle?(SoundID.PlayerKilled);
			case 6:
				return new SoundStyle?(SoundID.Grass);
			case 7:
				return new SoundStyle?(SoundID.Grab);
			case 8:
				return new SoundStyle?(SoundID.DoorOpen);
			case 9:
				return new SoundStyle?(SoundID.DoorClosed);
			case 10:
				return new SoundStyle?(SoundID.MenuOpen);
			case 11:
				return new SoundStyle?(SoundID.MenuClose);
			case 12:
				return new SoundStyle?(SoundID.MenuTick);
			case 13:
				return new SoundStyle?(SoundID.Shatter);
			case 14:
			{
				SoundStyle value;
				if (style != 489)
				{
					if (style == 542)
					{
						value = SoundID.SandShark;
						goto IL_236;
					}
					if (style != 586)
					{
						value = SoundID.ZombieMoan;
						goto IL_236;
					}
				}
				value = SoundID.BloodZombie;
				IL_236:
				return new SoundStyle?(value);
			}
			case 15:
			{
				SoundStyle? result;
				switch (style)
				{
				case 0:
					result = new SoundStyle?(SoundID.Roar);
					goto IL_29C;
				case 1:
					result = new SoundStyle?(SoundID.WormDig);
					goto IL_29C;
				case 2:
					result = new SoundStyle?(SoundID.ScaryScream);
					goto IL_29C;
				case 4:
					result = new SoundStyle?(SoundID.WormDigQuiet);
					goto IL_29C;
				}
				result = null;
				IL_29C:
				return result;
			}
			case 16:
				return new SoundStyle?(SoundID.DoubleJump);
			case 17:
				return new SoundStyle?(SoundID.Run);
			case 18:
				return new SoundStyle?(SoundID.Coins);
			case 19:
			{
				SoundStyle value;
				switch (style)
				{
				case 1:
					value = SoundID.SplashWeak;
					break;
				case 2:
					value = SoundID.Shimmer1;
					break;
				case 3:
					value = SoundID.Shimmer2;
					break;
				case 4:
					value = SoundID.ShimmerWeak1;
					break;
				case 5:
					value = SoundID.ShimmerWeak2;
					break;
				default:
					value = SoundID.Splash;
					break;
				}
				return new SoundStyle?(value);
			}
			case 20:
				return new SoundStyle?(SoundID.FemaleHit);
			case 21:
				return new SoundStyle?(SoundID.Tink);
			case 22:
				return new SoundStyle?(SoundID.Unlock);
			case 23:
				return new SoundStyle?(SoundID.Drown);
			case 24:
				return new SoundStyle?(SoundID.Chat);
			case 25:
				return new SoundStyle?(SoundID.MaxMana);
			case 26:
				return new SoundStyle?(SoundID.Mummy);
			case 27:
				return new SoundStyle?(SoundID.Pixie);
			case 28:
				return new SoundStyle?(SoundID.Mech);
			case 30:
				return new SoundStyle?(SoundID.Duck);
			case 31:
				return new SoundStyle?(SoundID.Frog);
			case 33:
				return new SoundStyle?(SoundID.Critter);
			case 34:
				return new SoundStyle?(SoundID.Waterfall);
			case 35:
				return new SoundStyle?(SoundID.Lavafall);
			case 36:
			{
				SoundStyle value;
				if (style == -1)
				{
					value = SoundID.ForceRoarPitched;
				}
				else
				{
					value = SoundID.ForceRoar;
				}
				return new SoundStyle?(value);
			}
			case 37:
			{
				SoundStyle value = SoundID.Meowmere;
				return new SoundStyle?(value.WithVolumeScale((!Main.starGame) ? ((float)style * 0.05f) : 0.15f));
			}
			case 38:
			{
				SoundStyle value = SoundID.CoinPickup;
				return new SoundStyle?(value.WithVolumeScale((!Main.starGame) ? 1f : 0.15f));
			}
			case 39:
			{
				SoundStyle value;
				if (style == 2)
				{
					value = SoundID.DripSplash;
				}
				else
				{
					value = SoundID.Drip;
				}
				return new SoundStyle?(value);
			}
			case 40:
				return new SoundStyle?(SoundID.Camera);
			case 41:
				return new SoundStyle?(SoundID.MoonLord);
			case 43:
				return new SoundStyle?(SoundID.Thunder);
			case 44:
				return new SoundStyle?(SoundID.Seagull);
			case 45:
				return new SoundStyle?(SoundID.Dolphin);
			case 46:
				return new SoundStyle?(SoundID.Owl);
			case 47:
				return new SoundStyle?(SoundID.GuitarC);
			case 48:
				return new SoundStyle?(SoundID.GuitarD);
			case 49:
				return new SoundStyle?(SoundID.GuitarEm);
			case 50:
				return new SoundStyle?(SoundID.GuitarG);
			case 51:
				return new SoundStyle?(SoundID.GuitarBm);
			case 52:
				return new SoundStyle?(SoundID.GuitarAm);
			case 53:
				return new SoundStyle?(SoundID.DrumHiHat);
			case 54:
				return new SoundStyle?(SoundID.DrumTomHigh);
			case 55:
				return new SoundStyle?(SoundID.DrumTomLow);
			case 56:
				return new SoundStyle?(SoundID.DrumTomMid);
			case 57:
				return new SoundStyle?(SoundID.DrumClosedHiHat);
			case 58:
				return new SoundStyle?(SoundID.DrumCymbal1);
			case 59:
				return new SoundStyle?(SoundID.DrumCymbal2);
			case 60:
				return new SoundStyle?(SoundID.DrumKick);
			case 61:
				return new SoundStyle?(SoundID.DrumTamaSnare);
			case 62:
				return new SoundStyle?(SoundID.DrumFloorTom);
			case 63:
				return new SoundStyle?(SoundID.Research);
			case 64:
				return new SoundStyle?(SoundID.ResearchComplete);
			case 65:
				return new SoundStyle?(SoundID.QueenSlime);
			case 66:
				return new SoundStyle?(SoundID.Clown);
			case 67:
				return new SoundStyle?(SoundID.Cockatiel);
			case 68:
				return new SoundStyle?(SoundID.Macaw);
			case 69:
				return new SoundStyle?(SoundID.Toucan);
			}
			return null;
		}

		// Token: 0x06003587 RID: 13703 RVA: 0x00577610 File Offset: 0x00575810
		[CompilerGenerated]
		internal static void <FillLegacyArrayedStylesMap>g__AddNumberedStyles|645_0(int type, string baseName, int start, int numStyles)
		{
			SoundStyle[] array = SoundID.legacyArrayedStylesMapping[type] = new SoundStyle[start + numStyles];
			for (int i = 0; i < numStyles; i++)
			{
				int ii = start + i;
				Type typeFromHandle = typeof(SoundID);
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 2);
				defaultInterpolatedStringHandler.AppendFormatted(baseName);
				defaultInterpolatedStringHandler.AppendFormatted<int>(ii);
				FieldInfo field = typeFromHandle.GetField(defaultInterpolatedStringHandler.ToStringAndClear(), BindingFlags.Static | BindingFlags.Public);
				object obj = (field != null) ? field.GetValue(null) : null;
				if (obj is SoundStyle)
				{
					SoundStyle soundStyle = (SoundStyle)obj;
					array[ii] = soundStyle;
				}
			}
		}

		// Token: 0x0400489A RID: 18586
		public static readonly int NPCHitCount = 58;

		// Token: 0x0400489B RID: 18587
		public static readonly int NPCDeathCount = 67;

		// Token: 0x0400489C RID: 18588
		public static readonly int ItemSoundCount = 179;

		// Token: 0x0400489D RID: 18589
		public static readonly SoundStyle DD2_GoblinBomb;

		// Token: 0x0400489E RID: 18590
		public static readonly SoundStyle AchievementComplete;

		// Token: 0x0400489F RID: 18591
		public static readonly SoundStyle BlizzardInsideBuildingLoop;

		// Token: 0x040048A0 RID: 18592
		public static readonly SoundStyle BlizzardStrongLoop;

		// Token: 0x040048A1 RID: 18593
		public static readonly SoundStyle LiquidsHoneyWater;

		// Token: 0x040048A2 RID: 18594
		public static readonly SoundStyle LiquidsHoneyLava;

		// Token: 0x040048A3 RID: 18595
		public static readonly SoundStyle LiquidsWaterLava;

		// Token: 0x040048A4 RID: 18596
		public static readonly SoundStyle DD2_BallistaTowerShot;

		// Token: 0x040048A5 RID: 18597
		public static readonly SoundStyle DD2_ExplosiveTrapExplode;

		// Token: 0x040048A6 RID: 18598
		public static readonly SoundStyle DD2_FlameburstTowerShot;

		// Token: 0x040048A7 RID: 18599
		public static readonly SoundStyle DD2_LightningAuraZap;

		// Token: 0x040048A8 RID: 18600
		public static readonly SoundStyle DD2_DefenseTowerSpawn;

		// Token: 0x040048A9 RID: 18601
		public static readonly SoundStyle DD2_BetsyDeath;

		// Token: 0x040048AA RID: 18602
		public static readonly SoundStyle DD2_BetsyFireballShot;

		// Token: 0x040048AB RID: 18603
		public static readonly SoundStyle DD2_BetsyFireballImpact;

		// Token: 0x040048AC RID: 18604
		public static readonly SoundStyle DD2_BetsyFlameBreath;

		// Token: 0x040048AD RID: 18605
		public static readonly SoundStyle DD2_BetsyFlyingCircleAttack;

		// Token: 0x040048AE RID: 18606
		public static readonly SoundStyle DD2_BetsyHurt;

		// Token: 0x040048AF RID: 18607
		public static readonly SoundStyle DD2_BetsyScream;

		// Token: 0x040048B0 RID: 18608
		public static readonly SoundStyle DD2_BetsySummon;

		// Token: 0x040048B1 RID: 18609
		public static readonly SoundStyle DD2_BetsyWindAttack;

		// Token: 0x040048B2 RID: 18610
		public static readonly SoundStyle DD2_DarkMageAttack;

		// Token: 0x040048B3 RID: 18611
		public static readonly SoundStyle DD2_DarkMageCastHeal;

		// Token: 0x040048B4 RID: 18612
		public static readonly SoundStyle DD2_DarkMageDeath;

		// Token: 0x040048B5 RID: 18613
		public static readonly SoundStyle DD2_DarkMageHealImpact;

		// Token: 0x040048B6 RID: 18614
		public static readonly SoundStyle DD2_DarkMageHurt;

		// Token: 0x040048B7 RID: 18615
		public static readonly SoundStyle DD2_DarkMageSummonSkeleton;

		// Token: 0x040048B8 RID: 18616
		public static readonly SoundStyle DD2_DrakinBreathIn;

		// Token: 0x040048B9 RID: 18617
		public static readonly SoundStyle DD2_DrakinDeath;

		// Token: 0x040048BA RID: 18618
		public static readonly SoundStyle DD2_DrakinHurt;

		// Token: 0x040048BB RID: 18619
		public static readonly SoundStyle DD2_DrakinShot;

		// Token: 0x040048BC RID: 18620
		public static readonly SoundStyle DD2_GoblinDeath;

		// Token: 0x040048BD RID: 18621
		public static readonly SoundStyle DD2_GoblinHurt;

		// Token: 0x040048BE RID: 18622
		public static readonly SoundStyle DD2_GoblinScream;

		// Token: 0x040048BF RID: 18623
		public static readonly SoundStyle DD2_GoblinBomberDeath;

		// Token: 0x040048C0 RID: 18624
		public static readonly SoundStyle DD2_GoblinBomberHurt;

		// Token: 0x040048C1 RID: 18625
		public static readonly SoundStyle DD2_GoblinBomberScream;

		// Token: 0x040048C2 RID: 18626
		public static readonly SoundStyle DD2_GoblinBomberThrow;

		// Token: 0x040048C3 RID: 18627
		public static readonly SoundStyle DD2_JavelinThrowersAttack;

		// Token: 0x040048C4 RID: 18628
		public static readonly SoundStyle DD2_JavelinThrowersDeath;

		// Token: 0x040048C5 RID: 18629
		public static readonly SoundStyle DD2_JavelinThrowersHurt;

		// Token: 0x040048C6 RID: 18630
		public static readonly SoundStyle DD2_JavelinThrowersTaunt;

		// Token: 0x040048C7 RID: 18631
		public static readonly SoundStyle DD2_KoboldDeath;

		// Token: 0x040048C8 RID: 18632
		public static readonly SoundStyle DD2_KoboldExplosion;

		// Token: 0x040048C9 RID: 18633
		public static readonly SoundStyle DD2_KoboldHurt;

		// Token: 0x040048CA RID: 18634
		public static readonly SoundStyle DD2_KoboldIgnite;

		// Token: 0x040048CB RID: 18635
		public static readonly SoundStyle DD2_KoboldIgniteLoop;

		// Token: 0x040048CC RID: 18636
		public static readonly SoundStyle DD2_KoboldScreamChargeLoop;

		// Token: 0x040048CD RID: 18637
		public static readonly SoundStyle DD2_KoboldFlyerChargeScream;

		// Token: 0x040048CE RID: 18638
		public static readonly SoundStyle DD2_KoboldFlyerDeath;

		// Token: 0x040048CF RID: 18639
		public static readonly SoundStyle DD2_KoboldFlyerHurt;

		// Token: 0x040048D0 RID: 18640
		public static readonly SoundStyle DD2_LightningBugDeath;

		// Token: 0x040048D1 RID: 18641
		public static readonly SoundStyle DD2_LightningBugHurt;

		// Token: 0x040048D2 RID: 18642
		public static readonly SoundStyle DD2_LightningBugZap;

		// Token: 0x040048D3 RID: 18643
		public static readonly SoundStyle DD2_OgreAttack;

		// Token: 0x040048D4 RID: 18644
		public static readonly SoundStyle DD2_OgreDeath;

		// Token: 0x040048D5 RID: 18645
		public static readonly SoundStyle DD2_OgreGroundPound;

		// Token: 0x040048D6 RID: 18646
		public static readonly SoundStyle DD2_OgreHurt;

		// Token: 0x040048D7 RID: 18647
		public static readonly SoundStyle DD2_OgreRoar;

		// Token: 0x040048D8 RID: 18648
		public static readonly SoundStyle DD2_OgreSpit;

		// Token: 0x040048D9 RID: 18649
		public static readonly SoundStyle DD2_SkeletonDeath;

		// Token: 0x040048DA RID: 18650
		public static readonly SoundStyle DD2_SkeletonHurt;

		// Token: 0x040048DB RID: 18651
		public static readonly SoundStyle DD2_SkeletonSummoned;

		// Token: 0x040048DC RID: 18652
		public static readonly SoundStyle DD2_WitherBeastAuraPulse;

		// Token: 0x040048DD RID: 18653
		public static readonly SoundStyle DD2_WitherBeastCrystalImpact;

		// Token: 0x040048DE RID: 18654
		public static readonly SoundStyle DD2_WitherBeastDeath;

		// Token: 0x040048DF RID: 18655
		public static readonly SoundStyle DD2_WitherBeastHurt;

		// Token: 0x040048E0 RID: 18656
		public static readonly SoundStyle DD2_WyvernDeath;

		// Token: 0x040048E1 RID: 18657
		public static readonly SoundStyle DD2_WyvernHurt;

		// Token: 0x040048E2 RID: 18658
		public static readonly SoundStyle DD2_WyvernScream;

		// Token: 0x040048E3 RID: 18659
		public static readonly SoundStyle DD2_WyvernDiveDown;

		// Token: 0x040048E4 RID: 18660
		public static readonly SoundStyle DD2_EtherianPortalDryadTouch;

		// Token: 0x040048E5 RID: 18661
		public static readonly SoundStyle DD2_EtherianPortalIdleLoop;

		// Token: 0x040048E6 RID: 18662
		public static readonly SoundStyle DD2_EtherianPortalOpen;

		// Token: 0x040048E7 RID: 18663
		public static readonly SoundStyle DD2_EtherianPortalSpawnEnemy;

		// Token: 0x040048E8 RID: 18664
		public static readonly SoundStyle DD2_CrystalCartImpact;

		// Token: 0x040048E9 RID: 18665
		public static readonly SoundStyle DD2_DefeatScene;

		// Token: 0x040048EA RID: 18666
		public static readonly SoundStyle DD2_WinScene;

		// Token: 0x040048EB RID: 18667
		public static readonly SoundStyle DD2_BetsysWrathShot;

		// Token: 0x040048EC RID: 18668
		public static readonly SoundStyle DD2_BetsysWrathImpact;

		// Token: 0x040048ED RID: 18669
		public static readonly SoundStyle DD2_BookStaffCast;

		// Token: 0x040048EE RID: 18670
		public static readonly SoundStyle DD2_BookStaffTwisterLoop;

		// Token: 0x040048EF RID: 18671
		public static readonly SoundStyle DD2_GhastlyGlaiveImpactGhost;

		// Token: 0x040048F0 RID: 18672
		public static readonly SoundStyle DD2_GhastlyGlaivePierce;

		// Token: 0x040048F1 RID: 18673
		public static readonly SoundStyle DD2_MonkStaffGroundImpact;

		// Token: 0x040048F2 RID: 18674
		public static readonly SoundStyle DD2_MonkStaffGroundMiss;

		// Token: 0x040048F3 RID: 18675
		public static readonly SoundStyle DD2_MonkStaffSwing;

		// Token: 0x040048F4 RID: 18676
		public static readonly SoundStyle DD2_PhantomPhoenixShot;

		// Token: 0x040048F5 RID: 18677
		public static readonly SoundStyle DD2_SonicBoomBladeSlash;

		// Token: 0x040048F6 RID: 18678
		public static readonly SoundStyle DD2_SkyDragonsFuryCircle;

		// Token: 0x040048F7 RID: 18679
		public static readonly SoundStyle DD2_SkyDragonsFuryShot;

		// Token: 0x040048F8 RID: 18680
		public static readonly SoundStyle DD2_SkyDragonsFurySwing;

		// Token: 0x040048F9 RID: 18681
		public static readonly SoundStyle LucyTheAxeTalk;

		// Token: 0x040048FA RID: 18682
		public static readonly SoundStyle DeerclopsHit;

		// Token: 0x040048FB RID: 18683
		public static readonly SoundStyle DeerclopsDeath;

		// Token: 0x040048FC RID: 18684
		public static readonly SoundStyle DeerclopsScream;

		// Token: 0x040048FD RID: 18685
		public static readonly SoundStyle DeerclopsIceAttack;

		// Token: 0x040048FE RID: 18686
		public static readonly SoundStyle DeerclopsRubbleAttack;

		// Token: 0x040048FF RID: 18687
		public static readonly SoundStyle DeerclopsStep;

		// Token: 0x04004900 RID: 18688
		public static readonly SoundStyle ChesterOpen;

		// Token: 0x04004901 RID: 18689
		public static readonly SoundStyle ChesterClose;

		// Token: 0x04004902 RID: 18690
		public static readonly SoundStyle AbigailSummon;

		// Token: 0x04004903 RID: 18691
		public static readonly SoundStyle AbigailCry;

		// Token: 0x04004904 RID: 18692
		public static readonly SoundStyle AbigailAttack;

		// Token: 0x04004905 RID: 18693
		public static readonly SoundStyle AbigailUpgrade;

		// Token: 0x04004906 RID: 18694
		public static readonly SoundStyle GlommerBounce;

		// Token: 0x04004907 RID: 18695
		public static readonly SoundStyle DSTMaleHurt;

		// Token: 0x04004908 RID: 18696
		public static readonly SoundStyle DSTFemaleHurt;

		// Token: 0x04004909 RID: 18697
		public static readonly SoundStyle JimsDrone;

		// Token: 0x0400490A RID: 18698
		private static List<string> _trackableLegacySoundPathList;

		// Token: 0x0400490B RID: 18699
		public static Dictionary<string, SoundStyle> SoundByName;

		// Token: 0x0400490C RID: 18700
		public static Dictionary<string, ushort> IndexByName;

		// Token: 0x0400490D RID: 18701
		public static Dictionary<ushort, SoundStyle> SoundByIndex;

		// Token: 0x0400490E RID: 18702
		private const string Prefix = "Terraria/Sounds/";

		// Token: 0x0400490F RID: 18703
		private const string PrefixCustom = "Terraria/Sounds/Custom/";

		// Token: 0x04004910 RID: 18704
		public static readonly SoundStyle Dig;

		// Token: 0x04004911 RID: 18705
		public static readonly SoundStyle PlayerHit;

		// Token: 0x04004912 RID: 18706
		public static readonly SoundStyle Item;

		// Token: 0x04004913 RID: 18707
		public static readonly SoundStyle PlayerKilled;

		// Token: 0x04004914 RID: 18708
		public static readonly SoundStyle Grass;

		// Token: 0x04004915 RID: 18709
		public static readonly SoundStyle Grab;

		// Token: 0x04004916 RID: 18710
		public static readonly SoundStyle DoorOpen;

		// Token: 0x04004917 RID: 18711
		public static readonly SoundStyle DoorClosed;

		// Token: 0x04004918 RID: 18712
		public static readonly SoundStyle MenuOpen;

		// Token: 0x04004919 RID: 18713
		public static readonly SoundStyle MenuClose;

		// Token: 0x0400491A RID: 18714
		public static readonly SoundStyle MenuTick;

		// Token: 0x0400491B RID: 18715
		public static readonly SoundStyle Shatter;

		// Token: 0x0400491C RID: 18716
		public static readonly SoundStyle ZombieMoan;

		// Token: 0x0400491D RID: 18717
		public static readonly SoundStyle SandShark;

		// Token: 0x0400491E RID: 18718
		public static readonly SoundStyle BloodZombie;

		// Token: 0x0400491F RID: 18719
		public static readonly SoundStyle Roar;

		// Token: 0x04004920 RID: 18720
		public static readonly SoundStyle WormDig;

		// Token: 0x04004921 RID: 18721
		public static readonly SoundStyle WormDigQuiet;

		// Token: 0x04004922 RID: 18722
		public static readonly SoundStyle ScaryScream;

		// Token: 0x04004923 RID: 18723
		public static readonly SoundStyle DoubleJump;

		// Token: 0x04004924 RID: 18724
		public static readonly SoundStyle Run;

		// Token: 0x04004925 RID: 18725
		public static readonly SoundStyle Coins;

		// Token: 0x04004926 RID: 18726
		public static readonly SoundStyle Splash;

		// Token: 0x04004927 RID: 18727
		public static readonly SoundStyle SplashWeak;

		// Token: 0x04004928 RID: 18728
		public static readonly SoundStyle Shimmer1;

		// Token: 0x04004929 RID: 18729
		public static readonly SoundStyle Shimmer2;

		// Token: 0x0400492A RID: 18730
		public static readonly SoundStyle ShimmerWeak1;

		// Token: 0x0400492B RID: 18731
		public static readonly SoundStyle ShimmerWeak2;

		// Token: 0x0400492C RID: 18732
		public static readonly SoundStyle FemaleHit;

		// Token: 0x0400492D RID: 18733
		public static readonly SoundStyle Tink;

		// Token: 0x0400492E RID: 18734
		public static readonly SoundStyle Unlock;

		// Token: 0x0400492F RID: 18735
		public static readonly SoundStyle Drown;

		// Token: 0x04004930 RID: 18736
		public static readonly SoundStyle Chat;

		// Token: 0x04004931 RID: 18737
		public static readonly SoundStyle MaxMana;

		// Token: 0x04004932 RID: 18738
		public static readonly SoundStyle Mummy;

		// Token: 0x04004933 RID: 18739
		public static readonly SoundStyle Pixie;

		// Token: 0x04004934 RID: 18740
		public static readonly SoundStyle Mech;

		// Token: 0x04004935 RID: 18741
		public static readonly SoundStyle Duck;

		// Token: 0x04004936 RID: 18742
		public static readonly SoundStyle Frog;

		// Token: 0x04004937 RID: 18743
		public static readonly SoundStyle Bird;

		// Token: 0x04004938 RID: 18744
		public static readonly SoundStyle Bird14;

		// Token: 0x04004939 RID: 18745
		public static readonly SoundStyle Bird16;

		// Token: 0x0400493A RID: 18746
		public static readonly SoundStyle Bird17;

		// Token: 0x0400493B RID: 18747
		public static readonly SoundStyle Bird18;

		// Token: 0x0400493C RID: 18748
		public static readonly SoundStyle Bird19;

		// Token: 0x0400493D RID: 18749
		public static readonly SoundStyle Critter;

		// Token: 0x0400493E RID: 18750
		public static readonly SoundStyle Waterfall;

		// Token: 0x0400493F RID: 18751
		public static readonly SoundStyle Lavafall;

		// Token: 0x04004940 RID: 18752
		public static readonly SoundStyle ForceRoar;

		// Token: 0x04004941 RID: 18753
		public static readonly SoundStyle ForceRoarPitched;

		// Token: 0x04004942 RID: 18754
		public static readonly SoundStyle Meowmere;

		// Token: 0x04004943 RID: 18755
		public static readonly SoundStyle CoinPickup;

		// Token: 0x04004944 RID: 18756
		public static readonly SoundStyle Drip;

		// Token: 0x04004945 RID: 18757
		public static readonly SoundStyle DripSplash;

		// Token: 0x04004946 RID: 18758
		public static readonly SoundStyle Camera;

		// Token: 0x04004947 RID: 18759
		public static readonly SoundStyle MoonLord;

		// Token: 0x04004948 RID: 18760
		public static readonly SoundStyle Thunder;

		// Token: 0x04004949 RID: 18761
		public static readonly SoundStyle Seagull;

		// Token: 0x0400494A RID: 18762
		public static readonly SoundStyle Dolphin;

		// Token: 0x0400494B RID: 18763
		public static readonly SoundStyle Owl;

		// Token: 0x0400494C RID: 18764
		public static readonly SoundStyle GuitarC;

		// Token: 0x0400494D RID: 18765
		public static readonly SoundStyle GuitarD;

		// Token: 0x0400494E RID: 18766
		public static readonly SoundStyle GuitarEm;

		// Token: 0x0400494F RID: 18767
		public static readonly SoundStyle GuitarG;

		// Token: 0x04004950 RID: 18768
		public static readonly SoundStyle GuitarBm;

		// Token: 0x04004951 RID: 18769
		public static readonly SoundStyle GuitarAm;

		// Token: 0x04004952 RID: 18770
		public static readonly SoundStyle DrumHiHat;

		// Token: 0x04004953 RID: 18771
		public static readonly SoundStyle DrumTomHigh;

		// Token: 0x04004954 RID: 18772
		public static readonly SoundStyle DrumTomLow;

		// Token: 0x04004955 RID: 18773
		public static readonly SoundStyle DrumTomMid;

		// Token: 0x04004956 RID: 18774
		public static readonly SoundStyle DrumClosedHiHat;

		// Token: 0x04004957 RID: 18775
		public static readonly SoundStyle DrumCymbal1;

		// Token: 0x04004958 RID: 18776
		public static readonly SoundStyle DrumCymbal2;

		// Token: 0x04004959 RID: 18777
		public static readonly SoundStyle DrumKick;

		// Token: 0x0400495A RID: 18778
		public static readonly SoundStyle DrumTamaSnare;

		// Token: 0x0400495B RID: 18779
		public static readonly SoundStyle DrumFloorTom;

		// Token: 0x0400495C RID: 18780
		public static readonly SoundStyle Research;

		// Token: 0x0400495D RID: 18781
		public static readonly SoundStyle ResearchComplete;

		// Token: 0x0400495E RID: 18782
		public static readonly SoundStyle QueenSlime;

		// Token: 0x0400495F RID: 18783
		public static readonly SoundStyle Clown;

		// Token: 0x04004960 RID: 18784
		public static readonly SoundStyle Cockatiel;

		// Token: 0x04004961 RID: 18785
		public static readonly SoundStyle Macaw;

		// Token: 0x04004962 RID: 18786
		public static readonly SoundStyle Toucan;

		// Token: 0x04004963 RID: 18787
		public static readonly SoundStyle NPCHit1;

		// Token: 0x04004964 RID: 18788
		public static readonly SoundStyle NPCHit2;

		// Token: 0x04004965 RID: 18789
		public static readonly SoundStyle NPCHit3;

		// Token: 0x04004966 RID: 18790
		public static readonly SoundStyle NPCHit4;

		// Token: 0x04004967 RID: 18791
		public static readonly SoundStyle NPCHit5;

		// Token: 0x04004968 RID: 18792
		public static readonly SoundStyle NPCHit6;

		// Token: 0x04004969 RID: 18793
		public static readonly SoundStyle NPCHit7;

		// Token: 0x0400496A RID: 18794
		public static readonly SoundStyle NPCHit8;

		// Token: 0x0400496B RID: 18795
		public static readonly SoundStyle NPCHit9;

		// Token: 0x0400496C RID: 18796
		public static readonly SoundStyle NPCHit10;

		// Token: 0x0400496D RID: 18797
		public static readonly SoundStyle NPCHit11;

		// Token: 0x0400496E RID: 18798
		public static readonly SoundStyle NPCHit12;

		// Token: 0x0400496F RID: 18799
		public static readonly SoundStyle NPCHit13;

		// Token: 0x04004970 RID: 18800
		public static readonly SoundStyle NPCHit14;

		// Token: 0x04004971 RID: 18801
		public static readonly SoundStyle NPCHit15;

		// Token: 0x04004972 RID: 18802
		public static readonly SoundStyle NPCHit16;

		// Token: 0x04004973 RID: 18803
		public static readonly SoundStyle NPCHit17;

		// Token: 0x04004974 RID: 18804
		public static readonly SoundStyle NPCHit18;

		// Token: 0x04004975 RID: 18805
		public static readonly SoundStyle NPCHit19;

		// Token: 0x04004976 RID: 18806
		public static readonly SoundStyle NPCHit20;

		// Token: 0x04004977 RID: 18807
		public static readonly SoundStyle NPCHit21;

		// Token: 0x04004978 RID: 18808
		public static readonly SoundStyle NPCHit22;

		// Token: 0x04004979 RID: 18809
		public static readonly SoundStyle NPCHit23;

		// Token: 0x0400497A RID: 18810
		public static readonly SoundStyle NPCHit24;

		// Token: 0x0400497B RID: 18811
		public static readonly SoundStyle NPCHit25;

		// Token: 0x0400497C RID: 18812
		public static readonly SoundStyle NPCHit26;

		// Token: 0x0400497D RID: 18813
		public static readonly SoundStyle NPCHit27;

		// Token: 0x0400497E RID: 18814
		public static readonly SoundStyle NPCHit28;

		// Token: 0x0400497F RID: 18815
		public static readonly SoundStyle NPCHit29;

		// Token: 0x04004980 RID: 18816
		public static readonly SoundStyle NPCHit30;

		// Token: 0x04004981 RID: 18817
		public static readonly SoundStyle NPCHit31;

		// Token: 0x04004982 RID: 18818
		public static readonly SoundStyle NPCHit32;

		// Token: 0x04004983 RID: 18819
		public static readonly SoundStyle NPCHit33;

		// Token: 0x04004984 RID: 18820
		public static readonly SoundStyle NPCHit34;

		// Token: 0x04004985 RID: 18821
		public static readonly SoundStyle NPCHit35;

		// Token: 0x04004986 RID: 18822
		public static readonly SoundStyle NPCHit36;

		// Token: 0x04004987 RID: 18823
		public static readonly SoundStyle NPCHit37;

		// Token: 0x04004988 RID: 18824
		public static readonly SoundStyle NPCHit38;

		// Token: 0x04004989 RID: 18825
		public static readonly SoundStyle NPCHit39;

		// Token: 0x0400498A RID: 18826
		public static readonly SoundStyle NPCHit40;

		// Token: 0x0400498B RID: 18827
		public static readonly SoundStyle NPCHit41;

		// Token: 0x0400498C RID: 18828
		public static readonly SoundStyle NPCHit42;

		// Token: 0x0400498D RID: 18829
		public static readonly SoundStyle NPCHit43;

		// Token: 0x0400498E RID: 18830
		public static readonly SoundStyle NPCHit44;

		// Token: 0x0400498F RID: 18831
		public static readonly SoundStyle NPCHit45;

		// Token: 0x04004990 RID: 18832
		public static readonly SoundStyle NPCHit46;

		// Token: 0x04004991 RID: 18833
		public static readonly SoundStyle NPCHit47;

		// Token: 0x04004992 RID: 18834
		public static readonly SoundStyle NPCHit48;

		// Token: 0x04004993 RID: 18835
		public static readonly SoundStyle NPCHit49;

		// Token: 0x04004994 RID: 18836
		public static readonly SoundStyle NPCHit50;

		// Token: 0x04004995 RID: 18837
		public static readonly SoundStyle NPCHit51;

		// Token: 0x04004996 RID: 18838
		public static readonly SoundStyle NPCHit52;

		// Token: 0x04004997 RID: 18839
		public static readonly SoundStyle NPCHit53;

		// Token: 0x04004998 RID: 18840
		public static readonly SoundStyle NPCHit54;

		// Token: 0x04004999 RID: 18841
		public static readonly SoundStyle NPCHit55;

		// Token: 0x0400499A RID: 18842
		public static readonly SoundStyle NPCHit56;

		// Token: 0x0400499B RID: 18843
		public static readonly SoundStyle NPCHit57;

		// Token: 0x0400499C RID: 18844
		public static readonly SoundStyle NPCDeath1;

		// Token: 0x0400499D RID: 18845
		public static readonly SoundStyle NPCDeath2;

		// Token: 0x0400499E RID: 18846
		public static readonly SoundStyle NPCDeath3;

		// Token: 0x0400499F RID: 18847
		public static readonly SoundStyle NPCDeath4;

		// Token: 0x040049A0 RID: 18848
		public static readonly SoundStyle NPCDeath5;

		// Token: 0x040049A1 RID: 18849
		public static readonly SoundStyle NPCDeath6;

		// Token: 0x040049A2 RID: 18850
		public static readonly SoundStyle NPCDeath7;

		// Token: 0x040049A3 RID: 18851
		public static readonly SoundStyle NPCDeath8;

		// Token: 0x040049A4 RID: 18852
		public static readonly SoundStyle NPCDeath9;

		// Token: 0x040049A5 RID: 18853
		public static readonly SoundStyle NPCDeath10;

		// Token: 0x040049A6 RID: 18854
		public static readonly SoundStyle NPCDeath11;

		// Token: 0x040049A7 RID: 18855
		public static readonly SoundStyle NPCDeath12;

		// Token: 0x040049A8 RID: 18856
		public static readonly SoundStyle NPCDeath13;

		// Token: 0x040049A9 RID: 18857
		public static readonly SoundStyle NPCDeath14;

		// Token: 0x040049AA RID: 18858
		public static readonly SoundStyle NPCDeath15;

		// Token: 0x040049AB RID: 18859
		public static readonly SoundStyle NPCDeath16;

		// Token: 0x040049AC RID: 18860
		public static readonly SoundStyle NPCDeath17;

		// Token: 0x040049AD RID: 18861
		public static readonly SoundStyle NPCDeath18;

		// Token: 0x040049AE RID: 18862
		public static readonly SoundStyle NPCDeath19;

		// Token: 0x040049AF RID: 18863
		public static readonly SoundStyle NPCDeath20;

		// Token: 0x040049B0 RID: 18864
		public static readonly SoundStyle NPCDeath21;

		// Token: 0x040049B1 RID: 18865
		public static readonly SoundStyle NPCDeath22;

		// Token: 0x040049B2 RID: 18866
		public static readonly SoundStyle NPCDeath23;

		// Token: 0x040049B3 RID: 18867
		public static readonly SoundStyle NPCDeath24;

		// Token: 0x040049B4 RID: 18868
		public static readonly SoundStyle NPCDeath25;

		// Token: 0x040049B5 RID: 18869
		public static readonly SoundStyle NPCDeath26;

		// Token: 0x040049B6 RID: 18870
		public static readonly SoundStyle NPCDeath27;

		// Token: 0x040049B7 RID: 18871
		public static readonly SoundStyle NPCDeath28;

		// Token: 0x040049B8 RID: 18872
		public static readonly SoundStyle NPCDeath29;

		// Token: 0x040049B9 RID: 18873
		public static readonly SoundStyle NPCDeath30;

		// Token: 0x040049BA RID: 18874
		public static readonly SoundStyle NPCDeath31;

		// Token: 0x040049BB RID: 18875
		public static readonly SoundStyle NPCDeath32;

		// Token: 0x040049BC RID: 18876
		public static readonly SoundStyle NPCDeath33;

		// Token: 0x040049BD RID: 18877
		public static readonly SoundStyle NPCDeath34;

		// Token: 0x040049BE RID: 18878
		public static readonly SoundStyle NPCDeath35;

		// Token: 0x040049BF RID: 18879
		public static readonly SoundStyle NPCDeath36;

		// Token: 0x040049C0 RID: 18880
		public static readonly SoundStyle NPCDeath37;

		// Token: 0x040049C1 RID: 18881
		public static readonly SoundStyle NPCDeath38;

		// Token: 0x040049C2 RID: 18882
		public static readonly SoundStyle NPCDeath39;

		// Token: 0x040049C3 RID: 18883
		public static readonly SoundStyle NPCDeath40;

		// Token: 0x040049C4 RID: 18884
		public static readonly SoundStyle NPCDeath41;

		// Token: 0x040049C5 RID: 18885
		public static readonly SoundStyle NPCDeath42;

		// Token: 0x040049C6 RID: 18886
		public static readonly SoundStyle NPCDeath43;

		// Token: 0x040049C7 RID: 18887
		public static readonly SoundStyle NPCDeath44;

		// Token: 0x040049C8 RID: 18888
		public static readonly SoundStyle NPCDeath45;

		// Token: 0x040049C9 RID: 18889
		public static readonly SoundStyle NPCDeath46;

		// Token: 0x040049CA RID: 18890
		public static readonly SoundStyle NPCDeath47;

		// Token: 0x040049CB RID: 18891
		public static readonly SoundStyle NPCDeath48;

		// Token: 0x040049CC RID: 18892
		public static readonly SoundStyle NPCDeath49;

		// Token: 0x040049CD RID: 18893
		public static readonly SoundStyle NPCDeath50;

		// Token: 0x040049CE RID: 18894
		public static readonly SoundStyle NPCDeath51;

		// Token: 0x040049CF RID: 18895
		public static readonly SoundStyle NPCDeath52;

		// Token: 0x040049D0 RID: 18896
		public static readonly SoundStyle NPCDeath53;

		// Token: 0x040049D1 RID: 18897
		public static readonly SoundStyle NPCDeath54;

		// Token: 0x040049D2 RID: 18898
		public static readonly SoundStyle NPCDeath55;

		// Token: 0x040049D3 RID: 18899
		public static readonly SoundStyle NPCDeath56;

		// Token: 0x040049D4 RID: 18900
		public static readonly SoundStyle NPCDeath57;

		// Token: 0x040049D5 RID: 18901
		public static readonly SoundStyle NPCDeath58;

		// Token: 0x040049D6 RID: 18902
		public static readonly SoundStyle NPCDeath59;

		// Token: 0x040049D7 RID: 18903
		public static readonly SoundStyle NPCDeath60;

		// Token: 0x040049D8 RID: 18904
		public static readonly SoundStyle NPCDeath61;

		// Token: 0x040049D9 RID: 18905
		public static readonly SoundStyle NPCDeath62;

		// Token: 0x040049DA RID: 18906
		public static readonly SoundStyle NPCDeath63;

		// Token: 0x040049DB RID: 18907
		public static readonly SoundStyle NPCDeath64;

		// Token: 0x040049DC RID: 18908
		public static readonly SoundStyle NPCDeath65;

		// Token: 0x040049DD RID: 18909
		public static readonly SoundStyle NPCDeath66;

		// Token: 0x040049DE RID: 18910
		public static readonly SoundStyle Item1;

		// Token: 0x040049DF RID: 18911
		public static readonly SoundStyle Item2;

		// Token: 0x040049E0 RID: 18912
		public static readonly SoundStyle Item3;

		// Token: 0x040049E1 RID: 18913
		public static readonly SoundStyle Item4;

		// Token: 0x040049E2 RID: 18914
		public static readonly SoundStyle Item5;

		// Token: 0x040049E3 RID: 18915
		public static readonly SoundStyle Item6;

		// Token: 0x040049E4 RID: 18916
		public static readonly SoundStyle Item7;

		// Token: 0x040049E5 RID: 18917
		public static readonly SoundStyle Item8;

		// Token: 0x040049E6 RID: 18918
		public static readonly SoundStyle Item9;

		// Token: 0x040049E7 RID: 18919
		public static readonly SoundStyle Item10;

		// Token: 0x040049E8 RID: 18920
		public static readonly SoundStyle Item11;

		// Token: 0x040049E9 RID: 18921
		public static readonly SoundStyle Item12;

		// Token: 0x040049EA RID: 18922
		public static readonly SoundStyle Item13;

		// Token: 0x040049EB RID: 18923
		public static readonly SoundStyle Item14;

		// Token: 0x040049EC RID: 18924
		public static readonly SoundStyle Item15;

		// Token: 0x040049ED RID: 18925
		public static readonly SoundStyle Item16;

		// Token: 0x040049EE RID: 18926
		public static readonly SoundStyle Item17;

		// Token: 0x040049EF RID: 18927
		public static readonly SoundStyle Item18;

		// Token: 0x040049F0 RID: 18928
		public static readonly SoundStyle Item19;

		// Token: 0x040049F1 RID: 18929
		public static readonly SoundStyle Item20;

		// Token: 0x040049F2 RID: 18930
		public static readonly SoundStyle Item21;

		// Token: 0x040049F3 RID: 18931
		public static readonly SoundStyle Item22;

		// Token: 0x040049F4 RID: 18932
		public static readonly SoundStyle Item23;

		// Token: 0x040049F5 RID: 18933
		public static readonly SoundStyle Item24;

		// Token: 0x040049F6 RID: 18934
		public static readonly SoundStyle Item25;

		// Token: 0x040049F7 RID: 18935
		public static readonly SoundStyle Item26;

		// Token: 0x040049F8 RID: 18936
		public static readonly SoundStyle Item27;

		// Token: 0x040049F9 RID: 18937
		public static readonly SoundStyle Item28;

		// Token: 0x040049FA RID: 18938
		public static readonly SoundStyle Item29;

		// Token: 0x040049FB RID: 18939
		public static readonly SoundStyle Item30;

		// Token: 0x040049FC RID: 18940
		public static readonly SoundStyle Item31;

		// Token: 0x040049FD RID: 18941
		public static readonly SoundStyle Item32;

		// Token: 0x040049FE RID: 18942
		public static readonly SoundStyle Item33;

		// Token: 0x040049FF RID: 18943
		public static readonly SoundStyle Item34;

		// Token: 0x04004A00 RID: 18944
		public static readonly SoundStyle Item35;

		// Token: 0x04004A01 RID: 18945
		public static readonly SoundStyle Item36;

		// Token: 0x04004A02 RID: 18946
		public static readonly SoundStyle Item37;

		// Token: 0x04004A03 RID: 18947
		public static readonly SoundStyle Item38;

		// Token: 0x04004A04 RID: 18948
		public static readonly SoundStyle Item39;

		// Token: 0x04004A05 RID: 18949
		public static readonly SoundStyle Item40;

		// Token: 0x04004A06 RID: 18950
		public static readonly SoundStyle Item41;

		// Token: 0x04004A07 RID: 18951
		public static readonly SoundStyle Item42;

		// Token: 0x04004A08 RID: 18952
		public static readonly SoundStyle Item43;

		// Token: 0x04004A09 RID: 18953
		public static readonly SoundStyle Item44;

		// Token: 0x04004A0A RID: 18954
		public static readonly SoundStyle Item45;

		// Token: 0x04004A0B RID: 18955
		public static readonly SoundStyle Item46;

		// Token: 0x04004A0C RID: 18956
		public static readonly SoundStyle Item47;

		// Token: 0x04004A0D RID: 18957
		public static readonly SoundStyle Item48;

		// Token: 0x04004A0E RID: 18958
		public static readonly SoundStyle Item49;

		// Token: 0x04004A0F RID: 18959
		public static readonly SoundStyle Item50;

		// Token: 0x04004A10 RID: 18960
		public static readonly SoundStyle Item51;

		// Token: 0x04004A11 RID: 18961
		public static readonly SoundStyle Item52;

		// Token: 0x04004A12 RID: 18962
		public static readonly SoundStyle Item53;

		// Token: 0x04004A13 RID: 18963
		public static readonly SoundStyle Item54;

		// Token: 0x04004A14 RID: 18964
		public static readonly SoundStyle Item55;

		// Token: 0x04004A15 RID: 18965
		public static readonly SoundStyle Item56;

		// Token: 0x04004A16 RID: 18966
		public static readonly SoundStyle Item57;

		// Token: 0x04004A17 RID: 18967
		public static readonly SoundStyle Item58;

		// Token: 0x04004A18 RID: 18968
		public static readonly SoundStyle Item59;

		// Token: 0x04004A19 RID: 18969
		public static readonly SoundStyle Item60;

		// Token: 0x04004A1A RID: 18970
		public static readonly SoundStyle Item61;

		// Token: 0x04004A1B RID: 18971
		public static readonly SoundStyle Item62;

		// Token: 0x04004A1C RID: 18972
		public static readonly SoundStyle Item63;

		// Token: 0x04004A1D RID: 18973
		public static readonly SoundStyle Item64;

		// Token: 0x04004A1E RID: 18974
		public static readonly SoundStyle Item65;

		// Token: 0x04004A1F RID: 18975
		public static readonly SoundStyle Item66;

		// Token: 0x04004A20 RID: 18976
		public static readonly SoundStyle Item67;

		// Token: 0x04004A21 RID: 18977
		public static readonly SoundStyle Item68;

		// Token: 0x04004A22 RID: 18978
		public static readonly SoundStyle Item69;

		// Token: 0x04004A23 RID: 18979
		public static readonly SoundStyle Item70;

		// Token: 0x04004A24 RID: 18980
		public static readonly SoundStyle Item71;

		// Token: 0x04004A25 RID: 18981
		public static readonly SoundStyle Item72;

		// Token: 0x04004A26 RID: 18982
		public static readonly SoundStyle Item73;

		// Token: 0x04004A27 RID: 18983
		public static readonly SoundStyle Item74;

		// Token: 0x04004A28 RID: 18984
		public static readonly SoundStyle Item75;

		// Token: 0x04004A29 RID: 18985
		public static readonly SoundStyle Item76;

		// Token: 0x04004A2A RID: 18986
		public static readonly SoundStyle Item77;

		// Token: 0x04004A2B RID: 18987
		public static readonly SoundStyle Item78;

		// Token: 0x04004A2C RID: 18988
		public static readonly SoundStyle Item79;

		// Token: 0x04004A2D RID: 18989
		public static readonly SoundStyle Item80;

		// Token: 0x04004A2E RID: 18990
		public static readonly SoundStyle Item81;

		// Token: 0x04004A2F RID: 18991
		public static readonly SoundStyle Item82;

		// Token: 0x04004A30 RID: 18992
		public static readonly SoundStyle Item83;

		// Token: 0x04004A31 RID: 18993
		public static readonly SoundStyle Item84;

		// Token: 0x04004A32 RID: 18994
		public static readonly SoundStyle Item85;

		// Token: 0x04004A33 RID: 18995
		public static readonly SoundStyle Item86;

		// Token: 0x04004A34 RID: 18996
		public static readonly SoundStyle Item87;

		// Token: 0x04004A35 RID: 18997
		public static readonly SoundStyle Item88;

		// Token: 0x04004A36 RID: 18998
		public static readonly SoundStyle Item89;

		// Token: 0x04004A37 RID: 18999
		public static readonly SoundStyle Item90;

		// Token: 0x04004A38 RID: 19000
		public static readonly SoundStyle Item91;

		// Token: 0x04004A39 RID: 19001
		public static readonly SoundStyle Item92;

		// Token: 0x04004A3A RID: 19002
		public static readonly SoundStyle Item93;

		// Token: 0x04004A3B RID: 19003
		public static readonly SoundStyle Item94;

		// Token: 0x04004A3C RID: 19004
		public static readonly SoundStyle Item95;

		// Token: 0x04004A3D RID: 19005
		public static readonly SoundStyle Item96;

		// Token: 0x04004A3E RID: 19006
		public static readonly SoundStyle Item97;

		// Token: 0x04004A3F RID: 19007
		public static readonly SoundStyle Item98;

		// Token: 0x04004A40 RID: 19008
		public static readonly SoundStyle Item99;

		// Token: 0x04004A41 RID: 19009
		public static readonly SoundStyle Item100;

		// Token: 0x04004A42 RID: 19010
		public static readonly SoundStyle Item101;

		// Token: 0x04004A43 RID: 19011
		public static readonly SoundStyle Item102;

		// Token: 0x04004A44 RID: 19012
		public static readonly SoundStyle Item103;

		// Token: 0x04004A45 RID: 19013
		public static readonly SoundStyle Item104;

		// Token: 0x04004A46 RID: 19014
		public static readonly SoundStyle Item105;

		// Token: 0x04004A47 RID: 19015
		public static readonly SoundStyle Item106;

		// Token: 0x04004A48 RID: 19016
		public static readonly SoundStyle Item107;

		// Token: 0x04004A49 RID: 19017
		public static readonly SoundStyle Item108;

		// Token: 0x04004A4A RID: 19018
		public static readonly SoundStyle Item109;

		// Token: 0x04004A4B RID: 19019
		public static readonly SoundStyle Item110;

		// Token: 0x04004A4C RID: 19020
		public static readonly SoundStyle Item111;

		// Token: 0x04004A4D RID: 19021
		public static readonly SoundStyle Item112;

		// Token: 0x04004A4E RID: 19022
		public static readonly SoundStyle Item113;

		// Token: 0x04004A4F RID: 19023
		public static readonly SoundStyle Item114;

		// Token: 0x04004A50 RID: 19024
		public static readonly SoundStyle Item115;

		// Token: 0x04004A51 RID: 19025
		public static readonly SoundStyle Item116;

		// Token: 0x04004A52 RID: 19026
		public static readonly SoundStyle Item117;

		// Token: 0x04004A53 RID: 19027
		public static readonly SoundStyle Item118;

		// Token: 0x04004A54 RID: 19028
		public static readonly SoundStyle Item119;

		// Token: 0x04004A55 RID: 19029
		public static readonly SoundStyle Item120;

		// Token: 0x04004A56 RID: 19030
		public static readonly SoundStyle Item121;

		// Token: 0x04004A57 RID: 19031
		public static readonly SoundStyle Item122;

		// Token: 0x04004A58 RID: 19032
		public static readonly SoundStyle Item123;

		// Token: 0x04004A59 RID: 19033
		public static readonly SoundStyle Item124;

		// Token: 0x04004A5A RID: 19034
		public static readonly SoundStyle Item125;

		// Token: 0x04004A5B RID: 19035
		public static readonly SoundStyle Item126;

		// Token: 0x04004A5C RID: 19036
		public static readonly SoundStyle Item127;

		// Token: 0x04004A5D RID: 19037
		public static readonly SoundStyle Item128;

		// Token: 0x04004A5E RID: 19038
		public static readonly SoundStyle Item129;

		// Token: 0x04004A5F RID: 19039
		public static readonly SoundStyle Item130;

		// Token: 0x04004A60 RID: 19040
		public static readonly SoundStyle Item131;

		// Token: 0x04004A61 RID: 19041
		public static readonly SoundStyle Item132;

		// Token: 0x04004A62 RID: 19042
		public static readonly SoundStyle Item133;

		// Token: 0x04004A63 RID: 19043
		public static readonly SoundStyle Item134;

		// Token: 0x04004A64 RID: 19044
		public static readonly SoundStyle Item135;

		// Token: 0x04004A65 RID: 19045
		public static readonly SoundStyle Item136;

		// Token: 0x04004A66 RID: 19046
		public static readonly SoundStyle Item137;

		// Token: 0x04004A67 RID: 19047
		public static readonly SoundStyle Item138;

		// Token: 0x04004A68 RID: 19048
		public static readonly SoundStyle Item139;

		// Token: 0x04004A69 RID: 19049
		public static readonly SoundStyle Item140;

		// Token: 0x04004A6A RID: 19050
		public static readonly SoundStyle Item141;

		// Token: 0x04004A6B RID: 19051
		public static readonly SoundStyle Item142;

		// Token: 0x04004A6C RID: 19052
		public static readonly SoundStyle Item143;

		// Token: 0x04004A6D RID: 19053
		public static readonly SoundStyle Item144;

		// Token: 0x04004A6E RID: 19054
		public static readonly SoundStyle Item145;

		// Token: 0x04004A6F RID: 19055
		public static readonly SoundStyle Item146;

		// Token: 0x04004A70 RID: 19056
		public static readonly SoundStyle Item147;

		// Token: 0x04004A71 RID: 19057
		public static readonly SoundStyle Item148;

		// Token: 0x04004A72 RID: 19058
		public static readonly SoundStyle Item149;

		// Token: 0x04004A73 RID: 19059
		public static readonly SoundStyle Item150;

		// Token: 0x04004A74 RID: 19060
		public static readonly SoundStyle Item151;

		// Token: 0x04004A75 RID: 19061
		public static readonly SoundStyle Item152;

		// Token: 0x04004A76 RID: 19062
		public static readonly SoundStyle Item153;

		// Token: 0x04004A77 RID: 19063
		public static readonly SoundStyle Item154;

		// Token: 0x04004A78 RID: 19064
		public static readonly SoundStyle Item155;

		// Token: 0x04004A79 RID: 19065
		public static readonly SoundStyle Item156;

		// Token: 0x04004A7A RID: 19066
		public static readonly SoundStyle Item157;

		// Token: 0x04004A7B RID: 19067
		public static readonly SoundStyle Item158;

		// Token: 0x04004A7C RID: 19068
		public static readonly SoundStyle Item159;

		// Token: 0x04004A7D RID: 19069
		public static readonly SoundStyle Item160;

		// Token: 0x04004A7E RID: 19070
		public static readonly SoundStyle Item161;

		// Token: 0x04004A7F RID: 19071
		public static readonly SoundStyle Item162;

		// Token: 0x04004A80 RID: 19072
		public static readonly SoundStyle Item163;

		// Token: 0x04004A81 RID: 19073
		public static readonly SoundStyle Item164;

		// Token: 0x04004A82 RID: 19074
		public static readonly SoundStyle Item165;

		// Token: 0x04004A83 RID: 19075
		public static readonly SoundStyle Item166;

		// Token: 0x04004A84 RID: 19076
		public static readonly SoundStyle Item167;

		// Token: 0x04004A85 RID: 19077
		public static readonly SoundStyle Item168;

		// Token: 0x04004A86 RID: 19078
		public static readonly SoundStyle Item169;

		// Token: 0x04004A87 RID: 19079
		public static readonly SoundStyle Item170;

		// Token: 0x04004A88 RID: 19080
		public static readonly SoundStyle Item171;

		// Token: 0x04004A89 RID: 19081
		public static readonly SoundStyle Item172;

		// Token: 0x04004A8A RID: 19082
		public static readonly SoundStyle Item173;

		// Token: 0x04004A8B RID: 19083
		public static readonly SoundStyle Item174;

		// Token: 0x04004A8C RID: 19084
		public static readonly SoundStyle Item175;

		// Token: 0x04004A8D RID: 19085
		public static readonly SoundStyle Item176;

		// Token: 0x04004A8E RID: 19086
		public static readonly SoundStyle Item177;

		// Token: 0x04004A8F RID: 19087
		public static readonly SoundStyle Item178;

		// Token: 0x04004A90 RID: 19088
		public static readonly SoundStyle Zombie1;

		// Token: 0x04004A91 RID: 19089
		public static readonly SoundStyle Zombie2;

		// Token: 0x04004A92 RID: 19090
		public static readonly SoundStyle Zombie3;

		// Token: 0x04004A93 RID: 19091
		public static readonly SoundStyle Zombie4;

		// Token: 0x04004A94 RID: 19092
		public static readonly SoundStyle Zombie5;

		// Token: 0x04004A95 RID: 19093
		public static readonly SoundStyle Zombie6;

		// Token: 0x04004A96 RID: 19094
		public static readonly SoundStyle Zombie7;

		// Token: 0x04004A97 RID: 19095
		public static readonly SoundStyle Zombie8;

		// Token: 0x04004A98 RID: 19096
		public static readonly SoundStyle Zombie9;

		// Token: 0x04004A99 RID: 19097
		public static readonly SoundStyle Zombie10;

		// Token: 0x04004A9A RID: 19098
		public static readonly SoundStyle Zombie11;

		// Token: 0x04004A9B RID: 19099
		public static readonly SoundStyle Zombie12;

		// Token: 0x04004A9C RID: 19100
		public static readonly SoundStyle Zombie13;

		// Token: 0x04004A9D RID: 19101
		public static readonly SoundStyle Zombie14;

		// Token: 0x04004A9E RID: 19102
		public static readonly SoundStyle Zombie15;

		// Token: 0x04004A9F RID: 19103
		public static readonly SoundStyle Zombie16;

		// Token: 0x04004AA0 RID: 19104
		public static readonly SoundStyle Zombie17;

		// Token: 0x04004AA1 RID: 19105
		public static readonly SoundStyle Zombie18;

		// Token: 0x04004AA2 RID: 19106
		public static readonly SoundStyle Zombie19;

		// Token: 0x04004AA3 RID: 19107
		public static readonly SoundStyle Zombie20;

		// Token: 0x04004AA4 RID: 19108
		public static readonly SoundStyle Zombie21;

		// Token: 0x04004AA5 RID: 19109
		public static readonly SoundStyle Zombie22;

		// Token: 0x04004AA6 RID: 19110
		public static readonly SoundStyle Zombie23;

		// Token: 0x04004AA7 RID: 19111
		public static readonly SoundStyle Zombie24;

		// Token: 0x04004AA8 RID: 19112
		public static readonly SoundStyle Zombie25;

		// Token: 0x04004AA9 RID: 19113
		public static readonly SoundStyle Zombie26;

		// Token: 0x04004AAA RID: 19114
		public static readonly SoundStyle Zombie27;

		// Token: 0x04004AAB RID: 19115
		public static readonly SoundStyle Zombie28;

		// Token: 0x04004AAC RID: 19116
		public static readonly SoundStyle Zombie29;

		// Token: 0x04004AAD RID: 19117
		public static readonly SoundStyle Zombie30;

		// Token: 0x04004AAE RID: 19118
		public static readonly SoundStyle Zombie31;

		// Token: 0x04004AAF RID: 19119
		public static readonly SoundStyle Zombie32;

		// Token: 0x04004AB0 RID: 19120
		public static readonly SoundStyle Zombie33;

		// Token: 0x04004AB1 RID: 19121
		public static readonly SoundStyle Zombie34;

		// Token: 0x04004AB2 RID: 19122
		public static readonly SoundStyle Zombie35;

		// Token: 0x04004AB3 RID: 19123
		public static readonly SoundStyle Zombie36;

		// Token: 0x04004AB4 RID: 19124
		public static readonly SoundStyle Zombie37;

		// Token: 0x04004AB5 RID: 19125
		public static readonly SoundStyle Zombie38;

		// Token: 0x04004AB6 RID: 19126
		public static readonly SoundStyle Zombie39;

		// Token: 0x04004AB7 RID: 19127
		public static readonly SoundStyle Zombie40;

		// Token: 0x04004AB8 RID: 19128
		public static readonly SoundStyle Zombie41;

		// Token: 0x04004AB9 RID: 19129
		public static readonly SoundStyle Zombie42;

		// Token: 0x04004ABA RID: 19130
		public static readonly SoundStyle Zombie43;

		// Token: 0x04004ABB RID: 19131
		public static readonly SoundStyle Zombie44;

		// Token: 0x04004ABC RID: 19132
		public static readonly SoundStyle Zombie45;

		// Token: 0x04004ABD RID: 19133
		public static readonly SoundStyle Zombie46;

		// Token: 0x04004ABE RID: 19134
		public static readonly SoundStyle Zombie47;

		// Token: 0x04004ABF RID: 19135
		public static readonly SoundStyle Zombie48;

		// Token: 0x04004AC0 RID: 19136
		public static readonly SoundStyle Zombie49;

		// Token: 0x04004AC1 RID: 19137
		public static readonly SoundStyle Zombie50;

		// Token: 0x04004AC2 RID: 19138
		public static readonly SoundStyle Zombie51;

		// Token: 0x04004AC3 RID: 19139
		public static readonly SoundStyle Zombie52;

		// Token: 0x04004AC4 RID: 19140
		public static readonly SoundStyle Zombie53;

		// Token: 0x04004AC5 RID: 19141
		public static readonly SoundStyle Zombie54;

		// Token: 0x04004AC6 RID: 19142
		public static readonly SoundStyle Zombie55;

		// Token: 0x04004AC7 RID: 19143
		public static readonly SoundStyle Zombie56;

		// Token: 0x04004AC8 RID: 19144
		public static readonly SoundStyle Zombie57;

		// Token: 0x04004AC9 RID: 19145
		public static readonly SoundStyle Zombie58;

		// Token: 0x04004ACA RID: 19146
		public static readonly SoundStyle Zombie59;

		// Token: 0x04004ACB RID: 19147
		public static readonly SoundStyle Zombie60;

		// Token: 0x04004ACC RID: 19148
		public static readonly SoundStyle Zombie61;

		// Token: 0x04004ACD RID: 19149
		public static readonly SoundStyle Zombie62;

		// Token: 0x04004ACE RID: 19150
		public static readonly SoundStyle Zombie63;

		// Token: 0x04004ACF RID: 19151
		public static readonly SoundStyle Zombie64;

		// Token: 0x04004AD0 RID: 19152
		public static readonly SoundStyle Zombie65;

		// Token: 0x04004AD1 RID: 19153
		public static readonly SoundStyle Zombie66;

		// Token: 0x04004AD2 RID: 19154
		public static readonly SoundStyle Zombie67;

		// Token: 0x04004AD3 RID: 19155
		public static readonly SoundStyle Zombie68;

		// Token: 0x04004AD4 RID: 19156
		public static readonly SoundStyle Zombie69;

		// Token: 0x04004AD5 RID: 19157
		public static readonly SoundStyle Zombie70;

		// Token: 0x04004AD6 RID: 19158
		public static readonly SoundStyle Zombie71;

		// Token: 0x04004AD7 RID: 19159
		public static readonly SoundStyle Zombie72;

		// Token: 0x04004AD8 RID: 19160
		public static readonly SoundStyle Zombie73;

		// Token: 0x04004AD9 RID: 19161
		public static readonly SoundStyle Zombie74;

		// Token: 0x04004ADA RID: 19162
		public static readonly SoundStyle Zombie75;

		// Token: 0x04004ADB RID: 19163
		public static readonly SoundStyle Zombie76;

		// Token: 0x04004ADC RID: 19164
		public static readonly SoundStyle Zombie77;

		// Token: 0x04004ADD RID: 19165
		public static readonly SoundStyle Zombie78;

		// Token: 0x04004ADE RID: 19166
		public static readonly SoundStyle Zombie79;

		// Token: 0x04004ADF RID: 19167
		public static readonly SoundStyle Zombie80;

		// Token: 0x04004AE0 RID: 19168
		public static readonly SoundStyle Zombie81;

		// Token: 0x04004AE1 RID: 19169
		public static readonly SoundStyle Zombie82;

		// Token: 0x04004AE2 RID: 19170
		public static readonly SoundStyle Zombie83;

		// Token: 0x04004AE3 RID: 19171
		public static readonly SoundStyle Zombie84;

		// Token: 0x04004AE4 RID: 19172
		public static readonly SoundStyle Zombie85;

		// Token: 0x04004AE5 RID: 19173
		public static readonly SoundStyle Zombie86;

		// Token: 0x04004AE6 RID: 19174
		public static readonly SoundStyle Zombie87;

		// Token: 0x04004AE7 RID: 19175
		public static readonly SoundStyle Zombie88;

		// Token: 0x04004AE8 RID: 19176
		public static readonly SoundStyle Zombie89;

		// Token: 0x04004AE9 RID: 19177
		public static readonly SoundStyle Zombie90;

		// Token: 0x04004AEA RID: 19178
		public static readonly SoundStyle Zombie91;

		// Token: 0x04004AEB RID: 19179
		public static readonly SoundStyle Zombie92;

		// Token: 0x04004AEC RID: 19180
		public static readonly SoundStyle Zombie93;

		// Token: 0x04004AED RID: 19181
		public static readonly SoundStyle Zombie94;

		// Token: 0x04004AEE RID: 19182
		public static readonly SoundStyle Zombie95;

		// Token: 0x04004AEF RID: 19183
		public static readonly SoundStyle Zombie96;

		// Token: 0x04004AF0 RID: 19184
		public static readonly SoundStyle Zombie97;

		// Token: 0x04004AF1 RID: 19185
		public static readonly SoundStyle Zombie98;

		// Token: 0x04004AF2 RID: 19186
		public static readonly SoundStyle Zombie99;

		// Token: 0x04004AF3 RID: 19187
		public static readonly SoundStyle Zombie100;

		// Token: 0x04004AF4 RID: 19188
		public static readonly SoundStyle Zombie101;

		// Token: 0x04004AF5 RID: 19189
		public static readonly SoundStyle Zombie102;

		// Token: 0x04004AF6 RID: 19190
		public static readonly SoundStyle Zombie103;

		// Token: 0x04004AF7 RID: 19191
		public static readonly SoundStyle Zombie104;

		// Token: 0x04004AF8 RID: 19192
		public static readonly SoundStyle Zombie105;

		// Token: 0x04004AF9 RID: 19193
		public static readonly SoundStyle Zombie106;

		// Token: 0x04004AFA RID: 19194
		public static readonly SoundStyle Zombie107;

		// Token: 0x04004AFB RID: 19195
		public static readonly SoundStyle Zombie108;

		// Token: 0x04004AFC RID: 19196
		public static readonly SoundStyle Zombie109;

		// Token: 0x04004AFD RID: 19197
		public static readonly SoundStyle Zombie110;

		// Token: 0x04004AFE RID: 19198
		public static readonly SoundStyle Zombie111;

		// Token: 0x04004AFF RID: 19199
		public static readonly SoundStyle Zombie112;

		// Token: 0x04004B00 RID: 19200
		public static readonly SoundStyle Zombie113;

		// Token: 0x04004B01 RID: 19201
		public static readonly SoundStyle Zombie114;

		// Token: 0x04004B02 RID: 19202
		public static readonly SoundStyle Zombie115;

		// Token: 0x04004B03 RID: 19203
		public static readonly SoundStyle Zombie116;

		// Token: 0x04004B04 RID: 19204
		public static readonly SoundStyle Zombie117;

		// Token: 0x04004B05 RID: 19205
		public static readonly SoundStyle Zombie118;

		// Token: 0x04004B06 RID: 19206
		public static readonly SoundStyle Zombie119;

		// Token: 0x04004B07 RID: 19207
		public static readonly SoundStyle Zombie120;

		// Token: 0x04004B08 RID: 19208
		public static readonly SoundStyle Zombie121;

		// Token: 0x04004B09 RID: 19209
		public static readonly SoundStyle Zombie122;

		// Token: 0x04004B0A RID: 19210
		public static readonly SoundStyle Zombie123;

		// Token: 0x04004B0B RID: 19211
		public static readonly SoundStyle Zombie124;

		// Token: 0x04004B0C RID: 19212
		public static readonly SoundStyle Zombie125;

		// Token: 0x04004B0D RID: 19213
		public static readonly SoundStyle Zombie126;

		// Token: 0x04004B0E RID: 19214
		public static readonly SoundStyle Zombie127;

		// Token: 0x04004B0F RID: 19215
		public static readonly SoundStyle Zombie128;

		// Token: 0x04004B10 RID: 19216
		public static readonly SoundStyle Zombie129;

		// Token: 0x04004B11 RID: 19217
		public static readonly SoundStyle Zombie130;

		// Token: 0x04004B12 RID: 19218
		public static readonly int ZombieSoundCount;

		// Token: 0x04004B13 RID: 19219
		private static SoundStyle[][] legacyArrayedStylesMapping;

		// Token: 0x02000B67 RID: 2919
		internal struct SoundStyleDefaults
		{
			// Token: 0x06005CBD RID: 23741 RVA: 0x006C3880 File Offset: 0x006C1A80
			public SoundStyleDefaults(float volume, float pitchVariance, SoundType type = SoundType.Sound)
			{
				this.PitchVariance = pitchVariance;
				this.Volume = volume;
				this.Type = type;
			}

			// Token: 0x06005CBE RID: 23742 RVA: 0x006C3897 File Offset: 0x006C1A97
			public void Apply(ref SoundStyle style)
			{
				style.PitchVariance = this.PitchVariance;
				style.Volume = this.Volume;
				style.Type = this.Type;
			}

			// Token: 0x04007539 RID: 30009
			public readonly float PitchVariance;

			// Token: 0x0400753A RID: 30010
			public readonly float Volume;

			// Token: 0x0400753B RID: 30011
			public readonly SoundType Type;

			// Token: 0x0400753C RID: 30012
			public static readonly SoundID.SoundStyleDefaults ItemDefaults = new SoundID.SoundStyleDefaults(1f, 0.12f, SoundType.Sound);

			// Token: 0x0400753D RID: 30013
			public static readonly SoundID.SoundStyleDefaults NPCHitDefaults = new SoundID.SoundStyleDefaults(1f, 0.2f, SoundType.Sound);

			// Token: 0x0400753E RID: 30014
			public static readonly SoundID.SoundStyleDefaults NPCDeathDefaults = new SoundID.SoundStyleDefaults(1f, 0.2f, SoundType.Sound);

			// Token: 0x0400753F RID: 30015
			public static readonly SoundID.SoundStyleDefaults ZombieDefaults = new SoundID.SoundStyleDefaults(1f, 0.2f, SoundType.Sound);
		}
	}
}
