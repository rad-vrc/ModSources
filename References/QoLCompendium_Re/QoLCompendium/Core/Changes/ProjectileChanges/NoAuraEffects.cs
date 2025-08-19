using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.ProjectileChanges
{
	// Token: 0x02000225 RID: 549
	public class NoAuraEffects : ModSystem
	{
		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000D46 RID: 3398 RVA: 0x0006790F File Offset: 0x00065B0F
		public static Asset<Texture2D> EmptyTexture
		{
			get
			{
				return Common.GetAsset("Projectiles", "Invisible", -1);
			}
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x00067921 File Offset: 0x00065B21
		public override void Load()
		{
			Action<GameTime> value;
			if ((value = NoAuraEffects.<>O.<0>__UpdateTextures) == null)
			{
				value = (NoAuraEffects.<>O.<0>__UpdateTextures = new Action<GameTime>(NoAuraEffects.UpdateTextures));
			}
			Main.OnPreDraw += value;
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x00067944 File Offset: 0x00065B44
		public override void Unload()
		{
			if (NoAuraEffects.OrigFlameRing != null && TextureAssets.FlameRing == NoAuraEffects.EmptyTexture)
			{
				TextureAssets.FlameRing = NoAuraEffects.OrigFlameRing;
			}
			if (NoAuraEffects.OrigTesla != null)
			{
				foreach (int Index in NoAuraEffects.OriginalTeslaProjectiles)
				{
					TextureAssets.Projectile[Index] = NoAuraEffects.OrigTesla;
				}
			}
			Action<GameTime> value;
			if ((value = NoAuraEffects.<>O.<0>__UpdateTextures) == null)
			{
				value = (NoAuraEffects.<>O.<0>__UpdateTextures = new Action<GameTime>(NoAuraEffects.UpdateTextures));
			}
			Main.OnPreDraw -= value;
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x000679E0 File Offset: 0x00065BE0
		private static void UpdateTextures(GameTime Time)
		{
			if (!Main.gameMenu && Time.TotalGameTime > NoAuraEffects.NextTextureUpdate)
			{
				NoAuraEffects.NextTextureUpdate = Time.TotalGameTime.Add(TimeSpan.FromSeconds(1.0));
				if (QoLCompendium.mainClientConfig.NoAuraVisuals && TextureAssets.FlameRing != NoAuraEffects.EmptyTexture)
				{
					if (NoAuraEffects.OrigFlameRing == null)
					{
						NoAuraEffects.OrigFlameRing = TextureAssets.FlameRing;
					}
					TextureAssets.FlameRing = NoAuraEffects.EmptyTexture;
				}
				else if (!QoLCompendium.mainClientConfig.NoAuraVisuals && NoAuraEffects.OrigFlameRing != null && TextureAssets.FlameRing != NoAuraEffects.OrigFlameRing)
				{
					TextureAssets.FlameRing = NoAuraEffects.OrigFlameRing;
				}
				if (ModConditions.calamityLoaded)
				{
					FieldInfo field = typeof(AssetRepository).GetField("_assets", BindingFlags.Instance | BindingFlags.NonPublic);
					Dictionary<string, IAsset> Assets = (Dictionary<string, IAsset>)field.GetValue(ModConditions.calamityMod.Assets);
					if (field != null && Assets != null && Assets.ContainsKey("Projectiles\\Typeless\\TeslaAura"))
					{
						if (QoLCompendium.mainClientConfig.NoAuraVisuals && !NoAuraEffects.WasTeslaEnabled)
						{
							if (NoAuraEffects.OrigTesla == null)
							{
								NoAuraEffects.OrigTesla = (Asset<Texture2D>)Assets["Projectiles\\Typeless\\TeslaAura"];
							}
							for (int Index = TextureAssets.Projectile.Length - 1; Index >= 0; Index--)
							{
								if (TextureAssets.Projectile[Index] == NoAuraEffects.OrigTesla)
								{
									NoAuraEffects.WasTeslaEnabled = true;
									if (!NoAuraEffects.OriginalTeslaProjectiles.Contains(Index))
									{
										NoAuraEffects.OriginalTeslaProjectiles.Add(Index);
									}
									TextureAssets.Projectile[Index] = NoAuraEffects.EmptyTexture;
								}
							}
							return;
						}
						if (!QoLCompendium.mainClientConfig.NoAuraVisuals && NoAuraEffects.OrigTesla != null && NoAuraEffects.WasTeslaEnabled)
						{
							NoAuraEffects.WasTeslaEnabled = false;
							foreach (int Index2 in NoAuraEffects.OriginalTeslaProjectiles)
							{
								TextureAssets.Projectile[Index2] = NoAuraEffects.OrigTesla;
							}
						}
					}
				}
			}
		}

		// Token: 0x04000585 RID: 1413
		public const string TeslaTexturePath = "Projectiles\\Typeless\\TeslaAura";

		// Token: 0x04000586 RID: 1414
		private static TimeSpan NextTextureUpdate = TimeSpan.Zero;

		// Token: 0x04000587 RID: 1415
		public static Asset<Texture2D> OrigFlameRing;

		// Token: 0x04000588 RID: 1416
		public static Asset<Texture2D> OrigTesla;

		// Token: 0x04000589 RID: 1417
		public static List<int> OriginalTeslaProjectiles = new List<int>();

		// Token: 0x0400058A RID: 1418
		public static bool WasTeslaEnabled = false;

		// Token: 0x02000546 RID: 1350
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000EEC RID: 3820
			public static Action<GameTime> <0>__UpdateTextures;
		}
	}
}
