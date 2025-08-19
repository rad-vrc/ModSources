using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;

namespace Terraria.ModLoader
{
	/// <summary>
	/// Represents an emote. Emotes are typically used by players or NPC. Players can use the emotes menu or chat commands to display an emote, while town NPC spawn emotes when talking to each other.<para />
	/// </summary>
	// Token: 0x020001AF RID: 431
	public abstract class ModEmoteBubble : ModType<EmoteBubble, ModEmoteBubble>, ILocalizedModType, IModType
	{
		/// <summary>
		/// The file name of this emote's texture file in the mod loader's file space.
		/// </summary>
		// Token: 0x170003AB RID: 939
		// (get) Token: 0x060020B9 RID: 8377 RVA: 0x004E3F6D File Offset: 0x004E216D
		public virtual string Texture
		{
			get
			{
				return (base.GetType().Namespace + "." + this.Name).Replace('.', '/');
			}
		}

		/// <summary>
		/// The internal ID of this EmoteBubble.
		/// </summary>
		// Token: 0x170003AC RID: 940
		// (get) Token: 0x060020BA RID: 8378 RVA: 0x004E3F93 File Offset: 0x004E2193
		// (set) Token: 0x060020BB RID: 8379 RVA: 0x004E3F9B File Offset: 0x004E219B
		public int Type { get; internal set; }

