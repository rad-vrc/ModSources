using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.Core;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This serves as the central class from which wall-related functions are supported and carried out.
	/// </summary>
	// Token: 0x02000233 RID: 563
	public static class WallLoader
	{
		// Token: 0x06002890 RID: 10384 RVA: 0x0050B8DE File Offset: 0x00509ADE
		internal static int ReserveWallID()
		{
			int result = WallLoader.nextWall;
			WallLoader.nextWall++;
			return result;
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06002891 RID: 10385 RVA: 0x0050B8F1 File Offset: 0x00509AF1
		public static int WallCount
		{
			get
			{
				return WallLoader.nextWall;
			}
		}

		/// <summary>
		/// Gets the ModWall instance with the given type. If no ModWall with the given type exists, returns null.
		/// </summary>
		// Token: 0x06002892 RID: 10386 RVA: 0x0050B8F8 File Offset: 0x00509AF8
		public static ModWall GetWall(int type)
		{
			if (type < (int)WallID.Count || type >= WallLoader.WallCount)
			{
				return null;
			}
			return WallLoader.walls[type - (int)WallID.Count];
		}

		// Token: 0x06002893 RID: 10387 RVA: 0x0050B920 File Offset: 0x00509B20
		private static void Resize2DArray<T>(ref T[,] array, int newSize)
		{
			int dim = array.GetLength(0);
			int dim2 = array.GetLength(1);
			T[,] newArray = new T[newSize, dim2];
			int i = 0;
			while (i < newSize && i < dim)
			{
				for (int j = 0; j < dim2; j++)
				{
					newArray[i, j] = array[i, j];
				}
				i++;
			}
			array = newArray;
		}

		// Token: 0x06002894 RID: 10388 RVA: 0x0050B980 File Offset: 0x00509B80
		internal unsafe static void ResizeArrays(bool unloading = false)
		{
			Array.Resize<Asset<Texture2D>>(ref TextureAssets.Wall, WallLoader.nextWall);
			LoaderUtils.ResetStaticMembers(typeof(WallID), true);
			Array.Resize<bool>(ref Main.wallHouse, WallLoader.nextWall);
			Array.Resize<bool>(ref Main.wallDungeon, WallLoader.nextWall);
			Array.Resize<bool>(ref Main.wallLight, WallLoader.nextWall);
			Array.Resize<int>(ref Main.wallBlend, WallLoader.nextWall);
			for (int i = (int)WallID.Count; i < WallLoader.nextWall; i++)
			{
				Main.wallBlend[i] = i;
			}
			Array.Resize<byte>(ref Main.wallLargeFrames, WallLoader.nextWall);
			Array.Resize<byte>(ref Main.wallFrame, WallLoader.nextWall);
			Array.Resize<byte>(ref Main.wallFrameCounter, WallLoader.nextWall);
			WallLoader.wallConversionDelegates = new List<WallLoader.ConvertWall>[WallLoader.nextWall][];
			ModLoader.BuildGlobalHook<GlobalWall, Func<int, int, int, bool, bool>>(ref WallLoader.HookKillSound, WallLoader.globalWalls, (GlobalWall g) => (Func<int, int, int, bool, bool>)methodof(GlobalBlockType.KillSound(int, int, int, bool)).CreateDelegate(typeof(Func<int, int, int, bool, bool>), g));
			ModLoader.BuildGlobalHook<GlobalWall, WallLoader.DelegateNumDust>(ref WallLoader.HookNumDust, WallLoader.globalWalls, (GlobalWall g) => (WallLoader.DelegateNumDust)methodof(GlobalBlockType.NumDust(int, int, int, bool, int*)).CreateDelegate(typeof(WallLoader.DelegateNumDust), g));
			ModLoader.BuildGlobalHook<GlobalWall, WallLoader.DelegateCreateDust>(ref WallLoader.HookCreateDust, WallLoader.globalWalls, (GlobalWall g) => (WallLoader.DelegateCreateDust)methodof(GlobalBlockType.CreateDust(int, int, int, int*)).CreateDelegate(typeof(WallLoader.DelegateCreateDust), g));
			ModLoader.BuildGlobalHook<GlobalWall, WallLoader.DelegateDrop>(ref WallLoader.HookDrop, WallLoader.globalWalls, (GlobalWall g) => (WallLoader.DelegateDrop)methodof(GlobalWall.Drop(int, int, int, int*)).CreateDelegate(typeof(WallLoader.DelegateDrop), g));
			ModLoader.BuildGlobalHook<GlobalWall, WallLoader.DelegateKillWall>(ref WallLoader.HookKillWall, WallLoader.globalWalls, (GlobalWall g) => (WallLoader.DelegateKillWall)methodof(GlobalWall.KillWall(int, int, int, bool*)).CreateDelegate(typeof(WallLoader.DelegateKillWall), g));
			ModLoader.BuildGlobalHook<GlobalWall, WallLoader.DelegateWallFrame>(ref WallLoader.HookWallFrame, WallLoader.globalWalls, (GlobalWall g) => (WallLoader.DelegateWallFrame)methodof(GlobalWall.WallFrame(int, int, int, bool, int*, int*)).CreateDelegate(typeof(WallLoader.DelegateWallFrame), g));
			ModLoader.BuildGlobalHook<GlobalWall, Func<int, int, int, bool>>(ref WallLoader.HookCanPlace, WallLoader.globalWalls, (GlobalWall g) => (Func<int, int, int, bool>)methodof(GlobalBlockType.CanPlace(int, int, int)).CreateDelegate(typeof(Func<int, int, int, bool>), g));
			ModLoader.BuildGlobalHook<GlobalWall, Func<int, int, int, bool>>(ref WallLoader.HookCanExplode, WallLoader.globalWalls, (GlobalWall g) => (Func<int, int, int, bool>)methodof(GlobalBlockType.CanExplode(int, int, int)).CreateDelegate(typeof(Func<int, int, int, bool>), g));
			ModLoader.BuildGlobalHook<GlobalWall, Func<int, int, int, Player, string, bool>>(ref WallLoader.HookCanBeTeleportedTo, WallLoader.globalWalls, (GlobalWall g) => (Func<int, int, int, Player, string, bool>)methodof(GlobalWall.CanBeTeleportedTo(int, int, int, Player, string)).CreateDelegate(typeof(Func<int, int, int, Player, string, bool>), g));
			ModLoader.BuildGlobalHook<GlobalWall, WallLoader.DelegateModifyLight>(ref WallLoader.HookModifyLight, WallLoader.globalWalls, (GlobalWall g) => (WallLoader.DelegateModifyLight)methodof(GlobalBlockType.ModifyLight(int, int, int, float*, float*, float*)).CreateDelegate(typeof(WallLoader.DelegateModifyLight), g));
			ModLoader.BuildGlobalHook<GlobalWall, Action<int, int, int>>(ref WallLoader.HookRandomUpdate, WallLoader.globalWalls, (GlobalWall g) => (Action<int, int, int>)methodof(GlobalBlockType.RandomUpdate(int, int, int)).CreateDelegate(typeof(Action<int, int, int>), g));
			ModLoader.BuildGlobalHook<GlobalWall, Func<int, int, int, SpriteBatch, bool>>(ref WallLoader.HookPreDraw, WallLoader.globalWalls, (GlobalWall g) => (Func<int, int, int, SpriteBatch, bool>)methodof(GlobalBlockType.PreDraw(int, int, int, SpriteBatch)).CreateDelegate(typeof(Func<int, int, int, SpriteBatch, bool>), g));
			ModLoader.BuildGlobalHook<GlobalWall, Action<int, int, int, SpriteBatch>>(ref WallLoader.HookPostDraw, WallLoader.globalWalls, (GlobalWall g) => (Action<int, int, int, SpriteBatch>)methodof(GlobalBlockType.PostDraw(int, int, int, SpriteBatch)).CreateDelegate(typeof(Action<int, int, int, SpriteBatch>), g));
			ModLoader.BuildGlobalHook<GlobalWall, Action<int, int, int, Item>>(ref WallLoader.HookPlaceInWorld, WallLoader.globalWalls, (GlobalWall g) => (Action<int, int, int, Item>)methodof(GlobalBlockType.PlaceInWorld(int, int, int, Item)).CreateDelegate(typeof(Action<int, int, int, Item>), g));
			if (!unloading)
			{
				WallLoader.loaded = true;
			}
		}

		// Token: 0x06002895 RID: 10389 RVA: 0x0050C2B5 File Offset: 0x0050A4B5
		internal static void Unload()
		{
			WallLoader.loaded = false;
			WallLoader.walls.Clear();
			WallLoader.nextWall = (int)WallID.Count;
			WallLoader.globalWalls.Clear();
			WallLoader.wallTypeToItemType.Clear();
			WallLoader.wallConversionDelegates = null;
		}

		// Token: 0x06002896 RID: 10390 RVA: 0x0050C2EB File Offset: 0x0050A4EB
		internal static void WriteType(ushort wall, byte[] data, ref int index, ref byte flags)
		{
			if (wall > 255)
			{
				data[index] = (byte)(wall >> 8);
				index++;
				flags |= 32;
			}
		}

		// Token: 0x06002897 RID: 10391 RVA: 0x0050C30B File Offset: 0x0050A50B
		internal static void ReadType(ref ushort wall, BinaryReader reader, byte flags, IDictionary<int, int> wallTable)
		{
			if ((flags & 32) == 32)
			{
				wall |= (ushort)(reader.ReadByte() << 8);
			}
			if (wallTable.ContainsKey((int)wall))
			{
				wall = (ushort)wallTable[(int)wall];
			}
		}

		// Token: 0x06002898 RID: 10392 RVA: 0x0050C33C File Offset: 0x0050A53C
		public static bool KillSound(int i, int j, int type, bool fail)
		{
			Func<int, int, int, bool, bool>[] hookKillSound = WallLoader.HookKillSound;
			for (int k = 0; k < hookKillSound.Length; k++)
			{
				if (!hookKillSound[k](i, j, type, fail))
				{
					return false;
				}
			}
			ModWall modWall = WallLoader.GetWall(type);
			if (modWall == null)
			{
				return true;
			}
			if (!modWall.KillSound(i, j, fail))
			{
				return false;
			}
			SoundEngine.PlaySound(modWall.HitSound, new Vector2?(new Vector2((float)(i * 16), (float)(j * 16))));
			return false;
		}

		// Token: 0x06002899 RID: 10393 RVA: 0x0050C3A8 File Offset: 0x0050A5A8
		public static void NumDust(int i, int j, int type, bool fail, ref int numDust)
		{
			ModWall wall = WallLoader.GetWall(type);
			if (wall != null)
			{
				wall.NumDust(i, j, fail, ref numDust);
			}
			WallLoader.DelegateNumDust[] hookNumDust = WallLoader.HookNumDust;
			for (int k = 0; k < hookNumDust.Length; k++)
			{
				hookNumDust[k](i, j, type, fail, ref numDust);
			}
		}

		// Token: 0x0600289A RID: 10394 RVA: 0x0050C3F0 File Offset: 0x0050A5F0
		public static bool CreateDust(int i, int j, int type, ref int dustType)
		{
			WallLoader.DelegateCreateDust[] hookCreateDust = WallLoader.HookCreateDust;
			for (int k = 0; k < hookCreateDust.Length; k++)
			{
				if (!hookCreateDust[k](i, j, type, ref dustType))
				{
					return false;
				}
			}
			ModWall wall = WallLoader.GetWall(type);
			return wall == null || wall.CreateDust(i, j, ref dustType);
		}

		// Token: 0x0600289B RID: 10395 RVA: 0x0050C438 File Offset: 0x0050A638
		public static bool Drop(int i, int j, int type, ref int dropType)
		{
			WallLoader.DelegateDrop[] hookDrop = WallLoader.HookDrop;
			for (int k = 0; k < hookDrop.Length; k++)
			{
				if (!hookDrop[k](i, j, type, ref dropType))
				{
					return false;
				}
			}
			ModWall modWall = WallLoader.GetWall(type);
			if (modWall != null)
			{
				int value;
				if (WallLoader.wallTypeToItemType.TryGetValue(type, out value))
				{
					dropType = value;
				}
				return modWall.Drop(i, j, ref dropType);
			}
			return true;
		}

		// Token: 0x0600289C RID: 10396 RVA: 0x0050C490 File Offset: 0x0050A690
		public static void KillWall(int i, int j, int type, ref bool fail)
		{
			ModWall wall = WallLoader.GetWall(type);
			if (wall != null)
			{
				wall.KillWall(i, j, ref fail);
			}
			WallLoader.DelegateKillWall[] hookKillWall = WallLoader.HookKillWall;
			for (int k = 0; k < hookKillWall.Length; k++)
			{
				hookKillWall[k](i, j, type, ref fail);
			}
		}

		// Token: 0x0600289D RID: 10397 RVA: 0x0050C4D4 File Offset: 0x0050A6D4
		public static bool CanPlace(int i, int j, int type)
		{
			Func<int, int, int, bool>[] hookCanPlace = WallLoader.HookCanPlace;
			for (int k = 0; k < hookCanPlace.Length; k++)
			{
				if (!hookCanPlace[k](i, j, type))
				{
					return false;
				}
			}
			ModWall wall = WallLoader.GetWall(type);
			return wall == null || wall.CanPlace(i, j);
		}

		// Token: 0x0600289E RID: 10398 RVA: 0x0050C518 File Offset: 0x0050A718
		public static bool CanExplode(int i, int j, int type)
		{
			Func<int, int, int, bool>[] hookCanExplode = WallLoader.HookCanExplode;
			for (int k = 0; k < hookCanExplode.Length; k++)
			{
				if (!hookCanExplode[k](i, j, type))
				{
					return false;
				}
			}
			ModWall wall = WallLoader.GetWall(type);
			return wall == null || wall.CanExplode(i, j);
		}

		// Token: 0x0600289F RID: 10399 RVA: 0x0050C55C File Offset: 0x0050A75C
		public static bool CanBeTeleportedTo(int i, int j, int type, Player player, string context)
		{
			Func<int, int, int, Player, string, bool>[] hookCanBeTeleportedTo = WallLoader.HookCanBeTeleportedTo;
			for (int k = 0; k < hookCanBeTeleportedTo.Length; k++)
			{
				if (!hookCanBeTeleportedTo[k](i, j, type, player, context))
				{
					return false;
				}
			}
			ModWall wall = WallLoader.GetWall(type);
			return wall == null || wall.CanBeTeleportedTo(i, j, player, context);
		}

		// Token: 0x060028A0 RID: 10400 RVA: 0x0050C5A8 File Offset: 0x0050A7A8
		public static void ModifyLight(int i, int j, int type, ref float r, ref float g, ref float b)
		{
			ModWall wall = WallLoader.GetWall(type);
			if (wall != null)
			{
				wall.ModifyLight(i, j, ref r, ref g, ref b);
			}
			WallLoader.DelegateModifyLight[] hookModifyLight = WallLoader.HookModifyLight;
			for (int k = 0; k < hookModifyLight.Length; k++)
			{
				hookModifyLight[k](i, j, type, ref r, ref g, ref b);
			}
		}

		/// <summary>
		/// Registers a wall type as having custom biome conversion code for this specific <see cref="T:Terraria.ID.BiomeConversionID" />. For modded walls, you can directly use <see cref="M:Terraria.ModLoader.WallLoader.Convert(System.Int32,System.Int32,System.Int32)" /> <br />
		/// If you need to register conversions that rely on <see cref="T:Terraria.ID.WallID.Sets.Conversion" /> being fully populated, consider doing it in <see cref="M:Terraria.ModLoader.ModBiomeConversion.PostSetupContent" />
		/// </summary>
		/// <param name="wallType">The wall type that has is affected by this custom conversion.</param>
		/// <param name="conversionType">The conversion type for which the wall should use custom conversion code.</param>
		/// <param name="conversionDelegate">Code to run when the wall attempts to get converted. Return false to signal that your custom conversion took place and that vanilla code shouldn't be ran.</param>
		// Token: 0x060028A1 RID: 10401 RVA: 0x0050C5F4 File Offset: 0x0050A7F4
		public static void RegisterConversion(int wallType, int conversionType, WallLoader.ConvertWall conversionDelegate)
		{
			if (WallLoader.wallConversionDelegates == null)
			{
				throw new Exception(Language.GetTextValue("tModLoader.LoadErrorCallDuringLoad", "WallLoader.RegisterConversion"));
			}
			List<WallLoader.ConvertWall>[][] array = WallLoader.wallConversionDelegates;
			List<WallLoader.ConvertWall>[] array2;
			if ((array2 = array[wallType]) == null)
			{
				array2 = (array[wallType] = new List<WallLoader.ConvertWall>[BiomeConversionLoader.BiomeConversionCount]);
			}
			List<WallLoader.ConvertWall>[] array3 = array2;
			List<WallLoader.ConvertWall> list;
			if ((list = array3[conversionType]) == null)
			{
				list = (array3[conversionType] = new List<WallLoader.ConvertWall>());
			}
			list.Add(conversionDelegate);
		}

		// Token: 0x060028A2 RID: 10402 RVA: 0x0050C658 File Offset: 0x0050A858
		public unsafe static bool Convert(int i, int j, int conversionType)
		{
			int type = (int)(*Main.tile[i, j].wall);
			List<WallLoader.ConvertWall>[] array = WallLoader.wallConversionDelegates[type];
			List<WallLoader.ConvertWall> list = (array != null) ? array[conversionType] : null;
			if (list != null)
			{
				Span<WallLoader.ConvertWall> span = CollectionsMarshal.AsSpan<WallLoader.ConvertWall>(list);
				for (int k = 0; k < span.Length; k++)
				{
					if (!(*span[k])(i, j, type, conversionType))
					{
						return false;
					}
				}
			}
			ModWall wall = WallLoader.GetWall(type);
			if (wall != null)
			{
				wall.Convert(i, j, conversionType);
			}
			return true;
		}

		// Token: 0x060028A3 RID: 10403 RVA: 0x0050C6D8 File Offset: 0x0050A8D8
		public static void RandomUpdate(int i, int j, int type)
		{
			ModWall wall = WallLoader.GetWall(type);
			if (wall != null)
			{
				wall.RandomUpdate(i, j);
			}
			Action<int, int, int>[] hookRandomUpdate = WallLoader.HookRandomUpdate;
			for (int k = 0; k < hookRandomUpdate.Length; k++)
			{
				hookRandomUpdate[k](i, j, type);
			}
		}

		// Token: 0x060028A4 RID: 10404 RVA: 0x0050C718 File Offset: 0x0050A918
		public static bool WallFrame(int i, int j, int type, bool randomizeFrame, ref int style, ref int frameNumber)
		{
			ModWall modWall = WallLoader.GetWall(type);
			if (modWall != null && !modWall.WallFrame(i, j, randomizeFrame, ref style, ref frameNumber))
			{
				return false;
			}
			WallLoader.DelegateWallFrame[] hookWallFrame = WallLoader.HookWallFrame;
			for (int k = 0; k < hookWallFrame.Length; k++)
			{
				if (!hookWallFrame[k](i, j, type, randomizeFrame, ref style, ref frameNumber))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060028A5 RID: 10405 RVA: 0x0050C76C File Offset: 0x0050A96C
		public static void AnimateWalls()
		{
			if (WallLoader.loaded)
			{
				for (int i = 0; i < WallLoader.walls.Count; i++)
				{
					ModWall modWall = WallLoader.walls[i];
					modWall.AnimateWall(ref Main.wallFrame[(int)modWall.Type], ref Main.wallFrameCounter[(int)modWall.Type]);
				}
			}
		}

		// Token: 0x060028A6 RID: 10406 RVA: 0x0050C7C8 File Offset: 0x0050A9C8
		public static bool PreDraw(int i, int j, int type, SpriteBatch spriteBatch)
		{
			Func<int, int, int, SpriteBatch, bool>[] hookPreDraw = WallLoader.HookPreDraw;
			for (int k = 0; k < hookPreDraw.Length; k++)
			{
				if (!hookPreDraw[k](i, j, type, spriteBatch))
				{
					return false;
				}
			}
			ModWall wall = WallLoader.GetWall(type);
			return wall == null || wall.PreDraw(i, j, spriteBatch);
		}

		// Token: 0x060028A7 RID: 10407 RVA: 0x0050C810 File Offset: 0x0050AA10
		public static void PostDraw(int i, int j, int type, SpriteBatch spriteBatch)
		{
			ModWall wall = WallLoader.GetWall(type);
			if (wall != null)
			{
				wall.PostDraw(i, j, spriteBatch);
			}
			Action<int, int, int, SpriteBatch>[] hookPostDraw = WallLoader.HookPostDraw;
			for (int k = 0; k < hookPostDraw.Length; k++)
			{
				hookPostDraw[k](i, j, type, spriteBatch);
			}
		}

		// Token: 0x060028A8 RID: 10408 RVA: 0x0050C854 File Offset: 0x0050AA54
		public static void PlaceInWorld(int i, int j, Item item)
		{
			int type = item.createWall;
			if (type < 0)
			{
				return;
			}
			Action<int, int, int, Item>[] hookPlaceInWorld = WallLoader.HookPlaceInWorld;
			for (int k = 0; k < hookPlaceInWorld.Length; k++)
			{
				hookPlaceInWorld[k](i, j, type, item);
			}
			ModWall wall = WallLoader.GetWall(type);
			if (wall == null)
			{
				return;
			}
			wall.PlaceInWorld(i, j, item);
		}

		// Token: 0x060028A9 RID: 10409 RVA: 0x0050C8A0 File Offset: 0x0050AAA0
		internal static void FinishSetup()
		{
			for (int i = 0; i < ItemLoader.ItemCount; i++)
			{
				Item item = ContentSamples.ItemsByType[i];
				if (!ItemID.Sets.DisableAutomaticPlaceableDrop[i] && item.createWall > -1)
				{
					WallLoader.wallTypeToItemType.TryAdd(item.createWall, item.type);
				}
			}
		}

		// Token: 0x04001936 RID: 6454
		private static int nextWall = (int)WallID.Count;

		// Token: 0x04001937 RID: 6455
		internal static readonly IList<ModWall> walls = new List<ModWall>();

		// Token: 0x04001938 RID: 6456
		internal static readonly IList<GlobalWall> globalWalls = new List<GlobalWall>();

		/// <summary> Maps Wall type to the Item type that places the wall. </summary>
		// Token: 0x04001939 RID: 6457
		internal static readonly Dictionary<int, int> wallTypeToItemType = new Dictionary<int, int>();

		// Token: 0x0400193A RID: 6458
		internal static List<WallLoader.ConvertWall>[][] wallConversionDelegates = null;

		// Token: 0x0400193B RID: 6459
		private static bool loaded = false;

		// Token: 0x0400193C RID: 6460
		private static Func<int, int, int, bool, bool>[] HookKillSound;

		// Token: 0x0400193D RID: 6461
		private static WallLoader.DelegateNumDust[] HookNumDust;

		// Token: 0x0400193E RID: 6462
		private static WallLoader.DelegateCreateDust[] HookCreateDust;

		// Token: 0x0400193F RID: 6463
		private static WallLoader.DelegateDrop[] HookDrop;

		// Token: 0x04001940 RID: 6464
		private static WallLoader.DelegateKillWall[] HookKillWall;

		// Token: 0x04001941 RID: 6465
		private static Func<int, int, int, bool>[] HookCanPlace;

		// Token: 0x04001942 RID: 6466
		private static Func<int, int, int, bool>[] HookCanExplode;

		// Token: 0x04001943 RID: 6467
		private static Func<int, int, int, Player, string, bool>[] HookCanBeTeleportedTo;

		// Token: 0x04001944 RID: 6468
		private static WallLoader.DelegateModifyLight[] HookModifyLight;

		// Token: 0x04001945 RID: 6469
		private static Action<int, int, int>[] HookRandomUpdate;

		// Token: 0x04001946 RID: 6470
		private static WallLoader.DelegateWallFrame[] HookWallFrame;

		// Token: 0x04001947 RID: 6471
		private static Func<int, int, int, SpriteBatch, bool>[] HookPreDraw;

		// Token: 0x04001948 RID: 6472
		private static Action<int, int, int, SpriteBatch>[] HookPostDraw;

		// Token: 0x04001949 RID: 6473
		private static Action<int, int, int, Item>[] HookPlaceInWorld;

		// Token: 0x020009E0 RID: 2528
		// (Invoke) Token: 0x06005690 RID: 22160
		public delegate bool ConvertWall(int i, int j, int type, int conversionType);

		// Token: 0x020009E1 RID: 2529
		// (Invoke) Token: 0x06005694 RID: 22164
		private delegate void DelegateNumDust(int i, int j, int type, bool fail, ref int num);

		// Token: 0x020009E2 RID: 2530
		// (Invoke) Token: 0x06005698 RID: 22168
		private delegate bool DelegateCreateDust(int i, int j, int type, ref int dustType);

		// Token: 0x020009E3 RID: 2531
		// (Invoke) Token: 0x0600569C RID: 22172
		private delegate bool DelegateDrop(int i, int j, int type, ref int dropType);

		// Token: 0x020009E4 RID: 2532
		// (Invoke) Token: 0x060056A0 RID: 22176
		private delegate void DelegateKillWall(int i, int j, int type, ref bool fail);

		// Token: 0x020009E5 RID: 2533
		// (Invoke) Token: 0x060056A4 RID: 22180
		private delegate void DelegateModifyLight(int i, int j, int type, ref float r, ref float g, ref float b);

		// Token: 0x020009E6 RID: 2534
		// (Invoke) Token: 0x060056A8 RID: 22184
		private delegate bool DelegateWallFrame(int i, int j, int type, bool randomizeFrame, ref int style, ref int frameNumber);
	}
}
