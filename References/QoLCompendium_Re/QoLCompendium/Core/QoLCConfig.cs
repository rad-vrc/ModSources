using System;
using System.Collections.Generic;
using System.ComponentModel;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.Config;

namespace QoLCompendium.Core
{
	// Token: 0x02000209 RID: 521
	public class QoLCConfig : ModConfig
	{
		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000BFC RID: 3068 RVA: 0x0000404D File Offset: 0x0000224D
		public override ConfigScope Mode
		{
			get
			{
				return ConfigScope.ServerSide;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x00059EFF File Offset: 0x000580FF
		// (set) Token: 0x06000BFE RID: 3070 RVA: 0x00059F07 File Offset: 0x00058107
		[Header("$Mods.QoLCompendium.QoLCConfig.Headers.Endless")]
		[DefaultValue(true)]
		public bool EndlessBuffs { get; set; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000BFF RID: 3071 RVA: 0x00059F10 File Offset: 0x00058110
		// (set) Token: 0x06000C00 RID: 3072 RVA: 0x00059F18 File Offset: 0x00058118
		[DefaultValue(true)]
		public bool EndlessHealing { get; set; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000C01 RID: 3073 RVA: 0x00059F21 File Offset: 0x00058121
		// (set) Token: 0x06000C02 RID: 3074 RVA: 0x00059F29 File Offset: 0x00058129
		[DefaultValue(true)]
		public bool EndlessAmmo { get; set; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000C03 RID: 3075 RVA: 0x00059F32 File Offset: 0x00058132
		// (set) Token: 0x06000C04 RID: 3076 RVA: 0x00059F3A File Offset: 0x0005813A
		[DefaultValue(true)]
		public bool EndlessBait { get; set; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x00059F43 File Offset: 0x00058143
		// (set) Token: 0x06000C06 RID: 3078 RVA: 0x00059F4B File Offset: 0x0005814B
		[DefaultValue(true)]
		[ReloadRequired]
		public bool EndlessBossSummons { get; set; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x00059F54 File Offset: 0x00058154
		// (set) Token: 0x06000C08 RID: 3080 RVA: 0x00059F5C File Offset: 0x0005815C
		[DefaultValue(true)]
		public bool EndlessConsumables { get; set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x00059F65 File Offset: 0x00058165
		// (set) Token: 0x06000C0A RID: 3082 RVA: 0x00059F6D File Offset: 0x0005816D
		[DefaultValue(true)]
		public bool EndlessWeapons { get; set; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000C0B RID: 3083 RVA: 0x00059F76 File Offset: 0x00058176
		// (set) Token: 0x06000C0C RID: 3084 RVA: 0x00059F7E File Offset: 0x0005817E
		[DefaultValue(30)]
		[Range(1, 99999)]
		public int EndlessBuffAmount { get; set; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000C0D RID: 3085 RVA: 0x00059F87 File Offset: 0x00058187
		// (set) Token: 0x06000C0E RID: 3086 RVA: 0x00059F8F File Offset: 0x0005818F
		[DefaultValue(1)]
		[Range(1, 99999)]
		public int EndlessStationAmount { get; set; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000C0F RID: 3087 RVA: 0x00059F98 File Offset: 0x00058198
		// (set) Token: 0x06000C10 RID: 3088 RVA: 0x00059FA0 File Offset: 0x000581A0
		[DefaultValue(30)]
		[Range(1, 99999)]
		public int EndlessHealingAmount { get; set; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000C11 RID: 3089 RVA: 0x00059FA9 File Offset: 0x000581A9
		// (set) Token: 0x06000C12 RID: 3090 RVA: 0x00059FB1 File Offset: 0x000581B1
		[DefaultValue(999)]
		[Range(1, 99999)]
		public int EndlessItemAmount { get; set; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000C13 RID: 3091 RVA: 0x00059FBA File Offset: 0x000581BA
		// (set) Token: 0x06000C14 RID: 3092 RVA: 0x00059FC2 File Offset: 0x000581C2
		[DefaultValue(999)]
		[Range(1, 99999)]
		public int EndlessWeaponAmount { get; set; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000C15 RID: 3093 RVA: 0x00059FCB File Offset: 0x000581CB
		// (set) Token: 0x06000C16 RID: 3094 RVA: 0x00059FD3 File Offset: 0x000581D3
		[DefaultValue(999)]
		[Range(1, 99999)]
		public int EndlessAmmoAmount { get; set; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000C17 RID: 3095 RVA: 0x00059FDC File Offset: 0x000581DC
		// (set) Token: 0x06000C18 RID: 3096 RVA: 0x00059FE4 File Offset: 0x000581E4
		[DefaultValue(999)]
		[Range(1, 99999)]
		public int EndlessBaitAmount { get; set; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000C19 RID: 3097 RVA: 0x00059FED File Offset: 0x000581ED
		// (set) Token: 0x06000C1A RID: 3098 RVA: 0x00059FF5 File Offset: 0x000581F5
		[DefaultValue(false)]
		public bool ActiveBuffsHaveEnchantedEffects { get; set; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x00059FFE File Offset: 0x000581FE
		// (set) Token: 0x06000C1C RID: 3100 RVA: 0x0005A006 File Offset: 0x00058206
		[Header("$Mods.QoLCompendium.QoLCConfig.Headers.Items2")]
		[DefaultValue(9999)]
		[Range(1, 99999)]
		public int IncreaseMaxStack { get; set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000C1D RID: 3101 RVA: 0x0005A00F File Offset: 0x0005820F
		// (set) Token: 0x06000C1E RID: 3102 RVA: 0x0005A017 File Offset: 0x00058217
		[DefaultValue(true)]
		public bool UtilityAccessoriesWorkInBanks { get; set; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000C1F RID: 3103 RVA: 0x0005A020 File Offset: 0x00058220
		// (set) Token: 0x06000C20 RID: 3104 RVA: 0x0005A028 File Offset: 0x00058228
		[DefaultValue(false)]
		public bool FountainsWorkFromInventories { get; set; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000C21 RID: 3105 RVA: 0x0005A031 File Offset: 0x00058231
		// (set) Token: 0x06000C22 RID: 3106 RVA: 0x0005A039 File Offset: 0x00058239
		[DefaultValue(true)]
		public bool StackableQuestItems { get; set; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000C23 RID: 3107 RVA: 0x0005A042 File Offset: 0x00058242
		// (set) Token: 0x06000C24 RID: 3108 RVA: 0x0005A04A File Offset: 0x0005824A
		[DefaultValue(true)]
		[ReloadRequired]
		public bool NonConsumableKeys { get; set; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000C25 RID: 3109 RVA: 0x0005A053 File Offset: 0x00058253
		// (set) Token: 0x06000C26 RID: 3110 RVA: 0x0005A05B File Offset: 0x0005825B
		[DefaultValue(true)]
		[ReloadRequired]
		public bool BossItemTransmutation { get; set; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000C27 RID: 3111 RVA: 0x0005A064 File Offset: 0x00058264
		// (set) Token: 0x06000C28 RID: 3112 RVA: 0x0005A06C File Offset: 0x0005826C
		[DefaultValue(true)]
		[ReloadRequired]
		public bool ItemConversions { get; set; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000C29 RID: 3113 RVA: 0x0005A075 File Offset: 0x00058275
		// (set) Token: 0x06000C2A RID: 3114 RVA: 0x0005A07D File Offset: 0x0005827D
		[DefaultValue(true)]
		public bool BossBagRecipes { get; set; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000C2B RID: 3115 RVA: 0x0005A086 File Offset: 0x00058286
		// (set) Token: 0x06000C2C RID: 3116 RVA: 0x0005A08E File Offset: 0x0005828E
		[DefaultValue(true)]
		public bool CrateRecipes { get; set; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000C2D RID: 3117 RVA: 0x0005A097 File Offset: 0x00058297
		// (set) Token: 0x06000C2E RID: 3118 RVA: 0x0005A09F File Offset: 0x0005829F
		[DefaultValue(true)]
		public bool BannerRecipes { get; set; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000C2F RID: 3119 RVA: 0x0005A0A8 File Offset: 0x000582A8
		// (set) Token: 0x06000C30 RID: 3120 RVA: 0x0005A0B0 File Offset: 0x000582B0
		[DefaultValue(true)]
		public bool NoDeveloperSetsFromBossBags { get; set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x0005A0B9 File Offset: 0x000582B9
		// (set) Token: 0x06000C32 RID: 3122 RVA: 0x0005A0C1 File Offset: 0x000582C1
		[Slider]
		[DefaultValue(5)]
		[Range(1, 25)]
		public int EnemiesDropMoreCoins { get; set; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000C33 RID: 3123 RVA: 0x0005A0CA File Offset: 0x000582CA
		// (set) Token: 0x06000C34 RID: 3124 RVA: 0x0005A0D2 File Offset: 0x000582D2
		[DefaultValue(true)]
		public bool EncumberingStoneAllowsCoins { get; set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000C35 RID: 3125 RVA: 0x0005A0DB File Offset: 0x000582DB
		// (set) Token: 0x06000C36 RID: 3126 RVA: 0x0005A0E3 File Offset: 0x000582E3
		[DefaultValue(true)]
		public bool AutoMoneyQuickStack { get; set; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000C37 RID: 3127 RVA: 0x0005A0EC File Offset: 0x000582EC
		// (set) Token: 0x06000C38 RID: 3128 RVA: 0x0005A0F4 File Offset: 0x000582F4
		[DefaultValue(true)]
		public bool GoldenCarpDelight { get; set; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x0005A0FD File Offset: 0x000582FD
		// (set) Token: 0x06000C3A RID: 3130 RVA: 0x0005A105 File Offset: 0x00058305
		[DefaultValue(true)]
		public bool EasierUniversalPylon { get; set; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000C3B RID: 3131 RVA: 0x0005A10E File Offset: 0x0005830E
		// (set) Token: 0x06000C3C RID: 3132 RVA: 0x0005A116 File Offset: 0x00058316
		[DefaultValue(true)]
		[ReloadRequired]
		public bool AutoReuseUpgrades { get; set; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000C3D RID: 3133 RVA: 0x0005A11F File Offset: 0x0005831F
		// (set) Token: 0x06000C3E RID: 3134 RVA: 0x0005A127 File Offset: 0x00058327
		[DefaultValue(false)]
		public bool GoodPrefixesHaveEnchantedEffects { get; set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000C3F RID: 3135 RVA: 0x0005A130 File Offset: 0x00058330
		// (set) Token: 0x06000C40 RID: 3136 RVA: 0x0005A138 File Offset: 0x00058338
		[DefaultValue(false)]
		[ReloadRequired]
		public bool FullyDisableRecipes { get; set; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000C41 RID: 3137 RVA: 0x0005A141 File Offset: 0x00058341
		// (set) Token: 0x06000C42 RID: 3138 RVA: 0x0005A149 File Offset: 0x00058349
		[Header("$Mods.QoLCompendium.QoLCConfig.Headers.NPCs")]
		[DefaultValue(true)]
		[ReloadRequired]
		public bool BlackMarketDealerCanSpawn { get; set; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000C43 RID: 3139 RVA: 0x0005A152 File Offset: 0x00058352
		// (set) Token: 0x06000C44 RID: 3140 RVA: 0x0005A15A File Offset: 0x0005835A
		[DefaultValue(true)]
		[ReloadRequired]
		public bool EtherealCollectorCanSpawn { get; set; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000C45 RID: 3141 RVA: 0x0005A163 File Offset: 0x00058363
		// (set) Token: 0x06000C46 RID: 3142 RVA: 0x0005A16B File Offset: 0x0005836B
		[DefaultValue(false)]
		public bool RemoveBiomeShopRequirements { get; set; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000C47 RID: 3143 RVA: 0x0005A174 File Offset: 0x00058374
		// (set) Token: 0x06000C48 RID: 3144 RVA: 0x0005A17C File Offset: 0x0005837C
		[DefaultValue(true)]
		public bool TownNPCsDontDie { get; set; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000C49 RID: 3145 RVA: 0x0005A185 File Offset: 0x00058385
		// (set) Token: 0x06000C4A RID: 3146 RVA: 0x0005A18D File Offset: 0x0005838D
		[Slider]
		[Range(1, 10)]
		[DefaultValue(1)]
		public int FastTownNPCSpawns { get; set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000C4B RID: 3147 RVA: 0x0005A196 File Offset: 0x00058396
		// (set) Token: 0x06000C4C RID: 3148 RVA: 0x0005A19E File Offset: 0x0005839E
		[DefaultValue(true)]
		public bool TownNPCSpawnImprovements { get; set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000C4D RID: 3149 RVA: 0x0005A1A7 File Offset: 0x000583A7
		// (set) Token: 0x06000C4E RID: 3150 RVA: 0x0005A1AF File Offset: 0x000583AF
		[DefaultValue(true)]
		public bool NoTownSlimes { get; set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000C4F RID: 3151 RVA: 0x0005A1B8 File Offset: 0x000583B8
		// (set) Token: 0x06000C50 RID: 3152 RVA: 0x0005A1C0 File Offset: 0x000583C0
		[DefaultValue(true)]
		public bool TownNPCsLiveInEvil { get; set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000C51 RID: 3153 RVA: 0x0005A1C9 File Offset: 0x000583C9
		// (set) Token: 0x06000C52 RID: 3154 RVA: 0x0005A1D1 File Offset: 0x000583D1
		[DefaultValue(true)]
		public bool DisableHappiness { get; set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000C53 RID: 3155 RVA: 0x0005A1DA File Offset: 0x000583DA
		// (set) Token: 0x06000C54 RID: 3156 RVA: 0x0005A1E2 File Offset: 0x000583E2
		[DefaultValue(false)]
		public bool OverridePylonSales { get; set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000C55 RID: 3157 RVA: 0x0005A1EB File Offset: 0x000583EB
		// (set) Token: 0x06000C56 RID: 3158 RVA: 0x0005A1F3 File Offset: 0x000583F3
		[Slider]
		[DefaultValue(0.75f)]
		[Increment(0.01f)]
		[Range(0, 1)]
		public float HappinessPriceChange { get; set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000C57 RID: 3159 RVA: 0x0005A1FC File Offset: 0x000583FC
		// (set) Token: 0x06000C58 RID: 3160 RVA: 0x0005A204 File Offset: 0x00058404
		[Range(0, 100)]
		[DefaultValue(25)]
		public int ReforgePriceChange { get; set; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000C59 RID: 3161 RVA: 0x0005A20D File Offset: 0x0005840D
		// (set) Token: 0x06000C5A RID: 3162 RVA: 0x0005A215 File Offset: 0x00058415
		[DefaultValue(true)]
		public bool AnglerQuestInstantReset { get; set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000C5B RID: 3163 RVA: 0x0005A21E File Offset: 0x0005841E
		// (set) Token: 0x06000C5C RID: 3164 RVA: 0x0005A226 File Offset: 0x00058426
		[Slider]
		[DefaultValue(10)]
		[Range(1, 100)]
		[Increment(5)]
		public int LunarPillarShieldHeath { get; set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000C5D RID: 3165 RVA: 0x0005A22F File Offset: 0x0005842F
		// (set) Token: 0x06000C5E RID: 3166 RVA: 0x0005A237 File Offset: 0x00058437
		[DefaultValue(true)]
		public bool LunarPillarsDropMoreFragments { get; set; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000C5F RID: 3167 RVA: 0x0005A240 File Offset: 0x00058440
		// (set) Token: 0x06000C60 RID: 3168 RVA: 0x0005A248 File Offset: 0x00058448
		[DefaultValue(true)]
		public bool LunarEnemiesDropFragments { get; set; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000C61 RID: 3169 RVA: 0x0005A251 File Offset: 0x00058451
		// (set) Token: 0x06000C62 RID: 3170 RVA: 0x0005A259 File Offset: 0x00058459
		[DefaultValue(true)]
		public bool OneKillForBestiaryEntries { get; set; }

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000C63 RID: 3171 RVA: 0x0005A262 File Offset: 0x00058462
		// (set) Token: 0x06000C64 RID: 3172 RVA: 0x0005A26A File Offset: 0x0005846A
		[DefaultValue(true)]
		public bool LavaSlimesDontDropLava { get; set; }

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000C65 RID: 3173 RVA: 0x0005A273 File Offset: 0x00058473
		// (set) Token: 0x06000C66 RID: 3174 RVA: 0x0005A27B File Offset: 0x0005847B
		[DefaultValue(true)]
		public bool ExtraDefenderMedalDrops { get; set; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000C67 RID: 3175 RVA: 0x0005A284 File Offset: 0x00058484
		// (set) Token: 0x06000C68 RID: 3176 RVA: 0x0005A28C File Offset: 0x0005848C
		[DefaultValue(true)]
		public bool RelicsInExpert { get; set; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000C69 RID: 3177 RVA: 0x0005A295 File Offset: 0x00058495
		// (set) Token: 0x06000C6A RID: 3178 RVA: 0x0005A29D File Offset: 0x0005849D
		[DefaultValue(true)]
		public bool NoSpawnsDuringBosses { get; set; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000C6B RID: 3179 RVA: 0x0005A2A6 File Offset: 0x000584A6
		// (set) Token: 0x06000C6C RID: 3180 RVA: 0x0005A2AE File Offset: 0x000584AE
		[DefaultValue(true)]
		public bool NoNaturalBossSpawns { get; set; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000C6D RID: 3181 RVA: 0x0005A2B7 File Offset: 0x000584B7
		// (set) Token: 0x06000C6E RID: 3182 RVA: 0x0005A2BF File Offset: 0x000584BF
		[Header("$Mods.QoLCompendium.QoLCConfig.Headers.Projectiles")]
		[Range(0, 25)]
		[DefaultValue(3)]
		public int ExtraLures { get; set; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000C6F RID: 3183 RVA: 0x0005A2C8 File Offset: 0x000584C8
		// (set) Token: 0x06000C70 RID: 3184 RVA: 0x0005A2D0 File Offset: 0x000584D0
		[DefaultValue(true)]
		public bool MobileStoragesFollowThePlayer { get; set; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000C71 RID: 3185 RVA: 0x0005A2D9 File Offset: 0x000584D9
		// (set) Token: 0x06000C72 RID: 3186 RVA: 0x0005A2E1 File Offset: 0x000584E1
		[DefaultValue(true)]
		public bool NoFallingSandDamage { get; set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000C73 RID: 3187 RVA: 0x0005A2EA File Offset: 0x000584EA
		// (set) Token: 0x06000C74 RID: 3188 RVA: 0x0005A2F2 File Offset: 0x000584F2
		[DefaultValue(true)]
		public bool NoLittering { get; set; }

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x0005A2FB File Offset: 0x000584FB
		// (set) Token: 0x06000C76 RID: 3190 RVA: 0x0005A303 File Offset: 0x00058503
		[DefaultValue(true)]
		public bool NoLarvaBreak { get; set; }

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000C77 RID: 3191 RVA: 0x0005A30C File Offset: 0x0005850C
		// (set) Token: 0x06000C78 RID: 3192 RVA: 0x0005A314 File Offset: 0x00058514
		[DefaultValue(true)]
		public bool PurificationPowderCleansesWalls { get; set; }

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000C79 RID: 3193 RVA: 0x0005A31D File Offset: 0x0005851D
		// (set) Token: 0x06000C7A RID: 3194 RVA: 0x0005A325 File Offset: 0x00058525
		[Header("$Mods.QoLCompendium.QoLCConfig.Headers.Player")]
		[Slider]
		[DefaultValue(0.2f)]
		[Range(0f, 1f)]
		[Increment(0.1f)]
		public float IncreasePlaceSpeed { get; set; }

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000C7B RID: 3195 RVA: 0x0005A32E File Offset: 0x0005852E
		// (set) Token: 0x06000C7C RID: 3196 RVA: 0x0005A336 File Offset: 0x00058536
		[Slider]
		[DefaultValue(4)]
		[Range(0, 60)]
		[Increment(2)]
		public int IncreasePlaceRange { get; set; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000C7D RID: 3197 RVA: 0x0005A33F File Offset: 0x0005853F
		// (set) Token: 0x06000C7E RID: 3198 RVA: 0x0005A347 File Offset: 0x00058547
		[DefaultValue(0.0)]
		[Range(0f, 1f)]
		[Slider]
		[Increment(0.125f)]
		public float IncreaseToolSpeed { get; set; }

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000C7F RID: 3199 RVA: 0x0005A350 File Offset: 0x00058550
		// (set) Token: 0x06000C80 RID: 3200 RVA: 0x0005A358 File Offset: 0x00058558
		[DefaultValue(true)]
		public bool FasterExtractinator { get; set; }

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000C81 RID: 3201 RVA: 0x0005A361 File Offset: 0x00058561
		// (set) Token: 0x06000C82 RID: 3202 RVA: 0x0005A369 File Offset: 0x00058569
		[DefaultValue(44)]
		[Range(0, 132)]
		[Slider]
		[Increment(22)]
		[ReloadRequired]
		public int ExtraBuffSlots { get; set; }

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000C83 RID: 3203 RVA: 0x0005A372 File Offset: 0x00058572
		// (set) Token: 0x06000C84 RID: 3204 RVA: 0x0005A37A File Offset: 0x0005857A
		[DefaultValue(true)]
		public bool KeepBuffsOnDeath { get; set; }

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000C85 RID: 3205 RVA: 0x0005A383 File Offset: 0x00058583
		// (set) Token: 0x06000C86 RID: 3206 RVA: 0x0005A38B File Offset: 0x0005858B
		[DefaultValue(false)]
		public bool KeepDebuffsOnDeath { get; set; }

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000C87 RID: 3207 RVA: 0x0005A394 File Offset: 0x00058594
		// (set) Token: 0x06000C88 RID: 3208 RVA: 0x0005A39C File Offset: 0x0005859C
		[DefaultValue(true)]
		[ReloadRequired]
		public bool InfiniteSliceOfCake { get; set; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000C89 RID: 3209 RVA: 0x0005A3A5 File Offset: 0x000585A5
		// (set) Token: 0x06000C8A RID: 3210 RVA: 0x0005A3AD File Offset: 0x000585AD
		[DefaultValue(false)]
		public bool HideBuffs { get; set; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000C8B RID: 3211 RVA: 0x0005A3B6 File Offset: 0x000585B6
		// (set) Token: 0x06000C8C RID: 3212 RVA: 0x0005A3BE File Offset: 0x000585BE
		[DefaultValue(true)]
		public bool RegrowthAutoReplant { get; set; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000C8D RID: 3213 RVA: 0x0005A3C7 File Offset: 0x000585C7
		// (set) Token: 0x06000C8E RID: 3214 RVA: 0x0005A3CF File Offset: 0x000585CF
		[DefaultValue(true)]
		public bool LifeformAnalyzerPointer { get; set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000C8F RID: 3215 RVA: 0x0005A3D8 File Offset: 0x000585D8
		// (set) Token: 0x06000C90 RID: 3216 RVA: 0x0005A3E0 File Offset: 0x000585E0
		[DefaultValue(true)]
		public bool NoExpertIceWaterChilled { get; set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000C91 RID: 3217 RVA: 0x0005A3E9 File Offset: 0x000585E9
		// (set) Token: 0x06000C92 RID: 3218 RVA: 0x0005A3F1 File Offset: 0x000585F1
		[DefaultValue(true)]
		public bool NoShimmerSink { get; set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000C93 RID: 3219 RVA: 0x0005A3FA File Offset: 0x000585FA
		// (set) Token: 0x06000C94 RID: 3220 RVA: 0x0005A402 File Offset: 0x00058602
		[Slider]
		[DefaultValue(1)]
		[Range(0, 5)]
		[Increment(1)]
		public int AutoTeams { get; set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000C95 RID: 3221 RVA: 0x0005A40B File Offset: 0x0005860B
		// (set) Token: 0x06000C96 RID: 3222 RVA: 0x0005A413 File Offset: 0x00058613
		[DefaultValue(true)]
		[ReloadRequired]
		public bool AllHairStylesAvailable { get; set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000C97 RID: 3223 RVA: 0x0005A41C File Offset: 0x0005861C
		// (set) Token: 0x06000C98 RID: 3224 RVA: 0x0005A424 File Offset: 0x00058624
		[DefaultValue(true)]
		public bool NoTombstoneDrops { get; set; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000C99 RID: 3225 RVA: 0x0005A42D File Offset: 0x0005862D
		// (set) Token: 0x06000C9A RID: 3226 RVA: 0x0005A435 File Offset: 0x00058635
		[DefaultValue(true)]
		public bool HellstoneSpelunker { get; set; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000C9B RID: 3227 RVA: 0x0005A43E File Offset: 0x0005863E
		// (set) Token: 0x06000C9C RID: 3228 RVA: 0x0005A446 File Offset: 0x00058646
		[DefaultValue(true)]
		public bool DangersenseIgnoresThinIce { get; set; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000C9D RID: 3229 RVA: 0x0005A44F File Offset: 0x0005864F
		// (set) Token: 0x06000C9E RID: 3230 RVA: 0x0005A457 File Offset: 0x00058657
		[DefaultValue(true)]
		public bool DangersenseHighlightsSiltAndSlush { get; set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000C9F RID: 3231 RVA: 0x0005A460 File Offset: 0x00058660
		// (set) Token: 0x06000CA0 RID: 3232 RVA: 0x0005A468 File Offset: 0x00058668
		[DefaultValue(true)]
		public bool AutoFishing { get; set; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x0005A471 File Offset: 0x00058671
		// (set) Token: 0x06000CA2 RID: 3234 RVA: 0x0005A479 File Offset: 0x00058679
		[DefaultValue(false)]
		public bool NoPylonTeleportRestrictions { get; set; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000CA3 RID: 3235 RVA: 0x0005A482 File Offset: 0x00058682
		// (set) Token: 0x06000CA4 RID: 3236 RVA: 0x0005A48A File Offset: 0x0005868A
		[DefaultValue(true)]
		public bool InstantRespawn { get; set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000CA5 RID: 3237 RVA: 0x0005A493 File Offset: 0x00058693
		// (set) Token: 0x06000CA6 RID: 3238 RVA: 0x0005A49B File Offset: 0x0005869B
		[DefaultValue(true)]
		public bool FullHealthRespawn { get; set; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x0005A4A4 File Offset: 0x000586A4
		// (set) Token: 0x06000CA8 RID: 3240 RVA: 0x0005A4AC File Offset: 0x000586AC
		[DefaultValue(false)]
		public bool WingSlot { get; set; }

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x0005A4B5 File Offset: 0x000586B5
		// (set) Token: 0x06000CAA RID: 3242 RVA: 0x0005A4BD File Offset: 0x000586BD
		[DefaultValue(false)]
		public bool BootSlot { get; set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x0005A4C6 File Offset: 0x000586C6
		// (set) Token: 0x06000CAC RID: 3244 RVA: 0x0005A4CE File Offset: 0x000586CE
		[DefaultValue(false)]
		public bool ShieldSlot { get; set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000CAD RID: 3245 RVA: 0x0005A4D7 File Offset: 0x000586D7
		// (set) Token: 0x06000CAE RID: 3246 RVA: 0x0005A4DF File Offset: 0x000586DF
		[DefaultValue(false)]
		public bool ExpertSlot { get; set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000CAF RID: 3247 RVA: 0x0005A4E8 File Offset: 0x000586E8
		// (set) Token: 0x06000CB0 RID: 3248 RVA: 0x0005A4F0 File Offset: 0x000586F0
		[DefaultValue(false)]
		public bool MapTeleporting { get; set; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x0005A4F9 File Offset: 0x000586F9
		// (set) Token: 0x06000CB2 RID: 3250 RVA: 0x0005A501 File Offset: 0x00058701
		[Header("$Mods.QoLCompendium.QoLCConfig.Headers.World")]
		[DefaultValue(true)]
		public bool DisableEvilBiomeSpread { get; set; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x0005A50A File Offset: 0x0005870A
		// (set) Token: 0x06000CB4 RID: 3252 RVA: 0x0005A512 File Offset: 0x00058712
		[DefaultValue(true)]
		public bool FountainsCauseBiomes { get; set; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x0005A51B File Offset: 0x0005871B
		// (set) Token: 0x06000CB6 RID: 3254 RVA: 0x0005A523 File Offset: 0x00058723
		[DefaultValue(true)]
		public bool FastTreeGrowth { get; set; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x0005A52C File Offset: 0x0005872C
		// (set) Token: 0x06000CB8 RID: 3256 RVA: 0x0005A534 File Offset: 0x00058734
		[DefaultValue(true)]
		public bool FastHerbGrowth { get; set; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x0005A53D File Offset: 0x0005873D
		// (set) Token: 0x06000CBA RID: 3258 RVA: 0x0005A545 File Offset: 0x00058745
		[DefaultValue(true)]
		public bool TreesDropMoreWhenShook { get; set; }

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000CBB RID: 3259 RVA: 0x0005A54E File Offset: 0x0005874E
		// (set) Token: 0x06000CBC RID: 3260 RVA: 0x0005A556 File Offset: 0x00058756
		[DefaultValue(true)]
		public bool BreakAllDungeonBricks { get; set; }

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x0005A55F File Offset: 0x0005875F
		// (set) Token: 0x06000CBE RID: 3262 RVA: 0x0005A567 File Offset: 0x00058767
		[DefaultValue(2)]
		[Range(1, 500)]
		public int MoreFallenStars { get; set; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000CBF RID: 3263 RVA: 0x0005A570 File Offset: 0x00058770
		// (set) Token: 0x06000CC0 RID: 3264 RVA: 0x0005A578 File Offset: 0x00058778
		[DefaultValue(true)]
		public bool NoMeteorSpawns { get; set; }

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x0005A581 File Offset: 0x00058781
		// (set) Token: 0x06000CC2 RID: 3266 RVA: 0x0005A589 File Offset: 0x00058789
		[DefaultValue(true)]
		public bool ChristmasActive { get; set; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000CC3 RID: 3267 RVA: 0x0005A592 File Offset: 0x00058792
		// (set) Token: 0x06000CC4 RID: 3268 RVA: 0x0005A59A File Offset: 0x0005879A
		[DefaultValue(true)]
		public bool HalloweenActive { get; set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000CC5 RID: 3269 RVA: 0x0005A5A3 File Offset: 0x000587A3
		// (set) Token: 0x06000CC6 RID: 3270 RVA: 0x0005A5AB File Offset: 0x000587AB
		[DefaultValue(false)]
		public bool DisableCredits { get; set; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x0005A5B4 File Offset: 0x000587B4
		// (set) Token: 0x06000CC8 RID: 3272 RVA: 0x0005A5BC File Offset: 0x000587BC
		[DefaultValue(true)]
		public bool VeinMiner { get; set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000CC9 RID: 3273 RVA: 0x0005A5C5 File Offset: 0x000587C5
		// (set) Token: 0x06000CCA RID: 3274 RVA: 0x0005A5CD File Offset: 0x000587CD
		[DefaultValue(3)]
		[Range(0, 15)]
		public int VeinMinerSpeed { get; set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000CCB RID: 3275 RVA: 0x0005A5D6 File Offset: 0x000587D6
		// (set) Token: 0x06000CCC RID: 3276 RVA: 0x0005A5DE File Offset: 0x000587DE
		[DefaultValue(120)]
		[Range(1, 750)]
		public int VeinMinerTileLimit { get; set; }

		// Token: 0x06000CCD RID: 3277 RVA: 0x0005A5E7 File Offset: 0x000587E7
		public override void OnLoaded()
		{
			QoLCompendium.mainConfig = this;
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x0005A5EF File Offset: 0x000587EF
		public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref NetworkText message)
		{
			return Common.TryAcceptChanges(whoAmI, ref message);
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x0005A5F8 File Offset: 0x000587F8
		public QoLCConfig()
		{
			HashSet<string> hashSet = new HashSet<string>();
			hashSet.Add("Terraria:" + TileID.Search.GetName(6));
			hashSet.Add("Terraria:" + TileID.Search.GetName(7));
			hashSet.Add("Terraria:" + TileID.Search.GetName(8));
			hashSet.Add("Terraria:" + TileID.Search.GetName(9));
			hashSet.Add("Terraria:" + TileID.Search.GetName(22));
			hashSet.Add("Terraria:" + TileID.Search.GetName(37));
			hashSet.Add("Terraria:" + TileID.Search.GetName(40));
			hashSet.Add("Terraria:" + TileID.Search.GetName(56));
			hashSet.Add("Terraria:" + TileID.Search.GetName(58));
			hashSet.Add("Terraria:" + TileID.Search.GetName(63));
			hashSet.Add("Terraria:" + TileID.Search.GetName(64));
			hashSet.Add("Terraria:" + TileID.Search.GetName(65));
			hashSet.Add("Terraria:" + TileID.Search.GetName(66));
			hashSet.Add("Terraria:" + TileID.Search.GetName(67));
			hashSet.Add("Terraria:" + TileID.Search.GetName(68));
			hashSet.Add("Terraria:" + TileID.Search.GetName(107));
			hashSet.Add("Terraria:" + TileID.Search.GetName(108));
			hashSet.Add("Terraria:" + TileID.Search.GetName(111));
			hashSet.Add("Terraria:" + TileID.Search.GetName(123));
			hashSet.Add("Terraria:" + TileID.Search.GetName(162));
			hashSet.Add("Terraria:" + TileID.Search.GetName(166));
			hashSet.Add("Terraria:" + TileID.Search.GetName(167));
			hashSet.Add("Terraria:" + TileID.Search.GetName(168));
			hashSet.Add("Terraria:" + TileID.Search.GetName(169));
			hashSet.Add("Terraria:" + TileID.Search.GetName(178));
			hashSet.Add("Terraria:" + TileID.Search.GetName(204));
			hashSet.Add("Terraria:" + TileID.Search.GetName(211));
			hashSet.Add("Terraria:" + TileID.Search.GetName(221));
			hashSet.Add("Terraria:" + TileID.Search.GetName(222));
			hashSet.Add("Terraria:" + TileID.Search.GetName(223));
			hashSet.Add("Terraria:" + TileID.Search.GetName(224));
			hashSet.Add("Terraria:" + TileID.Search.GetName(404));
			hashSet.Add("Terraria:" + TileID.Search.GetName(407));
			hashSet.Add("Terraria:" + TileID.Search.GetName(408));
			hashSet.Add("Terraria:" + TileID.Search.GetName(481));
			hashSet.Add("Terraria:" + TileID.Search.GetName(482));
			hashSet.Add("Terraria:" + TileID.Search.GetName(483));
			hashSet.Add("Terraria:" + TileID.Search.GetName(48));
			this.VeinMinerTileWhitelist = hashSet;
			base..ctor();
		}

		// Token: 0x0400055F RID: 1375
		[DefaultListValue("Terraria:")]
		public HashSet<string> VeinMinerTileWhitelist;

		// Token: 0x02000533 RID: 1331
		[SeparatePage]
		public class MainClientConfig : ModConfig
		{
			// Token: 0x17000245 RID: 581
			// (get) Token: 0x06001A08 RID: 6664 RVA: 0x00002430 File Offset: 0x00000630
			public override ConfigScope Mode
			{
				get
				{
					return ConfigScope.ClientSide;
				}
			}

			// Token: 0x17000246 RID: 582
			// (get) Token: 0x06001A09 RID: 6665 RVA: 0x00091855 File Offset: 0x0008FA55
			// (set) Token: 0x06001A0A RID: 6666 RVA: 0x0009185D File Offset: 0x0008FA5D
			[Header("$Mods.QoLCompendium.QoLCConfig.Headers.Player")]
			[DefaultValue(true)]
			public bool FavoriteResearching { get; set; }

			// Token: 0x17000247 RID: 583
			// (get) Token: 0x06001A0B RID: 6667 RVA: 0x00091866 File Offset: 0x0008FA66
			// (set) Token: 0x06001A0C RID: 6668 RVA: 0x0009186E File Offset: 0x0008FA6E
			[DefaultValue(false)]
			public bool NoAuraVisuals { get; set; }

			// Token: 0x17000248 RID: 584
			// (get) Token: 0x06001A0D RID: 6669 RVA: 0x00091877 File Offset: 0x0008FA77
			// (set) Token: 0x06001A0E RID: 6670 RVA: 0x0009187F File Offset: 0x0008FA7F
			[DefaultValue(false)]
			public bool DisableDashDoubleTap { get; set; }

			// Token: 0x17000249 RID: 585
			// (get) Token: 0x06001A0F RID: 6671 RVA: 0x00091888 File Offset: 0x0008FA88
			// (set) Token: 0x06001A10 RID: 6672 RVA: 0x00091890 File Offset: 0x0008FA90
			[DefaultValue(100)]
			[Range(1, 100000)]
			[ReloadRequired]
			public int CombatTextLimit { get; set; }

			// Token: 0x1700024A RID: 586
			// (get) Token: 0x06001A11 RID: 6673 RVA: 0x00091899 File Offset: 0x0008FA99
			// (set) Token: 0x06001A12 RID: 6674 RVA: 0x000918A1 File Offset: 0x0008FAA1
			[DefaultValue(0)]
			[DrawTicks]
			public QoLCConfig.MainClientConfig.ConfigGlintID GlintColor { get; set; }

			// Token: 0x06001A13 RID: 6675 RVA: 0x000918AA File Offset: 0x0008FAAA
			public override void OnLoaded()
			{
				QoLCompendium.mainClientConfig = this;
			}

			// Token: 0x0200057D RID: 1405
			public enum ConfigGlintID
			{
				// Token: 0x04000FE4 RID: 4068
				White,
				// Token: 0x04000FE5 RID: 4069
				Red,
				// Token: 0x04000FE6 RID: 4070
				Orange,
				// Token: 0x04000FE7 RID: 4071
				Yellow,
				// Token: 0x04000FE8 RID: 4072
				Green,
				// Token: 0x04000FE9 RID: 4073
				Lime,
				// Token: 0x04000FEA RID: 4074
				Blue,
				// Token: 0x04000FEB RID: 4075
				Cyan,
				// Token: 0x04000FEC RID: 4076
				SkyBlue,
				// Token: 0x04000FED RID: 4077
				Purple,
				// Token: 0x04000FEE RID: 4078
				Magenta,
				// Token: 0x04000FEF RID: 4079
				Pink,
				// Token: 0x04000FF0 RID: 4080
				Rainbow
			}
		}

		// Token: 0x02000534 RID: 1332
		[SeparatePage]
		public class ItemConfig : ModConfig
		{
			// Token: 0x1700024B RID: 587
			// (get) Token: 0x06001A15 RID: 6677 RVA: 0x0000404D File Offset: 0x0000224D
			public override ConfigScope Mode
			{
				get
				{
					return ConfigScope.ServerSide;
				}
			}

			// Token: 0x1700024C RID: 588
			// (get) Token: 0x06001A16 RID: 6678 RVA: 0x000918BA File Offset: 0x0008FABA
			// (set) Token: 0x06001A17 RID: 6679 RVA: 0x000918C2 File Offset: 0x0008FAC2
			[DefaultValue(false)]
			[ReloadRequired]
			public bool DisableModdedItems { get; set; }

			// Token: 0x1700024D RID: 589
			// (get) Token: 0x06001A18 RID: 6680 RVA: 0x000918CB File Offset: 0x0008FACB
			// (set) Token: 0x06001A19 RID: 6681 RVA: 0x000918D3 File Offset: 0x0008FAD3
			[DefaultValue(true)]
			public bool AsphaltPlatform { get; set; }

			// Token: 0x1700024E RID: 590
			// (get) Token: 0x06001A1A RID: 6682 RVA: 0x000918DC File Offset: 0x0008FADC
			// (set) Token: 0x06001A1B RID: 6683 RVA: 0x000918E4 File Offset: 0x0008FAE4
			[DefaultValue(true)]
			public bool AutoStructures { get; set; }

			// Token: 0x1700024F RID: 591
			// (get) Token: 0x06001A1C RID: 6684 RVA: 0x000918ED File Offset: 0x0008FAED
			// (set) Token: 0x06001A1D RID: 6685 RVA: 0x000918F5 File Offset: 0x0008FAF5
			[DefaultValue(true)]
			public bool BannerBox { get; set; }

			// Token: 0x17000250 RID: 592
			// (get) Token: 0x06001A1E RID: 6686 RVA: 0x000918FE File Offset: 0x0008FAFE
			// (set) Token: 0x06001A1F RID: 6687 RVA: 0x00091906 File Offset: 0x0008FB06
			[DefaultValue(true)]
			public bool BottomlessBuckets { get; set; }

			// Token: 0x17000251 RID: 593
			// (get) Token: 0x06001A20 RID: 6688 RVA: 0x0009190F File Offset: 0x0008FB0F
			// (set) Token: 0x06001A21 RID: 6689 RVA: 0x00091917 File Offset: 0x0008FB17
			[DefaultValue(true)]
			public bool BossSummons { get; set; }

			// Token: 0x17000252 RID: 594
			// (get) Token: 0x06001A22 RID: 6690 RVA: 0x00091920 File Offset: 0x0008FB20
			// (set) Token: 0x06001A23 RID: 6691 RVA: 0x00091928 File Offset: 0x0008FB28
			[DefaultValue(true)]
			public bool ChallengersCoin { get; set; }

			// Token: 0x17000253 RID: 595
			// (get) Token: 0x06001A24 RID: 6692 RVA: 0x00091931 File Offset: 0x0008FB31
			// (set) Token: 0x06001A25 RID: 6693 RVA: 0x00091939 File Offset: 0x0008FB39
			[DefaultValue(true)]
			public bool ConstructionAccessories { get; set; }

			// Token: 0x17000254 RID: 596
			// (get) Token: 0x06001A26 RID: 6694 RVA: 0x00091942 File Offset: 0x0008FB42
			// (set) Token: 0x06001A27 RID: 6695 RVA: 0x0009194A File Offset: 0x0008FB4A
			[DefaultValue(true)]
			public bool CraftingStations { get; set; }

			// Token: 0x17000255 RID: 597
			// (get) Token: 0x06001A28 RID: 6696 RVA: 0x00091953 File Offset: 0x0008FB53
			// (set) Token: 0x06001A29 RID: 6697 RVA: 0x0009195B File Offset: 0x0008FB5B
			[DefaultValue(true)]
			public bool DestinationGlobe { get; set; }

			// Token: 0x17000256 RID: 598
			// (get) Token: 0x06001A2A RID: 6698 RVA: 0x00091964 File Offset: 0x0008FB64
			// (set) Token: 0x06001A2B RID: 6699 RVA: 0x0009196C File Offset: 0x0008FB6C
			[DefaultValue(true)]
			public bool Eightworm { get; set; }

			// Token: 0x17000257 RID: 599
			// (get) Token: 0x06001A2C RID: 6700 RVA: 0x00091975 File Offset: 0x0008FB75
			// (set) Token: 0x06001A2D RID: 6701 RVA: 0x0009197D File Offset: 0x0008FB7D
			[DefaultValue(true)]
			public bool EndlessAmmo { get; set; }

			// Token: 0x17000258 RID: 600
			// (get) Token: 0x06001A2E RID: 6702 RVA: 0x00091986 File Offset: 0x0008FB86
			// (set) Token: 0x06001A2F RID: 6703 RVA: 0x0009198E File Offset: 0x0008FB8E
			[DefaultValue(true)]
			public bool EntityManipulator { get; set; }

			// Token: 0x17000259 RID: 601
			// (get) Token: 0x06001A30 RID: 6704 RVA: 0x00091997 File Offset: 0x0008FB97
			// (set) Token: 0x06001A31 RID: 6705 RVA: 0x0009199F File Offset: 0x0008FB9F
			[DefaultValue(true)]
			public bool FishingAccessories { get; set; }

			// Token: 0x1700025A RID: 602
			// (get) Token: 0x06001A32 RID: 6706 RVA: 0x000919A8 File Offset: 0x0008FBA8
			// (set) Token: 0x06001A33 RID: 6707 RVA: 0x000919B0 File Offset: 0x0008FBB0
			[DefaultValue(true)]
			public bool GoldenLockpick { get; set; }

			// Token: 0x1700025B RID: 603
			// (get) Token: 0x06001A34 RID: 6708 RVA: 0x000919B9 File Offset: 0x0008FBB9
			// (set) Token: 0x06001A35 RID: 6709 RVA: 0x000919C1 File Offset: 0x0008FBC1
			[DefaultValue(true)]
			public bool GoldenPowder { get; set; }

			// Token: 0x1700025C RID: 604
			// (get) Token: 0x06001A36 RID: 6710 RVA: 0x000919CA File Offset: 0x0008FBCA
			// (set) Token: 0x06001A37 RID: 6711 RVA: 0x000919D2 File Offset: 0x0008FBD2
			[DefaultValue(true)]
			public bool InformationAccessories { get; set; }

			// Token: 0x1700025D RID: 605
			// (get) Token: 0x06001A38 RID: 6712 RVA: 0x000919DB File Offset: 0x0008FBDB
			// (set) Token: 0x06001A39 RID: 6713 RVA: 0x000919E3 File Offset: 0x0008FBE3
			[DefaultValue(true)]
			public bool LegendaryCatcher { get; set; }

			// Token: 0x1700025E RID: 606
			// (get) Token: 0x06001A3A RID: 6714 RVA: 0x000919EC File Offset: 0x0008FBEC
			// (set) Token: 0x06001A3B RID: 6715 RVA: 0x000919F4 File Offset: 0x0008FBF4
			[DefaultValue(true)]
			public bool Magnets { get; set; }

			// Token: 0x1700025F RID: 607
			// (get) Token: 0x06001A3C RID: 6716 RVA: 0x000919FD File Offset: 0x0008FBFD
			// (set) Token: 0x06001A3D RID: 6717 RVA: 0x00091A05 File Offset: 0x0008FC05
			[DefaultValue(true)]
			public bool MiniSundial { get; set; }

			// Token: 0x17000260 RID: 608
			// (get) Token: 0x06001A3E RID: 6718 RVA: 0x00091A0E File Offset: 0x0008FC0E
			// (set) Token: 0x06001A3F RID: 6719 RVA: 0x00091A16 File Offset: 0x0008FC16
			[DefaultValue(true)]
			public bool Mirrors { get; set; }

			// Token: 0x17000261 RID: 609
			// (get) Token: 0x06001A40 RID: 6720 RVA: 0x00091A1F File Offset: 0x0008FC1F
			// (set) Token: 0x06001A41 RID: 6721 RVA: 0x00091A27 File Offset: 0x0008FC27
			[DefaultValue(true)]
			public bool MobileStorages { get; set; }

			// Token: 0x17000262 RID: 610
			// (get) Token: 0x06001A42 RID: 6722 RVA: 0x00091A30 File Offset: 0x0008FC30
			// (set) Token: 0x06001A43 RID: 6723 RVA: 0x00091A38 File Offset: 0x0008FC38
			[DefaultValue(true)]
			public bool MoonPedestals { get; set; }

			// Token: 0x17000263 RID: 611
			// (get) Token: 0x06001A44 RID: 6724 RVA: 0x00091A41 File Offset: 0x0008FC41
			// (set) Token: 0x06001A45 RID: 6725 RVA: 0x00091A49 File Offset: 0x0008FC49
			[DefaultValue(true)]
			public bool Paperweight { get; set; }

			// Token: 0x17000264 RID: 612
			// (get) Token: 0x06001A46 RID: 6726 RVA: 0x00091A52 File Offset: 0x0008FC52
			// (set) Token: 0x06001A47 RID: 6727 RVA: 0x00091A5A File Offset: 0x0008FC5A
			[DefaultValue(true)]
			public bool PermanentBuffs { get; set; }

			// Token: 0x17000265 RID: 613
			// (get) Token: 0x06001A48 RID: 6728 RVA: 0x00091A63 File Offset: 0x0008FC63
			// (set) Token: 0x06001A49 RID: 6729 RVA: 0x00091A6B File Offset: 0x0008FC6B
			[DefaultValue(true)]
			public bool PhaseInterrupter { get; set; }

			// Token: 0x17000266 RID: 614
			// (get) Token: 0x06001A4A RID: 6730 RVA: 0x00091A74 File Offset: 0x0008FC74
			// (set) Token: 0x06001A4B RID: 6731 RVA: 0x00091A7C File Offset: 0x0008FC7C
			[DefaultValue(true)]
			public bool PotionCrate { get; set; }

			// Token: 0x17000267 RID: 615
			// (get) Token: 0x06001A4C RID: 6732 RVA: 0x00091A85 File Offset: 0x0008FC85
			// (set) Token: 0x06001A4D RID: 6733 RVA: 0x00091A8D File Offset: 0x0008FC8D
			[DefaultValue(true)]
			public bool Pylons { get; set; }

			// Token: 0x17000268 RID: 616
			// (get) Token: 0x06001A4E RID: 6734 RVA: 0x00091A96 File Offset: 0x0008FC96
			// (set) Token: 0x06001A4F RID: 6735 RVA: 0x00091A9E File Offset: 0x0008FC9E
			[DefaultValue(true)]
			public bool RegrowthStaves { get; set; }

			// Token: 0x17000269 RID: 617
			// (get) Token: 0x06001A50 RID: 6736 RVA: 0x00091AA7 File Offset: 0x0008FCA7
			// (set) Token: 0x06001A51 RID: 6737 RVA: 0x00091AAF File Offset: 0x0008FCAF
			[DefaultValue(true)]
			public bool RestockNotice { get; set; }

			// Token: 0x1700026A RID: 618
			// (get) Token: 0x06001A52 RID: 6738 RVA: 0x00091AB8 File Offset: 0x0008FCB8
			// (set) Token: 0x06001A53 RID: 6739 RVA: 0x00091AC0 File Offset: 0x0008FCC0
			[DefaultValue(true)]
			public bool SkeletonRucksack { get; set; }

			// Token: 0x1700026B RID: 619
			// (get) Token: 0x06001A54 RID: 6740 RVA: 0x00091AC9 File Offset: 0x0008FCC9
			// (set) Token: 0x06001A55 RID: 6741 RVA: 0x00091AD1 File Offset: 0x0008FCD1
			[DefaultValue(true)]
			public bool StarterBag { get; set; }

			// Token: 0x1700026C RID: 620
			// (get) Token: 0x06001A56 RID: 6742 RVA: 0x00091ADA File Offset: 0x0008FCDA
			// (set) Token: 0x06001A57 RID: 6743 RVA: 0x00091AE2 File Offset: 0x0008FCE2
			[DefaultValue(true)]
			public bool SummoningRemote { get; set; }

			// Token: 0x1700026D RID: 621
			// (get) Token: 0x06001A58 RID: 6744 RVA: 0x00091AEB File Offset: 0x0008FCEB
			// (set) Token: 0x06001A59 RID: 6745 RVA: 0x00091AF3 File Offset: 0x0008FCF3
			[DefaultValue(true)]
			public bool SuperDummy { get; set; }

			// Token: 0x1700026E RID: 622
			// (get) Token: 0x06001A5A RID: 6746 RVA: 0x00091AFC File Offset: 0x0008FCFC
			// (set) Token: 0x06001A5B RID: 6747 RVA: 0x00091B04 File Offset: 0x0008FD04
			[DefaultValue(true)]
			public bool TravelersMannequin { get; set; }

			// Token: 0x1700026F RID: 623
			// (get) Token: 0x06001A5C RID: 6748 RVA: 0x00091B0D File Offset: 0x0008FD0D
			// (set) Token: 0x06001A5D RID: 6749 RVA: 0x00091B15 File Offset: 0x0008FD15
			[DefaultValue(true)]
			public bool UltimateChecklist { get; set; }

			// Token: 0x17000270 RID: 624
			// (get) Token: 0x06001A5E RID: 6750 RVA: 0x00091B1E File Offset: 0x0008FD1E
			// (set) Token: 0x06001A5F RID: 6751 RVA: 0x00091B26 File Offset: 0x0008FD26
			[DefaultValue(true)]
			public bool WatchingEye { get; set; }

			// Token: 0x17000271 RID: 625
			// (get) Token: 0x06001A60 RID: 6752 RVA: 0x00091B2F File Offset: 0x0008FD2F
			// (set) Token: 0x06001A61 RID: 6753 RVA: 0x00091B37 File Offset: 0x0008FD37
			[DefaultValue(true)]
			public bool DedicatedItems { get; set; }

			// Token: 0x17000272 RID: 626
			// (get) Token: 0x06001A62 RID: 6754 RVA: 0x00091B40 File Offset: 0x0008FD40
			// (set) Token: 0x06001A63 RID: 6755 RVA: 0x00091B48 File Offset: 0x0008FD48
			[DefaultValue(true)]
			public bool CrossModItems { get; set; }

			// Token: 0x17000273 RID: 627
			// (get) Token: 0x06001A64 RID: 6756 RVA: 0x00091B51 File Offset: 0x0008FD51
			// (set) Token: 0x06001A65 RID: 6757 RVA: 0x00091B59 File Offset: 0x0008FD59
			public List<ItemDefinition> CustomItems { get; set; }

			// Token: 0x17000274 RID: 628
			// (get) Token: 0x06001A66 RID: 6758 RVA: 0x00091B62 File Offset: 0x0008FD62
			// (set) Token: 0x06001A67 RID: 6759 RVA: 0x00091B6A File Offset: 0x0008FD6A
			[Range(1, 99999)]
			public List<int> CustomItemQuantities { get; set; }

			// Token: 0x06001A68 RID: 6760 RVA: 0x00091B73 File Offset: 0x0008FD73
			public override void OnLoaded()
			{
				QoLCompendium.itemConfig = this;
			}

			// Token: 0x06001A69 RID: 6761 RVA: 0x0005A5EF File Offset: 0x000587EF
			public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref NetworkText message)
			{
				return Common.TryAcceptChanges(whoAmI, ref message);
			}
		}

		// Token: 0x02000535 RID: 1333
		[SeparatePage]
		public class ShopConfig : ModConfig
		{
			// Token: 0x17000275 RID: 629
			// (get) Token: 0x06001A6B RID: 6763 RVA: 0x0000404D File Offset: 0x0000224D
			public override ConfigScope Mode
			{
				get
				{
					return ConfigScope.ServerSide;
				}
			}

			// Token: 0x17000276 RID: 630
			// (get) Token: 0x06001A6C RID: 6764 RVA: 0x00091B7B File Offset: 0x0008FD7B
			// (set) Token: 0x06001A6D RID: 6765 RVA: 0x00091B83 File Offset: 0x0008FD83
			[Header("$Mods.QoLCompendium.QoLCConfig.Headers.BMShop")]
			[DefaultValue(true)]
			[ReloadRequired]
			public bool BMPotionShop { get; set; }

			// Token: 0x17000277 RID: 631
			// (get) Token: 0x06001A6E RID: 6766 RVA: 0x00091B8C File Offset: 0x0008FD8C
			// (set) Token: 0x06001A6F RID: 6767 RVA: 0x00091B94 File Offset: 0x0008FD94
			[DefaultValue(true)]
			[ReloadRequired]
			public bool BMStationShop { get; set; }

			// Token: 0x17000278 RID: 632
			// (get) Token: 0x06001A70 RID: 6768 RVA: 0x00091B9D File Offset: 0x0008FD9D
			// (set) Token: 0x06001A71 RID: 6769 RVA: 0x00091BA5 File Offset: 0x0008FDA5
			[DefaultValue(true)]
			[ReloadRequired]
			public bool BMMaterialShop { get; set; }

			// Token: 0x17000279 RID: 633
			// (get) Token: 0x06001A72 RID: 6770 RVA: 0x00091BAE File Offset: 0x0008FDAE
			// (set) Token: 0x06001A73 RID: 6771 RVA: 0x00091BB6 File Offset: 0x0008FDB6
			[DefaultValue(true)]
			[ReloadRequired]
			public bool BMMovementAccessoryShop { get; set; }

			// Token: 0x1700027A RID: 634
			// (get) Token: 0x06001A74 RID: 6772 RVA: 0x00091BBF File Offset: 0x0008FDBF
			// (set) Token: 0x06001A75 RID: 6773 RVA: 0x00091BC7 File Offset: 0x0008FDC7
			[DefaultValue(true)]
			[ReloadRequired]
			public bool BMCombatAccessoryShop { get; set; }

			// Token: 0x1700027B RID: 635
			// (get) Token: 0x06001A76 RID: 6774 RVA: 0x00091BD0 File Offset: 0x0008FDD0
			// (set) Token: 0x06001A77 RID: 6775 RVA: 0x00091BD8 File Offset: 0x0008FDD8
			[DefaultValue(true)]
			[ReloadRequired]
			public bool BMInformationShop { get; set; }

			// Token: 0x1700027C RID: 636
			// (get) Token: 0x06001A78 RID: 6776 RVA: 0x00091BE1 File Offset: 0x0008FDE1
			// (set) Token: 0x06001A79 RID: 6777 RVA: 0x00091BE9 File Offset: 0x0008FDE9
			[DefaultValue(true)]
			[ReloadRequired]
			public bool BMBagShop { get; set; }

			// Token: 0x1700027D RID: 637
			// (get) Token: 0x06001A7A RID: 6778 RVA: 0x00091BF2 File Offset: 0x0008FDF2
			// (set) Token: 0x06001A7B RID: 6779 RVA: 0x00091BFA File Offset: 0x0008FDFA
			[DefaultValue(true)]
			[ReloadRequired]
			public bool BMCrateShop { get; set; }

			// Token: 0x1700027E RID: 638
			// (get) Token: 0x06001A7C RID: 6780 RVA: 0x00091C03 File Offset: 0x0008FE03
			// (set) Token: 0x06001A7D RID: 6781 RVA: 0x00091C0B File Offset: 0x0008FE0B
			[DefaultValue(true)]
			[ReloadRequired]
			public bool BMOreShop { get; set; }

			// Token: 0x1700027F RID: 639
			// (get) Token: 0x06001A7E RID: 6782 RVA: 0x00091C14 File Offset: 0x0008FE14
			// (set) Token: 0x06001A7F RID: 6783 RVA: 0x00091C1C File Offset: 0x0008FE1C
			[DefaultValue(true)]
			[ReloadRequired]
			public bool BMNaturalBlockShop { get; set; }

			// Token: 0x17000280 RID: 640
			// (get) Token: 0x06001A80 RID: 6784 RVA: 0x00091C25 File Offset: 0x0008FE25
			// (set) Token: 0x06001A81 RID: 6785 RVA: 0x00091C2D File Offset: 0x0008FE2D
			[DefaultValue(true)]
			[ReloadRequired]
			public bool BMBuildingBlockShop { get; set; }

			// Token: 0x17000281 RID: 641
			// (get) Token: 0x06001A82 RID: 6786 RVA: 0x00091C36 File Offset: 0x0008FE36
			// (set) Token: 0x06001A83 RID: 6787 RVA: 0x00091C3E File Offset: 0x0008FE3E
			[DefaultValue(true)]
			[ReloadRequired]
			public bool BMHerbShop { get; set; }

			// Token: 0x17000282 RID: 642
			// (get) Token: 0x06001A84 RID: 6788 RVA: 0x00091C47 File Offset: 0x0008FE47
			// (set) Token: 0x06001A85 RID: 6789 RVA: 0x00091C4F File Offset: 0x0008FE4F
			[DefaultValue(true)]
			[ReloadRequired]
			public bool BMFishShop { get; set; }

			// Token: 0x17000283 RID: 643
			// (get) Token: 0x06001A86 RID: 6790 RVA: 0x00091C58 File Offset: 0x0008FE58
			// (set) Token: 0x06001A87 RID: 6791 RVA: 0x00091C60 File Offset: 0x0008FE60
			[DefaultValue(true)]
			[ReloadRequired]
			public bool BMMountShop { get; set; }

			// Token: 0x17000284 RID: 644
			// (get) Token: 0x06001A88 RID: 6792 RVA: 0x00091C69 File Offset: 0x0008FE69
			// (set) Token: 0x06001A89 RID: 6793 RVA: 0x00091C71 File Offset: 0x0008FE71
			[DefaultValue(true)]
			[ReloadRequired]
			public bool BMAmmoShop { get; set; }

			// Token: 0x17000285 RID: 645
			// (get) Token: 0x06001A8A RID: 6794 RVA: 0x00091C7A File Offset: 0x0008FE7A
			// (set) Token: 0x06001A8B RID: 6795 RVA: 0x00091C82 File Offset: 0x0008FE82
			[Header("$Mods.QoLCompendium.QoLCConfig.Headers.ECShop")]
			[DefaultValue(true)]
			[ReloadRequired]
			public bool ECPotionShop { get; set; }

			// Token: 0x17000286 RID: 646
			// (get) Token: 0x06001A8C RID: 6796 RVA: 0x00091C8B File Offset: 0x0008FE8B
			// (set) Token: 0x06001A8D RID: 6797 RVA: 0x00091C93 File Offset: 0x0008FE93
			[DefaultValue(true)]
			[ReloadRequired]
			public bool ECStationShop { get; set; }

			// Token: 0x17000287 RID: 647
			// (get) Token: 0x06001A8E RID: 6798 RVA: 0x00091C9C File Offset: 0x0008FE9C
			// (set) Token: 0x06001A8F RID: 6799 RVA: 0x00091CA4 File Offset: 0x0008FEA4
			[DefaultValue(true)]
			[ReloadRequired]
			public bool ECMaterialShop { get; set; }

			// Token: 0x17000288 RID: 648
			// (get) Token: 0x06001A90 RID: 6800 RVA: 0x00091CAD File Offset: 0x0008FEAD
			// (set) Token: 0x06001A91 RID: 6801 RVA: 0x00091CB5 File Offset: 0x0008FEB5
			[DefaultValue(true)]
			[ReloadRequired]
			public bool ECBagShop { get; set; }

			// Token: 0x17000289 RID: 649
			// (get) Token: 0x06001A92 RID: 6802 RVA: 0x00091CBE File Offset: 0x0008FEBE
			// (set) Token: 0x06001A93 RID: 6803 RVA: 0x00091CC6 File Offset: 0x0008FEC6
			[DefaultValue(true)]
			[ReloadRequired]
			public bool ECCrateShop { get; set; }

			// Token: 0x1700028A RID: 650
			// (get) Token: 0x06001A94 RID: 6804 RVA: 0x00091CCF File Offset: 0x0008FECF
			// (set) Token: 0x06001A95 RID: 6805 RVA: 0x00091CD7 File Offset: 0x0008FED7
			[DefaultValue(true)]
			[ReloadRequired]
			public bool ECOreShop { get; set; }

			// Token: 0x1700028B RID: 651
			// (get) Token: 0x06001A96 RID: 6806 RVA: 0x00091CE0 File Offset: 0x0008FEE0
			// (set) Token: 0x06001A97 RID: 6807 RVA: 0x00091CE8 File Offset: 0x0008FEE8
			[DefaultValue(true)]
			[ReloadRequired]
			public bool ECNaturalBlocksShop { get; set; }

			// Token: 0x1700028C RID: 652
			// (get) Token: 0x06001A98 RID: 6808 RVA: 0x00091CF1 File Offset: 0x0008FEF1
			// (set) Token: 0x06001A99 RID: 6809 RVA: 0x00091CF9 File Offset: 0x0008FEF9
			[DefaultValue(true)]
			[ReloadRequired]
			public bool ECBuildingBlocksShop { get; set; }

			// Token: 0x1700028D RID: 653
			// (get) Token: 0x06001A9A RID: 6810 RVA: 0x00091D02 File Offset: 0x0008FF02
			// (set) Token: 0x06001A9B RID: 6811 RVA: 0x00091D0A File Offset: 0x0008FF0A
			[DefaultValue(true)]
			[ReloadRequired]
			public bool ECHerbShop { get; set; }

			// Token: 0x1700028E RID: 654
			// (get) Token: 0x06001A9C RID: 6812 RVA: 0x00091D13 File Offset: 0x0008FF13
			// (set) Token: 0x06001A9D RID: 6813 RVA: 0x00091D1B File Offset: 0x0008FF1B
			[DefaultValue(true)]
			[ReloadRequired]
			public bool ECFishShop { get; set; }

			// Token: 0x1700028F RID: 655
			// (get) Token: 0x06001A9E RID: 6814 RVA: 0x00091D24 File Offset: 0x0008FF24
			// (set) Token: 0x06001A9F RID: 6815 RVA: 0x00091D2C File Offset: 0x0008FF2C
			[Header("$Mods.QoLCompendium.QoLCConfig.Headers.Prices")]
			[DefaultValue(true)]
			public bool BossScaling { get; set; }

			// Token: 0x17000290 RID: 656
			// (get) Token: 0x06001AA0 RID: 6816 RVA: 0x00091D35 File Offset: 0x0008FF35
			// (set) Token: 0x06001AA1 RID: 6817 RVA: 0x00091D3D File Offset: 0x0008FF3D
			[DefaultValue(1)]
			[Increment(1)]
			[Range(1, 1000)]
			public int GlobalPriceMultiplier { get; set; }

			// Token: 0x17000291 RID: 657
			// (get) Token: 0x06001AA2 RID: 6818 RVA: 0x00091D46 File Offset: 0x0008FF46
			// (set) Token: 0x06001AA3 RID: 6819 RVA: 0x00091D4E File Offset: 0x0008FF4E
			[DefaultValue(1)]
			[Increment(1)]
			[Range(1, 1000)]
			public int PotionPriceMultiplier { get; set; }

			// Token: 0x17000292 RID: 658
			// (get) Token: 0x06001AA4 RID: 6820 RVA: 0x00091D57 File Offset: 0x0008FF57
			// (set) Token: 0x06001AA5 RID: 6821 RVA: 0x00091D5F File Offset: 0x0008FF5F
			[DefaultValue(1)]
			[Increment(1)]
			[Range(1, 1000)]
			public int StationPriceMultiplier { get; set; }

			// Token: 0x17000293 RID: 659
			// (get) Token: 0x06001AA6 RID: 6822 RVA: 0x00091D68 File Offset: 0x0008FF68
			// (set) Token: 0x06001AA7 RID: 6823 RVA: 0x00091D70 File Offset: 0x0008FF70
			[DefaultValue(1)]
			[Increment(1)]
			[Range(1, 1000)]
			public int MaterialPriceMultiplier { get; set; }

			// Token: 0x17000294 RID: 660
			// (get) Token: 0x06001AA8 RID: 6824 RVA: 0x00091D79 File Offset: 0x0008FF79
			// (set) Token: 0x06001AA9 RID: 6825 RVA: 0x00091D81 File Offset: 0x0008FF81
			[DefaultValue(1)]
			[Increment(1)]
			[Range(1, 1000)]
			public int AccessoryPriceMultiplier { get; set; }

			// Token: 0x17000295 RID: 661
			// (get) Token: 0x06001AAA RID: 6826 RVA: 0x00091D8A File Offset: 0x0008FF8A
			// (set) Token: 0x06001AAB RID: 6827 RVA: 0x00091D92 File Offset: 0x0008FF92
			[DefaultValue(1)]
			[Increment(1)]
			[Range(1, 1000)]
			public int BagPriceMultiplier { get; set; }

			// Token: 0x17000296 RID: 662
			// (get) Token: 0x06001AAC RID: 6828 RVA: 0x00091D9B File Offset: 0x0008FF9B
			// (set) Token: 0x06001AAD RID: 6829 RVA: 0x00091DA3 File Offset: 0x0008FFA3
			[DefaultValue(1)]
			[Increment(1)]
			[Range(1, 1000)]
			public int CratePriceMultiplier { get; set; }

			// Token: 0x17000297 RID: 663
			// (get) Token: 0x06001AAE RID: 6830 RVA: 0x00091DAC File Offset: 0x0008FFAC
			// (set) Token: 0x06001AAF RID: 6831 RVA: 0x00091DB4 File Offset: 0x0008FFB4
			[DefaultValue(1)]
			[Increment(1)]
			[Range(1, 1000)]
			public int OrePriceMultiplier { get; set; }

			// Token: 0x17000298 RID: 664
			// (get) Token: 0x06001AB0 RID: 6832 RVA: 0x00091DBD File Offset: 0x0008FFBD
			// (set) Token: 0x06001AB1 RID: 6833 RVA: 0x00091DC5 File Offset: 0x0008FFC5
			[DefaultValue(1)]
			[Increment(1)]
			[Range(1, 1000)]
			public int NaturalBlockPriceMultiplier { get; set; }

			// Token: 0x17000299 RID: 665
			// (get) Token: 0x06001AB2 RID: 6834 RVA: 0x00091DCE File Offset: 0x0008FFCE
			// (set) Token: 0x06001AB3 RID: 6835 RVA: 0x00091DD6 File Offset: 0x0008FFD6
			[DefaultValue(1)]
			[Increment(1)]
			[Range(1, 1000)]
			public int BuildingBlockPriceMultiplier { get; set; }

			// Token: 0x1700029A RID: 666
			// (get) Token: 0x06001AB4 RID: 6836 RVA: 0x00091DDF File Offset: 0x0008FFDF
			// (set) Token: 0x06001AB5 RID: 6837 RVA: 0x00091DE7 File Offset: 0x0008FFE7
			[DefaultValue(1)]
			[Increment(1)]
			[Range(1, 1000)]
			public int HerbPriceMultiplier { get; set; }

			// Token: 0x1700029B RID: 667
			// (get) Token: 0x06001AB6 RID: 6838 RVA: 0x00091DF0 File Offset: 0x0008FFF0
			// (set) Token: 0x06001AB7 RID: 6839 RVA: 0x00091DF8 File Offset: 0x0008FFF8
			[DefaultValue(1)]
			[Increment(1)]
			[Range(1, 1000)]
			public int FishPriceMultiplier { get; set; }

			// Token: 0x1700029C RID: 668
			// (get) Token: 0x06001AB8 RID: 6840 RVA: 0x00091E01 File Offset: 0x00090001
			// (set) Token: 0x06001AB9 RID: 6841 RVA: 0x00091E09 File Offset: 0x00090009
			[DefaultValue(1)]
			[Increment(1)]
			[Range(1, 1000)]
			public int MountPriceMultiplier { get; set; }

			// Token: 0x1700029D RID: 669
			// (get) Token: 0x06001ABA RID: 6842 RVA: 0x00091E12 File Offset: 0x00090012
			// (set) Token: 0x06001ABB RID: 6843 RVA: 0x00091E1A File Offset: 0x0009001A
			[DefaultValue(1)]
			[Increment(1)]
			[Range(1, 1000)]
			public int AmmoPriceMultiplier { get; set; }

			// Token: 0x06001ABC RID: 6844 RVA: 0x00091E23 File Offset: 0x00090023
			public override void OnLoaded()
			{
				QoLCompendium.shopConfig = this;
			}

			// Token: 0x06001ABD RID: 6845 RVA: 0x0005A5EF File Offset: 0x000587EF
			public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref NetworkText message)
			{
				return Common.TryAcceptChanges(whoAmI, ref message);
			}
		}

		// Token: 0x02000536 RID: 1334
		[SeparatePage]
		public class TooltipConfig : ModConfig
		{
			// Token: 0x1700029E RID: 670
			// (get) Token: 0x06001ABF RID: 6847 RVA: 0x00002430 File Offset: 0x00000630
			public override ConfigScope Mode
			{
				get
				{
					return ConfigScope.ClientSide;
				}
			}

			// Token: 0x1700029F RID: 671
			// (get) Token: 0x06001AC0 RID: 6848 RVA: 0x00091E2B File Offset: 0x0009002B
			// (set) Token: 0x06001AC1 RID: 6849 RVA: 0x00091E33 File Offset: 0x00090033
			[Header("$Mods.QoLCompendium.QoLCConfig.Headers.Tooltips")]
			[DefaultValue(true)]
			public bool NoFavoriteTooltip { get; set; }

			// Token: 0x170002A0 RID: 672
			// (get) Token: 0x06001AC2 RID: 6850 RVA: 0x00091E3C File Offset: 0x0009003C
			// (set) Token: 0x06001AC3 RID: 6851 RVA: 0x00091E44 File Offset: 0x00090044
			[DefaultValue(true)]
			public bool ShimmerableTooltip { get; set; }

			// Token: 0x170002A1 RID: 673
			// (get) Token: 0x06001AC4 RID: 6852 RVA: 0x00091E4D File Offset: 0x0009004D
			// (set) Token: 0x06001AC5 RID: 6853 RVA: 0x00091E55 File Offset: 0x00090055
			[DefaultValue(true)]
			public bool WorksInBanksTooltip { get; set; }

			// Token: 0x170002A2 RID: 674
			// (get) Token: 0x06001AC6 RID: 6854 RVA: 0x00091E5E File Offset: 0x0009005E
			// (set) Token: 0x06001AC7 RID: 6855 RVA: 0x00091E66 File Offset: 0x00090066
			[DefaultValue(true)]
			public bool UsedPermanentUpgradeTooltip { get; set; }

			// Token: 0x170002A3 RID: 675
			// (get) Token: 0x06001AC8 RID: 6856 RVA: 0x00091E6F File Offset: 0x0009006F
			// (set) Token: 0x06001AC9 RID: 6857 RVA: 0x00091E77 File Offset: 0x00090077
			[DefaultValue(true)]
			public bool WingStatsTooltips { get; set; }

			// Token: 0x170002A4 RID: 676
			// (get) Token: 0x06001ACA RID: 6858 RVA: 0x00091E80 File Offset: 0x00090080
			// (set) Token: 0x06001ACB RID: 6859 RVA: 0x00091E88 File Offset: 0x00090088
			[DefaultValue(true)]
			public bool HookStatsTooltips { get; set; }

			// Token: 0x170002A5 RID: 677
			// (get) Token: 0x06001ACC RID: 6860 RVA: 0x00091E91 File Offset: 0x00090091
			// (set) Token: 0x06001ACD RID: 6861 RVA: 0x00091E99 File Offset: 0x00090099
			[DefaultValue(true)]
			public bool AmmoTooltip { get; set; }

			// Token: 0x170002A6 RID: 678
			// (get) Token: 0x06001ACE RID: 6862 RVA: 0x00091EA2 File Offset: 0x000900A2
			// (set) Token: 0x06001ACF RID: 6863 RVA: 0x00091EAA File Offset: 0x000900AA
			[DefaultValue(true)]
			public bool ActiveTooltip { get; set; }

			// Token: 0x170002A7 RID: 679
			// (get) Token: 0x06001AD0 RID: 6864 RVA: 0x00091EB3 File Offset: 0x000900B3
			// (set) Token: 0x06001AD1 RID: 6865 RVA: 0x00091EBB File Offset: 0x000900BB
			[DefaultValue(true)]
			public bool NoYoyoTooltip { get; set; }

			// Token: 0x170002A8 RID: 680
			// (get) Token: 0x06001AD2 RID: 6866 RVA: 0x00091EC4 File Offset: 0x000900C4
			// (set) Token: 0x06001AD3 RID: 6867 RVA: 0x00091ECC File Offset: 0x000900CC
			[DefaultValue(false)]
			public bool FromModTooltip { get; set; }

			// Token: 0x170002A9 RID: 681
			// (get) Token: 0x06001AD4 RID: 6868 RVA: 0x00091ED5 File Offset: 0x000900D5
			// (set) Token: 0x06001AD5 RID: 6869 RVA: 0x00091EDD File Offset: 0x000900DD
			[DefaultValue(true)]
			public bool ClassTagTooltip { get; set; }

			// Token: 0x06001AD6 RID: 6870 RVA: 0x00091EE6 File Offset: 0x000900E6
			public override void OnLoaded()
			{
				QoLCompendium.tooltipConfig = this;
			}
		}
	}
}
