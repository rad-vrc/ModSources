using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;
using Terraria.ModLoader;
using VanillaQoL.Config;

namespace QoLCompendium.Core.Changes.PlayerChanges
{
	// Token: 0x02000234 RID: 564
	public class VeinMiningSystem : ModSystem
	{
		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x00068E85 File Offset: 0x00067085
		[JITWhenModsEnabled(new string[]
		{
			"VanillaQoL"
		})]
		public static bool VanillaQoLVeinminer
		{
			get
			{
				return QoLConfig.Instance.veinMining;
			}
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x00068E91 File Offset: 0x00067091
		public override void Load()
		{
			ILContext.Manipulator manipulator;
			if ((manipulator = VeinMiningSystem.<>O.<0>__PickTilePatch) == null)
			{
				manipulator = (VeinMiningSystem.<>O.<0>__PickTilePatch = new ILContext.Manipulator(VeinMiningSystem.PickTilePatch));
			}
			IL_Player.PickTile += manipulator;
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x00068EB3 File Offset: 0x000670B3
		public override void Unload()
		{
			ILContext.Manipulator manipulator;
			if ((manipulator = VeinMiningSystem.<>O.<0>__PickTilePatch) == null)
			{
				manipulator = (VeinMiningSystem.<>O.<0>__PickTilePatch = new ILContext.Manipulator(VeinMiningSystem.PickTilePatch));
			}
			IL_Player.PickTile -= manipulator;
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x00068ED5 File Offset: 0x000670D5
		public override void PreUpdateWorld()
		{
			VeinMiningSystem.threshold = QoLCompendium.mainConfig.VeinMinerTileLimit;
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x00068EE8 File Offset: 0x000670E8
		public static void PickTilePatch(ILContext il)
		{
			ILCursor ilCursor = new ILCursor(il);
			ILCursor ilcursor = ilCursor;
			MoveType moveType = 0;
			Func<Instruction, bool>[] array = new Func<Instruction, bool>[6];
			array[0] = ((Instruction i) => ILPatternMatchingExt.MatchLdarg1(i));
			array[1] = ((Instruction i) => ILPatternMatchingExt.MatchLdarg2(i));
			array[2] = ((Instruction i) => ILPatternMatchingExt.MatchLdcI4(i, 0));
			array[3] = ((Instruction i) => ILPatternMatchingExt.MatchLdcI4(i, 0));
			array[4] = ((Instruction i) => ILPatternMatchingExt.MatchLdcI4(i, 0));
			array[5] = ((Instruction i) => ILPatternMatchingExt.MatchCall<WorldGen>(i, "KillTile"));
			if (!ilcursor.TryGotoNext(moveType, array))
			{
				return;
			}
			ilCursor.EmitLdarg0();
			ilCursor.EmitCall("ClearCD");
			ilCursor.EmitLdarg0();
			ilCursor.EmitLdarg1();
			ilCursor.EmitLdarg2();
			ilCursor.EmitLdarg3();
			ilCursor.EmitCall("VeinMine");
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x00069013 File Offset: 0x00067213
		public static void ClearCD(Player player)
		{
			player.GetModPlayer<VeinMiningPlayer>().ctr = 0;
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x00069024 File Offset: 0x00067224
		public static void VeinMine(Player player, int x, int y, int pickPower)
		{
			Tile tile = Framing.GetTileSafely(x, y);
			VeinMiningPlayer modPlayer = player.GetModPlayer<VeinMiningPlayer>();
			if (tile.HasTile && VeinMiningSystem.CanVeinMine(tile) && modPlayer.CanMine)
			{
				modPlayer.pickPower = pickPower;
				foreach (ValueTuple<int, int> coords in VeinMiningSystem.TileRot(x, y))
				{
					Tile tile2 = Framing.GetTileSafely(coords.Item1, coords.Item2);
					bool notInQueue = modPlayer.NotInQueue(coords.Item1, coords.Item2);
					if (tile2.HasTile && VeinMiningSystem.CanVeinMine(tile2) && notInQueue)
					{
						modPlayer.ctr++;
						if (modPlayer.ctr > VeinMiningSystem.threshold)
						{
							modPlayer.ctr = 0;
							modPlayer.CanMine = false;
						}
						modPlayer.QueueTile(coords.Item1, coords.Item2);
						VeinMiningSystem.VeinMine(player, coords.Item1, coords.Item2, pickPower);
					}
				}
			}
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x00069138 File Offset: 0x00067338
		public unsafe static bool CanVeinMine(Tile tile)
		{
			if (VeinMiningSystem.VanillaQoLLoaded() || !QoLCompendium.mainConfig.VeinMiner)
			{
				return false;
			}
			if (QoLCompendium.mainConfig.VeinMinerTileWhitelist != null)
			{
				string fullName = Common.GetFullNameById((int)(*tile.TileType), -1);
				if (fullName == null)
				{
					return false;
				}
				if (QoLCompendium.mainConfig.VeinMinerTileWhitelist.Contains(fullName))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x0006918F File Offset: 0x0006738F
		[return: TupleElementNames(new string[]
		{
			"x",
			"y"
		})]
		public static IEnumerable<ValueTuple<int, int>> TileRot(int x, int y)
		{
			int num;
			for (int i = x - 1; i <= x + 1; i = num)
			{
				for (int j = y - 1; j <= y + 1; j = num)
				{
					if (i != x || j != y)
					{
						yield return new ValueTuple<int, int>(i, j);
					}
					num = j + 1;
				}
				num = i + 1;
			}
			yield break;
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x000691A6 File Offset: 0x000673A6
		public static bool VanillaQoLLoaded()
		{
			return ModLoader.HasMod("VanillaQoL") && VeinMiningSystem.VanillaQoLVeinminer;
		}

		// Token: 0x040005A6 RID: 1446
		public static int threshold = QoLCompendium.mainConfig.VeinMinerTileLimit;

		// Token: 0x0200054B RID: 1355
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000EF6 RID: 3830
			public static ILContext.Manipulator <0>__PickTilePatch;
		}
	}
}
