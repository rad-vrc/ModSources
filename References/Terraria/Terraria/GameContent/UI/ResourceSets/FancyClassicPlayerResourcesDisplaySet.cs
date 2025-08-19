using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using Terraria.DataStructures;

namespace Terraria.GameContent.UI.ResourceSets
{
	// Token: 0x020003B8 RID: 952
	public class FancyClassicPlayerResourcesDisplaySet : IPlayerResourcesDisplaySet, IConfigKeyHolder
	{
		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06002A17 RID: 10775 RVA: 0x005972F4 File Offset: 0x005954F4
		// (set) Token: 0x06002A18 RID: 10776 RVA: 0x005972FC File Offset: 0x005954FC
		public string NameKey { get; private set; }

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06002A19 RID: 10777 RVA: 0x00597305 File Offset: 0x00595505
		// (set) Token: 0x06002A1A RID: 10778 RVA: 0x0059730D File Offset: 0x0059550D
		public string ConfigKey { get; private set; }

		// Token: 0x06002A1B RID: 10779 RVA: 0x00597318 File Offset: 0x00595518
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
			string str = "Images\\UI\\PlayerResourceSets\\" + resourceFolderName;
			this._heartLeft = Main.Assets.Request<Texture2D>(str + "\\Heart_Left", mode);
			this._heartMiddle = Main.Assets.Request<Texture2D>(str + "\\Heart_Middle", mode);
			this._heartRight = Main.Assets.Request<Texture2D>(str + "\\Heart_Right", mode);
			this._heartRightFancy = Main.Assets.Request<Texture2D>(str + "\\Heart_Right_Fancy", mode);
			this._heartFill = Main.Assets.Request<Texture2D>(str + "\\Heart_Fill", mode);
			this._heartFillHoney = Main.Assets.Request<Texture2D>(str + "\\Heart_Fill_B", mode);
			this._heartSingleFancy = Main.Assets.Request<Texture2D>(str + "\\Heart_Single_Fancy", mode);
			this._starTop = Main.Assets.Request<Texture2D>(str + "\\Star_A", mode);
			this._starMiddle = Main.Assets.Request<Texture2D>(str + "\\Star_B", mode);
			this._starBottom = Main.Assets.Request<Texture2D>(str + "\\Star_C", mode);
			this._starSingle = Main.Assets.Request<Texture2D>(str + "\\Star_Single", mode);
			this._starFill = Main.Assets.Request<Texture2D>(str + "\\Star_Fill", mode);
		}

		// Token: 0x06002A1C RID: 10780 RVA: 0x005974C0 File Offset: 0x005956C0
		public void Draw()
		{
			Player localPlayer = Main.LocalPlayer;
			SpriteBatch spriteBatch = Main.spriteBatch;
			this.PrepareFields(localPlayer);
			this.DrawLifeBar(spriteBatch);
			this.DrawManaBar(spriteBatch);
		}

