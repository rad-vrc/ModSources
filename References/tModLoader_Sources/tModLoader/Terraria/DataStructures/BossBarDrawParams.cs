using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.DataStructures
{
	/// <summary>
	/// Holds data required for boss bar drawing.
	/// </summary>
	// Token: 0x020006D2 RID: 1746
	public struct BossBarDrawParams
	{
		// Token: 0x06004908 RID: 18696 RVA: 0x0064C508 File Offset: 0x0064A708
		public BossBarDrawParams(Texture2D barTexture, Vector2 barCenter, Texture2D iconTexture, Rectangle iconFrame, Color iconColor, float life, float lifeMax, float shield = 0f, float shieldMax = 0f, float iconScale = 1f, bool showText = true, Vector2 textOffset = default(Vector2))
		{
			this.BarTexture = barTexture;
			this.BarCenter = barCenter;
			this.IconTexture = iconTexture;
			this.IconFrame = iconFrame;
			this.IconColor = iconColor;
			this.Life = life;
			this.LifeMax = lifeMax;
			this.Shield = shield;
			this.ShieldMax = shieldMax;
			this.IconScale = iconScale;
			this.ShowText = showText;
			this.TextOffset = textOffset;
		}

		// Token: 0x06004909 RID: 18697 RVA: 0x0064C574 File Offset: 0x0064A774
		public void Deconstruct(out Texture2D barTexture, out Vector2 barCenter, out Texture2D iconTexture, out Rectangle iconFrame, out Color iconColor, out float life, out float lifeMax, out float shield, out float shieldMax, out float iconScale, out bool showText, out Vector2 textOffset)
		{
			barTexture = this.BarTexture;
			barCenter = this.BarCenter;
			iconTexture = this.IconTexture;
			iconFrame = this.IconFrame;
			iconColor = this.IconColor;
			life = this.Life;
			lifeMax = this.LifeMax;
			shield = this.Shield;
			shieldMax = this.ShieldMax;
			iconScale = this.IconScale;
			showText = this.ShowText;
			textOffset = this.TextOffset;
		}

		/// <summary>
		/// The texture with fixed dimensions (516x348) containing all the necessary parts.
		/// </summary>
		// Token: 0x04005E55 RID: 24149
		public Texture2D BarTexture;

		/// <summary>
		/// The screen position of the center of the bar.
		/// </summary>
		// Token: 0x04005E56 RID: 24150
		public Vector2 BarCenter;

		/// <summary>
		/// The displayed icon texture.
		/// </summary>
		// Token: 0x04005E57 RID: 24151
		public Texture2D IconTexture;

		/// <summary>
		/// The icon textures frame.
		/// </summary>
		// Token: 0x04005E58 RID: 24152
		public Rectangle IconFrame;

		/// <summary>
		/// The tint of the icon.
		/// </summary>
		// Token: 0x04005E59 RID: 24153
		public Color IconColor;

		/// <summary>
		/// The current life of the boss
		/// </summary>
		// Token: 0x04005E5A RID: 24154
		public float Life;

		/// <summary>
		/// The max life of the boss (the amount it spawned with)
		/// </summary>
		// Token: 0x04005E5B RID: 24155
		public float LifeMax;

		/// <summary>
		/// The current shield of the boss
		/// </summary>
		// Token: 0x04005E5C RID: 24156
		public float Shield;

		/// <summary>
		/// The max shield of the boss (may be 0 if the boss has no shield)
		/// </summary>
		// Token: 0x04005E5D RID: 24157
		public float ShieldMax;

		/// <summary>
		/// The scale the icon is drawn with. Defaults to 1f, modify if icon is bigger or smaller than 26x28.
		/// </summary>
		// Token: 0x04005E5E RID: 24158
		public float IconScale;

		/// <summary>
		/// If the current life (or shield) of the boss should be written on the bar.
		/// </summary>
		// Token: 0x04005E5F RID: 24159
		public bool ShowText;

		/// <summary>
		/// The text offset from the center (<see cref="F:Terraria.DataStructures.BossBarDrawParams.BarCenter" />)
		/// </summary>
		// Token: 0x04005E60 RID: 24160
		public Vector2 TextOffset;
	}
}
