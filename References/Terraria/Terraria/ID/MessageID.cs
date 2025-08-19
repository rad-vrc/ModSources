using System;
using Terraria.Utilities;

namespace Terraria.ID
{
	// Token: 0x020001C0 RID: 448
	public class MessageID
	{
		// Token: 0x04003775 RID: 14197
		public const byte NeverCalled = 0;

		// Token: 0x04003776 RID: 14198
		public const byte Hello = 1;

		// Token: 0x04003777 RID: 14199
		public const byte Kick = 2;

		// Token: 0x04003778 RID: 14200
		public const byte PlayerInfo = 3;

		// Token: 0x04003779 RID: 14201
		public const byte SyncPlayer = 4;

		// Token: 0x0400377A RID: 14202
		public const byte SyncEquipment = 5;

		// Token: 0x0400377B RID: 14203
		public const byte RequestWorldData = 6;

		// Token: 0x0400377C RID: 14204
		public const byte WorldData = 7;

		// Token: 0x0400377D RID: 14205
		public const byte SpawnTileData = 8;

		// Token: 0x0400377E RID: 14206
		public const byte StatusTextSize = 9;

		// Token: 0x0400377F RID: 14207
		public const byte TileSection = 10;

		// Token: 0x04003780 RID: 14208
		[Old("Deprecated. Framing happens as needed after TileSection is sent.")]
		public const byte TileFrameSection = 11;

		// Token: 0x04003781 RID: 14209
		public const byte PlayerSpawn = 12;

		// Token: 0x04003782 RID: 14210
		public const byte PlayerControls = 13;

		// Token: 0x04003783 RID: 14211
		public const byte PlayerActive = 14;

		// Token: 0x04003784 RID: 14212
		[Old("Deprecated.")]
		public const byte Unknown15 = 15;

		// Token: 0x04003785 RID: 14213
		public const byte PlayerLifeMana = 16;

		// Token: 0x04003786 RID: 14214
		public const byte TileManipulation = 17;

		// Token: 0x04003787 RID: 14215
		public const byte SetTime = 18;

		// Token: 0x04003788 RID: 14216
		public const byte ToggleDoorState = 19;

		// Token: 0x04003789 RID: 14217
		public const byte Unknown20 = 20;

		// Token: 0x0400378A RID: 14218
		public const byte SyncItem = 21;

		// Token: 0x0400378B RID: 14219
		public const byte ItemOwner = 22;

		// Token: 0x0400378C RID: 14220
		public const byte SyncNPC = 23;

		// Token: 0x0400378D RID: 14221
		public const byte UnusedMeleeStrike = 24;

		// Token: 0x0400378E RID: 14222
		[Old("Deprecated. Use NetTextModule instead.")]
		public const byte Unused25 = 25;

		// Token: 0x0400378F RID: 14223
		[Old("Deprecated.")]
		public const byte Unused26 = 26;

		// Token: 0x04003790 RID: 14224
		public const byte SyncProjectile = 27;

		// Token: 0x04003791 RID: 14225
		public const byte DamageNPC = 28;

		// Token: 0x04003792 RID: 14226
		public const byte KillProjectile = 29;

		// Token: 0x04003793 RID: 14227
		public const byte TogglePVP = 30;

		// Token: 0x04003794 RID: 14228
		public const byte RequestChestOpen = 31;

		// Token: 0x04003795 RID: 14229
		public const byte SyncChestItem = 32;

		// Token: 0x04003796 RID: 14230
		public const byte SyncPlayerChest = 33;

		// Token: 0x04003797 RID: 14231
		public const byte ChestUpdates = 34;

		// Token: 0x04003798 RID: 14232
		public const byte PlayerHeal = 35;

		// Token: 0x04003799 RID: 14233
		public const byte SyncPlayerZone = 36;

		// Token: 0x0400379A RID: 14234
		public const byte RequestPassword = 37;

		// Token: 0x0400379B RID: 14235
		public const byte SendPassword = 38;

		// Token: 0x0400379C RID: 14236
		public const byte ReleaseItemOwnership = 39;

		// Token: 0x0400379D RID: 14237
		public const byte SyncTalkNPC = 40;

		// Token: 0x0400379E RID: 14238
		public const byte ShotAnimationAndSound = 41;

		// Token: 0x0400379F RID: 14239
		public const byte Unknown42 = 42;

		// Token: 0x040037A0 RID: 14240
		public const byte Unknown43 = 43;

		// Token: 0x040037A1 RID: 14241
		[Old("Deprecated.")]
		public const byte Unknown44 = 44;

		// Token: 0x040037A2 RID: 14242
		public const byte Unknown45 = 45;

		// Token: 0x040037A3 RID: 14243
		public const byte Unknown46 = 46;

		// Token: 0x040037A4 RID: 14244
		public const byte Unknown47 = 47;

		// Token: 0x040037A5 RID: 14245
		[Old("Deprecated. Use NetLiquidModule instead.")]
		public const byte LiquidUpdate = 48;

