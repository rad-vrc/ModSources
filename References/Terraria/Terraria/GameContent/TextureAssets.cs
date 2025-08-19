using System;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ID;

namespace Terraria.GameContent
{
	// Token: 0x02000201 RID: 513
	public static class TextureAssets
	{
		// Token: 0x04004421 RID: 17441
		public static Asset<Texture2D>[] InfoIcon = new Asset<Texture2D>[14];

		// Token: 0x04004422 RID: 17442
		public static Asset<Texture2D>[] WireUi = new Asset<Texture2D>[12];

		// Token: 0x04004423 RID: 17443
		public static Asset<Texture2D> BuilderAcc;

		// Token: 0x04004424 RID: 17444
		public static Asset<Texture2D> QuicksIcon;

		// Token: 0x04004425 RID: 17445
		public static Asset<Texture2D>[] Clothes = new Asset<Texture2D>[6];

		// Token: 0x04004426 RID: 17446
		public static Asset<Texture2D>[] MapIcon = new Asset<Texture2D>[9];

		// Token: 0x04004427 RID: 17447
		public static Asset<Texture2D>[] Underworld = new Asset<Texture2D>[14];

		// Token: 0x04004428 RID: 17448
		public static Asset<Texture2D> MapPing;

		// Token: 0x04004429 RID: 17449
		public static Asset<Texture2D> Map;

		// Token: 0x0400442A RID: 17450
		public static Asset<Texture2D>[] MapBGs = new Asset<Texture2D>[42];

		// Token: 0x0400442B RID: 17451
		public static Asset<Texture2D> Hue;

		// Token: 0x0400442C RID: 17452
		public static Asset<Texture2D> FlameRing;

		// Token: 0x0400442D RID: 17453
		public static Asset<Texture2D> MapDeath;

		// Token: 0x0400442E RID: 17454
		public static Asset<Texture2D> ColorSlider;

		// Token: 0x0400442F RID: 17455
		public static Asset<Texture2D> ColorBar;

		// Token: 0x04004430 RID: 17456
		public static Asset<Texture2D> ColorBlip;

		// Token: 0x04004431 RID: 17457
		public static Asset<Texture2D> SmartDig;

		// Token: 0x04004432 RID: 17458
		public static Asset<Texture2D> ColorHighlight;

		// Token: 0x04004433 RID: 17459
		public static Asset<Texture2D> TileCrack;

		// Token: 0x04004434 RID: 17460
		public static Asset<Texture2D> LockOnCursor;

		// Token: 0x04004435 RID: 17461
		public static Asset<Texture2D> IceBarrier;

		// Token: 0x04004436 RID: 17462
		public static Asset<Texture2D>[] ChestStack = new Asset<Texture2D>[2];

		// Token: 0x04004437 RID: 17463
		public static Asset<Texture2D>[] NpcHead = new Asset<Texture2D>[NPCHeadID.Count];

		// Token: 0x04004438 RID: 17464
		public static Asset<Texture2D>[] NpcHeadBoss = new Asset<Texture2D>[40];

		// Token: 0x04004439 RID: 17465
		public static Asset<Texture2D>[] CraftToggle = new Asset<Texture2D>[4];

		// Token: 0x0400443A RID: 17466
		public static Asset<Texture2D>[] InventorySort = new Asset<Texture2D>[2];

		// Token: 0x0400443B RID: 17467
		public static Asset<Texture2D>[] TextGlyph = new Asset<Texture2D>[1];

		// Token: 0x0400443C RID: 17468
		public static Asset<Texture2D>[] HotbarRadial = new Asset<Texture2D>[3];

		// Token: 0x0400443D RID: 17469
		public static Asset<Texture2D> CraftUpButton;

		// Token: 0x0400443E RID: 17470
		public static Asset<Texture2D> CraftDownButton;

		// Token: 0x0400443F RID: 17471
		public static Asset<Texture2D> ScrollLeftButton;

		// Token: 0x04004440 RID: 17472
		public static Asset<Texture2D> ScrollRightButton;

		// Token: 0x04004441 RID: 17473
		public static Asset<Texture2D> Frozen;

