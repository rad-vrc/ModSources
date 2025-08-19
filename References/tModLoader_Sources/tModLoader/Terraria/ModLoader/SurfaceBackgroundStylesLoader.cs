using System;
using Microsoft.Xna.Framework;
using Terraria.GameContent;
using Terraria.Graphics.Effects;

namespace Terraria.ModLoader
{
	// Token: 0x02000145 RID: 325
	[Autoload(true, Side = ModSide.Client)]
	public class SurfaceBackgroundStylesLoader : SceneEffectLoader<ModSurfaceBackgroundStyle>
	{
		// Token: 0x06001ACC RID: 6860 RVA: 0x004CD47A File Offset: 0x004CB67A
		public SurfaceBackgroundStylesLoader()
		{
			base.Initialize(14);
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x004CD48A File Offset: 0x004CB68A
		internal override void ResizeArrays()
		{
			Array.Resize<float>(ref Main.bgAlphaFrontLayer, base.TotalCount);
			Array.Resize<float>(ref Main.bgAlphaFarBackLayer, base.TotalCount);
			SurfaceBackgroundStylesLoader.loaded = true;
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x004CD4B2 File Offset: 0x004CB6B2
		internal override void Unload()
		{
			base.Unload();
			SurfaceBackgroundStylesLoader.loaded = false;
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x004CD4C0 File Offset: 0x004CB6C0
		public override void ChooseStyle(out int style, out SceneEffectPriority priority)
		{
			priority = SceneEffectPriority.None;
			style = -1;
			if (!SurfaceBackgroundStylesLoader.loaded || !GlobalBackgroundStyleLoader.loaded)
			{
				return;
			}
			int playerSurfaceBackground = Main.LocalPlayer.CurrentSceneEffect.surfaceBackground.value;
			if (playerSurfaceBackground >= base.VanillaCount)
			{
				style = playerSurfaceBackground;
				priority = Main.LocalPlayer.CurrentSceneEffect.surfaceBackground.priority;
			}
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x004CD51C File Offset: 0x004CB71C
		public void ModifyFarFades(int style, float[] fades, float transitionSpeed)
		{
			if (!GlobalBackgroundStyleLoader.loaded)
			{
				return;
			}
			ModSurfaceBackgroundStyle modSurfaceBackgroundStyle = base.Get(style);
			if (modSurfaceBackgroundStyle != null)
			{
				modSurfaceBackgroundStyle.ModifyFarFades(fades, transitionSpeed);
			}
			Action<int, float[], float>[] hookModifyFarSurfaceFades = GlobalBackgroundStyleLoader.HookModifyFarSurfaceFades;
			for (int i = 0; i < hookModifyFarSurfaceFades.Length; i++)
			{
				hookModifyFarSurfaceFades[i](style, fades, transitionSpeed);
			}
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x004CD564 File Offset: 0x004CB764
		public void DrawFarTexture()
		{
			if (!GlobalBackgroundStyleLoader.loaded || MenuLoader.loading)
			{
				return;
			}
			if (base.TotalCount != Main.bgAlphaFarBackLayer.Length)
			{
				return;
			}
			foreach (ModSurfaceBackgroundStyle style in this.list)
			{
				int slot = style.Slot;
				float alpha = Main.bgAlphaFarBackLayer[slot];
				Main.ColorOfSurfaceBackgroundsModified = Main.ColorOfSurfaceBackgroundsBase * alpha;
				if (alpha > 0f)
				{
					int textureSlot = style.ChooseFarTexture();
					if (textureSlot >= 0 && textureSlot < TextureAssets.Background.Length)
					{
						Main.instance.LoadBackground(textureSlot);
						for (int i = 0; i < Main.instance.bgLoops; i++)
						{
							Main.spriteBatch.Draw(TextureAssets.Background[textureSlot].Value, new Vector2((float)(Main.instance.bgStartX + Main.bgWidthScaled * i), (float)Main.instance.bgTopY), new Rectangle?(new Rectangle(0, 0, Main.backgroundWidth[textureSlot], Main.backgroundHeight[textureSlot])), Main.ColorOfSurfaceBackgroundsModified, 0f, default(Vector2), Main.bgScale, 0, 0f);
						}
					}
				}
			}
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x004CD6C8 File Offset: 0x004CB8C8
		public void DrawMiddleTexture()
		{
			if (!GlobalBackgroundStyleLoader.loaded || MenuLoader.loading)
			{
				return;
			}
			foreach (ModSurfaceBackgroundStyle style in this.list)
			{
				int slot = style.Slot;
				float alpha = Main.bgAlphaFarBackLayer[slot];
				Main.ColorOfSurfaceBackgroundsModified = Main.ColorOfSurfaceBackgroundsBase * alpha;
				if (alpha > 0f)
				{
					int textureSlot = style.ChooseMiddleTexture();
					if (textureSlot >= 0 && textureSlot < TextureAssets.Background.Length)
					{
						Main.instance.LoadBackground(textureSlot);
						for (int i = 0; i < Main.instance.bgLoops; i++)
						{
							Main.spriteBatch.Draw(TextureAssets.Background[textureSlot].Value, new Vector2((float)(Main.instance.bgStartX + Main.bgWidthScaled * i), (float)Main.instance.bgTopY), new Rectangle?(new Rectangle(0, 0, Main.backgroundWidth[textureSlot], Main.backgroundHeight[textureSlot])), Main.ColorOfSurfaceBackgroundsModified, 0f, default(Vector2), Main.bgScale, 0, 0f);
						}
					}
				}
			}
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x004CD81C File Offset: 0x004CBA1C
		public void DrawCloseBackground(int style)
		{
			if (!GlobalBackgroundStyleLoader.loaded || MenuLoader.loading)
			{
				return;
			}
			if (Main.bgAlphaFrontLayer[style] <= 0f)
			{
				return;
			}
			ModSurfaceBackgroundStyle surfaceBackgroundStyle = base.Get(style);
			if (surfaceBackgroundStyle == null || !surfaceBackgroundStyle.PreDrawCloseBackground(Main.spriteBatch))
			{
				return;
			}
			Main.bgScale = 1.25f;
			Main.instance.bgParallax = 0.37;
			float a = 1800f;
			float b = 1750f;
			int textureSlot = surfaceBackgroundStyle.ChooseCloseTexture(ref Main.bgScale, ref Main.instance.bgParallax, ref a, ref b);
			if (textureSlot < 0 || textureSlot >= TextureAssets.Background.Length)
			{
				return;
			}
			Main.instance.LoadBackground(textureSlot);
			Main.bgScale *= 2f;
			Main.bgWidthScaled = (int)((float)Main.backgroundWidth[textureSlot] * Main.bgScale);
			SkyManager.Instance.DrawToDepth(Main.spriteBatch, 1f / (float)Main.instance.bgParallax);
			Main.instance.bgStartX = (int)(-Math.IEEERemainder((double)Main.screenPosition.X * Main.instance.bgParallax, (double)Main.bgWidthScaled) - (double)(Main.bgWidthScaled / 2));
			Main.instance.bgTopY = (int)((double)(-(double)Main.screenPosition.Y + Main.instance.screenOff / 2f) / (Main.worldSurface * 16.0) * (double)a + (double)b) + (int)Main.instance.scAdj;
			if (Main.gameMenu)
			{
				Main.instance.bgTopY = 320;
			}
			Main.instance.bgLoops = Main.screenWidth / Main.bgWidthScaled + 2;
			if ((double)Main.screenPosition.Y < Main.worldSurface * 16.0 + 16.0)
			{
				for (int i = 0; i < Main.instance.bgLoops; i++)
				{
					Main.spriteBatch.Draw(TextureAssets.Background[textureSlot].Value, new Vector2((float)(Main.instance.bgStartX + Main.bgWidthScaled * i), (float)Main.instance.bgTopY), new Rectangle?(new Rectangle(0, 0, Main.backgroundWidth[textureSlot], Main.backgroundHeight[textureSlot])), Main.ColorOfSurfaceBackgroundsModified, 0f, default(Vector2), Main.bgScale, 0, 0f);
				}
			}
		}

		// Token: 0x0400147B RID: 5243
		internal static bool loaded;
	}
}
