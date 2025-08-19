using System;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ID;

namespace Terraria.GameContent
{
	/// <summary>
	/// Contains the <see cref="T:Microsoft.Xna.Framework.Graphics.Texture2D" /> assets used by the game, each stored as an <c>Asset&lt;Texture2D&gt;</c>.
	/// <para /> Note that the vanilla entries in <see cref="F:Terraria.GameContent.TextureAssets.Item" />, <see cref="F:Terraria.GameContent.TextureAssets.Npc" />, <see cref="F:Terraria.GameContent.TextureAssets.Projectile" />, <see cref="F:Terraria.GameContent.TextureAssets.Gore" />, <see cref="F:Terraria.GameContent.TextureAssets.Wall" />, <see cref="F:Terraria.GameContent.TextureAssets.Tile" />, <see cref="F:Terraria.GameContent.TextureAssets.ItemFlame" />, <see cref="F:Terraria.GameContent.TextureAssets.Background" />, and all of the player equipment and hair related fields are not necessarily loaded. Use the <see cref="M:Terraria.Main.LoadItem(System.Int32)" /> or similar methods before attempting to use those texture assets. Modded content in these arrays are always preloaded during mod loading.
	/// <para /> For example, the following code could be used to access the Acorn item texture asset.
	/// <code>
	/// Main.instance.LoadItem(ItemID.Acorn);
	/// var acornTexture = TextureAssets.Item[ItemID.Acorn];
	/// </code>
	/// <para /> See <see href="https://github.com/tModLoader/tModLoader/wiki/Assets#preload-textures">the Asset wiki page</see> for more information.
	/// </summary>
	// Token: 0x020004BC RID: 1212
	public static class TextureAssets
	{
		// Token: 0x040052AD RID: 21165
		public static Asset<Texture2D>[] InfoIcon = new Asset<Texture2D>[14];

		// Token: 0x040052AE RID: 21166
		public static Asset<Texture2D>[] WireUi = new Asset<Texture2D>[12];

		// Token: 0x040052AF RID: 21167
		public static Asset<Texture2D> BuilderAcc;

		// Token: 0x040052B0 RID: 21168
		public static Asset<Texture2D> QuicksIcon;

		// Token: 0x040052B1 RID: 21169
		public static Asset<Texture2D>[] Clothes = new Asset<Texture2D>[6];

		// Token: 0x040052B2 RID: 21170
		public static Asset<Texture2D>[] MapIcon = new Asset<Texture2D>[9];

		// Token: 0x040052B3 RID: 21171
		public static Asset<Texture2D>[] Underworld = new Asset<Texture2D>[14];

		// Token: 0x040052B4 RID: 21172
		public static Asset<Texture2D> MapPing;

		// Token: 0x040052B5 RID: 21173
		public static Asset<Texture2D> Map;

		// Token: 0x040052B6 RID: 21174
		public static Asset<Texture2D>[] MapBGs = new Asset<Texture2D>[42];

		// Token: 0x040052B7 RID: 21175
		public static Asset<Texture2D> Hue;

		// Token: 0x040052B8 RID: 21176
		public static Asset<Texture2D> FlameRing;

		// Token: 0x040052B9 RID: 21177
		public static Asset<Texture2D> MapDeath;

		// Token: 0x040052BA RID: 21178
		public static Asset<Texture2D> ColorSlider;

		// Token: 0x040052BB RID: 21179
		public static Asset<Texture2D> ColorBar;

		// Token: 0x040052BC RID: 21180
		public static Asset<Texture2D> ColorBlip;

		// Token: 0x040052BD RID: 21181
		public static Asset<Texture2D> SmartDig;

		// Token: 0x040052BE RID: 21182
		public static Asset<Texture2D> ColorHighlight;

		// Token: 0x040052BF RID: 21183
		public static Asset<Texture2D> TileCrack;

		// Token: 0x040052C0 RID: 21184
		public static Asset<Texture2D> LockOnCursor;

		// Token: 0x040052C1 RID: 21185
		public static Asset<Texture2D> IceBarrier;

		// Token: 0x040052C2 RID: 21186
		public static Asset<Texture2D>[] ChestStack = new Asset<Texture2D>[2];

		// Token: 0x040052C3 RID: 21187
		public static Asset<Texture2D>[] NpcHead = new Asset<Texture2D>[NPCHeadID.Count];

