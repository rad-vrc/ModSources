using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria.Audio;

namespace Terraria.ID
{
	// Token: 0x020001AF RID: 431
	public class SoundID
	{
		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06001B8D RID: 7053 RVA: 0x004E8C62 File Offset: 0x004E6E62
		public static int TrackableLegacySoundCount
		{
			get
			{
				return SoundID._trackableLegacySoundPathList.Count;
			}
		}

		// Token: 0x06001B8E RID: 7054 RVA: 0x004E8C6E File Offset: 0x004E6E6E
		public static string GetTrackableLegacySoundPath(int id)
		{
			return SoundID._trackableLegacySoundPathList[id];
		}

		// Token: 0x06001B8F RID: 7055 RVA: 0x004E8C7B File Offset: 0x004E6E7B
		private static LegacySoundStyle CreateTrackable(string name, SoundID.SoundStyleDefaults defaults)
		{
			return SoundID.CreateTrackable(name, 1, defaults.Type).WithPitchVariance(defaults.PitchVariance).WithVolume(defaults.Volume);
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x004E8CA0 File Offset: 0x004E6EA0
		private static LegacySoundStyle CreateTrackable(string name, int variations, SoundID.SoundStyleDefaults defaults)
		{
			return SoundID.CreateTrackable(name, variations, defaults.Type).WithPitchVariance(defaults.PitchVariance).WithVolume(defaults.Volume);
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x004E8CC5 File Offset: 0x004E6EC5
		private static LegacySoundStyle CreateTrackable(string name, SoundType type = SoundType.Sound)
		{
			return SoundID.CreateTrackable(name, 1, type);
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x004E8CD0 File Offset: 0x004E6ED0
		private static LegacySoundStyle CreateTrackable(string name, int variations, SoundType type = SoundType.Sound)
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
					SoundID._trackableLegacySoundPathList.Add(name + "_" + i);
				}
			}
			return new LegacySoundStyle(42, count, variations, type);
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x004E8D3C File Offset: 0x004E6F3C
		public static void FillAccessMap()
		{
			Dictionary<string, LegacySoundStyle> ret = new Dictionary<string, LegacySoundStyle>();
			Dictionary<string, ushort> ret2 = new Dictionary<string, ushort>();
			Dictionary<ushort, LegacySoundStyle> ret3 = new Dictionary<ushort, LegacySoundStyle>();
			ushort nextIndex = 0;
			List<FieldInfo> list = (from f in typeof(SoundID).GetFields(BindingFlags.Static | BindingFlags.Public)
			where f.FieldType == typeof(LegacySoundStyle)
			select f).ToList<FieldInfo>();
			list.Sort((FieldInfo a, FieldInfo b) => string.Compare(a.Name, b.Name));
			list.ForEach(delegate(FieldInfo field)
			{
				ret[field.Name] = (LegacySoundStyle)field.GetValue(null);
				ushort nextIndex;
				ret2[field.Name] = nextIndex;
				ret3[nextIndex] = (LegacySoundStyle)field.GetValue(null);
				nextIndex = nextIndex;
				nextIndex += 1;
			});
			SoundID.SoundByName = ret;
			SoundID.IndexByName = ret2;
			SoundID.SoundByIndex = ret3;
		}

		// Token: 0x04001810 RID: 6160
		private static readonly SoundID.SoundStyleDefaults ItemDefaults = new SoundID.SoundStyleDefaults(1f, 0.06f, SoundType.Sound);

		// Token: 0x04001811 RID: 6161
		public const int Dig = 0;

		// Token: 0x04001812 RID: 6162
		public const int PlayerHit = 1;

		// Token: 0x04001813 RID: 6163
		public const int Item = 2;

		// Token: 0x04001814 RID: 6164
		public const int NPCHit = 3;

		// Token: 0x04001815 RID: 6165
		public const int NPCKilled = 4;

		// Token: 0x04001816 RID: 6166
		public const int PlayerKilled = 5;

		// Token: 0x04001817 RID: 6167
		public const int Grass = 6;

		// Token: 0x04001818 RID: 6168
		public const int Grab = 7;

		// Token: 0x04001819 RID: 6169
		public const int DoorOpen = 8;

		// Token: 0x0400181A RID: 6170
		public const int DoorClosed = 9;

		// Token: 0x0400181B RID: 6171
		public const int MenuOpen = 10;

		// Token: 0x0400181C RID: 6172
		public const int MenuClose = 11;

		// Token: 0x0400181D RID: 6173
		public const int MenuTick = 12;

		// Token: 0x0400181E RID: 6174
		public const int Shatter = 13;

		// Token: 0x0400181F RID: 6175
		public const int ZombieMoan = 14;

		// Token: 0x04001820 RID: 6176
		public const int Roar = 15;

		// Token: 0x04001821 RID: 6177
		public const int DoubleJump = 16;

		// Token: 0x04001822 RID: 6178
		public const int Run = 17;

		// Token: 0x04001823 RID: 6179
		public const int Coins = 18;

		// Token: 0x04001824 RID: 6180
		public const int Splash = 19;

		// Token: 0x04001825 RID: 6181
		public const int FemaleHit = 20;

		// Token: 0x04001826 RID: 6182
		public const int Tink = 21;

		// Token: 0x04001827 RID: 6183
		public const int Unlock = 22;

		// Token: 0x04001828 RID: 6184
		public const int Drown = 23;

		// Token: 0x04001829 RID: 6185
		public const int Chat = 24;

		// Token: 0x0400182A RID: 6186
		public const int MaxMana = 25;

		// Token: 0x0400182B RID: 6187
		public const int Mummy = 26;

		// Token: 0x0400182C RID: 6188
		public const int Pixie = 27;

		// Token: 0x0400182D RID: 6189
		public const int Mech = 28;

		// Token: 0x0400182E RID: 6190
		public const int Zombie = 29;

		// Token: 0x0400182F RID: 6191
		public const int Duck = 30;

		// Token: 0x04001830 RID: 6192
		public const int Frog = 31;

		// Token: 0x04001831 RID: 6193
		public const int Bird = 32;

		// Token: 0x04001832 RID: 6194
		public const int Critter = 33;

		// Token: 0x04001833 RID: 6195
		public const int Waterfall = 34;

		// Token: 0x04001834 RID: 6196
		public const int Lavafall = 35;

		// Token: 0x04001835 RID: 6197
		public const int ForceRoar = 36;

		// Token: 0x04001836 RID: 6198
		public const int Meowmere = 37;

		// Token: 0x04001837 RID: 6199
		public const int CoinPickup = 38;

		// Token: 0x04001838 RID: 6200
		public const int Drip = 39;

		// Token: 0x04001839 RID: 6201
		public const int Camera = 40;

		// Token: 0x0400183A RID: 6202
		public const int MoonLord = 41;

		// Token: 0x0400183B RID: 6203
		public const int Trackable = 42;

		// Token: 0x0400183C RID: 6204
		public const int Thunder = 43;

		// Token: 0x0400183D RID: 6205
		public const int Seagull = 44;

		// Token: 0x0400183E RID: 6206
		public const int Dolphin = 45;

		// Token: 0x0400183F RID: 6207
		public const int Owl = 46;

		// Token: 0x04001840 RID: 6208
		public const int GuitarC = 47;

		// Token: 0x04001841 RID: 6209
		public const int GuitarD = 48;

		// Token: 0x04001842 RID: 6210
		public const int GuitarEm = 49;

		// Token: 0x04001843 RID: 6211
		public const int GuitarG = 50;

		// Token: 0x04001844 RID: 6212
		public const int GuitarBm = 51;

		// Token: 0x04001845 RID: 6213
		public const int GuitarAm = 52;

		// Token: 0x04001846 RID: 6214
		public const int DrumHiHat = 53;

		// Token: 0x04001847 RID: 6215
		public const int DrumTomHigh = 54;

		// Token: 0x04001848 RID: 6216
		public const int DrumTomLow = 55;

		// Token: 0x04001849 RID: 6217
		public const int DrumTomMid = 56;

		// Token: 0x0400184A RID: 6218
		public const int DrumClosedHiHat = 57;

		// Token: 0x0400184B RID: 6219
		public const int DrumCymbal1 = 58;

		// Token: 0x0400184C RID: 6220
		public const int DrumCymbal2 = 59;

		// Token: 0x0400184D RID: 6221
		public const int DrumKick = 60;

		// Token: 0x0400184E RID: 6222
		public const int DrumTamaSnare = 61;

		// Token: 0x0400184F RID: 6223
		public const int DrumFloorTom = 62;

		// Token: 0x04001850 RID: 6224
		public const int Research = 63;

		// Token: 0x04001851 RID: 6225
		public const int ResearchComplete = 64;

		// Token: 0x04001852 RID: 6226
		public const int QueenSlime = 65;

		// Token: 0x04001853 RID: 6227
		public const int Clown = 66;

		// Token: 0x04001854 RID: 6228
		public const int Cockatiel = 67;

		// Token: 0x04001855 RID: 6229
		public const int Macaw = 68;

		// Token: 0x04001856 RID: 6230
		public const int Toucan = 69;

		// Token: 0x04001857 RID: 6231
		public static readonly LegacySoundStyle NPCHit1 = new LegacySoundStyle(3, 1, SoundType.Sound);

		// Token: 0x04001858 RID: 6232
		public static readonly LegacySoundStyle NPCHit2 = new LegacySoundStyle(3, 2, SoundType.Sound);

		// Token: 0x04001859 RID: 6233
		public static readonly LegacySoundStyle NPCHit3 = new LegacySoundStyle(3, 3, SoundType.Sound);

		// Token: 0x0400185A RID: 6234
		public static readonly LegacySoundStyle NPCHit4 = new LegacySoundStyle(3, 4, SoundType.Sound);

		// Token: 0x0400185B RID: 6235
		public static readonly LegacySoundStyle NPCHit5 = new LegacySoundStyle(3, 5, SoundType.Sound);

		// Token: 0x0400185C RID: 6236
		public static readonly LegacySoundStyle NPCHit6 = new LegacySoundStyle(3, 6, SoundType.Sound);

		// Token: 0x0400185D RID: 6237
		public static readonly LegacySoundStyle NPCHit7 = new LegacySoundStyle(3, 7, SoundType.Sound);

		// Token: 0x0400185E RID: 6238
		public static readonly LegacySoundStyle NPCHit8 = new LegacySoundStyle(3, 8, SoundType.Sound);

		// Token: 0x0400185F RID: 6239
		public static readonly LegacySoundStyle NPCHit9 = new LegacySoundStyle(3, 9, SoundType.Sound);

		// Token: 0x04001860 RID: 6240
		public static readonly LegacySoundStyle NPCHit10 = new LegacySoundStyle(3, 10, SoundType.Sound);

		// Token: 0x04001861 RID: 6241
		public static readonly LegacySoundStyle NPCHit11 = new LegacySoundStyle(3, 11, SoundType.Sound);

		// Token: 0x04001862 RID: 6242
		public static readonly LegacySoundStyle NPCHit12 = new LegacySoundStyle(3, 12, SoundType.Sound);

		// Token: 0x04001863 RID: 6243
		public static readonly LegacySoundStyle NPCHit13 = new LegacySoundStyle(3, 13, SoundType.Sound);

		// Token: 0x04001864 RID: 6244
		public static readonly LegacySoundStyle NPCHit14 = new LegacySoundStyle(3, 14, SoundType.Sound);

		// Token: 0x04001865 RID: 6245
		public static readonly LegacySoundStyle NPCHit15 = new LegacySoundStyle(3, 15, SoundType.Sound);

		// Token: 0x04001866 RID: 6246
		public static readonly LegacySoundStyle NPCHit16 = new LegacySoundStyle(3, 16, SoundType.Sound);

		// Token: 0x04001867 RID: 6247
		public static readonly LegacySoundStyle NPCHit17 = new LegacySoundStyle(3, 17, SoundType.Sound);

		// Token: 0x04001868 RID: 6248
		public static readonly LegacySoundStyle NPCHit18 = new LegacySoundStyle(3, 18, SoundType.Sound);

		// Token: 0x04001869 RID: 6249
		public static readonly LegacySoundStyle NPCHit19 = new LegacySoundStyle(3, 19, SoundType.Sound);

		// Token: 0x0400186A RID: 6250
		public static readonly LegacySoundStyle NPCHit20 = new LegacySoundStyle(3, 20, SoundType.Sound);

		// Token: 0x0400186B RID: 6251
		public static readonly LegacySoundStyle NPCHit21 = new LegacySoundStyle(3, 21, SoundType.Sound);

		// Token: 0x0400186C RID: 6252
		public static readonly LegacySoundStyle NPCHit22 = new LegacySoundStyle(3, 22, SoundType.Sound);

		// Token: 0x0400186D RID: 6253
		public static readonly LegacySoundStyle NPCHit23 = new LegacySoundStyle(3, 23, SoundType.Sound);

		// Token: 0x0400186E RID: 6254
		public static readonly LegacySoundStyle NPCHit24 = new LegacySoundStyle(3, 24, SoundType.Sound);

		// Token: 0x0400186F RID: 6255
		public static readonly LegacySoundStyle NPCHit25 = new LegacySoundStyle(3, 25, SoundType.Sound);

		// Token: 0x04001870 RID: 6256
		public static readonly LegacySoundStyle NPCHit26 = new LegacySoundStyle(3, 26, SoundType.Sound);

		// Token: 0x04001871 RID: 6257
		public static readonly LegacySoundStyle NPCHit27 = new LegacySoundStyle(3, 27, SoundType.Sound);

		// Token: 0x04001872 RID: 6258
		public static readonly LegacySoundStyle NPCHit28 = new LegacySoundStyle(3, 28, SoundType.Sound);

		// Token: 0x04001873 RID: 6259
		public static readonly LegacySoundStyle NPCHit29 = new LegacySoundStyle(3, 29, SoundType.Sound);

		// Token: 0x04001874 RID: 6260
		public static readonly LegacySoundStyle NPCHit30 = new LegacySoundStyle(3, 30, SoundType.Sound);

		// Token: 0x04001875 RID: 6261
		public static readonly LegacySoundStyle NPCHit31 = new LegacySoundStyle(3, 31, SoundType.Sound);

		// Token: 0x04001876 RID: 6262
		public static readonly LegacySoundStyle NPCHit32 = new LegacySoundStyle(3, 32, SoundType.Sound);

		// Token: 0x04001877 RID: 6263
		public static readonly LegacySoundStyle NPCHit33 = new LegacySoundStyle(3, 33, SoundType.Sound);

		// Token: 0x04001878 RID: 6264
		public static readonly LegacySoundStyle NPCHit34 = new LegacySoundStyle(3, 34, SoundType.Sound);

		// Token: 0x04001879 RID: 6265
		public static readonly LegacySoundStyle NPCHit35 = new LegacySoundStyle(3, 35, SoundType.Sound);

		// Token: 0x0400187A RID: 6266
		public static readonly LegacySoundStyle NPCHit36 = new LegacySoundStyle(3, 36, SoundType.Sound);

		// Token: 0x0400187B RID: 6267
		public static readonly LegacySoundStyle NPCHit37 = new LegacySoundStyle(3, 37, SoundType.Sound);

		// Token: 0x0400187C RID: 6268
		public static readonly LegacySoundStyle NPCHit38 = new LegacySoundStyle(3, 38, SoundType.Sound);

		// Token: 0x0400187D RID: 6269
		public static readonly LegacySoundStyle NPCHit39 = new LegacySoundStyle(3, 39, SoundType.Sound);

		// Token: 0x0400187E RID: 6270
		public static readonly LegacySoundStyle NPCHit40 = new LegacySoundStyle(3, 40, SoundType.Sound);

		// Token: 0x0400187F RID: 6271
		public static readonly LegacySoundStyle NPCHit41 = new LegacySoundStyle(3, 41, SoundType.Sound);

		// Token: 0x04001880 RID: 6272
		public static readonly LegacySoundStyle NPCHit42 = new LegacySoundStyle(3, 42, SoundType.Sound);

		// Token: 0x04001881 RID: 6273
		public static readonly LegacySoundStyle NPCHit43 = new LegacySoundStyle(3, 43, SoundType.Sound);

		// Token: 0x04001882 RID: 6274
		public static readonly LegacySoundStyle NPCHit44 = new LegacySoundStyle(3, 44, SoundType.Sound);

		// Token: 0x04001883 RID: 6275
		public static readonly LegacySoundStyle NPCHit45 = new LegacySoundStyle(3, 45, SoundType.Sound);

		// Token: 0x04001884 RID: 6276
		public static readonly LegacySoundStyle NPCHit46 = new LegacySoundStyle(3, 46, SoundType.Sound);

		// Token: 0x04001885 RID: 6277
		public static readonly LegacySoundStyle NPCHit47 = new LegacySoundStyle(3, 47, SoundType.Sound);

		// Token: 0x04001886 RID: 6278
		public static readonly LegacySoundStyle NPCHit48 = new LegacySoundStyle(3, 48, SoundType.Sound);

		// Token: 0x04001887 RID: 6279
		public static readonly LegacySoundStyle NPCHit49 = new LegacySoundStyle(3, 49, SoundType.Sound);

		// Token: 0x04001888 RID: 6280
		public static readonly LegacySoundStyle NPCHit50 = new LegacySoundStyle(3, 50, SoundType.Sound);

		// Token: 0x04001889 RID: 6281
		public static readonly LegacySoundStyle NPCHit51 = new LegacySoundStyle(3, 51, SoundType.Sound);

		// Token: 0x0400188A RID: 6282
		public static readonly LegacySoundStyle NPCHit52 = new LegacySoundStyle(3, 52, SoundType.Sound);

		// Token: 0x0400188B RID: 6283
		public static readonly LegacySoundStyle NPCHit53 = new LegacySoundStyle(3, 53, SoundType.Sound);

		// Token: 0x0400188C RID: 6284
		public static readonly LegacySoundStyle NPCHit54 = new LegacySoundStyle(3, 54, SoundType.Sound);

		// Token: 0x0400188D RID: 6285
		public static readonly LegacySoundStyle NPCHit55 = new LegacySoundStyle(3, 55, SoundType.Sound);

		// Token: 0x0400188E RID: 6286
		public static readonly LegacySoundStyle NPCHit56 = new LegacySoundStyle(3, 56, SoundType.Sound);

		// Token: 0x0400188F RID: 6287
		public static readonly LegacySoundStyle NPCHit57 = new LegacySoundStyle(3, 57, SoundType.Sound);

		// Token: 0x04001890 RID: 6288
		public static readonly LegacySoundStyle NPCDeath1 = new LegacySoundStyle(4, 1, SoundType.Sound);

		// Token: 0x04001891 RID: 6289
		public static readonly LegacySoundStyle NPCDeath2 = new LegacySoundStyle(4, 2, SoundType.Sound);

		// Token: 0x04001892 RID: 6290
		public static readonly LegacySoundStyle NPCDeath3 = new LegacySoundStyle(4, 3, SoundType.Sound);

		// Token: 0x04001893 RID: 6291
		public static readonly LegacySoundStyle NPCDeath4 = new LegacySoundStyle(4, 4, SoundType.Sound);

		// Token: 0x04001894 RID: 6292
		public static readonly LegacySoundStyle NPCDeath5 = new LegacySoundStyle(4, 5, SoundType.Sound);

		// Token: 0x04001895 RID: 6293
		public static readonly LegacySoundStyle NPCDeath6 = new LegacySoundStyle(4, 6, SoundType.Sound);

		// Token: 0x04001896 RID: 6294
		public static readonly LegacySoundStyle NPCDeath7 = new LegacySoundStyle(4, 7, SoundType.Sound);

		// Token: 0x04001897 RID: 6295
		public static readonly LegacySoundStyle NPCDeath8 = new LegacySoundStyle(4, 8, SoundType.Sound);

		// Token: 0x04001898 RID: 6296
		public static readonly LegacySoundStyle NPCDeath9 = new LegacySoundStyle(4, 9, SoundType.Sound);

		// Token: 0x04001899 RID: 6297
		public static readonly LegacySoundStyle NPCDeath10 = new LegacySoundStyle(4, 10, SoundType.Sound);

		// Token: 0x0400189A RID: 6298
		public static readonly LegacySoundStyle NPCDeath11 = new LegacySoundStyle(4, 11, SoundType.Sound);

		// Token: 0x0400189B RID: 6299
		public static readonly LegacySoundStyle NPCDeath12 = new LegacySoundStyle(4, 12, SoundType.Sound);

		// Token: 0x0400189C RID: 6300
		public static readonly LegacySoundStyle NPCDeath13 = new LegacySoundStyle(4, 13, SoundType.Sound);

		// Token: 0x0400189D RID: 6301
		public static readonly LegacySoundStyle NPCDeath14 = new LegacySoundStyle(4, 14, SoundType.Sound);

		// Token: 0x0400189E RID: 6302
		public static readonly LegacySoundStyle NPCDeath15 = new LegacySoundStyle(4, 15, SoundType.Sound);

		// Token: 0x0400189F RID: 6303
		public static readonly LegacySoundStyle NPCDeath16 = new LegacySoundStyle(4, 16, SoundType.Sound);

		// Token: 0x040018A0 RID: 6304
		public static readonly LegacySoundStyle NPCDeath17 = new LegacySoundStyle(4, 17, SoundType.Sound);

		// Token: 0x040018A1 RID: 6305
		public static readonly LegacySoundStyle NPCDeath18 = new LegacySoundStyle(4, 18, SoundType.Sound);

		// Token: 0x040018A2 RID: 6306
		public static readonly LegacySoundStyle NPCDeath19 = new LegacySoundStyle(4, 19, SoundType.Sound);

		// Token: 0x040018A3 RID: 6307
		public static readonly LegacySoundStyle NPCDeath20 = new LegacySoundStyle(4, 20, SoundType.Sound);

		// Token: 0x040018A4 RID: 6308
		public static readonly LegacySoundStyle NPCDeath21 = new LegacySoundStyle(4, 21, SoundType.Sound);

		// Token: 0x040018A5 RID: 6309
		public static readonly LegacySoundStyle NPCDeath22 = new LegacySoundStyle(4, 22, SoundType.Sound);

		// Token: 0x040018A6 RID: 6310
		public static readonly LegacySoundStyle NPCDeath23 = new LegacySoundStyle(4, 23, SoundType.Sound);

		// Token: 0x040018A7 RID: 6311
		public static readonly LegacySoundStyle NPCDeath24 = new LegacySoundStyle(4, 24, SoundType.Sound);

		// Token: 0x040018A8 RID: 6312
		public static readonly LegacySoundStyle NPCDeath25 = new LegacySoundStyle(4, 25, SoundType.Sound);

		// Token: 0x040018A9 RID: 6313
		public static readonly LegacySoundStyle NPCDeath26 = new LegacySoundStyle(4, 26, SoundType.Sound);

		// Token: 0x040018AA RID: 6314
		public static readonly LegacySoundStyle NPCDeath27 = new LegacySoundStyle(4, 27, SoundType.Sound);

		// Token: 0x040018AB RID: 6315
		public static readonly LegacySoundStyle NPCDeath28 = new LegacySoundStyle(4, 28, SoundType.Sound);

		// Token: 0x040018AC RID: 6316
		public static readonly LegacySoundStyle NPCDeath29 = new LegacySoundStyle(4, 29, SoundType.Sound);

		// Token: 0x040018AD RID: 6317
		public static readonly LegacySoundStyle NPCDeath30 = new LegacySoundStyle(4, 30, SoundType.Sound);

		// Token: 0x040018AE RID: 6318
		public static readonly LegacySoundStyle NPCDeath31 = new LegacySoundStyle(4, 31, SoundType.Sound);

		// Token: 0x040018AF RID: 6319
		public static readonly LegacySoundStyle NPCDeath32 = new LegacySoundStyle(4, 32, SoundType.Sound);

		// Token: 0x040018B0 RID: 6320
		public static readonly LegacySoundStyle NPCDeath33 = new LegacySoundStyle(4, 33, SoundType.Sound);

		// Token: 0x040018B1 RID: 6321
		public static readonly LegacySoundStyle NPCDeath34 = new LegacySoundStyle(4, 34, SoundType.Sound);

		// Token: 0x040018B2 RID: 6322
		public static readonly LegacySoundStyle NPCDeath35 = new LegacySoundStyle(4, 35, SoundType.Sound);

		// Token: 0x040018B3 RID: 6323
		public static readonly LegacySoundStyle NPCDeath36 = new LegacySoundStyle(4, 36, SoundType.Sound);

		// Token: 0x040018B4 RID: 6324
		public static readonly LegacySoundStyle NPCDeath37 = new LegacySoundStyle(4, 37, SoundType.Sound);

		// Token: 0x040018B5 RID: 6325
		public static readonly LegacySoundStyle NPCDeath38 = new LegacySoundStyle(4, 38, SoundType.Sound);

		// Token: 0x040018B6 RID: 6326
		public static readonly LegacySoundStyle NPCDeath39 = new LegacySoundStyle(4, 39, SoundType.Sound);

		// Token: 0x040018B7 RID: 6327
		public static readonly LegacySoundStyle NPCDeath40 = new LegacySoundStyle(4, 40, SoundType.Sound);

		// Token: 0x040018B8 RID: 6328
		public static readonly LegacySoundStyle NPCDeath41 = new LegacySoundStyle(4, 41, SoundType.Sound);

		// Token: 0x040018B9 RID: 6329
		public static readonly LegacySoundStyle NPCDeath42 = new LegacySoundStyle(4, 42, SoundType.Sound);

		// Token: 0x040018BA RID: 6330
		public static readonly LegacySoundStyle NPCDeath43 = new LegacySoundStyle(4, 43, SoundType.Sound);

		// Token: 0x040018BB RID: 6331
		public static readonly LegacySoundStyle NPCDeath44 = new LegacySoundStyle(4, 44, SoundType.Sound);

		// Token: 0x040018BC RID: 6332
		public static readonly LegacySoundStyle NPCDeath45 = new LegacySoundStyle(4, 45, SoundType.Sound);

		// Token: 0x040018BD RID: 6333
		public static readonly LegacySoundStyle NPCDeath46 = new LegacySoundStyle(4, 46, SoundType.Sound);

		// Token: 0x040018BE RID: 6334
		public static readonly LegacySoundStyle NPCDeath47 = new LegacySoundStyle(4, 47, SoundType.Sound);

		// Token: 0x040018BF RID: 6335
		public static readonly LegacySoundStyle NPCDeath48 = new LegacySoundStyle(4, 48, SoundType.Sound);

		// Token: 0x040018C0 RID: 6336
		public static readonly LegacySoundStyle NPCDeath49 = new LegacySoundStyle(4, 49, SoundType.Sound);

		// Token: 0x040018C1 RID: 6337
		public static readonly LegacySoundStyle NPCDeath50 = new LegacySoundStyle(4, 50, SoundType.Sound);

		// Token: 0x040018C2 RID: 6338
		public static readonly LegacySoundStyle NPCDeath51 = new LegacySoundStyle(4, 51, SoundType.Sound);

		// Token: 0x040018C3 RID: 6339
		public static readonly LegacySoundStyle NPCDeath52 = new LegacySoundStyle(4, 52, SoundType.Sound);

		// Token: 0x040018C4 RID: 6340
		public static readonly LegacySoundStyle NPCDeath53 = new LegacySoundStyle(4, 53, SoundType.Sound);

		// Token: 0x040018C5 RID: 6341
		public static readonly LegacySoundStyle NPCDeath54 = new LegacySoundStyle(4, 54, SoundType.Sound);

		// Token: 0x040018C6 RID: 6342
		public static readonly LegacySoundStyle NPCDeath55 = new LegacySoundStyle(4, 55, SoundType.Sound);

		// Token: 0x040018C7 RID: 6343
		public static readonly LegacySoundStyle NPCDeath56 = new LegacySoundStyle(4, 56, SoundType.Sound);

		// Token: 0x040018C8 RID: 6344
		public static readonly LegacySoundStyle NPCDeath57 = new LegacySoundStyle(4, 57, SoundType.Sound);

		// Token: 0x040018C9 RID: 6345
		public static readonly LegacySoundStyle NPCDeath58 = new LegacySoundStyle(4, 58, SoundType.Sound);

		// Token: 0x040018CA RID: 6346
		public static readonly LegacySoundStyle NPCDeath59 = new LegacySoundStyle(4, 59, SoundType.Sound);

		// Token: 0x040018CB RID: 6347
		public static readonly LegacySoundStyle NPCDeath60 = new LegacySoundStyle(4, 60, SoundType.Sound);

		// Token: 0x040018CC RID: 6348
		public static readonly LegacySoundStyle NPCDeath61 = new LegacySoundStyle(4, 61, SoundType.Sound);

		// Token: 0x040018CD RID: 6349
		public static readonly LegacySoundStyle NPCDeath62 = new LegacySoundStyle(4, 62, SoundType.Sound);

		// Token: 0x040018CE RID: 6350
		public static readonly LegacySoundStyle NPCDeath63 = new LegacySoundStyle(4, 63, SoundType.Sound);

		// Token: 0x040018CF RID: 6351
		public static readonly LegacySoundStyle NPCDeath64 = new LegacySoundStyle(4, 64, SoundType.Sound);

		// Token: 0x040018D0 RID: 6352
		public static readonly LegacySoundStyle NPCDeath65 = new LegacySoundStyle(4, 65, SoundType.Sound);

		// Token: 0x040018D1 RID: 6353
		public static readonly LegacySoundStyle NPCDeath66 = new LegacySoundStyle(4, 66, SoundType.Sound);

		// Token: 0x040018D2 RID: 6354
		public static short NPCDeathCount = 67;

		// Token: 0x040018D3 RID: 6355
		public static readonly LegacySoundStyle Item1 = new LegacySoundStyle(2, 1, SoundType.Sound);

		// Token: 0x040018D4 RID: 6356
		public static readonly LegacySoundStyle Item2 = new LegacySoundStyle(2, 2, SoundType.Sound);

		// Token: 0x040018D5 RID: 6357
		public static readonly LegacySoundStyle Item3 = new LegacySoundStyle(2, 3, SoundType.Sound);

		// Token: 0x040018D6 RID: 6358
		public static readonly LegacySoundStyle Item4 = new LegacySoundStyle(2, 4, SoundType.Sound);

		// Token: 0x040018D7 RID: 6359
		public static readonly LegacySoundStyle Item5 = new LegacySoundStyle(2, 5, SoundType.Sound);

		// Token: 0x040018D8 RID: 6360
		public static readonly LegacySoundStyle Item6 = new LegacySoundStyle(2, 6, SoundType.Sound);

		// Token: 0x040018D9 RID: 6361
		public static readonly LegacySoundStyle Item7 = new LegacySoundStyle(2, 7, SoundType.Sound);

		// Token: 0x040018DA RID: 6362
		public static readonly LegacySoundStyle Item8 = new LegacySoundStyle(2, 8, SoundType.Sound);

		// Token: 0x040018DB RID: 6363
		public static readonly LegacySoundStyle Item9 = new LegacySoundStyle(2, 9, SoundType.Sound);

		// Token: 0x040018DC RID: 6364
		public static readonly LegacySoundStyle Item10 = new LegacySoundStyle(2, 10, SoundType.Sound);

		// Token: 0x040018DD RID: 6365
		public static readonly LegacySoundStyle Item11 = new LegacySoundStyle(2, 11, SoundType.Sound);

		// Token: 0x040018DE RID: 6366
		public static readonly LegacySoundStyle Item12 = new LegacySoundStyle(2, 12, SoundType.Sound);

		// Token: 0x040018DF RID: 6367
		public static readonly LegacySoundStyle Item13 = new LegacySoundStyle(2, 13, SoundType.Sound);

		// Token: 0x040018E0 RID: 6368
		public static readonly LegacySoundStyle Item14 = new LegacySoundStyle(2, 14, SoundType.Sound);

		// Token: 0x040018E1 RID: 6369
		public static readonly LegacySoundStyle Item15 = new LegacySoundStyle(2, 15, SoundType.Sound);

		// Token: 0x040018E2 RID: 6370
		public static readonly LegacySoundStyle Item16 = new LegacySoundStyle(2, 16, SoundType.Sound);

		// Token: 0x040018E3 RID: 6371
		public static readonly LegacySoundStyle Item17 = new LegacySoundStyle(2, 17, SoundType.Sound);

		// Token: 0x040018E4 RID: 6372
		public static readonly LegacySoundStyle Item18 = new LegacySoundStyle(2, 18, SoundType.Sound);

		// Token: 0x040018E5 RID: 6373
		public static readonly LegacySoundStyle Item19 = new LegacySoundStyle(2, 19, SoundType.Sound);

		// Token: 0x040018E6 RID: 6374
		public static readonly LegacySoundStyle Item20 = new LegacySoundStyle(2, 20, SoundType.Sound);

		// Token: 0x040018E7 RID: 6375
		public static readonly LegacySoundStyle Item21 = new LegacySoundStyle(2, 21, SoundType.Sound);

		// Token: 0x040018E8 RID: 6376
		public static readonly LegacySoundStyle Item22 = new LegacySoundStyle(2, 22, SoundType.Sound);

		// Token: 0x040018E9 RID: 6377
		public static readonly LegacySoundStyle Item23 = new LegacySoundStyle(2, 23, SoundType.Sound);

		// Token: 0x040018EA RID: 6378
		public static readonly LegacySoundStyle Item24 = new LegacySoundStyle(2, 24, SoundType.Sound);

		// Token: 0x040018EB RID: 6379
		public static readonly LegacySoundStyle Item25 = new LegacySoundStyle(2, 25, SoundType.Sound);

		// Token: 0x040018EC RID: 6380
		public static readonly LegacySoundStyle Item26 = new LegacySoundStyle(2, 26, SoundType.Sound);

		// Token: 0x040018ED RID: 6381
		public static readonly LegacySoundStyle Item27 = new LegacySoundStyle(2, 27, SoundType.Sound);

		// Token: 0x040018EE RID: 6382
		public static readonly LegacySoundStyle Item28 = new LegacySoundStyle(2, 28, SoundType.Sound);

		// Token: 0x040018EF RID: 6383
		public static readonly LegacySoundStyle Item29 = new LegacySoundStyle(2, 29, SoundType.Sound);

		// Token: 0x040018F0 RID: 6384
		public static readonly LegacySoundStyle Item30 = new LegacySoundStyle(2, 30, SoundType.Sound);

		// Token: 0x040018F1 RID: 6385
		public static readonly LegacySoundStyle Item31 = new LegacySoundStyle(2, 31, SoundType.Sound);

		// Token: 0x040018F2 RID: 6386
		public static readonly LegacySoundStyle Item32 = new LegacySoundStyle(2, 32, SoundType.Sound);

		// Token: 0x040018F3 RID: 6387
		public static readonly LegacySoundStyle Item33 = new LegacySoundStyle(2, 33, SoundType.Sound);

		// Token: 0x040018F4 RID: 6388
		public static readonly LegacySoundStyle Item34 = new LegacySoundStyle(2, 34, SoundType.Sound);

		// Token: 0x040018F5 RID: 6389
		public static readonly LegacySoundStyle Item35 = new LegacySoundStyle(2, 35, SoundType.Sound);

		// Token: 0x040018F6 RID: 6390
		public static readonly LegacySoundStyle Item36 = new LegacySoundStyle(2, 36, SoundType.Sound);

		// Token: 0x040018F7 RID: 6391
		public static readonly LegacySoundStyle Item37 = new LegacySoundStyle(2, 37, SoundType.Sound);

		// Token: 0x040018F8 RID: 6392
		public static readonly LegacySoundStyle Item38 = new LegacySoundStyle(2, 38, SoundType.Sound);

		// Token: 0x040018F9 RID: 6393
		public static readonly LegacySoundStyle Item39 = new LegacySoundStyle(2, 39, SoundType.Sound);

		// Token: 0x040018FA RID: 6394
		public static readonly LegacySoundStyle Item40 = new LegacySoundStyle(2, 40, SoundType.Sound);

		// Token: 0x040018FB RID: 6395
		public static readonly LegacySoundStyle Item41 = new LegacySoundStyle(2, 41, SoundType.Sound);

		// Token: 0x040018FC RID: 6396
		public static readonly LegacySoundStyle Item42 = new LegacySoundStyle(2, 42, SoundType.Sound);

		// Token: 0x040018FD RID: 6397
		public static readonly LegacySoundStyle Item43 = new LegacySoundStyle(2, 43, SoundType.Sound);

		// Token: 0x040018FE RID: 6398
		public static readonly LegacySoundStyle Item44 = new LegacySoundStyle(2, 44, SoundType.Sound);

		// Token: 0x040018FF RID: 6399
		public static readonly LegacySoundStyle Item45 = new LegacySoundStyle(2, 45, SoundType.Sound);

		// Token: 0x04001900 RID: 6400
		public static readonly LegacySoundStyle Item46 = new LegacySoundStyle(2, 46, SoundType.Sound);

		// Token: 0x04001901 RID: 6401
		public static readonly LegacySoundStyle Item47 = new LegacySoundStyle(2, 47, SoundType.Sound);

		// Token: 0x04001902 RID: 6402
		public static readonly LegacySoundStyle Item48 = new LegacySoundStyle(2, 48, SoundType.Sound);

		// Token: 0x04001903 RID: 6403
		public static readonly LegacySoundStyle Item49 = new LegacySoundStyle(2, 49, SoundType.Sound);

		// Token: 0x04001904 RID: 6404
		public static readonly LegacySoundStyle Item50 = new LegacySoundStyle(2, 50, SoundType.Sound);

		// Token: 0x04001905 RID: 6405
		public static readonly LegacySoundStyle Item51 = new LegacySoundStyle(2, 51, SoundType.Sound);

		// Token: 0x04001906 RID: 6406
		public static readonly LegacySoundStyle Item52 = new LegacySoundStyle(2, 52, SoundType.Sound);

		// Token: 0x04001907 RID: 6407
		public static readonly LegacySoundStyle Item53 = new LegacySoundStyle(2, 53, SoundType.Sound);

		// Token: 0x04001908 RID: 6408
		public static readonly LegacySoundStyle Item54 = new LegacySoundStyle(2, 54, SoundType.Sound);

		// Token: 0x04001909 RID: 6409
		public static readonly LegacySoundStyle Item55 = new LegacySoundStyle(2, 55, SoundType.Sound);

		// Token: 0x0400190A RID: 6410
		public static readonly LegacySoundStyle Item56 = new LegacySoundStyle(2, 56, SoundType.Sound);

		// Token: 0x0400190B RID: 6411
		public static readonly LegacySoundStyle Item57 = new LegacySoundStyle(2, 57, SoundType.Sound);

		// Token: 0x0400190C RID: 6412
		public static readonly LegacySoundStyle Item58 = new LegacySoundStyle(2, 58, SoundType.Sound);

		// Token: 0x0400190D RID: 6413
		public static readonly LegacySoundStyle Item59 = new LegacySoundStyle(2, 59, SoundType.Sound);

		// Token: 0x0400190E RID: 6414
		public static readonly LegacySoundStyle Item60 = new LegacySoundStyle(2, 60, SoundType.Sound);

		// Token: 0x0400190F RID: 6415
		public static readonly LegacySoundStyle Item61 = new LegacySoundStyle(2, 61, SoundType.Sound);

		// Token: 0x04001910 RID: 6416
		public static readonly LegacySoundStyle Item62 = new LegacySoundStyle(2, 62, SoundType.Sound);

		// Token: 0x04001911 RID: 6417
		public static readonly LegacySoundStyle Item63 = new LegacySoundStyle(2, 63, SoundType.Sound);

		// Token: 0x04001912 RID: 6418
		public static readonly LegacySoundStyle Item64 = new LegacySoundStyle(2, 64, SoundType.Sound);

		// Token: 0x04001913 RID: 6419
		public static readonly LegacySoundStyle Item65 = new LegacySoundStyle(2, 65, SoundType.Sound);

		// Token: 0x04001914 RID: 6420
		public static readonly LegacySoundStyle Item66 = new LegacySoundStyle(2, 66, SoundType.Sound);

		// Token: 0x04001915 RID: 6421
		public static readonly LegacySoundStyle Item67 = new LegacySoundStyle(2, 67, SoundType.Sound);

		// Token: 0x04001916 RID: 6422
		public static readonly LegacySoundStyle Item68 = new LegacySoundStyle(2, 68, SoundType.Sound);

		// Token: 0x04001917 RID: 6423
		public static readonly LegacySoundStyle Item69 = new LegacySoundStyle(2, 69, SoundType.Sound);

		// Token: 0x04001918 RID: 6424
		public static readonly LegacySoundStyle Item70 = new LegacySoundStyle(2, 70, SoundType.Sound);

		// Token: 0x04001919 RID: 6425
		public static readonly LegacySoundStyle Item71 = new LegacySoundStyle(2, 71, SoundType.Sound);

		// Token: 0x0400191A RID: 6426
		public static readonly LegacySoundStyle Item72 = new LegacySoundStyle(2, 72, SoundType.Sound);

		// Token: 0x0400191B RID: 6427
		public static readonly LegacySoundStyle Item73 = new LegacySoundStyle(2, 73, SoundType.Sound);

		// Token: 0x0400191C RID: 6428
		public static readonly LegacySoundStyle Item74 = new LegacySoundStyle(2, 74, SoundType.Sound);

		// Token: 0x0400191D RID: 6429
		public static readonly LegacySoundStyle Item75 = new LegacySoundStyle(2, 75, SoundType.Sound);

		// Token: 0x0400191E RID: 6430
		public static readonly LegacySoundStyle Item76 = new LegacySoundStyle(2, 76, SoundType.Sound);

		// Token: 0x0400191F RID: 6431
		public static readonly LegacySoundStyle Item77 = new LegacySoundStyle(2, 77, SoundType.Sound);

		// Token: 0x04001920 RID: 6432
		public static readonly LegacySoundStyle Item78 = new LegacySoundStyle(2, 78, SoundType.Sound);

		// Token: 0x04001921 RID: 6433
		public static readonly LegacySoundStyle Item79 = new LegacySoundStyle(2, 79, SoundType.Sound);

		// Token: 0x04001922 RID: 6434
		public static readonly LegacySoundStyle Item80 = new LegacySoundStyle(2, 80, SoundType.Sound);

		// Token: 0x04001923 RID: 6435
		public static readonly LegacySoundStyle Item81 = new LegacySoundStyle(2, 81, SoundType.Sound);

		// Token: 0x04001924 RID: 6436
		public static readonly LegacySoundStyle Item82 = new LegacySoundStyle(2, 82, SoundType.Sound);

		// Token: 0x04001925 RID: 6437
		public static readonly LegacySoundStyle Item83 = new LegacySoundStyle(2, 83, SoundType.Sound);

		// Token: 0x04001926 RID: 6438
		public static readonly LegacySoundStyle Item84 = new LegacySoundStyle(2, 84, SoundType.Sound);

		// Token: 0x04001927 RID: 6439
		public static readonly LegacySoundStyle Item85 = new LegacySoundStyle(2, 85, SoundType.Sound);

		// Token: 0x04001928 RID: 6440
		public static readonly LegacySoundStyle Item86 = new LegacySoundStyle(2, 86, SoundType.Sound);

		// Token: 0x04001929 RID: 6441
		public static readonly LegacySoundStyle Item87 = new LegacySoundStyle(2, 87, SoundType.Sound);

		// Token: 0x0400192A RID: 6442
		public static readonly LegacySoundStyle Item88 = new LegacySoundStyle(2, 88, SoundType.Sound);

		// Token: 0x0400192B RID: 6443
		public static readonly LegacySoundStyle Item89 = new LegacySoundStyle(2, 89, SoundType.Sound);

		// Token: 0x0400192C RID: 6444
		public static readonly LegacySoundStyle Item90 = new LegacySoundStyle(2, 90, SoundType.Sound);

		// Token: 0x0400192D RID: 6445
		public static readonly LegacySoundStyle Item91 = new LegacySoundStyle(2, 91, SoundType.Sound);

		// Token: 0x0400192E RID: 6446
		public static readonly LegacySoundStyle Item92 = new LegacySoundStyle(2, 92, SoundType.Sound);

		// Token: 0x0400192F RID: 6447
		public static readonly LegacySoundStyle Item93 = new LegacySoundStyle(2, 93, SoundType.Sound);

		// Token: 0x04001930 RID: 6448
		public static readonly LegacySoundStyle Item94 = new LegacySoundStyle(2, 94, SoundType.Sound);

		// Token: 0x04001931 RID: 6449
		public static readonly LegacySoundStyle Item95 = new LegacySoundStyle(2, 95, SoundType.Sound);

		// Token: 0x04001932 RID: 6450
		public static readonly LegacySoundStyle Item96 = new LegacySoundStyle(2, 96, SoundType.Sound);

		// Token: 0x04001933 RID: 6451
		public static readonly LegacySoundStyle Item97 = new LegacySoundStyle(2, 97, SoundType.Sound);

		// Token: 0x04001934 RID: 6452
		public static readonly LegacySoundStyle Item98 = new LegacySoundStyle(2, 98, SoundType.Sound);

		// Token: 0x04001935 RID: 6453
		public static readonly LegacySoundStyle Item99 = new LegacySoundStyle(2, 99, SoundType.Sound);

		// Token: 0x04001936 RID: 6454
		public static readonly LegacySoundStyle Item100 = new LegacySoundStyle(2, 100, SoundType.Sound);

		// Token: 0x04001937 RID: 6455
		public static readonly LegacySoundStyle Item101 = new LegacySoundStyle(2, 101, SoundType.Sound);

		// Token: 0x04001938 RID: 6456
		public static readonly LegacySoundStyle Item102 = new LegacySoundStyle(2, 102, SoundType.Sound);

		// Token: 0x04001939 RID: 6457
		public static readonly LegacySoundStyle Item103 = new LegacySoundStyle(2, 103, SoundType.Sound);

		// Token: 0x0400193A RID: 6458
		public static readonly LegacySoundStyle Item104 = new LegacySoundStyle(2, 104, SoundType.Sound);

		// Token: 0x0400193B RID: 6459
		public static readonly LegacySoundStyle Item105 = new LegacySoundStyle(2, 105, SoundType.Sound);

		// Token: 0x0400193C RID: 6460
		public static readonly LegacySoundStyle Item106 = new LegacySoundStyle(2, 106, SoundType.Sound);

		// Token: 0x0400193D RID: 6461
		public static readonly LegacySoundStyle Item107 = new LegacySoundStyle(2, 107, SoundType.Sound);

		// Token: 0x0400193E RID: 6462
		public static readonly LegacySoundStyle Item108 = new LegacySoundStyle(2, 108, SoundType.Sound);

		// Token: 0x0400193F RID: 6463
		public static readonly LegacySoundStyle Item109 = new LegacySoundStyle(2, 109, SoundType.Sound);

		// Token: 0x04001940 RID: 6464
		public static readonly LegacySoundStyle Item110 = new LegacySoundStyle(2, 110, SoundType.Sound);

		// Token: 0x04001941 RID: 6465
		public static readonly LegacySoundStyle Item111 = new LegacySoundStyle(2, 111, SoundType.Sound);

		// Token: 0x04001942 RID: 6466
		public static readonly LegacySoundStyle Item112 = new LegacySoundStyle(2, 112, SoundType.Sound);

		// Token: 0x04001943 RID: 6467
		public static readonly LegacySoundStyle Item113 = new LegacySoundStyle(2, 113, SoundType.Sound);

		// Token: 0x04001944 RID: 6468
		public static readonly LegacySoundStyle Item114 = new LegacySoundStyle(2, 114, SoundType.Sound);

		// Token: 0x04001945 RID: 6469
		public static readonly LegacySoundStyle Item115 = new LegacySoundStyle(2, 115, SoundType.Sound);

		// Token: 0x04001946 RID: 6470
		public static readonly LegacySoundStyle Item116 = new LegacySoundStyle(2, 116, SoundType.Sound);

		// Token: 0x04001947 RID: 6471
		public static readonly LegacySoundStyle Item117 = new LegacySoundStyle(2, 117, SoundType.Sound);

		// Token: 0x04001948 RID: 6472
		public static readonly LegacySoundStyle Item118 = new LegacySoundStyle(2, 118, SoundType.Sound);

		// Token: 0x04001949 RID: 6473
		public static readonly LegacySoundStyle Item119 = new LegacySoundStyle(2, 119, SoundType.Sound);

		// Token: 0x0400194A RID: 6474
		public static readonly LegacySoundStyle Item120 = new LegacySoundStyle(2, 120, SoundType.Sound);

		// Token: 0x0400194B RID: 6475
		public static readonly LegacySoundStyle Item121 = new LegacySoundStyle(2, 121, SoundType.Sound);

		// Token: 0x0400194C RID: 6476
		public static readonly LegacySoundStyle Item122 = new LegacySoundStyle(2, 122, SoundType.Sound);

		// Token: 0x0400194D RID: 6477
		public static readonly LegacySoundStyle Item123 = new LegacySoundStyle(2, 123, SoundType.Sound);

		// Token: 0x0400194E RID: 6478
		public static readonly LegacySoundStyle Item124 = new LegacySoundStyle(2, 124, SoundType.Sound);

		// Token: 0x0400194F RID: 6479
		public static readonly LegacySoundStyle Item125 = new LegacySoundStyle(2, 125, SoundType.Sound);

		// Token: 0x04001950 RID: 6480
		public static readonly LegacySoundStyle Item126 = new LegacySoundStyle(2, 126, SoundType.Sound);

		// Token: 0x04001951 RID: 6481
		public static readonly LegacySoundStyle Item127 = new LegacySoundStyle(2, 127, SoundType.Sound);

		// Token: 0x04001952 RID: 6482
		public static readonly LegacySoundStyle Item128 = new LegacySoundStyle(2, 128, SoundType.Sound);

		// Token: 0x04001953 RID: 6483
		public static readonly LegacySoundStyle Item129 = new LegacySoundStyle(2, 129, SoundType.Sound);

		// Token: 0x04001954 RID: 6484
		public static readonly LegacySoundStyle Item130 = new LegacySoundStyle(2, 130, SoundType.Sound);

		// Token: 0x04001955 RID: 6485
		public static readonly LegacySoundStyle Item131 = new LegacySoundStyle(2, 131, SoundType.Sound);

		// Token: 0x04001956 RID: 6486
		public static readonly LegacySoundStyle Item132 = new LegacySoundStyle(2, 132, SoundType.Sound);

		// Token: 0x04001957 RID: 6487
		public static readonly LegacySoundStyle Item133 = new LegacySoundStyle(2, 133, SoundType.Sound);

		// Token: 0x04001958 RID: 6488
		public static readonly LegacySoundStyle Item134 = new LegacySoundStyle(2, 134, SoundType.Sound);

		// Token: 0x04001959 RID: 6489
		public static readonly LegacySoundStyle Item135 = new LegacySoundStyle(2, 135, SoundType.Sound);

		// Token: 0x0400195A RID: 6490
		public static readonly LegacySoundStyle Item136 = new LegacySoundStyle(2, 136, SoundType.Sound);

		// Token: 0x0400195B RID: 6491
		public static readonly LegacySoundStyle Item137 = new LegacySoundStyle(2, 137, SoundType.Sound);

		// Token: 0x0400195C RID: 6492
		public static readonly LegacySoundStyle Item138 = new LegacySoundStyle(2, 138, SoundType.Sound);

		// Token: 0x0400195D RID: 6493
		public static readonly LegacySoundStyle Item139 = new LegacySoundStyle(2, 139, SoundType.Sound);

		// Token: 0x0400195E RID: 6494
		public static readonly LegacySoundStyle Item140 = new LegacySoundStyle(2, 140, SoundType.Sound);

		// Token: 0x0400195F RID: 6495
		public static readonly LegacySoundStyle Item141 = new LegacySoundStyle(2, 141, SoundType.Sound);

		// Token: 0x04001960 RID: 6496
		public static readonly LegacySoundStyle Item142 = new LegacySoundStyle(2, 142, SoundType.Sound);

		// Token: 0x04001961 RID: 6497
		public static readonly LegacySoundStyle Item143 = new LegacySoundStyle(2, 143, SoundType.Sound);

		// Token: 0x04001962 RID: 6498
		public static readonly LegacySoundStyle Item144 = new LegacySoundStyle(2, 144, SoundType.Sound);

		// Token: 0x04001963 RID: 6499
		public static readonly LegacySoundStyle Item145 = new LegacySoundStyle(2, 145, SoundType.Sound);

		// Token: 0x04001964 RID: 6500
		public static readonly LegacySoundStyle Item146 = new LegacySoundStyle(2, 146, SoundType.Sound);

		// Token: 0x04001965 RID: 6501
		public static readonly LegacySoundStyle Item147 = new LegacySoundStyle(2, 147, SoundType.Sound);

		// Token: 0x04001966 RID: 6502
		public static readonly LegacySoundStyle Item148 = new LegacySoundStyle(2, 148, SoundType.Sound);

		// Token: 0x04001967 RID: 6503
		public static readonly LegacySoundStyle Item149 = new LegacySoundStyle(2, 149, SoundType.Sound);

		// Token: 0x04001968 RID: 6504
		public static readonly LegacySoundStyle Item150 = new LegacySoundStyle(2, 150, SoundType.Sound);

		// Token: 0x04001969 RID: 6505
		public static readonly LegacySoundStyle Item151 = new LegacySoundStyle(2, 151, SoundType.Sound);

		// Token: 0x0400196A RID: 6506
		public static readonly LegacySoundStyle Item152 = new LegacySoundStyle(2, 152, SoundType.Sound);

		// Token: 0x0400196B RID: 6507
		public static readonly LegacySoundStyle Item153 = new LegacySoundStyle(2, 153, SoundType.Sound);

		// Token: 0x0400196C RID: 6508
		public static readonly LegacySoundStyle Item154 = new LegacySoundStyle(2, 154, SoundType.Sound);

		// Token: 0x0400196D RID: 6509
		public static readonly LegacySoundStyle Item155 = new LegacySoundStyle(2, 155, SoundType.Sound);

		// Token: 0x0400196E RID: 6510
		public static readonly LegacySoundStyle Item156 = new LegacySoundStyle(2, 156, SoundType.Sound);

		// Token: 0x0400196F RID: 6511
		public static readonly LegacySoundStyle Item157 = new LegacySoundStyle(2, 157, SoundType.Sound);

		// Token: 0x04001970 RID: 6512
		public static readonly LegacySoundStyle Item158 = new LegacySoundStyle(2, 158, SoundType.Sound);

		// Token: 0x04001971 RID: 6513
		public static readonly LegacySoundStyle Item159 = new LegacySoundStyle(2, 159, SoundType.Sound);

		// Token: 0x04001972 RID: 6514
		public static readonly LegacySoundStyle Item160 = new LegacySoundStyle(2, 160, SoundType.Sound);

		// Token: 0x04001973 RID: 6515
		public static readonly LegacySoundStyle Item161 = new LegacySoundStyle(2, 161, SoundType.Sound);

		// Token: 0x04001974 RID: 6516
		public static readonly LegacySoundStyle Item162 = new LegacySoundStyle(2, 162, SoundType.Sound);

		// Token: 0x04001975 RID: 6517
		public static readonly LegacySoundStyle Item163 = new LegacySoundStyle(2, 163, SoundType.Sound);

		// Token: 0x04001976 RID: 6518
		public static readonly LegacySoundStyle Item164 = new LegacySoundStyle(2, 164, SoundType.Sound);

		// Token: 0x04001977 RID: 6519
		public static readonly LegacySoundStyle Item165 = new LegacySoundStyle(2, 165, SoundType.Sound);

		// Token: 0x04001978 RID: 6520
		public static readonly LegacySoundStyle Item166 = new LegacySoundStyle(2, 166, SoundType.Sound);

		// Token: 0x04001979 RID: 6521
		public static readonly LegacySoundStyle Item167 = new LegacySoundStyle(2, 167, SoundType.Sound);

		// Token: 0x0400197A RID: 6522
		public static readonly LegacySoundStyle Item168 = new LegacySoundStyle(2, 168, SoundType.Sound);

		// Token: 0x0400197B RID: 6523
		public static readonly LegacySoundStyle Item169 = new LegacySoundStyle(2, 169, SoundType.Sound);

		// Token: 0x0400197C RID: 6524
		public static readonly LegacySoundStyle Item170 = new LegacySoundStyle(2, 170, SoundType.Sound);

		// Token: 0x0400197D RID: 6525
		public static readonly LegacySoundStyle Item171 = new LegacySoundStyle(2, 171, SoundType.Sound);

		// Token: 0x0400197E RID: 6526
		public static readonly LegacySoundStyle Item172 = new LegacySoundStyle(2, 172, SoundType.Sound);

		// Token: 0x0400197F RID: 6527
		public static readonly LegacySoundStyle Item173 = new LegacySoundStyle(2, 173, SoundType.Sound);

		// Token: 0x04001980 RID: 6528
		public static readonly LegacySoundStyle Item174 = new LegacySoundStyle(2, 174, SoundType.Sound);

		// Token: 0x04001981 RID: 6529
		public static readonly LegacySoundStyle Item175 = new LegacySoundStyle(2, 175, SoundType.Sound);

		// Token: 0x04001982 RID: 6530
		public static readonly LegacySoundStyle Item176 = new LegacySoundStyle(2, 176, SoundType.Sound);

		// Token: 0x04001983 RID: 6531
		public static readonly LegacySoundStyle Item177 = new LegacySoundStyle(2, 177, SoundType.Sound);

		// Token: 0x04001984 RID: 6532
		public static readonly LegacySoundStyle Item178 = new LegacySoundStyle(2, 178, SoundType.Sound);

		// Token: 0x04001985 RID: 6533
		public static short ItemSoundCount = 179;

		// Token: 0x04001986 RID: 6534
		public static readonly LegacySoundStyle DD2_GoblinBomb = new LegacySoundStyle(2, 14, SoundType.Sound).WithVolume(0.5f);

		// Token: 0x04001987 RID: 6535
		public static readonly LegacySoundStyle AchievementComplete = SoundID.CreateTrackable("achievement_complete", SoundType.Sound);

		// Token: 0x04001988 RID: 6536
		public static readonly LegacySoundStyle BlizzardInsideBuildingLoop = SoundID.CreateTrackable("blizzard_inside_building_loop", SoundType.Ambient);

		// Token: 0x04001989 RID: 6537
		public static readonly LegacySoundStyle BlizzardStrongLoop = SoundID.CreateTrackable("blizzard_strong_loop", SoundType.Ambient).WithVolume(0.5f);

		// Token: 0x0400198A RID: 6538
		public static readonly LegacySoundStyle LiquidsHoneyWater = SoundID.CreateTrackable("liquids_honey_water", 3, SoundType.Ambient);

		// Token: 0x0400198B RID: 6539
		public static readonly LegacySoundStyle LiquidsHoneyLava = SoundID.CreateTrackable("liquids_honey_lava", 3, SoundType.Ambient);

		// Token: 0x0400198C RID: 6540
		public static readonly LegacySoundStyle LiquidsWaterLava = SoundID.CreateTrackable("liquids_water_lava", 3, SoundType.Ambient);

		// Token: 0x0400198D RID: 6541
		public static readonly LegacySoundStyle DD2_BallistaTowerShot = SoundID.CreateTrackable("dd2_ballista_tower_shot", 3, SoundType.Sound);

		// Token: 0x0400198E RID: 6542
		public static readonly LegacySoundStyle DD2_ExplosiveTrapExplode = SoundID.CreateTrackable("dd2_explosive_trap_explode", 3, SoundType.Sound);

		// Token: 0x0400198F RID: 6543
		public static readonly LegacySoundStyle DD2_FlameburstTowerShot = SoundID.CreateTrackable("dd2_flameburst_tower_shot", 3, SoundType.Sound);

		// Token: 0x04001990 RID: 6544
		public static readonly LegacySoundStyle DD2_LightningAuraZap = SoundID.CreateTrackable("dd2_lightning_aura_zap", 4, SoundType.Sound);

		// Token: 0x04001991 RID: 6545
		public static readonly LegacySoundStyle DD2_DefenseTowerSpawn = SoundID.CreateTrackable("dd2_defense_tower_spawn", SoundType.Sound);

		// Token: 0x04001992 RID: 6546
		public static readonly LegacySoundStyle DD2_BetsyDeath = SoundID.CreateTrackable("dd2_betsy_death", 3, SoundType.Sound);

		// Token: 0x04001993 RID: 6547
		public static readonly LegacySoundStyle DD2_BetsyFireballShot = SoundID.CreateTrackable("dd2_betsy_fireball_shot", 3, SoundType.Sound);

		// Token: 0x04001994 RID: 6548
		public static readonly LegacySoundStyle DD2_BetsyFireballImpact = SoundID.CreateTrackable("dd2_betsy_fireball_impact", 3, SoundType.Sound);

		// Token: 0x04001995 RID: 6549
		public static readonly LegacySoundStyle DD2_BetsyFlameBreath = SoundID.CreateTrackable("dd2_betsy_flame_breath", SoundType.Sound);

		// Token: 0x04001996 RID: 6550
		public static readonly LegacySoundStyle DD2_BetsyFlyingCircleAttack = SoundID.CreateTrackable("dd2_betsy_flying_circle_attack", SoundType.Sound);

		// Token: 0x04001997 RID: 6551
		public static readonly LegacySoundStyle DD2_BetsyHurt = SoundID.CreateTrackable("dd2_betsy_hurt", 3, SoundType.Sound);

		// Token: 0x04001998 RID: 6552
		public static readonly LegacySoundStyle DD2_BetsyScream = SoundID.CreateTrackable("dd2_betsy_scream", SoundType.Sound);

		// Token: 0x04001999 RID: 6553
		public static readonly LegacySoundStyle DD2_BetsySummon = SoundID.CreateTrackable("dd2_betsy_summon", 3, SoundType.Sound);

		// Token: 0x0400199A RID: 6554
		public static readonly LegacySoundStyle DD2_BetsyWindAttack = SoundID.CreateTrackable("dd2_betsy_wind_attack", 3, SoundType.Sound);

		// Token: 0x0400199B RID: 6555
		public static readonly LegacySoundStyle DD2_DarkMageAttack = SoundID.CreateTrackable("dd2_dark_mage_attack", 3, SoundType.Sound);

		// Token: 0x0400199C RID: 6556
		public static readonly LegacySoundStyle DD2_DarkMageCastHeal = SoundID.CreateTrackable("dd2_dark_mage_cast_heal", 3, SoundType.Sound);

		// Token: 0x0400199D RID: 6557
		public static readonly LegacySoundStyle DD2_DarkMageDeath = SoundID.CreateTrackable("dd2_dark_mage_death", 3, SoundType.Sound);

		// Token: 0x0400199E RID: 6558
		public static readonly LegacySoundStyle DD2_DarkMageHealImpact = SoundID.CreateTrackable("dd2_dark_mage_heal_impact", 3, SoundType.Sound);

		// Token: 0x0400199F RID: 6559
		public static readonly LegacySoundStyle DD2_DarkMageHurt = SoundID.CreateTrackable("dd2_dark_mage_hurt", 3, SoundType.Sound);

		// Token: 0x040019A0 RID: 6560
		public static readonly LegacySoundStyle DD2_DarkMageSummonSkeleton = SoundID.CreateTrackable("dd2_dark_mage_summon_skeleton", 3, SoundType.Sound);

		// Token: 0x040019A1 RID: 6561
		public static readonly LegacySoundStyle DD2_DrakinBreathIn = SoundID.CreateTrackable("dd2_drakin_breath_in", 3, SoundType.Sound);

		// Token: 0x040019A2 RID: 6562
		public static readonly LegacySoundStyle DD2_DrakinDeath = SoundID.CreateTrackable("dd2_drakin_death", 3, SoundType.Sound);

		// Token: 0x040019A3 RID: 6563
		public static readonly LegacySoundStyle DD2_DrakinHurt = SoundID.CreateTrackable("dd2_drakin_hurt", 3, SoundType.Sound);

		// Token: 0x040019A4 RID: 6564
		public static readonly LegacySoundStyle DD2_DrakinShot = SoundID.CreateTrackable("dd2_drakin_shot", 3, SoundType.Sound);

		// Token: 0x040019A5 RID: 6565
		public static readonly LegacySoundStyle DD2_GoblinDeath = SoundID.CreateTrackable("dd2_goblin_death", 3, SoundType.Sound);

		// Token: 0x040019A6 RID: 6566
		public static readonly LegacySoundStyle DD2_GoblinHurt = SoundID.CreateTrackable("dd2_goblin_hurt", 6, SoundType.Sound);

		// Token: 0x040019A7 RID: 6567
		public static readonly LegacySoundStyle DD2_GoblinScream = SoundID.CreateTrackable("dd2_goblin_scream", 3, SoundType.Sound);

		// Token: 0x040019A8 RID: 6568
		public static readonly LegacySoundStyle DD2_GoblinBomberDeath = SoundID.CreateTrackable("dd2_goblin_bomber_death", 3, SoundType.Sound);

		// Token: 0x040019A9 RID: 6569
		public static readonly LegacySoundStyle DD2_GoblinBomberHurt = SoundID.CreateTrackable("dd2_goblin_bomber_hurt", 3, SoundType.Sound);

		// Token: 0x040019AA RID: 6570
		public static readonly LegacySoundStyle DD2_GoblinBomberScream = SoundID.CreateTrackable("dd2_goblin_bomber_scream", 3, SoundType.Sound);

		// Token: 0x040019AB RID: 6571
		public static readonly LegacySoundStyle DD2_GoblinBomberThrow = SoundID.CreateTrackable("dd2_goblin_bomber_throw", 3, SoundType.Sound);

		// Token: 0x040019AC RID: 6572
		public static readonly LegacySoundStyle DD2_JavelinThrowersAttack = SoundID.CreateTrackable("dd2_javelin_throwers_attack", 3, SoundType.Sound);

		// Token: 0x040019AD RID: 6573
		public static readonly LegacySoundStyle DD2_JavelinThrowersDeath = SoundID.CreateTrackable("dd2_javelin_throwers_death", 3, SoundType.Sound);

		// Token: 0x040019AE RID: 6574
		public static readonly LegacySoundStyle DD2_JavelinThrowersHurt = SoundID.CreateTrackable("dd2_javelin_throwers_hurt", 3, SoundType.Sound);

		// Token: 0x040019AF RID: 6575
		public static readonly LegacySoundStyle DD2_JavelinThrowersTaunt = SoundID.CreateTrackable("dd2_javelin_throwers_taunt", 3, SoundType.Sound);

		// Token: 0x040019B0 RID: 6576
		public static readonly LegacySoundStyle DD2_KoboldDeath = SoundID.CreateTrackable("dd2_kobold_death", 3, SoundType.Sound);

		// Token: 0x040019B1 RID: 6577
		public static readonly LegacySoundStyle DD2_KoboldExplosion = SoundID.CreateTrackable("dd2_kobold_explosion", 3, SoundType.Sound);

		// Token: 0x040019B2 RID: 6578
		public static readonly LegacySoundStyle DD2_KoboldHurt = SoundID.CreateTrackable("dd2_kobold_hurt", 3, SoundType.Sound);

		// Token: 0x040019B3 RID: 6579
		public static readonly LegacySoundStyle DD2_KoboldIgnite = SoundID.CreateTrackable("dd2_kobold_ignite", SoundType.Sound);

		// Token: 0x040019B4 RID: 6580
		public static readonly LegacySoundStyle DD2_KoboldIgniteLoop = SoundID.CreateTrackable("dd2_kobold_ignite_loop", SoundType.Sound);

		// Token: 0x040019B5 RID: 6581
		public static readonly LegacySoundStyle DD2_KoboldScreamChargeLoop = SoundID.CreateTrackable("dd2_kobold_scream_charge_loop", SoundType.Sound);

		// Token: 0x040019B6 RID: 6582
		public static readonly LegacySoundStyle DD2_KoboldFlyerChargeScream = SoundID.CreateTrackable("dd2_kobold_flyer_charge_scream", 3, SoundType.Sound);

		// Token: 0x040019B7 RID: 6583
		public static readonly LegacySoundStyle DD2_KoboldFlyerDeath = SoundID.CreateTrackable("dd2_kobold_flyer_death", 3, SoundType.Sound);

		// Token: 0x040019B8 RID: 6584
		public static readonly LegacySoundStyle DD2_KoboldFlyerHurt = SoundID.CreateTrackable("dd2_kobold_flyer_hurt", 3, SoundType.Sound);

		// Token: 0x040019B9 RID: 6585
		public static readonly LegacySoundStyle DD2_LightningBugDeath = SoundID.CreateTrackable("dd2_lightning_bug_death", 3, SoundType.Sound);

		// Token: 0x040019BA RID: 6586
		public static readonly LegacySoundStyle DD2_LightningBugHurt = SoundID.CreateTrackable("dd2_lightning_bug_hurt", 3, SoundType.Sound);

		// Token: 0x040019BB RID: 6587
		public static readonly LegacySoundStyle DD2_LightningBugZap = SoundID.CreateTrackable("dd2_lightning_bug_zap", 3, SoundType.Sound);

		// Token: 0x040019BC RID: 6588
		public static readonly LegacySoundStyle DD2_OgreAttack = SoundID.CreateTrackable("dd2_ogre_attack", 3, SoundType.Sound);

		// Token: 0x040019BD RID: 6589
		public static readonly LegacySoundStyle DD2_OgreDeath = SoundID.CreateTrackable("dd2_ogre_death", 3, SoundType.Sound);

		// Token: 0x040019BE RID: 6590
		public static readonly LegacySoundStyle DD2_OgreGroundPound = SoundID.CreateTrackable("dd2_ogre_ground_pound", SoundType.Sound);

		// Token: 0x040019BF RID: 6591
		public static readonly LegacySoundStyle DD2_OgreHurt = SoundID.CreateTrackable("dd2_ogre_hurt", 3, SoundType.Sound);

		// Token: 0x040019C0 RID: 6592
		public static readonly LegacySoundStyle DD2_OgreRoar = SoundID.CreateTrackable("dd2_ogre_roar", 3, SoundType.Sound);

		// Token: 0x040019C1 RID: 6593
		public static readonly LegacySoundStyle DD2_OgreSpit = SoundID.CreateTrackable("dd2_ogre_spit", SoundType.Sound);

		// Token: 0x040019C2 RID: 6594
		public static readonly LegacySoundStyle DD2_SkeletonDeath = SoundID.CreateTrackable("dd2_skeleton_death", 3, SoundType.Sound);

		// Token: 0x040019C3 RID: 6595
		public static readonly LegacySoundStyle DD2_SkeletonHurt = SoundID.CreateTrackable("dd2_skeleton_hurt", 3, SoundType.Sound);

		// Token: 0x040019C4 RID: 6596
		public static readonly LegacySoundStyle DD2_SkeletonSummoned = SoundID.CreateTrackable("dd2_skeleton_summoned", SoundType.Sound);

		// Token: 0x040019C5 RID: 6597
		public static readonly LegacySoundStyle DD2_WitherBeastAuraPulse = SoundID.CreateTrackable("dd2_wither_beast_aura_pulse", 2, SoundType.Sound);

		// Token: 0x040019C6 RID: 6598
		public static readonly LegacySoundStyle DD2_WitherBeastCrystalImpact = SoundID.CreateTrackable("dd2_wither_beast_crystal_impact", 3, SoundType.Sound);

		// Token: 0x040019C7 RID: 6599
		public static readonly LegacySoundStyle DD2_WitherBeastDeath = SoundID.CreateTrackable("dd2_wither_beast_death", 3, SoundType.Sound);

		// Token: 0x040019C8 RID: 6600
		public static readonly LegacySoundStyle DD2_WitherBeastHurt = SoundID.CreateTrackable("dd2_wither_beast_hurt", 3, SoundType.Sound);

		// Token: 0x040019C9 RID: 6601
		public static readonly LegacySoundStyle DD2_WyvernDeath = SoundID.CreateTrackable("dd2_wyvern_death", 3, SoundType.Sound);

		// Token: 0x040019CA RID: 6602
		public static readonly LegacySoundStyle DD2_WyvernHurt = SoundID.CreateTrackable("dd2_wyvern_hurt", 3, SoundType.Sound);

		// Token: 0x040019CB RID: 6603
		public static readonly LegacySoundStyle DD2_WyvernScream = SoundID.CreateTrackable("dd2_wyvern_scream", 3, SoundType.Sound);

		// Token: 0x040019CC RID: 6604
		public static readonly LegacySoundStyle DD2_WyvernDiveDown = SoundID.CreateTrackable("dd2_wyvern_dive_down", 3, SoundType.Sound);

		// Token: 0x040019CD RID: 6605
		public static readonly LegacySoundStyle DD2_EtherianPortalDryadTouch = SoundID.CreateTrackable("dd2_etherian_portal_dryad_touch", SoundType.Sound);

		// Token: 0x040019CE RID: 6606
		public static readonly LegacySoundStyle DD2_EtherianPortalIdleLoop = SoundID.CreateTrackable("dd2_etherian_portal_idle_loop", SoundType.Sound);

		// Token: 0x040019CF RID: 6607
		public static readonly LegacySoundStyle DD2_EtherianPortalOpen = SoundID.CreateTrackable("dd2_etherian_portal_open", SoundType.Sound);

		// Token: 0x040019D0 RID: 6608
		public static readonly LegacySoundStyle DD2_EtherianPortalSpawnEnemy = SoundID.CreateTrackable("dd2_etherian_portal_spawn_enemy", 3, SoundType.Sound);

		// Token: 0x040019D1 RID: 6609
		public static readonly LegacySoundStyle DD2_CrystalCartImpact = SoundID.CreateTrackable("dd2_crystal_cart_impact", 3, SoundType.Sound);

		// Token: 0x040019D2 RID: 6610
		public static readonly LegacySoundStyle DD2_DefeatScene = SoundID.CreateTrackable("dd2_defeat_scene", SoundType.Sound);

		// Token: 0x040019D3 RID: 6611
		public static readonly LegacySoundStyle DD2_WinScene = SoundID.CreateTrackable("dd2_win_scene", SoundType.Sound);

		// Token: 0x040019D4 RID: 6612
		public static readonly LegacySoundStyle DD2_BetsysWrathShot = SoundID.DD2_BetsyFireballShot.WithVolume(0.4f);

		// Token: 0x040019D5 RID: 6613
		public static readonly LegacySoundStyle DD2_BetsysWrathImpact = SoundID.DD2_BetsyFireballImpact.WithVolume(0.4f);

		// Token: 0x040019D6 RID: 6614
		public static readonly LegacySoundStyle DD2_BookStaffCast = SoundID.CreateTrackable("dd2_book_staff_cast", 3, SoundType.Sound);

		// Token: 0x040019D7 RID: 6615
		public static readonly LegacySoundStyle DD2_BookStaffTwisterLoop = SoundID.CreateTrackable("dd2_book_staff_twister_loop", SoundType.Sound);

		// Token: 0x040019D8 RID: 6616
		public static readonly LegacySoundStyle DD2_GhastlyGlaiveImpactGhost = SoundID.CreateTrackable("dd2_ghastly_glaive_impact_ghost", 3, SoundType.Sound);

		// Token: 0x040019D9 RID: 6617
		public static readonly LegacySoundStyle DD2_GhastlyGlaivePierce = SoundID.CreateTrackable("dd2_ghastly_glaive_pierce", 3, SoundType.Sound);

		// Token: 0x040019DA RID: 6618
		public static readonly LegacySoundStyle DD2_MonkStaffGroundImpact = SoundID.CreateTrackable("dd2_monk_staff_ground_impact", 3, SoundType.Sound);

		// Token: 0x040019DB RID: 6619
		public static readonly LegacySoundStyle DD2_MonkStaffGroundMiss = SoundID.CreateTrackable("dd2_monk_staff_ground_miss", 3, SoundType.Sound);

		// Token: 0x040019DC RID: 6620
		public static readonly LegacySoundStyle DD2_MonkStaffSwing = SoundID.CreateTrackable("dd2_monk_staff_swing", 4, SoundType.Sound);

		// Token: 0x040019DD RID: 6621
		public static readonly LegacySoundStyle DD2_PhantomPhoenixShot = SoundID.CreateTrackable("dd2_phantom_phoenix_shot", 3, SoundType.Sound);

		// Token: 0x040019DE RID: 6622
		public static readonly LegacySoundStyle DD2_SonicBoomBladeSlash = SoundID.CreateTrackable("dd2_sonic_boom_blade_slash", 3, SoundID.ItemDefaults).WithVolume(0.5f);

		// Token: 0x040019DF RID: 6623
		public static readonly LegacySoundStyle DD2_SkyDragonsFuryCircle = SoundID.CreateTrackable("dd2_sky_dragons_fury_circle", 3, SoundType.Sound);

		// Token: 0x040019E0 RID: 6624
		public static readonly LegacySoundStyle DD2_SkyDragonsFuryShot = SoundID.CreateTrackable("dd2_sky_dragons_fury_shot", 3, SoundType.Sound);

		// Token: 0x040019E1 RID: 6625
		public static readonly LegacySoundStyle DD2_SkyDragonsFurySwing = SoundID.CreateTrackable("dd2_sky_dragons_fury_swing", 4, SoundType.Sound);

		// Token: 0x040019E2 RID: 6626
		public static readonly LegacySoundStyle LucyTheAxeTalk = SoundID.CreateTrackable("lucyaxe_talk", 5, SoundType.Sound).WithVolume(0.4f).WithPitchVariance(0.1f);

		// Token: 0x040019E3 RID: 6627
		public static readonly LegacySoundStyle DeerclopsHit = SoundID.CreateTrackable("deerclops_hit", 3, SoundType.Sound).WithVolume(0.3f);

		// Token: 0x040019E4 RID: 6628
		public static readonly LegacySoundStyle DeerclopsDeath = SoundID.CreateTrackable("deerclops_death", SoundType.Sound);

		// Token: 0x040019E5 RID: 6629
		public static readonly LegacySoundStyle DeerclopsScream = SoundID.CreateTrackable("deerclops_scream", 3, SoundType.Sound);

		// Token: 0x040019E6 RID: 6630
		public static readonly LegacySoundStyle DeerclopsIceAttack = SoundID.CreateTrackable("deerclops_ice_attack", 3, SoundType.Sound).WithVolume(0.1f);

		// Token: 0x040019E7 RID: 6631
		public static readonly LegacySoundStyle DeerclopsRubbleAttack = SoundID.CreateTrackable("deerclops_rubble_attack", SoundType.Sound).WithVolume(0.5f);

		// Token: 0x040019E8 RID: 6632
		public static readonly LegacySoundStyle DeerclopsStep = SoundID.CreateTrackable("deerclops_step", SoundType.Sound).WithVolume(0.2f);

		// Token: 0x040019E9 RID: 6633
		public static readonly LegacySoundStyle ChesterOpen = SoundID.CreateTrackable("chester_open", 2, SoundType.Sound);

		// Token: 0x040019EA RID: 6634
		public static readonly LegacySoundStyle ChesterClose = SoundID.CreateTrackable("chester_close", 2, SoundType.Sound);

		// Token: 0x040019EB RID: 6635
		public static readonly LegacySoundStyle AbigailSummon = SoundID.CreateTrackable("abigail_summon", SoundType.Sound);

		// Token: 0x040019EC RID: 6636
		public static readonly LegacySoundStyle AbigailCry = SoundID.CreateTrackable("abigail_cry", 3, SoundType.Sound).WithVolume(0.4f);

		// Token: 0x040019ED RID: 6637
		public static readonly LegacySoundStyle AbigailAttack = SoundID.CreateTrackable("abigail_attack", SoundType.Sound).WithVolume(0.35f);

		// Token: 0x040019EE RID: 6638
		public static readonly LegacySoundStyle AbigailUpgrade = SoundID.CreateTrackable("abigail_upgrade", 3, SoundType.Sound).WithVolume(0.5f);

		// Token: 0x040019EF RID: 6639
		public static readonly LegacySoundStyle GlommerBounce = SoundID.CreateTrackable("glommer_bounce", 2, SoundType.Sound).WithVolume(0.5f);

		// Token: 0x040019F0 RID: 6640
		public static readonly LegacySoundStyle DSTMaleHurt = SoundID.CreateTrackable("dst_male_hit", 3, SoundType.Sound).WithVolume(0.1f);

		// Token: 0x040019F1 RID: 6641
		public static readonly LegacySoundStyle DSTFemaleHurt = SoundID.CreateTrackable("dst_female_hit", 3, SoundType.Sound).WithVolume(0.1f);

		// Token: 0x040019F2 RID: 6642
		public static readonly LegacySoundStyle JimsDrone = SoundID.CreateTrackable("Drone", SoundType.Sound).WithVolume(0.1f);

		// Token: 0x040019F3 RID: 6643
		private static List<string> _trackableLegacySoundPathList;

		// Token: 0x040019F4 RID: 6644
		public static Dictionary<string, LegacySoundStyle> SoundByName = null;

		// Token: 0x040019F5 RID: 6645
		public static Dictionary<string, ushort> IndexByName = null;

		// Token: 0x040019F6 RID: 6646
		public static Dictionary<ushort, LegacySoundStyle> SoundByIndex = null;

		// Token: 0x020005E0 RID: 1504
		private struct SoundStyleDefaults
		{
			// Token: 0x060032E7 RID: 13031 RVA: 0x005EE320 File Offset: 0x005EC520
			public SoundStyleDefaults(float volume, float pitchVariance, SoundType type = SoundType.Sound)
			{
				this.PitchVariance = pitchVariance;
				this.Volume = volume;
				this.Type = type;
			}

			// Token: 0x04005E64 RID: 24164
			public readonly float PitchVariance;

			// Token: 0x04005E65 RID: 24165
			public readonly float Volume;

			// Token: 0x04005E66 RID: 24166
			public readonly SoundType Type;
		}
	}
}
