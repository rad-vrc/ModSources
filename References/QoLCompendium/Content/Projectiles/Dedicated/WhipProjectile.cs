// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Projectiles.Dedicated.WhipProjectile
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Projectiles.Dedicated;

public abstract class WhipProjectile : ModProjectile
{
  public Color fishingLineColor = Color.White;
  public Color lightingColor = Color.Transparent;
  public Color? drawColor;
  public int? swingDust;
  public int dustAmount;
  public SoundStyle? whipCrackSound = new SoundStyle?(SoundID.Item153);
  public Texture2D whipSegment;
  public Texture2D whipTip;
  public List<Vector2> whipPoints = new List<Vector2>();
  public int? tagType;
  public int tagDuration = 240 /*0xF0*/;
  public float multihitModifier = 0.8f;
  public float segmentRotation;
  private bool runOnce = true;

  public virtual void SetStaticDefaults() => ProjectileID.Sets.IsAWhip[this.Type] = true;

  public virtual void SetDefaults()
  {
    this.Projectile.friendly = true;
    this.Projectile.penetrate = -1;
    this.Projectile.tileCollide = false;
    this.Projectile.ownerHitCheck = true;
    this.Projectile.extraUpdates = 1;
    this.Projectile.usesLocalNPCImmunity = true;
    this.Projectile.localNPCHitCooldown = -1;
    this.Projectile.DamageType = DamageClass.Generic;
    this.whipSegment = Asset<Texture2D>.op_Explicit(ModContent.Request<Texture2D>("QoLCompendium/Content/Projectiles/Dedicated/SillySlapperWhip_Segment", (AssetRequestMode) 2));
    this.whipTip = Asset<Texture2D>.op_Explicit(ModContent.Request<Texture2D>("QoLCompendium/Content/Projectiles/Dedicated/SillySlapperWhip_Tip", (AssetRequestMode) 2));
    this.SetWhipStats();
  }

  public virtual bool PreAI()
  {
    if ((double) this.Timer % 2.0 < 0.001)
    {
      this.whipPoints.Clear();
      Projectile.FillWhipControlPoints(this.Projectile, this.whipPoints);
    }
    return true;
  }

  public virtual void SetWhipStats()
  {
    ((Entity) this.Projectile).width = 20;
    ((Entity) this.Projectile).height = 20;
    this.Projectile.WhipSettings.Segments = 30;
    this.Projectile.WhipSettings.RangeMultiplier = 1f;
  }

  internal float Timer
  {
    get => this.Projectile.ai[0];
    set => this.Projectile.ai[0] = value;
  }

  public virtual bool PreDraw(ref Color lightColor) => this.DrawWhip(this.fishingLineColor);