		// Token: 0x040052C4 RID: 21188
		public static Asset<Texture2D>[] NpcHeadBoss = new Asset<Texture2D>[40];

		// Token: 0x040052C5 RID: 21189
		public static Asset<Texture2D>[] CraftToggle = new Asset<Texture2D>[4];

		// Token: 0x040052C6 RID: 21190
		public static Asset<Texture2D>[] InventorySort = new Asset<Texture2D>[2];

		// Token: 0x040052C7 RID: 21191
		public static Asset<Texture2D>[] TextGlyph = new Asset<Texture2D>[1];

		// Token: 0x040052C8 RID: 21192
		public static Asset<Texture2D>[] HotbarRadial = new Asset<Texture2D>[3];

		// Token: 0x040052C9 RID: 21193
		public static Asset<Texture2D> CraftUpButton;

		// Token: 0x040052CA RID: 21194
		public static Asset<Texture2D> CraftDownButton;

		// Token: 0x040052CB RID: 21195
		public static Asset<Texture2D> ScrollLeftButton;

		// Token: 0x040052CC RID: 21196
		public static Asset<Texture2D> ScrollRightButton;

		// Token: 0x040052CD RID: 21197
		public static Asset<Texture2D> Frozen;

		// Token: 0x040052CE RID: 21198
		public static Asset<Texture2D> MagicPixel;

		// Token: 0x040052CF RID: 21199
		public static Asset<Texture2D> SettingsPanel;

		// Token: 0x040052D0 RID: 21200
		public static Asset<Texture2D> SettingsPanel2;

		// Token: 0x040052D1 RID: 21201
		public static Asset<Texture2D>[] Dest = new Asset<Texture2D>[3];

		// Token: 0x040052D2 RID: 21202
		public static Asset<Texture2D>[] Gem = new Asset<Texture2D>[7];

		// Token: 0x040052D3 RID: 21203
		public static Asset<Texture2D>[] RudolphMount = new Asset<Texture2D>[3];

		// Token: 0x040052D4 RID: 21204
		public static Asset<Texture2D> BunnyMount;

		// Token: 0x040052D5 RID: 21205
		public static Asset<Texture2D> PigronMount;

		// Token: 0x040052D6 RID: 21206
		public static Asset<Texture2D> SlimeMount;

		// Token: 0x040052D7 RID: 21207
		public static Asset<Texture2D> MinecartMount;

		// Token: 0x040052D8 RID: 21208
		public static Asset<Texture2D> TurtleMount;

		// Token: 0x040052D9 RID: 21209
		public static Asset<Texture2D> DesertMinecartMount;

		// Token: 0x040052DA RID: 21210
		public static Asset<Texture2D> FishMinecartMount;

		// Token: 0x040052DB RID: 21211
		public static Asset<Texture2D>[] BeeMount = new Asset<Texture2D>[2];

		// Token: 0x040052DC RID: 21212
		public static Asset<Texture2D>[] UfoMount = new Asset<Texture2D>[2];

		// Token: 0x040052DD RID: 21213
		public static Asset<Texture2D>[] DrillMount = new Asset<Texture2D>[6];

		// Token: 0x040052DE RID: 21214
		public static Asset<Texture2D>[] ScutlixMount = new Asset<Texture2D>[3];

		// Token: 0x040052DF RID: 21215
		public static Asset<Texture2D> UnicornMount;

		// Token: 0x040052E0 RID: 21216
		public static Asset<Texture2D> BasiliskMount;

		// Token: 0x040052E1 RID: 21217
		public static Asset<Texture2D>[] MinecartMechMount = new Asset<Texture2D>[2];

		// Token: 0x040052E2 RID: 21218
		public static Asset<Texture2D>[] CuteFishronMount = new Asset<Texture2D>[2];

		// Token: 0x040052E3 RID: 21219
		public static Asset<Texture2D> MinecartWoodMount;

		// Token: 0x040052E4 RID: 21220
		public static Asset<Texture2D>[] Wings = new Asset<Texture2D>[ArmorIDs.Wing.Count];

		// Token: 0x040052E5 RID: 21221
		public static Asset<Texture2D>[] ArmorHead = new Asset<Texture2D>[ArmorIDs.Head.Count];

