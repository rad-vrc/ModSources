// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.ProjectileChanges.NoAuraEffects
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.ProjectileChanges;

public class NoAuraEffects : ModSystem
{
  public const string TeslaTexturePath = "Projectiles\\Typeless\\TeslaAura";
  private static TimeSpan NextTextureUpdate = TimeSpan.Zero;
  public static Asset<Texture2D> OrigFlameRing;
  public static Asset<Texture2D> OrigTesla;
  public static List<int> OriginalTeslaProjectiles = new List<int>();
  public static bool WasTeslaEnabled = false;

  public static Asset<Texture2D> EmptyTexture => Common.GetAsset("Projectiles", "Invisible");

  public virtual void Load()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    Main.OnPreDraw += NoAuraEffects.\u003C\u003EO.\u003C0\u003E__UpdateTextures ?? (NoAuraEffects.\u003C\u003EO.\u003C0\u003E__UpdateTextures = new Action<GameTime>(NoAuraEffects.UpdateTextures));
  }

  public virtual void Unload()
  {
    if (NoAuraEffects.OrigFlameRing != null && TextureAssets.FlameRing == NoAuraEffects.EmptyTexture)
      TextureAssets.FlameRing = NoAuraEffects.OrigFlameRing;
    if (NoAuraEffects.OrigTesla != null)
    {
      foreach (int originalTeslaProjectile in NoAuraEffects.OriginalTeslaProjectiles)
        TextureAssets.Projectile[originalTeslaProjectile] = NoAuraEffects.OrigTesla;
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    Main.OnPreDraw -= NoAuraEffects.\u003C\u003EO.\u003C0\u003E__UpdateTextures ?? (NoAuraEffects.\u003C\u003EO.\u003C0\u003E__UpdateTextures = new Action<GameTime>(NoAuraEffects.UpdateTextures));
  }

  private static void UpdateTextures(GameTime Time)
  {
    if (Main.gameMenu || !(Time.TotalGameTime > NoAuraEffects.NextTextureUpdate))
      return;
    NoAuraEffects.NextTextureUpdate = Time.TotalGameTime.Add(TimeSpan.FromSeconds(1.0));
    if (QoLCompendium.QoLCompendium.mainClientConfig.NoAuraVisuals && TextureAssets.FlameRing != NoAuraEffects.EmptyTexture)
    {
      if (NoAuraEffects.OrigFlameRing == null)
        NoAuraEffects.OrigFlameRing = TextureAssets.FlameRing;
      TextureAssets.FlameRing = NoAuraEffects.EmptyTexture;
    }
    else if (!QoLCompendium.QoLCompendium.mainClientConfig.NoAuraVisuals && NoAuraEffects.OrigFlameRing != null && TextureAssets.FlameRing != NoAuraEffects.OrigFlameRing)
      TextureAssets.FlameRing = NoAuraEffects.OrigFlameRing;
    if (!ModConditions.calamityLoaded)
      return;
    FieldInfo field = typeof (AssetRepository).GetField("_assets", (BindingFlags) 36);
    Dictionary<string, IAsset> dictionary = (Dictionary<string, IAsset>) field.GetValue((object) ModConditions.calamityMod.Assets);
    if (!FieldInfo.op_Inequality(field, (FieldInfo) null) || dictionary == null || !dictionary.ContainsKey("Projectiles\\Typeless\\TeslaAura"))
      return;
    if (QoLCompendium.QoLCompendium.mainClientConfig.NoAuraVisuals && !NoAuraEffects.WasTeslaEnabled)
    {
      if (NoAuraEffects.OrigTesla == null)
        NoAuraEffects.OrigTesla = (Asset<Texture2D>) dictionary["Projectiles\\Typeless\\TeslaAura"];
      for (int index = TextureAssets.Projectile.Length - 1; index >= 0; --index)
      {
        if (TextureAssets.Projectile[index] == NoAuraEffects.OrigTesla)
        {
          NoAuraEffects.WasTeslaEnabled = true;
          if (!NoAuraEffects.OriginalTeslaProjectiles.Contains(index))
            NoAuraEffects.OriginalTeslaProjectiles.Add(index);
          TextureAssets.Projectile[index] = NoAuraEffects.EmptyTexture;
        }
      }
    }
    else
    {
      if (QoLCompendium.QoLCompendium.mainClientConfig.NoAuraVisuals || NoAuraEffects.OrigTesla == null || !NoAuraEffects.WasTeslaEnabled)
        return;
      NoAuraEffects.WasTeslaEnabled = false;
      foreach (int originalTeslaProjectile in NoAuraEffects.OriginalTeslaProjectiles)
        TextureAssets.Projectile[originalTeslaProjectile] = NoAuraEffects.OrigTesla;
    }
  }
}
