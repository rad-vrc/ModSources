using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.GameContent.Events;
using Terraria.Localization;

namespace Terraria
{
	// Token: 0x02000028 RID: 40
	public sealed class Condition : IEquatable<Condition>
	{
		// Token: 0x06000185 RID: 389 RVA: 0x00017087 File Offset: 0x00015287
		public Condition(LocalizedText Description, Func<bool> Predicate)
		{
			this.Description = Description;
			this.Predicate = Predicate;
			base..ctor();
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000186 RID: 390 RVA: 0x0001709D File Offset: 0x0001529D
		[Nullable(1)]
		[CompilerGenerated]
		private Type EqualityContract
		{
			[NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(Condition);
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000187 RID: 391 RVA: 0x000170A9 File Offset: 0x000152A9
		// (set) Token: 0x06000188 RID: 392 RVA: 0x000170B1 File Offset: 0x000152B1
		public LocalizedText Description { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000189 RID: 393 RVA: 0x000170BA File Offset: 0x000152BA
		// (set) Token: 0x0600018A RID: 394 RVA: 0x000170C2 File Offset: 0x000152C2
		public Func<bool> Predicate { get; set; }

		// Token: 0x0600018B RID: 395 RVA: 0x000170CB File Offset: 0x000152CB
		public Condition(string LocalizationKey, Func<bool> Predicate) : this(Language.GetOrRegister(LocalizationKey, null), Predicate)
		{
		}

		// Token: 0x0600018C RID: 396 RVA: 0x000170DB File Offset: 0x000152DB
		public bool IsMet()
		{
			return this.Predicate();
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000170E8 File Offset: 0x000152E8
		public static Condition PlayerCarriesItem(int itemId)
		{
			return new Condition(Language.GetText("Conditions.PlayerCarriesItem").WithFormatArgs(new object[]
			{
				Lang.GetItemName(itemId)
			}), () => Main.LocalPlayer.HasItem(itemId));
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00017138 File Offset: 0x00015338
		public static Condition GolfScoreOver(int score)
		{
			return new Condition(Language.GetText("Conditions.GolfScoreOver").WithFormatArgs(new object[]
			{
				score
			}), () => Main.LocalPlayer.golferScoreAccumulated >= score);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00017188 File Offset: 0x00015388
		public static Condition NpcIsPresent(int npcId)
		{
			return new Condition(Language.GetText("Conditions.NpcIsPresent").WithFormatArgs(new object[]
			{
				Lang.GetNPCName(npcId)
			}), () => NPC.AnyNPCs(npcId));
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000171D8 File Offset: 0x000153D8
		public static Condition AnglerQuestsFinishedOver(int quests)
		{
			return new Condition(Language.GetText("Conditions.AnglerQuestsFinishedOver").WithFormatArgs(new object[]
			{
				quests
			}), () => Main.LocalPlayer.anglerQuestsFinished >= quests);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00017228 File Offset: 0x00015428
		public static Condition BestiaryFilledPercent(int percent)
		{
			if (percent >= 100)
			{
				return new Condition("Conditions.BestiaryFull", () => Main.GetBestiaryProgressReport().CompletionPercent >= 1f);
			}
			return new Condition(Language.GetText("Conditions.BestiaryPercentage").WithFormatArgs(new object[]
			{
				percent
			}), () => Main.GetBestiaryProgressReport().CompletionPercent >= (float)percent / 100f);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x000172AC File Offset: 0x000154AC
		[NullableContext(1)]
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Condition");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000193 RID: 403 RVA: 0x000172F8 File Offset: 0x000154F8
		[NullableContext(1)]
		[CompilerGenerated]
		private bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Description = ");
			builder.Append(this.Description);
			builder.Append(", Predicate = ");
			builder.Append(this.Predicate);
			return true;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00017332 File Offset: 0x00015532
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(Condition left, Condition right)
		{
			return !(left == right);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0001733E File Offset: 0x0001553E
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(Condition left, Condition right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00017352 File Offset: 0x00015552
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<LocalizedText>.Default.GetHashCode(this.<Description>k__BackingField)) * -1521134295 + EqualityComparer<Func<bool>>.Default.GetHashCode(this.<Predicate>k__BackingField);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00017392 File Offset: 0x00015592
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as Condition);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x000173A0 File Offset: 0x000155A0
		[NullableContext(2)]
		[CompilerGenerated]
		public bool Equals(Condition other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<LocalizedText>.Default.Equals(this.<Description>k__BackingField, other.<Description>k__BackingField) && EqualityComparer<Func<bool>>.Default.Equals(this.<Predicate>k__BackingField, other.<Predicate>k__BackingField));
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00017401 File Offset: 0x00015601
		[CompilerGenerated]
		private Condition([Nullable(1)] Condition original)
		{
			this.Description = original.<Description>k__BackingField;
			this.Predicate = original.<Predicate>k__BackingField;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00017421 File Offset: 0x00015621
		[CompilerGenerated]
		public void Deconstruct(out LocalizedText Description, out Func<bool> Predicate)
		{
			Description = this.Description;
			Predicate = this.Predicate;
		}

		// Token: 0x040000DE RID: 222
		public static readonly Condition NearWater = new Condition("Conditions.NearWater", () => Main.LocalPlayer.adjWater || Main.LocalPlayer.adjTile[172]);

		// Token: 0x040000DF RID: 223
		public static readonly Condition NearLava = new Condition("Conditions.NearLava", () => Main.LocalPlayer.adjLava);

		// Token: 0x040000E0 RID: 224
		public static readonly Condition NearHoney = new Condition("Conditions.NearHoney", () => Main.LocalPlayer.adjHoney);

		// Token: 0x040000E1 RID: 225
		public static readonly Condition NearShimmer = new Condition("Conditions.NearShimmer", () => Main.LocalPlayer.adjShimmer);

		// Token: 0x040000E2 RID: 226
		public static readonly Condition TimeDay = new Condition("Conditions.TimeDay", () => Main.dayTime);

		// Token: 0x040000E3 RID: 227
		public static readonly Condition TimeNight = new Condition("Conditions.TimeNight", () => !Main.dayTime);

		// Token: 0x040000E4 RID: 228
		public static readonly Condition InDungeon = new Condition("Conditions.InDungeon", () => Main.LocalPlayer.ZoneDungeon);

		// Token: 0x040000E5 RID: 229
		public static readonly Condition InCorrupt = new Condition("Conditions.InCorrupt", () => Main.LocalPlayer.ZoneCorrupt);

		// Token: 0x040000E6 RID: 230
		public static readonly Condition InHallow = new Condition("Conditions.InHallow", () => Main.LocalPlayer.ZoneHallow);

		// Token: 0x040000E7 RID: 231
		public static readonly Condition InMeteor = new Condition("Conditions.InMeteor", () => Main.LocalPlayer.ZoneMeteor);

		// Token: 0x040000E8 RID: 232
		public static readonly Condition InJungle = new Condition("Conditions.InJungle", () => Main.LocalPlayer.ZoneJungle);

		// Token: 0x040000E9 RID: 233
		public static readonly Condition InSnow = new Condition("Conditions.InSnow", () => Main.LocalPlayer.ZoneSnow);

		// Token: 0x040000EA RID: 234
		public static readonly Condition InCrimson = new Condition("Conditions.InCrimson", () => Main.LocalPlayer.ZoneCrimson);

		// Token: 0x040000EB RID: 235
		public static readonly Condition InWaterCandle = new Condition("Conditions.InWaterCandle", () => Main.LocalPlayer.ZoneWaterCandle);

		// Token: 0x040000EC RID: 236
		public static readonly Condition InPeaceCandle = new Condition("Conditions.InPeaceCandle", () => Main.LocalPlayer.ZonePeaceCandle);

		// Token: 0x040000ED RID: 237
		public static readonly Condition InTowerSolar = new Condition("Conditions.InTowerSolar", () => Main.LocalPlayer.ZoneTowerSolar);

		// Token: 0x040000EE RID: 238
		public static readonly Condition InTowerVortex = new Condition("Conditions.InTowerVortex", () => Main.LocalPlayer.ZoneTowerVortex);

		// Token: 0x040000EF RID: 239
		public static readonly Condition InTowerNebula = new Condition("Conditions.InTowerNebula", () => Main.LocalPlayer.ZoneTowerNebula);

		// Token: 0x040000F0 RID: 240
		public static readonly Condition InTowerStardust = new Condition("Conditions.InTowerStardust", () => Main.LocalPlayer.ZoneTowerStardust);

		// Token: 0x040000F1 RID: 241
		public static readonly Condition InDesert = new Condition("Conditions.InDesert", () => Main.LocalPlayer.ZoneDesert);

		// Token: 0x040000F2 RID: 242
		public static readonly Condition InGlowshroom = new Condition("Conditions.InGlowshroom", () => Main.LocalPlayer.ZoneGlowshroom);

		// Token: 0x040000F3 RID: 243
		public static readonly Condition InUndergroundDesert = new Condition("Conditions.InUndergroundDesert", () => Main.LocalPlayer.ZoneUndergroundDesert);

		// Token: 0x040000F4 RID: 244
		public static readonly Condition InSkyHeight = new Condition("Conditions.InSkyHeight", () => Main.LocalPlayer.ZoneSkyHeight);

		// Token: 0x040000F5 RID: 245
		public static readonly Condition InSpace = Condition.InSkyHeight;

		// Token: 0x040000F6 RID: 246
		public static readonly Condition InOverworldHeight = new Condition("Conditions.InOverworldHeight", () => Main.LocalPlayer.ZoneOverworldHeight);

		// Token: 0x040000F7 RID: 247
		public static readonly Condition InDirtLayerHeight = new Condition("Conditions.InDirtLayerHeight", () => Main.LocalPlayer.ZoneDirtLayerHeight);

		// Token: 0x040000F8 RID: 248
		public static readonly Condition InRockLayerHeight = new Condition("Conditions.InRockLayerHeight", () => Main.LocalPlayer.ZoneRockLayerHeight);

		// Token: 0x040000F9 RID: 249
		public static readonly Condition InUnderworldHeight = new Condition("Conditions.InUnderworldHeight", () => Main.LocalPlayer.ZoneUnderworldHeight);

		// Token: 0x040000FA RID: 250
		public static readonly Condition InUnderworld = Condition.InUnderworldHeight;

		// Token: 0x040000FB RID: 251
		public static readonly Condition InBeach = new Condition("Conditions.InBeach", () => Main.LocalPlayer.ZoneBeach);

		// Token: 0x040000FC RID: 252
		public static readonly Condition InRain = new Condition("Conditions.InRain", () => Main.LocalPlayer.ZoneRain);

		// Token: 0x040000FD RID: 253
		public static readonly Condition InSandstorm = new Condition("Conditions.InSandstorm", () => Main.LocalPlayer.ZoneSandstorm);

		// Token: 0x040000FE RID: 254
		public static readonly Condition InOldOneArmy = new Condition("Conditions.InOldOneArmy", () => Main.LocalPlayer.ZoneOldOneArmy);

		// Token: 0x040000FF RID: 255
		public static readonly Condition InGranite = new Condition("Conditions.InGranite", () => Main.LocalPlayer.ZoneGranite);

		// Token: 0x04000100 RID: 256
		public static readonly Condition InMarble = new Condition("Conditions.InMarble", () => Main.LocalPlayer.ZoneMarble);

		// Token: 0x04000101 RID: 257
		public static readonly Condition InHive = new Condition("Conditions.InHive", () => Main.LocalPlayer.ZoneHive);

		// Token: 0x04000102 RID: 258
		public static readonly Condition InGemCave = new Condition("Conditions.InGemCave", () => Main.LocalPlayer.ZoneGemCave);

		// Token: 0x04000103 RID: 259
		public static readonly Condition InLihzhardTemple = new Condition("Conditions.InLihzardTemple", () => Main.LocalPlayer.ZoneLihzhardTemple);

		// Token: 0x04000104 RID: 260
		public static readonly Condition InGraveyard = new Condition("Conditions.InGraveyard", () => Main.LocalPlayer.ZoneGraveyard);

		// Token: 0x04000105 RID: 261
		public static readonly Condition InAether = new Condition("Conditions.InAether", () => Main.LocalPlayer.ZoneShimmer);

		// Token: 0x04000106 RID: 262
		public static readonly Condition InShoppingZoneForest = new Condition("Conditions.InShoppingForest", () => Main.LocalPlayer.ShoppingZone_Forest);

		// Token: 0x04000107 RID: 263
		public static readonly Condition InBelowSurface = new Condition("Conditions.InBelowSurface", () => Main.LocalPlayer.ShoppingZone_BelowSurface);

		// Token: 0x04000108 RID: 264
		public static readonly Condition InEvilBiome = new Condition("Conditions.InEvilBiome", () => Main.LocalPlayer.ZoneCrimson || Main.LocalPlayer.ZoneCorrupt);

		// Token: 0x04000109 RID: 265
		public static readonly Condition NotInEvilBiome = new Condition("Conditions.NotInEvilBiome", () => !Main.LocalPlayer.ZoneCrimson && !Main.LocalPlayer.ZoneCorrupt);

		// Token: 0x0400010A RID: 266
		public static readonly Condition NotInHallow = new Condition("Conditions.NotInHallow", () => !Main.LocalPlayer.ZoneHallow);

		// Token: 0x0400010B RID: 267
		public static readonly Condition NotInGraveyard = new Condition("Conditions.NotInGraveyard", () => !Main.LocalPlayer.ZoneGraveyard);

		// Token: 0x0400010C RID: 268
		public static readonly Condition NotInUnderworld = new Condition("Conditions.NotInUnderworld", () => !Main.LocalPlayer.ZoneUnderworldHeight);

		// Token: 0x0400010D RID: 269
		public static readonly Condition InClassicMode = new Condition("Conditions.InClassicMode", () => !Main.expertMode);

		// Token: 0x0400010E RID: 270
		public static readonly Condition InExpertMode = new Condition("Conditions.InExpertMode", () => Main.expertMode);

		// Token: 0x0400010F RID: 271
		public static readonly Condition InMasterMode = new Condition("Conditions.InMasterMode", () => Main.masterMode);

		// Token: 0x04000110 RID: 272
		public static readonly Condition InJourneyMode = new Condition("Conditions.InJourneyMode", () => Main.GameModeInfo.IsJourneyMode);

		// Token: 0x04000111 RID: 273
		public static readonly Condition Hardmode = new Condition("Conditions.InHardmode", () => Main.hardMode);

		// Token: 0x04000112 RID: 274
		public static readonly Condition PreHardmode = new Condition("Conditions.PreHardmode", () => !Main.hardMode);

		// Token: 0x04000113 RID: 275
		public static readonly Condition SmashedShadowOrb = new Condition("Conditions.SmashedShadowOrb", () => WorldGen.shadowOrbSmashed);

		// Token: 0x04000114 RID: 276
		public static readonly Condition CrimsonWorld = new Condition("Conditions.WorldCrimson", () => WorldGen.crimson);

		// Token: 0x04000115 RID: 277
		public static readonly Condition CorruptWorld = new Condition("Conditions.WorldCorrupt", () => !WorldGen.crimson);

		// Token: 0x04000116 RID: 278
		public static readonly Condition DrunkWorld = new Condition("Conditions.WorldDrunk", () => Main.drunkWorld);

		// Token: 0x04000117 RID: 279
		public static readonly Condition RemixWorld = new Condition("Conditions.WorldRemix", () => Main.remixWorld);

		// Token: 0x04000118 RID: 280
		public static readonly Condition NotTheBeesWorld = new Condition("Conditions.WorldNotTheBees", () => Main.notTheBeesWorld);

		// Token: 0x04000119 RID: 281
		public static readonly Condition ForTheWorthyWorld = new Condition("Conditions.WorldForTheWorthy", () => Main.getGoodWorld);

		// Token: 0x0400011A RID: 282
		public static readonly Condition TenthAnniversaryWorld = new Condition("Conditions.WorldAnniversary", () => Main.tenthAnniversaryWorld);

		// Token: 0x0400011B RID: 283
		public static readonly Condition DontStarveWorld = new Condition("Conditions.WorldDontStarve", () => Main.dontStarveWorld);

		// Token: 0x0400011C RID: 284
		public static readonly Condition NoTrapsWorld = new Condition("Conditions.WorldNoTraps", () => Main.noTrapsWorld);

		// Token: 0x0400011D RID: 285
		public static readonly Condition ZenithWorld = new Condition("Conditions.WorldZenith", () => Main.remixWorld && Main.getGoodWorld);

		// Token: 0x0400011E RID: 286
		public static readonly Condition NotDrunkWorld = new Condition("Conditions.WorldNotDrunk", () => !Main.drunkWorld);

		// Token: 0x0400011F RID: 287
		public static readonly Condition NotRemixWorld = new Condition("Conditions.WorldNotRemix", () => !Main.remixWorld);

		// Token: 0x04000120 RID: 288
		public static readonly Condition NotNotTheBeesWorld = new Condition("Conditions.WorldNotNotTheBees", () => !Main.notTheBeesWorld);

		// Token: 0x04000121 RID: 289
		public static readonly Condition NotForTheWorthy = new Condition("Conditions.WorldNotForTheWorthy", () => !Main.getGoodWorld);

		// Token: 0x04000122 RID: 290
		public static readonly Condition NotTenthAnniversaryWorld = new Condition("Conditions.WorldNotAnniversary", () => !Main.tenthAnniversaryWorld);

		// Token: 0x04000123 RID: 291
		public static readonly Condition NotDontStarveWorld = new Condition("Conditions.WorldNotDontStarve", () => !Main.dontStarveWorld);

		// Token: 0x04000124 RID: 292
		public static readonly Condition NotNoTrapsWorld = new Condition("Conditions.WorldNotNoTraps", () => !Main.noTrapsWorld);

		// Token: 0x04000125 RID: 293
		public static readonly Condition NotZenithWorld = new Condition("Conditions.WorldNotZenith", () => !Condition.ZenithWorld.IsMet());

		// Token: 0x04000126 RID: 294
		public static readonly Condition Christmas = new Condition("Conditions.Christmas", () => Main.xMas);

		// Token: 0x04000127 RID: 295
		public static readonly Condition Halloween = new Condition("Conditions.Halloween", () => Main.halloween);

		// Token: 0x04000128 RID: 296
		public static readonly Condition BloodMoon = new Condition("Conditions.BloodMoon", () => Main.bloodMoon);

		// Token: 0x04000129 RID: 297
		public static readonly Condition NotBloodMoon = new Condition("Conditions.NotBloodMoon", () => !Main.bloodMoon);

		// Token: 0x0400012A RID: 298
		public static readonly Condition Eclipse = new Condition("Conditions.SolarEclipse", () => Main.eclipse);

		// Token: 0x0400012B RID: 299
		public static readonly Condition NotEclipse = new Condition("Conditions.NotSolarEclipse", () => !Main.eclipse);

		// Token: 0x0400012C RID: 300
		public static readonly Condition EclipseOrBloodMoon = new Condition("Conditions.BloodOrSun", () => Main.bloodMoon || Main.eclipse);

		// Token: 0x0400012D RID: 301
		public static readonly Condition NotEclipseAndNotBloodMoon = new Condition("Conditions.NotBloodOrSun", () => !Main.bloodMoon && !Main.eclipse);

		// Token: 0x0400012E RID: 302
		public static readonly Condition Thunderstorm = new Condition("Conditions.Thunderstorm", () => Main.IsItStorming);

		// Token: 0x0400012F RID: 303
		public static readonly Condition BirthdayParty = new Condition("Conditions.BirthdayParty", () => Terraria.GameContent.Events.BirthdayParty.PartyIsUp);

		// Token: 0x04000130 RID: 304
		public static readonly Condition LanternNight = new Condition("Conditions.NightLanterns", () => Terraria.GameContent.Events.LanternNight.LanternsUp);

		// Token: 0x04000131 RID: 305
		public static readonly Condition HappyWindyDay = new Condition("Conditions.HappyWindyDay", () => Main.IsItAHappyWindyDay);

		// Token: 0x04000132 RID: 306
		public static readonly Condition DownedKingSlime = new Condition("Conditions.DownedKingSlime", () => NPC.downedSlimeKing);

		// Token: 0x04000133 RID: 307
		public static readonly Condition DownedEyeOfCthulhu = new Condition("Conditions.DownedEyeOfCthulhu", () => NPC.downedBoss1);

		// Token: 0x04000134 RID: 308
		public static readonly Condition DownedEowOrBoc = new Condition("Conditions.DownedBoss2", () => NPC.downedBoss2);

		// Token: 0x04000135 RID: 309
		public static readonly Condition DownedEaterOfWorlds = new Condition("Conditions.DownedEaterOfWorlds", () => NPC.downedBoss2 && !WorldGen.crimson);

		// Token: 0x04000136 RID: 310
		public static readonly Condition DownedBrainOfCthulhu = new Condition("Conditions.DownedBrainOfCthulhu", () => NPC.downedBoss2 && WorldGen.crimson);

		// Token: 0x04000137 RID: 311
		public static readonly Condition DownedQueenBee = new Condition("Conditions.DownedQueenBee", () => NPC.downedQueenBee);

		// Token: 0x04000138 RID: 312
		public static readonly Condition DownedSkeletron = new Condition("Conditions.DownedSkeletron", () => NPC.downedBoss3);

		// Token: 0x04000139 RID: 313
		public static readonly Condition DownedDeerclops = new Condition("Conditions.DownedDeerclops", () => NPC.downedDeerclops);

		// Token: 0x0400013A RID: 314
		public static readonly Condition DownedQueenSlime = new Condition("Conditions.DownedQueenSlime", () => NPC.downedQueenSlime);

		// Token: 0x0400013B RID: 315
		public static readonly Condition DownedEarlygameBoss = new Condition("Conditions.DownedEarlygameBoss", () => NPC.downedBoss1 || NPC.downedBoss2 || NPC.downedBoss3 || NPC.downedQueenBee || Main.hardMode);

		// Token: 0x0400013C RID: 316
		public static readonly Condition DownedMechBossAny = new Condition("Conditions.DownedMechBossAny", () => NPC.downedMechBossAny);

		// Token: 0x0400013D RID: 317
		public static readonly Condition DownedTwins = new Condition("Conditions.DownedTwins", () => NPC.downedMechBoss2);

		// Token: 0x0400013E RID: 318
		public static readonly Condition DownedDestroyer = new Condition("Conditions.DownedDestroyer", () => NPC.downedMechBoss1);

		// Token: 0x0400013F RID: 319
		public static readonly Condition DownedSkeletronPrime = new Condition("Conditions.DownedSkeletronPrime", () => NPC.downedMechBoss3);

		// Token: 0x04000140 RID: 320
		public static readonly Condition DownedMechBossAll = new Condition("Conditions.DownedMechBossAll", () => NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3);

		// Token: 0x04000141 RID: 321
		public static readonly Condition DownedPlantera = new Condition("Conditions.DownedPlantera", () => NPC.downedPlantBoss);

		// Token: 0x04000142 RID: 322
		public static readonly Condition DownedEmpressOfLight = new Condition("Conditions.DownedEmpressOfLight", () => NPC.downedEmpressOfLight);

		// Token: 0x04000143 RID: 323
		public static readonly Condition DownedDukeFishron = new Condition("Conditions.DownedDukeFishron", () => NPC.downedFishron);

		// Token: 0x04000144 RID: 324
		public static readonly Condition DownedGolem = new Condition("Conditions.DownedGolem", () => NPC.downedGolemBoss);

		// Token: 0x04000145 RID: 325
		public static readonly Condition DownedMourningWood = new Condition("Conditions.DownedMourningWood", () => NPC.downedHalloweenTree);

		// Token: 0x04000146 RID: 326
		public static readonly Condition DownedPumpking = new Condition("Conditions.DownedPumpking", () => NPC.downedHalloweenKing);

		// Token: 0x04000147 RID: 327
		public static readonly Condition DownedEverscream = new Condition("Conditions.DownedEverscream", () => NPC.downedChristmasTree);

		// Token: 0x04000148 RID: 328
		public static readonly Condition DownedSantaNK1 = new Condition("Conditions.DownedSantaNK1", () => NPC.downedChristmasSantank);

		// Token: 0x04000149 RID: 329
		public static readonly Condition DownedIceQueen = new Condition("Conditions.DownedIceQueen", () => NPC.downedChristmasIceQueen);

		// Token: 0x0400014A RID: 330
		public static readonly Condition DownedCultist = new Condition("Conditions.DownedLunaticCultist", () => NPC.downedAncientCultist);

		// Token: 0x0400014B RID: 331
		public static readonly Condition DownedMoonLord = new Condition("Conditions.DownedMoonLord", () => NPC.downedMoonlord);

		// Token: 0x0400014C RID: 332
		public static readonly Condition DownedClown = new Condition("Conditions.DownedClown", () => NPC.downedClown);

		// Token: 0x0400014D RID: 333
		public static readonly Condition DownedGoblinArmy = new Condition("Conditions.DownedGoblinArmy", () => NPC.downedGoblins);

		// Token: 0x0400014E RID: 334
		public static readonly Condition DownedPirates = new Condition("Conditions.DownedPirates", () => NPC.downedPirates);

		// Token: 0x0400014F RID: 335
		public static readonly Condition DownedMartians = new Condition("Conditions.DownedMartians", () => NPC.downedMartians);

		// Token: 0x04000150 RID: 336
		public static readonly Condition DownedFrostLegion = new Condition("Conditions.DownedFrostLegion", () => NPC.downedFrost);

		// Token: 0x04000151 RID: 337
		public static readonly Condition DownedSolarPillar = new Condition("Conditions.DownedSolarPillar", () => NPC.downedTowerSolar);

		// Token: 0x04000152 RID: 338
		public static readonly Condition DownedVortexPillar = new Condition("Conditions.DownedVortexPillar", () => NPC.downedTowerVortex);

		// Token: 0x04000153 RID: 339
		public static readonly Condition DownedNebulaPillar = new Condition("Conditions.DownedNebulaPillar", () => NPC.downedTowerNebula);

		// Token: 0x04000154 RID: 340
		public static readonly Condition DownedStardustPillar = new Condition("Conditions.DownedStardustPillar", () => NPC.downedTowerStardust);

		// Token: 0x04000155 RID: 341
		public static readonly Condition DownedOldOnesArmyAny = new Condition("Conditions.DownedOldOnesArmyAny", () => DD2Event.DownedInvasionAnyDifficulty);

		// Token: 0x04000156 RID: 342
		public static readonly Condition DownedOldOnesArmyT1 = new Condition("Conditions.DownedOldOnesArmyT1", () => DD2Event.DownedInvasionT1);

		// Token: 0x04000157 RID: 343
		public static readonly Condition DownedOldOnesArmyT2 = new Condition("Conditions.DownedOldOnesArmyT2", () => DD2Event.DownedInvasionT2);

		// Token: 0x04000158 RID: 344
		public static readonly Condition DownedOldOnesArmyT3 = new Condition("Conditions.DownedOldOnesArmyT3", () => DD2Event.DownedInvasionT3);

		// Token: 0x04000159 RID: 345
		public static readonly Condition NotDownedKingSlime = new Condition("Conditions.NotDownedKingSlime", () => !NPC.downedSlimeKing);

		// Token: 0x0400015A RID: 346
		public static readonly Condition NotDownedEyeOfCthulhu = new Condition("Conditions.NotDownedEyeOfCthulhu", () => !NPC.downedBoss1);

		// Token: 0x0400015B RID: 347
		public static readonly Condition NotDownedEowOrBoc = new Condition("Conditions.NotDownedBoss2", () => !NPC.downedBoss2);

		// Token: 0x0400015C RID: 348
		public static readonly Condition NotDownedEaterOfWorlds = new Condition("Conditions.NotDownedEaterOfWorlds", () => !NPC.downedBoss2 && !WorldGen.crimson);

		// Token: 0x0400015D RID: 349
		public static readonly Condition NotDownedBrainOfCthulhu = new Condition("Conditions.NotDownedBrainOfCthulhu", () => !NPC.downedBoss2 && WorldGen.crimson);

		// Token: 0x0400015E RID: 350
		public static readonly Condition NotDownedQueenBee = new Condition("Conditions.NotDownedQueenBee", () => !NPC.downedQueenBee);

		// Token: 0x0400015F RID: 351
		public static readonly Condition NotDownedSkeletron = new Condition("Conditions.NotDownedSkeletron", () => !NPC.downedBoss3);

		// Token: 0x04000160 RID: 352
		public static readonly Condition NotDownedDeerclops = new Condition("Conditions.NotDownedDeerclops", () => !NPC.downedDeerclops);

		// Token: 0x04000161 RID: 353
		public static readonly Condition NotDownedQueenSlime = new Condition("Conditions.NotDownedQueenSlime", () => !NPC.downedQueenSlime);

		// Token: 0x04000162 RID: 354
		public static readonly Condition NotDownedMechBossAny = new Condition("Conditions.NotDownedMechBossAny", () => !NPC.downedMechBossAny);

		// Token: 0x04000163 RID: 355
		public static readonly Condition NotDownedTwins = new Condition("Conditions.NotDownedTwins", () => !NPC.downedMechBoss2);

		// Token: 0x04000164 RID: 356
		public static readonly Condition NotDownedDestroyer = new Condition("Conditions.NotDownedDestroyer", () => !NPC.downedMechBoss1);

		// Token: 0x04000165 RID: 357
		public static readonly Condition NotDownedSkeletronPrime = new Condition("Conditions.NotDownedSkeletronPrime", () => !NPC.downedMechBoss3);

		// Token: 0x04000166 RID: 358
		public static readonly Condition NotDownedPlantera = new Condition("Conditions.NotDownedPlantera", () => !NPC.downedPlantBoss);

		// Token: 0x04000167 RID: 359
		public static readonly Condition NotDownedEmpressOfLight = new Condition("Conditions.NotDownedEmpressOfLight", () => !NPC.downedEmpressOfLight);

		// Token: 0x04000168 RID: 360
		public static readonly Condition NotDownedDukeFishron = new Condition("Conditions.NotDownedDukeFishron", () => !NPC.downedFishron);

		// Token: 0x04000169 RID: 361
		public static readonly Condition NotDownedGolem = new Condition("Conditions.NotDownedGolem", () => !NPC.downedGolemBoss);

		// Token: 0x0400016A RID: 362
		public static readonly Condition NotDownedMourningWood = new Condition("Conditions.NotDownedMourningWood", () => !NPC.downedHalloweenTree);

		// Token: 0x0400016B RID: 363
		public static readonly Condition NotDownedPumpking = new Condition("Conditions.NotDownedPumpking", () => !NPC.downedHalloweenKing);

		// Token: 0x0400016C RID: 364
		public static readonly Condition NotDownedEverscream = new Condition("Conditions.NotDownedEverscream", () => !NPC.downedChristmasTree);

		// Token: 0x0400016D RID: 365
		public static readonly Condition NotDownedSantaNK1 = new Condition("Conditions.NotDownedSantaNK1", () => !NPC.downedChristmasSantank);

		// Token: 0x0400016E RID: 366
		public static readonly Condition NotDownedIceQueen = new Condition("Conditions.NotDownedIceQueen", () => !NPC.downedChristmasIceQueen);

		// Token: 0x0400016F RID: 367
		public static readonly Condition NotDownedCultist = new Condition("Conditions.NotDownedLunaticCultist", () => !NPC.downedAncientCultist);

		// Token: 0x04000170 RID: 368
		public static readonly Condition NotDownedMoonLord = new Condition("Conditions.NotDownedMoonLord", () => !NPC.downedMoonlord);

		// Token: 0x04000171 RID: 369
		public static readonly Condition NotDownedClown = new Condition("Conditions.NotDownedClown", () => !NPC.downedClown);

		// Token: 0x04000172 RID: 370
		public static readonly Condition NotDownedGoblinArmy = new Condition("Conditions.NotDownedGoblinArmy", () => !NPC.downedGoblins);

		// Token: 0x04000173 RID: 371
		public static readonly Condition NotDownedPirates = new Condition("Conditions.NotDownedPirates", () => !NPC.downedPirates);

		// Token: 0x04000174 RID: 372
		public static readonly Condition NotDownedMartians = new Condition("Conditions.NotDownedMartians", () => !NPC.downedMartians);

		// Token: 0x04000175 RID: 373
		public static readonly Condition NotDownedFrostLegin = new Condition("Conditions.NotDownedFrostLegion", () => !NPC.downedFrost);

		// Token: 0x04000176 RID: 374
		public static readonly Condition NotDownedSolarPillar = new Condition("Conditions.NotDownedSolarPillar", () => !NPC.downedTowerSolar);

		// Token: 0x04000177 RID: 375
		public static readonly Condition NotDownedVortexPillar = new Condition("Conditions.NotDownedVortexPillar", () => !NPC.downedTowerVortex);

		// Token: 0x04000178 RID: 376
		public static readonly Condition NotDownedNebulaPillar = new Condition("Conditions.NotDownedNebulaPillar", () => !NPC.downedTowerNebula);

		// Token: 0x04000179 RID: 377
		public static readonly Condition NotDownedStardustPillar = new Condition("Conditions.NotDownedStardustPillar", () => !NPC.downedTowerStardust);

		// Token: 0x0400017A RID: 378
		public static readonly Condition NotDownedOldOnesArmyAny = new Condition("Conditions.NotDownedOldOnesArmyAny", () => !DD2Event.DownedInvasionAnyDifficulty);

		// Token: 0x0400017B RID: 379
		public static readonly Condition NotDownedOldOnesArmyT1 = new Condition("Conditions.NotDownedOldOnesArmyT1", () => !DD2Event.DownedInvasionT1);

		// Token: 0x0400017C RID: 380
		public static readonly Condition NotDownedOldOnesArmyT2 = new Condition("Conditions.NotDownedOldOnesArmyT2", () => !DD2Event.DownedInvasionT2);

		// Token: 0x0400017D RID: 381
		public static readonly Condition NotDownedOldOnesArmyT3 = new Condition("Conditions.NotDownedOldOnesArmyT3", () => !DD2Event.DownedInvasionT3);

		// Token: 0x0400017E RID: 382
		public static readonly Condition BloodMoonOrHardmode = new Condition("Conditions.BloodMoonOrHardmode", () => Main.bloodMoon || Main.hardMode);

		// Token: 0x0400017F RID: 383
		public static readonly Condition NightOrEclipse = new Condition("Conditions.NightOrEclipse", () => !Main.dayTime || Main.eclipse);

		// Token: 0x04000180 RID: 384
		public static readonly Condition Multiplayer = new Condition("Conditions.InMultiplayer", () => Main.netMode != 0);

		// Token: 0x04000181 RID: 385
		public static readonly Condition HappyEnough = new Condition("Conditions.HappyEnough", () => Main.LocalPlayer.currentShoppingSettings.PriceAdjustment <= 0.9);

		// Token: 0x04000182 RID: 386
		public static readonly Condition HappyEnoughToSellPylons = new Condition("Conditions.HappyEnoughForPylons", () => Main.remixWorld || Condition.HappyEnough.IsMet());

		// Token: 0x04000183 RID: 387
		public static readonly Condition AnotherTownNPCNearby = new Condition("Conditions.AnotherTownNPCNearby", () => TeleportPylonsSystem.DoesPositionHaveEnoughNPCs(2, Main.LocalPlayer.Center.ToTileCoordinates16()));

		// Token: 0x04000184 RID: 388
		public static readonly Condition IsNpcShimmered = new Condition("Conditions.IsNpcShimmered", delegate()
		{
			NPC talkNPC = Main.LocalPlayer.TalkNPC;
			return talkNPC != null && talkNPC.IsShimmerVariant;
		});

		// Token: 0x04000185 RID: 389
		public static readonly Condition MoonPhaseFull = new Condition("Conditions.FullMoon", () => Main.GetMoonPhase() == MoonPhase.Full);

		// Token: 0x04000186 RID: 390
		public static readonly Condition MoonPhaseWaningGibbous = new Condition("Conditions.WaningGibbousMoon", () => Main.GetMoonPhase() == MoonPhase.ThreeQuartersAtLeft);

		// Token: 0x04000187 RID: 391
		public static readonly Condition MoonPhaseThirdQuarter = new Condition("Conditions.ThirdQuarterMoon", () => Main.GetMoonPhase() == MoonPhase.HalfAtLeft);

		// Token: 0x04000188 RID: 392
		public static readonly Condition MoonPhaseWaningCrescent = new Condition("Conditions.WaningCrescentMoon", () => Main.GetMoonPhase() == MoonPhase.QuarterAtLeft);

		// Token: 0x04000189 RID: 393
		public static readonly Condition MoonPhaseNew = new Condition("Conditions.NewMoon", () => Main.GetMoonPhase() == MoonPhase.Empty);

		// Token: 0x0400018A RID: 394
		public static readonly Condition MoonPhaseWaxingCrescent = new Condition("Conditions.WaxingCrescentMoon", () => Main.GetMoonPhase() == MoonPhase.QuarterAtRight);

		// Token: 0x0400018B RID: 395
		public static readonly Condition MoonPhaseFirstQuarter = new Condition("Conditions.FirstQuarterMoon", () => Main.GetMoonPhase() == MoonPhase.HalfAtRight);

		// Token: 0x0400018C RID: 396
		public static readonly Condition MoonPhaseWaxingGibbous = new Condition("Conditions.WaxingGibbousMoon", () => Main.GetMoonPhase() == MoonPhase.ThreeQuartersAtRight);

		// Token: 0x0400018D RID: 397
		public static readonly Condition MoonPhasesQuarter0 = new Condition("Conditions.MoonPhasesQuarter0", () => Main.moonPhase / 2 == 0);

		// Token: 0x0400018E RID: 398
		public static readonly Condition MoonPhasesQuarter1 = new Condition("Conditions.MoonPhasesQuarter1", () => Main.moonPhase / 2 == 1);

		// Token: 0x0400018F RID: 399
		public static readonly Condition MoonPhasesQuarter2 = new Condition("Conditions.MoonPhasesQuarter2", () => Main.moonPhase / 2 == 2);

		// Token: 0x04000190 RID: 400
		public static readonly Condition MoonPhasesQuarter3 = new Condition("Conditions.MoonPhasesQuarter3", () => Main.moonPhase / 2 == 3);

		// Token: 0x04000191 RID: 401
		public static readonly Condition MoonPhasesHalf0 = new Condition("Conditions.MoonPhasesHalf0", () => Main.moonPhase / 4 == 0);

		// Token: 0x04000192 RID: 402
		public static readonly Condition MoonPhasesHalf1 = new Condition("Conditions.MoonPhasesHalf1", () => Main.moonPhase / 4 == 1);

		// Token: 0x04000193 RID: 403
		public static readonly Condition MoonPhasesEven = new Condition("Conditions.MoonPhasesEven", () => Main.moonPhase % 2 == 0);

		// Token: 0x04000194 RID: 404
		public static readonly Condition MoonPhasesOdd = new Condition("Conditions.MoonPhasesOdd", () => Main.moonPhase % 2 == 1);

		// Token: 0x04000195 RID: 405
		public static readonly Condition MoonPhasesNearNew = new Condition("Conditions.MoonPhasesNearNew", () => Main.moonPhase >= 3 && Main.moonPhase <= 5);

		// Token: 0x04000196 RID: 406
		public static readonly Condition MoonPhasesEvenQuarters = new Condition("Conditions.MoonPhasesEvenQuarters", () => Main.moonPhase / 2 % 2 == 0);

		// Token: 0x04000197 RID: 407
		public static readonly Condition MoonPhasesOddQuarters = new Condition("Conditions.MoonPhasesOddQuarters", () => Main.moonPhase / 2 % 2 == 1);

		// Token: 0x04000198 RID: 408
		public static readonly Condition MoonPhases04 = new Condition("Conditions.MoonPhases04", () => Main.moonPhase % 4 == 0);

		// Token: 0x04000199 RID: 409
		public static readonly Condition MoonPhases15 = new Condition("Conditions.MoonPhases15", () => Main.moonPhase % 4 == 1);

		// Token: 0x0400019A RID: 410
		public static readonly Condition MoonPhases26 = new Condition("Conditions.MoonPhases26", () => Main.moonPhase % 4 == 2);

		// Token: 0x0400019B RID: 411
		public static readonly Condition MoonPhases37 = new Condition("Conditions.MoonPhases37", () => Main.moonPhase % 4 == 3);
	}
}
