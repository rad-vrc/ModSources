using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics;
using Terraria.IO;
using Terraria.Localization;
using Terraria.Map;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.IO;
using Terraria.UI;
using Terraria.WorldBuilding;

namespace Terraria.ModLoader
{
	/// <summary>
	/// ModSystem is an abstract class that your classes can derive from. It contains general-use hooks, and, unlike Mod, can have unlimited amounts of types deriving from it.
	/// </summary>
	// Token: 0x020001CC RID: 460
	public abstract class ModSystem : ModType
	{
		// Token: 0x060023E3 RID: 9187 RVA: 0x004E91D0 File Offset: 0x004E73D0
		protected override void ValidateType()
		{
			base.ValidateType();
			LoaderUtils.MustOverrideTogether<ModSystem>(this, new Expression<Func<ModSystem, Delegate>>[]
			{
				(ModSystem s) => (Action<TagCompound>)methodof(ModSystem.SaveWorldData(TagCompound)).CreateDelegate(typeof(Action<TagCompound>), s),
				(ModSystem s) => (Action<TagCompound>)methodof(ModSystem.LoadWorldData(TagCompound)).CreateDelegate(typeof(Action<TagCompound>), s)
			});
			LoaderUtils.MustOverrideTogether<ModSystem>(this, new Expression<Func<ModSystem, Delegate>>[]
			{
				(ModSystem s) => (Action<BinaryWriter>)methodof(ModSystem.NetSend(BinaryWriter)).CreateDelegate(typeof(Action<BinaryWriter>), s),
				(ModSystem s) => (Action<BinaryReader>)methodof(ModSystem.NetReceive(BinaryReader)).CreateDelegate(typeof(Action<BinaryReader>), s)
			});
		}

		// Token: 0x060023E4 RID: 9188 RVA: 0x004E9433 File Offset: 0x004E7633
		protected override void Register()
		{
			SystemLoader.Add(this);
			ModTypeLookup<ModSystem>.Register(this);
		}

		/// <summary>
		/// Unlike other ModTypes, SetupContent is unsealed for you to do whatever you need. By default it just calls SetStaticDefaults.
		/// This is the place to finish initializing your mod's content. For content from other mods, and lookup tables, consider PostSetupContent
		/// </summary>
		// Token: 0x060023E5 RID: 9189 RVA: 0x004E9441 File Offset: 0x004E7641
		public override void SetupContent()
		{
			this.SetStaticDefaults();
		}

		/// <summary>
		/// This hook is called right after Mod.Load(), which is guaranteed to be called after all content has been autoloaded.
		/// </summary>
		// Token: 0x060023E6 RID: 9190 RVA: 0x004E9449 File Offset: 0x004E7649
		public virtual void OnModLoad()
		{
		}

		/// <summary>
		/// This hook is called right before Mod.UnLoad()
		/// </summary>
		// Token: 0x060023E7 RID: 9191 RVA: 0x004E944B File Offset: 0x004E764B
		public virtual void OnModUnload()
		{
		}

		/// <summary>
		/// Allows you to load things in your system after the mod's content has been setup (arrays have been resized to fit the content, etc).
		/// </summary>
		// Token: 0x060023E8 RID: 9192 RVA: 0x004E944D File Offset: 0x004E764D
		public virtual void PostSetupContent()
		{
		}

		/// <summary>
		/// Allows mods to react to language changing. <br />
		/// This happens whenever the language is changed, and when resource packs are reloaded.
		/// </summary>
		// Token: 0x060023E9 RID: 9193 RVA: 0x004E944F File Offset: 0x004E764F
		public virtual void OnLocalizationsLoaded()
		{
		}

		/// <summary>
		/// Override this method to add <see cref="T:Terraria.Recipe" />s to the game.<br />
		/// The <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-Recipes">Basic Recipes Guide</see> teaches how to add new recipes to the game and how to manipulate existing recipes.<br />
		/// </summary>
		// Token: 0x060023EA RID: 9194 RVA: 0x004E9451 File Offset: 0x004E7651
		public virtual void AddRecipes()
		{
		}

		/// <summary>
		/// This provides a hook into the mod-loading process immediately after recipes have been added.
		/// <br /> You can use this to edit recipes added by other mods.
		/// </summary>
		// Token: 0x060023EB RID: 9195 RVA: 0x004E9453 File Offset: 0x004E7653
		public virtual void PostAddRecipes()
		{
		}

