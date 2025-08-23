using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using TranslateTest2.Content.Items.Tools;

namespace TranslateTest2.Common.UI
{
    /// <summary>
    /// シンプルで保守性の高いラジアルメニューUI（ガイド準拠）
    /// </summary>
    public class AiPhoneRadialUI : UIState
    {
        // 設定
        private const float RADIUS = 90f;
        public const float ICON_SCALE = 1.5f;
        public const float HOVER_SCALE = 1.8f;

        // オプション
        public readonly WarpMode[] Options = {
            WarpMode.Spawn, WarpMode.Bed, WarpMode.Ocean, 
            WarpMode.Hell, WarpMode.Death, WarpMode.Random
        };

        // UI要素
        private RadialMenuPanel _menuPanel;
        private RadialOptionButton[] _optionButtons;

        // 状態
        private float _openAnimation = 0f;
        private int _hoveredIndex = -1;

        public int HoveredIndex => _hoveredIndex;

        public override void OnInitialize()
        {
            // ルートをフルスクリーンに
            Width.Set(0f, 1f);
            Height.Set(0f, 1f);

            // メインパネル（透明背景、中央配置）
            _menuPanel = new RadialMenuPanel();
            _menuPanel.Width.Set(220f, 0f);  // RADIUS * 2 + 余白
            _menuPanel.Height.Set(220f, 0f);
            _menuPanel.HAlign = 0.5f;
            _menuPanel.VAlign = 0.5f;
            Append(_menuPanel);

            // オプションボタン配置
            _optionButtons = new RadialOptionButton[Options.Length];
            for (int i = 0; i < Options.Length; i++)
            {
                _optionButtons[i] = new RadialOptionButton(Options[i], i, this);
                _menuPanel.Append(_optionButtons[i]);
            }

            // 初期レイアウトを確定
            Recalculate();
            _menuPanel.Recalculate();
        }

        public override void OnActivate()
        {
            // UIが有効化されたタイミングでレイアウトとアニメを初期化
            ResetAnimOnOpen();
            Recalculate();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            // 開きアニメーション
            _openAnimation = MathHelper.Clamp(_openAnimation + 0.15f, 0f, 1f);
            
            // ボタン位置更新
            UpdateButtonPositions();
        }

        private void UpdateButtonPositions()
        {
            var dims = _menuPanel.GetDimensions();
            Vector2 center = new Vector2(dims.Width * 0.5f, dims.Height * 0.5f);
            float currentRadius = RADIUS * EaseOut(_openAnimation);

            for (int i = 0; i < _optionButtons.Length; i++)
            {
                float angle = -MathHelper.PiOver2 + i * MathHelper.TwoPi / Options.Length;
                Vector2 offset = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * currentRadius;
                
                _optionButtons[i].Left.Set(center.X + offset.X - 20f, 0f); // 20f = アイコン半分
                _optionButtons[i].Top.Set(center.Y + offset.Y - 20f, 0f);
                // 位置変更を即時レイアウトに反映
                _optionButtons[i].Recalculate();
            }
        }

        public void OnOptionHovered(int index)
        {
            _hoveredIndex = index;
        }

        public void OnOptionUnhovered(int index)
        {
            if (_hoveredIndex == index)
                _hoveredIndex = -1;
        }

        public void ResetAnimOnOpen()
        {
            _openAnimation = 0f;
            _hoveredIndex = -1;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            // 念のため、描画前にも配置を更新（Update未呼びの保険）
            UpdateButtonPositions();

            // ヒント表示
            DrawHint(spriteBatch);
        }

        private void DrawHint(SpriteBatch spriteBatch)
        {
            string hint = Terraria.Localization.Language.GetTextValue("Mods.TranslateTest2.UI.AiPhoneRadial.Hint.ReleaseToCast");
            var dims = _menuPanel.GetDimensions();
            Vector2 center = new Vector2(dims.X + dims.Width * 0.5f, dims.Y + dims.Height * 0.5f);
            Vector2 hintSize = FontAssets.MouseText.Value.MeasureString(hint);
            Vector2 hintPos = center - new Vector2(hintSize.X * 0.5f, -110f);

            Utils.DrawBorderString(spriteBatch, hint, hintPos, Color.LightGray, 0.9f);
        }

