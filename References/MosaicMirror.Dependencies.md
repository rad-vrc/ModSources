# QoLCompendium MosaicMirror 依存関係まとめ (tModLoader 1.4.4)

本メモは `References/QoLCompendium_Re` のソースを基に、MosaicMirror とその周辺機能の依存・連携点を簡潔に整理したものです。TranslateTest2 側での条件付き融合（QoLCompendium 有効時のみ）に利用できます。

---

## 対象ソース（相対パス）

- QoLCompendium/Content/Items/Tools/Mirrors/MosaicMirror.cs
- QoLCompendium/Content/Items/Tools/Mirrors/CursedMirror.cs
- QoLCompendium/Content/Items/Tools/Mirrors/MirrorOfReturn.cs
- QoLCompendium/Content/Items/Tools/Mirrors/TeleportationMirror.cs
- QoLCompendium/Content/Items/Tools/Mirrors/WarpMirror.cs
- QoLCompendium/Content/Items/Tools/Mirrors/WormholeMirror.cs
- QoLCompendium/Core/QoLCPlayer.cs
- QoLCompendium/Core/Changes/WorldChanges/MapTeleporting.cs
- QoLCompendium/Core/ModConditions.cs
- QoLCompendium/Core/QoLCItem.cs（補助: 研究数達成フラグ）

---

## MosaicMirror の仕様

- クラス: `public class MosaicMirror : ModItem`
- 読み込み条件: `IsLoadingEnabled(Mod) => !itemConfig.DisableModdedItems || itemConfig.Mirrors`
- 既定: `CloneDefaults(50)`, `Item.SetShopValues(Lime7, 10g)`, 研究数 1
- 非消費化: `OnConsumeItem` で `Item.stack++`
- モード切替:
  - フィールド: `public int Mode;`（0/1/2）
  - 右クリックで `Mode` を循環（0 → 1 → 2 → 0）
  - セーブ/ロード: `Tag["MosaicMirrorMode"]` に保存・復元
  - `UpdateInventory` で `Item.SetNameOverride(...)` により動的名称を上書き
    - Mode 0: `Mods.QoLCompendium.ItemNames.MosaicMirror.CursedMirror`
    - Mode 1: `...MirrorOfReturn`
    - Mode 2: `...TeleportationMirror`
- 所持時フラグ: `player.GetModPlayer<QoLCPlayer>().warpMirror = true;`
  - これにより QoLCPlayer のマップ左クリックTP（後述）が有効化
- 情報アクセ連動: `UpdateInfoAccessory`（`itemConfig.InformationAccessories` 有効時）で `InfoPlayer` の複数フラグを true 化
- 使用挙動（UseStyle, 中間フレームで発動）:
  - 共通: グラップル解除、`aiStyle == 7` の投擲プロジェクトタイル Kill、Dust 演出
  - Mode 0（Cursed）: 最終死亡地点 `lastDeathPostion` が有効ならそこへ `Player.Teleport(...)`、無ければメッセージ表示
  - Mode 1（Return）: `Player.DoPotionOfReturnTeleportationAndSetTheComebackPoint()`
  - Mode 2（Teleportation）: `Player.TeleportationPotion()`
- レシピ:
  - 生成: `ModConditions.GetItemRecipe(() => itemConfig.Mirrors, Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled")`
  - 素材: `CursedMirror`, `MirrorOfReturn`, `TeleportationMirror`, `WarpMirror`, `WormholeMirror`
  - タイル: 114（Tinkerer's Workshop）
- ツールチップ:
  - `itemConfig.Mirrors` による無効化表示
  - `itemConfig.InformationAccessories` が無効なとき Tooltip1 に Disabled 追記（`Mods.QoLCompendium.CommonItemTooltips.Disabled`）

---

## 関連ミラーの要点

- CursedMirror
  - UseStyle: 死亡地点TP（Mosaic Mode 0 と同等）
  - レシピ: Tombstone系, GoldBars, 鏡, Anvil
