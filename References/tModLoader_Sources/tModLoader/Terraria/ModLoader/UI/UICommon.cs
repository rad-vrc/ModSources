using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Terraria.ModLoader.UI
{
	// Token: 0x02000240 RID: 576
	public static class UICommon
	{
		// Token: 0x06002913 RID: 10515 RVA: 0x00511140 File Offset: 0x0050F340
		public static T WithFadedMouseOver<T>(this T elem, Color overColor = default(Color), Color outColor = default(Color), Color overBorderColor = default(Color), Color outBorderColor = default(Color)) where T : UIPanel
		{
			if (overColor == default(Color))
			{
				overColor = UICommon.DefaultUIBlue;
			}
			if (outColor == default(Color))
			{
				outColor = UICommon.DefaultUIBlueMouseOver;
			}
			if (overBorderColor == default(Color))
			{
				overBorderColor = UICommon.DefaultUIBorderMouseOver;
			}
			if (outBorderColor == default(Color))
			{
				outBorderColor = UICommon.DefaultUIBorder;
			}
			elem.OnMouseOver += delegate(UIMouseEvent evt, UIElement _)
			{
				SoundEngine.PlaySound(SoundID.MenuTick, null, null);
				elem.BackgroundColor = overColor;
				elem.BorderColor = overBorderColor;
			};
			elem.OnMouseOut += delegate(UIMouseEvent evt, UIElement _)
			{
				elem.BackgroundColor = outColor;
				elem.BorderColor = outBorderColor;
			};
			return elem;
		}

		// Token: 0x06002914 RID: 10516 RVA: 0x00511239 File Offset: 0x0050F439
		public static T WithPadding<T>(this T elem, float pixels) where T : UIElement
		{
			elem.SetPadding(pixels);
			return elem;
		}

		// Token: 0x06002915 RID: 10517 RVA: 0x00511248 File Offset: 0x0050F448
		public static T WithPadding<T>(this T elem, string name, int id, Vector2? anchor = null, Vector2? offset = null) where T : UIElement
		{
			elem.SetSnapPoint(name, id, anchor, offset);
			return elem;
		}

		// Token: 0x06002916 RID: 10518 RVA: 0x0051125B File Offset: 0x0050F45B
		public static T WithView<T>(this T elem, float viewSize, float maxViewSize) where T : UIScrollbar
		{
			elem.SetView(viewSize, maxViewSize);
			return elem;
		}

		// Token: 0x06002917 RID: 10519 RVA: 0x0051126B File Offset: 0x0050F46B
		public static void AddOrRemoveChild(this UIElement elem, UIElement child, bool add)
		{
			if (!add)
			{
				elem.RemoveChild(child);
				return;
			}
			if (!elem.HasChild(child))
			{
				elem.Append(child);
			}
		}

		// Token: 0x06002918 RID: 10520 RVA: 0x00511288 File Offset: 0x0050F488
		public static void DrawHoverStringInBounds(SpriteBatch spriteBatch, string text, Rectangle? bounds = null)
		{
			if (bounds == null)
			{
				bounds = new Rectangle?(new Rectangle(0, 0, Main.screenWidth, Main.screenHeight));
			}
			Vector2 stringSize = ChatManager.GetStringSize(FontAssets.MouseText.Value, text, Vector2.One, -1f);
			Vector2 vector = Main.MouseScreen + new Vector2(16f);
			vector.X = Math.Min(vector.X, (float)bounds.Value.Right - stringSize.X - 16f);
			vector.Y = Math.Min(vector.Y, (float)bounds.Value.Bottom - stringSize.Y - 16f);
			Color color;
			color..ctor((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, 255);
			ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.MouseText.Value, text, vector, color, 0f, Vector2.Zero, Vector2.One, -1f, 2f);
		}

		/// <summary>
		/// Draws a tooltip on the mouse cursor. Functions like <see cref="M:Terraria.Main.MouseText(System.String,System.Int32,System.Byte,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32)" /> and <see cref="F:Terraria.Main.hoverItemName" />, but adds the same background seen in item tooltips behind the text.
		/// </summary>
		/// <param name="text"></param>
		// Token: 0x06002919 RID: 10521 RVA: 0x0051138C File Offset: 0x0050F58C
		public static void TooltipMouseText(string text)
		{
			if (Main.SettingsEnabled_OpaqueBoxBehindTooltips)
			{
				Item item = new Item();
				item.SetDefaults(0, true, null);
				item.SetNameOverride(text);
				item.type = 1;
				item.scale = 0f;
				item.rare = 0;
				item.value = -1;
				Main.HoverItem = item;
				Main.instance.MouseText("", 0, 0, -1, -1, -1, -1, 0);
				Main.mouseText = true;
				return;
			}
			Main.instance.MouseText(text, 0, 0, -1, -1, -1, -1, 0);
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x0600291A RID: 10522 RVA: 0x00511409 File Offset: 0x0050F609
		// (set) Token: 0x0600291B RID: 10523 RVA: 0x00511410 File Offset: 0x0050F610
		public static Asset<Texture2D> ButtonErrorTexture { get; internal set; }

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x0600291C RID: 10524 RVA: 0x00511418 File Offset: 0x0050F618
		// (set) Token: 0x0600291D RID: 10525 RVA: 0x0051141F File Offset: 0x0050F61F
		public static Asset<Texture2D> ButtonConfigTexture { get; internal set; }

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x0600291E RID: 10526 RVA: 0x00511427 File Offset: 0x0050F627
		// (set) Token: 0x0600291F RID: 10527 RVA: 0x0051142E File Offset: 0x0050F62E
		public static Asset<Texture2D> ButtonPlusTexture { get; internal set; }

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06002920 RID: 10528 RVA: 0x00511436 File Offset: 0x0050F636
		// (set) Token: 0x06002921 RID: 10529 RVA: 0x0051143D File Offset: 0x0050F63D
		public static Asset<Texture2D> ButtonUpDownTexture { get; internal set; }

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06002922 RID: 10530 RVA: 0x00511445 File Offset: 0x0050F645
		// (set) Token: 0x06002923 RID: 10531 RVA: 0x0051144C File Offset: 0x0050F64C
		public static Asset<Texture2D> ButtonCollapsedTexture { get; internal set; }

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06002924 RID: 10532 RVA: 0x00511454 File Offset: 0x0050F654
		// (set) Token: 0x06002925 RID: 10533 RVA: 0x0051145B File Offset: 0x0050F65B
		public static Asset<Texture2D> ButtonExpandedTexture { get; internal set; }

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06002926 RID: 10534 RVA: 0x00511463 File Offset: 0x0050F663
		// (set) Token: 0x06002927 RID: 10535 RVA: 0x0051146A File Offset: 0x0050F66A
		public static Asset<Texture2D> ModBrowserIconsTexture { get; internal set; }

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06002928 RID: 10536 RVA: 0x00511472 File Offset: 0x0050F672
		// (set) Token: 0x06002929 RID: 10537 RVA: 0x00511479 File Offset: 0x0050F679
		public static Asset<Texture2D> ConfigSideIndicatorTexture { get; internal set; }

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x0600292A RID: 10538 RVA: 0x00511481 File Offset: 0x0050F681
		// (set) Token: 0x0600292B RID: 10539 RVA: 0x00511488 File Offset: 0x0050F688
		public static Asset<Texture2D> ButtonExclamationTexture { get; internal set; }

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x0600292C RID: 10540 RVA: 0x00511490 File Offset: 0x0050F690
		// (set) Token: 0x0600292D RID: 10541 RVA: 0x00511497 File Offset: 0x0050F697
		public static Asset<Texture2D> ButtonDepsTexture { get; internal set; }

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x0600292E RID: 10542 RVA: 0x0051149F File Offset: 0x0050F69F
		// (set) Token: 0x0600292F RID: 10543 RVA: 0x005114A6 File Offset: 0x0050F6A6
		public static Asset<Texture2D> ButtonUpgradeCsproj { get; internal set; }

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06002930 RID: 10544 RVA: 0x005114AE File Offset: 0x0050F6AE
		// (set) Token: 0x06002931 RID: 10545 RVA: 0x005114B5 File Offset: 0x0050F6B5
		public static Asset<Texture2D> ButtonUpgradeLang { get; internal set; }

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06002932 RID: 10546 RVA: 0x005114BD File Offset: 0x0050F6BD
		// (set) Token: 0x06002933 RID: 10547 RVA: 0x005114C4 File Offset: 0x0050F6C4
		public static Asset<Texture2D> ButtonRunTModPorter { get; internal set; }

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06002934 RID: 10548 RVA: 0x005114CC File Offset: 0x0050F6CC
		// (set) Token: 0x06002935 RID: 10549 RVA: 0x005114D3 File Offset: 0x0050F6D3
		public static Asset<Texture2D> ButtonOpenFolder { get; internal set; }

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06002936 RID: 10550 RVA: 0x005114DB File Offset: 0x0050F6DB
		// (set) Token: 0x06002937 RID: 10551 RVA: 0x005114E2 File Offset: 0x0050F6E2
		public static Asset<Texture2D> ButtonOpenFolderCustom { get; internal set; }

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06002938 RID: 10552 RVA: 0x005114EA File Offset: 0x0050F6EA
		// (set) Token: 0x06002939 RID: 10553 RVA: 0x005114F1 File Offset: 0x0050F6F1
		public static Asset<Texture2D> ButtonTranslationModTexture { get; internal set; }

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x0600293A RID: 10554 RVA: 0x005114F9 File Offset: 0x0050F6F9
		// (set) Token: 0x0600293B RID: 10555 RVA: 0x00511500 File Offset: 0x0050F700
		public static Asset<Texture2D> LoaderTexture { get; internal set; }

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x0600293C RID: 10556 RVA: 0x00511508 File Offset: 0x0050F708
		// (set) Token: 0x0600293D RID: 10557 RVA: 0x0051150F File Offset: 0x0050F70F
		public static Asset<Texture2D> LoaderBgTexture { get; internal set; }

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x0600293E RID: 10558 RVA: 0x00511517 File Offset: 0x0050F717
		// (set) Token: 0x0600293F RID: 10559 RVA: 0x0051151E File Offset: 0x0050F71E
		public static Asset<Texture2D> ButtonDownloadTexture { get; internal set; }

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06002940 RID: 10560 RVA: 0x00511526 File Offset: 0x0050F726
		// (set) Token: 0x06002941 RID: 10561 RVA: 0x0051152D File Offset: 0x0050F72D
		public static Asset<Texture2D> ButtonDowngradeTexture { get; internal set; }

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06002942 RID: 10562 RVA: 0x00511535 File Offset: 0x0050F735
		// (set) Token: 0x06002943 RID: 10563 RVA: 0x0051153C File Offset: 0x0050F73C
		public static Asset<Texture2D> ButtonDownloadMultipleTexture { get; internal set; }

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06002944 RID: 10564 RVA: 0x00511544 File Offset: 0x0050F744
		// (set) Token: 0x06002945 RID: 10565 RVA: 0x0051154B File Offset: 0x0050F74B
		public static Asset<Texture2D> ButtonModInfoTexture { get; internal set; }

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06002946 RID: 10566 RVA: 0x00511553 File Offset: 0x0050F753
		// (set) Token: 0x06002947 RID: 10567 RVA: 0x0051155A File Offset: 0x0050F75A
		public static Asset<Texture2D> ButtonModConfigTexture { get; internal set; }

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06002948 RID: 10568 RVA: 0x00511562 File Offset: 0x0050F762
		// (set) Token: 0x06002949 RID: 10569 RVA: 0x00511569 File Offset: 0x0050F769
		public static Asset<Texture2D> ModLocationModPackIcon { get; internal set; }

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x0600294A RID: 10570 RVA: 0x00511571 File Offset: 0x0050F771
		// (set) Token: 0x0600294B RID: 10571 RVA: 0x00511578 File Offset: 0x0050F778
		public static Asset<Texture2D> ModLocationLocalIcon { get; internal set; }

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x0600294C RID: 10572 RVA: 0x00511580 File Offset: 0x0050F780
		// (set) Token: 0x0600294D RID: 10573 RVA: 0x00511587 File Offset: 0x0050F787
		public static Asset<Texture2D> DividerTexture { get; internal set; }

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x0600294E RID: 10574 RVA: 0x0051158F File Offset: 0x0050F78F
		// (set) Token: 0x0600294F RID: 10575 RVA: 0x00511596 File Offset: 0x0050F796
		public static Asset<Texture2D> InnerPanelTexture { get; internal set; }

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06002950 RID: 10576 RVA: 0x0051159E File Offset: 0x0050F79E
		// (set) Token: 0x06002951 RID: 10577 RVA: 0x005115A5 File Offset: 0x0050F7A5
		public static Asset<Texture2D> InfoDisplayPageArrowTexture { get; internal set; }

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06002952 RID: 10578 RVA: 0x005115AD File Offset: 0x0050F7AD
		// (set) Token: 0x06002953 RID: 10579 RVA: 0x005115B4 File Offset: 0x0050F7B4
		public static Asset<Texture2D> tModLoaderTitleLinkButtonsTexture { get; internal set; }

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06002954 RID: 10580 RVA: 0x005115BC File Offset: 0x0050F7BC
		// (set) Token: 0x06002955 RID: 10581 RVA: 0x005115C3 File Offset: 0x0050F7C3
		public static Asset<Texture2D> CopyCodeButtonTexture { get; internal set; }

		// Token: 0x06002956 RID: 10582 RVA: 0x005115CC File Offset: 0x0050F7CC
		internal static void LoadTextures()
		{
			UICommon.ButtonErrorTexture = UICommon.<LoadTextures>g__LoadEmbeddedTexture|133_0("UI.ButtonError");
			UICommon.ButtonPlusTexture = UICommon.<LoadTextures>g__LoadEmbeddedTexture|133_0("Config.UI.ButtonPlus");
			UICommon.ButtonUpDownTexture = UICommon.<LoadTextures>g__LoadEmbeddedTexture|133_0("Config.UI.ButtonUpDown");
			UICommon.ButtonCollapsedTexture = UICommon.<LoadTextures>g__LoadEmbeddedTexture|133_0("Config.UI.ButtonCollapsed");
			UICommon.ButtonExpandedTexture = UICommon.<LoadTextures>g__LoadEmbeddedTexture|133_0("Config.UI.ButtonExpanded");
			UICommon.ModBrowserIconsTexture = UICommon.<LoadTextures>g__LoadEmbeddedTexture|133_0("UI.UIModBrowserIcons");
			UICommon.ConfigSideIndicatorTexture = UICommon.<LoadTextures>g__LoadEmbeddedTexture|133_0("UI.ConfigSideIndicator");
			UICommon.ButtonExclamationTexture = UICommon.<LoadTextures>g__LoadEmbeddedTexture|133_0("UI.ButtonExclamation");
			UICommon.ButtonDepsTexture = UICommon.<LoadTextures>g__LoadEmbeddedTexture|133_0("UI.ButtonDeps");
			UICommon.ButtonUpgradeCsproj = UICommon.<LoadTextures>g__LoadEmbeddedTexture|133_0("UI.ButtonUpgradeCsproj");
			UICommon.ButtonUpgradeLang = UICommon.<LoadTextures>g__LoadEmbeddedTexture|133_0("UI.ButtonUpgradeLang");
			UICommon.ButtonRunTModPorter = UICommon.<LoadTextures>g__LoadEmbeddedTexture|133_0("UI.ButtonRunTModPorter");
			UICommon.ButtonOpenFolder = UICommon.<LoadTextures>g__LoadEmbeddedTexture|133_0("UI.ButtonOpenFolder");
			UICommon.ButtonOpenFolderCustom = UICommon.<LoadTextures>g__LoadEmbeddedTexture|133_0("UI.ButtonOpenFolderCustom");
			UICommon.ButtonTranslationModTexture = UICommon.<LoadTextures>g__LoadEmbeddedTexture|133_0("UI.ButtonTranslationMod");
			UICommon.LoaderTexture = UICommon.<LoadTextures>g__LoadEmbeddedTexture|133_0("UI.Loader");
			UICommon.LoaderBgTexture = UICommon.<LoadTextures>g__LoadEmbeddedTexture|133_0("UI.LoaderBG");
			UICommon.ButtonDownloadTexture = UICommon.<LoadTextures>g__LoadEmbeddedTexture|133_0("UI.ButtonDownload");
			UICommon.ButtonDowngradeTexture = UICommon.<LoadTextures>g__LoadEmbeddedTexture|133_0("UI.ButtonDowngrade");
			UICommon.ButtonDownloadMultipleTexture = UICommon.<LoadTextures>g__LoadEmbeddedTexture|133_0("UI.ButtonDownloadMultiple");
			UICommon.ButtonModInfoTexture = UICommon.<LoadTextures>g__LoadEmbeddedTexture|133_0("UI.ButtonModInfo");
			UICommon.ButtonModConfigTexture = UICommon.<LoadTextures>g__LoadEmbeddedTexture|133_0("UI.ButtonModConfig");
			UICommon.ModLocationModPackIcon = UICommon.<LoadTextures>g__LoadEmbeddedTexture|133_0("UI.ModLocationModPackIcon");
			UICommon.ModLocationLocalIcon = UICommon.<LoadTextures>g__LoadEmbeddedTexture|133_0("UI.ModLocationLocalIcon");
			UICommon.DividerTexture = Main.Assets.Request<Texture2D>("Images/UI/Divider");
			UICommon.InnerPanelTexture = Main.Assets.Request<Texture2D>("Images/UI/InnerPanelBackground");
			UICommon.InfoDisplayPageArrowTexture = UICommon.<LoadTextures>g__LoadEmbeddedTexture|133_0("UI.InfoDisplayPageArrow");
			UICommon.tModLoaderTitleLinkButtonsTexture = UICommon.<LoadTextures>g__LoadEmbeddedTexture|133_0("UI.tModLoaderTitleLinkButtons");
			UICommon.CopyCodeButtonTexture = UICommon.<LoadTextures>g__LoadEmbeddedTexture|133_0("UI.CopyCodeButton");
		}

		// Token: 0x06002958 RID: 10584 RVA: 0x00511817 File Offset: 0x0050FA17
		[CompilerGenerated]
		internal static Asset<Texture2D> <LoadTextures>g__LoadEmbeddedTexture|133_0(string name)
		{
			return ModLoader.ManifestAssets.Request<Texture2D>("Terraria.ModLoader." + name);
		}

		// Token: 0x04001A11 RID: 6673
		public static Color DefaultUIBlue = new Color(73, 94, 171);

		// Token: 0x04001A12 RID: 6674
		public static Color DefaultUIBlueMouseOver = new Color(63, 82, 151) * 0.7f;

		// Token: 0x04001A13 RID: 6675
		public static Color DefaultUIBorder = Color.Black;

		// Token: 0x04001A14 RID: 6676
		public static Color DefaultUIBorderMouseOver = Colors.FancyUIFatButtonMouseOver;

		// Token: 0x04001A15 RID: 6677
		public static Color MainPanelBackground = new Color(33, 43, 79) * 0.8f;

		// Token: 0x04001A16 RID: 6678
		public static StyleDimension MaxPanelWidth = new StyleDimension(600f, 0f);
	}
}
