using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.GameContent.UI.BigProgressBar;
using Terraria.Localization;
using Terraria.ModLoader.Default;

namespace Terraria.ModLoader
{
	// Token: 0x02000149 RID: 329
	public static class BossBarLoader
	{
		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06001AE6 RID: 6886 RVA: 0x004CDF13 File Offset: 0x004CC113
		public static Asset<Texture2D> VanillaBossBarTexture
		{
			get
			{
				Asset<Texture2D> result;
				if ((result = BossBarLoader.vanillaBossBarTexture) == null)
				{
					result = (BossBarLoader.vanillaBossBarTexture = Main.Assets.Request<Texture2D>("Images/UI/UI_BossBar"));
				}
				return result;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06001AE7 RID: 6887 RVA: 0x004CDF33 File Offset: 0x004CC133
		// (set) Token: 0x06001AE8 RID: 6888 RVA: 0x004CDF3A File Offset: 0x004CC13A
		public static ModBossBarStyle CurrentStyle { get; private set; } = BossBarLoader.vanillaStyle;

		// Token: 0x06001AE9 RID: 6889 RVA: 0x004CDF44 File Offset: 0x004CC144
		internal static void Unload()
		{
			BossBarLoader.drawingInfo = null;
			BossBarLoader.vanillaBossBarTexture = null;
			BossBarLoader.styleLoading = true;
			BossBarLoader.bossBars.Clear();
			BossBarLoader.globalBossBars.Clear();
			List<ModBossBarStyle> obj = BossBarLoader.bossBarStyles;
			lock (obj)
			{
				BossBarLoader.bossBarStyles.RemoveRange(BossBarLoader.defaultStyleCount, BossBarLoader.bossBarStyles.Count - BossBarLoader.defaultStyleCount);
			}
			BossBarLoader.bossBarTextures.Clear();
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x004CDFD4 File Offset: 0x004CC1D4
		internal static void AddBossBar(ModBossBar bossBar)
		{
			bossBar.index = BossBarLoader.bossBars.Count;
			BossBarLoader.bossBars.Add(bossBar);
			ModTypeLookup<ModBossBar>.Register(bossBar);
			Asset<Texture2D> bossBarTexture;
			if (ModContent.RequestIfExists<Texture2D>(bossBar.Texture, out bossBarTexture, 2))
			{
				BossBarLoader.bossBarTextures[bossBar.index] = bossBarTexture;
			}
		}

		// Token: 0x06001AEB RID: 6891 RVA: 0x004CE023 File Offset: 0x004CC223
		internal static void AddGlobalBossBar(GlobalBossBar globalBossBar)
		{
			BossBarLoader.globalBossBars.Add(globalBossBar);
			ModTypeLookup<GlobalBossBar>.Register(globalBossBar);
		}

		// Token: 0x06001AEC RID: 6892 RVA: 0x004CE038 File Offset: 0x004CC238
		internal static void AddBossBarStyle(ModBossBarStyle bossBarStyle)
		{
			List<ModBossBarStyle> obj = BossBarLoader.bossBarStyles;
			lock (obj)
			{
				BossBarLoader.bossBarStyles.Add(bossBarStyle);
				ModTypeLookup<ModBossBarStyle>.Register(bossBarStyle);
			}
		}

		/// <summary>
		/// Sets the pending style that should be switched to
		/// </summary>
		/// <param name="bossBarStyle">Pending boss bar style</param>
		// Token: 0x06001AED RID: 6893 RVA: 0x004CE084 File Offset: 0x004CC284
		internal static void SwitchBossBarStyle(ModBossBarStyle bossBarStyle)
		{
			BossBarLoader.switchToStyle = bossBarStyle;
		}

		/// <summary>
		/// Sets the saved style that should be switched to, handles possibly unloaded/invalid ones and defaults to the vanilla style
		/// </summary>
		// Token: 0x06001AEE RID: 6894 RVA: 0x004CE08C File Offset: 0x004CC28C
		internal static void GotoSavedStyle()
		{
			BossBarLoader.switchToStyle = BossBarLoader.vanillaStyle;
			ModBossBarStyle value;
			if (ModContent.TryFind<ModBossBarStyle>(BossBarLoader.lastSelectedStyle, out value))
			{
				BossBarLoader.switchToStyle = value;
			}
			BossBarLoader.styleLoading = false;
		}

		/// <summary>
		/// Checks if the style was changed and applies it, saves the config if required
		/// </summary>
		// Token: 0x06001AEF RID: 6895 RVA: 0x004CE0C0 File Offset: 0x004CC2C0
		internal static void HandleStyle()
		{
			if (BossBarLoader.switchToStyle != null && BossBarLoader.switchToStyle != BossBarLoader.CurrentStyle)
			{
				BossBarLoader.CurrentStyle.OnDeselected();
				BossBarLoader.CurrentStyle = BossBarLoader.switchToStyle;
				BossBarLoader.CurrentStyle.OnSelected();
			}
			BossBarLoader.switchToStyle = null;
			if (!BossBarLoader.styleLoading && BossBarLoader.CurrentStyle.FullName != BossBarLoader.lastSelectedStyle)
			{
				BossBarLoader.lastSelectedStyle = BossBarLoader.CurrentStyle.FullName;
				Main.SaveSettings();
			}
		}

		/// <summary>
		/// Returns the texture that the given bar is using. If it does not have a custom one, it returns the vanilla texture
		/// </summary>
		/// <param name="bossBar">The ModBossBar</param>
		/// <returns>Its texture, or the vanilla texture</returns>
		// Token: 0x06001AF0 RID: 6896 RVA: 0x004CE138 File Offset: 0x004CC338
		public static Asset<Texture2D> GetTexture(ModBossBar bossBar)
		{
			Asset<Texture2D> texture;
			if (!BossBarLoader.bossBarTextures.TryGetValue(bossBar.index, out texture))
			{
				texture = BossBarLoader.VanillaBossBarTexture;
			}
			return texture;
		}

		/// <summary>
		/// Gets the ModBossBar associated with this NPC
		/// </summary>
		/// <param name="npc">The NPC</param>
		/// <param name="value">When this method returns, contains the ModBossBar associated with the specified NPC</param>
		/// <returns><see langword="true" /> if a ModBossBar is assigned to it; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001AF1 RID: 6897 RVA: 0x004CE160 File Offset: 0x004CC360
		public static bool NpcToBossBar(NPC npc, out ModBossBar value)
		{
			value = null;
			ModBossBar bossBar = npc.BossBar as ModBossBar;
			if (bossBar != null)
			{
				value = bossBar;
			}
			return value != null;
		}

		/// <summary>
		/// Inserts the boss bar style select option into the main and in-game menu under the "Interface" category
		/// </summary>
		// Token: 0x06001AF2 RID: 6898 RVA: 0x004CE188 File Offset: 0x004CC388
		internal static string InsertMenu(out Action onClick)
		{
			string styleText = null;
			ModBossBarStyle pendingBossBarStyle = null;
			foreach (ModBossBarStyle bossBarStyle in BossBarLoader.bossBarStyles)
			{
				if (bossBarStyle == BossBarLoader.CurrentStyle)
				{
					styleText = bossBarStyle.DisplayName;
					break;
				}
				pendingBossBarStyle = bossBarStyle;
			}
			if (pendingBossBarStyle == null)
			{
				pendingBossBarStyle = BossBarLoader.bossBarStyles.Last<ModBossBarStyle>();
			}
			if (styleText == null || BossBarLoader.bossBarStyles.Count == 1)
			{
				styleText = Language.GetTextValue("tModLoader.BossBarStyleNoOptions");
			}
			onClick = delegate()
			{
				BossBarLoader.SwitchBossBarStyle(pendingBossBarStyle);
			};
			return Language.GetTextValue("tModLoader.BossBarStyle", styleText);
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x004CE248 File Offset: 0x004CC448
		public static bool PreDraw(SpriteBatch spriteBatch, BigProgressBarInfo info, ref BossBarDrawParams drawParams)
		{
			int index = info.npcIndexToAimAt;
			if (index < 0 || index > Main.maxNPCs)
			{
				return false;
			}
			NPC npc = Main.npc[index];
			ModBossBar bossBar;
			bool isModded = BossBarLoader.NpcToBossBar(npc, out bossBar);
			if (isModded)
			{
				drawParams.BarTexture = BossBarLoader.GetTexture(bossBar).Value;
			}
			bool modify = true;
			foreach (GlobalBossBar globalBossBar in BossBarLoader.globalBossBars)
			{
				modify &= globalBossBar.PreDraw(spriteBatch, npc, ref drawParams);
			}
			if (modify && isModded)
			{
				modify = bossBar.PreDraw(spriteBatch, npc, ref drawParams);
			}
			return modify;
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x004CE2F4 File Offset: 0x004CC4F4
		public static void PostDraw(SpriteBatch spriteBatch, BigProgressBarInfo info, BossBarDrawParams drawParams)
		{
			int index = info.npcIndexToAimAt;
			if (index < 0 || index > Main.maxNPCs)
			{
				return;
			}
			NPC npc = Main.npc[index];
			ModBossBar bossBar;
			if (BossBarLoader.NpcToBossBar(npc, out bossBar))
			{
				bossBar.PostDraw(spriteBatch, npc, drawParams);
			}
			foreach (GlobalBossBar globalBossBar in BossBarLoader.globalBossBars)
			{
				globalBossBar.PostDraw(spriteBatch, npc, drawParams);
			}
		}

		/// <summary>
		/// Draws a healthbar with fixed barTexture dimensions (516x348) where the effective bar top left starts at 32x24, and is 456x22 big
		/// <para>The icon top left starts at 4x20, and is 26x28 big</para>
		/// <para>Frame 0 contains the frame (outline)</para>
		/// <para>Frame 1 contains the 2 pixel wide strip for the tip of the bar itself</para>
		/// <para>Frame 2 contains the 2 pixel wide strip for the bar itself, stretches out</para>
		/// <para>Frame 3 contains the background</para>
		/// <para>Frame 4 contains the 2 pixel wide strip for the tip of the bar itself (optional shield)</para>
		/// <para>Frame 5 contains the 2 pixel wide strip for the bar itself, stretches out (optional shield)</para>
		/// <para>Supply your own textures if you need a different shape/color, otherwise you can make your own method to draw it</para>
		/// </summary>
		/// <param name="spriteBatch">The spriteBatch that is drawn on</param>
		/// <param name="drawParams">The draw parameters for the boss bar</param>
		// Token: 0x06001AF5 RID: 6901 RVA: 0x004CE370 File Offset: 0x004CC570
		public static void DrawFancyBar_TML(SpriteBatch spriteBatch, BossBarDrawParams drawParams)
		{
			BossBarDrawParams bossBarDrawParams = drawParams;
			Texture2D texture2D;
			Vector2 vector;
			Texture2D texture2D2;
			Rectangle rectangle;
			Color color;
			float num;
			float num2;
			float num3;
			float num4;
			float num5;
			bool flag;
			Vector2 vector2;
			bossBarDrawParams.Deconstruct(out texture2D, out vector, out texture2D2, out rectangle, out color, out num, out num2, out num3, out num4, out num5, out flag, out vector2);
			Texture2D barTexture = texture2D;
			Vector2 center = vector;
			Texture2D iconTexture = texture2D2;
			Rectangle iconFrame = rectangle;
			Color iconColor = color;
			float life = num;
			float lifeMax = num2;
			float shield = num3;
			float shieldMax = num4;
			float iconScale = num5;
			bool showText = flag;
			Vector2 textOffset = vector2;
			Point barSize;
			barSize..ctor(456, 22);
			Point topLeftOffset;
			topLeftOffset..ctor(32, 24);
			int frameCount = 6;
			Rectangle bgFrame = barTexture.Frame(1, frameCount, 0, 3, 0, 0);
			Color bgColor = Color.White * 0.2f;
			int scale = (int)((float)barSize.X * life / lifeMax);
			scale -= scale % 2;
			Rectangle barFrame = barTexture.Frame(1, frameCount, 0, 2, 0, 0);
			barFrame.X += topLeftOffset.X;
			barFrame.Y += topLeftOffset.Y;
			barFrame.Width = 2;
			barFrame.Height = barSize.Y;
			Rectangle tipFrame = barTexture.Frame(1, frameCount, 0, 1, 0, 0);
			tipFrame.X += topLeftOffset.X;
			tipFrame.Y += topLeftOffset.Y;
			tipFrame.Width = 2;
			tipFrame.Height = barSize.Y;
			int shieldScale = (int)((float)barSize.X * shield / shieldMax);
			shieldScale -= shieldScale % 2;
			Rectangle barShieldFrame = barTexture.Frame(1, frameCount, 0, 5, 0, 0);
			barShieldFrame.X += topLeftOffset.X;
			barShieldFrame.Y += topLeftOffset.Y;
			barShieldFrame.Width = 2;
			barShieldFrame.Height = barSize.Y;
			Rectangle tipShieldFrame = barTexture.Frame(1, frameCount, 0, 4, 0, 0);
			tipShieldFrame.X += topLeftOffset.X;
			tipShieldFrame.Y += topLeftOffset.Y;
			tipShieldFrame.Width = 2;
			tipShieldFrame.Height = barSize.Y;
			Rectangle barPosition = Utils.CenteredRectangle(center, barSize.ToVector2());
			Vector2 barTopLeft = barPosition.TopLeft();
			Vector2 topLeft = barTopLeft - topLeftOffset.ToVector2();
			spriteBatch.Draw(barTexture, topLeft, new Rectangle?(bgFrame), bgColor, 0f, Vector2.Zero, 1f, 0, 0f);
			Vector2 stretchScale;
			stretchScale..ctor((float)(scale / barFrame.Width), 1f);
			Color barColor = Color.White;
			spriteBatch.Draw(barTexture, barTopLeft, new Rectangle?(barFrame), barColor, 0f, Vector2.Zero, stretchScale, 0, 0f);
			spriteBatch.Draw(barTexture, barTopLeft + new Vector2((float)(scale - 2), 0f), new Rectangle?(tipFrame), barColor, 0f, Vector2.Zero, 1f, 0, 0f);
			if (shield > 0f)
			{
				stretchScale..ctor((float)(shieldScale / barFrame.Width), 1f);
				spriteBatch.Draw(barTexture, barTopLeft, new Rectangle?(barShieldFrame), barColor, 0f, Vector2.Zero, stretchScale, 0, 0f);
				spriteBatch.Draw(barTexture, barTopLeft + new Vector2((float)(shieldScale - 2), 0f), new Rectangle?(tipShieldFrame), barColor, 0f, Vector2.Zero, 1f, 0, 0f);
			}
			Rectangle frameFrame = barTexture.Frame(1, frameCount, 0, 0, 0, 0);
			spriteBatch.Draw(barTexture, topLeft, new Rectangle?(frameFrame), Color.White, 0f, Vector2.Zero, 1f, 0, 0f);
			Vector2 vector3 = new Vector2(4f, 20f);
			Vector2 iconSize;
			iconSize..ctor(26f, 28f);
			Vector2 iconPos = vector3 + iconSize / 2f;
			spriteBatch.Draw(iconTexture, topLeft + iconPos, new Rectangle?(iconFrame), iconColor, 0f, iconFrame.Size() / 2f, iconScale, 0, 0f);
			if (BigProgressBarSystem.ShowText && showText)
			{
				if (shield > 0f)
				{
					BigProgressBarHelper.DrawHealthText(spriteBatch, barPosition, textOffset, shield, shieldMax);
					return;
				}
				BigProgressBarHelper.DrawHealthText(spriteBatch, barPosition, textOffset, life, lifeMax);
			}
		}

		/// <summary>
		/// Set to the current info that is being drawn just before any registered bar draws through the vanilla system (modded and vanilla), reset in the method used to draw it.
		/// <para>Allows tML to short-circuit the draw method and make ModBossBar and GlobalBossBar modify the draw parameters. Is null if a ModBossBarStyle skips drawing</para>
		/// </summary>
		// Token: 0x04001484 RID: 5252
		internal static BigProgressBarInfo? drawingInfo = null;

		// Token: 0x04001485 RID: 5253
		private static Asset<Texture2D> vanillaBossBarTexture;

		/// <summary>
		/// Used to prevent switching to the default style while the mods are loading (The code responsible for it runs in the main menu too)
		/// </summary>
		// Token: 0x04001486 RID: 5254
		private static bool styleLoading = true;

		// Token: 0x04001487 RID: 5255
		internal static readonly ModBossBarStyle vanillaStyle = new DefaultBossBarStyle();

		// Token: 0x04001488 RID: 5256
		private static ModBossBarStyle switchToStyle = null;

		/// <summary>
		/// The string that is saved in the config
		/// </summary>
		// Token: 0x0400148A RID: 5258
		internal static string lastSelectedStyle = BossBarLoader.CurrentStyle.FullName;

		// Token: 0x0400148B RID: 5259
		internal static readonly IList<ModBossBar> bossBars = new List<ModBossBar>();

		// Token: 0x0400148C RID: 5260
		internal static readonly IList<GlobalBossBar> globalBossBars = new List<GlobalBossBar>();

		// Token: 0x0400148D RID: 5261
		internal static readonly List<ModBossBarStyle> bossBarStyles = new List<ModBossBarStyle>
		{
			BossBarLoader.vanillaStyle
		};

		// Token: 0x0400148E RID: 5262
		internal static readonly int defaultStyleCount = BossBarLoader.bossBarStyles.Count;

		/// <summary>
		/// Only contains textures that exist.
		/// </summary>
		// Token: 0x0400148F RID: 5263
		internal static readonly Dictionary<int, Asset<Texture2D>> bossBarTextures = new Dictionary<int, Asset<Texture2D>>();
	}
}
