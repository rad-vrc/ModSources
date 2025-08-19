using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.ObjectInteractions;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Map;
using Terraria.ObjectData;
using Terraria.UI;

namespace Terraria.ModLoader
{
	/// <summary>
	/// Extension to <seealso cref="T:Terraria.ModLoader.ModTile" /> that streamlines the process of creating a
	/// modded Pylon. Has all of ModTile's hooks for customization, but additional hooks for
	/// Pylon functionality.
	/// </summary>
	/// <remarks>
	/// One of the key features of this class is the <b>ValidTeleportCheck</b> process. At first glance it can look a bit
	/// messy, however here is a rough break-down of the call process to help you if you're lost:
	/// <br></br>
	/// 1) Game queries if the specified player is near a Pylon (<seealso cref="M:Terraria.GameContent.TeleportPylonsSystem.IsPlayerNearAPylon(Terraria.Player)" />)
	/// <br></br>
	/// 2) Assuming Step 1 has passed, game queries if the DESTINATION PYLON (the pylon the player CLICKED on the map) has enough NPCs nearby (NPCCount step)
	/// <br></br>
	/// 3) Assuming Step 2 has passed, game queries if there is ANY DANGER at ALL across the entire map, ignoring the lunar pillar event (AnyDanger step)
	/// <br></br>
	/// 4) Assuming Step 3 has passed, game queries if the DESTINATION PYLON is in the Lihzahrd Temple before Plantera is defeated.
	/// <br></br>
	/// 5) Assuming Step 4 has passed, game queries if the DESTINATION PYLON meets its biome specifications for whatever type of pylon it is (BiomeRequirements step)
	/// <br></br>
	/// 6) Regardless of all the past checks, if the DESTINATION PYLON is a modded one, <seealso cref="M:Terraria.ModLoader.ModPylon.ValidTeleportCheck_DestinationPostCheck(Terraria.GameContent.TeleportPylonInfo,System.Boolean@,System.String@)" /> is called on it.
	/// <br></br>
	/// 7) The game queries all pylons on the map and checks if any of them are in interaction distance with the player (<seealso cref="M:Terraria.Player.InInteractionRange(System.Int32,System.Int32,Terraria.DataStructures.TileReachCheckSettings)" />), and if so, checks Step 2 on it. If Step 2 passes, Step 5 is then called on it as well (NPCCount &amp; BiomeRequirements step).
	/// If Step 5 also passes, the loop breaks and no further pylons are checked, and for the next steps, the pylon that succeeded will be the designated NEARBY PYLON.
	/// <br></br>
	/// 8) Regardless of all the past checks, if the designated NEARBY PYLON is a modded one, <seealso cref="M:Terraria.ModLoader.ModPylon.ValidTeleportCheck_NearbyPostCheck(Terraria.GameContent.TeleportPylonInfo,System.Boolean@,System.Boolean@,System.String@)" /> is called on it.
	/// <br></br>
	/// 9) Any <seealso cref="T:Terraria.ModLoader.GlobalPylon" /> instances run <seealso cref="M:Terraria.ModLoader.GlobalPylon.PostValidTeleportCheck(Terraria.GameContent.TeleportPylonInfo,Terraria.GameContent.TeleportPylonInfo,System.Boolean@,System.Boolean@,System.String@)" />.
	/// <br></br>
	/// 10) Finally, if all previous checks pass AND the DESTINATION pylon is a modded one, <seealso cref="M:Terraria.ModLoader.ModPylon.ModifyTeleportationPosition(Terraria.GameContent.TeleportPylonInfo,Microsoft.Xna.Framework.Vector2@)" /> is called on it, right before the player is teleported.
	/// </remarks>
	// Token: 0x020001C5 RID: 453
	public abstract class ModPylon : ModTile
	{
		/// <summary>
		/// What type of Pylon this ModPylon represents.
		/// </summary>
		/// <remarks>
		/// The TeleportPylonType enum only has string names up until Count (9). The very first modded pylon to be added will
		/// technically be accessible with the enum type of "Count" since that value isn't an actual "type" of pylon, and modded
		/// pylons are assigned IDs starting with the Count value (9). All other modded pylons added after 9 (i.e 10+) will have no
		/// enum name, and will only every be referred to by their number values.
		/// </remarks>
		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06002399 RID: 9113 RVA: 0x004E8887 File Offset: 0x004E6A87
		// (set) Token: 0x0600239A RID: 9114 RVA: 0x004E888F File Offset: 0x004E6A8F
		public TeleportPylonType PylonType { get; internal set; }

