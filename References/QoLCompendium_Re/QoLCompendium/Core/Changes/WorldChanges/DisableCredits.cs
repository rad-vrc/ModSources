using System;
using System.Runtime.CompilerServices;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.WorldChanges
{
	// Token: 0x02000210 RID: 528
	public class DisableCredits : ModSystem
	{
		// Token: 0x06000CF2 RID: 3314 RVA: 0x00064D50 File Offset: 0x00062F50
		public override bool IsLoadingEnabled(Mod mod)
		{
			Mod VanillaQoL;
			ModLoader.TryGetMod("VanillaQoL", out VanillaQoL);
			return VanillaQoL == null;
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x00064D6E File Offset: 0x00062F6E
		public override void Load()
		{
			ILContext.Manipulator manipulator;
			if ((manipulator = DisableCredits.<>O.<0>__NoCredits) == null)
			{
				manipulator = (DisableCredits.<>O.<0>__NoCredits = new ILContext.Manipulator(DisableCredits.NoCredits));
			}
			IL_NPC.OnGameEventClearedForTheFirstTime += manipulator;
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x00064D90 File Offset: 0x00062F90
		public override void Unload()
		{
			ILContext.Manipulator manipulator;
			if ((manipulator = DisableCredits.<>O.<0>__NoCredits) == null)
			{
				manipulator = (DisableCredits.<>O.<0>__NoCredits = new ILContext.Manipulator(DisableCredits.NoCredits));
			}
			IL_NPC.OnGameEventClearedForTheFirstTime -= manipulator;
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x00064DB4 File Offset: 0x00062FB4
		public static void NoCredits(ILContext il)
		{
			ILCursor c = new ILCursor(il);
			ILCursor ilcursor = c;
			MoveType moveType = 0;
			Func<Instruction, bool>[] array = new Func<Instruction, bool>[1];
			array[0] = ((Instruction i) => ILPatternMatchingExt.MatchCall<CreditsRollEvent>(i, "TryStartingCreditsRoll"));
			ilcursor.GotoNext(moveType, array);
			c.Remove();
		}

		// Token: 0x0200053B RID: 1339
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000EC3 RID: 3779
			public static ILContext.Manipulator <0>__NoCredits;
		}
	}
}
