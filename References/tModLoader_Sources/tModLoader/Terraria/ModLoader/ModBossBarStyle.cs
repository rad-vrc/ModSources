using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.BigProgressBar;

namespace Terraria.ModLoader
{
	/// <summary>
	/// A class that is used to swap out the entire boss bar display system with your own implementation
	/// </summary>
	// Token: 0x020001A3 RID: 419
	public abstract class ModBossBarStyle : ModType
	{
		/// <summary>
		/// Checks if the selected style matches this ModBossBarStyle.
		/// </summary>
		// Token: 0x1700038E RID: 910
		// (get) Token: 0x0600202C RID: 8236 RVA: 0x004E2CA2 File Offset: 0x004E0EA2
		public bool IsSelected
		{
			get
			{
				return BossBarLoader.CurrentStyle == this;
			}
		}

		/// <summary>
		/// Controls the name that shows up in the menu selection. If not overridden, it will use this mod's display name.
		/// </summary>
		// Token: 0x1700038F RID: 911
		// (get) Token: 0x0600202D RID: 8237 RVA: 0x004E2CAC File Offset: 0x004E0EAC
		public virtual string DisplayName
		{
			get
			{
				return base.Mod.DisplayNameClean;
			}
		}

		/// <summary>
		/// Return true to skip update code for boss bars. Useful if you want to use your own code for finding out which NPCs to track. Returns false by default.
		/// </summary>
		// Token: 0x17000390 RID: 912
		// (get) Token: 0x0600202E RID: 8238 RVA: 0x004E2CB9 File Offset: 0x004E0EB9
		public virtual bool PreventUpdate
		{
			get
			{
				return false;
			}
		}

		/// <summary>
		/// Return true to skip draw code for boss bars. Useful if you want to use your own code for drawing boss bars. Returns false by default.
		/// </summary>
		// Token: 0x17000391 RID: 913
		// (get) Token: 0x0600202F RID: 8239 RVA: 0x004E2CBC File Offset: 0x004E0EBC
		public virtual bool PreventDraw
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002030 RID: 8240 RVA: 0x004E2CBF File Offset: 0x004E0EBF
		protected sealed override void Register()
		{
			BossBarLoader.AddBossBarStyle(this);
		}

		// Token: 0x06002031 RID: 8241 RVA: 0x004E2CC7 File Offset: 0x004E0EC7
		public sealed override void SetupContent()
		{
			this.SetStaticDefaults();
		}

		/// <summary>
		/// Runs after update code for boss bars (skipped if PreventUpdate returns true), can be used to identify which NPCs to draw.
		/// </summary>
		/// <param name="currentBar">The boss bar that vanilla update code decided to draw. Can be null if skipped or if no suitable NPCs found. Can be casted to ModBossBar</param>
		/// <param name="info">Contains the index of the NPC the game decided to focus on</param>
		// Token: 0x06002032 RID: 8242 RVA: 0x004E2CCF File Offset: 0x004E0ECF
		public virtual void Update(IBigProgressBar currentBar, ref BigProgressBarInfo info)
		{
		}

		/// <summary>
		/// Called when this ModBossBarStyle is selected
		/// </summary>
		// Token: 0x06002033 RID: 8243 RVA: 0x004E2CD1 File Offset: 0x004E0ED1
		public virtual void OnSelected()
		{
		}

		/// <summary>
		/// Called when this ModBossBarStyle is deselected
		/// </summary>
		// Token: 0x06002034 RID: 8244 RVA: 0x004E2CD3 File Offset: 0x004E0ED3
		public virtual void OnDeselected()
		{
		}

		/// <summary>
		/// Runs after draw code for boss bars (skipped if PreventDraw returns true), can be used to draw your own bars, or reinvoke draw for currently selected-to-draw bar
		/// </summary>
		/// <param name="spriteBatch">The spriteBatch that is drawn on</param>
		/// <param name="currentBar">The boss bar that vanilla update code decided to draw. Can be null if skipped or if no suitable NPCs found. Can be casted to ModBossBar</param>
		/// <param name="info">Contains the index of the NPC the game decided to focus on</param>
		// Token: 0x06002035 RID: 8245 RVA: 0x004E2CD5 File Offset: 0x004E0ED5
		public virtual void Draw(SpriteBatch spriteBatch, IBigProgressBar currentBar, BigProgressBarInfo info)
		{
		}
	}
}
