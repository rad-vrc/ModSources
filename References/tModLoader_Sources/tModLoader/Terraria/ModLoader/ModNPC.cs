using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Utilities;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.IO;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This class serves as a place for you to place all your properties and hooks for each NPC.
	/// <br /> To use it, simply create a new class deriving from this one. Implementations will be registered automatically.
	/// <para /> The <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-NPC">Basic NPC Guide</see> teaches the basics of making a modded NPC.
	/// </summary>
	// Token: 0x020001B9 RID: 441
	public abstract class ModNPC : ModType<NPC, ModNPC>, ILocalizedModType, IModType
	{
		/// <summary> The NPC object that this ModNPC controls. </summary>
		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06002206 RID: 8710 RVA: 0x004E769D File Offset: 0x004E589D
		public NPC NPC
		{
			get
			{
				return base.Entity;
			}
		}

		/// <summary> Shorthand for NPC.type; </summary>
		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06002207 RID: 8711 RVA: 0x004E76A5 File Offset: 0x004E58A5
		public int Type
		{
			get
			{
				return this.NPC.type;
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06002208 RID: 8712 RVA: 0x004E76B2 File Offset: 0x004E58B2
		public virtual string LocalizationCategory
		{
			get
			{
				return "NPCs";
			}
		}

		/// <summary> The translations for the display name of this NPC. </summary>
		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06002209 RID: 8713 RVA: 0x004E76B9 File Offset: 0x004E58B9
		public virtual LocalizedText DisplayName
		{
			get
			{
				return this.GetLocalization("DisplayName", new Func<string>(base.PrettyPrintName));
			}
		}

		/// <summary>
		/// Allows you to modify the death message of a town NPC or boss. This also affects what the dropped tombstone will say in the case of a town NPC.
		/// <para /> If substitutions are not provided by using <see cref="M:Terraria.Localization.LocalizedText.WithFormatArgs(System.Object[])" />, the NPC name will be substituted into the "{0}" placeholder.
		/// <para /> This won't have any effect if the given NPC isn't a town NPC or a boss.
		/// <para /> Returns null by default, with the text being "NPC has been defeated!" for bosses and "NPC was slain..." (or "NPC has left!" if <see cref="F:Terraria.ID.NPCID.Sets.IsTownChild" /> or <see cref="F:Terraria.ID.NPCID.Sets.IsTownPet" /> are set to <see langword="true" />) for town NPCs.
		/// <para /> The keys "Announcement.HasBeenDefeated_Plural" and "Announcement.HasBeenDefeated_Single" will be useful if creating a generic boss defeat message with a custom boss name. For example:
		/// <br /> <c>Language.GetText("Announcement.HasBeenDefeated_Plural").WithFormatArgs(Language.GetText("Mods.MyMod.NPCs.MyBoss.TheTriplets"))</c>. Make sure to use GetText instead of GetTextValue when using WithFormatArgs so the message will properly sync.
		/// </summary>
		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x0600220A RID: 8714 RVA: 0x004E76D2 File Offset: 0x004E58D2
		public virtual LocalizedText DeathMessage
		{
			get
			{
				return null;
			}
		}

		/// <summary>
		/// The file name of this type's texture file in the mod loader's file space.<br />
		/// The resulting  Asset&lt;Texture2D&gt; can be retrieved using <see cref="F:Terraria.GameContent.TextureAssets.Npc" /> indexed by <see cref="P:Terraria.ModLoader.ModNPC.Type" /> if needed. <br />
		/// You can use a vanilla texture by returning <c>$"Terraria/Images/NPC_{NPCID.NPCNameHere}"</c> <br />
		/// </summary>
		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x0600220B RID: 8715 RVA: 0x004E76D5 File Offset: 0x004E58D5
		public virtual string Texture
		{
			get
			{
				return (base.GetType().Namespace + "." + this.Name).Replace('.', '/');
			}
		}

		/// <summary> The file name of this NPC's head texture file, to be used in autoloading. </summary>
		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x0600220C RID: 8716 RVA: 0x004E76FB File Offset: 0x004E58FB
		public virtual string HeadTexture
		{
			get
			{
				return this.Texture + "_Head";
			}
		}

		/// <summary> This file name of this NPC's boss head texture file, to be used in autoloading. </summary>
		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x0600220D RID: 8717 RVA: 0x004E770D File Offset: 0x004E590D
		public virtual string BossHeadTexture
		{
			get
			{
				return this.Texture + "_Head_Boss";
			}
		}

		/// <summary> Determines which type of vanilla NPC this ModNPC will copy the behavior (AI) of. Leave as 0 to not copy any behavior. Defaults to 0. </summary>
		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x0600220E RID: 8718 RVA: 0x004E771F File Offset: 0x004E591F
		// (set) Token: 0x0600220F RID: 8719 RVA: 0x004E7727 File Offset: 0x004E5927
		public int AIType { get; set; }

		/// <summary>
		/// Determines which type of vanilla NPC this ModNPC will copy the animation/framing logic of, which includes checks to sets such as <seealso cref="F:Terraria.Main.npcFrameCount" />
		/// and <seealso cref="F:Terraria.ID.NPCID.Sets.ExtraFramesCount" />, and others. For example, selecting the Guide's type will copy how many frames, extra frames, and attack frames the
		/// Guide has, and use those value for animation of this NPC. This is entirely based off of type and not the ModNPC instance itself; so be cautious if you
		/// change this NPC's type.
		/// </summary>
		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06002210 RID: 8720 RVA: 0x004E7730 File Offset: 0x004E5930
		// (set) Token: 0x06002211 RID: 8721 RVA: 0x004E7738 File Offset: 0x004E5938
		public int AnimationType { get; set; }

		/// <summary>
		/// The ID of the music that plays when this NPC is on or near the screen. Defaults to -1, which means music plays normally.
		/// <br /><br /> See <see cref="F:Terraria.Main.swapMusic" /> for information about playing alternate music when the Otherworld soundtrack is enabled.
		/// </summary>
		/// <remarks>
		/// Note: This property gets ignored if the game would not play music for this NPC by default (i.e. it's not a boss, or it doesn't belong to an invasion)
		/// </remarks>
		/// Will be superseded by ModSceneEffect. Kept for legacy.
		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06002212 RID: 8722 RVA: 0x004E7741 File Offset: 0x004E5941
		// (set) Token: 0x06002213 RID: 8723 RVA: 0x004E7749 File Offset: 0x004E5949
		public int Music { get; set; } = -1;

		/// <summary> The priority of the music that plays when this NPC is on or near the screen. </summary>
		/// Will be superseded by ModSceneEffect. Kept for legacy.
		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06002214 RID: 8724 RVA: 0x004E7752 File Offset: 0x004E5952
		// (set) Token: 0x06002215 RID: 8725 RVA: 0x004E775A File Offset: 0x004E595A
		public SceneEffectPriority SceneEffectPriority { get; set; } = SceneEffectPriority.BossLow;

		/// <summary> The vertical offset used for drawing this NPC. Defaults to 0. </summary>
		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06002216 RID: 8726 RVA: 0x004E7763 File Offset: 0x004E5963
		// (set) Token: 0x06002217 RID: 8727 RVA: 0x004E776B File Offset: 0x004E596B
		public float DrawOffsetY { get; set; }

		/// <summary> The type of NPC that this NPC will be considered as when determining banner drops and banner bonuses. Also known as the BannerID. By default this will be 0, which means this NPC is not associated with any banner. To give your NPC its own banner, set this field to the NPC's type and also set <see cref="P:Terraria.ModLoader.ModNPC.BannerItem" />:
		/// <code>
		/// Banner = Type;
		/// BannerItem = ModContent.ItemType&lt;ExampleWormHeadBanner&gt;();
		/// </code>
		/// <para /> For NPC with multiple parts, such as worms, be sure to set this to the same value for each ModNPC. Typically the head NPC type is used as the common Banner value. <see href="https://github.com/tModLoader/tModLoader/blob/stable/ExampleMod/Content/NPCs/ExampleWorm.cs">ExampleWorm.cs</see> is an example of this:
		/// <code>
		/// // In ExampleWormBody.SetDefaults:
		/// Banner = ModContent.NPCType&lt;ExampleWormHead&gt;();</code>
		/// <para /> For NPC that are variants of each other, it is also useful to share a Banner value so that each variant counts towards the same banner kill count rather than each having their own banner drop. <see href="https://github.com/tModLoader/tModLoader/blob/stable/ExampleMod/Content/NPCs/PartyZombie.cs">PartyZombie.cs</see> does this and shares the vanilla Zombie banner. By doing this it is affected by the Zombie banner and counts towards dropping it as well.
		/// <para /> To retrieve and use a vanilla banner value, use the <see cref="M:Terraria.Item.NPCtoBanner(System.Int32)" /> method:
		/// <code>Banner = Item.NPCtoBanner(NPCID.Zombie);</code>
		/// </summary>
		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06002218 RID: 8728 RVA: 0x004E7774 File Offset: 0x004E5974
		// (set) Token: 0x06002219 RID: 8729 RVA: 0x004E777C File Offset: 0x004E597C
		public int Banner { get; set; }

		/// <summary> The item type this NPC drops after some number of it is defeated. The exact number can be customized by using <see cref="F:Terraria.ID.ItemID.Sets.KillsToBanner" /> and defaults to 50. For any ModNPC whose <see cref="P:Terraria.ModLoader.ModNPC.Banner" /> field is set to the type of this NPC, that ModNPC will drop this banner. Must be set along with <see cref="P:Terraria.ModLoader.ModNPC.Banner" />.
		/// <para /> If querying the banner item drop for a specific NPC that you know has an assigned banner, you'll need to use the following for accurate results since BannerItem isn't necessarily assigned for npc sharing a banner: <c>Item.BannerToItem(Item.NPCtoBanner(npc.BannerID()));</c>
		/// </summary>
		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x0600221A RID: 8730 RVA: 0x004E7785 File Offset: 0x004E5985
		// (set) Token: 0x0600221B RID: 8731 RVA: 0x004E778D File Offset: 0x004E598D
		public int BannerItem { get; set; }

		/// <summary> The ModBiome Types associated with this NPC spawning, if applicable. Used in Bestiary.<para />Vanilla biomes are added to the bestiary via <see cref="M:Terraria.ModLoader.ModNPC.SetBestiary(Terraria.GameContent.Bestiary.BestiaryDatabase,Terraria.GameContent.Bestiary.BestiaryEntry)" /> directly.</summary>
		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x0600221C RID: 8732 RVA: 0x004E7796 File Offset: 0x004E5996
		// (set) Token: 0x0600221D RID: 8733 RVA: 0x004E779E File Offset: 0x004E599E
		public int[] SpawnModBiomes { get; set; } = new int[0];

		/// <summary> Setting this to true will make the NPC not appear in the housing menu nor make it find an house. </summary>
		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x0600221E RID: 8734 RVA: 0x004E77A7 File Offset: 0x004E59A7
		// (set) Token: 0x0600221F RID: 8735 RVA: 0x004E77AF File Offset: 0x004E59AF
		public bool TownNPCStayingHomeless { get; set; }

		// Token: 0x06002220 RID: 8736 RVA: 0x004E77B8 File Offset: 0x004E59B8
		protected override NPC CreateTemplateEntity()
		{
			return new NPC
			{
				ModNPC = this
			};
		}

		// Token: 0x06002221 RID: 8737 RVA: 0x004E77C8 File Offset: 0x004E59C8
		protected sealed override void Register()
		{
			ModTypeLookup<ModNPC>.Register(this);
			this.NPC.type = NPCLoader.Register(this);
			Type type = base.GetType();
			if (AttributeUtilities.GetAttribute<AutoloadHead>(type) != null)
			{
				base.Mod.AddNPCHeadTexture(this.NPC.type, this.HeadTexture);
			}
			if (AttributeUtilities.GetAttribute<AutoloadBossHead>(type) != null)
			{
				base.Mod.AddBossHeadTexture(this.BossHeadTexture, this.NPC.type);
			}
		}

		/// <summary>
		/// Allows you to change the emote that the NPC will pick
		/// <para /> Called in single player or on the server only.
		/// </summary>
		/// <param name="closestPlayer">The <see cref="T:Terraria.Player" /> closest to the NPC. You can check the biome the player is in and let the NPC pick the emote that corresponds to the biome.</param>
		/// <param name="emoteList">A list of emote IDs from which the NPC will randomly select one</param>
		/// <param name="otherAnchor">A <see cref="T:Terraria.GameContent.UI.WorldUIAnchor" /> instance that indicates the target of this emote conversation. Use this to get the instance of the <see cref="P:Terraria.ModLoader.ModNPC.NPC" /> or <see cref="T:Terraria.Player" /> this NPC is talking to.</param>
		/// <returns>Return null to use vanilla mechanic (pick one from the list), otherwise pick the emote by the returned ID. Returning -1 will prevent the emote from being used. Returns null by default</returns>
		// Token: 0x06002222 RID: 8738 RVA: 0x004E783C File Offset: 0x004E5A3C
		public virtual int? PickEmote(Player closestPlayer, List<int> emoteList, WorldUIAnchor otherAnchor)
		{
			return null;
		}

		// Token: 0x06002223 RID: 8739 RVA: 0x004E7852 File Offset: 0x004E5A52
		public sealed override void SetupContent()
		{
			NPCLoader.SetDefaults(this.NPC, false);
			this.AutoStaticDefaults();
			this.SetStaticDefaults();
			NPCID.Search.Add(base.FullName, this.Type);
			LocalizedText deathMessage = this.DeathMessage;
		}

		/// <summary>
		/// Allows you to set all your NPC's properties, such as width, damage, aiStyle, lifeMax, etc.
		/// </summary>
		// Token: 0x06002224 RID: 8740 RVA: 0x004E7889 File Offset: 0x004E5A89
		public virtual void SetDefaults()
		{
		}

		/// <summary>
		/// Gets called when your NPC spawns in world
		/// <para /> Called in single player or on the server only.
		/// </summary>
		// Token: 0x06002225 RID: 8741 RVA: 0x004E788B File Offset: 0x004E5A8B
		public virtual void OnSpawn(IEntitySource source)
		{
		}

		/// <summary>
		/// Automatically sets certain static defaults. Override this if you do not want the properties to be set for you.
		/// </summary>
		// Token: 0x06002226 RID: 8742 RVA: 0x004E7890 File Offset: 0x004E5A90
		public virtual void AutoStaticDefaults()
		{
			TextureAssets.Npc[this.NPC.type] = ModContent.Request<Texture2D>(this.Texture, 2);
			if (this.Banner != 0 && this.BannerItem != 0)
			{
				NPCLoader.bannerToItem[this.Banner] = this.BannerItem;
				NPCLoader.itemToBanner[this.BannerItem] = this.Banner;
			}
			if (this.NPC.lifeMax > 32767 || this.NPC.boss)
			{
				Main.npcLifeBytes[this.NPC.type] = 4;
				return;
			}
			if (this.NPC.lifeMax > 127)
			{
				Main.npcLifeBytes[this.NPC.type] = 2;
				return;
			}
			Main.npcLifeBytes[this.NPC.type] = 1;
		}

		/// <summary>
		/// Allows you to customize this NPC's stats when the difficulty is expert or higher.<br />
		/// This runs after <see cref="F:Terraria.NPC.value" />,  <see cref="F:Terraria.NPC.lifeMax" />,  <see cref="F:Terraria.NPC.damage" />,  <see cref="F:Terraria.NPC.knockBackResist" /> have been adjusted for the current difficulty, (expert/master/FTW)<br />
		/// It is common to multiply lifeMax by the balance factor, and sometimes adjust knockbackResist.<br />
		/// <br />
		/// Eg:<br />
		/// <code>lifeMax = (int)(lifeMax * balance * bossAdjustment)</code>
		/// </summary>
		/// <param name="numPlayers">The number of active players</param>
		/// <param name="balance">Scaling factor that increases by a fraction for each player</param>
		/// <param name="bossAdjustment">An extra reduction factor to be applied to boss life in high difficulty modes</param>
		// Token: 0x06002227 RID: 8743 RVA: 0x004E7968 File Offset: 0x004E5B68
		public virtual void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
		{
		}

		/// <summary>
		/// Allows you to set an NPC's information in the Bestiary.
		/// </summary>
		/// <param name="database"></param>
		/// <param name="bestiaryEntry"></param>
		// Token: 0x06002228 RID: 8744 RVA: 0x004E796A File Offset: 0x004E5B6A
		public virtual void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
		}

		/// <summary>
		/// Allows you to modify the type name of this NPC dynamically.
		/// </summary>
		// Token: 0x06002229 RID: 8745 RVA: 0x004E796C File Offset: 0x004E5B6C
		public virtual void ModifyTypeName(ref string typeName)
		{
		}

		/// <summary>
		/// Allows you to modify the bounding box for hovering over this NPC (affects things like whether or not its name is displayed).
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="boundingBox">The bounding box used for determining whether or not the NPC counts as being hovered over.</param>
		// Token: 0x0600222A RID: 8746 RVA: 0x004E796E File Offset: 0x004E5B6E
		public virtual void ModifyHoverBoundingBox(ref Rectangle boundingBox)
		{
		}

		/// <summary>
		/// Allows you to give a list of names this NPC can be given on spawn.<br></br>
		/// By default, returns a blank list, which means the NPC will simply use its type name as its given name when prompted.
		/// </summary>
		/// <returns></returns>
		// Token: 0x0600222B RID: 8747 RVA: 0x004E7970 File Offset: 0x004E5B70
		public virtual List<string> SetNPCNameList()
		{
			return new List<string>();
		}

		/// <summary>
		/// Allows you to set the town NPC profile that this NPC uses.<br></br>
		/// By default, returns null, meaning that the NPC doesn't use one.
		/// </summary>
		// Token: 0x0600222C RID: 8748 RVA: 0x004E7977 File Offset: 0x004E5B77
		public virtual ITownNPCProfile TownNPCProfile()
		{
			return null;
		}

		/// <summary>
		/// This is where you reset any fields you add to your subclass to their default states. This is necessary in order to reset your fields if they are conditionally set by a tick update but the condition is no longer satisfied. (Note: This hook is only really useful for GlobalNPC, but is included in ModNPC for completion.)
		/// <para /> Called on the server and clients.
		/// </summary>
		// Token: 0x0600222D RID: 8749 RVA: 0x004E797A File Offset: 0x004E5B7A
		public virtual void ResetEffects()
		{
		}

		/// <summary>
		/// Allows you to determine how this NPC behaves. Return false to stop the vanilla AI and the AI hook from being run. Returns true by default.
		/// <para /> Called on the server and clients.
		/// <AIMethodOrder>
		/// 		<para /> The order of the AI related methods is: <code>
		/// if (PreAI()) {
		/// 	VanillaAI()
		/// 	AI()
		/// }
		/// PostAI()</code>
		/// 	</AIMethodOrder>
		/// </summary>
		/// <returns />
		// Token: 0x0600222E RID: 8750 RVA: 0x004E797C File Offset: 0x004E5B7C
		public virtual bool PreAI()
		{
			return true;
		}

		/// <summary>
		/// Allows you to determine how this NPC behaves. This will only be called if PreAI returns true.
		/// <para /> Called on the server and clients.
		/// <AIMethodOrder>
		/// 		<para /> The order of the AI related methods is: <code>
		/// if (PreAI()) {
		/// 	VanillaAI()
		/// 	AI()
		/// }
		/// PostAI()</code>
		/// 	</AIMethodOrder>
		/// </summary>
		// Token: 0x0600222F RID: 8751 RVA: 0x004E797F File Offset: 0x004E5B7F
		public virtual void AI()
		{
		}

		/// <summary>
		/// Allows you to determine how any NPC behaves. This will be called regardless of what PreAI returns.
		/// <para /> Called on the server and remote clients.
		/// <AIMethodOrder>
		/// 		<para /> The order of the AI related methods is: <code>
		/// if (PreAI()) {
		/// 	VanillaAI()
		/// 	AI()
		/// }
		/// PostAI()</code>
		/// 	</AIMethodOrder>
		/// </summary>
		// Token: 0x06002230 RID: 8752 RVA: 0x004E7981 File Offset: 0x004E5B81
		public virtual void PostAI()
		{
		}

		/// <summary>
		/// If you are storing AI information outside of the NPC.ai array, use this to send that AI information between clients and servers, which will be handled in <see cref="M:Terraria.ModLoader.ModNPC.ReceiveExtraAI(System.IO.BinaryReader)" />.
		/// <para /> Called whenever <see cref="F:Terraria.ID.MessageID.SyncNPC" /> is successfully sent, for example on NPC creation, on player join, or whenever NPC.netUpdate is set to true in the update loop for that tick.
		/// <para /> Only called on the server.
		/// </summary>
		/// <param name="writer">The writer.</param>
		// Token: 0x06002231 RID: 8753 RVA: 0x004E7983 File Offset: 0x004E5B83
		public virtual void SendExtraAI(BinaryWriter writer)
		{
		}

		/// <summary>
		/// Use this to receive information that was sent in <see cref="M:Terraria.ModLoader.ModNPC.SendExtraAI(System.IO.BinaryWriter)" />.
		/// <para /> Called whenever <see cref="F:Terraria.ID.MessageID.SyncNPC" /> is successfully received.
		/// <para /> Only called on the client.
		/// </summary>
		/// <param name="reader">The reader.</param>
		// Token: 0x06002232 RID: 8754 RVA: 0x004E7985 File Offset: 0x004E5B85
		public virtual void ReceiveExtraAI(BinaryReader reader)
		{
		}

		/// <summary>
		/// Allows you to modify the frame from this NPC's texture that is drawn, which is necessary in order to animate NPCs.
		/// <para /> Called on the server and clients.
		/// </summary>
		/// <param name="frameHeight"></param>
		// Token: 0x06002233 RID: 8755 RVA: 0x004E7987 File Offset: 0x004E5B87
		public virtual void FindFrame(int frameHeight)
		{
		}

		/// <summary>
		/// Allows you to make things happen whenever this NPC is hit, such as creating dust or gores.
		/// <para /> Called on local, server, and remote clients.
		/// <para /> Usually when something happens when an NPC dies such as item spawning, you use NPCLoot, but you can use HitEffect paired with a check for <c>if (NPC.life &lt;= 0)</c> to do client-side death effects, such as spawning dust, gore, or death sounds.
		/// </summary>
		// Token: 0x06002234 RID: 8756 RVA: 0x004E7989 File Offset: 0x004E5B89
		public virtual void HitEffect(NPC.HitInfo hit)
		{
		}

		/// <summary>
		/// Allows you to make the NPC either regenerate health or take damage over time by setting <see cref="F:Terraria.NPC.lifeRegen" />. This is useful for implementing damage over time debuffs such as <see cref="F:Terraria.ID.BuffID.Poisoned" /> or <see cref="F:Terraria.ID.BuffID.OnFire" />. Regeneration or damage will occur at a rate of half of <see cref="F:Terraria.NPC.lifeRegen" /> per second.
		/// <para />Essentially, modders implementing damage over time debuffs should subtract from <see cref="F:Terraria.NPC.lifeRegen" /> a number that is twice as large as the intended damage per second. See <see href="https://github.com/tModLoader/tModLoader/blob/stable/ExampleMod/Common/GlobalNPCs/DamageOverTimeGlobalNPC.cs#L16">DamageOverTimeGlobalNPC.cs</see> for an example of this.
		/// <para />The damage parameter is the number that appears above the NPC's head if it takes damage over time.
		/// <para />Multiple debuffs work together by following some conventions: <see cref="F:Terraria.NPC.lifeRegen" /> should not be assigned a number, rather it should be subtracted from. <paramref name="damage" /> should only be assigned if the intended popup text is larger then its current value.
		/// <para /> Called on the server and clients.
		/// </summary>
		/// <param name="damage"></param>
		// Token: 0x06002235 RID: 8757 RVA: 0x004E798B File Offset: 0x004E5B8B
		public virtual void UpdateLifeRegen(ref int damage)
		{
		}

		/// <summary>
		/// Whether or not to run the code for checking whether this NPC will remain active. Return false to stop this NPC from being despawned and to stop this NPC from counting towards the limit for how many NPCs can exist near a player. Returns true by default.
		/// <para /> See also <see cref="F:Terraria.ID.NPCID.Sets.DoesntDespawnToInactivityAndCountsNPCSlots" /> for an option that still counts towards NPC spawn limits.
		/// <para /> Called on the server and clients.
		/// </summary>
		/// <returns></returns>
		// Token: 0x06002236 RID: 8758 RVA: 0x004E798D File Offset: 0x004E5B8D
		public virtual bool CheckActive()
		{
			return true;
		}

		/// <summary>
		/// Whether or not this NPC should be killed when it reaches 0 health. You may program extra effects in this hook (for example, how Golem's head lifts up for the second phase of its fight). Return false to stop this NPC from being killed. Returns true by default.
		/// <para /> Called on the server and clients.
		/// </summary>
		/// <returns></returns>
		// Token: 0x06002237 RID: 8759 RVA: 0x004E7990 File Offset: 0x004E5B90
		public virtual bool CheckDead()
		{
			return true;
		}

		/// <summary>
		/// Allows you to call OnKill on your own when the NPC dies, rather then letting vanilla call it on its own. Returns false by default.
		/// <para /> Called on the server and clients.
		/// </summary>
		/// <returns>Return true to stop vanilla from calling OnKill on its own. Do this if you call OnKill yourself.</returns>
		// Token: 0x06002238 RID: 8760 RVA: 0x004E7993 File Offset: 0x004E5B93
		public virtual bool SpecialOnKill()
		{
			return false;
		}

		/// <summary>
		/// Allows you to determine whether or not this NPC will do anything upon death (besides dying). This method can also be used to dynamically prevent specific item loot using <see cref="F:Terraria.ModLoader.NPCLoader.blockLoot" />, but editing the drop rules themselves is usually the better approach.
		/// <para /> Returning false will skip dropping loot, the <see cref="M:Terraria.ModLoader.NPCLoader.OnKill(Terraria.NPC)" /> methods, and logic setting boss flags (<see cref="M:Terraria.NPC.DoDeathEvents(Terraria.Player)" />).
		/// <para /> Returns true by default.
		/// <para /> Called in single player or on the server only.
		/// </summary>
		/// <returns></returns>
		// Token: 0x06002239 RID: 8761 RVA: 0x004E7996 File Offset: 0x004E5B96
		public virtual bool PreKill()
		{
			return true;
		}

		/// <summary>
		/// Allows you to make things happen when this NPC dies (for example, dropping items manually and setting ModSystem fields).
		/// <para /> Called in single player or on the server only.
		/// <para /> Most item drops should be done via drop rules registered in <see cref="M:Terraria.ModLoader.ModNPC.ModifyNPCLoot(Terraria.ModLoader.NPCLoot)" />. Some dynamic NPC drops, such as additional hearts, are more suited for OnKill instead. <see href="https://github.com/tModLoader/tModLoader/blob/stable/ExampleMod/Content/NPCs/MinionBoss/MinionBossMinion.cs#L101">MinionBossMinion.cs</see> shows an example of an NPC that drops additional hearts. See <see cref="F:Terraria.NPC.lastInteraction" /> and <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-NPC-Drops-and-Loot-1.4#player-who-killed-npc">Player who killed NPC wiki section</see> as well for determining which players attacked or killed this NPC.
		/// <para /> Bosses need to set flags when they are defeated, and some bosses run world generation code such as spawning new ore. <see href="https://github.com/tModLoader/tModLoader/blob/stable/ExampleMod/Content/NPCs/MinionBoss/MinionBossBody.cs#L218">MinionBossMinion.cs</see> shows an example of these effects.
		/// </summary>
		// Token: 0x0600223A RID: 8762 RVA: 0x004E7999 File Offset: 0x004E5B99
		public virtual void OnKill()
		{
		}

		/// <summary>
		/// Allows you to determine how and when this NPC can fall through platforms and similar tiles.
		/// <para /> Return true to allow this NPC to fall through platforms, false to prevent it. Returns null by default, applying vanilla behaviors (based on aiStyle and type).
		/// <para /> Called on the server and clients.
		/// </summary>
		// Token: 0x0600223B RID: 8763 RVA: 0x004E799C File Offset: 0x004E5B9C
		public virtual bool? CanFallThroughPlatforms()
		{
			return null;
		}

		/// <summary>
		/// Allows you to determine whether the given item can catch this NPC. Return true or false to say this NPC can or cannot be caught, respectively, regardless of vanilla rules.
		/// <para /> Returns null by default, which allows vanilla's NPC catching rules to decide the target's fate.
		/// <para /> If this returns false, <see cref="M:Terraria.ModLoader.CombinedHooks.OnCatchNPC(Terraria.Player,Terraria.NPC,Terraria.Item,System.Boolean)" /> is never called.
		/// <para /> NOTE: this does not classify the given item as an NPC-catching tool, which is necessary for catching NPCs in the first place. To do that, you will need to use the "CatchingTool" set in ItemID.Sets.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="item">The item with which the player is trying to catch this NPC.</param>
		/// <param name="player">The player attempting to catch this NPC.</param>
		/// <returns></returns>
		// Token: 0x0600223C RID: 8764 RVA: 0x004E79B4 File Offset: 0x004E5BB4
		public virtual bool? CanBeCaughtBy(Item item, Player player)
		{
			return null;
		}

		/// <summary>
		/// Allows you to make things happen when the given item attempts to catch this NPC.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="player">The player attempting to catch this NPC.</param>
		/// <param name="item">The item used to catch this NPC.</param>
		/// <param name="failed">Whether or not this NPC has been successfully caught.</param>
		// Token: 0x0600223D RID: 8765 RVA: 0x004E79CA File Offset: 0x004E5BCA
		public virtual void OnCaughtBy(Player player, Item item, bool failed)
		{
		}

		/// <summary>
		/// Allows you to add and modify NPC loot tables to drop on death and to appear in the Bestiary.
		/// <para /> The <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-NPC-Drops-and-Loot-1.4">Basic NPC Drops and Loot 1.4 Guide</see> explains how to use this hook to modify NPC loot.
		/// <para /> This hook only runs once during mod loading, any dynamic behavior must be contained in the rules themselves.
		/// </summary>
		/// <param name="npcLoot">A reference to the item drop database for this npc type</param>
		// Token: 0x0600223E RID: 8766 RVA: 0x004E79CC File Offset: 0x004E5BCC
		public virtual void ModifyNPCLoot(NPCLoot npcLoot)
		{
		}

		// Token: 0x0600223F RID: 8767 RVA: 0x004E79CE File Offset: 0x004E5BCE
		[Obsolete("Use BossLoot(ref int potionType) instead. The name can be changed by modifying DeathMessage")]
		public virtual void BossLoot(ref string name, ref int potionType)
		{
		}

		/// <summary>
		/// Allows you to customize what happens when this boss dies, as well as letting you modify the type of potion it drops.
		/// <para /> Called in single player or on the server only.
		/// </summary>
		// Token: 0x06002240 RID: 8768 RVA: 0x004E79D0 File Offset: 0x004E5BD0
		public virtual void BossLoot(ref int potionType)
		{
		}

		/// <summary>
		/// Allows you to determine whether this NPC can hit the given player. Return false to block this NPC from hitting the target. Returns true by default. CooldownSlot determines which of the player's cooldown counters (<see cref="T:Terraria.ID.ImmunityCooldownID" />) to use, and defaults to -1 (<see cref="F:Terraria.ID.ImmunityCooldownID.General" />).
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="target"></param>
		/// <param name="cooldownSlot"></param>
		/// <returns></returns>
		// Token: 0x06002241 RID: 8769 RVA: 0x004E79D2 File Offset: 0x004E5BD2
		public virtual bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return true;
		}

		/// <summary>
		/// Allows you to modify the damage, etc., that this NPC does to a player.
		/// <para /> This hook should be used ONLY to modify properties of the HitModifiers. Any extra side effects should occur in OnHit hooks instead.
		/// <para /> The final hit values such as the final damage can only be retrieved from the HurtInfo in the <see cref="M:Terraria.ModLoader.ModNPC.OnHitPlayer(Terraria.Player,Terraria.Player.HurtInfo)" /> hook.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="target"></param>
		/// <param name="modifiers"></param>
		// Token: 0x06002242 RID: 8770 RVA: 0x004E79D5 File Offset: 0x004E5BD5
		public virtual void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
		{
		}

		/// <summary>
		/// Allows you to create special effects when this NPC hits a player (for example, inflicting debuffs).
		/// <para /> Changes to the hit such as the damage must be done by modifying the HurtModifiers properties in the <see cref="M:Terraria.ModLoader.ModNPC.ModifyHitPlayer(Terraria.Player,Terraria.Player.HurtModifiers@)" /> hook.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="target"></param>
		/// <param name="hurtInfo"></param>
		// Token: 0x06002243 RID: 8771 RVA: 0x004E79D7 File Offset: 0x004E5BD7
		public virtual void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
		}

		/// <summary>
		/// Allows you to determine whether this NPC can hit the given friendly NPC. Return false to block the NPC from hitting the target, and return true to use the vanilla code for whether the target can be hit. Returns true by default.
		/// <para /> Called in single player or on the server only.
		/// </summary>
		/// <param name="target"></param>
		/// <returns></returns>
		// Token: 0x06002244 RID: 8772 RVA: 0x004E79D9 File Offset: 0x004E5BD9
		public virtual bool CanHitNPC(NPC target)
		{
			return true;
		}

		/// <summary>
		/// Allows you to determine whether a friendly NPC can be hit by an NPC. Return false to block the attacker from hitting the NPC, and return true to use the vanilla code for whether the target can be hit. Returns true by default.
		/// <para /> Called in single player or on the server only.
		/// </summary>
		/// <param name="attacker"></param>
		/// <returns></returns>
		// Token: 0x06002245 RID: 8773 RVA: 0x004E79DC File Offset: 0x004E5BDC
		public virtual bool CanBeHitByNPC(NPC attacker)
		{
			return true;
		}

		/// <summary>
		/// Allows you to modify the damage, knockback, etc., that this NPC does to a friendly NPC.
		/// <para /> This hook should be used ONLY to modify properties of the HitModifiers. Any extra side effects should occur in OnHit hooks instead.
		/// <para /> The final hit values such as the final damage can only be retrieved from the HitInfo in the <see cref="M:Terraria.ModLoader.ModNPC.OnHitNPC(Terraria.NPC,Terraria.NPC.HitInfo)" /> hook.
		/// <para /> Called in single player or on the server only.
		/// </summary>
		/// <param name="target"></param>
		/// <param name="modifiers"></param>
		// Token: 0x06002246 RID: 8774 RVA: 0x004E79DF File Offset: 0x004E5BDF
		public virtual void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
		}

		/// <summary>
		/// Allows you to create special effects when this NPC hits a friendly NPC.
		/// <para /> Changes to the hit such as the damage must be done by modifying the HitModifiers properties in the <see cref="M:Terraria.ModLoader.ModNPC.ModifyHitNPC(Terraria.NPC,Terraria.NPC.HitModifiers@)" /> hook.
		/// <para /> Called in single player or on the server only.
		/// </summary>
		/// <param name="target"></param>
		/// <param name="hit"></param>
		// Token: 0x06002247 RID: 8775 RVA: 0x004E79E1 File Offset: 0x004E5BE1
		public virtual void OnHitNPC(NPC target, NPC.HitInfo hit)
		{
		}

		/// <summary>
		/// Allows you to determine whether this NPC can be hit by the given melee weapon when swung. Return true to allow hitting the NPC, return false to block hitting the NPC, and return null to use the vanilla code for whether the NPC can be hit. Returns null by default.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="player"></param>
		/// <param name="item"></param>
		/// <returns></returns>
		// Token: 0x06002248 RID: 8776 RVA: 0x004E79E4 File Offset: 0x004E5BE4
		public virtual bool? CanBeHitByItem(Player player, Item item)
		{
			return null;
		}

		/// <summary>
		/// Allows you to determine whether an NPC can be collided with the player melee weapon when swung.
		/// <para /> Use <see cref="M:Terraria.ModLoader.ModNPC.CanBeHitByItem(Terraria.Player,Terraria.Item)" /> instead for Guide Voodoo Doll-type effects.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="player">The player wielding this item.</param>
		/// <param name="item">The weapon item the player is holding.</param>
		/// <param name="meleeAttackHitbox">Hitbox of melee attack.</param>
		/// <returns>
		/// Return true to allow colliding with the melee attack, return false to block the weapon from colliding with the NPC, and return null to use the vanilla code for whether the attack can be colliding. Returns null by default.
		/// </returns>
		// Token: 0x06002249 RID: 8777 RVA: 0x004E79FC File Offset: 0x004E5BFC
		public virtual bool? CanCollideWithPlayerMeleeAttack(Player player, Item item, Rectangle meleeAttackHitbox)
		{
			return null;
		}

		/// <summary>
		/// Allows you to modify the damage, knockback, etc., that this NPC takes from a melee weapon.
		/// <para /> This hook should be used ONLY to modify properties of the HitModifiers. Any extra side effects should occur in OnHit hooks instead.
		/// <para /> The final hit values such as the final damage can only be retrieved from the HitInfo in the <see cref="M:Terraria.ModLoader.ModNPC.OnHitByItem(Terraria.Player,Terraria.Item,Terraria.NPC.HitInfo,System.Int32)" /> hook.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="player"></param>
		/// <param name="item"></param>
		/// <param name="modifiers"></param>
		// Token: 0x0600224A RID: 8778 RVA: 0x004E7A12 File Offset: 0x004E5C12
		public virtual void ModifyHitByItem(Player player, Item item, ref NPC.HitModifiers modifiers)
		{
		}

		/// <summary>
		/// Allows you to create special effects when this NPC is hit by a melee weapon.
		/// <para /> Changes to the hit such as the damage must be done by modifying the HitModifiers properties in the <see cref="M:Terraria.ModLoader.ModNPC.ModifyHitByItem(Terraria.Player,Terraria.Item,Terraria.NPC.HitModifiers@)" /> hook.
		/// <para /> Called on the client doing the damage.
		/// </summary>
		/// <param name="player"></param>
		/// <param name="item"></param>
		/// <param name="hit"></param>
		/// <param name="damageDone"></param>
		// Token: 0x0600224B RID: 8779 RVA: 0x004E7A14 File Offset: 0x004E5C14
		public virtual void OnHitByItem(Player player, Item item, NPC.HitInfo hit, int damageDone)
		{
		}

		/// <summary>
		/// Allows you to determine whether this NPC can be hit by the given projectile. Return true to allow hitting the NPC, return false to block hitting the NPC, and return null to use the vanilla code for whether the NPC can be hit. Returns null by default.
		/// <para /> Can be called on the local client or server, depending on who owns the projectile.
		/// </summary>
		/// <param name="projectile"></param>
		/// <returns></returns>
		// Token: 0x0600224C RID: 8780 RVA: 0x004E7A18 File Offset: 0x004E5C18
		public virtual bool? CanBeHitByProjectile(Projectile projectile)
		{
			return null;
		}

		/// <summary>
		/// Allows you to modify the damage, knockback, etc., that this NPC takes from a projectile.
		/// <para /> This hook should be used ONLY to modify properties of the HitModifiers. Any extra side effects should occur in OnHit hooks instead.
		/// <para /> The final hit values such as the final damage can only be retrieved from the HitInfo in the <see cref="M:Terraria.ModLoader.ModNPC.OnHitByProjectile(Terraria.Projectile,Terraria.NPC.HitInfo,System.Int32)" /> hook.
		/// <para /> Can be called on the local client or server, depending on who owns the projectile.
		/// </summary>
		/// <param name="projectile"></param>
		/// <param name="modifiers"></param>
		// Token: 0x0600224D RID: 8781 RVA: 0x004E7A2E File Offset: 0x004E5C2E
		public virtual void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
		{
		}

		/// <summary>
		/// Allows you to create special effects when this NPC is hit by a projectile.
		/// <para /> Changes to the hit such as the damage must be done by modifying the HitModifiers properties in the <see cref="M:Terraria.ModLoader.ModNPC.ModifyHitByProjectile(Terraria.Projectile,Terraria.NPC.HitModifiers@)" /> hook.
		/// <para /> Can be called on the local client or server, depending on who owns the projectile.
		/// </summary>
		/// <param name="projectile"></param>
		/// <param name="hit"></param>
		/// <param name="damageDone"></param>
		// Token: 0x0600224E RID: 8782 RVA: 0x004E7A30 File Offset: 0x004E5C30
		public virtual void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
		{
		}

		/// <summary>
		/// Allows you to use a custom damage formula for when this NPC takes damage from any source. For example, you can change the way defense works or use a different crit multiplier.
		/// This hook should be used ONLY to modify properties of the HitModifiers. Any extra side effects should occur in OnHit hooks instead.
		/// <para /> Can be called on the local client or server, depending on who is dealing damage.
		/// </summary>
		/// <param name="modifiers"></param>
		// Token: 0x0600224F RID: 8783 RVA: 0x004E7A32 File Offset: 0x004E5C32
		public virtual void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
		{
		}

		/// <summary>
		/// Allows you to customize the boss head texture used by an NPC based on its state. Set index to -1 to stop the texture from being displayed.
		/// <para /> Called on all clients.
		/// </summary>
		/// <param name="index">The index for NPCID.Sets.BossHeadTextures</param>
		// Token: 0x06002250 RID: 8784 RVA: 0x004E7A34 File Offset: 0x004E5C34
		public virtual void BossHeadSlot(ref int index)
		{
		}

		/// <summary>
		/// Allows you to customize the rotation of this NPC's boss head icon on the map.
		/// <para /> Called on all clients.
		/// </summary>
		/// <param name="rotation"></param>
		// Token: 0x06002251 RID: 8785 RVA: 0x004E7A36 File Offset: 0x004E5C36
		public virtual void BossHeadRotation(ref float rotation)
		{
		}

		/// <summary>
		/// Allows you to flip this NPC's boss head icon on the map.
		/// <para /> Called on all clients.
		/// </summary>
		/// <param name="spriteEffects"></param>
		// Token: 0x06002252 RID: 8786 RVA: 0x004E7A38 File Offset: 0x004E5C38
		public virtual void BossHeadSpriteEffects(ref SpriteEffects spriteEffects)
		{
		}

		/// <summary>
		/// Allows you to determine the color and transparency in which this NPC is drawn. Return null to use the default color (normally light and buff color). Returns null by default.
		/// <para /> Called on all clients.
		/// </summary>
		/// <param name="drawColor"></param>
		/// <returns></returns>
		// Token: 0x06002253 RID: 8787 RVA: 0x004E7A3C File Offset: 0x004E5C3C
		public virtual Color? GetAlpha(Color drawColor)
		{
			return null;
		}

		/// <summary>
		/// Allows you to add special visual effects to this NPC (such as creating dust), and modify the color in which the NPC is drawn.
		/// <para /> Called on all clients.
		/// </summary>
		/// <param name="drawColor"></param>
		// Token: 0x06002254 RID: 8788 RVA: 0x004E7A52 File Offset: 0x004E5C52
		public virtual void DrawEffects(ref Color drawColor)
		{
		}

		/// <summary>
		/// Allows you to draw things behind this NPC, or to modify the way this NPC is drawn. Substract screenPos from the draw position before drawing. Return false to stop the game from drawing the NPC (useful if you're manually drawing the NPC). Returns true by default.
		/// <para /> Called on all clients.
		/// </summary>
		/// <param name="spriteBatch">The spritebatch to draw on</param>
		/// <param name="screenPos">The screen position used to translate world position into screen position</param>
		/// <param name="drawColor">The color the NPC is drawn in</param>
		/// <returns></returns>
		// Token: 0x06002255 RID: 8789 RVA: 0x004E7A54 File Offset: 0x004E5C54
		public virtual bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			return true;
		}

		/// <summary>
		/// Allows you to draw things in front of this NPC. Substract screenPos from the draw position before drawing. This method is called even if PreDraw returns false.
		/// <para /> Called on all clients.
		/// </summary>
		/// <param name="spriteBatch">The spritebatch to draw on</param>
		/// <param name="screenPos">The screen position used to translate world position into screen position</param>
		/// <param name="drawColor">The color the NPC is drawn in</param>
		// Token: 0x06002256 RID: 8790 RVA: 0x004E7A57 File Offset: 0x004E5C57
		public virtual void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
		}

		/// <summary>
		/// When used in conjunction with "NPC.hide = true", allows you to specify that this NPC should be drawn behind certain elements. Add the index to one of Main.DrawCacheNPCsMoonMoon, DrawCacheNPCsOverPlayers, DrawCacheNPCProjectiles, or DrawCacheNPCsBehindNonSolidTiles.
		/// <para /> Called on all clients.
		/// </summary>
		/// <param name="index"></param>
		// Token: 0x06002257 RID: 8791 RVA: 0x004E7A59 File Offset: 0x004E5C59
		public virtual void DrawBehind(int index)
		{
		}

		/// <summary>
		/// Allows you to control how the health bar for this NPC is drawn. The hbPosition parameter is the same as Main.hbPosition; it determines whether the health bar gets drawn above or below the NPC by default. The scale parameter is the health bar's size. By default, it will be the normal 1f; most bosses set this to 1.5f. Return null to let the normal vanilla health-bar-drawing code to run. Return false to stop the health bar from being drawn. Return true to draw the health bar in the position specified by the position parameter (note that this is the world position, not screen position).
		/// <para /> Called on all clients.
		/// </summary>
		/// <param name="hbPosition"></param>
		/// <param name="scale"></param>
		/// <param name="position"></param>
		/// <returns></returns>
		// Token: 0x06002258 RID: 8792 RVA: 0x004E7A5C File Offset: 0x004E5C5C
		public virtual bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			return null;
		}

		/// <summary>
		/// Whether or not this NPC can spawn with the given spawning conditions. Return the weight for the chance of this NPC to spawn compared to vanilla mobs. All vanilla mobs combined have a total weight of 1. Returns 0 by default, which disables natural spawning. Remember to always use spawnInfo.player and not Main.LocalPlayer when checking Player or ModPlayer fields, otherwise your mod won't work in Multiplayer.
		/// <para /> Called in single player or on the server only.
		/// </summary>
		/// <param name="spawnInfo"></param>
		/// <returns></returns>
		// Token: 0x06002259 RID: 8793 RVA: 0x004E7A72 File Offset: 0x004E5C72
		public virtual float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return 0f;
		}

		/// <summary>
		/// Allows you to customize how this NPC is created when it naturally spawns (for example, its position or ai array). Return the return value of NPC.NewNPC. By default this method spawns this NPC on top of the tile at the given coordinates.
		/// <para /> Called in single player or on the server only.
		/// </summary>
		/// <param name="tileX"></param>
		/// <param name="tileY"></param>
		/// <returns></returns>
		// Token: 0x0600225A RID: 8794 RVA: 0x004E7A7C File Offset: 0x004E5C7C
		public virtual int SpawnNPC(int tileX, int tileY)
		{
			return NPC.NewNPC(null, tileX * 16 + 8, tileY * 16, this.NPC.type, 0, 0f, 0f, 0f, 0f, 255);
		}

		/// <summary>
		/// Whether or not the conditions have been met for this town NPC to be able to move into town. For example, the Demolitionist requires that any player has an explosive.
		/// <para /> Town NPC spawn conditions typically check if specific bosses have been defeated (<see cref="F:Terraria.NPC.downedGolemBoss" />), the npc has been "saved" somewhere in the world, or any player has specific items in their inventory. To check for inventory items, iterate over <see cref="P:Terraria.Main.ActivePlayers" /> and check <see cref="M:Terraria.Player.HasItem(System.Int32)" /> or <see cref="M:Terraria.Player.CountItem(System.Int32,System.Int32)" />, returning true if any player satisfies the requirement.
		/// <para /> To support allowing town NPC to respawn without needing to meet the original respawn requirements, a feature added in Terraria v1.4.4, store a bool in a <see cref="T:Terraria.ModLoader.ModSystem" /> and check it. <see href="https://github.com/tModLoader/tModLoader/blob/stable/ExampleMod/Content/NPCs/ExamplePerson.cs#L184">ExamplePerson.CanTownNPCSpawn</see> and <see href="https://github.com/tModLoader/tModLoader/blob/stable/ExampleMod/Common/Systems/TownNPCRespawnSystem.cs">TownNPCRespawnSystem.cs</see> show an example of this.
		/// <para /> Returns false by default, preventing the town NPC from spawning.
		/// <para /> Called in single player or on the server only.
		/// </summary>
		/// <param name="numTownNPCs"></param>
		/// <returns></returns>
		// Token: 0x0600225B RID: 8795 RVA: 0x004E7ABE File Offset: 0x004E5CBE
		public virtual bool CanTownNPCSpawn(int numTownNPCs)
		{
			return false;
		}

		/// <summary>
		/// Allows you to define special conditions required for this town NPC's house. For example, Truffle requires the house to be in an aboveground mushroom biome.
		/// <para /> The <paramref name="left" />, <paramref name="right" />, <paramref name="top" />, and <paramref name="bottom" /> parameters define the bounds of the room being checked.
		/// <para /> Methods like <see cref="M:Terraria.WorldGen.Housing_GetTestedRoomBounds(System.Int32@,System.Int32@,System.Int32@,System.Int32@)" /> and <see cref="M:Terraria.WorldGen.CountTileTypesInArea(System.Int32[],System.Int32,System.Int32,System.Int32,System.Int32)" /> can facilitate implementing specific checks.
		/// <para /> Return false to prevent the npc from spawning due to failed condition checks.
		/// <para /> Called on the server and clients.
		/// </summary>
		// Token: 0x0600225C RID: 8796 RVA: 0x004E7AC1 File Offset: 0x004E5CC1
		public virtual bool CheckConditions(int left, int right, int top, int bottom)
		{
			return true;
		}

		/// <summary>
		/// Allows you to determine whether this town NPC wears a party hat during a party. Returns true by default.
		/// <para /> Called on all clients.
		/// </summary>
		/// <returns></returns>
		// Token: 0x0600225D RID: 8797 RVA: 0x004E7AC4 File Offset: 0x004E5CC4
		public virtual bool UsesPartyHat()
		{
			return true;
		}

		/// <summary>
		/// Allows you to determine whether this NPC can talk with the player. By default, returns if the NPC is a town NPC.
		/// <para /> This hook is not based on the type of the NPC, and is queried specifically on the ModNPC itself, regardless of if,
		/// for example, the type of the NPC instance is changed. Returning true in all circumstances will *always* make the NPC
		/// able to be chatted with no matter what else you do the NPC instance itself.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <returns></returns>
		// Token: 0x0600225E RID: 8798 RVA: 0x004E7AC7 File Offset: 0x004E5CC7
		public virtual bool CanChat()
		{
			return this.NPC.townNPC;
		}

		/// <summary>
		/// Allows you to give this NPC a chat message when a player talks to it. By default returns something embarrassing.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <returns></returns>
		// Token: 0x0600225F RID: 8799 RVA: 0x004E7AD4 File Offset: 0x004E5CD4
		public virtual string GetChat()
		{
			return Language.GetTextValue("tModLoader.DefaultTownNPCChat");
		}

		/// <summary>
		/// Allows you to set the text for the buttons that appear on this NPC's chat window. A parameter left as an empty string will not be included as a button on the chat window.
		/// <br /><br /> The value <c>Language.GetTextValue("LegacyInterface.28")</c> should be used for the "Shop" button.
		/// <br /><br /> Called on the local client only.
		/// </summary>
		/// <param name="button"></param>
		/// <param name="button2"></param>
		// Token: 0x06002260 RID: 8800 RVA: 0x004E7AE0 File Offset: 0x004E5CE0
		public virtual void SetChatButtons(ref string button, ref string button2)
		{
		}

		/// <summary>
		/// Allows you to make something happen whenever a button is clicked on this NPC's chat window. The firstButton parameter tells whether the first button or second button (button and button2 from SetChatButtons) was clicked. Set the shopName parameter to "Shop" to open this NPC's shop.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="firstButton"></param>
		/// <param name="shopName"></param>
		// Token: 0x06002261 RID: 8801 RVA: 0x004E7AE2 File Offset: 0x004E5CE2
		public virtual void OnChatButtonClicked(bool firstButton, ref string shopName)
		{
		}

		/// <summary>
		/// Allows you to add shops to this NPC, similar to adding recipes for items. <br />
		/// Make a new <see cref="T:Terraria.ModLoader.NPCShop" />, and items to it, and call <see cref="M:Terraria.ModLoader.AbstractNPCShop.Register" />
		/// </summary>
		// Token: 0x06002262 RID: 8802 RVA: 0x004E7AE4 File Offset: 0x004E5CE4
		public virtual void AddShops()
		{
		}

		/// <summary>
		/// Allows you to modify the contents of a shop whenever player opens it. <br />
		/// To create a shop, use <see cref="M:Terraria.ModLoader.ModNPC.AddShops" /> <br />
		/// Note that for special shops like travelling merchant, the <paramref name="shopName" /> may not correspond to a <see cref="T:Terraria.ModLoader.NPCShop" /> in the <see cref="T:Terraria.ModLoader.NPCShopDatabase" />
		/// <para /> Also note that unused slots in <paramref name="items" /> are null while <see cref="P:Terraria.Item.IsAir" /> entries are entries that have a reserved slot (<see cref="P:Terraria.ModLoader.NPCShop.Entry.SlotReserved" />) but did not have their conditions met. These should not be overwritten.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="shopName">The full name of the shop being opened. See <see cref="M:Terraria.ModLoader.NPCShopDatabase.GetShopName(System.Int32,System.String)" /> for the format. </param>
		/// <param name="items">Items in the shop including 'air' items in empty slots.</param>
		// Token: 0x06002263 RID: 8803 RVA: 0x004E7AE6 File Offset: 0x004E5CE6
		public virtual void ModifyActiveShop(string shopName, Item[] items)
		{
		}

		/// <summary>
		/// Whether this NPC can be teleported to a King or Queen statue. Returns false by default.
		/// <para /> Called in single player or on the server only.
		/// </summary>
		/// <param name="toKingStatue">Whether the NPC is being teleported to a King or Queen statue.</param>
		// Token: 0x06002264 RID: 8804 RVA: 0x004E7AE8 File Offset: 0x004E5CE8
		public virtual bool CanGoToStatue(bool toKingStatue)
		{
			return false;
		}

		/// <summary>
		/// Allows you to make things happen when this NPC teleports to a King or Queen statue.
		/// <para /> Called in single player or on the server only.
		/// </summary>
		/// <param name="toKingStatue">Whether the NPC was teleported to a King or Queen statue.</param>
		// Token: 0x06002265 RID: 8805 RVA: 0x004E7AEB File Offset: 0x004E5CEB
		public virtual void OnGoToStatue(bool toKingStatue)
		{
		}

		/// <summary>
		/// Allows you to modify the death message of a town NPC or boss (potentially derived from <see cref="P:Terraria.ModLoader.ModNPC.DeathMessage" />). This also affects what the dropped tombstone will say in the case of a town NPC. The text color can also be modified.
		/// <para /> When modifying the death message, use <see cref="M:Terraria.NPC.GetFullNetName" /> to retrieve the NPC name to use in substitutions.
		/// <para /> This is intended for more advanced use cases, you should be modifying only <see cref="P:Terraria.ModLoader.ModNPC.DeathMessage" /> for basic usages.
		/// <para /> Return false to skip the vanilla code for sending the message. This is useful if the death message is handled by this method or if the message should be skipped for any other reason, such as if there are multiple bosses. Returns true by default.
		/// </summary>
		// Token: 0x06002266 RID: 8806 RVA: 0x004E7AED File Offset: 0x004E5CED
		public virtual bool ModifyDeathMessage(ref NetworkText customText, ref Color color)
		{
			return true;
		}

		/// <summary>
		/// Allows you to determine the damage and knockback of this town NPC's attack before the damage is scaled. (More information on scaling in GlobalNPC.BuffTownNPCs.)
		/// <para /> Called on the server and clients.
		/// </summary>
		/// <param name="damage"></param>
		/// <param name="knockback"></param>
		// Token: 0x06002267 RID: 8807 RVA: 0x004E7AF0 File Offset: 0x004E5CF0
		public virtual void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
		}

		/// <summary>
		/// Allows you to determine the cooldown between each of this town NPC's attack. The cooldown will be a number greater than or equal to the first parameter, and less then the sum of the two parameters.
		/// <para /> Called on the server and clients.
		/// </summary>
		/// <param name="cooldown"></param>
		/// <param name="randExtraCooldown"></param>
		// Token: 0x06002268 RID: 8808 RVA: 0x004E7AF2 File Offset: 0x004E5CF2
		public virtual void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
		{
		}

		/// <summary>
		/// Allows you to determine the projectile type of this town NPC's attack, and how long it takes for the projectile to actually appear. This hook is only used when the town NPC has an attack type (<see cref="F:Terraria.ID.NPCID.Sets.AttackType" />) of 0 (throwing), 1 (shooting), or 2 (magic).
		/// <para /> Called on the server and clients.
		/// </summary>
		/// <param name="projType"></param>
		/// <param name="attackDelay"></param>
		// Token: 0x06002269 RID: 8809 RVA: 0x004E7AF4 File Offset: 0x004E5CF4
		public virtual void TownNPCAttackProj(ref int projType, ref int attackDelay)
		{
		}

		/// <summary>
		/// Allows you to determine the speed at which this town NPC throws a projectile when it attacks. Multiplier is the speed of the projectile, gravityCorrection is how much extra the projectile gets thrown upwards, and randomOffset allows you to randomize the projectile's velocity in a square centered around the original velocity. This hook is only used when the town NPC has an attack type (<see cref="F:Terraria.ID.NPCID.Sets.AttackType" />) of 0 (throwing), 1 (shooting), or 2 (magic).
		/// <para /> Called on the server and clients.
		/// </summary>
		/// <param name="multiplier"></param>
		/// <param name="gravityCorrection"></param>
		/// <param name="randomOffset"></param>
		// Token: 0x0600226A RID: 8810 RVA: 0x004E7AF6 File Offset: 0x004E5CF6
		public virtual void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
		{
		}

		/// <summary>
		/// Allows you to tell the game that this town NPC has already created a projectile and will still create more projectiles as part of a single attack so that the game can animate the NPC's attack properly. Only used when the town NPC has an attack type (<see cref="F:Terraria.ID.NPCID.Sets.AttackType" />) of 1 (shooting).
		/// <para /> Called on the server and clients.
		/// </summary>
		/// <param name="inBetweenShots"></param>
		// Token: 0x0600226B RID: 8811 RVA: 0x004E7AF8 File Offset: 0x004E5CF8
		public virtual void TownNPCAttackShoot(ref bool inBetweenShots)
		{
		}

		/// <summary>
		/// Allows you to control the brightness of the light emitted by this town NPC's aura when it performs a magic attack. Only used when the town NPC has an attack type (<see cref="F:Terraria.ID.NPCID.Sets.AttackType" />) of 2 (magic)
		/// <para /> Called on the server and clients.
		/// </summary>
		/// <param name="auraLightMultiplier"></param>
		// Token: 0x0600226C RID: 8812 RVA: 0x004E7AFA File Offset: 0x004E5CFA
		public virtual void TownNPCAttackMagic(ref float auraLightMultiplier)
		{
		}

		/// <summary>
		/// Allows you to determine the width and height of the item this town NPC swings when it attacks, which controls the range of this NPC's swung weapon. Only used when the town NPC has an attack type (<see cref="F:Terraria.ID.NPCID.Sets.AttackType" />) of 3 (swinging).
		/// <para /> Called on the server and clients.
		/// </summary>
		/// <param name="itemWidth"></param>
		/// <param name="itemHeight"></param>
		// Token: 0x0600226D RID: 8813 RVA: 0x004E7AFC File Offset: 0x004E5CFC
		public virtual void TownNPCAttackSwing(ref int itemWidth, ref int itemHeight)
		{
		}

		/// <summary>
		/// Allows you to customize how this town NPC's weapon is drawn when this NPC is shooting (this NPC must have an attack type (<see cref="F:Terraria.ID.NPCID.Sets.AttackType" />) of 1). <paramref name="scale" /> is a multiplier for the item's drawing size, <paramref name="item" /> is the Texture2D instance of the item to be drawn, <paramref name="itemFrame" /> is the section of the texture to draw, and <paramref name="horizontalHoldoutOffset" /> is how far away the item should be drawn from the NPC.<br />
		/// To use an actual item sprite, use <code>Main.GetItemDrawFrame(itemTypeHere, out item, out itemFrame);
		/// horizontalHoldoutOffset = (int)Main.DrawPlayerItemPos(1f, itemType).X - someOffsetHere</code>
		/// <para /> Called on all clients.
		/// </summary>
		/// <param name="item"></param>
		/// <param name="itemFrame"></param>
		/// <param name="scale"></param>
		/// <param name="horizontalHoldoutOffset"></param>
		// Token: 0x0600226E RID: 8814 RVA: 0x004E7AFE File Offset: 0x004E5CFE
		public virtual void DrawTownAttackGun(ref Texture2D item, ref Rectangle itemFrame, ref float scale, ref int horizontalHoldoutOffset)
		{
		}

		/// <summary>
		/// Allows you to customize how this town NPC's weapon is drawn when this NPC is swinging it (this NPC must have an attack type (<see cref="F:Terraria.ID.NPCID.Sets.AttackType" />) of 3). <paramref name="item" /> is the Texture2D instance of the item to be drawn, <paramref name="itemFrame" /> is the section of the texture to draw, <paramref name="itemSize" /> is the width and height of the item's hitbox (the same values for TownNPCAttackSwing), <paramref name="scale" /> is the multiplier for the item's drawing size, and <paramref name="offset" /> is the offset from which to draw the item from its normal position. The item texture can be any texture, but if it is an item texture you can use  <see cref="M:Terraria.Main.GetItemDrawFrame(System.Int32,Microsoft.Xna.Framework.Graphics.Texture2D@,Microsoft.Xna.Framework.Rectangle@)" /> to set <paramref name="item" /> and <paramref name="itemFrame" /> easily.
		/// <para /> Called on all clients.
		/// </summary>
		/// <param name="item"></param>
		/// <param name="itemFrame"></param>
		/// <param name="itemSize"></param>
		/// <param name="scale"></param>
		/// <param name="offset"></param>
		// Token: 0x0600226F RID: 8815 RVA: 0x004E7B00 File Offset: 0x004E5D00
		public virtual void DrawTownAttackSwing(ref Texture2D item, ref Rectangle itemFrame, ref int itemSize, ref float scale, ref Vector2 offset)
		{
		}

		/// <summary>
		/// Allows you to modify the NPC's <seealso cref="T:Terraria.ID.ImmunityCooldownID" />, damage multiplier, and hitbox. Useful for implementing dynamic damage hitboxes that change in dimensions or deal extra damage. Returns false to prevent vanilla code from running. Returns true by default.
		/// <para /> Called on the server and clients.
		/// </summary>
		/// <param name="victimHitbox"></param>
		/// <param name="immunityCooldownSlot"></param>
		/// <param name="damageMultiplier"></param>
		/// <param name="npcHitbox"></param>
		/// <returns></returns>
		// Token: 0x06002270 RID: 8816 RVA: 0x004E7B02 File Offset: 0x004E5D02
		public virtual bool ModifyCollisionData(Rectangle victimHitbox, ref int immunityCooldownSlot, ref MultipliableFloat damageMultiplier, ref Rectangle npcHitbox)
		{
			return true;
		}

		/// <summary>
		/// Makes this ModNPC save along the world even if it's not a townNPC. Defaults to false.
		/// <br /><b>NOTE:</b> A town NPC will always be saved.
		/// <br /><b>NOTE:</b> A NPC that needs saving will not despawn naturally.
		/// </summary>
		// Token: 0x06002271 RID: 8817 RVA: 0x004E7B05 File Offset: 0x004E5D05
		public virtual bool NeedSaving()
		{
			return false;
		}

		/// <summary>
		/// Allows you to save custom data for the given item.
		/// Allows you to save custom data for the given npc.
		/// <br />
		/// <br /><b>NOTE:</b> The provided tag is always empty by default, and is provided as an argument only for the sake of convenience and optimization.
		/// <br /><b>NOTE:</b> Try to only save data that isn't default values.
		/// <br /><b>NOTE:</b> The npc may be saved even if NeedSaving returns false and this is not a townNPC, if another mod returns true on NeedSaving.
		/// </summary>
		/// <param name="tag">The TagCompound to save data into. Note that this is always empty by default, and is provided as an argument</param>
		// Token: 0x06002272 RID: 8818 RVA: 0x004E7B08 File Offset: 0x004E5D08
		public virtual void SaveData(TagCompound tag)
		{
		}

		/// <summary>
		/// Allows you to load custom data that you have saved for this npc.
		/// </summary>
		/// <param name="tag">The tag.</param>
		// Token: 0x06002273 RID: 8819 RVA: 0x004E7B0A File Offset: 0x004E5D0A
		public virtual void LoadData(TagCompound tag)
		{
		}

		/// <summary>
		/// Allows you to change the location and sprite direction of the chat bubble that shows up while hovering over a Town NPC.
		/// <para /> Called on all clients.
		/// </summary>
		/// <param name="position">
		/// <br>The default position is:</br>
		/// <br>The X component is set to the NPC's Center - half the width of the chat bubble texture. Then +/- half of the NPC's width + 8 pixels for facing right/left respectively.</br>
		/// <br>Code: <c>npc.Center.X - screenPosition.X - (TextureAssets.Chat.Width() / 2f) +/- (npc.width / 2f + 8)</c></br>
		/// <br>The Y component is set to the top of the NPC - the height of the chat bubble texture. (Negative Y is up.)</br>
		/// <br>Code: <c>npc.position.Y - TextureAssets.Chat.Height() - (float)(int)screenPosition.Y</c></br>
		/// </param>
		/// <param name="spriteEffects">Allows you to change which way the chat bubble is flipped.</param>
		// Token: 0x06002274 RID: 8820 RVA: 0x004E7B0C File Offset: 0x004E5D0C
		public virtual void ChatBubblePosition(ref Vector2 position, ref SpriteEffects spriteEffects)
		{
		}

		/// <summary>
		/// <br>Allows you to fully control the location of the party and sprite direction of the party while an NPC is wearing it.</br>
		/// <br><seealso cref="F:Terraria.ID.NPCID.Sets.HatOffsetY" /> can be used instead of this hook for a constant Y offset.</br>
		/// <br><seealso cref="F:Terraria.ID.NPCID.Sets.NPCFramingGroup" /> can be additionally be used for the Y offset for the Town NPC's animations.</br>
		/// <para /> Called on all clients.
		/// </summary>
		/// <param name="position">
		/// <br>This is the final position right before the party hat gets drawn which is generally the top center of the NPC's hitbox.</br>
		/// <br><seealso cref="F:Terraria.ID.NPCID.Sets.HatOffsetY" /> and <seealso cref="F:Terraria.ID.NPCID.Sets.NPCFramingGroup" /> are already taken into account.</br>
		/// </param>
		/// <param name="spriteEffects">Allows you to change which way the party hat is flipped.</param>
		// Token: 0x06002275 RID: 8821 RVA: 0x004E7B0E File Offset: 0x004E5D0E
		public virtual void PartyHatPosition(ref Vector2 position, ref SpriteEffects spriteEffects)
		{
		}

		/// <summary>
		/// Allows you to change the location and sprite direction of the emote bubble when anchored to an NPC.
		/// <para /> Called on all clients.
		/// </summary>
		/// <param name="position">
		/// <br>The default position is:</br>
		/// <br>The X component is set to the NPC's Top + 75% of their width.</br>
		/// <br>Code: <c>entity.Top.X + ((-entity.direction * entity.width) * 0.75f)</c></br>
		/// <br>The Y component is set to the NPC's Y position + 2 pixels. (Positive Y is down.)</br>
		/// <br>Code: <c>entity.VisualPosition.Y + 2f</c></br>
		/// <br>(<seealso cref="P:Terraria.Entity.VisualPosition" /> is only used for the player for <seealso cref="F:Terraria.Player.gfxOffY" />)</br>
		/// </param>
		/// <param name="spriteEffects">Allows you to change which way the emote bubble is flipped.</param>
		// Token: 0x06002276 RID: 8822 RVA: 0x004E7B10 File Offset: 0x004E5D10
		public virtual void EmoteBubblePosition(ref Vector2 position, ref SpriteEffects spriteEffects)
		{
		}
	}
}
