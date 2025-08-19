using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent.UI.ResourceSets;

namespace Terraria.ModLoader
{
	// Token: 0x020001F4 RID: 500
	public struct ResourceOverlayDrawContext
	{
		/// <summary>
		/// The resource display set that this context is drawing from
		/// </summary>
		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06002702 RID: 9986 RVA: 0x00501BD6 File Offset: 0x004FFDD6
		// (set) Token: 0x06002703 RID: 9987 RVA: 0x00501BDE File Offset: 0x004FFDDE
		public IPlayerResourcesDisplaySet DisplaySet { readonly get; set; }

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06002704 RID: 9988 RVA: 0x00501BE7 File Offset: 0x004FFDE7
		// (set) Token: 0x06002705 RID: 9989 RVA: 0x00501BEF File Offset: 0x004FFDEF
		public SpriteBatch SpriteBatch { readonly get; set; }

		/// <summary>
		/// Creates a context for drawing resources from a display set
		/// </summary>
		/// <param name="snapshot">A snapshot of a player's life and mana stats</param>
		/// <param name="displaySet">The display set that this context is for</param>
		/// <param name="resourceNumber">The resource number within the resource set</param>
		/// <param name="texture">The texture being drawn</param>
		// Token: 0x06002706 RID: 9990 RVA: 0x00501BF8 File Offset: 0x004FFDF8
		public ResourceOverlayDrawContext(PlayerStatsSnapshot snapshot, IPlayerResourcesDisplaySet displaySet, int resourceNumber, Asset<Texture2D> texture)
		{
			this.snapshot = snapshot;
			this.DisplaySet = displaySet;
			this.resourceNumber = resourceNumber;
			this.texture = texture;
			this.position = Vector2.Zero;
			this.source = null;
			this.color = Color.White;
			this.rotation = 0f;
			this.origin = Vector2.Zero;
			this.scale = Vector2.One;
			this.effects = 0;
			this.SpriteBatch = Main.spriteBatch;
		}

		// Token: 0x06002707 RID: 9991 RVA: 0x00501C78 File Offset: 0x004FFE78
		public void Draw()
		{
			this.SpriteBatch.Draw(this.texture.Value, this.position, this.source, this.color, this.rotation, this.origin, this.scale, this.effects, 0f);
		}

		/// <summary>
		/// A snapshot of the player's health and mana stats
		/// </summary>
		// Token: 0x0400189F RID: 6303
		public readonly PlayerStatsSnapshot snapshot;

		/// <summary>
		/// Which heart/star/bar/panel is being drawn<br />
		/// <b>NOTE:</b> This value usually starts at 0, but it can start at other values
		/// </summary>
		// Token: 0x040018A0 RID: 6304
		public readonly int resourceNumber;

		// Token: 0x040018A1 RID: 6305
		public Asset<Texture2D> texture;

		// Token: 0x040018A2 RID: 6306
		public Vector2 position;

		/// <summary>
		/// The slice of the texture to draw<br />
		/// <see langword="null" /> represents the entire texture
		/// </summary>
		// Token: 0x040018A3 RID: 6307
		public Rectangle? source;

		// Token: 0x040018A4 RID: 6308
		public Color color;

		// Token: 0x040018A5 RID: 6309
		public float rotation;

		/// <summary>
		/// The center for rotation and scaling within the source rectangle
		/// </summary>
		// Token: 0x040018A6 RID: 6310
		public Vector2 origin;

		// Token: 0x040018A7 RID: 6311
		public Vector2 scale;

		// Token: 0x040018A8 RID: 6312
		public SpriteEffects effects;
	}
}
