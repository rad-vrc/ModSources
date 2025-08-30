# tModLoader ボス実装ベストプラクティス（初参加者向け）

この文書は、tModLoader で「ボス（Boss）」を実装する際の実践的な指針をまとめたものです。初めてプロジェクトに参加する開発者でも迷わないよう、設計の考え方から、AI・同期・ドロップ・演出まで、必要最小限＋つまずきやすいポイントを中心に整理しています。

参考資料:

- ExampleMod（公式サンプル）
- tModLoader wiki（本リポジトリの `References/tModLoader.wiki_en/` 配下）

## 最低限の構成（骨子）

ボス実装に必要な主要コンポーネント:

- ModNPC（ボス本体）
  - `SetStaticDefaults`, `SetDefaults`, `AI`, `FindFrame`, `HitEffect`, `OnKill` など
  - `ModifyNPCLoot`（1.4 の新しい戦利品API。1.3の `NPCLoot` ではなくこちらを使用）
- ModItem（召喚アイテムなど）
- ModProjectile（ボスが放つ弾・設置物・分裂体など）
- BossBag（Expert/上位難易度用のボスバッグ）

最小骨子（抜粋）:

```cs
public class MyBoss : ModNPC
{
    public override void SetStaticDefaults() {
        Main.npcFrameCount[Type] = 1; // アニメがあれば枚数を設定
        NPCID.Sets.BossBestiaryPriority.Add(Type); // Bestiary 優先度（必要に応じて）
    }

    public override void SetDefaults() {
        NPC.width = 120;
        NPC.height = 120;
        NPC.damage = 50;
        NPC.defense = 10;
        NPC.lifeMax = 12000;
        NPC.knockBackResist = 0f;
        NPC.value = Item.buyPrice(0, 10, 0, 0);
        NPC.aiStyle = -1; // 独自AIを使う
        NPC.noGravity = true;
        NPC.noTileCollide = true;
        NPC.boss = true;
        Music = MusicID.Boss2; // 独自BGMを使うなら MusicLoader を利用
    }

    public override void AI() {
        // 状態によるAI（下の「AI 設計」参照）
    }

    public override void ModifyNPCLoot(NPCLoot npcLoot) {
        // 下の「ドロップ/報酬」参照
    }
}
```

## 設計原則（まずここを守る）

- テレグラフ（予備動作）を必ず用意
  - 攻撃直前の色変化・移動停止・エフェクト・サウンドなどでプレイヤーに「来る」ことを伝える
  - 高速連続弾は「間隔」「速度」「ばらつき」「見た目」で理不尽感を抑える
- フェーズ（体力割合）を明確化
  - HP 75/50/25% などで行動を切り替え、難易度カーブを調整
  - 切替時は硬直・無敵・広域ノックバック等で演出＋安全確保を行う
- 攻撃→小休止→移動→小休止のリズム
  - 連続攻撃は 1〜2 サイクルに留め、間に体勢立て直し時間を確保
- 当たり判定は見た目と一致
  - 大型ボスほど「中央コアだけ当たり判定」「周囲装飾は無効」など理不尽を回避
  - 反撃可能な「安全地帯」「対処法」を用意
- 弾幕・設置の「掃除タイミング」を用意
  - 長時間残留物が蓄積し続けると詰みやすい。フェーズ遷移・咆哮で一掃など

## AI 設計（状態遷移とタイマー）

基本は「状態マシン＋タイマー」:

- `NPC.ai[]`（4本まで）やフィールド変数で
  - 現在状態（int/enum）
  - 経過フレーム（float カウンタ）
  - ターゲット情報 / 次の目的地
を保持

典型パターン:

