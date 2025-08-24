using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using TranslateTest2.Content.Items.Tools;
using TranslateTest2.Common.UI;

namespace TranslateTest2.Common.Systems
{
    /// <summary>
    /// Vキー長押しでラジアルUIを表示する簡潔なシステム
    /// </summary>
    public class AiPhoneRadialSystem : ModSystem
    {
        public static ModKeybind HoldWheelKey;
        
        private UserInterface _ui;
        internal AiPhoneRadialUI _state;
        private GameTime _lastUpdateUiGameTime;
        private bool _isOpen;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                _ui = new UserInterface();
                _state = new AiPhoneRadialUI();
                _state.Activate();
            }
            
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

            // キー押し始め - UI表示
            if (HoldWheelKey?.JustPressed == true && HasAiPhone(Main.LocalPlayer))
            {
                OpenRadialMenu();
            }

            // キー離した - 選択確定
            if (_isOpen && HoldWheelKey?.Current != true)
            {
                CloseAndExecuteSelection();
            }
        }

        private void OpenRadialMenu()
        {
            _isOpen = true;
            _state?.ResetAnimOnOpen();
            _state?.Recalculate(); // レイアウトと初期位置を即時反映
            _ui?.SetState(_state);
            SoundEngine.PlaySound(SoundID.MenuOpen);
        }

        private void CloseAndExecuteSelection()
        {
            int selectedIndex = _state?.HoveredIndex ?? -1;
            
            if (selectedIndex != -1 && HasAiPhone(Main.LocalPlayer))
            {
                ExecuteSelection(selectedIndex);
            }

            _isOpen = false;
            _ui?.SetState(null);
            SoundEngine.PlaySound(SoundID.MenuClose);
        }

        private void ExecuteSelection(int index)
        {
            var item = FindAiPhone(Main.LocalPlayer);
            if (item?.ModItem is AiPhone aiPhone)
            {
                var selectedMode = _state.Options[index];
                aiPhone.Mode = selectedMode;
                aiPhone.Item.SetNameOverride($"AI Phone ({aiPhone.Mode})");
                
                // 即座にワープ実行
                AiPhone.PerformMode(Main.LocalPlayer, aiPhone.Mode);
                SoundEngine.PlaySound(SoundID.MenuTick);
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            _lastUpdateUiGameTime = gameTime;
            _ui?.Update(gameTime); // 標準パターン：内部状態も更新される
        }

        public override void ModifyInterfaceLayers(System.Collections.Generic.List<GameInterfaceLayer> layers)
        {
            if (_ui?.CurrentState == null) return;

            int mouseTextIndex = layers.FindIndex(l => l.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex == -1) return;

            layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                "TranslateTest2: AiPhone Radial",
                () =>
                {
                    if (_lastUpdateUiGameTime != null)
                    {
                        _ui.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                    }
                    return true;
                },
                InterfaceScaleType.UI));
        }

        // ユーティリティメソッド
        private static bool HasAiPhone(Player player) => FindAiPhone(player) != null;
        
        private static Item FindAiPhone(Player player)
        {
            int aiPhoneType = ModContent.ItemType<AiPhone>();
            for (int i = 0; i < player.inventory.Length; i++)
            {
                if (player.inventory[i].type == aiPhoneType)
                    return player.inventory[i];
            }
            return null;
        }
    }
}