  internal bool DrawWhip(Color lineColor)
  {
    if (this.whipPoints == null || this.whipPoints.Count < 1)
      return false;
    WhipProjectile.DrawFishingLineBetweenPoints(this.whipPoints, lineColor);
    SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 2;
    Main.instance.LoadProjectile(this.Type);
    Texture2D texture2D = TextureAssets.Projectile[this.Type].Value;
    Rectangle rectangle;
    // ISSUE: explicit constructor call
    ((Rectangle) ref rectangle).\u002Ector(0, 0, texture2D.Width, texture2D.Height);
    Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle), 2f);
    Vector2 vector2_2 = this.whipPoints[0];
    for (int index = 0; index < this.whipPoints.Count - 1; ++index)
    {
      float num1 = 1f;
      if (index == this.whipPoints.Count - 2)
      {
        texture2D = this.whipTip;
        // ISSUE: explicit constructor call
        ((Rectangle) ref rectangle).\u002Ector(0, 0, texture2D.Width, texture2D.Height);
        vector2_1 = Vector2.op_Division(Utils.Size(rectangle), 2f);
        float num2;
        int num3;
        float num4;
        Projectile.GetWhipSettings(this.Projectile, ref num2, ref num3, ref num4);
        float num5 = this.Timer / num2;
        num1 = MathHelper.Lerp(0.5f, 1.5f, Utils.GetLerpValue(0.1f, 0.7f, num5, true) * Utils.GetLerpValue(0.9f, 0.7f, num5, true));
      }
      else if (index > 0)
      {
        texture2D = this.whipSegment;
        // ISSUE: explicit constructor call
        ((Rectangle) ref rectangle).\u002Ector(0, 0, texture2D.Width, texture2D.Height);
        vector2_1 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      }
      Vector2 whipPoint = this.whipPoints[index];
      Vector2 vector2_3 = Vector2.op_Subtraction(this.whipPoints[index + 1], whipPoint);
      float num6 = Utils.ToRotation(vector2_3) + this.segmentRotation;
      if (index == 0)
        num6 = Utils.ToRotation(vector2_3);
      Color color = Lighting.GetColor(Utils.ToTileCoordinates(whipPoint));
      if (this.drawColor.HasValue)
        color = this.drawColor.Value;
      Main.EntitySpriteDraw(texture2D, Vector2.op_Subtraction(vector2_2, Main.screenPosition), new Rectangle?(rectangle), color, num6, vector2_1, num1, spriteEffects, 0.0f);
      vector2_2 = Vector2.op_Addition(vector2_2, vector2_3);
    }
    return false;
  }

  public virtual void WhipAIMotion()
  {
    Player player = Main.player[this.Projectile.owner];
    float num = (float) (player.itemAnimationMax * this.Projectile.MaxUpdates);
    if (this.runOnce)
    {
      this.Projectile.WhipSettings.Segments = (int) (((double) player.whipRangeMultiplier + 1.0) * (double) this.Projectile.WhipSettings.Segments);
      this.runOnce = false;
    }
    this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f;
    Projectile projectile = this.Projectile;
    Vector2 center = ((Entity) this.Projectile).Center;
    List<Vector2> whipPoints = this.whipPoints;
    Vector2 vector2_1 = whipPoints[whipPoints.Count - 1];
    Vector2 vector2_2 = Vector2.Lerp(center, vector2_1, 1f);
    ((Entity) projectile).Center = vector2_2;
    this.Projectile.spriteDirection = (double) ((Entity) this.Projectile).velocity.X >= 0.0 ? 1 : -1;
    ++this.Timer;
    if ((double) this.Timer < (double) num && player.itemAnimation > 0)
      return;
    this.Projectile.Kill();
  }

  public virtual void WhipSFX(Color lightingCol, int? dustID, int dustNum, SoundStyle? sound)
  {
    Player player = Main.player[this.Projectile.owner];
    float num = (float) (player.itemAnimationMax * this.Projectile.MaxUpdates);
    player.heldProj = ((Entity) this.Projectile).whoAmI;
    Vector2 tipPosition = this.GetTipPosition();
    if ((double) this.Timer == (double) num / 2.0 && sound.HasValue)
      SoundEngine.PlaySound(ref sound, new Vector2?(tipPosition), (SoundUpdateCallback) null);
    if ((double) this.Timer < (double) num * 0.5)
      return;
    if (dustID.HasValue)
    {
      for (int index = 0; index < dustNum; ++index)
        Dust.NewDust(tipPosition, 2, 2, dustID.Value, 0.0f, 0.0f, 0, new Color(), 0.5f);
    }
    if (!Color.op_Inequality(lightingCol, Color.Transparent))
      return;
    Lighting.AddLight(tipPosition, (float) ((Color) ref lightingCol).R / (float) byte.MaxValue, (float) ((Color) ref lightingCol).G / (float) byte.MaxValue, (float) ((Color) ref lightingCol).B / (float) byte.MaxValue);
  }

  public virtual void AI()
  {
    this.WhipAIMotion();
    this.WhipSFX(this.lightingColor, this.swingDust, this.dustAmount, this.whipCrackSound);
  }

  internal static void DrawFishingLineBetweenPoints(
    List<Vector2> list,
    Color lineCol,
    bool useLighCol = true)
  {
    Texture2D texture2D = TextureAssets.FishingLine.Value;
    Rectangle rectangle = Utils.Frame(texture2D, 1, 1, 0, 0, 0, 0);
    Vector2 vector2_1;
    // ISSUE: explicit constructor call
    ((Vector2) ref vector2_1).\u002Ector((float) (rectangle.Width / 2), 2f);
    Vector2 vector2_2 = list[0];
    for (int index = 0; index < list.Count - 2; ++index)
    {
      Vector2 vector2_3 = list[index];
      Vector2 vector2_4 = Vector2.op_Subtraction(list[index + 1], vector2_3);
      float num = Utils.ToRotation(vector2_4) - 1.57079637f;
      Color color = lineCol;
      if (useLighCol)
        color = Lighting.GetColor(Utils.ToTileCoordinates(vector2_3), lineCol);
      Vector2 vector2_5;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2_5).\u002Ector(1f, (((Vector2) ref vector2_4).Length() + 2f) / (float) rectangle.Height);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Subtraction(vector2_2, Main.screenPosition), new Rectangle?(rectangle), color, num, vector2_1, vector2_5, (SpriteEffects) 0, 0.0f);
      vector2_2 = Vector2.op_Addition(vector2_2, vector2_4);
    }
  }

  internal Vector2 GetTipPosition()
  {
    List<Vector2> whipPoints = this.whipPoints;
    return whipPoints[whipPoints.Count - 2];
  }
}