```cs
enum BossState { Idle, Telegraph, Dash, Shoot, PhaseChange }

BossState state;
int stateTimer;

void DoAI()
{
    Player target = Main.player[NPC.target];
    NPC.TargetClosest();

    switch (state) {
        case BossState.Idle:
            // プレイヤーへホバー移動（ゆっくり）
            if (++stateTimer > 60) { ChangeState(BossState.Telegraph); }
            break;
        case BossState.Telegraph:
            // 光る/停止/音を鳴らすなど
            if (++stateTimer > 30) { ChangeState(BossState.Dash); }
            break;
        case BossState.Dash:
            // プレイヤー方向にダッシュ（慣性/速度上限）
            if (++stateTimer > 20) { ChangeState(BossState.Shoot); }
            break;
        case BossState.Shoot:
            // 弾を数発→小休止
            if (++stateTimer % 12 == 1) Fire();
            if (stateTimer > 60) { ChangeState(BossState.Idle); }
            break;
    }

    // HP 閾値でフェーズ遷移
    if (NeedsPhaseChange()) { ChangeState(BossState.PhaseChange); }
}

void ChangeState(BossState next) {
    state = next;
    stateTimer = 0;
    NPC.netUpdate = true; // マルチ同期（下節）
}
```

注意点:

- `AI` の分岐や乱数に依存する決定は「サーバー側で行い」「結果を同期」する
- タイマーのオーバーフロー防止（一定以上でリセット）
- ダッシュ等は「最高速度」「加速/減速」「旋回性」を分けて調整

## マルチプレイ/ネットコードの注意

基本原則（wiki: Basic-Netcode）:

- サーバーが NPC の正解状態を持つ
- 非決定（ランダム・分岐）はサーバーで確定→`NPC.netUpdate = true` で同期
- 追加の同期データが必要なら `SendExtraAI` / `ReceiveExtraAI` を活用
- 弾生成などは「クライアントで重複生成しない」こと

NPC が弾を撃つ典型:

```cs
public override void AI() {
    if (Main.netMode != NetmodeID.MultiplayerClient) {
        if (ShouldShoot()) {
            Vector2 pos = NPC.Center;
            Vector2 vel = (TargetPos - pos).SafeNormalize(Vector2.UnitY) * 10f;
            int type = ProjectileID.PinkLaser; // 独自ProjでもOK
            int dmg  = NPC.GetProjectileDamage(type); // スケールに合わせる
            Projectile.NewProjectile(NPC.GetSource_FromAI(), pos, vel, type, dmg, 0f, Main.myPlayer);
        }
    }
}
```

ポイント:

- `Main.netMode != MultiplayerClient` でサーバー（またはSP）だけ生成
- `GetSource_FromAI()` を使い発生源を明示
- `Main.myPlayer` を owner に（wiki: Basic-NPC の注意事項）

## 生成/召喚/デスポーン

- 召喚アイテム: `CanUseItem` で使用条件（夜/バイオーム/既存ボス不在）
- スポーン位置: プレイヤーから少し離れた安全座標、足場上に湧かせない/壁抜け前提なら `noTileCollide`
- デスポーン条件:
  - 全プレイヤー死亡/離脱→ゆっくり上昇し消える（即時消滅は避ける）
  - 日中/夜間制約を満たさなくなった場合の処理
- Bestiary / スポーン説明は `SetBestiary` で設定可能

## ドロップ/報酬（1.4 ルールベース）

1. ボス本体（`ModifyNPCLoot`）でバッグ登録＋非 Expert の直ドロップを定義

```cs
public override void ModifyNPCLoot(NPCLoot npcLoot) {
    // Boss Bag（Expert/上位難易度）
    npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<MyBossBag>()));

    // 非 Expert 時（LeadingConditionRule を使う）
    var notExpert = new LeadingConditionRule(new Conditions.NotExpert());
    notExpert.OnSuccess(ItemDropRule.Common(ModContent.ItemType<MyBossMask>(), 7));
    notExpert.OnSuccess(ItemDropRule.Common(ModContent.ItemType<MyBossRelic>(), 10));
    notExpert.OnSuccess(ItemDropRule.Common(ModContent.ItemType<MyBossWeapon>(), 1));
    npcLoot.Add(notExpert);

    // コインは NPC.value ベース
    npcLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(Type));
}
```

