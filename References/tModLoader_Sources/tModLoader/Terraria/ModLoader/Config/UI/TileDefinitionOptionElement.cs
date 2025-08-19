using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.ModLoader.Default;
using Terraria.ModLoader.UI;
using Terraria.ObjectData;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x020003C0 RID: 960
	internal class TileDefinitionOptionElement : DefinitionOptionElement<TileDefinition>
	{
		// Token: 0x060032E5 RID: 13029 RVA: 0x00545E4C File Offset: 0x0054404C
		public TileDefinitionOptionElement(TileDefinition definition, float scale = 0.5f) : base(definition, scale)
		{
		}

		// Token: 0x060032E6 RID: 13030 RVA: 0x00545E56 File Offset: 0x00544056
		public override void SetItem(TileDefinition definition)
		{
			base.NullID = -1;
			base.SetItem(definition);
		}

		// Token: 0x060032E7 RID: 13031 RVA: 0x00545E68 File Offset: 0x00544068
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			TileDefinitionOptionElement.<>c__DisplayClass2_0 CS$<>8__locals1;
			CS$<>8__locals1.spriteBatch = spriteBatch;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.dimensions = base.GetInnerDimensions();
			CS$<>8__locals1.spriteBatch.Draw(base.BackgroundTexture.Value, CS$<>8__locals1.dimensions.Position(), null, Color.White, 0f, Vector2.Zero, base.Scale, 0, 0f);
			if (base.Definition != null && (base.Type > base.NullID || base.Unloaded))
			{
				int type = base.Unloaded ? ModContent.TileType<UnloadedSolidTile>() : base.Definition.Type;
				if (TextureAssets.Tile[type].State == null)
				{
					Main.Assets.Request<Texture2D>(TextureAssets.Tile[type].Name, 2);
				}
				if (Main.tileFrameImportant[type])
				{
					TileObjectData objData = TileObjectData.GetTileData(type, 0, 0);
					if (objData != null)
					{
						this.<DrawSelf>g__DrawMultiTile|2_1(type, objData, ref CS$<>8__locals1);
					}
					else
					{
						this.<DrawSelf>g__Draw1x1Tile|2_0(type, new Point(0, 0), null, ref CS$<>8__locals1);
					}
				}
				else
				{
					this.<DrawSelf>g__Draw1x1Tile|2_0(type, new Point(9, 3), null, ref CS$<>8__locals1);
				}
			}
			if (base.IsMouseHovering)
			{
				UIModConfig.Tooltip = base.Tooltip;
			}
		}

		// Token: 0x060032E8 RID: 13032 RVA: 0x00545FA8 File Offset: 0x005441A8
		[CompilerGenerated]
		private void <DrawSelf>g__Draw1x1Tile|2_0(int type, Point coords, Point? offsetFromCenter = null, ref TileDefinitionOptionElement.<>c__DisplayClass2_0 A_4)
		{
			Point offset = offsetFromCenter ?? new Point(0, 0);
			Asset<Texture2D> texture = TextureAssets.Tile[type];
			int frameX = coords.X * 18;
			int frameY = coords.Y * 18;
			Rectangle sourceRect;
			sourceRect..ctor(frameX, frameY, 16, 16);
			Vector2 position = A_4.dimensions.Center() + offset.ToVector2() * 16f;
			A_4.spriteBatch.Draw(texture.Value, position, new Rectangle?(sourceRect), Color.White, 0f, Vector2.One * 8f, base.Scale * 2f, 0, 0f);
		}

		// Token: 0x060032E9 RID: 13033 RVA: 0x00546064 File Offset: 0x00544264
		[CompilerGenerated]
		private void <DrawSelf>g__DrawMultiTile|2_1(int type, TileObjectData tileData, ref TileDefinitionOptionElement.<>c__DisplayClass2_0 A_3)
		{
			RasterizerState rasterizerState = A_3.spriteBatch.GraphicsDevice.RasterizerState;
			A_3.spriteBatch.End();
			A_3.spriteBatch.Begin(0, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, rasterizerState, null, Main.UIScaleMatrix);
			Vector2 positionTopLeft = A_3.dimensions.Position() + new Vector2(4f, 4f);
			float drawDimensionHeight = A_3.dimensions.Height - 8f;
			float drawDimensionWidth = A_3.dimensions.Width - 8f;
			float drawScale = Math.Min(drawDimensionWidth / (float)(tileData.CoordinateWidth * tileData.Width), drawDimensionHeight / (float)tileData.CoordinateHeights.Sum());
			float adjustX = (tileData.Width < tileData.Height) ? ((float)(tileData.Height - tileData.Width) / ((float)tileData.Height * 2f)) : 0f;
			float adjustY = (tileData.Height < tileData.Width) ? ((float)(tileData.Width - tileData.Height) / ((float)tileData.Width * 2f)) : 0f;
			int frameCounter = Interface.modConfig.UpdateCount / 60;
			List<TileObjectData> subTiles = tileData.SubTiles;
			int frames = (subTiles != null) ? subTiles.Count : 0;
			int frame = (frames > 0) ? (frameCounter % frames) : 0;
			Texture2D tileTexture = TextureAssets.Tile[type].Value;
			int placeStyle = tileData.CalculatePlacementStyle(frame, 0, 0);
			int row = 0;
			int drawYOffset = tileData.DrawYOffset;
			int drawXOffset = tileData.DrawXOffset;
			placeStyle += tileData.DrawStyleOffset;
			int styleWrapLimit = tileData.StyleWrapLimit;
			int styleLineSkip = tileData.StyleLineSkip;
			if (tileData.StyleWrapLimitVisualOverride != null)
			{
				styleWrapLimit = tileData.StyleWrapLimitVisualOverride.Value;
			}
			if (tileData.styleLineSkipVisualOverride != null)
			{
				styleLineSkip = tileData.styleLineSkipVisualOverride.Value;
			}
			if (styleWrapLimit > 0)
			{
				row = placeStyle / styleWrapLimit * styleLineSkip;
				placeStyle %= styleWrapLimit;
			}
			int topLeftX;
			int topLeftY;
			if (tileData.StyleHorizontal)
			{
				topLeftX = tileData.CoordinateFullWidth * placeStyle;
				topLeftY = tileData.CoordinateFullHeight * row;
			}
			else
			{
				topLeftX = tileData.CoordinateFullWidth * row;
				topLeftY = tileData.CoordinateFullHeight * placeStyle;
			}
			int tileWidth = tileData.Width;
			int tileHeight = tileData.Height;
			int maxTileDimension = Math.Max(tileData.Width, tileData.Height);
			for (int i = 0; i < tileWidth; i++)
			{
				int x = topLeftX + i * (tileData.CoordinateWidth + tileData.CoordinatePadding);
				int y = topLeftY;
				for (int j = 0; j < tileHeight; j++)
				{
					if (j == 0 && tileData.DrawStepDown != 0)
					{
						drawYOffset += tileData.DrawStepDown;
					}
					if (type == 567)
					{
						drawYOffset = ((j != 0) ? tileData.DrawYOffset : (tileData.DrawYOffset - 2));
					}
					int drawWidth = tileData.CoordinateWidth;
					int drawHeight = tileData.CoordinateHeights[j];
					if (type == 114 && j == 1)
					{
						drawHeight += 2;
					}
					SpriteBatch spriteBatch = A_3.spriteBatch;
					Rectangle? rectangle = new Rectangle?(new Rectangle(x, y, drawWidth, drawHeight));
					spriteBatch.Draw(tileTexture, new Vector2(positionTopLeft.X + ((float)i / (float)maxTileDimension + adjustX) * drawDimensionWidth, positionTopLeft.Y + ((float)j / (float)maxTileDimension + adjustY) * drawDimensionHeight), rectangle, Color.White, 0f, Vector2.Zero, drawScale, 0, 0f);
					y += drawHeight + tileData.CoordinatePadding;
				}
			}
		}
	}
}
