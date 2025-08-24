// Content/Items/Tools/AiPhone.cs
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Reflection;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.GameContent; // TextureAssets
using Terraria.Localization;
using TranslateTest2.Core;
using TranslateTest2.Content.Items.Tools;

namespace TranslateTest2.Content.Items.Tools
{
    public enum WarpMode { Dummy, Spawn, Bed, Ocean, Hell, Death, Random }

    public class AiPhone : ModItem
    {
        public WarpMode Mode;
    private static readonly WarpMode[] CycleModes = new[] { WarpMode.Spawn, WarpMode.Bed, WarpMode.Ocean, WarpMode.Hell, WarpMode.Death, WarpMode.Random };

        public override void SetDefaults() {
            Item.width = Item.height = 32;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = Item.useAnimation = 15;
            Item.rare = ItemRarityID.Red;
            Item.value = Item.sellPrice(gold: 30);
            Item.UseSound = SoundID.Item6;
            Item.noMelee = true;
            Item.channel = true;
            Item.maxStack = 1; // 安全: 右クリック動作は単体前提
        }

        // MosaicMirror/Shellphone式：右クリックで機能モードのみ巡回（Dummyは巡回に含めない）
        public override bool CanRightClick() => true;
        public override void RightClick(Player player) {
            int idx = System.Array.IndexOf(CycleModes, Mode);
            if (idx < 0) idx = 0; // Dummyなど不正値は最初の機能モードへ
            else idx = (idx + 1) % CycleModes.Length;
            Mode = CycleModes[idx];
            Item.SetNameOverride(FormatModeName(Mode));
            SoundEngine.PlaySound(SoundID.MenuTick);
        }

        // 右クリック後にデフォルトで1消費されるため、直ちに打ち消す
        public override void OnConsumeItem(Player player) {
            // 非消費化（MosaicMirrorと同じパターン）
            Item.stack++;
        }

        // 左クリック＝現在モードで即ワープ
        public override bool? UseItem(Player player) {
            PerformMode(player, Mode);
            return true;
        }

        public override void SaveData(TagCompound tag) => tag["mode"] = (int)Mode;
        public override void LoadData(TagCompound tag) => Mode = (WarpMode)tag.GetInt("mode");

        // インスタンス複製時にModeを維持
        public override ModItem Clone(Item newEntity)
        {
            var clone = (AiPhone)base.Clone(newEntity);
            clone.Mode = this.Mode;
            return clone;
        }

        // マルチ同期
        public override void NetSend(BinaryWriter writer)
        {
            writer.Write((int)Mode);
        }
        public override void NetReceive(BinaryReader reader)
        {
            Mode = (WarpMode)reader.ReadInt32();
        }

        // 情報系（必要ならここでまとめてON。細かいトグルは後でModConfigに）
        public override void UpdateInventory(Player p)
        {
            // 情報付与
            AiPhoneInfo.Apply(p);
            // ShellphoneDummy同様、Dummyは実モードとして扱わない（自動補正）
            if (Mode == WarpMode.Dummy)
                Mode = WarpMode.Spawn;
            // 名称をモードに同期（ロード直後や描画反映の安定化）
            Item.SetNameOverride(FormatModeName(Mode));
        }

        // 情報アクセサリフック：両親アイテム（Shellphone + MosaicMirror）の情報表示機能を完全継承
        public override void UpdateInfoAccessory(Player player)
        {
            // Shellphone系の情報表示継承（BiomeInfoAccessoryGlobalItemと同じ処理）
            player.GetModPlayer<InfoPlayer>().biomeDisplay = true;
            
            // MosaicMirror系の情報表示継承（AiPhoneInfo経由で実装済み）
            AiPhoneInfo.Apply(player);
        }


        // スプライト差し替え（7枚）
        public override string Texture => "TranslateTest2/Content/Items/Tools/AiPhone_Dummy";

        // パスヘルパ
        private static string TexPath(string name) => $"TranslateTest2/Content/Items/Tools/{name}";

        public override bool PreDrawInInventory(SpriteBatch sb, Vector2 pos, Rectangle frame, Color draw, Color itemClr, Vector2 origin, float scale)
        {
            var tex = ModContent.Request<Texture2D>(TexPath($"AiPhone_{Mode}")).Value;
            sb.Draw(tex, pos, null, draw, 0f, origin, scale, SpriteEffects.None, 0f);
            return false;
        }
        public override bool PreDrawInWorld(SpriteBatch sb, Color light, Color alpha, ref float rot, ref float scale, int whoAmI)
        {
            var tex = ModContent.Request<Texture2D>(TexPath($"AiPhone_{Mode}")).Value;
            var origin = new Vector2(tex.Width, tex.Height) * 0.5f;
            sb.Draw(tex, Item.Center - Main.screenPosition, null, light, 0f, origin, 1f, SpriteEffects.None, 0f);
            return false;
        }