		// Token: 0x040037A6 RID: 14246
		public const byte InitialSpawn = 49;

		// Token: 0x040037A7 RID: 14247
		public const byte PlayerBuffs = 50;

		// Token: 0x040037A8 RID: 14248
		public const byte MiscDataSync = 51;

		// Token: 0x040037A9 RID: 14249
		public const byte LockAndUnlock = 52;

		// Token: 0x040037AA RID: 14250
		public const byte AddNPCBuff = 53;

		// Token: 0x040037AB RID: 14251
		public const byte NPCBuffs = 54;

		// Token: 0x040037AC RID: 14252
		public const byte AddPlayerBuff = 55;

		// Token: 0x040037AD RID: 14253
		public const byte UniqueTownNPCInfoSyncRequest = 56;

		// Token: 0x040037AE RID: 14254
		public const byte Unknown57 = 57;

		// Token: 0x040037AF RID: 14255
		public const byte InstrumentSound = 58;

		// Token: 0x040037B0 RID: 14256
		public const byte HitSwitch = 59;

		// Token: 0x040037B1 RID: 14257
		public const byte Unknown60 = 60;

		// Token: 0x040037B2 RID: 14258
		public const byte SpawnBossUseLicenseStartEvent = 61;

		// Token: 0x040037B3 RID: 14259
		public const byte Unknown62 = 62;

		// Token: 0x040037B4 RID: 14260
		public const byte Unknown63 = 63;

		// Token: 0x040037B5 RID: 14261
		public const byte Unknown64 = 64;

		// Token: 0x040037B6 RID: 14262
		public const byte TeleportEntity = 65;

		// Token: 0x040037B7 RID: 14263
		public const byte Unknown66 = 66;

		// Token: 0x040037B8 RID: 14264
		public const byte Unknown67 = 67;

		// Token: 0x040037B9 RID: 14265
		public const byte Unknown68 = 68;

		// Token: 0x040037BA RID: 14266
		public const byte ChestName = 69;

		// Token: 0x040037BB RID: 14267
		public const byte BugCatching = 70;

		// Token: 0x040037BC RID: 14268
		public const byte BugReleasing = 71;

		// Token: 0x040037BD RID: 14269
		public const byte TravelMerchantItems = 72;

		// Token: 0x040037BE RID: 14270
		public const byte RequestTeleportationByServer = 73;

		// Token: 0x040037BF RID: 14271
		public const byte AnglerQuest = 74;

		// Token: 0x040037C0 RID: 14272
		public const byte AnglerQuestFinished = 75;

		// Token: 0x040037C1 RID: 14273
		public const byte QuestsCountSync = 76;

		// Token: 0x040037C2 RID: 14274
		public const byte TemporaryAnimation = 77;

		// Token: 0x040037C3 RID: 14275
		public const byte InvasionProgressReport = 78;

		// Token: 0x040037C4 RID: 14276
		public const byte PlaceObject = 79;

		// Token: 0x040037C5 RID: 14277
		public const byte SyncPlayerChestIndex = 80;

		// Token: 0x040037C6 RID: 14278
		public const byte CombatTextInt = 81;

		// Token: 0x040037C7 RID: 14279
		public const byte NetModules = 82;

		// Token: 0x040037C8 RID: 14280
		public const byte NPCKillCountDeathTally = 83;

		// Token: 0x040037C9 RID: 14281
		public const byte PlayerStealth = 84;

		// Token: 0x040037CA RID: 14282
		public const byte QuickStackChests = 85;

		// Token: 0x040037CB RID: 14283
		public const byte TileEntitySharing = 86;

		// Token: 0x040037CC RID: 14284
		public const byte TileEntityPlacement = 87;

		// Token: 0x040037CD RID: 14285
		public const byte ItemTweaker = 88;

		// Token: 0x040037CE RID: 14286
		public const byte ItemFrameTryPlacing = 89;

		// Token: 0x040037CF RID: 14287
		public const byte InstancedItem = 90;

		// Token: 0x040037D0 RID: 14288
		public const byte SyncEmoteBubble = 91;

		// Token: 0x040037D1 RID: 14289
		public const byte SyncExtraValue = 92;

		// Token: 0x040037D2 RID: 14290
		public const byte SocialHandshake = 93;

		// Token: 0x040037D3 RID: 14291
		public const byte Deprecated1 = 94;

		// Token: 0x040037D4 RID: 14292
		public const byte MurderSomeoneElsesPortal = 95;

		// Token: 0x040037D5 RID: 14293
		public const byte TeleportPlayerThroughPortal = 96;

		// Token: 0x040037D6 RID: 14294
		public const byte AchievementMessageNPCKilled = 97;

		// Token: 0x040037D7 RID: 14295
		public const byte AchievementMessageEventHappened = 98;

		// Token: 0x040037D8 RID: 14296
		public const byte MinionRestTargetUpdate = 99;

