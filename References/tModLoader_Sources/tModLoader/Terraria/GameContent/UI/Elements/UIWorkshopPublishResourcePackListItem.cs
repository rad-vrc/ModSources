using System;
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
	// Token: 0x02000539 RID: 1337
	public class UIWorkshopPublishResourcePackListItem : UIPanel
	{
		// Token: 0x06003FA6 RID: 16294 RVA: 0x005DA8F4 File Offset: 0x005D8AF4
		public UIWorkshopPublishResourcePackListItem(UIState ownerState, ResourcePack data, int orderInList, bool canBePublished)
		{
			this._ownerState = ownerState;
			this._orderInList = orderInList;
			this._data = data;
			this._canPublish = canBePublished;
			this.LoadTextures();
			this.InitializeAppearance();
			UIElement uIElement = new UIElement
			{
				Width = new StyleDimension(72f, 0f),
				Height = new StyleDimension(72f, 0f),
				Left = new StyleDimension(4f, 0f),
				VAlign = 0.5f
			};
			uIElement.OnLeftDoubleClick += this.PublishButtonClick_ImportResourcePackToLocalFiles;
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
			UIImage element2 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Achievement_Borders"))
			{
				HAlign = 0.5f,
				VAlign = 0.5f,
				IgnoresMouseInteraction = true
			};
			uIElement.Append(element);
			uIElement.Append(element2);
			base.Append(uIElement);
			this._iconArea = uIElement;
			this._publishButton = new UIIconTextButton(Language.GetText("Workshop.Publish"), Color.White, "Images/UI/Workshop/Publish", 1f, 0.5f, 10f);
			this._publishButton.HAlign = 1f;
			this._publishButton.VAlign = 1f;
			this._publishButton.OnLeftClick += this.PublishButtonClick_ImportResourcePackToLocalFiles;
			base.OnLeftDoubleClick += this.PublishButtonClick_ImportResourcePackToLocalFiles;
			base.Append(this._publishButton);
			this._publishButton.SetSnapPoint("Publish", orderInList, null, null);
		}

		// Token: 0x06003FA7 RID: 16295 RVA: 0x005DAAE8 File Offset: 0x005D8CE8
		private void LoadTextures()
		{
			this._dividerTexture = Main.Assets.Request<Texture2D>("Images/UI/Divider");
			this._innerPanelTexture = Main.Assets.Request<Texture2D>("Images/UI/InnerPanelBackground");
			this._iconBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/Achievement_Borders");
			this._workshopIconTexture = TextureAssets.Extra[243];
		}

		// Token: 0x06003FA8 RID: 16296 RVA: 0x005DAB45 File Offset: 0x005D8D45
		private void InitializeAppearance()
		{
			this.Height.Set(82f, 0f);
			this.Width.Set(0f, 1f);
			base.SetPadding(6f);
			this.SetColorsToNotHovered();
		}

		// Token: 0x06003FA9 RID: 16297 RVA: 0x005DAB84 File Offset: 0x005D8D84
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

		// Token: 0x06003FAA RID: 16298 RVA: 0x005DAC10 File Offset: 0x005D8E10
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

		// Token: 0x06003FAB RID: 16299 RVA: 0x005DACAF File Offset: 0x005D8EAF
		private void PublishButtonClick_ImportResourcePackToLocalFiles(UIMouseEvent evt, UIElement listeningElement)
		{
			if (listeningElement == evt.Target && !this.TryMovingToRejectionMenuIfNeeded())
			{
				Main.MenuUI.SetState(new WorkshopPublishInfoStateForResourcePack(this._ownerState, this._data));
			}
		}

		// Token: 0x06003FAC RID: 16300 RVA: 0x005DACE0 File Offset: 0x005D8EE0
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

		// Token: 0x06003FAD RID: 16301 RVA: 0x005DAD48 File Offset: 0x005D8F48
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

		// Token: 0x06003FAE RID: 16302 RVA: 0x005DAD88 File Offset: 0x005D8F88
		public override int CompareTo(object obj)
		{
			UIWorkshopPublishResourcePackListItem uIWorkshopPublishResourcePackListItem = obj as UIWorkshopPublishResourcePackListItem;
			if (uIWorkshopPublishResourcePackListItem != null)
			{
				return this._orderInList.CompareTo(uIWorkshopPublishResourcePackListItem._orderInList);
			}
			return base.CompareTo(obj);
		}

		// Token: 0x06003FAF RID: 16303 RVA: 0x005DADB8 File Offset: 0x005D8FB8
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			this.SetColorsToHovered();
		}

		// Token: 0x06003FB0 RID: 16304 RVA: 0x005DADC7 File Offset: 0x005D8FC7
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this.SetColorsToNotHovered();
		}

		// Token: 0x06003FB1 RID: 16305 RVA: 0x005DADD8 File Offset: 0x005D8FD8
		private void DrawPanel(SpriteBatch spriteBatch, Vector2 position, float width, float height)
		{
			Utils.DrawSplicedPanel(spriteBatch, this._innerPanelTexture.Value, (int)position.X, (int)position.Y, (int)width, (int)height, 10, 10, 10, 10, Color.White);
		}

		// Token: 0x06003FB2 RID: 16306 RVA: 0x005DAE18 File Offset: 0x005D9018
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			CalculatedStyle innerDimensions = base.GetInnerDimensions();
			CalculatedStyle dimensions = this._iconArea.GetDimensions();
			float num = dimensions.X + dimensions.Width;
			Color white = Color.White;
			Utils.DrawBorderString(spriteBatch, this._data.Name, new Vector2(num + 8f, innerDimensions.Y + 3f), white, 1f, 0f, 0f, -1);
			float num6 = (innerDimensions.Width - 22f - dimensions.Width - this._publishButton.GetDimensions().Width) / 2f;
			float height = this._publishButton.GetDimensions().Height;
			Vector2 vector;
			vector..ctor(num + 8f, innerDimensions.Y + innerDimensions.Height - height);
			float num2 = num6;
			this.DrawPanel(spriteBatch, vector, num2, height);
			string textValue = Language.GetTextValue("UI.Author", this._data.Author);
			Color white2 = Color.White;
			Vector2 vector2 = FontAssets.MouseText.Value.MeasureString(textValue);
			float x = vector2.X;
			float y = vector2.Y;
			float x2 = num2 * 0.5f - x * 0.5f;
			float num3 = height * 0.5f - y * 0.5f;
			Utils.DrawBorderString(spriteBatch, textValue, vector + new Vector2(x2, num3 + 3f), white2, 1f, 0f, 0f, -1);
			vector.X += num2 + 5f;
			float num4 = num6;
			this.DrawPanel(spriteBatch, vector, num4, height);
			string textValue2 = Language.GetTextValue("UI.Version", this._data.Version.GetFormattedVersion());
			Color white3 = Color.White;
			Vector2 vector3 = FontAssets.MouseText.Value.MeasureString(textValue2);
			float x3 = vector3.X;
			float y2 = vector3.Y;
			float x4 = num4 * 0.5f - x3 * 0.5f;
			float num5 = height * 0.5f - y2 * 0.5f;
			Utils.DrawBorderString(spriteBatch, textValue2, vector + new Vector2(x4, num5 + 3f), white3, 1f, 0f, 0f, -1);
			vector.X += num4 + 5f;
		}

		// Token: 0x040057FA RID: 22522
		private ResourcePack _data;

		// Token: 0x040057FB RID: 22523
		private Asset<Texture2D> _dividerTexture;

		// Token: 0x040057FC RID: 22524
		private Asset<Texture2D> _workshopIconTexture;

		// Token: 0x040057FD RID: 22525
		private Asset<Texture2D> _iconBorderTexture;

		// Token: 0x040057FE RID: 22526
		private Asset<Texture2D> _innerPanelTexture;

		// Token: 0x040057FF RID: 22527
		private UIElement _iconArea;

		// Token: 0x04005800 RID: 22528
		private UIElement _publishButton;

		// Token: 0x04005801 RID: 22529
		private int _orderInList;

		// Token: 0x04005802 RID: 22530
		private UIState _ownerState;

		// Token: 0x04005803 RID: 22531
		private const int ICON_SIZE = 64;

		// Token: 0x04005804 RID: 22532
		private const int ICON_BORDER_PADDING = 4;

		// Token: 0x04005805 RID: 22533
		private const int HEIGHT_FLUFF = 10;

		// Token: 0x04005806 RID: 22534
		private bool _canPublish;
	}
}
