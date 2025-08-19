using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics.Effects
{
	// Token: 0x02000113 RID: 275
	public class OverlayManager : EffectManager<Overlay>
	{
		// Token: 0x060016DB RID: 5851 RVA: 0x004C9ED0 File Offset: 0x004C80D0
		public OverlayManager()
		{
			for (int i = 0; i < this._activeOverlays.Length; i++)
			{
				this._activeOverlays[i] = new LinkedList<Overlay>();
			}
		}

		// Token: 0x060016DC RID: 5852 RVA: 0x004C9F20 File Offset: 0x004C8120
		public override void OnActivate(Overlay overlay, Vector2 position)
		{
			LinkedList<Overlay> linkedList = this._activeOverlays[(int)overlay.Priority];
			if (overlay.Mode == OverlayMode.FadeIn || overlay.Mode == OverlayMode.Active)
			{
				return;
			}
			if (overlay.Mode == OverlayMode.FadeOut)
			{
				linkedList.Remove(overlay);
				this._overlayCount--;
			}
			else
			{
				overlay.Opacity = 0f;
			}
			if (linkedList.Count != 0)
			{
				foreach (Overlay overlay2 in linkedList)
				{
					overlay2.Mode = OverlayMode.FadeOut;
				}
			}
			linkedList.AddLast(overlay);
			this._overlayCount++;
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x004C9FD8 File Offset: 0x004C81D8
		public void Update(GameTime gameTime)
		{
			for (int i = 0; i < this._activeOverlays.Length; i++)
			{
				LinkedListNode<Overlay> next;
				for (LinkedListNode<Overlay> linkedListNode = this._activeOverlays[i].First; linkedListNode != null; linkedListNode = next)
				{
					Overlay value = linkedListNode.Value;
					next = linkedListNode.Next;
					value.Update(gameTime);
					switch (value.Mode)
					{
					case OverlayMode.FadeIn:
						value.Opacity += (float)gameTime.ElapsedGameTime.TotalSeconds * 1f;
						if (value.Opacity >= 1f)
						{
							value.Opacity = 1f;
							value.Mode = OverlayMode.Active;
						}
						break;
					case OverlayMode.Active:
						value.Opacity = Math.Min(1f, value.Opacity + (float)gameTime.ElapsedGameTime.TotalSeconds * 1f);
						break;
					case OverlayMode.FadeOut:
						value.Opacity -= (float)gameTime.ElapsedGameTime.TotalSeconds * 1f;
						if (value.Opacity <= 0f)
						{
							value.Opacity = 0f;
							value.Mode = OverlayMode.Inactive;
							this._activeOverlays[i].Remove(linkedListNode);
							this._overlayCount--;
						}
						break;
					}
				}
			}
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x004CA124 File Offset: 0x004C8324
		public void Draw(SpriteBatch spriteBatch, RenderLayers layer)
		{
			if (this._overlayCount == 0)
			{
				return;
			}
			bool flag = false;
			for (int i = 0; i < this._activeOverlays.Length; i++)
			{
				for (LinkedListNode<Overlay> linkedListNode = this._activeOverlays[i].First; linkedListNode != null; linkedListNode = linkedListNode.Next)
				{
					Overlay value = linkedListNode.Value;
					if (value.Layer == layer && value.IsVisible())
					{
						if (!flag)
						{
							spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.Transform);
							flag = true;
						}
						value.Draw(spriteBatch);
					}
				}
			}
			if (flag)
			{
				spriteBatch.End();
			}
		}

		// Token: 0x0400139B RID: 5019
		private const float OPACITY_RATE = 1f;

		// Token: 0x0400139C RID: 5020
		private LinkedList<Overlay>[] _activeOverlays = new LinkedList<Overlay>[Enum.GetNames(typeof(EffectPriority)).Length];

		// Token: 0x0400139D RID: 5021
		private int _overlayCount;
	}
}