		// Token: 0x04004442 RID: 17474
		public static Asset<Texture2D> MagicPixel;

		// Token: 0x04004443 RID: 17475
		public static Asset<Texture2D> SettingsPanel;

		// Token: 0x04004444 RID: 17476
		public static Asset<Texture2D> SettingsPanel2;

		// Token: 0x04004445 RID: 17477
		public static Asset<Texture2D>[] Dest = new Asset<Texture2D>[3];

		// Token: 0x04004446 RID: 17478
		public static Asset<Texture2D>[] Gem = new Asset<Texture2D>[7];

		// Token: 0x04004447 RID: 17479
		public static Asset<Texture2D>[] RudolphMount = new Asset<Texture2D>[3];

		// Token: 0x04004448 RID: 17480
		public static Asset<Texture2D> BunnyMount;

		// Token: 0x04004449 RID: 17481
		public static Asset<Texture2D> PigronMount;

		// Token: 0x0400444A RID: 17482
		public static Asset<Texture2D> SlimeMount;

		// Token: 0x0400444B RID: 17483
		public static Asset<Texture2D> MinecartMount;

		// Token: 0x0400444C RID: 17484
		public static Asset<Texture2D> TurtleMount;

		// Token: 0x0400444D RID: 17485
		public static Asset<Texture2D> DesertMinecartMount;

		// Token: 0x0400444E RID: 17486
		public static Asset<Texture2D> FishMinecartMount;

		// Token: 0x0400444F RID: 17487
		public static Asset<Texture2D>[] BeeMount = new Asset<Texture2D>[2];

		// Token: 0x04004450 RID: 17488
		public static Asset<Texture2D>[] UfoMount = new Asset<Texture2D>[2];

		// Token: 0x04004451 RID: 17489
		public static Asset<Texture2D>[] DrillMount = new Asset<Texture2D>[6];

		// Token: 0x04004452 RID: 17490
		public static Asset<Texture2D>[] ScutlixMount = new Asset<Texture2D>[3];

		// Token: 0x04004453 RID: 17491
		public static Asset<Texture2D> UnicornMount;

		// Token: 0x04004454 RID: 17492
		public static Asset<Texture2D> BasiliskMount;

		// Token: 0x04004455 RID: 17493
		public static Asset<Texture2D>[] MinecartMechMount = new Asset<Texture2D>[2];

		// Token: 0x04004456 RID: 17494
		public static Asset<Texture2D>[] CuteFishronMount = new Asset<Texture2D>[2];

		// Token: 0x04004457 RID: 17495
		public static Asset<Texture2D> MinecartWoodMount;

		// Token: 0x04004458 RID: 17496
		public static Asset<Texture2D>[] Wings = new Asset<Texture2D>[ArmorIDs.Wing.Count];

		// Token: 0x04004459 RID: 17497
		public static Asset<Texture2D>[] ArmorHead = new Asset<Texture2D>[ArmorIDs.Head.Count];

		// Token: 0x0400445A RID: 17498
		public static Asset<Texture2D>[] ArmorBody = new Asset<Texture2D>[ArmorIDs.Body.Count];

		// Token: 0x0400445B RID: 17499
		public static Asset<Texture2D>[] ArmorBodyComposite = new Asset<Texture2D>[ArmorIDs.Body.Count];

		// Token: 0x0400445C RID: 17500
		public static Asset<Texture2D>[] FemaleBody = new Asset<Texture2D>[ArmorIDs.Body.Count];

		// Token: 0x0400445D RID: 17501
		public static Asset<Texture2D>[] ArmorArm = new Asset<Texture2D>[ArmorIDs.Body.Count];

		// Token: 0x0400445E RID: 17502
		public static Asset<Texture2D>[] ArmorLeg = new Asset<Texture2D>[ArmorIDs.Legs.Count];

		// Token: 0x0400445F RID: 17503
		public static Asset<Texture2D>[] AccHandsOn = new Asset<Texture2D>[ArmorIDs.HandOn.Count];

		// Token: 0x04004460 RID: 17504
		public static Asset<Texture2D>[] AccHandsOnComposite = new Asset<Texture2D>[ArmorIDs.HandOn.Count];

