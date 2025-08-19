using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.IO;

namespace Terraria.ModLoader
{
	/// <summary>
	/// A ModPlayer instance represents an extension of a Player instance.
	/// <br /> To use it, simply create a new class deriving from this one. Implementations will be registered automatically.
	/// <br /> You can store fields in the ModPlayer classes, much like how the Player class abuses field usage, to keep track of mod-specific information on the player that a ModPlayer instance represents. It also contains hooks to insert your code into the Player class.
	/// </summary>
	// Token: 0x020001C1 RID: 449
	public abstract class ModPlayer : ModType<Player, ModPlayer>, IIndexed
	{
		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x060022BD RID: 8893 RVA: 0x004E80A8 File Offset: 0x004E62A8
		// (set) Token: 0x060022BE RID: 8894 RVA: 0x004E80B0 File Offset: 0x004E62B0
		public ushort Index { get; internal set; }

		/// <summary>
		/// The Player instance that this ModPlayer instance is attached to.
		/// </summary>
		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x060022BF RID: 8895 RVA: 0x004E80B9 File Offset: 0x004E62B9
		public Player Player
		{
			get
			{
				return base.Entity;
			}
		}

		// Token: 0x060022C0 RID: 8896 RVA: 0x004E80C1 File Offset: 0x004E62C1
		protected override Player CreateTemplateEntity()
		{
			return null;
		}

		// Token: 0x060022C1 RID: 8897 RVA: 0x004E80C4 File Offset: 0x004E62C4
		public override ModPlayer NewInstance(Player entity)
		{
			ModPlayer modPlayer = base.NewInstance(entity);
			modPlayer.Index = this.Index;
			return modPlayer;
		}

		// Token: 0x060022C2 RID: 8898 RVA: 0x004E80D9 File Offset: 0x004E62D9
		public bool TypeEquals(ModPlayer other)
		{
			return base.Mod == other.Mod && this.Name == other.Name;
		}

		// Token: 0x060022C3 RID: 8899 RVA: 0x004E80FC File Offset: 0x004E62FC
		protected override void ValidateType()
		{
			base.ValidateType();
			LoaderUtils.MustOverrideTogether<ModPlayer>(this, new Expression<Func<ModPlayer, Delegate>>[]
			{
				(ModPlayer p) => (Action<TagCompound>)methodof(ModPlayer.SaveData(TagCompound)).CreateDelegate(typeof(Action<TagCompound>), p),
				(ModPlayer p) => (Action<TagCompound>)methodof(ModPlayer.LoadData(TagCompound)).CreateDelegate(typeof(Action<TagCompound>), p)
			});
			LoaderUtils.MustOverrideTogether<ModPlayer>(this, new Expression<Func<ModPlayer, Delegate>>[]
			{
				(ModPlayer p) => (Action<ModPlayer>)methodof(ModPlayer.CopyClientState(ModPlayer)).CreateDelegate(typeof(Action<ModPlayer>), p),
				(ModPlayer p) => (Action<ModPlayer>)methodof(ModPlayer.SendClientChanges(ModPlayer)).CreateDelegate(typeof(Action<ModPlayer>), p)
			});
		}

		// Token: 0x060022C4 RID: 8900 RVA: 0x004E835F File Offset: 0x004E655F
		protected sealed override void Register()
		{
			ModTypeLookup<ModPlayer>.Register(this);
			PlayerLoader.Add(this);
		}

		// Token: 0x060022C5 RID: 8901 RVA: 0x004E836D File Offset: 0x004E656D
		public sealed override void SetupContent()
		{
			this.SetStaticDefaults();
		}

		/// <summary>
		/// Called whenever the player is loaded (on the player selection screen). This can be used to initialize data structures, etc.
		/// </summary>
		// Token: 0x060022C6 RID: 8902 RVA: 0x004E8375 File Offset: 0x004E6575
		public virtual void Initialize()
		{
		}

		/// <summary>
		/// This is where you reset any fields you add to your ModPlayer subclass to their default states. This is necessary in order to reset your fields if they are conditionally set by a tick update but the condition is no longer satisfied.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		// Token: 0x060022C7 RID: 8903 RVA: 0x004E8377 File Offset: 0x004E6577
		public virtual void ResetEffects()
		{
		}

		/// <summary>
		/// This is where you reset any fields related to INFORMATION accessories to their "default" states. This is identical to ResetEffects(); but should ONLY be used to
		/// reset info accessories. It will cause unintended side-effects if used with other fields.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <remarks>
		/// This method is called in tandem with <seealso cref="M:Terraria.ModLoader.ModPlayer.ResetEffects" />, but it also called in <seealso cref="M:Terraria.Player.RefreshInfoAccs" /> even when the game is paused;
		/// this allows for info accessories to keep properly updating while the game is paused, a feature/fix added in 1.4.4.
		/// </remarks>
		// Token: 0x060022C8 RID: 8904 RVA: 0x004E8379 File Offset: 0x004E6579
		public virtual void ResetInfoAccessories()
		{
		}

		/// <summary>
		/// This is where you set any fields related to INFORMATION accessories based on the passed in player argument.
		/// <para /> Called on the local client.
		/// <para /> Note that this hook is only called if all of the requirements
		/// for a "nearby teammate" is met, which is when the other player is on the same team and within a certain distance, determined by the following code:
		/// <code>(Main.player[i].Center - base.Center).Length() &lt; 800f</code>
		/// </summary>
		// Token: 0x060022C9 RID: 8905 RVA: 0x004E837B File Offset: 0x004E657B
		public virtual void RefreshInfoAccessoriesFromTeamPlayers(Player otherPlayer)
		{
		}

		/// <summary>
		/// Allows you to modify the player's max stats.  This hook runs after vanilla increases from the Life Crystal, Life Fruit and Mana Crystal are applied
		/// <para /> Called on local, server, and remote clients.
		/// <para /> <b>NOTE:</b> You should NOT modify <see cref="F:Terraria.Player.statLifeMax" /> nor <see cref="F:Terraria.Player.statManaMax" /> here.  Use the <paramref name="health" /> and <paramref name="mana" /> parameters.
		/// <para /> Also note that unlike many other tModLoader hooks, the default implementation of this hook has code that will assign <paramref name="health" /> and <paramref name="mana" /> to <see cref="F:Terraria.ModLoader.StatModifier.Default" />. Take care to place <c>base.ModifyMaxStats(out health, out mana);</c> before any other code you add to this hook to avoid issues, if you use it.
		/// </summary>
		/// <param name="health">The modifier to the player's maximum health</param>
		/// <param name="mana">The modifier to the player's maximum mana</param>
		// Token: 0x060022CA RID: 8906 RVA: 0x004E837D File Offset: 0x004E657D
		public virtual void ModifyMaxStats(out StatModifier health, out StatModifier mana)
		{
			health = StatModifier.Default;
			mana = StatModifier.Default;
		}

		/// <summary>
		/// Similar to <see cref="M:Terraria.ModLoader.ModPlayer.ResetEffects" />, except this is only called when the player is dead. If this is called, then <see cref="M:Terraria.ModLoader.ModPlayer.ResetEffects" /> will not be called.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		// Token: 0x060022CB RID: 8907 RVA: 0x004E8395 File Offset: 0x004E6595
		public virtual void UpdateDead()
		{
		}

		/// <summary>
		/// Currently never gets called, so this is useless.
		/// </summary>
		// Token: 0x060022CC RID: 8908 RVA: 0x004E8397 File Offset: 0x004E6597
		public virtual void PreSaveCustomData()
		{
		}

		/// <summary>
		/// Allows you to save custom data for this player.
		/// <para /> <b>NOTE:</b> The provided tag is always empty by default, and is provided as an argument only for the sake of convenience and optimization.
		/// <para /> <b>NOTE:</b> Try to only save data that isn't default values.
		/// </summary>
		/// <param name="tag"> The TagCompound to save data into. Note that this is always empty by default, and is provided as an argument only for the sake of convenience and optimization. </param>
		// Token: 0x060022CD RID: 8909 RVA: 0x004E8399 File Offset: 0x004E6599
		public virtual void SaveData(TagCompound tag)
		{
		}

		/// <summary>
		/// Allows you to load custom data that you have saved for this player.
		/// <para /> <b>Try to write defensive loading code that won't crash if something's missing.</b>
		/// </summary>
		/// <param name="tag"> The TagCompound to load data from. </param>
		// Token: 0x060022CE RID: 8910 RVA: 0x004E839B File Offset: 0x004E659B
		public virtual void LoadData(TagCompound tag)
		{
		}

		/// <summary>
		/// PreSavePlayer and PostSavePlayer wrap the vanilla player saving code (both are before the ModPlayer.Save). Useful for advanced situations where a save might be corrupted or rendered unusable by the values that normally would save.
		/// </summary>
		// Token: 0x060022CF RID: 8911 RVA: 0x004E839D File Offset: 0x004E659D
		public virtual void PreSavePlayer()
		{
		}

		/// <summary>
		/// PreSavePlayer and PostSavePlayer wrap the vanilla player saving code (both are before the ModPlayer.Save). Useful for advanced situations where a save might be corrupted or rendered unusable by the values that normally would save.
		/// </summary>
		// Token: 0x060022D0 RID: 8912 RVA: 0x004E839F File Offset: 0x004E659F
		public virtual void PostSavePlayer()
		{
		}

