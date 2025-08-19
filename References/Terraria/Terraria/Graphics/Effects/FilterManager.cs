using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.IO;

namespace Terraria.Graphics.Effects
{
	// Token: 0x0200010D RID: 269
	public class FilterManager : EffectManager<Filter>
	{
		// Token: 0x1400002E RID: 46
		// (add) Token: 0x060016C1 RID: 5825 RVA: 0x004C9890 File Offset: 0x004C7A90
		// (remove) Token: 0x060016C2 RID: 5826 RVA: 0x004C98C8 File Offset: 0x004C7AC8
		public event Action OnPostDraw;

		// Token: 0x060016C4 RID: 5828 RVA: 0x004C9918 File Offset: 0x004C7B18
		public void BindTo(Preferences preferences)
		{
			preferences.OnSave += this.Configuration_OnSave;
			preferences.OnLoad += this.Configuration_OnLoad;
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x004C993E File Offset: 0x004C7B3E
		private void Configuration_OnSave(Preferences preferences)
		{
			preferences.Put("FilterLimit", this._filterLimit);
			preferences.Put("FilterPriorityThreshold", Enum.GetName(typeof(EffectPriority), this._priorityThreshold));
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x004C997C File Offset: 0x004C7B7C
		private void Configuration_OnLoad(Preferences preferences)
		{
			this._filterLimit = preferences.Get<int>("FilterLimit", 16);
			EffectPriority priorityThreshold;
			if (Enum.TryParse<EffectPriority>(preferences.Get<string>("FilterPriorityThreshold", "VeryLow"), out priorityThreshold))
			{
				this._priorityThreshold = priorityThreshold;
			}
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x004C99BC File Offset: 0x004C7BBC
		public override void OnActivate(Filter effect, Vector2 position)
		{
			if (this._activeFilters.Contains(effect))
			{
				if (effect.Active)
				{
					return;
				}
				if (effect.Priority >= this._priorityThreshold)
				{
					this._activeFilterCount--;
				}
				this._activeFilters.Remove(effect);
			}
			else
			{
				effect.Opacity = 0f;
			}
			if (effect.Priority >= this._priorityThreshold)
			{
				this._activeFilterCount++;
			}
			if (this._activeFilters.Count == 0)
			{
				this._activeFilters.AddLast(effect);
				return;
			}
			for (LinkedListNode<Filter> linkedListNode = this._activeFilters.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				Filter value = linkedListNode.Value;
				if (effect.Priority <= value.Priority)
				{
					this._activeFilters.AddAfter(linkedListNode, effect);
					return;
				}
			}
			this._activeFilters.AddLast(effect);
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x004C9A98 File Offset: 0x004C7C98
		public void BeginCapture(RenderTarget2D screenTarget1, Color clearColor)
		{
			if (this._activeFilterCount == 0 && this.OnPostDraw == null)
			{
				this._captureThisFrame = false;
				return;
			}
			this._captureThisFrame = true;
			Main.instance.GraphicsDevice.SetRenderTarget(screenTarget1);
			Main.instance.GraphicsDevice.Clear(clearColor);
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x004C9AE4 File Offset: 0x004C7CE4
		public void Update(GameTime gameTime)
		{
			LinkedListNode<Filter> linkedListNode = this._activeFilters.First;
			int count = this._activeFilters.Count;
			int num = 0;
			while (linkedListNode != null)
			{
				Filter value = linkedListNode.Value;
				LinkedListNode<Filter> next = linkedListNode.Next;
				bool flag = false;
				if (value.Priority >= this._priorityThreshold)
				{
					num++;
					if (num > this._activeFilterCount - this._filterLimit)
					{
						value.Update(gameTime);
						flag = true;
					}
				}
				if (value.Active && flag)
				{
					value.Opacity = Math.Min(value.Opacity + (float)gameTime.ElapsedGameTime.TotalSeconds * 1f, 1f);
				}
				else
				{
					value.Opacity = Math.Max(value.Opacity - (float)gameTime.ElapsedGameTime.TotalSeconds * 1f, 0f);
				}
				if (!value.Active && value.Opacity == 0f)
				{
					if (value.Priority >= this._priorityThreshold)
					{
						this._activeFilterCount--;
					}
					this._activeFilters.Remove(linkedListNode);
				}
				linkedListNode = next;
			}
		}

		// Token: 0x060016CA RID: 5834 RVA: 0x004C9BF4 File Offset: 0x004C7DF4
		public void EndCapture(RenderTarget2D finalTexture, RenderTarget2D screenTarget1, RenderTarget2D screenTarget2, Color clearColor)
		{
			if (!this._captureThisFrame)
			{
				return;
			}
			LinkedListNode<Filter> linkedListNode = this._activeFilters.First;
			int count = this._activeFilters.Count;
			Filter filter = null;
			RenderTarget2D renderTarget2D = screenTarget1;
			GraphicsDevice graphicsDevice = Main.instance.GraphicsDevice;
			int num = 0;
			if (Main.player[Main.myPlayer].gravDir == -1f)
			{
				graphicsDevice.SetRenderTarget(screenTarget2);
				graphicsDevice.Clear(clearColor);
				Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Matrix.Invert(Main.GameViewMatrix.EffectMatrix));
				Main.spriteBatch.Draw(renderTarget2D, Vector2.Zero, Color.White);
				Main.spriteBatch.End();
				renderTarget2D = screenTarget2;
			}
			while (linkedListNode != null)
			{
				Filter value = linkedListNode.Value;
				LinkedListNode<Filter> next = linkedListNode.Next;
				if (value.Priority >= this._priorityThreshold)
				{
					num++;
					if (num > this._activeFilterCount - this._filterLimit && value.IsVisible())
					{
						if (filter != null)
						{
							RenderTarget2D renderTarget;
							if (renderTarget2D == screenTarget1)
							{
								renderTarget = screenTarget2;
							}
							else
							{
								renderTarget = screenTarget1;
							}
							graphicsDevice.SetRenderTarget(renderTarget);
							graphicsDevice.Clear(clearColor);
							Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
							filter.Apply();
							Main.spriteBatch.Draw(renderTarget2D, Vector2.Zero, Main.ColorOfTheSkies);
							Main.spriteBatch.End();
							if (renderTarget2D == screenTarget1)
							{
								renderTarget2D = screenTarget2;
							}
							else
							{
								renderTarget2D = screenTarget1;
							}
						}
						filter = value;
					}
				}
				linkedListNode = next;
			}
			graphicsDevice.SetRenderTarget(finalTexture);
			graphicsDevice.Clear(clearColor);
			if (Main.player[Main.myPlayer].gravDir == -1f)
			{
				Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.EffectMatrix);
			}
			else
			{
				Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
			}
			if (filter != null)
			{
				filter.Apply();
				Main.spriteBatch.Draw(renderTarget2D, Vector2.Zero, Main.ColorOfTheSkies);
			}
			else
			{
				Main.spriteBatch.Draw(renderTarget2D, Vector2.Zero, Color.White);
			}
			Main.spriteBatch.End();
			for (int i = 0; i < 8; i++)
			{
				graphicsDevice.Textures[i] = null;
			}
			if (this.OnPostDraw != null)
			{
				this.OnPostDraw();
			}
		}

		// Token: 0x060016CB RID: 5835 RVA: 0x004C9E35 File Offset: 0x004C8035
		public bool HasActiveFilter()
		{
			return this._activeFilters.Count != 0;
		}

		// Token: 0x060016CC RID: 5836 RVA: 0x004C9E45 File Offset: 0x004C8045
		public bool CanCapture()
		{
			return this.HasActiveFilter() || this.OnPostDraw != null;
		}

		// Token: 0x04001388 RID: 5000
		private const float OPACITY_RATE = 1f;

		// Token: 0x0400138A RID: 5002
		private LinkedList<Filter> _activeFilters = new LinkedList<Filter>();

		// Token: 0x0400138B RID: 5003
		private int _filterLimit = 16;

		// Token: 0x0400138C RID: 5004
		private EffectPriority _priorityThreshold;

		// Token: 0x0400138D RID: 5005
		private int _activeFilterCount;

		// Token: 0x0400138E RID: 5006
		private bool _captureThisFrame;
	}
}
