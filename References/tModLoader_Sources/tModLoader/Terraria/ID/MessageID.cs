using System;
using ReLogic.Reflection;
using Terraria.Utilities;

namespace Terraria.ID
{
	/// <summary>
	/// Vanilla netcode messages. These are used in <see cref="M:Terraria.NetMessage.SendData(System.Int32,System.Int32,System.Int32,Terraria.Localization.NetworkText,System.Int32,System.Single,System.Single,System.Single,System.Int32,System.Int32,System.Int32)" /> to send data between the server and clients. The documentation of each <see cref="T:Terraria.ID.MessageID" /> value explains the the purpose of the message and most importantly the meaning of each of the SendData parameters for that message.
	/// </summary>
	// Token: 0x02000415 RID: 1045
	public class MessageID
	{
		// Token: 0x06003537 RID: 13623 RVA: 0x00570700 File Offset: 0x0056E900
		public static string GetName(int id)
		{
			string name;
			if (!MessageID.Search.TryGetName(id, ref name))
			{
				return "Unknown";
			}
			return name;
		}

		// Token: 0x04003DF0 RID: 15856
		public const byte NeverCalled = 0;

		// Token: 0x04003DF1 RID: 15857
		public const byte Hello = 1;

		// Token: 0x04003DF2 RID: 15858
		public const byte Kick = 2;

		// Token: 0x04003DF3 RID: 15859
		public const byte PlayerInfo = 3;

		// Token: 0x04003DF4 RID: 15860
		public const byte SyncPlayer = 4;

		// Token: 0x04003DF5 RID: 15861
		public const byte SyncEquipment = 5;

		// Token: 0x04003DF6 RID: 15862
		public const byte RequestWorldData = 6;

		/// <summary>
		/// Sends all of the world state data, such as time of day, weather, events, world size and name, biome info, killed bosses, etc from the server to the clients. This includes modded world data from <see cref="M:Terraria.ModLoader.ModSystem.NetSend(System.IO.BinaryWriter)" />.
		/// <br /><br />Sent whenever any of the properties in the packet change (except time of day, which gets synced whenever other properties change anyway)
		/// <br /><br /> Mods should send this message when manually manipulating world state data, such as a value synced in <see cref="M:Terraria.ModLoader.ModSystem.NetSend(System.IO.BinaryWriter)" />, to keep each client in sync with the server.
		/// <br /><br /> <c>NetMessage.SendData</c> parameters: None
		/// <br /><br /> <b>Server-&gt;Clients only</b>
		/// </summary>
		// Token: 0x04003DF7 RID: 15863
		public const byte WorldData = 7;

		// Token: 0x04003DF8 RID: 15864
		public const byte SpawnTileData = 8;

		// Token: 0x04003DF9 RID: 15865
		public const byte StatusTextSize = 9;

		// Token: 0x04003DFA RID: 15866
		public const byte TileSection = 10;

		// Token: 0x04003DFB RID: 15867
		[Old("Deprecated. Framing happens as needed after TileSection is sent.")]
		public const byte TileFrameSection = 11;

		// Token: 0x04003DFC RID: 15868
		public const byte PlayerSpawn = 12;

		/// <summary>
		/// Syncs the player's movement keystates, item actions, grapple, gravity, stealth and velocity. Primarily sent from sync with clientClone.
		/// <br /><br /> <c>NetMessage.SendData</c> parameters:
		/// <br /> <b>number:</b> The player.whoAmI of the player
		/// <br /><br /> <b>Forwarded to other clients</b>
		/// </summary>
		// Token: 0x04003DFD RID: 15869
		public const byte PlayerControls = 13;

		// Token: 0x04003DFE RID: 15870
		public const byte PlayerActive = 14;

		// Token: 0x04003DFF RID: 15871
		[Old("Deprecated.")]
		public const byte Unused15 = 15;

		// Token: 0x04003E00 RID: 15872
		public const byte PlayerLifeMana = 16;