		// Token: 0x04004461 RID: 17505
		public static Asset<Texture2D>[] AccHandsOff = new Asset<Texture2D>[ArmorIDs.HandOff.Count];

		// Token: 0x04004462 RID: 17506
		public static Asset<Texture2D>[] AccHandsOffComposite = new Asset<Texture2D>[ArmorIDs.HandOff.Count];

		// Token: 0x04004463 RID: 17507
		public static Asset<Texture2D>[] AccBack = new Asset<Texture2D>[ArmorIDs.Back.Count];

		// Token: 0x04004464 RID: 17508
		public static Asset<Texture2D>[] AccFront = new Asset<Texture2D>[ArmorIDs.Front.Count];

		// Token: 0x04004465 RID: 17509
		public static Asset<Texture2D>[] AccShoes = new Asset<Texture2D>[ArmorIDs.Shoe.Count];

		// Token: 0x04004466 RID: 17510
		public static Asset<Texture2D>[] AccWaist = new Asset<Texture2D>[ArmorIDs.Waist.Count];

		// Token: 0x04004467 RID: 17511
		public static Asset<Texture2D>[] AccShield = new Asset<Texture2D>[ArmorIDs.Shield.Count];

		// Token: 0x04004468 RID: 17512
		public static Asset<Texture2D>[] AccNeck = new Asset<Texture2D>[ArmorIDs.Neck.Count];

		// Token: 0x04004469 RID: 17513
		public static Asset<Texture2D>[] AccFace = new Asset<Texture2D>[(int)ArmorIDs.Face.Count];

		// Token: 0x0400446A RID: 17514
		public static Asset<Texture2D>[] AccBalloon = new Asset<Texture2D>[ArmorIDs.Balloon.Count];

		// Token: 0x0400446B RID: 17515
		public static Asset<Texture2D>[] AccBeard = new Asset<Texture2D>[(int)ArmorIDs.Beard.Count];

		// Token: 0x0400446C RID: 17516
		public static Asset<Texture2D> Pulley;

		// Token: 0x0400446D RID: 17517
		public static Asset<Texture2D>[] XmasTree = new Asset<Texture2D>[5];

		// Token: 0x0400446E RID: 17518
		public static Asset<Texture2D>[] Flames = new Asset<Texture2D>[18];

		// Token: 0x0400446F RID: 17519
		public static Asset<Texture2D> Timer;

		// Token: 0x04004470 RID: 17520
		public static Asset<Texture2D>[] Reforge = new Asset<Texture2D>[2];

		// Token: 0x04004471 RID: 17521
		public static Asset<Texture2D> EmoteMenuButton;

		// Token: 0x04004472 RID: 17522
		public static Asset<Texture2D> BestiaryMenuButton;

		// Token: 0x04004473 RID: 17523
		public static Asset<Texture2D> WallOutline;

		// Token: 0x04004474 RID: 17524
		public static Asset<Texture2D> Actuator;

		// Token: 0x04004475 RID: 17525
		public static Asset<Texture2D> Wire;

		// Token: 0x04004476 RID: 17526
		public static Asset<Texture2D> Wire2;

		// Token: 0x04004477 RID: 17527
		public static Asset<Texture2D> Wire3;

		// Token: 0x04004478 RID: 17528
		public static Asset<Texture2D> Wire4;

		// Token: 0x04004479 RID: 17529
		public static Asset<Texture2D> WireNew;

		// Token: 0x0400447A RID: 17530
		public static Asset<Texture2D>[] Camera = new Asset<Texture2D>[8];

		// Token: 0x0400447B RID: 17531
		public static Asset<Texture2D> FlyingCarpet;

		// Token: 0x0400447C RID: 17532
		public static Asset<Texture2D> Grid;

		// Token: 0x0400447D RID: 17533
		public static Asset<Texture2D> LightDisc;

		// Token: 0x0400447E RID: 17534
		public static Asset<Texture2D> EyeLaser;

		// Token: 0x0400447F RID: 17535
		public static Asset<Texture2D> BoneEyes;

		// Token: 0x04004480 RID: 17536
		public static Asset<Texture2D> BoneLaser;

