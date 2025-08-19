using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.UI;
using Terraria.Localization;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.IO;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This class allows you to modify and use hooks for all NPCs, both vanilla and modded.
	/// <br /> To use it, simply create a new class deriving from this one. Implementations will be registered automatically.
	/// </summary>
	// Token: 0x0200016E RID: 366
	public abstract class GlobalNPC : GlobalType<NPC, GlobalNPC>
	{
		// Token: 0x06001D13 RID: 7443 RVA: 0x004D3E30 File Offset: 0x004D2030
		protected override void ValidateType()
		{
			base.ValidateType();
			LoaderUtils.MustOverrideTogether<GlobalNPC>(this, new Expression<Func<GlobalNPC, Delegate>>[]
			{
				(GlobalNPC g) => (Action<NPC, TagCompound>)methodof(GlobalNPC.SaveData(NPC, TagCompound)).CreateDelegate(typeof(Action<NPC, TagCompound>), g),
				(GlobalNPC g) => (Action<NPC, TagCompound>)methodof(GlobalNPC.LoadData(NPC, TagCompound)).CreateDelegate(typeof(Action<NPC, TagCompound>), g)
			});
			LoaderUtils.MustOverrideTogether<GlobalNPC>(this, new Expression<Func<GlobalNPC, Delegate>>[]
			{
				(GlobalNPC g) => (Action<NPC, BitWriter, BinaryWriter>)methodof(GlobalNPC.SendExtraAI(NPC, BitWriter, BinaryWriter)).CreateDelegate(typeof(Action<NPC, BitWriter, BinaryWriter>), g),
				(GlobalNPC g) => (Action<NPC, BitReader, BinaryReader>)methodof(GlobalNPC.ReceiveExtraAI(NPC, BitReader, BinaryReader)).CreateDelegate(typeof(Action<NPC, BitReader, BinaryReader>), g)
			});
		}

		// Token: 0x06001D14 RID: 7444 RVA: 0x004D4093 File Offset: 0x004D2293
		protected sealed override void Register()
		{
			base.Register();
		}

		// Token: 0x06001D15 RID: 7445 RVA: 0x004D409B File Offset: 0x004D229B
		public sealed override void SetupContent()
		{
			this.SetStaticDefaults();
		}

		/// <summary>
		/// Called after SetDefaults for NPCs with a negative <see cref="F:Terraria.NPC.netID" /><br />
		/// This hook is required because <see cref="M:Terraria.NPC.SetDefaultsFromNetId(System.Int32,Terraria.NPCSpawnParams)" /> only sets <see cref="F:Terraria.NPC.netID" /> after SetDefaults<br />
		/// Remember that <see cref="F:Terraria.NPC.type" /> does not support negative numbers and AppliesToEntity cannot distinguish between NPCs with the same type but different netID<br />
		/// </summary>
		// Token: 0x06001D16 RID: 7446 RVA: 0x004D40A3 File Offset: 0x004D22A3
		public virtual void SetDefaultsFromNetId(NPC npc)
		{
		}

		/// <summary>
		/// Gets called when any NPC spawns in world
		/// <para /> Called in single player or on the server only.
		/// </summary>
		// Token: 0x06001D17 RID: 7447 RVA: 0x004D40A5 File Offset: 0x004D22A5
		public virtual void OnSpawn(NPC npc, IEntitySource source)
		{
		}

		/// <summary>
		/// Allows you to customize this NPC's stats when the difficulty is expert or higher.<br />
		/// This runs after <see cref="F:Terraria.NPC.value" />,  <see cref="F:Terraria.NPC.lifeMax" />,  <see cref="F:Terraria.NPC.damage" />,  <see cref="F:Terraria.NPC.knockBackResist" /> have been adjusted for the current difficulty, (expert/master/FTW)<br />
		/// It is common to multiply lifeMax by the balance factor, and sometimes adjust knockbackResist.<br />
		/// <br />
		/// Eg:<br />
		/// <code>lifeMax = (int)(lifeMax * balance * bossAdjustment)</code>
		/// </summary>
		/// <param name="npc">The newly spawned NPC</param>
		/// <param name="numPlayers">The number of active players</param>
		/// <param name="balance">Scaling factor that increases by a fraction for each player</param>
		/// <param name="bossAdjustment">An extra reduction factor to be applied to boss life in high difficulty modes</param>
		// Token: 0x06001D18 RID: 7448 RVA: 0x004D40A7 File Offset: 0x004D22A7
		public virtual void ApplyDifficultyAndPlayerScaling(NPC npc, int numPlayers, float balance, float bossAdjustment)
		{
		}

		/// <summary>
		/// Allows you to set an NPC's information in the Bestiary.
		/// </summary>
		/// <param name="npc"></param>
		/// <param name="database"></param>
		/// <param name="bestiaryEntry"></param>
		// Token: 0x06001D19 RID: 7449 RVA: 0x004D40A9 File Offset: 0x004D22A9
		public virtual void SetBestiary(NPC npc, BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
		}

		/// <summary>
		/// Allows you to modify the type name of this NPC dynamically.
		/// </summary>
		// Token: 0x06001D1A RID: 7450 RVA: 0x004D40AB File Offset: 0x004D22AB
		public virtual void ModifyTypeName(NPC npc, ref string typeName)
		{
		}

		/// <summary>
		/// Allows you to modify the bounding box for hovering over the given NPC (affects things like whether or not its name is displayed).
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="npc">The NPC in question.</param>
		/// <param name="boundingBox">The bounding box used for determining whether or not the NPC counts as being hovered over.</param>
		// Token: 0x06001D1B RID: 7451 RVA: 0x004D40AD File Offset: 0x004D22AD
		public virtual void ModifyHoverBoundingBox(NPC npc, ref Rectangle boundingBox)
		{
		}

		/// <summary>
		/// Allows you to set the town NPC profile that a given NPC uses.
		/// </summary>
		/// <param name="npc">The NPC in question.</param>
		/// <returns>The profile that you want the given NPC to use.<br></br>
		/// This will only influence their choice of profile if you do not return null.<br></br>
		/// By default, returns null, which causes no change.</returns>
		// Token: 0x06001D1C RID: 7452 RVA: 0x004D40AF File Offset: 0x004D22AF
		public virtual ITownNPCProfile ModifyTownNPCProfile(NPC npc)
		{
			return null;
		}

		/// <summary>
		/// Allows you to modify the list of names available to the given town NPC.
		/// </summary>
		// Token: 0x06001D1D RID: 7453 RVA: 0x004D40B2 File Offset: 0x004D22B2
		public virtual void ModifyNPCNameList(NPC npc, List<string> nameList)
		{
		}

		/// <summary>
		/// This is where you reset any fields you add to your subclass to their default states. This is necessary in order to reset your fields if they are conditionally set by a tick update but the condition is no longer satisfied.
		/// <para /> Called on the server and clients.
		/// </summary>
		/// <param name="npc"></param>
		// Token: 0x06001D1E RID: 7454 RVA: 0x004D40B4 File Offset: 0x004D22B4
		public virtual void ResetEffects(NPC npc)
		{
		}

		/// <summary>
		/// Allows you to determine how any NPC behaves. Return false to stop the vanilla AI and the AI hook from being run. Returns true by default.
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
		/// <param name="npc" />
		/// <returns />
		// Token: 0x06001D1F RID: 7455 RVA: 0x004D40B6 File Offset: 0x004D22B6
		public virtual bool PreAI(NPC npc)
		{
			return true;
		}

		/// <summary>
		/// Allows you to determine how any NPC behaves. This will only be called if PreAI returns true.
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
		/// <param name="npc" />
		// Token: 0x06001D20 RID: 7456 RVA: 0x004D40B9 File Offset: 0x004D22B9
		public virtual void AI(NPC npc)
		{
		}

		/// <summary>
		/// Allows you to determine how any NPC behaves. This will be called regardless of what PreAI returns.
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
		/// <param name="npc" />
		// Token: 0x06001D21 RID: 7457 RVA: 0x004D40BB File Offset: 0x004D22BB
		public virtual void PostAI(NPC npc)
		{
		}

		/// <summary>
		/// Use this judiciously to avoid straining the network.
		/// <para /> Checks and methods such as <see cref="M:Terraria.ModLoader.GlobalType`2.AppliesToEntity(`0,System.Boolean)" /> can reduce how much data must be sent for how many projectiles.
		/// <para /> Called whenever <see cref="F:Terraria.ID.MessageID.SyncNPC" /> is successfully sent, for example on NPC creation, on player join, or whenever NPC.netUpdate is set to true in the update loop for that tick.
		/// <para /> Can be called on the server.
		/// </summary>
		/// <param name="npc">The NPC.</param>
		/// <param name="bitWriter">The compressible bit writer. Booleans written via this are compressed across all mods to improve multiplayer performance.</param>
		/// <param name="binaryWriter">The writer.</param>
		// Token: 0x06001D22 RID: 7458 RVA: 0x004D40BD File Offset: 0x004D22BD
		public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
		{
		}

		/// <summary>
		/// Use this to receive information that was sent in <see cref="M:Terraria.ModLoader.GlobalNPC.SendExtraAI(Terraria.NPC,Terraria.ModLoader.IO.BitWriter,System.IO.BinaryWriter)" />.
		/// <para /> Called whenever <see cref="F:Terraria.ID.MessageID.SyncNPC" /> is successfully received.
		/// <para /> Can be called on multiplayer clients.
		/// </summary>
		/// <param name="npc">The NPC.</param>
		/// <param name="bitReader">The compressible bit reader.</param>
		/// <param name="binaryReader">The reader.</param>
		// Token: 0x06001D23 RID: 7459 RVA: 0x004D40BF File Offset: 0x004D22BF
		public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
		{
		}

		/// <summary>
		/// Allows you to modify the frame from an NPC's texture that is drawn, which is necessary in order to animate NPCs.
		/// <para /> Called on the server and clients.
		/// </summary>
		/// <param name="npc"></param>
		/// <param name="frameHeight"></param>
		// Token: 0x06001D24 RID: 7460 RVA: 0x004D40C1 File Offset: 0x004D22C1
		public virtual void FindFrame(NPC npc, int frameHeight)
		{
		}

		/// <summary>
		/// Allows you to make things happen whenever an NPC is hit, such as creating dust or gores.
		/// <para /> Called on local, server, and remote clients.
		/// <para /> Usually when something happens when an npc dies such as item spawning, you use NPCLoot, but you can use HitEffect paired with a check for <c>if (npc.life &lt;= 0)</c> to do client-side death effects, such as spawning dust, gore, or death sounds. <br />
		/// </summary>
		// Token: 0x06001D25 RID: 7461 RVA: 0x004D40C3 File Offset: 0x004D22C3
		public virtual void HitEffect(NPC npc, NPC.HitInfo hit)
		{
		}

		/// <summary>
		/// Allows you to make the NPC either regenerate health or take damage over time by setting <see cref="F:Terraria.NPC.lifeRegen" />. This is useful for implementing damage over time debuffs such as <see cref="F:Terraria.ID.BuffID.Poisoned" /> or <see cref="F:Terraria.ID.BuffID.OnFire" />. Regeneration or damage will occur at a rate of half of <see cref="F:Terraria.NPC.lifeRegen" /> per second.
		/// <para /> Essentially, modders implementing damage over time debuffs should subtract from <see cref="F:Terraria.NPC.lifeRegen" /> a number that is twice as large as the intended damage per second. See <see href="https://github.com/tModLoader/tModLoader/blob/stable/ExampleMod/Common/GlobalNPCs/DamageOverTimeGlobalNPC.cs#L16">DamageOverTimeGlobalNPC.cs</see> for an example of this.
		/// <para /> The damage parameter is the number that appears above the NPC's head if it takes damage over time.
		/// <para /> Multiple debuffs work together by following some conventions: <see cref="F:Terraria.NPC.lifeRegen" /> should not be assigned a number, rather it should be subtracted from. <paramref name="damage" /> should only be assigned if the intended popup text is larger then its current value.
		/// <para /> Called on the server and clients.
		/// </summary>
		/// <param name="npc"></param>
		/// <param name="damage"></param>
		// Token: 0x06001D26 RID: 7462 RVA: 0x004D40C5 File Offset: 0x004D22C5
		public virtual void UpdateLifeRegen(NPC npc, ref int damage)
		{
		}

		/// <summary>
		/// Whether or not to run the code for checking whether an NPC will remain active. Return false to stop the NPC from being despawned and to stop the NPC from counting towards the limit for how many NPCs can exist near a player. Returns true by default.
		/// <para /> Called on the server and clients.
		/// </summary>
		/// <param name="npc"></param>
		/// <returns></returns>
		// Token: 0x06001D27 RID: 7463 RVA: 0x004D40C7 File Offset: 0x004D22C7
		public virtual bool CheckActive(NPC npc)
		{
			return true;
		}

		/// <summary>
		/// Whether or not an NPC should be killed when it reaches 0 health. You may program extra effects in this hook (for example, how Golem's head lifts up for the second phase of its fight). Return false to stop the NPC from being killed. Returns true by default.
		/// <para /> Called on the server and clients.
		/// </summary>
		/// <param name="npc"></param>
		/// <returns></returns>
		// Token: 0x06001D28 RID: 7464 RVA: 0x004D40CA File Offset: 0x004D22CA
		public virtual bool CheckDead(NPC npc)
		{
			return true;
		}

		/// <summary>
		/// Allows you to call OnKill on your own when the NPC dies, rather then letting vanilla call it on its own. Returns false by default.
		/// <para /> Called on the server and clients.
		/// </summary>
		/// <returns>Return true to stop vanilla from calling OnKill on its own. Do this if you call OnKill yourself.</returns>
		// Token: 0x06001D29 RID: 7465 RVA: 0x004D40CD File Offset: 0x004D22CD
		public virtual bool SpecialOnKill(NPC npc)
		{
			return false;
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.ModNPC.PreKill" />
		// Token: 0x06001D2A RID: 7466 RVA: 0x004D40D0 File Offset: 0x004D22D0
		public virtual bool PreKill(NPC npc)
		{
			return true;
		}

		/// <summary>
		/// Allows you to make things happen when an NPC dies (for example, setting ModSystem fields). For client-side effects, such as dust, gore, and sounds, see <see cref="M:Terraria.ModLoader.GlobalNPC.HitEffect(Terraria.NPC,Terraria.NPC.HitInfo)" />.
		/// <br /><br /> Boss flags are set after this, so this method is suitable to check if a boss defeated flag is false to detect if this is the first victory against that boss.
		/// <para /> Called in single player or on the server only.
		/// <para /> Most item drops should be done via drop rules registered in <see cref="M:Terraria.ModLoader.GlobalNPC.ModifyNPCLoot(Terraria.NPC,Terraria.ModLoader.NPCLoot)" /> or <see cref="M:Terraria.ModLoader.GlobalNPC.ModifyGlobalLoot(Terraria.ModLoader.GlobalLoot)" />. Some dynamic NPC drops, such as additional hearts, are more suited for OnKill instead. <see href="https://github.com/tModLoader/tModLoader/blob/stable/ExampleMod/Content/NPCs/MinionBoss/MinionBossMinion.cs#L101">MinionBossMinion.cs</see> shows an example of an NPC that drops additional hearts. See <see cref="F:Terraria.NPC.lastInteraction" /> and <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-NPC-Drops-and-Loot-1.4#player-who-killed-npc">Player who killed NPC wiki section</see> as well for determining which players attacked or killed this NPC.
		/// </summary>
		// Token: 0x06001D2B RID: 7467 RVA: 0x004D40D3 File Offset: 0x004D22D3
		public virtual void OnKill(NPC npc)
		{
		}

		/// <summary>
		/// Allows you to determine how and when an NPC can fall through platforms and similar tiles.
		/// <para /> Return true to allow an NPC to fall through platforms, false to prevent it. Returns null by default, applying vanilla behaviors (based on aiStyle and type).
		/// <para /> Called on the server and clients.
		/// </summary>
		// Token: 0x06001D2C RID: 7468 RVA: 0x004D40D8 File Offset: 0x004D22D8
		public virtual bool? CanFallThroughPlatforms(NPC npc)
		{
			return null;
		}

		/// <summary>
		/// Allows you to determine whether the given item can catch the given NPC.<br></br>
		/// Return true or false to say the given NPC can or cannot be caught, respectively, regardless of vanilla rules.
		/// <para /> Returns null by default, which allows vanilla's NPC catching rules to decide the target's fate.
		/// <para /> If this returns false, <see cref="M:Terraria.ModLoader.CombinedHooks.OnCatchNPC(Terraria.Player,Terraria.NPC,Terraria.Item,System.Boolean)" /> is never called.
		/// <para /> NOTE: this does not classify the given item as an NPC-catching tool, which is necessary for catching NPCs in the first place. To do that, you will need to use the "CatchingTool" set in ItemID.Sets.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="npc">The NPC that can potentially be caught.</param>
		/// <param name="item">The item with which the player is trying to catch the given NPC.</param>
		/// <param name="player">The player attempting to catch the given NPC.</param>
		/// <returns></returns>
		// Token: 0x06001D2D RID: 7469 RVA: 0x004D40F0 File Offset: 0x004D22F0
		public virtual bool? CanBeCaughtBy(NPC npc, Item item, Player player)
		{
			return null;
		}

		/// <summary>
		/// Allows you to make things happen when the given item attempts to catch the given NPC.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="npc">The NPC which the player attempted to catch.</param>
		/// <param name="player">The player attempting to catch the given NPC.</param>
		/// <param name="item">The item used to catch the given NPC.</param>
		/// <param name="failed">Whether or not the given NPC has been successfully caught.</param>
		// Token: 0x06001D2E RID: 7470 RVA: 0x004D4106 File Offset: 0x004D2306
		public virtual void OnCaughtBy(NPC npc, Player player, Item item, bool failed)
		{
		}

		/// <summary>
		/// Allows you to add and modify NPC loot tables to drop on death and to appear in the Bestiary.<br />
		/// The <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-NPC-Drops-and-Loot-1.4">Basic NPC Drops and Loot 1.4 Guide</see> explains how to use this hook to modify npc loot.
		/// <br /> This hook only runs once per npc type during mod loading, any dynamic behavior must be contained in the rules themselves.
		/// </summary>
		/// <param name="npc">A default npc of the type being opened, not the actual npc instance.</param>
		/// <param name="npcLoot">A reference to the item drop database for this npc type.</param>
		// Token: 0x06001D2F RID: 7471 RVA: 0x004D4108 File Offset: 0x004D2308
		public virtual void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
		{
		}

		/// <summary>
		/// Allows you to add and modify global loot rules that are conditional, i.e. vanilla's biome keys and souls.<br />
		/// The <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-NPC-Drops-and-Loot-1.4">Basic NPC Drops and Loot 1.4 Guide</see> explains how to use this hook to modify npc loot.
		/// </summary>
		/// <param name="globalLoot"></param>
		// Token: 0x06001D30 RID: 7472 RVA: 0x004D410A File Offset: 0x004D230A
		public virtual void ModifyGlobalLoot(GlobalLoot globalLoot)
		{
		}

		/// <summary>
		/// Allows you to determine whether an NPC can hit the given player. Return false to block the NPC from hitting the target. Returns true by default. CooldownSlot determines which of the player's cooldown counters (<see cref="T:Terraria.ID.ImmunityCooldownID" />) to use, and defaults to -1 (<see cref="F:Terraria.ID.ImmunityCooldownID.General" />).
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="npc"></param>
		/// <param name="target"></param>
		/// <param name="cooldownSlot"></param>
		/// <returns></returns>
		// Token: 0x06001D31 RID: 7473 RVA: 0x004D410C File Offset: 0x004D230C
		public virtual bool CanHitPlayer(NPC npc, Player target, ref int cooldownSlot)
		{
			return true;
		}

		/// <summary>
		/// Allows you to modify the damage, etc., that an NPC does to a player.
		/// <para /> This hook should be used ONLY to modify properties of the HitModifiers. Any extra side effects should occur in OnHit hooks instead.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="npc"></param>
		/// <param name="target"></param>
		/// <param name="modifiers"></param>
		// Token: 0x06001D32 RID: 7474 RVA: 0x004D410F File Offset: 0x004D230F
		public virtual void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
		{
		}

		/// <summary>
		/// Allows you to create special effects when an NPC hits a player (for example, inflicting debuffs).
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="npc"></param>
		/// <param name="target"></param>
		/// <param name="hurtInfo"></param>
		// Token: 0x06001D33 RID: 7475 RVA: 0x004D4111 File Offset: 0x004D2311
		public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
		{
		}

		/// <summary>
		/// Allows you to determine whether an NPC can hit the given friendly NPC. Return false to block the NPC from hitting the target, and return true to use the vanilla code for whether the target can be hit. Returns true by default.
		/// <para /> Called in single player or on the server only.
		/// </summary>
		/// <param name="npc"></param>
		/// <param name="target"></param>
		/// <returns></returns>
		// Token: 0x06001D34 RID: 7476 RVA: 0x004D4113 File Offset: 0x004D2313
		public virtual bool CanHitNPC(NPC npc, NPC target)
		{
			return true;
		}

		/// <summary>
		/// Allows you to determine whether a friendly NPC can be hit by an NPC. Return false to block the attacker from hitting the NPC, and return true to use the vanilla code for whether the target can be hit. Returns true by default.
		/// <para /> Called in single player or on the server only.
		/// </summary>
		/// <param name="npc"></param>
		/// <param name="attacker"></param>
		/// <returns></returns>
		// Token: 0x06001D35 RID: 7477 RVA: 0x004D4116 File Offset: 0x004D2316
		public virtual bool CanBeHitByNPC(NPC npc, NPC attacker)
		{
			return true;
		}

		/// <summary>
		/// Allows you to modify the damage, knockback, etc., that an NPC does to a friendly NPC.
		/// <para /> This hook should be used ONLY to modify properties of the HitModifiers. Any extra side effects should occur in OnHit hooks instead.
		/// <para /> Called in single player or on the server only.
		/// </summary>
		/// <param name="npc"></param>
		/// <param name="target"></param>
		/// <param name="modifiers"></param>
		// Token: 0x06001D36 RID: 7478 RVA: 0x004D4119 File Offset: 0x004D2319
		public virtual void ModifyHitNPC(NPC npc, NPC target, ref NPC.HitModifiers modifiers)
		{
		}

		/// <summary>
		/// Allows you to create special effects when an NPC hits a friendly NPC.
		/// <para /> Called in single player or on the server only.
		/// </summary>
		/// <param name="npc"></param>
		/// <param name="target"></param>
		/// <param name="hit"></param>
		// Token: 0x06001D37 RID: 7479 RVA: 0x004D411B File Offset: 0x004D231B
		public virtual void OnHitNPC(NPC npc, NPC target, NPC.HitInfo hit)
		{
		}

		/// <summary>
		/// Allows you to determine whether an NPC can be hit by the given melee weapon when swung. Return true to allow hitting the NPC, return false to block hitting the NPC, and return null to use the vanilla code for whether the NPC can be hit. Returns null by default.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="npc"></param>
		/// <param name="player"></param>
		/// <param name="item"></param>
		/// <returns></returns>
		// Token: 0x06001D38 RID: 7480 RVA: 0x004D4120 File Offset: 0x004D2320
		public virtual bool? CanBeHitByItem(NPC npc, Player player, Item item)
		{
			return null;
		}

		/// <summary>
		/// Allows you to determine whether an NPC can be collided with the player melee weapon when swung.
		/// <para /> Use <see cref="M:Terraria.ModLoader.GlobalNPC.CanBeHitByItem(Terraria.NPC,Terraria.Player,Terraria.Item)" /> instead for Guide Voodoo Doll-type effects.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="npc">The NPC being collided with</param>
		/// <param name="player">The player wielding this item.</param>
		/// <param name="item">The weapon item the player is holding.</param>
		/// <param name="meleeAttackHitbox">Hitbox of melee attack.</param>
		/// <returns>
		/// Return true to allow colliding with the melee attack, return false to block the weapon from colliding with the NPC, and return null to use the vanilla code for whether the attack can be colliding. Returns null by default.
		/// </returns>
		// Token: 0x06001D39 RID: 7481 RVA: 0x004D4138 File Offset: 0x004D2338
		public virtual bool? CanCollideWithPlayerMeleeAttack(NPC npc, Player player, Item item, Rectangle meleeAttackHitbox)
		{
			return null;
		}

		/// <summary>
		/// Allows you to modify the damage, knockback, etc., that an NPC takes from a melee weapon.
		/// <para /> This hook should be used ONLY to modify properties of the HitModifiers. Any extra side effects should occur in OnHit hooks instead.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="npc"></param>
		/// <param name="player"></param>
		/// <param name="item"></param>
		/// <param name="modifiers"></param>
		// Token: 0x06001D3A RID: 7482 RVA: 0x004D414E File Offset: 0x004D234E
		public virtual void ModifyHitByItem(NPC npc, Player player, Item item, ref NPC.HitModifiers modifiers)
		{
		}

		/// <summary>
		/// Allows you to create special effects when an NPC is hit by a melee weapon.
		/// <para /> Called on the client doing the damage.
		/// </summary>
		/// <param name="npc"></param>
		/// <param name="player"></param>
		/// <param name="item"></param>
		/// <param name="hit"></param>
		/// <param name="damageDone"></param>
		// Token: 0x06001D3B RID: 7483 RVA: 0x004D4150 File Offset: 0x004D2350
		public virtual void OnHitByItem(NPC npc, Player player, Item item, NPC.HitInfo hit, int damageDone)
		{
		}

		/// <summary>
		/// Allows you to determine whether an NPC can be hit by the given projectile. Return true to allow hitting the NPC, return false to block hitting the NPC, and return null to use the vanilla code for whether the NPC can be hit. Returns null by default.
		/// <para /> Can be called on the local client or server, depending on who owns the projectile.
		/// </summary>
		/// <param name="npc"></param>
		/// <param name="projectile"></param>
		/// <returns></returns>
		// Token: 0x06001D3C RID: 7484 RVA: 0x004D4154 File Offset: 0x004D2354
		public virtual bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
		{
			return null;
		}

		/// <summary>
		/// Allows you to modify the damage, knockback, etc., that an NPC takes from a projectile.
		/// <para /> This hook should be used ONLY to modify properties of the HitModifiers. Any extra side effects should occur in OnHit hooks instead.
		/// <para /> Can be called on the local client or server, depending on who owns the projectile.
		/// </summary>
		/// <param name="npc"></param>
		/// <param name="projectile"></param>
		/// <param name="modifiers"></param>
		// Token: 0x06001D3D RID: 7485 RVA: 0x004D416A File Offset: 0x004D236A
		public virtual void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
		{
		}

		/// <summary>
		/// Allows you to create special effects when an NPC is hit by a projectile.
		/// <para /> Can be called on the local client or server, depending on who owns the projectile.
		/// </summary>
		/// <param name="npc"></param>
		/// <param name="projectile"></param>
		/// <param name="hit"></param>
		/// <param name="damageDone"></param>
		// Token: 0x06001D3E RID: 7486 RVA: 0x004D416C File Offset: 0x004D236C
		public virtual void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
		{
		}

		/// <summary>
		/// Allows you to use a custom damage formula for when an NPC takes damage from any source. For example, you can change the way defense works or use a different crit multiplier.
		/// <para /> This hook should be used ONLY to modify properties of the HitModifiers. Any extra side effects should occur in OnHit hooks instead.
		/// <para /> Can be called on the local client or server, depending on who is dealing damage.
		/// </summary>
		/// <param name="npc"></param>
		/// <param name="modifiers"></param>
		// Token: 0x06001D3F RID: 7487 RVA: 0x004D416E File Offset: 0x004D236E
		public virtual void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
		{
		}

		/// <summary>
		/// Allows you to customize the boss head texture used by an NPC based on its state. Set index to -1 to stop the texture from being displayed.
		/// <para /> Called on all clients.
		/// </summary>
		/// <param name="npc"></param>
		/// <param name="index">The index for NPCID.Sets.BossHeadTextures</param>
		// Token: 0x06001D40 RID: 7488 RVA: 0x004D4170 File Offset: 0x004D2370
		public virtual void BossHeadSlot(NPC npc, ref int index)
		{
		}

		/// <summary>
		/// Allows you to customize the rotation of an NPC's boss head icon on the map.
		/// <para /> Called on all clients.
		/// </summary>
		/// <param name="npc"></param>
		/// <param name="rotation"></param>
		// Token: 0x06001D41 RID: 7489 RVA: 0x004D4172 File Offset: 0x004D2372
		public virtual void BossHeadRotation(NPC npc, ref float rotation)
		{
		}

		/// <summary>
		/// Allows you to flip an NPC's boss head icon on the map.
		/// <para /> Called on all clients.
		/// </summary>
		/// <param name="npc"></param>
		/// <param name="spriteEffects"></param>
		// Token: 0x06001D42 RID: 7490 RVA: 0x004D4174 File Offset: 0x004D2374
		public virtual void BossHeadSpriteEffects(NPC npc, ref SpriteEffects spriteEffects)
		{
		}

		/// <summary>
		/// Allows you to determine the color and transparency in which an NPC is drawn. Return null to use the default color (normally light and buff color). Returns null by default.
		/// <para /> Called on all clients.
		/// </summary>
		/// <param name="npc"></param>
		/// <param name="drawColor"></param>
		/// <returns></returns>
		// Token: 0x06001D43 RID: 7491 RVA: 0x004D4178 File Offset: 0x004D2378
		public virtual Color? GetAlpha(NPC npc, Color drawColor)
		{
			return null;
		}

		/// <summary>
		/// Allows you to add special visual effects to an NPC (such as creating dust), and modify the color in which the NPC is drawn.
		/// <para /> Called on all clients.
		/// </summary>
		/// <param name="npc"></param>
		/// <param name="drawColor"></param>
		// Token: 0x06001D44 RID: 7492 RVA: 0x004D418E File Offset: 0x004D238E
		public virtual void DrawEffects(NPC npc, ref Color drawColor)
		{
		}

		/// <summary>
		/// Allows you to draw things behind an NPC, or to modify the way the NPC is drawn. Substract screenPos from the draw position before drawing. Return false to stop the game from drawing the NPC (useful if you're manually drawing the NPC). Returns true by default.
		/// <para /> Called on all clients.
		/// </summary>
		/// <param name="npc">The NPC that is being drawn</param>
		/// <param name="spriteBatch">The spritebatch to draw on</param>
		/// <param name="screenPos">The screen position used to translate world position into screen position</param>
		/// <param name="drawColor">The color the NPC is drawn in</param>
		/// <returns></returns>
		// Token: 0x06001D45 RID: 7493 RVA: 0x004D4190 File Offset: 0x004D2390
		public virtual bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			return true;
		}

		/// <summary>
		/// Allows you to draw things in front of this NPC. Substract screenPos from the draw position before drawing. This method is called even if PreDraw returns false.
		/// <para /> Called on all clients.
		/// </summary>
		/// <param name="npc">The NPC that is being drawn</param>
		/// <param name="spriteBatch">The spritebatch to draw on</param>
		/// <param name="screenPos">The screen position used to translate world position into screen position</param>
		/// <param name="drawColor">The color the NPC is drawn in</param>
		// Token: 0x06001D46 RID: 7494 RVA: 0x004D4193 File Offset: 0x004D2393
		public virtual void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
		}

		/// <summary>
		/// When used in conjunction with "npc.hide = true", allows you to specify that this npc should be drawn behind certain elements. Add the index to one of Main.DrawCacheNPCsMoonMoon, DrawCacheNPCsOverPlayers, DrawCacheNPCProjectiles, or DrawCacheNPCsBehindNonSolidTiles.
		/// <para /> Called on all clients.
		/// </summary>
		/// <param name="npc"></param>
		/// <param name="index"></param>
		// Token: 0x06001D47 RID: 7495 RVA: 0x004D4195 File Offset: 0x004D2395
		public virtual void DrawBehind(NPC npc, int index)
		{
		}

		/// <summary>
		/// Allows you to control how the health bar for the given NPC is drawn. The hbPosition parameter is the same as Main.hbPosition; it determines whether the health bar gets drawn above or below the NPC by default. The scale parameter is the health bar's size. By default, it will be the normal 1f; most bosses set this to 1.5f. Return null to let the normal vanilla health-bar-drawing code to run. Return false to stop the health bar from being drawn. Return true to draw the health bar in the position specified by the position parameter (note that this is the world position, not screen position).
		/// <para /> Called on all clients.
		/// </summary>
		/// <param name="npc"></param>
		/// <param name="hbPosition"></param>
		/// <param name="scale"></param>
		/// <param name="position"></param>
		/// <returns></returns>
		// Token: 0x06001D48 RID: 7496 RVA: 0x004D4198 File Offset: 0x004D2398
		public virtual bool? DrawHealthBar(NPC npc, byte hbPosition, ref float scale, ref Vector2 position)
		{
			return null;
		}

		/// <summary>
		/// Allows you to modify the chance of NPCs spawning around the given player and the maximum number of NPCs that can spawn around the player. Lower spawnRates mean a higher chance for NPCs to spawn.
		/// <para /> Called in single player or on the server only.
		/// </summary>
		/// <param name="player"></param>
		/// <param name="spawnRate"></param>
		/// <param name="maxSpawns"></param>
		// Token: 0x06001D49 RID: 7497 RVA: 0x004D41AE File Offset: 0x004D23AE
		public virtual void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
		{
		}

		/// <summary>
		/// Allows you to modify the range at which NPCs can spawn around the given player. The spawnRanges determine that maximum distance NPCs can spawn from the player, and the safeRanges determine the minimum distance.
		/// <para /> Called in single player or on the server only.
		/// </summary>
		/// <param name="player"></param>
		/// <param name="spawnRangeX"></param>
		/// <param name="spawnRangeY"></param>
		/// <param name="safeRangeX"></param>
		/// <param name="safeRangeY"></param>
		// Token: 0x06001D4A RID: 7498 RVA: 0x004D41B0 File Offset: 0x004D23B0
		public virtual void EditSpawnRange(Player player, ref int spawnRangeX, ref int spawnRangeY, ref int safeRangeX, ref int safeRangeY)
		{
		}

		/// <summary>
		/// Allows you to control which NPCs can spawn and how likely each one is to spawn. The pool parameter maps NPC types to their spawning weights (likelihood to spawn compared to other NPCs). A type of 0 in the pool represents the default vanilla NPC spawning.
		/// <para /> Called in single player or on the server only.
		/// </summary>
		/// <param name="pool"></param>
		/// <param name="spawnInfo"></param>
		// Token: 0x06001D4B RID: 7499 RVA: 0x004D41B2 File Offset: 0x004D23B2
		public virtual void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
		{
		}

		/// <summary>
		/// Allows you to customize an NPC (for example, its position or ai array) after it naturally spawns and before it is synced between servers and clients. As of right now, this only works for modded NPCs.
		/// <para /> Called in single player or on the server only.
		/// </summary>
		/// <param name="npc"></param>
		/// <param name="tileX"></param>
		/// <param name="tileY"></param>
		// Token: 0x06001D4C RID: 7500 RVA: 0x004D41B4 File Offset: 0x004D23B4
		public virtual void SpawnNPC(int npc, int tileX, int tileY)
		{
		}

		/// <summary>
		/// Allows you to determine whether this NPC can talk with the player. Return true to allow talking with the player, return false to block this NPC from talking with the player, and return null to use the vanilla code for whether the NPC can talk. Returns null by default.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="npc"></param>
		/// <returns></returns>
		// Token: 0x06001D4D RID: 7501 RVA: 0x004D41B8 File Offset: 0x004D23B8
		public virtual bool? CanChat(NPC npc)
		{
			return null;
		}

		/// <summary>
		/// Allows you to modify the chat message of any NPC that the player can talk to.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="npc"></param>
		/// <param name="chat"></param>
		// Token: 0x06001D4E RID: 7502 RVA: 0x004D41CE File Offset: 0x004D23CE
		public virtual void GetChat(NPC npc, ref string chat)
		{
		}

		/// <summary>
		/// Allows you to determine if something can happen whenever a button is clicked on this NPC's chat window. The firstButton parameter tells whether the first button or second button (button and button2 from SetChatButtons) was clicked. Return false to prevent the normal code for this button from running. Returns true by default.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="npc"></param>
		/// <param name="firstButton"></param>
		/// <returns></returns>
		// Token: 0x06001D4F RID: 7503 RVA: 0x004D41D0 File Offset: 0x004D23D0
		public virtual bool PreChatButtonClicked(NPC npc, bool firstButton)
		{
			return true;
		}

		/// <summary>
		/// Allows you to make something happen whenever a button is clicked on this NPC's chat window. The firstButton parameter tells whether the first button or second button (button and button2 from SetChatButtons) was clicked.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="npc"></param>
		/// <param name="firstButton"></param>
		// Token: 0x06001D50 RID: 7504 RVA: 0x004D41D3 File Offset: 0x004D23D3
		public virtual void OnChatButtonClicked(NPC npc, bool firstButton)
		{
		}

		/// <summary>
		/// Allows you to modify existing shop. Be aware that this hook is called just one time during loading.
		/// <para /> The traveling merchant shop is handled separately in <see cref="M:Terraria.ModLoader.GlobalNPC.SetupTravelShop(System.Int32[],System.Int32@)" />.
		/// </summary>
		/// <param name="shop">A <seealso cref="T:Terraria.ModLoader.NPCShop" /> instance.</param>
		// Token: 0x06001D51 RID: 7505 RVA: 0x004D41D5 File Offset: 0x004D23D5
		public virtual void ModifyShop(NPCShop shop)
		{
		}

		/// <summary>
		/// Allows you to modify the contents of a shop whenever player opens it. <br />
		/// If possible, use <see cref="M:Terraria.ModLoader.GlobalNPC.ModifyShop(Terraria.ModLoader.NPCShop)" /> instead, to reduce mod conflicts and improve compatibility.
		/// Note that for special shops like travelling merchant, the <paramref name="shopName" /> may not correspond to a <see cref="T:Terraria.ModLoader.NPCShop" /> in the <see cref="T:Terraria.ModLoader.NPCShopDatabase" />
		/// <para /> Also note that unused slots in <paramref name="items" /> are null while <see cref="P:Terraria.Item.IsAir" /> entries are entries that have a reserved slot (<see cref="P:Terraria.ModLoader.NPCShop.Entry.SlotReserved" />) but did not have their conditions met. These should not be overwritten.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="npc">An instance of <seealso cref="T:Terraria.NPC" /> that currently player talks to.</param>
		/// <param name="shopName">The full name of the shop being opened. See <see cref="M:Terraria.ModLoader.NPCShopDatabase.GetShopName(System.Int32,System.String)" /> for the format. </param>
		/// <param name="items">Items in the shop including 'air' items in empty slots.</param>
		// Token: 0x06001D52 RID: 7506 RVA: 0x004D41D7 File Offset: 0x004D23D7
		public virtual void ModifyActiveShop(NPC npc, string shopName, Item[] items)
		{
		}

		/// <summary>
		/// Allows you to add items to the traveling merchant's shop. Add an item by setting shop[nextSlot] to the ID of the item you are adding then incrementing nextSlot. In the end, nextSlot must have a value of 1 greater than the highest index in shop that represents an item ID. If you want to remove an item, you will have to be familiar with programming.
		/// <para /> Called in single player or on the server only.
		/// </summary>
		/// <param name="shop"></param>
		/// <param name="nextSlot"></param>
		// Token: 0x06001D53 RID: 7507 RVA: 0x004D41D9 File Offset: 0x004D23D9
		public virtual void SetupTravelShop(int[] shop, ref int nextSlot)
		{
		}

		/// <summary>
		/// Whether this NPC can be teleported to a King or Queen statue. Return true to allow the NPC to teleport to the statue, return false to block this NPC from teleporting to the statue, and return null to use the vanilla code for whether the NPC can teleport to the statue. Returns null by default.
		/// <para /> Called in single player or on the server only.
		/// </summary>
		/// <param name="npc">The NPC</param>
		/// <param name="toKingStatue">Whether the NPC is being teleported to a King or Queen statue.</param>
		// Token: 0x06001D54 RID: 7508 RVA: 0x004D41DC File Offset: 0x004D23DC
		public virtual bool? CanGoToStatue(NPC npc, bool toKingStatue)
		{
			return null;
		}

		/// <summary>
		/// Allows you to make things happen when this NPC teleports to a King or Queen statue.
		/// <para /> Called in single player or on the server only.
		/// </summary>
		/// <param name="npc">The NPC</param>
		/// <param name="toKingStatue">Whether the NPC was teleported to a King or Queen statue.</param>
		// Token: 0x06001D55 RID: 7509 RVA: 0x004D41F2 File Offset: 0x004D23F2
		public virtual void OnGoToStatue(NPC npc, bool toKingStatue)
		{
		}

		/// <summary>
		/// Allows you to modify the stats of town NPCs. Useful for buffing town NPCs when certain bosses are defeated, etc.
		/// <para /> Called on the server and clients.
		/// </summary>
		/// <param name="damageMult"></param>
		/// <param name="defense"></param>
		// Token: 0x06001D56 RID: 7510 RVA: 0x004D41F4 File Offset: 0x004D23F4
		public virtual void BuffTownNPC(ref float damageMult, ref int defense)
		{
		}

		/// <summary>
		/// Allows you to modify the death message of a town NPC or boss. This also affects what the dropped tombstone will say in the case of a town NPC. The text color can also be modified.
		/// <para /> When modifying the death message, use <see cref="M:Terraria.NPC.GetFullNetName" /> to retrieve the NPC name to use in substitutions.
		/// <para /> Return false to skip the vanilla code for sending the message. This is useful if the death message is handled by this method or if the message should be skipped for any other reason, such as if there are multiple bosses. Returns true by default.
		/// </summary>
		// Token: 0x06001D57 RID: 7511 RVA: 0x004D41F6 File Offset: 0x004D23F6
		public virtual bool ModifyDeathMessage(NPC npc, ref NetworkText customText, ref Color color)
		{
			return true;
		}

		/// <summary>
		/// Allows you to determine the damage and knockback of a town NPC's attack before the damage is scaled. (More information on scaling in GlobalNPC.BuffTownNPCs.)
		/// <para /> Called on the server and clients.
		/// </summary>
		/// <param name="npc"></param>
		/// <param name="damage"></param>
		/// <param name="knockback"></param>
		// Token: 0x06001D58 RID: 7512 RVA: 0x004D41F9 File Offset: 0x004D23F9
		public virtual void TownNPCAttackStrength(NPC npc, ref int damage, ref float knockback)
		{
		}

		/// <summary>
		/// Allows you to determine the cooldown between each of a town NPC's attack. The cooldown will be a number greater than or equal to the first parameter, and less then the sum of the two parameters.
		/// <para /> Called on the server and clients.
		/// </summary>
		/// <param name="npc"></param>
		/// <param name="cooldown"></param>
		/// <param name="randExtraCooldown"></param>
		// Token: 0x06001D59 RID: 7513 RVA: 0x004D41FB File Offset: 0x004D23FB
		public virtual void TownNPCAttackCooldown(NPC npc, ref int cooldown, ref int randExtraCooldown)
		{
		}

		/// <summary>
		/// Allows you to determine the projectile type of a town NPC's attack, and how long it takes for the projectile to actually appear. This hook is only used when the town NPC has an attack type of 0 (throwing), 1 (shooting), or 2 (magic).
		/// <para /> Called on the server and clients.
		/// </summary>
		/// <param name="npc"></param>
		/// <param name="projType"></param>
		/// <param name="attackDelay"></param>
		// Token: 0x06001D5A RID: 7514 RVA: 0x004D41FD File Offset: 0x004D23FD
		public virtual void TownNPCAttackProj(NPC npc, ref int projType, ref int attackDelay)
		{
		}

		/// <summary>
		/// Allows you to determine the speed at which a town NPC throws a projectile when it attacks. Multiplier is the speed of the projectile, gravityCorrection is how much extra the projectile gets thrown upwards, and randomOffset allows you to randomize the projectile's velocity in a square centered around the original velocity. This hook is only used when the town NPC has an attack type of 0 (throwing), 1 (shooting), or 2 (magic).
		/// <para /> Called on the server and clients.
		/// </summary>
		/// <param name="npc"></param>
		/// <param name="multiplier"></param>
		/// <param name="gravityCorrection"></param>
		/// <param name="randomOffset"></param>
		// Token: 0x06001D5B RID: 7515 RVA: 0x004D41FF File Offset: 0x004D23FF
		public virtual void TownNPCAttackProjSpeed(NPC npc, ref float multiplier, ref float gravityCorrection, ref float randomOffset)
		{
		}

		/// <summary>
		/// Allows you to tell the game that a town NPC has already created a projectile and will still create more projectiles as part of a single attack so that the game can animate the NPC's attack properly. Only used when the town NPC has an attack type of 1 (shooting).
		/// <para /> Called on the server and clients.
		/// </summary>
		/// <param name="npc"></param>
		/// <param name="inBetweenShots"></param>
		// Token: 0x06001D5C RID: 7516 RVA: 0x004D4201 File Offset: 0x004D2401
		public virtual void TownNPCAttackShoot(NPC npc, ref bool inBetweenShots)
		{
		}

		/// <summary>
		/// Allows you to control the brightness of the light emitted by a town NPC's aura when it performs a magic attack. Only used when the town NPC has an attack type of 2 (magic)
		/// <para /> Called on the server and clients.
		/// </summary>
		/// <param name="npc"></param>
		/// <param name="auraLightMultiplier"></param>
		// Token: 0x06001D5D RID: 7517 RVA: 0x004D4203 File Offset: 0x004D2403
		public virtual void TownNPCAttackMagic(NPC npc, ref float auraLightMultiplier)
		{
		}

		/// <summary>
		/// Allows you to determine the width and height of the item a town NPC swings when it attacks, which controls the range of the NPC's swung weapon. Only used when the town NPC has an attack type of 3 (swinging).
		/// <para /> Called on the server and clients.
		/// </summary>
		/// <param name="npc"></param>
		/// <param name="itemWidth"></param>
		/// <param name="itemHeight"></param>
		// Token: 0x06001D5E RID: 7518 RVA: 0x004D4205 File Offset: 0x004D2405
		public virtual void TownNPCAttackSwing(NPC npc, ref int itemWidth, ref int itemHeight)
		{
		}

		/// <summary>
		/// Allows you to customize how a town NPC's weapon is drawn when the NPC is shooting (the NPC must have an attack type of 1). <paramref name="scale" /> is a multiplier for the item's drawing size, <paramref name="item" /> is the Texture2D instance of the item to be drawn, <paramref name="itemFrame" /> is the section of the texture to draw, and <paramref name="horizontalHoldoutOffset" /> is how far away the item should be drawn from the NPC.
		/// <para /> Called on the server and clients.
		/// </summary>
		/// <param name="npc"></param>
		/// <param name="item"></param>
		/// <param name="itemFrame"></param>
		/// <param name="scale"></param>
		/// <param name="horizontalHoldoutOffset"></param>
		// Token: 0x06001D5F RID: 7519 RVA: 0x004D4207 File Offset: 0x004D2407
		public virtual void DrawTownAttackGun(NPC npc, ref Texture2D item, ref Rectangle itemFrame, ref float scale, ref int horizontalHoldoutOffset)
		{
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.ModNPC.DrawTownAttackSwing(Microsoft.Xna.Framework.Graphics.Texture2D@,Microsoft.Xna.Framework.Rectangle@,System.Int32@,System.Single@,Microsoft.Xna.Framework.Vector2@)" />
		// Token: 0x06001D60 RID: 7520 RVA: 0x004D4209 File Offset: 0x004D2409
		public virtual void DrawTownAttackSwing(NPC npc, ref Texture2D item, ref Rectangle itemFrame, ref int itemSize, ref float scale, ref Vector2 offset)
		{
		}

		/// <summary>
		/// Allows you to modify the NPC's <seealso cref="T:Terraria.ID.ImmunityCooldownID" />, damage multiplier, and hitbox. Useful for implementing dynamic damage hitboxes that change in dimensions or deal extra damage. Returns false to prevent vanilla code from running. Returns true by default.
		/// <para /> Called on the server and clients.
		/// </summary>
		/// <param name="npc"></param>
		/// <param name="victimHitbox"></param>
		/// <param name="immunityCooldownSlot"></param>
		/// <param name="damageMultiplier"></param>
		/// <param name="npcHitbox"></param>
		/// <returns></returns>
		// Token: 0x06001D61 RID: 7521 RVA: 0x004D420B File Offset: 0x004D240B
		public virtual bool ModifyCollisionData(NPC npc, Rectangle victimHitbox, ref int immunityCooldownSlot, ref MultipliableFloat damageMultiplier, ref Rectangle npcHitbox)
		{
			return true;
		}

		/// <summary>
		/// Allows you to make a npc be saved even if it's not a townNPC and NPCID.Sets.SavesAndLoads[npc.type] is false.
		/// <br /><b>NOTE:</b> A town NPC will always be saved (except the Travelling Merchant that never will).
		/// <br /><b>NOTE:</b> A NPC that needs saving will not despawn naturally.
		/// </summary>
		/// <param name="npc"></param>
		/// <returns></returns>
		// Token: 0x06001D62 RID: 7522 RVA: 0x004D420E File Offset: 0x004D240E
		public virtual bool NeedSaving(NPC npc)
		{
			return false;
		}

		/// <summary>
		/// Allows you to save custom data for the given npc.
		/// <br />
		/// <br /><b>NOTE:</b> The provided tag is always empty by default, and is provided as an argument only for the sake of convenience and optimization.
		/// <br /><b>NOTE:</b> Try to only save data that isn't default values.
		/// <br /><b>NOTE:</b> The npc may be saved even if NeedSaving returns false and npc is not a townNPC, if another mod returns true on NeedSaving.
		/// </summary>
		/// <param name="npc"></param>
		/// <param name="tag">The TagCompound to save data into. Note that this is always empty by default, and is provided as an argument</param>
		// Token: 0x06001D63 RID: 7523 RVA: 0x004D4211 File Offset: 0x004D2411
		public virtual void SaveData(NPC npc, TagCompound tag)
		{
		}

		/// <summary>
		/// Allows you to load custom data that you have saved for the given npc.
		/// </summary>
		/// <param name="npc"></param>
		/// <param name="tag"></param>
		// Token: 0x06001D64 RID: 7524 RVA: 0x004D4213 File Offset: 0x004D2413
		public virtual void LoadData(NPC npc, TagCompound tag)
		{
		}

		/// <summary>
		/// Allows you to change the emote that the NPC will pick
		/// <para /> Called in single player or on the server only.
		/// </summary>
		/// <param name="npc"></param>
		/// <param name="closestPlayer">The <see cref="T:Terraria.Player" /> closest to the NPC. You can check the biome the player is in and let the NPC pick the emote that corresponds to the biome.</param>
		/// <param name="emoteList">A list of emote IDs from which the NPC will randomly select one</param>
		/// <param name="otherAnchor">A <see cref="T:Terraria.GameContent.UI.WorldUIAnchor" /> instance that indicates the target of this emote conversation. Use this to get the instance of the <see cref="T:Terraria.NPC" /> or <see cref="T:Terraria.Player" /> this NPC is talking to.</param>
		/// <returns>Return null to use vanilla mechanic (pick one from the list), otherwise pick the emote by the returned ID. Returning -1 will prevent the emote from being used. Returns null by default</returns>
		// Token: 0x06001D65 RID: 7525 RVA: 0x004D4218 File Offset: 0x004D2418
		public virtual int? PickEmote(NPC npc, Player closestPlayer, List<int> emoteList, WorldUIAnchor otherAnchor)
		{
			return null;
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.ModNPC.ChatBubblePosition(Microsoft.Xna.Framework.Vector2@,Microsoft.Xna.Framework.Graphics.SpriteEffects@)" />
		// Token: 0x06001D66 RID: 7526 RVA: 0x004D422E File Offset: 0x004D242E
		public virtual void ChatBubblePosition(NPC npc, ref Vector2 position, ref SpriteEffects spriteEffects)
		{
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.ModNPC.PartyHatPosition(Microsoft.Xna.Framework.Vector2@,Microsoft.Xna.Framework.Graphics.SpriteEffects@)" />
		// Token: 0x06001D67 RID: 7527 RVA: 0x004D4230 File Offset: 0x004D2430
		public virtual void PartyHatPosition(NPC npc, ref Vector2 position, ref SpriteEffects spriteEffects)
		{
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.ModNPC.EmoteBubblePosition(Microsoft.Xna.Framework.Vector2@,Microsoft.Xna.Framework.Graphics.SpriteEffects@)" />
		// Token: 0x06001D68 RID: 7528 RVA: 0x004D4232 File Offset: 0x004D2432
		public virtual void EmoteBubblePosition(NPC npc, ref Vector2 position, ref SpriteEffects spriteEffects)
		{
		}
	}
}