		/// <summary>
		/// Allows you to copy information to the <paramref name="targetCopy" /> parameter that you intend to sync between this local client and both the server and other clients. 
		/// <br /><br /> You would then use the <see cref="M:Terraria.ModLoader.ModPlayer.SendClientChanges(Terraria.ModLoader.ModPlayer)" /> hook to compare against that data and decide what needs synchronizing, sending that data in a <see cref="T:Terraria.ModLoader.ModPacket" /> to the server. The server will then need to relay that information to the other remote clients. 
		/// <br /><br /> This hook is called with every call of the <see cref="M:Terraria.Player.clientClone" /> method.
		/// <br /><br /> See <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-Netcode#player--modplayer">the Player / ModPlayer section of the Basic Netcode wiki page</see> for more information.
		/// <br />
		/// <br /> <b>NOTE:</b> For performance reasons, avoid deep cloning or copying any excessive information.
		/// <br /> <b>NOTE:</b> Using <see cref="M:Terraria.Item.CopyNetStateTo(Terraria.Item)" /> is the recommended way of creating item snapshots.
		/// </summary>
		/// <param name="targetCopy"></param>
		// Token: 0x060022D1 RID: 8913 RVA: 0x004E83A1 File Offset: 0x004E65A1
		public virtual void CopyClientState(ModPlayer targetCopy)
		{
		}

		/// <summary>
		/// Allows you to sync information about this player between server and client. The toWho and fromWho parameters correspond to the remoteClient/toClient and ignoreClient arguments, respectively, of NetMessage.SendData/ModPacket.Send. They should be passed in as-is. The newPlayer parameter is whether or not the player is joining the server (it is true on the joining client).
		/// <br /><br /> This hook will be called on the local client to send the data to the server and also on the server to send the data to other clients.
		/// </summary>
		/// <param name="toWho"></param>
		/// <param name="fromWho"></param>
		/// <param name="newPlayer"></param>
		// Token: 0x060022D2 RID: 8914 RVA: 0x004E83A3 File Offset: 0x004E65A3
		public virtual void SyncPlayer(int toWho, int fromWho, bool newPlayer)
		{
		}

		/// <summary>
		/// Allows you to sync any information that has changed for this ModPlayer from this local client to the server. The server will need to take those changes and relay them to other remote clients.
		/// <br /><br /> Here, you should check the information you have copied in the clientClone parameter; if they differ between this ModPlayer and the clientPlayer parameter, then you should send that information using NetMessage.SendData or ModPacket.Send. All of the differences are the changes that occurred during the last game update for the local player.
		/// <br /><br /> See <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-Netcode#player--modplayer">the Player / ModPlayer section of the Basic Netcode wiki page</see> for more information.
		/// </summary>
		/// <param name="clientPlayer"></param>
		// Token: 0x060022D3 RID: 8915 RVA: 0x004E83A5 File Offset: 0x004E65A5
		public virtual void SendClientChanges(ModPlayer clientPlayer)
		{
		}

		/// <summary>
		/// Allows you to give the player a negative life regeneration based on its state (for example, the "On Fire!" debuff makes the player take damage-over-time). This is typically done by setting Player.lifeRegen to 0 if it is positive, setting Player.lifeRegenTime to 0, and subtracting a number from Player.lifeRegen. The player will take damage at a rate of half the number you subtract per second.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		// Token: 0x060022D4 RID: 8916 RVA: 0x004E83A7 File Offset: 0x004E65A7
		public virtual void UpdateBadLifeRegen()
		{
		}

		/// <summary>
		/// Allows you to increase the player's life regeneration based on its state. This can be done by incrementing Player.lifeRegen by a certain number. The player will recover life at a rate of half the number you add per second. You can also increment Player.lifeRegenTime to increase the speed at which the player reaches its maximum natural life regeneration.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		// Token: 0x060022D5 RID: 8917 RVA: 0x004E83A9 File Offset: 0x004E65A9
		public virtual void UpdateLifeRegen()
		{
		}

		/// <summary>
		/// Allows you to modify the power of the player's natural life regeneration. This can be done by multiplying the regen parameter by any number. For example, campfires multiply it by 1.1, while walking multiplies it by 0.5.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="regen"></param>
		// Token: 0x060022D6 RID: 8918 RVA: 0x004E83AB File Offset: 0x004E65AB
		public virtual void NaturalLifeRegen(ref float regen)
		{
		}

		/// <summary>
		/// Allows you to modify the player's stats while the game is paused due to the autopause setting being on.
		/// This is called in single player only, some time before the player's tick update would happen when the game isn't paused.
		/// </summary>
		// Token: 0x060022D7 RID: 8919 RVA: 0x004E83AD File Offset: 0x004E65AD
		public virtual void UpdateAutopause()
		{
		}

		/// <summary>
		/// This is called at the beginning of every tick update for this player, after checking whether the player exists. <br />
		/// This can be used to adjust timers and cooldowns.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		// Token: 0x060022D8 RID: 8920 RVA: 0x004E83AF File Offset: 0x004E65AF
		public virtual void PreUpdate()
		{
		}

		/// <summary>
		/// Use this to check on keybinds you have registered. While SetControls is set even while in text entry mode, this hook is only called during gameplay.
		/// <para /> Called on the local client only.
		/// <para /> Read <see href="https://github.com/tModLoader/tModLoader/blob/stable/ExampleMod/Common/Players/ExampleKeybindPlayer.cs">ExampleKeybindPlayer.cs</see> for examples and information on using this hook.
		/// </summary>
		/// <param name="triggersSet"></param>
		// Token: 0x060022D9 RID: 8921 RVA: 0x004E83B1 File Offset: 0x004E65B1
		public virtual void ProcessTriggers(TriggersSet triggersSet)
		{
		}

		/// <summary>
		/// This is called when the player activates their armor set bonus by double tapping down (or up if <see cref="F:Terraria.Main.ReversedUpDownArmorSetBonuses" /> is true). As an example, the Vortex armor uses this to toggle stealth mode.
		/// <para /> Use this to implement armor set bonuses that need to be activated by the player.
		/// <para /> Don't forget to check if your armor set is active.
		/// <para /> While this technically can be used for other effects, it will likely be frustrating for your players if non-armor set effects are being triggered in tandem with armor set bonus effects. Modders can use <see cref="F:Terraria.Player.holdDownCardinalTimer" /> and <see cref="F:Terraria.Player.doubleTapCardinalTimer" /> directly in other hooks for similar effects if needed.
		/// <para /> Called on the local client only.
		/// </summary>
		// Token: 0x060022DA RID: 8922 RVA: 0x004E83B3 File Offset: 0x004E65B3
		public virtual void ArmorSetBonusActivated()
		{
		}

		/// <summary>
		/// This is called when the player activates their armor set bonus by holding down (or up if <see cref="F:Terraria.Main.ReversedUpDownArmorSetBonuses" /> is true) for some amount of time. The <paramref name="holdTime" /> parameter indicates how many ticks the key has been held down for. As an example, the Stardust armor prior to 1.4.4 used to use this to set the location of the Stardust Guardian if <paramref name="holdTime" /> was greater than 60.
		/// <para /> Use this to implement armor set bonuses that need to be activated by the player.
		/// <para /> Don't forget to check if your armor set is active.
		/// <para /> While this technically can be used for other effects, it will likely be frustrating for your players if non-armor set effects are being triggered in tandem with armor set bonus effects. Modders can use <see cref="F:Terraria.Player.holdDownCardinalTimer" /> and <see cref="F:Terraria.Player.doubleTapCardinalTimer" /> directly in other hooks for similar effects if needed.
		/// <para /> Called on the local client only.
		/// </summary>
		// Token: 0x060022DB RID: 8923 RVA: 0x004E83B5 File Offset: 0x004E65B5
		public virtual void ArmorSetBonusHeld(int holdTime)
		{
		}

		/// <summary>
		/// Use this to modify the control inputs that the player receives. For example, the Confused debuff swaps the values of Player.controlLeft and Player.controlRight. This is called sometime after PreUpdate is called.
		/// <para /> Called on the local client only.
		/// </summary>
		// Token: 0x060022DC RID: 8924 RVA: 0x004E83B7 File Offset: 0x004E65B7
		public virtual void SetControls()
		{
		}

		/// <summary>
		/// This is called sometime after SetControls is called, and right before all the buffs update on this player. This hook can be used to add buffs to the player based on the player's state (for example, the Campfire buff is added if the player is near a Campfire).
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		// Token: 0x060022DD RID: 8925 RVA: 0x004E83B9 File Offset: 0x004E65B9
		public virtual void PreUpdateBuffs()
		{
		}

		/// <summary>
		/// This is called right after all of this player's buffs update on the player. This can be used to modify the effects that the buff updates had on this player, and can also be used for general update tasks.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		// Token: 0x060022DE RID: 8926 RVA: 0x004E83BB File Offset: 0x004E65BB
		public virtual void PostUpdateBuffs()
		{
		}

		/// <summary>
		/// Called after Update Accessories.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		// Token: 0x060022DF RID: 8927 RVA: 0x004E83BD File Offset: 0x004E65BD
		public virtual void UpdateEquips()
		{
		}

		/// <summary>
		/// This is called right after all of this player's equipment and armor sets update on the player, which is sometime after PostUpdateBuffs is called. This can be used to modify the effects that the equipment had on this player, and can also be used for general update tasks.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		// Token: 0x060022E0 RID: 8928 RVA: 0x004E83BF File Offset: 0x004E65BF
		public virtual void PostUpdateEquips()
		{
		}