		// Token: 0x04004481 RID: 17537
		public static Asset<Texture2D> Trash;

		// Token: 0x04004482 RID: 17538
		public static Asset<Texture2D> FishingLine;

		// Token: 0x04004483 RID: 17539
		public static Asset<Texture2D> Beetle;

		// Token: 0x04004484 RID: 17540
		public static Asset<Texture2D> Probe;

		// Token: 0x04004485 RID: 17541
		public static Asset<Texture2D> EyeLaserSmall;

		// Token: 0x04004486 RID: 17542
		public static Asset<Texture2D> XmasLight;

		// Token: 0x04004487 RID: 17543
		public static Asset<Texture2D>[] Golem = new Asset<Texture2D>[4];

		// Token: 0x04004488 RID: 17544
		public static Asset<Texture2D> Confuse;

		// Token: 0x04004489 RID: 17545
		public static Asset<Texture2D> SunOrb;

		// Token: 0x0400448A RID: 17546
		public static Asset<Texture2D> SunAltar;

		// Token: 0x0400448B RID: 17547
		public static Asset<Texture2D>[] Chains = new Asset<Texture2D>[(int)ChainID.Count];

		// Token: 0x0400448C RID: 17548
		public static Asset<Texture2D> Chain;

		// Token: 0x0400448D RID: 17549
		public static Asset<Texture2D>[] GemChain = new Asset<Texture2D>[7];

		// Token: 0x0400448E RID: 17550
		public static Asset<Texture2D> Chain2;

		// Token: 0x0400448F RID: 17551
		public static Asset<Texture2D> Chain3;

		// Token: 0x04004490 RID: 17552
		public static Asset<Texture2D> Chain4;

		// Token: 0x04004491 RID: 17553
		public static Asset<Texture2D> Chain5;

		// Token: 0x04004492 RID: 17554
		public static Asset<Texture2D> Chain6;

		// Token: 0x04004493 RID: 17555
		public static Asset<Texture2D> Chain7;

		// Token: 0x04004494 RID: 17556
		public static Asset<Texture2D> Chain8;

		// Token: 0x04004495 RID: 17557
		public static Asset<Texture2D> Chain9;

		// Token: 0x04004496 RID: 17558
		public static Asset<Texture2D> Chain10;

		// Token: 0x04004497 RID: 17559
		public static Asset<Texture2D> Chain11;

		// Token: 0x04004498 RID: 17560
		public static Asset<Texture2D> Chain12;

		// Token: 0x04004499 RID: 17561
		public static Asset<Texture2D> Chain13;

		// Token: 0x0400449A RID: 17562
		public static Asset<Texture2D> Chain14;

		// Token: 0x0400449B RID: 17563
		public static Asset<Texture2D> Chain15;

		// Token: 0x0400449C RID: 17564
		public static Asset<Texture2D> Chain16;

		// Token: 0x0400449D RID: 17565
		public static Asset<Texture2D> Chain17;

		// Token: 0x0400449E RID: 17566
		public static Asset<Texture2D> Chain18;

		// Token: 0x0400449F RID: 17567
		public static Asset<Texture2D> Chain19;

		// Token: 0x040044A0 RID: 17568
		public static Asset<Texture2D> Chain20;

		// Token: 0x040044A1 RID: 17569
		public static Asset<Texture2D> Chain21;

		// Token: 0x040044A2 RID: 17570
		public static Asset<Texture2D> Chain22;

		// Token: 0x040044A3 RID: 17571
		public static Asset<Texture2D> Chain23;

		// Token: 0x040044A4 RID: 17572
		public static Asset<Texture2D> Chain24;

		// Token: 0x040044A5 RID: 17573
		public static Asset<Texture2D> Chain25;

		// Token: 0x040044A6 RID: 17574
		public static Asset<Texture2D> Chain26;

		// Token: 0x040044A7 RID: 17575
		public static Asset<Texture2D> Chain27;

		// Token: 0x040044A8 RID: 17576
		public static Asset<Texture2D> Chain28;

		// Token: 0x040044A9 RID: 17577
		public static Asset<Texture2D> Chain29;

		// Token: 0x040044AA RID: 17578
		public static Asset<Texture2D> Chain30;

