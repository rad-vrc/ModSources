// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.MobileStorages.KillMobileStorages
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using QoLCompendium.Core;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Items.Tools.MobileStorages;

public class KillMobileStorages : GlobalItem
{
  public virtual bool AppliesToEntity(Item entity, bool lateInstantiation)
  {
    return entity.type == 3213 || entity.type == 4131 || entity.type == 5325 || entity.type == ModContent.ItemType<FlyingSafe>() || entity.type == ModContent.ItemType<EtherianConstruct>();
  }

  public virtual bool CanRightClick(Item item) => true;

  public virtual void RightClick(Item item, Player player)
  {
    foreach (Projectile projectile in Main.projectile)
    {
      if (Common.MobileStorages.Contains(projectile.type) && player.ownedProjectileCounts[projectile.type] > 0 && projectile.owner == ((Entity) player).whoAmI)
        ((Entity) projectile).active = false;
    }
  }

  public virtual void OnConsumeItem(Item item, Player player) => ++item.stack;

  public virtual void ModifyTooltips(Item item, List<TooltipLine> tooltips)
  {
    Common.AddLastTooltip(tooltips, new TooltipLine(((ModType) this).Mod, "KillMobileStorage", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.KillMobileStorage"))
    {
      OverrideColor = new Color?(Color.Gray)
    });
  }
}
