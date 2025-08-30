# Items 学習 (5-of-10) Consumables

目的: 消費アイテム（ポーション/食料/スタック管理/成長系/多回使用/釣りクレート/ボス召喚/町ペットライセンス）の実装パターン整理

共通
- 基本: useStyle/useTime/useAnimation/UseSound/consumable/maxStack/rare/value
- 研究: ResearchUnlockCount（Food=5, Potion=20, Bag=3 等）
- Buff: Item.buffType/buffTime（QuickBuff 対応のため主要Buffは SetDefaults で指定）
- 表示: 飲食パーティクル色（ItemID.Sets.DrinkParticleColors / FoodParticleColors）、Food は IsFood=true と 3フレーム縦アニメ

ポーション類
- ExampleBuffPotion/ExampleCratePotion: DrinkLiquid、DrinkParticleColors 指定、buffType/buffTime 付与
- ExampleFlask: 武器付与系（ExampleWeaponImbue）、Item.flaskTime、ImbuingStation レシピ
- ExampleHealingPotion: 動的回復
  - Item.healLife>0 + Item.potion=true（クイックヒール対象）
  - ModifyTooltips で表示をローカライズ値に置換、GetHealLife(player, quickHeal, ref healValue) で実値(最大HPの1/2 or 1/4)

食料
- ExampleFoodItem: Item.DefaultToFood(width,height, buffId, buffTime)、OnConsumeItemで追加Buff
  - Main.RegisterItemAnimation(Type, new DrawAnimationVertical(int.MaxValue, 3)) で縦3フレーム（インベントリ/持ち/置き）
  - FoodParticleColors でパン粉色、ItemID.Sets.IsFood[Type]=true

スタック/カスタムデータ
- ExampleCanStackItem: craftedPlayerName を保持
  - CanRightClick: 未クラフトの袋は不可、クラフト主は開けられない
  - CanStack: craftedPlayerName が一致する場合のみ（空は現在プレイヤー名扱い）
  - OnStack: 空側に名前を引き継ぎ
  - SaveData/LoadData/NetSend/NetReceive で永続化/同期
  - ModifyItemLoot: 難易度条件で中身分岐

成長系（恒久上限増加）
- ExampleLifeFruit: 追加10回まで、1回10HP。CanUseItem でバニラ上限達成後のみ可。UseItem で UseHealthMaxIncreasingItem とカウント増
- ExampleManaCrystal: 同様にマナ版（UseManaMaxIncreasingItem）。各カウントは ModPlayer 側で保存/同期が必要

多回使用
- ExampleMultiUseItem: MaxUses=4。ConsumeItem をオーバーライドし useCount を増やし、Max に達したら消費
  - Save/Load/NetSend/NetReceive で同期、PostDrawInInventory で残回数バーを描画
  - OnStack/SplitStack で useCount の合算/分配調整（Max超過で1個減らし残余を保持）

釣りクレート
- ExampleFishingCrate: IsFishingCrate=true、DefaultToPlaceableTile、Crates グループ分類
  - ModifyItemLoot: テーマ品/コイン/鉱石や延べ棒/探索用ポーション/高級餌 から抽選

ボス召喚
- MinionBossSummonItem（モッドボス）: SortingPriorityBossSpawns、CanUseItem は !NPC.AnyNPCs(Boss)
  - UseItem: ソロは SpawnOnPlayer、マルチは NetMessage.SendData(SpawnBossUseLicenseStartEvent)（MPAllowedEnemies をボス側で true）
- PlanteraItem（バニラ）: NPCID.Sets.MPAllowedEnemies[Plantera]=true をここで設定
  - CanUseItem: ハード/三機械撃破済/未出現

町ペットライセンス
- ExampleTownPetLicense: UseItem で未購入 or 既に存在チェック。マルチは ModPacket で同期、SPはフラグON+チャット+WorldData送信
  - Broadcast メッセージや色、RerollVariationForNPCType で外見抽選

注意/落とし穴
- 動的回復: Item.healLife は0超を維持（ポーション扱いのため）。表示は ModifyTooltips、実値は GetHealLife で
- CanStack/OnStack と Save/Load/NetSync を組み合わせ、MPやクラフト連打で崩れないように
- 召喚: マルチは MPAllowedEnemies を必ず有効化、UseItem の分岐に注意
- 成長系: バニラ上限達成チェックと ModPlayer カウント保存/同期を忘れない