		/// <summary>
		/// Override this method to do treatment about recipes once they have been setup. You shouldn't edit any recipe here.
		/// </summary>
		// Token: 0x060023EC RID: 9196 RVA: 0x004E9455 File Offset: 0x004E7655
		public virtual void PostSetupRecipes()
		{
		}

		/// <summary>
		/// Override this method to add recipe groups to the game.
		/// <br /> You must add recipe groups by calling the <see cref="M:Terraria.RecipeGroup.RegisterGroup(System.String,Terraria.RecipeGroup)" /> method here.
		/// <br /> A recipe group is a set of items that can be used interchangeably in the same recipe.
		/// </summary>
		// Token: 0x060023ED RID: 9197 RVA: 0x004E9457 File Offset: 0x004E7657
		public virtual void AddRecipeGroups()
		{
		}

		/// <summary>
		/// Called whenever a world is loaded, before <see cref="M:Terraria.ModLoader.ModSystem.LoadWorldData(Terraria.ModLoader.IO.TagCompound)" /> <br />
		/// If you need to initialize tile or other world related data-structures, use <see cref="M:Terraria.ModLoader.ModSystem.ClearWorld" /> instead
		/// </summary>
		// Token: 0x060023EE RID: 9198 RVA: 0x004E9459 File Offset: 0x004E7659
		public virtual void OnWorldLoad()
		{
		}

		/// <summary>
		/// Called whenever a world is unloaded.
		/// </summary>
		// Token: 0x060023EF RID: 9199 RVA: 0x004E945B File Offset: 0x004E765B
		public virtual void OnWorldUnload()
		{
		}

		/// <summary>
		/// Called whenever the world is cleared. Use this to reset world-related data structures before world-gen or loading in both single and multiplayer. <br />
		/// Also called just before mods are unloaded.
		/// </summary>
		// Token: 0x060023F0 RID: 9200 RVA: 0x004E945D File Offset: 0x004E765D
		public virtual void ClearWorld()
		{
		}

		/// <summary>
		/// Use this hook to modify <see cref="F:Terraria.Main.screenPosition" /> after weapon zoom and camera lerp have taken place.
		/// <para /> Called on all clients.
		/// <para /> Also consider using <c>Main.instance.CameraModifiers.Add(CameraModifier);</c> as shown in ExampleMods MinionBossBody for screen shakes.
		/// </summary>
		// Token: 0x060023F1 RID: 9201 RVA: 0x004E945F File Offset: 0x004E765F
		public virtual void ModifyScreenPosition()
		{
		}

		/// <summary>
		/// Allows you to set the transformation of the screen that is drawn. (Translations, rotations, scales, etc.)
		/// <para /> Called on all clients.
		/// </summary>
		// Token: 0x060023F2 RID: 9202 RVA: 0x004E9461 File Offset: 0x004E7661
		public virtual void ModifyTransformMatrix(ref SpriteViewMatrix Transform)
		{
		}

		/// <summary>
		/// Ran every update and suitable for calling Update for UserInterface classes
		/// <para /> Called on all clients.
		/// </summary>
		// Token: 0x060023F3 RID: 9203 RVA: 0x004E9463 File Offset: 0x004E7663
		public virtual void UpdateUI(GameTime gameTime)
		{
		}

		/// <summary>
		/// Use this if you want to do something before anything in the World gets updated.
		/// Called after UI updates, but before anything in the World (Players, NPCs, Projectiles, Tiles) gets updated.
		/// <para /> When <see cref="F:Terraria.Main.autoPause" /> is true or <see cref="F:Terraria.Main.FrameSkipMode" /> is 0 or 2, the game may do a partial update. This means that it only updates menus and some animations, but not the World or Entities. This hook - and every hook after it - only gets called on frames with a full update.
		/// <para /> Called on all clients and the server.
		/// </summary>
		// Token: 0x060023F4 RID: 9204 RVA: 0x004E9465 File Offset: 0x004E7665
		public virtual void PreUpdateEntities()
		{
		}

		/// <summary>
		/// Called before Players get updated.
		/// <para /> Called on all clients and the server.
		/// </summary>
		// Token: 0x060023F5 RID: 9205 RVA: 0x004E9467 File Offset: 0x004E7667
		public virtual void PreUpdatePlayers()
		{
		}

