using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x02000127 RID: 295
	public class FlameParticle : ABasicParticle
	{
		// Token: 0x0600176D RID: 5997 RVA: 0x004D2A7E File Offset: 0x004D0C7E
		public override void FetchFromPool()
		{
			base.FetchFromPool();
			this.FadeOutNormalizedTime = 1f;
			this._timeTolive = 0f;
			this._timeSinceSpawn = 0f;
			this._indexOfPlayerWhoSpawnedThis = 0;
			this._packedShaderIndex = 0;
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x004D2AB5 File Offset: 0x004D0CB5
		public override void SetBasicInfo(Asset<Texture2D> textureAsset, Rectangle? frame, Vector2 initialVelocity, Vector2 initialLocalPosition)
		{
			base.SetBasicInfo(textureAsset, frame, initialVelocity, initialLocalPosition);
			this._origin = new Vector2((float)(this._frame.Width / 2), (float)(this._frame.Height - 2));
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x004D2AE9 File Offset: 0x004D0CE9
		public void SetTypeInfo(float timeToLive, int indexOfPlayerWhoSpawnedIt, int packedShaderIndex)
		{
			this._timeTolive = timeToLive;
			this._indexOfPlayerWhoSpawnedThis = indexOfPlayerWhoSpawnedIt;
			this._packedShaderIndex = packedShaderIndex;
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x004D2B00 File Offset: 0x004D0D00
		public override void Update(ref ParticleRendererSettings settings)
		{
			base.Update(ref settings);
			this._timeSinceSpawn += 1f;
			if (this._timeSinceSpawn >= this._timeTolive)
			{
				base.ShouldBeRemovedFromRenderer = true;
			}
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x004D2B30 File Offset: 0x004D0D30
		public override void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch)
		{
			Color color = new Color(120, 120, 120, 60) * Utils.GetLerpValue(1f, this.FadeOutNormalizedTime, this._timeSinceSpawn / this._timeTolive, true);
			Vector2 value = settings.AnchorPosition + this.LocalPosition;
			ulong num = Main.TileFrameSeed ^ ((ulong)this.LocalPosition.X << 32 | (ulong)((uint)this.LocalPosition.Y));
			Player player = Main.player[this._indexOfPlayerWhoSpawnedThis];
			for (int i = 0; i < 4; i++)
			{
				Vector2 value2 = new Vector2((float)Utils.RandomInt(ref num, -2, 3), (float)Utils.RandomInt(ref num, -2, 3));
				DrawData drawData = new DrawData(this._texture.Value, value + value2 * this.Scale, new Rectangle?(this._frame), color, this.Rotation, this._origin, this.Scale, SpriteEffects.None, 0f)
				{
					shader = this._packedShaderIndex
				};
				PlayerDrawHelper.SetShaderForData(player, 0, ref drawData);
				drawData.Draw(spritebatch);
			}
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		// Token: 0x04001423 RID: 5155
		public float FadeOutNormalizedTime = 1f;

		// Token: 0x04001424 RID: 5156
		private float _timeTolive;

		// Token: 0x04001425 RID: 5157
		private float _timeSinceSpawn;

		// Token: 0x04001426 RID: 5158
		private int _indexOfPlayerWhoSpawnedThis;

		// Token: 0x04001427 RID: 5159
		private int _packedShaderIndex;
	}
}