		// Token: 0x040044AB RID: 17579
		public static Asset<Texture2D> Chain31;

		// Token: 0x040044AC RID: 17580
		public static Asset<Texture2D> Chain32;

		// Token: 0x040044AD RID: 17581
		public static Asset<Texture2D> Chain33;

		// Token: 0x040044AE RID: 17582
		public static Asset<Texture2D> Chain34;

		// Token: 0x040044AF RID: 17583
		public static Asset<Texture2D> Chain35;

		// Token: 0x040044B0 RID: 17584
		public static Asset<Texture2D> Chain36;

		// Token: 0x040044B1 RID: 17585
		public static Asset<Texture2D> Chain37;

		// Token: 0x040044B2 RID: 17586
		public static Asset<Texture2D> Chain38;

		// Token: 0x040044B3 RID: 17587
		public static Asset<Texture2D> Chain39;

		// Token: 0x040044B4 RID: 17588
		public static Asset<Texture2D> Chain40;

		// Token: 0x040044B5 RID: 17589
		public static Asset<Texture2D> Chain41;

		// Token: 0x040044B6 RID: 17590
		public static Asset<Texture2D> Chain42;

		// Token: 0x040044B7 RID: 17591
		public static Asset<Texture2D> Chain43;

		// Token: 0x040044B8 RID: 17592
		public static Asset<Texture2D> Hb1;

		// Token: 0x040044B9 RID: 17593
		public static Asset<Texture2D> Hb2;

		// Token: 0x040044BA RID: 17594
		public static Asset<Texture2D> Chaos;

		// Token: 0x040044BB RID: 17595
		public static Asset<Texture2D> Cd;

		// Token: 0x040044BC RID: 17596
		public static Asset<Texture2D> Wof;

		// Token: 0x040044BD RID: 17597
		public static Asset<Texture2D> BoneArm;

		// Token: 0x040044BE RID: 17598
		public static Asset<Texture2D> BoneArm2;

		// Token: 0x040044BF RID: 17599
		public static Asset<Texture2D> PumpkingArm;

		// Token: 0x040044C0 RID: 17600
		public static Asset<Texture2D> PumpkingCloak;

		// Token: 0x040044C1 RID: 17601
		public static Asset<Texture2D>[] EquipPage = new Asset<Texture2D>[11];

		// Token: 0x040044C2 RID: 17602
		public static Asset<Texture2D> HouseBanner;

		// Token: 0x040044C3 RID: 17603
		public static Asset<Texture2D>[] Pvp = new Asset<Texture2D>[3];

		// Token: 0x040044C4 RID: 17604
		public static Asset<Texture2D>[] NpcToggle = new Asset<Texture2D>[2];

		// Token: 0x040044C5 RID: 17605
		public static Asset<Texture2D>[] HbLock = new Asset<Texture2D>[2];

		// Token: 0x040044C6 RID: 17606
		public static Asset<Texture2D>[] blockReplaceIcon = new Asset<Texture2D>[2];

		// Token: 0x040044C7 RID: 17607
		public static Asset<Texture2D>[] Buff = new Asset<Texture2D>[BuffID.Count];

		// Token: 0x040044C8 RID: 17608
		public static Asset<Texture2D>[] Item = new Asset<Texture2D>[(int)ItemID.Count];

		// Token: 0x040044C9 RID: 17609
		public static Asset<Texture2D>[] ItemFlame = new Asset<Texture2D>[(int)ItemID.Count];

		// Token: 0x040044CA RID: 17610
		public static Asset<Texture2D>[] Npc = new Asset<Texture2D>[(int)NPCID.Count];

		// Token: 0x040044CB RID: 17611
		public static Asset<Texture2D>[] Projectile = new Asset<Texture2D>[(int)ProjectileID.Count];

		// Token: 0x040044CC RID: 17612
		public static Asset<Texture2D>[] Gore = new Asset<Texture2D>[(int)GoreID.Count];

		// Token: 0x040044CD RID: 17613
		public static Asset<Texture2D>[] BackPack = new Asset<Texture2D>[10];

		// Token: 0x040044CE RID: 17614
		public static Asset<Texture2D> Rain;