		/// <summary>
		/// Whether or not this Pylon can even be placed.
		/// By default, it returns false if a Pylon of this type already exists in the world,
		/// otherwise true. If you want to allow an infinite amount of these pylons to be placed,
		/// simply always return true.
		/// </summary>
		/// <remarks>
		/// Note that in Multiplayer environments, granted that any GlobalPylon instances do not return false in <seealso cref="M:Terraria.ModLoader.GlobalPylon.PreCanPlacePylon(System.Int32,System.Int32,System.Int32,Terraria.GameContent.TeleportPylonType)" />,
		/// this is called first on the client, and then is subsequently called &amp; double checked on the server.
		/// <br>If the server disagrees with the client that the given pylon CANNOT be placed for any given reason, the server will reject the placement
		/// and subsequently break the associated tile.</br>
		/// </remarks>
		// Token: 0x0600239B RID: 9115 RVA: 0x004E8898 File Offset: 0x004E6A98
		public virtual bool CanPlacePylon()
		{
			return !Main.PylonSystem.HasPylonOfType(this.PylonType);
		}

		/// <summary>
		/// Creates the npc shop entry which will be registered to the shops of all NPCs which can sell pylons. <br />
		/// Override this to change the sold item type, or alter the conditions of sale. <br />
		/// Return null to prevent automatically registering this pylon in shops. <br />
		/// By default, the pylon will be sold in all shops when the provided conditions are met, if the pylon has a non-zero item drop.<br />
		/// <br />
		/// The standard pylon conditions are <see cref="F:Terraria.Condition.HappyEnoughToSellPylons" />, <see cref="F:Terraria.Condition.AnotherTownNPCNearby" />, <see cref="F:Terraria.Condition.NotInEvilBiome" />
		/// </summary>
		// Token: 0x0600239C RID: 9116 RVA: 0x004E88B0 File Offset: 0x004E6AB0
		public virtual NPCShop.Entry GetNPCShopEntry()
		{
			int drop = TileLoader.GetItemDropFromTypeAndStyle((int)base.Type, 0);
			if (drop == 0)
			{
				return null;
			}
			return new NPCShop.Entry(drop, new Condition[]
			{
				Condition.HappyEnoughToSellPylons,
				Condition.AnotherTownNPCNearby,
				Condition.NotInEvilBiome
			});
		}

		/// <summary>
		/// Step 1 of the ValidTeleportCheck process. This is the first vanilla check that is called when
		/// checking both the destination pylon and any possible nearby pylons. This check should be where you check
		/// how many NPCs are nearby, returning false if the Pylon does not satisfy the conditions.
		/// By default, returns true if there are 2 or more NPCs nearby.
		/// </summary>
		/// <remarks>
		/// Note that it's important you put the right checks in the right ValidTeleportCheck step,
		/// as whatever one returns false (if any) will determine the error message sent to the player.
		/// <br></br>
		/// <b> If you're confused about the order of which the ValidTeleportCheck methods are called, check out the XML summary
		/// on the ModPylon class.</b>
		/// </remarks>
		/// <param name="pylonInfo"> The internal information pertaining to the current pylon being teleported to or from. </param>
		/// <param name="defaultNecessaryNPCCount"> The default amount of NPCs nearby required to satisfy a VANILLA pylon. </param>
		// Token: 0x0600239D RID: 9117 RVA: 0x004E88F3 File Offset: 0x004E6AF3
		public virtual bool ValidTeleportCheck_NPCCount(TeleportPylonInfo pylonInfo, int defaultNecessaryNPCCount)
		{
			return TeleportPylonsSystem.DoesPositionHaveEnoughNPCs(defaultNecessaryNPCCount, pylonInfo.PositionInTiles);
		}

