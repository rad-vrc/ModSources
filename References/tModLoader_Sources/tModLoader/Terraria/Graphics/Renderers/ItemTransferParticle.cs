using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x02000454 RID: 1108
	public class ItemTransferParticle : IPooledParticle, IParticle
	{
		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x0600367C RID: 13948 RVA: 0x0057C30E File Offset: 0x0057A50E
		// (set) Token: 0x0600367D RID: 13949 RVA: 0x0057C316 File Offset: 0x0057A516
		public bool ShouldBeRemovedFromRenderer { get; private set; }

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x0600367E RID: 13950 RVA: 0x0057C31F File Offset: 0x0057A51F
		// (set) Token: 0x0600367F RID: 13951 RVA: 0x0057C327 File Offset: 0x0057A527
		public bool IsRestingInPool { get; private set; }

		// Token: 0x06003680 RID: 13952 RVA: 0x0057C330 File Offset: 0x0057A530
		public ItemTransferParticle()
		{
			this._itemInstance = new Item();
		}

		// Token: 0x06003681 RID: 13953 RVA: 0x0057C344 File Offset: 0x0057A544
		public void Update(ref ParticleRendererSettings settings)
		{
			int num = this._lifeTimeCounted + 1;
			this._lifeTimeCounted = num;
			if (num >= this._lifeTimeTotal)
			{
				this.ShouldBeRemovedFromRenderer = true;
			}
		}

		// Token: 0x06003682 RID: 13954 RVA: 0x0057C374 File Offset: 0x0057A574
		public void Prepare(int itemType, int lifeTimeTotal, Vector2 playerPosition, Vector2 chestPosition)
		{
			this._itemInstance.SetDefaults(itemType);
			this._lifeTimeTotal = lifeTimeTotal;
			this.StartPosition = playerPosition;
			this.EndPosition = chestPosition;
			Vector2 vector = (this.EndPosition - this.StartPosition).SafeNormalize(Vector2.UnitY).RotatedBy(1.5707963705062866, default(Vector2));
			bool flag2 = vector.Y < 0f;
			bool flag = vector.Y == 0f;
			if (!flag2 || (flag && Main.rand.Next(2) == 0))
			{
				vector *= -1f;
			}
			vector..ctor(0f, -1f);
			float num2 = Vector2.Distance(this.EndPosition, this.StartPosition);
			this.BezierHelper1 = vector * num2 + Main.rand.NextVector2Circular(32f, 32f);
			this.BezierHelper2 = -vector * num2 + Main.rand.NextVector2Circular(32f, 32f);
		}

		// Token: 0x06003683 RID: 13955 RVA: 0x0057C484 File Offset: 0x0057A684
		public void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch)
		{
			float fromValue = (float)this._lifeTimeCounted / (float)this._lifeTimeTotal;
			float toMin = Utils.Remap(fromValue, 0.1f, 0.5f, 0f, 0.85f, true);
			toMin = Utils.Remap(fromValue, 0.5f, 0.9f, toMin, 1f, true);
			Vector2 result;
			Vector2.Hermite(ref this.StartPosition, ref this.BezierHelper1, ref this.EndPosition, ref this.BezierHelper2, toMin, ref result);
			float toMin2 = Utils.Remap(fromValue, 0f, 0.1f, 0f, 1f, true);
			toMin2 = Utils.Remap(fromValue, 0.85f, 0.95f, toMin2, 0f, true);
			float num = Utils.Remap(fromValue, 0f, 0.25f, 0f, 1f, true) * Utils.Remap(fromValue, 0.85f, 0.95f, 1f, 0f, true);
			ItemSlot.DrawItemIcon(this._itemInstance, 31, Main.spriteBatch, settings.AnchorPosition + result, this._itemInstance.scale * toMin2, 100f, Color.White * num);
		}

		// Token: 0x06003684 RID: 13956 RVA: 0x0057C59F File Offset: 0x0057A79F
		public void RestInPool()
		{
			this.IsRestingInPool = true;
		}

		// Token: 0x06003685 RID: 13957 RVA: 0x0057C5A8 File Offset: 0x0057A7A8
		public virtual void FetchFromPool()
		{
			this._lifeTimeCounted = 0;
			this._lifeTimeTotal = 0;
			this.IsRestingInPool = false;
			this.ShouldBeRemovedFromRenderer = false;
			this.StartPosition = (this.EndPosition = (this.BezierHelper1 = (this.BezierHelper2 = Vector2.Zero)));
		}

		// Token: 0x0400505B RID: 20571
		public Vector2 StartPosition;

		// Token: 0x0400505C RID: 20572
		public Vector2 EndPosition;

		// Token: 0x0400505D RID: 20573
		public Vector2 BezierHelper1;

		// Token: 0x0400505E RID: 20574
		public Vector2 BezierHelper2;

		// Token: 0x0400505F RID: 20575
		private Item _itemInstance;

		// Token: 0x04005060 RID: 20576
		private int _lifeTimeCounted;

		// Token: 0x04005061 RID: 20577
		private int _lifeTimeTotal;
	}
}
