using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Social.Base;
using Terraria.UI;

namespace Terraria.ModLoader.UI.ModBrowser
{
	// Token: 0x02000270 RID: 624
	internal class UIModDownloadItem : UIPanel
	{
		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06002B25 RID: 11045 RVA: 0x005209CB File Offset: 0x0051EBCB
		private bool HasModIcon
		{
			get
			{
				return !string.IsNullOrEmpty(this.ModDownload.ModIconUrl);
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06002B26 RID: 11046 RVA: 0x005209E0 File Offset: 0x0051EBE0
		private float ModIconAdjust
		{
			get
			{
				return 85f;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06002B27 RID: 11047 RVA: 0x005209E7 File Offset: 0x0051EBE7
		private string ViewModInfoText
		{
			get
			{
				return Language.GetTextValue("tModLoader.ModsMoreInfo");
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06002B28 RID: 11048 RVA: 0x005209F3 File Offset: 0x0051EBF3
		private string UpdateWithDepsText
		{
			get
			{
				if (!this.ModDownload.NeedUpdate)
				{
					return Language.GetTextValue("tModLoader.MBDownloadWithDependencies");
				}
				if (!this.UpdateIsDowngrade)
				{
					return Language.GetTextValue("tModLoader.MBUpdateWithDependencies");
				}
				return Language.GetTextValue("tModLoader.MBDowngradeWithDependencies");
			}
		}

		// Token: 0x06002B29 RID: 11049 RVA: 0x00520A2C File Offset: 0x0051EC2C
		public UIModDownloadItem(ModDownloadItem modDownloadItem)
		{
			this.ModDownload = modDownloadItem;
			this.BorderColor = new Color(89, 116, 213) * 0.7f;
			this._dividerTexture = UICommon.DividerTexture;
			this._innerPanelTexture = UICommon.InnerPanelTexture;
			this.Height.Pixels = 92f;
			this.Width.Percent = 1f;
			base.SetPadding(6f);
			float leftOffset = this.HasModIcon ? this.ModIconAdjust : 0f;
			UIText uitext = new UIText(this.ModDownload.DisplayName, 1f, false);
			uitext.Left = new StyleDimension(leftOffset + 5f, 0f);
			uitext.Top.Pixels = 5f;
			this._modName = uitext;
			base.Append(this._modName);
			UIImage uiimage = new UIImage(UICommon.ButtonModInfoTexture);
			uiimage.RemoveFloatingPointsFromDrawPosition = true;
			uiimage.Width.Pixels = 36f;
			uiimage.Height.Pixels = 36f;
			uiimage.Left.Pixels = leftOffset;
			uiimage.Top.Pixels = 40f;
			this._moreInfoButton = uiimage;
			this._moreInfoButton.OnLeftClick += this.ViewModInfo;
			base.Append(this._moreInfoButton);
			Version modBuildVersion = this.ModDownload.ModloaderVersion;
			this.tMLNeedUpdate = (!BuildInfo.IsDev && BuildInfo.tMLVersion.MajorMinorBuild() < modBuildVersion.MajorMinorBuild());
			if (this.tMLNeedUpdate)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 1);
				defaultInterpolatedStringHandler.AppendLiteral("v");
				defaultInterpolatedStringHandler.AppendFormatted<Version>(modBuildVersion);
				string updateVersion = defaultInterpolatedStringHandler.ToStringAndClear();
				if (modBuildVersion.Build == 2)
				{
					updateVersion = "Preview " + updateVersion;
				}
				this.tMLUpdateRequired = new UIAutoScaleTextTextPanel<string>(Language.GetTextValue("tModLoader.MBRequiresTMLUpdate", updateVersion), 1f, false).WithFadedMouseOver(Color.Orange, Color.Orange * 0.7f, default(Color), default(Color));
				this.tMLUpdateRequired.BackgroundColor = Color.Orange * 0.7f;
				this.tMLUpdateRequired.CopyStyle(this._moreInfoButton);
				this.tMLUpdateRequired.Width.Pixels = 340f;
				UIAutoScaleTextTextPanel<string> uiautoScaleTextTextPanel = this.tMLUpdateRequired;
				uiautoScaleTextTextPanel.Left.Pixels = uiautoScaleTextTextPanel.Left.Pixels + 41f;
				this.tMLUpdateRequired.OnLeftClick += delegate(UIMouseEvent a, UIElement b)
				{
					Utils.OpenToURL("https://github.com/tModLoader/tModLoader/releases/latest");
				};
				base.Append(this.tMLUpdateRequired);
			}
			this._updateButton = new UIImage(UICommon.ButtonExclamationTexture);
			this._updateButton.CopyStyle(this._moreInfoButton);
			UIImage updateButton = this._updateButton;
			updateButton.Left.Pixels = updateButton.Left.Pixels + 41f;
			this._updateButton.OnLeftClick += this.ShowGameNeedsRestart;
			this._updateWithDepsButton = new UIImage(UICommon.ButtonDownloadMultipleTexture);
			this._updateWithDepsButton.CopyStyle(this._moreInfoButton);
			UIImage updateWithDepsButton = this._updateWithDepsButton;
			updateWithDepsButton.Left.Pixels = updateWithDepsButton.Left.Pixels + 41f;
			this._updateWithDepsButton.OnLeftClick += this.DownloadWithDeps;
			string modReferencesBySlug = this.ModDownload.ModReferencesBySlug;
			if (modReferencesBySlug != null && modReferencesBySlug.Length > 0)
			{
				Asset<Texture2D> icon = UICommon.ButtonDepsTexture;
				UIHoverImage uihoverImage = new UIHoverImage(icon, Language.GetTextValue("tModLoader.MBClickToViewDependencyMods", string.Join("\n", from x in this.ModDownload.ModReferencesBySlug.Split(',', StringSplitOptions.None)
				select x.Trim())));
				uihoverImage.RemoveFloatingPointsFromDrawPosition = true;
				uihoverImage.UseTooltipMouseText = true;
				uihoverImage.Left.Pixels = (float)(-(float)icon.Width()) - 5f;
				uihoverImage.Left.Percent = 1f;
				UIHoverImage modReferenceIcon = uihoverImage;
				modReferenceIcon.OnLeftClick += this.ShowModDependencies;
				base.Append(modReferenceIcon);
			}
			base.OnLeftDoubleClick += this.ViewModInfo;
			this.UpdateInstallDisplayState();
		}

		// Token: 0x06002B2A RID: 11050 RVA: 0x00520E58 File Offset: 0x0051F058
		public void UpdateInstallDisplayState()
		{
			if (this.tMLNeedUpdate)
			{
				return;
			}
			this._updateWithDepsButton.Remove();
			this._updateButton.Remove();
			if (this.ModDownload.AppNeedRestartToReinstall)
			{
				base.Append(this._updateButton);
				return;
			}
			if (this.ModDownload.NeedUpdate || !this.ModDownload.IsInstalled)
			{
				base.Append(this._updateWithDepsButton);
			}
		}

		// Token: 0x06002B2B RID: 11051 RVA: 0x00520EC4 File Offset: 0x0051F0C4
		private void ShowModDependencies(UIMouseEvent evt, UIElement element)
		{
			UIModDownloadItem modListItem = (UIModDownloadItem)element.Parent;
			Interface.modBrowser.SpecialModPackFilter = modListItem.ModDownload.ModReferenceByModId.ToList<ModPubId_t>();
			Interface.modBrowser.SpecialModPackFilterTitle = Language.GetTextValue("tModLoader.MBFilterDependencies");
			Interface.modBrowser.FilterTextBox.Text = "";
			Interface.modBrowser.ResetTagFilters();
			Interface.modBrowser.UpdateNeeded = true;
			SoundEngine.PlaySound(SoundID.MenuOpen, null, null);
		}

		// Token: 0x06002B2C RID: 11052 RVA: 0x00520F49 File Offset: 0x0051F149
		private void ShowGameNeedsRestart(UIMouseEvent evt, UIElement element)
		{
			Utils.ShowFancyErrorMessage(Language.GetTextValue("tModLoader.SteamRejectUpdate", this.ModDownload.DisplayName), 10007, null);
		}

		// Token: 0x06002B2D RID: 11053 RVA: 0x00520F6C File Offset: 0x0051F16C
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			if (this.HasModIcon && this.ModIconStatus == ModIconStatus.UNKNOWN)
			{
				this.RequestModIcon();
			}
			CalculatedStyle innerDimensions = base.GetInnerDimensions();
			float leftOffset = this.HasModIcon ? this.ModIconAdjust : 0f;
			Vector2 drawPos;
			drawPos..ctor(innerDimensions.X + 5f + leftOffset, innerDimensions.Y + 30f);
			spriteBatch.Draw(this._dividerTexture.Value, drawPos, null, Color.White, 0f, Vector2.Zero, new Vector2((innerDimensions.Width - 10f - leftOffset) / 8f, 1f), 0, 0f);
			drawPos..ctor(innerDimensions.X + innerDimensions.Width - 125f, innerDimensions.Y + 45f);
			this.DrawTimeText(spriteBatch, drawPos);
			UIImage updateButton = this._updateButton;
			if (updateButton != null && updateButton.IsMouseHovering)
			{
				this.tooltip = Language.GetTextValue("tModLoader.BrowserRejectWarning");
				return;
			}
			UIImage updateWithDepsButton = this._updateWithDepsButton;
			if (updateWithDepsButton != null && updateWithDepsButton.IsMouseHovering)
			{
				this.tooltip = this.UpdateWithDepsText;
				return;
			}
			UIImage moreInfoButton = this._moreInfoButton;
			if (moreInfoButton != null && moreInfoButton.IsMouseHovering)
			{
				this.tooltip = this.ViewModInfoText;
			}
		}

		// Token: 0x06002B2E RID: 11054 RVA: 0x005210B8 File Offset: 0x0051F2B8
		public override void Draw(SpriteBatch spriteBatch)
		{
			this.tooltip = null;
			base.Draw(spriteBatch);
			if (!string.IsNullOrEmpty(this.tooltip))
			{
				base.GetOuterDimensions().ToRectangle().Height += 16;
				UICommon.TooltipMouseText(this.tooltip);
			}
		}

		// Token: 0x06002B2F RID: 11055 RVA: 0x00521108 File Offset: 0x0051F308
		protected override void DrawChildren(SpriteBatch spriteBatch)
		{
			base.DrawChildren(spriteBatch);
			UIAutoScaleTextTextPanel<string> uiautoScaleTextTextPanel = this.tMLUpdateRequired;
			if (uiautoScaleTextTextPanel != null && uiautoScaleTextTextPanel.IsMouseHovering)
			{
				UICommon.TooltipMouseText(Language.GetTextValue("tModLoader.MBClickToUpdate"));
			}
			if (this._modName.IsMouseHovering)
			{
				UICommon.TooltipMouseText(Language.GetTextValue("tModLoader.ModsByline", this.ModDownload.Author));
			}
		}

		// Token: 0x06002B30 RID: 11056 RVA: 0x00521166 File Offset: 0x0051F366
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (this.ModIconStatus == ModIconStatus.READY)
			{
				if (this._modIcon == null)
				{
					this.AdjustPositioningFailedIcon();
				}
				else
				{
					base.Append(this._modIcon);
				}
				this.ModIconStatus = ModIconStatus.DISPLAYED_OR_FAILED;
			}
		}

		// Token: 0x06002B31 RID: 11057 RVA: 0x0052119C File Offset: 0x0051F39C
		private void RequestModIcon()
		{
			UIModDownloadItem.<RequestModIcon>d__33 <RequestModIcon>d__;
			<RequestModIcon>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<RequestModIcon>d__.<>4__this = this;
			<RequestModIcon>d__.<>1__state = -1;
			<RequestModIcon>d__.<>t__builder.Start<UIModDownloadItem.<RequestModIcon>d__33>(ref <RequestModIcon>d__);
		}

		// Token: 0x06002B32 RID: 11058 RVA: 0x005211D4 File Offset: 0x0051F3D4
		[return: Nullable(new byte[]
		{
			0,
			2
		})]
		private static Task<Texture2D> GetOrDownloadTextureAsync(string url)
		{
			UIModDownloadItem.<GetOrDownloadTextureAsync>d__34 <GetOrDownloadTextureAsync>d__;
			<GetOrDownloadTextureAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Texture2D>.Create();
			<GetOrDownloadTextureAsync>d__.url = url;
			<GetOrDownloadTextureAsync>d__.<>1__state = -1;
			<GetOrDownloadTextureAsync>d__.<>t__builder.Start<UIModDownloadItem.<GetOrDownloadTextureAsync>d__34>(ref <GetOrDownloadTextureAsync>d__);
			return <GetOrDownloadTextureAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06002B33 RID: 11059 RVA: 0x00521218 File Offset: 0x0051F418
		private void AdjustPositioningFailedIcon()
		{
			UIText modName = this._modName;
			modName.Left.Pixels = modName.Left.Pixels - this.ModIconAdjust;
			UIImage moreInfoButton = this._moreInfoButton;
			moreInfoButton.Left.Pixels = moreInfoButton.Left.Pixels - this.ModIconAdjust;
			if (this._updateButton != null)
			{
				UIImage updateButton = this._updateButton;
				updateButton.Left.Pixels = updateButton.Left.Pixels - this.ModIconAdjust;
			}
			if (this._updateWithDepsButton != null)
			{
				UIImage updateWithDepsButton = this._updateWithDepsButton;
				updateWithDepsButton.Left.Pixels = updateWithDepsButton.Left.Pixels - this.ModIconAdjust;
			}
		}

		// Token: 0x06002B34 RID: 11060 RVA: 0x005212A0 File Offset: 0x0051F4A0
		private void DrawTimeText(SpriteBatch spriteBatch, Vector2 drawPos)
		{
			if (this.ModDownload.TimeStamp == DateTime.MinValue)
			{
				return;
			}
			spriteBatch.Draw(this._innerPanelTexture.Value, drawPos, new Rectangle?(new Rectangle(0, 0, 8, this._innerPanelTexture.Height())), Color.White);
			spriteBatch.Draw(this._innerPanelTexture.Value, new Vector2(drawPos.X + 8f, drawPos.Y), new Rectangle?(new Rectangle(8, 0, 8, this._innerPanelTexture.Height())), Color.White, 0f, Vector2.Zero, new Vector2(13.625f, 1f), 0, 0f);
			spriteBatch.Draw(this._innerPanelTexture.Value, new Vector2(drawPos.X + 125f - 8f, drawPos.Y), new Rectangle?(new Rectangle(16, 0, 8, this._innerPanelTexture.Height())), Color.White);
			drawPos += new Vector2(0f, 2f);
			try
			{
				string text = TimeHelper.HumanTimeSpanString(this.ModDownload.TimeStamp);
				int textWidth = (int)FontAssets.MouseText.Value.MeasureString(text).X;
				int diffWidth = 125 - textWidth;
				drawPos.X += (float)diffWidth * 0.5f;
				Utils.DrawBorderString(spriteBatch, text, drawPos, Color.White, 1f, 0f, 0f, -1);
			}
			catch (Exception e)
			{
				Logging.tML.Error("Problem during drawing of time text", e);
			}
		}

		// Token: 0x06002B35 RID: 11061 RVA: 0x00521440 File Offset: 0x0051F640
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			this.BackgroundColor = UICommon.DefaultUIBlue;
			this.BorderColor = new Color(89, 116, 213);
		}

		// Token: 0x06002B36 RID: 11062 RVA: 0x00521468 File Offset: 0x0051F668
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this.BackgroundColor = new Color(63, 82, 151) * 0.7f;
			this.BorderColor = new Color(89, 116, 213) * 0.7f;
		}

		// Token: 0x06002B37 RID: 11063 RVA: 0x005214B8 File Offset: 0x0051F6B8
		private void DownloadWithDeps(UIMouseEvent evt, UIElement listeningElement)
		{
			UIModDownloadItem.<DownloadWithDeps>d__39 <DownloadWithDeps>d__;
			<DownloadWithDeps>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<DownloadWithDeps>d__.<>4__this = this;
			<DownloadWithDeps>d__.<>1__state = -1;
			<DownloadWithDeps>d__.<>t__builder.Start<UIModDownloadItem.<DownloadWithDeps>d__39>(ref <DownloadWithDeps>d__);
		}

		// Token: 0x06002B38 RID: 11064 RVA: 0x005214F0 File Offset: 0x0051F6F0
		private void ViewModInfo(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(SoundID.MenuOpen, null, null);
			Utils.OpenToURL(Interface.modBrowser.SocialBackend.GetModWebPage(this.ModDownload.PublishId));
		}

		// Token: 0x04001BAF RID: 7087
		private const float PADDING = 5f;

		// Token: 0x04001BB0 RID: 7088
		public readonly ModDownloadItem ModDownload;

		// Token: 0x04001BB1 RID: 7089
		private readonly Asset<Texture2D> _dividerTexture;

		// Token: 0x04001BB2 RID: 7090
		private readonly Asset<Texture2D> _innerPanelTexture;

		// Token: 0x04001BB3 RID: 7091
		private readonly UIText _modName;

		// Token: 0x04001BB4 RID: 7092
		private readonly UIImage _updateButton;

		// Token: 0x04001BB5 RID: 7093
		private readonly UIImage _updateWithDepsButton;

		// Token: 0x04001BB6 RID: 7094
		private readonly UIImage _moreInfoButton;

		// Token: 0x04001BB7 RID: 7095
		private readonly UIAutoScaleTextTextPanel<string> tMLUpdateRequired;

		// Token: 0x04001BB8 RID: 7096
		internal ModIconStatus ModIconStatus;

		// Token: 0x04001BB9 RID: 7097
		private UIImage _modIcon;

		// Token: 0x04001BBA RID: 7098
		internal string tooltip;

		// Token: 0x04001BBB RID: 7099
		private static int MaxFails = 3;

		// Token: 0x04001BBC RID: 7100
		private static int ModIconDownloadFailCount = 0;

		// Token: 0x04001BBD RID: 7101
		private bool UpdateIsDowngrade;

		// Token: 0x04001BBE RID: 7102
		private readonly bool tMLNeedUpdate;

		// Token: 0x04001BBF RID: 7103
		private static ConcurrentDictionary<string, Texture2D> TextureDownloadCache = new ConcurrentDictionary<string, Texture2D>();
	}
}