		/// <summary>
		/// Sends changes made to a specific tile coordinate.
		/// <br /><br /> Use this whenever manipulating tiles using the <see cref="T:Terraria.WorldGen" /> methods mentioned below.
		/// <br /><br /> <c>NetMessage.SendData</c> parameters:
		/// <br /> <b>number:</b> The change type. These correspond to methods in <see cref="T:Terraria.WorldGen" />:
		/// <br /> 0-&gt;KillTile, 1-&gt;PlaceTile, 2-&gt;KillWall, 3-&gt;PlaceWall, 4-&gt;KillTile (No Item), 5-&gt;PlaceWire, 6-&gt;KillWire, 7-&gt;PoundTile, 8-&gt;PlaceActuator, 9-&gt;KillActuator, 10-&gt;PlaceWire2, 11-&gt;KillWire2, 10-&gt;PlaceWire3, 13-&gt;KillWire3, 14-&gt;SlopeTile, 15-&gt;Minecart.FrameTrack, 16-&gt;PlaceWire4, 17-&gt;KillWire4, 18-&gt;Wiring.PokeLogicGate, 19-&gt;Wiring.Actuate, 20-&gt;KillTile (Determine Fail On Server), 21-&gt;ReplaceTile, 22-&gt;ReplaceWall, 23-&gt;SlopeTile+PoundTile
		/// <br /> <b>number2:</b> x tile coordinate
		/// <br /> <b>number3:</b> y tile coordinate
		/// <br /> <b>number4:</b> Changes meaning based on change type:
		/// <br /> KillTile/KillWall/KillTile (NoItem)/KillTile (Determine Fail On Server)-&gt;Fail if 1, PlaceTile-&gt;Tile type, PlaceWall-&gt;Wall type, SlopeTile-&gt;slope value, ReplaceTile-&gt;target tile type, ReplaceWall-&gt;target wall type, SlopeTile+PoundTile-&gt;slope value,
		/// <br /> <b>number5:</b> Tile style, only used with PlaceTile and ReplaceTile change types
		/// <br /><br /> <b>Client-&gt;Server and Server-&gt;Clients. Automatically forwarded to other clients</b>
		/// </summary>
		// Token: 0x04003E01 RID: 15873
		public const byte TileManipulation = 17;

		// Token: 0x04003E02 RID: 15874
		public const byte SetTime = 18;

		// Token: 0x04003E03 RID: 15875
		public const byte ToggleDoorState = 19;

		/// <summary>
		/// Sends a square area of tile data (with upper left x, y) and given size (minimum 1)
		/// <br /> Forwarded to other clients
		/// <br /><br /> This is usually used through the <see cref="M:Terraria.NetMessage.SendTileSquare(System.Int32,System.Int32,System.Int32,Terraria.ID.TileChangeType)" /> helper methods.
		/// </summary>
		// Token: 0x04003E04 RID: 15876
		public const byte TileSquare = 20;

		/// <summary>
		/// Syncs all info about an item in the world, includding modded data.
		/// <br /><br /> Use this whenever an item in the world is manipulated. <c>Item.NewItem</c> calls this automatically on the server to sync the item with other clients, but on clients this needs to be used or <c>Player.QuickSpawnItem</c> should be used instead.
		/// <br /><br /> <c>NetMessage.SendData</c> parameters:
		/// <br /> <b>number:</b> The index of the item within <see cref="F:Terraria.Main.item" />
		/// <br /> <b>number2:</b> Set to 1 to ignore the delay for the spawning client to pickup the item. The <c>noGrabDelay</c> parameter of <c>Item.NewItem</c> sets this.
		/// <br /><br /> <b>Forwarded to other clients</b>
		/// </summary>
		// Token: 0x04003E05 RID: 15877
		public const byte SyncItem = 21;

		// Token: 0x04003E06 RID: 15878
		public const byte ItemOwner = 22;

		/// <summary>
		/// Sends all info about an NPC, position, velocity, and AI. This includes modded data from <see cref="M:Terraria.ModLoader.ModNPC.SendExtraAI(System.IO.BinaryWriter)" /> and <see cref="M:Terraria.ModLoader.GlobalNPC.SendExtraAI(Terraria.NPC,Terraria.ModLoader.IO.BitWriter,System.IO.BinaryWriter)" />.
		/// <br /><br /> Use this when manually spawning an NPC on the server using a <c>NPC.NewNPC</c>.
		/// <br /><br /> <c>NetMessage.SendData</c> parameters:
		/// <br /> <b>number:</b> The index of the spawned NPC within the <see cref="F:Terraria.Main.npc" /> array
		/// <br /><br /> <b>Server-&gt;Clients only</b>
		/// </summary>
		// Token: 0x04003E07 RID: 15879
		public const byte SyncNPC = 23;

		// Token: 0x04003E08 RID: 15880
		public const byte UnusedMeleeStrike = 24;

		// Token: 0x04003E09 RID: 15881
		[Old("Deprecated. Use NetTextModule instead.")]
		public const byte ChatText = 25;

		// Token: 0x04003E0A RID: 15882
		[Old("Deprecated.")]
		public const byte HurtPlayer = 26;

		// Token: 0x04003E0B RID: 15883
		public const byte SyncProjectile = 27;

		// Token: 0x04003E0C RID: 15884
		public const byte DamageNPC = 28;

		// Token: 0x04003E0D RID: 15885
		public const byte KillProjectile = 29;

		// Token: 0x04003E0E RID: 15886
		public const byte TogglePVP = 30;

