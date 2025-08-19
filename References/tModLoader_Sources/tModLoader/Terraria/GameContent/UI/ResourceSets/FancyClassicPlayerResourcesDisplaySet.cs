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
	// Token: 0x020004EE RID: 1262
	public class FancyClassicPlayerResourcesDisplaySet : IPlayerResourcesDisplaySet, IConfigKeyHolder
	{
		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06003D2E RID: 15662 RVA: 0x005C8C00 File Offset: 0x005C6E00
		public string DisplayedName
		{
			get
			{
				return Language.GetTextValue("UI.HealthManaStyle_" + this.NameKey);
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06003D2F RID: 15663 RVA: 0x005C8C17 File Offset: 0x005C6E17
		// (set) Token: 0x06003D30 RID: 15664 RVA: 0x005C8C1F File Offset: 0x005C6E1F
		public string NameKey { get; private set; }

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06003D31 RID: 15665 RVA: 0x005C8C28 File Offset: 0x005C6E28
		// (set) Token: 0x06003D32 RID: 15666 RVA: 0x005C8C30 File Offset: 0x005C6E30
		public string ConfigKey { get; private set; }

		// Token: 0x06003D33 RID: 15667 RVA: 0x005C8C3C File Offset: 0x005C6E3C
		public FancyClassicPlayerResourcesDisplaySet(string nameKey, string configKey, string resourceFolderName, AssetRequestMode mode)
		{
			this.NameKey = nameKey;
			this.ConfigKey = configKey;
			if (configKey == "NewWithText")
			{
				this._drawText = true;
			}
			else
			{
				this._drawText = false;
			}
			string text = "Images\\UI\\PlayerResourceSets\\" + resourceFolderName;
			this._heartLeft = Main.Assets.Request<Texture2D>(text + "\\Heart_Left", mode);
			this._heartMiddle = Main.Assets.Request<Texture2D>(text + "\\Heart_Middle", mode);
			this._heartRight = Main.Assets.Request<Texture2D>(text + "\\Heart_Right", mode);
			this._heartRightFancy = Main.Assets.Request<Texture2D>(text + "\\Heart_Right_Fancy", mode);
			this._heartFill = Main.Assets.Request<Texture2D>(text + "\\Heart_Fill", mode);
			this._heartFillHoney = Main.Assets.Request<Texture2D>(text + "\\Heart_Fill_B", mode);
			this._heartSingleFancy = Main.Assets.Request<Texture2D>(text + "\\Heart_Single_Fancy", mode);
			this._starTop = Main.Assets.Request<Texture2D>(text + "\\Star_A", mode);
			this._starMiddle = Main.Assets.Request<Texture2D>(text + "\\Star_B", mode);
			this._starBottom = Main.Assets.Request<Texture2D>(text + "\\Star_C", mode);
			this._starSingle = Main.Assets.Request<Texture2D>(text + "\\Star_Single", mode);
			this._starFill = Main.Assets.Request<Texture2D>(text + "\\Star_Fill", mode);
		}

		// Token: 0x06003D34 RID: 15668 RVA: 0x005C8DE4 File Offset: 0x005C6FE4
		public void Draw()
		{
			Player localPlayer = Main.LocalPlayer;
			SpriteBatch spriteBatch = Main.spriteBatch;
			this.PrepareFields(localPlayer);
			this.DrawLifeBar(spriteBatch);
			this.DrawManaBar(spriteBatch);
		}

		// Token: 0x06003D35 RID: 15669 RVA: 0x005C8E14 File Offset: 0x005C7014
		private void DrawLifeBar(SpriteBatch spriteBatch)
		{
			Color color;
			color..ctor((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
			bool drawText;
			if (ResourceOverlayLoader.PreDrawResourceDisplay(this.preparedSnapshot, this, true, ref color, out drawText))
			{
				Vector2 vector;
				vector..ctor((float)(Main.screenWidth - 300 + 4), 15f);
				if (this._drawText)
				{
					vector.Y += 6f;
					FancyClassicPlayerResourcesDisplaySet.DrawLifeBarText(spriteBatch, vector + new Vector2(-4f, 3f));
				}
				bool isHovered = false;
				ResourceDrawSettings resourceDrawSettings = this.defaultResourceDrawSettings;
				resourceDrawSettings.ElementCount = this._heartCountRow1;
				resourceDrawSettings.ElementIndexOffset = 0;
				resourceDrawSettings.TopLeftAnchor = vector;
				resourceDrawSettings.GetTextureMethod = new ResourceDrawSettings.TextureGetter(this.HeartPanelDrawer);
				resourceDrawSettings.OffsetPerDraw = Vector2.Zero;
				resourceDrawSettings.OffsetPerDrawByTexturePercentile = Vector2.UnitX;
				resourceDrawSettings.OffsetSpriteAnchor = Vector2.Zero;
				resourceDrawSettings.OffsetSpriteAnchorByTexturePercentile = Vector2.Zero;
				resourceDrawSettings.Draw(spriteBatch, ref isHovered);
				resourceDrawSettings = this.defaultResourceDrawSettings;
				resourceDrawSettings.ElementCount = this._heartCountRow2;
				resourceDrawSettings.ElementIndexOffset = 10;
				resourceDrawSettings.TopLeftAnchor = vector + new Vector2(0f, 28f);
				resourceDrawSettings.GetTextureMethod = new ResourceDrawSettings.TextureGetter(this.HeartPanelDrawer);
				resourceDrawSettings.OffsetPerDraw = Vector2.Zero;
				resourceDrawSettings.OffsetPerDrawByTexturePercentile = Vector2.UnitX;
				resourceDrawSettings.OffsetSpriteAnchor = Vector2.Zero;
				resourceDrawSettings.OffsetSpriteAnchorByTexturePercentile = Vector2.Zero;
				resourceDrawSettings.Draw(spriteBatch, ref isHovered);
				resourceDrawSettings = this.defaultResourceDrawSettings;
				resourceDrawSettings.ElementCount = this._heartCountRow1;
				resourceDrawSettings.ElementIndexOffset = 0;
				resourceDrawSettings.TopLeftAnchor = vector + new Vector2(15f, 15f);
				resourceDrawSettings.GetTextureMethod = new ResourceDrawSettings.TextureGetter(this.HeartFillingDrawer);
				resourceDrawSettings.OffsetPerDraw = Vector2.UnitX * 2f;
				resourceDrawSettings.OffsetPerDrawByTexturePercentile = Vector2.UnitX;
				resourceDrawSettings.OffsetSpriteAnchor = Vector2.Zero;
				resourceDrawSettings.OffsetSpriteAnchorByTexturePercentile = new Vector2(0.5f, 0.5f);
				resourceDrawSettings.Draw(spriteBatch, ref isHovered);
				resourceDrawSettings = this.defaultResourceDrawSettings;
				resourceDrawSettings.ElementCount = this._heartCountRow2;
				resourceDrawSettings.ElementIndexOffset = 10;
				resourceDrawSettings.TopLeftAnchor = vector + new Vector2(15f, 15f) + new Vector2(0f, 28f);
				resourceDrawSettings.GetTextureMethod = new ResourceDrawSettings.TextureGetter(this.HeartFillingDrawer);
				resourceDrawSettings.OffsetPerDraw = Vector2.UnitX * 2f;
				resourceDrawSettings.OffsetPerDrawByTexturePercentile = Vector2.UnitX;
				resourceDrawSettings.OffsetSpriteAnchor = Vector2.Zero;
				resourceDrawSettings.OffsetSpriteAnchorByTexturePercentile = new Vector2(0.5f, 0.5f);
				resourceDrawSettings.Draw(spriteBatch, ref isHovered);
				this._hoverLife = (isHovered && ResourceOverlayLoader.DisplayHoverText(this.preparedSnapshot, this, true));
			}
			ResourceOverlayLoader.PostDrawResourceDisplay(this.preparedSnapshot, this, true, color, drawText);
		}

		// Token: 0x06003D36 RID: 15670 RVA: 0x005C910C File Offset: 0x005C730C
		private static void DrawLifeBarText(SpriteBatch spriteBatch, Vector2 topLeftAnchor)
		{
			Vector2 vector = topLeftAnchor + new Vector2(130f, -24f);
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

		// Token: 0x06003D37 RID: 15671 RVA: 0x005C92A0 File Offset: 0x005C74A0
		private void DrawManaBar(SpriteBatch spriteBatch)
		{
			Color color;
			color..ctor((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
			bool drawText;
			if (ResourceOverlayLoader.PreDrawResourceDisplay(this.preparedSnapshot, this, false, ref color, out drawText))
			{
				Vector2 vector;
				vector..ctor((float)(Main.screenWidth - 40), 22f);
				int starCount = this._starCount;
				bool isHovered = false;
				ResourceDrawSettings resourceDrawSettings = this.defaultResourceDrawSettings;
				resourceDrawSettings.ElementCount = this._starCount;
				resourceDrawSettings.ElementIndexOffset = 0;
				resourceDrawSettings.TopLeftAnchor = vector;
				resourceDrawSettings.GetTextureMethod = new ResourceDrawSettings.TextureGetter(this.StarPanelDrawer);
				resourceDrawSettings.OffsetPerDraw = Vector2.Zero;
				resourceDrawSettings.OffsetPerDrawByTexturePercentile = Vector2.UnitY;
				resourceDrawSettings.OffsetSpriteAnchor = Vector2.Zero;
				resourceDrawSettings.OffsetSpriteAnchorByTexturePercentile = Vector2.Zero;
				resourceDrawSettings.Draw(spriteBatch, ref isHovered);
				resourceDrawSettings = this.defaultResourceDrawSettings;
				resourceDrawSettings.ElementCount = this._starCount;
				resourceDrawSettings.ElementIndexOffset = 0;
				resourceDrawSettings.TopLeftAnchor = vector + new Vector2(15f, 16f);
				resourceDrawSettings.GetTextureMethod = new ResourceDrawSettings.TextureGetter(this.StarFillingDrawer);
				resourceDrawSettings.OffsetPerDraw = Vector2.UnitY * -2f;
				resourceDrawSettings.OffsetPerDrawByTexturePercentile = Vector2.UnitY;
				resourceDrawSettings.OffsetSpriteAnchor = Vector2.Zero;
				resourceDrawSettings.OffsetSpriteAnchorByTexturePercentile = new Vector2(0.5f, 0.5f);
				resourceDrawSettings.Draw(spriteBatch, ref isHovered);
				this._hoverMana = (isHovered && ResourceOverlayLoader.DisplayHoverText(this.preparedSnapshot, this, false));
			}
			ResourceOverlayLoader.PostDrawResourceDisplay(this.preparedSnapshot, this, false, color, drawText);
		}

		// Token: 0x06003D38 RID: 15672 RVA: 0x005C9430 File Offset: 0x005C7630
		private static void DrawManaText(SpriteBatch spriteBatch)
		{
			Vector2 vector = FontAssets.MouseText.Value.MeasureString(Lang.inter[2].Value);
			Color color;
			color..ctor((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
			int num = 50;
			if (vector.X >= 45f)
			{
				num = (int)vector.X + 5;
			}
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, Lang.inter[2].Value, new Vector2((float)(Main.screenWidth - num), 6f), color, 0f, default(Vector2), 1f, 0, 0f);
		}

		// Token: 0x06003D39 RID: 15673 RVA: 0x005C94D8 File Offset: 0x005C76D8
		private void HeartPanelDrawer(int elementIndex, int firstElementIndex, int lastElementIndex, out Asset<Texture2D> sprite, out Vector2 offset, out float drawScale, out Rectangle? sourceRect)
		{
			sourceRect = null;
			offset = Vector2.Zero;
			sprite = this._heartLeft;
			drawScale = 1f;
			if (elementIndex == lastElementIndex && elementIndex == firstElementIndex)
			{
				sprite = this._heartSingleFancy;
				offset = new Vector2(-4f, -4f);
				return;
			}
			if (elementIndex == lastElementIndex && lastElementIndex == this._lastHeartPanelIndex)
			{
				sprite = this._heartRightFancy;
				offset = new Vector2(-8f, -4f);
				return;
			}
			if (elementIndex == lastElementIndex)
			{
				sprite = this._heartRight;
				return;
			}
			if (elementIndex != firstElementIndex)
			{
				sprite = this._heartMiddle;
			}
		}

		// Token: 0x06003D3A RID: 15674 RVA: 0x005C957C File Offset: 0x005C777C
		private void HeartFillingDrawer(int elementIndex, int firstElementIndex, int lastElementIndex, out Asset<Texture2D> sprite, out Vector2 offset, out float drawScale, out Rectangle? sourceRect)
		{
			sourceRect = null;
			offset = Vector2.Zero;
			sprite = this._heartLeft;
			if (elementIndex < this._playerLifeFruitCount)
			{
				sprite = this._heartFillHoney;
			}
			else
			{
				sprite = this._heartFill;
			}
			float num = drawScale = Utils.GetLerpValue(this._lifePerHeart * (float)elementIndex, this._lifePerHeart * (float)(elementIndex + 1), this._currentPlayerLife, true);
			if (elementIndex == this._lastHeartFillingIndex && num > 0f)
			{
				drawScale += Main.cursorScale - 1f;
			}
		}

		// Token: 0x06003D3B RID: 15675 RVA: 0x005C9610 File Offset: 0x005C7810
		private void StarPanelDrawer(int elementIndex, int firstElementIndex, int lastElementIndex, out Asset<Texture2D> sprite, out Vector2 offset, out float drawScale, out Rectangle? sourceRect)
		{
			sourceRect = null;
			offset = Vector2.Zero;
			sprite = this._starTop;
			drawScale = 1f;
			if (elementIndex == lastElementIndex && elementIndex == firstElementIndex)
			{
				sprite = this._starSingle;
				return;
			}
			if (elementIndex == lastElementIndex)
			{
				sprite = this._starBottom;
				offset = new Vector2(0f, 0f);
				return;
			}
			if (elementIndex != firstElementIndex)
			{
				sprite = this._starMiddle;
			}
		}

		// Token: 0x06003D3C RID: 15676 RVA: 0x005C9688 File Offset: 0x005C7888
		private void StarFillingDrawer(int elementIndex, int firstElementIndex, int lastElementIndex, out Asset<Texture2D> sprite, out Vector2 offset, out float drawScale, out Rectangle? sourceRect)
		{
			sourceRect = null;
			offset = Vector2.Zero;
			sprite = this._starFill;
			float num = drawScale = Utils.GetLerpValue(this._manaPerStar * (float)elementIndex, this._manaPerStar * (float)(elementIndex + 1), this._currentPlayerMana, true);
			if (elementIndex == this._lastStarFillingIndex && num > 0f)
			{
				drawScale += Main.cursorScale - 1f;
			}
		}

		// Token: 0x06003D3D RID: 15677 RVA: 0x005C96FC File Offset: 0x005C78FC
		private void PrepareFields(Player player)
		{
			PlayerStatsSnapshot playerStatsSnapshot = new PlayerStatsSnapshot(player);
			this._playerLifeFruitCount = playerStatsSnapshot.LifeFruitCount;
			this._lifePerHeart = playerStatsSnapshot.LifePerSegment;
			this._currentPlayerLife = (float)playerStatsSnapshot.Life;
			this._manaPerStar = playerStatsSnapshot.ManaPerSegment;
			this._heartCountRow1 = Utils.Clamp<int>(playerStatsSnapshot.AmountOfLifeHearts, 0, 10);
			this._heartCountRow2 = Utils.Clamp<int>(playerStatsSnapshot.AmountOfLifeHearts - 10, 0, 10);
			int lastHeartFillingIndex = (int)((float)playerStatsSnapshot.Life / this._lifePerHeart);
			this._lastHeartFillingIndex = lastHeartFillingIndex;
			this._lastHeartPanelIndex = this._heartCountRow1 + this._heartCountRow2 - 1;
			this._starCount = playerStatsSnapshot.AmountOfManaStars;
			this._currentPlayerMana = (float)playerStatsSnapshot.Mana;
			this._lastStarFillingIndex = (int)(this._currentPlayerMana / this._manaPerStar);
			this.preparedSnapshot = playerStatsSnapshot;
			this.defaultResourceDrawSettings = default(ResourceDrawSettings);
			this.defaultResourceDrawSettings.StatsSnapshot = this.preparedSnapshot;
			this.defaultResourceDrawSettings.DisplaySet = this;
		}

		// Token: 0x06003D3E RID: 15678 RVA: 0x005C97FB File Offset: 0x005C79FB
		public void TryToHover()
		{
			if (this._hoverLife)
			{
				CommonResourceBarMethods.DrawLifeMouseOver();
			}
			if (this._hoverMana)
			{
				CommonResourceBarMethods.DrawManaMouseOver();
			}
		}

		// Token: 0x04005601 RID: 22017
		private PlayerStatsSnapshot preparedSnapshot;

		// Token: 0x04005602 RID: 22018
		private ResourceDrawSettings defaultResourceDrawSettings;

		// Token: 0x04005603 RID: 22019
		private float _currentPlayerLife;

		// Token: 0x04005604 RID: 22020
		private float _lifePerHeart;

		// Token: 0x04005605 RID: 22021
		private int _playerLifeFruitCount;

		// Token: 0x04005606 RID: 22022
		private int _lastHeartFillingIndex;

		// Token: 0x04005607 RID: 22023
		private int _lastHeartPanelIndex;

		// Token: 0x04005608 RID: 22024
		private int _heartCountRow1;

		// Token: 0x04005609 RID: 22025
		private int _heartCountRow2;

		// Token: 0x0400560A RID: 22026
		private int _starCount;

		// Token: 0x0400560B RID: 22027
		private int _lastStarFillingIndex;

		// Token: 0x0400560C RID: 22028
		private float _manaPerStar;

		// Token: 0x0400560D RID: 22029
		private float _currentPlayerMana;

		// Token: 0x0400560E RID: 22030
		private Asset<Texture2D> _heartLeft;

		// Token: 0x0400560F RID: 22031
		private Asset<Texture2D> _heartMiddle;

		// Token: 0x04005610 RID: 22032
		private Asset<Texture2D> _heartRight;

		// Token: 0x04005611 RID: 22033
		private Asset<Texture2D> _heartRightFancy;

		// Token: 0x04005612 RID: 22034
		private Asset<Texture2D> _heartFill;

		// Token: 0x04005613 RID: 22035
		private Asset<Texture2D> _heartFillHoney;

		// Token: 0x04005614 RID: 22036
		private Asset<Texture2D> _heartSingleFancy;

		// Token: 0x04005615 RID: 22037
		private Asset<Texture2D> _starTop;

		// Token: 0x04005616 RID: 22038
		private Asset<Texture2D> _starMiddle;

		// Token: 0x04005617 RID: 22039
		private Asset<Texture2D> _starBottom;

		// Token: 0x04005618 RID: 22040
		private Asset<Texture2D> _starSingle;

		// Token: 0x04005619 RID: 22041
		private Asset<Texture2D> _starFill;

		// Token: 0x0400561A RID: 22042
		private bool _hoverLife;

		// Token: 0x0400561B RID: 22043
		private bool _hoverMana;

		// Token: 0x0400561C RID: 22044
		private bool _drawText;
	}
}
