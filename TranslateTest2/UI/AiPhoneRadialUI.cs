// UI/AiPhoneRadialUI.cs
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input; // KeyboardState, Keys
using Terraria;
using Terraria.GameContent; // FontAssets, TextureAssets
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.GameInput; // PlayerInput.ScrollWheelDeltaForUI
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
    public static readonly bool RELATIVE_ROTATE_MODE = true;  // 相対回転で全周選択（マウス位置の半面に依存しない）
        const float RADIUS               = 90f;          // 円半径（UI座標）
    const float BASE_ICON_SCALE      = 1.50f;        // 32x28 をさらに少し拡大
        const float HOVER_SCALE_ADD      = 0.15f;        // ホバー時の追い増し

        // Dummyは選択肢から除外
        public readonly WarpMode[] Options = new[] {
            WarpMode.Spawn, WarpMode.Bed, WarpMode.Ocean, WarpMode.Hell, WarpMode.Death, WarpMode.Random
        };

        // ===== 状態 ==========================================================
        UIElement anchor;            // 常にUI中央に配置されるアンカー
        float openT;                 // 0→1 開きアニメ
        int hovered = -1;            // 現在ホバー中
    int frameCounter = 0;        // フレームカウンター（デバッグ用）
        public int HoveredIndex => hovered;

    // 相対回転モード用状態
    bool relInited = false;
    float relAccum = 0f;         // 角度差累積（ラジアン）
    float relPrevAngle = 0f;     // 前フレーム角度
    int relStartIndex = 0;       // 開いた瞬間のセクター
    int relExtraSteps = 0;       // ホイール等の加算回転
    KeyboardState _prevKeyState; // 前フレームのキー状態（Q/Eのエッジ検出用）

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
            _prevKeyState = Keyboard.GetState();
        }

        public void ResetAnimOnOpen()
        {
            openT = 0f;
            hovered = -1;
            // 相対回転モード初期化
            relInited = false;
            relAccum = 0f;
            relPrevAngle = 0f;
            relStartIndex = 0;
            relExtraSteps = 0;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // 開きアニメ進行
            openT = MathHelper.Clamp(openT + 0.2f, 0f, 1f);

            Vector2 centerUI = anchor.GetDimensions().Center();
            Vector2 mousePx = new Vector2(Main.mouseX, Main.mouseY);
            Vector2 centerPx = Vector2.Transform(centerUI, Main.UIScaleMatrix);
            Vector2 vPx = mousePx - centerPx; // 入力・判定はピクセル座標で統一
            // フレームカウンター更新（デバッグ用途）
            frameCounter++;

            // 中心に近すぎる場合は前回の選択を維持
            if (vPx.LengthSquared() < 1e-6f)
            {
                if (hovered < 0) hovered = 0;
                _prevKeyState = Main.keyState;
                return;
            }

            // ==== 6方向ユニットベクトルとのドット積最大化（Px統一） ====
            int DecideByDot(Vector2 v)
            {
                Vector2 dir = v;
                float lenSq2 = dir.LengthSquared();
                if (lenSq2 > 1e-12f)
                    dir /= (float)Math.Sqrt(lenSq2);
                int bestIdx = 0;
                float best = -2f;
                float step = MathHelper.TwoPi / Options.Length;
                for (int i = 0; i < Options.Length; i++)
                {
                    float a = -MathHelper.PiOver2 + i * step; // 上から時計回り
                    Vector2 u = new Vector2((float)Math.Cos(a), (float)Math.Sin(a));
                    float d = Vector2.Dot(dir, u);
                    if (d > best)
                    {
                        best = d;
                        bestIdx = i;
                    }
                }
                return bestIdx;
            }

            if (RELATIVE_ROTATE_MODE)
            {
                // 相対角はピクセル座標基準で取得
                Vector2 vSrc = vPx;
                float currAngle = (float)Math.Atan2(vSrc.Y, vSrc.X);
                float stepAngle = MathHelper.TwoPi / Options.Length;

                if (!relInited)
                {
                    relInited = true;
                    relPrevAngle = currAngle;
                    relStartIndex = DecideByDot(vSrc);
                    hovered = relStartIndex;
                    _prevKeyState = Main.keyState;
                    return;
                }

                // 符号付き最短角度差に正規化（[-π, π]）
                float d = currAngle - relPrevAngle;
                d = (float)Math.IEEERemainder(d, MathHelper.TwoPi);
                if (d > MathHelper.Pi) d -= MathHelper.TwoPi;
                if (d < -MathHelper.Pi) d += MathHelper.TwoPi;

                relPrevAngle = currAngle;
                relAccum += d;

                // ホイール補助（上=前のセクター、下=次のセクター）
                int scroll = PlayerInput.ScrollWheelDeltaForUI;
                if (scroll != 0)
                    relExtraSteps += (scroll > 0) ? -1 : 1;

                // Q/E エッジ検出（1押し=1ステップ）
                var ks = Main.keyState;
                bool qDown = ks.IsKeyDown(Keys.Q) && !_prevKeyState.IsKeyDown(Keys.Q);
                bool eDown = ks.IsKeyDown(Keys.E) && !_prevKeyState.IsKeyDown(Keys.E);
                if (qDown) relExtraSteps -= 1;
                if (eDown) relExtraSteps += 1;

                int steps = (int)Math.Round(relAccum / stepAngle);
                int idx = relStartIndex + steps + relExtraSteps;
                idx %= Options.Length;
                if (idx < 0) idx += Options.Length;
                hovered = idx;
            }
            else
            {
                // 絶対方式（Px）
                hovered = DecideByDot(vPx);
            }

            // 前フレームのキー状態を更新（Q/Eエッジ検出用）
            _prevKeyState = Main.keyState;
        }

        protected override void DrawSelf(SpriteBatch sb)
        {
            base.DrawSelf(sb);

            Vector2 center = anchor.GetDimensions().Center();

            // アイコン群
            for (int i = 0; i < Options.Length; i++)
            {
                Texture2D tex = GetIconTexture(Options[i]);
                Vector2 pos = IconPos(center, i);
                Vector2 origin = tex.Size() / 2f;

                float scale = BASE_ICON_SCALE * EaseOut(openT);
                if (i == hovered) scale *= (1f + HOVER_SCALE_ADD);

                if (i == hovered)
                    sb.Draw(tex, pos + new Vector2(0, 1), null, new Color(255, 255, 255, 90), 0f, origin, scale * 1.18f, SpriteEffects.None, 0f);

                sb.Draw(tex, pos, null, Color.White, 0f, origin, scale, SpriteEffects.None, 0f);

                if (i == hovered)
                {
                    // Localized label for mode name
                    string key = $"Mods.TranslateTest2.Items.AiPhone.Modes.{Options[i]}";
                    string label = Terraria.Localization.Language.GetTextValue(key);
                    if (string.IsNullOrEmpty(label)) label = Options[i].ToString();
                    var size = FontAssets.MouseText.Value.MeasureString(label);
                    var textPos = pos - new Vector2(size.X / 2f, 30f);
                    Utils.DrawBorderString(sb, label, textPos, Color.White, 0.95f);
                }
            }

            // ヒント
            string hintKey = QUICK_CAST_ON_RELEASE ? "Mods.TranslateTest2.UI.AiPhoneRadial.Hint.ReleaseToCast" : "Mods.TranslateTest2.UI.AiPhoneRadial.Hint.ReleaseToSelect";
            string hint = Terraria.Localization.Language.GetTextValue(hintKey);
            if (string.IsNullOrEmpty(hint))
                hint = QUICK_CAST_ON_RELEASE ? "Release to cast" : "Release to select";
            if (RELATIVE_ROTATE_MODE)
            {
                string rel = Terraria.Localization.Language.GetTextValue("Mods.TranslateTest2.UI.AiPhoneRadial.Hint.RelativeOps");
                if (string.IsNullOrEmpty(rel)) rel = "[REL] Q/E: rotate, Wheel: rotate";
                hint += "  " + rel;
            }
            var hintSize = FontAssets.MouseText.Value.MeasureString(hint);
            Utils.DrawBorderString(sb, hint, center - new Vector2(hintSize.X / 2f, -98f), new Color(220, 220, 220), 0.9f);

            // デバッグ（F3〜F6）
            bool showBasicDebug = Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.F3);
            bool showCoordDebug = Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.F4);
            bool showModInterferenceDebug = Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.F5);
            bool showComparisonDebug = Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.F6);

            if (!(showBasicDebug || showCoordDebug || showModInterferenceDebug || showComparisonDebug))
                return;

            Vector2 mousePx = new Vector2(Main.mouseX, Main.mouseY);
            Vector2 debugCenterUI = anchor.GetDimensions().Center();
            Vector2 debugCenterPx = Vector2.Transform(debugCenterUI, Main.UIScaleMatrix);
            Vector2 debugV = mousePx - debugCenterPx;

            string debugText = "";

            if (showBasicDebug && debugV.LengthSquared() > 1e-6f)
            {
                float dx = debugV.X;
                float dy = debugV.Y;
                float rawMouseAngle = (float)Math.Atan2(debugV.Y, debugV.X);

                int calculatedSector = 0;
                {
                    if (debugV.LengthSquared() > 1e-9f)
                    {
                        float best = -2f; int bestIdx = 0; float step6 = MathHelper.TwoPi / Options.Length;
                        Vector2 dir2 = Vector2.Normalize(debugV);
                        for (int i = 0; i < Options.Length; i++)
                        {
                            float a = -MathHelper.PiOver2 + i * step6;
                            Vector2 u = new Vector2((float)Math.Cos(a), (float)Math.Sin(a));
                            float d2 = Vector2.Dot(dir2, u);
                            if (d2 > best) { best = d2; bestIdx = i; }
                        }
                        calculatedSector = bestIdx;
                    }
                }
                string judgmentProcess = "Px dot method";

                debugText += $"=== BASIC DEBUG (F3) ===\n";
                debugText += $"Frame: {frameCounter}\n";
                debugText += $"Raw Angle: {MathHelper.ToDegrees(rawMouseAngle):F1}°\n";
                debugText += $"Mouse Pos: ({dx:F1}, {dy:F1})\n";
                debugText += $"Calculated: {calculatedSector}, Actual: {hovered}\n";
                debugText += $"Process: {judgmentProcess}\n";
                debugText += $"tan(30°) threshold: {Math.Abs(dy) * 0.577f:F1}\n";
                debugText += $"|dx|: {Math.Abs(dx):F1}\n";
                debugText += $"dy <= 0: {dy <= 0}\n";
                debugText += $"dx > 0: {dx > 0}\n";
                if (RELATIVE_ROTATE_MODE)
                {
                    float stepAngleDbg = MathHelper.TwoPi / Options.Length;
                    int relStepsDbg = (int)Math.Round(relAccum / stepAngleDbg);
                    debugText += $"REL: inited={relInited}, start={relStartIndex}, accum={relAccum:F3}, steps={relStepsDbg}, extra={relExtraSteps}\n";
                }

                for (int i = 0; i < Options.Length; i++)
                {
                    float displayAngle = -MathHelper.PiOver2 + i * MathHelper.TwoPi / Options.Length;
                    float normalizedDisplay = ((displayAngle + MathHelper.PiOver2 + MathHelper.TwoPi) % MathHelper.TwoPi);
                    string marker = (i == hovered) ? " <-- SELECTED" : "";
                    debugText += $"Sector {i}: Display={MathHelper.ToDegrees(displayAngle):F1}°, Norm={MathHelper.ToDegrees(normalizedDisplay):F1}°{marker}\n";
                }
            }

            if (showCoordDebug)
            {
                Vector2 physicalCenter = new Vector2(Main.screenWidth, Main.screenHeight) * 0.5f;
                Vector2 centerDiff = debugCenterPx - physicalCenter;

                debugText += $"\n=== COORDINATE DEBUG (F4) ===\n";
                debugText += $"SCALE ANALYSIS:\n";
                debugText += $"Main.UIScale: {Main.UIScale:F3}\n";
                debugText += $"InterfaceScaleType: UI\n";
                debugText += $"Pointer: UserInterface.MousePosition\n";
                debugText += $"\nCOORDINATE ANALYSIS:\n";
                debugText += $"Main.MouseScreen: {Main.MouseScreen}\n";
                debugText += $"mousePx: {mousePx}\n";
                debugText += $"anchor.GetDimensions(): {anchor.GetDimensions()}\n";
                debugText += $"debugCenterPx: {debugCenterPx}\n";
                debugText += $"physicalCenter: {physicalCenter}\n";
                debugText += $"centerDiff: {centerDiff} (length: {centerDiff.Length():F3})\n";
                debugText += $"debugV: {debugV}\n";
                debugText += $"debugV.Length(): {debugV.Length():F2}\n";
                debugText += $"Screen Size: {Main.screenWidth}x{Main.screenHeight}\n";
            }

            if (showModInterferenceDebug)
            {
                debugText += $"\n=== MOD INTERFERENCE DEBUG (F5) ===\n";

                bool hasAbnormalValues = false;
                if (Main.UIScale <= 0.1f || Main.UIScale > 10f)
                {
                    debugText += $"ALERT: Abnormal UIScale: {Main.UIScale}\n";
                    hasAbnormalValues = true;
                }
                if (float.IsNaN(mousePx.X) || float.IsNaN(mousePx.Y))
                {
                    debugText += $"ALERT: NaN in mousePx: {mousePx}\n";
                    hasAbnormalValues = true;
                }
                if (debugCenterPx.X < 0 || debugCenterPx.Y < 0 || debugCenterPx.X > Main.screenWidth || debugCenterPx.Y > Main.screenHeight)
                {
                    debugText += $"ALERT: Center outside screen bounds: {debugCenterPx}\n";
                    hasAbnormalValues = true;
                }

                if (!hasAbnormalValues)
                {
                    debugText += "No interference detected\n";
                }

                Vector2 altCenter = new Vector2(Main.screenWidth, Main.screenHeight) / 2f;
                Vector2 altV = mousePx - altCenter;
                float altDistance = Vector2.Distance(debugCenterPx, altCenter);
                debugText += $"Alternative center: {altCenter}\n";
                debugText += $"Center distance: {altDistance:F2}\n";
            }

            if (showComparisonDebug && debugV.LengthSquared() > 1e-6f)
            {
                debugText += $"\n=== COMPARISON DEBUG (F6) ===\n";

                float rawAngle = (float)Math.Atan2(debugV.Y, debugV.X);
                float adjustedAngle = rawAngle + MathHelper.PiOver2;
                adjustedAngle = ((adjustedAngle % MathHelper.TwoPi) + MathHelper.TwoPi) % MathHelper.TwoPi;
                int newMethod = Math.Max(0, Math.Min(Options.Length - 1, (int)Math.Floor(adjustedAngle * Options.Length / MathHelper.TwoPi + 0.5f)));

                float altNormalizedAngle = ((rawAngle + MathHelper.Pi) / MathHelper.TwoPi) % 1.0f;
                int altMethod = (int)(altNormalizedAngle * Options.Length) % Options.Length;

                Vector2 screenCenter = new Vector2(Main.screenWidth, Main.screenHeight) / 2f;
                Vector2 altV2 = mousePx - screenCenter;
                float altRawAngle = (float)Math.Atan2(altV2.Y, altV2.X);
                float altAdjustedAngle = altRawAngle + MathHelper.PiOver2;
                altAdjustedAngle = ((altAdjustedAngle % MathHelper.TwoPi) + MathHelper.TwoPi) % MathHelper.TwoPi;
                int screenCenterMethod = Math.Max(0, Math.Min(Options.Length - 1, (int)Math.Floor(altAdjustedAngle * Options.Length / MathHelper.TwoPi + 0.5f)));

                debugText += $"New direct method: {newMethod}\n";
                debugText += $"Alternative normalized: {altMethod}\n";
                debugText += $"Screen center method: {screenCenterMethod}\n";

                Vector2 dbgMouseUI = Main.MouseScreen / Main.UIScale;
                Vector2 dbgCenterUI = anchor.GetDimensions().Center();
                Vector2 dbgVUI = dbgMouseUI - dbgCenterUI;
                Vector2 dbgMousePx = new Vector2(Main.mouseX, Main.mouseY);
                Vector2 dbgCenterPx2 = Vector2.Transform(dbgCenterUI, Main.UIScaleMatrix);
                Vector2 dbgVPx = dbgMousePx - dbgCenterPx2;
                float step6 = MathHelper.TwoPi / Options.Length;
                int bestUI = -1, bestPx = -1; float bestDotUI = -2f, bestDotPx = -2f;
                debugText += "Dots(UI|Px): ";
                for (int i = 0; i < Options.Length; i++)
                {
                    float a = -MathHelper.PiOver2 + i * step6;
                    Vector2 u = new Vector2((float)Math.Cos(a), (float)Math.Sin(a));
                    float du = 0f, dp = 0f;
                    if (dbgVUI.LengthSquared() > 1e-9f)
                        du = Vector2.Dot(Vector2.Normalize(dbgVUI), u);
                    if (dbgVPx.LengthSquared() > 1e-9f)
                        dp = Vector2.Dot(Vector2.Normalize(dbgVPx), u);
                    if (du > bestDotUI) { bestDotUI = du; bestUI = i; }
                    if (dp > bestDotPx) { bestDotPx = dp; bestPx = i; }
                    debugText += $"[{i}:{du:F3}|{dp:F3}] ";
                }
                debugText += $"=> bestUI={bestUI}, bestPx={bestPx}\n";

                if (newMethod != altMethod || newMethod != screenCenterMethod)
                    debugText += "ALERT: Methods disagree!\n";
                else
                    debugText += "All methods agree - GOOD!\n";
            }

            Utils.DrawBorderString(sb, debugText, new Vector2(10, 10), Color.Yellow, 0.7f);
        }

        // ===== ヘルパー ======================================================
        Vector2 IconPos(Vector2 center, int i)
        {
            // 「上(-90°)から時計回り」で配置（判定と完全一致）
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