		/// <summary>
		/// Is called in Player.Frame() after vanilla functional slots are evaluated, including selection screen to prepare and denote visible accessories. Player Instance sensitive.
		/// </summary>
		// Token: 0x060022E1 RID: 8929 RVA: 0x004E83C1 File Offset: 0x004E65C1
		public virtual void UpdateVisibleAccessories()
		{
		}

		/// <summary>
		/// Is called in Player.Frame() after vanilla vanity slots are evaluated, including selection screen to prepare and denote visible accessories. Player Instance sensitive.
		/// </summary>
		// Token: 0x060022E2 RID: 8930 RVA: 0x004E83C3 File Offset: 0x004E65C3
		public virtual void UpdateVisibleVanityAccessories()
		{
		}

		/// <summary>
		/// Is called in Player.UpdateDyes(), including selection screen. Player Instance sensitive.
		/// </summary>
		// Token: 0x060022E3 RID: 8931 RVA: 0x004E83C5 File Offset: 0x004E65C5
		public virtual void UpdateDyes()
		{
		}

		/// <summary>
		/// This is called after miscellaneous update code is called in Player.Update, which is sometime after PostUpdateEquips is called. This can be used for general update tasks.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		// Token: 0x060022E4 RID: 8932 RVA: 0x004E83C7 File Offset: 0x004E65C7
		public virtual void PostUpdateMiscEffects()
		{
		}

		/// <summary>
		/// This is called after the player's horizontal speeds are modified, which is sometime after PostUpdateMiscEffects is called, and right before the player's horizontal position is updated. Use this to modify maxRunSpeed, accRunSpeed, runAcceleration, and similar variables before the player moves forwards/backwards.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		// Token: 0x060022E5 RID: 8933 RVA: 0x004E83C9 File Offset: 0x004E65C9
		public virtual void PostUpdateRunSpeeds()
		{
		}

		/// <summary>
		/// This is called right before modifying the player's position based on velocity. Use this to make direct changes to the velocity.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		// Token: 0x060022E6 RID: 8934 RVA: 0x004E83CB File Offset: 0x004E65CB
		public virtual void PreUpdateMovement()
		{
		}

		/// <summary>
		/// This is called at the very end of the Player.Update method. Final general update tasks can be placed here.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		// Token: 0x060022E7 RID: 8935 RVA: 0x004E83CD File Offset: 0x004E65CD
		public virtual void PostUpdate()
		{
		}

		/// <summary>
		/// Use this hook to modify the jump duration from an extra jump.
		/// <para /> Called on local, server, and remote clients.
		/// <para /> Vanilla's extra jumps use the following values:
		/// <para>
		/// Basilisk mount: 0.75<br />
		/// Blizzard in a Bottle: 1.5<br />
		/// Cloud in a Bottle: 0.75<br />
		/// Fart in a Jar: 2<br />
		/// Goat mount: 2<br />
		/// Sandstorm in a Bottle: 3<br />
		/// Santank mount: 2<br />
		/// Tsunami in a Bottle: 1.25<br />
		/// Unicorn mount: 2
		/// </para>
		/// </summary>
		/// <param name="jump">The jump being performed</param>
		/// <param name="duration">A modifier to the player's jump height, which when combined effectively acts as the duration for the extra jump</param>
		// Token: 0x060022E8 RID: 8936 RVA: 0x004E83CF File Offset: 0x004E65CF
		public virtual void ModifyExtraJumpDurationMultiplier(ExtraJump jump, ref float duration)
		{
		}

		/// <summary>
		/// An extra condition for whether an extra jump can be started.  Returns <see langword="true" /> by default.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="jump">The jump that would be performed</param>
		/// <returns><see langword="true" /> to let the jump be started, <see langword="false" /> otherwise.</returns>
		// Token: 0x060022E9 RID: 8937 RVA: 0x004E83D1 File Offset: 0x004E65D1
		public virtual bool CanStartExtraJump(ExtraJump jump)
		{
			return true;
		}

		/// <summary>
		/// Effects that should appear when the extra jump starts should happen here.
		/// <para /> For example, the Cloud in a Bottle's initial puff of smoke is spawned here.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="jump">The jump being performed</param>
		/// <param name="playSound">Whether the poof sound should play.  Set this parameter to <see langword="false" /> if you want to play a different sound.</param>
		// Token: 0x060022EA RID: 8938 RVA: 0x004E83D4 File Offset: 0x004E65D4
		public virtual void OnExtraJumpStarted(ExtraJump jump, ref bool playSound)
		{
		}

		/// <summary>
		/// This hook runs before the <see cref="P:Terraria.DataStructures.ExtraJumpState.Active" /> flag for an extra jump is set from <see langword="true" /> to <see langword="false" /> when the extra jump's duration has expired
		/// <para /> This occurs when a grappling hook is thrown, the player grabs onto a rope, the jump's duration has finished and when the player's frozen, turned to stone or webbed.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="jump">The jump that was performed</param>
		// Token: 0x060022EB RID: 8939 RVA: 0x004E83D6 File Offset: 0x004E65D6
		public virtual void OnExtraJumpEnded(ExtraJump jump)
		{
		}

		/// <summary>
		/// This hook runs before the <see cref="P:Terraria.DataStructures.ExtraJumpState.Available" /> flag for an extra jump is set to <see langword="true" /> in <see cref="M:Terraria.Player.RefreshDoubleJumps" />
		/// <para /> This occurs at the start of the grounded jump and while the player is grounded.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="jump">The jump instance</param>
		// Token: 0x060022EC RID: 8940 RVA: 0x004E83D8 File Offset: 0x004E65D8
		public virtual void OnExtraJumpRefreshed(ExtraJump jump)
		{
		}

		/// <summary>
		/// Effects that should appear while the player is performing an extra jump should happen here.
		/// <para /> For example, the Sandstorm in a Bottle's dusts are spawned here.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		// Token: 0x060022ED RID: 8941 RVA: 0x004E83DA File Offset: 0x004E65DA
		public virtual void ExtraJumpVisuals(ExtraJump jump)
		{
		}

		/// <summary>
		/// Return <see langword="false" /> to prevent <see cref="M:Terraria.ModLoader.ExtraJump.ShowVisuals(Terraria.Player)" /> from executing on <paramref name="jump" />.
		/// <para /> By default, this hook returns whether the player is moving upwards with respect to <see cref="F:Terraria.Player.gravDir" />
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="jump">The jump instance</param>
		// Token: 0x060022EE RID: 8942 RVA: 0x004E83DC File Offset: 0x004E65DC
		public virtual bool CanShowExtraJumpVisuals(ExtraJump jump)
		{
			return true;
		}

		/// <summary>
		/// This hook runs before the <see cref="P:Terraria.DataStructures.ExtraJumpState.Available" /> flag for an extra jump is set to <see langword="false" />  in <see cref="M:Terraria.Player.Update(System.Int32)" /> due to the jump being unavailable or when calling <see cref="M:Terraria.Player.ConsumeAllExtraJumps" /> (vanilla calls it when a mount that blocks jumps is active)
		/// </summary>
		/// <param name="jump">The jump instance</param>
		// Token: 0x060022EF RID: 8943 RVA: 0x004E83DF File Offset: 0x004E65DF
		public virtual void OnExtraJumpCleared(ExtraJump jump)
		{
		}

		/// <summary>
		/// Allows you to modify the armor and accessories that visually appear on the player. In addition, you can create special effects around this character, such as creating dust.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		// Token: 0x060022F0 RID: 8944 RVA: 0x004E83E1 File Offset: 0x004E65E1
		public virtual void FrameEffects()
		{
		}

		/// <summary>
		/// Allows you to make a player immune to damage from a certain source, or at a certain time.
		/// Vanilla examples include shimmer and journey god mode. Runs before dodges are used, or any damage calculations are performed.
		/// <para /> If immunity is determined on the local player, the hit will not be sent across the network.
		/// <para /> In pvp the hit will be sent regardless, and all clients will determine immunity independently, though it only really matters for the receiving player.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="damageSource">The source of the damage (projectile, NPC, etc)</param>
		/// <param name="cooldownCounter">The <see cref="T:Terraria.ID.ImmunityCooldownID" /> of the hit</param>
		/// <param name="dodgeable">Whether the hit is dodgeable</param>
		/// <returns>True to completely ignore the hit</returns>
		// Token: 0x060022F1 RID: 8945 RVA: 0x004E83E3 File Offset: 0x004E65E3
		public virtual bool ImmuneTo(PlayerDeathReason damageSource, int cooldownCounter, bool dodgeable)
		{
			return false;
		}

		/// <summary>
		/// Allows you to dodge damage for a player. Intended for guaranteed 'free' or random dodges. Vanilla example is black belt.
		/// <para /> For dodges which consume a stack/buff or have a cooldown, use <see cref="M:Terraria.ModLoader.ModPlayer.ConsumableDodge(Terraria.Player.HurtInfo)" /> instead.
		/// <para /> Called on the local client receiving damage.
		/// <para /> If dodge is determined on the local player, the hit will not be sent across the network. If visual indication of the dodge is required on remote clients, you will need to send your own packet.
		/// </summary>
		/// <returns>True to completely ignore the hit</returns>
		// Token: 0x060022F2 RID: 8946 RVA: 0x004E83E6 File Offset: 0x004E65E6
		public virtual bool FreeDodge(Player.HurtInfo info)
		{
			return false;
		}