		// Token: 0x040052E6 RID: 21222
		public static Asset<Texture2D>[] ArmorBody = new Asset<Texture2D>[ArmorIDs.Body.Count];

		// Token: 0x040052E7 RID: 21223
		public static Asset<Texture2D>[] ArmorBodyComposite = new Asset<Texture2D>[ArmorIDs.Body.Count];

		// Token: 0x040052E8 RID: 21224
		public static Asset<Texture2D>[] FemaleBody = new Asset<Texture2D>[ArmorIDs.Body.Count];

		// Token: 0x040052E9 RID: 21225
		public static Asset<Texture2D>[] ArmorArm = new Asset<Texture2D>[ArmorIDs.Body.Count];

		// Token: 0x040052EA RID: 21226
		public static Asset<Texture2D>[] ArmorLeg = new Asset<Texture2D>[ArmorIDs.Legs.Count];

		// Token: 0x040052EB RID: 21227
		public static Asset<Texture2D>[] AccHandsOn = new Asset<Texture2D>[ArmorIDs.HandOn.Count];

		// Token: 0x040052EC RID: 21228
		public static Asset<Texture2D>[] AccHandsOnComposite = new Asset<Texture2D>[ArmorIDs.HandOn.Count];

		// Token: 0x040052ED RID: 21229
		public static Asset<Texture2D>[] AccHandsOff = new Asset<Texture2D>[ArmorIDs.HandOff.Count];

		// Token: 0x040052EE RID: 21230
		public static Asset<Texture2D>[] AccHandsOffComposite = new Asset<Texture2D>[ArmorIDs.HandOff.Count];

		// Token: 0x040052EF RID: 21231
		public static Asset<Texture2D>[] AccBack = new Asset<Texture2D>[ArmorIDs.Back.Count];

		// Token: 0x040052F0 RID: 21232
		public static Asset<Texture2D>[] AccFront = new Asset<Texture2D>[ArmorIDs.Front.Count];

		// Token: 0x040052F1 RID: 21233
		public static Asset<Texture2D>[] AccShoes = new Asset<Texture2D>[ArmorIDs.Shoe.Count];

		// Token: 0x040052F2 RID: 21234
		public static Asset<Texture2D>[] AccWaist = new Asset<Texture2D>[ArmorIDs.Waist.Count];

		// Token: 0x040052F3 RID: 21235
		public static Asset<Texture2D>[] AccShield = new Asset<Texture2D>[ArmorIDs.Shield.Count];

		// Token: 0x040052F4 RID: 21236
		public static Asset<Texture2D>[] AccNeck = new Asset<Texture2D>[ArmorIDs.Neck.Count];

		// Token: 0x040052F5 RID: 21237
		public static Asset<Texture2D>[] AccFace = new Asset<Texture2D>[(int)ArmorIDs.Face.Count];

		// Token: 0x040052F6 RID: 21238
		public static Asset<Texture2D>[] AccBalloon = new Asset<Texture2D>[ArmorIDs.Balloon.Count];

		// Token: 0x040052F7 RID: 21239
		public static Asset<Texture2D>[] AccBeard = new Asset<Texture2D>[(int)ArmorIDs.Beard.Count];

		// Token: 0x040052F8 RID: 21240
		public static Asset<Texture2D> Pulley;

		// Token: 0x040052F9 RID: 21241
		public static Asset<Texture2D>[] XmasTree = new Asset<Texture2D>[5];

		// Token: 0x040052FA RID: 21242
		public static Asset<Texture2D>[] Flames = new Asset<Texture2D>[18];

		// Token: 0x040052FB RID: 21243
		public static Asset<Texture2D> Timer;

		// Token: 0x040052FC RID: 21244
		public static Asset<Texture2D>[] Reforge = new Asset<Texture2D>[2];

		// Token: 0x040052FD RID: 21245
		public static Asset<Texture2D> EmoteMenuButton;

		// Token: 0x040052FE RID: 21246
		public static Asset<Texture2D> BestiaryMenuButton;

		// Token: 0x040052FF RID: 21247
		public static Asset<Texture2D> WallOutline;

		// Token: 0x04005300 RID: 21248
		public static Asset<Texture2D> Actuator;

		// Token: 0x04005301 RID: 21249
		public static Asset<Texture2D> Wire;

		// Token: 0x04005302 RID: 21250
		public static Asset<Texture2D> Wire2;

