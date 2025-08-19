using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Terraria.Map
{
	// Token: 0x020003CD RID: 973
	public class MapIconOverlay
	{
		// Token: 0x06003344 RID: 13124 RVA: 0x0055369B File Offset: 0x0055189B
		public MapIconOverlay()
		{
			this._readOnlyLayers = this._layers.AsReadOnly();
		}

		// Token: 0x06003345 RID: 13125 RVA: 0x005536BF File Offset: 0x005518BF
		internal MapIconOverlay AddLayer(IMapLayer layer)
		{
			this._layers.Add(layer);
			return this;
		}

		// Token: 0x06003346 RID: 13126 RVA: 0x005536D0 File Offset: 0x005518D0
		public void Draw(Vector2 mapPosition, Vector2 mapOffset, Rectangle? clippingRect, float mapScale, float drawScale, ref string text)
		{
			MapOverlayDrawContext context = new MapOverlayDrawContext(mapPosition, mapOffset, clippingRect, mapScale, drawScale);
			foreach (IMapLayer mapLayer in this._layers)
			{
				mapLayer.Visible = true;
			}
			SystemLoader.PreDrawMapIconOverlay(this._readOnlyLayers, context);
			foreach (IMapLayer layer in this._layers)
			{
				if (layer.Visible)
				{
					layer.Draw(ref context, ref text);
				}
			}
		}

		// Token: 0x04001E2F RID: 7727
		private readonly List<IMapLayer> _layers = new List<IMapLayer>();

		// Token: 0x04001E30 RID: 7728
		private IReadOnlyList<IMapLayer> _readOnlyLayers;
	}
}
