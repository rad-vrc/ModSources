using System;
using Microsoft.Xna.Framework;

namespace Terraria.UI.Gamepad
{
	// Token: 0x020000BE RID: 190
	public class UILinkPoint
	{
		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06001655 RID: 5717 RVA: 0x004B34F7 File Offset: 0x004B16F7
		// (set) Token: 0x06001656 RID: 5718 RVA: 0x004B34FF File Offset: 0x004B16FF
		public int Page { get; private set; }

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x06001657 RID: 5719 RVA: 0x004B3508 File Offset: 0x004B1708
		// (remove) Token: 0x06001658 RID: 5720 RVA: 0x004B3540 File Offset: 0x004B1740
		public event Func<string> OnSpecialInteracts;

		// Token: 0x06001659 RID: 5721 RVA: 0x004B3575 File Offset: 0x004B1775
		public UILinkPoint(int id, bool enabled, int left, int right, int up, int down)
		{
			this.ID = id;
			this.Enabled = enabled;
			this.Left = left;
			this.Right = right;
			this.Up = up;
			this.Down = down;
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x004B35AA File Offset: 0x004B17AA
		public void SetPage(int page)
		{
			this.Page = page;
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x004B35B3 File Offset: 0x004B17B3
		public void Unlink()
		{
			this.Left = -3;
			this.Right = -4;
			this.Up = -1;
			this.Down = -2;
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x004B35D4 File Offset: 0x004B17D4
		public string SpecialInteractions()
		{
			if (this.OnSpecialInteracts != null)
			{
				return this.OnSpecialInteracts();
			}
			return string.Empty;
		}

		// Token: 0x04001284 RID: 4740
		public int ID;

		// Token: 0x04001285 RID: 4741
		public bool Enabled;

		// Token: 0x04001286 RID: 4742
		public Vector2 Position;

		// Token: 0x04001287 RID: 4743
		public int Left;

		// Token: 0x04001288 RID: 4744
		public int Right;

		// Token: 0x04001289 RID: 4745
		public int Up;

		// Token: 0x0400128A RID: 4746
		public int Down;
	}
}