		/// <summary>
		/// Sent from a client to request access to a <see cref="T:Terraria.Chest" />. Clients have to request access to chests for the server to send the current chest contents and to avoid networking bugs that could arise from network lag and multiple clients interacting with the same chest inventory.
		/// <br /><br /> If not in use by another client, the server will reply with <see cref="F:Terraria.ID.MessageID.SyncChestItem" /> for each item in the chest and then <see cref="F:Terraria.ID.MessageID.SyncPlayerChest" />.  Other clients are sent the <see cref="F:Terraria.ID.MessageID.SyncPlayerChestIndex" /> message.
		/// <br /><br /> Modders should use this message exactly as shown in <see href="https://github.com/tModLoader/tModLoader/blob/stable/ExampleMod/Content/Tiles/Furniture/ExampleChest.cs">ExampleChest</see>.
		/// <br /><br /> <c>NetMessage.SendData</c> parameters:
		/// <br /> <b>number:</b> x tile coordinate of the chest (top left corner)
		/// <br /> <b>number2:</b> y tile coordinate of the chest (top left corner)
		/// <br /><br /> <b>Client-&gt;Server only</b>
		/// </summary>
		// Token: 0x04003E0F RID: 15887
		public const byte RequestChestOpen = 31;

		/// <summary>
		/// Syncs the item in a specific chest slot.
		/// <br /><br /> <c>NetMessage.SendData</c> parameters:
		/// <br /> <b>number:</b> The index of the chest within the <see cref="F:Terraria.Main.chest" /> array
		/// <br /> <b>number2:</b> The index of the item within that chest's <see cref="F:Terraria.Chest.item" /> array
		/// </summary>
		// Token: 0x04003E10 RID: 15888
		public const byte SyncChestItem = 32;

		/// <summary>
		/// Syncs basic information about a chest. Used on clients to send chest name changes to the server (the server then syncs the name change to other clients via <see cref="F:Terraria.ID.MessageID.ChestName" />). Used on the server to allow a specific client to open the chest the client previously requested to open via a <see cref="F:Terraria.ID.MessageID.RequestChestOpen" /> message. Other clients are sent the <see cref="F:Terraria.ID.MessageID.SyncPlayerChestIndex" /> message. 
		/// <br /><br /> Modders should use this message exactly as shown in <see href="https://github.com/tModLoader/tModLoader/blob/stable/ExampleMod/Content/Tiles/Furniture/ExampleChest.cs">ExampleChest</see>.
		/// <br /><br /> <c>NetMessage.SendData</c> parameters:
		/// <br /> <b>text:</b> The name of the chest. <c>NetworkText.FromLiteral(Main.chest[chestIndex].name)</c> should be used. Only relevant if <c>number2</c> is set to 1.
		/// <br /> <b>number:</b> The index of the chest within the <see cref="F:Terraria.Main.chest" /> array. This is typically <see cref="F:Terraria.Player.chest" />.
		/// <br /> <b>number2:</b> If 1, then the <c>text</c> parameter (chest name) is being synced.
		/// </summary>
		// Token: 0x04003E11 RID: 15889
		public const byte SyncPlayerChest = 33;

		// Token: 0x04003E12 RID: 15890
		public const byte ChestUpdates = 34;

		// Token: 0x04003E13 RID: 15891
		public const byte PlayerHeal = 35;

		// Token: 0x04003E14 RID: 15892
		public const byte SyncPlayerZone = 36;

		// Token: 0x04003E15 RID: 15893
		public const byte RequestPassword = 37;

		// Token: 0x04003E16 RID: 15894
		public const byte SendPassword = 38;

		// Token: 0x04003E17 RID: 15895
		public const byte ReleaseItemOwnership = 39;

		// Token: 0x04003E18 RID: 15896
		public const byte SyncTalkNPC = 40;

		// Token: 0x04003E19 RID: 15897
		public const byte ShotAnimationAndSound = 41;

		// Token: 0x04003E1A RID: 15898
		public const byte PlayerMana = 42;

		// Token: 0x04003E1B RID: 15899
		public const byte ManaEffect = 43;

		// Token: 0x04003E1C RID: 15900
		[Old("Deprecated.")]
		public const byte KillPlayer = 44;

		// Token: 0x04003E1D RID: 15901
		public const byte PlayerTeam = 45;

		// Token: 0x04003E1E RID: 15902
		public const byte RequestReadSign = 46;

		// Token: 0x04003E1F RID: 15903
		public const byte ReadSign = 47;

		// Token: 0x04003E20 RID: 15904
		[Old("Deprecated. Use NetLiquidModule instead.")]
		public const byte LiquidUpdate = 48;

		// Token: 0x04003E21 RID: 15905
		public const byte InitialSpawn = 49;