		// Token: 0x06002A1D RID: 10781 RVA: 0x005974F0 File Offset: 0x005956F0
		private void DrawLifeBar(SpriteBatch spriteBatch)
		{
			Vector2 vector = new Vector2((float)(Main.screenWidth - 300 + 4), 15f);
			if (this._drawText)
			{
				vector.Y += 6f;
				FancyClassicPlayerResourcesDisplaySet.DrawLifeBarText(spriteBatch, vector + new Vector2(-4f, 3f));
			}
			bool hoverLife = false;
			ResourceDrawSettings resourceDrawSettings = default(ResourceDrawSettings);
			resourceDrawSettings.ElementCount = this._heartCountRow1;
			resourceDrawSettings.ElementIndexOffset = 0;
			resourceDrawSettings.TopLeftAnchor = vector;
			resourceDrawSettings.GetTextureMethod = new ResourceDrawSettings.TextureGetter(this.HeartPanelDrawer);
			resourceDrawSettings.OffsetPerDraw = Vector2.Zero;
			resourceDrawSettings.OffsetPerDrawByTexturePercentile = Vector2.UnitX;
			resourceDrawSettings.OffsetSpriteAnchor = Vector2.Zero;
			resourceDrawSettings.OffsetSpriteAnchorByTexturePercentile = Vector2.Zero;
			resourceDrawSettings.Draw(spriteBatch, ref hoverLife);
			resourceDrawSettings = default(ResourceDrawSettings);
			resourceDrawSettings.ElementCount = this._heartCountRow2;
			resourceDrawSettings.ElementIndexOffset = 10;
			resourceDrawSettings.TopLeftAnchor = vector + new Vector2(0f, 28f);
			resourceDrawSettings.GetTextureMethod = new ResourceDrawSettings.TextureGetter(this.HeartPanelDrawer);
			resourceDrawSettings.OffsetPerDraw = Vector2.Zero;
			resourceDrawSettings.OffsetPerDrawByTexturePercentile = Vector2.UnitX;
			resourceDrawSettings.OffsetSpriteAnchor = Vector2.Zero;
			resourceDrawSettings.OffsetSpriteAnchorByTexturePercentile = Vector2.Zero;
			resourceDrawSettings.Draw(spriteBatch, ref hoverLife);
			resourceDrawSettings = default(ResourceDrawSettings);
			resourceDrawSettings.ElementCount = this._heartCountRow1;
			resourceDrawSettings.ElementIndexOffset = 0;
			resourceDrawSettings.TopLeftAnchor = vector + new Vector2(15f, 15f);
			resourceDrawSettings.GetTextureMethod = new ResourceDrawSettings.TextureGetter(this.HeartFillingDrawer);
			resourceDrawSettings.OffsetPerDraw = Vector2.UnitX * 2f;
			resourceDrawSettings.OffsetPerDrawByTexturePercentile = Vector2.UnitX;
			resourceDrawSettings.OffsetSpriteAnchor = Vector2.Zero;
			resourceDrawSettings.OffsetSpriteAnchorByTexturePercentile = new Vector2(0.5f, 0.5f);
			resourceDrawSettings.Draw(spriteBatch, ref hoverLife);
			resourceDrawSettings = default(ResourceDrawSettings);
			resourceDrawSettings.ElementCount = this._heartCountRow2;
			resourceDrawSettings.ElementIndexOffset = 10;
			resourceDrawSettings.TopLeftAnchor = vector + new Vector2(15f, 15f) + new Vector2(0f, 28f);
			resourceDrawSettings.GetTextureMethod = new ResourceDrawSettings.TextureGetter(this.HeartFillingDrawer);
			resourceDrawSettings.OffsetPerDraw = Vector2.UnitX * 2f;
			resourceDrawSettings.OffsetPerDrawByTexturePercentile = Vector2.UnitX;
			resourceDrawSettings.OffsetSpriteAnchor = Vector2.Zero;
			resourceDrawSettings.OffsetSpriteAnchorByTexturePercentile = new Vector2(0.5f, 0.5f);
			resourceDrawSettings.Draw(spriteBatch, ref hoverLife);
			this._hoverLife = hoverLife;
		}

