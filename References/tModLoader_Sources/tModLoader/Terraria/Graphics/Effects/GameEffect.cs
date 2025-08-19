using System;
using Microsoft.Xna.Framework;

namespace Terraria.Graphics.Effects
{
	// Token: 0x0200046D RID: 1133
	public abstract class GameEffect
	{
		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06003739 RID: 14137 RVA: 0x00587027 File Offset: 0x00585227
		public bool IsLoaded
		{
			get
			{
				return this._isLoaded;
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x0600373A RID: 14138 RVA: 0x0058702F File Offset: 0x0058522F
		public EffectPriority Priority
		{
			get
			{
				return this._priority;
			}
		}

		// Token: 0x0600373B RID: 14139 RVA: 0x00587037 File Offset: 0x00585237
		public void Load()
		{
			if (!this._isLoaded)
			{
				this._isLoaded = true;
				this.OnLoad();
			}
		}

		// Token: 0x0600373C RID: 14140 RVA: 0x0058704E File Offset: 0x0058524E
		public virtual void OnLoad()
		{
		}

		// Token: 0x0600373D RID: 14141
		public abstract bool IsVisible();

		// Token: 0x0600373E RID: 14142
		public abstract void Activate(Vector2 position, params object[] args);

		// Token: 0x0600373F RID: 14143
		public abstract void Deactivate(params object[] args);

		// Token: 0x040050F1 RID: 20721
		public float Opacity;

		// Token: 0x040050F2 RID: 20722
		protected bool _isLoaded;

		// Token: 0x040050F3 RID: 20723
		protected EffectPriority _priority;
	}
}