		// Token: 0x04005303 RID: 21251
		public static Asset<Texture2D> Wire3;

		// Token: 0x04005304 RID: 21252
		public static Asset<Texture2D> Wire4;

		// Token: 0x04005305 RID: 21253
		public static Asset<Texture2D> WireNew;

		// Token: 0x04005306 RID: 21254
		public static Asset<Texture2D>[] Camera = new Asset<Texture2D>[8];

		// Token: 0x04005307 RID: 21255
		public static Asset<Texture2D> FlyingCarpet;

		// Token: 0x04005308 RID: 21256
		public static Asset<Texture2D> Grid;

		// Token: 0x04005309 RID: 21257
		public static Asset<Texture2D> LightDisc;

		// Token: 0x0400530A RID: 21258
		public static Asset<Texture2D> EyeLaser;

		// Token: 0x0400530B RID: 21259
		public static Asset<Texture2D> BoneEyes;

		// Token: 0x0400530C RID: 21260
		public static Asset<Texture2D> BoneLaser;

		// Token: 0x0400530D RID: 21261
		public static Asset<Texture2D> Trash;

		// Token: 0x0400530E RID: 21262
		public static Asset<Texture2D> FishingLine;

		// Token: 0x0400530F RID: 21263
		public static Asset<Texture2D> Beetle;

		// Token: 0x04005310 RID: 21264
		public static Asset<Texture2D> Probe;

		// Token: 0x04005311 RID: 21265
		public static Asset<Texture2D> EyeLaserSmall;

		// Token: 0x04005312 RID: 21266
		public static Asset<Texture2D> XmasLight;

		// Token: 0x04005313 RID: 21267
		public static Asset<Texture2D>[] Golem = new Asset<Texture2D>[4];

		// Token: 0x04005314 RID: 21268
		public static Asset<Texture2D> Confuse;

		// Token: 0x04005315 RID: 21269
		public static Asset<Texture2D> SunOrb;

		// Token: 0x04005316 RID: 21270
		public static Asset<Texture2D> SunAltar;

		// Token: 0x04005317 RID: 21271
		public static Asset<Texture2D>[] Chains = new Asset<Texture2D>[(int)ChainID.Count];

		// Token: 0x04005318 RID: 21272
		public static Asset<Texture2D> Chain;

		// Token: 0x04005319 RID: 21273
		public static Asset<Texture2D>[] GemChain = new Asset<Texture2D>[7];

		// Token: 0x0400531A RID: 21274
		public static Asset<Texture2D> Chain2;

		// Token: 0x0400531B RID: 21275
		public static Asset<Texture2D> Chain3;

		// Token: 0x0400531C RID: 21276
		public static Asset<Texture2D> Chain4;

		// Token: 0x0400531D RID: 21277
		public static Asset<Texture2D> Chain5;

		// Token: 0x0400531E RID: 21278
		public static Asset<Texture2D> Chain6;

		// Token: 0x0400531F RID: 21279
		public static Asset<Texture2D> Chain7;

		// Token: 0x04005320 RID: 21280
		public static Asset<Texture2D> Chain8;

		// Token: 0x04005321 RID: 21281
		public static Asset<Texture2D> Chain9;

		// Token: 0x04005322 RID: 21282
		public static Asset<Texture2D> Chain10;

		// Token: 0x04005323 RID: 21283
		public static Asset<Texture2D> Chain11;

		// Token: 0x04005324 RID: 21284
		public static Asset<Texture2D> Chain12;

		// Token: 0x04005325 RID: 21285
		public static Asset<Texture2D> Chain13;

		// Token: 0x04005326 RID: 21286
		public static Asset<Texture2D> Chain14;

		// Token: 0x04005327 RID: 21287
		public static Asset<Texture2D> Chain15;

		// Token: 0x04005328 RID: 21288
		public static Asset<Texture2D> Chain16;

		// Token: 0x04005329 RID: 21289
		public static Asset<Texture2D> Chain17;

		// Token: 0x0400532A RID: 21290
		public static Asset<Texture2D> Chain18;

		// Token: 0x0400532B RID: 21291
		public static Asset<Texture2D> Chain19;

		// Token: 0x0400532C RID: 21292
		public static Asset<Texture2D> Chain20;

		// Token: 0x0400532D RID: 21293
		public static Asset<Texture2D> Chain21;

