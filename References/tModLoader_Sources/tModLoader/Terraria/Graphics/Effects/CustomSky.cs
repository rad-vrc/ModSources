using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics.Effects
{
	// Token: 0x02000467 RID: 1127
	public abstract class CustomSky : GameEffect
	{
		// Token: 0x06003710 RID: 14096
		public abstract void Update(GameTime gameTime);

		// Token: 0x06003711 RID: 14097
		public abstract void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth);

		// Token: 0x06003712 RID: 14098
		public abstract bool IsActive();

		// Token: 0x06003713 RID: 14099
		public abstract void Reset();

		// Token: 0x06003714 RID: 14100 RVA: 0x00586744 File Offset: 0x00584944
		public virtual Color OnTileColor(Color inColor)
		{
			return inColor;
		}

		// Token: 0x06003715 RID: 14101 RVA: 0x00586747 File Offset: 0x00584947
		public virtual float GetCloudAlpha()
		{
			return 1f;
		}

		// Token: 0x06003716 RID: 14102 RVA: 0x0058674E File Offset: 0x0058494E
		public override bool IsVisible()
		{
			return true;
		}
	}
}
