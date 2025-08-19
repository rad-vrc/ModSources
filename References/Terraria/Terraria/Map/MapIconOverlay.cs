using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Terraria.Map
{
	// Token: 0x020000CA RID: 202
	public class MapIconOverlay
	{
		// Token: 0x06001468 RID: 5224 RVA: 0x004A2EDC File Offset: 0x004A10DC
		public MapIconOverlay AddLayer(IMapLayer layer)
		{
			this._layers.Add(layer);
			return this;
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x004A2EEC File Offset: 0x004A10EC
		public void Draw(Vector2 mapPosition, Vector2 mapOffset, Rectangle? clippingRect, float mapScale, float drawScale, ref string text)
		{
			MapOverlayDrawContext mapOverlayDrawContext = new MapOverlayDrawContext(mapPosition, mapOffset, clippingRect, mapScale, drawScale);
			foreach (IMapLayer mapLayer in this._layers)
			{
				mapLayer.Draw(ref mapOverlayDrawContext, ref text);
			}
		}

		// Token: 0x04001209 RID: 4617
		private readonly List<IMapLayer> _layers = new List<IMapLayer>();
	}
}
