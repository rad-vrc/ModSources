using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Terraria.Graphics.Effects
{
	// Token: 0x02000109 RID: 265
	public abstract class EffectManager<T> where T : GameEffect
	{
		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060016AD RID: 5805 RVA: 0x004C95FF File Offset: 0x004C77FF
		public bool IsLoaded
		{
			get
			{
				return this._isLoaded;
			}
		}

		// Token: 0x17000200 RID: 512
		public T this[string key]
		{
			get
			{
				T result;
				if (this._effects.TryGetValue(key, out result))
				{
					return result;
				}
				return default(T);
			}
			set
			{
				this.Bind(key, value);
			}
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x004C963A File Offset: 0x004C783A
		public void Bind(string name, T effect)
		{
			this._effects[name] = effect;
			if (this._isLoaded)
			{
				effect.Load();
			}
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x004C965C File Offset: 0x004C785C
		public void Load()
		{
			if (this._isLoaded)
			{
				return;
			}
			this._isLoaded = true;
			foreach (T t in this._effects.Values)
			{
				t.Load();
			}
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x004C96C8 File Offset: 0x004C78C8
		public T Activate(string name, Vector2 position = default(Vector2), params object[] args)
		{
			if (!this._effects.ContainsKey(name))
			{
				throw new MissingEffectException(string.Concat(new object[]
				{
					"Unable to find effect named: ",
					name,
					". Type: ",
					typeof(T),
					"."
				}));
			}
			T t = this._effects[name];
			this.OnActivate(t, position);
			t.Activate(position, args);
			return t;
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x004C9740 File Offset: 0x004C7940
		public void Deactivate(string name, params object[] args)
		{
			if (!this._effects.ContainsKey(name))
			{
				throw new MissingEffectException(string.Concat(new object[]
				{
					"Unable to find effect named: ",
					name,
					". Type: ",
					typeof(T),
					"."
				}));
			}
			T t = this._effects[name];
			this.OnDeactivate(t);
			t.Deactivate(args);
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public virtual void OnActivate(T effect, Vector2 position)
		{
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public virtual void OnDeactivate(T effect)
		{
		}

		// Token: 0x0400137C RID: 4988
		protected bool _isLoaded;

		// Token: 0x0400137D RID: 4989
		protected Dictionary<string, T> _effects = new Dictionary<string, T>();
	}
}
