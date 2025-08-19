using System;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.UI;

namespace Terraria.Map
{
	// Token: 0x020003D0 RID: 976
	public class PingMapLayer : IMapLayer
	{
		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x0600335B RID: 13147 RVA: 0x00553B23 File Offset: 0x00551D23
		// (set) Token: 0x0600335C RID: 13148 RVA: 0x00553B2B File Offset: 0x00551D2B
		public bool Visible { get; set; } = true;

		// Token: 0x0600335D RID: 13149 RVA: 0x00553B34 File Offset: 0x00551D34
		public void Draw(ref MapOverlayDrawContext context, ref string text)
		{
			SpriteFrame frame = new SpriteFrame(1, 5);
			DateTime now = DateTime.Now;
			foreach (SlotVector<PingMapLayer.Ping>.ItemPair item in this._pings)
			{
				PingMapLayer.Ping value = item.Value;
				double totalSeconds = (now - value.Time).TotalSeconds;
				int num = (int)(totalSeconds * 10.0);
				frame.CurrentRow = (byte)(num % (int)frame.RowCount);
				context.Draw(TextureAssets.MapPing.Value, value.Position, frame, Alignment.Center);
				if (totalSeconds > 15.0)
				{
					this._pings.Remove(item.Id);
				}
			}
		}

		// Token: 0x0600335E RID: 13150 RVA: 0x00553C04 File Offset: 0x00551E04
		public void Add(Vector2 position)
		{
			if (this._pings.Count != this._pings.Capacity)
			{
				this._pings.Add(new PingMapLayer.Ping(position));
			}
		}

		// Token: 0x04001E3A RID: 7738
		private const double PING_DURATION_IN_SECONDS = 15.0;

		// Token: 0x04001E3B RID: 7739
		private const double PING_FRAME_RATE = 10.0;

		// Token: 0x04001E3C RID: 7740
		private readonly SlotVector<PingMapLayer.Ping> _pings = new SlotVector<PingMapLayer.Ping>(100);

		// Token: 0x02000B17 RID: 2839
		private struct Ping
		{
			// Token: 0x06005B60 RID: 23392 RVA: 0x006A5AE4 File Offset: 0x006A3CE4
			public Ping(Vector2 position)
			{
				this.Position = position;
				this.Time = DateTime.Now;
			}

			// Token: 0x04006EFA RID: 28410
			public readonly Vector2 Position;

			// Token: 0x04006EFB RID: 28411
			public readonly DateTime Time;
		}
	}
}