        private static float EaseOut(float x) => 1f - (float)Math.Pow(1f - x, 3f);
    }

    /// <summary>
    /// ラジアルメニューのメインパネル（マウス干渉処理）
    /// </summary>
    public class RadialMenuPanel : UIElement
    {
        public override void OnInitialize()
        {
            // UIElementベースで透明パネルとして動作
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            
            // マウス干渉防止（ガイド準拠）
            if (ContainsPoint(Main.MouseScreen))
            {
                Main.LocalPlayer.mouseInterface = true;
                Terraria.GameInput.PlayerInput.LockVanillaMouseScroll("TranslateTest2/AiPhoneRadial");
            }
        }
    }

    /// <summary>
    /// 個別のオプションボタン（イベント駆動）
    /// </summary>
    public class RadialOptionButton : UIElement
    {
        private readonly WarpMode _mode;
        private readonly int _index;
        private readonly AiPhoneRadialUI _parentUI;
        private Texture2D _texture;
        private bool _isHovered = false;

        public RadialOptionButton(WarpMode mode, int index, AiPhoneRadialUI parentUI)
        {
            _mode = mode;
            _index = index;
            _parentUI = parentUI;
            
            Width.Set(40f, 0f);
            Height.Set(40f, 0f);
        }

        public override void OnInitialize()
        {
            // テクスチャ読み込み
            string path = $"TranslateTest2/Content/Items/Tools/AiPhone_{_mode}";
            _texture = ModContent.HasAsset(path) 
                ? ModContent.Request<Texture2D>(path, AssetRequestMode.ImmediateLoad).Value
                : ModContent.Request<Texture2D>("TranslateTest2/Content/Items/Tools/AiPhone_Dummy", AssetRequestMode.ImmediateLoad).Value;
        }

        public override void MouseOver(UIMouseEvent evt)
        {
            base.MouseOver(evt);
            _isHovered = true;
            _parentUI.OnOptionHovered(_index);
        }

        public override void MouseOut(UIMouseEvent evt)
        {
            base.MouseOut(evt);
            _isHovered = false;
            _parentUI.OnOptionUnhovered(_index);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            var dims = GetDimensions();
            Vector2 center = new Vector2(dims.X + dims.Width * 0.5f, dims.Y + dims.Height * 0.5f);
            Vector2 origin = new Vector2(_texture.Width, _texture.Height) * 0.5f;
            
            float scale = AiPhoneRadialUI.ICON_SCALE;
            Color color = Color.White;
            
            if (_isHovered)
            {
                scale = AiPhoneRadialUI.HOVER_SCALE;
                // グロー効果
                spriteBatch.Draw(_texture, center + Vector2.One, null, 
                    new Color(255, 255, 255, 60), 0f, origin, scale * 1.1f, SpriteEffects.None, 0f);
                
                // ラベル表示
                DrawLabel(spriteBatch, center);
            }

            spriteBatch.Draw(_texture, center, null, color, 0f, origin, scale, SpriteEffects.None, 0f);
        }

        private void DrawLabel(SpriteBatch spriteBatch, Vector2 center)
        {
            string key = $"Mods.TranslateTest2.Items.AiPhone.Modes.{_mode}";
            string label = Terraria.Localization.Language.GetTextValue(key);
            if (string.IsNullOrEmpty(label)) 
                label = _mode.ToString();

            Vector2 labelSize = FontAssets.MouseText.Value.MeasureString(label);
            Vector2 labelPos = center - new Vector2(labelSize.X * 0.5f, 35f);
            
            Utils.DrawBorderString(spriteBatch, label, labelPos, Color.White, 0.95f);
        }
    }
}