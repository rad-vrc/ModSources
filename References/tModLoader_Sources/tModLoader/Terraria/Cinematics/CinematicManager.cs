using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Terraria.Cinematics
{
	// Token: 0x02000742 RID: 1858
	public class CinematicManager
	{
		// Token: 0x06004B63 RID: 19299 RVA: 0x0066A08C File Offset: 0x0066828C
		public void Update(GameTime gameTime)
		{
			if (this._films.Count > 0)
			{
				if (!this._films[0].IsActive)
				{
					this._films[0].OnBegin();
				}
				if (Main.hasFocus && !Main.gamePaused && !this._films[0].OnUpdate(gameTime))
				{
					this._films[0].OnEnd();
					this._films.RemoveAt(0);
				}
			}
		}

		// Token: 0x06004B64 RID: 19300 RVA: 0x0066A10A File Offset: 0x0066830A
		public void PlayFilm(Film film)
		{
			this._films.Add(film);
		}

		// Token: 0x06004B65 RID: 19301 RVA: 0x0066A118 File Offset: 0x00668318
		public void StopAll()
		{
		}

		// Token: 0x0400607F RID: 24703
		public static CinematicManager Instance = new CinematicManager();

		// Token: 0x04006080 RID: 24704
		private List<Film> _films = new List<Film>();
	}
}
