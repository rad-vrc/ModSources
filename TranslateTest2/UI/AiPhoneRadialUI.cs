// UI/AiPhoneRadialUI.cs
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent; // FontAssets, TextureAssets
using Terraria.ModLoader;
using Terraria.UI;
using TranslateTest2.Items.Tools; // WarpMode

namespace TranslateTest2.UI
{
    /// <summary>
    /// UI中央(HAlign=0.5, VAlign=0.5)に固定したラジアル描画。
    /// 当たり判定は「中心→マウス」の角度で 6 等分（距離は無視 = 全画面判定）。
    /// 背景は描かず、アイコン＋ラベルのみ。
    /// </summary>
    public class AiPhoneRadialUI : UIState
    {
        // ===== 調整パラメータ ===============================================
        public const bool QUICK_CAST_ON_RELEASE = true; // 離した瞬間に PerformMode
        const float RADIUS               = 90f;          // 円半径（UI座標）
        const float BASE_ICON_SCALE      = 1.20f;        // 32x28 を拡大
        const float HOVER_SCALE_ADD      = 0.15f;        // ホバー時の追い増し

        // Dummyは選択肢から除外
        public readonly WarpMode[] Options = new[] {
            WarpMode.Spawn, WarpMode.Bed, WarpMode.Ocean, WarpMode.Hell, WarpMode.Death, WarpMode.Random
        };

        // ===== 状態 ==========================================================
        UIElement anchor;            // 常にUI中央に配置されるアンカー
        float openT;                 // 0→1 開きアニメ
        int hovered = -1;            // 現在ホバー中
        public int HoveredIndex => hovered;

        public override void OnInitialize()
        {
            anchor = new UIElement
            {
                HAlign = 0.5f,
                VAlign = 0.5f
            };
            anchor.Width.Set(0f, 0f);
            anchor.Height.Set(0f, 0f);
            Append(anchor);
        }

        public void ResetAnimOnOpen()
        {
            openT = 0f;
            hovered = -1;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // 開きアニメ進行
            openT = MathHelper.Clamp(openT + 0.2f, 0f, 1f);

            // マウス座標（UI空間）と中心
            Vector2 mouseUI = Main.MouseScreen / Main.UIScale;
            Vector2 center  = anchor.GetDimensions().Center();
            Vector2 v       = mouseUI - center;

            // ==== 角度ベース判定（上を0°, 時計回りで6等分）====================
            // 1) θ = atan2(y,x) を [0, 2π) に正規化（画面座標: yは下が正 = 時計回り）
            float theta = (float)Math.Atan2(v.Y, v.X);      // [-π, π]
            if (theta < 0f) theta += MathHelper.TwoPi;      // [0, 2π)

            // 2) 起点を「上(-90°)」に合わせる：delta = θ - (-π/2) = θ + π/2
            float delta = theta + MathHelper.PiOver2;
            // 3) [0, 2π) に再正規化（2πを超えるケースをケア）
            delta %= MathHelper.TwoPi;

            // 4) セクタ幅（60°）で割って 0..5 を得る（境界での丸め誤差を避けるため微小εを加える）
            float sector = MathHelper.TwoPi / Options.Length; // = 60°
            int idx = (int)Math.Floor((delta + 1e-6f) / sector);
            if (idx < 0) idx = 0; else if (idx >= Options.Length) idx = Options.Length - 1;

            hovered = idx;
        }

        protected override void DrawSelf(SpriteBatch sb)
        {
            base.DrawSelf(sb);

            Vector2 center = anchor.GetDimensions().Center();

            // 円周にアイコン（表示は従来通り）
            for (int i = 0; i < Options.Length; i++)
            {
                Texture2D tex = GetIconTexture(Options[i]);
                Vector2 pos   = IconPos(center, i);
                Vector2 origin = tex.Size() / 2f;

                float scale = BASE_ICON_SCALE * EaseOut(openT);
                if (i == hovered) scale *= (1f + HOVER_SCALE_ADD);

                // グロー（背景なしでも視認性を上げる）
                if (i == hovered)
                    sb.Draw(tex, pos + new Vector2(0, 1), null, new Color(255,255,255,90), 0f, origin, scale * 1.18f, SpriteEffects.None, 0f);

                // 本体
                sb.Draw(tex, pos, null, Color.White, 0f, origin, scale, SpriteEffects.None, 0f);

                // ラベル（ホバー中のみ）
                if (i == hovered)
                {
                    string label = Options[i].ToString();
                    var size = FontAssets.MouseText.Value.MeasureString(label);
                    var textPos = pos - new Vector2(size.X / 2f, 30f);
                    Utils.DrawBorderString(sb, label, textPos, Color.White, 0.95f);
                }
            }

            // 中央ヒント（控えめ）
            string hint = QUICK_CAST_ON_RELEASE ? "Vを離して決定＆発動" : "Vを離して決定";
            var hintSize = FontAssets.MouseText.Value.MeasureString(hint);
            Utils.DrawBorderString(sb, hint, center - new Vector2(hintSize.X / 2f, -98f), new Color(220,220,220), 0.9f);
        }

        // ===== ヘルパー ======================================================
        Vector2 IconPos(Vector2 center, int i)
        {
            // 表示も「上(-90°)から時計回り」で配置 → 判定と完全一致
            float angle = -MathHelper.PiOver2 + i * MathHelper.TwoPi / Options.Length;
            float r = RADIUS * EaseOut(openT);
            return center + new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * r;
        }

        static float EaseOut(float x) => 1f - (float)Math.Pow(1f - x, 3f);

        // テクスチャ取得（無ければ Dummy にフォールバック）
        static Texture2D GetIconTexture(WarpMode mode)
        {
            string path = $"TranslateTest2/Items/Tools/AiPhone_{mode}";
            if (ModContent.HasAsset(path))
                return ModContent.Request<Texture2D>(path).Value;
            return ModContent.Request<Texture2D>("TranslateTest2/Items/Tools/AiPhone_Dummy").Value;
        }
    }
}
