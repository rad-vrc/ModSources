using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using Terraria.DataStructures;

namespace Terraria.GameContent.UI.ResourceSets
{
	// Token: 0x020003B9 RID: 953
	public class HorizontalBarsPlayerResourcesDisplaySet : IPlayerResourcesDisplaySet, IConfigKeyHolder
	{
		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06002A27 RID: 10791 RVA: 0x00597E34 File Offset: 0x00596034
		// (set) Token: 0x06002A28 RID: 10792 RVA: 0x00597E3C File Offset: 0x0059603C
		public string NameKey { get; private set; }

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06002A29 RID: 10793 RVA: 0x00597E45 File Offset: 0x00596045
		// (set) Token: 0x06002A2A RID: 10794 RVA: 0x00597E4D File Offset: 0x0059604D
		public string ConfigKey { get; private set; }

		// Token: 0x06002A2B RID: 10795 RVA: 0x00597E58 File Offset: 0x00596058
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
			string str = "Images\\UI\\PlayerResourceSets\\" + resourceFolderName;
			this._hpFill = Main.Assets.Request<Texture2D>(str + "\\HP_Fill", mode);
			this._hpFillHoney = Main.Assets.Request<Texture2D>(str + "\\HP_Fill_Honey", mode);
			this._mpFill = Main.Assets.Request<Texture2D>(str + "\\MP_Fill", mode);
			this._panelLeft = Main.Assets.Request<Texture2D>(str + "\\Panel_Left", mode);
			this._panelMiddleHP = Main.Assets.Request<Texture2D>(str + "\\HP_Panel_Middle", mode);
			this._panelRightHP = Main.Assets.Request<Texture2D>(str + "\\HP_Panel_Right", mode);
			this._panelMiddleMP = Main.Assets.Request<Texture2D>(str + "\\MP_Panel_Middle", mode);
			this._panelRightMP = Main.Assets.Request<Texture2D>(str + "\\MP_Panel_Right", mode);
		}

