using System;
using Microsoft.Xna.Framework;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.PlayerChanges;
using Terraria;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.ID;

namespace QoLCompendium.Content.Projectiles.MobileStorages
{
	// Token: 0x02000028 RID: 40
	public class PortableBankAI
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x000049A4 File Offset: 0x00002BA4
		public static void BankAI(Projectile projectile, int itemType, int chestType, ref int playerBank, Player player, BankPlayer bankPlayer)
		{
			if ((Main.gamePaused && !Main.gameMenu) || Main.SmartCursorIsUsed)
			{
				return;
			}
			Vector2 projectileWorldPosition = projectile.position - Main.screenPosition;
			Vector2 center = player.Center;
			int playerCenterX = (int)((double)player.Center.X / 16.0);
			int playerCenterY = (int)((double)player.Center.Y / 16.0);
			int projectileCenterX = (int)projectile.Center.X / 16;
			int projectileCenterY = (int)projectile.Center.Y / 16;
			int lastTileRangeX = player.lastTileRangeX;
			int lastTileRangeY = player.lastTileRangeY;
			if ((playerCenterX < projectileCenterX - lastTileRangeX || playerCenterX > projectileCenterX + lastTileRangeX + 1 || playerCenterY < projectileCenterY - lastTileRangeY || playerCenterY > projectileCenterY + lastTileRangeY + 1) && playerBank == projectile.whoAmI)
			{
				playerBank = -1;
				bankPlayer.chests = false;
				return;
			}
			if ((float)Main.mouseX <= projectileWorldPosition.X || (float)Main.mouseX >= projectileWorldPosition.X + (float)projectile.width || (float)Main.mouseY <= projectileWorldPosition.Y || (float)Main.mouseY >= projectileWorldPosition.Y + (float)projectile.height)
			{
				return;
			}
			player.noThrow = 2;
			player.cursorItemIconEnabled = true;
			player.cursorItemIconID = itemType;
			if (PlayerInput.UsingGamepad)
			{
				player.GamepadEnableGrappleCooldown();
			}
			if (!Main.mouseRight || !Main.mouseRightRelease || Player.BlockInteractionWithProjectiles != 0)
			{
				return;
			}
			Main.mouseRightRelease = false;
			if (player.chest == chestType)
			{
				SoundEngine.PlaySound(SoundID.MenuClose, new Vector2?(Main.LocalPlayer.position), null);
				player.chest = -1;
				Recipe.FindRecipes(false);
				return;
			}
			bool flag = false;
			playerCenterX = ((player.SpawnX == -1) ? Main.spawnTileX : player.SpawnX);
			playerCenterY = ((player.SpawnY == -1) ? Main.spawnTileY : player.SpawnY);
			if (!Common.SolidTile(projectileCenterX, projectileCenterY))
			{
				for (int i = 0; i < 5000; i++)
				{
					for (int j = 0; j < 2000; j++)
					{
						if (playerCenterX - i > 40 && playerCenterY + j < Main.maxTilesY - 40 && Common.SolidTile(playerCenterX - i, playerCenterY + j))
						{
							projectileCenterX = playerCenterX - i;
							projectileCenterY = playerCenterY + j;
							flag = true;
							break;
						}
						if (playerCenterX + i < Main.maxTilesX - 40 && playerCenterY + j < Main.maxTilesY - 40 && Common.SolidTile(playerCenterX + i, playerCenterY + j))
						{
							projectileCenterX = playerCenterX + i;
							projectileCenterY = playerCenterY + j;
							flag = true;
							break;
						}
						if (playerCenterX + i < Main.maxTilesX - 40 && playerCenterY - j > 40 && Common.SolidTile(playerCenterX + i, playerCenterY - j))
						{
							projectileCenterX = playerCenterX + i;
							projectileCenterY = playerCenterY - j;
							flag = true;
							break;
						}
						if (playerCenterX - i > 40 && playerCenterY - j > 40 && Common.SolidTile(playerCenterX - i, playerCenterY - j))
						{
							projectileCenterX = playerCenterX - i;
							projectileCenterY = playerCenterY - j;
							flag = true;
							break;
						}
					}
					if (flag)
					{
						break;
					}
				}
			}
			playerBank = projectile.whoAmI;
			bankPlayer.chests = true;
			player.chest = chestType;
			player.chestX = projectileCenterX;
			player.chestY = projectileCenterY;
			player.SetTalkNPC(playerBank, false);
			Main.oldNPCShop = 0;
			Main.playerInventory = true;
			SoundEngine.PlaySound(SoundID.MenuOpen, new Vector2?(Main.LocalPlayer.position), null);
			Recipe.FindRecipes(false);
		}
	}
}
