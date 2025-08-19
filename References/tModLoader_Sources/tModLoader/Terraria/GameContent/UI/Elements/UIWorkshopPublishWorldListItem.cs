using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent.UI.States;
using Terraria.IO;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x0200053A RID: 1338
	public class UIWorkshopPublishWorldListItem : AWorldListItem
	{
		// Token: 0x06003FB3 RID: 16307 RVA: 0x005DB060 File Offset: 0x005D9260
		public UIWorkshopPublishWorldListItem(UIState ownerState, WorldFileData data, int orderInList)
		{
			this._ownerState = ownerState;
			this._orderInList = orderInList;
			this._data = data;
			this.LoadTextures();
			this.InitializeAppearance();
			this._worldIcon = base.GetIconElement();
			this._worldIcon.Left.Set(4f, 0f);
			this._worldIcon.VAlign = 0.5f;
			this._worldIcon.OnLeftDoubleClick += this.PublishButtonClick_ImportWorldToLocalFiles;
			base.Append(this._worldIcon);
			this._publishButton = new UIIconTextButton(Language.GetText("Workshop.Publish"), Color.White, "Images/UI/Workshop/Publish", 1f, 0.5f, 10f);
			this._publishButton.HAlign = 1f;
			this._publishButton.VAlign = 1f;
			this._publishButton.OnLeftClick += this.PublishButtonClick_ImportWorldToLocalFiles;
			base.OnLeftDoubleClick += this.PublishButtonClick_ImportWorldToLocalFiles;
			base.Append(this._publishButton);
			this._publishButton.SetSnapPoint("Publish", orderInList, null, null);
		}

		// Token: 0x06003FB4 RID: 16308 RVA: 0x005DB193 File Offset: 0x005D9393
		private void LoadTextures()
		{
			this._innerPanelTexture = Main.Assets.Request<Texture2D>("Images/UI/InnerPanelBackground");
			this._workshopIconTexture = TextureAssets.Extra[243];
		}

		// Token: 0x06003FB5 RID: 16309 RVA: 0x005DB1BB File Offset: 0x005D93BB
		private void InitializeAppearance()
		{
			this.Height.Set(82f, 0f);
			this.Width.Set(0f, 1f);
			base.SetPadding(6f);
			this.SetColorsToNotHovered();
		}

		// Token: 0x06003FB6 RID: 16310 RVA: 0x005DB1F8 File Offset: 0x005D93F8
		private void SetColorsToHovered()
		{
			this.BackgroundColor = new Color(73, 94, 171);
			this.BorderColor = new Color(89, 116, 213);
		}

		// Token: 0x06003FB7 RID: 16311 RVA: 0x005DB222 File Offset: 0x005D9422
		private void SetColorsToNotHovered()
		{
			this.BackgroundColor = new Color(63, 82, 151) * 0.7f;
			this.BorderColor = new Color(89, 116, 213) * 0.7f;
		}

		// Token: 0x06003FB8 RID: 16312 RVA: 0x005DB260 File Offset: 0x005D9460
		private void PublishButtonClick_ImportWorldToLocalFiles(UIMouseEvent evt, UIElement listeningElement)
		{
			if (listeningElement == evt.Target)
			{
				Main.MenuUI.SetState(new WorkshopPublishInfoStateForWorld(this._ownerState, this._data));
			}
		}

		// Token: 0x06003FB9 RID: 16313 RVA: 0x005DB288 File Offset: 0x005D9488
		public override int CompareTo(object obj)
		{
			UIWorkshopPublishWorldListItem uIWorkshopPublishWorldListItem = obj as UIWorkshopPublishWorldListItem;
			if (uIWorkshopPublishWorldListItem != null)
			{
				return this._orderInList.CompareTo(uIWorkshopPublishWorldListItem._orderInList);
			}
			return base.CompareTo(obj);
		}

		// Token: 0x06003FBA RID: 16314 RVA: 0x005DB2B8 File Offset: 0x005D94B8
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			this.SetColorsToHovered();
		}

		// Token: 0x06003FBB RID: 16315 RVA: 0x005DB2C7 File Offset: 0x005D94C7
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this.SetColorsToNotHovered();
		}

		// Token: 0x06003FBC RID: 16316 RVA: 0x005DB2D8 File Offset: 0x005D94D8
		private void DrawPanel(SpriteBatch spriteBatch, Vector2 position, float width, float height)
		{
			Utils.DrawSplicedPanel(spriteBatch, this._innerPanelTexture.Value, (int)position.X, (int)position.Y, (int)width, (int)height, 10, 10, 10, 10, Color.White);
		}

		// Token: 0x06003FBD RID: 16317 RVA: 0x005DB318 File Offset: 0x005D9518
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			CalculatedStyle innerDimensions = base.GetInnerDimensions();
			CalculatedStyle dimensions = this._worldIcon.GetDimensions();
			float num = dimensions.X + dimensions.Width;
			Color color = this._data.IsValid ? Color.White : Color.Gray;
			string worldName = this._data.GetWorldName(true);
			Utils.DrawBorderString(spriteBatch, worldName, new Vector2(num + 6f, innerDimensions.Y + 3f), color, 1f, 0f, 0f, -1);
			float num6 = (innerDimensions.Width - 22f - dimensions.Width - this._publishButton.GetDimensions().Width) / 2f;
			float height = this._publishButton.GetDimensions().Height;
			Vector2 vector;
			vector..ctor(num + 6f, innerDimensions.Y + innerDimensions.Height - height);
			float num2 = num6;
			this.DrawPanel(spriteBatch, vector, num2, height);
			string expertText = "";
			Color gameModeColor = Color.White;
			base.GetDifficulty(out expertText, out gameModeColor);
			Vector2 vector2 = FontAssets.MouseText.Value.MeasureString(expertText);
			float x = vector2.X;
			float y = vector2.Y;
			float x2 = num2 * 0.5f - x * 0.5f;
			float num3 = height * 0.5f - y * 0.5f;
			Utils.DrawBorderString(spriteBatch, expertText, vector + new Vector2(x2, num3 + 3f), gameModeColor, 1f, 0f, 0f, -1);
			vector.X += num2 + 5f;
			float num4 = num6;
			if (!GameCulture.FromCultureName(GameCulture.CultureName.English).IsActive)
			{
				num4 += 40f;
			}
			this.DrawPanel(spriteBatch, vector, num4, height);
			string textValue = Language.GetTextValue("UI.WorldSizeFormat", this._data.WorldSizeName);
			Vector2 vector3 = FontAssets.MouseText.Value.MeasureString(textValue);
			float x3 = vector3.X;
			float y2 = vector3.Y;
			float x4 = num4 * 0.5f - x3 * 0.5f;
			float num5 = height * 0.5f - y2 * 0.5f;
			Utils.DrawBorderString(spriteBatch, textValue, vector + new Vector2(x4, num5 + 3f), Color.White, 1f, 0f, 0f, -1);
			vector.X += num4 + 5f;
		}

		// Token: 0x04005807 RID: 22535
		private Asset<Texture2D> _workshopIconTexture;

		// Token: 0x04005808 RID: 22536
		private Asset<Texture2D> _innerPanelTexture;

		// Token: 0x04005809 RID: 22537
		private UIElement _worldIcon;

		// Token: 0x0400580A RID: 22538
		private UIElement _publishButton;

		// Token: 0x0400580B RID: 22539
		private int _orderInList;

		// Token: 0x0400580C RID: 22540
		private UIState _ownerState;
	}
}
