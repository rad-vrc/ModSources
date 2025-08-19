using System;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This serves as the central class from which ModUndergroundBackgroundStyle functions are supported and carried out.
	/// </summary>
	// Token: 0x02000144 RID: 324
	[Autoload(true, Side = ModSide.Client)]
	public class UndergroundBackgroundStylesLoader : SceneEffectLoader<ModUndergroundBackgroundStyle>
	{
		// Token: 0x06001AC9 RID: 6857 RVA: 0x004CD3D0 File Offset: 0x004CB5D0
		public UndergroundBackgroundStylesLoader()
		{
			base.Initialize(22);
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x004CD3E0 File Offset: 0x004CB5E0
		public override void ChooseStyle(out int style, out SceneEffectPriority priority)
		{
			priority = SceneEffectPriority.None;
			style = -1;
			if (!GlobalBackgroundStyleLoader.loaded)
			{
				return;
			}
			int playerUndergroundBackground = Main.LocalPlayer.CurrentSceneEffect.undergroundBackground.value;
			if (playerUndergroundBackground >= base.VanillaCount)
			{
				style = playerUndergroundBackground;
				priority = Main.LocalPlayer.CurrentSceneEffect.undergroundBackground.priority;
			}
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x004CD434 File Offset: 0x004CB634
		public void FillTextureArray(int style, int[] textureSlots)
		{
			if (!GlobalBackgroundStyleLoader.loaded)
			{
				return;
			}
			ModUndergroundBackgroundStyle modUndergroundBackgroundStyle = base.Get(style);
			if (modUndergroundBackgroundStyle != null)
			{
				modUndergroundBackgroundStyle.FillTextureArray(textureSlots);
			}
			Action<int, int[]>[] hookFillUndergroundTextureArray = GlobalBackgroundStyleLoader.HookFillUndergroundTextureArray;
			for (int i = 0; i < hookFillUndergroundTextureArray.Length; i++)
			{
				hookFillUndergroundTextureArray[i](style, textureSlots);
			}
		}

		// Token: 0x0400147A RID: 5242
		public const int VanillaUndergroundBackgroundStylesCount = 22;
	}
}