		// Token: 0x0400532E RID: 21294
		public static Asset<Texture2D> Chain22;

		// Token: 0x0400532F RID: 21295
		public static Asset<Texture2D> Chain23;

		// Token: 0x04005330 RID: 21296
		public static Asset<Texture2D> Chain24;

		// Token: 0x04005331 RID: 21297
		public static Asset<Texture2D> Chain25;

		// Token: 0x04005332 RID: 21298
		public static Asset<Texture2D> Chain26;

		// Token: 0x04005333 RID: 21299
		public static Asset<Texture2D> Chain27;

		// Token: 0x04005334 RID: 21300
		public static Asset<Texture2D> Chain28;

		// Token: 0x04005335 RID: 21301
		public static Asset<Texture2D> Chain29;

		// Token: 0x04005336 RID: 21302
		public static Asset<Texture2D> Chain30;

		// Token: 0x04005337 RID: 21303
		public static Asset<Texture2D> Chain31;

		// Token: 0x04005338 RID: 21304
		public static Asset<Texture2D> Chain32;

		// Token: 0x04005339 RID: 21305
		public static Asset<Texture2D> Chain33;

		// Token: 0x0400533A RID: 21306
		public static Asset<Texture2D> Chain34;

		// Token: 0x0400533B RID: 21307
		public static Asset<Texture2D> Chain35;

		// Token: 0x0400533C RID: 21308
		public static Asset<Texture2D> Chain36;

		// Token: 0x0400533D RID: 21309
		public static Asset<Texture2D> Chain37;

		// Token: 0x0400533E RID: 21310
		public static Asset<Texture2D> Chain38;

		// Token: 0x0400533F RID: 21311
		public static Asset<Texture2D> Chain39;

		// Token: 0x04005340 RID: 21312
		public static Asset<Texture2D> Chain40;

		// Token: 0x04005341 RID: 21313
		public static Asset<Texture2D> Chain41;

		// Token: 0x04005342 RID: 21314
		public static Asset<Texture2D> Chain42;

		// Token: 0x04005343 RID: 21315
		public static Asset<Texture2D> Chain43;

		// Token: 0x04005344 RID: 21316
		public static Asset<Texture2D> Hb1;

		// Token: 0x04005345 RID: 21317
		public static Asset<Texture2D> Hb2;

		// Token: 0x04005346 RID: 21318
		public static Asset<Texture2D> Chaos;

		// Token: 0x04005347 RID: 21319
		public static Asset<Texture2D> Cd;

		// Token: 0x04005348 RID: 21320
		public static Asset<Texture2D> Wof;

		// Token: 0x04005349 RID: 21321
		public static Asset<Texture2D> BoneArm;

		// Token: 0x0400534A RID: 21322
		public static Asset<Texture2D> BoneArm2;

		// Token: 0x0400534B RID: 21323
		public static Asset<Texture2D> PumpkingArm;

		// Token: 0x0400534C RID: 21324
		public static Asset<Texture2D> PumpkingCloak;

		// Token: 0x0400534D RID: 21325
		public static Asset<Texture2D>[] EquipPage = new Asset<Texture2D>[11];

		// Token: 0x0400534E RID: 21326
		public static Asset<Texture2D> HouseBanner;

		// Token: 0x0400534F RID: 21327
		public static Asset<Texture2D>[] Pvp = new Asset<Texture2D>[3];

		// Token: 0x04005350 RID: 21328
		public static Asset<Texture2D>[] NpcToggle = new Asset<Texture2D>[2];

		// Token: 0x04005351 RID: 21329
		public static Asset<Texture2D>[] HbLock = new Asset<Texture2D>[2];

		// Token: 0x04005352 RID: 21330
		public static Asset<Texture2D>[] blockReplaceIcon = new Asset<Texture2D>[2];

		// Token: 0x04005353 RID: 21331
		public static Asset<Texture2D>[] Buff = new Asset<Texture2D>[BuffID.Count];

		// Token: 0x04005354 RID: 21332
		public static Asset<Texture2D>[] Item = new Asset<Texture2D>[(int)ItemID.Count];

		// Token: 0x04005355 RID: 21333
		public static Asset<Texture2D>[] ItemFlame = new Asset<Texture2D>[(int)ItemID.Count];

		// Token: 0x04005356 RID: 21334
		public static Asset<Texture2D>[] Npc = new Asset<Texture2D>[(int)NPCID.Count];

