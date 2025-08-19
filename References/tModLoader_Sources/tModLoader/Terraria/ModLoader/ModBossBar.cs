using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.UI.BigProgressBar;

namespace Terraria.ModLoader
{
	/// <summary>
	/// A class that is used to create custom boss health bars for modded and vanilla NPCs.
	/// </summary>
	// Token: 0x020001A2 RID: 418
	public abstract class ModBossBar : ModTexturedType, IBigProgressBar
	{
		// Token: 0x1700038A RID: 906
		// (get) Token: 0x0600201F RID: 8223 RVA: 0x004E2B32 File Offset: 0x004E0D32
		public float Life
		{
			get
			{
				return this.life;
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06002020 RID: 8224 RVA: 0x004E2B3A File Offset: 0x004E0D3A
		public float LifeMax
		{
			get
			{
				return this.lifeMax;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06002021 RID: 8225 RVA: 0x004E2B42 File Offset: 0x004E0D42
		public float Shield
		{
			get
			{
				return this.shield;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06002022 RID: 8226 RVA: 0x004E2B4A File Offset: 0x004E0D4A
		public float ShieldMax
		{
			get
			{
				return this.shieldMax;
			}
		}

		// Token: 0x06002023 RID: 8227 RVA: 0x004E2B52 File Offset: 0x004E0D52
		protected sealed override void Register()
		{
			BossBarLoader.AddBossBar(this);
		}

		// Token: 0x06002024 RID: 8228 RVA: 0x004E2B5A File Offset: 0x004E0D5A
		public sealed override void SetupContent()
		{
			this.SetStaticDefaults();
		}

		/// <summary>
		/// Allows you to specify the icon texture, and optionally the frame it should be displayed on. The frame defaults to the entire texture otherwise.
		/// </summary>
		/// <param name="iconFrame">The frame the texture should be displayed on</param>
		/// <returns>The icon texture</returns>
		// Token: 0x06002025 RID: 8229 RVA: 0x004E2B62 File Offset: 0x004E0D62
		public virtual Asset<Texture2D> GetIconTexture(ref Rectangle? iconFrame)
		{
			return null;
		}

		/// <summary>
		/// Allows you to handle the logic for when and how this ModBossBar should work. You want to override this if you have a multi-segment NPC. Returns null by default. Failing to return false otherwise will lead to your bar being displayed at wrong times.
		/// <para>Return null to let the basic logic run after this hook is called (index validity check and assigning lifePercent to match the health of the NPC) and then allowing it to be drawn.</para>
		/// <para>Return true to allow this ModBossBar to be drawn.</para>
		/// <para>Return false to prevent this ModBossBar from being drawn so that the game will try to pick a different one.</para>
		/// </summary>
		/// <param name="info">Contains the index of the NPC the game decided to focus on</param>
		/// <param name="life">The current life of the boss</param>
		/// <param name="lifeMax">The max (initial) life of the boss</param>
		/// <param name="shield">The current shield of the boss</param>
		/// <param name="shieldMax">The max shield for the boss (may be 0 if the boss has no shield)</param>
		/// <returns><see langword="null" /> for "single-segment" NPC logic, <see langword="true" /> for allowing drawing, <see langword="false" /> for preventing drawing</returns>
		// Token: 0x06002026 RID: 8230 RVA: 0x004E2B68 File Offset: 0x004E0D68
		public virtual bool? ModifyInfo(ref BigProgressBarInfo info, ref float life, ref float lifeMax, ref float shield, ref float shieldMax)
		{
			return null;
		}

		/// <summary>
		/// Allows you to draw things before the default draw code is ran. Return false to prevent drawing the ModBossBar. Returns true by default.
		/// </summary>
		/// <param name="spriteBatch">The spriteBatch that is drawn on</param>
		/// <param name="npc">The NPC this ModBossBar is focused on</param>
		/// <param name="drawParams">The draw parameters for the boss bar</param>
		/// <returns><see langword="true" /> for allowing drawing, <see langword="false" /> for preventing drawing</returns>
		// Token: 0x06002027 RID: 8231 RVA: 0x004E2B7E File Offset: 0x004E0D7E
		public virtual bool PreDraw(SpriteBatch spriteBatch, NPC npc, ref BossBarDrawParams drawParams)
		{
			return true;
		}

		/// <summary>
		/// Allows you to draw things after the bar has been drawn. skipped is true if you or another mod has skipped drawing the bar in PreDraw (possibly hiding it or in favor of new visuals).
		/// </summary>
		/// <param name="spriteBatch">The spriteBatch that is drawn on</param>
		/// <param name="npc">The NPC this ModBossBar is focused on</param>
		/// <param name="drawParams">The draw parameters for the boss bar</param>
		// Token: 0x06002028 RID: 8232 RVA: 0x004E2B81 File Offset: 0x004E0D81
		public virtual void PostDraw(SpriteBatch spriteBatch, NPC npc, BossBarDrawParams drawParams)
		{
		}

		// Token: 0x06002029 RID: 8233 RVA: 0x004E2B84 File Offset: 0x004E0D84
		public bool ValidateAndCollectNecessaryInfo(ref BigProgressBarInfo info)
		{
			if (info.npcIndexToAimAt < 0 || info.npcIndexToAimAt > Main.maxNPCs)
			{
				return false;
			}
			bool? modify = this.ModifyInfo(ref info, ref this.life, ref this.lifeMax, ref this.shield, ref this.shieldMax);
			if (modify != null)
			{
				return modify.Value;
			}
			NPC npc = Main.npc[info.npcIndexToAimAt];
			if (!npc.active)
			{
				return false;
			}
			this.life = Utils.Clamp<float>((float)npc.life, 0f, (float)npc.lifeMax);
			this.lifeMax = (float)npc.lifeMax;
			return true;
		}

		// Token: 0x0600202A RID: 8234 RVA: 0x004E2C20 File Offset: 0x004E0E20
		public void Draw(ref BigProgressBarInfo info, SpriteBatch spriteBatch)
		{
			Rectangle? iconFrame = null;
			Texture2D iconTexture = (this.GetIconTexture(ref iconFrame) ?? TextureAssets.NpcHead[0]).Value;
			Rectangle value = iconFrame.GetValueOrDefault();
			if (iconFrame == null)
			{
				value = iconTexture.Frame(1, 1, 0, 0, 0, 0);
				iconFrame = new Rectangle?(value);
			}
			BigProgressBarHelper.DrawFancyBar(spriteBatch, this.life, this.lifeMax, iconTexture, iconFrame.Value, this.shield, this.shieldMax);
		}

		// Token: 0x040016A8 RID: 5800
		internal int index;

		// Token: 0x040016A9 RID: 5801
		private float life;

		// Token: 0x040016AA RID: 5802
		private float lifeMax;

		// Token: 0x040016AB RID: 5803
		private float shield;

		// Token: 0x040016AC RID: 5804
		private float shieldMax;
	}
}
