using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Terraria.Graphics.Effects
{
	// Token: 0x02000468 RID: 1128
	public abstract class EffectManager<T> where T : GameEffect
	{
		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06003718 RID: 14104 RVA: 0x00586759 File Offset: 0x00584959
		public bool IsLoaded
		{
			get
			{
				return this._isLoaded;
			}
		}

		// Token: 0x17000689 RID: 1673
		public T this[string key]
		{
			get
			{
				T value;
				if (this._effects.TryGetValue(key, out value))
				{
					return value;
				}
				return default(T);
			}
			set
			{
				this.Bind(key, value);
			}
		}

		// Token: 0x0600371B RID: 14107 RVA: 0x00586796 File Offset: 0x00584996
		public void Bind(string name, T effect)
		{
			this._effects[name] = effect;
			if (this._isLoaded)
			{
				effect.Load();
			}
		}

		// Token: 0x0600371C RID: 14108 RVA: 0x005867B8 File Offset: 0x005849B8
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

		// Token: 0x0600371D RID: 14109 RVA: 0x00586824 File Offset: 0x00584A24
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
			T val = this._effects[name];
			this.OnActivate(val, position);
			val.Activate(position, args);
			return val;
		}

		// Token: 0x0600371E RID: 14110 RVA: 0x0058689C File Offset: 0x00584A9C
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
			T val = this._effects[name];
			this.OnDeactivate(val);
			val.Deactivate(args);
		}

		// Token: 0x0600371F RID: 14111 RVA: 0x00586911 File Offset: 0x00584B11
		public virtual void OnActivate(T effect, Vector2 position)
		{
		}

		// Token: 0x06003720 RID: 14112 RVA: 0x00586913 File Offset: 0x00584B13
		public virtual void OnDeactivate(T effect)
		{
		}

		// Token: 0x040050DE RID: 20702
		protected bool _isLoaded;

		// Token: 0x040050DF RID: 20703
		protected internal Dictionary<string, T> _effects = new Dictionary<string, T>();
	}
}
