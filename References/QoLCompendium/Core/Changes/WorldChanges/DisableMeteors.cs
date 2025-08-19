// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.WorldChanges.DisableMeteors
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Chat;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.WorldChanges;

public class DisableMeteors : ModSystem
{
  public virtual void Load()
  {
    // ISSUE: method pointer
    On_WorldGen.dropMeteor += new On_WorldGen.hook_dropMeteor((object) this, __methodptr(StopMeteor));
  }

  public virtual void Unload()
  {
    // ISSUE: method pointer
    On_WorldGen.dropMeteor -= new On_WorldGen.hook_dropMeteor((object) this, __methodptr(StopMeteor));
  }

  private void StopMeteor(On_WorldGen.orig_dropMeteor orig)
  {
    if (!QoLCompendium.QoLCompendium.mainConfig.NoMeteorSpawns)
    {
      orig.Invoke();
    }
    else
    {
      ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Mods.QoLCompendium.Messages.MeteorStopped", Array.Empty<object>()), Color.White, -1);
      int length = ((IEnumerable<Player>) Main.player).Where<Player>((Func<Player, bool>) (player => ((Entity) player).active)).ToArray<Player>().Length;
      for (int index = 0; index < Main.player.Length; ++index)
      {
        Player player = Main.player[index];
        if (((Entity) player).active)
        {
          int num = Main.rand.Next(400, 500) / length;
          player.QuickSpawnItem(((Entity) player).GetSource_FromThis((string) null), 116, num);
          ChatHelper.SendChatMessageToClient(NetworkText.FromKey("Mods.QoLCompendium.Messages.MeteoriteGiven", new object[1]
          {
            (object) num
          }), Color.White, index);
        }
      }
    }
  }
}
