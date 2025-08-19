using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace Terraria.ModLoader
{
	/// <summary>
	/// A class that is used to modify existing boss health bars. To add them, use ModBossBar instead.
	/// </summary>
	// Token: 0x02000167 RID: 359
	public abstract class GlobalBossBar : ModType
	{
		// Token: 0x06001C67 RID: 7271 RVA: 0x004D36BF File Offset: 0x004D18BF
		protected sealed override void Register()
		{
			BossBarLoader.AddGlobalBossBar(this);
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x004D36C7 File Offset: 0x004D18C7
		public sealed override void SetupContent()
		{
			this.SetStaticDefaults();
		}

		/// <summary>
		/// Allows you to draw things before the default draw code is ran. Return false to prevent drawing the bar. Returns true by default.
		/// </summary>
		/// <param name="spriteBatch">The spriteBatch that is drawn on</param>
		/// <param name="npc">The NPC this bar is focused on</param>
		/// <param name="drawParams">The draw parameters for the boss bar</param>
		/// <returns><see langword="true" /> for allowing drawing, <see langword="false" /> for preventing drawing</returns>
		// Token: 0x06001C69 RID: 7273 RVA: 0x004D36CF File Offset: 0x004D18CF
		public virtual bool PreDraw(SpriteBatch spriteBatch, NPC npc, ref BossBarDrawParams drawParams)
		{
			return true;
		}

		/// <summary>
		/// Allows you to draw things after the bar has been drawn. skipped is true if you or another mod has skipped drawing the bar (possibly hiding it or in favor of new visuals).
		/// </summary>
		/// <param name="spriteBatch">The spriteBatch that is drawn on</param>
		/// <param name="npc">The NPC this bar is focused on</param>
		/// <param name="drawParams">The draw parameters for the boss bar</param>
		// Token: 0x06001C6A RID: 7274 RVA: 0x004D36D2 File Offset: 0x004D18D2
		public virtual void PostDraw(SpriteBatch spriteBatch, NPC npc, BossBarDrawParams drawParams)
		{
		}
	}
}