		/// <summary>
		/// Step 2 of the ValidTeleportCheck process. This is the second vanilla check that is called when
		/// checking the destination pylon. This check should be where you check
		/// if there is any "Danger" nearby, such as bosses or if there is an event happening.
		/// It is unlikely you will need to use this.
		/// By default, returns true if there are not any events happening (Lunar Pillars do not count)
		/// and there are no bosses currently alive.
		/// </summary>
		/// <remarks>
		/// Note that it's important you put the right checks in the right ValidTeleportCheck step,
		/// as whatever one returns false (if any) will determine the error message sent to the player.
		/// <br></br>
		/// <b> If you're confused about the order of which the ValidTeleportCheck methods are called, check out the XML summary
		/// on the ModPylon class.</b>
		/// </remarks>
		/// <param name="pylonInfo"> The internal information pertaining to the current pylon being teleported TO. </param>
		// Token: 0x0600239E RID: 9118 RVA: 0x004E8901 File Offset: 0x004E6B01
		public virtual bool ValidTeleportCheck_AnyDanger(TeleportPylonInfo pylonInfo)
		{
			return !NPC.AnyDanger(false, true);
		}

		/// <summary>
		/// Step 3 of the ValidTeleportCheck process. This is the fourth vanilla check that is called when
		/// checking both the destination pylon and any possible nearby pylons. This check should be where you check biome related
		/// things, such as the simple check of whether or not the Pylon is in the proper biome.
		/// By default, returns true.
		/// </summary>
		/// <remarks>
		/// Note that it's important you put the right checks in the right ValidTeleportCheck step,
		/// as whatever one returns false (if any) will determine the error message sent to the player.
		/// <br></br>
		/// <b> If you're confused about the order of which the ValidTeleportCheck methods are called, check out the XML summary
		/// on the ModPylon class.</b>
		/// </remarks>
		/// <param name="pylonInfo"> The internal information pertaining to the current pylon being teleported to or from. </param>
		/// <param name="sceneData"> The scene metrics data AT THE LOCATION of the destination pylon, NOT the player. </param>
		// Token: 0x0600239F RID: 9119 RVA: 0x004E890D File Offset: 0x004E6B0D
		public virtual bool ValidTeleportCheck_BiomeRequirements(TeleportPylonInfo pylonInfo, SceneMetrics sceneData)
		{
			return true;
		}

		/// <summary>
		/// The 4th check of the ValidTeleportCheck process. This check is for modded Pylons only, called after
		/// ALL other checks have completed pertaining the pylon clicked on the map (the destination pylon), but before
		/// any nearby pylon information is calculated. This is where you an do custom checks that don't pertain to the past destination checks,
		/// as well as customize the localization key to give custom messages to the player on teleportation failure. By default, does nothing.
		/// <br></br>
		/// <b> If you're confused about the order of which the ValidTeleportCheck methods are called, check out the XML summary
		/// on the ModPylon class.</b>
		/// </summary>
		/// <param name="destinationPylonInfo"> The Pylon information for the Pylon that the player is attempt to teleport to. </param>
		/// <param name="destinationPylonValid"> Whether or not after all of the checks, the destination Pylon is valid. </param>
		/// <param name="errorKey"> The localization key for the message sent to the player if destinationPylonValid is false. </param>
		// Token: 0x060023A0 RID: 9120 RVA: 0x004E8910 File Offset: 0x004E6B10
		public virtual void ValidTeleportCheck_DestinationPostCheck(TeleportPylonInfo destinationPylonInfo, ref bool destinationPylonValid, ref string errorKey)
		{
		}