		// Token: 0x040044CF RID: 17615
		public static Asset<Texture2D>[] GlowMask = new Asset<Texture2D>[(int)GlowMaskID.Count];

		// Token: 0x040044D0 RID: 17616
		public static Asset<Texture2D>[] Extra = new Asset<Texture2D>[(int)ExtrasID.Count];

		// Token: 0x040044D1 RID: 17617
		public static Asset<Texture2D>[] HighlightMask = new Asset<Texture2D>[(int)TileID.Count];

		// Token: 0x040044D2 RID: 17618
		public static Asset<Texture2D>[] Coin = new Asset<Texture2D>[4];

		// Token: 0x040044D3 RID: 17619
		public static Asset<Texture2D>[] Cursors = new Asset<Texture2D>[18];

		// Token: 0x040044D4 RID: 17620
		public static Asset<Texture2D> CursorRadial;

		// Token: 0x040044D5 RID: 17621
		public static Asset<Texture2D> Dust;

		// Token: 0x040044D6 RID: 17622
		public static Asset<Texture2D> Sun;

		// Token: 0x040044D7 RID: 17623
		public static Asset<Texture2D> Sun2;

		// Token: 0x040044D8 RID: 17624
		public static Asset<Texture2D> Sun3;

		// Token: 0x040044D9 RID: 17625
		public static Asset<Texture2D>[] Moon = new Asset<Texture2D>[9];

		// Token: 0x040044DA RID: 17626
		public static Asset<Texture2D> SmileyMoon;

		// Token: 0x040044DB RID: 17627
		public static Asset<Texture2D> PumpkinMoon;

		// Token: 0x040044DC RID: 17628
		public static Asset<Texture2D> SnowMoon;

		// Token: 0x040044DD RID: 17629
		public static Asset<Texture2D> OneDropLogo;

		// Token: 0x040044DE RID: 17630
		public static Asset<Texture2D>[] Tile = new Asset<Texture2D>[(int)TileID.Count];

		// Token: 0x040044DF RID: 17631
		public static Asset<Texture2D> BlackTile;

		// Token: 0x040044E0 RID: 17632
		public static Asset<Texture2D>[] Wall = new Asset<Texture2D>[(int)WallID.Count];

		// Token: 0x040044E1 RID: 17633
		public static Asset<Texture2D>[] Background = new Asset<Texture2D>[Main.maxBackgrounds];

		// Token: 0x040044E2 RID: 17634
		public static Asset<Texture2D>[] Cloud = new Asset<Texture2D>[CloudID.Count];

		// Token: 0x040044E3 RID: 17635
		public static Asset<Texture2D>[] Star = new Asset<Texture2D>[4];

		// Token: 0x040044E4 RID: 17636
		public static Asset<Texture2D>[] Liquid = new Asset<Texture2D>[15];

		// Token: 0x040044E5 RID: 17637
		public static Asset<Texture2D>[] LiquidSlope = new Asset<Texture2D>[15];

		// Token: 0x040044E6 RID: 17638
		public static Asset<Texture2D> Heart;

		// Token: 0x040044E7 RID: 17639
		public static Asset<Texture2D> Heart2;

		// Token: 0x040044E8 RID: 17640
		public static Asset<Texture2D> Mana;

		// Token: 0x040044E9 RID: 17641
		public static Asset<Texture2D> Bubble;

		// Token: 0x040044EA RID: 17642
		public static Asset<Texture2D> Flame;

		// Token: 0x040044EB RID: 17643
		public static Asset<Texture2D>[] CageTop = new Asset<Texture2D>[5];

		// Token: 0x040044EC RID: 17644
		public static Asset<Texture2D>[] TreeTop = new Asset<Texture2D>[32];

		// Token: 0x040044ED RID: 17645
		public static Asset<Texture2D>[] TreeBranch = new Asset<Texture2D>[32];

		// Token: 0x040044EE RID: 17646
		public static Asset<Texture2D>[] Wood = new Asset<Texture2D>[7];

		// Token: 0x040044EF RID: 17647
		public static Asset<Texture2D> ShroomCap;