		/// <summary>
		/// Allows you to dodge damage for a player. Vanilla examples include hallowed armor shadow dodge, and brain of confusion.
		/// <para /> For dodges which are 'free' and should be used before triggering consumables, use <see cref="M:Terraria.ModLoader.ModPlayer.FreeDodge(Terraria.Player.HurtInfo)" /> instead.
		/// <para /> Called on the local client receiving damage.
		/// <para /> If dodge is determined on the local player, the hit will not be sent across the network.
		/// <para /> You may need to send your own packet to synchronize the consumption of the effect, or application of the cooldown in multiplayer.
		/// </summary>
		/// <returns>True to completely ignore the hit</returns>
		// Token: 0x060022F3 RID: 8947 RVA: 0x004E83E9 File Offset: 0x004E65E9
		public virtual bool ConsumableDodge(Player.HurtInfo info)
		{
			return false;
		}

		/// <summary>
		/// Allows you to adjust an instance of player taking damage.
		/// <para /> Called on the local client taking damage.
		/// <para /> Only use this hook if you need to modify the hurt parameters in some way, eg consuming a buff which reduces the damage of the next hit. Use <see cref="M:Terraria.ModLoader.ModPlayer.OnHurt(Terraria.Player.HurtInfo)" /> or <see cref="M:Terraria.ModLoader.ModPlayer.PostHurt(Terraria.Player.HurtInfo)" /> instead where possible.
		/// <para /> The player will always take at least 1 damage. To prevent damage use <see cref="M:Terraria.ModLoader.ModPlayer.ImmuneTo(Terraria.DataStructures.PlayerDeathReason,System.Int32,System.Boolean)" /> or <see cref="M:Terraria.ModLoader.ModPlayer.FreeDodge(Terraria.Player.HurtInfo)" /> <br />
		/// </summary>
		// Token: 0x060022F4 RID: 8948 RVA: 0x004E83EC File Offset: 0x004E65EC
		public virtual void ModifyHurt(ref Player.HurtModifiers modifiers)
		{
		}

		/// <summary>
		/// Allows you to make anything happen when the player takes damage.
		/// <para /> Called on the local client taking damage.
		/// <para /> Called right before health is reduced.
		/// </summary>
		// Token: 0x060022F5 RID: 8949 RVA: 0x004E83EE File Offset: 0x004E65EE
		public virtual void OnHurt(Player.HurtInfo info)
		{
		}

		/// <summary>
		/// Allows you to make anything happen when the player takes damage.
		/// <para /> Called on the local client taking damage
		/// <para /> Only called if the player survives the hit.
		/// </summary>
		// Token: 0x060022F6 RID: 8950 RVA: 0x004E83F0 File Offset: 0x004E65F0
		public virtual void PostHurt(Player.HurtInfo info)
		{
		}

		/// <summary>
		/// This hook is called whenever the player is about to be killed after reaching 0 health.
		/// <para /> Called on local, server, and remote clients.
		/// <para /> Set the <paramref name="playSound" /> parameter to false to stop the death sound from playing. Set the <paramref name="genDust" /> parameter to false to stop the dust from being created. These are useful for creating your own sound or dust to replace the normal death effects, such as how the Frost armor set spawns <see cref="F:Terraria.ID.DustID.IceTorch" /> instead of <see cref="F:Terraria.ID.DustID.Blood" />. For mod compatibility, it is recommended to check if these values are true before setting them to true and spawning dust or playing sounds to avoid overlapping sounds and dust effects.
		/// <para /> Return false to stop the player from being killed. Only return false if you know what you are doing! Returns true by default.
		/// </summary>
		/// <param name="damage"></param>
		/// <param name="hitDirection"></param>
		/// <param name="pvp"></param>
		/// <param name="playSound"></param>
		/// <param name="genDust"></param>
		/// <param name="damageSource"></param>
		/// <returns></returns>
		// Token: 0x060022F7 RID: 8951 RVA: 0x004E83F2 File Offset: 0x004E65F2
		public virtual bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genDust, ref PlayerDeathReason damageSource)
		{
			return true;
		}

		/// <summary>
		/// Allows you to make anything happen when the player dies.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="damage"></param>
		/// <param name="hitDirection"></param>
		/// <param name="pvp"></param>
		/// <param name="damageSource"></param>
		// Token: 0x060022F8 RID: 8952 RVA: 0x004E83F5 File Offset: 0x004E65F5
		public virtual void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
		{
		}

		/// <summary>
		/// Called before vanilla makes any luck calculations. Return false to prevent vanilla from making their luck calculations. Returns true by default.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="luck"></param>
		// Token: 0x060022F9 RID: 8953 RVA: 0x004E83F7 File Offset: 0x004E65F7
		public virtual bool PreModifyLuck(ref float luck)
		{
			return true;
		}

		/// <summary>
		/// Allows you to modify a player's luck amount.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="luck"></param>
		// Token: 0x060022FA RID: 8954 RVA: 0x004E83FA File Offset: 0x004E65FA
		public virtual void ModifyLuck(ref float luck)
		{
		}

		/// <summary>
		/// Allows you to do anything before the update code for the player's held item is run. Return false to stop the held item update code from being run (for example, if the player is frozen). Returns true by default.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <returns></returns>
		// Token: 0x060022FB RID: 8955 RVA: 0x004E83FC File Offset: 0x004E65FC
		public virtual bool PreItemCheck()
		{
			return true;
		}

		/// <summary>
		/// Allows you to do anything after the update code for the player's held item is run. Hooks for the middle of the held item update code have more specific names in ModItem and ModPlayer.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		// Token: 0x060022FC RID: 8956 RVA: 0x004E83FF File Offset: 0x004E65FF
		public virtual void PostItemCheck()
		{
		}

		/// <summary>
		/// Allows you to change the effective useTime of an item.
		/// <para /> Note that this hook may cause items' actions to run less or more times than they should per a single use.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <returns> The multiplier on the usage time. 1f by default. Values greater than 1 increase the item use's length. </returns>
		// Token: 0x060022FD RID: 8957 RVA: 0x004E8401 File Offset: 0x004E6601
		public virtual float UseTimeMultiplier(Item item)
		{
			return 1f;
		}

		/// <summary>
		/// Allows you to change the effective useAnimation of an item.
		/// <para /> Note that this hook may cause items' actions to run less or more times than they should per a single use.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <returns>The multiplier on the animation time. 1f by default. Values greater than 1 increase the item animation's length. </returns>
		// Token: 0x060022FE RID: 8958 RVA: 0x004E8408 File Offset: 0x004E6608
		public virtual float UseAnimationMultiplier(Item item)
		{
			return 1f;
		}

		/// <summary>
		/// Allows you to safely change both useTime and useAnimation while keeping the values relative to each other.
		/// <para /> Useful for status effects.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <returns> The multiplier on the use speed. 1f by default. Values greater than 1 increase the overall item speed. </returns>
		// Token: 0x060022FF RID: 8959 RVA: 0x004E840F File Offset: 0x004E660F
		public virtual float UseSpeedMultiplier(Item item)
		{
			return 1f;
		}

		/// <summary>
		/// Allows you to temporarily modify the amount of life a life healing item will heal for, based on player buffs, accessories, etc. This is only called for items with a <see cref="F:Terraria.Item.healLife" /> value.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="quickHeal">Whether the item is being used through quick heal or not.</param>
		/// <param name="healValue">The amount of life being healed.</param>
		// Token: 0x06002300 RID: 8960 RVA: 0x004E8416 File Offset: 0x004E6616
		public virtual void GetHealLife(Item item, bool quickHeal, ref int healValue)
		{
		}

		/// <summary>
		/// Allows you to temporarily modify the amount of mana a mana healing item will heal for, based on player buffs, accessories, etc. This is only called for items with a <see cref="F:Terraria.Item.healMana" /> value.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="quickHeal">Whether the item is being used through quick heal or not.</param>
		/// <param name="healValue">The amount of mana being healed.</param>
		// Token: 0x06002301 RID: 8961 RVA: 0x004E8418 File Offset: 0x004E6618
		public virtual void GetHealMana(Item item, bool quickHeal, ref int healValue)
		{
		}

		/// <summary>
		/// Allows you to temporarily modify the amount of mana an item will consume on use, based on player buffs, accessories, etc. This is only called for items with a mana value.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="item">The item being used.</param>
		/// <param name="reduce">Used for decreasingly stacking buffs (most common). Only ever use -= on this field.</param>
		/// <param name="mult">Use to directly multiply the item's effective mana cost. Good for debuffs, or things which should stack separately (eg meteor armor set bonus).</param>
		// Token: 0x06002302 RID: 8962 RVA: 0x004E841A File Offset: 0x004E661A
		public virtual void ModifyManaCost(Item item, ref float reduce, ref float mult)
		{
		}

		/// <summary>
		/// Allows you to make stuff happen when a player doesn't have enough mana for the item they are trying to use.
		/// If the player has high enough mana after this hook runs, mana consumption will happen normally.
		/// Only runs once per item use.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="item">The item being used.</param>
		/// <param name="neededMana">The mana needed to use the item.</param>
		// Token: 0x06002303 RID: 8963 RVA: 0x004E841C File Offset: 0x004E661C
		public virtual void OnMissingMana(Item item, int neededMana)
		{
		}

		/// <summary>
		/// Allows you to make stuff happen when a player consumes mana on use of an item.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="item">The item being used.</param>
		/// <param name="manaConsumed">The mana consumed from the player.</param>
		// Token: 0x06002304 RID: 8964 RVA: 0x004E841E File Offset: 0x004E661E
		public virtual void OnConsumeMana(Item item, int manaConsumed)
		{
		}

