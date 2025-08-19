using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.IO;

namespace Terraria.Graphics.Effects
{
	// Token: 0x0200046B RID: 1131
	public class FilterManager : EffectManager<Filter>
	{
		// Token: 0x1400005C RID: 92
		// (add) Token: 0x0600372B RID: 14123 RVA: 0x005869E4 File Offset: 0x00584BE4
		// (remove) Token: 0x0600372C RID: 14124 RVA: 0x00586A1C File Offset: 0x00584C1C
		public event Action OnPostDraw;

		// Token: 0x0600372D RID: 14125 RVA: 0x00586A51 File Offset: 0x00584C51
		public void BindTo(Preferences preferences)
		{
			preferences.OnSave += this.Configuration_OnSave;
			preferences.OnLoad += this.Configuration_OnLoad;
		}

		// Token: 0x0600372E RID: 14126 RVA: 0x00586A77 File Offset: 0x00584C77
		private void Configuration_OnSave(Preferences preferences)
		{
			preferences.Put("FilterLimit", this._filterLimit);
			preferences.Put("FilterPriorityThreshold", Enum.GetName(typeof(EffectPriority), this._priorityThreshold));
		}

		// Token: 0x0600372F RID: 14127 RVA: 0x00586AB4 File Offset: 0x00584CB4
		private void Configuration_OnLoad(Preferences preferences)
		{
			this._filterLimit = preferences.Get<int>("FilterLimit", 16);
			EffectPriority result;
			if (Enum.TryParse<EffectPriority>(preferences.Get<string>("FilterPriorityThreshold", "VeryLow"), out result))
			{
				this._priorityThreshold = result;
			}
		}

		// Token: 0x06003730 RID: 14128 RVA: 0x00586AF4 File Offset: 0x00584CF4
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

		// Token: 0x06003731 RID: 14129 RVA: 0x00586BD0 File Offset: 0x00584DD0
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

		// Token: 0x06003732 RID: 14130 RVA: 0x00586C1C File Offset: 0x00584E1C
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

		// Token: 0x06003733 RID: 14131 RVA: 0x00586D2C File Offset: 0x00584F2C
		public void EndCapture(RenderTarget2D finalTexture, RenderTarget2D screenTarget1, RenderTarget2D screenTarget2, Color clearColor)
		{
			if (!this._captureThisFrame)
			{
				return;
			}
			LinkedListNode<Filter> linkedListNode = this._activeFilters.First;
			int count = this._activeFilters.Count;
			Filter filter = null;
			RenderTarget2D renderTarget2D2 = screenTarget1;
			GraphicsDevice graphicsDevice = Main.instance.GraphicsDevice;
			int num = 0;
			if (Main.player[Main.myPlayer].gravDir == -1f)
			{
				graphicsDevice.SetRenderTarget(screenTarget2);
				graphicsDevice.Clear(clearColor);
				Main.spriteBatch.Begin(1, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Matrix.Invert(Main.GameViewMatrix.EffectMatrix));
				Main.spriteBatch.Draw(renderTarget2D2, Vector2.Zero, Color.White);
				Main.spriteBatch.End();
				renderTarget2D2 = screenTarget2;
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
							RenderTarget2D renderTarget2D3 = (renderTarget2D2 != screenTarget1) ? screenTarget1 : screenTarget2;
							graphicsDevice.SetRenderTarget(renderTarget2D3);
							graphicsDevice.Clear(clearColor);
							Main.spriteBatch.Begin(1, BlendState.AlphaBlend);
							filter.Apply();
							Main.spriteBatch.Draw(renderTarget2D2, Vector2.Zero, Main.ColorOfTheSkies);
							Main.spriteBatch.End();
							renderTarget2D2 = ((renderTarget2D2 != screenTarget1) ? screenTarget1 : screenTarget2);
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
				Main.spriteBatch.Begin(1, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.EffectMatrix);
			}
			else
			{
				Main.spriteBatch.Begin(1, BlendState.AlphaBlend);
			}
			if (filter != null)
			{
				filter.Apply();
				Main.spriteBatch.Draw(renderTarget2D2, Vector2.Zero, Main.ColorOfTheSkies);
			}
			else
			{
				Main.spriteBatch.Draw(renderTarget2D2, Vector2.Zero, Color.White);
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

		// Token: 0x06003734 RID: 14132 RVA: 0x00586F68 File Offset: 0x00585168
		public bool HasActiveFilter()
		{
			return this._activeFilters.Count != 0;
		}

		// Token: 0x06003735 RID: 14133 RVA: 0x00586F78 File Offset: 0x00585178
		public bool CanCapture()
		{
			return this.HasActiveFilter() || this.OnPostDraw != null;
		}

		// Token: 0x06003736 RID: 14134 RVA: 0x00586F90 File Offset: 0x00585190
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

		// Token: 0x040050E9 RID: 20713
		private const float OPACITY_RATE = 1f;

		// Token: 0x040050EA RID: 20714
		private LinkedList<Filter> _activeFilters = new LinkedList<Filter>();

		// Token: 0x040050EB RID: 20715
		private int _filterLimit = 16;

		// Token: 0x040050EC RID: 20716
		private EffectPriority _priorityThreshold;

		// Token: 0x040050ED RID: 20717
		private int _activeFilterCount;

		// Token: 0x040050EE RID: 20718
		private bool _captureThisFrame;
	}
}