		/// <summary>
		/// Called after Players get updated.
		/// <para /> Called on all clients and the server.
		/// </summary>
		// Token: 0x060023F6 RID: 9206 RVA: 0x004E9469 File Offset: 0x004E7669
		public virtual void PostUpdatePlayers()
		{
		}

		/// <summary>
		/// Called before NPCs get updated.
		/// <para /> Called on all clients and the server.
		/// </summary>
		// Token: 0x060023F7 RID: 9207 RVA: 0x004E946B File Offset: 0x004E766B
		public virtual void PreUpdateNPCs()
		{
		}

		/// <summary>
		/// Called after NPCs get updated.
		/// <para /> Called on all clients and the server.
		/// </summary>
		// Token: 0x060023F8 RID: 9208 RVA: 0x004E946D File Offset: 0x004E766D
		public virtual void PostUpdateNPCs()
		{
		}

		/// <summary>
		/// Called before Gores get updated.
		/// <para /> Called on all clients and the server.
		/// </summary>
		// Token: 0x060023F9 RID: 9209 RVA: 0x004E946F File Offset: 0x004E766F
		public virtual void PreUpdateGores()
		{
		}

		/// <summary>
		/// Called after Gores get updated.
		/// <para /> Called on all clients and the server.
		/// </summary>
		// Token: 0x060023FA RID: 9210 RVA: 0x004E9471 File Offset: 0x004E7671
		public virtual void PostUpdateGores()
		{
		}

		/// <summary>
		/// Called before Projectiles get updated.
		/// <para /> Called on all clients and the server.
		/// </summary>
		// Token: 0x060023FB RID: 9211 RVA: 0x004E9473 File Offset: 0x004E7673
		public virtual void PreUpdateProjectiles()
		{
		}

		/// <summary>
		/// Called after Projectiles get updated.
		/// <para /> Called on all clients and the server.
		/// </summary>
		// Token: 0x060023FC RID: 9212 RVA: 0x004E9475 File Offset: 0x004E7675
		public virtual void PostUpdateProjectiles()
		{
		}

		/// <summary>
		/// Called before Items get updated.
		/// <para /> Called on all clients and the server.
		/// </summary>
		// Token: 0x060023FD RID: 9213 RVA: 0x004E9477 File Offset: 0x004E7677
		public virtual void PreUpdateItems()
		{
		}

		/// <summary>
		/// Called after Items get updated.
		/// <para /> Called on all clients and the server.
		/// </summary>
		// Token: 0x060023FE RID: 9214 RVA: 0x004E9479 File Offset: 0x004E7679
		public virtual void PostUpdateItems()
		{
		}

		/// <summary>
		/// Called before Dusts get updated.
		/// <para /> Called on all clients and the server.
		/// </summary>
		// Token: 0x060023FF RID: 9215 RVA: 0x004E947B File Offset: 0x004E767B
		public virtual void PreUpdateDusts()
		{
		}

		/// <summary>
		/// Called after Dusts get updated.
		/// <para /> Called on all clients and the server.
		/// </summary>
		// Token: 0x06002400 RID: 9216 RVA: 0x004E947D File Offset: 0x004E767D
		public virtual void PostUpdateDusts()
		{
		}

		/// <summary>
		/// Called before Time gets updated.
		/// <para /> Called on all clients and the server.
		/// </summary>
		// Token: 0x06002401 RID: 9217 RVA: 0x004E947F File Offset: 0x004E767F
		public virtual void PreUpdateTime()
		{
		}

		/// <summary>
		/// Called after Time gets updated.
		/// <para /> Called on all clients and the server.
		/// </summary>
		// Token: 0x06002402 RID: 9218 RVA: 0x004E9481 File Offset: 0x004E7681
		public virtual void PostUpdateTime()
		{
		}

		/// <summary>
		/// Use this method to have things happen in the world. In vanilla Terraria, a good example of code suitable for this hook is how Falling Stars fall to the ground during the night.
		/// <para /> Called in single player or on the server only.
		/// </summary>
		// Token: 0x06002403 RID: 9219 RVA: 0x004E9483 File Offset: 0x004E7683
		public virtual void PreUpdateWorld()
		{
		}

		/// <summary>
		/// Use this method to have things happen in the world. In vanilla Terraria, a good example of code suitable for this hook is how Falling Stars fall to the ground during the night.
		/// <para /> Called in single player or on the server only.
		/// </summary>
		// Token: 0x06002404 RID: 9220 RVA: 0x004E9485 File Offset: 0x004E7685
		public virtual void PostUpdateWorld()
		{
		}

