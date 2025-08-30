using DragonLens.Core.Loaders.UILoading;
using DragonLens.Core.Systems.ThemeSystem;
using DragonLens.Helpers;
using ReLogic.Localization.IME;
using ReLogic.OS;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.UI;

namespace DragonLens.Content.GUI.FieldEditors
{
    public enum InputType
    {
        text,
        integer,
        number
    }

    /// <summary>
    /// IME 対応テキストフィールド。
    /// - IMEの合成文字列（CompositionString）を表示
    /// - フォーカス（typing）の安定化。CurrentSelected の消し忘れ／他UIとの競合で入力が“未入力扱い”になるのを防止
    /// - Backspace と合成中の競合回避
    /// </summary>
    internal class TextField : SmartUIElement
    {
        public bool typing;
        public bool updated;
        public bool reset;
        public InputType inputType;

        /// <summary>現在確定済みの文字列</summary>
        public string currentValue = "";

        // 直前フレームの合成中フラグ（Backspace対策）
        private bool _oldHasCompositionString;

        // 現在選択中の TextField（フォーカス管理を単一箇所に）
        public static TextField CurrentSelected;

        public TextField(InputType inputType = InputType.text)
        {
            this.inputType = inputType;
            Width.Set(130, 0);
            Height.Set(24, 0);
        }

        public void SetTyping()
        {
            typing = true;
            Main.blockInput = true;               // ゲーム側の入力をブロック
            PlayerInput.WritingText = true;       // 文字入力モード
            Main.clrInput();                      // 直前のバッファをクリア
            CurrentSelected = this;               // 自分を選択扱いに
        }

        public void SetNotTyping()
        {
            typing = false;
            Main.blockInput = false;
            PlayerInput.WritingText = false;

            // ★ 自分が選択中だった場合のみクリア（他要素を巻き込まない）
            if (CurrentSelected == this)
                CurrentSelected = null;
        }

        public override void SafeClick(UIMouseEvent evt)
        {
            SetTyping();
        }

        public override void SafeRightClick(UIMouseEvent evt)
        {
            SetTyping();
            currentValue = "";
            updated = true;
        }

        public override void SafeUpdate(GameTime gameTime)
        {
            if (reset)
            {
                updated = false;
                reset = false;
            }

            if (updated)
                reset = true;

            // ★ フォーカスの安定化
            //   ・テキスト外を左クリックしたら解除
            //   ・「自分が選択中でないのに typing が true」→ 競合と判断して解除
            bool clickedOutside = Main.mouseLeft && !IsMouseHovering;
            bool lostOwnership = typing && CurrentSelected != this;

            if (clickedOutside || lostOwnership)
                SetNotTyping();

            // 入力処理は Update で実行して描画と分離（IME安定化）
            if (typing)
                HandleText();
        }

        private static readonly Regex NumberRegex = new(@"^-?\d*\.?\d*$", RegexOptions.Compiled);
        private static readonly Regex IntegerRegex = new(@"^-?\d*$", RegexOptions.Compiled);

        public void HandleText()
        {
            // Esc でフォーカス解除
            if (Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
            {
                SetNotTyping();
                return;
            }

            PlayerInput.WritingText = true;

            // ★ IME 状態更新（候補など）
            Main.instance.HandleIME();

            // ★ tML/Terraria 標準のテキスト取得（IME/デッドキー対応）
            string newText = Main.GetInputText(currentValue);

            // ★ 合成中 + Backspace のバグ回避：
            // GetInputText() は IME 合成直後の Backspace で確定済み文字列まで消してしまう場合がある。
            // 直前フレームで合成中だった場合は、このフレームの Backspace を確定文字列には効かせない。
            if (_oldHasCompositionString && Main.inputText.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Back))
            {
                newText = currentValue; // 強制的に据え置き
            }

            bool accept = true;
            if (inputType == InputType.integer)
            {
                accept = IntegerRegex.IsMatch(newText);
            }
            else if (inputType == InputType.number)
            {
                accept = NumberRegex.IsMatch(newText); // 先頭マイナス/小数点1回まで
            }

            if (accept && newText != currentValue)
            {
                currentValue = newText;
                updated = true;
            }

            // 次フレーム用に合成中フラグを保存
            _oldHasCompositionString = Platform.Get<IImeService>().CompositionString is { Length: > 0 };
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // 背景
            GUIHelper.DrawBox(spriteBatch, GetDimensions().ToRectangle(), ThemeHandler.ButtonColor);

            if (typing)
            {
                // フォーカス中のアウトライン
                GUIHelper.DrawOutline(spriteBatch, GetDimensions().ToRectangle(), ThemeHandler.ButtonColor.InvertColor());

                // ★ Windows IME の候補パネル描画（CompositionString が無ければ何も描かれない）
                Main.instance.DrawWindowsIMEPanel(GetDimensions().Position());
            }

            Vector2 pos = GetDimensions().Position() + Vector2.One * 4;
            const float scale = 0.75f;

            // 確定済み文字列
            string displayed = currentValue ?? "";
            Utils.DrawBorderString(spriteBatch, displayed, pos, Color.White, scale);

            // キャレット位置を進める
            pos.X += FontAssets.MouseText.Value.MeasureString(displayed).X * scale;

            // ★ 合成中文字列（黄色っぽく）
            string compositionString = Platform.Get<IImeService>().CompositionString;
            if (compositionString is { Length: > 0 })
            {
                Utils.DrawBorderString(spriteBatch, compositionString, pos, new Color(255, 240, 20), scale);
                pos.X += FontAssets.MouseText.Value.MeasureString(compositionString).X * scale;
            }

            // 点滅キャレット
            if (Main.GameUpdateCount % 20 < 10)
                Utils.DrawBorderString(spriteBatch, "|", pos, Color.White, scale);
        }
    }
}
