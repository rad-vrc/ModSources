using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace SmarterCursor
{
	// Token: 0x02000005 RID: 5
	public class SmarterCursor : Mod
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002090 File Offset: 0x00000290
		public override void Load()
		{
			ILContext.Manipulator manipulator;
			if ((manipulator = SmarterCursor.<>O.<0>__TileFillPatch) == null)
			{
				manipulator = (SmarterCursor.<>O.<0>__TileFillPatch = new ILContext.Manipulator(SmarterCursor.TileFillPatch));
			}
			IL_SmartCursorHelper.Step_BlocksFilling += manipulator;
			ILContext.Manipulator manipulator2;
			if ((manipulator2 = SmarterCursor.<>O.<1>__TileLinePatch) == null)
			{
				manipulator2 = (SmarterCursor.<>O.<1>__TileLinePatch = new ILContext.Manipulator(SmarterCursor.TileLinePatch));
			}
			IL_SmartCursorHelper.Step_BlocksLines += manipulator2;
			ILContext.Manipulator manipulator3;
			if ((manipulator3 = SmarterCursor.<>O.<2>__PlatformSelectPatch) == null)
			{
				manipulator3 = (SmarterCursor.<>O.<2>__PlatformSelectPatch = new ILContext.Manipulator(SmarterCursor.PlatformSelectPatch));
			}
			IL_SmartCursorHelper.Step_Platforms += manipulator3;
			ILContext.Manipulator manipulator4;
			if ((manipulator4 = SmarterCursor.<>O.<3>__PlatformZonePatch) == null)
			{
				manipulator4 = (SmarterCursor.<>O.<3>__PlatformZonePatch = new ILContext.Manipulator(SmarterCursor.PlatformZonePatch));
			}
			IL_SmartCursorHelper.Step_Platforms += manipulator4;
			ILContext.Manipulator manipulator5;
			if ((manipulator5 = SmarterCursor.<>O.<4>__DoorDelegatePatch) == null)
			{
				manipulator5 = (SmarterCursor.<>O.<4>__DoorDelegatePatch = new ILContext.Manipulator(SmarterCursor.DoorDelegatePatch));
			}
			IL_DelegateMethods.NotDoorStand += manipulator5;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002140 File Offset: 0x00000340
		private static void TileFillPatch(ILContext il)
		{
			ILCursor c = new ILCursor(il);
			ILLabel label;
			if (!c.TryGotoNext(new Func<Instruction, bool>[]
			{
				(Instruction i) => ILPatternMatchingExt.MatchBrtrue(i, ref label)
			}))
			{
				return;
			}
			c.EmitDelegate<Func<bool, bool>>((bool input) => true);
			c.GotoNext(new Func<Instruction, bool>[]
			{
				(Instruction i) => ILPatternMatchingExt.MatchBrtrue(i, ref label)
			});
			c.GotoNext(new Func<Instruction, bool>[]
			{
				(Instruction i) => ILPatternMatchingExt.MatchBrtrue(i, ref label)
			});
			c.EmitDelegate<Func<bool, bool>>((bool input) => !input);
			ILCursor ilcursor = c;
			Func<Instruction, bool>[] array = new Func<Instruction, bool>[1];
			array[0] = ((Instruction i) => ILPatternMatchingExt.MatchStloc(i, 7));
			ilcursor.GotoNext(array);
			c.GotoNext(new Func<Instruction, bool>[]
			{
				(Instruction i) => ILPatternMatchingExt.MatchBrfalse(i, ref label)
			});
			c.EmitDelegate<Func<bool, bool>>((bool input) => false);
			ILCursor ilcursor2 = c;
			Func<Instruction, bool>[] array2 = new Func<Instruction, bool>[1];
			array2[0] = ((Instruction i) => ILPatternMatchingExt.MatchStloc(i, 19));
			ilcursor2.GotoNext(array2);
			c.Emit(OpCodes.Ldloc, 17);
			c.EmitDelegate<Func<float, Vector2, float>>((float input, Vector2 vector) => Math.Max(input, Math.Abs(vector.Y)));
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000022D0 File Offset: 0x000004D0
		private static void TileLinePatch(ILContext il)
		{
			ILCursor c = new ILCursor(il);
			ILLabel label;
			if (!c.TryGotoNext(new Func<Instruction, bool>[]
			{
				(Instruction i) => ILPatternMatchingExt.MatchBrfalse(i, ref label)
			}))
			{
				return;
			}
			c.EmitDelegate<Func<bool, bool>>((bool input) => !input);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002330 File Offset: 0x00000530
		private unsafe static void DoorDelegatePatch(ILContext il)
		{
			ILCursor c = new ILCursor(il);
			ILLabel target = il.DefineLabel();
			if (!c.TryGotoNext(new Func<Instruction, bool>[]
			{
				(Instruction i) => ILPatternMatchingExt.MatchBlt(i, ref target)
			}))
			{
				return;
			}
			ILLabel label;
			c.GotoPrev(new Func<Instruction, bool>[]
			{
				(Instruction i) => ILPatternMatchingExt.MatchBrfalse(i, ref label)
			});
			ILCursor ilcursor = c;
			int index = ilcursor.Index;
			ilcursor.Index = index + 1;
			c.Emit(OpCodes.Ldc_I4_0);
			c.Emit(OpCodes.Ldloc, 0);
			c.EmitDelegate<Func<bool, Tile, bool>>((bool input, Tile tile) => TileID.Sets.Platforms[(int)(*tile.TileType)] && tile.Slope == SlopeType.Solid);
			c.Emit(OpCodes.Brtrue, target);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000023F4 File Offset: 0x000005F4
		private unsafe static void PlatformSelectPatch(ILContext il)
		{
			ILCursor c = new ILCursor(il);
			ILCursor ilcursor = c;
			Func<Instruction, bool>[] array = new Func<Instruction, bool>[1];
			array[0] = ((Instruction i) => ILPatternMatchingExt.MatchStloc(i, 5));
			if (!ilcursor.TryGotoNext(array))
			{
				return;
			}
			ILLabel label;
			c.GotoPrev(new Func<Instruction, bool>[]
			{
				(Instruction i) => ILPatternMatchingExt.MatchBrtrue(i, ref label)
			});
			c.Emit(OpCodes.Ldloc, 4);
			c.EmitDelegate<Func<bool, Tile, bool>>((bool input, Tile tile) => input && !Main.tileCut[(int)(*tile.TileType)]);
			c.GotoPrev(new Func<Instruction, bool>[]
			{
				(Instruction i) => ILPatternMatchingExt.MatchBrtrue(i, ref label)
			});
			c.Emit(OpCodes.Ldloc, 1);
			c.EmitDelegate<Func<bool, Tile, bool>>((bool input, Tile tile) => input && !Main.tileCut[(int)(*tile.TileType)]);
			c.GotoPrev(new Func<Instruction, bool>[]
			{
				(Instruction i) => ILPatternMatchingExt.MatchBrtrue(i, ref label)
			});
			c.Emit(OpCodes.Ldloc, 1);
			c.EmitDelegate<Func<bool, Tile, bool>>((bool input, Tile tile) => input && !Main.tileCut[(int)(*tile.TileType)]);
			c.GotoPrev(new Func<Instruction, bool>[]
			{
				(Instruction i) => ILPatternMatchingExt.MatchBrtrue(i, ref label)
			});
			c.Emit(OpCodes.Ldloc, 1);
			c.EmitDelegate<Func<bool, Tile, bool>>((bool input, Tile tile) => input && !Main.tileCut[(int)(*tile.TileType)]);
			c.GotoPrev(new Func<Instruction, bool>[]
			{
				(Instruction i) => ILPatternMatchingExt.MatchBrtrue(i, ref label)
			});
			c.Emit(OpCodes.Ldloc, 1);
			c.EmitDelegate<Func<bool, Tile, bool>>((bool input, Tile tile) => input && !Main.tileCut[(int)(*tile.TileType)]);
			c.GotoPrev(new Func<Instruction, bool>[]
			{
				(Instruction i) => ILPatternMatchingExt.MatchBrtrue(i, ref label)
			});
			c.Emit(OpCodes.Ldloc, 1);
			c.EmitDelegate<Func<bool, Tile, bool>>((bool input, Tile tile) => input && !Main.tileCut[(int)(*tile.TileType)]);
			c.GotoPrev(new Func<Instruction, bool>[]
			{
				(Instruction i) => ILPatternMatchingExt.MatchBrtrue(i, ref label)
			});
			c.Emit(OpCodes.Ldloc, 1);
			c.EmitDelegate<Func<bool, Tile, bool>>((bool input, Tile tile) => input && !Main.tileCut[(int)(*tile.TileType)]);
			ILCursor ilcursor2 = c;
			Func<Instruction, bool>[] array2 = new Func<Instruction, bool>[1];
			array2[0] = ((Instruction i) => ILPatternMatchingExt.MatchStloc(i, 5));
			ilcursor2.GotoNext(array2);
			c.GotoNext(new Func<Instruction, bool>[]
			{
				(Instruction i) => ILPatternMatchingExt.MatchBrtrue(i, ref label)
			});
			c.GotoNext(new Func<Instruction, bool>[]
			{
				(Instruction i) => ILPatternMatchingExt.MatchBrtrue(i, ref label)
			});
			c.GotoPrev(new Func<Instruction, bool>[]
			{
				(Instruction i) => ILPatternMatchingExt.MatchBrfalse(i, ref label)
			});
			c.Emit(OpCodes.Ldloc_2);
			c.Emit(OpCodes.Ldloc_3);
			c.Emit(OpCodes.Ldloc, 7);
			c.EmitDelegate<Func<bool, int, int, Tile, bool>>((bool input, int x, int y, Tile tile) => !Main.tileCut[(int)(*Main.tile[x - 1, y].TileType)] && (input || TileID.Sets.Platforms[(int)(*Main.tile[x - 2, y].TileType)]));
			c.GotoNext(new Func<Instruction, bool>[]
			{
				(Instruction i) => ILPatternMatchingExt.MatchBrtrue(i, ref label)
			});
			c.GotoNext(new Func<Instruction, bool>[]
			{
				(Instruction i) => ILPatternMatchingExt.MatchBrtrue(i, ref label)
			});
			c.GotoPrev(new Func<Instruction, bool>[]
			{
				(Instruction i) => ILPatternMatchingExt.MatchBrfalse(i, ref label)
			});
			c.Emit(OpCodes.Ldloc_2);
			c.Emit(OpCodes.Ldloc_3);
			c.Emit(OpCodes.Ldloc, 7);
			c.EmitDelegate<Func<bool, int, int, Tile, bool>>((bool input, int x, int y, Tile tile) => !Main.tileCut[(int)(*Main.tile[x + 1, y].TileType)] && (input || TileID.Sets.Platforms[(int)(*Main.tile[x + 2, y].TileType)]));
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000027D8 File Offset: 0x000009D8
		private unsafe static void PlatformZonePatch(ILContext il)
		{
			ILCursor c = new ILCursor(il);
			ILCursor ilcursor = c;
			Func<Instruction, bool>[] array = new Func<Instruction, bool>[1];
			array[0] = ((Instruction i) => ILPatternMatchingExt.MatchLdloc(i, 9));
			if (!ilcursor.TryGotoNext(array))
			{
				return;
			}
			ILCursor ilcursor2 = c;
			int index = ilcursor2.Index;
			ilcursor2.Index = index + 1;
			c.Emit(OpCodes.Ldarg_0);
			c.Emit(OpCodes.Ldfld, typeof(Main).Assembly.GetType("Terraria.GameContent.SmartCursorHelper+SmartCursorUsageInfo").GetField("mouse"));
			c.Emit(OpCodes.Ldloc, 8);
			c.EmitDelegate<Func<Tuple<int, int>, Vector2, float, Tuple<int, int>>>(delegate(Tuple<int, int> input, Vector2 mouse, float baseDistance)
			{
				if ((!Main.tile[input.Item1 + 1, input.Item2].HasTile || !Main.tileSolid[(int)(*Main.tile[input.Item1 + 1, input.Item2].TileType)]) && (!Main.tile[input.Item1 - 1, input.Item2].HasTile || !Main.tileSolid[(int)(*Main.tile[input.Item1 - 1, input.Item2].TileType)]) && (!Main.tile[input.Item1, input.Item2 + 1].HasTile || !Main.tileSolid[(int)(*Main.tile[input.Item1, input.Item2 + 1].TileType)]))
				{
					List<Tuple<int, int>> targets = (List<Tuple<int, int>>)typeof(SmartCursorHelper).GetField("_targets", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					if (targets.Count > 1)
					{
						float minDistance = -1f;
						Tuple<int, int> tuple = targets[0];
						for (int i = 0; i < targets.Count; i++)
						{
							float distance = Vector2.Distance(new Vector2((float)targets[i].Item1, (float)targets[i].Item2) * 16f + Vector2.One * 8f, mouse);
							if ((minDistance == -1f || distance < minDistance) && targets[i] != input)
							{
								minDistance = distance;
								tuple = targets[i];
							}
						}
						if (baseDistance > minDistance - 6f)
						{
							input = tuple;
						}
					}
				}
				return input;
			});
			c.Emit(OpCodes.Stloc, 9);
			c.Emit(OpCodes.Ldloc, 9);
		}

		// Token: 0x02000006 RID: 6
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000003 RID: 3
			public static ILContext.Manipulator <0>__TileFillPatch;

			// Token: 0x04000004 RID: 4
			public static ILContext.Manipulator <1>__TileLinePatch;

			// Token: 0x04000005 RID: 5
			public static ILContext.Manipulator <2>__PlatformSelectPatch;

			// Token: 0x04000006 RID: 6
			public static ILContext.Manipulator <3>__PlatformZonePatch;

			// Token: 0x04000007 RID: 7
			public static ILContext.Manipulator <4>__DoorDelegatePatch;
		}
	}
}
