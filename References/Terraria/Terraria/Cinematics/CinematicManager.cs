using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Terraria.Cinematics
{
	// Token: 0x02000460 RID: 1120
	public class CinematicManager
	{
		// Token: 0x06002CE1 RID: 11489 RVA: 0x005BC378 File Offset: 0x005BA578
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

		// Token: 0x06002CE2 RID: 11490 RVA: 0x005BC3F6 File Offset: 0x005BA5F6
		public void PlayFilm(Film film)
		{
			this._films.Add(film);
		}

		// Token: 0x06002CE3 RID: 11491 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void StopAll()
		{
		}

		// Token: 0x0400511E RID: 20766
		public static CinematicManager Instance = new CinematicManager();

		// Token: 0x0400511F RID: 20767
		private List<Film> _films = new List<Film>();
	}
}
