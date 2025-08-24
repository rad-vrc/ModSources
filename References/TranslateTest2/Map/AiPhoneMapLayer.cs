// Content/Map/AiPhoneMapLayer.cs
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Map;
using Terraria.UI; // Alignment
using Terraria.DataStructures; // SpriteFrame
using Terraria.ModLoader;

namespace TranslateTest2.Map {
  public class AiPhoneMapLayer : ModMapLayer {
    private static Texture2D _pinTex;
    private static Texture2D PinTex => _pinTex ??= ModContent.Request<Texture2D>("TranslateTest2/Content/Items/Tools/AiPhone_Dummy").Value;
    public override void Draw(ref MapOverlayDrawContext ctx, ref string text) {
      // QoLCompendium が導入されている場合にのみ描画（未導入なら何もせず終了し、エラーも出さない）
      if (!ModLoader.TryGetMod("QoLCompendium", out _))
        return;

      var me = Main.LocalPlayer;
      if (!HasAiPhone(me)) return;
  var pinTex = PinTex;

      // 味方プレイヤー
      for (int i = 0; i < Main.maxPlayers; i++) {
        var pl = Main.player[i];
        if (i == me.whoAmI || !pl.active) continue;
    // MapOverlayDrawContext expects tile coordinates, not world coordinates
    var tilePos = pl.Center / 16f;
  var drawResult = ctx.Draw(pinTex, tilePos, Color.White, new SpriteFrame(1, 1, 0, 0), 0.9f, 1.1f, Terraria.UI.Alignment.Center);
    if (drawResult.IsMouseOver) {
          if (Main.mouseLeft && Main.mouseLeftRelease)
      Content.Items.Tools.AiPhone.TeleportTo(me, pl.Center);
        }
      }
      // タウンNPC
      for (int n = 0; n < Main.maxNPCs; n++) {
        var npc = Main.npc[n];
        if (!npc.active || !npc.townNPC) continue;
    // NPCヘッドインデックス取得API差異があるため、同一ピンテクスチャを流用
    var tilePos = npc.Center / 16f;
  var drawResult = ctx.Draw(pinTex, tilePos, Color.White, new SpriteFrame(1, 1, 0, 0), 0.9f, 1.1f, Terraria.UI.Alignment.Center);
    if (drawResult.IsMouseOver) {
          if (Main.mouseLeft && Main.mouseLeftRelease)
      Content.Items.Tools.AiPhone.TeleportTo(me, npc.Center + new Vector2(32, 0));
        }
      }
    }
    static bool HasAiPhone(Player p) {
      for (int i = 0; i < p.inventory.Length; i++)
  if (p.inventory[i].type == ModContent.ItemType<Content.Items.Tools.AiPhone>()) return true;
      return false;
    }
  }
}