		// Token: 0x04003E22 RID: 15906
		public const byte PlayerBuffs = 50;

		// Token: 0x04003E23 RID: 15907
		public const byte MiscDataSync = 51;

		/// <summary>
		/// Unlocks or locks the chest or door at the provided tile coordinates. The server will also sync the changed tiles to all clients.   
		/// <br /><br /> Modders should use this message exactly as shown in <see href="https://github.com/tModLoader/tModLoader/blob/stable/ExampleMod/Content/Tiles/Furniture/ExampleChest.cs">ExampleChest</see>.
		/// <br /><br /> <c>NetMessage.SendData</c> parameters:
		/// <br /> <b>number2:</b> UnlockType. 1-&gt;Chest.Unlock, 2-&gt;WorldGen.UnlockDoor, 3-&gt;Chest.Lock
		/// <br /> <b>number3:</b> x tile coordinate of the chest (top left corner)
		/// <br /> <b>number4:</b> y tile coordinate of the chest (top left corner)
		/// <br /><br /> <b>Forwarded to other clients</b>
		/// </summary>
		// Token: 0x04003E24 RID: 15908
		public const byte LockAndUnlock = 52;

		// Token: 0x04003E25 RID: 15909
		public const byte AddNPCBuff = 53;

		// Token: 0x04003E26 RID: 15910
		public const byte NPCBuffs = 54;

		// Token: 0x04003E27 RID: 15911
		public const byte AddPlayerBuff = 55;

		// Token: 0x04003E28 RID: 15912
		public const byte UniqueTownNPCInfoSyncRequest = 56;

		// Token: 0x04003E29 RID: 15913
		public const byte TileCounts = 57;

		// Token: 0x04003E2A RID: 15914
		public const byte InstrumentSound = 58;

		// Token: 0x04003E2B RID: 15915
		public const byte HitSwitch = 59;

		// Token: 0x04003E2C RID: 15916
		public const byte NPCHome = 60;

		/// <summary>
		/// Attempts to spawn an NPC on the player, start a specific event, or use a pet license. Spawned NPC must be in <see cref="F:Terraria.ID.NPCID.Sets.MPAllowedEnemies" />, this will not allow multiple to spawn. 
		/// <br /><br /> Mods should use this in boss spawner items to spawn a boss.
		/// <br /><br /> <c>NetMessage.SendData</c> parameters:
		/// <br /> <b>number:</b> The player.whoAmI of the player to spawn on.
		/// <br /> <b>number2:</b> If positive, this is the NPC type to spawn. If negative, this is a special event value from one of the following:
		/// GoblinArmy: -1, FrostLegion: -2, Pirates: -3, PumpkinMoon: -4, SnowMoon: -5, Eclipse: -6, MartianInvation: -7, MoonLordTimer: -8, BloodMoon: -10, <see cref="F:Terraria.NPC.combatBookWasUsed" />: -11, Pet License: -12 through -15, MechQueen: 16, <see cref="F:Terraria.NPC.combatBookVolumeTwoWasUsed" />: -17, <see cref="F:Terraria.NPC.peddlersSatchelWasUsed" />: -18,
		/// <br /><br /> <b>Client-&gt;Server only</b>
		/// </summary>
		// Token: 0x04003E2D RID: 15917
		public const byte SpawnBossUseLicenseStartEvent = 61;

		// Token: 0x04003E2E RID: 15918
		public const byte Dodge = 62;

		// Token: 0x04003E2F RID: 15919
		public const byte PaintTile = 63;

		// Token: 0x04003E30 RID: 15920
		public const byte PaintWall = 64;

		/// <summary>
		/// Teleports the player (or an NPC) to a provided world coordinate. (Sets position, not Center)
		/// <br /><br /> When using any of these teleport methods in multiplayer, make sure to use this instead of calling the methods directly on the client.
		/// <br /><br /> <c>NetMessage.SendData</c> parameters:
		/// <br /> <b>number:</b> teleportType: 0-&gt;Player, 1-&gt;NPC, 2-&gt;PlayerToPlayer
		/// <br /> <b>number2:</b> The player.whoAmI of the player to teleport (or NPC.whoAmI for NPC teleports)
		/// <br /> <b>number3:</b> The destination x world coordinate
		/// <br /> <b>number4:</b> The destination y world coordinate
		/// <br /> <b>number5:</b> The <c>Style</c> parameter of <see cref="M:Terraria.Player.Teleport(Microsoft.Xna.Framework.Vector2,System.Int32,System.Int32)" />, controls the visuals and sounds of the teleport. Equivalent to <see cref="T:Terraria.ID.TeleportationStyleID" /> values.
		/// <br /> <b>number6:</b> Unknown
		/// <br /> <b>number7:</b> The <c>extraInfo</c> parameter of <see cref="M:Terraria.Player.Teleport(Microsoft.Xna.Framework.Vector2,System.Int32,System.Int32)" />, only relevant with pylon teleporting.
		/// <br /><br /> Using this message for long range player teleports won't work correctly because the client might not have the destination section loaded. You might need to use a <see cref="F:Terraria.ID.MessageID.ModPacket" /> to call <see cref="M:Terraria.RemoteClient.CheckSection(System.Int32,Microsoft.Xna.Framework.Vector2,System.Int32)" /> on the server and then call Player.Teleport and relay this message manually.
		/// <br /><br /> <b>Client-&gt;Server for player teleports, Server-&gt;Clients for NPC teleports.</b> The player teleport message is relayed to other clients to sync the visuals.
		/// </summary>
		// Token: 0x04003E31 RID: 15921
		public const byte TeleportEntity = 65;