		// Token: 0x04005357 RID: 21335
		public static Asset<Texture2D>[] Projectile = new Asset<Texture2D>[(int)ProjectileID.Count];

		// Token: 0x04005358 RID: 21336
		public static Asset<Texture2D>[] Gore = new Asset<Texture2D>[(int)GoreID.Count];

		// Token: 0x04005359 RID: 21337
		public static Asset<Texture2D>[] BackPack = new Asset<Texture2D>[10];

		// Token: 0x0400535A RID: 21338
		public static Asset<Texture2D> Rain;

		// Token: 0x0400535B RID: 21339
		public static Asset<Texture2D>[] GlowMask = new Asset<Texture2D>[(int)GlowMaskID.Count];

		// Token: 0x0400535C RID: 21340
		public static Asset<Texture2D>[] Extra = new Asset<Texture2D>[(int)ExtrasID.Count];

		// Token: 0x0400535D RID: 21341
		public static Asset<Texture2D>[] HighlightMask = new Asset<Texture2D>[(int)TileID.Count];

		// Token: 0x0400535E RID: 21342
		public static Asset<Texture2D>[] Coin = new Asset<Texture2D>[4];

		// Token: 0x0400535F RID: 21343
		public static Asset<Texture2D>[] Cursors = new Asset<Texture2D>[18];

		// Token: 0x04005360 RID: 21344
		public static Asset<Texture2D> CursorRadial;

		// Token: 0x04005361 RID: 21345
		public static Asset<Texture2D> Dust;

		// Token: 0x04005362 RID: 21346
		public static Asset<Texture2D> Sun;

		// Token: 0x04005363 RID: 21347
		public static Asset<Texture2D> Sun2;

		// Token: 0x04005364 RID: 21348
		public static Asset<Texture2D> Sun3;

		// Token: 0x04005365 RID: 21349
		public static Asset<Texture2D>[] Moon = new Asset<Texture2D>[9];

		// Token: 0x04005366 RID: 21350
		public static Asset<Texture2D> SmileyMoon;

		// Token: 0x04005367 RID: 21351
		public static Asset<Texture2D> PumpkinMoon;

		// Token: 0x04005368 RID: 21352
		public static Asset<Texture2D> SnowMoon;

		// Token: 0x04005369 RID: 21353
		public static Asset<Texture2D> OneDropLogo;

		// Token: 0x0400536A RID: 21354
		public static Asset<Texture2D>[] Tile = new Asset<Texture2D>[(int)TileID.Count];

		// Token: 0x0400536B RID: 21355
		public static Asset<Texture2D> BlackTile;

		// Token: 0x0400536C RID: 21356
		public static Asset<Texture2D>[] Wall = new Asset<Texture2D>[(int)WallID.Count];

		// Token: 0x0400536D RID: 21357
		public static Asset<Texture2D>[] Background = new Asset<Texture2D>[Main.maxBackgrounds];

		// Token: 0x0400536E RID: 21358
		public static Asset<Texture2D>[] Cloud = new Asset<Texture2D>[CloudID.Count];

		// Token: 0x0400536F RID: 21359
		public static Asset<Texture2D>[] Star = new Asset<Texture2D>[4];

		// Token: 0x04005370 RID: 21360
		public static Asset<Texture2D>[] Liquid = new Asset<Texture2D>[15];

		// Token: 0x04005371 RID: 21361
		public static Asset<Texture2D>[] LiquidSlope = new Asset<Texture2D>[15];

		// Token: 0x04005372 RID: 21362
		public static Asset<Texture2D> Heart;

		// Token: 0x04005373 RID: 21363
		public static Asset<Texture2D> Heart2;

		// Token: 0x04005374 RID: 21364
		public static Asset<Texture2D> Mana;

		// Token: 0x04005375 RID: 21365
		public static Asset<Texture2D> Bubble;

		// Token: 0x04005376 RID: 21366
		public static Asset<Texture2D> Flame;

		// Token: 0x04005377 RID: 21367
		public static Asset<Texture2D>[] CageTop = new Asset<Texture2D>[5];

		// Token: 0x04005378 RID: 21368
		public static Asset<Texture2D>[] TreeTop = new Asset<Texture2D>[32];

