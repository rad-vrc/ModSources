// Systems/AiPhoneRadialSystem.cs
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using TranslateTest2.Content.Items.Tools; // AiPhone / WarpMode
using TranslateTest2.Common.UI;          // AiPhoneRadialUI

namespace TranslateTest2.Common.Systems
{
    /// <summary>
    /// V長押しでラジアルUIを表示（UI中央固定）。離したら選択＆（既定）即発動。
    /// </summary>
    public class AiPhoneRadialSystem : ModSystem
    {
        public static ModKeybind HoldWheelKey;   // 既定: V（長押し）
        private bool _isOpen;

        private UserInterface _ui;
        internal AiPhoneRadialUI _state;
        private GameTime _lastUpdateUiGameTime;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                _ui = new UserInterface();
                _state = new AiPhoneRadialUI();
                _state.Activate(); // ガイド準拠: 初期化/OnActivate を適切なタイミングで実行
            }
            // Use human-readable display name; tML will localize via Keybinds."<display>".DisplayName
            HoldWheelKey = KeybindLoader.RegisterKeybind(Mod, "AI Phone: Hold Radial Wheel", "V");
        }

        public override void Unload()
        {
            _ui = null;
            _state = null;
            HoldWheelKey = null;
        }

        public override void PostUpdateInput()
        {
            if (Main.dedServ) return;

            // 押し始め：所持していれば表示開始
            if (HoldWheelKey != null && HoldWheelKey.JustPressed && HasAiPhone(Main.LocalPlayer))
            {
                _isOpen = true;
                _state?.ResetAnimOnOpen();
                _ui?.SetState(_state);
                SoundEngine.PlaySound(SoundID.MenuOpen);
            }

            // 離した瞬間：コミット＆閉じる
            if (HoldWheelKey != null && _isOpen && !HoldWheelKey.Current)
            {
                int hovered = _state?.HoveredIndex ?? -1;
                if (hovered != -1 && HasAiPhone(Main.LocalPlayer))
                {
                    var item = FindAiPhone(Main.LocalPlayer);
                    if (item?.ModItem is AiPhone ai)
                    {
                        var chosen = _state.Options[hovered];
                        ai.Mode = chosen;
                        ai.Item.SetNameOverride($"AI Phone ({ai.Mode})");
                        if (AiPhoneRadialUI.QUICK_CAST_ON_RELEASE)
                            AiPhone.PerformMode(Main.LocalPlayer, ai.Mode);
                        SoundEngine.PlaySound(SoundID.MenuTick);
                    }
                }
                _isOpen = false;
                _ui?.SetState(null);
                SoundEngine.PlaySound(SoundID.MenuClose);
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            _lastUpdateUiGameTime = gameTime;
            if (_ui?.CurrentState != null)
                _ui.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(System.Collections.Generic.List<GameInterfaceLayer> layers)
        {
            if (!_isOpen) return;

            int mouseTextIndex = layers.FindIndex(l => l.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex == -1) return;

            layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                "TranslateTest2: AiPhone Radial (UI Scale)",
                () =>
                {
                    if (_lastUpdateUiGameTime != null && _ui?.CurrentState != null)
                    {
                        _ui.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                    }
                    return true;
                },
                InterfaceScaleType.UI));
        }

        // 所持チェック / 取得
        private static bool HasAiPhone(Player p) => FindAiPhone(p) != null;
        private static Item FindAiPhone(Player p)
        {
            int type = ModContent.ItemType<AiPhone>();
            for (int i = 0; i < p.inventory.Length; i++)
                if (p.inventory[i].type == type) return p.inventory[i];
            return null;
        }
    }
}