		// Token: 0x04003E32 RID: 15922
		public const byte SpiritHeal = 66;

		// Token: 0x04003E33 RID: 15923
		public const byte Unknown67 = 67;

		// Token: 0x04003E34 RID: 15924
		public const byte ClientUUID = 68;

		// Token: 0x04003E35 RID: 15925
		public const byte ChestName = 69;

		// Token: 0x04003E36 RID: 15926
		public const byte BugCatching = 70;

		// Token: 0x04003E37 RID: 15927
		public const byte BugReleasing = 71;

		// Token: 0x04003E38 RID: 15928
		public const byte TravelMerchantItems = 72;

		/// <summary>
		/// Teleports the client to set locations using one of 4 teleport methods. Use <see cref="F:Terraria.ID.MessageID.TeleportEntity" /> instead to teleport to arbitrary coordinates.
		/// <br /><br /> When using any of these teleport methods in multiplayer, make sure to use this instead of calling the methods directly on the client.
		/// <br /><br /> <c>NetMessage.SendData</c> parameters:
		/// <br /> <b>number: </b> The teleportation method to call:
		/// <br /> 0-&gt;<see cref="M:Terraria.Player.TeleportationPotion" /> (Random Location), 1-&gt;<see cref="M:Terraria.Player.MagicConch" /> (Ocean), 2-&gt;<see cref="M:Terraria.Player.DemonConch" /> (Underworld), 3-&gt;<see cref="M:Terraria.Player.Shellphone_Spawn" /> (World spawn point).
		/// <br /><br /> <b>Client-&gt;Server only</b>
		/// </summary>
		// Token: 0x04003E39 RID: 15929
		public const byte RequestTeleportationByServer = 73;

		// Token: 0x04003E3A RID: 15930
		public const byte AnglerQuest = 74;

		// Token: 0x04003E3B RID: 15931
		public const byte AnglerQuestFinished = 75;

		// Token: 0x04003E3C RID: 15932
		public const byte QuestsCountSync = 76;

		/// <summary>
		/// Sends a temporary tile animation created in <see cref="M:Terraria.Animation.NewTemporaryAnimation(System.Int32,System.UInt16,System.Int32,System.Int32)" /> on the server to clients.
		/// <br /><br /> No need to manually use this message.
		/// <br /><br /> <c>NetMessage.SendData</c> parameters:
		/// <br /> <b>number:</b> Temporary tile animation ID (<see cref="M:Terraria.Animation.RegisterTemporaryAnimation(System.Int32,System.Int32[])" />)
		/// <br /> <b>number2:</b> Tile Type
		/// <br /> <b>number3:</b> x tile coordinate
		/// <br /> <b>number4:</b> y tile coordinate
		/// </summary>
		// Token: 0x04003E3D RID: 15933
		public const byte TemporaryAnimation = 77;

		// Token: 0x04003E3E RID: 15934
		public const byte InvasionProgressReport = 78;

		// Token: 0x04003E3F RID: 15935
		public const byte PlaceObject = 79;

		// Token: 0x04003E40 RID: 15936
		public const byte SyncPlayerChestIndex = 80;

		/// <summary>
		/// Sends a <see cref="T:Terraria.CombatText" /> from the server to clients. Sends a number, use <see cref="F:Terraria.ID.MessageID.CombatTextString" /> instead for the string version. Used automatically by <see cref="M:Terraria.Player.HealEffect(System.Int32,System.Boolean)" />, but custom <see cref="T:Terraria.CombatText" /> would need to manually sync using this message.
		/// <br /><br /> <c>NetMessage.SendData</c> parameters, correlating to <see cref="M:Terraria.CombatText.NewText(Microsoft.Xna.Framework.Rectangle,Microsoft.Xna.Framework.Color,System.Int32,System.Boolean,System.Boolean)" /> parameters:
		/// <br /><b>number:</b> The text color: <see cref="P:Microsoft.Xna.Framework.Color.PackedValue" />
		/// <br /><b>number2:</b> The CombatText center X coordinate
		/// <br /><b>number3:</b> The CombatText center Y coordinate
		/// <br /><b>number4:</b> The number shown by the CombatText
		/// </summary>
		// Token: 0x04003E41 RID: 15937
		public const byte CombatTextInt = 81;

