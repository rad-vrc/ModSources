using System;
using System.Reflection;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria.ModLoader;

namespace Coyoteframes
{
	// Token: 0x02000005 RID: 5
	public class LuneWalPatch : ILoadable
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000021F0 File Offset: 0x000003F0
		private Mod LuneWoL
		{
			get
			{
				Mod lune;
				if (!ModLoader.TryGetMod("LuneWoL", out lune))
				{
					return null;
				}
				return lune;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000220E File Offset: 0x0000040E
		public bool IsLoadingEnabled(Mod mod)
		{
			return this.LuneWoL != null;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002219 File Offset: 0x00000419
		public void Load(Mod mod)
		{
			MonoModHooks.Modify(this.LuneWoL.Code.GetType("LuneWoL.LuneWoL").GetMethod("Load", BindingFlags.Instance | BindingFlags.Public), new ILContext.Manipulator(this.Callback));
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000224D File Offset: 0x0000044D
		public void Unload()
		{
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002250 File Offset: 0x00000450
		private void Callback(ILContext il)
		{
			ILCursor cursor = new ILCursor(il);
			ILCursor ilcursor = cursor;
			MoveType moveType = 0;
			Func<Instruction, bool>[] array = new Func<Instruction, bool>[1];
			array[0] = ((Instruction i) => ILPatternMatchingExt.MatchLdstr(i, "disable coyote frames mod... skill issue if you need that shit!!!! this cannot be turned off because i just dont feel like it LMFAO\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n"));
			ilcursor.TryGotoNext(moveType, array);
			cursor.RemoveRange(3);
		}
	}
}