		// Token: 0x04005379 RID: 21369
		public static Asset<Texture2D>[] TreeBranch = new Asset<Texture2D>[32];

		// Token: 0x0400537A RID: 21370
		public static Asset<Texture2D>[] Wood = new Asset<Texture2D>[7];

		// Token: 0x0400537B RID: 21371
		public static Asset<Texture2D> ShroomCap;

		// Token: 0x0400537C RID: 21372
		public static Asset<Texture2D> InventoryBack;

		// Token: 0x0400537D RID: 21373
		public static Asset<Texture2D> InventoryBack2;

		// Token: 0x0400537E RID: 21374
		public static Asset<Texture2D> InventoryBack3;

		// Token: 0x0400537F RID: 21375
		public static Asset<Texture2D> InventoryBack4;

		// Token: 0x04005380 RID: 21376
		public static Asset<Texture2D> InventoryBack5;

		// Token: 0x04005381 RID: 21377
		public static Asset<Texture2D> InventoryBack6;

		// Token: 0x04005382 RID: 21378
		public static Asset<Texture2D> InventoryBack7;

		// Token: 0x04005383 RID: 21379
		public static Asset<Texture2D> InventoryBack8;

		// Token: 0x04005384 RID: 21380
		public static Asset<Texture2D> InventoryBack9;

		// Token: 0x04005385 RID: 21381
		public static Asset<Texture2D> InventoryBack10;

		// Token: 0x04005386 RID: 21382
		public static Asset<Texture2D> InventoryBack11;

		// Token: 0x04005387 RID: 21383
		public static Asset<Texture2D> InventoryBack12;

		// Token: 0x04005388 RID: 21384
		public static Asset<Texture2D> InventoryBack13;

		// Token: 0x04005389 RID: 21385
		public static Asset<Texture2D> InventoryBack14;

		// Token: 0x0400538A RID: 21386
		public static Asset<Texture2D> InventoryBack15;

		// Token: 0x0400538B RID: 21387
		public static Asset<Texture2D> InventoryBack16;

		// Token: 0x0400538C RID: 21388
		public static Asset<Texture2D> InventoryBack17;

		// Token: 0x0400538D RID: 21389
		public static Asset<Texture2D> InventoryBack18;

		// Token: 0x0400538E RID: 21390
		public static Asset<Texture2D> InventoryBack19;

		// Token: 0x0400538F RID: 21391
		public static Asset<Texture2D> HairStyleBack;

		// Token: 0x04005390 RID: 21392
		public static Asset<Texture2D> ClothesStyleBack;

		// Token: 0x04005391 RID: 21393
		public static Asset<Texture2D> InventoryTickOn;

		// Token: 0x04005392 RID: 21394
		public static Asset<Texture2D> InventoryTickOff;

		// Token: 0x04005393 RID: 21395
		public static Asset<Texture2D> SplashTexture16x9;

		// Token: 0x04005394 RID: 21396
		public static Asset<Texture2D> SplashTexture4x3;

		// Token: 0x04005395 RID: 21397
		public static Asset<Texture2D> SplashTextureLegoBack;

		// Token: 0x04005396 RID: 21398
		public static Asset<Texture2D> SplashTextureLegoResonanace;

		// Token: 0x04005397 RID: 21399
		public static Asset<Texture2D> SplashTextureLegoTree;

		// Token: 0x04005398 RID: 21400
		public static Asset<Texture2D> SplashTextureLegoFront;

		// Token: 0x04005399 RID: 21401
		public static Asset<Texture2D> Logo;

		// Token: 0x0400539A RID: 21402
		public static Asset<Texture2D> Logo2;

		// Token: 0x0400539B RID: 21403
		public static Asset<Texture2D> Logo3;

		// Token: 0x0400539C RID: 21404
		public static Asset<Texture2D> Logo4;

		// Token: 0x0400539D RID: 21405
		public static Asset<Texture2D> TextBack;

		// Token: 0x0400539E RID: 21406
		public static Asset<Texture2D> Chat;

		// Token: 0x0400539F RID: 21407
		public static Asset<Texture2D> Chat2;

		// Token: 0x040053A0 RID: 21408
		public static Asset<Texture2D> ChatBack;

		// Token: 0x040053A1 RID: 21409
		public static Asset<Texture2D> Team;

		// Token: 0x040053A2 RID: 21410
		public static Asset<Texture2D> Re;