		/// <summary>
		/// The 5th and final check of the ValidTeleportCheck process. This check is for modded Pylons only, called after
		/// ALL other checks have completed for the destination pylon and all normal checks have taken place for the nearby
		/// pylon, if applicable. This is where you can do custom checks that don't pertain to the past nearby pylon checks,
		/// as well as customize the localization key to give custom messages to the player on teleportation failure. By default, does nothing.
		/// <br></br>
		/// <b> If you're confused about the order of which the ValidTeleportCheck methods are called, check out the XML summary
		/// on the ModPylon class.</b>
		/// </summary>
		/// <param name="nearbyPylonInfo">
		/// The pylon information of the pylon the player in question is standing NEAR. This always has a value.
		/// </param>
		/// <param name="destinationPylonValid"> Whether or not after all of the checks, the destination Pylon is valid. </param>
		/// <param name="anyNearbyValidPylon"> Whether or not after all of the checks, there is a Pylon nearby to the player that is valid. </param>
		/// <param name="errorKey"> The localization key for the message sent to the player if destinationPylonValid is false. </param>
		// Token: 0x060023A1 RID: 9121 RVA: 0x004E8912 File Offset: 0x004E6B12
		public virtual void ValidTeleportCheck_NearbyPostCheck(TeleportPylonInfo nearbyPylonInfo, ref bool destinationPylonValid, ref bool anyNearbyValidPylon, ref string errorKey)
		{
		}

		/// <summary>
		/// Called right BEFORE the teleportation of the player occurs, when all checks succeed during the ValidTeleportCheck process. Allows the modification
		/// of where the player ends up when the teleportation takes place. Remember that the teleport location is in WORLD coordinates, not tile coordinates.
		/// </summary>
		/// <remarks>
		/// You shouldn't need to use this method if your pylon is the same size as a normal vanilla pylons (3x4 tiles).
		/// </remarks>
		/// <param name="destinationPylonInfo"> The information of the pylon the player intends to teleport to. </param>
		/// <param name="teleportationPosition"> The position (IN WORLD COORDINATES) of where the player ends up when the teleportation occurs. </param>
		// Token: 0x060023A2 RID: 9122 RVA: 0x004E8914 File Offset: 0x004E6B14
		public virtual void ModifyTeleportationPosition(TeleportPylonInfo destinationPylonInfo, ref Vector2 teleportationPosition)
		{
		}

		/// <summary>
		/// Called when the map is visible, in order to draw the passed in Pylon on the map.
		/// In order to draw on the map, you must use <seealso cref="T:Terraria.Map.MapOverlayDrawContext" />'s Draw Method. By default, doesn't draw anything.
		/// </summary>
		/// <param name="context"> The current map context on which you can draw. </param>
		/// <param name="mouseOverText"> The text that will overlay on the mouse when the icon is being hovered over. </param>
		/// <param name="pylonInfo"> The pylon that is currently needing its icon to be drawn. </param>
		/// <param name="isNearPylon"> Whether or not the player is currently near a pylon. </param>
		/// <param name="drawColor"> The draw color of the icon. This is bright white when the player is near a Pylon, but gray and translucent otherwise. </param>
		/// <param name="deselectedScale"> The scale of the icon if it is NOT currently being hovered over. In vanilla, this is 1f, or 100%. </param>
		/// <param name="selectedScale"> The scale of the icon if it IS currently being over. In vanilla, this is 2f, or 200%. </param>
		// Token: 0x060023A3 RID: 9123 RVA: 0x004E8916 File Offset: 0x004E6B16
		public virtual void DrawMapIcon(ref MapOverlayDrawContext context, ref string mouseOverText, TeleportPylonInfo pylonInfo, bool isNearPylon, Color drawColor, float deselectedScale, float selectedScale)
		{
		}

