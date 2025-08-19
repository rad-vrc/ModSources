using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics.Effects
{
	// Token: 0x02000107 RID: 263
	public abstract class CustomSky : GameEffect
	{
		// Token: 0x06001698 RID: 5784
		public abstract void Update(GameTime gameTime);

		// Token: 0x06001699 RID: 5785
		public abstract void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth);

		// Token: 0x0600169A RID: 5786
		public abstract bool IsActive();

		// Token: 0x0600169B RID: 5787
		public abstract void Reset();

		// Token: 0x0600169C RID: 5788 RVA: 0x001D7265 File Offset: 0x001D5465
		public virtual Color OnTileColor(Color inColor)
		{
			return inColor;
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x0003D9EB File Offset: 0x0003BBEB
		public virtual float GetCloudAlpha()
		{
			return 1f;
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x0003266D File Offset: 0x0003086D
		public override bool IsVisible()
		{
			return true;
		}
	}
}
