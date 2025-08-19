﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.UI.States;
using Terraria.IO;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x0200036F RID: 879
	public class UIWorkshopPublishResourcePackListItem : UIPanel
	{
		// Token: 0x06002841 RID: 10305 RVA: 0x00589390 File Offset: 0x00587590
		public UIWorkshopPublishResourcePackListItem(UIState ownerState, ResourcePack data, int orderInList, bool canBePublished)
		{
			this._ownerState = ownerState;
			this._orderInList = orderInList;
			this._data = data;
			this._canPublish = canBePublished;
			this.LoadTextures();
			this.InitializeAppearance();
			UIElement uielement = new UIElement
			{
				Width = new StyleDimension(72f, 0f),
				Height = new StyleDimension(72f, 0f),
				Left = new StyleDimension(4f, 0f),
				VAlign = 0.5f
			};
			uielement.OnLeftDoubleClick += this.PublishButtonClick_ImportResourcePackToLocalFiles;
			UIImage element = new UIImage(data.Icon)
			{
				Width = new StyleDimension(-6f, 1f),
				Height = new StyleDimension(-6f, 1f),
				HAlign = 0.5f,
				VAlign = 0.5f,
				ScaleToFit = true,
				AllowResizingDimensions = false,
				IgnoresMouseInteraction = true
			};
			UIImage element2 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Achievement_Borders", 1))
			{
				HAlign = 0.5f,
				VAlign = 0.5f,
				IgnoresMouseInteraction = true
			};
			uielement.Append(element);
			uielement.Append(element2);
			base.Append(uielement);
			this._iconArea = uielement;
			this._publishButton = new UIIconTextButton(Language.GetText("Workshop.Publish"), Color.White, "Images/UI/Workshop/Publish", 1f, 0.5f, 10f);
			this._publishButton.HAlign = 1f;
			this._publishButton.VAlign = 1f;
			this._publishButton.OnLeftClick += this.PublishButtonClick_ImportResourcePackToLocalFiles;
			base.OnLeftDoubleClick += this.PublishButtonClick_ImportResourcePackToLocalFiles;
			base.Append(this._publishButton);
			this._publishButton.SetSnapPoint("Publish", orderInList, null, null);
		}

		// Token: 0x06002842 RID: 10306 RVA: 0x0058958C File Offset: 0x0058778C
		private void LoadTextures()
		{
			this._dividerTexture = Main.Assets.Request<Texture2D>("Images/UI/Divider", 1);
			this._innerPanelTexture = Main.Assets.Request<Texture2D>("Images/UI/InnerPanelBackground", 1);
			this._iconBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/Achievement_Borders", 1);
			this._workshopIconTexture = TextureAssets.Extra[243];
		}

		// Token: 0x06002843 RID: 10307 RVA: 0x005895EC File Offset: 0x005877EC
		private void InitializeAppearance()
		{
			this.Height.Set(82f, 0f);
			this.Width.Set(0f, 1f);
			base.SetPadding(6f);
			this.SetColorsToNotHovered();
		}

		// Token: 0x06002844 RID: 10308 RVA: 0x0058962C File Offset: 0x0058782C
		private void SetColorsToHovered()
		{
			this.BackgroundColor = new Color(73, 94, 171);
			this.BorderColor = new Color(89, 116, 213);
			if (!this._canPublish)
			{
				this.BorderColor = new Color(150, 150, 150) * 1f;
				this.BackgroundColor = Color.Lerp(this.BackgroundColor, new Color(120, 120, 120), 0.5f) * 1f;
			}
		}

		// Token: 0x06002845 RID: 10309 RVA: 0x005896B8 File Offset: 0x005878B8
		private void SetColorsToNotHovered()
		{
			this.BackgroundColor = new Color(63, 82, 151) * 0.7f;
			this.BorderColor = new Color(89, 116, 213) * 0.7f;
			if (!this._canPublish)
			{
				this.BorderColor = new Color(127, 127, 127) * 0.7f;
				this.BackgroundColor = Color.Lerp(new Color(63, 82, 151), new Color(80, 80, 80), 0.5f) * 0.7f;
			}
		}

		// Token: 0x06002846 RID: 10310 RVA: 0x00589757 File Offset: 0x00587957
		private void PublishButtonClick_ImportResourcePackToLocalFiles(UIMouseEvent evt, UIElement listeningElement)
		{
			if (listeningElement != evt.Target)
			{
				return;
			}
			if (this.TryMovingToRejectionMenuIfNeeded())
			{
				return;
			}
			Main.MenuUI.SetState(new WorkshopPublishInfoStateForResourcePack(this._ownerState, this._data));
		}

		// Token: 0x06002847 RID: 10311 RVA: 0x00589788 File Offset: 0x00587988
		private bool TryMovingToRejectionMenuIfNeeded()
		{
			if (!this._canPublish)
			{
				SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
				Main.instance.RejectionMenuInfo = new RejectionMenuInfo
				{
					TextToShow = Language.GetTextValue("Workshop.ReportIssue_CannotPublishZips"),
					ExitAction = new ReturnFromRejectionMenuAction(this.RejectionMenuExitAction)
				};
				Main.menuMode = 1000001;
				return true;
			}
			return false;
		}

		// Token: 0x06002848 RID: 10312 RVA: 0x005897F0 File Offset: 0x005879F0
		private void RejectionMenuExitAction()
		{
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			if (this._ownerState == null)
			{
				Main.menuMode = 0;
				return;
			}
			Main.menuMode = 888;
			Main.MenuUI.SetState(this._ownerState);
		}

		// Token: 0x06002849 RID: 10313 RVA: 0x00589830 File Offset: 0x00587A30
		public override int CompareTo(object obj)
		{
			UIWorkshopPublishResourcePackListItem uiworkshopPublishResourcePackListItem = obj as UIWorkshopPublishResourcePackListItem;
			if (uiworkshopPublishResourcePackListItem != null)
			{
				return this._orderInList.CompareTo(uiworkshopPublishResourcePackListItem._orderInList);
			}
			return base.CompareTo(obj);
		}

		// Token: 0x0600284A RID: 10314 RVA: 0x00589860 File Offset: 0x00587A60
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			this.SetColorsToHovered();
		}

		// Token: 0x0600284B RID: 10315 RVA: 0x0058986F File Offset: 0x00587A6F
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this.SetColorsToNotHovered();
		}

		// Token: 0x0600284C RID: 10316 RVA: 0x00589880 File Offset: 0x00587A80
		private void DrawPanel(SpriteBatch spriteBatch, Vector2 position, float width, float height)
		{
			Utils.DrawSplicedPanel(spriteBatch, this._innerPanelTexture.Value, (int)position.X, (int)position.Y, (int)width, (int)height, 10, 10, 10, 10, Color.White);
		}

		// Token: 0x0600284D RID: 10317 RVA: 0x005898C0 File Offset: 0x00587AC0
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			CalculatedStyle innerDimensions = base.GetInnerDimensions();
			CalculatedStyle dimensions = this._iconArea.GetDimensions();
			float num = dimensions.X + dimensions.Width;
			Color white = Color.White;
			Utils.DrawBorderString(spriteBatch, this._data.Name, new Vector2(num + 8f, innerDimensions.Y + 3f), white, 1f, 0f, 0f, -1);
			float num2 = (innerDimensions.Width - 22f - dimensions.Width - this._publishButton.GetDimensions().Width) / 2f;
			float height = this._publishButton.GetDimensions().Height;
			Vector2 vector = new Vector2(num + 8f, innerDimensions.Y + innerDimensions.Height - height);
			float num3 = num2;
			this.DrawPanel(spriteBatch, vector, num3, height);
			string textValue = Language.GetTextValue("UI.Author", this._data.Author);
			Color white2 = Color.White;
			Vector2 vector2 = FontAssets.MouseText.Value.MeasureString(textValue);
			float x = vector2.X;
			float y = vector2.Y;
			float x2 = num3 * 0.5f - x * 0.5f;
			float num4 = height * 0.5f - y * 0.5f;
			Utils.DrawBorderString(spriteBatch, textValue, vector + new Vector2(x2, num4 + 3f), white2, 1f, 0f, 0f, -1);
			vector.X += num3 + 5f;
			float num5 = num2;
			this.DrawPanel(spriteBatch, vector, num5, height);
			string textValue2 = Language.GetTextValue("UI.Version", this._data.Version.GetFormattedVersion());
			Color white3 = Color.White;
			Vector2 vector3 = FontAssets.MouseText.Value.MeasureString(textValue2);
			float x3 = vector3.X;
			float y2 = vector3.Y;
			float x4 = num5 * 0.5f - x3 * 0.5f;
			float num6 = height * 0.5f - y2 * 0.5f;
			Utils.DrawBorderString(spriteBatch, textValue2, vector + new Vector2(x4, num6 + 3f), white3, 1f, 0f, 0f, -1);
			vector.X += num5 + 5f;
		}

		// Token: 0x04004B5F RID: 19295
		private ResourcePack _data;

		// Token: 0x04004B60 RID: 19296
		private Asset<Texture2D> _dividerTexture;

		// Token: 0x04004B61 RID: 19297
		private Asset<Texture2D> _workshopIconTexture;

		// Token: 0x04004B62 RID: 19298
		private Asset<Texture2D> _iconBorderTexture;

		// Token: 0x04004B63 RID: 19299
		private Asset<Texture2D> _innerPanelTexture;

		// Token: 0x04004B64 RID: 19300
		private UIElement _iconArea;

		// Token: 0x04004B65 RID: 19301
		private UIElement _publishButton;

		// Token: 0x04004B66 RID: 19302
		private int _orderInList;

		// Token: 0x04004B67 RID: 19303
		private UIState _ownerState;

		// Token: 0x04004B68 RID: 19304
		private const int ICON_SIZE = 64;

		// Token: 0x04004B69 RID: 19305
		private const int ICON_BORDER_PADDING = 4;

		// Token: 0x04004B6A RID: 19306
		private const int HEIGHT_FLUFF = 10;

		// Token: 0x04004B6B RID: 19307
		private bool _canPublish;
	}
}
