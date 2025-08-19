// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.TextHelper
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.Localization;

#nullable disable
namespace QoLCompendium.Core;

public static class TextHelper
{
  public static void PrintText(string text) => TextHelper.PrintText(text, Color.White);

  public static void PrintText(string text, Color color, bool myPlayerOnly = false)
  {
    if (Main.netMode == 0)
      Main.NewText((object) text, new Color?(color));
    else if (Main.netMode == 2)
    {
      ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(text), color, -1);
    }
    else
    {
      if (!(Main.netMode == 2 & myPlayerOnly))
        return;
      ChatHelper.SendChatMessageToClient(NetworkText.FromLiteral(text), color, Main.myPlayer);
    }
  }

  public static void PrintText(string text, int r, int g, int b)
  {
    TextHelper.PrintText(text, new Color(r, g, b));
  }
}
