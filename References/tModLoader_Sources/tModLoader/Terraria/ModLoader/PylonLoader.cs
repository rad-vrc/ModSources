using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria.GameContent;
using Terraria.Map;

namespace Terraria.ModLoader
{
	// Token: 0x020001EE RID: 494
	public static class PylonLoader
	{
		// Token: 0x060026D6 RID: 9942 RVA: 0x00501208 File Offset: 0x004FF408
		internal static void FinishSetup()
		{
			foreach (ModPylon pylon in TileLoader.tiles.OfType<ModPylon>())
			{
				ModTypeLookup<ModPylon>.Register(pylon);
				pylon.PylonType = PylonLoader.ReservePylonID();
				PylonLoader.modPylons.Add(pylon);
			}
		}

		// Token: 0x060026D7 RID: 9943 RVA: 0x00501270 File Offset: 0x004FF470
		internal static void Unload()
		{
			PylonLoader.globalPylons.Clear();
			PylonLoader.modPylons.Clear();
			PylonLoader.nextPylonID = PylonLoader.VanillaPylonCount;
		}

		// Token: 0x060026D8 RID: 9944 RVA: 0x00501290 File Offset: 0x004FF490
		internal static TeleportPylonType ReservePylonID()
		{
			TeleportPylonType result = PylonLoader.nextPylonID;
			PylonLoader.nextPylonID += 1;
			return result;
		}

		/// <summary>
		/// Returns the ModPylon associated with the passed in type. Returns null on failure.
		/// </summary>
		// Token: 0x060026D9 RID: 9945 RVA: 0x005012A4 File Offset: 0x004FF4A4
		public static ModPylon GetModPylon(TeleportPylonType pylonType)
		{
			if (pylonType < PylonLoader.VanillaPylonCount)
			{
				return null;
			}
			return PylonLoader.modPylons[(int)(pylonType - PylonLoader.VanillaPylonCount)];
		}

		// Token: 0x060026DA RID: 9946 RVA: 0x005012C4 File Offset: 0x004FF4C4
		public static bool PreDrawMapIcon(ref MapOverlayDrawContext context, ref string mouseOverText, ref TeleportPylonInfo pylonInfo, ref bool isNearPylon, ref Color drawColor, ref float deselectedScale, ref float selectedScale)
		{
			bool returnValue = true;
			foreach (GlobalPylon globalPylon in PylonLoader.globalPylons)
			{
				returnValue &= globalPylon.PreDrawMapIcon(ref context, ref mouseOverText, ref pylonInfo, ref isNearPylon, ref drawColor, ref deselectedScale, ref selectedScale);
			}
			return returnValue;
		}

		// Token: 0x060026DB RID: 9947 RVA: 0x00501320 File Offset: 0x004FF520
		public static bool? PreCanPlacePylon(int x, int y, int tileType, TeleportPylonType pylonType)
		{
			bool? returnValue = null;
			foreach (GlobalPylon globalPylon in PylonLoader.globalPylons)
			{
				bool? shouldSucceed = globalPylon.PreCanPlacePylon(x, y, tileType, pylonType);
				if (shouldSucceed != null)
				{
					if (!shouldSucceed.Value)
					{
						return new bool?(false);
					}
					returnValue = new bool?(true);
				}
			}
			return returnValue;
		}

		// Token: 0x060026DC RID: 9948 RVA: 0x0050139C File Offset: 0x004FF59C
		public static bool? ValidTeleportCheck_PreNPCCount(TeleportPylonInfo pylonInfo, ref int defaultNecessaryNPCCount)
		{
			bool? returnValue = null;
			foreach (GlobalPylon globalPylon in PylonLoader.globalPylons)
			{
				bool? shouldSucceed = globalPylon.ValidTeleportCheck_PreNPCCount(pylonInfo, ref defaultNecessaryNPCCount);
				if (shouldSucceed != null)
				{
					if (!shouldSucceed.Value)
					{
						return new bool?(false);
					}
					returnValue = new bool?(true);
				}
			}
			return returnValue;
		}

		// Token: 0x060026DD RID: 9949 RVA: 0x00501418 File Offset: 0x004FF618
		public static bool? ValidTeleportCheck_PreAnyDanger(TeleportPylonInfo pylonInfo)
		{
			bool? returnValue = null;
			foreach (GlobalPylon globalPylon in PylonLoader.globalPylons)
			{
				bool? shouldSucceed = globalPylon.ValidTeleportCheck_PreAnyDanger(pylonInfo);
				if (shouldSucceed != null)
				{
					if (!shouldSucceed.Value)
					{
						return new bool?(false);
					}
					returnValue = new bool?(true);
				}
			}
			return returnValue;
		}

		// Token: 0x060026DE RID: 9950 RVA: 0x00501490 File Offset: 0x004FF690
		public static bool? ValidTeleportCheck_PreBiomeRequirements(TeleportPylonInfo pylonInfo, SceneMetrics sceneData)
		{
			bool? returnValue = null;
			foreach (GlobalPylon globalPylon in PylonLoader.globalPylons)
			{
				bool? shouldSucceed = globalPylon.ValidTeleportCheck_PreBiomeRequirements(pylonInfo, sceneData);
				if (shouldSucceed != null)
				{
					if (!shouldSucceed.Value)
					{
						return new bool?(false);
					}
					returnValue = new bool?(true);
				}
			}
			return returnValue;
		}

		// Token: 0x060026DF RID: 9951 RVA: 0x0050150C File Offset: 0x004FF70C
		public static void PostValidTeleportCheck(TeleportPylonInfo destinationPylonInfo, TeleportPylonInfo nearbyPylonInfo, ref bool destinationPylonValid, ref bool validNearbyPylonFound, ref string errorKey)
		{
			foreach (GlobalPylon globalPylon in PylonLoader.globalPylons)
			{
				globalPylon.PostValidTeleportCheck(destinationPylonInfo, nearbyPylonInfo, ref destinationPylonValid, ref validNearbyPylonFound, ref errorKey);
			}
		}

		// Token: 0x04001894 RID: 6292
		public static readonly TeleportPylonType VanillaPylonCount = TeleportPylonType.Count;

		// Token: 0x04001895 RID: 6293
		internal static readonly IList<GlobalPylon> globalPylons = new List<GlobalPylon>();

		// Token: 0x04001896 RID: 6294
		internal static readonly IList<ModPylon> modPylons = new List<ModPylon>();

		// Token: 0x04001897 RID: 6295
		internal static TeleportPylonType nextPylonID = PylonLoader.VanillaPylonCount;
	}
}
