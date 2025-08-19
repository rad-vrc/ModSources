using System;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.UI;

namespace Terraria.Map
{
	// Token: 0x020000CC RID: 204
	public class PingMapLayer : IMapLayer
	{
		// Token: 0x0600146F RID: 5231 RVA: 0x004A31B8 File Offset: 0x004A13B8
		public void Draw(ref MapOverlayDrawContext context, ref string text)
		{
			SpriteFrame spriteFrame = new SpriteFrame(1, 5);
			DateTime now = DateTime.Now;
			foreach (SlotVector<PingMapLayer.Ping>.ItemPair itemPair in this._pings)
			{
				PingMapLayer.Ping value = itemPair.Value;
				double totalSeconds = (now - value.Time).TotalSeconds;
				int num = (int)(totalSeconds * 10.0);
				spriteFrame.CurrentRow = (byte)(num % (int)spriteFrame.RowCount);
				context.Draw(TextureAssets.MapPing.Value, value.Position, spriteFrame, Alignment.Center);
				if (totalSeconds > 15.0)
				{
					this._pings.Remove(itemPair.Id);
				}
			}
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x004A3288 File Offset: 0x004A1488
		public void Add(Vector2 position)
		{
			if (this._pings.Count == this._pings.Capacity)
			{
				return;
			}
			this._pings.Add(new PingMapLayer.Ping(position));
		}

		// Token: 0x0400120F RID: 4623
		private const double PING_DURATION_IN_SECONDS = 15.0;

		// Token: 0x04001210 RID: 4624
		private const double PING_FRAME_RATE = 10.0;

		// Token: 0x04001211 RID: 4625
		private readonly SlotVector<PingMapLayer.Ping> _pings = new SlotVector<PingMapLayer.Ping>(100);

		// Token: 0x02000561 RID: 1377
		private struct Ping
		{
			// Token: 0x0600312B RID: 12587 RVA: 0x005E4D82 File Offset: 0x005E2F82
			public Ping(Vector2 position)
			{
				this.Position = position;
				this.Time = DateTime.Now;
			}

			// Token: 0x040058F2 RID: 22770
			public readonly Vector2 Position;

			// Token: 0x040058F3 RID: 22771
			public readonly DateTime Time;
		}
	}
}
