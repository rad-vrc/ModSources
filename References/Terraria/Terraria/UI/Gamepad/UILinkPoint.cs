using System;
using Microsoft.Xna.Framework;

namespace Terraria.UI.Gamepad
{
	// Token: 0x020000A4 RID: 164
	public class UILinkPoint
	{
		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600135A RID: 4954 RVA: 0x0049ECC8 File Offset: 0x0049CEC8
		// (set) Token: 0x0600135B RID: 4955 RVA: 0x0049ECD0 File Offset: 0x0049CED0
		public int Page { get; private set; }

		// Token: 0x0600135C RID: 4956 RVA: 0x0049ECD9 File Offset: 0x0049CED9
		public UILinkPoint(int id, bool enabled, int left, int right, int up, int down)
		{
			this.ID = id;
			this.Enabled = enabled;
			this.Left = left;
			this.Right = right;
			this.Up = up;
			this.Down = down;
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x0049ED0E File Offset: 0x0049CF0E
		public void SetPage(int page)
		{
			this.Page = page;
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x0049ED17 File Offset: 0x0049CF17
		public void Unlink()
		{
			this.Left = -3;
			this.Right = -4;
			this.Up = -1;
			this.Down = -2;
		}

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x0600135F RID: 4959 RVA: 0x0049ED38 File Offset: 0x0049CF38
		// (remove) Token: 0x06001360 RID: 4960 RVA: 0x0049ED70 File Offset: 0x0049CF70
		public event Func<string> OnSpecialInteracts;

		// Token: 0x06001361 RID: 4961 RVA: 0x0049EDA5 File Offset: 0x0049CFA5
		public string SpecialInteractions()
		{
			if (this.OnSpecialInteracts != null)
			{
				return this.OnSpecialInteracts();
			}
			return string.Empty;
		}

		// Token: 0x04001193 RID: 4499
		public int ID;

		// Token: 0x04001195 RID: 4501
		public bool Enabled;

		// Token: 0x04001196 RID: 4502
		public Vector2 Position;

		// Token: 0x04001197 RID: 4503
		public int Left;

		// Token: 0x04001198 RID: 4504
		public int Right;

		// Token: 0x04001199 RID: 4505
		public int Up;

		// Token: 0x0400119A RID: 4506
		public int Down;
	}
}