		/// <summary>
		/// Called before Invasions get updated.
		/// <para /> Called in single player or on the server only.
		/// </summary>
		// Token: 0x06002405 RID: 9221 RVA: 0x004E9487 File Offset: 0x004E7687
		public virtual void PreUpdateInvasions()
		{
		}

		/// <summary>
		/// Called after Invasions get updated.
		/// <para /> Called in single player or on the server only.
		/// </summary>
		// Token: 0x06002406 RID: 9222 RVA: 0x004E9489 File Offset: 0x004E7689
		public virtual void PostUpdateInvasions()
		{
		}

		/// <summary>
		/// Called after the Network got updated, this is the last hook that happens in an update.
		/// <para /> Called in single player or on the server only.
		/// </summary>
		// Token: 0x06002407 RID: 9223 RVA: 0x004E948B File Offset: 0x004E768B
		public virtual void PostUpdateEverything()
		{
		}

		/// <summary>
		/// Allows you to modify the elements of the in-game interface that get drawn. GameInterfaceLayer can be found in the Terraria.UI namespace. Check the <see href="https://github.com/tModLoader/tModLoader/wiki/Vanilla-Interface-layers-values">Vanilla Interface layers values wiki page</see> for vanilla interface layer names
		/// <para /> Called on all clients.
		/// </summary>
		/// <param name="layers">The layers.</param>
		// Token: 0x06002408 RID: 9224 RVA: 0x004E948D File Offset: 0x004E768D
		public virtual void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
		}

		/// <summary>
		/// Allows you to set the visibility of any added vanilla or modded GameTips. In order to add your OWN tips, add them in
		/// your localization file, with the key prefix of "Mods.ModName.GameTips".
		/// </summary>
		/// <param name="gameTips"> The current list of all added game tips. </param>
		// Token: 0x06002409 RID: 9225 RVA: 0x004E948F File Offset: 0x004E768F
		public virtual void ModifyGameTipVisibility(IReadOnlyList<GameTipData> gameTips)
		{
		}

		/// <summary>
		/// Called after interface is drawn but right before mouse and mouse hover text is drawn. Allows for drawing interface.
		/// Note: This hook should no longer be used. It is better to use the ModifyInterfaceLayers hook.
		/// <para /> Called on all clients.
		/// </summary>
		/// <param name="spriteBatch">The sprite batch.</param>
		// Token: 0x0600240A RID: 9226 RVA: 0x004E9491 File Offset: 0x004E7691
		public virtual void PostDrawInterface(SpriteBatch spriteBatch)
		{
		}

		/// <summary>
		/// Called right before map icon overlays are drawn. Use this hook to selectively hide existing <see cref="T:Terraria.Map.IMapLayer" /> or <see cref="T:Terraria.ModLoader.ModMapLayer" />
		/// <para /> Called on all clients.
		/// </summary>
		/// <param name="layers"></param>
		/// <param name="mapOverlayDrawContext"></param>
		// Token: 0x0600240B RID: 9227 RVA: 0x004E9493 File Offset: 0x004E7693
		public virtual void PreDrawMapIconOverlay(IReadOnlyList<IMapLayer> layers, MapOverlayDrawContext mapOverlayDrawContext)
		{
		}

		/// <summary>
		/// Called while the fullscreen map is active. Allows custom drawing to the map. Using <see cref="T:Terraria.ModLoader.ModMapLayer" /> is more compatible and allows drawing on the minimap and fullscreen maps.
		/// <para /> Called on all clients.
		/// </summary>
		/// <param name="mouseText">The mouse text.</param>
		// Token: 0x0600240C RID: 9228 RVA: 0x004E9495 File Offset: 0x004E7695
		public virtual void PostDrawFullscreenMap(ref string mouseText)
		{
		}

		/// <summary>
		/// Called after the input keys are polled. Allows for modifying things like scroll wheel if your custom drawing should capture that.
		/// <para /> Called on all clients.
		/// </summary>
		// Token: 0x0600240D RID: 9229 RVA: 0x004E9497 File Offset: 0x004E7697
		public virtual void PostUpdateInput()
		{
		}

