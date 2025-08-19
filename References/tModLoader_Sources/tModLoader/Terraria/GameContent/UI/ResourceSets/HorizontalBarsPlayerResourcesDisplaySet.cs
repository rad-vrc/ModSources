using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Terraria.GameContent.UI.ResourceSets
{
	// Token: 0x020004EF RID: 1263
	public class HorizontalBarsPlayerResourcesDisplaySet : IPlayerResourcesDisplaySet, IConfigKeyHolder
	{
		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06003D3F RID: 15679 RVA: 0x005C9817 File Offset: 0x005C7A17
		public string DisplayedName
		{
			get
			{
				return Language.GetTextValue("UI.HealthManaStyle_" + this.NameKey);
			}
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06003D40 RID: 15680 RVA: 0x005C982E File Offset: 0x005C7A2E
		// (set) Token: 0x06003D41 RID: 15681 RVA: 0x005C9836 File Offset: 0x005C7A36
		public string NameKey { get; private set; }

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06003D42 RID: 15682 RVA: 0x005C983F File Offset: 0x005C7A3F
		// (set) Token: 0x06003D43 RID: 15683 RVA: 0x005C9847 File Offset: 0x005C7A47
		public string ConfigKey { get; private set; }

		// Token: 0x06003D44 RID: 15684 RVA: 0x005C9850 File Offset: 0x005C7A50
		public HorizontalBarsPlayerResourcesDisplaySet(string nameKey, string configKey, string resourceFolderName, AssetRequestMode mode)
		{
			this.NameKey = nameKey;
			this.ConfigKey = configKey;
			if (configKey == "HorizontalBarsWithFullText")
			{
				this._drawTextStyle = 2;
			}
			else if (configKey == "HorizontalBarsWithText")
			{
				this._drawTextStyle = 1;
			}
			else
			{
				this._drawTextStyle = 0;
			}
			string text = "Images\\UI\\PlayerResourceSets\\" + resourceFolderName;
			this._hpFill = Main.Assets.Request<Texture2D>(text + "\\HP_Fill", mode);
			this._hpFillHoney = Main.Assets.Request<Texture2D>(text + "\\HP_Fill_Honey", mode);
			this._mpFill = Main.Assets.Request<Texture2D>(text + "\\MP_Fill", mode);
			this._panelLeft = Main.Assets.Request<Texture2D>(text + "\\Panel_Left", mode);
			this._panelMiddleHP = Main.Assets.Request<Texture2D>(text + "\\HP_Panel_Middle", mode);
			this._panelRightHP = Main.Assets.Request<Texture2D>(text + "\\HP_Panel_Right", mode);
			this._panelMiddleMP = Main.Assets.Request<Texture2D>(text + "\\MP_Panel_Middle", mode);
			this._panelRightMP = Main.Assets.Request<Texture2D>(text + "\\MP_Panel_Right", mode);
		}

		// Token: 0x06003D45 RID: 15685 RVA: 0x005C9998 File Offset: 0x005C7B98
		public void Draw()
		{
			this.PrepareFields(Main.LocalPlayer);
			SpriteBatch spriteBatch = Main.spriteBatch;
			int num = 16;
			int num2 = 18;
			int num3 = Main.screenWidth - 300 - 22 + num;
			if (this._drawTextStyle == 2)
			{
				num2 += 2;
				HorizontalBarsPlayerResourcesDisplaySet.DrawLifeBarText(spriteBatch, new Vector2((float)num3, (float)num2));
				HorizontalBarsPlayerResourcesDisplaySet.DrawManaText(spriteBatch);
			}
			else if (this._drawTextStyle == 1)
			{
				num2 += 4;
				HorizontalBarsPlayerResourcesDisplaySet.DrawLifeBarText(spriteBatch, new Vector2((float)num3, (float)num2));
			}
			Color color;
			color..ctor((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
			bool drawText;
			if (ResourceOverlayLoader.PreDrawResourceDisplay(this.preparedSnapshot, this, true, ref color, out drawText))
			{
				Vector2 vector;
				vector..ctor((float)num3, (float)num2);
				vector.X += (float)((this._maxSegmentCount - this._hpSegmentsCount) * this._panelMiddleHP.Width());
				bool isHovered = false;
				ResourceDrawSettings resourceDrawSettings = default(ResourceDrawSettings);
				resourceDrawSettings.ElementCount = this._hpSegmentsCount + 2;
				resourceDrawSettings.ElementIndexOffset = 0;
				resourceDrawSettings.TopLeftAnchor = vector;
				resourceDrawSettings.GetTextureMethod = new ResourceDrawSettings.TextureGetter(this.LifePanelDrawer);
				resourceDrawSettings.OffsetPerDraw = Vector2.Zero;
				resourceDrawSettings.OffsetPerDrawByTexturePercentile = Vector2.UnitX;
				resourceDrawSettings.OffsetSpriteAnchor = Vector2.Zero;
				resourceDrawSettings.OffsetSpriteAnchorByTexturePercentile = Vector2.Zero;
				resourceDrawSettings.StatsSnapshot = this.preparedSnapshot;
				resourceDrawSettings.DisplaySet = this;
				resourceDrawSettings.ResourceIndexOffset = -1;
				resourceDrawSettings.Draw(spriteBatch, ref isHovered);
				resourceDrawSettings = default(ResourceDrawSettings);
				resourceDrawSettings.ElementCount = this._hpSegmentsCount;
				resourceDrawSettings.ElementIndexOffset = 0;
				resourceDrawSettings.TopLeftAnchor = vector + new Vector2(6f, 6f);
				resourceDrawSettings.GetTextureMethod = new ResourceDrawSettings.TextureGetter(this.LifeFillingDrawer);
				resourceDrawSettings.OffsetPerDraw = new Vector2((float)this._hpFill.Width(), 0f);
				resourceDrawSettings.OffsetPerDrawByTexturePercentile = Vector2.Zero;
				resourceDrawSettings.OffsetSpriteAnchor = Vector2.Zero;
				resourceDrawSettings.OffsetSpriteAnchorByTexturePercentile = Vector2.Zero;
				resourceDrawSettings.StatsSnapshot = this.preparedSnapshot;
				resourceDrawSettings.DisplaySet = this;
				resourceDrawSettings.Draw(spriteBatch, ref isHovered);
				this._hpHovered = isHovered;
			}
			ResourceOverlayLoader.PostDrawResourceDisplay(this.preparedSnapshot, this, true, color, drawText);
			color..ctor((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
			if (ResourceOverlayLoader.PreDrawResourceDisplay(this.preparedSnapshot, this, false, ref color, out drawText))
			{
				bool isHovered = false;
				Vector2 vector2;
				vector2..ctor((float)(num3 - 10), (float)(num2 + 24));
				vector2.X += (float)((this._maxSegmentCount - this._mpSegmentsCount) * this._panelMiddleMP.Width());
				ResourceDrawSettings resourceDrawSettings = default(ResourceDrawSettings);
				resourceDrawSettings.ElementCount = this._mpSegmentsCount + 2;
				resourceDrawSettings.ElementIndexOffset = 0;
				resourceDrawSettings.TopLeftAnchor = vector2;
				resourceDrawSettings.GetTextureMethod = new ResourceDrawSettings.TextureGetter(this.ManaPanelDrawer);
				resourceDrawSettings.OffsetPerDraw = Vector2.Zero;
				resourceDrawSettings.OffsetPerDrawByTexturePercentile = Vector2.UnitX;
				resourceDrawSettings.OffsetSpriteAnchor = Vector2.Zero;
				resourceDrawSettings.OffsetSpriteAnchorByTexturePercentile = Vector2.Zero;
				resourceDrawSettings.StatsSnapshot = this.preparedSnapshot;
				resourceDrawSettings.DisplaySet = this;
				resourceDrawSettings.ResourceIndexOffset = -1;
				resourceDrawSettings.Draw(spriteBatch, ref isHovered);
				resourceDrawSettings = default(ResourceDrawSettings);
				resourceDrawSettings.ElementCount = this._mpSegmentsCount;
				resourceDrawSettings.ElementIndexOffset = 0;
				resourceDrawSettings.TopLeftAnchor = vector2 + new Vector2(6f, 6f);
				resourceDrawSettings.GetTextureMethod = new ResourceDrawSettings.TextureGetter(this.ManaFillingDrawer);
				resourceDrawSettings.OffsetPerDraw = new Vector2((float)this._mpFill.Width(), 0f);
				resourceDrawSettings.OffsetPerDrawByTexturePercentile = Vector2.Zero;
				resourceDrawSettings.OffsetSpriteAnchor = Vector2.Zero;
				resourceDrawSettings.OffsetSpriteAnchorByTexturePercentile = Vector2.Zero;
				resourceDrawSettings.StatsSnapshot = this.preparedSnapshot;
				resourceDrawSettings.DisplaySet = this;
				resourceDrawSettings.Draw(spriteBatch, ref isHovered);
				this._mpHovered = isHovered;
			}
			ResourceOverlayLoader.PostDrawResourceDisplay(this.preparedSnapshot, this, false, color, drawText);
		}

		// Token: 0x06003D46 RID: 15686 RVA: 0x005C9D8C File Offset: 0x005C7F8C
		private static void DrawManaText(SpriteBatch spriteBatch)
		{
			Color color;
			color..ctor((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
			int num = 180;
			Player localPlayer = Main.LocalPlayer;
			string text = Lang.inter[2].Value + ":";
			string text2 = localPlayer.statMana.ToString() + "/" + localPlayer.statManaMax2.ToString();
			Vector2 vector;
			vector..ctor((float)(Main.screenWidth - num), 65f);
			string text3 = text + " " + text2;
			Vector2 vector2 = FontAssets.MouseText.Value.MeasureString(text3);
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, text, vector + new Vector2((0f - vector2.X) * 0.5f, 0f), color, 0f, default(Vector2), 1f, 0, 0f);
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, text2, vector + new Vector2(vector2.X * 0.5f, 0f), color, 0f, new Vector2(FontAssets.MouseText.Value.MeasureString(text2).X, 0f), 1f, 0, 0f);
		}

		// Token: 0x06003D47 RID: 15687 RVA: 0x005C9EE0 File Offset: 0x005C80E0
		private static void DrawLifeBarText(SpriteBatch spriteBatch, Vector2 topLeftAnchor)
		{
			Vector2 vector = topLeftAnchor + new Vector2(130f, -20f);
			Player localPlayer = Main.LocalPlayer;
			Color color;
			color..ctor((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
			string text = string.Concat(new string[]
			{
				Lang.inter[0].Value,
				" ",
				localPlayer.statLifeMax2.ToString(),
				"/",
				localPlayer.statLifeMax2.ToString()
			});
			Vector2 vector2 = FontAssets.MouseText.Value.MeasureString(text);
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, Lang.inter[0].Value, vector + new Vector2((0f - vector2.X) * 0.5f, 0f), color, 0f, default(Vector2), 1f, 0, 0f);
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, localPlayer.statLife.ToString() + "/" + localPlayer.statLifeMax2.ToString(), vector + new Vector2(vector2.X * 0.5f, 0f), color, 0f, new Vector2(FontAssets.MouseText.Value.MeasureString(localPlayer.statLife.ToString() + "/" + localPlayer.statLifeMax2.ToString()).X, 0f), 1f, 0, 0f);
		}

		// Token: 0x06003D48 RID: 15688 RVA: 0x005CA074 File Offset: 0x005C8274
		private void PrepareFields(Player player)
		{
			PlayerStatsSnapshot playerStatsSnapshot = new PlayerStatsSnapshot(player);
			this._hpSegmentsCount = playerStatsSnapshot.AmountOfLifeHearts;
			this._mpSegmentsCount = playerStatsSnapshot.AmountOfManaStars;
			this._maxSegmentCount = 20;
			this._hpFruitCount = playerStatsSnapshot.LifeFruitCount;
			this._hpPercent = (float)playerStatsSnapshot.Life / (float)playerStatsSnapshot.LifeMax;
			this._mpPercent = (float)playerStatsSnapshot.Mana / (float)playerStatsSnapshot.ManaMax;
			this.preparedSnapshot = playerStatsSnapshot;
		}

		// Token: 0x06003D49 RID: 15689 RVA: 0x005CA0E8 File Offset: 0x005C82E8
		private void LifePanelDrawer(int elementIndex, int firstElementIndex, int lastElementIndex, out Asset<Texture2D> sprite, out Vector2 offset, out float drawScale, out Rectangle? sourceRect)
		{
			sourceRect = null;
			offset = Vector2.Zero;
			sprite = this._panelLeft;
			drawScale = 1f;
			if (elementIndex == lastElementIndex)
			{
				sprite = this._panelRightHP;
				offset = new Vector2(-16f, -10f);
				return;
			}
			if (elementIndex != firstElementIndex)
			{
				sprite = this._panelMiddleHP;
				int drawIndexOffset = lastElementIndex - (elementIndex - firstElementIndex) - elementIndex;
				offset.X = (float)(drawIndexOffset * this._panelMiddleHP.Width());
			}
		}

		// Token: 0x06003D4A RID: 15690 RVA: 0x005CA168 File Offset: 0x005C8368
		private void ManaPanelDrawer(int elementIndex, int firstElementIndex, int lastElementIndex, out Asset<Texture2D> sprite, out Vector2 offset, out float drawScale, out Rectangle? sourceRect)
		{
			sourceRect = null;
			offset = Vector2.Zero;
			sprite = this._panelLeft;
			drawScale = 1f;
			if (elementIndex == lastElementIndex)
			{
				sprite = this._panelRightMP;
				offset = new Vector2(-16f, -6f);
				return;
			}
			if (elementIndex != firstElementIndex)
			{
				sprite = this._panelMiddleMP;
				int drawIndexOffset = lastElementIndex - (elementIndex - firstElementIndex) - elementIndex;
				offset.X = (float)(drawIndexOffset * this._panelMiddleMP.Width());
			}
		}

		// Token: 0x06003D4B RID: 15691 RVA: 0x005CA1E8 File Offset: 0x005C83E8
		private void LifeFillingDrawer(int elementIndex, int firstElementIndex, int lastElementIndex, out Asset<Texture2D> sprite, out Vector2 offset, out float drawScale, out Rectangle? sourceRect)
		{
			sprite = this._hpFill;
			if (elementIndex < this._hpFruitCount)
			{
				sprite = this._hpFillHoney;
			}
			HorizontalBarsPlayerResourcesDisplaySet.FillBarByValues(elementIndex, sprite, this._hpSegmentsCount, this._hpPercent, out offset, out drawScale, out sourceRect);
			int drawIndexOffset = lastElementIndex - (elementIndex - firstElementIndex) - elementIndex;
			offset.X += (float)(drawIndexOffset * sprite.Width());
		}

		// Token: 0x06003D4C RID: 15692 RVA: 0x005CA24C File Offset: 0x005C844C
		public static void FillBarByValues(int elementIndex, Asset<Texture2D> sprite, int segmentsCount, float fillPercent, out Vector2 offset, out float drawScale, out Rectangle? sourceRect)
		{
			sourceRect = null;
			offset = Vector2.Zero;
			float num2 = 1f / (float)segmentsCount;
			float num3 = Utils.GetLerpValue(num2 * (float)elementIndex, num2 * (float)(elementIndex + 1), fillPercent, true);
			drawScale = 1f;
			Rectangle value = sprite.Frame(1, 1, 0, 0, 0, 0);
			int num4 = (int)((float)value.Width * (1f - num3));
			offset.X += (float)num4;
			value.X += num4;
			value.Width -= num4;
			sourceRect = new Rectangle?(value);
		}

		// Token: 0x06003D4D RID: 15693 RVA: 0x005CA2EC File Offset: 0x005C84EC
		private void ManaFillingDrawer(int elementIndex, int firstElementIndex, int lastElementIndex, out Asset<Texture2D> sprite, out Vector2 offset, out float drawScale, out Rectangle? sourceRect)
		{
			sprite = this._mpFill;
			HorizontalBarsPlayerResourcesDisplaySet.FillBarByValues(elementIndex, sprite, this._mpSegmentsCount, this._mpPercent, out offset, out drawScale, out sourceRect);
			int drawIndexOffset = lastElementIndex - (elementIndex - firstElementIndex) - elementIndex;
			offset.X += (float)(drawIndexOffset * sprite.Width());
		}

		// Token: 0x06003D4E RID: 15694 RVA: 0x005CA33B File Offset: 0x005C853B
		public void TryToHover()
		{
			if (this._hpHovered)
			{
				CommonResourceBarMethods.DrawLifeMouseOver();
			}
			if (this._mpHovered)
			{
				CommonResourceBarMethods.DrawManaMouseOver();
			}
		}

		// Token: 0x0400561F RID: 22047
		private int _maxSegmentCount;

		// Token: 0x04005620 RID: 22048
		private int _hpSegmentsCount;

		// Token: 0x04005621 RID: 22049
		private int _mpSegmentsCount;

		// Token: 0x04005622 RID: 22050
		private int _hpFruitCount;

		// Token: 0x04005623 RID: 22051
		private float _hpPercent;

		// Token: 0x04005624 RID: 22052
		private float _mpPercent;

		// Token: 0x04005625 RID: 22053
		private byte _drawTextStyle;

		// Token: 0x04005626 RID: 22054
		private bool _hpHovered;

		// Token: 0x04005627 RID: 22055
		private bool _mpHovered;

		// Token: 0x04005628 RID: 22056
		private Asset<Texture2D> _hpFill;

		// Token: 0x04005629 RID: 22057
		private Asset<Texture2D> _hpFillHoney;

		// Token: 0x0400562A RID: 22058
		private Asset<Texture2D> _mpFill;

		// Token: 0x0400562B RID: 22059
		private Asset<Texture2D> _panelLeft;

		// Token: 0x0400562C RID: 22060
		private Asset<Texture2D> _panelMiddleHP;

		// Token: 0x0400562D RID: 22061
		private Asset<Texture2D> _panelRightHP;

		// Token: 0x0400562E RID: 22062
		private Asset<Texture2D> _panelMiddleMP;

		// Token: 0x0400562F RID: 22063
		private Asset<Texture2D> _panelRightMP;

		// Token: 0x04005632 RID: 22066
		private PlayerStatsSnapshot preparedSnapshot;
	}
}
