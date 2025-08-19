using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This class serves as a place for you to define a new buff and how that buff behaves.
	/// </summary>
	// Token: 0x020001A4 RID: 420
	public abstract class ModBuff : ModTexturedType, ILocalizedModType, IModType
	{
		/// <summary> The buff id of this buff. </summary>
		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06002037 RID: 8247 RVA: 0x004E2CDF File Offset: 0x004E0EDF
		// (set) Token: 0x06002038 RID: 8248 RVA: 0x004E2CE7 File Offset: 0x004E0EE7
		public int Type { get; internal set; }

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06002039 RID: 8249 RVA: 0x004E2CF0 File Offset: 0x004E0EF0
		public virtual string LocalizationCategory
		{
			get
			{
				return "Buffs";
			}
		}

		/// <summary> The translations of this buff's display name. </summary>
		// Token: 0x17000394 RID: 916
		// (get) Token: 0x0600203A RID: 8250 RVA: 0x004E2CF7 File Offset: 0x004E0EF7
		public virtual LocalizedText DisplayName
		{
			get
			{
				return this.GetLocalization("DisplayName", new Func<string>(base.PrettyPrintName));
			}
		}

		/// <summary> The translations of this buff's description. </summary>
		// Token: 0x17000395 RID: 917
		// (get) Token: 0x0600203B RID: 8251 RVA: 0x004E2D10 File Offset: 0x004E0F10
		public virtual LocalizedText Description
		{
			get
			{
				return this.GetLocalization("Description", null);
			}
		}

		// Token: 0x0600203C RID: 8252 RVA: 0x004E2D1E File Offset: 0x004E0F1E
		protected sealed override void Register()
		{
			ModTypeLookup<ModBuff>.Register(this);
			this.Type = BuffLoader.ReserveBuffID();
			BuffLoader.buffs.Add(this);
		}

		// Token: 0x0600203D RID: 8253 RVA: 0x004E2D3C File Offset: 0x004E0F3C
		public sealed override void SetupContent()
		{
			TextureAssets.Buff[this.Type] = ModContent.Request<Texture2D>(this.Texture, 2);
			this.SetStaticDefaults();
			BuffID.Search.Add(base.FullName, this.Type);
		}

		/// <summary>
		/// Allows you to modify the properties after initial loading has completed.
		/// <br /> This is where all buff related assignments go.
		/// <br /> For example:
		/// <list type="bullet">
		/// <item> Main.debuff[Type] = true; </item>
		/// <item> Main.buffNoTimeDisplay[Type] = true; </item>
		/// <item> Main.pvpBuff[Type] = true; </item>
		/// <item> Main.vanityPet[Type] = true; </item>
		/// <item> Main.lightPet[Type] = true; </item>
		/// </list>
		/// </summary>
		// Token: 0x0600203E RID: 8254 RVA: 0x004E2D72 File Offset: 0x004E0F72
		public override void SetStaticDefaults()
		{
		}

		/// <summary>
		/// Allows you to make this buff give certain effects to the given player. If you remove the buff from the player, make sure the decrement the buffIndex parameter by 1.
		/// </summary>
		/// <param name="player">The player to update this buff on.</param>
		/// <param name="buffIndex">The index in <see cref="F:Terraria.Player.buffType" /> and <see cref="F:Terraria.Player.buffType" /> of this buff. For use with <see cref="M:Terraria.Player.DelBuff(System.Int32)" />.</param>
		// Token: 0x0600203F RID: 8255 RVA: 0x004E2D74 File Offset: 0x004E0F74
		public virtual void Update(Player player, ref int buffIndex)
		{
		}

		/// <summary>
		/// Allows you to make this buff give certain effects to the given NPC. If you remove the buff from the NPC, make sure to decrement the buffIndex parameter by 1.
		/// </summary>
		// Token: 0x06002040 RID: 8256 RVA: 0x004E2D76 File Offset: 0x004E0F76
		public virtual void Update(NPC npc, ref int buffIndex)
		{
		}

		/// <summary>
		/// Allows to you make special things happen when adding this buff to a player when the player already has this buff. Return true to block the vanilla re-apply code from being called; returns false by default. The vanilla re-apply code sets the buff time to the "time" argument if that argument is larger than the current buff time.
		/// </summary>
		// Token: 0x06002041 RID: 8257 RVA: 0x004E2D78 File Offset: 0x004E0F78
		public virtual bool ReApply(Player player, int time, int buffIndex)
		{
			return false;
		}

		/// <summary>
		/// Allows to you make special things happen when adding this buff to an NPC when the NPC already has this buff. Return true to block the vanilla re-apply code from being called; returns false by default. The vanilla re-apply code sets the buff time to the "time" argument if that argument is larger than the current buff time.
		/// </summary>
		// Token: 0x06002042 RID: 8258 RVA: 0x004E2D7B File Offset: 0x004E0F7B
		public virtual bool ReApply(NPC npc, int time, int buffIndex)
		{
			return false;
		}

		/// <summary>
		/// Allows you to modify the name and tooltip that displays when the mouse hovers over the buff icon, as well as the color the buff's name is drawn in.
		/// </summary>
		// Token: 0x06002043 RID: 8259 RVA: 0x004E2D7E File Offset: 0x004E0F7E
		public virtual void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
		{
		}

		/// <summary>
		/// Allows you to draw things before the default draw code is ran. Return false to prevent drawing the buff. Returns true by default.
		/// </summary>
		/// <param name="spriteBatch">The spriteBatch that is drawn on</param>
		/// <param name="buffIndex">The index in Main.LocalPlayer.buffType and .buffTime of the buff</param>
		/// <param name="drawParams">The draw parameters for the buff</param>
		/// <returns><see langword="true" /> for allowing drawing, <see langword="false" /> for preventing drawing</returns>
		// Token: 0x06002044 RID: 8260 RVA: 0x004E2D80 File Offset: 0x004E0F80
		public virtual bool PreDraw(SpriteBatch spriteBatch, int buffIndex, ref BuffDrawParams drawParams)
		{
			return true;
		}

		/// <summary>
		/// Allows you to draw things after the buff has been drawn. skipped is true if you or another mod has skipped drawing the buff (possibly hiding it or in favor of new visuals).
		/// </summary>
		/// <param name="spriteBatch">The spriteBatch that is drawn on</param>
		/// <param name="buffIndex">The index in Main.LocalPlayer.buffType and .buffTime of the buff</param>
		/// <param name="drawParams">The draw parameters for the buff</param>
		// Token: 0x06002045 RID: 8261 RVA: 0x004E2D83 File Offset: 0x004E0F83
		public virtual void PostDraw(SpriteBatch spriteBatch, int buffIndex, BuffDrawParams drawParams)
		{
		}

		/// <summary>
		/// Allows you to make things happen when the buff icon is right-clicked. Return false to prevent the buff from being cancelled.
		/// </summary>
		/// <param name="buffIndex">The index in Main.LocalPlayer.buffType and .buffTime of the buff</param>
		/// <returns><see langword="true" /> for allowing the buff to be cancelled, <see langword="false" /> to prevent the buff from being cancelled</returns>
		// Token: 0x06002046 RID: 8262 RVA: 0x004E2D85 File Offset: 0x004E0F85
		public virtual bool RightClick(int buffIndex)
		{
			return true;
		}
	}
}
