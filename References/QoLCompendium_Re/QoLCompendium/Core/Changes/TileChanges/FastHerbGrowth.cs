using System;
using System.Runtime.CompilerServices;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.TileChanges
{
	// Token: 0x0200021A RID: 538
	public class FastHerbGrowth : ModSystem
	{
		// Token: 0x06000D1D RID: 3357 RVA: 0x00066A9C File Offset: 0x00064C9C
		public override void Load()
		{
			ILContext.Manipulator manipulator;
			if ((manipulator = FastHerbGrowth.<>O.<0>__WorldGen_GrowAlch) == null)
			{
				manipulator = (FastHerbGrowth.<>O.<0>__WorldGen_GrowAlch = new ILContext.Manipulator(FastHerbGrowth.WorldGen_GrowAlch));
			}
			IL_WorldGen.GrowAlch += manipulator;
			On_TileDrawing.hook_IsAlchemyPlantHarvestable hook_IsAlchemyPlantHarvestable;
			if ((hook_IsAlchemyPlantHarvestable = FastHerbGrowth.<>O.<1>__TileDrawing_IsAlchemyPlantHarvestable) == null)
			{
				hook_IsAlchemyPlantHarvestable = (FastHerbGrowth.<>O.<1>__TileDrawing_IsAlchemyPlantHarvestable = new On_TileDrawing.hook_IsAlchemyPlantHarvestable(FastHerbGrowth.TileDrawing_IsAlchemyPlantHarvestable));
			}
			On_TileDrawing.IsAlchemyPlantHarvestable += hook_IsAlchemyPlantHarvestable;
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x00066AEC File Offset: 0x00064CEC
		public override void Unload()
		{
			ILContext.Manipulator manipulator;
			if ((manipulator = FastHerbGrowth.<>O.<0>__WorldGen_GrowAlch) == null)
			{
				manipulator = (FastHerbGrowth.<>O.<0>__WorldGen_GrowAlch = new ILContext.Manipulator(FastHerbGrowth.WorldGen_GrowAlch));
			}
			IL_WorldGen.GrowAlch -= manipulator;
			On_TileDrawing.hook_IsAlchemyPlantHarvestable hook_IsAlchemyPlantHarvestable;
			if ((hook_IsAlchemyPlantHarvestable = FastHerbGrowth.<>O.<1>__TileDrawing_IsAlchemyPlantHarvestable) == null)
			{
				hook_IsAlchemyPlantHarvestable = (FastHerbGrowth.<>O.<1>__TileDrawing_IsAlchemyPlantHarvestable = new On_TileDrawing.hook_IsAlchemyPlantHarvestable(FastHerbGrowth.TileDrawing_IsAlchemyPlantHarvestable));
			}
			On_TileDrawing.IsAlchemyPlantHarvestable -= hook_IsAlchemyPlantHarvestable;
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x00066B3C File Offset: 0x00064D3C
		public static void WorldGen_GrowAlch(ILContext il)
		{
			ILCursor c = new ILCursor(il);
			ILCursor ilcursor = c;
			MoveType moveType = 2;
			Func<Instruction, bool>[] array = new Func<Instruction, bool>[2];
			array[0] = ((Instruction i) => ILPatternMatchingExt.Match(i, OpCodes.Call));
			array[1] = ((Instruction i) => ILPatternMatchingExt.Match(i, OpCodes.Ldc_I4_S));
			if (!ilcursor.TryGotoNext(moveType, array))
			{
				return;
			}
			c.EmitDelegate<Func<int, int>>(delegate(int num)
			{
				if (!QoLCompendium.mainConfig.FastHerbGrowth)
				{
					return num;
				}
				return 1;
			});
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x00066BCA File Offset: 0x00064DCA
		public static bool TileDrawing_IsAlchemyPlantHarvestable(On_TileDrawing.orig_IsAlchemyPlantHarvestable orig, TileDrawing self, int style)
		{
			return QoLCompendium.mainConfig.FastHerbGrowth || orig.Invoke(self, style);
		}

		// Token: 0x02000542 RID: 1346
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000EDC RID: 3804
			public static ILContext.Manipulator <0>__WorldGen_GrowAlch;

			// Token: 0x04000EDD RID: 3805
			public static On_TileDrawing.hook_IsAlchemyPlantHarvestable <1>__TileDrawing_IsAlchemyPlantHarvestable;
		}
	}
}