		/// <summary>
		/// Allows you to dynamically modify a weapon's damage based on player and item conditions.
		/// Can be utilized to modify damage beyond the tools that DamageClass has to offer.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="item">The item being used.</param>
		/// <param name="damage">The StatModifier object representing the totality of the various modifiers to be applied to the item's base damage.</param>
		// Token: 0x06002305 RID: 8965 RVA: 0x004E8420 File Offset: 0x004E6620
		public virtual void ModifyWeaponDamage(Item item, ref StatModifier damage)
		{
		}

		/// <summary>
		/// Allows you to dynamically modify a weapon's knockback based on player and item conditions.
		/// Can be utilized to modify damage beyond the tools that DamageClass has to offer.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="item">The item being used.</param>
		/// <param name="knockback">The StatModifier object representing the totality of the various modifiers to be applied to the item's base knockback.</param>
		// Token: 0x06002306 RID: 8966 RVA: 0x004E8422 File Offset: 0x004E6622
		public virtual void ModifyWeaponKnockback(Item item, ref StatModifier knockback)
		{
		}

		/// <summary>
		/// Allows you to dynamically modify a weapon's crit chance based on player and item conditions.
		/// Can be utilized to modify damage beyond the tools that DamageClass has to offer.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="crit">The total crit chance of the item after all normal crit chance calculations.</param>
		// Token: 0x06002307 RID: 8967 RVA: 0x004E8424 File Offset: 0x004E6624
		public virtual void ModifyWeaponCrit(Item item, ref float crit)
		{
		}

		/// <summary>
		/// Whether or not the given ammo item will be consumed by this weapon.<br></br>
		/// By default, returns true; return false to prevent ammo consumption. <br></br>
		/// If false is returned, the <see cref="M:Terraria.ModLoader.ModPlayer.OnConsumeAmmo(Terraria.Item,Terraria.Item)" /> hook is never called.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="weapon">The weapon that this player is attempting to use.</param>
		/// <param name="ammo">The ammo that the given weapon is attempting to consume.</param>
		/// <returns></returns>
		// Token: 0x06002308 RID: 8968 RVA: 0x004E8426 File Offset: 0x004E6626
		public virtual bool CanConsumeAmmo(Item weapon, Item ammo)
		{
			return true;
		}

		/// <summary>
		/// Allows you to make things happen when the given ammo is consumed by the given weapon.<br></br>
		/// Called before the ammo stack is reduced, and is never called if the ammo isn't consumed in the first place.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="weapon">The weapon that is currently using the given ammo.</param>
		/// <param name="ammo">The ammo that the given weapon is currently using.</param>
		// Token: 0x06002309 RID: 8969 RVA: 0x004E8429 File Offset: 0x004E6629
		public virtual void OnConsumeAmmo(Item weapon, Item ammo)
		{
		}

		/// <summary>
		/// Allows you to prevent an item from shooting a projectile on use. Returns true by default.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="item"> The item being used. </param>
		/// <returns></returns>
		// Token: 0x0600230A RID: 8970 RVA: 0x004E842B File Offset: 0x004E662B
		public virtual bool CanShoot(Item item)
		{
			return true;
		}

		/// <summary>
		/// Allows you to modify the position, velocity, type, damage and/or knockback of a projectile being shot by an item.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="item"> The item being used. </param>
		/// <param name="position"> The center position of the projectile. </param>
		/// <param name="velocity"> The velocity of the projectile. </param>
		/// <param name="type"> The ID of the projectile. </param>
		/// <param name="damage"> The damage of the projectile. </param>
		/// <param name="knockback"> The knockback of the projectile. </param>
		// Token: 0x0600230B RID: 8971 RVA: 0x004E842E File Offset: 0x004E662E
		public virtual void ModifyShootStats(Item item, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
		}