		// Token: 0x04003E42 RID: 15938
		public const byte NetModules = 82;

		// Token: 0x04003E43 RID: 15939
		public const byte NPCKillCountDeathTally = 83;

		// Token: 0x04003E44 RID: 15940
		public const byte PlayerStealth = 84;

		// Token: 0x04003E45 RID: 15941
		public const byte QuickStackChests = 85;

		/// <summary>
		/// Syncs the data of a TileEntity from server to clients. Will cause TileEntities to send and receive their data to sync their values. (<see cref="M:Terraria.ModLoader.ModTileEntity.NetSend(System.IO.BinaryWriter)" />, <see cref="M:Terraria.ModLoader.ModTileEntity.NetReceive(System.IO.BinaryReader)" />)
		/// <para /> <c>NetMessage.SendData</c> parameters:
		/// <br /><b>number:</b> The <see cref="F:Terraria.DataStructures.TileEntity.ID" />
		/// <br /><br /> <b>Server-&gt;Clients only</b>
		/// </summary>
		// Token: 0x04003E46 RID: 15942
		public const byte TileEntitySharing = 86;

		/// <summary>
		/// Syncs a TileEntity placement attempt from a client to the server. Will cause the server to place the TileEntity and then sync it using <see cref="F:Terraria.ID.MessageID.TileEntitySharing" />.
		/// <br /><br /> This is typically used in the method contained in the <see cref="P:Terraria.ObjectData.TileObjectData.HookPostPlaceMyPlayer" /> of the Tile placing the Tile Entity, but modders can use <see cref="P:Terraria.ModLoader.ModTileEntity.Generic_HookPostPlaceMyPlayer" /> instead of writing custom netcode using this message.
		/// <br /><br /> <c>NetMessage.SendData</c> parameters:
		/// <br /><b>number:</b> The x tile coordinate of the TileEntity, not necessarily the top left corner of the multitile.
		/// <br /><b>number2:</b> The y tile coordinate of the TileEntity, not necessarily the top left corner of the multitile.
		/// <br /><b>number3:</b> The <see cref="T:Terraria.ID.TileEntityID" /> or <see cref="P:Terraria.ModLoader.ModTileEntity.Type" />
		/// <br /><br /> <b>Client-&gt;Server only</b>
		/// </summary>
		// Token: 0x04003E47 RID: 15943
		public const byte TileEntityPlacement = 87;

		// Token: 0x04003E48 RID: 15944
		public const byte ItemTweaker = 88;

		// Token: 0x04003E49 RID: 15945
		public const byte ItemFrameTryPlacing = 89;

		/// <summary>
		/// Exactly the same as <see cref="F:Terraria.ID.MessageID.SyncItem" />, but sets the instanced flag and is only sent server -&gt; client. Used by loot that is dropped per player, like boss bags.
		/// <br /><br /> <see cref="F:Terraria.ID.MessageID.SyncItem" /> docs:
		/// <br /><br /> <inheritdoc cref="F:Terraria.ID.MessageID.SyncItem" />
		/// </summary>
		// Token: 0x04003E4A RID: 15946
		public const byte InstancedItem = 90;

		// Token: 0x04003E4B RID: 15947
		public const byte SyncEmoteBubble = 91;

		// Token: 0x04003E4C RID: 15948
		public const byte SyncExtraValue = 92;

		// Token: 0x04003E4D RID: 15949
		public const byte SocialHandshake = 93;

		// Token: 0x04003E4E RID: 15950
		public const byte Deprecated1 = 94;

		// Token: 0x04003E4F RID: 15951
		public const byte MurderSomeoneElsesPortal = 95;

		// Token: 0x04003E50 RID: 15952
		public const byte TeleportPlayerThroughPortal = 96;

		// Token: 0x04003E51 RID: 15953
		public const byte AchievementMessageNPCKilled = 97;

		// Token: 0x04003E52 RID: 15954
		public const byte AchievementMessageEventHappened = 98;

		// Token: 0x04003E53 RID: 15955
		public const byte MinionRestTargetUpdate = 99;

		// Token: 0x04003E54 RID: 15956
		public const byte TeleportNPCThroughPortal = 100;

		// Token: 0x04003E55 RID: 15957
		public const byte UpdateTowerShieldStrengths = 101;

		// Token: 0x04003E56 RID: 15958
		public const byte NebulaLevelupRequest = 102;

