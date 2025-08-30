# ExampleMod – NPCs Summary (Part 2–5)

対象: ExampleMod/Content/NPCs 下の未読ファイルを全読了。構成別に要点を整理。

— Part 2: 単体NPC（AI/描画/Global） —
- ExampleCustomAISlimeNPC
  - 完全カスタムAI（aiStyle=-1）でステート駆動: Asleep → Notice → Jump → Hover → Fall。
  - 同期: HoverでFlutter時間をサーバ決定→NPC.netUpdate。Debuff免疫（Poisoned, ExampleGravityDebuff）。
  - ModifyCollisionDataで落下中に底部狭域ヒット時ダメージ2倍＋ローカライズメッセージ。
  - CanFallThroughPlatforms: プレイヤー真下にいるときFall状態で足場すり抜け。
- ExampleDrawBehindNPC
  - DrawBehindの全レイヤ周回（6段階）。indexを各DrawCacheリストに振り分け。
  - PreHoverInteractで右クリック専用挙動（所持アイテム消費→爆発音→自傷）とホバー時アイコン表示。CanChat上書きでスマートインタラクト対象に。
- ExampleGlobalNPC
  - InstancePerEntity=true（townNPCのみに適用）。HasBeenHitByPlayerをOnHitByProjectile/OnHitByItemで設定。
  - ModifyActiveShop: 殴られた店主は販売価格2倍。Save/Loadでフラグ永続化。

— Part 3: Worm（多パート敵） —
- Worm.cs（基底）
  - Worm/WormHead/WormBody/WormTailの抽象実装。SegmentType毎にAI分岐。realLife共有HP。
  - HeadAI: セグメント生成（Min/MaxSegmentLength）、衝突/距離判定、掘削移動、回転とnetUpdate制御、ForcedTargetPosition対応、CanFly/タイル衝突距離閾値。
  - Body/Tail: 親セグメントの位置追従（速度0で位置補正）。
- ExampleWorm.cs
  - ExampleWormHead/Body/Tail。Digger系CloneDefaults、aiStyle=-1、カスタムBestiary。
  - InitでCommonWormInit: MoveSpeed=5.5, Accel=0.045。Kill数バナー25。
  - Headは近距離/視線ありでShadowBeamHostileをランダム散布射出（ExtraAIでattackCounter同期）。

— Part 4: MinionBoss（本体＋随伴） —
- MinionBossBody
  - 二段階フェーズ管理（SecondStage=NPC.ai[0]）。第一段階はMinionの総HPに応じて透明化・無敵、目的地テレグラフ線描画。
  - 召喚時に随伴Minionを多数生成（難度シードで増加）, 親インデックス/オフセット割当て、合計随伴HPをBossBar表示用に保持。
  - Minion全滅でSecondStage遷移（サーバ側でnetUpdate）。第二段階は頭上左右に往復移動＋MinionBossEye弾を周期生成（体力に応じ短周期化）。
  - 50%以下でOnFire系耐性をBecomeImmuneTo→ClearImmuneToBuffs（サーバのみ）＋演出Dust。
  - 戦闘中BGM/ボスバー/Bestiary/ドロップ（Trophy/Mask/Bag/Relic/Pet）設定、撃破時にExampleOre生成・撃破フラグ。
- MinionBossMinion
  - 親（本体）依存スポーン検証（ParentIndexで不正湧き自壊）。
  - 透過フェードイン→本体中心を囲む輪状編隊（PositionOffset→2π化、連続回転で接触機会）。
  - Bossesクールダウン採用で多段無効化緩和。死亡時に近傍プレイヤーのHPを見て心臓追加ドロップ抽選。

— Part 5: Town Pet —
- TownPets/ExampleTownPet
  - IsTownPet, housingCategory=1（TownNPCと同居可）, AnimationType=TownBunny。
  - 追加頭部テクスチャ6種をLoadで登録。ITownNPCProfileを自前実装（ModContent.Request使用）でバリアント別テクスチャ/頭部切替・名前リロール。
  - 名前リストは一部ローカライズ連動（Language.FindAll）。
  - 椅子着座時の描画補正（PreAIでDrawOffsetY、ChatBubblePosition、PartyHatPositionフレーム別XY微調整）。
  - CanTownNPCSpawnは購入フラグ（ExampleTownPetSystem）で制御。ペットボタン表示などUI連携。

カバレッジ: NPCsフォルダの残存全8ファイル（CustomAISlime/DrawBehind/GlobalNPC/Worm系3/TownPet/MinionBoss系2）を読了・要約。
