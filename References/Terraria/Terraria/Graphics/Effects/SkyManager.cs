using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics.Effects
{
	// Token: 0x02000108 RID: 264
	public class SkyManager : EffectManager<CustomSky>
	{
		// Token: 0x060016A0 RID: 5792 RVA: 0x004C9384 File Offset: 0x004C7584
		public void Reset()
		{
			foreach (CustomSky customSky in this._effects.Values)
			{
				customSky.Reset();
			}
			this._activeSkies.Clear();
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x004C93E4 File Offset: 0x004C75E4
		public void Update(GameTime gameTime)
		{
			int num = Main.dayRate;
			if (num < 1)
			{
				num = 1;
			}
			for (int i = 0; i < num; i++)
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

		// Token: 0x060016A2 RID: 5794 RVA: 0x004C9443 File Offset: 0x004C7643
		public void Draw(SpriteBatch spriteBatch)
		{
			this.DrawDepthRange(spriteBatch, float.MinValue, float.MaxValue);
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x004C9456 File Offset: 0x004C7656
		public void DrawToDepth(SpriteBatch spriteBatch, float minDepth)
		{
			if (this._lastDepth <= minDepth)
			{
				return;
			}
			this.DrawDepthRange(spriteBatch, minDepth, this._lastDepth);
			this._lastDepth = minDepth;
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x004C9478 File Offset: 0x004C7678
		public void DrawDepthRange(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			foreach (CustomSky customSky in this._activeSkies)
			{
				customSky.Draw(spriteBatch, minDepth, maxDepth);
			}
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x004C94CC File Offset: 0x004C76CC
		public void DrawRemainingDepth(SpriteBatch spriteBatch)
		{
			this.DrawDepthRange(spriteBatch, float.MinValue, this._lastDepth);
			this._lastDepth = float.MinValue;
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x004C94EB File Offset: 0x004C76EB
		public void ResetDepthTracker()
		{
			this._lastDepth = float.MaxValue;
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x004C94F8 File Offset: 0x004C76F8
		public void SetStartingDepth(float depth)
		{
			this._lastDepth = depth;
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x004C9501 File Offset: 0x004C7701
		public override void OnActivate(CustomSky effect, Vector2 position)
		{
			this._activeSkies.Remove(effect);
			this._activeSkies.AddLast(effect);
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x004C9520 File Offset: 0x004C7720
		public Color ProcessTileColor(Color color)
		{
			foreach (CustomSky customSky in this._activeSkies)
			{
				color = customSky.OnTileColor(color);
			}
			return color;
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x004C9574 File Offset: 0x004C7774
		public float ProcessCloudAlpha()
		{
			float num = 1f;
			foreach (CustomSky customSky in this._activeSkies)
			{
				num *= customSky.GetCloudAlpha();
			}
			return MathHelper.Clamp(num, 0f, 1f);
		}

		// Token: 0x04001379 RID: 4985
		public static SkyManager Instance = new SkyManager();

		// Token: 0x0400137A RID: 4986
		private float _lastDepth;

		// Token: 0x0400137B RID: 4987
		private LinkedList<CustomSky> _activeSkies = new LinkedList<CustomSky>();
	}
}
