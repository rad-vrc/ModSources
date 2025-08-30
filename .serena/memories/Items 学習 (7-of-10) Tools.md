# Items 学習 (7-of-10) Tools

対象: ExampleMod/Content/Items/Tools/*
tModLoader: 1.4.4

共通パターン／注意点
- CloneDefaults でバニラ基準を継承し、差分だけ上書き（Hook, FishingRod, MagicMirror 等）。
- ツール固有セット
  - ItemID.Sets.CatchingTool / LavaproofCatchingTool(言及) / DuplicationMenuToolsFilter
  - ItemID.Sets.IsDrill（他Modの効果フラグにもなる）
  - ItemID.Sets.CanFishInLava
  - GamepadWholeScreenUseRange（SandRod）
- モーションと当たり
  - MeleeNoSpeed（Drill）/ attackSpeedOnlyAffectsWeaponAnimation（Pickaxe/Hamaxe）
  - channel（Drill/SandRod）/ noUseGraphic + noMelee（Held projectile系）
- ネットワーク／同期
  - EntitySource_Caught（捕獲生成 item の判定）
  - NetMessage.SendData(MessageID.TileManipulation, ...)（SandRod の KillTile 同期）

個別メモ
- ExampleBugNet
  - Sets: CatchingTool, DuplicationMenuToolsFilter
  - CanCatchNPC で溶岩クリッター捕獲（20%/Warmth中50%）
  - GlobalItem.OnSpawn(EntitySource_Caught) で 5% 重複（HeldItem=ExampleBugNet のとき）

- ExampleDrill
  - Sets.IsDrill=true
  - DamageType=MeleeNoSpeed, channel, noUseGraphic/noMelee, shoot=ExampleDrillProjectile, shootSpeed=32
  - Item.tileBoost=10（到達範囲拡張）/ pick=190
  - Drill/Chainsawはバニラ軽減ロジックがあるため CloneDefaults 非使用時は数値を0.6掛け相当で調整

- ExampleFishingRod
  - CanFishInLava=true; CloneDefaults(WoodFishingPole)
  - fishingPole=30, shoot=ExampleBobber（アクセのBobberで上書きされ得る）
  - HoldItem: accFishingLine 付与
  - Shoot: 3〜5本の複数ボバー射出（Truffle Worm 複数でDuke複数召喚の注意）
  - ModifyFishingLine: lineOriginOffset(43,-30), lineColor=ExampleBobber.FishingLineColor or DiscoColor

- ExampleHamaxe
  - 斧＋ハンマー複合（axe=30→表示150, hammer=100）
  - attackSpeedOnlyAffectsWeaponAnimation=true
  - MeleeEffects で Dust

- ExampleHook（grappling hook 一式）
  - Item: CloneDefaults(AmethystHook), shootSpeed=18, shoot=ExampleHookProjectile
  - Projectile: CloneDefaults(GemHookAmethyst)
    - CanUseGrapple: 同型の発射中数を集計して2本まで
    - GrappleRange=500f, NumGrappleHooks=2
    - GrappleRetreatSpeed=18, GrapplePullSpeed=10
    - GrappleTargetPoint: ハング距離 50px
    - GrappleCanLatchOnTo: 木の幹にラッチ可
    - PreDrawExtras: 独自チェーン描画（Asset<Texture2D> を Load 時キャッシュ）

- ExampleInteractableProjectileItem
  - useStyle=Swing, shootSpeed=4, shoot=ExampleInteractableProjectile
  - シンプルな『インタラクト可能な投射体』スポーン例

- ExampleMagicMirror
  - CloneDefaults(IceMirror), Texture をバニラ流用, Item.color=Violet
  - UseStyle 内で動作（itemTime 制御, 中間フレームで RemoveAllGrapplingHooks→Spawn(RecallFromItem)→演出Dust）
  - ModifyTooltips: ItemName の色を4色で循環（Color.Lerp + Main.GameUpdateCount）

- ExamplePickaxe
  - pick=220, attackSpeedOnlyAffectsWeaponAnimation=true
  - MeleeEffects: Dust
  - UseAnimation: ランダムで EmoteBubble(ExamplePickaxeEmote)

- ExampleSandRod（Dirt Rod の砂版）
  - Sets: DuplicationMenuToolsFilter, GamepadWholeScreenUseRange
  - CanUseItem:
    - Mouse座標からタイル取得→砂以外は不可
    - TileID.Sets.FallingBlockProjectile から FallingProjectileType を得る
    - WorldGen.KillTile(noItem:true) 成功時のみ Item.shoot=data.FallingProjectileType 設定
    - MPクライアント時は TileManipulation を SendData で同期
  - ModifyShootStats: position=MouseWorld & LimitPointToPlayerReachableArea

API/呼び出し要点
- ItemID.Sets.*（CatchingTool/IsDrill/CanFishInLava/DuplicationMenuToolsFilter/GamepadWholeScreenUseRange）
- Player: ApplyItemTime/RemoveAllGrapplingHooks/Spawn, accFishingLine, LimitPointToPlayerReachableArea
- Projectile hooks: GrappleRange/NumGrappleHooks/GrappleRetreatSpeed/GrapplePullSpeed/GrappleTargetPoint/GrappleCanLatchOnTo/PreDrawExtras
- Net: NetMessage.SendData(MessageID.TileManipulation,...)
- Tile: TileID.Sets.FallingBlockProjectile

落とし穴/Tips
- Drill系は melee speed を無視（MeleeNoSpeed）し、channel/noUseGraphic/noMelee で held projectile に委譲
- MagicMirror系は UseStyle 中盤で実処理、UseItem ではない点に注意
- Fishing 複数ボバーはボス召喚に影響し得るためテスト必須
- Grapple 複数本制御は ActiveProjectiles 列挙で whoAmI/type を確認
- SandRod は KillTile 成否・MP同期を考慮（戻し不能な破壊を避ける）
