using System;
using System.Collections.Generic;

namespace Terraria.ModLoader
{
	// Token: 0x02000146 RID: 326
	internal static class GlobalBackgroundStyleLoader
	{
		// Token: 0x06001AD4 RID: 6868 RVA: 0x004CDA68 File Offset: 0x004CBC68
		internal unsafe static void ResizeAndFillArrays(bool unloading = false)
		{
			ModLoader.BuildGlobalHook<GlobalBackgroundStyle, GlobalBackgroundStyleLoader.DelegateChooseUndergroundBackgroundStyle>(ref GlobalBackgroundStyleLoader.HookChooseUndergroundBackgroundStyle, GlobalBackgroundStyleLoader.globalBackgroundStyles, (GlobalBackgroundStyle g) => (GlobalBackgroundStyleLoader.DelegateChooseUndergroundBackgroundStyle)methodof(GlobalBackgroundStyle.ChooseUndergroundBackgroundStyle(int*)).CreateDelegate(typeof(GlobalBackgroundStyleLoader.DelegateChooseUndergroundBackgroundStyle), g));
			ModLoader.BuildGlobalHook<GlobalBackgroundStyle, GlobalBackgroundStyleLoader.DelegateChooseSurfaceBackgroundStyle>(ref GlobalBackgroundStyleLoader.HookChooseSurfaceBackgroundStyle, GlobalBackgroundStyleLoader.globalBackgroundStyles, (GlobalBackgroundStyle g) => (GlobalBackgroundStyleLoader.DelegateChooseSurfaceBackgroundStyle)methodof(GlobalBackgroundStyle.ChooseSurfaceBackgroundStyle(int*)).CreateDelegate(typeof(GlobalBackgroundStyleLoader.DelegateChooseSurfaceBackgroundStyle), g));
			ModLoader.BuildGlobalHook<GlobalBackgroundStyle, Action<int, int[]>>(ref GlobalBackgroundStyleLoader.HookFillUndergroundTextureArray, GlobalBackgroundStyleLoader.globalBackgroundStyles, (GlobalBackgroundStyle g) => (Action<int, int[]>)methodof(GlobalBackgroundStyle.FillUndergroundTextureArray(int, int[])).CreateDelegate(typeof(Action<int, int[]>), g));
			ModLoader.BuildGlobalHook<GlobalBackgroundStyle, Action<int, float[], float>>(ref GlobalBackgroundStyleLoader.HookModifyFarSurfaceFades, GlobalBackgroundStyleLoader.globalBackgroundStyles, (GlobalBackgroundStyle g) => (Action<int, float[], float>)methodof(GlobalBackgroundStyle.ModifyFarSurfaceFades(int, float[], float)).CreateDelegate(typeof(Action<int, float[], float>), g));
			if (!unloading)
			{
				GlobalBackgroundStyleLoader.loaded = true;
			}
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x004CDCE6 File Offset: 0x004CBEE6
		internal static void Unload()
		{
			GlobalBackgroundStyleLoader.loaded = false;
			GlobalBackgroundStyleLoader.globalBackgroundStyles.Clear();
		}

		// Token: 0x0400147C RID: 5244
		internal static readonly IList<GlobalBackgroundStyle> globalBackgroundStyles = new List<GlobalBackgroundStyle>();

		// Token: 0x0400147D RID: 5245
		internal static bool loaded = false;

		// Token: 0x0400147E RID: 5246
		internal static GlobalBackgroundStyleLoader.DelegateChooseUndergroundBackgroundStyle[] HookChooseUndergroundBackgroundStyle;

		// Token: 0x0400147F RID: 5247
		internal static GlobalBackgroundStyleLoader.DelegateChooseSurfaceBackgroundStyle[] HookChooseSurfaceBackgroundStyle;

		// Token: 0x04001480 RID: 5248
		internal static Action<int, int[]>[] HookFillUndergroundTextureArray;

		// Token: 0x04001481 RID: 5249
		internal static Action<int, float[], float>[] HookModifyFarSurfaceFades;

		// Token: 0x020008AC RID: 2220
		// (Invoke) Token: 0x0600521D RID: 21021
		internal delegate void DelegateChooseUndergroundBackgroundStyle(ref int style);

		// Token: 0x020008AD RID: 2221
		// (Invoke) Token: 0x06005221 RID: 21025
		internal delegate void DelegateChooseSurfaceBackgroundStyle(ref int style);
	}
}
