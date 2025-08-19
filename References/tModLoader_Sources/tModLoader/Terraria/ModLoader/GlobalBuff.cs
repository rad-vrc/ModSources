using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This class allows you to modify the behavior of any buff in the game.
	/// </summary>
	// Token: 0x02000168 RID: 360
	public abstract class GlobalBuff : ModType
	{
		// Token: 0x06001C6C RID: 7276 RVA: 0x004D36DC File Offset: 0x004D18DC
		protected sealed override void Register()
		{
			ModTypeLookup<GlobalBuff>.Register(this);
			BuffLoader.globalBuffs.Add(this);
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x004D36EF File Offset: 0x004D18EF
		public sealed override void SetupContent()
		{
			this.SetStaticDefaults();
		}

		/// <summary>
		/// Allows you to make the buff with the given ID give certain effects to a player. If you remove the buff from the player, make sure the decrement the buffIndex parameter by 1.
		/// </summary>
		// Token: 0x06001C6E RID: 7278 RVA: 0x004D36F7 File Offset: 0x004D18F7
		public virtual void Update(int type, Player player, ref int buffIndex)
		{
		}

		/// <summary>
		/// Allows you to make the buff with the given ID give certain effects to an NPC. If you remove the buff from the NPC, make sure to decrement the buffIndex parameter by 1.
		/// </summary>
		// Token: 0x06001C6F RID: 7279 RVA: 0x004D36F9 File Offset: 0x004D18F9
		public virtual void Update(int type, NPC npc, ref int buffIndex)
		{
		}

		/// <summary>
		/// Allows to you make special things happen when adding the given type of buff to a player when the player already has that buff. Return true to block the vanilla re-apply code from being called; returns false by default. The vanilla re-apply code sets the buff time to the "time" argument if that argument is larger than the current buff time. (For Mana Sickness, the vanilla re-apply code adds the "time" argument to the current buff time.)
		/// </summary>
		// Token: 0x06001C70 RID: 7280 RVA: 0x004D36FB File Offset: 0x004D18FB
		public virtual bool ReApply(int type, Player player, int time, int buffIndex)
		{
			return false;
		}

		/// <summary>
		/// Allows to you make special things happen when adding the given buff type to an NPC when the NPC already has that buff. Return true to block the vanilla re-apply code from being called; returns false by default. The vanilla re-apply code sets the buff time to the "time" argument if that argument is larger than the current buff time.
		/// </summary>
		// Token: 0x06001C71 RID: 7281 RVA: 0x004D36FE File Offset: 0x004D18FE
		public virtual bool ReApply(int type, NPC npc, int time, int buffIndex)
		{
			return false;
		}

		/// <summary>
		/// Allows you to modify the name and tooltip that displays when the mouse hovers over the buff icon, as well as the color the buff's name is drawn in.
		/// </summary>
		// Token: 0x06001C72 RID: 7282 RVA: 0x004D3701 File Offset: 0x004D1901
		public virtual void ModifyBuffText(int type, ref string buffName, ref string tip, ref int rare)
		{
		}

		/// <summary>
		/// If you are using the DrawCustomBuffTip hook, then you must use this hook as well. Calculate the location (relative to the origin) of the bottom-right corner of everything you will draw, and add that location to the sizes parameter.
		/// </summary>
		// Token: 0x06001C73 RID: 7283 RVA: 0x004D3703 File Offset: 0x004D1903
		public virtual void CustomBuffTipSize(string buffTip, List<Vector2> sizes)
		{
		}

		/// <summary>
		/// Allows you to draw whatever you want when a buff tooltip is drawn. The originX and originY parameters are the top-left corner of everything that's drawn; you should add these to the position argument passed to SpriteBatch.Draw.
		/// </summary>
		// Token: 0x06001C74 RID: 7284 RVA: 0x004D3705 File Offset: 0x004D1905
		public virtual void DrawCustomBuffTip(string buffTip, SpriteBatch spriteBatch, int originX, int originY)
		{
		}

		/// <summary>
		/// Allows you to draw things before the default draw code is ran. Return false to prevent drawing the buff. Returns true by default.
		/// </summary>
		/// <param name="spriteBatch">The spriteBatch that is drawn on</param>
		/// <param name="type">The buff type</param>
		/// <param name="buffIndex">The index in Main.LocalPlayer.buffType and .buffTime of the buff</param>
		/// <param name="drawParams">The draw parameters for the buff</param>
		/// <returns><see langword="true" /> for allowing drawing, <see langword="false" /> for preventing drawing</returns>
		// Token: 0x06001C75 RID: 7285 RVA: 0x004D3707 File Offset: 0x004D1907
		public virtual bool PreDraw(SpriteBatch spriteBatch, int type, int buffIndex, ref BuffDrawParams drawParams)
		{
			return true;
		}

		/// <summary>
		/// Allows you to draw things after the buff has been drawn. skipped is true if you or another mod has skipped drawing the buff (possibly hiding it or in favor of new visuals).
		/// </summary>
		/// <param name="spriteBatch">The spriteBatch that is drawn on</param>
		/// <param name="type">The buff type</param>
		/// <param name="buffIndex">The index in Main.LocalPlayer.buffType and .buffTime of the buff</param>
		/// <param name="drawParams">The draw parameters for the buff</param>
		// Token: 0x06001C76 RID: 7286 RVA: 0x004D370A File Offset: 0x004D190A
		public virtual void PostDraw(SpriteBatch spriteBatch, int type, int buffIndex, BuffDrawParams drawParams)
		{
		}

		/// <summary>
		/// Allows you to make things happen when the buff icon is right-clicked. Return false to prevent the buff from being cancelled.
		/// </summary>
		/// <param name="type">The buff type</param>
		/// <param name="buffIndex">The index in Main.LocalPlayer.buffType and .buffTime of the buff</param>
		/// <returns><see langword="true" /> for allowing the buff to be cancelled, <see langword="false" /> to prevent the buff from being cancelled</returns>
		// Token: 0x06001C77 RID: 7287 RVA: 0x004D370C File Offset: 0x004D190C
		public virtual bool RightClick(int type, int buffIndex)
		{
			return true;
		}
	}
}