1. ボスバッグ（`ModItem`）側で中身を定義

```cs
public class MyBossBag : ModItem
{
    public override void SetStaticDefaults() {
        ItemID.Sets.BossBag[Type] = true;
    }
    public override bool CanRightClick() => true;
    public override void ModifyItemLoot(ItemLoot itemLoot) {
        itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<MyBossMask>(), 7));
        itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<MyBossPetItem>(), 10));
        itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<MyBossWeapon>()));
        itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(ModContent.NPCType<MyBoss>()));
    }
}
```

ベストプラクティス:

- Expert 以上は「バッグから出す」に統一（直ドロップと二重定義しない）
- 戦利品の希少度・入手時期・DPS を進行バランスに合わせて調整
- トロフィー・レリック・仮面など演出用アイテムも追加

## 視覚・演出（BGM/SE/ダスト/ライト）

- BGM: 可能なら専用曲（`MusicLoader.AddMusic`）を用意。ボス開始/終了時の切替が唐突にならないようレイヤの挿入順（UI 層）やフェードを確認
- 効果音: テレグラフ用SE、フェーズ移行・咆哮・撃破時ジングル
- ダスト/ライト: `Dust.NewDust`, `Lighting.AddLight` を多用しすぎない（描画負荷・視認性）
- 画面揺れ・ポストエフェクトは控えめに（酔い対策）

## パフォーマンス・最適化

- `AI` 内での都度割当を避ける（Vector2 の一時変数は OK、重い LINQ/配列確保は避ける）
- 大量弾幕は発生上限・寿命・間引きを設計
- `Texture2D` などアセットは読み込み時に `Assets.Request`（wiki: Assets）で確保、`AI` 中に `Request` しない
- `netUpdate` は「必要時のみ」。毎フレーム連打しない

## アセット/ローカライズ

- テクスチャ命名・配置は Autoload 規約に従う（`Basic-Autoload`）
- DisplayName, Bestiary, 戦利品説明、召喚アイテムの Tooltip は `Localization` で多言語化
- スプライトサイズとヒットボックスは一致させる（必要なら `DrawOffsetX` などで補正。`Basic-Projectile` の「Drawing and Collision」を参照）

## 実装チェックリスト

- 【AI/ゲーム性】
  - 予備動作（テレグラフ）があり、見て避けられる
  - フェーズごとに役割の違う行動があり、詰み解消の掃除タイミングがある
  - 当たり判定が見た目と一致し、理不尽な接触ダメがない
- 【マルチ/同期】
  - 非決定処理はサーバーで実行し同期できている
  - 弾の生成は重複しない
- 【生成/撤退】
  - 召喚条件/時間帯/既存ボス不在チェックがある
  - 全滅・遠距離での穏当な撤退ロジック
- 【報酬】
  - BossBag と非 Expert ドロップが適切に分離
  - コイン・トロフィー・レリック・仮面等が設定
- 【演出/最適化】
  - BGM/SE/エフェクトが過剰でない
  - AI で余計な割当・過剰同期をしていない
  - アセットはロード時に Request 済み

## 参考（読み物）

- 戦利品: `Basic-NPC-Drops-and-Loot-1.4.txt`
- ネットコード: `Basic-Netcode.txt`
- プロジェクタイル基礎/描画と当たり判定: `Basic-Projectile.txt`
- 自作UI・トグル等（ボス専用UIを作る場合）: `Advanced-guide-to-custom-UI.txt`
- Detour/Hook（高度な振る舞い変更が必要な場合）: `Advanced-Detouring-Guide.txt`
- ExampleMod（実コード雛形）: `ExampleMod/` 配下の NPC/Items/Projectiles サンプル

---
本ドキュメントは実装時の「落とし穴を避けるチェックリスト」として使えます。困ったときは ExampleMod と wiki の該当ページを必ず合わせて確認してください。
