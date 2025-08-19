using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI;
using Terraria.GameContent.UI.Elements;

namespace Terraria.ModLoader
{
	// Token: 0x02000169 RID: 361
	public abstract class GlobalEmoteBubble : GlobalType<EmoteBubble, GlobalEmoteBubble>
	{
		// Token: 0x06001C79 RID: 7289 RVA: 0x004D3717 File Offset: 0x004D1917
		protected sealed override void Register()
		{
			ModTypeLookup<GlobalEmoteBubble>.Register(this);
			EmoteBubbleLoader.globalEmoteBubbles.Add(this);
		}

		// Token: 0x06001C7A RID: 7290 RVA: 0x004D372A File Offset: 0x004D192A
		public sealed override void SetupContent()
		{
			this.SetStaticDefaults();
		}

		/// <summary>
		/// Gets called when emote bubbles spawn in world.
		/// </summary>
		/// <param name="emoteBubble"></param>
		// Token: 0x06001C7B RID: 7291 RVA: 0x004D3732 File Offset: 0x004D1932
		public virtual void OnSpawn(EmoteBubble emoteBubble)
		{
		}

		/// <summary>
		/// Allows you to modify the frame of this emote bubble. Return false to stop vanilla frame update code from running. Returns true by default.
		/// </summary>
		/// <param name="emoteBubble"></param>
		/// <returns>If false, the vanilla frame update code will not run.</returns>
		// Token: 0x06001C7C RID: 7292 RVA: 0x004D3734 File Offset: 0x004D1934
		public virtual bool UpdateFrame(EmoteBubble emoteBubble)
		{
			return true;
		}

		/// <summary>
		/// Allows you to modify the frame of this emote bubble which displays in emotes menu. Return false to stop vanilla frame update code from running. Returns true by default.
		/// <br />Do note that 
		/// </summary>
		/// <param name="emoteType">The emote id for this emote.</param>
		/// <param name="frameCounter"></param>
		/// <returns>If false, the vanilla frame update code will not run.</returns>
		// Token: 0x06001C7D RID: 7293 RVA: 0x004D3737 File Offset: 0x004D1937
		public virtual bool UpdateFrameInEmoteMenu(int emoteType, ref int frameCounter)
		{
			return true;
		}

		/// <summary>
		/// Allows you to draw things behind this emote bubble, or to modify the way this emote bubble is drawn. Return false to stop the game from drawing the emote bubble (useful if you're manually drawing the emote bubble). Returns true by default.
		/// </summary>
		/// <param name="emoteBubble"></param>
		/// <param name="spriteBatch"></param>
		/// <param name="texture"></param>
		/// <param name="position"></param>
		/// <param name="frame"></param>
		/// <param name="origin"></param>
		/// <param name="spriteEffects"></param>
		/// <returns>If false, the vanilla drawing code will not run.</returns>
		// Token: 0x06001C7E RID: 7294 RVA: 0x004D373A File Offset: 0x004D193A
		public virtual bool PreDraw(EmoteBubble emoteBubble, SpriteBatch spriteBatch, Texture2D texture, Vector2 position, Rectangle frame, Vector2 origin, SpriteEffects spriteEffects)
		{
			return true;
		}

		/// <summary>
		/// Allows you to draw things in front of this emote bubble. This method is called even if PreDraw returns false.
		/// </summary>
		/// <param name="emoteBubble"></param>
		/// <param name="spriteBatch"></param>
		/// <param name="texture"></param>
		/// <param name="position"></param>
		/// <param name="frame"></param>
		/// <param name="origin"></param>
		/// <param name="spriteEffects"></param>
		// Token: 0x06001C7F RID: 7295 RVA: 0x004D373D File Offset: 0x004D193D
		public virtual void PostDraw(EmoteBubble emoteBubble, SpriteBatch spriteBatch, Texture2D texture, Vector2 position, Rectangle frame, Vector2 origin, SpriteEffects spriteEffects)
		{
		}

		/// <summary>
		/// Allows you to draw things behind this emote bubble that displays in emotes menu, or to modify the way this emote bubble is drawn. Return false to stop the game from drawing the emote bubble (useful if you're manually drawing the emote bubble). Returns true by default.
		/// </summary>
		/// <param name="emoteType"></param>
		/// <param name="spriteBatch"></param>
		/// <param name="uiEmoteButton">The <see cref="T:Terraria.GameContent.UI.Elements.EmoteButton" /> instance. You can get useful textures and frameCounter from it.</param>
		/// <param name="position"></param>
		/// <param name="frame"></param>
		/// <param name="origin"></param>
		/// <returns>If false, the vanilla drawing code will not run.</returns>
		// Token: 0x06001C80 RID: 7296 RVA: 0x004D373F File Offset: 0x004D193F
		public virtual bool PreDrawInEmoteMenu(int emoteType, SpriteBatch spriteBatch, EmoteButton uiEmoteButton, Vector2 position, Rectangle frame, Vector2 origin)
		{
			return true;
		}

		/// <summary>
		/// Allows you to draw things in front of this emote bubble. This method is called even if PreDraw returns false.
		/// </summary>
		/// <param name="emoteType"></param>
		/// <param name="spriteBatch"></param>
		/// <param name="uiEmoteButton">The <see cref="T:Terraria.GameContent.UI.Elements.EmoteButton" /> instance. You can get useful textures and frameCounter from it.</param>
		/// <param name="position"></param>
		/// <param name="frame"></param>
		/// <param name="origin"></param>
		// Token: 0x06001C81 RID: 7297 RVA: 0x004D3742 File Offset: 0x004D1942
		public virtual void PostDrawInEmoteMenu(int emoteType, SpriteBatch spriteBatch, EmoteButton uiEmoteButton, Vector2 position, Rectangle frame, Vector2 origin)
		{
		}

		/// <summary>
		/// Allows you to modify the frame rectangle for drawing this emote. Useful for emote bubbles that share the same texture.
		/// </summary>
		/// <param name="emoteBubble"></param>
		/// <returns></returns>
		// Token: 0x06001C82 RID: 7298 RVA: 0x004D3744 File Offset: 0x004D1944
		public virtual Rectangle? GetFrame(EmoteBubble emoteBubble)
		{
			return null;
		}

		/// <summary>
		/// Allows you to modify the frame rectangle for drawing this emote in emotes menu. Useful for emote bubbles that share the same texture.
		/// </summary>
		/// <param name="emoteType"></param>
		/// <param name="frame"></param>
		/// <param name="frameCounter"></param>
		/// <returns></returns>
		// Token: 0x06001C83 RID: 7299 RVA: 0x004D375C File Offset: 0x004D195C
		public virtual Rectangle? GetFrameInEmoteMenu(int emoteType, int frame, int frameCounter)
		{
			return null;
		}
	}
}
