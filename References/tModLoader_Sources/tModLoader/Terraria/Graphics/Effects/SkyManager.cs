using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics.Effects
{
	// Token: 0x02000475 RID: 1141
	public class SkyManager : EffectManager<CustomSky>
	{
		// Token: 0x06003753 RID: 14163 RVA: 0x005874C4 File Offset: 0x005856C4
		public void Reset()
		{
			foreach (CustomSky customSky in this._effects.Values)
			{
				customSky.Reset();
			}
			this._activeSkies.Clear();
		}

		// Token: 0x06003754 RID: 14164 RVA: 0x00587524 File Offset: 0x00585724
		public void Update(GameTime gameTime)
		{
			for (int i = 0; i < Main.worldEventUpdates; i++)
			{
				LinkedListNode<CustomSky> next;
				for (LinkedListNode<CustomSky> linkedListNode = this._activeSkies.First; linkedListNode != null; linkedListNode = next)
				{
					CustomSky value = linkedListNode.Value;
					next = linkedListNode.Next;
					value.Update(gameTime);
					if (!value.IsActive())
					{
						this._activeSkies.Remove(linkedListNode);
					}
				}
			}
		}

		// Token: 0x06003755 RID: 14165 RVA: 0x0058757B File Offset: 0x0058577B
		public void Draw(SpriteBatch spriteBatch)
		{
			this.DrawDepthRange(spriteBatch, float.MinValue, float.MaxValue);
		}

		// Token: 0x06003756 RID: 14166 RVA: 0x0058758E File Offset: 0x0058578E
		public void DrawToDepth(SpriteBatch spriteBatch, float minDepth)
		{
			if (this._lastDepth > minDepth)
			{
				this.DrawDepthRange(spriteBatch, minDepth, this._lastDepth);
				this._lastDepth = minDepth;
			}
		}

		// Token: 0x06003757 RID: 14167 RVA: 0x005875B0 File Offset: 0x005857B0
		public void DrawDepthRange(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			foreach (CustomSky customSky in this._activeSkies)
			{
				customSky.Draw(spriteBatch, minDepth, maxDepth);
			}
		}

		// Token: 0x06003758 RID: 14168 RVA: 0x00587604 File Offset: 0x00585804
		public void DrawRemainingDepth(SpriteBatch spriteBatch)
		{
			this.DrawDepthRange(spriteBatch, float.MinValue, this._lastDepth);
			this._lastDepth = float.MinValue;
		}

		// Token: 0x06003759 RID: 14169 RVA: 0x00587623 File Offset: 0x00585823
		public void ResetDepthTracker()
		{
			this._lastDepth = float.MaxValue;
		}

		// Token: 0x0600375A RID: 14170 RVA: 0x00587630 File Offset: 0x00585830
		public void SetStartingDepth(float depth)
		{
			this._lastDepth = depth;
		}

		// Token: 0x0600375B RID: 14171 RVA: 0x00587639 File Offset: 0x00585839
		public override void OnActivate(CustomSky effect, Vector2 position)
		{
			this._activeSkies.Remove(effect);
			this._activeSkies.AddLast(effect);
		}

		// Token: 0x0600375C RID: 14172 RVA: 0x00587658 File Offset: 0x00585858
		public Color ProcessTileColor(Color color)
		{
			foreach (CustomSky customSky in this._activeSkies)
			{
				color = customSky.OnTileColor(color);
			}
			return color;
		}

		// Token: 0x0600375D RID: 14173 RVA: 0x005876AC File Offset: 0x005858AC
		public float ProcessCloudAlpha()
		{
			float num = 1f;
			foreach (CustomSky activeSky in this._activeSkies)
			{
				num *= activeSky.GetCloudAlpha();
			}
			return MathHelper.Clamp(num, 0f, 1f);
		}

		// Token: 0x0600375E RID: 14174 RVA: 0x00587718 File Offset: 0x00585918
		internal void DeactivateAll()
		{
			foreach (string key in this._effects.Keys)
			{
				if (base[key].IsActive())
				{
					base[key].Deactivate(Array.Empty<object>());
				}
			}
		}

		// Token: 0x0400510E RID: 20750
		public static SkyManager Instance = new SkyManager();

		// Token: 0x0400510F RID: 20751
		private float _lastDepth;

		// Token: 0x04005110 RID: 20752
		private LinkedList<CustomSky> _activeSkies = new LinkedList<CustomSky>();
	}
}