		// Token: 0x04003E57 RID: 15959
		public const byte MoonlordHorror = 103;

		// Token: 0x04003E58 RID: 15960
		public const byte ShopOverride = 104;

		// Token: 0x04003E59 RID: 15961
		public const byte GemLockToggle = 105;

		// Token: 0x04003E5A RID: 15962
		public const byte PoofOfSmoke = 106;

		// Token: 0x04003E5B RID: 15963
		public const byte SmartTextMessage = 107;

		// Token: 0x04003E5C RID: 15964
		public const byte WiredCannonShot = 108;

		// Token: 0x04003E5D RID: 15965
		public const byte MassWireOperation = 109;

		// Token: 0x04003E5E RID: 15966
		public const byte MassWireOperationPay = 110;

		// Token: 0x04003E5F RID: 15967
		public const byte ToggleParty = 111;

		// Token: 0x04003E60 RID: 15968
		public const byte SpecialFX = 112;

		// Token: 0x04003E61 RID: 15969
		public const byte CrystalInvasionStart = 113;

		// Token: 0x04003E62 RID: 15970
		public const byte CrystalInvasionWipeAllTheThingsss = 114;

		// Token: 0x04003E63 RID: 15971
		public const byte MinionAttackTargetUpdate = 115;

		// Token: 0x04003E64 RID: 15972
		public const byte CrystalInvasionSendWaitTime = 116;

		// Token: 0x04003E65 RID: 15973
		public const byte PlayerHurtV2 = 117;

		// Token: 0x04003E66 RID: 15974
		public const byte PlayerDeathV2 = 118;

		/// <summary>
		/// Sends a <see cref="T:Terraria.CombatText" /> from the server to clients. Sends a string, use <see cref="F:Terraria.ID.MessageID.CombatTextInt" /> instead for the int version. Used automatically by <see cref="M:Terraria.Player.HealEffect(System.Int32,System.Boolean)" />, but custom <see cref="T:Terraria.CombatText" /> would need to manually sync using this message.
		/// <para /> <c>NetMessage.SendData</c> parameters, correlating to <see cref="M:Terraria.CombatText.NewText(Microsoft.Xna.Framework.Rectangle,Microsoft.Xna.Framework.Color,System.String,System.Boolean,System.Boolean)" /> parameters:
		/// <br /><b>number:</b> The text color: <see cref="P:Microsoft.Xna.Framework.Color.PackedValue" />
		/// <br /><b>number2:</b> The CombatText center X coordinate
		/// <br /><b>number3:</b> The CombatText center Y coordinate
		/// <br /><b>text:</b> The text shown by the CombatText
		/// </summary>
		// Token: 0x04003E67 RID: 15975
		public const byte CombatTextString = 119;

		// Token: 0x04003E68 RID: 15976
		public const byte Emoji = 120;

		// Token: 0x04003E69 RID: 15977
		public const byte TEDisplayDollItemSync = 121;

		// Token: 0x04003E6A RID: 15978
		public const byte RequestTileEntityInteraction = 122;

		// Token: 0x04003E6B RID: 15979
		public const byte WeaponsRackTryPlacing = 123;

		// Token: 0x04003E6C RID: 15980
		public const byte TEHatRackItemSync = 124;

		// Token: 0x04003E6D RID: 15981
		public const byte SyncTilePicking = 125;

		// Token: 0x04003E6E RID: 15982
		public const byte SyncRevengeMarker = 126;

		// Token: 0x04003E6F RID: 15983
		public const byte RemoveRevengeMarker = 127;

		// Token: 0x04003E70 RID: 15984
		public const byte LandGolfBallInCup = 128;

		// Token: 0x04003E71 RID: 15985
		public const byte FinishedConnectingToServer = 129;

		// Token: 0x04003E72 RID: 15986
		public const byte FishOutNPC = 130;

		// Token: 0x04003E73 RID: 15987
		public const byte TamperWithNPC = 131;

		// Token: 0x04003E74 RID: 15988
		public const byte PlayLegacySound = 132;

		// Token: 0x04003E75 RID: 15989
		public const byte FoodPlatterTryPlacing = 133;

		// Token: 0x04003E76 RID: 15990
		public const byte UpdatePlayerLuckFactors = 134;

		// Token: 0x04003E77 RID: 15991
		public const byte DeadPlayer = 135;

		// Token: 0x04003E78 RID: 15992
		public const byte SyncCavernMonsterType = 136;

		// Token: 0x04003E79 RID: 15993
		public const byte RequestNPCBuffRemoval = 137;

		// Token: 0x04003E7A RID: 15994
		public const byte ClientSyncedInventory = 138;

		// Token: 0x04003E7B RID: 15995
		public const byte SetCountsAsHostForGameplay = 139;