		// Token: 0x040044F0 RID: 17648
		public static Asset<Texture2D> InventoryBack;

		// Token: 0x040044F1 RID: 17649
		public static Asset<Texture2D> InventoryBack2;

		// Token: 0x040044F2 RID: 17650
		public static Asset<Texture2D> InventoryBack3;

		// Token: 0x040044F3 RID: 17651
		public static Asset<Texture2D> InventoryBack4;

		// Token: 0x040044F4 RID: 17652
		public static Asset<Texture2D> InventoryBack5;

		// Token: 0x040044F5 RID: 17653
		public static Asset<Texture2D> InventoryBack6;

		// Token: 0x040044F6 RID: 17654
		public static Asset<Texture2D> InventoryBack7;

		// Token: 0x040044F7 RID: 17655
		public static Asset<Texture2D> InventoryBack8;

		// Token: 0x040044F8 RID: 17656
		public static Asset<Texture2D> InventoryBack9;

		// Token: 0x040044F9 RID: 17657
		public static Asset<Texture2D> InventoryBack10;

		// Token: 0x040044FA RID: 17658
		public static Asset<Texture2D> InventoryBack11;

		// Token: 0x040044FB RID: 17659
		public static Asset<Texture2D> InventoryBack12;

		// Token: 0x040044FC RID: 17660
		public static Asset<Texture2D> InventoryBack13;

		// Token: 0x040044FD RID: 17661
		public static Asset<Texture2D> InventoryBack14;

		// Token: 0x040044FE RID: 17662
		public static Asset<Texture2D> InventoryBack15;

		// Token: 0x040044FF RID: 17663
		public static Asset<Texture2D> InventoryBack16;

		// Token: 0x04004500 RID: 17664
		public static Asset<Texture2D> InventoryBack17;

		// Token: 0x04004501 RID: 17665
		public static Asset<Texture2D> InventoryBack18;

		// Token: 0x04004502 RID: 17666
		public static Asset<Texture2D> InventoryBack19;

		// Token: 0x04004503 RID: 17667
		public static Asset<Texture2D> HairStyleBack;

		// Token: 0x04004504 RID: 17668
		public static Asset<Texture2D> ClothesStyleBack;

		// Token: 0x04004505 RID: 17669
		public static Asset<Texture2D> InventoryTickOn;

		// Token: 0x04004506 RID: 17670
		public static Asset<Texture2D> InventoryTickOff;

		// Token: 0x04004507 RID: 17671
		public static Asset<Texture2D> SplashTexture16x9;

		// Token: 0x04004508 RID: 17672
		public static Asset<Texture2D> SplashTexture4x3;

		// Token: 0x04004509 RID: 17673
		public static Asset<Texture2D> SplashTextureLegoBack;

		// Token: 0x0400450A RID: 17674
		public static Asset<Texture2D> SplashTextureLegoResonanace;

		// Token: 0x0400450B RID: 17675
		public static Asset<Texture2D> SplashTextureLegoTree;

		// Token: 0x0400450C RID: 17676
		public static Asset<Texture2D> SplashTextureLegoFront;

		// Token: 0x0400450D RID: 17677
		public static Asset<Texture2D> Logo;

		// Token: 0x0400450E RID: 17678
		public static Asset<Texture2D> Logo2;

		// Token: 0x0400450F RID: 17679
		public static Asset<Texture2D> Logo3;

		// Token: 0x04004510 RID: 17680
		public static Asset<Texture2D> Logo4;

		// Token: 0x04004511 RID: 17681
		public static Asset<Texture2D> TextBack;

		// Token: 0x04004512 RID: 17682
		public static Asset<Texture2D> Chat;

		// Token: 0x04004513 RID: 17683
		public static Asset<Texture2D> Chat2;

		// Token: 0x04004514 RID: 17684
		public static Asset<Texture2D> ChatBack;

		// Token: 0x04004515 RID: 17685
		public static Asset<Texture2D> Team;

		// Token: 0x04004516 RID: 17686
		public static Asset<Texture2D> Re;

		// Token: 0x04004517 RID: 17687
		public static Asset<Texture2D> Ra;

