// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Projectiles.MobileStorages.PortableBankAI
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.PlayerChanges;
using Terraria;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.ID;

#nullable disable
namespace QoLCompendium.Content.Projectiles.MobileStorages;

public class PortableBankAI
{
  public static void BankAI(
    Projectile projectile,
    int itemType,
    int chestType,
    ref int playerBank,
    Player player,
    BankPlayer bankPlayer)
  {
    if (Main.gamePaused && !Main.gameMenu || Main.SmartCursorIsUsed)
      return;
    Vector2 vector2 = Vector2.op_Subtraction(((Entity) projectile).position, Main.screenPosition);
    Vector2 center = ((Entity) player).Center;
    int num1 = (int) ((double) ((Entity) player).Center.X / 16.0);
    int num2 = (int) ((double) ((Entity) player).Center.Y / 16.0);
    int x = (int) ((Entity) projectile).Center.X / 16 /*0x10*/;
    int y = (int) ((Entity) projectile).Center.Y / 16 /*0x10*/;
    int lastTileRangeX = player.lastTileRangeX;
    int lastTileRangeY = player.lastTileRangeY;
    if ((num1 < x - lastTileRangeX || num1 > x + lastTileRangeX + 1 || num2 < y - lastTileRangeY || num2 > y + lastTileRangeY + 1) && playerBank == ((Entity) projectile).whoAmI)
    {
      playerBank = -1;
      bankPlayer.chests = false;
    }
    else
    {
      if ((double) Main.mouseX <= (double) vector2.X || (double) Main.mouseX >= (double) vector2.X + (double) ((Entity) projectile).width || (double) Main.mouseY <= (double) vector2.Y || (double) Main.mouseY >= (double) vector2.Y + (double) ((Entity) projectile).height)
        return;
      player.noThrow = 2;
      player.cursorItemIconEnabled = true;
      player.cursorItemIconID = itemType;
      if (PlayerInput.UsingGamepad)
        player.GamepadEnableGrappleCooldown();
      if (!Main.mouseRight || !Main.mouseRightRelease || Player.BlockInteractionWithProjectiles != 0)
        return;
      Main.mouseRightRelease = false;
      if (player.chest == chestType)
      {
        SoundEngine.PlaySound(ref SoundID.MenuClose, new Vector2?(((Entity) Main.LocalPlayer).position), (SoundUpdateCallback) null);
        player.chest = -1;
        Recipe.FindRecipes(false);
      }
      else
      {
        bool flag = false;
        int num3 = player.SpawnX == -1 ? Main.spawnTileX : player.SpawnX;
        int num4 = player.SpawnY == -1 ? Main.spawnTileY : player.SpawnY;
        if (!Common.SolidTile(x, y))
        {
          for (int index1 = 0; index1 < 5000; ++index1)
          {
            for (int index2 = 0; index2 < 2000; ++index2)
            {
              if (num3 - index1 > 40 && num4 + index2 < Main.maxTilesY - 40 && Common.SolidTile(num3 - index1, num4 + index2))
              {
                x = num3 - index1;
                y = num4 + index2;
                flag = true;
                break;
              }
              if (num3 + index1 < Main.maxTilesX - 40 && num4 + index2 < Main.maxTilesY - 40 && Common.SolidTile(num3 + index1, num4 + index2))
              {
                x = num3 + index1;
                y = num4 + index2;
                flag = true;
                break;
              }
              if (num3 + index1 < Main.maxTilesX - 40 && num4 - index2 > 40 && Common.SolidTile(num3 + index1, num4 - index2))
              {
                x = num3 + index1;
                y = num4 - index2;
                flag = true;
                break;
              }
              if (num3 - index1 > 40 && num4 - index2 > 40 && Common.SolidTile(num3 - index1, num4 - index2))
              {
                x = num3 - index1;
                y = num4 - index2;
                flag = true;
                break;
              }
            }
            if (flag)
              break;
          }
        }
        playerBank = ((Entity) projectile).whoAmI;
        bankPlayer.chests = true;
        player.chest = chestType;
        player.chestX = x;
        player.chestY = y;
        player.SetTalkNPC(playerBank, false);
        Main.oldNPCShop = 0;
        Main.playerInventory = true;
        SoundEngine.PlaySound(ref SoundID.MenuOpen, new Vector2?(((Entity) Main.LocalPlayer).position), (SoundUpdateCallback) null);
        Recipe.FindRecipes(false);
      }
    }
  }
}