		// Token: 0x04003E7C RID: 15996
		public const byte SetMiscEventValues = 140;

		// Token: 0x04003E7D RID: 15997
		public const byte RequestLucyPopup = 141;

		// Token: 0x04003E7E RID: 15998
		public const byte SyncProjectileTrackers = 142;

		// Token: 0x04003E7F RID: 15999
		public const byte CrystalInvasionRequestedToSkipWaitTime = 143;

		// Token: 0x04003E80 RID: 16000
		public const byte RequestQuestEffect = 144;

		/// <summary>
		/// Exactly the same as <see cref="F:Terraria.ID.MessageID.SyncItem" />, but also syncs Item.shimmered and Item.shimmerTime. Used when an item is transformed in shimmer.
		/// <br /><br /> <see cref="F:Terraria.ID.MessageID.SyncItem" /> docs:
		/// <br /><br /> <inheritdoc cref="F:Terraria.ID.MessageID.SyncItem" />
		/// </summary>
		// Token: 0x04003E81 RID: 16001
		public const byte SyncItemsWithShimmer = 145;

		// Token: 0x04003E82 RID: 16002
		public const byte ShimmerActions = 146;

		// Token: 0x04003E83 RID: 16003
		public const byte SyncLoadout = 147;

		/// <summary>
		/// Exactly the same as <see cref="F:Terraria.ID.MessageID.SyncItem" />, but also syncs Item.timeLeftInWhichTheItemCannotBeTakenByEnemies. Used by the Lucky Coin OnHit effect.
		/// <br /><br /> <see cref="F:Terraria.ID.MessageID.SyncItem" /> docs:
		/// <br /><br /> <inheritdoc cref="F:Terraria.ID.MessageID.SyncItem" />
		/// </summary>
		// Token: 0x04003E84 RID: 16004
		public const byte SyncItemCannotBeTakenByEnemies = 148;

		// Token: 0x04003E85 RID: 16005
		public static readonly byte Count = 149;

		/// <summary>
		/// Sent by Clients who wish to change ConfigScope.ServerSide ModConfigs. Clients send Modname, configname, broadcast, and json string.
		/// <br /><br /> Server determines if ModConfig.ReloadRequired and ModConfig.ShouldAcceptClientChanges. Replies with ShouldAcceptClientChanges message if rejected.
		/// <br /><br /> Client receives bool success, message, modname, configname, broadcast, requestor player, if success additionally json, and applies them locally.
		/// </summary>
		// Token: 0x04003E86 RID: 16006
		public const byte InGameChangeConfig = 249;

		/// <summary>
		/// Contains a netID followed by custom data sent by mods
		/// <br /> Special case netID == -1, is sent by the server in response to SyncMods and contains the netIDs of every non-server only mod
		/// <br /> NetIDs will be sent for no-sync mods, and packets will be ignored if the mod is not installed on the client
		/// </summary>
		// Token: 0x04003E87 RID: 16007
		public const byte ModPacket = 250;

		/// <summary>
		/// Sent instead of LoadPlayer for non-vanilla clients
		/// <br /> - value of ModNet.AllowVanillaClients is synchronized for common net spec
		/// <br /> - list of all mods loaded on the server with side == Both {name, version, hash, isBrowserSigned}
		/// <br /> The client then enables/disables mods to ensure a matching mod set
		/// <br /> If the client is missing a mod, or has a different hash, it sends ModFile with the name of the mod
		/// <br /> If mod downloading is disabled, or only signed mods are accepted, and the given mod isn't signed, an error message is displayed
		/// <br /> If there are no mods to be downloaded, a reload may be performed if necessary, and then the client returns SyncMods
		/// <br /> when the server receives SyncMods, it sends ModPacket with the netIDs and then LoadPlayer
		/// </summary>
		// Token: 0x04003E88 RID: 16008
		public const byte SyncMods = 251;

		/// <summary>
		/// The server receives the name of one of the mods sent in SyncMods
		/// <br /><br /> Sends one packet containing the display name and length, then a series of packets containing up to 64k bytes containing the contents of the file
		/// <br /><br /> Client displays the downloading mod UI when it receives the first packet with display name and length
		/// </summary> Once the file is downloaded, the client either sends a request for the next file, or reloads and sends SyncMods
		// Token: 0x04003E89 RID: 16009
		public const byte ModFile = 252;

		/// <summary> Sent periodically while mods are reloading to keep connection alive. Default timeout is 2 minutes, which a large modpack might need to reload. </summary>
		// Token: 0x04003E8A RID: 16010
		public const byte KeepAliveDuringModReload = 253;

		// Token: 0x04003E8B RID: 16011
		public static readonly IdDictionary Search = IdDictionary.Create<MessageID, byte>();
	}
}