		/// <summary>
		/// Called when the Save and Quit button is pressed. One use for this hook is clearing out custom UI slots to return items to the player.
		/// <para /> Called on the local client only.
		/// </summary>
		// Token: 0x0600240E RID: 9230 RVA: 0x004E9499 File Offset: 0x004E7699
		public virtual void PreSaveAndQuit()
		{
		}

		/// <summary>
		/// Called after drawing Tiles. Can be used for drawing a tile overlay akin to wires. Note that spritebatch should be begun and ended within this method.
		/// <para /> Called on all clients.
		/// </summary>
		// Token: 0x0600240F RID: 9231 RVA: 0x004E949B File Offset: 0x004E769B
		public virtual void PostDrawTiles()
		{
		}

		/// <summary>
		/// Called after all other time calculations. Can be used to modify the speed at which time should progress per tick in seconds, along with the rate at which the tiles in the world and the events in the world should update with it.
		/// All fields are measured in in-game minutes per real-life second (min/sec).
		/// You may want to consider <see cref="M:Terraria.Main.IsFastForwardingTime" /> and CreativePowerManager.Instance.GetPower&lt;CreativePowers.FreezeTime&gt;().Enabled here.
		/// <para /> Called on all clients and the server.
		/// </summary>
		/// <param name="timeRate">The speed at which time flows in min/sec.</param>
		/// <param name="tileUpdateRate">The speed at which tiles in the world update in min/sec.</param>
		/// <param name="eventUpdateRate">The speed at which various events in the world (weather changes, fallen star/fairy spawns, etc.) update in min/sec.</param>
		// Token: 0x06002410 RID: 9232 RVA: 0x004E949D File Offset: 0x004E769D
		public virtual void ModifyTimeRate(ref double timeRate, ref double tileUpdateRate, ref double eventUpdateRate)
		{
		}

		/// <summary>
		/// Allows you to save custom data for this system in the current world. Useful for things like saving world specific flags.
		/// <para /> For example, if your mod adds a boss and you want certain NPC to only spawn once it has been defeated, this is where you would store the information that that boss has been defeated in this world.
		/// <para /> <b>NOTE:</b> The provided tag is always empty by default, and is provided as an argument only for the sake of convenience and optimization.
		/// <para /> <b>NOTE:</b> Try to only save data that isn't default values.
		/// </summary>
		/// <param name="tag"> The TagCompound to save data into. Note that this is always empty by default, and is provided as an argument only for the sake of convenience and optimization. </param>
		// Token: 0x06002411 RID: 9233 RVA: 0x004E949F File Offset: 0x004E769F
		public virtual void SaveWorldData(TagCompound tag)
		{
		}

		/// <summary>
		/// Allows you to load custom data you have saved for this system in the currently loading world.
		/// <br /><b>Try to write defensive loading code that won't crash if something's missing.</b>
		/// </summary>
		/// <param name="tag"> The TagCompound to load data from. </param>
		// Token: 0x06002412 RID: 9234 RVA: 0x004E94A1 File Offset: 0x004E76A1
		public virtual void LoadWorldData(TagCompound tag)
		{
		}

		/// <summary>
		/// Allows you to save custom data for this system in the current world, and have that data accessible in the world select UI and during vanilla world loading.
		/// <br /><b>WARNING:</b> Saving too much data here will cause lag when opening the world select menu for users with many worlds.
		/// <br />Can be retrieved via <see cref="M:Terraria.IO.WorldFileData.TryGetHeaderData(Terraria.ModLoader.ModSystem,Terraria.ModLoader.IO.TagCompound@)" /> and <see cref="F:Terraria.Main.ActiveWorldFileData" />
		/// <br />
		/// <br /><b>NOTE:</b> The provided tag is always empty by default, and is provided as an argument only for the sake of convenience and optimization.
		/// <br /><b>NOTE:</b> Try to only save data that isn't default values.
		/// </summary>
		/// <param name="tag"> The TagCompound to save data into. Note that this is always empty by default, and is provided as an argument only for the sake of convenience and optimization. </param>
		// Token: 0x06002413 RID: 9235 RVA: 0x004E94A3 File Offset: 0x004E76A3
		public virtual void SaveWorldHeader(TagCompound tag)
		{
		}

		/// <summary>
		/// Allows you to prevent the world and player from being loaded/selected as a valid combination, similar to Journey Mode pairing.
		/// </summary>
		// Token: 0x06002414 RID: 9236 RVA: 0x004E94A5 File Offset: 0x004E76A5
		public virtual bool CanWorldBePlayed(PlayerFileData playerData, WorldFileData worldFileData)
		{
			return true;
		}