		// Token: 0x040037D9 RID: 14297
		public const byte TeleportNPCThroughPortal = 100;

		// Token: 0x040037DA RID: 14298
		public const byte UpdateTowerShieldStrengths = 101;

		// Token: 0x040037DB RID: 14299
		public const byte NebulaLevelupRequest = 102;

		// Token: 0x040037DC RID: 14300
		public const byte MoonlordHorror = 103;

		// Token: 0x040037DD RID: 14301
		public const byte ShopOverride = 104;

		// Token: 0x040037DE RID: 14302
		public const byte GemLockToggle = 105;

		// Token: 0x040037DF RID: 14303
		public const byte PoofOfSmoke = 106;

		// Token: 0x040037E0 RID: 14304
		public const byte SmartTextMessage = 107;

		// Token: 0x040037E1 RID: 14305
		public const byte WiredCannonShot = 108;

		// Token: 0x040037E2 RID: 14306
		public const byte MassWireOperation = 109;

		// Token: 0x040037E3 RID: 14307
		public const byte MassWireOperationPay = 110;

		// Token: 0x040037E4 RID: 14308
		public const byte ToggleParty = 111;

		// Token: 0x040037E5 RID: 14309
		public const byte SpecialFX = 112;

		// Token: 0x040037E6 RID: 14310
		public const byte CrystalInvasionStart = 113;

		// Token: 0x040037E7 RID: 14311
		public const byte CrystalInvasionWipeAllTheThingsss = 114;

		// Token: 0x040037E8 RID: 14312
		public const byte MinionAttackTargetUpdate = 115;

		// Token: 0x040037E9 RID: 14313
		public const byte CrystalInvasionSendWaitTime = 116;

		// Token: 0x040037EA RID: 14314
		public const byte PlayerHurtV2 = 117;

		// Token: 0x040037EB RID: 14315
		public const byte PlayerDeathV2 = 118;

		// Token: 0x040037EC RID: 14316
		public const byte CombatTextString = 119;

		// Token: 0x040037ED RID: 14317
		public const byte Emoji = 120;

		// Token: 0x040037EE RID: 14318
		public const byte TEDisplayDollItemSync = 121;

		// Token: 0x040037EF RID: 14319
		public const byte RequestTileEntityInteraction = 122;

		// Token: 0x040037F0 RID: 14320
		public const byte WeaponsRackTryPlacing = 123;

		// Token: 0x040037F1 RID: 14321
		public const byte TEHatRackItemSync = 124;

		// Token: 0x040037F2 RID: 14322
		public const byte SyncTilePicking = 125;

		// Token: 0x040037F3 RID: 14323
		public const byte SyncRevengeMarker = 126;

		// Token: 0x040037F4 RID: 14324
		public const byte RemoveRevengeMarker = 127;

		// Token: 0x040037F5 RID: 14325
		public const byte LandGolfBallInCup = 128;

		// Token: 0x040037F6 RID: 14326
		public const byte FinishedConnectingToServer = 129;

		// Token: 0x040037F7 RID: 14327
		public const byte FishOutNPC = 130;

		// Token: 0x040037F8 RID: 14328
		public const byte TamperWithNPC = 131;

		// Token: 0x040037F9 RID: 14329
		public const byte PlayLegacySound = 132;

		// Token: 0x040037FA RID: 14330
		public const byte FoodPlatterTryPlacing = 133;

		// Token: 0x040037FB RID: 14331
		public const byte UpdatePlayerLuckFactors = 134;

		// Token: 0x040037FC RID: 14332
		public const byte DeadPlayer = 135;

		// Token: 0x040037FD RID: 14333
		public const byte SyncCavernMonsterType = 136;

		// Token: 0x040037FE RID: 14334
		public const byte RequestNPCBuffRemoval = 137;

		// Token: 0x040037FF RID: 14335
		public const byte ClientSyncedInventory = 138;

		// Token: 0x04003800 RID: 14336
		public const byte SetCountsAsHostForGameplay = 139;

		// Token: 0x04003801 RID: 14337
		public const byte SetMiscEventValues = 140;

		// Token: 0x04003802 RID: 14338
		public const byte RequestLucyPopup = 141;

		// Token: 0x04003803 RID: 14339
		public const byte SyncProjectileTrackers = 142;

		// Token: 0x04003804 RID: 14340
		public const byte CrystalInvasionRequestedToSkipWaitTime = 143;

		// Token: 0x04003805 RID: 14341
		public const byte RequestQuestEffect = 144;

		// Token: 0x04003806 RID: 14342
		public const byte SyncItemsWithShimmer = 145;

		// Token: 0x04003807 RID: 14343
		public const byte ShimmerActions = 146;

		// Token: 0x04003808 RID: 14344
		public const byte SyncLoadout = 147;

		// Token: 0x04003809 RID: 14345
		public const byte SyncItemCannotBeTakenByEnemies = 148;

		// Token: 0x0400380A RID: 14346
		public static readonly byte Count = 149;
	}
}
