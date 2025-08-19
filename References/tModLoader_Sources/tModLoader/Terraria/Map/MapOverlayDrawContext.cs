using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.UI;

namespace Terraria.Map
{
	// Token: 0x020003CE RID: 974
	public struct MapOverlayDrawContext
	{
		// Token: 0x06003347 RID: 13127 RVA: 0x00553788 File Offset: 0x00551988
		public MapOverlayDrawContext(Vector2 mapPosition, Vector2 mapOffset, Rectangle? clippingRect, float mapScale, float drawScale)
		{
			this._mapPosition = mapPosition;
			this._mapOffset = mapOffset;
			this._clippingRect = clippingRect;
			this._mapScale = mapScale;
			this._drawScale = drawScale;
		}

		/// <inheritdoc cref="M:Terraria.Map.MapOverlayDrawContext.Draw(Microsoft.Xna.Framework.Graphics.Texture2D,Microsoft.Xna.Framework.Vector2,Microsoft.Xna.Framework.Color,Terraria.DataStructures.SpriteFrame,System.Single,System.Single,Terraria.UI.Alignment)" />
		// Token: 0x06003348 RID: 13128 RVA: 0x005537AF File Offset: 0x005519AF
		public MapOverlayDrawContext.DrawResult Draw(Texture2D texture, Vector2 position, Alignment alignment)
		{
			return this.Draw(texture, position, new SpriteFrame(1, 1), alignment);
		}

		/// <inheritdoc cref="M:Terraria.Map.MapOverlayDrawContext.Draw(Microsoft.Xna.Framework.Graphics.Texture2D,Microsoft.Xna.Framework.Vector2,Microsoft.Xna.Framework.Color,Terraria.DataStructures.SpriteFrame,System.Single,System.Single,Terraria.UI.Alignment)" />
		// Token: 0x06003349 RID: 13129 RVA: 0x005537C4 File Offset: 0x005519C4
		public MapOverlayDrawContext.DrawResult Draw(Texture2D texture, Vector2 position, SpriteFrame frame, Alignment alignment)
		{
			position = (position - this._mapPosition) * this._mapScale + this._mapOffset;
			if (this._clippingRect != null && !this._clippingRect.Value.Contains(position.ToPoint()))
			{
				return MapOverlayDrawContext.DrawResult.Culled;
			}
			Rectangle sourceRectangle = frame.GetSourceRectangle(texture);
			Vector2 vector = sourceRectangle.Size() * alignment.OffsetMultiplier;
			Main.spriteBatch.Draw(texture, position, new Rectangle?(sourceRectangle), Color.White, 0f, vector, this._drawScale, 0, 0f);
			position -= vector * this._drawScale;
			return new MapOverlayDrawContext.DrawResult(new Rectangle((int)position.X, (int)position.Y, (int)((float)texture.Width * this._drawScale), (int)((float)texture.Height * this._drawScale)).Contains(Main.MouseScreen.ToPoint()));
		}

		/// <inheritdoc cref="M:Terraria.Map.MapOverlayDrawContext.Draw(Microsoft.Xna.Framework.Graphics.Texture2D,Microsoft.Xna.Framework.Vector2,Microsoft.Xna.Framework.Color,Terraria.DataStructures.SpriteFrame,System.Single,System.Single,Terraria.UI.Alignment,Microsoft.Xna.Framework.Graphics.SpriteEffects)" />
		// Token: 0x0600334A RID: 13130 RVA: 0x005538C4 File Offset: 0x00551AC4
		public MapOverlayDrawContext.DrawResult Draw(Texture2D texture, Vector2 position, Color color, SpriteFrame frame, float scaleIfNotSelected, float scaleIfSelected, Alignment alignment)
		{
			return this.Draw(texture, position, color, frame, scaleIfNotSelected, scaleIfSelected, alignment, 0);
		}