		// Token: 0x040053A3 RID: 21411
		public static Asset<Texture2D> Ra;

		// Token: 0x040053A4 RID: 21412
		public static Asset<Texture2D> Splash;

		// Token: 0x040053A5 RID: 21413
		public static Asset<Texture2D> Fade;

		// Token: 0x040053A6 RID: 21414
		public static Asset<Texture2D> Ninja;

		// Token: 0x040053A7 RID: 21415
		public static Asset<Texture2D> AntLion;

		// Token: 0x040053A8 RID: 21416
		public static Asset<Texture2D> SpikeBase;

		// Token: 0x040053A9 RID: 21417
		public static Asset<Texture2D> Ghost;

		// Token: 0x040053AA RID: 21418
		public static Asset<Texture2D> EvilCactus;

		// Token: 0x040053AB RID: 21419
		public static Asset<Texture2D> GoodCactus;

		// Token: 0x040053AC RID: 21420
		public static Asset<Texture2D> CrimsonCactus;

		// Token: 0x040053AD RID: 21421
		public static Asset<Texture2D> WraithEye;

		// Token: 0x040053AE RID: 21422
		public static Asset<Texture2D> Firefly;

		// Token: 0x040053AF RID: 21423
		public static Asset<Texture2D> FireflyJar;

		// Token: 0x040053B0 RID: 21424
		public static Asset<Texture2D> Lightningbug;

		// Token: 0x040053B1 RID: 21425
		public static Asset<Texture2D> LightningbugJar;

		// Token: 0x040053B2 RID: 21426
		public static Asset<Texture2D>[] JellyfishBowl = new Asset<Texture2D>[3];

		// Token: 0x040053B3 RID: 21427
		public static Asset<Texture2D> GlowSnail;

		// Token: 0x040053B4 RID: 21428
		public static Asset<Texture2D> IceQueen;

		// Token: 0x040053B5 RID: 21429
		public static Asset<Texture2D> SantaTank;

		// Token: 0x040053B6 RID: 21430
		public static Asset<Texture2D> ReaperEye;

		// Token: 0x040053B7 RID: 21431
		public static Asset<Texture2D> JackHat;

		// Token: 0x040053B8 RID: 21432
		public static Asset<Texture2D> TreeFace;

		// Token: 0x040053B9 RID: 21433
		public static Asset<Texture2D> PumpkingFace;

		// Token: 0x040053BA RID: 21434
		public static Asset<Texture2D> DukeFishron;

		// Token: 0x040053BB RID: 21435
		public static Asset<Texture2D> MiniMinotaur;

		// Token: 0x040053BC RID: 21436
		public static Asset<Texture2D>[,] Players;

		// Token: 0x040053BD RID: 21437
		public static Asset<Texture2D>[] PlayerHair = new Asset<Texture2D>[165];

		// Token: 0x040053BE RID: 21438
		public static Asset<Texture2D>[] PlayerHairAlt = new Asset<Texture2D>[165];

		// Token: 0x040053BF RID: 21439
		public static Asset<Texture2D> LoadingSunflower;

		// Token: 0x040053C0 RID: 21440
		public static Asset<Texture2D> GolfSwingBarPanel;

		// Token: 0x040053C1 RID: 21441
		public static Asset<Texture2D> GolfSwingBarFill;

		// Token: 0x040053C2 RID: 21442
		public static Asset<Texture2D> SpawnPoint;

		// Token: 0x040053C3 RID: 21443
		public static Asset<Texture2D> SpawnBed;

		// Token: 0x040053C4 RID: 21444
		public static Asset<Texture2D> GolfBallArrow;

		// Token: 0x040053C5 RID: 21445
		public static Asset<Texture2D> GolfBallArrowShadow;

		// Token: 0x040053C6 RID: 21446
		public static Asset<Texture2D> GolfBallOutline;

		// Token: 0x02000BC0 RID: 3008
		public static class RenderTargets
		{
			// Token: 0x04007708 RID: 30472
			public static PlayerRainbowWingsTextureContent PlayerRainbowWings;

			// Token: 0x04007709 RID: 30473
			public static PlayerTitaniumStormBuffTextureContent PlayerTitaniumStormBuff;

			// Token: 0x0400770A RID: 30474
			public static PlayerQueenSlimeMountTextureContent QueenSlimeMount;
		}
	}
}
