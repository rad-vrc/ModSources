using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace Terraria.UI
{
	// Token: 0x0200008B RID: 139
	[DebuggerDisplay("Snap Point - {Name} {Id}")]
	public class SnapPoint
	{
		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600121F RID: 4639 RVA: 0x00491349 File Offset: 0x0048F549
		// (set) Token: 0x06001220 RID: 4640 RVA: 0x00491351 File Offset: 0x0048F551
		public int Id { get; private set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06001221 RID: 4641 RVA: 0x0049135A File Offset: 0x0048F55A
		// (set) Token: 0x06001222 RID: 4642 RVA: 0x00491362 File Offset: 0x0048F562
		public Vector2 Position { get; private set; }

		// Token: 0x06001223 RID: 4643 RVA: 0x0049136B File Offset: 0x0048F56B
		public SnapPoint(string name, int id, Vector2 anchor, Vector2 offset)
		{
			this.Name = name;
			this.Id = id;
			this._anchor = anchor;
			this._offset = offset;
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x00491390 File Offset: 0x0048F590
		public void Calculate(UIElement element)
		{
			CalculatedStyle dimensions = element.GetDimensions();
			this.Position = dimensions.Position() + this._offset + this._anchor * new Vector2(dimensions.Width, dimensions.Height);
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x004913DD File Offset: 0x0048F5DD
		public void ThisIsAHackThatChangesTheSnapPointsInfo(Vector2 anchor, Vector2 offset, int id)
		{
			this._anchor = anchor;
			this._offset = offset;
			this.Id = id;
		}

		// Token: 0x04001003 RID: 4099
		public string Name;

		// Token: 0x04001006 RID: 4102
		private Vector2 _anchor;

		// Token: 0x04001007 RID: 4103
		private Vector2 _offset;
	}
}
