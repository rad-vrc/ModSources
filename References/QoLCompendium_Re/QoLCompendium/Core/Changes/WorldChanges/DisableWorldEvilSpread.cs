using System;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.WorldChanges
{
	// Token: 0x02000212 RID: 530
	public class DisableWorldEvilSpread : ModSystem
	{
		// Token: 0x06000CFB RID: 3323 RVA: 0x00064F01 File Offset: 0x00063101
		public override void Load()
		{
			IL_WorldGen.UpdateWorld_Inner += new ILContext.Manipulator(this.WorldGen_UpdateWorld_Inner);
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x00064F14 File Offset: 0x00063114
		public override void Unload()
		{
			IL_WorldGen.UpdateWorld_Inner -= new ILContext.Manipulator(this.WorldGen_UpdateWorld_Inner);
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x00064F28 File Offset: 0x00063128
		private void WorldGen_UpdateWorld_Inner(ILContext il)
		{
			ILCursor ilcursor = new ILCursor(il);
			Func<Instruction, bool>[] array2 = new Func<Instruction, bool>[2];
			array2[0] = ((Instruction i) => ILPatternMatchingExt.MatchLdcI4(i, 3));
			array2[1] = ((Instruction i) => ILPatternMatchingExt.MatchStloc(i, 1));
			Func<Instruction, bool>[] array = array2;
			ilcursor.GotoNext(0, array);
			ilcursor.MoveAfterLabels();
			ilcursor.EmitDelegate<Action>(delegate()
			{
				if (DisableWorldEvilSpread.CorruptionSpreadDisabled)
				{
					WorldGen.AllowedToSpreadInfections = false;
				}
			});
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x00064FBD File Offset: 0x000631BD
		public override void ClearWorld()
		{
			DisableWorldEvilSpread.CorruptionSpreadDisabled = QoLCompendium.mainConfig.DisableEvilBiomeSpread;
		}

		// Token: 0x04000581 RID: 1409
		public static bool CorruptionSpreadDisabled;
	}
}
