using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.Default;
using Terraria.UI;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This serves as a central place to store equipment slots and their corresponding textures. You will use this to obtain the IDs for your equipment textures.
	/// </summary>
	// Token: 0x02000138 RID: 312
	public class AccessorySlotLoader : Loader<ModAccessorySlot>
	{
		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06001A84 RID: 6788 RVA: 0x004CBF3B File Offset: 0x004CA13B
		private static Player Player
		{
			get
			{
				return Main.LocalPlayer;
			}
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x004CBF42 File Offset: 0x004CA142
		internal static ModAccessorySlotPlayer ModSlotPlayer(Player player)
		{
			return player.GetModPlayer<ModAccessorySlotPlayer>();
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x004CBF4A File Offset: 0x004CA14A
		public AccessorySlotLoader()
		{
			base.Initialize(0);
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x004CBF59 File Offset: 0x004CA159
		private ModAccessorySlot GetIdCorrected(int id)
		{
			if (id < this.list.Count)
			{
				return this.list[id];
			}
			return new UnloadedAccessorySlot(id, "TEMP'd");
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x004CBF81 File Offset: 0x004CA181
		public ModAccessorySlot Get(int id, Player player)
		{
			return this.GetIdCorrected(id % AccessorySlotLoader.ModSlotPlayer(player).SlotCount);
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x004CBF96 File Offset: 0x004CA196
		public new ModAccessorySlot Get(int id)
		{
			return this.Get(id, AccessorySlotLoader.Player);
		}

		// Token: 0x06001A8A RID: 6794 RVA: 0x004CBFA4 File Offset: 0x004CA1A4
		internal int GetAccessorySlotPerColumn()
		{
			float minimumClearance = (float)AccessorySlotLoader.DrawVerticalAlignment + 112f * Main.inventoryScale + 4f;
			return (int)(((float)Main.screenHeight - minimumClearance) / (56f * Main.inventoryScale) - 1.8f);
		}

		/// <summary>
		/// The variable known as num20 used to align all equipment slot drawing in Main.
		/// Represents the y position where equipment slots start to be drawn from.
		/// </summary>
		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06001A8B RID: 6795 RVA: 0x004CBFE5 File Offset: 0x004CA1E5
		// (set) Token: 0x06001A8C RID: 6796 RVA: 0x004CBFEC File Offset: 0x004CA1EC
		public static int DrawVerticalAlignment { get; private set; }

		/// <summary>
		/// The variable that determines where the DefenseIcon will be drawn, after considering all slot information.
		/// </summary>
		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06001A8D RID: 6797 RVA: 0x004CBFF4 File Offset: 0x004CA1F4
		// (set) Token: 0x06001A8E RID: 6798 RVA: 0x004CBFFB File Offset: 0x004CA1FB
		public static Vector2 DefenseIconPosition { get; private set; }

		// Token: 0x06001A8F RID: 6799 RVA: 0x004CC004 File Offset: 0x004CA204
		public void DrawAccSlots(int num20)
		{
			int skip = 0;
			AccessorySlotLoader.DrawVerticalAlignment = num20;
			Color color = Main.inventoryBack;
			for (int vanillaSlot = 3; vanillaSlot < AccessorySlotLoader.Player.dye.Length; vanillaSlot++)
			{
				if (!this.Draw(skip, false, vanillaSlot, color))
				{
					skip++;
				}
			}
			for (int modSlot = 0; modSlot < AccessorySlotLoader.ModSlotPlayer(AccessorySlotLoader.Player).SlotCount; modSlot++)
			{
				if (!this.Draw(skip, true, modSlot, color))
				{
					skip++;
				}
			}
			if (skip == 7 + AccessorySlotLoader.ModSlotPlayer(AccessorySlotLoader.Player).SlotCount)
			{
				AccessorySlotLoader.ModSlotPlayer(AccessorySlotLoader.Player).scrollbarSlotPosition = 0;
				return;
			}
			int accessoryPerColumn = this.GetAccessorySlotPerColumn();
			int slotsToRender = AccessorySlotLoader.ModSlotPlayer(AccessorySlotLoader.Player).SlotCount + 7 - skip;
			int scrollIncrement = slotsToRender - accessoryPerColumn;
			if (scrollIncrement < 0)
			{
				accessoryPerColumn = slotsToRender;
				scrollIncrement = 0;
			}
			AccessorySlotLoader.DefenseIconPosition = new Vector2((float)(Main.screenWidth - 64 - 28), (float)AccessorySlotLoader.DrawVerticalAlignment + (float)((accessoryPerColumn + 2) * 56) * Main.inventoryScale + 4f);
			if (scrollIncrement > 0)
			{
				this.DrawScrollSwitch();
				if (AccessorySlotLoader.ModSlotPlayer(AccessorySlotLoader.Player).scrollSlots)
				{
					this.DrawScrollbar(accessoryPerColumn, slotsToRender, scrollIncrement);
					return;
				}
			}
			else
			{
				AccessorySlotLoader.ModSlotPlayer(AccessorySlotLoader.Player).scrollbarSlotPosition = 0;
			}
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x004CC130 File Offset: 0x004CA330
		internal void DrawScrollSwitch()
		{
			Texture2D value4 = TextureAssets.InventoryTickOn.Value;
			if (AccessorySlotLoader.ModSlotPlayer(AccessorySlotLoader.Player).scrollSlots)
			{
				value4 = TextureAssets.InventoryTickOff.Value;
			}
			int xLoc2 = Main.screenWidth - 64 - 28 + 47 + 9;
			int yLoc2 = (int)((float)AccessorySlotLoader.DrawVerticalAlignment + 168f * Main.inventoryScale) - 10;
			Main.spriteBatch.Draw(value4, new Vector2((float)xLoc2, (float)yLoc2), Color.White * 0.7f);
			Rectangle rectangle;
			rectangle..ctor(xLoc2, yLoc2, value4.Width, value4.Height);
			if (!rectangle.Contains(new Point(Main.mouseX, Main.mouseY)) || PlayerInput.IgnoreMouseInterface)
			{
				return;
			}
			Main.LocalPlayer.mouseInterface = true;
			if (Main.mouseLeft && Main.mouseLeftRelease)
			{
				AccessorySlotLoader.ModSlotPlayer(AccessorySlotLoader.Player).scrollSlots = !AccessorySlotLoader.ModSlotPlayer(AccessorySlotLoader.Player).scrollSlots;
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}
			int num45 = (AccessorySlotLoader.ModSlotPlayer(AccessorySlotLoader.Player).scrollSlots > false) ? 1 : 0;
			Main.HoverItem = new Item();
			Main.hoverItemName = AccessorySlotLoader.scrollStackLang[num45];
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x004CC260 File Offset: 0x004CA460
		internal void DrawScrollbar(int accessoryPerColumn, int slotsToRender, int scrollIncrement)
		{
			int xLoc = Main.screenWidth - 64 - 28;
			int chkMax = (int)((float)AccessorySlotLoader.DrawVerticalAlignment + (float)((accessoryPerColumn + 3) * 56) * Main.inventoryScale) + 4;
			int chkMin = (int)((float)AccessorySlotLoader.DrawVerticalAlignment + 168f * Main.inventoryScale) + 4;
			UIScrollbar uiscrollbar = new UIScrollbar();
			Rectangle rectangle;
			rectangle..ctor(xLoc + 47 + 6, chkMin, 5, chkMax - chkMin);
			uiscrollbar.DrawBar(Main.spriteBatch, Main.Assets.Request<Texture2D>("Images/UI/Scrollbar").Value, rectangle, Color.White);
			int barSize = (chkMax - chkMin) / (scrollIncrement + 1);
			rectangle..ctor(xLoc + 47 + 5, chkMin + AccessorySlotLoader.ModSlotPlayer(AccessorySlotLoader.Player).scrollbarSlotPosition * barSize, 3, barSize);
			uiscrollbar.DrawBar(Main.spriteBatch, Main.Assets.Request<Texture2D>("Images/UI/ScrollbarInner").Value, rectangle, Color.White);
			rectangle..ctor(xLoc - 94, chkMin, 141, chkMax - chkMin);
			if (!rectangle.Contains(new Point(Main.mouseX, Main.mouseY)) || PlayerInput.IgnoreMouseInterface)
			{
				return;
			}
			PlayerInput.LockVanillaMouseScroll("ModLoader/Acc");
			int scrollDelta = AccessorySlotLoader.ModSlotPlayer(AccessorySlotLoader.Player).scrollbarSlotPosition + PlayerInput.ScrollWheelDelta / 120;
			scrollDelta = Math.Min(scrollDelta, scrollIncrement);
			scrollDelta = Math.Max(scrollDelta, 0);
			AccessorySlotLoader.ModSlotPlayer(AccessorySlotLoader.Player).scrollbarSlotPosition = scrollDelta;
			PlayerInput.ScrollWheelDelta = 0;
		}

		/// <summary>
		/// Draws Vanilla and Modded Accessory Slots
		/// </summary>
		// Token: 0x06001A92 RID: 6802 RVA: 0x004CC3B8 File Offset: 0x004CA5B8
		public bool Draw(int skip, bool modded, int slot, Color color)
		{
			bool flag4 = false;
			bool loadoutConflict = false;
			bool accessoryConflict = false;
			bool vanityConflict = false;
			bool flag5;
			if (modded)
			{
				flag5 = !this.ModdedIsItemSlotUnlockedAndUsable(slot, AccessorySlotLoader.Player);
				flag4 = !this.ModdedCanSlotBeShown(slot);
				ModAccessorySlotPlayer modAccessorySlotPlayer = AccessorySlotLoader.ModSlotPlayer(AccessorySlotLoader.Player);
				if (modAccessorySlotPlayer.SharedSlotHasLoadoutConflict(slot, false))
				{
					loadoutConflict = true;
					accessoryConflict = true;
				}
				if (modAccessorySlotPlayer.SharedSlotHasLoadoutConflict(slot, true))
				{
					loadoutConflict = true;
					vanityConflict = true;
				}
			}
			else
			{
				flag5 = !AccessorySlotLoader.Player.IsItemSlotUnlockedAndUsable(slot);
				if (slot == 8)
				{
					flag4 = (slot == 8 && !AccessorySlotLoader.Player.CanDemonHeartAccessoryBeShown());
				}
				else if (slot == 9)
				{
					flag4 = !AccessorySlotLoader.Player.CanMasterModeAccessoryBeShown();
				}
			}
			if ((flag4 && flag5 && !loadoutConflict) || (modded && this.IsHidden(slot)))
			{
				return false;
			}
			Main.inventoryBack = (flag5 ? new Color(80, 80, 80, 80) : color);
			this.slotDrawLoopCounter = 0;
			int yLoc = 0;
			int xLoc = 0;
			bool customLoc = false;
			if (modded)
			{
				ModAccessorySlot mAccSlot = this.Get(slot);
				Vector2? customLocation = mAccSlot.CustomLocation;
				customLoc = (customLocation != null);
				if (!customLoc && Main.EquipPage != 0)
				{
					Main.inventoryBack = color;
					return false;
				}
				if (customLoc)
				{
					customLocation = mAccSlot.CustomLocation;
					xLoc = (int)((customLocation != null) ? new float?(customLocation.GetValueOrDefault().X) : null).Value;
					customLocation = mAccSlot.CustomLocation;
					yLoc = (int)((customLocation != null) ? new float?(customLocation.GetValueOrDefault().Y) : null).Value;
				}
				else if (!this.SetDrawLocation(slot + AccessorySlotLoader.Player.dye.Length - 3, skip, ref xLoc, ref yLoc))
				{
					Main.inventoryBack = color;
					return true;
				}
				ModAccessorySlot modAccessorySlot = this.Get(slot);
				ModAccessorySlotPlayer modSlotPlayer = AccessorySlotLoader.ModSlotPlayer(AccessorySlotLoader.Player);
				Main.inventoryBack = ((flag5 || accessoryConflict) ? new Color(80, 80, 80, 80) : color);
				if (modAccessorySlot.DrawFunctionalSlot)
				{
					int xLoc2;
					int yLoc2;
					Texture2D value4;
					bool skipMouse = this.DrawVisibility(ref modSlotPlayer.exHideAccessory[slot], -10, xLoc, yLoc, out xLoc2, out yLoc2, out value4);
					this.DrawSlot(modSlotPlayer.exAccessorySlot, -10, slot, flag5, xLoc, yLoc, skipMouse);
					Main.spriteBatch.Draw(value4, new Vector2((float)xLoc2, (float)yLoc2), Color.White * 0.7f);
				}
				Main.inventoryBack = ((flag5 || vanityConflict) ? new Color(80, 80, 80, 80) : color);
				if (modAccessorySlot.DrawVanitySlot)
				{
					this.DrawSlot(modSlotPlayer.exAccessorySlot, -11, slot + modSlotPlayer.SlotCount, flag5, xLoc, yLoc, false);
				}
				Main.inventoryBack = (flag5 ? new Color(80, 80, 80, 80) : color);
				if (modAccessorySlot.DrawDyeSlot)
				{
					this.DrawSlot(modSlotPlayer.exDyesAccessory, -12, slot, flag5, xLoc, yLoc, false);
				}
			}
			else
			{
				if (!customLoc && Main.EquipPage != 0)
				{
					Main.inventoryBack = color;
					return false;
				}
				if (!this.SetDrawLocation(slot - 3, skip, ref xLoc, ref yLoc))
				{
					Main.inventoryBack = color;
					return true;
				}
				int xLoc3;
				int yLoc3;
				Texture2D value5;
				bool skipMouse2 = this.DrawVisibility(ref AccessorySlotLoader.Player.hideVisibleAccessory[slot], 10, xLoc, yLoc, out xLoc3, out yLoc3, out value5);
				this.DrawSlot(AccessorySlotLoader.Player.armor, 10, slot, flag5, xLoc, yLoc, skipMouse2);
				Main.spriteBatch.Draw(value5, new Vector2((float)xLoc3, (float)yLoc3), Color.White * 0.7f);
				this.DrawSlot(AccessorySlotLoader.Player.armor, 11, slot + AccessorySlotLoader.Player.dye.Length, flag5, xLoc, yLoc, false);
				this.DrawSlot(AccessorySlotLoader.Player.dye, 12, slot, flag5, xLoc, yLoc, false);
			}
			Main.inventoryBack = color;
			return !customLoc;
		}

		/// <summary>
		/// Applies Xloc and Yloc data for the slot, based on ModAccessorySlotPlayer.scrollSlots
		/// </summary>
		// Token: 0x06001A93 RID: 6803 RVA: 0x004CC758 File Offset: 0x004CA958
		internal bool SetDrawLocation(int trueSlot, int skip, ref int xLoc, ref int yLoc)
		{
			int accessoryPerColumn = this.GetAccessorySlotPerColumn();
			int xColumn = trueSlot / accessoryPerColumn;
			int yRow = trueSlot % accessoryPerColumn;
			if (AccessorySlotLoader.ModSlotPlayer(AccessorySlotLoader.Player).scrollSlots)
			{
				int row = yRow + xColumn * accessoryPerColumn - AccessorySlotLoader.ModSlotPlayer(AccessorySlotLoader.Player).scrollbarSlotPosition - skip;
				yLoc = (int)((float)AccessorySlotLoader.DrawVerticalAlignment + (float)((row + 3) * 56) * Main.inventoryScale) + 4;
				int chkMin = (int)((float)AccessorySlotLoader.DrawVerticalAlignment + 168f * Main.inventoryScale) + 4;
				int chkMax = (int)((float)AccessorySlotLoader.DrawVerticalAlignment + (float)((accessoryPerColumn - 1 + 3) * 56) * Main.inventoryScale) + 4;
				if (yLoc > chkMax || yLoc < chkMin)
				{
					return false;
				}
				xLoc = Main.screenWidth - 64 - 28;
			}
			else
			{
				int row2 = yRow;
				int col = xColumn;
				if (skip > 0)
				{
					int tempSlot = trueSlot - skip;
					row2 = tempSlot % accessoryPerColumn;
					col = tempSlot / accessoryPerColumn;
				}
				yLoc = (int)((float)AccessorySlotLoader.DrawVerticalAlignment + (float)((row2 + 3) * 56) * Main.inventoryScale) + 4;
				if (col > 0)
				{
					xLoc = Main.screenWidth - 64 - 28 - 141 * col - 50;
				}
				else
				{
					xLoc = Main.screenWidth - 64 - 28 - 141 * col;
				}
			}
			return true;
		}

		/// <summary>
		/// Is run in AccessorySlotLoader.Draw.
		/// Creates &amp; sets up Hide Visibility Button.
		/// </summary>
		// Token: 0x06001A94 RID: 6804 RVA: 0x004CC87C File Offset: 0x004CAA7C
		internal bool DrawVisibility(ref bool visbility, int context, int xLoc, int yLoc, out int xLoc2, out int yLoc2, out Texture2D value4)
		{
			yLoc2 = yLoc - 2;
			xLoc2 = xLoc - 58 + 64 + 28;
			value4 = TextureAssets.InventoryTickOn.Value;
			if (visbility)
			{
				value4 = TextureAssets.InventoryTickOff.Value;
			}
			Rectangle rectangle;
			rectangle..ctor(xLoc2, yLoc2, value4.Width, value4.Height);
			int num45 = 0;
			bool skipCheck = false;
			if (rectangle.Contains(new Point(Main.mouseX, Main.mouseY)) && !PlayerInput.IgnoreMouseInterface)
			{
				skipCheck = true;
				AccessorySlotLoader.Player.mouseInterface = true;
				if (Main.mouseLeft && Main.mouseLeftRelease)
				{
					visbility = !visbility;
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
					if (Main.netMode == 1 && context > 0)
					{
						NetMessage.SendData(4, -1, -1, null, AccessorySlotLoader.Player.whoAmI, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				num45 = ((!visbility) ? 1 : 2);
			}
			if (num45 > 0)
			{
				Main.HoverItem = new Item();
				Main.hoverItemName = Lang.inter[58 + num45].Value;
			}
			return skipCheck;
		}

		/// <summary>
		/// Is run in AccessorySlotLoader.Draw.
		/// Generates a significant amount of functionality for the slot, despite being named drawing because vanilla.
		/// At the end, calls this.DrawRedirect to enable custom drawing
		/// </summary>
		// Token: 0x06001A95 RID: 6805 RVA: 0x004CC990 File Offset: 0x004CAB90
		private void DrawSlot(Item[] items, int context, int slot, bool flag3, int xLoc, int yLoc, bool skipCheck = false)
		{
			bool flag4 = flag3 && !Main.mouseItem.IsAir;
			int num = 47;
			int num2 = this.slotDrawLoopCounter;
			this.slotDrawLoopCounter = num2 + 1;
			int xLoc2 = xLoc - num * num2;
			bool isHovered = false;
			if (!skipCheck && Main.mouseX >= xLoc2 && (float)Main.mouseX <= (float)xLoc2 + (float)TextureAssets.InventoryBack.Width() * Main.inventoryScale && Main.mouseY >= yLoc && (float)Main.mouseY <= (float)yLoc + (float)TextureAssets.InventoryBack.Height() * Main.inventoryScale && !PlayerInput.IgnoreMouseInterface)
			{
				isHovered = true;
				AccessorySlotLoader.Player.mouseInterface = true;
				Main.armorHide = true;
				ItemSlot.OverrideHover(items, Math.Abs(context), slot);
				if (!flag4)
				{
					if (Math.Abs(context) == 12)
					{
						if (Main.mouseRightRelease && Main.mouseRight)
						{
							ItemSlot.RightClick(items, context, slot);
						}
						ItemSlot.LeftClick(items, context, slot);
					}
					else if (Math.Abs(context) == 11)
					{
						ItemSlot.LeftClick(items, context, slot);
						ItemSlot.RightClick(items, context, slot);
					}
					else if (Math.Abs(context) == 10)
					{
						ItemSlot.LeftClick(items, context, slot);
					}
				}
				ItemSlot.MouseHover(items, Math.Abs(context), slot);
				if (context < 0)
				{
					this.OnHover(slot, context);
					if (Math.Abs(context) != 12 && AccessorySlotLoader.ModSlotPlayer(AccessorySlotLoader.Player).SharedSlotHasLoadoutConflict(slot, Math.Abs(context) == 11))
					{
						Main.HoverItem = new Item();
						Main.hoverItemName = Language.GetTextValue("tModLoader.SharedAccessorySlotConflictTooltip");
					}
				}
			}
			this.DrawRedirect(items, context, slot, new Vector2((float)xLoc2, (float)yLoc), isHovered);
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x004CCB1C File Offset: 0x004CAD1C
		internal void DrawRedirect(Item[] inv, int context, int slot, Vector2 location, bool isHovered)
		{
			if (context < 0)
			{
				if (this.Get(slot).PreDraw(this.ContextToEnum(context), inv[slot], location, isHovered))
				{
					ItemSlot.Draw(Main.spriteBatch, inv, context, slot, location, default(Color));
				}
				this.Get(slot).PostDraw(this.ContextToEnum(context), inv[slot], location, isHovered);
				return;
			}
			ItemSlot.Draw(Main.spriteBatch, inv, context, slot, location, default(Color));
		}

		/// <summary>
		/// Provides the Texture for a Modded Accessory Slot
		/// This probably will need optimization down the road.
		/// </summary>
		// Token: 0x06001A97 RID: 6807 RVA: 0x004CCB94 File Offset: 0x004CAD94
		[return: TupleElementNames(new string[]
		{
			null,
			"shared"
		})]
		internal ValueTuple<Texture2D, bool> GetBackgroundTexture(int slot, int context)
		{
			ModAccessorySlot thisSlot = this.Get(slot);
			switch (context)
			{
			case -12:
			{
				Asset<Texture2D> dyeTexture;
				if (ModContent.RequestIfExists<Texture2D>(thisSlot.DyeBackgroundTexture, out dyeTexture, 2))
				{
					return new ValueTuple<Texture2D, bool>(dyeTexture.Value, true);
				}
				break;
			}
			case -11:
			{
				Asset<Texture2D> vanityTexture;
				if (ModContent.RequestIfExists<Texture2D>(thisSlot.VanityBackgroundTexture, out vanityTexture, 2))
				{
					return new ValueTuple<Texture2D, bool>(vanityTexture.Value, true);
				}
				break;
			}
			case -10:
			{
				Asset<Texture2D> funcTexture;
				if (ModContent.RequestIfExists<Texture2D>(thisSlot.FunctionalBackgroundTexture, out funcTexture, 2))
				{
					return new ValueTuple<Texture2D, bool>(funcTexture.Value, true);
				}
				break;
			}
			}
			if (AccessorySlotLoader.ModSlotPlayer(AccessorySlotLoader.Player).IsSharedSlot(thisSlot.Type))
			{
				ValueTuple<Texture2D, bool> result;
				switch (context)
				{
				case -12:
					result = new ValueTuple<Texture2D, bool>(TextureAssets.InventoryBack12.Value, true);
					break;
				case -11:
					result = new ValueTuple<Texture2D, bool>(TextureAssets.InventoryBack8.Value, true);
					break;
				case -10:
					result = new ValueTuple<Texture2D, bool>(TextureAssets.InventoryBack3.Value, true);
					break;
				default:
					throw new NotImplementedException();
				}
				return result;
			}
			return new ValueTuple<Texture2D, bool>(TextureAssets.InventoryBack13.Value, false);
		}

		// Token: 0x06001A98 RID: 6808 RVA: 0x004CCC9C File Offset: 0x004CAE9C
		internal void DrawSlotTexture(Texture2D value6, Vector2 position, Rectangle rectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth, int slot, int context)
		{
			ModAccessorySlot thisSlot = this.Get(slot);
			Texture2D texture = null;
			switch (context)
			{
			case -12:
			{
				Asset<Texture2D> dyeTexture;
				if (ModContent.RequestIfExists<Texture2D>(thisSlot.DyeTexture, out dyeTexture, 2))
				{
					texture = dyeTexture.Value;
				}
				break;
			}
			case -11:
			{
				Asset<Texture2D> vanityTexture;
				if (ModContent.RequestIfExists<Texture2D>(thisSlot.VanityTexture, out vanityTexture, 2))
				{
					texture = vanityTexture.Value;
				}
				break;
			}
			case -10:
			{
				Asset<Texture2D> funcTexture;
				if (ModContent.RequestIfExists<Texture2D>(thisSlot.FunctionalTexture, out funcTexture, 2))
				{
					texture = funcTexture.Value;
				}
				break;
			}
			}
			if (texture == null)
			{
				texture = value6;
			}
			else
			{
				rectangle..ctor(0, 0, texture.Width, texture.Height);
				origin = rectangle.Size() / 2f;
			}
			Main.spriteBatch.Draw(texture, position, new Rectangle?(rectangle), color, rotation, origin, scale, effects, layerDepth);
		}

		// Token: 0x06001A99 RID: 6809 RVA: 0x004CCD62 File Offset: 0x004CAF62
		public AccessorySlotType ContextToEnum(int context)
		{
			return (AccessorySlotType)Math.Abs(context);
		}

		/// <summary>
		/// Checks if the ModAccessorySlot at the given index is enabled. Does not account for the functional or vanity slots individually being disabled due to conflicts arising from shared accessory slots.
		/// </summary>
		// Token: 0x06001A9A RID: 6810 RVA: 0x004CCD6A File Offset: 0x004CAF6A
		public bool ModdedIsItemSlotUnlockedAndUsable(int index, Player player)
		{
			return this.Get(index, player).IsEnabled();
		}

		/// <summary>
		/// Like <see cref="M:Terraria.ModLoader.AccessorySlotLoader.ModdedIsItemSlotUnlockedAndUsable(System.Int32,Terraria.Player)" />, except this also checks if the functional or vanity slot specifically is disabled due to conflicts arising from switching loadouts while using shared accessory slots.
		/// </summary>
		// Token: 0x06001A9B RID: 6811 RVA: 0x004CCD7C File Offset: 0x004CAF7C
		public bool ModdedIsSpecificItemSlotUnlockedAndUsable(int index, Player player, bool vanity)
		{
			ModAccessorySlot slot = this.Get(index, player);
			return !AccessorySlotLoader.ModSlotPlayer(player).SharedSlotHasLoadoutConflict(index, vanity) && slot.IsEnabled();
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x004CCDA9 File Offset: 0x004CAFA9
		public void CustomUpdateEquips(int index, Player player)
		{
			this.Get(index, player).ApplyEquipEffects();
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x004CCDB8 File Offset: 0x004CAFB8
		public bool ModdedCanSlotBeShown(int index)
		{
			return this.Get(index).IsVisibleWhenNotEnabled();
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x004CCDC6 File Offset: 0x004CAFC6
		public bool IsHidden(int index)
		{
			return this.Get(index).IsHidden();
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x004CCDD4 File Offset: 0x004CAFD4
		public bool CanAcceptItem(int index, Item checkItem, int context)
		{
			return this.Get(index).CanAcceptItem(checkItem, this.ContextToEnum(context));
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x004CCDEA File Offset: 0x004CAFEA
		public bool CanPlayerAcceptItem(Player player, int index, Item checkItem, int context)
		{
			return this.Get(index, player).CanAcceptItem(checkItem, this.ContextToEnum(context));
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x004CCE02 File Offset: 0x004CB002
		public void OnHover(int index, int context)
		{
			this.Get(index).OnMouseHover(this.ContextToEnum(context));
		}

		/// <summary>
		/// Checks if the provided item can go in to the provided slot.
		/// Includes checking if the item already exists in either of Player.Armor or ModSlotPlayer.exAccessorySlot
		/// Invokes directly ItemSlot.AccCheck &amp; ModSlot.CanAcceptItem
		/// Note that this doesn't check for conflicts of shared slots with the items of other loadouts, that check is done in <see cref="M:Terraria.ModLoader.Default.ModAccessorySlotPlayer.DetectConflictsWithSharedSlots" /> to prevent a confusing user experience. The accessory slot acts like a disabled slot while in conflict, allowing the player to choose how to fix the issue.
		/// </summary>
		// Token: 0x06001AA2 RID: 6818 RVA: 0x004CCE18 File Offset: 0x004CB018
		public bool ModSlotCheck(Item checkItem, int slot, int context)
		{
			return this.CanAcceptItem(slot, checkItem, context) && !ItemSlot.AccCheck_ForLocalPlayer(AccessorySlotLoader.Player.armor.Concat(AccessorySlotLoader.ModSlotPlayer(AccessorySlotLoader.Player).exAccessorySlot).ToArray<Item>(), checkItem, slot + AccessorySlotLoader.Player.armor.Length);
		}

		/// <summary>
		/// Similar to <see cref="M:Terraria.ModLoader.AccessorySlotLoader.ModSlotCheck(Terraria.Item,System.Int32,System.Int32)" /> except it ignores the item in <paramref name="slot" /> since that item is being passed in as <paramref name="checkItem" />.
		/// </summary>
		// Token: 0x06001AA3 RID: 6819 RVA: 0x004CCE6C File Offset: 0x004CB06C
		public bool IsAccessoryInConflict(Player player, Item checkItem, int slot, int context)
		{
			if (checkItem.IsAir)
			{
				return false;
			}
			if (!this.CanPlayerAcceptItem(player, slot, checkItem, context))
			{
				return true;
			}
			Item[] itemCollection = player.armor.Concat(AccessorySlotLoader.ModSlotPlayer(player).exAccessorySlot).ToArray<Item>();
			itemCollection[slot + player.armor.Length] = AccessorySlotLoader.dummyAccessoryCheckItem;
			return ItemSlot.AccCheck_ForPlayer(player, itemCollection, checkItem, slot + player.armor.Length);
		}

		/// <summary>
		/// After checking for empty slots in ItemSlot.AccessorySwap, this allows for changing what the target slot will be if the accessory isn't already equipped.
		/// DOES NOT affect vanilla behavior of swapping items like for like where existing in a slot
		/// </summary>
		// Token: 0x06001AA4 RID: 6820 RVA: 0x004CCED4 File Offset: 0x004CB0D4
		public void ModifyDefaultSwapSlot(Item item, ref int accSlotToSwapTo)
		{
			for (int num = AccessorySlotLoader.ModSlotPlayer(AccessorySlotLoader.Player).SlotCount - 1; num >= 0; num--)
			{
				if (this.ModdedIsSpecificItemSlotUnlockedAndUsable(num, AccessorySlotLoader.Player, false) && this.Get(num).ModifyDefaultSwapSlot(item, accSlotToSwapTo))
				{
					accSlotToSwapTo = num + 20;
				}
			}
		}

		/// <summary>
		/// Mirrors Player.GetPreferredGolfBallToUse.
		/// Provides the golf ball projectile from an accessory slot.
		/// </summary>
		// Token: 0x06001AA5 RID: 6821 RVA: 0x004CCF24 File Offset: 0x004CB124
		public bool PreferredGolfBall(ref int projType)
		{
			for (int num = AccessorySlotLoader.ModSlotPlayer(AccessorySlotLoader.Player).SlotCount * 2 - 1; num >= 0; num--)
			{
				if (this.ModdedIsSpecificItemSlotUnlockedAndUsable(num, AccessorySlotLoader.Player, num >= AccessorySlotLoader.ModSlotPlayer(AccessorySlotLoader.Player).SlotCount))
				{
					Item item2 = AccessorySlotLoader.ModSlotPlayer(AccessorySlotLoader.Player).exAccessorySlot[num];
					if (!item2.IsAir && item2.shoot > 0 && ProjectileID.Sets.IsAGolfBall[item2.shoot])
					{
						projType = item2.shoot;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x04001461 RID: 5217
		public const int MaxVanillaSlotCount = 7;

		// Token: 0x04001464 RID: 5220
		public static string[] scrollStackLang = new string[]
		{
			Language.GetTextValue("tModLoader.slotStack"),
			Language.GetTextValue("tModLoader.slotScroll")
		};

		// Token: 0x04001465 RID: 5221
		private int slotDrawLoopCounter;

		// Token: 0x04001466 RID: 5222
		private static Item dummyAccessoryCheckItem = new Item();
	}
}
