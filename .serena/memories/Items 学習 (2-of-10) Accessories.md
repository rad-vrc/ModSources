# Items 学習 (2-of-10) Accessories

目的: アクセサリー実装の代表パターンと主要API/フックの把握（移動系/耐性/情報/追加ジャンプ/ダッシュ/ウィング/IL応用 等）

全体パターン
- 基本フック: ModItem.UpdateAccessory(Player,bool)、UpdateVanity、UpdateInfoAccessory、VerticalWingSpeeds、AddRecipes
- ModPlayer連携: ResetEffects でフラグ初期化 → UpdateAccessory 等でフラグON → 適切なタイミング（PreUpdateMovement/PostUpdateRunSpeeds/OnHurt など）で効果適用
- 移動速度系注意: runAcceleration/maxRunSpeed は UpdateAccessory ではなく ModPlayer.PostUpdateRunSpeeds で調整（ロジック順序の都合）
- 可視効果: hideVisual チェック、CancelAllBootRunVisualEffects、DoBootsEffect(...)、UpdateVanity でのビジュアルのみ適用
- チーム/ネットワーク: UpdateAccessory は全プレイヤーで呼ばれることを意識。ローカル/他人判定、team/range 条件でのバフ配布
- ステータス操作: StatModifier（Additive/Multiplicative/Base/Flat）、GetCritChance、GetAttackSpeed、GetArmorPenetration、GetKnockback
- 装備スロット: [AutoloadEquip(EquipType.X)]（Shield/Beard/Wings/Back/Shoes）

各ファイル要点
1) AbsorbTeamDamageAccessory.cs
- パラディンの盾類似: ライフ閾値50%、範囲800内の味方を防御。ローカルプレイヤーに AbsorbTeamDamageBuff を一定間隔で付与
- ExampleDamageModificationPlayer.hasAbsorbTeamDamageEffect フラグON
- UpdateAccessoryで player!=Main.myPlayer 時にteam/距離/HP条件を満たせば Main.LocalPlayer にバフ

2) ExampleBeard.cs
- [AutoloadEquip(EquipType.Beard)]。ArmorIDs.Beard.Sets.UseHairColor[Item.beardSlot] = true で髪色反映
- Item.color = Main.LocalPlayer.hairColor、vanity アクセサリー

3) ExampleBoots.cs
- 移動系の集大成: moveSpeed += 0.08、accRunSpeed=6.75、rocketBoots=2、vanityRocketBoots=2
- waterWalk/waterWalk2、iceSkate、desertBoots、fireWalk、noFallDmg、lavaRose、lavaMax += 2秒
- hideVisual=false なら CancelAllBootRunVisualEffects + hellfireTreads + DoBootsEffect_PlaceFlamesOnTile
- UpdateVanity でもビジュアル再現。runAcceleration/maxRunSpeed はModPlayer側で扱うのが推奨

4) ExampleExtraJumpAccessory.cs + SimpleExtraJump
- ExtraJump 実装: GetDefaultPosition() => After(BlizzardInABottle)
- GetModdedConstraints: Before(MultipleUseExtraJump) でモッド間順序制御
- GetDurationMultiplier/UpdateHorizontalSpeeds/OnStarted/ShowVisuals で挙動・演出（Cloud/Blizzard風ダスト・Gore）
- UpdateAccessory: player.GetJumpState<SimpleExtraJump>().Enable()

5) ExampleImmunityAccessory.cs
- ExampleImmunityPlayer.HasExampleImmunityAcc = true を立てる（PostHurt 側で実際の無敵処理等を行う想定）

6) ExampleInfoAccessory.cs
- 情報アクセ: Radar クローン、UpdateInfoAccessory で ExampleInfoDisplayPlayer.showMinionCount = true
- Void Bagでも動作する既定の挙動説明（必要時に無効化も可能）