		// Token: 0x06002A1E RID: 10782 RVA: 0x00597798 File Offset: 0x00595998
		private static void DrawLifeBarText(SpriteBatch spriteBatch, Vector2 topLeftAnchor)
		{
			Vector2 value = topLeftAnchor + new Vector2(130f, -24f);
			Player localPlayer = Main.LocalPlayer;
			Color color = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
			string text = string.Concat(new object[]
			{
				Lang.inter[0].Value,
				" ",
				localPlayer.statLifeMax2,
				"/",
				localPlayer.statLifeMax2
			});
			Vector2 vector = FontAssets.MouseText.Value.MeasureString(text);
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, Lang.inter[0].Value, value + new Vector2(-vector.X * 0.5f, 0f), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, localPlayer.statLife + "/" + localPlayer.statLifeMax2, value + new Vector2(vector.X * 0.5f, 0f), color, 0f, new Vector2(FontAssets.MouseText.Value.MeasureString(localPlayer.statLife + "/" + localPlayer.statLifeMax2).X, 0f), 1f, SpriteEffects.None, 0f);
		}

		// Token: 0x06002A1F RID: 10783 RVA: 0x00597928 File Offset: 0x00595B28
		private void DrawManaBar(SpriteBatch spriteBatch)
		{
			Vector2 vector = new Vector2((float)(Main.screenWidth - 40), 22f);
			int starCount = this._starCount;
			bool hoverMana = false;
			ResourceDrawSettings resourceDrawSettings = default(ResourceDrawSettings);
			resourceDrawSettings.ElementCount = this._starCount;
			resourceDrawSettings.ElementIndexOffset = 0;
			resourceDrawSettings.TopLeftAnchor = vector;
			resourceDrawSettings.GetTextureMethod = new ResourceDrawSettings.TextureGetter(this.StarPanelDrawer);
			resourceDrawSettings.OffsetPerDraw = Vector2.Zero;
			resourceDrawSettings.OffsetPerDrawByTexturePercentile = Vector2.UnitY;
			resourceDrawSettings.OffsetSpriteAnchor = Vector2.Zero;
			resourceDrawSettings.OffsetSpriteAnchorByTexturePercentile = Vector2.Zero;
			resourceDrawSettings.Draw(spriteBatch, ref hoverMana);
			resourceDrawSettings = default(ResourceDrawSettings);
			resourceDrawSettings.ElementCount = this._starCount;
			resourceDrawSettings.ElementIndexOffset = 0;
			resourceDrawSettings.TopLeftAnchor = vector + new Vector2(15f, 16f);
			resourceDrawSettings.GetTextureMethod = new ResourceDrawSettings.TextureGetter(this.StarFillingDrawer);
			resourceDrawSettings.OffsetPerDraw = Vector2.UnitY * -2f;
			resourceDrawSettings.OffsetPerDrawByTexturePercentile = Vector2.UnitY;
			resourceDrawSettings.OffsetSpriteAnchor = Vector2.Zero;
			resourceDrawSettings.OffsetSpriteAnchorByTexturePercentile = new Vector2(0.5f, 0.5f);
			resourceDrawSettings.Draw(spriteBatch, ref hoverMana);
			this._hoverMana = hoverMana;
		}

		// Token: 0x06002A20 RID: 10784 RVA: 0x00597A68 File Offset: 0x00595C68
		private static void DrawManaText(SpriteBatch spriteBatch)
		{
			Vector2 vector = FontAssets.MouseText.Value.MeasureString(Lang.inter[2].Value);
			Color color = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
			int num = 50;
			if (vector.X >= 45f)
			{
				num = (int)vector.X + 5;
			}
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, Lang.inter[2].Value, new Vector2((float)(Main.screenWidth - num), 6f), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
		}

		// Token: 0x06002A21 RID: 10785 RVA: 0x00597B10 File Offset: 0x00595D10
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

		// Token: 0x06002A22 RID: 10786 RVA: 0x00597BB4 File Offset: 0x00595DB4
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
			float lerpValue = Utils.GetLerpValue(this._lifePerHeart * (float)elementIndex, this._lifePerHeart * (float)(elementIndex + 1), this._currentPlayerLife, true);
			drawScale = lerpValue;
			if (elementIndex == this._lastHeartFillingIndex && lerpValue > 0f)
			{
				drawScale += Main.cursorScale - 1f;
			}
		}

		// Token: 0x06002A23 RID: 10787 RVA: 0x00597C44 File Offset: 0x00595E44
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

		// Token: 0x06002A24 RID: 10788 RVA: 0x00597CBC File Offset: 0x00595EBC
		private void StarFillingDrawer(int elementIndex, int firstElementIndex, int lastElementIndex, out Asset<Texture2D> sprite, out Vector2 offset, out float drawScale, out Rectangle? sourceRect)
		{
			sourceRect = null;
			offset = Vector2.Zero;
			sprite = this._starFill;
			float lerpValue = Utils.GetLerpValue(this._manaPerStar * (float)elementIndex, this._manaPerStar * (float)(elementIndex + 1), this._currentPlayerMana, true);
			drawScale = lerpValue;
			if (elementIndex == this._lastStarFillingIndex && lerpValue > 0f)
			{
				drawScale += Main.cursorScale - 1f;
			}
		}

		// Token: 0x06002A25 RID: 10789 RVA: 0x00597D30 File Offset: 0x00595F30
		private void PrepareFields(Player player)
		{
			PlayerStatsSnapshot playerStatsSnapshot = new PlayerStatsSnapshot(player);
			this._playerLifeFruitCount = playerStatsSnapshot.LifeFruitCount;
			this._lifePerHeart = playerStatsSnapshot.LifePerSegment;
			this._currentPlayerLife = (float)playerStatsSnapshot.Life;
			this._manaPerStar = playerStatsSnapshot.ManaPerSegment;
			this._heartCountRow1 = Utils.Clamp<int>((int)((float)playerStatsSnapshot.LifeMax / this._lifePerHeart), 0, 10);
			this._heartCountRow2 = Utils.Clamp<int>((int)((float)(playerStatsSnapshot.LifeMax - 200) / this._lifePerHeart), 0, 10);
			int lastHeartFillingIndex = (int)((float)playerStatsSnapshot.Life / this._lifePerHeart);
			this._lastHeartFillingIndex = lastHeartFillingIndex;
			this._lastHeartPanelIndex = this._heartCountRow1 + this._heartCountRow2 - 1;
			this._starCount = (int)((float)playerStatsSnapshot.ManaMax / this._manaPerStar);
			this._currentPlayerMana = (float)playerStatsSnapshot.Mana;
			this._lastStarFillingIndex = (int)(this._currentPlayerMana / this._manaPerStar);
		}

		// Token: 0x06002A26 RID: 10790 RVA: 0x00597E18 File Offset: 0x00596018
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

		// Token: 0x04004CE1 RID: 19681
		private float _currentPlayerLife;

		// Token: 0x04004CE2 RID: 19682
		private float _lifePerHeart;

		// Token: 0x04004CE3 RID: 19683
		private int _playerLifeFruitCount;

		// Token: 0x04004CE4 RID: 19684
		private int _lastHeartFillingIndex;

		// Token: 0x04004CE5 RID: 19685
		private int _lastHeartPanelIndex;

		// Token: 0x04004CE6 RID: 19686
		private int _heartCountRow1;

		// Token: 0x04004CE7 RID: 19687
		private int _heartCountRow2;

		// Token: 0x04004CE8 RID: 19688
		private int _starCount;

		// Token: 0x04004CE9 RID: 19689
		private int _lastStarFillingIndex;

		// Token: 0x04004CEA RID: 19690
		private float _manaPerStar;

		// Token: 0x04004CEB RID: 19691
		private float _currentPlayerMana;

		// Token: 0x04004CEC RID: 19692
		private Asset<Texture2D> _heartLeft;

		// Token: 0x04004CED RID: 19693
		private Asset<Texture2D> _heartMiddle;

		// Token: 0x04004CEE RID: 19694
		private Asset<Texture2D> _heartRight;

		// Token: 0x04004CEF RID: 19695
		private Asset<Texture2D> _heartRightFancy;

		// Token: 0x04004CF0 RID: 19696
		private Asset<Texture2D> _heartFill;

		// Token: 0x04004CF1 RID: 19697
		private Asset<Texture2D> _heartFillHoney;

		// Token: 0x04004CF2 RID: 19698
		private Asset<Texture2D> _heartSingleFancy;

		// Token: 0x04004CF3 RID: 19699
		private Asset<Texture2D> _starTop;

		// Token: 0x04004CF4 RID: 19700
		private Asset<Texture2D> _starMiddle;

		// Token: 0x04004CF5 RID: 19701
		private Asset<Texture2D> _starBottom;

		// Token: 0x04004CF6 RID: 19702
		private Asset<Texture2D> _starSingle;

		// Token: 0x04004CF7 RID: 19703
		private Asset<Texture2D> _starFill;

		// Token: 0x04004CF8 RID: 19704
		private bool _hoverLife;

		// Token: 0x04004CF9 RID: 19705
		private bool _hoverMana;

		// Token: 0x04004CFA RID: 19706
		private bool _drawText;
	}
}