		/// <summary>
		/// Allows you to modify an item's shooting mechanism. Return false to prevent vanilla's shooting code from running. Returns true by default.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="item"> The item being used. </param>
		/// <param name="source"> The projectile source's information. </param>
		/// <param name="position"> The center position of the projectile. </param>
		/// <param name="velocity"> The velocity of the projectile. </param>
		/// <param name="type"> The ID of the projectile. </param>
		/// <param name="damage"> The damage of the projectile. </param>
		/// <param name="knockback"> The knockback of the projectile. </param>
		// Token: 0x0600230C RID: 8972 RVA: 0x004E8430 File Offset: 0x004E6630
		public virtual bool Shoot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			return true;
		}

		/// <summary>
		/// Allows you to give this player's melee weapon special effects, such as creating light or dust. This is typically used to implement a weapon enchantment, similar to flasks, frost armor, or magma stone effects.
		/// <para /> If implementing a weapon enchantment, also implement <see cref="M:Terraria.ModLoader.ModPlayer.EmitEnchantmentVisualsAt(Terraria.Projectile,Microsoft.Xna.Framework.Vector2,System.Int32,System.Int32)" /> to support enchantment visuals for projectiles as well.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="item"></param>
		/// <param name="hitbox"></param>
		// Token: 0x0600230D RID: 8973 RVA: 0x004E8433 File Offset: 0x004E6633
		public virtual void MeleeEffects(Item item, Rectangle hitbox)
		{
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.ModProjectile.EmitEnchantmentVisualsAt(Microsoft.Xna.Framework.Vector2,System.Int32,System.Int32)" />
		// Token: 0x0600230E RID: 8974 RVA: 0x004E8435 File Offset: 0x004E6635
		public virtual void EmitEnchantmentVisualsAt(Projectile projectile, Vector2 boxPosition, int boxWidth, int boxHeight)
		{
		}

		/// <summary>
		/// Allows you to determine whether the given item can catch the given NPC.<br></br>
		/// Return true or false to say the target can or cannot be caught, respectively, regardless of vanilla rules.<br></br>
		/// Returns null by default, which allows vanilla's NPC catching rules to decide the target's fate.<br></br>
		/// If this returns false, <see cref="M:Terraria.ModLoader.CombinedHooks.OnCatchNPC(Terraria.Player,Terraria.NPC,Terraria.Item,System.Boolean)" /> is never called.<br></br>
		/// <para /> Called on the local client only.
		/// <para /> NOTE: this does not classify the given item as a catch tool, which is necessary for catching NPCs in the first place.
		/// To do that, you will need to use the "CatchingTool" set in ItemID.Sets.
		/// </summary>
		/// <param name="target">The NPC the player is trying to catch.</param>
		/// <param name="item">The item with which the player is trying to catch the target NPC.</param>
		// Token: 0x0600230F RID: 8975 RVA: 0x004E8438 File Offset: 0x004E6638
		public virtual bool? CanCatchNPC(NPC target, Item item)
		{
			return null;
		}

		/// <summary>
		/// Allows you to make things happen when the given item attempts to catch the given NPC.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="npc">The NPC which the player attempted to catch.</param>
		/// <param name="item">The item used to catch the given NPC.</param>
		/// <param name="failed">Whether or not the given NPC has been successfully caught.</param>
		// Token: 0x06002310 RID: 8976 RVA: 0x004E844E File Offset: 0x004E664E
		public virtual void OnCatchNPC(NPC npc, Item item, bool failed)
		{
		}

		/// <summary>
		/// Allows you to dynamically modify the given item's size for this player, similarly to the effect of the Titan Glove.
		/// <para /> Called on local and remote clients
		/// </summary>
		/// <param name="item">The item to modify the scale of.</param>
		/// <param name="scale">
		/// The scale multiplier to be applied to the given item.<br></br>
		/// Will be 1.1 if the Titan Glove is equipped, and 1 otherwise.
		/// </param>
		// Token: 0x06002311 RID: 8977 RVA: 0x004E8450 File Offset: 0x004E6650
		public virtual void ModifyItemScale(Item item, ref float scale)
		{
		}

		/// <summary>
		/// This hook is called when a player damages anything, whether it be an NPC or another player, using anything, whether it be a melee weapon or a projectile. The x and y parameters are the coordinates of the victim parameter's center.
		/// <para /> Called on the client hitting the target.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="victim"></param>
		// Token: 0x06002312 RID: 8978 RVA: 0x004E8452 File Offset: 0x004E6652
		public virtual void OnHitAnything(float x, float y, Entity victim)
		{
		}

		/// <summary>
		/// Allows you to determine whether a player can hit the given NPC. Returns true by default.
		/// <para /> Called on the client hitting the target.
		/// </summary>
		/// <param name="target"></param>
		/// <returns>True by default</returns>
		// Token: 0x06002313 RID: 8979 RVA: 0x004E8454 File Offset: 0x004E6654
		public virtual bool CanHitNPC(NPC target)
		{
			return true;
		}

		/// <summary>
		/// Allows you to determine whether a player melee attack can collide the given NPC by swinging a melee weapon.
		/// Use <see cref="M:Terraria.ModLoader.ModPlayer.CanHitNPCWithItem(Terraria.Item,Terraria.NPC)" /> instead for Guide Voodoo Doll-type effects.
		/// <para /> Called on the client hitting the target.
		/// </summary>
		/// <param name="item">The weapon item the player is holding.</param>
		/// <param name="meleeAttackHitbox">Hitbox of melee attack.</param>
		/// <param name="target">The target npc.</param>
		/// <returns>
		/// Return true to allow colliding the target, return false to block the player weapon from colliding the target, and return null to use the vanilla code for whether the target can be colliding by melee weapon. Returns null by default.
		/// </returns>
		// Token: 0x06002314 RID: 8980 RVA: 0x004E8458 File Offset: 0x004E6658
		public virtual bool? CanMeleeAttackCollideWithNPC(Item item, Rectangle meleeAttackHitbox, NPC target)
		{
			return null;
		}

		/// <summary>
		/// Allows you to modify the damage, knockback, etc that this player does to an NPC.
		/// <para /> Called on the client hitting the target.
		/// </summary>
		/// <param name="target"></param>
		/// <param name="modifiers"></param>
		// Token: 0x06002315 RID: 8981 RVA: 0x004E846E File Offset: 0x004E666E
		public virtual void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
		}

		/// <summary>
		/// Allows you to create special effects when this player hits an NPC.
		/// <para /> Called on the client hitting the target.
		/// </summary>
		/// <param name="target"></param>
		/// <param name="hit"></param>
		/// <param name="damageDone"></param>
		// Token: 0x06002316 RID: 8982 RVA: 0x004E8470 File Offset: 0x004E6670
		public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
		}

		/// <summary>
		/// Allows you to determine whether a player can hit the given NPC by swinging a melee weapon. Return true to allow hitting the target, return false to block this player from hitting the target, and return null to use the vanilla code for whether the target can be hit. Returns null by default.
		/// <para /> Called on the client hitting the target.
		/// </summary>
		/// <param name="item"></param>
		/// <param name="target"></param>
		/// <returns></returns>
		// Token: 0x06002317 RID: 8983 RVA: 0x004E8474 File Offset: 0x004E6674
		public virtual bool? CanHitNPCWithItem(Item item, NPC target)
		{
			return null;
		}

		/// <summary>
		/// Allows you to modify the damage, knockback, etc., that this player does to an NPC by swinging a melee weapon.
		/// <para /> Called on the client hitting the target.
		/// </summary>
		/// <param name="item"></param>
		/// <param name="target"></param>
		/// <param name="modifiers"></param>
		// Token: 0x06002318 RID: 8984 RVA: 0x004E848A File Offset: 0x004E668A
		public virtual void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)
		{
		}

		/// <summary>
		/// Allows you to create special effects when this player hits an NPC by swinging a melee weapon (for example how the Pumpkin Sword creates pumpkin heads).
		/// <para /> Called on the client hitting the target.
		/// </summary>
		/// <param name="item"></param>
		/// <param name="target"></param>
		/// <param name="hit"></param>
		/// <param name="damageDone"></param>
		// Token: 0x06002319 RID: 8985 RVA: 0x004E848C File Offset: 0x004E668C
		public virtual void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
		{
		}

		/// <summary>
		/// Allows you to determine whether a projectile created by this player can hit the given NPC. Return true to allow hitting the target, return false to block this projectile from hitting the target, and return null to use the vanilla code for whether the target can be hit. Returns null by default.
		/// <para /> Called on the client hitting the target.
		/// </summary>
		/// <param name="proj"></param>
		/// <param name="target"></param>
		/// <returns></returns>
		// Token: 0x0600231A RID: 8986 RVA: 0x004E8490 File Offset: 0x004E6690
		public virtual bool? CanHitNPCWithProj(Projectile proj, NPC target)
		{
			return null;
		}

		/// <summary>
		/// Allows you to modify the damage, knockback, etc., that a projectile created by this player does to an NPC.
		/// <para /> Called on the client hitting the target.
		/// </summary>
		/// <param name="proj"></param>
		/// <param name="target"></param>
		/// <param name="modifiers"></param>
		// Token: 0x0600231B RID: 8987 RVA: 0x004E84A6 File Offset: 0x004E66A6
		public virtual void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
		{
		}

		/// <summary>
		/// Allows you to create special effects when a projectile created by this player hits an NPC (for example, inflicting debuffs).
		/// <para /> Called on the client hitting the target.
		/// </summary>
		/// <param name="proj"></param>
		/// <param name="target"></param>
		/// <param name="hit"></param>
		/// <param name="damageDone"></param>
		// Token: 0x0600231C RID: 8988 RVA: 0x004E84A8 File Offset: 0x004E66A8
		public virtual void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
		{
		}

		/// <summary>
		/// Allows you to determine whether a melee weapon swung by this player can hit the given opponent player. Return false to block this weapon from hitting the target. Returns true by default.
		/// <para /> Called on the client hitting the target.
		/// </summary>
		/// <param name="item"></param>
		/// <param name="target"></param>
		/// <returns></returns>
		// Token: 0x0600231D RID: 8989 RVA: 0x004E84AA File Offset: 0x004E66AA
		public virtual bool CanHitPvp(Item item, Player target)
		{
			return true;
		}

		/// <summary>
		/// Allows you to determine whether a projectile created by this player can hit the given opponent player. Return false to block the projectile from hitting the target. Returns true by default.
		/// <para /> Called on the client hitting the target.
		/// </summary>
		/// <param name="proj"></param>
		/// <param name="target"></param>
		/// <returns></returns>
		// Token: 0x0600231E RID: 8990 RVA: 0x004E84AD File Offset: 0x004E66AD
		public virtual bool CanHitPvpWithProj(Projectile proj, Player target)
		{
			return true;
		}

		/// <summary>
		/// Allows you to determine whether the given NPC can hit this player. Return false to block this player from being hit by the NPC. Returns true by default. CooldownSlot determines which of the player's cooldown counters (<see cref="T:Terraria.ID.ImmunityCooldownID" />) to use, and defaults to -1 (<see cref="F:Terraria.ID.ImmunityCooldownID.General" />).
		/// <para /> Called on the client taking damage
		/// </summary>
		/// <param name="npc"></param>
		/// <param name="cooldownSlot"></param>
		/// <returns></returns>
		// Token: 0x0600231F RID: 8991 RVA: 0x004E84B0 File Offset: 0x004E66B0
		public virtual bool CanBeHitByNPC(NPC npc, ref int cooldownSlot)
		{
			return true;
		}

		/// <summary>
		/// Allows you to modify the damage, etc., that an NPC does to this player.
		/// <para /> Called on the client taking damage
		/// </summary>
		// Token: 0x06002320 RID: 8992 RVA: 0x004E84B3 File Offset: 0x004E66B3
		public virtual void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
		{
		}

		/// <summary>
		/// Allows you to create special effects when an NPC hits this player (for example, inflicting debuffs).
		/// <para /> Called on the client taking damage
		/// </summary>
		// Token: 0x06002321 RID: 8993 RVA: 0x004E84B5 File Offset: 0x004E66B5
		public virtual void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
		{
		}

		/// <summary>
		/// Allows you to determine whether the given hostile projectile can hit this player. Return false to block this player from being hit. Returns true by default.
		/// <para /> Called on the client taking damage
		/// </summary>
		/// <param name="proj"></param>
		/// <returns></returns>
		// Token: 0x06002322 RID: 8994 RVA: 0x004E84B7 File Offset: 0x004E66B7
		public virtual bool CanBeHitByProjectile(Projectile proj)
		{
			return true;
		}

		/// <summary>
		/// Allows you to modify the damage, etc., that a hostile projectile does to this player.
		/// <para /> Called on the client taking damage
		/// </summary>
		// Token: 0x06002323 RID: 8995 RVA: 0x004E84BA File Offset: 0x004E66BA
		public virtual void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
		{
		}

		/// <summary>
		/// Allows you to create special effects when a hostile projectile hits this player.
		/// <para /> Called on the client taking damage
		/// </summary>
		// Token: 0x06002324 RID: 8996 RVA: 0x004E84BC File Offset: 0x004E66BC
		public virtual void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
		{
		}

		/// <summary>
		/// Allows you to change information about the ongoing fishing attempt before caught items/NPCs are decided, after all vanilla information has been gathered.
		/// <para /> Will not be called if various conditions for getting a catch aren't met, meaning you can't modify those.
		/// <para /> Setting <see cref="F:Terraria.DataStructures.FishingAttempt.rolledItemDrop" /> or <see cref="F:Terraria.DataStructures.FishingAttempt.rolledEnemySpawn" /> is not allowed and will be reset, use <see cref="M:Terraria.ModLoader.ModPlayer.CatchFish(Terraria.DataStructures.FishingAttempt,System.Int32@,System.Int32@,Terraria.AdvancedPopupRequest@,Microsoft.Xna.Framework.Vector2@)" /> for that.
		/// <para /> Called for the local client only.
		/// </summary>
		/// <param name="attempt">The structure containing most data from the vanilla fishing attempt</param>
		// Token: 0x06002325 RID: 8997 RVA: 0x004E84BE File Offset: 0x004E66BE
		public virtual void ModifyFishingAttempt(ref FishingAttempt attempt)
		{
		}

		/// <summary>
		/// Allows you to change the item or enemy the player gets when successfully catching an item or NPC. The Fishing Attempt structure contains most information about the vanilla event, including the Item Rod and Bait used by the player, the liquid it is being fished on, and so on.
		/// The Sonar and Sonar position fields allow you to change the text, color, velocity and position of the catch's name (be it item or NPC) freely
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="attempt">The structure containing most data from the vanilla fishing attempt</param>
		/// <param name="itemDrop">The item that will be created when this fishing attempt succeeds. leave &lt;0 for no item</param>
		/// <param name="npcSpawn">The enemy that will be spawned if there is no item caught. leave &lt;0 for no NPC spawn</param>
		/// <param name="sonar">Fill all of this structure's fields to override the sonar text, or make sonar.Text null to disable custom sonar</param>
		/// <param name="sonarPosition">The position the Sonar text will spawn. Bobber location by default.</param>
		// Token: 0x06002326 RID: 8998 RVA: 0x004E84C0 File Offset: 0x004E66C0
		public virtual void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn, ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition)
		{
		}

		/// <summary>
		/// Allows you to modify the item caught by the fishing player, including stack
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="fish">The item (Fish) to modify</param>
		// Token: 0x06002327 RID: 8999 RVA: 0x004E84C2 File Offset: 0x004E66C2
		public virtual void ModifyCaughtFish(Item fish)
		{
		}

		/// <summary>
		/// Choose if this bait will be consumed or not when used for fishing. return null for vanilla behavior.
		/// Not consuming will always take priority over forced consumption
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="bait">The item (bait) that would be consumed</param>
		// Token: 0x06002328 RID: 9000 RVA: 0x004E84C4 File Offset: 0x004E66C4
		public virtual bool? CanConsumeBait(Item bait)
		{
			return null;
		}

		/// <summary>
		/// Allows you to modify the player's fishing power. As an example of the type of stuff that should go here, the phase of the moon can influence fishing power.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="fishingRod"></param>
		/// <param name="bait"></param>
		/// <param name="fishingLevel"></param>
		// Token: 0x06002329 RID: 9001 RVA: 0x004E84DA File Offset: 0x004E66DA
		public virtual void GetFishingLevel(Item fishingRod, Item bait, ref float fishingLevel)
		{
		}

		/// <summary>
		/// Allows you to add to, change, or remove from the items the player earns when finishing an Angler quest. The rareMultiplier is a number between 0.15 and 1 inclusively; the lower it is the higher chance there should be for the player to earn rare items.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="rareMultiplier"></param>
		/// <param name="rewardItems"></param>
		// Token: 0x0600232A RID: 9002 RVA: 0x004E84DC File Offset: 0x004E66DC
		public virtual void AnglerQuestReward(float rareMultiplier, List<Item> rewardItems)
		{
		}

		/// <summary>
		/// Allows you to modify what items are possible for the player to earn when giving a Strange Plant to the Dye Trader.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="rewardPool"></param>
		// Token: 0x0600232B RID: 9003 RVA: 0x004E84DE File Offset: 0x004E66DE
		public virtual void GetDyeTraderReward(List<int> rewardPool)
		{
		}

		/// <summary>
		/// Allows you to create special effects when this player is drawn, such as creating dust, modifying the color the player is drawn in, etc. The fullBright parameter makes it so that the drawn player ignores the modified color and lighting. Make sure to add the indexes of any dusts you create to drawInfo.DustCache, and the indexes of any gore you create to drawInfo.GoreCache.
		/// <para /> This will be called multiple times a frame if a player afterimage is being drawn. Check <code>if(drawinfo.shadow == 0f)</code> to do some logic only when drawing the original player image. For example, spawning dust only for the original player image is commonly the desired behavior.
		/// <para /> Called on local and remote clients.
		/// </summary>
		/// <param name="drawInfo"></param>
		/// <param name="r"></param>
		/// <param name="g"></param>
		/// <param name="b"></param>
		/// <param name="a"></param>
		/// <param name="fullBright"></param>
		// Token: 0x0600232C RID: 9004 RVA: 0x004E84E0 File Offset: 0x004E66E0
		public virtual void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
		{
		}

		/// <summary>
		/// Allows you to modify the drawing parameters of the player before drawing begins.
		/// <para /> Called on local and remote clients.
		/// </summary>
		/// <param name="drawInfo"></param>
		// Token: 0x0600232D RID: 9005 RVA: 0x004E84E2 File Offset: 0x004E66E2
		public virtual void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
		{
		}

		/// <summary>
		/// Allows you to reorder the player draw layers.
		/// This is called once at the end of mod loading, not during the game.
		/// Use with extreme caution, or risk breaking other mods.
		/// </summary>
		/// <param name="positions">Add/remove/change the positions applied to each layer here</param>
		// Token: 0x0600232E RID: 9006 RVA: 0x004E84E4 File Offset: 0x004E66E4
		public virtual void ModifyDrawLayerOrdering(IDictionary<PlayerDrawLayer, PlayerDrawLayer.Position> positions)
		{
		}

		/// <summary>
		/// Allows you to modify the visibility of layers about to be drawn. All layers can be accessed via <see cref="P:Terraria.ModLoader.PlayerDrawLayerLoader.Layers" />. Individual layers can be accessed by the <see cref="T:Terraria.DataStructures.PlayerDrawLayers" /> fields for vanilla layers or <see cref="M:Terraria.ModLoader.ModContent.GetInstance``1" /> for modded layers. The <see cref="M:Terraria.ModLoader.PlayerDrawLayer.Hide" /> method is how a layer can be hidden.
		/// <br /><br /> For example: <code>PlayerDrawLayers.Wings.Hide();
		/// ModContent.GetInstance&lt;ExamplePlayerDrawLayer&gt;().Hide();</code>
		/// <br /><br /> Called on local and remote clients.
		/// </summary>
		/// <param name="drawInfo"></param>
		// Token: 0x0600232F RID: 9007 RVA: 0x004E84E6 File Offset: 0x004E66E6
		public virtual void HideDrawLayers(PlayerDrawSet drawInfo)
		{
		}

		/// <summary>
		/// Use this hook to modify <see cref="F:Terraria.Main.screenPosition" /> after weapon zoom and camera lerp have taken place.
		/// <para /> Called on the local client only.
		/// <para /> Also consider using <c>Main.instance.CameraModifiers.Add(CameraModifier);</c> as shown in ExampleMods MinionBossBody for screen shakes.
		/// </summary>
		// Token: 0x06002330 RID: 9008 RVA: 0x004E84E8 File Offset: 0x004E66E8
		public virtual void ModifyScreenPosition()
		{
		}

		/// <summary>
		/// Use this to modify the zoom factor for the player. The zoom correlates to the percentage of half the screen size the zoom can reach. A value of -1 passed in means no vanilla scope is in effect. A value of 1.0 means the scope can zoom half a screen width/height away, putting the player on the edge of the game screen. Vanilla values include .8, .6666, and .5.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="zoom"></param>
		// Token: 0x06002331 RID: 9009 RVA: 0x004E84EA File Offset: 0x004E66EA
		public virtual void ModifyZoom(ref float zoom)
		{
		}

		/// <summary>
		/// Called on remote clients when a player connects.
		/// </summary>
		// Token: 0x06002332 RID: 9010 RVA: 0x004E84EC File Offset: 0x004E66EC
		public virtual void PlayerConnect()
		{
		}

		/// <summary>
		/// Called on the server and remote clients when a player disconnects.
		/// </summary>
		// Token: 0x06002333 RID: 9011 RVA: 0x004E84EE File Offset: 0x004E66EE
		public virtual void PlayerDisconnect()
		{
		}

		/// <summary>
		/// Called when the player enters the world. A possible use is ensuring that UI elements are reset to the configuration specified in data saved to the ModPlayer. Can also be used for informational messages.
		/// <para /> Called on the local client only.
		/// </summary>
		// Token: 0x06002334 RID: 9012 RVA: 0x004E84F0 File Offset: 0x004E66F0
		public virtual void OnEnterWorld()
		{
		}

		/// <summary>
		/// Called when a player respawns in the world.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		// Token: 0x06002335 RID: 9013 RVA: 0x004E84F2 File Offset: 0x004E66F2
		public virtual void OnRespawn()
		{
		}

		/// <summary>
		/// Called whenever the player shift-clicks an item slot. This can be used to override default clicking behavior (ie. selling, trashing, moving items).
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="inventory">The array of items the slot is part of.</param>
		/// <param name="context">The Terraria.UI.ItemSlot.Context of the inventory.</param>
		/// <param name="slot">The index in the inventory of the clicked slot.</param>
		/// <returns>Whether or not to block the default code (sell, trash, move, etc) from running. Returns false by default.</returns>
		// Token: 0x06002336 RID: 9014 RVA: 0x004E84F4 File Offset: 0x004E66F4
		public virtual bool ShiftClickSlot(Item[] inventory, int context, int slot)
		{
			return false;
		}

		/// <summary>
		/// Called whenever the player hovers over an item slot. This can be used to override <see cref="F:Terraria.Main.cursorOverride" />
		/// <para /> Called on the local client only.
		/// <para /> See <see cref="T:Terraria.ID.CursorOverrideID" /> for cursor override style IDs
		/// </summary>
		/// <param name="inventory">The array of items the slot is part of.</param>
		/// <param name="context">The Terraria.UI.ItemSlot.Context of the inventory.</param>
		/// <param name="slot">The index in the inventory of the hover slot.</param>
		/// <returns>Whether or not to block the default code that modifies <see cref="F:Terraria.Main.cursorOverride" /> from running. Returns false by default.</returns>
		// Token: 0x06002337 RID: 9015 RVA: 0x004E84F7 File Offset: 0x004E66F7
		public virtual bool HoverSlot(Item[] inventory, int context, int slot)
		{
			return false;
		}

		/// <summary>
		/// Called whenever the player sells an item to an NPC.
		/// <para /> Called on the local client only.
		/// <para /> Note that <paramref name="item" /> might be an item sold by the NPC, not an item to buy back. Check <see cref="F:Terraria.Item.buyOnce" /> if relevant to your logic.
		/// </summary>
		/// <param name="vendor">The NPC vendor.</param>
		/// <param name="shopInventory">The current inventory of the NPC shop.</param>
		/// <param name="item">The item the player just sold.</param>
		// Token: 0x06002338 RID: 9016 RVA: 0x004E84FA File Offset: 0x004E66FA
		public virtual void PostSellItem(NPC vendor, Item[] shopInventory, Item item)
		{
		}

		/// <summary>
		/// Return false to prevent a transaction. Called before the transaction.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="vendor">The NPC vendor.</param>
		/// <param name="shopInventory">The current inventory of the NPC shop.</param>
		/// <param name="item">The item the player is attempting to sell.</param>
		/// <returns></returns>
		// Token: 0x06002339 RID: 9017 RVA: 0x004E84FC File Offset: 0x004E66FC
		public virtual bool CanSellItem(NPC vendor, Item[] shopInventory, Item item)
		{
			return true;
		}

		/// <summary>
		/// Called whenever the player buys an item from an NPC.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="vendor">The NPC vendor.</param>
		/// <param name="shopInventory">The current inventory of the NPC shop.</param>
		/// <param name="item">The item the player just purchased.</param>
		// Token: 0x0600233A RID: 9018 RVA: 0x004E84FF File Offset: 0x004E66FF
		public virtual void PostBuyItem(NPC vendor, Item[] shopInventory, Item item)
		{
		}

		/// <summary>
		/// Return false to prevent a transaction. Called before the transaction.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="vendor">The NPC vendor.</param>
		/// <param name="shopInventory">The current inventory of the NPC shop.</param>
		/// <param name="item">The item the player is attempting to buy.</param>
		/// <returns></returns>
		// Token: 0x0600233B RID: 9019 RVA: 0x004E8501 File Offset: 0x004E6701
		public virtual bool CanBuyItem(NPC vendor, Item[] shopInventory, Item item)
		{
			return true;
		}

		/// <summary>
		/// Return false to prevent an item from being used. By default returns true.
		/// <para /> Called on local, server, and remote clients.
		/// <br /><br /> The item may or not be used after this method is called, so logic in this method should have no side effects such as consuming items or resources.
		/// </summary>
		/// <param name="item">The item the player is attempting to use.</param>
		// Token: 0x0600233C RID: 9020 RVA: 0x004E8504 File Offset: 0x004E6704
		public virtual bool CanUseItem(Item item)
		{
			return true;
		}

		/// <summary>
		/// Allows you to modify the autoswing (auto-reuse) behavior of any item without having to mess with Item.autoReuse.
		/// <para /> Useful to create effects like the Feral Claws which makes melee weapons and whips auto-reusable.
		/// <para /> Return true to enable autoswing (if not already enabled through autoReuse), return false to prevent autoswing. Returns null by default, which applies vanilla behavior.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="item"> The item. </param>
		// Token: 0x0600233D RID: 9021 RVA: 0x004E8508 File Offset: 0x004E6708
		public virtual bool? CanAutoReuseItem(Item item)
		{
			return null;
		}

		/// <summary>
		/// Called while the nurse chat is displayed. Return false to prevent the player from healing. If you return false, you need to set chatText so the user knows why they can't heal.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="nurse">The Nurse NPC instance.</param>
		/// <param name="health">How much health the player gains.</param>
		/// <param name="removeDebuffs">If set to false, debuffs will not be healed.</param>
		/// <param name="chatText">Set this to the Nurse chat text that will display if healing is prevented.</param>
		/// <returns>True by default. False to prevent nurse services.</returns>
		// Token: 0x0600233E RID: 9022 RVA: 0x004E851E File Offset: 0x004E671E
		public virtual bool ModifyNurseHeal(NPC nurse, ref int health, ref bool removeDebuffs, ref string chatText)
		{
			return true;
		}

		/// <summary>
		/// Called while the nurse chat is displayed and after ModifyNurseHeal. Allows custom pricing for Nurse services. See the <see href="https://terraria.wiki.gg/wiki/Nurse">Nurse wiki page</see> for the default pricing.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="nurse">The Nurse NPC instance.</param>
		/// <param name="health">How much health the player gains.</param>
		/// <param name="removeDebuffs">Whether or not debuffs will be healed.</param>
		/// <param name="price"></param>
		// Token: 0x0600233F RID: 9023 RVA: 0x004E8521 File Offset: 0x004E6721
		public virtual void ModifyNursePrice(NPC nurse, int health, bool removeDebuffs, ref int price)
		{
		}

		/// <summary>
		/// Called after the player heals themselves with the Nurse NPC.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="nurse">The Nurse npc providing the heal.</param>
		/// <param name="health">How much health the player gained.</param>
		/// /// <param name="removeDebuffs">Whether or not debuffs were healed.</param>
		/// <param name="price">The price the player paid in copper coins.</param>
		// Token: 0x06002340 RID: 9024 RVA: 0x004E8523 File Offset: 0x004E6723
		public virtual void PostNurseHeal(NPC nurse, int health, bool removeDebuffs, int price)
		{
		}

		/// <summary>
		/// Called when the player is created in the menu.
		/// You can use this method to add items to the player's starting inventory, as well as their inventory when they respawn in mediumcore.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="mediumCoreDeath">Whether you are setting up a mediumcore player's inventory after their death.</param>
		/// <returns>An enumerable of the items you want to add. If you want to add nothing, return Enumerable.Empty&lt;Item&gt;().</returns>
		// Token: 0x06002341 RID: 9025 RVA: 0x004E8525 File Offset: 0x004E6725
		public virtual IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
		{
			return Enumerable.Empty<Item>();
		}

		/// <summary>
		/// Allows you to modify the items that will be added to the player's inventory. Useful if you want to stop vanilla or other mods from adding an item.
		/// You can access a mod's items by using the mod's internal name as the indexer, such as: additions["ModName"]. To access vanilla items you can use "Terraria" as the index.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="itemsByMod">The items that will be added. Each key is the internal mod name of the mod adding the items. Vanilla items use the "Terraria" key.</param>
		/// <param name="mediumCoreDeath">Whether you are setting up a mediumcore player's inventory after their death.</param>
		// Token: 0x06002342 RID: 9026 RVA: 0x004E852C File Offset: 0x004E672C
		public virtual void ModifyStartingInventory(IReadOnlyDictionary<string, List<Item>> itemsByMod, bool mediumCoreDeath)
		{
		}

		/// <summary>
		/// Called when Recipe.FindRecipes is called or the player is crafting an item
		/// You can use this method to add items as the materials that may be used for crafting items
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="itemConsumedCallback">The action that gets invoked when the item is consumed</param>
		/// <returns>A list of the items that may be used as crafting materials or null if none are available.</returns>
		// Token: 0x06002343 RID: 9027 RVA: 0x004E852E File Offset: 0x004E672E
		public virtual IEnumerable<Item> AddMaterialsForCrafting(out ModPlayer.ItemConsumedCallback itemConsumedCallback)
		{
			itemConsumedCallback = null;
			return null;
		}

		/// <summary>
		/// Allows you to make special things happen when this player picks up an item. Return false to stop the item from being added to the player's inventory; returns true by default.
		/// <para /> Called on the local client only.
		/// </summary>
		/// <param name="item">The item being picked up</param>
		/// <returns></returns>
		// Token: 0x06002344 RID: 9028 RVA: 0x004E8534 File Offset: 0x004E6734
		public virtual bool OnPickup(Item item)
		{
			return true;
		}

		/// <summary>
		/// Whether or not the player can be teleported to the given coordinates with methods such as Teleportation Potions or the Rod of Discord.
		/// <para /> The coordinates correspond to the top left corner of the player position after teleporting.
		/// <para /> This gets called in <see cref="M:Terraria.Player.CheckForGoodTeleportationSpot(System.Boolean@,System.Int32,System.Int32,System.Int32,System.Int32,Terraria.Player.RandomTeleportationAttemptSettings)" /> and <see cref="M:Terraria.Player.ItemCheck_UseTeleportRod(Terraria.Item)" />. The <paramref name="context" /> will have a value of "CheckForGoodTeleportationSpot" or "TeleportRod" respectively indicating which type of teleport is being attempted.
		/// </summary>
		// Token: 0x06002345 RID: 9029 RVA: 0x004E8537 File Offset: 0x004E6737
		public virtual bool CanBeTeleportedTo(Vector2 teleportPosition, string context)
		{
			return true;
		}

		/// <summary>
		/// Allows to execute some code if the equipment loadout was switched.
		/// <br /><br /> Called on the server and on all clients.
		/// </summary>
		/// <param name="oldLoadoutIndex">The old loadout index.</param>
		/// <param name="loadoutIndex">The new loadout index.</param>
		// Token: 0x06002346 RID: 9030 RVA: 0x004E853A File Offset: 0x004E673A
		public virtual void OnEquipmentLoadoutSwitched(int oldLoadoutIndex, int loadoutIndex)
		{
		}

		/// <summary>
		/// An action to be invoked when an item is partially or fully consumed
		/// </summary>
		/// <param name="item">The item that has been consumed. May have been set to air if the item was fully consumed.</param>
		/// <param name="index">The index of the item enumerated in IEnumerable&lt;Item&gt;</param>
		// Token: 0x0200093D RID: 2365
		// (Invoke) Token: 0x06005401 RID: 21505
		public delegate void ItemConsumedCallback(Item item, int index);
	}
}