7) ExampleMultiExtraJumpAccessory.cs + MultipleUseExtraJump + MultipleUseExtraJumpPlayer
- 空中3回ジャンプ。jump残数に応じて Duration/演出/SE(Pitch) を変化
- OnRefreshed で 3 にリセット。OnStarted でリング状ダスト、SoundID.Item8 をPitch可変で再生
- 使用後に jumps--、まだ残っていれば Available=true で続けて発動可能
- ModifyTooltips で <JUMPS> プレースホルダを現在値で置換

8) ExampleResourceAccessory.cs
- ExampleResourcePlayer へ: 最大値 +100、再生速度 x6、マグネットON（ExampleResourcePickupの吸引範囲強化）

9) ExampleShield.cs + ExampleDashPlayer
- [EquipType.Shield]。防御/再生、汎用ダメージ+100%、endurance 合成、ダッシュフラグON
- Double-tap 検出は ResetEffects 内（Player.doubleTapCardinalTimer）。PreUpdateMovement で速度付与
- DashCooldown=50、DashDuration=35、DashVelocity=10。EoC風残像: Player.eocDash/armorEffectDrawShadowEOCShield
- CanUseDash: 他ダッシュ優先/ソーラー/マウント中は不可

10) ExampleStatBonusAccessory.cs + ExampleStatBonusAccessoryPlayer
- StatModifier活用: Genericに +25% と x1.12、Base+4、Flat+5。MeleeCrit+10%、Ranged攻速+15%、Magic貫通+5
- 例ダメージクラスのノックバック+100%。AdditiveCritDamageBonus を ExampleDamageModificationPlayer へ
- PostUpdateRunSpeeds: マウント非搭乗かつフラグON時に加速/最高速/減速係数を強化

11) ExampleWings.cs
- [EquipType.Wings]。Configで読み込み可否（IsLoadingEnabled）。WingStats: 180/9/2.5
- VerticalWingSpeeds で上昇/滑空/定数上昇などの細調整
- レシピ SortBefore で全ウィングの直前に並べ替え

12) WaspNest.cs + WaspNestPlayer
- [AutoloadEquip(EquipType.Back)]。HivePack クローン + 追加効果
- IL編集: IL_Player.beeType にフック。strongBeesUpgrade かつ確率で Beenade に昇格
- UpdateAccessory: player.strongBees=true、ModPlayerに strongBeesUpgrade/strongBeesItem
- OnHurt: GetSource_Accessory_OnHurt から蜂弾を発射（Item段階でダメージ差別化可）
- 競合回避: CanAccessoryBeEquippedWith で HiveBackpack と同時装備不可

実装チートシート（よく使うAPI）
- 移動/ビジュアル: player.moveSpeed/accRunSpeed/rocketBoots/vanityRocketBoots/CancelAllBootRunVisualEffects/DoBootsEffect
- 耐性/歩行: waterWalk/waterWalk2/iceSkate/desertBoots/fireWalk/noFallDmg/lavaRose/lavaMax
- 戦闘: player.GetDamage/GetCritChance/GetAttackSpeed/GetArmorPenetration/GetKnockback、player.endurance
- 追加ジャンプ: ExtraJump.GetDefaultPosition/GetModdedConstraints/GetDurationMultiplier/UpdateHorizontalSpeeds/OnRefreshed/OnStarted/ShowVisuals
- ダッシュ: ModPlayer.ResetEffects/PreUpdateMovement、Player.doubleTapCardinalTimer、eocDash、armorEffectDrawShadowEOCShield
- 情報アクセ: ModItem.UpdateInfoAccessory、InfoDisplay/ModPlayer 連携
- 翼: [AutoloadEquip(EquipType.Wings)]、ArmorIDs.Wing.Sets.Stats[...]、VerticalWingSpeeds
- IL: IL_Player.beeType フック、MonoMod.ILCursor、EmitDelegate で拡張

想定エッジ/注意
- UpdateAccessory は全員分呼ばれる → team/距離/ローカル判定で副作用を最小化
- runAcceleration/maxRunSpeed は PostUpdateRunSpeeds で。UpdateAccessory での直接変更は非推奨
- ダッシュは他ダッシュ/装備と優先度競合あり → CanUseDashでバニラとの整合
- IL 失敗時のフォールバック: DumpIL で診断、致命的なら例外に切替可能