		// Token: 0x06002415 RID: 9237 RVA: 0x004E94A8 File Offset: 0x004E76A8
		public virtual string WorldCanBePlayedRejectionMessage(PlayerFileData playerData, WorldFileData worldData)
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(141, 2);
			defaultInterpolatedStringHandler.AppendLiteral("The selected character ");
			defaultInterpolatedStringHandler.AppendFormatted(playerData.Name);
			defaultInterpolatedStringHandler.AppendLiteral(" can not be used with the selected world ");
			defaultInterpolatedStringHandler.AppendFormatted(worldData.Name);
			defaultInterpolatedStringHandler.AppendLiteral(".\n");
			defaultInterpolatedStringHandler.AppendLiteral("This could be due to mismatched Journey Mode or other mod specific changes.");
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		/// <summary>
		/// Allows you to send custom data between clients and server, which will be handled in <see cref="M:Terraria.ModLoader.ModSystem.NetReceive(System.IO.BinaryReader)" />. This is useful for syncing information such as bosses that have been defeated.
		/// <para /> Called whenever <see cref="F:Terraria.ID.MessageID.WorldData" /> is successfully sent, for example after a boss is defeated, a new day starts, or a player joins the server.
		/// <para /> Only called on the server.
		/// </summary>
		/// <param name="writer">The writer.</param>
		// Token: 0x06002416 RID: 9238 RVA: 0x004E9513 File Offset: 0x004E7713
		public virtual void NetSend(BinaryWriter writer)
		{
		}

		/// <summary>
		/// Use this to receive information that was sent in <see cref="M:Terraria.ModLoader.ModSystem.NetSend(System.IO.BinaryWriter)" />.
		/// <para /> Called whenever <see cref="F:Terraria.ID.MessageID.WorldData" /> is successfully received.
		/// <para /> Only called on the client.
		/// </summary>
		/// <param name="reader">The reader.</param>
		// Token: 0x06002417 RID: 9239 RVA: 0x004E9515 File Offset: 0x004E7715
		public virtual void NetReceive(BinaryReader reader)
		{
		}

		/// <summary>
		/// Allows you to modify net message / packet information that is received before the game can act on it.
		/// </summary>
		/// <param name="messageType">Type of the message.</param>
		/// <param name="reader">The reader.</param>
		/// <param name="playerNumber">The player number the message is from.</param>
		// Token: 0x06002418 RID: 9240 RVA: 0x004E9517 File Offset: 0x004E7717
		public virtual bool HijackGetData(ref byte messageType, ref BinaryReader reader, int playerNumber)
		{
			return false;
		}

		/// <summary>
		/// Hijacks the send data method. Only use if you absolutely know what you are doing. If any hooks return true, the message is not sent.
		/// </summary>
		// Token: 0x06002419 RID: 9241 RVA: 0x004E951A File Offset: 0x004E771A
		public virtual bool HijackSendData(int whoAmI, int msgType, int remoteClient, int ignoreClient, NetworkText text, int number, float number2, float number3, float number4, int number5, int number6, int number7)
		{
			return false;
		}

		/// <summary>
		/// Allows a mod to run code before a world is generated.
		/// <br />If you use this to initialize data used during worldgen, which you save on the world, also initialize it in <see cref="M:Terraria.ModLoader.ModSystem.OnWorldLoad" />.
		/// </summary>
		// Token: 0x0600241A RID: 9242 RVA: 0x004E951D File Offset: 0x004E771D
		public virtual void PreWorldGen()
		{
		}

		/// <summary>
		/// A more advanced option to PostWorldGen, this method allows you modify the list of Generation Passes before a new world begins to be generated. <para />
		/// For example, disabling the "Planting Trees" pass will cause a world to generate without trees. Placing a new Generation Pass before the "Dungeon" pass will prevent the mod's pass from cutting into the dungeon. <para />
		/// To disable or hide generation passes, please use <see cref="M:Terraria.WorldBuilding.GenPass.Disable" /> and defensive coding.
		/// <para /> See the <see href="https://github.com/tModLoader/tModLoader/wiki/World-Generation#determining-a-suitable-index">"Determining a suitable index" section of the World Generation wiki guide</see> for more information about how to properly use this for adding new world generation passes.
		/// </summary>
		// Token: 0x0600241B RID: 9243 RVA: 0x004E951F File Offset: 0x004E771F
		public virtual void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
		{
		}

