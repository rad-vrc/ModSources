using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics.Effects
{
	// Token: 0x02000470 RID: 1136
	public class OverlayManager : EffectManager<Overlay>
	{
		// Token: 0x06003746 RID: 14150 RVA: 0x00587090 File Offset: 0x00585290
		public OverlayManager()
		{
			for (int i = 0; i < this._activeOverlays.Length; i++)
			{
				this._activeOverlays[i] = new LinkedList<Overlay>();
			}
		}

		// Token: 0x06003747 RID: 14151 RVA: 0x005870E0 File Offset: 0x005852E0
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

		// Token: 0x06003748 RID: 14152 RVA: 0x00587198 File Offset: 0x00585398
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

		// Token: 0x06003749 RID: 14153 RVA: 0x005872E4 File Offset: 0x005854E4
		public void Draw(SpriteBatch spriteBatch, RenderLayers layer, bool beginSpriteBatch = false)
		{
			if (this._overlayCount == 0)
			{
				return;
			}
			bool flag = !beginSpriteBatch;
			for (int i = 0; i < this._activeOverlays.Length; i++)
			{
				for (LinkedListNode<Overlay> linkedListNode = this._activeOverlays[i].First; linkedListNode != null; linkedListNode = linkedListNode.Next)
				{
					Overlay value = linkedListNode.Value;
					if (value.Layer == layer && value.IsVisible())
					{
						if (!flag)
						{
							spriteBatch.Begin(1, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.Transform);
							flag = true;
						}
						value.Draw(spriteBatch);
					}
				}
			}
			if (flag && beginSpriteBatch)
			{
				spriteBatch.End();
			}
		}

		// Token: 0x040050F6 RID: 20726
		private const float OPACITY_RATE = 1f;

		// Token: 0x040050F7 RID: 20727
		private LinkedList<Overlay>[] _activeOverlays = new LinkedList<Overlay>[Enum.GetNames(typeof(EffectPriority)).Length];

		// Token: 0x040050F8 RID: 20728
		private int _overlayCount;
	}
}
