using System;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.NPCChanges
{
	// Token: 0x02000240 RID: 576
	public class TownNPCsCanLiveInEvil : ModSystem
	{
		// Token: 0x06000DB9 RID: 3513 RVA: 0x0006D558 File Offset: 0x0006B758
		public override bool IsLoadingEnabled(Mod mod)
		{
			Mod VanillaQoL;
			ModLoader.TryGetMod("VanillaQoL", out VanillaQoL);
			return VanillaQoL == null;
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x0006D576 File Offset: 0x0006B776
		public override void Load()
		{
			IL_WorldGen.ScoreRoom += new ILContext.Manipulator(this.LiveInCorrupt);
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x0006D589 File Offset: 0x0006B789
		public override void Unload()
		{
			IL_WorldGen.ScoreRoom -= new ILContext.Manipulator(this.LiveInCorrupt);
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x0006D59C File Offset: 0x0006B79C
		private void LiveInCorrupt(ILContext il)
		{
			ILCursor c = new ILCursor(il);
			ILCursor ilcursor = c;
			MoveType moveType = 2;
			Func<Instruction, bool>[] array = new Func<Instruction, bool>[5];
			array[0] = ((Instruction i) => ILPatternMatchingExt.MatchCall(i, typeof(WorldGen).GetMethod("GetTileTypeCountByCategory")));
			array[1] = ((Instruction i) => ILPatternMatchingExt.Match(i, OpCodes.Neg));
			array[2] = ((Instruction i) => ILPatternMatchingExt.Match(i, OpCodes.Stloc_S));
			array[3] = ((Instruction i) => ILPatternMatchingExt.Match(i, OpCodes.Ldloc_S));
			array[4] = ((Instruction i) => ILPatternMatchingExt.Match(i, OpCodes.Ldc_I4_S));
			if (!ilcursor.TryGotoNext(moveType, array))
			{
				return;
			}
			c.EmitDelegate<Func<int, int>>(delegate(int returnValue)
			{
				if (!QoLCompendium.mainConfig.TownNPCsLiveInEvil)
				{
					return returnValue;
				}
				return 114514;
			});
		}
	}
}