		/// <summary>
		/// Use this method to place tiles in the world after world generation is complete.
		/// </summary>
		// Token: 0x0600241C RID: 9244 RVA: 0x004E9521 File Offset: 0x004E7721
		public virtual void PostWorldGen()
		{
		}

		/// <summary>
		/// Use this to reset any fields you set in any of your ModTile.NearbyEffects hooks back to their default values.
		/// <para /> Called on all clients.
		/// </summary>
		// Token: 0x0600241D RID: 9245 RVA: 0x004E9523 File Offset: 0x004E7723
		public virtual void ResetNearbyTileEffects()
		{
		}

		/// <summary>
		/// Similar to <see cref="M:Terraria.ModLoader.ModSystem.ModifyWorldGenTasks(System.Collections.Generic.List{Terraria.WorldBuilding.GenPass},System.Double@)" />, but occurs in-game when Hardmode starts. Can be used to modify which tasks should be done and/or add custom tasks. <para />
		/// By default the list will only contain 5 items, the vanilla hardmode tasks called "Hardmode Good Remix", "Hardmode Good", "Hardmode Evil", "Hardmode Walls", and "Hardmode Announcement". "Hardmode Good Remix" will only be enabled on <see href="https://terraria.wiki.gg/wiki/Don%27t_dig_up">Don't dig up</see> worlds (<see cref="F:Terraria.Main.remixWorld" />) while "Hardmode Good" and "Hardmode Evil" will be enabled otherwise.<para />
		/// To disable or hide tasks, please use <see cref="M:Terraria.WorldBuilding.GenPass.Disable" /> and defensive coding.
		/// </summary>
		// Token: 0x0600241E RID: 9246 RVA: 0x004E9525 File Offset: 0x004E7725
		public virtual void ModifyHardmodeTasks(List<GenPass> list)
		{
		}

		/// <summary>
		/// Allows you to modify color of light the sun emits.
		/// <para /> Called on all clients.
		/// </summary>
		/// <param name="tileColor">Tile lighting color</param>
		/// <param name="backgroundColor">Background lighting color</param>
		// Token: 0x0600241F RID: 9247 RVA: 0x004E9527 File Offset: 0x004E7727
		public virtual void ModifySunLightColor(ref Color tileColor, ref Color backgroundColor)
		{
		}

		/// <summary>
		/// Allows you to modify overall brightness of lights. Can be used to create effects similar to what night vision and darkness (de)buffs give you. Values too high or too low might result in glitches. For night vision effect use scale 1.03
		/// <para /> Called on all clients.
		/// </summary>
		/// <param name="scale">Brightness scale</param>
		// Token: 0x06002420 RID: 9248 RVA: 0x004E9529 File Offset: 0x004E7729
		public virtual void ModifyLightingBrightness(ref float scale)
		{
		}

		/// <summary>
		/// Allows you to store information about how many of each tile is nearby the player. This is useful for counting how many tiles of a certain custom biome there are.
		/// <para /> The <paramref name="tileCounts" /> parameter is a read-only span (treat this as an array) that stores the tile count indexed by tile type.
		/// <para /> Called on all clients.
		/// </summary>
		// Token: 0x06002421 RID: 9249 RVA: 0x004E952B File Offset: 0x004E772B
		public virtual void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
		{
		}

		/// <summary>
		/// Called after all mods register content and before all mods start setting up content. This can be used to initialize data structures dependent on the count of various content IDs, usually through SetFactory instances directly.
		/// <para /> For example, <c>ItemID.Sets.Factory.CreateBoolSet(false, ItemID.PoisonDart, ItemID.PoisonedKnife)</c> will create an array with length equal to the total number of items in the game with the specified item types set to true.
		/// <para /> The CreateNamedXSet methods can be used to expose an ID set for other mods to use by name without a mod dependency.
		/// <para /> See also <see cref="T:Terraria.ModLoader.ReinitializeDuringResizeArraysAttribute" /> for another similar option.
		/// </summary>
		// Token: 0x06002422 RID: 9250 RVA: 0x004E952D File Offset: 0x004E772D
		public virtual void ResizeArrays()
		{
		}
	}
}
