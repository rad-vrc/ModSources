using System;
using Microsoft.Xna.Framework;

namespace Terraria.Graphics.Effects
{
	// Token: 0x0200010E RID: 270
	public abstract class GameEffect
	{
		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060016CD RID: 5837 RVA: 0x004C9E5A File Offset: 0x004C805A
		public bool IsLoaded
		{
			get
			{
				return this._isLoaded;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060016CE RID: 5838 RVA: 0x004C9E62 File Offset: 0x004C8062
		public EffectPriority Priority
		{
			get
			{
				return this._priority;
			}
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x004C9E6A File Offset: 0x004C806A
		public void Load()
		{
			if (this._isLoaded)
			{
				return;
			}
			this._isLoaded = true;
			this.OnLoad();
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public virtual void OnLoad()
		{
		}

		// Token: 0x060016D1 RID: 5841
		public abstract bool IsVisible();

		// Token: 0x060016D2 RID: 5842
		public abstract void Activate(Vector2 position, params object[] args);

		// Token: 0x060016D3 RID: 5843
		public abstract void Deactivate(params object[] args);

		// Token: 0x0400138F RID: 5007
		public float Opacity;

		// Token: 0x04001390 RID: 5008
		protected bool _isLoaded;

		// Token: 0x04001391 RID: 5009
		protected EffectPriority _priority;
	}
}
