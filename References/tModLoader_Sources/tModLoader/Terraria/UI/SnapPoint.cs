using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace Terraria.UI
{
	// Token: 0x020000B0 RID: 176
	[DebuggerDisplay("Snap Point - {Name} {Id}")]
	public class SnapPoint
	{
		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06001591 RID: 5521 RVA: 0x004B058D File Offset: 0x004AE78D
		// (set) Token: 0x06001592 RID: 5522 RVA: 0x004B0595 File Offset: 0x004AE795
		public int Id { get; private set; }

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06001593 RID: 5523 RVA: 0x004B059E File Offset: 0x004AE79E
		// (set) Token: 0x06001594 RID: 5524 RVA: 0x004B05A6 File Offset: 0x004AE7A6
		public Vector2 Position { get; private set; }

		// Token: 0x06001595 RID: 5525 RVA: 0x004B05AF File Offset: 0x004AE7AF
		public SnapPoint(string name, int id, Vector2 anchor, Vector2 offset)
		{
			this.Name = name;
			this.Id = id;
			this._anchor = anchor;
			this._offset = offset;
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x004B05D4 File Offset: 0x004AE7D4
		public void Calculate(UIElement element)
		{
			CalculatedStyle dimensions = element.GetDimensions();
			this.Position = dimensions.Position() + this._offset + this._anchor * new Vector2(dimensions.Width, dimensions.Height);
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x004B0621 File Offset: 0x004AE821
		public void ThisIsAHackThatChangesTheSnapPointsInfo(Vector2 anchor, Vector2 offset, int id)
		{
			this._anchor = anchor;
			this._offset = offset;
			this.Id = id;
		}

		// Token: 0x04001112 RID: 4370
		public string Name;

		// Token: 0x04001113 RID: 4371
		private Vector2 _anchor;

		// Token: 0x04001114 RID: 4372
		private Vector2 _offset;
	}
}