		// Token: 0x04004518 RID: 17688
		public static Asset<Texture2D> Splash;

		// Token: 0x04004519 RID: 17689
		public static Asset<Texture2D> Fade;

		// Token: 0x0400451A RID: 17690
		public static Asset<Texture2D> Ninja;

		// Token: 0x0400451B RID: 17691
		public static Asset<Texture2D> AntLion;

		// Token: 0x0400451C RID: 17692
		public static Asset<Texture2D> SpikeBase;

		// Token: 0x0400451D RID: 17693
		public static Asset<Texture2D> Ghost;

		// Token: 0x0400451E RID: 17694
		public static Asset<Texture2D> EvilCactus;

		// Token: 0x0400451F RID: 17695
		public static Asset<Texture2D> GoodCactus;

		// Token: 0x04004520 RID: 17696
		public static Asset<Texture2D> CrimsonCactus;

		// Token: 0x04004521 RID: 17697
		public static Asset<Texture2D> WraithEye;

		// Token: 0x04004522 RID: 17698
		public static Asset<Texture2D> Firefly;

		// Token: 0x04004523 RID: 17699
		public static Asset<Texture2D> FireflyJar;

		// Token: 0x04004524 RID: 17700
		public static Asset<Texture2D> Lightningbug;

		// Token: 0x04004525 RID: 17701
		public static Asset<Texture2D> LightningbugJar;

		// Token: 0x04004526 RID: 17702
		public static Asset<Texture2D>[] JellyfishBowl = new Asset<Texture2D>[3];

		// Token: 0x04004527 RID: 17703
		public static Asset<Texture2D> GlowSnail;

		// Token: 0x04004528 RID: 17704
		public static Asset<Texture2D> IceQueen;

		// Token: 0x04004529 RID: 17705
		public static Asset<Texture2D> SantaTank;

		// Token: 0x0400452A RID: 17706
		public static Asset<Texture2D> ReaperEye;

		// Token: 0x0400452B RID: 17707
		public static Asset<Texture2D> JackHat;

		// Token: 0x0400452C RID: 17708
		public static Asset<Texture2D> TreeFace;

		// Token: 0x0400452D RID: 17709
		public static Asset<Texture2D> PumpkingFace;

		// Token: 0x0400452E RID: 17710
		public static Asset<Texture2D> DukeFishron;

		// Token: 0x0400452F RID: 17711
		public static Asset<Texture2D> MiniMinotaur;

		// Token: 0x04004530 RID: 17712
		public static Asset<Texture2D>[,] Players;

		// Token: 0x04004531 RID: 17713
		public static Asset<Texture2D>[] PlayerHair = new Asset<Texture2D>[165];

		// Token: 0x04004532 RID: 17714
		public static Asset<Texture2D>[] PlayerHairAlt = new Asset<Texture2D>[165];

		// Token: 0x04004533 RID: 17715
		public static Asset<Texture2D> LoadingSunflower;

		// Token: 0x04004534 RID: 17716
		public static Asset<Texture2D> GolfSwingBarPanel;

		// Token: 0x04004535 RID: 17717
		public static Asset<Texture2D> GolfSwingBarFill;

		// Token: 0x04004536 RID: 17718
		public static Asset<Texture2D> SpawnPoint;

		// Token: 0x04004537 RID: 17719
		public static Asset<Texture2D> SpawnBed;

		// Token: 0x04004538 RID: 17720
		public static Asset<Texture2D> GolfBallArrow;

		// Token: 0x04004539 RID: 17721
		public static Asset<Texture2D> GolfBallArrowShadow;

		// Token: 0x0400453A RID: 17722
		public static Asset<Texture2D> GolfBallOutline;

		// Token: 0x02000622 RID: 1570
		public static class RenderTargets
		{
			// Token: 0x040060B0 RID: 24752
			public static PlayerRainbowWingsTextureContent PlayerRainbowWings;

			// Token: 0x040060B1 RID: 24753
			public static PlayerTitaniumStormBuffTextureContent PlayerTitaniumStormBuff;

			// Token: 0x040060B2 RID: 24754
			public static PlayerQueenSlimeMountTextureContent QueenSlimeMount;
		}
	}
}
