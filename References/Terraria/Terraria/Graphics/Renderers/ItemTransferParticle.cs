using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x02000123 RID: 291
	public class ItemTransferParticle : IPooledParticle, IParticle
	{
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x0600174F RID: 5967 RVA: 0x004D22B7 File Offset: 0x004D04B7
		// (set) Token: 0x06001750 RID: 5968 RVA: 0x004D22BF File Offset: 0x004D04BF
		public bool ShouldBeRemovedFromRenderer { get; private set; }

		// Token: 0x06001751 RID: 5969 RVA: 0x004D22C8 File Offset: 0x004D04C8
		public ItemTransferParticle()
		{
			this._itemInstance = new Item();
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x004D22DC File Offset: 0x004D04DC
		public void Update(ref ParticleRendererSettings settings)
		{
			int num = this._lifeTimeCounted + 1;
			this._lifeTimeCounted = num;
			if (num >= this._lifeTimeTotal)
			{
				this.ShouldBeRemovedFromRenderer = true;
			}
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x004D230C File Offset: 0x004D050C
		public void Prepare(int itemType, int lifeTimeTotal, Vector2 playerPosition, Vector2 chestPosition)
		{
			this._itemInstance.SetDefaults(itemType);
			this._lifeTimeTotal = lifeTimeTotal;
			this.StartPosition = playerPosition;
			this.EndPosition = chestPosition;
			Vector2 vector = (this.EndPosition - this.StartPosition).SafeNormalize(Vector2.UnitY).RotatedBy(1.5707963705062866, default(Vector2));
			bool flag = vector.Y < 0f;
			bool flag2 = vector.Y == 0f;
			if (!flag || (flag2 && Main.rand.Next(2) == 0))
			{
				vector *= -1f;
			}
			vector = new Vector2(0f, -1f);
			float scaleFactor = Vector2.Distance(this.EndPosition, this.StartPosition);
			this.BezierHelper1 = vector * scaleFactor + Main.rand.NextVector2Circular(32f, 32f);
			this.BezierHelper2 = -vector * scaleFactor + Main.rand.NextVector2Circular(32f, 32f);
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x004D241C File Offset: 0x004D061C
		public void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch)
		{
			float fromValue = (float)this._lifeTimeCounted / (float)this._lifeTimeTotal;
			float num = Utils.Remap(fromValue, 0.1f, 0.5f, 0f, 0.85f, true);
			num = Utils.Remap(fromValue, 0.5f, 0.9f, num, 1f, true);
			Vector2 value;
			Vector2.Hermite(ref this.StartPosition, ref this.BezierHelper1, ref this.EndPosition, ref this.BezierHelper2, num, out value);
			float num2 = Utils.Remap(fromValue, 0f, 0.1f, 0f, 1f, true);
			num2 = Utils.Remap(fromValue, 0.85f, 0.95f, num2, 0f, true);
			float scale = Utils.Remap(fromValue, 0f, 0.25f, 0f, 1f, true) * Utils.Remap(fromValue, 0.85f, 0.95f, 1f, 0f, true);
			ItemSlot.DrawItemIcon(this._itemInstance, 31, Main.spriteBatch, settings.AnchorPosition + value, this._itemInstance.scale * num2, 100f, Color.White * scale);
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06001755 RID: 5973 RVA: 0x004D2537 File Offset: 0x004D0737
		// (set) Token: 0x06001756 RID: 5974 RVA: 0x004D253F File Offset: 0x004D073F
		public bool IsRestingInPool { get; private set; }

		// Token: 0x06001757 RID: 5975 RVA: 0x004D2548 File Offset: 0x004D0748
		public void RestInPool()
		{
			this.IsRestingInPool = true;
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x004D2554 File Offset: 0x004D0754
		public virtual void FetchFromPool()
		{
			this._lifeTimeCounted = 0;
			this._lifeTimeTotal = 0;
			this.IsRestingInPool = false;
			this.ShouldBeRemovedFromRenderer = false;
			this.StartPosition = (this.EndPosition = (this.BezierHelper1 = (this.BezierHelper2 = Vector2.Zero)));
		}

		// Token: 0x040013FE RID: 5118
		public Vector2 StartPosition;

		// Token: 0x040013FF RID: 5119
		public Vector2 EndPosition;

		// Token: 0x04001400 RID: 5120
		public Vector2 BezierHelper1;

		// Token: 0x04001401 RID: 5121
		public Vector2 BezierHelper2;

		// Token: 0x04001402 RID: 5122
		private Item _itemInstance;

		// Token: 0x04001403 RID: 5123
		private int _lifeTimeCounted;

		// Token: 0x04001404 RID: 5124
		private int _lifeTimeTotal;
	}
}
