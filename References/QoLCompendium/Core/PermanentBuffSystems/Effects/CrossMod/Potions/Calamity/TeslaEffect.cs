// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Calamity.TeslaEffect
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems.Effects.CrossMod.Potions.Calamity;

public class TeslaEffect : IPermanentModdedBuff
{
  internal override void ApplyEffect(PermanentBuffPlayer player)
  {
    if (!ModConditions.calamityLoaded || player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "TeslaBuff")] || PermanentBuffPlayer.PermanentCalamityBuffsBools[22])
      return;
    this.buffToApply = BuffLoader.GetBuff(Common.GetModBuff(ModConditions.calamityMod, "TeslaBuff"));
    this.buffToApply.Update(player.Player, ref this.index);
    player.Player.buffImmune[Common.GetModBuff(ModConditions.calamityMod, "TeslaBuff")] = true;
    if (player.Player.ownedProjectileCounts[Common.GetModProjectile(ModConditions.calamityMod, "TeslaAura")] >= 1)
      return;
    StatModifier bestClassDamage = player.Player.GetBestClassDamage();
    int num = (int) ((StatModifier) ref bestClassDamage).ApplyTo(10f);
    Projectile.NewProjectile(((Entity) player.Player).GetSource_FromAI((string) null), ((Entity) player.Player).Center, Vector2.Zero, Common.GetModProjectile(ModConditions.calamityMod, "TeslaAura"), num, 0.0f, ((Entity) player.Player).whoAmI, 0.0f, 0.0f, 0.0f);
  }
}