		// Token: 0x060023A4 RID: 9124 RVA: 0x004E8918 File Offset: 0x004E6B18
		public unsafe override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
		{
			if (WorldGen.destroyObject)
			{
				return false;
			}
			Tile tileSafely = Framing.GetTileSafely(i, j);
			TileObjectData tileData = TileObjectData.GetTileData(tileSafely);
			bool shouldBreak = false;
			int topLeftX = i - (int)(*tileSafely.frameX) / tileData.CoordinateWidth % tileData.Width;
			int topLeftY = j - (int)(*tileSafely.frameY / 18) % tileData.Height;
			int rightX = topLeftX + tileData.Width;
			int bottomY = topLeftY + tileData.Height;
			for (int x = topLeftX; x < rightX; x++)
			{
				for (int y = topLeftY; y < bottomY; y++)
				{
					Tile tile = Main.tile[x, y];
					if (!tile.HasTile || *tile.type != base.Type)
					{
						shouldBreak = true;
						break;
					}
				}
			}
			for (int x2 = topLeftX; x2 < rightX; x2++)
			{
				if (!WorldGen.SolidTileAllowBottomSlope(x2, bottomY))
				{
					shouldBreak = true;
					break;
				}
			}
			if (!shouldBreak)
			{
				noBreak = true;
				return true;
			}
			WorldGen.KillTile_DropItems(i, j, tileSafely, true, true);
			this.KillMultiTile(topLeftX, topLeftY, (int)(*tileSafely.TileFrameX), (int)(*tileSafely.TileFrameY));
			WorldGen.destroyObject = true;
			for (int x3 = topLeftX; x3 < rightX; x3++)
			{
				for (int y2 = topLeftY; y2 < bottomY; y2++)
				{
					Tile tile2 = Main.tile[x3, y2];
					if (tile2.HasTile && *tile2.TileType == base.Type)
					{
						WorldGen.KillTile(x3, y2, false, false, false);
					}
				}
			}
			WorldGen.destroyObject = false;
			return true;
		}

		// Token: 0x060023A5 RID: 9125 RVA: 0x004E8A87 File Offset: 0x004E6C87
		public override bool RightClick(int i, int j)
		{
			Main.LocalPlayer.TryOpeningFullscreenMap();
			return true;
		}

		// Token: 0x060023A6 RID: 9126 RVA: 0x004E8A94 File Offset: 0x004E6C94
		public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
		{
			return true;
		}

		// Token: 0x060023A7 RID: 9127 RVA: 0x004E8A97 File Offset: 0x004E6C97
		public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
		{
			if ((int)drawData.tileFrameX % TileObjectData.GetTileData(drawData.tileCache).CoordinateFullWidth == 0 && drawData.tileFrameY == 0)
			{
				Main.instance.TilesRenderer.AddSpecialLegacyPoint(i, j);
			}
		}

		// Token: 0x060023A8 RID: 9128 RVA: 0x004E8AD4 File Offset: 0x004E6CD4
		[Obsolete("Parameters have changed; parameters crystalDrawColor, frameHeight, and crystalHorizontalFrameCount no longer exist. There are 5 new parameters: crystalHighlightTexture, crystalOffset, pylonShadowColor, dustColor, and dustChanceDenominator.", true)]
		public void DefaultDrawPylonCrystal(SpriteBatch spriteBatch, int i, int j, Asset<Texture2D> crystalTexture, Color crystalDrawColor, int frameHeight, int crystalHorizontalFrameCount, int crystalVerticalFrameCount)
		{
			this.DefaultDrawPylonCrystal(spriteBatch, i, j, crystalTexture, crystalTexture, new Vector2(0f, -12f), Color.White * 0.1f, Color.White, 4, crystalVerticalFrameCount);
		}

