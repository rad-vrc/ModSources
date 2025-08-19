using System;
using System.Linq;
using Microsoft.Xna.Framework;
using QoLCompendium.Content.Items.Tools.Mirrors;
using QoLCompendium.Core.Changes.PlayerChanges;
using QoLCompendium.Core.UI.Panels;
using Terraria;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ObjectData;

namespace QoLCompendium.Core
{
	// Token: 0x02000205 RID: 517
	public class KeybindPlayer : ModPlayer
	{
		// Token: 0x06000BCA RID: 3018 RVA: 0x0004EBA0 File Offset: 0x0004CDA0
		public unsafe override void ProcessTriggers(TriggersSet triggersSet)
		{
			if (KeybindSystem.SendNPCsHome.JustPressed)
			{
				foreach (NPC npc in from n in Main.npc
				where n != null && n.active && n.townNPC && !n.homeless
				select n)
				{
					QoLCompendium.TownEntitiesTeleportToHome(npc, npc.homeTileX, npc.homeTileY);
				}
				Main.NewText(Language.GetTextValue("Mods.QoLCompendium.Messages.TeleportNPCsHome"), byte.MaxValue, byte.MaxValue, byte.MaxValue);
			}
			if (KeybindSystem.QuickRecall.JustPressed)
			{
				this.AutoUseMirror();
			}
			if (KeybindSystem.QuickMosaicMirror.JustPressed)
			{
				this.AutoUseMosaicMirror();
			}
			if (KeybindSystem.QuickRod.JustPressed)
			{
				this.AutoUseRod();
			}
			if (KeybindSystem.Dash.JustPressed)
			{
				base.Player.dashTime = 30;
				if (base.Player.GetModPlayer<DashPlayer>().LeftLastPressed || (base.Player.direction == -1 && base.Player.velocity.X == 0f))
				{
					base.Player.controlLeft = true;
					base.Player.releaseLeft = true;
					for (int i = 0; i < 10; i++)
					{
						if (i >= 9)
						{
							base.Player.controlLeft = true;
							base.Player.releaseLeft = true;
						}
					}
				}
				if (base.Player.GetModPlayer<DashPlayer>().RightLastPressed || (base.Player.direction == 1 && base.Player.velocity.X == 0f))
				{
					base.Player.controlRight = true;
					base.Player.releaseRight = true;
					for (int j = 0; j < 10; j++)
					{
						if (j >= 9)
						{
							base.Player.controlRight = true;
							base.Player.releaseRight = true;
						}
					}
				}
			}
			if (Main.netMode == 0 && KeybindPlayer.timeout > 0)
			{
				KeybindPlayer.timeout -= 1;
			}
			if (KeybindSystem.AddTileToWhitelist.JustPressed)
			{
				Tile target = Main.tile[Player.tileTargetX, Player.tileTargetY];
				TileLoader.GetTile((int)(*target.TileType));
				int style = TileObjectData.GetTileStyle(target);
				if (target.HasTile)
				{
					Common.UpdateWhitelist((int)(*target.TileType), Common.GetFullNameById((int)(*target.TileType), style), style, false);
					Main.NewText(Language.GetTextValue("Mods.QoLCompendium.TileStuff.Whitelisted") + " " + new TileDefinition((int)(*target.TileType)).Name, byte.MaxValue, byte.MaxValue, byte.MaxValue);
				}
			}
			if (KeybindSystem.RemoveTileFromWhitelist.JustPressed)
			{
				Tile target2 = Main.tile[Player.tileTargetX, Player.tileTargetY];
				TileLoader.GetTile((int)(*target2.TileType));
				int style2 = TileObjectData.GetTileStyle(target2);
				if (target2.HasTile)
				{
					Common.UpdateWhitelist((int)(*target2.TileType), Common.GetFullNameById((int)(*target2.TileType), style2), style2, true);
					Main.NewText(Language.GetTextValue("Mods.QoLCompendium.TileStuff.Removed") + " " + new TileDefinition((int)(*target2.TileType)).Name, byte.MaxValue, byte.MaxValue, byte.MaxValue);
				}
			}
			if (KeybindSystem.PermanentBuffUIToggle.JustPressed)
			{
				PermanentBuffSelectorUI.timeStart = Main.GameUpdateCount;
				PermanentBuffSelectorUI.visible = !PermanentBuffSelectorUI.visible;
				if (PermanentBuffSelectorUI.visible)
				{
					SoundEngine.PlaySound(SoundID.MenuOpen, new Vector2?(Main.LocalPlayer.position), null);
				}
				else
				{
					SoundEngine.PlaySound(SoundID.MenuClose, new Vector2?(Main.LocalPlayer.position), null);
				}
				if (!PermanentBuffSelectorUI.visible)
				{
					PermanentBuffUI.visible = false;
					PermanentCalamityBuffUI.visible = false;
					PermanentMartinsOrderBuffUI.visible = false;
					PermanentSOTSBuffUI.visible = false;
					PermanentSpiritClassicBuffUI.visible = false;
					PermanentThoriumBuffUI.visible = false;
				}
			}
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x0004EF74 File Offset: 0x0004D174
		public override void PostUpdate()
		{
			if (this.autoRevertSelectedItem && base.Player.itemTime == 0 && base.Player.itemAnimation == 0)
			{
				base.Player.selectedItem = this.originalSelectedItem;
				this.autoRevertSelectedItem = false;
			}
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x0004EFB0 File Offset: 0x0004D1B0
		public void AutoUseRod()
		{
			if (base.Player.HasItem(5335))
			{
				this.QuickUseItemAt(Common.GetSlotItemIsIn(new Item(5335, 1, 0), base.Player.inventory), true);
			}
			if (base.Player.HasItem(1326))
			{
				this.QuickUseItemAt(Common.GetSlotItemIsIn(new Item(1326, 1, 0), base.Player.inventory), true);
			}
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x0004F028 File Offset: 0x0004D228
		public void AutoUseMirror()
		{
			int potionofReturn = -1;
			int recallPotion = -1;
			int magicMirror = -1;
			int i = 0;
			while (i < base.Player.inventory.Length)
			{
				int type = base.Player.inventory[i].type;
				if (type <= 3124)
				{
					if (type == 50)
					{
						goto IL_66;
					}
					if (type != 2350)
					{
						if (type == 3124)
						{
							goto IL_66;
						}
					}
					else
					{
						recallPotion = i;
					}
				}
				else
				{
					if (type == 3199)
					{
						goto IL_66;
					}
					if (type != 4870)
					{
						if (type == 5358)
						{
							goto IL_66;
						}
					}
					else
					{
						potionofReturn = i;
					}
				}
				IL_68:
				i++;
				continue;
				IL_66:
				magicMirror = i;
				goto IL_68;
			}
			if (potionofReturn != -1)
			{
				this.QuickUseItemAt(potionofReturn, true);
				return;
			}
			if (recallPotion != -1)
			{
				this.QuickUseItemAt(recallPotion, true);
				return;
			}
			if (magicMirror != -1)
			{
				this.QuickUseItemAt(magicMirror, true);
			}
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x0004F0D8 File Offset: 0x0004D2D8
		public void AutoUseMosaicMirror()
		{
			int mosaicMirror = -1;
			for (int i = 0; i < base.Player.inventory.Length; i++)
			{
				if (base.Player.inventory[i].type == ModContent.ItemType<MosaicMirror>())
				{
					mosaicMirror = i;
					break;
				}
			}
			if (mosaicMirror != -1)
			{
				this.QuickUseItemAt(mosaicMirror, true);
			}
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x0004F128 File Offset: 0x0004D328
		public void QuickUseItemAt(int index, bool use = true)
		{
			if (!this.autoRevertSelectedItem && base.Player.selectedItem != index && base.Player.inventory[index].type != 0)
			{
				this.originalSelectedItem = base.Player.selectedItem;
				this.autoRevertSelectedItem = true;
				base.Player.selectedItem = index;
				base.Player.controlUseItem = true;
				if (use && CombinedHooks.CanUseItem(base.Player, base.Player.inventory[base.Player.selectedItem]) && base.Player.whoAmI == Main.myPlayer)
				{
					base.Player.ItemCheck();
				}
			}
		}

		// Token: 0x0400008D RID: 141
		public int originalSelectedItem;

		// Token: 0x0400008E RID: 142
		public bool autoRevertSelectedItem;

		// Token: 0x0400008F RID: 143
		public int dashTimeMod;

		// Token: 0x04000090 RID: 144
		public static byte timeout;
	}
}