		/// <summary>
		/// This is the <see cref="P:Terraria.ModLoader.ModEmoteBubble.EmoteBubble" /> instance.
		/// </summary>
		// Token: 0x170003AD RID: 941
		// (get) Token: 0x060020BC RID: 8380 RVA: 0x004E3FA4 File Offset: 0x004E21A4
		public EmoteBubble EmoteBubble
		{
			get
			{
				return base.Entity;
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x060020BD RID: 8381 RVA: 0x004E3FAC File Offset: 0x004E21AC
		public virtual string LocalizationCategory
		{
			get
			{
				return "Emotes";
			}
		}

		/// <summary>
		/// This is the name that will show up as the emote command.
		/// </summary>
		// Token: 0x170003AF RID: 943
		// (get) Token: 0x060020BE RID: 8382 RVA: 0x004E3FB3 File Offset: 0x004E21B3
		public virtual LocalizedText Command
		{
			get
			{
				return this.GetLocalization("Command", () => this.Name.ToLower());
			}
		}

		// Token: 0x060020BF RID: 8383 RVA: 0x004E3FCC File Offset: 0x004E21CC
		public sealed override void SetupContent()
		{
			ModContent.Request<Texture2D>(this.Texture, 2);
			this.SetStaticDefaults();
		}

		// Token: 0x060020C0 RID: 8384 RVA: 0x004E3FE1 File Offset: 0x004E21E1
		protected sealed override void Register()
		{
			ModTypeLookup<ModEmoteBubble>.Register(this);
			this.Type = EmoteBubbleLoader.Add(this);
		}

		// Token: 0x060020C1 RID: 8385 RVA: 0x004E3FF5 File Offset: 0x004E21F5
		protected override EmoteBubble CreateTemplateEntity()
		{
			return new EmoteBubble(this.Type, new WorldUIAnchor(), 180)
			{
				ModEmoteBubble = this
			};
		}

		/// <summary>
		/// Allows you to add this emote to a specific vanilla category.
		/// <br><b>This should only be called in <see cref="M:Terraria.ModLoader.ModType.SetStaticDefaults" /></b></br>
		/// </summary>
		/// <param name="categoryId">The category to which this emote will be added. Use <see cref="T:Terraria.GameContent.UI.EmoteID.Category" /> to select the category you want.</param>
		// Token: 0x060020C2 RID: 8386 RVA: 0x004E4014 File Offset: 0x004E2214
		public void AddToCategory(int categoryId)
		{
			List<ModEmoteBubble> list;
			if (!EmoteBubbleLoader.categoryEmoteLookup.TryGetValue(categoryId, out list))
			{
				EmoteBubbleLoader.categoryEmoteLookup.Add(categoryId, new List<ModEmoteBubble>
				{
					this
				});
				return;
			}
			list.Add(this);
		}

		/// <summary>
		/// Allows you to determine whether or not this emote can be seen in emotes menu. Returns true by default.
		/// <br />Do note that this doesn't effect emote command and NPC using.
		/// </summary>
		/// <returns>If true, this emote will be shown in emotes menu.</returns>
		// Token: 0x060020C3 RID: 8387 RVA: 0x004E404F File Offset: 0x004E224F
		public virtual bool IsUnlocked()
		{
			return true;
		}

		/// <summary>
		/// Gets called when your emote bubble spawns in world.
		/// </summary>
		// Token: 0x060020C4 RID: 8388 RVA: 0x004E4052 File Offset: 0x004E2252
		public virtual void OnSpawn()
		{
		}

		/// <summary>
		/// Allows you to modify the frame of this emote bubble. Return false to stop vanilla frame update code from running. Returns true by default.
		/// </summary>
		/// <returns>If false, the vanilla frame update code will not run.</returns>
		// Token: 0x060020C5 RID: 8389 RVA: 0x004E4054 File Offset: 0x004E2254
		public virtual bool UpdateFrame()
		{
			return true;
		}

		/// <summary>
		/// Allows you to modify the frame of this emote bubble which displays in emotes menu. Return false to stop vanilla frame update code from running. Returns true by default.
		/// <br />Do note that you should <b>NEVER</b> use the <see cref="P:Terraria.ModLoader.ModEmoteBubble.EmoteBubble" /> field in this method because it's null.
		/// </summary>
		/// <param name="frameCounter"></param>
		/// <returns>If false, the vanilla frame update code will not run.</returns>
		// Token: 0x060020C6 RID: 8390 RVA: 0x004E4057 File Offset: 0x004E2257
		public virtual bool UpdateFrameInEmoteMenu(ref int frameCounter)
		{
			return true;
		}

		/// <summary>
		/// Allows you to draw things behind this emote bubble, or to modify the way this emote bubble is drawn. Return false to stop the game from drawing the emote bubble (useful if you're manually drawing the emote bubble). Returns true by default.
		/// </summary>
		/// <param name="spriteBatch"></param>
		/// <param name="texture"></param>
		/// <param name="position"></param>
		/// <param name="frame"></param>
		/// <param name="origin"></param>
		/// <param name="spriteEffects"></param>
		/// <returns>If false, the vanilla drawing code will not run.</returns>
		// Token: 0x060020C7 RID: 8391 RVA: 0x004E405A File Offset: 0x004E225A
		public virtual bool PreDraw(SpriteBatch spriteBatch, Texture2D texture, Vector2 position, Rectangle frame, Vector2 origin, SpriteEffects spriteEffects)
		{
			return true;
		}

		/// <summary>
		/// Allows you to draw things in front of this emote bubble. This method is called even if PreDraw returns false.
		/// </summary>
		/// <param name="spriteBatch"></param>
		/// <param name="texture"></param>
		/// <param name="position"></param>
		/// <param name="frame"></param>
		/// <param name="origin"></param>
		/// <param name="spriteEffects"></param>
		// Token: 0x060020C8 RID: 8392 RVA: 0x004E405D File Offset: 0x004E225D
		public virtual void PostDraw(SpriteBatch spriteBatch, Texture2D texture, Vector2 position, Rectangle frame, Vector2 origin, SpriteEffects spriteEffects)
		{
		}

		/// <summary>
		/// Allows you to draw things behind this emote bubble that displays in emotes menu, or to modify the way this emote bubble is drawn. Return false to stop the game from drawing the emote bubble (useful if you're manually drawing the emote bubble). Returns true by default.
		/// <br />Do note that you should <b>NEVER</b> use the <see cref="P:Terraria.ModLoader.ModEmoteBubble.EmoteBubble" /> field in this method because it's null.
		/// </summary>
		/// <param name="spriteBatch"></param>
		/// <param name="uiEmoteButton">The <see cref="T:Terraria.GameContent.UI.Elements.EmoteButton" /> instance. You can get useful textures and frameCounter from it.</param>
		/// <param name="position"></param>
		/// <param name="frame"></param>
		/// <param name="origin"></param>
		/// <returns>If false, the vanilla drawing code will not run.</returns>
		// Token: 0x060020C9 RID: 8393 RVA: 0x004E405F File Offset: 0x004E225F
		public virtual bool PreDrawInEmoteMenu(SpriteBatch spriteBatch, EmoteButton uiEmoteButton, Vector2 position, Rectangle frame, Vector2 origin)
		{
			return true;
		}

		/// <summary>
		/// Allows you to draw things in front of this emote bubble. This method is called even if PreDraw returns false.
		/// <br />Do note that you should <b>NEVER</b> use the <see cref="P:Terraria.ModLoader.ModEmoteBubble.EmoteBubble" /> field in this method because it's null.
		/// </summary>
		/// <param name="spriteBatch"></param>
		/// <param name="uiEmoteButton">The <see cref="T:Terraria.GameContent.UI.Elements.EmoteButton" /> instance. You can get useful textures and frameCounter from it.</param>
		/// <param name="position"></param>
		/// <param name="frame"></param>
		/// <param name="origin"></param>
		// Token: 0x060020CA RID: 8394 RVA: 0x004E4062 File Offset: 0x004E2262
		public virtual void PostDrawInEmoteMenu(SpriteBatch spriteBatch, EmoteButton uiEmoteButton, Vector2 position, Rectangle frame, Vector2 origin)
		{
		}

		/// <summary>
		/// Allows you to modify the frame rectangle for drawing this emote. Useful for emote bubbles that share the same texture.
		/// </summary>
		/// <returns></returns>
		// Token: 0x060020CB RID: 8395 RVA: 0x004E4064 File Offset: 0x004E2264
		public virtual Rectangle? GetFrame()
		{
			return null;
		}

		/// <summary>
		/// Allows you to modify the frame rectangle for drawing this emote in emotes menu. Useful for emote bubbles that share the same texture.
		/// </summary>
		/// <param name="frame"></param>
		/// <param name="frameCounter"></param>
		/// <returns></returns>
		// Token: 0x060020CC RID: 8396 RVA: 0x004E407C File Offset: 0x004E227C
		public virtual Rectangle? GetFrameInEmoteMenu(int frame, int frameCounter)
		{
			return null;
		}
	}
}