		/// <summary>
		/// Draws the texture (icon) over the map using the provided arguments. Check the returned <see cref="F:Terraria.Map.MapOverlayDrawContext.DrawResult.IsMouseOver" /> to check if the mouse is hovering over this icon. This is usually used to assign the <c>text</c> parameter of <see cref="M:Terraria.ModLoader.ModMapLayer.Draw(Terraria.Map.MapOverlayDrawContext@,System.String@)" /> to the hover text of the icon.
		/// <para /> Note that the <paramref name="position" /> argument expects tile coordinates expressed as a Vector2. Don't scale tile coordinates to world coordinates by multiplying by 16. If you are working with world coordinates you'll need to divide them by 16 to convert them to tile coordinates.
		/// </summary>
		// Token: 0x0600334B RID: 13131 RVA: 0x005538E4 File Offset: 0x00551AE4
		public MapOverlayDrawContext.DrawResult Draw(Texture2D texture, Vector2 position, Color color, SpriteFrame frame, float scaleIfNotSelected, float scaleIfSelected, Alignment alignment, SpriteEffects spriteEffects)
		{
			position = (position - this._mapPosition) * this._mapScale + this._mapOffset;
			if (this._clippingRect != null && !this._clippingRect.Value.Contains(position.ToPoint()))
			{
				return MapOverlayDrawContext.DrawResult.Culled;
			}
			Rectangle sourceRectangle = frame.GetSourceRectangle(texture);
			Vector2 vector = sourceRectangle.Size() * alignment.OffsetMultiplier;
			Vector2 position2 = position;
			float num = this._drawScale * scaleIfNotSelected;
			Vector2 vector2 = position - vector * num;
			bool flag = new Rectangle((int)vector2.X, (int)vector2.Y, (int)((float)sourceRectangle.Width * num), (int)((float)sourceRectangle.Height * num)).Contains(Main.MouseScreen.ToPoint());
			float scale = num;
			if (flag)
			{
				scale = this._drawScale * scaleIfSelected;
			}
			Main.spriteBatch.Draw(texture, position2, new Rectangle?(sourceRectangle), color, 0f, vector, scale, spriteEffects, 0f);
			return new MapOverlayDrawContext.DrawResult(flag);
		}

		/// <summary>
		/// The tile coordinate that the top left corner of the map is showing. Note that this will always be <see cref="P:Microsoft.Xna.Framework.Vector2.Zero" /> when drawing the overlay and fullscreen maps.
		/// </summary>
		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x0600334C RID: 13132 RVA: 0x005539EF File Offset: 0x00551BEF
		public readonly Vector2 MapPosition
		{
			get
			{
				return this._mapPosition;
			}
		}

		/// <summary>
		/// The position of the top left corner of the map in screen coordinates.
		/// </summary>
		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x0600334D RID: 13133 RVA: 0x005539F7 File Offset: 0x00551BF7
		public readonly Vector2 MapOffset
		{
			get
			{
				return this._mapOffset;
			}
		}

		/// <summary>
		/// The bounding box of the map in screen coordinates. This is used in vanilla to make icons disappear on the minimap when they go out of view. Note this only applies to the minimap and will be <see langword="null" /> when drawing the overlay and fullscreen maps.
		/// </summary>
		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x0600334E RID: 13134 RVA: 0x005539FF File Offset: 0x00551BFF
		public readonly Rectangle? ClippingRectangle
		{
			get
			{
				return this._clippingRect;
			}
		}

		/// <summary>
		/// A scale multiplier for the size of the tiles on the map. See <see cref="F:Terraria.Main.mapMinimapScale" />, <see cref="F:Terraria.Main.mapFullscreenScale" />, and <see cref="F:Terraria.Main.mapOverlayScale" />.
		/// </summary>
		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x0600334F RID: 13135 RVA: 0x00553A07 File Offset: 0x00551C07
		public readonly float MapScale
		{
			get
			{
				return this._mapScale;
			}
		}

		/// <summary>
		/// A scale multiplier for the size of the icons on the map. This is typically passed as the 'scale' parameter of draw calls and accounts for things like <see cref="P:Terraria.Main.UIScale" />.
		/// </summary>
		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06003350 RID: 13136 RVA: 0x00553A0F File Offset: 0x00551C0F
		public readonly float DrawScale
		{
			get
			{
				return this._drawScale;
			}
		}

		// Token: 0x04001E31 RID: 7729
		private readonly Vector2 _mapPosition;

		// Token: 0x04001E32 RID: 7730
		private readonly Vector2 _mapOffset;

		// Token: 0x04001E33 RID: 7731
		private readonly Rectangle? _clippingRect;

		// Token: 0x04001E34 RID: 7732
		private readonly float _mapScale;

		// Token: 0x04001E35 RID: 7733
		private readonly float _drawScale;

		// Token: 0x02000B16 RID: 2838
		public struct DrawResult
		{
			// Token: 0x06005B5E RID: 23390 RVA: 0x006A5ACE File Offset: 0x006A3CCE
			public DrawResult(bool isMouseOver)
			{
				this.IsMouseOver = isMouseOver;
			}

			// Token: 0x04006EF8 RID: 28408
			public static readonly MapOverlayDrawContext.DrawResult Culled = new MapOverlayDrawContext.DrawResult(false);

			// Token: 0x04006EF9 RID: 28409
			public readonly bool IsMouseOver;
		}
	}
}