- MirrorOfReturn
  - UseStyle: `DoPotionOfReturnTeleportationAndSetTheComebackPoint()`（Mode 1 相当）
  - レシピ: Recall系, GoldBars, 鏡, Anvil
- TeleportationMirror
  - UseStyle: `TeleportationPotion()`（Mode 2 相当）
  - レシピ: Teleportation系, GoldBars, 鏡, Anvil
- WarpMirror
  - `UpdateInventory`: `QoLCPlayer.warpMirror = true;`
  - `CanUseItem=false`（直接使用不可、マップ操作で効果）
- WormholeMirror（重要なフック）
  - `On_Player.HasUnityPotion`/`TakeUnityPotion` にフック
  - プレイヤーが WormholeMirror または MosaicMirror を所持している場合:
    - HasUnityPotion: true を返す（ポーション無しでもワームホール可能）
    - TakeUnityPotion: ポーション消費をスキップ

---

## 連動システム

- QoLCPlayer（ModPlayer）
  - `Reset()` 毎tick: `warpMirror = false` に戻す（所持品の `UpdateInventory` で都度 true に）
  - `PostUpdateMiscEffects()`:
    - 条件: ローカルプレイヤー、フルスクリーンマップ、左クリック、`warpMirror == true`
    - 動作: TownNPC のクリック範囲上でマップを閉じ、その NPC 位置へ `Player.Teleport(...)`
- MapTeleporting（ModSystem）
  - `PostDrawFullscreenMap`: `mainConfig.MapTeleporting` が true のとき、右クリックで任意座標にTP
  - `NetMessage.SendData(65)` による同期送信あり

---

## 設定トグル

- `QoLCompendium.itemConfig.Mirrors`（アイテム群の有効/無効）
- `QoLCompendium.itemConfig.InformationAccessories`（情報アクセ連動の有無）
- `QoLCompendium.mainConfig.MapTeleporting`（フルスクリーンマップ右クリックTP）

---

## ローカライズキー（参照）

- 名称上書き: `Mods.QoLCompendium.ItemNames.MosaicMirror.{CursedMirror|MirrorOfReturn|TeleportationMirror}`
- 無効化文言: `Mods.QoLCompendium.CommonItemTooltips.Disabled`
- レシピ条件表示: `Mods.QoLCompendium.ItemToggledConditions.ItemEnabled`

---

## TranslateTest2 側での統合指針（条件付き融合）

- 存在検出・型解決:
  - `if (ModLoader.TryGetMod("QoLCompendium", out var qlc) && qlc.TryFind<ModItem>("MosaicMirror", out var mosaic)) { /* レシピ登録 */ }`
- レシピ登録先: `ModSystem.AddRecipes()` 推奨
- 設計上の注意:
  - 名前は `SetNameOverride` により動的に変わるため、論理分岐は名称に依存しない
  - Mosaic 所持は `warpMirror` と Wormhole フックの恩恵を付与する。融合後も同等機能を保持させるかは仕様次第（素材消費しない、あるいは融合品側で `warpMirror` 相当機能を再現 など）
  - マップ右クリックTPは `mainConfig.MapTeleporting` のみで発火（Mosaic 所持は不要）

---

## 既知の副作用/エッジケース

- Grapple/`aiStyle == 7` のプロジェクタイルを強制 Kill（他Modの同 aiStyle 装備が巻き込まれる可能性）
- 死亡地点が未設定（0,0）の場合、Mode 0 はテキスト表示のみでTPしない
- `QoLCPlayer.Reset()` により所持解除の即時反映が行われる（旗は毎tick更新前提）

---

## 簡易チェックリスト

- [ ] QoLCompendium が存在する時のみ融合レシピを登録
- [ ] `TryFind("MosaicMirror")` で Type 解決し、素材に使用
- [ ] 競合を避けるため、名称ではなく Type/ModItem.FullName を用いる
- [ ] 仕様として Mosaic の warpMirror/Wormhole 代替効果の継続方針を決める
- [ ] ローカライズは TranslateTest2 側のキー空間（`Mods.TranslateTest2.*`）で実装

作成日: 2025-08-19
