using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x0200044F RID: 1103
	public class FlameParticle : ABasicParticle
	{
		// Token: 0x06003669 RID: 13929 RVA: 0x0057BD9E File Offset: 0x00579F9E
		public override void FetchFromPool()
		{
			base.FetchFromPool();
			this.FadeOutNormalizedTime = 1f;
			this._timeTolive = 0f;
			this._timeSinceSpawn = 0f;
			this._indexOfPlayerWhoSpawnedThis = 0;
			this._packedShaderIndex = 0;
		}

		// Token: 0x0600366A RID: 13930 RVA: 0x0057BDD5 File Offset: 0x00579FD5
		public override void SetBasicInfo(Asset<Texture2D> textureAsset, Rectangle? frame, Vector2 initialVelocity, Vector2 initialLocalPosition)
		{
			base.SetBasicInfo(textureAsset, frame, initialVelocity, initialLocalPosition);
			this._origin = new Vector2((float)(this._frame.Width / 2), (float)(this._frame.Height - 2));
		}

		// Token: 0x0600366B RID: 13931 RVA: 0x0057BE09 File Offset: 0x0057A009
		public void SetTypeInfo(float timeToLive, int indexOfPlayerWhoSpawnedIt, int packedShaderIndex)
		{
			this._timeTolive = timeToLive;
			this._indexOfPlayerWhoSpawnedThis = indexOfPlayerWhoSpawnedIt;
			this._packedShaderIndex = packedShaderIndex;
		}

		// Token: 0x0600366C RID: 13932 RVA: 0x0057BE20 File Offset: 0x0057A020
		public override void Update(ref ParticleRendererSettings settings)
		{
			base.Update(ref settings);
			this._timeSinceSpawn += 1f;
			if (this._timeSinceSpawn >= this._timeTolive)
			{
				base.ShouldBeRemovedFromRenderer = true;
			}
		}

		// Token: 0x0600366D RID: 13933 RVA: 0x0057BE50 File Offset: 0x0057A050
		public override void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch)
		{
			Color color = new Color(120, 120, 120, 60) * Utils.GetLerpValue(1f, this.FadeOutNormalizedTime, this._timeSinceSpawn / this._timeTolive, true);
			Vector2 vector = settings.AnchorPosition + this.LocalPosition;
			ulong seed = Main.TileFrameSeed ^ ((ulong)this.LocalPosition.X << 32 | (ulong)((uint)this.LocalPosition.Y));
			Player player = Main.player[this._indexOfPlayerWhoSpawnedThis];
			for (int i = 0; i < 4; i++)
			{
				Vector2 position = vector + new Vector2((float)Utils.RandomInt(ref seed, -2, 3), (float)Utils.RandomInt(ref seed, -2, 3)) * this.Scale;
				DrawData cdd = new DrawData(this._texture.Value, position, new Rectangle?(this._frame), color, this.Rotation, this._origin, this.Scale, 0, 0f)
				{
					shader = this._packedShaderIndex
				};
				PlayerDrawHelper.SetShaderForData(player, 0, ref cdd);
				cdd.Draw(spritebatch);
			}
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		// Token: 0x04005048 RID: 20552
		public float FadeOutNormalizedTime = 1f;

		// Token: 0x04005049 RID: 20553
		private float _timeTolive;

		// Token: 0x0400504A RID: 20554
		private float _timeSinceSpawn;

		// Token: 0x0400504B RID: 20555
		private int _indexOfPlayerWhoSpawnedThis;

		// Token: 0x0400504C RID: 20556
		private int _packedShaderIndex;
	}
}
