# tModLoader における PNG 使用ベストプラクティス

この文書は、tModLoader プロジェクトに初めて参加する方向けに、PNG アセットの作り方・置き方・サイズやレイアウト・読み込みの流儀（パフォーマンス含む）を簡潔にまとめたものです。根拠: ExampleMod と tModLoader.wiki_en の各ガイド。

## 1) 基本方針（ファイル形式とカラーモード）

- PNG は 32bit RGBA（フルカラー+アルファ）を使用する。
  - インデックスカラーやグレースケールは避ける（Armor 変換ツールの注意事項にもある通り、黒白化・パレット不整合・ディザが起きやすい）。
  - 不要なマット（フリンジ）や半透明のハローが出ないよう、透過背景を丁寧に処理する。
- 1px=1px のピクセルアート基準。縮小・拡大リサンプリングは避ける（にじみ・ぼやけの原因）。
- アニメーションは GIF ではなく「1 枚の PNG スプライトシート」で行う（Projectile/Tile など）。

## 2) 配置と命名（Autoload の前提）

tModLoader の Autoload は「クラスの namespace と同じフォルダ」「クラス名と同じファイル名.png」を想定します。

- `.cs` と `.png` を同ディレクトリに置き、クラス名と同名の `.png` を用意する。
- フォルダ構成は namespace と一致させる（`.` は `\` に）。
- 代表的な追加テクスチャの命名規則:
  - Town NPC / Boss のミニマップ用: `ClassName_Head.png`, `ClassName_Head_Boss.png`
  - タイルのハイライト枠: `ClassName_Highlight.png`（`TileID.Sets.HasOutlines` 用）
  - Glowmask: `ClassName_Glowmask.png`（または `_Glow`）。ベースと同サイズで用意。
  - Mount 等: `_Back`, `_Front` など（必要な種別に応じて）。
- 例外系の自動ロード:
  - `Backgrounds` フォルダ配下は背景として、`Music` フォルダ配下は音楽として自動認識。

## 3) 種別別のスプライト作法（サイズ/レイアウト）

### A. Tile（ブロック/家具など）

- 1 タイル基本は 16×16px。
- スプライトシートでは各セルの「右端と下端に 2px のパディング」（合計 18×18px グリッド）を守る。
  - 便利ツール: tSpritePadder（タイル用 2px パディングを自動付与）。
- 複数タイル（MultiTile）や家具は `TileObjectData` の幅/高さ・`CoordinateWidth/Heights` で定義に合わせたシートにする。
  - 最下段だけ 18px 高さにして地面に少し沈める表現にするか、`DrawYOffset` で沈める。
- Framed（地形）タイルはオートマージ用のテンプレ配置や「3 種のランダムバリエーション」を前提にシートを作る。
- `Main.tileLargeFrames` を使うタイル（例: 模様を繋げる 2×2/3×4 パターン）は該当パターンの並びに合わせてスプライトを配置する。

### B. Projectile（弾/投擲/光弾など）

- 当たり判定（`Projectile.width/height`）はスプライトとは独立。見た目を大きくしても Hitbox は別で定義する。
- スプライトの向きは「右向き」を基準にすると実装が単純（上向き基準の場合は回転に `+Pi/2` を足す）。
- ヒットボックスとのズレは `DrawOffsetX/DrawOriginOffsetY/DrawOriginOffsetX` で補正する。
- アニメは縦にフレームを積む（`Main.projFrames[Type] = フレーム数` を設定）。

### C. Item / NPC / UI

- Item/NPC は固定サイズの制約はない（UI 上の見栄えが良いかを優先して調整）。
- 余白が大きすぎると縮小表示で見えづらくなるため、必要最小限の透明余白に留める。
- Town NPC の頭アイコンは `_Head` 命名で別 PNG を用意する。

### D. Armor/Accessory（1.4 の新様式）

- Body/Arms/FemaleBody は 1 枚に統合された新シートを使用する（最終サイズ例: 360×224）。
- HandsOn/HandsOff も新方式に揃える。変換には Sprite Transformer を使えるが、最終調整は手作業推奨。
- 変換時の注意: 出力が白黒になる・処理されない・背景が黒くなる場合、元画像を RGB/32bit にする。

## 4) Glow（Glowmask）

- ベースとは別 PNG を用意（同サイズ・同座標）。命名は `ClassName_Glowmask.png` 推奨。
- 描画は PostDraw 系のフックでベースの上に重ねる。
  - `Color.White`（不透明）で描画する（ライティングの影響を受けない発光表現）。
  - 1 フレーム毎に `Request` しない。事前に `Asset<Texture2D>` をキャッシュして使う。

## 5) アセット読み込みとパフォーマンス

- `Texture2D` を直接保持せず、`Asset<Texture2D>` を保持して `.Value` で使う。
- 事前読込は `Load`/`SetStaticDefaults` で `Mod.Assets.Request<Texture2D>(path)` を行う。
- ゲーム中にリクエストする場合は `AsyncLoad` を基本にし、毎フレーム `Request` はしない（フィールドにキャッシュ）。
- UI レイアウトでサイズが即時必要な例外のみ `ImmediateLoad`/`Asset.Wait()` を検討する。

## 6) アニメーションとフレーム設計

- Projectile など: フレームは縦積み。各フレームは同サイズで並べる。`Main.projFrames` と `frameCounter` で更新。
- Tile のアニメはシートを縦に複製（`CoordinateHeights`/`StyleLineSkip` などに合わせて配置）。
- GIF は使用しない（各クラスの描画・更新フックで制御）。

## 7) トラブルシュート

- 読み込みエラー（テクスチャが見つからない）
  - クラスと PNG のファイル名が一致しているか。
  - `.cs` と `.png` が同じフォルダ/namespace 構成か。
- タイルのズレ/食い込み
  - 2px パディング（18×18 グリッド）を守っているか。
  - 最下段 18px or `DrawYOffset`/`CoordinateHeights` で沈める調整をする。
  - Modders Toolkit でヒットボックスと見た目の位置を可視化し `DrawOffset*` を詰める。
- Glowmask が発光しない/滲む
  - ベースと同サイズ・同座標か、描画色が `Color.White` か、PostDraw で上書きしているか。
- Armor/Hands 系が崩れる
  - 新様式のシートを使用しているか（Body 統合・推奨 360×224）。
  - 画像のカラーモードが RGB/32bit か。

## 8) 便利ツール / 参考

- tSpritePadder: タイルの 2px パディング付与ツール。
- Sprite Transformer: 旧 1.3 → 1.4 の Armor/Accessory シート変換補助。
- TConvert: バニラアセットの展開・参考確認。
- Modders Toolkit: ヒットボックス/描画デバッグ。

## 9) 最終チェックリスト

- [ ] PNG は 32bit RGBA（非インデックス/非グレースケール）。
- [ ] クラス名と同名・同階層に配置（追加頭/Glow は規約サフィックス）。
- [ ] Tile は 16×16 基本 + 右下 2px パディング（18×18 グリッド）。
- [ ] Projectile のヒットボックスと見た目の整合（必要なら DrawOffset* で補正）。
- [ ] アニメは PNG シートで構成し、フレーム数をコードで設定。
- [ ] `Asset<Texture2D>` をキャッシュし、毎フレーム Request しない。

（根拠: Autoload/Assets/Basic Tile/Glowmask/Armor Texture Migration/Basic Projectile 各ガイド）
