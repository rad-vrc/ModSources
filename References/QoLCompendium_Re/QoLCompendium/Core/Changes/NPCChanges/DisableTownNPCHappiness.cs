using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using MonoMod.Utils;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.NPCChanges
{
	// Token: 0x02000241 RID: 577
	public class DisableTownNPCHappiness : ModSystem
	{
		// Token: 0x06000DBE RID: 3518 RVA: 0x0006D690 File Offset: 0x0006B890
		public override void Load()
		{
			IL_Condition.cctor += delegate(ILContext il)
			{
				ILCursor c = new ILCursor(il);
				MethodReference method = null;
				ILCursor ilcursor = c;
				Func<Instruction, bool>[] array = new Func<Instruction, bool>[1];
				array[0] = ((Instruction x) => ILPatternMatchingExt.MatchLdstr(x, "Conditions.HappyEnoughForPylons"));
				ilcursor.GotoNext(array);
				c.GotoNext(new Func<Instruction, bool>[]
				{
					(Instruction x) => ILPatternMatchingExt.MatchLdftn(x, ref method)
				});
				MethodBase methodBase = ReflectionHelper.ResolveReflection(method);
				Func<Func<object, bool>, object, bool> func;
				if ((func = DisableTownNPCHappiness.<>O.<0>__IgnoreIfUnhappy) == null)
				{
					func = (DisableTownNPCHappiness.<>O.<0>__IgnoreIfUnhappy = new Func<Func<object, bool>, object, bool>(DisableTownNPCHappiness.IgnoreIfUnhappy));
				}
				this.hook = new Hook(methodBase, func);
				ILCursor ilcursor2 = c;
				Func<Instruction, bool>[] array2 = new Func<Instruction, bool>[1];
				array2[0] = ((Instruction x) => ILPatternMatchingExt.MatchLdstr(x, "Conditions.AnotherTownNPCNearby"));
				ilcursor2.GotoNext(array2);
				c.GotoNext(new Func<Instruction, bool>[]
				{
					(Instruction x) => ILPatternMatchingExt.MatchLdftn(x, ref method)
				});
				MethodBase methodBase2 = ReflectionHelper.ResolveReflection(method);
				Func<Func<object, bool>, object, bool> func2;
				if ((func2 = DisableTownNPCHappiness.<>O.<0>__IgnoreIfUnhappy) == null)
				{
					func2 = (DisableTownNPCHappiness.<>O.<0>__IgnoreIfUnhappy = new Func<Func<object, bool>, object, bool>(DisableTownNPCHappiness.IgnoreIfUnhappy));
				}
				this.hook2 = new Hook(methodBase2, func2);
			};
			IL_Main.DrawNPCChatButtons += delegate(ILContext il)
			{
				ILCursor c = new ILCursor(il);
				ILCursor ilcursor = c;
				MoveType moveType = 2;
				Func<Instruction, bool>[] array = new Func<Instruction, bool>[1];
				array[0] = ((Instruction x) => ILPatternMatchingExt.MatchLdstr(x, "UI.NPCCheckHappiness"));
				if (!ilcursor.TryGotoNext(moveType, array))
				{
					return;
				}
				c.EmitDelegate<Func<string, string>>(delegate(string text)
				{
					if (QoLCompendium.mainConfig.DisableHappiness)
					{
						return "";
					}
					return text;
				});
			};
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x0006D6C7 File Offset: 0x0006B8C7
		public override void Unload()
		{
			Hook hook = this.hook;
			if (hook != null)
			{
				hook.Dispose();
			}
			this.hook = null;
			Hook hook2 = this.hook2;
			if (hook2 != null)
			{
				hook2.Dispose();
			}
			this.hook2 = null;
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x0006D6F9 File Offset: 0x0006B8F9
		private static bool IgnoreIfUnhappy(Func<object, bool> orig, object self)
		{
			return orig(self) || QoLCompendium.mainConfig.OverridePylonSales;
		}

		// Token: 0x040005AB RID: 1451
		private Hook hook;

		// Token: 0x040005AC RID: 1452
		private Hook hook2;

		// Token: 0x02000551 RID: 1361
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000F15 RID: 3861
			public static Func<Func<object, bool>, object, bool> <0>__IgnoreIfUnhappy;
		}
	}
}
