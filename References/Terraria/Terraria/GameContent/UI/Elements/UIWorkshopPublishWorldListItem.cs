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
	// Token: 0x02000370 RID: 880
	public class UIWorkshopPublishWorldListItem : AWorldListItem
	{
		// Token: 0x0600284E RID: 10318 RVA: 0x00589B08 File Offset: 0x00587D08
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

		// Token: 0x0600284F RID: 10319 RVA: 0x00589C41 File Offset: 0x00587E41
		private void LoadTextures()
		{
			this._innerPanelTexture = Main.Assets.Request<Texture2D>("Images/UI/InnerPanelBackground", 1);
			this._workshopIconTexture = TextureAssets.Extra[243];
		}

		// Token: 0x06002850 RID: 10320 RVA: 0x00589C6A File Offset: 0x00587E6A
		private void InitializeAppearance()
		{
			this.Height.Set(82f, 0f);
			this.Width.Set(0f, 1f);
			base.SetPadding(6f);
			this.SetColorsToNotHovered();
		}

		// Token: 0x06002851 RID: 10321 RVA: 0x00588D50 File Offset: 0x00586F50
		private void SetColorsToHovered()
		{
			this.BackgroundColor = new Color(73, 94, 171);
			this.BorderColor = new Color(89, 116, 213);
		}

		// Token: 0x06002852 RID: 10322 RVA: 0x00588D7A File Offset: 0x00586F7A
		private void SetColorsToNotHovered()
		{
			this.BackgroundColor = new Color(63, 82, 151) * 0.7f;
			this.BorderColor = new Color(89, 116, 213) * 0.7f;
		}

		// Token: 0x06002853 RID: 10323 RVA: 0x00589CA7 File Offset: 0x00587EA7
		private void PublishButtonClick_ImportWorldToLocalFiles(UIMouseEvent evt, UIElement listeningElement)
		{
			if (listeningElement != evt.Target)
			{
				return;
			}
			Main.MenuUI.SetState(new WorkshopPublishInfoStateForWorld(this._ownerState, this._data));
		}

		// Token: 0x06002854 RID: 10324 RVA: 0x00589CD0 File Offset: 0x00587ED0
		public override int CompareTo(object obj)
		{
			UIWorkshopPublishWorldListItem uiworkshopPublishWorldListItem = obj as UIWorkshopPublishWorldListItem;
			if (uiworkshopPublishWorldListItem != null)
			{
				return this._orderInList.CompareTo(uiworkshopPublishWorldListItem._orderInList);
			}
			return base.CompareTo(obj);
		}

		// Token: 0x06002855 RID: 10325 RVA: 0x00589D00 File Offset: 0x00587F00
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			this.SetColorsToHovered();
		}

		// Token: 0x06002856 RID: 10326 RVA: 0x00589D0F File Offset: 0x00587F0F
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this.SetColorsToNotHovered();
		}

		// Token: 0x06002857 RID: 10327 RVA: 0x00589D20 File Offset: 0x00587F20
		private void DrawPanel(SpriteBatch spriteBatch, Vector2 position, float width, float height)
		{
			Utils.DrawSplicedPanel(spriteBatch, this._innerPanelTexture.Value, (int)position.X, (int)position.Y, (int)width, (int)height, 10, 10, 10, 10, Color.White);
		}

		// Token: 0x06002858 RID: 10328 RVA: 0x00589D60 File Offset: 0x00587F60
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			CalculatedStyle innerDimensions = base.GetInnerDimensions();
			CalculatedStyle dimensions = this._worldIcon.GetDimensions();
			float num = dimensions.X + dimensions.Width;
			Color color = this._data.IsValid ? Color.White : Color.Gray;
			string worldName = this._data.GetWorldName(true);
			Utils.DrawBorderString(spriteBatch, worldName, new Vector2(num + 6f, innerDimensions.Y + 3f), color, 1f, 0f, 0f, -1);
			float num2 = (innerDimensions.Width - 22f - dimensions.Width - this._publishButton.GetDimensions().Width) / 2f;
			float height = this._publishButton.GetDimensions().Height;
			Vector2 vector = new Vector2(num + 6f, innerDimensions.Y + innerDimensions.Height - height);
			float num3 = num2;
			this.DrawPanel(spriteBatch, vector, num3, height);
			string text = "";
			Color white = Color.White;
			base.GetDifficulty(out text, out white);
			Vector2 vector2 = FontAssets.MouseText.Value.MeasureString(text);
			float x = vector2.X;
			float y = vector2.Y;
			float x2 = num3 * 0.5f - x * 0.5f;
			float num4 = height * 0.5f - y * 0.5f;
			Utils.DrawBorderString(spriteBatch, text, vector + new Vector2(x2, num4 + 3f), white, 1f, 0f, 0f, -1);
			vector.X += num3 + 5f;
			float num5 = num2;
			if (!GameCulture.FromCultureName(GameCulture.CultureName.English).IsActive)
			{
				num5 += 40f;
			}
			this.DrawPanel(spriteBatch, vector, num5, height);
			string textValue = Language.GetTextValue("UI.WorldSizeFormat", this._data.WorldSizeName);
			Vector2 vector3 = FontAssets.MouseText.Value.MeasureString(textValue);
			float x3 = vector3.X;
			float y2 = vector3.Y;
			float x4 = num5 * 0.5f - x3 * 0.5f;
			float num6 = height * 0.5f - y2 * 0.5f;
			Utils.DrawBorderString(spriteBatch, textValue, vector + new Vector2(x4, num6 + 3f), Color.White, 1f, 0f, 0f, -1);
			vector.X += num5 + 5f;
		}

		// Token: 0x04004B6C RID: 19308
		private Asset<Texture2D> _workshopIconTexture;

		// Token: 0x04004B6D RID: 19309
		private Asset<Texture2D> _innerPanelTexture;

		// Token: 0x04004B6E RID: 19310
		private UIElement _worldIcon;

		// Token: 0x04004B6F RID: 19311
		private UIElement _publishButton;

		// Token: 0x04004B70 RID: 19312
		private int _orderInList;

		// Token: 0x04004B71 RID: 19313
		private UIState _ownerState;
	}
}