		/// <summary>
		/// Draws the passed in pylon crystal texture the exact way that vanilla draws it. This MUST be called in SpecialDraw in order
		/// to function properly.
		/// </summary>
		/// <param name="spriteBatch"> The sprite batch that will draw the crystal. </param>
		/// <param name="i"> The X tile coordinate to start drawing from. </param>
		/// <param name="j"> The Y tile coordinate to start drawing from. </param>
		/// <param name="crystalTexture"> The texture of the crystal that will actually be drawn. </param>
		/// <param name="crystalHighlightTexture"> The texture of the smart cursor highlight for the corresponding crystal texture. </param>
		/// <param name="crystalOffset">
		/// The offset of the actual position of the crystal. Assuming that a pylon tile itself and the crystals are equivalent to
		/// vanilla's sizes, this value should be Vector2(0, -12).
		/// </param>
		/// <param name="pylonShadowColor"> The color of the "shadow" that is drawn on top of the crystal texture. </param>
		/// <param name="dustColor"> The color of the dust that emanates from the crystal. </param>
		/// <param name="dustChanceDenominator"> Every draw call, this is this the denominator value of a Main.rand.NextBool() (1/denominator chance) check for whether or not a dust particle will spawn. 4 is the value vanilla uses. </param>
		/// <param name="crystalVerticalFrameCount"> How many vertical frames the crystal texture has. </param>
		// Token: 0x060023A9 RID: 9129 RVA: 0x004E8B14 File Offset: 0x004E6D14
		public void DefaultDrawPylonCrystal(SpriteBatch spriteBatch, int i, int j, Asset<Texture2D> crystalTexture, Asset<Texture2D> crystalHighlightTexture, Vector2 crystalOffset, Color pylonShadowColor, Color dustColor, int dustChanceDenominator, int crystalVerticalFrameCount)
		{
			Vector2 offscreenVector;
			offscreenVector..ctor((float)Main.offScreenRange);
			if (Main.drawToScreen)
			{
				offscreenVector = Vector2.Zero;
			}
			Point point;
			point..ctor(i, j);
			Tile tile = Main.tile[point.X, point.Y];
			if (tile == null || !tile.HasTile)
			{
				return;
			}
			TileObjectData tileData = TileObjectData.GetTileData(tile);
			int frameY = Main.tileFrameCounter[597] / crystalVerticalFrameCount;
			Rectangle crystalFrame = crystalTexture.Frame(1, crystalVerticalFrameCount, 0, frameY, 0, 0);
			Rectangle smartCursorGlowFrame = crystalHighlightTexture.Frame(1, crystalVerticalFrameCount, 0, frameY, 0, 0);
			crystalFrame.Height--;
			smartCursorGlowFrame.Height--;
			Vector2 origin = crystalFrame.Size() / 2f;
			Vector2 tileOrigin;
			tileOrigin..ctor((float)tileData.CoordinateFullWidth / 2f, (float)tileData.CoordinateFullHeight / 2f);
			Vector2 crystalPosition = point.ToWorldCoordinates(tileOrigin.X - 2f, tileOrigin.Y) + crystalOffset;
			float sinusoidalOffset = (float)Math.Sin((double)Main.GlobalTimeWrappedHourly * 6.283185307179586 / 5.0);
			Vector2 drawingPosition = crystalPosition + offscreenVector + new Vector2(0f, sinusoidalOffset * 4f);
			if (!Main.gamePaused && Main.instance.IsActive && (!Lighting.UpdateEveryFrame || Main.rand.NextBool(4)) && Main.rand.NextBool(dustChanceDenominator))
			{
				Rectangle dustBox = Utils.CenteredRectangle(crystalPosition, crystalFrame.Size());
				int numForDust = Dust.NewDust(dustBox.TopLeft(), dustBox.Width, dustBox.Height, 43, 0f, 0f, 254, dustColor, 0.5f);
				Main.dust[numForDust].velocity *= 0.1f;
				Dust dust = Main.dust[numForDust];
				dust.velocity.Y = dust.velocity.Y - 0.2f;
			}
			Color color = Lighting.GetColor(point.X, point.Y);
			color = Color.Lerp(color, Color.White, 0.8f);
			spriteBatch.Draw(crystalTexture.Value, drawingPosition - Main.screenPosition, new Rectangle?(crystalFrame), color * 0.7f, 0f, origin, 1f, 0, 0f);
			float scale = (float)Math.Sin((double)(Main.GlobalTimeWrappedHourly * 6.2831855f / 1f)) * 0.2f + 0.8f;
			Color shadowColor = pylonShadowColor * scale;
			for (float shadowPos = 0f; shadowPos < 1f; shadowPos += 0.16666667f)
			{
				spriteBatch.Draw(crystalTexture.Value, drawingPosition - Main.screenPosition + (6.2831855f * shadowPos).ToRotationVector2() * (6f + sinusoidalOffset * 2f), new Rectangle?(crystalFrame), shadowColor, 0f, origin, 1f, 0, 0f);
			}
			int selectionLevel = 0;
			bool actuallySelected;
			if (Main.InSmartCursorHighlightArea(point.X, point.Y, out actuallySelected))
			{
				selectionLevel = 1;
				if (actuallySelected)
				{
					selectionLevel = 2;
				}
			}
			if (selectionLevel == 0)
			{
				return;
			}
			int averageBrightness = (int)((color.R + color.G + color.B) / 3);
			if (averageBrightness <= 10)
			{
				return;
			}
			Color selectionGlowColor = Colors.GetSelectionGlowColor(selectionLevel == 2, averageBrightness);
			spriteBatch.Draw(crystalHighlightTexture.Value, drawingPosition - Main.screenPosition, new Rectangle?(smartCursorGlowFrame), selectionGlowColor, 0f, origin, 1f, 0, 0f);
		}