        // —— ワープ実体（UI/ホットキーからも呼べるよう static） ——
        public static void PerformMode(Player p, WarpMode mode) {
            switch (mode) {
                case WarpMode.Spawn:
                    // Player.Shellphone_Spawn() が無い環境はフォールバック
                    var m = typeof(Player).GetMethod("Shellphone_Spawn", BindingFlags.Instance|BindingFlags.Public|BindingFlags.NonPublic);
                    if (m != null) m.Invoke(p, null);
                    else TeleportTo(p, new Vector2(Main.spawnTileX, Main.spawnTileY) * 16f);
                    break;
                case WarpMode.Bed:
                    TeleportTo(p, GetBedOrSpawn(p));
                    break;
                case WarpMode.Ocean:
                    CallIfExists(p, "MagicConch", () => TeleportTo(p, GetNearestOcean(p)));
                    break;
                case WarpMode.Hell:
                    CallIfExists(p, "DemonConch", () => TeleportTo(p, new Vector2(p.Center.X, (Main.maxTilesY - 200) * 16f)));
                    break;
                case WarpMode.Death:
                    if (p.lastDeathPostion != Vector2.Zero) TeleportTo(p, p.lastDeathPostion);
                    else if (p.whoAmI == Main.myPlayer) Main.NewText(Language.GetTextValue("Mods.TranslateTest2.Items.AiPhone.NoDeathPosition"));
                    break;
                case WarpMode.Random:
                    CallIfExists(p, "TeleportationPotion", () => { /* 代替なし */ });
                    break;
                default:
                    TeleportTo(p, GetBedOrSpawn(p));
                    break;
            }
        }

        public static string FormatModeName(WarpMode mode)
        {
            // Items.AiPhone.Modes.<Mode>
            string modeKey = $"Mods.TranslateTest2.Items.AiPhone.Modes.{mode}";
            string modeText = Language.GetTextValue(modeKey);
            // Items.AiPhone.ModeFormat with {0}
            string fmt = Language.GetTextValue("Mods.TranslateTest2.Items.AiPhone.ModeFormat");
            if (string.IsNullOrEmpty(fmt))
                return $"AI Phone ({modeText})"; // フォールバック
            return string.Format(fmt, modeText);
        }

        public static WarpMode NextMode(WarpMode current, int dir)
        {
            int idx = System.Array.IndexOf(CycleModes, current);
            if (idx < 0) idx = 0;
            idx = (idx + (dir >= 0 ? 1 : -1)) % CycleModes.Length;
            if (idx < 0) idx += CycleModes.Length;
            return CycleModes[idx];
        }

        private static readonly long[] _lastTpTick = new long[255];
        public static void TeleportTo(Player p, Vector2 world) {
            if (Main.myPlayer != p.whoAmI) return;
            // 再入防止: 直近のテレポートから一定ティックは無視（多重発火対策）
            long now = Main.GameUpdateCount;
            if (now - _lastTpTick[p.whoAmI] < 15) return; // ~0.25s @60fps
            _lastTpTick[p.whoAmI] = now;
            p.grappling[0] = -1; p.grapCount = 0;
            p.fallStart = (int)(world.Y / 16f);
            p.Teleport(world, 0, 0);
            NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, p.whoAmI, world.X, world.Y, 0);
            SoundEngine.PlaySound(SoundID.Item6, p.Center);
            p.velocity = Vector2.Zero;
        }
        static Vector2 GetBedOrSpawn(Player p) =>
            (p.SpawnX >= 0 ? new Vector2(p.SpawnX, p.SpawnY) : new Vector2(Main.spawnTileX, Main.spawnTileY)) * 16f;

        static Vector2 GetNearestOcean(Player p) {
            float leftX = 200 * 16f, rightX = (Main.maxTilesX - 200) * 16f;
            float targetX = (System.Math.Abs(p.Center.X - leftX) <= System.Math.Abs(p.Center.X - rightX)) ? leftX : rightX;
            return new Vector2(targetX, p.position.Y);
        }
        static void CallIfExists(Player p, string method, System.Action fallback) {
            var m = typeof(Player).GetMethod(method, BindingFlags.Instance|BindingFlags.Public|BindingFlags.NonPublic);
            if (m != null) m.Invoke(p, null); else fallback?.Invoke();
        }

        // —— レシピ（ここでOK。QoLCompendiumがある時だけ解禁） ——
        public override void AddRecipes() {
            if (ModLoader.TryGetMod("QoLCompendium", out var ql) &&
                ql.TryFind<ModItem>("MosaicMirror", out var mosaic))
            {
                var group = new RecipeGroup(() => "Any Shellphone",
                    ItemID.Shellphone, ItemID.ShellphoneSpawn,
                    ItemID.ShellphoneOcean, ItemID.ShellphoneHell);
                // 重複登録を避ける
                if (!RecipeGroup.recipeGroupIDs.ContainsKey("TranslateTest2:AnyShellphone"))
                    RecipeGroup.RegisterGroup("TranslateTest2:AnyShellphone", group);

                CreateRecipe()
                    .AddRecipeGroup("TranslateTest2:AnyShellphone", 1)
                    .AddIngredient(mosaic.Type, 1)
                    .AddTile(TileID.TinkerersWorkbench)
                    .Register();
            }
        }
    }
}
