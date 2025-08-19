// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.PlayerChanges.MiscEffectsPlayer
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.PlayerChanges;

public class MiscEffectsPlayer : ModPlayer
{
  public bool joinedTeam;

  public virtual void OnEnterWorld()
  {
    if (this.Player != Main.player[Main.myPlayer] || QoLCompendium.QoLCompendium.mainConfig.AutoTeams <= 0 || this.joinedTeam || Main.netMode != 1)
      return;
    Main.player[Main.myPlayer].team = QoLCompendium.QoLCompendium.mainConfig.AutoTeams;
    this.joinedTeam = true;
    NetMessage.SendData(45, -1, -1, (NetworkText) null, Main.myPlayer, (float) QoLCompendium.QoLCompendium.mainConfig.AutoTeams, 0.0f, 0.0f, 0, 0, 0);
  }

  public virtual void PostUpdateMiscEffects()
  {
    this.Player.tileSpeed -= QoLCompendium.QoLCompendium.mainConfig.IncreasePlaceSpeed;
    this.Player.wallSpeed -= QoLCompendium.QoLCompendium.mainConfig.IncreasePlaceSpeed;
    Player.tileRangeX += QoLCompendium.QoLCompendium.mainConfig.IncreasePlaceRange;
    Player.tileRangeY += QoLCompendium.QoLCompendium.mainConfig.IncreasePlaceRange;
  }

  public virtual void PostUpdateEquips()
  {
    if (ModContent.GetInstance<QoLCConfig>().NoExpertIceWaterChilled && ((Entity) this.Player).wet && this.Player.ZoneSnow && Main.expertMode)
    {
      this.Player.buffImmune[46] = true;
      this.Player.chilled = false;
    }
    if (!ModContent.GetInstance<QoLCConfig>().NoShimmerSink || !((Entity) this.Player).wet)
      return;
    this.Player.buffImmune[353] = true;
    this.Player.shimmerImmune = true;
  }

  public virtual void PreUpdateBuffs()
  {
    if (!QoLCompendium.QoLCompendium.mainConfig.DisableHappiness)
      return;
    this.Player.currentShoppingSettings.PriceAdjustment = (double) QoLCompendium.QoLCompendium.mainConfig.HappinessPriceChange;
  }
}
