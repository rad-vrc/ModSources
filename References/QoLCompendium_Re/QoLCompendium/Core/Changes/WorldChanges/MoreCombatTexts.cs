using System;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.WorldChanges
{
	// Token: 0x02000215 RID: 533
	public class MoreCombatTexts : ModSystem
	{
		// Token: 0x06000D05 RID: 3333 RVA: 0x000651C0 File Offset: 0x000633C0
		public override void Load()
		{
			int maximumTexts = QoLCompendium.mainClientConfig.CombatTextLimit;
			Array.Resize<CombatText>(ref Main.combatText, maximumTexts);
			for (int i = 0; i < maximumTexts; i++)
			{
				Main.combatText[i] = new CombatText();
			}
			On_CombatText.UpdateCombatText += delegate(On_CombatText.orig_UpdateCombatText <p0>)
			{
				for (int j = 0; j < maximumTexts; j++)
				{
					if (Main.combatText[j].active)
					{
						Main.combatText[j].Update();
					}
				}
			};
			On_CombatText.clearAll += delegate(On_CombatText.orig_clearAll <p0>)
			{
				for (int j = 0; j < maximumTexts; j++)
				{
					Main.combatText[j].active = false;
				}
			};
			Func<int, int> <>9__4;
			IL_CombatText.NewText_Rectangle_Color_string_bool_bool += delegate(ILContext il)
			{
				ILCursor val2 = new ILCursor(il);
				for (;;)
				{
					ILCursor ilcursor = val2;
					MoveType moveType = 2;
					Func<Instruction, bool>[] array = new Func<Instruction, bool>[1];
					array[0] = ((Instruction x) => ILPatternMatchingExt.Match<sbyte>(x, OpCodes.Ldc_I4_S, 100));
					if (!ilcursor.TryGotoNext(moveType, array))
					{
						break;
					}
					ILCursor ilcursor2 = val2;
					Func<int, int> func;
					if ((func = <>9__4) == null)
					{
						func = (<>9__4 = ((int _) => maximumTexts));
					}
					ilcursor2.EmitDelegate<Func<int, int>>(func);
				}
			};
			Func<int, int> <>9__6;
			IL_Main.DoDraw += delegate(ILContext il)
			{
				ILCursor val = new ILCursor(il);
				for (;;)
				{
					ILCursor ilcursor = val;
					MoveType moveType = 2;
					Func<Instruction, bool>[] array = new Func<Instruction, bool>[1];
					array[0] = ((Instruction x) => ILPatternMatchingExt.Match<sbyte>(x, OpCodes.Ldc_I4_S, 100));
					if (!ilcursor.TryGotoNext(moveType, array))
					{
						break;
					}
					ILCursor ilcursor2 = val;
					Func<int, int> func;
					if ((func = <>9__6) == null)
					{
						func = (<>9__6 = ((int _) => maximumTexts));
					}
					ilcursor2.EmitDelegate<Func<int, int>>(func);
				}
			};
		}
	}
}
