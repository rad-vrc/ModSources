using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using ReLogic.Reflection;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Terraria.ID
{
	// Token: 0x0200041B RID: 1051
	public class NPCID
	{
		// Token: 0x06003542 RID: 13634 RVA: 0x005707A0 File Offset: 0x0056E9A0
		public static int FromLegacyName(string name)
		{
			int value;
			if (NPCID.LegacyNameToIdMap.TryGetValue(name, out value))
			{
				return value;
			}
			return 0;
		}

		// Token: 0x06003543 RID: 13635 RVA: 0x005707BF File Offset: 0x0056E9BF
		public static int FromNetId(int id)
		{
			if (id < 0)
			{
				return NPCID.NetIdMap[-id - 1];
			}
			return id;
		}

		// Token: 0x04003FF2 RID: 16370
		private static readonly int[] NetIdMap = new int[]
		{
			81,
			81,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			6,
			6,
			31,
			31,
			77,
			42,
			42,
			176,
			176,
			176,
			176,
			173,
			173,
			183,
			183,
			3,
			3,
			132,
			132,
			186,
			186,
			187,
			187,
			188,
			188,
			189,
			189,
			190,
			191,
			192,
			193,
			194,
			2,
			200,
			200,
			21,
			21,
			201,
			201,
			202,
			202,
			203,
			203,
			223,
			223,
			231,
			231,
			232,
			232,
			233,
			233,
			234,
			234,
			235,
			235
		};

		// Token: 0x04003FF3 RID: 16371
		private static readonly Dictionary<string, int> LegacyNameToIdMap = new Dictionary<string, int>
		{
			{
				"Slimeling",
				-1
			},
			{
				"Slimer2",
				-2
			},
			{
				"Green Slime",
				-3
			},
			{
				"Pinky",
				-4
			},
			{
				"Baby Slime",
				-5
			},
			{
				"Black Slime",
				-6
			},
			{
				"Purple Slime",
				-7
			},
			{
				"Red Slime",
				-8
			},
			{
				"Yellow Slime",
				-9
			},
			{
				"Jungle Slime",
				-10
			},
			{
				"Little Eater",
				-11
			},
			{
				"Big Eater",
				-12
			},
			{
				"Short Bones",
				-13
			},
			{
				"Big Boned",
				-14
			},
			{
				"Heavy Skeleton",
				-15
			},
			{
				"Little Stinger",
				-16
			},
			{
				"Big Stinger",
				-17
			},
			{
				"Tiny Moss Hornet",
				-18
			},
			{
				"Little Moss Hornet",
				-19
			},
			{
				"Big Moss Hornet",
				-20
			},
			{
				"Giant Moss Hornet",
				-21
			},
			{
				"Little Crimera",
				-22
			},
			{
				"Big Crimera",
				-23
			},
			{
				"Little Crimslime",
				-24
			},
			{
				"Big Crimslime",
				-25
			},
			{
				"Small Zombie",
				-26
			},
			{
				"Big Zombie",
				-27
			},
			{
				"Small Bald Zombie",
				-28
			},
			{
				"Big Bald Zombie",
				-29
			},
			{
				"Small Pincushion Zombie",
				-30
			},
			{
				"Big Pincushion Zombie",
				-31
			},
			{
				"Small Slimed Zombie",
				-32
			},
			{
				"Big Slimed Zombie",
				-33
			},
			{
				"Small Swamp Zombie",
				-34
			},
			{
				"Big Swamp Zombie",
				-35
			},
			{
				"Small Twiggy Zombie",
				-36
			},
			{
				"Big Twiggy Zombie",
				-37
			},
			{
				"Cataract Eye 2",
				-38
			},
			{
				"Sleepy Eye 2",
				-39
			},
			{
				"Dialated Eye 2",
				-40
			},
			{
				"Green Eye 2",
				-41
			},
			{
				"Purple Eye 2",
				-42
			},
			{
				"Demon Eye 2",
				-43
			},
			{
				"Small Female Zombie",
				-44
			},
			{
				"Big Female Zombie",
				-45
			},
			{
				"Small Skeleton",
				-46
			},
			{
				"Big Skeleton",
				-47
			},
			{
				"Small Headache Skeleton",
				-48
			},
			{
				"Big Headache Skeleton",
				-49
			},
			{
				"Small Misassembled Skeleton",
				-50
			},
			{
				"Big Misassembled Skeleton",
				-51
			},
			{
				"Small Pantless Skeleton",
				-52
			},
			{
				"Big Pantless Skeleton",
				-53
			},
			{
				"Small Rain Zombie",
				-54
			},
			{
				"Big Rain Zombie",
				-55
			},
			{
				"Little Hornet Fatty",
				-56
			},
			{
				"Big Hornet Fatty",
				-57
			},
			{
				"Little Hornet Honey",
				-58
			},
			{
				"Big Hornet Honey",
				-59
			},
			{
				"Little Hornet Leafy",
				-60
			},
			{
				"Big Hornet Leafy",
				-61
			},
			{
				"Little Hornet Spikey",
				-62
			},
			{
				"Big Hornet Spikey",
				-63
			},
			{
				"Little Hornet Stingy",
				-64
			},
			{
				"Big Hornet Stingy",
				-65
			},
			{
				"Blue Slime",
				1
			},
			{
				"Demon Eye",
				2
			},
			{
				"Zombie",
				3
			},
			{
				"Eye of Cthulhu",
				4
			},
			{
				"Servant of Cthulhu",
				5
			},
			{
				"Eater of Souls",
				6
			},
			{
				"Devourer",
				7
			},
			{
				"Giant Worm",
				10
			},
			{
				"Eater of Worlds",
				13
			},
			{
				"Mother Slime",
				16
			},
			{
				"Merchant",
				17
			},
			{
				"Nurse",
				18
			},
			{
				"Arms Dealer",
				19
			},
			{
				"Dryad",
				20
			},
			{
				"Skeleton",
				21
			},
			{
				"Guide",
				22
			},
			{
				"Meteor Head",
				23
			},
			{
				"Fire Imp",
				24
			},
			{
				"Burning Sphere",
				25
			},
			{
				"Goblin Peon",
				26
			},
			{
				"Goblin Thief",
				27
			},
			{
				"Goblin Warrior",
				28
			},
			{
				"Goblin Sorcerer",
				29
			},
			{
				"Chaos Ball",
				30
			},
			{
				"Angry Bones",
				31
			},
			{
				"Dark Caster",
				32
			},
			{
				"Water Sphere",
				33
			},
			{
				"Cursed Skull",
				34
			},
			{
				"Skeletron",
				35
			},
			{
				"Old Man",
				37
			},
			{
				"Demolitionist",
				38
			},
			{
				"Bone Serpent",
				39
			},
			{
				"Hornet",
				42
			},
			{
				"Man Eater",
				43
			},
			{
				"Undead Miner",
				44
			},
			{
				"Tim",
				45
			},
			{
				"Bunny",
				46
			},
			{
				"Corrupt Bunny",
				47
			},
			{
				"Harpy",
				48
			},
			{
				"Cave Bat",
				49
			},
			{
				"King Slime",
				50
			},
			{
				"Jungle Bat",
				51
			},
			{
				"Doctor Bones",
				52
			},
			{
				"The Groom",
				53
			},
			{
				"Clothier",
				54
			},
			{
				"Goldfish",
				55
			},
			{
				"Snatcher",
				56
			},
			{
				"Corrupt Goldfish",
				57
			},
			{
				"Piranha",
				58
			},
			{
				"Lava Slime",
				59
			},
			{
				"Hellbat",
				60
			},
			{
				"Vulture",
				61
			},
			{
				"Demon",
				62
			},
			{
				"Blue Jellyfish",
				63
			},
			{
				"Pink Jellyfish",
				64
			},
			{
				"Shark",
				65
			},
			{
				"Voodoo Demon",
				66
			},
			{
				"Crab",
				67
			},
			{
				"Dungeon Guardian",
				68
			},
			{
				"Antlion",
				69
			},
			{
				"Spike Ball",
				70
			},
			{
				"Dungeon Slime",
				71
			},
			{
				"Blazing Wheel",
				72
			},
			{
				"Goblin Scout",
				73
			},
			{
				"Bird",
				74
			},
			{
				"Pixie",
				75
			},
			{
				"Armored Skeleton",
				77
			},
			{
				"Mummy",
				78
			},
			{
				"Dark Mummy",
				79
			},
			{
				"Light Mummy",
				80
			},
			{
				"Corrupt Slime",
				81
			},
			{
				"Wraith",
				82
			},
			{
				"Cursed Hammer",
				83
			},
			{
				"Enchanted Sword",
				84
			},
			{
				"Mimic",
				85
			},
			{
				"Unicorn",
				86
			},
			{
				"Wyvern",
				87
			},
			{
				"Giant Bat",
				93
			},
			{
				"Corruptor",
				94
			},
			{
				"Digger",
				95
			},
			{
				"World Feeder",
				98
			},
			{
				"Clinger",
				101
			},
			{
				"Angler Fish",
				102
			},
			{
				"Green Jellyfish",
				103
			},
			{
				"Werewolf",
				104
			},
			{
				"Bound Goblin",
				105
			},
			{
				"Bound Wizard",
				106
			},
			{
				"Goblin Tinkerer",
				107
			},
			{
				"Wizard",
				108
			},
			{
				"Clown",
				109
			},
			{
				"Skeleton Archer",
				110
			},
			{
				"Goblin Archer",
				111
			},
			{
				"Vile Spit",
				112
			},
			{
				"Wall of Flesh",
				113
			},
			{
				"The Hungry",
				115
			},
			{
				"Leech",
				117
			},
			{
				"Chaos Elemental",
				120
			},
			{
				"Slimer",
				121
			},
			{
				"Gastropod",
				122
			},
			{
				"Bound Mechanic",
				123
			},
			{
				"Mechanic",
				124
			},
			{
				"Retinazer",
				125
			},
			{
				"Spazmatism",
				126
			},
			{
				"Skeletron Prime",
				127
			},
			{
				"Prime Cannon",
				128
			},
			{
				"Prime Saw",
				129
			},
			{
				"Prime Vice",
				130
			},
			{
				"Prime Laser",
				131
			},
			{
				"Wandering Eye",
				133
			},
			{
				"The Destroyer",
				134
			},
			{
				"Illuminant Bat",
				137
			},
			{
				"Illuminant Slime",
				138
			},
			{
				"Probe",
				139
			},
			{
				"Possessed Armor",
				140
			},
			{
				"Toxic Sludge",
				141
			},
			{
				"Santa Claus",
				142
			},
			{
				"Snowman Gangsta",
				143
			},
			{
				"Mister Stabby",
				144
			},
			{
				"Snow Balla",
				145
			},
			{
				"Ice Slime",
				147
			},
			{
				"Penguin",
				148
			},
			{
				"Ice Bat",
				150
			},
			{
				"Lava Bat",
				151
			},
			{
				"Giant Flying Fox",
				152
			},
			{
				"Giant Tortoise",
				153
			},
			{
				"Ice Tortoise",
				154
			},
			{
				"Wolf",
				155
			},
			{
				"Red Devil",
				156
			},
			{
				"Arapaima",
				157
			},
			{
				"Vampire",
				158
			},
			{
				"Truffle",
				160
			},
			{
				"Zombie Eskimo",
				161
			},
			{
				"Frankenstein",
				162
			},
			{
				"Black Recluse",
				163
			},
			{
				"Wall Creeper",
				164
			},
			{
				"Swamp Thing",
				166
			},
			{
				"Undead Viking",
				167
			},
			{
				"Corrupt Penguin",
				168
			},
			{
				"Ice Elemental",
				169
			},
			{
				"Pigron",
				170
			},
			{
				"Rune Wizard",
				172
			},
			{
				"Crimera",
				173
			},
			{
				"Herpling",
				174
			},
			{
				"Angry Trapper",
				175
			},
			{
				"Moss Hornet",
				176
			},
			{
				"Derpling",
				177
			},
			{
				"Steampunker",
				178
			},
			{
				"Crimson Axe",
				179
			},
			{
				"Face Monster",
				181
			},
			{
				"Floaty Gross",
				182
			},
			{
				"Crimslime",
				183
			},
			{
				"Spiked Ice Slime",
				184
			},
			{
				"Snow Flinx",
				185
			},
			{
				"Lost Girl",
				195
			},
			{
				"Nymph",
				196
			},
			{
				"Armored Viking",
				197
			},
			{
				"Lihzahrd",
				198
			},
			{
				"Spiked Jungle Slime",
				204
			},
			{
				"Moth",
				205
			},
			{
				"Icy Merman",
				206
			},
			{
				"Dye Trader",
				207
			},
			{
				"Party Girl",
				208
			},
			{
				"Cyborg",
				209
			},
			{
				"Bee",
				210
			},
			{
				"Pirate Deckhand",
				212
			},
			{
				"Pirate Corsair",
				213
			},
			{
				"Pirate Deadeye",
				214
			},
			{
				"Pirate Crossbower",
				215
			},
			{
				"Pirate Captain",
				216
			},
			{
				"Cochineal Beetle",
				217
			},
			{
				"Cyan Beetle",
				218
			},
			{
				"Lac Beetle",
				219
			},
			{
				"Sea Snail",
				220
			},
			{
				"Squid",
				221
			},
			{
				"Queen Bee",
				222
			},
			{
				"Raincoat Zombie",
				223
			},
			{
				"Flying Fish",
				224
			},
			{
				"Umbrella Slime",
				225
			},
			{
				"Flying Snake",
				226
			},
			{
				"Painter",
				227
			},
			{
				"Witch Doctor",
				228
			},
			{
				"Pirate",
				229
			},
			{
				"Jungle Creeper",
				236
			},
			{
				"Blood Crawler",
				239
			},
			{
				"Blood Feeder",
				241
			},
			{
				"Blood Jelly",
				242
			},
			{
				"Ice Golem",
				243
			},
			{
				"Rainbow Slime",
				244
			},
			{
				"Golem",
				245
			},
			{
				"Golem Head",
				246
			},
			{
				"Golem Fist",
				247
			},
			{
				"Angry Nimbus",
				250
			},
			{
				"Eyezor",
				251
			},
			{
				"Parrot",
				252
			},
			{
				"Reaper",
				253
			},
			{
				"Spore Zombie",
				254
			},
			{
				"Fungo Fish",
				256
			},
			{
				"Anomura Fungus",
				257
			},
			{
				"Mushi Ladybug",
				258
			},
			{
				"Fungi Bulb",
				259
			},
			{
				"Giant Fungi Bulb",
				260
			},
			{
				"Fungi Spore",
				261
			},
			{
				"Plantera",
				262
			},
			{
				"Plantera's Hook",
				263
			},
			{
				"Plantera's Tentacle",
				264
			},
			{
				"Spore",
				265
			},
			{
				"Brain of Cthulhu",
				266
			},
			{
				"Creeper",
				267
			},
			{
				"Ichor Sticker",
				268
			},
			{
				"Rusty Armored Bones",
				269
			},
			{
				"Blue Armored Bones",
				273
			},
			{
				"Hell Armored Bones",
				277
			},
			{
				"Ragged Caster",
				281
			},
			{
				"Necromancer",
				283
			},
			{
				"Diabolist",
				285
			},
			{
				"Bone Lee",
				287
			},
			{
				"Dungeon Spirit",
				288
			},
			{
				"Giant Cursed Skull",
				289
			},
			{
				"Paladin",
				290
			},
			{
				"Skeleton Sniper",
				291
			},
			{
				"Tactical Skeleton",
				292
			},
			{
				"Skeleton Commando",
				293
			},
			{
				"Blue Jay",
				297
			},
			{
				"Cardinal",
				298
			},
			{
				"Squirrel",
				299
			},
			{
				"Mouse",
				300
			},
			{
				"Raven",
				301
			},
			{
				"Slime",
				302
			},
			{
				"Hoppin' Jack",
				304
			},
			{
				"Scarecrow",
				305
			},
			{
				"Headless Horseman",
				315
			},
			{
				"Ghost",
				316
			},
			{
				"Mourning Wood",
				325
			},
			{
				"Splinterling",
				326
			},
			{
				"Pumpking",
				327
			},
			{
				"Hellhound",
				329
			},
			{
				"Poltergeist",
				330
			},
			{
				"Zombie Elf",
				338
			},
			{
				"Present Mimic",
				341
			},
			{
				"Gingerbread Man",
				342
			},
			{
				"Yeti",
				343
			},
			{
				"Everscream",
				344
			},
			{
				"Ice Queen",
				345
			},
			{
				"Santa",
				346
			},
			{
				"Elf Copter",
				347
			},
			{
				"Nutcracker",
				348
			},
			{
				"Elf Archer",
				350
			},
			{
				"Krampus",
				351
			},
			{
				"Flocko",
				352
			},
			{
				"Stylist",
				353
			},
			{
				"Webbed Stylist",
				354
			},
			{
				"Firefly",
				355
			},
			{
				"Butterfly",
				356
			},
			{
				"Worm",
				357
			},
			{
				"Lightning Bug",
				358
			},
			{
				"Snail",
				359
			},
			{
				"Glowing Snail",
				360
			},
			{
				"Frog",
				361
			},
			{
				"Duck",
				362
			},
			{
				"Scorpion",
				366
			},
			{
				"Traveling Merchant",
				368
			},
			{
				"Angler",
				369
			},
			{
				"Duke Fishron",
				370
			},
			{
				"Detonating Bubble",
				371
			},
			{
				"Sharkron",
				372
			},
			{
				"Truffle Worm",
				374
			},
			{
				"Sleeping Angler",
				376
			},
			{
				"Grasshopper",
				377
			},
			{
				"Chattering Teeth Bomb",
				378
			},
			{
				"Blue Cultist Archer",
				379
			},
			{
				"White Cultist Archer",
				380
			},
			{
				"Brain Scrambler",
				381
			},
			{
				"Ray Gunner",
				382
			},
			{
				"Martian Officer",
				383
			},
			{
				"Bubble Shield",
				384
			},
			{
				"Gray Grunt",
				385
			},
			{
				"Martian Engineer",
				386
			},
			{
				"Tesla Turret",
				387
			},
			{
				"Martian Drone",
				388
			},
			{
				"Gigazapper",
				389
			},
			{
				"Scutlix Gunner",
				390
			},
			{
				"Scutlix",
				391
			},
			{
				"Martian Saucer",
				392
			},
			{
				"Martian Saucer Turret",
				393
			},
			{
				"Martian Saucer Cannon",
				394
			},
			{
				"Moon Lord",
				396
			},
			{
				"Moon Lord's Hand",
				397
			},
			{
				"Moon Lord's Core",
				398
			},
			{
				"Martian Probe",
				399
			},
			{
				"Milkyway Weaver",
				402
			},
			{
				"Star Cell",
				405
			},
			{
				"Flow Invader",
				407
			},
			{
				"Twinkle Popper",
				409
			},
			{
				"Twinkle",
				410
			},
			{
				"Stargazer",
				411
			},
			{
				"Crawltipede",
				412
			},
			{
				"Drakomire",
				415
			},
			{
				"Drakomire Rider",
				416
			},
			{
				"Sroller",
				417
			},
			{
				"Corite",
				418
			},
			{
				"Selenian",
				419
			},
			{
				"Nebula Floater",
				420
			},
			{
				"Brain Suckler",
				421
			},
			{
				"Vortex Pillar",
				422
			},
			{
				"Evolution Beast",
				423
			},
			{
				"Predictor",
				424
			},
			{
				"Storm Diver",
				425
			},
			{
				"Alien Queen",
				426
			},
			{
				"Alien Hornet",
				427
			},
			{
				"Alien Larva",
				428
			},
			{
				"Vortexian",
				429
			},
			{
				"Mysterious Tablet",
				437
			},
			{
				"Lunatic Devote",
				438
			},
			{
				"Lunatic Cultist",
				439
			},
			{
				"Tax Collector",
				441
			},
			{
				"Gold Bird",
				442
			},
			{
				"Gold Bunny",
				443
			},
			{
				"Gold Butterfly",
				444
			},
			{
				"Gold Frog",
				445
			},
			{
				"Gold Grasshopper",
				446
			},
			{
				"Gold Mouse",
				447
			},
			{
				"Gold Worm",
				448
			},
			{
				"Phantasm Dragon",
				454
			},
			{
				"Butcher",
				460
			},
			{
				"Creature from the Deep",
				461
			},
			{
				"Fritz",
				462
			},
			{
				"Nailhead",
				463
			},
			{
				"Crimtane Bunny",
				464
			},
			{
				"Crimtane Goldfish",
				465
			},
			{
				"Psycho",
				466
			},
			{
				"Deadly Sphere",
				467
			},
			{
				"Dr. Man Fly",
				468
			},
			{
				"The Possessed",
				469
			},
			{
				"Vicious Penguin",
				470
			},
			{
				"Goblin Summoner",
				471
			},
			{
				"Shadowflame Apparation",
				472
			},
			{
				"Corrupt Mimic",
				473
			},
			{
				"Crimson Mimic",
				474
			},
			{
				"Hallowed Mimic",
				475
			},
			{
				"Jungle Mimic",
				476
			},
			{
				"Mothron",
				477
			},
			{
				"Mothron Egg",
				478
			},
			{
				"Baby Mothron",
				479
			},
			{
				"Medusa",
				480
			},
			{
				"Hoplite",
				481
			},
			{
				"Granite Golem",
				482
			},
			{
				"Granite Elemental",
				483
			},
			{
				"Enchanted Nightcrawler",
				484
			},
			{
				"Grubby",
				485
			},
			{
				"Sluggy",
				486
			},
			{
				"Buggy",
				487
			},
			{
				"Target Dummy",
				488
			},
			{
				"Blood Zombie",
				489
			},
			{
				"Drippler",
				490
			},
			{
				"Stardust Pillar",
				493
			},
			{
				"Crawdad",
				494
			},
			{
				"Giant Shelly",
				496
			},
			{
				"Salamander",
				498
			},
			{
				"Nebula Pillar",
				507
			},
			{
				"Antlion Charger",
				508
			},
			{
				"Antlion Swarmer",
				509
			},
			{
				"Dune Splicer",
				510
			},
			{
				"Tomb Crawler",
				513
			},
			{
				"Solar Flare",
				516
			},
			{
				"Solar Pillar",
				517
			},
			{
				"Drakanian",
				518
			},
			{
				"Solar Fragment",
				519
			},
			{
				"Martian Walker",
				520
			},
			{
				"Ancient Vision",
				521
			},
			{
				"Ancient Light",
				522
			},
			{
				"Ancient Doom",
				523
			},
			{
				"Ghoul",
				524
			},
			{
				"Vile Ghoul",
				525
			},
			{
				"Tainted Ghoul",
				526
			},
			{
				"Dreamer Ghoul",
				527
			},
			{
				"Lamia",
				528
			},
			{
				"Sand Poacher",
				530
			},
			{
				"Basilisk",
				532
			},
			{
				"Desert Spirit",
				533
			},
			{
				"Tortured Soul",
				534
			},
			{
				"Spiked Slime",
				535
			},
			{
				"The Bride",
				536
			},
			{
				"Sand Slime",
				537
			},
			{
				"Red Squirrel",
				538
			},
			{
				"Gold Squirrel",
				539
			},
			{
				"Sand Elemental",
				541
			},
			{
				"Sand Shark",
				542
			},
			{
				"Bone Biter",
				543
			},
			{
				"Flesh Reaver",
				544
			},
			{
				"Crystal Thresher",
				545
			},
			{
				"Angry Tumbler",
				546
			},
			{
				"???",
				547
			},
			{
				"Eternia Crystal",
				548
			},
			{
				"Mysterious Portal",
				549
			},
			{
				"Tavernkeep",
				550
			},
			{
				"Betsy",
				551
			},
			{
				"Etherian Goblin",
				552
			},
			{
				"Etherian Goblin Bomber",
				555
			},
			{
				"Etherian Wyvern",
				558
			},
			{
				"Etherian Javelin Thrower",
				561
			},
			{
				"Dark Mage",
				564
			},
			{
				"Old One's Skeleton",
				566
			},
			{
				"Wither Beast",
				568
			},
			{
				"Drakin",
				570
			},
			{
				"Kobold",
				572
			},
			{
				"Kobold Glider",
				574
			},
			{
				"Ogre",
				576
			},
			{
				"Etherian Lightning Bug",
				578
			}
		};

		// Token: 0x04003FF4 RID: 16372
		public const short NegativeIDCount = -66;

		// Token: 0x04003FF5 RID: 16373
		public const short BigHornetStingy = -65;

		// Token: 0x04003FF6 RID: 16374
		public const short LittleHornetStingy = -64;

		// Token: 0x04003FF7 RID: 16375
		public const short BigHornetSpikey = -63;

		// Token: 0x04003FF8 RID: 16376
		public const short LittleHornetSpikey = -62;

		// Token: 0x04003FF9 RID: 16377
		public const short BigHornetLeafy = -61;

		// Token: 0x04003FFA RID: 16378
		public const short LittleHornetLeafy = -60;

		// Token: 0x04003FFB RID: 16379
		public const short BigHornetHoney = -59;

		// Token: 0x04003FFC RID: 16380
		public const short LittleHornetHoney = -58;

		// Token: 0x04003FFD RID: 16381
		public const short BigHornetFatty = -57;

		// Token: 0x04003FFE RID: 16382
		public const short LittleHornetFatty = -56;

		// Token: 0x04003FFF RID: 16383
		public const short BigRainZombie = -55;

		// Token: 0x04004000 RID: 16384
		public const short SmallRainZombie = -54;

		// Token: 0x04004001 RID: 16385
		public const short BigPantlessSkeleton = -53;

		// Token: 0x04004002 RID: 16386
		public const short SmallPantlessSkeleton = -52;

		// Token: 0x04004003 RID: 16387
		public const short BigMisassembledSkeleton = -51;

		// Token: 0x04004004 RID: 16388
		public const short SmallMisassembledSkeleton = -50;

		// Token: 0x04004005 RID: 16389
		public const short BigHeadacheSkeleton = -49;

		// Token: 0x04004006 RID: 16390
		public const short SmallHeadacheSkeleton = -48;

		// Token: 0x04004007 RID: 16391
		public const short BigSkeleton = -47;

		// Token: 0x04004008 RID: 16392
		public const short SmallSkeleton = -46;

		// Token: 0x04004009 RID: 16393
		public const short BigFemaleZombie = -45;

		// Token: 0x0400400A RID: 16394
		public const short SmallFemaleZombie = -44;

		// Token: 0x0400400B RID: 16395
		public const short DemonEye2 = -43;

		// Token: 0x0400400C RID: 16396
		public const short PurpleEye2 = -42;

		// Token: 0x0400400D RID: 16397
		public const short GreenEye2 = -41;

		// Token: 0x0400400E RID: 16398
		public const short DialatedEye2 = -40;

		// Token: 0x0400400F RID: 16399
		public const short SleepyEye2 = -39;

		// Token: 0x04004010 RID: 16400
		public const short CataractEye2 = -38;

		// Token: 0x04004011 RID: 16401
		public const short BigTwiggyZombie = -37;

		// Token: 0x04004012 RID: 16402
		public const short SmallTwiggyZombie = -36;

		// Token: 0x04004013 RID: 16403
		public const short BigSwampZombie = -35;

		// Token: 0x04004014 RID: 16404
		public const short SmallSwampZombie = -34;

		// Token: 0x04004015 RID: 16405
		public const short BigSlimedZombie = -33;

		// Token: 0x04004016 RID: 16406
		public const short SmallSlimedZombie = -32;

		// Token: 0x04004017 RID: 16407
		public const short BigPincushionZombie = -31;

		// Token: 0x04004018 RID: 16408
		public const short SmallPincushionZombie = -30;

		// Token: 0x04004019 RID: 16409
		public const short BigBaldZombie = -29;

		// Token: 0x0400401A RID: 16410
		public const short SmallBaldZombie = -28;

		// Token: 0x0400401B RID: 16411
		public const short BigZombie = -27;

		// Token: 0x0400401C RID: 16412
		public const short SmallZombie = -26;

		// Token: 0x0400401D RID: 16413
		public const short BigCrimslime = -25;

		// Token: 0x0400401E RID: 16414
		public const short LittleCrimslime = -24;

		// Token: 0x0400401F RID: 16415
		public const short BigCrimera = -23;

		// Token: 0x04004020 RID: 16416
		public const short LittleCrimera = -22;

		// Token: 0x04004021 RID: 16417
		public const short GiantMossHornet = -21;

		// Token: 0x04004022 RID: 16418
		public const short BigMossHornet = -20;

		// Token: 0x04004023 RID: 16419
		public const short LittleMossHornet = -19;

		// Token: 0x04004024 RID: 16420
		public const short TinyMossHornet = -18;

		// Token: 0x04004025 RID: 16421
		public const short BigStinger = -17;

		// Token: 0x04004026 RID: 16422
		public const short LittleStinger = -16;

		// Token: 0x04004027 RID: 16423
		public const short HeavySkeleton = -15;

		// Token: 0x04004028 RID: 16424
		public const short BigBoned = -14;

		// Token: 0x04004029 RID: 16425
		public const short ShortBones = -13;

		// Token: 0x0400402A RID: 16426
		public const short BigEater = -12;

		// Token: 0x0400402B RID: 16427
		public const short LittleEater = -11;

		// Token: 0x0400402C RID: 16428
		public const short JungleSlime = -10;

		// Token: 0x0400402D RID: 16429
		public const short YellowSlime = -9;

		// Token: 0x0400402E RID: 16430
		public const short RedSlime = -8;

		// Token: 0x0400402F RID: 16431
		public const short PurpleSlime = -7;

		// Token: 0x04004030 RID: 16432
		public const short BlackSlime = -6;

		// Token: 0x04004031 RID: 16433
		public const short BabySlime = -5;

		// Token: 0x04004032 RID: 16434
		public const short Pinky = -4;

		// Token: 0x04004033 RID: 16435
		public const short GreenSlime = -3;

		// Token: 0x04004034 RID: 16436
		public const short Slimer2 = -2;

		// Token: 0x04004035 RID: 16437
		public const short Slimeling = -1;

		// Token: 0x04004036 RID: 16438
		public const short None = 0;

		// Token: 0x04004037 RID: 16439
		public const short BlueSlime = 1;

		// Token: 0x04004038 RID: 16440
		public const short DemonEye = 2;

		// Token: 0x04004039 RID: 16441
		public const short Zombie = 3;

		// Token: 0x0400403A RID: 16442
		public const short EyeofCthulhu = 4;

		// Token: 0x0400403B RID: 16443
		public const short ServantofCthulhu = 5;

		// Token: 0x0400403C RID: 16444
		public const short EaterofSouls = 6;

		// Token: 0x0400403D RID: 16445
		public const short DevourerHead = 7;

		// Token: 0x0400403E RID: 16446
		public const short DevourerBody = 8;

		// Token: 0x0400403F RID: 16447
		public const short DevourerTail = 9;

		// Token: 0x04004040 RID: 16448
		public const short GiantWormHead = 10;

		// Token: 0x04004041 RID: 16449
		public const short GiantWormBody = 11;

		// Token: 0x04004042 RID: 16450
		public const short GiantWormTail = 12;

		// Token: 0x04004043 RID: 16451
		public const short EaterofWorldsHead = 13;

		// Token: 0x04004044 RID: 16452
		public const short EaterofWorldsBody = 14;

		// Token: 0x04004045 RID: 16453
		public const short EaterofWorldsTail = 15;

		// Token: 0x04004046 RID: 16454
		public const short MotherSlime = 16;

		// Token: 0x04004047 RID: 16455
		public const short Merchant = 17;

		// Token: 0x04004048 RID: 16456
		public const short Nurse = 18;

		// Token: 0x04004049 RID: 16457
		public const short ArmsDealer = 19;

		// Token: 0x0400404A RID: 16458
		public const short Dryad = 20;

		// Token: 0x0400404B RID: 16459
		public const short Skeleton = 21;

		// Token: 0x0400404C RID: 16460
		public const short Guide = 22;

		// Token: 0x0400404D RID: 16461
		public const short MeteorHead = 23;

		// Token: 0x0400404E RID: 16462
		public const short FireImp = 24;

		// Token: 0x0400404F RID: 16463
		public const short BurningSphere = 25;

		// Token: 0x04004050 RID: 16464
		public const short GoblinPeon = 26;

		// Token: 0x04004051 RID: 16465
		public const short GoblinThief = 27;

		// Token: 0x04004052 RID: 16466
		public const short GoblinWarrior = 28;

		// Token: 0x04004053 RID: 16467
		public const short GoblinSorcerer = 29;

		// Token: 0x04004054 RID: 16468
		public const short ChaosBall = 30;

		// Token: 0x04004055 RID: 16469
		public const short AngryBones = 31;

		// Token: 0x04004056 RID: 16470
		public const short DarkCaster = 32;

		// Token: 0x04004057 RID: 16471
		public const short WaterSphere = 33;

		// Token: 0x04004058 RID: 16472
		public const short CursedSkull = 34;

		// Token: 0x04004059 RID: 16473
		public const short SkeletronHead = 35;

		// Token: 0x0400405A RID: 16474
		public const short SkeletronHand = 36;

		// Token: 0x0400405B RID: 16475
		public const short OldMan = 37;

		// Token: 0x0400405C RID: 16476
		public const short Demolitionist = 38;

		// Token: 0x0400405D RID: 16477
		public const short BoneSerpentHead = 39;

		// Token: 0x0400405E RID: 16478
		public const short BoneSerpentBody = 40;

		// Token: 0x0400405F RID: 16479
		public const short BoneSerpentTail = 41;

		// Token: 0x04004060 RID: 16480
		public const short Hornet = 42;

		// Token: 0x04004061 RID: 16481
		public const short ManEater = 43;

		// Token: 0x04004062 RID: 16482
		public const short UndeadMiner = 44;

		// Token: 0x04004063 RID: 16483
		public const short Tim = 45;

		// Token: 0x04004064 RID: 16484
		public const short Bunny = 46;

		// Token: 0x04004065 RID: 16485
		public const short CorruptBunny = 47;

		// Token: 0x04004066 RID: 16486
		public const short Harpy = 48;

		// Token: 0x04004067 RID: 16487
		public const short CaveBat = 49;

		// Token: 0x04004068 RID: 16488
		public const short KingSlime = 50;

		// Token: 0x04004069 RID: 16489
		public const short JungleBat = 51;

		// Token: 0x0400406A RID: 16490
		public const short DoctorBones = 52;

		// Token: 0x0400406B RID: 16491
		public const short TheGroom = 53;

		// Token: 0x0400406C RID: 16492
		public const short Clothier = 54;

		// Token: 0x0400406D RID: 16493
		public const short Goldfish = 55;

		// Token: 0x0400406E RID: 16494
		public const short Snatcher = 56;

		// Token: 0x0400406F RID: 16495
		public const short CorruptGoldfish = 57;

		// Token: 0x04004070 RID: 16496
		public const short Piranha = 58;

		// Token: 0x04004071 RID: 16497
		public const short LavaSlime = 59;

		// Token: 0x04004072 RID: 16498
		public const short Hellbat = 60;

		// Token: 0x04004073 RID: 16499
		public const short Vulture = 61;

		// Token: 0x04004074 RID: 16500
		public const short Demon = 62;

		// Token: 0x04004075 RID: 16501
		public const short BlueJellyfish = 63;

		// Token: 0x04004076 RID: 16502
		public const short PinkJellyfish = 64;

		// Token: 0x04004077 RID: 16503
		public const short Shark = 65;

		// Token: 0x04004078 RID: 16504
		public const short VoodooDemon = 66;

		// Token: 0x04004079 RID: 16505
		public const short Crab = 67;

		// Token: 0x0400407A RID: 16506
		public const short DungeonGuardian = 68;

		// Token: 0x0400407B RID: 16507
		public const short Antlion = 69;

		// Token: 0x0400407C RID: 16508
		public const short SpikeBall = 70;

		// Token: 0x0400407D RID: 16509
		public const short DungeonSlime = 71;

		// Token: 0x0400407E RID: 16510
		public const short BlazingWheel = 72;

		// Token: 0x0400407F RID: 16511
		public const short GoblinScout = 73;

		// Token: 0x04004080 RID: 16512
		public const short Bird = 74;

		// Token: 0x04004081 RID: 16513
		public const short Pixie = 75;

		// Token: 0x04004082 RID: 16514
		public const short None2 = 76;

		// Token: 0x04004083 RID: 16515
		public const short ArmoredSkeleton = 77;

		// Token: 0x04004084 RID: 16516
		public const short Mummy = 78;

		// Token: 0x04004085 RID: 16517
		public const short DarkMummy = 79;

		// Token: 0x04004086 RID: 16518
		public const short LightMummy = 80;

		// Token: 0x04004087 RID: 16519
		public const short CorruptSlime = 81;

		// Token: 0x04004088 RID: 16520
		public const short Wraith = 82;

		// Token: 0x04004089 RID: 16521
		public const short CursedHammer = 83;

		// Token: 0x0400408A RID: 16522
		public const short EnchantedSword = 84;

		// Token: 0x0400408B RID: 16523
		public const short Mimic = 85;

		// Token: 0x0400408C RID: 16524
		public const short Unicorn = 86;

		// Token: 0x0400408D RID: 16525
		public const short WyvernHead = 87;

		// Token: 0x0400408E RID: 16526
		public const short WyvernLegs = 88;

		// Token: 0x0400408F RID: 16527
		public const short WyvernBody = 89;

		// Token: 0x04004090 RID: 16528
		public const short WyvernBody2 = 90;

		// Token: 0x04004091 RID: 16529
		public const short WyvernBody3 = 91;

		// Token: 0x04004092 RID: 16530
		public const short WyvernTail = 92;

		// Token: 0x04004093 RID: 16531
		public const short GiantBat = 93;

		// Token: 0x04004094 RID: 16532
		public const short Corruptor = 94;

		// Token: 0x04004095 RID: 16533
		public const short DiggerHead = 95;

		// Token: 0x04004096 RID: 16534
		public const short DiggerBody = 96;

		// Token: 0x04004097 RID: 16535
		public const short DiggerTail = 97;

		// Token: 0x04004098 RID: 16536
		public const short SeekerHead = 98;

		// Token: 0x04004099 RID: 16537
		public const short SeekerBody = 99;

		// Token: 0x0400409A RID: 16538
		public const short SeekerTail = 100;

		// Token: 0x0400409B RID: 16539
		public const short Clinger = 101;

		// Token: 0x0400409C RID: 16540
		public const short AnglerFish = 102;

		// Token: 0x0400409D RID: 16541
		public const short GreenJellyfish = 103;

		// Token: 0x0400409E RID: 16542
		public const short Werewolf = 104;

		// Token: 0x0400409F RID: 16543
		public const short BoundGoblin = 105;

		// Token: 0x040040A0 RID: 16544
		public const short BoundWizard = 106;

		// Token: 0x040040A1 RID: 16545
		public const short GoblinTinkerer = 107;

		// Token: 0x040040A2 RID: 16546
		public const short Wizard = 108;

		// Token: 0x040040A3 RID: 16547
		public const short Clown = 109;

		// Token: 0x040040A4 RID: 16548
		public const short SkeletonArcher = 110;

		// Token: 0x040040A5 RID: 16549
		public const short GoblinArcher = 111;

		// Token: 0x040040A6 RID: 16550
		public const short VileSpit = 112;

		// Token: 0x040040A7 RID: 16551
		public const short WallofFlesh = 113;

		// Token: 0x040040A8 RID: 16552
		public const short WallofFleshEye = 114;

		// Token: 0x040040A9 RID: 16553
		public const short TheHungry = 115;

		// Token: 0x040040AA RID: 16554
		public const short TheHungryII = 116;

		// Token: 0x040040AB RID: 16555
		public const short LeechHead = 117;

		// Token: 0x040040AC RID: 16556
		public const short LeechBody = 118;

		// Token: 0x040040AD RID: 16557
		public const short LeechTail = 119;

		// Token: 0x040040AE RID: 16558
		public const short ChaosElemental = 120;

		// Token: 0x040040AF RID: 16559
		public const short Slimer = 121;

		// Token: 0x040040B0 RID: 16560
		public const short Gastropod = 122;

		// Token: 0x040040B1 RID: 16561
		public const short BoundMechanic = 123;

		// Token: 0x040040B2 RID: 16562
		public const short Mechanic = 124;

		// Token: 0x040040B3 RID: 16563
		public const short Retinazer = 125;

		// Token: 0x040040B4 RID: 16564
		public const short Spazmatism = 126;

		// Token: 0x040040B5 RID: 16565
		public const short SkeletronPrime = 127;

		// Token: 0x040040B6 RID: 16566
		public const short PrimeCannon = 128;

		// Token: 0x040040B7 RID: 16567
		public const short PrimeSaw = 129;

		// Token: 0x040040B8 RID: 16568
		public const short PrimeVice = 130;

		// Token: 0x040040B9 RID: 16569
		public const short PrimeLaser = 131;

		// Token: 0x040040BA RID: 16570
		public const short BaldZombie = 132;

		// Token: 0x040040BB RID: 16571
		public const short WanderingEye = 133;

		// Token: 0x040040BC RID: 16572
		public const short TheDestroyer = 134;

		// Token: 0x040040BD RID: 16573
		public const short TheDestroyerBody = 135;

		// Token: 0x040040BE RID: 16574
		public const short TheDestroyerTail = 136;

		// Token: 0x040040BF RID: 16575
		public const short IlluminantBat = 137;

		// Token: 0x040040C0 RID: 16576
		public const short IlluminantSlime = 138;

		// Token: 0x040040C1 RID: 16577
		public const short Probe = 139;

		// Token: 0x040040C2 RID: 16578
		public const short PossessedArmor = 140;

		// Token: 0x040040C3 RID: 16579
		public const short ToxicSludge = 141;

		// Token: 0x040040C4 RID: 16580
		public const short SantaClaus = 142;

		// Token: 0x040040C5 RID: 16581
		public const short SnowmanGangsta = 143;

		// Token: 0x040040C6 RID: 16582
		public const short MisterStabby = 144;

		// Token: 0x040040C7 RID: 16583
		public const short SnowBalla = 145;

		// Token: 0x040040C8 RID: 16584
		public const short None3 = 146;

		// Token: 0x040040C9 RID: 16585
		public const short IceSlime = 147;

		// Token: 0x040040CA RID: 16586
		public const short Penguin = 148;

		// Token: 0x040040CB RID: 16587
		public const short PenguinBlack = 149;

		// Token: 0x040040CC RID: 16588
		public const short IceBat = 150;

		// Token: 0x040040CD RID: 16589
		public const short Lavabat = 151;

		// Token: 0x040040CE RID: 16590
		public const short GiantFlyingFox = 152;

		// Token: 0x040040CF RID: 16591
		public const short GiantTortoise = 153;

		// Token: 0x040040D0 RID: 16592
		public const short IceTortoise = 154;

		// Token: 0x040040D1 RID: 16593
		public const short Wolf = 155;

		// Token: 0x040040D2 RID: 16594
		public const short RedDevil = 156;

		// Token: 0x040040D3 RID: 16595
		public const short Arapaima = 157;

		// Token: 0x040040D4 RID: 16596
		public const short VampireBat = 158;

		// Token: 0x040040D5 RID: 16597
		public const short Vampire = 159;

		// Token: 0x040040D6 RID: 16598
		public const short Truffle = 160;

		// Token: 0x040040D7 RID: 16599
		public const short ZombieEskimo = 161;

		// Token: 0x040040D8 RID: 16600
		public const short Frankenstein = 162;

		// Token: 0x040040D9 RID: 16601
		public const short BlackRecluse = 163;

		// Token: 0x040040DA RID: 16602
		public const short WallCreeper = 164;

		// Token: 0x040040DB RID: 16603
		public const short WallCreeperWall = 165;

		// Token: 0x040040DC RID: 16604
		public const short SwampThing = 166;

		// Token: 0x040040DD RID: 16605
		public const short UndeadViking = 167;

		// Token: 0x040040DE RID: 16606
		public const short CorruptPenguin = 168;

		// Token: 0x040040DF RID: 16607
		public const short IceElemental = 169;

		// Token: 0x040040E0 RID: 16608
		public const short PigronCorruption = 170;

		// Token: 0x040040E1 RID: 16609
		public const short PigronHallow = 171;

		// Token: 0x040040E2 RID: 16610
		public const short RuneWizard = 172;

		// Token: 0x040040E3 RID: 16611
		public const short Crimera = 173;

		// Token: 0x040040E4 RID: 16612
		public const short Herpling = 174;

		// Token: 0x040040E5 RID: 16613
		public const short AngryTrapper = 175;

		// Token: 0x040040E6 RID: 16614
		public const short MossHornet = 176;

		// Token: 0x040040E7 RID: 16615
		public const short Derpling = 177;

		// Token: 0x040040E8 RID: 16616
		public const short Steampunker = 178;

		// Token: 0x040040E9 RID: 16617
		public const short CrimsonAxe = 179;

		// Token: 0x040040EA RID: 16618
		public const short PigronCrimson = 180;

		// Token: 0x040040EB RID: 16619
		public const short FaceMonster = 181;

		// Token: 0x040040EC RID: 16620
		public const short FloatyGross = 182;

		// Token: 0x040040ED RID: 16621
		public const short Crimslime = 183;

		// Token: 0x040040EE RID: 16622
		public const short SpikedIceSlime = 184;

		// Token: 0x040040EF RID: 16623
		public const short SnowFlinx = 185;

		// Token: 0x040040F0 RID: 16624
		public const short PincushionZombie = 186;

		// Token: 0x040040F1 RID: 16625
		public const short SlimedZombie = 187;

		// Token: 0x040040F2 RID: 16626
		public const short SwampZombie = 188;

		// Token: 0x040040F3 RID: 16627
		public const short TwiggyZombie = 189;

		// Token: 0x040040F4 RID: 16628
		public const short CataractEye = 190;

		// Token: 0x040040F5 RID: 16629
		public const short SleepyEye = 191;

		// Token: 0x040040F6 RID: 16630
		public const short DialatedEye = 192;

		// Token: 0x040040F7 RID: 16631
		public const short GreenEye = 193;

		// Token: 0x040040F8 RID: 16632
		public const short PurpleEye = 194;

		// Token: 0x040040F9 RID: 16633
		public const short LostGirl = 195;

		// Token: 0x040040FA RID: 16634
		public const short Nymph = 196;

		// Token: 0x040040FB RID: 16635
		public const short ArmoredViking = 197;

		// Token: 0x040040FC RID: 16636
		public const short Lihzahrd = 198;

		// Token: 0x040040FD RID: 16637
		public const short LihzahrdCrawler = 199;

		// Token: 0x040040FE RID: 16638
		public const short FemaleZombie = 200;

		// Token: 0x040040FF RID: 16639
		public const short HeadacheSkeleton = 201;

		// Token: 0x04004100 RID: 16640
		public const short MisassembledSkeleton = 202;

		// Token: 0x04004101 RID: 16641
		public const short PantlessSkeleton = 203;

		// Token: 0x04004102 RID: 16642
		public const short SpikedJungleSlime = 204;

		// Token: 0x04004103 RID: 16643
		public const short Moth = 205;

		// Token: 0x04004104 RID: 16644
		public const short IcyMerman = 206;

		// Token: 0x04004105 RID: 16645
		public const short DyeTrader = 207;

		// Token: 0x04004106 RID: 16646
		public const short PartyGirl = 208;

		// Token: 0x04004107 RID: 16647
		public const short Cyborg = 209;

		// Token: 0x04004108 RID: 16648
		public const short Bee = 210;

		// Token: 0x04004109 RID: 16649
		public const short BeeSmall = 211;

		// Token: 0x0400410A RID: 16650
		public const short PirateDeckhand = 212;

		// Token: 0x0400410B RID: 16651
		public const short PirateCorsair = 213;

		// Token: 0x0400410C RID: 16652
		public const short PirateDeadeye = 214;

		// Token: 0x0400410D RID: 16653
		public const short PirateCrossbower = 215;

		// Token: 0x0400410E RID: 16654
		public const short PirateCaptain = 216;

		// Token: 0x0400410F RID: 16655
		public const short CochinealBeetle = 217;

		// Token: 0x04004110 RID: 16656
		public const short CyanBeetle = 218;

		// Token: 0x04004111 RID: 16657
		public const short LacBeetle = 219;

		// Token: 0x04004112 RID: 16658
		public const short SeaSnail = 220;

		// Token: 0x04004113 RID: 16659
		public const short Squid = 221;

		// Token: 0x04004114 RID: 16660
		public const short QueenBee = 222;

		// Token: 0x04004115 RID: 16661
		public const short ZombieRaincoat = 223;

		// Token: 0x04004116 RID: 16662
		public const short FlyingFish = 224;

		// Token: 0x04004117 RID: 16663
		public const short UmbrellaSlime = 225;

		// Token: 0x04004118 RID: 16664
		public const short FlyingSnake = 226;

		// Token: 0x04004119 RID: 16665
		public const short Painter = 227;

		// Token: 0x0400411A RID: 16666
		public const short WitchDoctor = 228;

		// Token: 0x0400411B RID: 16667
		public const short Pirate = 229;

		// Token: 0x0400411C RID: 16668
		public const short GoldfishWalker = 230;

		// Token: 0x0400411D RID: 16669
		public const short HornetFatty = 231;

		// Token: 0x0400411E RID: 16670
		public const short HornetHoney = 232;

		// Token: 0x0400411F RID: 16671
		public const short HornetLeafy = 233;

		// Token: 0x04004120 RID: 16672
		public const short HornetSpikey = 234;

		// Token: 0x04004121 RID: 16673
		public const short HornetStingy = 235;

		// Token: 0x04004122 RID: 16674
		public const short JungleCreeper = 236;

		// Token: 0x04004123 RID: 16675
		public const short JungleCreeperWall = 237;

		// Token: 0x04004124 RID: 16676
		public const short BlackRecluseWall = 238;

		// Token: 0x04004125 RID: 16677
		public const short BloodCrawler = 239;

		// Token: 0x04004126 RID: 16678
		public const short BloodCrawlerWall = 240;

		// Token: 0x04004127 RID: 16679
		public const short BloodFeeder = 241;

		// Token: 0x04004128 RID: 16680
		public const short BloodJelly = 242;

		// Token: 0x04004129 RID: 16681
		public const short IceGolem = 243;

		// Token: 0x0400412A RID: 16682
		public const short RainbowSlime = 244;

		// Token: 0x0400412B RID: 16683
		public const short Golem = 245;

		// Token: 0x0400412C RID: 16684
		public const short GolemHead = 246;

		// Token: 0x0400412D RID: 16685
		public const short GolemFistLeft = 247;

		// Token: 0x0400412E RID: 16686
		public const short GolemFistRight = 248;

		// Token: 0x0400412F RID: 16687
		public const short GolemHeadFree = 249;

		// Token: 0x04004130 RID: 16688
		public const short AngryNimbus = 250;

		// Token: 0x04004131 RID: 16689
		public const short Eyezor = 251;

		// Token: 0x04004132 RID: 16690
		public const short Parrot = 252;

		// Token: 0x04004133 RID: 16691
		public const short Reaper = 253;

		// Token: 0x04004134 RID: 16692
		public const short ZombieMushroom = 254;

		// Token: 0x04004135 RID: 16693
		public const short ZombieMushroomHat = 255;

		// Token: 0x04004136 RID: 16694
		public const short FungoFish = 256;

		// Token: 0x04004137 RID: 16695
		public const short AnomuraFungus = 257;

		// Token: 0x04004138 RID: 16696
		public const short MushiLadybug = 258;

		// Token: 0x04004139 RID: 16697
		public const short FungiBulb = 259;

		// Token: 0x0400413A RID: 16698
		public const short GiantFungiBulb = 260;

		// Token: 0x0400413B RID: 16699
		public const short FungiSpore = 261;

		// Token: 0x0400413C RID: 16700
		public const short Plantera = 262;

		// Token: 0x0400413D RID: 16701
		public const short PlanterasHook = 263;

		// Token: 0x0400413E RID: 16702
		public const short PlanterasTentacle = 264;

		// Token: 0x0400413F RID: 16703
		public const short Spore = 265;

		// Token: 0x04004140 RID: 16704
		public const short BrainofCthulhu = 266;

		// Token: 0x04004141 RID: 16705
		public const short Creeper = 267;

		// Token: 0x04004142 RID: 16706
		public const short IchorSticker = 268;

		// Token: 0x04004143 RID: 16707
		public const short RustyArmoredBonesAxe = 269;

		// Token: 0x04004144 RID: 16708
		public const short RustyArmoredBonesFlail = 270;

		// Token: 0x04004145 RID: 16709
		public const short RustyArmoredBonesSword = 271;

		// Token: 0x04004146 RID: 16710
		public const short RustyArmoredBonesSwordNoArmor = 272;

		// Token: 0x04004147 RID: 16711
		public const short BlueArmoredBones = 273;

		// Token: 0x04004148 RID: 16712
		public const short BlueArmoredBonesMace = 274;

		// Token: 0x04004149 RID: 16713
		public const short BlueArmoredBonesNoPants = 275;

		// Token: 0x0400414A RID: 16714
		public const short BlueArmoredBonesSword = 276;

		// Token: 0x0400414B RID: 16715
		public const short HellArmoredBones = 277;

		// Token: 0x0400414C RID: 16716
		public const short HellArmoredBonesSpikeShield = 278;

		// Token: 0x0400414D RID: 16717
		public const short HellArmoredBonesMace = 279;

		// Token: 0x0400414E RID: 16718
		public const short HellArmoredBonesSword = 280;

		// Token: 0x0400414F RID: 16719
		public const short RaggedCaster = 281;

		// Token: 0x04004150 RID: 16720
		public const short RaggedCasterOpenCoat = 282;

		// Token: 0x04004151 RID: 16721
		public const short Necromancer = 283;

		// Token: 0x04004152 RID: 16722
		public const short NecromancerArmored = 284;

		// Token: 0x04004153 RID: 16723
		public const short DiabolistRed = 285;

		// Token: 0x04004154 RID: 16724
		public const short DiabolistWhite = 286;

		// Token: 0x04004155 RID: 16725
		public const short BoneLee = 287;

		// Token: 0x04004156 RID: 16726
		public const short DungeonSpirit = 288;

		// Token: 0x04004157 RID: 16727
		public const short GiantCursedSkull = 289;

		// Token: 0x04004158 RID: 16728
		public const short Paladin = 290;

		// Token: 0x04004159 RID: 16729
		public const short SkeletonSniper = 291;

		// Token: 0x0400415A RID: 16730
		public const short TacticalSkeleton = 292;

		// Token: 0x0400415B RID: 16731
		public const short SkeletonCommando = 293;

		// Token: 0x0400415C RID: 16732
		public const short AngryBonesBig = 294;

		// Token: 0x0400415D RID: 16733
		public const short AngryBonesBigMuscle = 295;

		// Token: 0x0400415E RID: 16734
		public const short AngryBonesBigHelmet = 296;

		// Token: 0x0400415F RID: 16735
		public const short BirdBlue = 297;

		// Token: 0x04004160 RID: 16736
		public const short BirdRed = 298;

		// Token: 0x04004161 RID: 16737
		public const short Squirrel = 299;

		// Token: 0x04004162 RID: 16738
		public const short Mouse = 300;

		// Token: 0x04004163 RID: 16739
		public const short Raven = 301;

		// Token: 0x04004164 RID: 16740
		public const short SlimeMasked = 302;

		// Token: 0x04004165 RID: 16741
		public const short BunnySlimed = 303;

		// Token: 0x04004166 RID: 16742
		public const short HoppinJack = 304;

		// Token: 0x04004167 RID: 16743
		public const short Scarecrow1 = 305;

		// Token: 0x04004168 RID: 16744
		public const short Scarecrow2 = 306;

		// Token: 0x04004169 RID: 16745
		public const short Scarecrow3 = 307;

		// Token: 0x0400416A RID: 16746
		public const short Scarecrow4 = 308;

		// Token: 0x0400416B RID: 16747
		public const short Scarecrow5 = 309;

		// Token: 0x0400416C RID: 16748
		public const short Scarecrow6 = 310;

		// Token: 0x0400416D RID: 16749
		public const short Scarecrow7 = 311;

		// Token: 0x0400416E RID: 16750
		public const short Scarecrow8 = 312;

		// Token: 0x0400416F RID: 16751
		public const short Scarecrow9 = 313;

		// Token: 0x04004170 RID: 16752
		public const short Scarecrow10 = 314;

		// Token: 0x04004171 RID: 16753
		public const short HeadlessHorseman = 315;

		// Token: 0x04004172 RID: 16754
		public const short Ghost = 316;

		// Token: 0x04004173 RID: 16755
		public const short DemonEyeOwl = 317;

		// Token: 0x04004174 RID: 16756
		public const short DemonEyeSpaceship = 318;

		// Token: 0x04004175 RID: 16757
		public const short ZombieDoctor = 319;

		// Token: 0x04004176 RID: 16758
		public const short ZombieSuperman = 320;

		// Token: 0x04004177 RID: 16759
		public const short ZombiePixie = 321;

		// Token: 0x04004178 RID: 16760
		public const short SkeletonTopHat = 322;

		// Token: 0x04004179 RID: 16761
		public const short SkeletonAstonaut = 323;

		// Token: 0x0400417A RID: 16762
		public const short SkeletonAlien = 324;

		// Token: 0x0400417B RID: 16763
		public const short MourningWood = 325;

		// Token: 0x0400417C RID: 16764
		public const short Splinterling = 326;

		// Token: 0x0400417D RID: 16765
		public const short Pumpking = 327;

		// Token: 0x0400417E RID: 16766
		public const short PumpkingBlade = 328;

		// Token: 0x0400417F RID: 16767
		public const short Hellhound = 329;

		// Token: 0x04004180 RID: 16768
		public const short Poltergeist = 330;

		// Token: 0x04004181 RID: 16769
		public const short ZombieXmas = 331;

		// Token: 0x04004182 RID: 16770
		public const short ZombieSweater = 332;

		// Token: 0x04004183 RID: 16771
		public const short SlimeRibbonWhite = 333;

		// Token: 0x04004184 RID: 16772
		public const short SlimeRibbonYellow = 334;

		// Token: 0x04004185 RID: 16773
		public const short SlimeRibbonGreen = 335;

		// Token: 0x04004186 RID: 16774
		public const short SlimeRibbonRed = 336;

		// Token: 0x04004187 RID: 16775
		public const short BunnyXmas = 337;

		// Token: 0x04004188 RID: 16776
		public const short ZombieElf = 338;

		// Token: 0x04004189 RID: 16777
		public const short ZombieElfBeard = 339;

		// Token: 0x0400418A RID: 16778
		public const short ZombieElfGirl = 340;

		// Token: 0x0400418B RID: 16779
		public const short PresentMimic = 341;

		// Token: 0x0400418C RID: 16780
		public const short GingerbreadMan = 342;

		// Token: 0x0400418D RID: 16781
		public const short Yeti = 343;

		// Token: 0x0400418E RID: 16782
		public const short Everscream = 344;

		// Token: 0x0400418F RID: 16783
		public const short IceQueen = 345;

		// Token: 0x04004190 RID: 16784
		public const short SantaNK1 = 346;

		// Token: 0x04004191 RID: 16785
		public const short ElfCopter = 347;

		// Token: 0x04004192 RID: 16786
		public const short Nutcracker = 348;

		// Token: 0x04004193 RID: 16787
		public const short NutcrackerSpinning = 349;

		// Token: 0x04004194 RID: 16788
		public const short ElfArcher = 350;

		// Token: 0x04004195 RID: 16789
		public const short Krampus = 351;

		// Token: 0x04004196 RID: 16790
		public const short Flocko = 352;

		// Token: 0x04004197 RID: 16791
		public const short Stylist = 353;

		// Token: 0x04004198 RID: 16792
		public const short WebbedStylist = 354;

		// Token: 0x04004199 RID: 16793
		public const short Firefly = 355;

		// Token: 0x0400419A RID: 16794
		public const short Butterfly = 356;

		// Token: 0x0400419B RID: 16795
		public const short Worm = 357;

		// Token: 0x0400419C RID: 16796
		public const short LightningBug = 358;

		// Token: 0x0400419D RID: 16797
		public const short Snail = 359;

		// Token: 0x0400419E RID: 16798
		public const short GlowingSnail = 360;

		// Token: 0x0400419F RID: 16799
		public const short Frog = 361;

		// Token: 0x040041A0 RID: 16800
		public const short Duck = 362;

		// Token: 0x040041A1 RID: 16801
		public const short Duck2 = 363;

		// Token: 0x040041A2 RID: 16802
		public const short DuckWhite = 364;

		// Token: 0x040041A3 RID: 16803
		public const short DuckWhite2 = 365;

		// Token: 0x040041A4 RID: 16804
		public const short ScorpionBlack = 366;

		// Token: 0x040041A5 RID: 16805
		public const short Scorpion = 367;

		// Token: 0x040041A6 RID: 16806
		public const short TravellingMerchant = 368;

		// Token: 0x040041A7 RID: 16807
		public const short Angler = 369;

		// Token: 0x040041A8 RID: 16808
		public const short DukeFishron = 370;

		// Token: 0x040041A9 RID: 16809
		public const short DetonatingBubble = 371;

		// Token: 0x040041AA RID: 16810
		public const short Sharkron = 372;

		// Token: 0x040041AB RID: 16811
		public const short Sharkron2 = 373;

		// Token: 0x040041AC RID: 16812
		public const short TruffleWorm = 374;

		// Token: 0x040041AD RID: 16813
		public const short TruffleWormDigger = 375;

		// Token: 0x040041AE RID: 16814
		public const short SleepingAngler = 376;

		// Token: 0x040041AF RID: 16815
		public const short Grasshopper = 377;

		// Token: 0x040041B0 RID: 16816
		public const short ChatteringTeethBomb = 378;

		// Token: 0x040041B1 RID: 16817
		public const short CultistArcherBlue = 379;

		// Token: 0x040041B2 RID: 16818
		public const short CultistArcherWhite = 380;

		// Token: 0x040041B3 RID: 16819
		public const short BrainScrambler = 381;

		// Token: 0x040041B4 RID: 16820
		public const short RayGunner = 382;

		// Token: 0x040041B5 RID: 16821
		public const short MartianOfficer = 383;

		// Token: 0x040041B6 RID: 16822
		public const short ForceBubble = 384;

		// Token: 0x040041B7 RID: 16823
		public const short GrayGrunt = 385;

		// Token: 0x040041B8 RID: 16824
		public const short MartianEngineer = 386;

		// Token: 0x040041B9 RID: 16825
		public const short MartianTurret = 387;

		// Token: 0x040041BA RID: 16826
		public const short MartianDrone = 388;

		// Token: 0x040041BB RID: 16827
		public const short GigaZapper = 389;

		// Token: 0x040041BC RID: 16828
		public const short ScutlixRider = 390;

		// Token: 0x040041BD RID: 16829
		public const short Scutlix = 391;

		// Token: 0x040041BE RID: 16830
		public const short MartianSaucer = 392;

		// Token: 0x040041BF RID: 16831
		public const short MartianSaucerTurret = 393;

		// Token: 0x040041C0 RID: 16832
		public const short MartianSaucerCannon = 394;

		// Token: 0x040041C1 RID: 16833
		public const short MartianSaucerCore = 395;

		// Token: 0x040041C2 RID: 16834
		public const short MoonLordHead = 396;

		// Token: 0x040041C3 RID: 16835
		public const short MoonLordHand = 397;

		// Token: 0x040041C4 RID: 16836
		public const short MoonLordCore = 398;

		// Token: 0x040041C5 RID: 16837
		public const short MartianProbe = 399;

		// Token: 0x040041C6 RID: 16838
		public const short MoonLordFreeEye = 400;

		// Token: 0x040041C7 RID: 16839
		public const short MoonLordLeechBlob = 401;

		// Token: 0x040041C8 RID: 16840
		public const short StardustWormHead = 402;

		// Token: 0x040041C9 RID: 16841
		public const short StardustWormBody = 403;

		// Token: 0x040041CA RID: 16842
		public const short StardustWormTail = 404;

		// Token: 0x040041CB RID: 16843
		public const short StardustCellBig = 405;

		// Token: 0x040041CC RID: 16844
		public const short StardustCellSmall = 406;

		// Token: 0x040041CD RID: 16845
		public const short StardustJellyfishBig = 407;

		// Token: 0x040041CE RID: 16846
		public const short StardustJellyfishSmall = 408;

		// Token: 0x040041CF RID: 16847
		public const short StardustSpiderBig = 409;

		// Token: 0x040041D0 RID: 16848
		public const short StardustSpiderSmall = 410;

		// Token: 0x040041D1 RID: 16849
		public const short StardustSoldier = 411;

		// Token: 0x040041D2 RID: 16850
		public const short SolarCrawltipedeHead = 412;

		// Token: 0x040041D3 RID: 16851
		public const short SolarCrawltipedeBody = 413;

		// Token: 0x040041D4 RID: 16852
		public const short SolarCrawltipedeTail = 414;

		// Token: 0x040041D5 RID: 16853
		public const short SolarDrakomire = 415;

		// Token: 0x040041D6 RID: 16854
		public const short SolarDrakomireRider = 416;

		// Token: 0x040041D7 RID: 16855
		public const short SolarSroller = 417;

		// Token: 0x040041D8 RID: 16856
		public const short SolarCorite = 418;

		// Token: 0x040041D9 RID: 16857
		public const short SolarSolenian = 419;

		// Token: 0x040041DA RID: 16858
		public const short NebulaBrain = 420;

		// Token: 0x040041DB RID: 16859
		public const short NebulaHeadcrab = 421;

		// Token: 0x040041DC RID: 16860
		public const short NebulaBeast = 423;

		// Token: 0x040041DD RID: 16861
		public const short NebulaSoldier = 424;

		// Token: 0x040041DE RID: 16862
		public const short VortexRifleman = 425;

		// Token: 0x040041DF RID: 16863
		public const short VortexHornetQueen = 426;

		// Token: 0x040041E0 RID: 16864
		public const short VortexHornet = 427;

		// Token: 0x040041E1 RID: 16865
		public const short VortexLarva = 428;

		// Token: 0x040041E2 RID: 16866
		public const short VortexSoldier = 429;

		// Token: 0x040041E3 RID: 16867
		public const short ArmedZombie = 430;

		// Token: 0x040041E4 RID: 16868
		public const short ArmedZombieEskimo = 431;

		// Token: 0x040041E5 RID: 16869
		public const short ArmedZombiePincussion = 432;

		// Token: 0x040041E6 RID: 16870
		public const short ArmedZombieSlimed = 433;

		// Token: 0x040041E7 RID: 16871
		public const short ArmedZombieSwamp = 434;

		// Token: 0x040041E8 RID: 16872
		public const short ArmedZombieTwiggy = 435;

		// Token: 0x040041E9 RID: 16873
		public const short ArmedZombieCenx = 436;

		// Token: 0x040041EA RID: 16874
		public const short CultistTablet = 437;

		// Token: 0x040041EB RID: 16875
		public const short CultistDevote = 438;

		// Token: 0x040041EC RID: 16876
		public const short CultistBoss = 439;

		// Token: 0x040041ED RID: 16877
		public const short CultistBossClone = 440;

		// Token: 0x040041EE RID: 16878
		public const short GoldBird = 442;

		// Token: 0x040041EF RID: 16879
		public const short GoldBunny = 443;

		// Token: 0x040041F0 RID: 16880
		public const short GoldButterfly = 444;

		// Token: 0x040041F1 RID: 16881
		public const short GoldFrog = 445;

		// Token: 0x040041F2 RID: 16882
		public const short GoldGrasshopper = 446;

		// Token: 0x040041F3 RID: 16883
		public const short GoldMouse = 447;

		// Token: 0x040041F4 RID: 16884
		public const short GoldWorm = 448;

		// Token: 0x040041F5 RID: 16885
		public const short BoneThrowingSkeleton = 449;

		// Token: 0x040041F6 RID: 16886
		public const short BoneThrowingSkeleton2 = 450;

		// Token: 0x040041F7 RID: 16887
		public const short BoneThrowingSkeleton3 = 451;

		// Token: 0x040041F8 RID: 16888
		public const short BoneThrowingSkeleton4 = 452;

		// Token: 0x040041F9 RID: 16889
		public const short SkeletonMerchant = 453;

		// Token: 0x040041FA RID: 16890
		public const short CultistDragonHead = 454;

		// Token: 0x040041FB RID: 16891
		public const short CultistDragonBody1 = 455;

		// Token: 0x040041FC RID: 16892
		public const short CultistDragonBody2 = 456;

		// Token: 0x040041FD RID: 16893
		public const short CultistDragonBody3 = 457;

		// Token: 0x040041FE RID: 16894
		public const short CultistDragonBody4 = 458;

		// Token: 0x040041FF RID: 16895
		public const short CultistDragonTail = 459;

		// Token: 0x04004200 RID: 16896
		public const short Butcher = 460;

		// Token: 0x04004201 RID: 16897
		public const short CreatureFromTheDeep = 461;

		// Token: 0x04004202 RID: 16898
		public const short Fritz = 462;

		// Token: 0x04004203 RID: 16899
		public const short Nailhead = 463;

		// Token: 0x04004204 RID: 16900
		public const short CrimsonBunny = 464;

		// Token: 0x04004205 RID: 16901
		public const short CrimsonGoldfish = 465;

		// Token: 0x04004206 RID: 16902
		public const short Psycho = 466;

		// Token: 0x04004207 RID: 16903
		public const short DeadlySphere = 467;

		// Token: 0x04004208 RID: 16904
		public const short DrManFly = 468;

		// Token: 0x04004209 RID: 16905
		public const short ThePossessed = 469;

		// Token: 0x0400420A RID: 16906
		public const short CrimsonPenguin = 470;

		// Token: 0x0400420B RID: 16907
		public const short GoblinSummoner = 471;

		// Token: 0x0400420C RID: 16908
		public const short ShadowFlameApparition = 472;

		// Token: 0x0400420D RID: 16909
		public const short BigMimicCorruption = 473;

		// Token: 0x0400420E RID: 16910
		public const short BigMimicCrimson = 474;

		// Token: 0x0400420F RID: 16911
		public const short BigMimicHallow = 475;

		// Token: 0x04004210 RID: 16912
		public const short BigMimicJungle = 476;

		// Token: 0x04004211 RID: 16913
		public const short Mothron = 477;

		// Token: 0x04004212 RID: 16914
		public const short MothronEgg = 478;

		// Token: 0x04004213 RID: 16915
		public const short MothronSpawn = 479;

		// Token: 0x04004214 RID: 16916
		public const short Medusa = 480;

		// Token: 0x04004215 RID: 16917
		public const short GreekSkeleton = 481;

		// Token: 0x04004216 RID: 16918
		public const short GraniteGolem = 482;

		// Token: 0x04004217 RID: 16919
		public const short GraniteFlyer = 483;

		// Token: 0x04004218 RID: 16920
		public const short EnchantedNightcrawler = 484;

		// Token: 0x04004219 RID: 16921
		public const short Grubby = 485;

		// Token: 0x0400421A RID: 16922
		public const short Sluggy = 486;

		// Token: 0x0400421B RID: 16923
		public const short Buggy = 487;

		// Token: 0x0400421C RID: 16924
		public const short TargetDummy = 488;

		// Token: 0x0400421D RID: 16925
		public const short BloodZombie = 489;

		// Token: 0x0400421E RID: 16926
		public const short Drippler = 490;

		// Token: 0x0400421F RID: 16927
		public const short PirateShip = 491;

		// Token: 0x04004220 RID: 16928
		public const short PirateShipCannon = 492;

		// Token: 0x04004221 RID: 16929
		public const short LunarTowerStardust = 493;

		// Token: 0x04004222 RID: 16930
		public const short Crawdad = 494;

		// Token: 0x04004223 RID: 16931
		public const short Crawdad2 = 495;

		// Token: 0x04004224 RID: 16932
		public const short GiantShelly = 496;

		// Token: 0x04004225 RID: 16933
		public const short GiantShelly2 = 497;

		// Token: 0x04004226 RID: 16934
		public const short Salamander = 498;

		// Token: 0x04004227 RID: 16935
		public const short Salamander2 = 499;

		// Token: 0x04004228 RID: 16936
		public const short Salamander3 = 500;

		// Token: 0x04004229 RID: 16937
		public const short Salamander4 = 501;

		// Token: 0x0400422A RID: 16938
		public const short Salamander5 = 502;

		// Token: 0x0400422B RID: 16939
		public const short Salamander6 = 503;

		// Token: 0x0400422C RID: 16940
		public const short Salamander7 = 504;

		// Token: 0x0400422D RID: 16941
		public const short Salamander8 = 505;

		// Token: 0x0400422E RID: 16942
		public const short Salamander9 = 506;

		// Token: 0x0400422F RID: 16943
		public const short LunarTowerNebula = 507;

		// Token: 0x04004230 RID: 16944
		public const short LunarTowerVortex = 422;

		// Token: 0x04004231 RID: 16945
		public const short TaxCollector = 441;

		// Token: 0x04004232 RID: 16946
		public const short GiantWalkingAntlion = 508;

		// Token: 0x04004233 RID: 16947
		public const short GiantFlyingAntlion = 509;

		// Token: 0x04004234 RID: 16948
		public const short DuneSplicerHead = 510;

		// Token: 0x04004235 RID: 16949
		public const short DuneSplicerBody = 511;

		// Token: 0x04004236 RID: 16950
		public const short DuneSplicerTail = 512;

		// Token: 0x04004237 RID: 16951
		public const short TombCrawlerHead = 513;

		// Token: 0x04004238 RID: 16952
		public const short TombCrawlerBody = 514;

		// Token: 0x04004239 RID: 16953
		public const short TombCrawlerTail = 515;

		// Token: 0x0400423A RID: 16954
		public const short SolarFlare = 516;

		// Token: 0x0400423B RID: 16955
		public const short LunarTowerSolar = 517;

		// Token: 0x0400423C RID: 16956
		public const short SolarSpearman = 518;

		// Token: 0x0400423D RID: 16957
		public const short SolarGoop = 519;

		// Token: 0x0400423E RID: 16958
		public const short MartianWalker = 520;

		// Token: 0x0400423F RID: 16959
		public const short AncientCultistSquidhead = 521;

		// Token: 0x04004240 RID: 16960
		public const short AncientLight = 522;

		// Token: 0x04004241 RID: 16961
		public const short AncientDoom = 523;

		// Token: 0x04004242 RID: 16962
		public const short DesertGhoul = 524;

		// Token: 0x04004243 RID: 16963
		public const short DesertGhoulCorruption = 525;

		// Token: 0x04004244 RID: 16964
		public const short DesertGhoulCrimson = 526;

		// Token: 0x04004245 RID: 16965
		public const short DesertGhoulHallow = 527;

		// Token: 0x04004246 RID: 16966
		public const short DesertLamiaLight = 528;

		// Token: 0x04004247 RID: 16967
		public const short DesertLamiaDark = 529;

		// Token: 0x04004248 RID: 16968
		public const short DesertScorpionWalk = 530;

		// Token: 0x04004249 RID: 16969
		public const short DesertScorpionWall = 531;

		// Token: 0x0400424A RID: 16970
		public const short DesertBeast = 532;

		// Token: 0x0400424B RID: 16971
		public const short DesertDjinn = 533;

		// Token: 0x0400424C RID: 16972
		public const short DemonTaxCollector = 534;

		// Token: 0x0400424D RID: 16973
		public const short SlimeSpiked = 535;

		// Token: 0x0400424E RID: 16974
		public const short TheBride = 536;

		// Token: 0x0400424F RID: 16975
		public const short SandSlime = 537;

		// Token: 0x04004250 RID: 16976
		public const short SquirrelRed = 538;

		// Token: 0x04004251 RID: 16977
		public const short SquirrelGold = 539;

		// Token: 0x04004252 RID: 16978
		public const short PartyBunny = 540;

		// Token: 0x04004253 RID: 16979
		public const short SandElemental = 541;

		// Token: 0x04004254 RID: 16980
		public const short SandShark = 542;

		// Token: 0x04004255 RID: 16981
		public const short SandsharkCorrupt = 543;

		// Token: 0x04004256 RID: 16982
		public const short SandsharkCrimson = 544;

		// Token: 0x04004257 RID: 16983
		public const short SandsharkHallow = 545;

		// Token: 0x04004258 RID: 16984
		public const short Tumbleweed = 546;

		// Token: 0x04004259 RID: 16985
		public const short DD2AttackerTest = 547;

		// Token: 0x0400425A RID: 16986
		public const short DD2EterniaCrystal = 548;

		// Token: 0x0400425B RID: 16987
		public const short DD2LanePortal = 549;

		// Token: 0x0400425C RID: 16988
		public const short DD2Bartender = 550;

		// Token: 0x0400425D RID: 16989
		public const short DD2Betsy = 551;

		// Token: 0x0400425E RID: 16990
		public const short DD2GoblinT1 = 552;

		// Token: 0x0400425F RID: 16991
		public const short DD2GoblinT2 = 553;

		// Token: 0x04004260 RID: 16992
		public const short DD2GoblinT3 = 554;

		// Token: 0x04004261 RID: 16993
		public const short DD2GoblinBomberT1 = 555;

		// Token: 0x04004262 RID: 16994
		public const short DD2GoblinBomberT2 = 556;

		// Token: 0x04004263 RID: 16995
		public const short DD2GoblinBomberT3 = 557;

		// Token: 0x04004264 RID: 16996
		public const short DD2WyvernT1 = 558;

		// Token: 0x04004265 RID: 16997
		public const short DD2WyvernT2 = 559;

		// Token: 0x04004266 RID: 16998
		public const short DD2WyvernT3 = 560;

		// Token: 0x04004267 RID: 16999
		public const short DD2JavelinstT1 = 561;

		// Token: 0x04004268 RID: 17000
		public const short DD2JavelinstT2 = 562;

		// Token: 0x04004269 RID: 17001
		public const short DD2JavelinstT3 = 563;

		// Token: 0x0400426A RID: 17002
		public const short DD2DarkMageT1 = 564;

		// Token: 0x0400426B RID: 17003
		public const short DD2DarkMageT3 = 565;

		// Token: 0x0400426C RID: 17004
		public const short DD2SkeletonT1 = 566;

		// Token: 0x0400426D RID: 17005
		public const short DD2SkeletonT3 = 567;

		// Token: 0x0400426E RID: 17006
		public const short DD2WitherBeastT2 = 568;

		// Token: 0x0400426F RID: 17007
		public const short DD2WitherBeastT3 = 569;

		// Token: 0x04004270 RID: 17008
		public const short DD2DrakinT2 = 570;

		// Token: 0x04004271 RID: 17009
		public const short DD2DrakinT3 = 571;

		// Token: 0x04004272 RID: 17010
		public const short DD2KoboldWalkerT2 = 572;

		// Token: 0x04004273 RID: 17011
		public const short DD2KoboldWalkerT3 = 573;

		// Token: 0x04004274 RID: 17012
		public const short DD2KoboldFlyerT2 = 574;

		// Token: 0x04004275 RID: 17013
		public const short DD2KoboldFlyerT3 = 575;

		// Token: 0x04004276 RID: 17014
		public const short DD2OgreT2 = 576;

		// Token: 0x04004277 RID: 17015
		public const short DD2OgreT3 = 577;

		// Token: 0x04004278 RID: 17016
		public const short DD2LightningBugT3 = 578;

		// Token: 0x04004279 RID: 17017
		public const short BartenderUnconscious = 579;

		// Token: 0x0400427A RID: 17018
		public const short WalkingAntlion = 580;

		// Token: 0x0400427B RID: 17019
		public const short FlyingAntlion = 581;

		// Token: 0x0400427C RID: 17020
		public const short LarvaeAntlion = 582;

		// Token: 0x0400427D RID: 17021
		public const short FairyCritterPink = 583;

		// Token: 0x0400427E RID: 17022
		public const short FairyCritterGreen = 584;

		// Token: 0x0400427F RID: 17023
		public const short FairyCritterBlue = 585;

		// Token: 0x04004280 RID: 17024
		public const short ZombieMerman = 586;

		// Token: 0x04004281 RID: 17025
		public const short EyeballFlyingFish = 587;

		// Token: 0x04004282 RID: 17026
		public const short Golfer = 588;

		// Token: 0x04004283 RID: 17027
		public const short GolferRescue = 589;

		// Token: 0x04004284 RID: 17028
		public const short TorchZombie = 590;

		// Token: 0x04004285 RID: 17029
		public const short ArmedTorchZombie = 591;

		// Token: 0x04004286 RID: 17030
		public const short GoldGoldfish = 592;

		// Token: 0x04004287 RID: 17031
		public const short GoldGoldfishWalker = 593;

		// Token: 0x04004288 RID: 17032
		public const short WindyBalloon = 594;

		// Token: 0x04004289 RID: 17033
		public const short BlackDragonfly = 595;

		// Token: 0x0400428A RID: 17034
		public const short BlueDragonfly = 596;

		// Token: 0x0400428B RID: 17035
		public const short GreenDragonfly = 597;

		// Token: 0x0400428C RID: 17036
		public const short OrangeDragonfly = 598;

		// Token: 0x0400428D RID: 17037
		public const short RedDragonfly = 599;

		// Token: 0x0400428E RID: 17038
		public const short YellowDragonfly = 600;

		// Token: 0x0400428F RID: 17039
		public const short GoldDragonfly = 601;

		// Token: 0x04004290 RID: 17040
		public const short Seagull = 602;

		// Token: 0x04004291 RID: 17041
		public const short Seagull2 = 603;

		// Token: 0x04004292 RID: 17042
		public const short LadyBug = 604;

		// Token: 0x04004293 RID: 17043
		public const short GoldLadyBug = 605;

		// Token: 0x04004294 RID: 17044
		public const short Maggot = 606;

		// Token: 0x04004295 RID: 17045
		public const short Pupfish = 607;

		// Token: 0x04004296 RID: 17046
		public const short Grebe = 608;

		// Token: 0x04004297 RID: 17047
		public const short Grebe2 = 609;

		// Token: 0x04004298 RID: 17048
		public const short Rat = 610;

		// Token: 0x04004299 RID: 17049
		public const short Owl = 611;

		// Token: 0x0400429A RID: 17050
		public const short WaterStrider = 612;

		// Token: 0x0400429B RID: 17051
		public const short GoldWaterStrider = 613;

		// Token: 0x0400429C RID: 17052
		public const short ExplosiveBunny = 614;

		// Token: 0x0400429D RID: 17053
		public const short Dolphin = 615;

		// Token: 0x0400429E RID: 17054
		public const short Turtle = 616;

		// Token: 0x0400429F RID: 17055
		public const short TurtleJungle = 617;

		// Token: 0x040042A0 RID: 17056
		public const short BloodNautilus = 618;

		// Token: 0x040042A1 RID: 17057
		public const short BloodSquid = 619;

		// Token: 0x040042A2 RID: 17058
		public const short GoblinShark = 620;

		// Token: 0x040042A3 RID: 17059
		public const short BloodEelHead = 621;

		// Token: 0x040042A4 RID: 17060
		public const short BloodEelBody = 622;

		// Token: 0x040042A5 RID: 17061
		public const short BloodEelTail = 623;

		// Token: 0x040042A6 RID: 17062
		public const short Gnome = 624;

		// Token: 0x040042A7 RID: 17063
		public const short SeaTurtle = 625;

		// Token: 0x040042A8 RID: 17064
		public const short Seahorse = 626;

		// Token: 0x040042A9 RID: 17065
		public const short GoldSeahorse = 627;

		// Token: 0x040042AA RID: 17066
		public const short Dandelion = 628;

		// Token: 0x040042AB RID: 17067
		public const short IceMimic = 629;

		// Token: 0x040042AC RID: 17068
		public const short BloodMummy = 630;

		// Token: 0x040042AD RID: 17069
		public const short RockGolem = 631;

		// Token: 0x040042AE RID: 17070
		public const short MaggotZombie = 632;

		// Token: 0x040042AF RID: 17071
		public const short BestiaryGirl = 633;

		// Token: 0x040042B0 RID: 17072
		public const short SporeBat = 634;

		// Token: 0x040042B1 RID: 17073
		public const short SporeSkeleton = 635;

		// Token: 0x040042B2 RID: 17074
		public const short HallowBoss = 636;

		// Token: 0x040042B3 RID: 17075
		public const short TownCat = 637;

		// Token: 0x040042B4 RID: 17076
		public const short TownDog = 638;

		// Token: 0x040042B5 RID: 17077
		public const short GemSquirrelAmethyst = 639;

		// Token: 0x040042B6 RID: 17078
		public const short GemSquirrelTopaz = 640;

		// Token: 0x040042B7 RID: 17079
		public const short GemSquirrelSapphire = 641;

		// Token: 0x040042B8 RID: 17080
		public const short GemSquirrelEmerald = 642;

		// Token: 0x040042B9 RID: 17081
		public const short GemSquirrelRuby = 643;

		// Token: 0x040042BA RID: 17082
		public const short GemSquirrelDiamond = 644;

		// Token: 0x040042BB RID: 17083
		public const short GemSquirrelAmber = 645;

		// Token: 0x040042BC RID: 17084
		public const short GemBunnyAmethyst = 646;

		// Token: 0x040042BD RID: 17085
		public const short GemBunnyTopaz = 647;

		// Token: 0x040042BE RID: 17086
		public const short GemBunnySapphire = 648;

		// Token: 0x040042BF RID: 17087
		public const short GemBunnyEmerald = 649;

		// Token: 0x040042C0 RID: 17088
		public const short GemBunnyRuby = 650;

		// Token: 0x040042C1 RID: 17089
		public const short GemBunnyDiamond = 651;

		// Token: 0x040042C2 RID: 17090
		public const short GemBunnyAmber = 652;

		// Token: 0x040042C3 RID: 17091
		public const short HellButterfly = 653;

		// Token: 0x040042C4 RID: 17092
		public const short Lavafly = 654;

		// Token: 0x040042C5 RID: 17093
		public const short MagmaSnail = 655;

		// Token: 0x040042C6 RID: 17094
		public const short TownBunny = 656;

		// Token: 0x040042C7 RID: 17095
		public const short QueenSlimeBoss = 657;

		// Token: 0x040042C8 RID: 17096
		public const short QueenSlimeMinionBlue = 658;

		// Token: 0x040042C9 RID: 17097
		public const short QueenSlimeMinionPink = 659;

		// Token: 0x040042CA RID: 17098
		public const short QueenSlimeMinionPurple = 660;

		// Token: 0x040042CB RID: 17099
		public const short EmpressButterfly = 661;

		// Token: 0x040042CC RID: 17100
		public const short PirateGhost = 662;

		// Token: 0x040042CD RID: 17101
		public const short Princess = 663;

		// Token: 0x040042CE RID: 17102
		public const short TorchGod = 664;

		// Token: 0x040042CF RID: 17103
		public const short ChaosBallTim = 665;

		// Token: 0x040042D0 RID: 17104
		public const short VileSpitEaterOfWorlds = 666;

		// Token: 0x040042D1 RID: 17105
		public const short GoldenSlime = 667;

		// Token: 0x040042D2 RID: 17106
		public const short Deerclops = 668;

		// Token: 0x040042D3 RID: 17107
		public const short Stinkbug = 669;

		// Token: 0x040042D4 RID: 17108
		public const short TownSlimeBlue = 670;

		// Token: 0x040042D5 RID: 17109
		public const short ScarletMacaw = 671;

		// Token: 0x040042D6 RID: 17110
		public const short BlueMacaw = 672;

		// Token: 0x040042D7 RID: 17111
		public const short Toucan = 673;

		// Token: 0x040042D8 RID: 17112
		public const short YellowCockatiel = 674;

		// Token: 0x040042D9 RID: 17113
		public const short GrayCockatiel = 675;

		// Token: 0x040042DA RID: 17114
		public const short ShimmerSlime = 676;

		// Token: 0x040042DB RID: 17115
		public const short Shimmerfly = 677;

		// Token: 0x040042DC RID: 17116
		public const short TownSlimeGreen = 678;

		// Token: 0x040042DD RID: 17117
		public const short TownSlimeOld = 679;

		// Token: 0x040042DE RID: 17118
		public const short TownSlimePurple = 680;

		// Token: 0x040042DF RID: 17119
		public const short TownSlimeRainbow = 681;

		// Token: 0x040042E0 RID: 17120
		public const short TownSlimeRed = 682;

		// Token: 0x040042E1 RID: 17121
		public const short TownSlimeYellow = 683;

		// Token: 0x040042E2 RID: 17122
		public const short TownSlimeCopper = 684;

		// Token: 0x040042E3 RID: 17123
		public const short BoundTownSlimeOld = 685;

		// Token: 0x040042E4 RID: 17124
		public const short BoundTownSlimePurple = 686;

		// Token: 0x040042E5 RID: 17125
		public const short BoundTownSlimeYellow = 687;

		// Token: 0x040042E6 RID: 17126
		public static readonly short Count = 688;

		/// <inheritdoc cref="T:ReLogic.Reflection.IdDictionary" />
		// Token: 0x040042E7 RID: 17127
		public static readonly IdDictionary Search = IdDictionary.Create<NPCID, short>();

		// Token: 0x02000B59 RID: 2905
		public static class Sets
		{
			// Token: 0x06005C7C RID: 23676 RVA: 0x006B272C File Offset: 0x006B092C
			public static Dictionary<int, NPCID.Sets.NPCBestiaryDrawModifiers> NPCBestiaryDrawOffsetCreation()
			{
				Dictionary<int, NPCID.Sets.NPCBestiaryDrawModifiers> redigitEntries = NPCID.Sets.GetRedigitEntries();
				Dictionary<int, NPCID.Sets.NPCBestiaryDrawModifiers> leinforsEntries = NPCID.Sets.GetLeinforsEntries();
				Dictionary<int, NPCID.Sets.NPCBestiaryDrawModifiers> groxEntries = NPCID.Sets.GetGroxEntries();
				Dictionary<int, NPCID.Sets.NPCBestiaryDrawModifiers> dictionary = new Dictionary<int, NPCID.Sets.NPCBestiaryDrawModifiers>();
				foreach (KeyValuePair<int, NPCID.Sets.NPCBestiaryDrawModifiers> item in groxEntries)
				{
					dictionary[item.Key] = NPCID.Sets.<NPCBestiaryDrawOffsetCreation>g__FilterPaths|66_0(item.Value);
				}
				foreach (KeyValuePair<int, NPCID.Sets.NPCBestiaryDrawModifiers> item2 in leinforsEntries)
				{
					dictionary[item2.Key] = NPCID.Sets.<NPCBestiaryDrawOffsetCreation>g__FilterPaths|66_0(item2.Value);
				}
				foreach (KeyValuePair<int, NPCID.Sets.NPCBestiaryDrawModifiers> item3 in redigitEntries)
				{
					dictionary[item3.Key] = NPCID.Sets.<NPCBestiaryDrawOffsetCreation>g__FilterPaths|66_0(item3.Value);
				}
				return dictionary;
			}

			// Token: 0x06005C7D RID: 23677 RVA: 0x006B2844 File Offset: 0x006B0A44
			private static Dictionary<int, NPCID.Sets.NPCBestiaryDrawModifiers> GetRedigitEntries()
			{
				Dictionary<int, NPCID.Sets.NPCBestiaryDrawModifiers> dictionary = new Dictionary<int, NPCID.Sets.NPCBestiaryDrawModifiers>();
				int key = 430;
				NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key, value);
				int key2 = 431;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key2, value);
				int key3 = 432;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key3, value);
				int key4 = 433;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key4, value);
				int key5 = 434;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key5, value);
				int key6 = 435;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key6, value);
				int key7 = 436;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key7, value);
				int key8 = 591;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key8, value);
				int key9 = 449;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key9, value);
				int key10 = 450;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key10, value);
				int key11 = 451;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key11, value);
				int key12 = 452;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key12, value);
				int key13 = 595;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key13, value);
				int key14 = 596;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key14, value);
				int key15 = 597;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key15, value);
				int key16 = 598;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key16, value);
				int key17 = 600;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key17, value);
				int key18 = 495;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key18, value);
				int key19 = 497;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key19, value);
				int key20 = 498;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key20, value);
				int key21 = 500;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key21, value);
				int key22 = 501;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key22, value);
				int key23 = 502;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key23, value);
				int key24 = 503;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key24, value);
				int key25 = 504;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key25, value);
				int key26 = 505;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key26, value);
				int key27 = 506;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key27, value);
				int key28 = 230;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key28, value);
				int key29 = 593;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key29, value);
				int key30 = 158;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key30, value);
				int key31 = -2;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key31, value);
				int key32 = 440;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key32, value);
				int key33 = 568;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key33, value);
				int key34 = 566;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key34, value);
				int key35 = 576;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key35, value);
				int key36 = 558;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key36, value);
				int key37 = 559;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key37, value);
				int key38 = 552;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key38, value);
				int key39 = 553;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key39, value);
				int key40 = 564;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key40, value);
				int key41 = 570;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key41, value);
				int key42 = 555;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key42, value);
				int key43 = 556;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key43, value);
				int key44 = 574;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key44, value);
				int key45 = 561;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key45, value);
				int key46 = 562;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key46, value);
				int key47 = 572;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key47, value);
				int key48 = 535;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key48, value);
				return dictionary;
			}

			// Token: 0x06005C7E RID: 23678 RVA: 0x006B2D93 File Offset: 0x006B0F93
			private static Dictionary<int, NPCID.Sets.NPCBestiaryDrawModifiers> GetGroxEntries()
			{
				return new Dictionary<int, NPCID.Sets.NPCBestiaryDrawModifiers>();
			}

			// Token: 0x06005C7F RID: 23679 RVA: 0x006B2D9C File Offset: 0x006B0F9C
			private static Dictionary<int, NPCID.Sets.NPCBestiaryDrawModifiers> GetLeinforsEntries()
			{
				Dictionary<int, NPCID.Sets.NPCBestiaryDrawModifiers> dictionary = new Dictionary<int, NPCID.Sets.NPCBestiaryDrawModifiers>();
				int key = -65;
				NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key, value);
				int key2 = -64;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key2, value);
				int key3 = -63;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key3, value);
				int key4 = -62;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key4, value);
				int key5 = -61;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, 4f),
					Scale = 1.1f,
					Hide = true
				};
				dictionary.Add(key5, value);
				int key6 = -60;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, 3f),
					Scale = 0.9f,
					Hide = true
				};
				dictionary.Add(key6, value);
				int key7 = -59;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key7, value);
				int key8 = -58;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key8, value);
				int key9 = -57;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key9, value);
				int key10 = -56;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key10, value);
				int key11 = -55;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true,
					Velocity = 1f,
					Scale = 1.1f
				};
				dictionary.Add(key11, value);
				int key12 = -54;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true,
					Velocity = 1f,
					Scale = 0.9f
				};
				dictionary.Add(key12, value);
				int key13 = -53;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 1.1f,
					Hide = true
				};
				dictionary.Add(key13, value);
				int key14 = -52;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 0.9f,
					Hide = true
				};
				dictionary.Add(key14, value);
				int key15 = -51;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 1.1f,
					Hide = true
				};
				dictionary.Add(key15, value);
				int key16 = -50;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 0.9f,
					Hide = true
				};
				dictionary.Add(key16, value);
				int key17 = -49;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 1.1f,
					Hide = true
				};
				dictionary.Add(key17, value);
				int key18 = -48;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 0.9f,
					Hide = true
				};
				dictionary.Add(key18, value);
				int key19 = -47;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 1.1f,
					Hide = true
				};
				dictionary.Add(key19, value);
				int key20 = -46;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 0.9f,
					Hide = true
				};
				dictionary.Add(key20, value);
				int key21 = -45;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 1.1f,
					Hide = true
				};
				dictionary.Add(key21, value);
				int key22 = -44;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 0.9f,
					Hide = true
				};
				dictionary.Add(key22, value);
				int key23 = -43;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(5f, -15f),
					Hide = true
				};
				dictionary.Add(key23, value);
				int key24 = -42;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(5f, -15f),
					Hide = true
				};
				dictionary.Add(key24, value);
				int key25 = -41;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(5f, -15f),
					Hide = true
				};
				dictionary.Add(key25, value);
				int key26 = -40;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(5f, -15f),
					Hide = true
				};
				dictionary.Add(key26, value);
				int key27 = -39;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(5f, -15f),
					Hide = true
				};
				dictionary.Add(key27, value);
				int key28 = -38;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(5f, -15f),
					Hide = true
				};
				dictionary.Add(key28, value);
				int key29 = -37;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 1.1f,
					Hide = true
				};
				dictionary.Add(key29, value);
				int key30 = -36;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 0.9f,
					Hide = true
				};
				dictionary.Add(key30, value);
				int key31 = -35;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 1.1f,
					Hide = true
				};
				dictionary.Add(key31, value);
				int key32 = -34;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 0.9f,
					Hide = true
				};
				dictionary.Add(key32, value);
				int key33 = -33;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 1.1f,
					Hide = true
				};
				dictionary.Add(key33, value);
				int key34 = -32;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 0.9f,
					Hide = true
				};
				dictionary.Add(key34, value);
				int key35 = -31;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 1.1f,
					Hide = true
				};
				dictionary.Add(key35, value);
				int key36 = -30;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 0.9f,
					Hide = true
				};
				dictionary.Add(key36, value);
				int key37 = -29;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 1.1f,
					Hide = true
				};
				dictionary.Add(key37, value);
				int key38 = -28;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 0.9f,
					Hide = true
				};
				dictionary.Add(key38, value);
				int key39 = -27;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 1.1f,
					Hide = true
				};
				dictionary.Add(key39, value);
				int key40 = -26;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 0.9f,
					Hide = true
				};
				dictionary.Add(key40, value);
				int key41 = -23;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(5f, -9f),
					Rotation = 0.75f,
					Scale = 1.2f,
					Hide = true
				};
				dictionary.Add(key41, value);
				int key42 = -22;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(5f, -9f),
					Rotation = 0.75f,
					Scale = 0.8f,
					Hide = true
				};
				dictionary.Add(key42, value);
				int key43 = -25;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key43, value);
				int key44 = -24;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key44, value);
				int key45 = -21;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, 5f),
					Scale = 1.2f,
					Hide = true
				};
				dictionary.Add(key45, value);
				int key46 = -20;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, 4f),
					Scale = 1.1f,
					Hide = true
				};
				dictionary.Add(key46, value);
				int key47 = -19;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, 3f),
					Scale = 0.9f,
					Hide = true
				};
				dictionary.Add(key47, value);
				int key48 = -18;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 2f),
					Scale = 0.8f,
					Hide = true
				};
				dictionary.Add(key48, value);
				int key49 = -17;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 3f),
					Scale = 1.2f,
					Hide = true
				};
				dictionary.Add(key49, value);
				int key50 = -16;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, 3f),
					Scale = 0.8f,
					Hide = true
				};
				dictionary.Add(key50, value);
				int key51 = -15;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 1.2f,
					Hide = true
				};
				dictionary.Add(key51, value);
				int key52 = -14;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 1.1f,
					Hide = true
				};
				dictionary.Add(key52, value);
				int key53 = -13;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Scale = 0.9f,
					Hide = true
				};
				dictionary.Add(key53, value);
				int key54 = -12;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(5f, -9f),
					Rotation = 0.75f,
					Scale = 1.2f,
					Hide = true
				};
				dictionary.Add(key54, value);
				int key55 = -11;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(5f, -9f),
					Rotation = 0.75f,
					Scale = 0.8f,
					Hide = true
				};
				dictionary.Add(key55, value);
				int key56 = 2;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(6f, -15f),
					PortraitPositionYOverride = new float?(-35f)
				};
				dictionary.Add(key56, value);
				int key57 = 3;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					PortraitPositionXOverride = new float?(2f)
				};
				dictionary.Add(key57, value);
				int key58 = 4;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(25f, -30f),
					Rotation = 0.7f,
					Frame = new int?(4)
				};
				dictionary.Add(key58, value);
				int key59 = 5;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(3f, 4f),
					Rotation = 1.5f
				};
				dictionary.Add(key59, value);
				int key60 = 6;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(5f, -9f),
					Rotation = 0.75f
				};
				dictionary.Add(key60, value);
				int key61 = 7;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_7",
					Position = new Vector2(20f, 29f),
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?(10f)
				};
				dictionary.Add(key61, value);
				int key62 = 8;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key62, value);
				int key63 = 9;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key63, value);
				int key64 = 10;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_10",
					Position = new Vector2(2f, 24f),
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?(10f)
				};
				dictionary.Add(key64, value);
				int key65 = 11;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key65, value);
				int key66 = 12;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key66, value);
				int key67 = 13;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_13",
					Position = new Vector2(40f, 22f),
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?(10f)
				};
				dictionary.Add(key67, value);
				int key68 = 14;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key68, value);
				int key69 = 15;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key69, value);
				int key70 = 17;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key70, value);
				int key71 = 18;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					PortraitPositionXOverride = new float?(2f)
				};
				dictionary.Add(key71, value);
				int key72 = 19;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key72, value);
				int key73 = 20;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					PortraitPositionXOverride = new float?(2f)
				};
				dictionary.Add(key73, value);
				int key74 = 22;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key74, value);
				int key75 = 25;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key75, value);
				int key76 = 26;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key76, value);
				int key77 = 27;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key77, value);
				int key78 = 28;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key78, value);
				int key79 = 30;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key79, value);
				int key80 = 665;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key80, value);
				int key81 = 31;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key81, value);
				int key82 = 33;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key82, value);
				int key83 = 34;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Direction = new int?(1)
				};
				dictionary.Add(key83, value);
				int key84 = 21;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key84, value);
				int key85 = 35;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, -12f),
					Scale = 0.9f,
					PortraitPositionXOverride = new float?(-1f),
					PortraitPositionYOverride = new float?(-3f)
				};
				dictionary.Add(key85, value);
				int key86 = 36;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key86, value);
				int key87 = 38;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key87, value);
				int key88 = 37;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key88, value);
				int key89 = 39;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_39",
					Position = new Vector2(40f, 23f),
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?(10f)
				};
				dictionary.Add(key89, value);
				int key90 = 40;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key90, value);
				int key91 = 41;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key91, value);
				int key92 = 43;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(4f, -6f),
					Rotation = 2.3561945f
				};
				dictionary.Add(key92, value);
				int key93 = 44;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key93, value);
				int key94 = 46;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key94, value);
				int key95 = 47;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key95, value);
				int key96 = 48;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, -14f),
					PortraitPositionYOverride = new float?(-30f)
				};
				dictionary.Add(key96, value);
				int key97 = 49;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, -13f),
					PortraitPositionYOverride = new float?(-30f)
				};
				dictionary.Add(key97, value);
				int key98 = 50;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 90f),
					PortraitScale = new float?(1.1f),
					PortraitPositionYOverride = new float?(70f)
				};
				dictionary.Add(key98, value);
				int key99 = 51;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, -13f),
					PortraitPositionYOverride = new float?(-30f)
				};
				dictionary.Add(key99, value);
				int key100 = 52;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key100, value);
				int key101 = 53;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key101, value);
				int key102 = 54;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key102, value);
				int key103 = 55;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 6f),
					PortraitPositionYOverride = new float?(7f),
					IsWet = true
				};
				dictionary.Add(key103, value);
				int key104 = 56;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(4f, -6f),
					Rotation = 2.3561945f
				};
				dictionary.Add(key104, value);
				int key105 = 57;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 6f),
					PortraitPositionYOverride = new float?(6f),
					IsWet = true
				};
				dictionary.Add(key105, value);
				int key106 = 58;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 6f),
					PortraitPositionYOverride = new float?(6f),
					IsWet = true
				};
				dictionary.Add(key106, value);
				int key107 = 60;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, -19f),
					PortraitPositionYOverride = new float?(-36f)
				};
				dictionary.Add(key107, value);
				int key108 = 61;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 0.05f,
					PortraitPositionYOverride = new float?(-15f)
				};
				dictionary.Add(key108, value);
				int key109 = 62;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, -10f),
					PortraitPositionYOverride = new float?(-25f)
				};
				dictionary.Add(key109, value);
				int key110 = 65;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(35f, 4f),
					Velocity = 1f,
					PortraitPositionXOverride = new float?(5f),
					IsWet = true
				};
				dictionary.Add(key110, value);
				int key111 = 66;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, -6f),
					Scale = 0.9f,
					PortraitPositionYOverride = new float?(-15f)
				};
				dictionary.Add(key111, value);
				int key112 = 68;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-1f, -12f),
					Scale = 0.9f,
					PortraitPositionYOverride = new float?(-3f)
				};
				dictionary.Add(key112, value);
				int key113 = 70;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key113, value);
				int key114 = 72;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key114, value);
				int key115 = 73;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key115, value);
				int key116 = 74;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, -14f),
					Velocity = 0.05f,
					PortraitPositionYOverride = new float?(-30f)
				};
				dictionary.Add(key116, value);
				int key117 = 75;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 6f)
				};
				dictionary.Add(key117, value);
				int key118 = 76;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key118, value);
				int key119 = 77;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key119, value);
				int key120 = 78;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 0.5f
				};
				dictionary.Add(key120, value);
				int key121 = 79;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 0.5f
				};
				dictionary.Add(key121, value);
				int key122 = 80;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 0.5f
				};
				dictionary.Add(key122, value);
				int key123 = 83;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-4f, -4f),
					Scale = 0.9f,
					PortraitPositionYOverride = new float?(-25f)
				};
				dictionary.Add(key123, value);
				int key124 = 84;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(6f, -11f),
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?(-28f)
				};
				dictionary.Add(key124, value);
				int key125 = 86;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(20f, 6f),
					Velocity = 1f,
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?(2f)
				};
				dictionary.Add(key125, value);
				int key126 = 87;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_87",
					Position = new Vector2(55f, 15f),
					PortraitPositionXOverride = new float?(4f),
					PortraitPositionYOverride = new float?(10f)
				};
				dictionary.Add(key126, value);
				int key127 = 88;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key127, value);
				int key128 = 89;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key128, value);
				int key129 = 90;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key129, value);
				int key130 = 91;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key130, value);
				int key131 = 92;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key131, value);
				int key132 = 93;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(4f, -11f),
					PortraitPositionYOverride = new float?(-30f)
				};
				dictionary.Add(key132, value);
				int key133 = 94;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(8f, 0f),
					Rotation = 0.75f
				};
				dictionary.Add(key133, value);
				int key134 = 95;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_95",
					Position = new Vector2(20f, 28f),
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?(10f)
				};
				dictionary.Add(key134, value);
				int key135 = 96;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key135, value);
				int key136 = 97;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key136, value);
				int key137 = 98;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_98",
					Position = new Vector2(40f, 24f),
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?(12f)
				};
				dictionary.Add(key137, value);
				int key138 = 99;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key138, value);
				int key139 = 100;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key139, value);
				int key140 = 101;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-2f, 6f),
					Rotation = 2.3561945f
				};
				dictionary.Add(key140, value);
				int key141 = 102;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 6f),
					PortraitPositionYOverride = new float?(6f),
					IsWet = true
				};
				dictionary.Add(key141, value);
				int key142 = 104;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key142, value);
				int key143 = 105;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key143, value);
				int key144 = 106;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key144, value);
				int key145 = 107;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key145, value);
				int key146 = 108;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key146, value);
				int key147 = 109;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 35f),
					Velocity = 1f,
					PortraitPositionYOverride = new float?(0f)
				};
				dictionary.Add(key147, value);
				int key148 = 110;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 3f
				};
				dictionary.Add(key148, value);
				int key149 = 111;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 3f
				};
				dictionary.Add(key149, value);
				int key150 = 112;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key150, value);
				int key151 = 666;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key151, value);
				int key152 = 113;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_113",
					Position = new Vector2(56f, 5f),
					PortraitPositionXOverride = new float?(10f),
					PortraitPositionYOverride = new float?(0f)
				};
				dictionary.Add(key152, value);
				int key153 = 114;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key153, value);
				int key154 = 115;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_115",
					Position = new Vector2(56f, 3f),
					PortraitPositionXOverride = new float?(55f),
					PortraitPositionYOverride = new float?(0f)
				};
				dictionary.Add(key154, value);
				int key155 = 116;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(3f, -5f),
					PortraitPositionXOverride = new float?(4f),
					PortraitPositionYOverride = new float?(-26f)
				};
				dictionary.Add(key155, value);
				int key156 = 117;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_117",
					Position = new Vector2(10f, 20f),
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?(0f)
				};
				dictionary.Add(key156, value);
				int key157 = 118;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key157, value);
				int key158 = 119;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key158, value);
				int key159 = 120;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key159, value);
				int key160 = 123;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key160, value);
				int key161 = 124;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					PortraitPositionXOverride = new float?(2f)
				};
				dictionary.Add(key161, value);
				int key162 = 121;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-2f, -4f),
					PortraitPositionYOverride = new float?(-20f)
				};
				dictionary.Add(key162, value);
				int key163 = 122;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 4f)
				};
				dictionary.Add(key163, value);
				int key164 = 125;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-28f, -23f),
					Rotation = -0.75f
				};
				dictionary.Add(key164, value);
				int key165 = 126;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(28f, 30f),
					Rotation = 2.25f
				};
				dictionary.Add(key165, value);
				int key166 = 127;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_127",
					Position = new Vector2(0f, 0f),
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?(1f)
				};
				dictionary.Add(key166, value);
				int key167 = 128;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-6f, -2f),
					Rotation = -0.75f,
					Hide = true
				};
				dictionary.Add(key167, value);
				int key168 = 129;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(4f, 4f),
					Rotation = 0.75f,
					Hide = true
				};
				dictionary.Add(key168, value);
				int key169 = 130;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(6f, 8f),
					Rotation = 2.25f,
					Hide = true
				};
				dictionary.Add(key169, value);
				int key170 = 131;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-8f, 8f),
					Rotation = -2.25f,
					Hide = true
				};
				dictionary.Add(key170, value);
				int key171 = 132;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key171, value);
				int key172 = 133;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, -5f),
					PortraitPositionYOverride = new float?(-25f)
				};
				dictionary.Add(key172, value);
				int key173 = 134;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_134",
					Position = new Vector2(60f, 8f),
					PortraitPositionXOverride = new float?(3f),
					PortraitPositionYOverride = new float?(0f)
				};
				dictionary.Add(key173, value);
				int key174 = 135;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key174, value);
				int key175 = 136;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key175, value);
				int key176 = 137;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(4f, -11f),
					PortraitPositionYOverride = new float?(-30f)
				};
				dictionary.Add(key176, value);
				int key177 = 140;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key177, value);
				int key178 = 142;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					PortraitPositionXOverride = new float?(1f)
				};
				dictionary.Add(key178, value);
				int key179 = 146;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key179, value);
				int key180 = 148;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key180, value);
				int key181 = 149;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key181, value);
				int key182 = 150;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, -11f),
					PortraitPositionYOverride = new float?(-30f)
				};
				dictionary.Add(key182, value);
				int key183 = 151;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, -11f),
					PortraitPositionYOverride = new float?(-30f)
				};
				dictionary.Add(key183, value);
				int key184 = 152;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, -11f),
					PortraitPositionYOverride = new float?(-30f)
				};
				dictionary.Add(key184, value);
				int key185 = 153;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(20f, 0f),
					Velocity = 1f,
					PortraitPositionXOverride = new float?(0f)
				};
				dictionary.Add(key185, value);
				int key186 = 154;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(20f, 0f),
					Velocity = 1f,
					PortraitPositionXOverride = new float?(0f)
				};
				dictionary.Add(key186, value);
				int key187 = 155;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(15f, 0f),
					Velocity = 3f,
					PortraitPositionXOverride = new float?(0f)
				};
				dictionary.Add(key187, value);
				int key188 = 156;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					PortraitPositionYOverride = new float?(-15f)
				};
				dictionary.Add(key188, value);
				int key189 = 157;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(20f, 5f),
					PortraitPositionXOverride = new float?(5f),
					PortraitPositionYOverride = new float?(10f),
					IsWet = true
				};
				dictionary.Add(key189, value);
				int key190 = 160;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key190, value);
				int key191 = 158;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, -11f),
					PortraitPositionYOverride = new float?(-30f)
				};
				dictionary.Add(key191, value);
				int key192 = 159;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key192, value);
				int key193 = 161;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key193, value);
				int key194 = 162;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key194, value);
				int key195 = 163;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key195, value);
				int key196 = 164;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key196, value);
				int key197 = 165;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Rotation = -1.6f,
					Velocity = 2f
				};
				dictionary.Add(key197, value);
				int key198 = 167;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key198, value);
				int key199 = 168;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key199, value);
				int key200 = 170;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(10f, 5f),
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?(-12f)
				};
				dictionary.Add(key200, value);
				int key201 = 171;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(10f, 5f),
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?(-12f)
				};
				dictionary.Add(key201, value);
				int key202 = 173;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Rotation = 0.75f,
					Position = new Vector2(0f, -5f)
				};
				dictionary.Add(key202, value);
				int key203 = 174;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, -5f),
					Velocity = 1f
				};
				dictionary.Add(key203, value);
				int key204 = 175;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(4f, -2f),
					Rotation = 2.3561945f
				};
				dictionary.Add(key204, value);
				int key205 = 176;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 5f)
				};
				dictionary.Add(key205, value);
				int key206 = 177;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(10f, 15f),
					PortraitPositionXOverride = new float?(-4f),
					PortraitPositionYOverride = new float?(1f),
					Frame = new int?(0)
				};
				dictionary.Add(key206, value);
				int key207 = 178;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					PortraitPositionXOverride = new float?(2f)
				};
				dictionary.Add(key207, value);
				int key208 = 179;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-5f, 12f),
					PortraitPositionYOverride = new float?(-7f)
				};
				dictionary.Add(key208, value);
				int key209 = 180;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(10f, 5f),
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?(-12f)
				};
				dictionary.Add(key209, value);
				int key210 = 181;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key210, value);
				int key211 = 185;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key211, value);
				int key212 = 186;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key212, value);
				int key213 = 187;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key213, value);
				int key214 = 188;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key214, value);
				int key215 = 189;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key215, value);
				int key216 = 190;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(6f, -15f),
					PortraitPositionYOverride = new float?(-35f)
				};
				dictionary.Add(key216, value);
				int key217 = 191;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(6f, -15f),
					PortraitPositionYOverride = new float?(-35f)
				};
				dictionary.Add(key217, value);
				int key218 = 192;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(6f, -15f),
					PortraitPositionYOverride = new float?(-35f)
				};
				dictionary.Add(key218, value);
				int key219 = 193;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(5f, -15f),
					PortraitPositionYOverride = new float?(-35f)
				};
				dictionary.Add(key219, value);
				int key220 = 194;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(5f, -15f),
					PortraitPositionYOverride = new float?(-35f)
				};
				dictionary.Add(key220, value);
				int key221 = 196;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key221, value);
				int key222 = 197;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key222, value);
				int key223 = 198;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key223, value);
				int key224 = 199;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key224, value);
				int key225 = 200;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					PortraitPositionYOverride = new float?(2f)
				};
				dictionary.Add(key225, value);
				int key226 = 201;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key226, value);
				int key227 = 202;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key227, value);
				int key228 = 203;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key228, value);
				int key229 = 206;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 2f
				};
				dictionary.Add(key229, value);
				int key230 = 207;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key230, value);
				int key231 = 208;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					PortraitPositionXOverride = new float?(2f)
				};
				dictionary.Add(key231, value);
				int key232 = 209;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key232, value);
				int key233 = 212;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key233, value);
				int key234 = 213;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key234, value);
				int key235 = 214;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 3f
				};
				dictionary.Add(key235, value);
				int key236 = 215;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 3f
				};
				dictionary.Add(key236, value);
				int key237 = 216;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 3f
				};
				dictionary.Add(key237, value);
				int key238 = 221;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 5f),
					Velocity = 1f
				};
				dictionary.Add(key238, value);
				int key239 = 222;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(10f, 55f),
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?(40f)
				};
				dictionary.Add(key239, value);
				int key240 = 223;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key240, value);
				int key241 = 224;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(5f, -10f),
					PortraitPositionYOverride = new float?(-30f)
				};
				dictionary.Add(key241, value);
				int key242 = 226;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(5f, 3f),
					PortraitPositionYOverride = new float?(-15f)
				};
				dictionary.Add(key242, value);
				int key243 = 227;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					PortraitPositionXOverride = new float?(-3f)
				};
				dictionary.Add(key243, value);
				int key244 = 228;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					PortraitPositionXOverride = new float?(-5f)
				};
				dictionary.Add(key244, value);
				int key245 = 229;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key245, value);
				int key246 = 225;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, 0f)
				};
				dictionary.Add(key246, value);
				int key247 = 230;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key247, value);
				int key248 = 231;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 5f)
				};
				dictionary.Add(key248, value);
				int key249 = 232;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 5f)
				};
				dictionary.Add(key249, value);
				int key250 = 233;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 5f)
				};
				dictionary.Add(key250, value);
				int key251 = 234;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 5f)
				};
				dictionary.Add(key251, value);
				int key252 = 235;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 5f)
				};
				dictionary.Add(key252, value);
				int key253 = 236;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key253, value);
				int key254 = 237;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Rotation = -1.6f,
					Velocity = 2f
				};
				dictionary.Add(key254, value);
				int key255 = 238;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Rotation = -1.6f,
					Velocity = 2f
				};
				dictionary.Add(key255, value);
				int key256 = 239;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key256, value);
				int key257 = 240;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Rotation = -1.6f,
					Velocity = 2f
				};
				dictionary.Add(key257, value);
				int key258 = 241;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 6f),
					PortraitPositionYOverride = new float?(6f),
					IsWet = true
				};
				dictionary.Add(key258, value);
				int key259 = 242;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 10f)
				};
				dictionary.Add(key259, value);
				int key260 = 243;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 60f),
					Velocity = 1f,
					PortraitPositionYOverride = new float?(15f)
				};
				dictionary.Add(key260, value);
				int key261 = 245;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_245",
					Position = new Vector2(2f, 48f),
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?(24f)
				};
				dictionary.Add(key261, value);
				int key262 = 246;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key262, value);
				int key263 = 247;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key263, value);
				int key264 = 248;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key264, value);
				int key265 = 249;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key265, value);
				int key266 = 250;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, -6f),
					PortraitPositionYOverride = new float?(-26f)
				};
				dictionary.Add(key266, value);
				int key267 = 251;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key267, value);
				int key268 = 252;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(3f, 3f),
					Velocity = 0.05f
				};
				dictionary.Add(key268, value);
				int key269 = 253;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 5f)
				};
				dictionary.Add(key269, value);
				int key270 = 254;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key270, value);
				int key271 = 255;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					PortraitPositionXOverride = new float?(-2f)
				};
				dictionary.Add(key271, value);
				int key272 = 256;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 5f)
				};
				dictionary.Add(key272, value);
				int key273 = 257;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key273, value);
				int key274 = 258;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key274, value);
				int key275 = 259;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_259",
					Position = new Vector2(0f, 25f),
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?(8f)
				};
				dictionary.Add(key275, value);
				int key276 = 260;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_260",
					Position = new Vector2(0f, 25f),
					PortraitPositionXOverride = new float?(1f),
					PortraitPositionYOverride = new float?(4f)
				};
				dictionary.Add(key276, value);
				int key277 = 261;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key277, value);
				int key278 = 262;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 20f),
					Scale = 0.8f
				};
				dictionary.Add(key278, value);
				int key279 = 264;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key279, value);
				int key280 = 263;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key280, value);
				int key281 = 265;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key281, value);
				int key282 = 266;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(5f, 5f),
					Frame = new int?(4)
				};
				dictionary.Add(key282, value);
				int key283 = 268;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, -5f)
				};
				dictionary.Add(key283, value);
				int key284 = 269;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-5f, 0f),
					Velocity = 1f
				};
				dictionary.Add(key284, value);
				int key285 = 270;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-5f, 0f),
					Velocity = 1f
				};
				dictionary.Add(key285, value);
				int key286 = 271;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-2f, 0f),
					Velocity = 1f
				};
				dictionary.Add(key286, value);
				int key287 = 272;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-5f, 0f),
					Velocity = 1f
				};
				dictionary.Add(key287, value);
				int key288 = 273;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-5f, 0f),
					Velocity = 1f
				};
				dictionary.Add(key288, value);
				int key289 = 274;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-3f, 0f),
					Velocity = 1f
				};
				dictionary.Add(key289, value);
				int key290 = 275;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-3f, 2f),
					PortraitPositionYOverride = new float?(3f),
					Velocity = 1f
				};
				dictionary.Add(key290, value);
				int key291 = 276;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-5f, 0f),
					Velocity = 1f
				};
				dictionary.Add(key291, value);
				int key292 = 277;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-5f, 0f),
					Velocity = 1f
				};
				dictionary.Add(key292, value);
				int key293 = 278;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-2f, 0f),
					Velocity = 1f
				};
				dictionary.Add(key293, value);
				int key294 = 279;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-5f, 0f),
					Velocity = 1f
				};
				dictionary.Add(key294, value);
				int key295 = 280;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-3f, 0f),
					Velocity = 1f
				};
				dictionary.Add(key295, value);
				int key296 = 287;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key296, value);
				int key297 = 289;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, 10f),
					Direction = new int?(1)
				};
				dictionary.Add(key297, value);
				int key298 = 290;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(4f, 6f),
					Velocity = 1f
				};
				dictionary.Add(key298, value);
				int key299 = 291;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key299, value);
				int key300 = 292;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key300, value);
				int key301 = 293;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key301, value);
				int key302 = 294;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key302, value);
				int key303 = 295;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key303, value);
				int key304 = 296;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key304, value);
				int key305 = 297;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, -14f),
					Velocity = 0.05f,
					PortraitPositionYOverride = new float?(-30f)
				};
				dictionary.Add(key305, value);
				int key306 = 298;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, -14f),
					Velocity = 0.05f,
					PortraitPositionYOverride = new float?(-30f)
				};
				dictionary.Add(key306, value);
				int key307 = 299;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key307, value);
				int key308 = 301;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					PortraitPositionYOverride = new float?(-20f),
					Direction = new int?(-1),
					SpriteDirection = new int?(1),
					Velocity = 0.05f
				};
				dictionary.Add(key308, value);
				int key309 = 303;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key309, value);
				int key310 = 305;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 0.05f
				};
				dictionary.Add(key310, value);
				int key311 = 306;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 0.05f
				};
				dictionary.Add(key311, value);
				int key312 = 307;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 0.05f
				};
				dictionary.Add(key312, value);
				int key313 = 308;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 0.05f
				};
				dictionary.Add(key313, value);
				int key314 = 309;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 0.05f
				};
				dictionary.Add(key314, value);
				int key315 = 310;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key315, value);
				int key316 = 311;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key316, value);
				int key317 = 312;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key317, value);
				int key318 = 313;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key318, value);
				int key319 = 314;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key319, value);
				int key320 = 315;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(14f, 26f),
					Velocity = 2f,
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?(0f)
				};
				dictionary.Add(key320, value);
				int key321 = 316;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 4f)
				};
				dictionary.Add(key321, value);
				int key322 = 317;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(5f, -15f),
					PortraitPositionYOverride = new float?(-35f)
				};
				dictionary.Add(key322, value);
				int key323 = 318;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(5f, -13f),
					PortraitPositionYOverride = new float?(-31f)
				};
				dictionary.Add(key323, value);
				int key324 = 319;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key324, value);
				int key325 = 320;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key325, value);
				int key326 = 321;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					PortraitPositionXOverride = new float?(2f)
				};
				dictionary.Add(key326, value);
				int key327 = 322;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key327, value);
				int key328 = 323;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key328, value);
				int key329 = 324;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key329, value);
				int key330 = 325;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 36f)
				};
				dictionary.Add(key330, value);
				int key331 = 326;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key331, value);
				int key332 = 327;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, -8f)
				};
				dictionary.Add(key332, value);
				int key333 = 328;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key333, value);
				int key334 = 329;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 2f
				};
				dictionary.Add(key334, value);
				int key335 = 330;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, 14f)
				};
				dictionary.Add(key335, value);
				int key336 = 331;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key336, value);
				int key337 = 332;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key337, value);
				int key338 = 337;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key338, value);
				int key339 = 338;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key339, value);
				int key340 = 339;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key340, value);
				int key341 = 340;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key341, value);
				int key342 = 342;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key342, value);
				int key343 = 343;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(5f, 25f),
					Velocity = 1f,
					PortraitPositionYOverride = new float?(0f)
				};
				dictionary.Add(key343, value);
				int key344 = 344;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 90f)
				};
				dictionary.Add(key344, value);
				int key345 = 345;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-1f, 90f)
				};
				dictionary.Add(key345, value);
				int key346 = 346;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(30f, 80f),
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?(60f)
				};
				dictionary.Add(key346, value);
				int key347 = 347;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 4f)
				};
				dictionary.Add(key347, value);
				int key348 = 348;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Hide = true
				};
				dictionary.Add(key348, value);
				int key349 = 349;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-3f, 18f),
					PortraitPositionYOverride = new float?(0f)
				};
				dictionary.Add(key349, value);
				int key350 = 350;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(3f, 0f),
					Velocity = 2f
				};
				dictionary.Add(key350, value);
				int key351 = 351;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(6f, 60f),
					Velocity = 1f,
					PortraitPositionYOverride = new float?(30f)
				};
				dictionary.Add(key351, value);
				int key352 = 353;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					PortraitPositionXOverride = new float?(2f)
				};
				dictionary.Add(key352, value);
				int key353 = 633;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					PortraitPositionXOverride = new float?(2f)
				};
				dictionary.Add(key353, value);
				int key354 = 354;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key354, value);
				int key355 = 355;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, 2f)
				};
				dictionary.Add(key355, value);
				int key356 = 356;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, 3f),
					PortraitPositionYOverride = new float?(1f)
				};
				dictionary.Add(key356, value);
				int key357 = 357;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 2f),
					Velocity = 1f
				};
				dictionary.Add(key357, value);
				int key358 = 358;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, 2f)
				};
				dictionary.Add(key358, value);
				int key359 = 359;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 18f),
					PortraitPositionYOverride = new float?(40f)
				};
				dictionary.Add(key359, value);
				int key360 = 360;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, 17f),
					PortraitPositionYOverride = new float?(39f)
				};
				dictionary.Add(key360, value);
				int key361 = 362;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(3f, 0f),
					Velocity = 1f
				};
				dictionary.Add(key361, value);
				int key362 = 363;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Hide = true
				};
				dictionary.Add(key362, value);
				int key363 = 364;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(3f, 0f),
					Velocity = 1f
				};
				dictionary.Add(key363, value);
				int key364 = 365;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Hide = true
				};
				dictionary.Add(key364, value);
				int key365 = 366;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, 0f),
					Velocity = 1f
				};
				dictionary.Add(key365, value);
				int key366 = 367;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, 0f),
					Velocity = 1f
				};
				dictionary.Add(key366, value);
				int key367 = 368;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key367, value);
				int key368 = 369;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					PortraitPositionXOverride = new float?(2f)
				};
				dictionary.Add(key368, value);
				int key369 = 370;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(56f, -4f),
					Direction = new int?(1),
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?(0f)
				};
				dictionary.Add(key369, value);
				int key370 = 371;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key370, value);
				int key371 = 372;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(35f, 4f),
					Velocity = 1f,
					PortraitPositionXOverride = new float?(10f),
					PortraitPositionYOverride = new float?(-3f)
				};
				dictionary.Add(key371, value);
				int key372 = 373;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key372, value);
				int key373 = 374;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key373, value);
				int key374 = 375;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key374, value);
				int key375 = 376;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key375, value);
				int key376 = 379;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key376, value);
				int key377 = 380;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key377, value);
				int key378 = 381;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-2f, 0f),
					Velocity = 1f
				};
				dictionary.Add(key378, value);
				int key379 = 382;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-2f, 0f),
					Velocity = 1f
				};
				dictionary.Add(key379, value);
				int key380 = 383;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-2f, 0f),
					Velocity = 1f
				};
				dictionary.Add(key380, value);
				int key381 = 384;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key381, value);
				int key382 = 385;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-2f, 0f),
					Velocity = 1f
				};
				dictionary.Add(key382, value);
				int key383 = 386;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-2f, 0f),
					Velocity = 1f
				};
				dictionary.Add(key383, value);
				int key384 = 387;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, 0f),
					Velocity = 3f
				};
				dictionary.Add(key384, value);
				int key385 = 388;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Direction = new int?(1)
				};
				dictionary.Add(key385, value);
				int key386 = 389;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-6f, 0f),
					Velocity = 1f
				};
				dictionary.Add(key386, value);
				int key387 = 390;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(12f, 0f),
					Direction = new int?(-1),
					SpriteDirection = new int?(1),
					Velocity = 2f,
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?(-12f)
				};
				dictionary.Add(key387, value);
				int key388 = 391;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(16f, 12f),
					Direction = new int?(-1),
					SpriteDirection = new int?(1),
					Velocity = 2f,
					PortraitPositionXOverride = new float?(3f),
					PortraitPositionYOverride = new float?(0f)
				};
				dictionary.Add(key388, value);
				int key389 = 392;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key389, value);
				int key390 = 395;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_395",
					Position = new Vector2(-1f, 18f),
					PortraitPositionXOverride = new float?(1f),
					PortraitPositionYOverride = new float?(0f)
				};
				dictionary.Add(key390, value);
				int key391 = 393;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key391, value);
				int key392 = 394;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key392, value);
				int key393 = 396;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key393, value);
				int key394 = 397;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key394, value);
				int key395 = 398;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_398",
					Position = new Vector2(0f, 5f),
					Scale = 0.4f,
					PortraitScale = new float?(0.7f),
					PortraitPositionYOverride = new float?(0f)
				};
				dictionary.Add(key395, value);
				int key396 = 400;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key396, value);
				int key397 = 401;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key397, value);
				int key398 = 402;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_402",
					Position = new Vector2(42f, 15f),
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?(0f)
				};
				dictionary.Add(key398, value);
				int key399 = 403;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key399, value);
				int key400 = 404;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key400, value);
				int key401 = 408;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key401, value);
				int key402 = 410;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key402, value);
				int key403 = 411;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Direction = new int?(-1),
					SpriteDirection = new int?(1),
					Velocity = 1f
				};
				dictionary.Add(key403, value);
				int key404 = 412;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_412",
					Position = new Vector2(50f, 28f),
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?(4f)
				};
				dictionary.Add(key404, value);
				int key405 = 413;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key405, value);
				int key406 = 414;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key406, value);
				int key407 = 415;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(26f, 0f),
					Velocity = 3f,
					PortraitPositionXOverride = new float?(5f)
				};
				dictionary.Add(key407, value);
				int key408 = 416;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(6f, 20f),
					Velocity = 1f,
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?(0f)
				};
				dictionary.Add(key408, value);
				int key409 = 417;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-2f, 8f),
					Velocity = 1f,
					PortraitPositionYOverride = new float?(0f)
				};
				dictionary.Add(key409, value);
				int key410 = 418;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, 4f)
				};
				dictionary.Add(key410, value);
				int key411 = 419;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-2f, 0f),
					Velocity = 1f
				};
				dictionary.Add(key411, value);
				int key412 = 420;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 4f),
					Direction = new int?(1)
				};
				dictionary.Add(key412, value);
				int key413 = 421;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, -1f)
				};
				dictionary.Add(key413, value);
				int key414 = 422;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, 44f),
					Scale = 0.4f,
					PortraitPositionXOverride = new float?(2f),
					PortraitPositionYOverride = new float?(134f)
				};
				dictionary.Add(key414, value);
				int key415 = 423;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Direction = new int?(-1),
					SpriteDirection = new int?(1),
					Velocity = 1f
				};
				dictionary.Add(key415, value);
				int key416 = 424;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(4f, 0f),
					Direction = new int?(-1),
					SpriteDirection = new int?(1),
					Velocity = 2f
				};
				dictionary.Add(key416, value);
				int key417 = 425;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(4f, 0f),
					Direction = new int?(-1),
					SpriteDirection = new int?(1),
					Velocity = 2f
				};
				dictionary.Add(key417, value);
				int key418 = 426;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, 8f),
					Velocity = 2f,
					PortraitPositionYOverride = new float?(0f)
				};
				dictionary.Add(key418, value);
				int key419 = 427;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					PortraitPositionYOverride = new float?(-4f)
				};
				dictionary.Add(key419, value);
				int key420 = 428;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key420, value);
				int key421 = 429;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, 0f),
					Velocity = 1f
				};
				dictionary.Add(key421, value);
				int key422 = 430;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key422, value);
				int key423 = 431;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key423, value);
				int key424 = 432;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key424, value);
				int key425 = 433;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key425, value);
				int key426 = 434;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key426, value);
				int key427 = 435;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key427, value);
				int key428 = 436;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key428, value);
				int key429 = 437;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key429, value);
				int key430 = 439;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key430, value);
				int key431 = 440;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key431, value);
				int key432 = 441;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key432, value);
				int key433 = 442;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, -14f),
					Velocity = 0.05f,
					PortraitPositionYOverride = new float?(-30f)
				};
				dictionary.Add(key433, value);
				int key434 = 443;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key434, value);
				int key435 = 444;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, 2f),
					PortraitPositionYOverride = new float?(0f)
				};
				dictionary.Add(key435, value);
				int key436 = 448;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key436, value);
				int key437 = 449;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key437, value);
				int key438 = 450;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key438, value);
				int key439 = 451;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key439, value);
				int key440 = 452;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key440, value);
				int key441 = 453;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key441, value);
				int key442 = 454;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_454",
					Position = new Vector2(57f, 10f),
					PortraitPositionXOverride = new float?(5f),
					PortraitPositionYOverride = new float?(0f)
				};
				dictionary.Add(key442, value);
				int key443 = 455;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key443, value);
				int key444 = 456;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key444, value);
				int key445 = 457;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key445, value);
				int key446 = 458;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key446, value);
				int key447 = 459;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key447, value);
				int key448 = 460;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key448, value);
				int key449 = 461;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-2f, 0f),
					Velocity = 1f
				};
				dictionary.Add(key449, value);
				int key450 = 462;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key450, value);
				int key451 = 463;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key451, value);
				int key452 = 464;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key452, value);
				int key453 = 465;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 6f),
					PortraitPositionYOverride = new float?(6f),
					IsWet = true
				};
				dictionary.Add(key453, value);
				int key454 = 466;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key454, value);
				int key455 = 467;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key455, value);
				int key456 = 468;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key456, value);
				int key457 = 469;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key457, value);
				int key458 = 470;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key458, value);
				int key459 = 471;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key459, value);
				int key460 = 472;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 0f),
					PortraitPositionYOverride = new float?(-30f),
					SpriteDirection = new int?(-1),
					Velocity = 1f
				};
				dictionary.Add(key460, value);
				int key461 = 476;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key461, value);
				int key462 = 477;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(25f, 6f),
					PortraitPositionXOverride = new float?(10f)
				};
				dictionary.Add(key462, value);
				int key463 = 478;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key463, value);
				int key464 = 479;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(5f, 4f),
					PortraitPositionYOverride = new float?(-15f)
				};
				dictionary.Add(key464, value);
				int key465 = 481;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key465, value);
				int key466 = 482;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key466, value);
				int key467 = 483;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, -10f),
					PortraitPositionYOverride = new float?(-30f)
				};
				dictionary.Add(key467, value);
				int key468 = 484;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key468, value);
				int key469 = 487;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 0.5f
				};
				dictionary.Add(key469, value);
				int key470 = 486;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 0.5f
				};
				dictionary.Add(key470, value);
				int key471 = 485;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 0.5f
				};
				dictionary.Add(key471, value);
				int key472 = 489;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key472, value);
				int key473 = 491;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_491",
					Position = new Vector2(30f, -5f),
					Scale = 0.8f,
					PortraitPositionXOverride = new float?(1f),
					PortraitPositionYOverride = new float?(-1f)
				};
				dictionary.Add(key473, value);
				int key474 = 492;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key474, value);
				int key475 = 493;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, 44f),
					Scale = 0.4f,
					PortraitPositionXOverride = new float?(2f),
					PortraitPositionYOverride = new float?(134f)
				};
				dictionary.Add(key475, value);
				int key476 = 494;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key476, value);
				int key477 = 495;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-4f, 0f),
					Velocity = 1f
				};
				dictionary.Add(key477, value);
				int key478 = 496;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 0.5f
				};
				dictionary.Add(key478, value);
				int key479 = 497;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 0.5f
				};
				dictionary.Add(key479, value);
				int key480 = 498;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key480, value);
				int key481 = 499;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key481, value);
				int key482 = 500;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key482, value);
				int key483 = 501;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key483, value);
				int key484 = 502;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key484, value);
				int key485 = 503;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key485, value);
				int key486 = 504;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key486, value);
				int key487 = 505;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key487, value);
				int key488 = 506;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key488, value);
				int key489 = 507;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, 44f),
					Scale = 0.4f,
					PortraitPositionXOverride = new float?(2f),
					PortraitPositionYOverride = new float?(134f)
				};
				dictionary.Add(key489, value);
				int key490 = 508;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Position = new Vector2(10f, 0f),
					PortraitPositionXOverride = new float?(0f)
				};
				dictionary.Add(key490, value);
				int key491 = 509;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(6f, 0f),
					PortraitPositionXOverride = new float?(-10f),
					PortraitPositionYOverride = new float?(-20f)
				};
				dictionary.Add(key491, value);
				int key492 = 510;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_510",
					Position = new Vector2(55f, 18f),
					PortraitPositionXOverride = new float?(10f),
					PortraitPositionYOverride = new float?(12f)
				};
				dictionary.Add(key492, value);
				int key493 = 512;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key493, value);
				int key494 = 511;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key494, value);
				int key495 = 513;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_513",
					Position = new Vector2(37f, 24f),
					PortraitPositionXOverride = new float?(10f),
					PortraitPositionYOverride = new float?(17f)
				};
				dictionary.Add(key495, value);
				int key496 = 514;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key496, value);
				int key497 = 515;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key497, value);
				int key498 = 516;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key498, value);
				int key499 = 517;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, 44f),
					Scale = 0.4f,
					PortraitPositionXOverride = new float?(2f),
					PortraitPositionYOverride = new float?(135f)
				};
				dictionary.Add(key499, value);
				int key500 = 518;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-17f, 0f),
					Velocity = 1f
				};
				dictionary.Add(key500, value);
				int key501 = 519;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key501, value);
				int key502 = 520;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 56f),
					Velocity = 1f,
					PortraitPositionYOverride = new float?(10f)
				};
				dictionary.Add(key502, value);
				int key503 = 521;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(5f, 5f),
					PortraitPositionYOverride = new float?(-10f),
					SpriteDirection = new int?(-1),
					Velocity = 0.05f
				};
				dictionary.Add(key503, value);
				int key504 = 522;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key504, value);
				int key505 = 523;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key505, value);
				int key506 = 524;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key506, value);
				int key507 = 525;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key507, value);
				int key508 = 526;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key508, value);
				int key509 = 527;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key509, value);
				int key510 = 528;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key510, value);
				int key511 = 529;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key511, value);
				int key512 = 530;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key512, value);
				int key513 = 531;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 4f),
					Velocity = 2f,
					PortraitPositionYOverride = new float?(10f)
				};
				dictionary.Add(key513, value);
				int key514 = 532;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(6f, 0f),
					Velocity = 1f
				};
				dictionary.Add(key514, value);
				int key515 = 533;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(3f, 5f)
				};
				dictionary.Add(key515, value);
				int key516 = 534;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key516, value);
				int key517 = 536;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-2f, 0f),
					Velocity = 1f
				};
				dictionary.Add(key517, value);
				int key518 = 538;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key518, value);
				int key519 = 539;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key519, value);
				int key520 = 540;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key520, value);
				int key521 = 541;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 30f),
					PortraitPositionYOverride = new float?(0f)
				};
				dictionary.Add(key521, value);
				int key522 = 542;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(35f, -3f),
					PortraitPositionXOverride = new float?(0f)
				};
				dictionary.Add(key522, value);
				int key523 = 543;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(35f, -3f),
					PortraitPositionXOverride = new float?(0f)
				};
				dictionary.Add(key523, value);
				int key524 = 544;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(35f, -3f),
					PortraitPositionXOverride = new float?(0f)
				};
				dictionary.Add(key524, value);
				int key525 = 545;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(35f, -3f),
					PortraitPositionXOverride = new float?(0f)
				};
				dictionary.Add(key525, value);
				int key526 = 546;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, -3f),
					Direction = new int?(1)
				};
				dictionary.Add(key526, value);
				int key527 = 547;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key527, value);
				int key528 = 548;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key528, value);
				int key529 = 549;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key529, value);
				int key530 = 550;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					PortraitPositionXOverride = new float?(-2f)
				};
				dictionary.Add(key530, value);
				int key531 = 551;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(95f, -4f)
				};
				dictionary.Add(key531, value);
				int key532 = 552;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key532, value);
				int key533 = 553;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key533, value);
				int key534 = 554;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key534, value);
				int key535 = 555;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key535, value);
				int key536 = 556;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key536, value);
				int key537 = 557;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key537, value);
				int key538 = 558;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(3f, -2f)
				};
				dictionary.Add(key538, value);
				int key539 = 559;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(3f, -2f)
				};
				dictionary.Add(key539, value);
				int key540 = 560;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(3f, -2f)
				};
				dictionary.Add(key540, value);
				int key541 = 561;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key541, value);
				int key542 = 562;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key542, value);
				int key543 = 563;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key543, value);
				int key544 = 566;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-3f, 0f),
					Velocity = 1f
				};
				dictionary.Add(key544, value);
				int key545 = 567;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-3f, 0f),
					Velocity = 1f
				};
				dictionary.Add(key545, value);
				int key546 = 568;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key546, value);
				int key547 = 569;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key547, value);
				int key548 = 570;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(10f, 5f),
					Velocity = 1f,
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?(2f)
				};
				dictionary.Add(key548, value);
				int key549 = 571;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(10f, 5f),
					Velocity = 1f,
					PortraitPositionXOverride = new float?(0f),
					PortraitPositionYOverride = new float?(2f)
				};
				dictionary.Add(key549, value);
				int key550 = 572;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key550, value);
				int key551 = 573;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key551, value);
				int key552 = 578;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 4f)
				};
				dictionary.Add(key552, value);
				int key553 = 574;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(16f, 6f),
					Velocity = 1f
				};
				dictionary.Add(key553, value);
				int key554 = 575;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(16f, 6f),
					Velocity = 1f
				};
				dictionary.Add(key554, value);
				int key555 = 576;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(20f, 70f),
					Velocity = 1f,
					PortraitPositionXOverride = new float?(10f),
					PortraitPositionYOverride = new float?(0f),
					PortraitScale = new float?(0.75f)
				};
				dictionary.Add(key555, value);
				int key556 = 577;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(20f, 70f),
					Velocity = 1f,
					PortraitPositionXOverride = new float?(10f),
					PortraitPositionYOverride = new float?(0f),
					PortraitScale = new float?(0.75f)
				};
				dictionary.Add(key556, value);
				int key557 = 580;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 0f),
					Scale = 0.9f,
					Velocity = 1f
				};
				dictionary.Add(key557, value);
				int key558 = 581;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, -8f),
					PortraitPositionYOverride = new float?(-30f)
				};
				dictionary.Add(key558, value);
				int key559 = 582;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key559, value);
				int key560 = 585;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 1f),
					Direction = new int?(1)
				};
				dictionary.Add(key560, value);
				int key561 = 584;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 1f),
					Direction = new int?(1)
				};
				dictionary.Add(key561, value);
				int key562 = 583;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 1f),
					Direction = new int?(1)
				};
				dictionary.Add(key562, value);
				int key563 = 586;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key563, value);
				int key564 = 579;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key564, value);
				int key565 = 588;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					PortraitPositionXOverride = new float?(1f)
				};
				dictionary.Add(key565, value);
				int key566 = 587;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, -14f),
					PortraitPositionYOverride = new float?(-30f)
				};
				dictionary.Add(key566, value);
				int key567 = 591;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(9f, 0f)
				};
				dictionary.Add(key567, value);
				int key568 = 590;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					PortraitPositionXOverride = new float?(2f)
				};
				dictionary.Add(key568, value);
				int key569 = 592;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 6f),
					PortraitPositionYOverride = new float?(7f),
					IsWet = true
				};
				dictionary.Add(key569, value);
				int key570 = 593;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key570, value);
				int key571 = 594;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_594",
					Scale = 0.8f
				};
				dictionary.Add(key571, value);
				int key572 = 589;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key572, value);
				int key573 = 602;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key573, value);
				int key574 = 603;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key574, value);
				int key575 = 604;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, 22f),
					PortraitPositionYOverride = new float?(41f)
				};
				dictionary.Add(key575, value);
				int key576 = 605;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, 22f),
					PortraitPositionYOverride = new float?(41f)
				};
				dictionary.Add(key576, value);
				int key577 = 606;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key577, value);
				int key578 = 607;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 6f),
					PortraitPositionYOverride = new float?(7f),
					IsWet = true
				};
				dictionary.Add(key578, value);
				int key579 = 608;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key579, value);
				int key580 = 609;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key580, value);
				int key581 = 611;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 0f),
					Direction = new int?(-1),
					SpriteDirection = new int?(1)
				};
				dictionary.Add(key581, value);
				int key582 = 612;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key582, value);
				int key583 = 613;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key583, value);
				int key584 = 614;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key584, value);
				int key585 = 615;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 10f),
					Scale = 0.88f,
					PortraitPositionYOverride = new float?(20f),
					IsWet = true
				};
				dictionary.Add(key585, value);
				int key586 = 616;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key586, value);
				int key587 = 617;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key587, value);
				int key588 = 618;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(12f, -5f),
					Scale = 0.9f,
					PortraitPositionYOverride = new float?(0f)
				};
				dictionary.Add(key588, value);
				int key589 = 619;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, 7f),
					Scale = 0.85f,
					PortraitPositionYOverride = new float?(10f)
				};
				dictionary.Add(key589, value);
				int key590 = 620;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(6f, 5f),
					Scale = 0.78f,
					Velocity = 1f
				};
				dictionary.Add(key590, value);
				int key591 = 621;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					CustomTexturePath = "Images/UI/Bestiary/NPCs/NPC_621",
					Position = new Vector2(46f, 20f),
					PortraitPositionXOverride = new float?(10f),
					PortraitPositionYOverride = new float?(17f)
				};
				dictionary.Add(key591, value);
				int key592 = 622;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key592, value);
				int key593 = 623;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key593, value);
				int key594 = 624;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 2f
				};
				dictionary.Add(key594, value);
				int key595 = 625;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, -12f),
					Velocity = 1f
				};
				dictionary.Add(key595, value);
				int key596 = 626;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, -16f)
				};
				dictionary.Add(key596, value);
				int key597 = 627;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, -16f)
				};
				dictionary.Add(key597, value);
				int key598 = 628;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Direction = new int?(1)
				};
				dictionary.Add(key598, value);
				int key599 = 630;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 0.5f
				};
				dictionary.Add(key599, value);
				int key600 = 632;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key600, value);
				int key601 = 631;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 0.75f
				};
				dictionary.Add(key601, value);
				int key602 = 634;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					Position = new Vector2(0f, -13f),
					PortraitPositionYOverride = new float?(-30f)
				};
				dictionary.Add(key602, value);
				int key603 = 635;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key603, value);
				int key604 = 636;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 50f),
					PortraitPositionYOverride = new float?(30f)
				};
				dictionary.Add(key604, value);
				int key605 = 639;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key605, value);
				int key606 = 640;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key606, value);
				int key607 = 641;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key607, value);
				int key608 = 642;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key608, value);
				int key609 = 643;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key609, value);
				int key610 = 644;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key610, value);
				int key611 = 645;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key611, value);
				int key612 = 646;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key612, value);
				int key613 = 647;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key613, value);
				int key614 = 648;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key614, value);
				int key615 = 649;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key615, value);
				int key616 = 650;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key616, value);
				int key617 = 651;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key617, value);
				int key618 = 652;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key618, value);
				int key619 = 637;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 0.25f
				};
				dictionary.Add(key619, value);
				int key620 = 638;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key620, value);
				int key621 = 653;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, 3f),
					PortraitPositionYOverride = new float?(1f)
				};
				dictionary.Add(key621, value);
				int key622 = 654;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, 2f)
				};
				dictionary.Add(key622, value);
				int key623 = 655;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, 17f),
					PortraitPositionYOverride = new float?(39f)
				};
				dictionary.Add(key623, value);
				int key624 = 656;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f
				};
				dictionary.Add(key624, value);
				int key625 = 657;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(0f, 60f),
					PortraitPositionYOverride = new float?(40f)
				};
				dictionary.Add(key625, value);
				int key626 = 660;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(-2f, -4f),
					PortraitPositionYOverride = new float?(-20f)
				};
				dictionary.Add(key626, value);
				int key627 = 661;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, 3f),
					PortraitPositionYOverride = new float?(1f)
				};
				dictionary.Add(key627, value);
				int key628 = 662;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key628, value);
				int key629 = 663;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 1f,
					PortraitPositionXOverride = new float?(1f)
				};
				dictionary.Add(key629, value);
				dictionary.Add(664, new NPCID.Sets.NPCBestiaryDrawModifiers(0));
				int key630 = 667;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key630, value);
				int key631 = 668;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 2.5f,
					Position = new Vector2(36f, 40f),
					Scale = 0.9f,
					PortraitPositionXOverride = new float?(6f),
					PortraitPositionYOverride = new float?(50f)
				};
				dictionary.Add(key631, value);
				int key632 = 669;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(2f, 22f),
					PortraitPositionYOverride = new float?(41f)
				};
				dictionary.Add(key632, value);
				int key633 = 670;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					SpriteDirection = new int?(1),
					Velocity = 0.7f
				};
				dictionary.Add(key633, value);
				int key634 = 678;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					SpriteDirection = new int?(1),
					Velocity = 0.7f
				};
				dictionary.Add(key634, value);
				int key635 = 679;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					SpriteDirection = new int?(1),
					Velocity = 0.7f
				};
				dictionary.Add(key635, value);
				int key636 = 680;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					SpriteDirection = new int?(1),
					Velocity = 0.7f
				};
				dictionary.Add(key636, value);
				int key637 = 681;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					SpriteDirection = new int?(1),
					Velocity = 0.7f
				};
				dictionary.Add(key637, value);
				int key638 = 682;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					SpriteDirection = new int?(1),
					Velocity = 0.7f
				};
				dictionary.Add(key638, value);
				int key639 = 683;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					SpriteDirection = new int?(1),
					Velocity = 0.7f
				};
				dictionary.Add(key639, value);
				int key640 = 684;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					SpriteDirection = new int?(1),
					Velocity = 0.7f
				};
				dictionary.Add(key640, value);
				int key641 = 671;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, -18f),
					Velocity = 0.05f,
					PortraitPositionYOverride = new float?(-35f)
				};
				dictionary.Add(key641, value);
				int key642 = 672;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, -18f),
					Velocity = 0.05f,
					PortraitPositionYOverride = new float?(-35f)
				};
				dictionary.Add(key642, value);
				int key643 = 673;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, -16f),
					Velocity = 0.05f,
					PortraitPositionYOverride = new float?(-35f)
				};
				dictionary.Add(key643, value);
				int key644 = 674;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, -16f),
					Velocity = 0.05f,
					PortraitPositionYOverride = new float?(-35f)
				};
				dictionary.Add(key644, value);
				int key645 = 675;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, -16f),
					Velocity = 0.05f,
					PortraitPositionYOverride = new float?(-35f)
				};
				dictionary.Add(key645, value);
				int key646 = 677;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Position = new Vector2(1f, 2f)
				};
				dictionary.Add(key646, value);
				int key647 = 685;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key647, value);
				int key648 = 686;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key648, value);
				int key649 = 687;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Velocity = 0f
				};
				dictionary.Add(key649, value);
				int key650 = 0;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key650, value);
				int key651 = 488;
				value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
				{
					Hide = true
				};
				dictionary.Add(key651, value);
				return dictionary;
			}

			// Token: 0x06005C80 RID: 23680 RVA: 0x006BA0B0 File Offset: 0x006B82B0
			static Sets()
			{
				NPCID.Sets.ImmuneToAllBuffs = NPCID.Sets.Factory.CreateBoolSet(Array.Empty<int>());
				NPCID.Sets.ImmuneToRegularBuffs = NPCID.Sets.Factory.CreateBoolSet(Array.Empty<int>());
				NPCID.Sets.SpecificDebuffImmunity = NPCID.Sets.Factory.CreateCustomSet<bool?[]>(null, Array.Empty<object>());
				for (int type = 0; type < NPCLoader.NPCCount; type++)
				{
					NPCID.Sets.SpecificDebuffImmunity[type] = new bool?[BuffLoader.BuffCount];
					NPCDebuffImmunityData data;
					if (NPCID.Sets.DebuffImmunitySets.TryGetValue(type, out data) && data != null)
					{
						NPCID.Sets.ImmuneToAllBuffs[type] = (data.ImmuneToAllBuffsThatAreNotWhips && data.ImmuneToWhips);
						NPCID.Sets.ImmuneToRegularBuffs[type] = data.ImmuneToAllBuffsThatAreNotWhips;
						if (data.SpecificallyImmuneTo != null)
						{
							foreach (int buff in data.SpecificallyImmuneTo)
							{
								NPCID.Sets.SpecificDebuffImmunity[type][buff] = new bool?(true);
							}
						}
					}
					NPCID.Sets.SpecificDebuffImmunity[type][353] = new bool?(NPCID.Sets.ShimmerImmunity[type]);
				}
			}

			// Token: 0x06005C81 RID: 23681 RVA: 0x006C0916 File Offset: 0x006BEB16
			[CompilerGenerated]
			internal static NPCID.Sets.NPCBestiaryDrawModifiers <NPCBestiaryDrawOffsetCreation>g__FilterPaths|66_0(NPCID.Sets.NPCBestiaryDrawModifiers modifiers)
			{
				if (modifiers.CustomTexturePath != null)
				{
					modifiers.CustomTexturePath = "Terraria/" + modifiers.CustomTexturePath;
				}
				return modifiers;
			}

			// Token: 0x04007490 RID: 29840
			public static SetFactory Factory = new SetFactory(NPCLoader.NPCCount, "NPCID", NPCID.Search);

			/// <summary>
			/// Determines the special spawning rules for an NPC to use when respawning using the coin loss system (<see cref="T:Terraria.GameContent.CoinLossRevengeSystem" />).
			/// <br /> If <c>0</c> for a given NPC type (<see cref="F:Terraria.NPC.type" />), then <c><see cref="F:Terraria.NPC.ai" />[0]</c> and <c><see cref="F:Terraria.NPC.ai" />[1]</c> are set to this <see cref="T:Terraria.NPC" />'s position in tile coordinates.
			/// </summary>
			/// <remarks>All vanilla entries in this set use <c>0</c>. All vanilla entries in this set are tethered to tiles (<see cref="F:Terraria.ID.NPCID.ManEater" />, <see cref="F:Terraria.ID.NPCID.Snatcher" />, etc.).</remarks>
			// Token: 0x04007491 RID: 29841
			public static Dictionary<int, int> SpecialSpawningRules = new Dictionary<int, int>
			{
				{
					259,
					0
				},
				{
					260,
					0
				},
				{
					175,
					0
				},
				{
					43,
					0
				},
				{
					56,
					0
				},
				{
					101,
					0
				}
			};

			/// <summary>
			/// The settings to use for a given NPC type's (<see cref="F:Terraria.NPC.type" />) Bestiary drawing.
			/// </summary>
			// Token: 0x04007492 RID: 29842
			public static Dictionary<int, NPCID.Sets.NPCBestiaryDrawModifiers> NPCBestiaryDrawOffset = NPCID.Sets.NPCBestiaryDrawOffsetCreation();

			/// <summary>
			/// <b>Unused:</b> Replaced by <see cref="F:Terraria.ID.NPCID.Sets.SpecificDebuffImmunity" />, <see cref="F:Terraria.ID.NPCID.Sets.ImmuneToAllBuffs" />, and <see cref="F:Terraria.ID.NPCID.Sets.ImmuneToRegularBuffs" /><br /><br />
			/// The default debuff immunities for a given NPC type (<see cref="F:Terraria.NPC.type" />).
			/// </summary>
			/// <remarks><see cref="F:Terraria.NPC.buffImmune" /> can still be manually changed to grant temporary immunity.</remarks>
			// Token: 0x04007493 RID: 29843
			private static Dictionary<int, NPCDebuffImmunityData> DebuffImmunitySets = new Dictionary<int, NPCDebuffImmunityData>
			{
				{
					0,
					null
				},
				{
					1,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					2,
					null
				},
				{
					3,
					null
				},
				{
					4,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					5,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					6,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					7,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					8,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					9,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					10,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					11,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					12,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					13,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					14,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					15,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					16,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					17,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					18,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					19,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					20,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					21,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					22,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					23,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							24,
							31,
							323
						}
					}
				},
				{
					24,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							24,
							31,
							323
						}
					}
				},
				{
					25,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					26,
					null
				},
				{
					27,
					null
				},
				{
					28,
					null
				},
				{
					29,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					30,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					665,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					31,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					32,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					33,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					34,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					35,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31,
							169,
							337,
							344
						}
					}
				},
				{
					36,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					37,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					38,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					39,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							24,
							31,
							323
						}
					}
				},
				{
					40,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							24,
							31,
							323
						}
					}
				},
				{
					41,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							24,
							31,
							323
						}
					}
				},
				{
					42,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					43,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					44,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					45,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					46,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					47,
					null
				},
				{
					48,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					49,
					null
				},
				{
					50,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					51,
					null
				},
				{
					52,
					null
				},
				{
					53,
					null
				},
				{
					54,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					55,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					56,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					57,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					58,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					59,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							24,
							323
						}
					}
				},
				{
					60,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							24,
							323
						}
					}
				},
				{
					61,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					62,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							24,
							31,
							153,
							323
						}
					}
				},
				{
					63,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					64,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					65,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					66,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							24,
							31,
							153,
							323
						}
					}
				},
				{
					67,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					68,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true,
						ImmuneToWhips = true
					}
				},
				{
					69,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					70,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							24,
							31,
							39,
							70,
							323
						}
					}
				},
				{
					71,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					72,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true,
						ImmuneToWhips = true
					}
				},
				{
					73,
					null
				},
				{
					74,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					75,
					null
				},
				{
					76,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					77,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					78,
					null
				},
				{
					79,
					null
				},
				{
					80,
					null
				},
				{
					81,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					82,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{
					83,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							24,
							31,
							323
						}
					}
				},
				{
					84,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							24,
							31,
							323
						}
					}
				},
				{
					85,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							24,
							31,
							323
						}
					}
				},
				{
					86,
					null
				},
				{
					87,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					88,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					89,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					90,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					91,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					92,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					93,
					null
				},
				{
					94,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					95,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					96,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					97,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					98,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					99,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					100,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					101,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							39,
							31
						}
					}
				},
				{
					102,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					103,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					104,
					null
				},
				{
					105,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					106,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					107,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					108,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					109,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					110,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					111,
					null
				},
				{
					112,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					666,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					113,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							24,
							31,
							323
						}
					}
				},
				{
					114,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							24,
							31,
							323
						}
					}
				},
				{
					115,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					116,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					117,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					118,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					119,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					120,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					121,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					122,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					123,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					124,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					125,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					126,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					127,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31,
							169,
							337,
							344
						}
					}
				},
				{
					128,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					129,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					130,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					131,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					132,
					null
				},
				{
					133,
					null
				},
				{
					134,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{
					135,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{
					136,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{
					137,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					138,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					139,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{
					140,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							24,
							323
						}
					}
				},
				{
					141,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							70
						}
					}
				},
				{
					142,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					143,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							44,
							324
						}
					}
				},
				{
					144,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							44,
							324
						}
					}
				},
				{
					145,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							44,
							324
						}
					}
				},
				{
					146,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					147,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							44,
							324
						}
					}
				},
				{
					148,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					149,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					150,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							44,
							324
						}
					}
				},
				{
					151,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							24,
							323
						}
					}
				},
				{
					152,
					null
				},
				{
					153,
					null
				},
				{
					154,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							44,
							324
						}
					}
				},
				{
					155,
					null
				},
				{
					156,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							24,
							31,
							153,
							323
						}
					}
				},
				{
					157,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					158,
					null
				},
				{
					159,
					null
				},
				{
					160,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					161,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							44,
							324
						}
					}
				},
				{
					162,
					null
				},
				{
					163,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					164,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					165,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					166,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					167,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							44,
							324
						}
					}
				},
				{
					168,
					null
				},
				{
					169,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31,
							44,
							324
						}
					}
				},
				{
					170,
					null
				},
				{
					171,
					null
				},
				{
					172,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					173,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					174,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					175,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					176,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					177,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					178,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					179,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							24,
							31,
							323
						}
					}
				},
				{
					180,
					null
				},
				{
					181,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					182,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					183,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					184,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							44,
							324
						}
					}
				},
				{
					185,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							44,
							324
						}
					}
				},
				{
					186,
					null
				},
				{
					187,
					null
				},
				{
					188,
					null
				},
				{
					189,
					null
				},
				{
					190,
					null
				},
				{
					191,
					null
				},
				{
					192,
					null
				},
				{
					193,
					null
				},
				{
					194,
					null
				},
				{
					195,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					196,
					null
				},
				{
					197,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							44,
							324
						}
					}
				},
				{
					198,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					199,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					200,
					null
				},
				{
					201,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					202,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					203,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					204,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					205,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					206,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							44,
							324
						}
					}
				},
				{
					207,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					208,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					209,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					210,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					211,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					212,
					null
				},
				{
					213,
					null
				},
				{
					214,
					null
				},
				{
					215,
					null
				},
				{
					216,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					217,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					218,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					219,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					220,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					221,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					222,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					223,
					null
				},
				{
					224,
					null
				},
				{
					225,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					226,
					null
				},
				{
					227,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					228,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					229,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					230,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					231,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					232,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					233,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					234,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					235,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					236,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					237,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					238,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					239,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					240,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					241,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					242,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					243,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31,
							44,
							324
						}
					}
				},
				{
					244,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					245,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							24,
							31,
							323
						}
					}
				},
				{
					246,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							24,
							31,
							323
						}
					}
				},
				{
					247,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							24,
							31,
							323
						}
					}
				},
				{
					248,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							24,
							31,
							323
						}
					}
				},
				{
					249,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							24,
							31,
							323
						}
					}
				},
				{
					250,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					251,
					null
				},
				{
					252,
					null
				},
				{
					253,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{
					254,
					null
				},
				{
					255,
					null
				},
				{
					256,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					257,
					null
				},
				{
					258,
					null
				},
				{
					259,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					260,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					261,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					262,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					263,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					264,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					265,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					266,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					267,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					268,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							69
						}
					}
				},
				{
					269,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					270,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					271,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					272,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					273,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					274,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					275,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					276,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					277,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							24,
							323
						}
					}
				},
				{
					278,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							24,
							323
						}
					}
				},
				{
					279,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							24,
							323
						}
					}
				},
				{
					280,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							24,
							323
						}
					}
				},
				{
					281,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					282,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					283,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					284,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					285,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					286,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					287,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					288,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{
					289,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					290,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31,
							69
						}
					}
				},
				{
					291,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					292,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					293,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					294,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					295,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					296,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					297,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					298,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					671,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					672,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					673,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					674,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					675,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					299,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					300,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					301,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					302,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					303,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					304,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					305,
					null
				},
				{
					306,
					null
				},
				{
					307,
					null
				},
				{
					308,
					null
				},
				{
					309,
					null
				},
				{
					310,
					null
				},
				{
					311,
					null
				},
				{
					312,
					null
				},
				{
					313,
					null
				},
				{
					314,
					null
				},
				{
					315,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							24,
							31,
							323
						}
					}
				},
				{
					316,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							24,
							31,
							39,
							44,
							69,
							70,
							153,
							189,
							203,
							204,
							323,
							324
						}
					}
				},
				{
					317,
					null
				},
				{
					318,
					null
				},
				{
					319,
					null
				},
				{
					320,
					null
				},
				{
					321,
					null
				},
				{
					322,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					323,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					324,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					325,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					326,
					null
				},
				{
					327,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							24,
							31,
							323
						}
					}
				},
				{
					328,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							24,
							31,
							323
						}
					}
				},
				{
					329,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							24,
							323
						}
					}
				},
				{
					330,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{
					331,
					null
				},
				{
					332,
					null
				},
				{
					333,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					334,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					335,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					336,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					337,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					338,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							44,
							324
						}
					}
				},
				{
					339,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							44,
							324
						}
					}
				},
				{
					340,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							44,
							324
						}
					}
				},
				{
					341,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							24,
							31,
							323
						}
					}
				},
				{
					342,
					null
				},
				{
					343,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							44,
							324
						}
					}
				},
				{
					344,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31,
							44,
							324
						}
					}
				},
				{
					345,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31,
							44,
							324
						}
					}
				},
				{
					346,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31,
							44,
							324
						}
					}
				},
				{
					347,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					348,
					null
				},
				{
					349,
					null
				},
				{
					350,
					null
				},
				{
					351,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							44,
							324
						}
					}
				},
				{
					352,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31,
							44,
							324
						}
					}
				},
				{
					353,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					354,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					355,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					356,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					357,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					358,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					359,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					360,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					361,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					362,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					363,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					364,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					365,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					366,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					367,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					368,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					369,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					370,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					371,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					372,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					373,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					374,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					375,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					376,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					377,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					378,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					379,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					380,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					381,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					382,
					null
				},
				{
					383,
					null
				},
				{
					384,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{
					385,
					null
				},
				{
					386,
					null
				},
				{
					387,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					388,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					389,
					null
				},
				{
					390,
					null
				},
				{
					391,
					null
				},
				{
					392,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true,
						ImmuneToWhips = true
					}
				},
				{
					393,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{
					394,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{
					395,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{
					396,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					397,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					398,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					399,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					400,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					401,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					402,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					403,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					404,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					405,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					406,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					407,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					408,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					409,
					null
				},
				{
					410,
					null
				},
				{
					411,
					null
				},
				{
					412,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					413,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					414,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					415,
					null
				},
				{
					416,
					null
				},
				{
					417,
					null
				},
				{
					418,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							24,
							31,
							323
						}
					}
				},
				{
					419,
					null
				},
				{
					420,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					421,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					422,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{
					423,
					null
				},
				{
					424,
					null
				},
				{
					425,
					null
				},
				{
					426,
					null
				},
				{
					427,
					null
				},
				{
					428,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					429,
					null
				},
				{
					430,
					null
				},
				{
					431,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							44,
							324
						}
					}
				},
				{
					432,
					null
				},
				{
					433,
					null
				},
				{
					434,
					null
				},
				{
					435,
					null
				},
				{
					436,
					null
				},
				{
					437,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					438,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					439,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{
					440,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true,
						ImmuneToWhips = true
					}
				},
				{
					441,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					442,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					443,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					444,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					445,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					446,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					447,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					448,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					449,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					450,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					451,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					452,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					453,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					454,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{
					455,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{
					456,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{
					457,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{
					458,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{
					459,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{
					460,
					null
				},
				{
					461,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					462,
					null
				},
				{
					463,
					null
				},
				{
					464,
					null
				},
				{
					465,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					466,
					null
				},
				{
					467,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							24,
							31,
							323
						}
					}
				},
				{
					468,
					null
				},
				{
					469,
					null
				},
				{
					470,
					null
				},
				{
					471,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31,
							153
						}
					}
				},
				{
					472,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31,
							153
						}
					}
				},
				{
					473,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							24,
							31,
							323
						}
					}
				},
				{
					474,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							24,
							31,
							323
						}
					}
				},
				{
					475,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							24,
							31,
							323
						}
					}
				},
				{
					476,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							24,
							31,
							323
						}
					}
				},
				{
					477,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					478,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					479,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					480,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					481,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					482,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							24,
							31,
							323
						}
					}
				},
				{
					483,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							24,
							31,
							323
						}
					}
				},
				{
					484,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					485,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					486,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					487,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					488,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					489,
					null
				},
				{
					490,
					null
				},
				{
					491,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true,
						ImmuneToWhips = true
					}
				},
				{
					492,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{
					493,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{
					494,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					495,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					496,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					497,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					498,
					null
				},
				{
					499,
					null
				},
				{
					500,
					null
				},
				{
					501,
					null
				},
				{
					502,
					null
				},
				{
					503,
					null
				},
				{
					504,
					null
				},
				{
					505,
					null
				},
				{
					506,
					null
				},
				{
					507,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{
					508,
					null
				},
				{
					509,
					null
				},
				{
					510,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					511,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					512,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					513,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					514,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					515,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					516,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					517,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{
					518,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					519,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					520,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							24,
							31,
							323
						}
					}
				},
				{
					521,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							24,
							31,
							39,
							44,
							69,
							70,
							153,
							189,
							203,
							204,
							323,
							324
						}
					}
				},
				{
					522,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					523,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					524,
					null
				},
				{
					525,
					null
				},
				{
					526,
					null
				},
				{
					527,
					null
				},
				{
					528,
					null
				},
				{
					529,
					null
				},
				{
					530,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					531,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					532,
					null
				},
				{
					533,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							24,
							31,
							39,
							44,
							69,
							70,
							153,
							189,
							203,
							204,
							323,
							324
						}
					}
				},
				{
					534,
					null
				},
				{
					535,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					536,
					null
				},
				{
					537,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					538,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					539,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					540,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					541,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					542,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					543,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					544,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					545,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					546,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					547,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					548,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					549,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true,
						ImmuneToWhips = true
					}
				},
				{
					550,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					551,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							24,
							31,
							323
						}
					}
				},
				{
					552,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					553,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					554,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					555,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					556,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					557,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					558,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					559,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					560,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					561,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					562,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					563,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					564,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					565,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					566,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					567,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					568,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					569,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					570,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					571,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					572,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					573,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					574,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					575,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					576,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					577,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					578,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					579,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					580,
					null
				},
				{
					581,
					null
				},
				{
					582,
					null
				},
				{
					583,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true,
						ImmuneToWhips = true
					}
				},
				{
					584,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true,
						ImmuneToWhips = true
					}
				},
				{
					585,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true,
						ImmuneToWhips = true
					}
				},
				{
					586,
					null
				},
				{
					587,
					null
				},
				{
					588,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					589,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					590,
					null
				},
				{
					591,
					null
				},
				{
					592,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					593,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					594,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					595,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					596,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					597,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					598,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					599,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					600,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					601,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					602,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					603,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					604,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					605,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					606,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					607,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					608,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					609,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					610,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					611,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					612,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					613,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					614,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					615,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					616,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					617,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					618,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					619,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					620,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					621,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					622,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					623,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					624,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					625,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					626,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					627,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					628,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					629,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							24,
							31,
							44,
							323,
							324
						}
					}
				},
				{
					630,
					null
				},
				{
					631,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							24,
							31,
							323
						}
					}
				},
				{
					632,
					null
				},
				{
					633,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					634,
					null
				},
				{
					635,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20
						}
					}
				},
				{
					636,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					637,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					638,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					639,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					640,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					641,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					642,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					643,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					644,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					645,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					646,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					647,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					648,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					649,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					650,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					651,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					652,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					653,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					654,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					655,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					656,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					657,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					658,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					659,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					660,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							20,
							31
						}
					}
				},
				{
					661,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					662,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true
					}
				},
				{
					663,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					668,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					669,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					670,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					678,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					679,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					680,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					681,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					682,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					683,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					684,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					677,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					685,
					new NPCDebuffImmunityData
					{
						SpecificallyImmuneTo = new int[]
						{
							31
						}
					}
				},
				{
					686,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true,
						ImmuneToWhips = true
					}
				},
				{
					687,
					new NPCDebuffImmunityData
					{
						ImmuneToAllBuffsThatAreNotWhips = true,
						ImmuneToWhips = true
					}
				}
			};

			/// <summary>
			/// The order of critter NPC types (<see cref="F:Terraria.NPC.type" />) in the Bestiary.
			/// </summary>
			// Token: 0x04007494 RID: 29844
			public static List<int> NormalGoldCritterBestiaryPriority = new List<int>
			{
				46,
				540,
				614,
				303,
				337,
				443,
				74,
				297,
				298,
				671,
				672,
				673,
				674,
				675,
				442,
				55,
				230,
				592,
				593,
				299,
				538,
				539,
				300,
				447,
				361,
				445,
				377,
				446,
				356,
				444,
				357,
				448,
				595,
				596,
				597,
				598,
				599,
				600,
				601,
				626,
				627,
				612,
				613,
				604,
				605,
				669,
				677
			};

			/// <summary>
			/// The order of boss NPC types (<see cref="F:Terraria.NPC.type" />) in the Bestiary.
			/// </summary>
			// Token: 0x04007495 RID: 29845
			public static List<int> BossBestiaryPriority = new List<int>
			{
				664,
				4,
				5,
				50,
				535,
				13,
				14,
				15,
				266,
				267,
				668,
				35,
				36,
				222,
				113,
				114,
				117,
				115,
				116,
				657,
				658,
				659,
				660,
				125,
				126,
				134,
				135,
				136,
				139,
				127,
				128,
				131,
				129,
				130,
				262,
				263,
				264,
				636,
				245,
				246,
				249,
				247,
				248,
				370,
				372,
				373,
				439,
				438,
				379,
				380,
				440,
				521,
				454,
				507,
				517,
				422,
				493,
				398,
				396,
				397,
				400,
				401
			};

			/// <summary>
			/// The order of town NPC NPC types (<see cref="F:Terraria.NPC.type" />) in the Bestiary.
			/// </summary>
			// Token: 0x04007496 RID: 29846
			public static List<int> TownNPCBestiaryPriority = new List<int>
			{
				22,
				17,
				18,
				38,
				369,
				20,
				19,
				207,
				227,
				353,
				633,
				550,
				588,
				107,
				228,
				124,
				54,
				108,
				178,
				229,
				160,
				441,
				209,
				208,
				663,
				142,
				637,
				638,
				656,
				670,
				678,
				679,
				680,
				681,
				682,
				683,
				684,
				368,
				453,
				37,
				687
			};

			/// <summary>
			/// If <see langword="true" /> for a given NPC type (<see cref="F:Terraria.NPC.type" />), then that NPC will not have its stats increased in <see cref="P:Terraria.Main.expertMode" /> or higher difficulties.
			/// <br /> Defaults to <see langword="false" />.
			/// </summary>
			// Token: 0x04007497 RID: 29847
			public static bool[] DontDoHardmodeScaling = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				5,
				13,
				14,
				15,
				267,
				113,
				114,
				115,
				116,
				117,
				118,
				119,
				658,
				659,
				660,
				400,
				522
			});

			/// <summary>
			/// If <see langword="true" /> for a given NPC type (<see cref="F:Terraria.NPC.type" />), then that NPC will reflect <see cref="F:Terraria.ID.ProjectileID.StarCannonStar" /> and <see cref="F:Terraria.ID.ProjectileID.SuperStar" /> on the "For the Worthy" secret seed.
			/// <br /> Defaults to <see langword="false" />.
			/// </summary>
			// Token: 0x04007498 RID: 29848
			public static bool[] ReflectStarShotsInForTheWorthy = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				4,
				5,
				13,
				14,
				15,
				266,
				267,
				35,
				36,
				113,
				114,
				115,
				116,
				117,
				118,
				119,
				125,
				126,
				134,
				135,
				136,
				139,
				127,
				128,
				131,
				129,
				130,
				262,
				263,
				264,
				245,
				247,
				248,
				246,
				249,
				398,
				400,
				397,
				396,
				401
			});

			/// <summary>
			/// If <see langword="true" /> for a given NPC type (<see cref="F:Terraria.NPC.type" />), then that NPC is categorized as a town pet.
			/// <br /> Town pets must have <c><see cref="F:Terraria.NPC.aiStyle" /> == <see cref="F:Terraria.ID.NPCAIStyleID.Passive" /></c> to function properly.
			/// <br /> Town pets cannot party, can be pet, can be moved into valid rooms even if they contain a stinkbug, and "leave" on death.
			/// <br /> Defaults to <see langword="false" />.
			/// </summary>
			// Token: 0x04007499 RID: 29849
			public static bool[] IsTownPet = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				637,
				638,
				656,
				670,
				678,
				679,
				680,
				681,
				682,
				683,
				684
			});

			/// <summary>
			/// If <see langword="true" /> for a given NPC type (<see cref="F:Terraria.NPC.type" />), then that NPC is categorized as a town slime.
			/// <br /> Town slimes must have <c><see cref="F:Terraria.NPC.aiStyle" /> == <see cref="F:Terraria.ID.NPCAIStyleID.Passive" /></c> to function properly. Additionally, they should also be in the <see cref="F:Terraria.ID.NPCID.Sets.IsTownPet" /> set.
			/// <br /> Town slimes cannot sit on chairs, will try to play their idle animations more often, and have horizontally flipped sprites compared to normal town NPCs.
			/// <br /> Defaults to <see langword="false" />.
			/// </summary>
			// Token: 0x0400749A RID: 29850
			public static bool[] IsTownSlime = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				670,
				678,
				679,
				680,
				681,
				682,
				683,
				684
			});

			/// <summary>
			/// If <see langword="true" /> for a given NPC type (<see cref="F:Terraria.NPC.type" />), then that NPC can pick up <see cref="F:Terraria.ID.ItemID.CopperShortsword" /> or <see cref="F:Terraria.ID.ItemID.CopperHelmet" /> to become <see cref="F:Terraria.ID.NPCID.TownSlimeCopper" />.
			/// <br /> Defaults to <see langword="false" />.
			/// </summary>
			// Token: 0x0400749B RID: 29851
			public static bool[] CanConvertIntoCopperSlimeTownNPC = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				1,
				302,
				335,
				336,
				333,
				334
			});

			/// <summary>
			/// A list of all NPC types (<see cref="F:Terraria.NPC.type" />) categorized as gold critters.
			/// <br /> If a gold critter shows up on the Lifeform Analyzer, its name will be gold (<see cref="M:Terraria.Main.DrawInfoAccs_AdjustInfoTextColorsForNPC(Terraria.NPC,Microsoft.Xna.Framework.Color@,Microsoft.Xna.Framework.Color@)" />).
			/// </summary>
			// Token: 0x0400749C RID: 29852
			public static List<int> GoldCrittersCollection = new List<int>
			{
				443,
				442,
				592,
				593,
				444,
				601,
				445,
				446,
				605,
				447,
				627,
				613,
				448,
				539
			};

			/// <summary>
			/// If <see langword="true" /> for a given NPC type (<see cref="F:Terraria.NPC.type" />), then that NPC, if currently <see cref="F:Terraria.NPC.dontTakeDamage" />, will damage the player when hit with a melee attack, a whip, certain <see cref="T:Terraria.ID.ProjAIStyleID" />s (<see cref="F:Terraria.ID.ProjAIStyleID.Spear" />, <see cref="F:Terraria.ID.ProjAIStyleID.ShortSword" />, <see cref="F:Terraria.ID.ProjAIStyleID.SleepyOctopod" />, <see cref="F:Terraria.ID.ProjAIStyleID.HeldProjectile" />), or by any projectile type (<see cref="F:Terraria.Projectile.type" />) in the <see cref="F:Terraria.ID.ProjectileID.Sets.AllowsContactDamageFromJellyfish" /> or <see cref="F:Terraria.ID.ProjectileID.Sets.IsAWhip" /> sets.
			/// <br /> Defaults to <see langword="false" />.
			/// </summary>
			// Token: 0x0400749D RID: 29853
			public static bool[] ZappingJellyfish = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				63,
				64,
				103,
				242
			});

			/// <summary>
			/// If <see langword="true" /> for a given NPC type (<see cref="F:Terraria.NPC.type" />), then that NPC cannot pick up dropped coins in Expert Mode.
			/// <para /> Vanilla entries include mainly boss summons and other NPC that shouldn't be able to respawn naturally. See also <see cref="F:Terraria.ID.NPCID.Sets.RespawnEnemyID" />.
			/// <para /> Defaults to <see langword="false" />.
			/// </summary>
			// Token: 0x0400749E RID: 29854
			public static bool[] CantTakeLunchMoney = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				394,
				393,
				392,
				492,
				491,
				662,
				384,
				478,
				535,
				658,
				659,
				660,
				128,
				131,
				129,
				130,
				139,
				267,
				247,
				248,
				246,
				249,
				245,
				409,
				410,
				397,
				396,
				401,
				400,
				440,
				68,
				534
			});

			/// <summary>
			/// Associates an NPC type (<see cref="F:Terraria.NPC.type" />) with the NPC type it respawns as from the coin loss system (<see cref="T:Terraria.GameContent.CoinLossRevengeSystem" />).
			/// <br /> If an NPC type is not a key in this dictionary, then that NPC respawns as itself.
			/// <br /> If the value for an NPC type is <c>0</c>, then that NPC won't be cached (<see cref="M:Terraria.GameContent.CoinLossRevengeSystem.CacheEnemy(Terraria.NPC)" />).
			/// <para /> Some vanilla entries contain 0 values for special enemies that shouldn't respawn and other entries contain values indicating the "head" NPC of worm enemies to allow them to spawn the worm correctly. (Worm enemies currently don't work with the coin loss revenge system, however.) See also <see cref="F:Terraria.ID.NPCID.Sets.CantTakeLunchMoney" />.
			/// </summary>
			// Token: 0x0400749F RID: 29855
			public static Dictionary<int, int> RespawnEnemyID = new Dictionary<int, int>
			{
				{
					492,
					0
				},
				{
					491,
					0
				},
				{
					394,
					0
				},
				{
					393,
					0
				},
				{
					392,
					0
				},
				{
					13,
					0
				},
				{
					14,
					0
				},
				{
					15,
					0
				},
				{
					412,
					0
				},
				{
					413,
					0
				},
				{
					414,
					0
				},
				{
					134,
					0
				},
				{
					135,
					0
				},
				{
					136,
					0
				},
				{
					454,
					0
				},
				{
					455,
					0
				},
				{
					456,
					0
				},
				{
					457,
					0
				},
				{
					458,
					0
				},
				{
					459,
					0
				},
				{
					8,
					7
				},
				{
					9,
					7
				},
				{
					11,
					10
				},
				{
					12,
					10
				},
				{
					40,
					39
				},
				{
					41,
					39
				},
				{
					96,
					95
				},
				{
					97,
					95
				},
				{
					99,
					98
				},
				{
					100,
					98
				},
				{
					88,
					87
				},
				{
					89,
					87
				},
				{
					90,
					87
				},
				{
					91,
					87
				},
				{
					92,
					87
				},
				{
					118,
					117
				},
				{
					119,
					117
				},
				{
					514,
					513
				},
				{
					515,
					513
				},
				{
					511,
					510
				},
				{
					512,
					510
				},
				{
					622,
					621
				},
				{
					623,
					621
				}
			};

			/// <summary>
			/// If <c>!= -1</c> for a given NPC type (<see cref="F:Terraria.NPC.type" />), then that NPC will store its past positions (and potentially more) for use in a trail.
			/// <br /> A value of <c>0</c> will store the NPC's position in <see cref="F:Terraria.NPC.oldPos" /> every three frames. <strong>Vanilla automatically uses <see cref="F:Terraria.NPC.localAI" />[3] to count frames for this, so make sure your code doesn't interfere with that.</strong>
			/// <br /> A value of <c>1</c>, <c>5</c>, or <c>6</c> will store the NPC's position in <see cref="F:Terraria.NPC.oldPos" /> every frame.
			/// <br /> A value of <c>2</c> will store the NPC's position and rotation in <see cref="F:Terraria.NPC.oldPos" /> and <see cref="F:Terraria.NPC.oldRot" /> respectively every frame <strong>if</strong> <c><see cref="F:Terraria.NPC.ai" />[0] == 4 or 5 or 6</c>.
			/// <br /> A value of <c>3</c> or <c>7</c> will store the NPC's position and rotation in <see cref="F:Terraria.NPC.oldPos" /> and <see cref="F:Terraria.NPC.oldRot" /> respectively every frame.
			/// <br /> A value of <c>4</c> will store the NPC's position in <see cref="F:Terraria.NPC.oldPos" /> every frame, as well as creating light (0x4D0033) at the NPC's position.
			/// <br /> Other values will do nothing.
			/// <br /> <strong>Vanilla will not automatically draw trails for you.</strong>
			/// <br /> Defaults to <c>-1</c>.
			/// </summary>
			/// <remarks>The length of an NPC's trail is determined by <see cref="F:Terraria.ID.NPCID.Sets.TrailCacheLength" />.</remarks>
			// Token: 0x040074A0 RID: 29856
			public static int[] TrailingMode = NPCID.Sets.Factory.CreateIntSet(-1, new int[]
			{
				439,
				0,
				440,
				0,
				370,
				1,
				372,
				1,
				373,
				1,
				396,
				1,
				400,
				1,
				401,
				1,
				473,
				2,
				474,
				2,
				475,
				2,
				476,
				2,
				4,
				3,
				471,
				3,
				477,
				3,
				479,
				3,
				120,
				4,
				137,
				4,
				138,
				4,
				94,
				5,
				125,
				6,
				126,
				6,
				127,
				6,
				128,
				6,
				129,
				6,
				130,
				6,
				131,
				6,
				139,
				6,
				140,
				6,
				407,
				6,
				420,
				6,
				425,
				6,
				427,
				6,
				426,
				6,
				581,
				6,
				516,
				6,
				542,
				6,
				543,
				6,
				544,
				6,
				545,
				6,
				402,
				7,
				417,
				7,
				419,
				7,
				418,
				7,
				574,
				7,
				575,
				7,
				519,
				7,
				521,
				7,
				522,
				7,
				546,
				7,
				558,
				7,
				559,
				7,
				560,
				7,
				551,
				7,
				620,
				7,
				657,
				6,
				636,
				7,
				677,
				7,
				685,
				7
			});

			/// <summary>
			/// If <see langword="true" /> for a given NPC type (<see cref="F:Terraria.NPC.type" />), then that NPC is categorized as a dragonfly.
			/// <br /> Defaults to <see langword="false" />.
			/// </summary>
			/// <remarks>Vanilla only uses this set to ensure the <see cref="F:Terraria.ID.ItemID.DragonflyStatue" /> doesn't spawn too many dragonflies without checking each type individually. It's still recommended to add your NPC to this set if it applies.</remarks>
			// Token: 0x040074A1 RID: 29857
			public static bool[] IsDragonfly = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				595,
				596,
				597,
				598,
				599,
				600,
				601
			});

			/// <summary>
			/// If <see langword="true" /> for a given NPC type (<see cref="F:Terraria.NPC.type" />), then that NPC belongs to the Old One's Army event.
			/// <br /> If any NPC in this set is alive, the OOA music will play. Additionally, all NPCs in this set will be erased when the OOA ends.
			/// <br /> NPCs in this set will target the <see cref="F:Terraria.ID.NPCID.DD2EterniaCrystal" /> if it is alive (using <see cref="M:Terraria.Utilities.NPCUtils.TargetClosestOldOnesInvasion(Terraria.NPC,System.Boolean,System.Nullable{Microsoft.Xna.Framework.Vector2})" />).
			/// <br /> Defaults to <see langword="false" />.
			/// </summary>
			// Token: 0x040074A2 RID: 29858
			public static bool[] BelongsToInvasionOldOnesArmy = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				552,
				553,
				554,
				561,
				562,
				563,
				555,
				556,
				557,
				558,
				559,
				560,
				576,
				577,
				568,
				569,
				566,
				567,
				570,
				571,
				572,
				573,
				548,
				549,
				564,
				565,
				574,
				575,
				551,
				578
			});

			/// <summary>
			/// If <see langword="true" /> for a given NPC type (<see cref="F:Terraria.NPC.type" />), then that NPC cannot be teleported using <see cref="F:Terraria.ID.TileID.Teleporter" />s.
			/// <br /> Defaults to <see langword="false" />.
			/// </summary>
			/// <remarks>
			/// Bosses and tile-phasing (<see cref="F:Terraria.NPC.noTileCollide" />) NPCs can't be teleported at all, regardless of this set.
			/// </remarks>
			// Token: 0x040074A3 RID: 29859
			public static bool[] TeleportationImmune = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				552,
				553,
				554,
				561,
				562,
				563,
				555,
				556,
				557,
				558,
				559,
				560,
				576,
				577,
				568,
				569,
				566,
				567,
				570,
				571,
				572,
				573,
				548,
				549,
				564,
				565,
				574,
				575,
				551,
				578
			});

			/// <summary>
			/// If <see langword="true" /> for a given NPC type (<see cref="F:Terraria.NPC.type" />), then that NPC can target other NPCs.
			/// <br /> This set does <strong>not</strong> automatically handle NPC targeting. You should use <see cref="M:Terraria.Utilities.NPCUtils.SearchForTarget(Terraria.NPC,Terraria.Utilities.NPCUtils.TargetSearchFlag,Terraria.Utilities.NPCUtils.SearchFilter{Terraria.Player},Terraria.Utilities.NPCUtils.SearchFilter{Terraria.NPC})" /> or any of its overloads to write your own targeting function.
			/// <br /> Defaults to <see langword="false" />.
			/// </summary>
			/// <remarks>
			/// You can check if an NPC is in this set using <see cref="P:Terraria.NPC.SupportsNPCTargets" />.
			/// <br /> You can use <see cref="M:Terraria.NPC.GetTargetData(System.Boolean)" /> to get info about a target, regardless of if that target is a <see cref="T:Terraria.Player" /> or <see cref="T:Terraria.NPC" />.
			/// </remarks>
			// Token: 0x040074A4 RID: 29860
			public static bool[] UsesNewTargetting = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				547,
				552,
				553,
				554,
				561,
				562,
				563,
				555,
				556,
				557,
				558,
				559,
				560,
				576,
				577,
				568,
				569,
				566,
				567,
				570,
				571,
				572,
				573,
				564,
				565,
				574,
				575,
				551,
				578,
				210,
				211,
				620,
				668
			});

			/// <summary>
			/// If <see langword="true" /> for a given NPC type (<see cref="F:Terraria.NPC.type" />), then that NPC can take damage from hostile NPCs without being friendly.
			/// <br /> Used in vanilla for critters and trapped town slimes.
			/// <br /> Defaults to <see langword="false" />.
			/// </summary>
			// Token: 0x040074A5 RID: 29861
			public static bool[] TakesDamageFromHostilesWithoutBeingFriendly = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				46,
				55,
				74,
				148,
				149,
				230,
				297,
				298,
				299,
				303,
				355,
				356,
				358,
				359,
				360,
				361,
				362,
				363,
				364,
				365,
				366,
				367,
				377,
				357,
				374,
				442,
				443,
				444,
				445,
				446,
				448,
				538,
				539,
				337,
				540,
				484,
				485,
				486,
				487,
				592,
				593,
				595,
				596,
				597,
				598,
				599,
				600,
				601,
				602,
				603,
				604,
				605,
				606,
				607,
				608,
				609,
				611,
				612,
				613,
				614,
				615,
				616,
				617,
				625,
				626,
				627,
				639,
				640,
				641,
				642,
				643,
				644,
				645,
				646,
				647,
				648,
				649,
				650,
				651,
				652,
				653,
				654,
				655,
				583,
				584,
				585,
				669,
				671,
				672,
				673,
				674,
				675,
				677,
				687
			});

			/// <summary>
			/// An array of length <see cref="P:Terraria.ModLoader.NPCLoader.NPCCount" /> with only <see langword="true" /> entries.
			/// <br /> Used for methods that take sets as parameters, like <see cref="M:Terraria.NPC.GetHurtByOtherNPCs(System.Boolean[])" />.
			/// </summary>
			// Token: 0x040074A6 RID: 29862
			public static bool[] AllNPCs = NPCID.Sets.Factory.CreateBoolSet(true, Array.Empty<int>());

			/// <summary>
			/// If <see langword="true" /> for a given NPC type (<see cref="F:Terraria.NPC.type" />), then that NPC is a bee that will hurt other non-bee NPCs on contact.
			/// <br /> Defaults to <see langword="false" />.
			/// </summary>
			// Token: 0x040074A7 RID: 29863
			public static bool[] HurtingBees = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				210,
				211,
				222
			});

			/// <summary>
			/// If <see langword="true" /> for a given NPC type (<see cref="F:Terraria.NPC.type" />), then that NPC will have a special spawning animation when using <see cref="F:Terraria.ID.NPCAIStyleID.DD2Fighter" />.
			/// <br /> Defaults to <see langword="false" />.
			/// </summary>
			// Token: 0x040074A8 RID: 29864
			public static bool[] FighterUsesDD2PortalAppearEffect = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				552,
				553,
				554,
				561,
				562,
				563,
				555,
				556,
				557,
				576,
				577,
				568,
				569,
				570,
				571,
				572,
				573,
				564,
				565
			});

			/// <summary>
			/// If <c>!= -1f</c> for a given NPC type (<see cref="F:Terraria.NPC.type" />), then if that NPC was spawned from a statue (<see cref="F:Terraria.NPC.SpawnedFromStatue" />), it will have a (retrieved value)% chance to actually drop loot when killed.
			/// <br /> Defaults to <c>-1f</c>.
			/// </summary>
			/// <remarks>NPCs in this set will never drop loot unless interacted with by a player (<see cref="M:Terraria.NPC.AnyInteractions" />).</remarks>
			// Token: 0x040074A9 RID: 29865
			public static float[] StatueSpawnedDropRarity = NPCID.Sets.Factory.CreateCustomSet<float>(-1f, new object[]
			{
				480,
				0.05f,
				82,
				0.05f,
				86,
				0.05f,
				48,
				0.05f,
				490,
				0.05f,
				489,
				0.05f,
				170,
				0.05f,
				180,
				0.05f,
				171,
				0.05f,
				167,
				0.25f,
				73,
				0.01f,
				24,
				0.05f,
				481,
				0.05f,
				42,
				0.05f,
				6,
				0.05f,
				2,
				0.05f,
				49,
				0.2f,
				3,
				0.2f,
				58,
				0.2f,
				21,
				0.2f,
				65,
				0.2f,
				449,
				0.2f,
				482,
				0.2f,
				103,
				0.2f,
				64,
				0.2f,
				63,
				0.2f,
				85,
				0f
			});

			/// <summary>
			/// If <see langword="true" /> for a given NPC type (<see cref="F:Terraria.NPC.type" />), then if that NPC was spawned from a statue (<see cref="F:Terraria.NPC.SpawnedFromStatue" />), it will not drop loot in pre-hardmode.
			/// <br /> Defaults to <see langword="false" />.
			/// </summary>
			// Token: 0x040074AA RID: 29866
			public static bool[] NoEarlymodeLootWhenSpawnedFromStatue = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				480,
				82,
				86,
				170,
				180,
				171
			});

			/// <summary>
			/// If <see langword="true" /> for a given NPC type (<see cref="F:Terraria.NPC.type" />), then that NPC will receive stat scaling in Expert Mode, even if its normal stats would prevent that.
			/// <br /> Defaults to <see langword="false" />.
			/// </summary>
			/// <remarks>NPCs with less than 5 health are most prone to needing this set. The full scaling conditions can be found at the start of <see cref="M:Terraria.NPC.ScaleStats(System.Nullable{System.Int32},Terraria.DataStructures.GameModeData,System.Nullable{System.Single})" />.</remarks>
			// Token: 0x040074AB RID: 29867
			public static bool[] NeedsExpertScaling = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				25,
				30,
				665,
				33,
				112,
				666,
				261,
				265,
				371,
				516,
				519,
				397,
				396,
				398,
				491
			});

			/// <summary>
			/// If <see langword="true" /> for a given NPC type (<see cref="F:Terraria.NPC.type" />), then that NPC is treated specially for difficulty scaling.
			/// <br /> Projectile NPCs never scale their max health, defense, or value.
			/// <br /> Additionally, kills are not counted for NPCs in this set.
			/// <br /> Defaults to <see langword="false" />.
			/// </summary>
			// Token: 0x040074AC RID: 29868
			public static bool[] ProjectileNPC = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				25,
				30,
				665,
				33,
				112,
				666,
				261,
				265,
				371,
				516,
				519
			});

			// Token: 0x040074AD RID: 29869
			internal static bool[] SavesAndLoads = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				422,
				507,
				517,
				493
			});

			/// <summary>
			/// The length of this NPC type's (<see cref="F:Terraria.NPC.type" />) <see cref="F:Terraria.NPC.oldPos" /> and <see cref="F:Terraria.NPC.oldRot" /> arrays.
			/// <br /> <strong>This set does nothing by itself.</strong> You will need to set <see cref="F:Terraria.ID.NPCID.Sets.TrailingMode" /> in order to actually store values.
			/// <br /> Defaults to <c>10</c>.
			/// </summary>
			// Token: 0x040074AE RID: 29870
			public static int[] TrailCacheLength = NPCID.Sets.Factory.CreateIntSet(10, new int[]
			{
				402,
				36,
				519,
				20,
				522,
				20,
				620,
				20,
				677,
				60,
				685,
				10
			});

			/// <summary>
			/// If <see langword="true" /> for a given NPC type (<see cref="F:Terraria.NPC.type" />), then that NPC will sync more the closer it is to a player.
			/// <br /> Defaults to <see langword="true" />.
			/// </summary>
			// Token: 0x040074AF RID: 29871
			public static bool[] UsesMultiplayerProximitySyncing = NPCID.Sets.Factory.CreateBoolSet(true, new int[]
			{
				398,
				397,
				396
			});

			/// <summary>
			/// If <see langword="true" /> for a given NPC type (<see cref="F:Terraria.NPC.type" />), then that NPC will not smooth its visual position in multiplayer.
			/// <br /> Defaults to <see langword="false" />.
			/// </summary>
			/// <remarks>To prevent smoothing by <see cref="T:Terraria.ID.NPCAIStyleID" /> (<see cref="F:Terraria.NPC.aiStyle" />), use <see cref="F:Terraria.ID.NPCID.Sets.NoMultiplayerSmoothingByAI" />.</remarks>
			// Token: 0x040074B0 RID: 29872
			public static bool[] NoMultiplayerSmoothingByType = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				113,
				114,
				50,
				657,
				120,
				245,
				247,
				248,
				246,
				370,
				222,
				398,
				397,
				396,
				400,
				401,
				668,
				70
			});

			/// <summary>
			/// If <see langword="true" /> for a given <strong><see cref="T:Terraria.ID.NPCAIStyleID" /></strong> (<see cref="F:Terraria.NPC.aiStyle" />), then that AI style will not smooth its visual position in multiplayer.
			/// <br /> Defaults to <see langword="false" />.
			/// </summary>
			/// <remarks>To prevent smoothing by NPC type (<see cref="F:Terraria.NPC.type" />), use <see cref="F:Terraria.ID.NPCID.Sets.NoMultiplayerSmoothingByType" />.</remarks>
			// Token: 0x040074B1 RID: 29873
			public static bool[] NoMultiplayerSmoothingByAI = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				6,
				8,
				37
			});

			/// <summary>
			/// If <see langword="true" /> for a given NPC type (<see cref="F:Terraria.NPC.type" />), then that NPC can be summoned in multiplayer using <see cref="F:Terraria.ID.MessageID.SpawnBossUseLicenseStartEvent" />.
			/// <br /> <strong>If you don't set this, your boss most likely won't work in multiplayer.</strong>
			/// <br /> Defaults to <see langword="false" />.
			/// </summary>
			// Token: 0x040074B2 RID: 29874
			public static bool[] MPAllowedEnemies = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				4,
				13,
				50,
				126,
				125,
				134,
				127,
				128,
				131,
				129,
				130,
				222,
				245,
				266,
				370,
				657,
				668
			});

			/// <summary>
			/// If <see langword="true" /> for a given NPC type (<see cref="F:Terraria.NPC.type" />), then that NPC is a critter that can spawn in towns.
			/// <br /> Town critters use <see cref="F:Terraria.ID.NPCAIStyleID.Passive" />.
			/// <br /> Defaults to <see langword="false" />.
			/// </summary>
			/// <remarks>Make sure to also set <see cref="F:Terraria.ID.NPCID.Sets.CountsAsCritter" /> for NPCs in this set.</remarks>
			// Token: 0x040074B3 RID: 29875
			public static bool[] TownCritter = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				46,
				148,
				149,
				230,
				299,
				300,
				303,
				337,
				361,
				362,
				364,
				366,
				367,
				443,
				445,
				447,
				538,
				539,
				540,
				583,
				584,
				585,
				592,
				593,
				602,
				607,
				608,
				610,
				616,
				617,
				625,
				626,
				627,
				639,
				640,
				641,
				642,
				643,
				644,
				645,
				646,
				647,
				648,
				649,
				650,
				651,
				652,
				687
			});

			/// <summary>
			/// If <see langword="true" /> for a given NPC type (<see cref="F:Terraria.NPC.type" />), then that NPC is counted as a critter.
			/// <br /> Critters cannot be damage by players with <see cref="F:Terraria.Player.dontHurtCritters" /> set to <see langword="true" />.
			/// <br /> Also, critters can't be used to summon Horseman's Blade projectiles, nor can they be chased by said projectiles.
			/// </summary>
			// Token: 0x040074B4 RID: 29876
			public static bool[] CountsAsCritter = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				46,
				303,
				337,
				540,
				443,
				74,
				297,
				298,
				442,
				611,
				377,
				446,
				612,
				613,
				356,
				444,
				595,
				596,
				597,
				598,
				599,
				600,
				601,
				604,
				605,
				357,
				448,
				374,
				484,
				355,
				358,
				606,
				359,
				360,
				485,
				486,
				487,
				148,
				149,
				55,
				230,
				592,
				593,
				299,
				538,
				539,
				300,
				447,
				361,
				445,
				362,
				363,
				364,
				365,
				367,
				366,
				583,
				584,
				585,
				602,
				603,
				607,
				608,
				609,
				610,
				616,
				617,
				625,
				626,
				627,
				615,
				639,
				640,
				641,
				642,
				643,
				644,
				645,
				646,
				647,
				648,
				649,
				650,
				651,
				652,
				653,
				654,
				655,
				661,
				669,
				671,
				672,
				673,
				674,
				675,
				677,
				687
			});

			/// <summary>
			/// <strong>Only applies to vanilla NPCs.</strong> Also only applies to town NPCs.
			/// <br /> If <see langword="true" /> for a given NPC type (<see cref="F:Terraria.NPC.type" />), then that NPC doesn't have any special dialogue for parties.
			/// <br /> Defaults to <see langword="false" />.
			/// </summary>
			// Token: 0x040074B5 RID: 29877
			public static bool[] HasNoPartyText = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				441,
				453
			});

			/// <summary>
			/// The vertical offset, in pixels, that an NPC's party hat will draw with.
			/// <br /> Defaults to <c>0</c>.
			/// </summary>
			/// <remarks><see cref="F:Terraria.ID.NPCID.Sets.TownNPCsFramingGroups" /> applies a specific offset per frame, this applies the same offset to all frames.</remarks>
			// Token: 0x040074B6 RID: 29878
			public static int[] HatOffsetY = NPCID.Sets.Factory.CreateIntSet(0, new int[]
			{
				227,
				4,
				107,
				2,
				108,
				2,
				229,
				4,
				17,
				2,
				38,
				8,
				160,
				-10,
				208,
				2,
				142,
				2,
				124,
				2,
				453,
				2,
				37,
				4,
				54,
				4,
				209,
				4,
				369,
				6,
				441,
				6,
				353,
				-2,
				633,
				-2,
				550,
				-2,
				588,
				2,
				663,
				2,
				637,
				0,
				638,
				0,
				656,
				4,
				670,
				0,
				678,
				0,
				679,
				0,
				680,
				0,
				681,
				0,
				682,
				0,
				683,
				0,
				684,
				0
			});

			/// <summary>
			/// Associates a town NPC's NPC type (<see cref="F:Terraria.NPC.type" />) with its corresponding <see cref="T:Terraria.GameContent.UI.EmoteID" />.
			/// <br /> Town NPCs can emote using emotes in this set if the corresponding NPC is alive.
			/// <br /> If <c>0</c> for a given NPC type, then that NPC has no associated emote.
			/// <br /> Defaults to <c>0</c>.
			/// </summary>
			/// <remarks>This only applies to town NPCs -- there isn't a set that associates, say, the Eye of Cthulhu to an associated emote.</remarks>
			// Token: 0x040074B7 RID: 29879
			public static int[] FaceEmote = NPCID.Sets.Factory.CreateIntSet(0, new int[]
			{
				17,
				101,
				18,
				102,
				19,
				103,
				20,
				104,
				22,
				105,
				37,
				106,
				38,
				107,
				54,
				108,
				107,
				109,
				108,
				110,
				124,
				111,
				142,
				112,
				160,
				113,
				178,
				114,
				207,
				115,
				208,
				116,
				209,
				117,
				227,
				118,
				228,
				119,
				229,
				120,
				353,
				121,
				368,
				122,
				369,
				123,
				453,
				124,
				441,
				125,
				588,
				140,
				633,
				141,
				663,
				145
			});

			/// <summary>
			/// The number of extra frames this town NPC has. These frames are used for special actions, such as sitting down and talking to other NPCs.
			/// <br /> Defaults to <c>0</c>.
			/// </summary>
			/// <remarks>If you want to copy a vanilla NPC's framing, use <see cref="P:Terraria.ModLoader.ModNPC.AnimationType" />.</remarks>
			// Token: 0x040074B8 RID: 29880
			public static int[] ExtraFramesCount = NPCID.Sets.Factory.CreateIntSet(0, new int[]
			{
				17,
				9,
				18,
				9,
				19,
				9,
				20,
				7,
				22,
				10,
				37,
				5,
				38,
				9,
				54,
				7,
				107,
				9,
				108,
				7,
				124,
				9,
				142,
				9,
				160,
				7,
				178,
				9,
				207,
				9,
				208,
				9,
				209,
				10,
				227,
				9,
				228,
				10,
				229,
				10,
				353,
				9,
				633,
				9,
				368,
				10,
				369,
				9,
				453,
				9,
				441,
				9,
				550,
				9,
				588,
				9,
				663,
				7,
				637,
				18,
				638,
				11,
				656,
				20,
				670,
				6,
				678,
				6,
				679,
				6,
				680,
				6,
				681,
				6,
				682,
				6,
				683,
				6,
				684,
				6
			});

			/// <summary>
			/// The number of this town NPC's extra frames that are dedicated to attacking.
			/// <br /> Defaults to <c>0</c>.
			/// </summary>
			/// <remarks>If you want to copy a vanilla NPC's framing, use <see cref="P:Terraria.ModLoader.ModNPC.AnimationType" />.</remarks>
			// Token: 0x040074B9 RID: 29881
			public static int[] AttackFrameCount = NPCID.Sets.Factory.CreateIntSet(0, new int[]
			{
				17,
				4,
				18,
				4,
				19,
				4,
				20,
				2,
				22,
				5,
				37,
				0,
				38,
				4,
				54,
				2,
				107,
				4,
				108,
				2,
				124,
				4,
				142,
				4,
				160,
				2,
				178,
				4,
				207,
				4,
				208,
				4,
				209,
				5,
				227,
				4,
				228,
				5,
				229,
				5,
				353,
				4,
				633,
				4,
				368,
				5,
				369,
				4,
				453,
				4,
				441,
				4,
				550,
				4,
				588,
				4,
				663,
				2,
				637,
				0,
				638,
				0,
				656,
				0,
				670,
				0,
				678,
				0,
				679,
				0,
				680,
				0,
				681,
				0,
				682,
				0,
				683,
				0,
				684,
				0
			});

			/// <summary>
			/// The distance, in pixels, that this town NPC will check for enemies to attack. Any enemies beyond this distance are not considered.
			/// <br /> Also serves as the attack range for NPCs with an <see cref="F:Terraria.ID.NPCID.Sets.AttackType" /> of <c>0</c> or <c>2</c>.
			/// <br /> Defaults to <c>-1</c>, which uses a default of <c>200</c> pixels (12.5 tiles).
			/// </summary>
			/// <remarks><see cref="F:Terraria.ID.NPCID.Sets.PrettySafe" /> determines an NPC's attack range.</remarks>
			// Token: 0x040074BA RID: 29882
			public static int[] DangerDetectRange = NPCID.Sets.Factory.CreateIntSet(-1, new int[]
			{
				38,
				300,
				17,
				320,
				107,
				300,
				19,
				900,
				22,
				700,
				124,
				800,
				228,
				800,
				178,
				900,
				18,
				300,
				229,
				1000,
				209,
				1000,
				54,
				700,
				108,
				700,
				160,
				700,
				20,
				1200,
				369,
				300,
				453,
				300,
				368,
				900,
				207,
				60,
				227,
				800,
				208,
				400,
				142,
				500,
				441,
				50,
				353,
				60,
				633,
				100,
				550,
				120,
				588,
				120,
				663,
				700,
				638,
				250,
				637,
				250,
				656,
				250,
				670,
				250,
				678,
				250,
				679,
				250,
				680,
				250,
				681,
				250,
				682,
				250,
				683,
				250,
				684,
				250
			});

			/// <summary>
			/// <b>Unused:</b> Replaced by <see cref="F:Terraria.ID.NPCID.Sets.SpecificDebuffImmunity" /><br /><br />
			/// If <see langword="true" /> for a given NPC type (<see cref="F:Terraria.NPC.type" />), then that NPC is immune to the effects of the shimmer.
			/// <br /> Defaults to <see langword="false" />.
			/// </summary>
			/// <remarks>To make an NPC ignore the player's shimmer invulnerability, use <see cref="F:Terraria.ID.NPCID.Sets.CanHitPastShimmer" /> instead.</remarks>
			// Token: 0x040074BB RID: 29883
			private static bool[] ShimmerImmunity = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				637,
				638,
				656,
				670,
				684,
				678,
				679,
				680,
				681,
				682,
				683,
				356,
				669,
				676,
				244,
				677,
				594,
				667,
				662,
				5,
				115,
				116,
				139,
				245,
				247,
				248,
				246,
				249,
				344,
				325,
				50,
				535,
				657,
				658,
				659,
				660,
				668,
				25,
				30,
				33,
				70,
				72,
				665,
				666,
				112,
				516,
				517,
				518,
				519,
				520,
				521,
				522,
				523,
				381,
				382,
				383,
				384,
				385,
				386,
				387,
				388,
				389,
				390,
				391,
				392,
				393,
				394,
				395,
				396,
				397,
				398,
				399,
				400,
				401,
				402,
				403,
				404,
				405,
				406,
				407,
				408,
				409,
				410,
				411,
				412,
				413,
				414,
				415,
				416,
				417,
				418,
				419,
				420,
				421,
				423,
				424,
				425,
				426,
				427,
				428,
				429,
				548,
				549,
				551,
				552,
				553,
				554,
				555,
				556,
				557,
				558,
				559,
				560,
				561,
				562,
				563,
				564,
				565,
				566,
				567,
				568,
				569,
				570,
				571,
				572,
				573,
				574,
				575,
				576,
				577,
				578
			});

			/// <summary>
			/// If not <c>-1</c> for a given NPC type (<see cref="F:Terraria.NPC.type" />), then that NPC will transform into the retrieved item type (<see cref="F:Terraria.Item.type" />) when touching shimmer.
			/// <br /> Defaults to <c>-1</c>.
			/// </summary>
			/// <remarks>The created item will always have a stack of 1.</remarks>
			// Token: 0x040074BC RID: 29884
			public static int[] ShimmerTransformToItem = NPCID.Sets.Factory.CreateIntSet(-1, new int[]
			{
				651,
				182,
				644,
				182,
				650,
				178,
				643,
				178,
				649,
				179,
				642,
				179,
				648,
				177,
				641,
				177,
				640,
				180,
				647,
				180,
				646,
				181,
				639,
				181,
				652,
				999,
				645,
				999,
				448,
				5341
			});

			/// <summary>
			/// If <see langword="true" /> for a given NPC type (<see cref="F:Terraria.NPC.type" />), then that NPC is a town NPC with an alternate shimmered texture.
			/// <br /> Defaults to <see langword="false" />.
			/// </summary>
			// Token: 0x040074BD RID: 29885
			public static bool[] ShimmerTownTransform = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				22,
				17,
				18,
				227,
				207,
				633,
				588,
				208,
				369,
				353,
				38,
				20,
				550,
				19,
				107,
				228,
				54,
				124,
				441,
				229,
				160,
				108,
				178,
				209,
				142,
				663,
				37,
				453,
				368
			});

			/// <summary>
			/// Associates an NPC type (<see cref="F:Terraria.NPC.type" />) with the NPC type it will turn into when submerged in shimmer.
			/// <br /> A value of<c>-1</c> means the given NPC won't transform.
			/// <br /> Defaults to <c>-1</c>.
			/// <br /> Also applies to transforming critter items (<c><see cref="F:Terraria.Item.makeNPC" /> &gt; 0</c>) into NPC.
			/// </summary>
			// Token: 0x040074BE RID: 29886
			public static int[] ShimmerTransformToNPC = NPCID.Sets.Factory.CreateIntSet(-1, new int[]
			{
				3,
				21,
				132,
				202,
				186,
				201,
				187,
				21,
				188,
				21,
				189,
				202,
				200,
				203,
				590,
				21,
				1,
				676,
				302,
				676,
				335,
				676,
				336,
				676,
				334,
				676,
				333,
				676,
				225,
				676,
				141,
				676,
				16,
				676,
				147,
				676,
				184,
				676,
				537,
				676,
				204,
				676,
				81,
				676,
				183,
				676,
				138,
				676,
				121,
				676,
				591,
				449,
				430,
				449,
				436,
				452,
				432,
				450,
				433,
				449,
				434,
				449,
				435,
				451,
				614,
				677,
				74,
				677,
				297,
				677,
				298,
				677,
				673,
				677,
				672,
				677,
				671,
				677,
				675,
				677,
				674,
				677,
				362,
				677,
				363,
				677,
				364,
				677,
				365,
				677,
				608,
				677,
				609,
				677,
				602,
				677,
				603,
				677,
				611,
				677,
				148,
				677,
				149,
				677,
				46,
				677,
				303,
				677,
				337,
				677,
				299,
				677,
				538,
				677,
				55,
				677,
				607,
				677,
				615,
				677,
				625,
				677,
				626,
				677,
				361,
				677,
				687,
				677,
				484,
				677,
				604,
				677,
				358,
				677,
				355,
				677,
				616,
				677,
				617,
				677,
				654,
				677,
				653,
				677,
				655,
				677,
				585,
				677,
				584,
				677,
				583,
				677,
				595,
				677,
				596,
				677,
				600,
				677,
				597,
				677,
				598,
				677,
				599,
				677,
				357,
				677,
				377,
				677,
				606,
				677,
				359,
				677,
				360,
				677,
				367,
				677,
				366,
				677,
				300,
				677,
				610,
				677,
				612,
				677,
				487,
				677,
				486,
				677,
				485,
				677,
				669,
				677,
				356,
				677,
				661,
				677,
				374,
				677,
				442,
				677,
				443,
				677,
				444,
				677,
				601,
				677,
				445,
				677,
				592,
				677,
				446,
				677,
				605,
				677,
				447,
				677,
				627,
				677,
				539,
				677,
				613,
				677
			});

			/// <summary>
			/// The duration of this town NPC's attack animation in ticks.
			/// <br /> Defaults to <c>-1</c>.
			/// </summary>
			// Token: 0x040074BF RID: 29887
			public static int[] AttackTime = NPCID.Sets.Factory.CreateIntSet(-1, new int[]
			{
				38,
				34,
				17,
				34,
				107,
				60,
				19,
				40,
				22,
				30,
				124,
				34,
				228,
				40,
				178,
				24,
				18,
				34,
				229,
				60,
				209,
				60,
				54,
				60,
				108,
				30,
				160,
				60,
				20,
				600,
				369,
				34,
				453,
				34,
				368,
				60,
				207,
				15,
				227,
				60,
				208,
				34,
				142,
				34,
				441,
				15,
				353,
				12,
				633,
				12,
				550,
				34,
				588,
				20,
				663,
				60,
				638,
				-1,
				637,
				-1,
				656,
				-1,
				670,
				-1,
				678,
				-1,
				679,
				-1,
				680,
				-1,
				681,
				-1,
				682,
				-1,
				683,
				-1,
				684,
				-1
			});

			/// <summary>
			/// The chance that this town NPC attacks if it detects danger.
			/// <br /> The actual chance is <c>1 / (retrieved value * 2)</c>.
			/// <br /> Defaults to <c>1</c>.
			/// </summary>
			// Token: 0x040074C0 RID: 29888
			public static int[] AttackAverageChance = NPCID.Sets.Factory.CreateIntSet(1, new int[]
			{
				38,
				40,
				17,
				30,
				107,
				60,
				19,
				30,
				22,
				30,
				124,
				30,
				228,
				50,
				178,
				50,
				18,
				60,
				229,
				40,
				209,
				30,
				54,
				30,
				108,
				30,
				160,
				60,
				20,
				60,
				369,
				50,
				453,
				30,
				368,
				40,
				207,
				1,
				227,
				30,
				208,
				50,
				142,
				50,
				441,
				1,
				353,
				1,
				633,
				1,
				550,
				40,
				588,
				20,
				663,
				1,
				638,
				1,
				637,
				1,
				656,
				1,
				670,
				1,
				678,
				1,
				679,
				1,
				680,
				1,
				681,
				1,
				682,
				1,
				683,
				1,
				684,
				1
			});

			/// <summary>
			/// Determines how a town NPC with the given NPC type (<see cref="F:Terraria.NPC.type" />) attacks. Use the corresponding hooks to implement and customize the attack.
			/// <para /> For <c>-1</c>, this NPC won't attack.
			/// <para /> For <c>0</c>, this NPC will throw a projectile. <see cref="M:Terraria.ModLoader.ModNPC.TownNPCAttackProj(System.Int32@,System.Int32@)" />, <see cref="M:Terraria.ModLoader.ModNPC.TownNPCAttackProjSpeed(System.Single@,System.Single@,System.Single@)" />
			/// <para /> For <c>1</c>, this NPC will shoot a weapon. <see cref="M:Terraria.ModLoader.ModNPC.TownNPCAttackProj(System.Int32@,System.Int32@)" />, <see cref="M:Terraria.ModLoader.ModNPC.TownNPCAttackProjSpeed(System.Single@,System.Single@,System.Single@)" />, <see cref="M:Terraria.ModLoader.ModNPC.TownNPCAttackShoot(System.Boolean@)" />, <see cref="M:Terraria.ModLoader.ModNPC.DrawTownAttackGun(Microsoft.Xna.Framework.Graphics.Texture2D@,Microsoft.Xna.Framework.Rectangle@,System.Single@,System.Int32@)" />
			/// <para /> For <c>2</c>, this NPC will use magic. <see cref="M:Terraria.ModLoader.ModNPC.TownNPCAttackProj(System.Int32@,System.Int32@)" />, <see cref="M:Terraria.ModLoader.ModNPC.TownNPCAttackProjSpeed(System.Single@,System.Single@,System.Single@)" />, <see cref="M:Terraria.ModLoader.ModNPC.TownNPCAttackMagic(System.Single@)" />
			/// <para /> For <c>3</c>, this NPC will swing a weapon. <see cref="M:Terraria.ModLoader.ModNPC.TownNPCAttackSwing(System.Int32@,System.Int32@)" />, <see cref="M:Terraria.ModLoader.ModNPC.DrawTownAttackSwing(Microsoft.Xna.Framework.Graphics.Texture2D@,Microsoft.Xna.Framework.Rectangle@,System.Int32@,System.Single@,Microsoft.Xna.Framework.Vector2@)" />
			/// <para /> Defaults to <c>-1</c>.
			/// </summary>
			/// <remarks>
			/// You can modify most aspects of a town NPC's attack, including the item used and projectile shot, using any of the "TownNPCAttack" or "TownAttack" hooks in <see cref="T:Terraria.ModLoader.ModNPC" /> or <see cref="T:Terraria.ModLoader.GlobalNPC" />.
			/// <br /> You can change the color of a magic (<c>2</c>) NPC's aura using <see cref="F:Terraria.ID.NPCID.Sets.MagicAuraColor" />.
			/// </remarks>
			// Token: 0x040074C1 RID: 29889
			public static int[] AttackType = NPCID.Sets.Factory.CreateIntSet(-1, new int[]
			{
				38,
				0,
				17,
				0,
				107,
				0,
				19,
				1,
				22,
				1,
				124,
				0,
				228,
				1,
				178,
				1,
				18,
				0,
				229,
				1,
				209,
				1,
				54,
				2,
				108,
				2,
				160,
				2,
				20,
				2,
				369,
				0,
				453,
				0,
				368,
				1,
				207,
				3,
				227,
				1,
				208,
				0,
				142,
				0,
				441,
				3,
				353,
				3,
				633,
				0,
				550,
				0,
				588,
				0,
				663,
				2,
				638,
				-1,
				637,
				-1,
				656,
				-1,
				670,
				-1,
				678,
				-1,
				679,
				-1,
				680,
				-1,
				681,
				-1,
				682,
				-1,
				683,
				-1,
				684,
				-1
			});

			/// <summary>
			/// The maximum distance in pixels that an enemy can be from this town NPC before they try to attack.
			/// <br /> Defaults to <c>-1</c>.
			/// </summary>
			// Token: 0x040074C2 RID: 29890
			public static int[] PrettySafe = NPCID.Sets.Factory.CreateIntSet(-1, new int[]
			{
				19,
				300,
				22,
				200,
				124,
				200,
				228,
				300,
				178,
				300,
				229,
				300,
				209,
				300,
				54,
				100,
				108,
				100,
				160,
				100,
				20,
				200,
				368,
				200,
				227,
				200
			});

			/// <summary>
			/// The <see cref="T:Microsoft.Xna.Framework.Color" /> of the magical aura used by town NPCs with magic (<c>3</c>) attacks.
			/// <br /> Defaults to <see cref="P:Microsoft.Xna.Framework.Color.White" />.
			/// </summary>
			// Token: 0x040074C3 RID: 29891
			public static Color[] MagicAuraColor = NPCID.Sets.Factory.CreateCustomSet<Color>(Color.White, new object[]
			{
				54,
				new Color(100, 4, 227, 127),
				108,
				new Color(255, 80, 60, 127),
				160,
				new Color(40, 80, 255, 127),
				20,
				new Color(40, 255, 80, 127),
				663,
				Main.hslToRgb(0.92f, 1f, 0.78f, 127)
			});

			/// <summary>
			/// <strong>Unused in vanilla.</strong>
			/// <br /> If <see langword="true" /> for a given NPC type (<see cref="F:Terraria.NPC.type" />), then that NPC is a type of demon eye.
			/// <br /> Defaults to <see langword="false" />.
			/// </summary>
			// Token: 0x040074C4 RID: 29892
			public static bool[] DemonEyes = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				2,
				190,
				192,
				193,
				191,
				194,
				317,
				318
			});

			/// <summary>
			/// <strong>Unused in vanilla.</strong>
			/// <br /> If <see langword="true" /> for a given NPC type (<see cref="F:Terraria.NPC.type" />), then that NPC is a type of zombie.
			/// <br /> Defaults to <see langword="false" />.
			/// </summary>
			// Token: 0x040074C5 RID: 29893
			public static bool[] Zombies = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				3,
				132,
				186,
				187,
				188,
				189,
				200,
				223,
				161,
				254,
				255,
				52,
				53,
				536,
				319,
				320,
				321,
				332,
				436,
				431,
				432,
				433,
				434,
				435,
				331,
				430,
				590
			});

			/// <summary>
			/// If <see langword="true" /> for a given NPC type (<see cref="F:Terraria.NPC.type" />), then that NPC is a type of skeleton.
			/// <br /> Skeletons cannot hurt the <see cref="F:Terraria.ID.NPCID.SkeletonMerchant" />.
			/// <br /> Defaults to <see langword="false" />.
			/// </summary>
			// Token: 0x040074C6 RID: 29894
			public static bool[] Skeletons = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				77,
				449,
				450,
				451,
				452,
				481,
				201,
				202,
				203,
				21,
				324,
				110,
				323,
				293,
				291,
				322,
				292,
				197,
				167,
				44,
				635
			});

			/// <summary>
			/// Associates an NPC type (<see cref="F:Terraria.NPC.type" />) with the index in <see cref="F:Terraria.GameContent.TextureAssets.NpcHeadBoss" /> of its default map icon.
			/// <br /> Auto-set using <see cref="T:Terraria.ModLoader.AutoloadBossHead" />.
			/// <br /> Defaults to <c>-1</c>.
			/// </summary>
			/// <remarks>
			/// If you need to dynamically change your boss's head icon, use <see cref="M:Terraria.ModLoader.ModNPC.BossHeadSlot(System.Int32@)" /> and <see cref="M:Terraria.ModLoader.NPCHeadLoader.GetBossHeadSlot(System.String)" />.
			/// <br /> If you need an NPC's <em>current</em> head index, using <see cref="M:Terraria.NPC.GetBossHeadTextureIndex" />, which handles phase changes.
			/// </remarks>
			// Token: 0x040074C7 RID: 29895
			public static int[] BossHeadTextures = NPCID.Sets.Factory.CreateIntSet(-1, new int[]
			{
				4,
				0,
				13,
				2,
				344,
				3,
				370,
				4,
				246,
				5,
				249,
				5,
				345,
				6,
				50,
				7,
				396,
				8,
				395,
				9,
				325,
				10,
				262,
				11,
				327,
				13,
				222,
				14,
				125,
				15,
				126,
				20,
				346,
				17,
				127,
				18,
				35,
				19,
				68,
				19,
				113,
				22,
				266,
				23,
				439,
				24,
				440,
				24,
				134,
				25,
				491,
				26,
				517,
				27,
				422,
				28,
				507,
				29,
				493,
				30,
				549,
				35,
				564,
				32,
				565,
				32,
				576,
				33,
				577,
				33,
				551,
				34,
				548,
				36,
				636,
				37,
				657,
				38,
				668,
				39
			});

			/// <summary>
			/// If <see langword="true" /> for a given NPC type (<see cref="F:Terraria.NPC.type" />), then that NPC will not have its kills counted.
			/// <br /> Used in vanilla for special projectile-like NPCs (not found in <see cref="F:Terraria.ID.NPCID.Sets.ProjectileNPC" />), for NPCs that turn into another NPC on death, and for temporary NPCs, like the <see cref="F:Terraria.ID.NPCID.MothronEgg" />.
			/// <br /> Defaults to <see langword="false" />.
			/// </summary>
			/// <remarks>Any NPC in the <see cref="F:Terraria.ID.NPCID.Sets.ProjectileNPC" /> set will not have its kills counted either.</remarks>
			// Token: 0x040074C8 RID: 29896
			public static bool[] PositiveNPCTypesExcludedFromDeathTally = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				121,
				384,
				478,
				479,
				410,
				472,
				378
			});

			/// <summary>
			/// If <see langword="true" /> for a given NPC type (<see cref="F:Terraria.NPC.type" />), then that NPC is sorted with bosses despite <see cref="F:Terraria.NPC.boss" /> being <see langword="false" />.
			/// <Br /> Used in vanilla for the Celestial Pillars, the Eater of Worlds' head, and the Torch God.
			/// <br /> Defaults to <see langword="false" />.
			/// </summary>
			// Token: 0x040074C9 RID: 29897
			public static bool[] ShouldBeCountedAsBoss = NPCID.Sets.Factory.CreateBoolSet(false, new int[]
			{
				517,
				422,
				507,
				493,
				13,
				664
			});

			/// <summary>
			/// If <see langword="true" /> for a given NPC type (<see cref="F:Terraria.NPC.type" />), then that NPC is considered dangerous without having <see cref="F:Terraria.NPC.boss" /> set.
			/// <br /> <see cref="F:Terraria.Main.CurrentFrameFlags.AnyActiveBossNPC" /> will be set to <see langword="true" /> if any NPC in this set is alive, and <see cref="M:Terraria.NPC.AnyDanger(System.Boolean,System.Boolean)" /> will return <see langword="true" /> if any NPCs in this set are alive.
			/// <br /> Defaults to <see langword="false" />.
			/// </summary>
			// Token: 0x040074CA RID: 29898
			public static bool[] DangerThatPreventsOtherDangers = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				517,
				422,
				507,
				493,
				399
			});

			/// <summary>
			/// If <see langword="true" /> for a given NPC type (<see cref="F:Terraria.NPC.type" />), then that NPC will always draw, even if its hitbox is off-screen.
			/// <br /> Default to <see langword="false" />.
			/// </summary>
			// Token: 0x040074CB RID: 29899
			public static bool[] MustAlwaysDraw = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				113,
				114,
				115,
				116,
				126,
				125
			});

			/// <summary>
			/// The number of extra textures this town NPC has.
			/// <br /> Most extra textures are hatless versions used for parties, though the <see cref="F:Terraria.ID.NPCID.BestiaryGirl" /> has an additional transformed texture as well.
			/// <br /> Unused in vanilla.
			/// <br /> Defaults to <c>0</c>.
			/// </summary>
			// Token: 0x040074CC RID: 29900
			public static int[] ExtraTextureCount = NPCID.Sets.Factory.CreateIntSet(0, new int[]
			{
				38,
				1,
				17,
				1,
				107,
				0,
				19,
				0,
				22,
				0,
				124,
				1,
				228,
				0,
				178,
				1,
				18,
				1,
				229,
				1,
				209,
				1,
				54,
				1,
				108,
				1,
				160,
				0,
				20,
				0,
				369,
				1,
				453,
				1,
				368,
				1,
				207,
				1,
				227,
				1,
				208,
				0,
				142,
				1,
				441,
				1,
				353,
				1,
				633,
				1,
				550,
				0,
				588,
				1,
				633,
				2,
				663,
				1,
				638,
				0,
				637,
				0,
				656,
				0,
				670,
				0,
				678,
				0,
				679,
				0,
				680,
				0,
				681,
				0,
				682,
				0,
				683,
				0,
				684,
				0
			});

			/// <summary>
			/// The index for <see cref="F:Terraria.ID.NPCID.Sets.TownNPCsFramingGroups" /> for a given NPC type (<see cref="F:Terraria.NPC.type" />). See <see cref="F:Terraria.ID.NPCID.Sets.TownNPCsFramingGroups" /> for more info.
			/// <br /> Defaults to <c>0</c>.
			/// </summary>
			// Token: 0x040074CD RID: 29901
			public static int[] NPCFramingGroup = NPCID.Sets.Factory.CreateIntSet(0, new int[]
			{
				18,
				1,
				20,
				1,
				208,
				1,
				178,
				1,
				124,
				1,
				353,
				1,
				633,
				1,
				369,
				2,
				160,
				3,
				637,
				4,
				638,
				5,
				656,
				6,
				670,
				7,
				678,
				7,
				679,
				7,
				680,
				7,
				681,
				7,
				682,
				7,
				683,
				7,
				684,
				7
			});

			/// <summary>
			/// If <see langword="true" /> for a given NPC type (<see cref="F:Terraria.NPC.type" />), then that NPC can hit players that are submerged in shimmer.
			/// <br /> Defaults to <see langword="false" />.
			/// </summary>
			/// <remarks>Bosses (<see cref="F:Terraria.NPC.boss" />) and invasion enemies (<see cref="M:Terraria.NPC.GetNPCInvasionGroup(System.Int32)" />) can always hit shimmered players.</remarks>
			// Token: 0x040074CE RID: 29902
			public static bool[] CanHitPastShimmer = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				535,
				5,
				13,
				14,
				15,
				666,
				267,
				36,
				210,
				211,
				115,
				116,
				117,
				118,
				119,
				658,
				659,
				660,
				134,
				135,
				136,
				139,
				128,
				131,
				129,
				130,
				263,
				264,
				246,
				249,
				247,
				248,
				371,
				372,
				373,
				566,
				567,
				440,
				522,
				523,
				521,
				454,
				455,
				456,
				457,
				458,
				459,
				397,
				396,
				400
			});

			/// <summary>
			/// The vertical offset, in pixels, that this NPC's party hat will draw. Indexed using values from <see cref="F:Terraria.ID.NPCID.Sets.NPCFramingGroup" />.
			/// <br>Group 0: The default for all Town NPCs. This group matches up with the standard walking animations which are the same animations that the player uses.</br>
			/// <br>Group 1: Used by Nurse, Dryad, Party Girl, Steampunker, Mechanic, Stylist, Zoologist. These Town NPCs have only 12 walking frames.</br>
			/// <br>Group 2: Used by Angler.</br>
			/// <br>Group 3: Used by Truffle.</br>
			/// <br>Group 4: Used by Town Cat.</br>
			/// <br>Group 5: Used by Town Dog.</br>
			/// <br>Group 6: Used by Town Bunny.</br>
			/// <br>Group 7: Used by all Town Slimes.</br>
			/// <br>Group 8: No offset (added by tModLoader).</br>
			/// </summary>
			/// <remarks><see cref="F:Terraria.ID.NPCID.Sets.HatOffsetY" /> applies the same offset to all frames, this applies a specific offset per frame.</remarks>
			// Token: 0x040074CF RID: 29903
			public static int[][] TownNPCsFramingGroups = new int[][]
			{
				new int[]
				{
					0,
					0,
					0,
					-2,
					-2,
					-2,
					0,
					0,
					0,
					0,
					-2,
					-2,
					-2,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0
				},
				new int[]
				{
					0,
					0,
					0,
					-2,
					-2,
					-2,
					0,
					0,
					0,
					-2,
					-2,
					-2,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0
				},
				new int[]
				{
					0,
					0,
					0,
					-2,
					-2,
					-2,
					0,
					0,
					-2,
					-2,
					-2,
					-2,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0
				},
				new int[]
				{
					0,
					0,
					-2,
					0,
					0,
					0,
					0,
					-2,
					-2,
					-2,
					0,
					0,
					0,
					0,
					-2,
					-2,
					0,
					0,
					0,
					0,
					0,
					0
				},
				new int[]
				{
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					2,
					2,
					4,
					6,
					4,
					2,
					2,
					-2,
					-4,
					-6,
					-4,
					-2,
					-4,
					-4,
					-6,
					-6,
					-6,
					-4
				},
				new int[]
				{
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					-2,
					-2,
					-2,
					0,
					0,
					-2,
					-2,
					0,
					0,
					4,
					6,
					6,
					6,
					6,
					4,
					4,
					4,
					4,
					4,
					4
				},
				new int[]
				{
					0,
					0,
					-2,
					-4,
					-4,
					-2,
					0,
					-2,
					0,
					0,
					2,
					4,
					6,
					4,
					2,
					0,
					-2,
					-4,
					-6,
					-6,
					-6,
					-6,
					-6,
					-6,
					-4,
					-2
				},
				new int[]
				{
					0,
					-2,
					0,
					-2,
					-4,
					-6,
					-4,
					-2,
					0,
					0,
					2,
					2,
					4,
					2
				},
				new int[1]
			};

			/// <summary>
			/// Whether or not the spawned NPC will start looking for a suitable slot from the end of <seealso cref="F:Terraria.Main.npc" />, ignoring the Start parameter of <see cref="M:Terraria.NPC.NewNPC(Terraria.DataStructures.IEntitySource,System.Int32,System.Int32,System.Int32,System.Int32,System.Single,System.Single,System.Single,System.Single,System.Int32)" />.
			/// Useful if you have a multi-segmented boss and want its parts to draw over the main body (body will be in this set).
			/// </summary>
			// Token: 0x040074D0 RID: 29904
			public static bool[] SpawnFromLastEmptySlot = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				222,
				245
			});

			/// <summary>
			/// If true, the given Town NPC won't drop a tombstone on death in hardcore mode and will have the "NPC has left!" death message unless specified otherwise by <see cref="P:Terraria.ModLoader.ModNPC.DeathMessage" />.
			/// <br /> This does NOT affect the gore spawned when the NPC dies.
			/// <para /> Defaults to <see langword="false" />.
			/// </summary>
			// Token: 0x040074D1 RID: 29905
			public static bool[] IsTownChild = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				369,
				663
			});

			/// <summary>
			/// Whether or not a given NPC will act like a town NPC in terms of AI, animations, and attacks, but not in other regards, such as appearing on the minimap, like the bone merchant in vanilla.
			/// </summary>
			// Token: 0x040074D2 RID: 29906
			public static bool[] ActsLikeTownNPC = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				453
			});

			/// <summary>
			/// If true, the given NPC will not count towards town NPC happiness and won't have a happiness button. Pets (<see cref="F:Terraria.ID.NPCID.Sets.IsTownPet" />) do not need to set this.
			/// </summary>
			// Token: 0x040074D3 RID: 29907
			public static bool[] NoTownNPCHappiness = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				37,
				368,
				453
			});

			/// <summary>
			/// Whether or not a given NPC will spawn with a custom name like a town NPC. In order to determine what name will be selected, override the TownNPCName hook.
			/// True will force a name to be rolled regardless of vanilla behavior. False will have vanilla handle the naming.
			/// </summary>
			// Token: 0x040074D4 RID: 29908
			public static bool[] SpawnsWithCustomName = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				453
			});

			/// <summary>
			/// Whether or not a given NPC can sit on suitable furniture (<see cref="F:Terraria.ID.TileID.Sets.CanBeSatOnForNPCs" />)
			/// </summary>
			// Token: 0x040074D5 RID: 29909
			public static bool[] CannotSitOnFurniture = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				638,
				656
			});

			/// <summary>
			/// Whether or not a given NPC is excluded from dropping hardmode souls (Soul of Night/Light)
			/// <br />Contains vanilla NPCs that are easy to spawn in large numbers, preventing easy soul farming
			/// <br />Do not add your NPC to this if it would be excluded automatically (i.e. critter, town NPC, or no coin drops)
			/// </summary>
			// Token: 0x040074D6 RID: 29910
			public static bool[] CannotDropSouls = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				1,
				13,
				14,
				15,
				121,
				535
			});

			/// <summary>
			/// Whether or not this NPC can still interact with doors if they use the Vanilla TownNPC aiStyle (AKA aiStyle == 7)
			/// but are not actually marked as Town NPCs (AKA npc.townNPC == true).
			/// </summary>
			/// <remarks>
			/// Note: This set DOES NOT DO ANYTHING if your NPC doesn't use the Vanilla TownNPC aiStyle (aiStyle == 7).
			/// </remarks>
			// Token: 0x040074D7 RID: 29911
			public static bool[] AllowDoorInteraction = NPCID.Sets.Factory.CreateBoolSet(Array.Empty<int>());

			/// <summary>
			/// If <see langword="true" />, this NPC type (<see cref="F:Terraria.NPC.type" />) will be immune to all debuffs and "tag" buffs by default.<br /><br />
			/// Use this for special NPCs that cannot be hit at all, such as fairy critters, container NPCs like Martian Saucer and Pirate Ship, bound town slimes, and Blazing Wheel. Dungeon Guardian also is in this set to prevent the bonus damage from "tag" buffs.<br /><br />
			/// If the NPC should be attacked, it is recommended to set <see cref="F:Terraria.ID.NPCID.Sets.ImmuneToRegularBuffs" /> to <see langword="true" /> instead. This will prevent all debuffs except "tag" buffs (<see cref="F:Terraria.ID.BuffID.Sets.IsATagBuff" />), which are intended to affect enemies typically seen as immune to all debuffs. Tag debuffs are special debuffs that facilitate combat mechanics, they are not something that adversely affects NPC.<br /><br />
			/// Modders can specify specific buffs to be vulnerable to by assigning <see cref="F:Terraria.ID.NPCID.Sets.SpecificDebuffImmunity" /> to false.
			/// </summary>
			// Token: 0x040074D8 RID: 29912
			public static bool[] ImmuneToAllBuffs;

			/// <summary>
			/// If <see langword="true" />, this NPC type (<see cref="F:Terraria.NPC.type" />) will be immune to all debuffs except tag debuffs (<see cref="F:Terraria.ID.BuffID.Sets.IsATagBuff" />) by default.<br /><br />
			/// Use this for NPCs that can be attacked that should be immune to all normal debuffs. Tag debuffs are special debuffs that facilitate combat mechanics, such as the "summon tag damage" applied by whip weapons. Wraith, Reaper, Lunatic Cultist, the Celestial Pillars, The Destroyer, and the Martian Saucer Turret/Cannon/Core are examples of NPCs that use this setting.<br /><br />
			/// Modders can specify specific buffs to be vulnerable to by assigning <see cref="F:Terraria.ID.NPCID.Sets.SpecificDebuffImmunity" /> to false.
			/// </summary>
			// Token: 0x040074D9 RID: 29913
			public static bool[] ImmuneToRegularBuffs;

			/// <summary>
			/// Indexed by NPC type and then Buff type. If <see langword="true" />, this NPC type (<see cref="F:Terraria.NPC.type" />) will be immune (<see cref="F:Terraria.NPC.buffImmune" />) to the specified buff type. If <see langword="false" />, the NPC will not be immune.<br /><br />
			/// By default, NPCs aren't immune to any buffs, but <see cref="F:Terraria.ID.NPCID.Sets.ImmuneToRegularBuffs" /> or <see cref="F:Terraria.ID.NPCID.Sets.ImmuneToAllBuffs" /> can make an NPC immune to all buffs. The values in this set override those settings.<br /><br />
			/// Additionally, the effects of <see cref="F:Terraria.ID.BuffID.Sets.GrantImmunityWith" /> will also be applied. Inherited buff immunities do not need to be specifically assigned, as they will be automatically applied. Setting an inherited debuff to false in this set can be used to undo the effects of <see cref="F:Terraria.ID.BuffID.Sets.GrantImmunityWith" />, if needed.<br /><br />
			/// Defaults to <see langword="null" />, indicating no immunity override.<br />
			/// </summary>
			// Token: 0x040074DA RID: 29914
			public static bool?[][] SpecificDebuffImmunity;

			/// <summary>
			/// If <see langword="true" /> for a given NPC type (<see cref="F:Terraria.NPC.type" />), then that NPC belongs to the Goblin Army invasion.
			/// <br /> During the Goblin Army invasion, NPCs in this set will decrement <see cref="F:Terraria.Main.invasionSize" /> by the amount specified in <see cref="F:Terraria.ID.NPCID.Sets.InvasionSlotCount" /> when killed.
			/// <br /> If any NPC in this set is alive and <see cref="F:Terraria.ID.NPCID.Sets.InvasionSlotCount" /> is above 0, the Goblin Army music will play.
			/// <br /> Defaults to <see langword="false" />.
			/// </summary>
			// Token: 0x040074DB RID: 29915
			public static bool[] BelongsToInvasionGoblinArmy = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				26,
				27,
				28,
				29,
				111,
				471,
				472
			});

			/// <summary>
			/// If <see langword="true" /> for a given NPC type (<see cref="F:Terraria.NPC.type" />), then that NPC belongs to the Frost Legion invasion.
			/// <br /> During the Frost Legion invasion, NPCs in this set will decrement <see cref="F:Terraria.Main.invasionSize" /> by the amount specified in <see cref="F:Terraria.ID.NPCID.Sets.InvasionSlotCount" /> when killed.
			/// <br /> If any NPC in this set is alive and <see cref="F:Terraria.ID.NPCID.Sets.InvasionSlotCount" /> is above 0, the Boss 3 music will play.
			/// <br /> Defaults to <see langword="false" />.
			/// </summary>
			// Token: 0x040074DC RID: 29916
			public static bool[] BelongsToInvasionFrostLegion = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				143,
				144,
				145
			});

			/// <summary>
			/// If <see langword="true" /> for a given NPC type (<see cref="F:Terraria.NPC.type" />), then that NPC belongs to the Pirate invasion.
			/// <br /> During the Pirate invasion, NPCs in this set will decrement <see cref="F:Terraria.Main.invasionSize" /> by the amount specified in <see cref="F:Terraria.ID.NPCID.Sets.InvasionSlotCount" /> when killed.
			/// <br /> If any NPC in this set is alive and <see cref="F:Terraria.ID.NPCID.Sets.InvasionSlotCount" /> is above 0, the Pirate Invasion music will play.
			/// <br /> Defaults to <see langword="false" />.
			/// </summary>
			// Token: 0x040074DD RID: 29917
			public static bool[] BelongsToInvasionPirate = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				212,
				213,
				214,
				215,
				216,
				491
			});

			/// <summary>
			/// If <see langword="true" /> for a given NPC type (<see cref="F:Terraria.NPC.type" />), then that NPC belongs to the Martian Madness invasion.
			/// <br /> During the Martian Madness invasion, NPCs in this set will decrement <see cref="F:Terraria.Main.invasionSize" /> by the amount specified in <see cref="F:Terraria.ID.NPCID.Sets.InvasionSlotCount" /> when killed.
			/// <br /> If any NPC in this set is alive and <see cref="F:Terraria.ID.NPCID.Sets.InvasionSlotCount" /> is above 0, the Martian Madness music will play.
			/// <br /> Defaults to <see langword="false" />.
			/// </summary>
			// Token: 0x040074DE RID: 29918
			public static bool[] BelongsToInvasionMartianMadness = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				381,
				382,
				383,
				385,
				386,
				387,
				388,
				389,
				390,
				391,
				395,
				520
			});

			/// <summary>
			/// If <see langword="true" /> for a given NPC type (<see cref="F:Terraria.NPC.type" />), then that NPC will not play its associated invasion music.
			/// <br /> By default, alive NPCs in any BelongsToInvasion set will automatically play the associated invasion music if <see cref="F:Terraria.ID.NPCID.Sets.InvasionSlotCount" /> is above 0.
			/// <br /> Defaults to <see langword="false" />.
			/// </summary>
			// Token: 0x040074DF RID: 29919
			public static bool[] NoInvasionMusic = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				387
			});

			/// <summary>
			/// If above 0 for a given NPC type (<see cref="F:Terraria.NPC.type" />), and its associated invasion is NOT a wave-based one, then that NPC will decrement <see cref="F:Terraria.Main.invasionSize" /> by that amount when killed.
			/// <br /> If this NPC's entry is 0, it won't play its associated invasion's music when alive.
			/// </summary>
			/// <remarks>
			/// Note: Even though this defaults to 1, this set should only be checked if <see cref="M:Terraria.NPC.GetNPCInvasionGroup(System.Int32)" /> is above 0 or if any BelongsToInvasion sets are <see langword="true" />.
			/// </remarks>
			// Token: 0x040074E0 RID: 29920
			public static int[] InvasionSlotCount = NPCID.Sets.Factory.CreateIntSet(1, new int[]
			{
				216,
				5,
				395,
				10,
				491,
				10,
				471,
				10,
				472,
				0,
				387,
				0
			});

			/// <summary>
			/// While petting, the number of pixels away the player stands from the NPC. Defaults to 36 pixels.
			/// </summary>
			// Token: 0x040074E1 RID: 29921
			public static int[] PlayerDistanceWhilePetting = NPCID.Sets.Factory.CreateIntSet(36, new int[]
			{
				637,
				28,
				656,
				24,
				670,
				26,
				678,
				26,
				679,
				26,
				680,
				26,
				681,
				26,
				683,
				26,
				682,
				22,
				684,
				20
			});

			/// <summary>
			/// While petting, the player's arm will be angled up by default. If the NPC is in this set, the player's armor will be angled down instead. Defaults to false.
			/// </summary>
			// Token: 0x040074E2 RID: 29922
			public static bool[] IsPetSmallForPetting = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				637,
				656,
				670,
				678,
				679,
				680,
				681,
				683,
				682,
				684
			});

			/// <summary>
			/// NPC in this set do not drop resource pickups, such as hearts or star items. Vanilla entries in this set include MotherSlime, CorruptSlime, and Slimer, all of which spawn other NPC when killed, suggesting that they split apart rather than died, hinting at why they shouldn't drop resource pickups.
			/// <para /> Defaults to false.
			/// </summary>
			// Token: 0x040074E3 RID: 29923
			public static bool[] NeverDropsResourcePickups = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				16,
				81,
				121
			});

			/// <summary>
			/// This NPC will not despawn due to being far offscreen, but will still count towards <see cref="F:Terraria.Player.nearbyActiveNPCs" />. Unlike returning false in <see cref="M:Terraria.ModLoader.ModNPC.CheckActive" />, this NPC still counts towards spawn limits. 
			/// </summary>
			// Token: 0x040074E4 RID: 29924
			public static bool[] DoesntDespawnToInactivityAndCountsNPCSlots = NPCID.Sets.Factory.CreateBoolSet(new int[]
			{
				668
			});

			/// <summary>
			/// Stores the draw parameters for an NPC type (<see cref="F:Terraria.NPC.type" />) in the Bestiary.
			/// <br /> <strong>Do not use <see langword="default" /> to create this struct.</strong> Use <see langword="new" /> <see cref="M:Terraria.ID.NPCID.Sets.NPCBestiaryDrawModifiers.#ctor" /> instead to set proper default values.
			/// </summary>
			// Token: 0x02000E45 RID: 3653
			public struct NPCBestiaryDrawModifiers
			{
				/// <summary>
				/// Creates a new <see cref="T:Terraria.ID.NPCID.Sets.NPCBestiaryDrawModifiers" /> with default values.
				/// </summary>
				/// <param name="seriouslyWhyCantStructsHaveParameterlessConstructors">Unused.</param>
				// Token: 0x060065B1 RID: 26033 RVA: 0x006DFEAC File Offset: 0x006DE0AC
				[Obsolete("Use the no argument constructor instead")]
				public NPCBestiaryDrawModifiers(int seriouslyWhyCantStructsHaveParameterlessConstructors)
				{
					this.Position = default(Vector2);
					this.Rotation = 0f;
					this.Scale = 1f;
					this.PortraitScale = new float?(1f);
					this.Hide = false;
					this.IsWet = false;
					this.Frame = null;
					this.Direction = null;
					this.SpriteDirection = null;
					this.Velocity = 0f;
					this.PortraitPositionXOverride = null;
					this.PortraitPositionYOverride = null;
					this.CustomTexturePath = null;
				}

				/// <inheritdoc cref="M:Terraria.ID.NPCID.Sets.NPCBestiaryDrawModifiers.#ctor(System.Int32)" />
				// Token: 0x060065B2 RID: 26034 RVA: 0x006DFF47 File Offset: 0x006DE147
				public NPCBestiaryDrawModifiers()
				{
					this = new NPCID.Sets.NPCBestiaryDrawModifiers(0);
				}

				/// <summary>
				/// The offset of this <see cref="T:Terraria.NPC" />'s Bestiary image in pixels.
				/// <br /> Defaults to <see cref="P:Microsoft.Xna.Framework.Vector2.Zero" />.
				/// </summary>
				// Token: 0x04007CF8 RID: 31992
				public Vector2 Position;

				/// <summary>
				/// A custom value for <see cref="F:Terraria.ID.NPCID.Sets.NPCBestiaryDrawModifiers.Position" />.X to use when drawing this <see cref="T:Terraria.NPC" />'s Bestiary portrait. If <see langword="null" />, then do not override the value.
				/// <br /> Defaults to <see langword="null" />.
				/// </summary>
				// Token: 0x04007CF9 RID: 31993
				public float? PortraitPositionXOverride;

				/// <summary>
				/// A custom value for <see cref="F:Terraria.ID.NPCID.Sets.NPCBestiaryDrawModifiers.Position" />.Y to use when drawing this <see cref="T:Terraria.NPC" />'s Bestiary portrait. If <see langword="null" />, then do not override the value.
				/// <br /> Defaults to <see langword="null" />.
				/// </summary>
				// Token: 0x04007CFA RID: 31994
				public float? PortraitPositionYOverride;

				/// <summary>
				/// The clockwise rotation of this <see cref="T:Terraria.NPC" />'s Bestiary image in radians.
				/// <br /> Defaults to <c>0f</c>.
				/// </summary>
				// Token: 0x04007CFB RID: 31995
				public float Rotation;

				/// <summary>
				/// The visual scale of this <see cref="T:Terraria.NPC" />'s Bestiary image.
				/// <br /> Defaults to <c>1f</c>.
				/// </summary>
				// Token: 0x04007CFC RID: 31996
				public float Scale;

				/// <summary>
				/// A custom value for <see cref="F:Terraria.ID.NPCID.Sets.NPCBestiaryDrawModifiers.Scale" /> to use when drawing this <see cref="T:Terraria.NPC" />'s Bestiary portrait. If <see langword="null" />, then do not override the value.
				/// <br /> Defaults to <see langword="null" />.
				/// </summary>
				// Token: 0x04007CFD RID: 31997
				public float? PortraitScale;

				/// <summary>
				/// If <see langword="true" />, this <see cref="T:Terraria.NPC" /> will never show up in the Bestiary.
				/// <br /> Useful for multi-segment <see cref="T:Terraria.NPC" />s.
				/// <br /> Defaults to <see langword="false" />.
				/// </summary>
				// Token: 0x04007CFE RID: 31998
				public bool Hide;

				/// <summary>
				/// The specific <see cref="F:Terraria.Entity.wet" /> to use when drawing this <see cref="T:Terraria.NPC" />'s Bestiary image.
				/// <br /> Useful for <see cref="T:Terraria.NPC" />s that draw differently when wet.
				/// <br /> Defaults to <see langword="false" />.
				/// </summary>
				// Token: 0x04007CFF RID: 31999
				public bool IsWet;

				/// <summary>
				/// The specific vertical frame to use when drawing this <see cref="T:Terraria.NPC" /> in the Bestiary.
				/// <br /> If <see langword="null" />, then this <see cref="T:Terraria.NPC" /> will play a default walking animation.
				/// <br /> Defaults to <see langword="null" />.
				/// </summary>
				// Token: 0x04007D00 RID: 32000
				public int? Frame;

				/// <summary>
				/// The specific <see cref="F:Terraria.Entity.direction" /> to use when drawing this <see cref="T:Terraria.NPC" />'s Bestiary image.
				/// <br /> If <see langword="null" />, then <c>-1</c> is used.
				/// <br /> Defaults to <see langword="null" />.
				/// </summary>
				// Token: 0x04007D01 RID: 32001
				public int? Direction;

				/// <summary>
				/// The specific <see cref="F:Terraria.NPC.spriteDirection" /> to use when drawing this <see cref="T:Terraria.NPC" />'s Bestiary image.
				/// <br /> If <see langword="null" />, then the value of <see cref="F:Terraria.ID.NPCID.Sets.NPCBestiaryDrawModifiers.Direction" /> is used.
				/// <br /> Defaults to <see langword="null" />.
				/// </summary>
				// Token: 0x04007D02 RID: 32002
				public int? SpriteDirection;

				/// <summary>
				/// The magnitude of <see cref="F:Terraria.Entity.velocity" />.X to use when drawing this <see cref="T:Terraria.NPC" />'s Bestiary image, either when in Portrait mode or when hovered.
				/// <br /> Defaults to <c>0f</c>.
				/// </summary>
				/// <remarks>This value is only used if <see cref="F:Terraria.ID.NPCID.Sets.NPCBestiaryDrawModifiers.Frame" /> is <see langword="null" />.</remarks>
				// Token: 0x04007D03 RID: 32003
				public float Velocity;

				/// <summary>
				/// The path to the custom texture to use when drawing this <see cref="T:Terraria.NPC" />'s Bestiary image.
				/// <br /> If <see langword="null" />, then this NPC's default texture is used.
				/// <br /> Defaults to <see langword="null" />.
				/// </summary>
				// Token: 0x04007D04 RID: 32004
				public string CustomTexturePath;
			}

			// Token: 0x02000E46 RID: 3654
			private static class LocalBuffID
			{
				// Token: 0x04007D05 RID: 32005
				public const int Confused = 31;

				// Token: 0x04007D06 RID: 32006
				public const int Poisoned = 20;

				// Token: 0x04007D07 RID: 32007
				public const int OnFire = 24;

				// Token: 0x04007D08 RID: 32008
				public const int OnFire3 = 323;

				// Token: 0x04007D09 RID: 32009
				public const int ShadowFlame = 153;

				// Token: 0x04007D0A RID: 32010
				public const int Daybreak = 189;

				// Token: 0x04007D0B RID: 32011
				public const int Frostburn = 44;

				// Token: 0x04007D0C RID: 32012
				public const int Frostburn2 = 324;

				// Token: 0x04007D0D RID: 32013
				public const int CursedInferno = 39;

				// Token: 0x04007D0E RID: 32014
				public const int BetsysCurse = 203;

				// Token: 0x04007D0F RID: 32015
				public const int Ichor = 69;

				// Token: 0x04007D10 RID: 32016
				public const int Venom = 70;

				// Token: 0x04007D11 RID: 32017
				public const int Oiled = 204;

				// Token: 0x04007D12 RID: 32018
				public const int BoneJavelin = 169;

				// Token: 0x04007D13 RID: 32019
				public const int TentacleSpike = 337;

				// Token: 0x04007D14 RID: 32020
				public const int BloodButcherer = 344;
			}
		}
	}
}