		/// <summary>
		/// Draws the passed in map icon texture for pylons the exact way that vanilla would draw it. Note that this method
		/// assumes that the texture is NOT framed, i.e there is only a single sprite that is not animated.
		/// Returns whether or not the player is currently hovering over the icon.
		/// </summary>
		/// <param name="context"> The draw context that will allow for drawing on thj</param>
		/// <param name="mapIcon"> The icon that is to be drawn on the map. </param>
		/// <param name="drawCenter"> The position in TILE coordinates for where the CENTER of the map icon should be. </param>
		/// <param name="drawColor"> The color to draw the icon as. </param>
		/// <param name="deselectedScale"> The scale to draw the map icon when it is not selected (not being hovered over). </param>
		/// <param name="selectedScale"> The scale to draw the map icon when it IS selected (being hovered over). </param>
		// Token: 0x060023AA RID: 9130 RVA: 0x004E8EA8 File Offset: 0x004E70A8
		public bool DefaultDrawMapIcon(ref MapOverlayDrawContext context, Asset<Texture2D> mapIcon, Vector2 drawCenter, Color drawColor, float deselectedScale, float selectedScale)
		{
			return context.Draw(mapIcon.Value, drawCenter, drawColor, new SpriteFrame(1, 1, 0, 0), deselectedScale, selectedScale, Alignment.Center).IsMouseOver;
		}

		/// <summary>
		/// Handles mouse clicking on the map icon the exact way that vanilla handles it. In normal circumstances, this should be called
		/// directly after DefaultDrawMapIcon.
		/// </summary>
		/// <param name="mouseIsHovering"> Whether or not the map icon is currently being hovered over. </param>
		/// <param name="pylonInfo"> The information pertaining to the current pylon being drawn. </param>
		/// <param name="hoveringTextKey">
		/// The localization key that will be used to display text on the mouse, granted the mouse is currently hovering over the map icon.
		/// </param>
		/// <param name="mouseOverText"> The reference to the string value that actually changes the mouse text value. </param>
		// Token: 0x060023AB RID: 9131 RVA: 0x004E8ED0 File Offset: 0x004E70D0
		public void DefaultMapClickHandle(bool mouseIsHovering, TeleportPylonInfo pylonInfo, string hoveringTextKey, ref string mouseOverText)
		{
			if (!mouseIsHovering)
			{
				return;
			}
			Main.cancelWormHole = true;
			mouseOverText = Language.GetTextValue(hoveringTextKey);
			if (Main.mouseLeft && Main.mouseLeftRelease)
			{
				Main.mouseLeftRelease = false;
				Main.mapFullscreen = false;
				PlayerInput.LockGamepadButtons("MouseLeft");
				Main.PylonSystem.RequestTeleportation(pylonInfo, Main.LocalPlayer);
			}
		}
	}
}