		// Token: 0x06002A2C RID: 10796 RVA: 0x00597FA0 File Offset: 0x005961A0
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
			Vector2 vector = new Vector2((float)num3, (float)num2);
			vector.X += (float)((this._maxSegmentCount - this._hpSegmentsCount) * this._panelMiddleHP.Width());
			bool flag = false;
			ResourceDrawSettings resourceDrawSettings = default(ResourceDrawSettings);
			resourceDrawSettings.ElementCount = this._hpSegmentsCount + 2;
			resourceDrawSettings.ElementIndexOffset = 0;
			resourceDrawSettings.TopLeftAnchor = vector;
			resourceDrawSettings.GetTextureMethod = new ResourceDrawSettings.TextureGetter(this.LifePanelDrawer);
			resourceDrawSettings.OffsetPerDraw = Vector2.Zero;
			resourceDrawSettings.OffsetPerDrawByTexturePercentile = Vector2.UnitX;
			resourceDrawSettings.OffsetSpriteAnchor = Vector2.Zero;
			resourceDrawSettings.OffsetSpriteAnchorByTexturePercentile = Vector2.Zero;
			resourceDrawSettings.Draw(spriteBatch, ref flag);
			resourceDrawSettings = default(ResourceDrawSettings);
			resourceDrawSettings.ElementCount = this._hpSegmentsCount;
			resourceDrawSettings.ElementIndexOffset = 0;
			resourceDrawSettings.TopLeftAnchor = vector + new Vector2(6f, 6f);
			resourceDrawSettings.GetTextureMethod = new ResourceDrawSettings.TextureGetter(this.LifeFillingDrawer);
			resourceDrawSettings.OffsetPerDraw = new Vector2((float)this._hpFill.Width(), 0f);
			resourceDrawSettings.OffsetPerDrawByTexturePercentile = Vector2.Zero;
			resourceDrawSettings.OffsetSpriteAnchor = Vector2.Zero;
			resourceDrawSettings.OffsetSpriteAnchorByTexturePercentile = Vector2.Zero;
			resourceDrawSettings.Draw(spriteBatch, ref flag);
			this._hpHovered = flag;
			flag = false;
			Vector2 vector2 = new Vector2((float)(num3 - 10), (float)(num2 + 24));
			vector2.X += (float)((this._maxSegmentCount - this._mpSegmentsCount) * this._panelMiddleMP.Width());
			resourceDrawSettings = default(ResourceDrawSettings);
			resourceDrawSettings.ElementCount = this._mpSegmentsCount + 2;
			resourceDrawSettings.ElementIndexOffset = 0;
			resourceDrawSettings.TopLeftAnchor = vector2;
			resourceDrawSettings.GetTextureMethod = new ResourceDrawSettings.TextureGetter(this.ManaPanelDrawer);
			resourceDrawSettings.OffsetPerDraw = Vector2.Zero;
			resourceDrawSettings.OffsetPerDrawByTexturePercentile = Vector2.UnitX;
			resourceDrawSettings.OffsetSpriteAnchor = Vector2.Zero;
			resourceDrawSettings.OffsetSpriteAnchorByTexturePercentile = Vector2.Zero;
			resourceDrawSettings.Draw(spriteBatch, ref flag);
			resourceDrawSettings = default(ResourceDrawSettings);
			resourceDrawSettings.ElementCount = this._mpSegmentsCount;
			resourceDrawSettings.ElementIndexOffset = 0;
			resourceDrawSettings.TopLeftAnchor = vector2 + new Vector2(6f, 6f);
			resourceDrawSettings.GetTextureMethod = new ResourceDrawSettings.TextureGetter(this.ManaFillingDrawer);
			resourceDrawSettings.OffsetPerDraw = new Vector2((float)this._mpFill.Width(), 0f);
			resourceDrawSettings.OffsetPerDrawByTexturePercentile = Vector2.Zero;
			resourceDrawSettings.OffsetSpriteAnchor = Vector2.Zero;
			resourceDrawSettings.OffsetSpriteAnchorByTexturePercentile = Vector2.Zero;
			resourceDrawSettings.Draw(spriteBatch, ref flag);
			this._mpHovered = flag;
		}

		// Token: 0x06002A2D RID: 10797 RVA: 0x005982AC File Offset: 0x005964AC
		private static void DrawManaText(SpriteBatch spriteBatch)
		{
			Color color = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
			int num = 180;
			Player localPlayer = Main.LocalPlayer;
			string text = Lang.inter[2].Value + ":";
			string text2 = localPlayer.statMana + "/" + localPlayer.statManaMax2;
			Vector2 value = new Vector2((float)(Main.screenWidth - num), 65f);
			string text3 = text + " " + text2;
			Vector2 vector = FontAssets.MouseText.Value.MeasureString(text3);
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, text, value + new Vector2(-vector.X * 0.5f, 0f), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, text2, value + new Vector2(vector.X * 0.5f, 0f), color, 0f, new Vector2(FontAssets.MouseText.Value.MeasureString(text2).X, 0f), 1f, SpriteEffects.None, 0f);
		}

		// Token: 0x06002A2E RID: 10798 RVA: 0x005983FC File Offset: 0x005965FC
		private static void DrawLifeBarText(SpriteBatch spriteBatch, Vector2 topLeftAnchor)
		{
			Vector2 value = topLeftAnchor + new Vector2(130f, -20f);
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

		// Token: 0x06002A2F RID: 10799 RVA: 0x0059858C File Offset: 0x0059678C
		private void PrepareFields(Player player)
		{
			PlayerStatsSnapshot playerStatsSnapshot = new PlayerStatsSnapshot(player);
			this._hpSegmentsCount = (int)((float)playerStatsSnapshot.LifeMax / playerStatsSnapshot.LifePerSegment);
			this._mpSegmentsCount = (int)((float)playerStatsSnapshot.ManaMax / playerStatsSnapshot.ManaPerSegment);
			this._maxSegmentCount = 20;
			this._hpFruitCount = playerStatsSnapshot.LifeFruitCount;
			this._hpPercent = (float)playerStatsSnapshot.Life / (float)playerStatsSnapshot.LifeMax;
			this._mpPercent = (float)playerStatsSnapshot.Mana / (float)playerStatsSnapshot.ManaMax;
		}

		// Token: 0x06002A30 RID: 10800 RVA: 0x0059860C File Offset: 0x0059680C
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
			}
		}

		// Token: 0x06002A31 RID: 10801 RVA: 0x00598670 File Offset: 0x00596870
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
			}
		}

		// Token: 0x06002A32 RID: 10802 RVA: 0x005986D3 File Offset: 0x005968D3
		private void LifeFillingDrawer(int elementIndex, int firstElementIndex, int lastElementIndex, out Asset<Texture2D> sprite, out Vector2 offset, out float drawScale, out Rectangle? sourceRect)
		{
			sprite = this._hpFill;
			if (elementIndex >= this._hpSegmentsCount - this._hpFruitCount)
			{
				sprite = this._hpFillHoney;
			}
			HorizontalBarsPlayerResourcesDisplaySet.FillBarByValues(elementIndex, sprite, this._hpSegmentsCount, this._hpPercent, out offset, out drawScale, out sourceRect);
		}

		// Token: 0x06002A33 RID: 10803 RVA: 0x00598714 File Offset: 0x00596914
		private static void FillBarByValues(int elementIndex, Asset<Texture2D> sprite, int segmentsCount, float fillPercent, out Vector2 offset, out float drawScale, out Rectangle? sourceRect)
		{
			sourceRect = null;
			offset = Vector2.Zero;
			float num = 1f / (float)segmentsCount;
			float t = 1f - fillPercent;
			float lerpValue = Utils.GetLerpValue(num * (float)elementIndex, num * (float)(elementIndex + 1), t, true);
			float num2 = 1f - lerpValue;
			drawScale = 1f;
			Rectangle rectangle = sprite.Frame(1, 1, 0, 0, 0, 0);
			int num3 = (int)((float)rectangle.Width * (1f - num2));
			offset.X += (float)num3;
			rectangle.X += num3;
			rectangle.Width -= num3;
			sourceRect = new Rectangle?(rectangle);
		}

		// Token: 0x06002A34 RID: 10804 RVA: 0x005987C9 File Offset: 0x005969C9
		private void ManaFillingDrawer(int elementIndex, int firstElementIndex, int lastElementIndex, out Asset<Texture2D> sprite, out Vector2 offset, out float drawScale, out Rectangle? sourceRect)
		{
			sprite = this._mpFill;
			HorizontalBarsPlayerResourcesDisplaySet.FillBarByValues(elementIndex, sprite, this._mpSegmentsCount, this._mpPercent, out offset, out drawScale, out sourceRect);
		}

		// Token: 0x06002A35 RID: 10805 RVA: 0x005987EF File Offset: 0x005969EF
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

		// Token: 0x04004CFD RID: 19709
		private int _maxSegmentCount;

		// Token: 0x04004CFE RID: 19710
		private int _hpSegmentsCount;

		// Token: 0x04004CFF RID: 19711
		private int _mpSegmentsCount;

		// Token: 0x04004D00 RID: 19712
		private int _hpFruitCount;

		// Token: 0x04004D01 RID: 19713
		private float _hpPercent;

		// Token: 0x04004D02 RID: 19714
		private float _mpPercent;

		// Token: 0x04004D03 RID: 19715
		private byte _drawTextStyle;

		// Token: 0x04004D04 RID: 19716
		private bool _hpHovered;

		// Token: 0x04004D05 RID: 19717
		private bool _mpHovered;

		// Token: 0x04004D06 RID: 19718
		private Asset<Texture2D> _hpFill;

		// Token: 0x04004D07 RID: 19719
		private Asset<Texture2D> _hpFillHoney;

		// Token: 0x04004D08 RID: 19720
		private Asset<Texture2D> _mpFill;

		// Token: 0x04004D09 RID: 19721
		private Asset<Texture2D> _panelLeft;

		// Token: 0x04004D0A RID: 19722
		private Asset<Texture2D> _panelMiddleHP;

		// Token: 0x04004D0B RID: 19723
		private Asset<Texture2D> _panelRightHP;

		// Token: 0x04004D0C RID: 19724
		private Asset<Texture2D> _panelMiddleMP;

		// Token: 0x04004D0D RID: 19725
		private Asset<Texture2D> _panelRightMP;
	}
}
