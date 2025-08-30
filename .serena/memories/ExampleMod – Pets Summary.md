# ExampleMod – Pets Summary

Covers: Content/Pets/ExamplePet, ExampleLightPet, MinionBossPet (Buff/Item/Projectile)

## ExamplePet (vanity pet)
- Buff: `Main.buffNoTimeDisplay=true`, `Main.vanityPet=true`; `Player.BuffHandle_SpawnPetIfNeededAndSetTime(buffIndex, ref unused, ProjType)` でスポーン維持。
- Item: `CloneDefaults(ItemID.ZephyrFish)`; `Item.shoot=ExamplePetProjectile`; `Item.buffType=ExamplePetBuff`; `UseItem` で自身に 3600tick バフ付与。
- Projectile: `CloneDefaults(ProjectileID.ZephyrFish)` + `AIType=ZephyrFish`; `Main.projFrames=4`, `Main.projPet=true`;
  - CharacterPreview: `ProjectileID.Sets.SimpleLoop(...).WithOffset(...).WithSpriteDirection(-1).WithCode(DelegateMethods.CharacterPreview.Float)`。
  - PreAI: `player.zephyrfish=false`（AIType遺物の影響抑制）。
  - AI: バフがある間 `timeLeft=2` で維持。

## ExampleLightPet (light pet, custom behaviors)
- Buff: `Main.lightPet=true`; スポーン維持は vanity と同様メソッド。
- Item: 明示的に各種 `Item` 設定、`buffType=ExampleLightPetBuff`、`UseStyle` 内で buff 3600tick 付与。
- Projectile:
  - Static: `Main.projPet=true`, `ProjectileID.Sets.TrailingMode=2`, `ProjectileID.Sets.LightPet=true`。
  - Defaults: 30x30, `penetrate=-1`, `netImportant=true`, `ignoreWater`, `scale=0.8`, `tileCollide=false`。
  - AI: 3分割ロジック
    - Dash: `ai[1]` をチャージ(AIDashCharge)、クールダウンとフェード進行に応じて範囲内のNPCへ突進(`DashSpeed` 加算)。
    - Fading: `ai[0]` をフェード進行(AIFadeProgress)に使い、フェードイン→一定明度→フェードアウト→ランダム再配置→再加速。距離外補正、100tick毎のランダム回頭。
    - ExtraMovement: 摩擦(>1の時0.98倍)、完全停止ならランダム方向へ小加速。
  - 視覚: 横移動でわずかに回転、`Lighting.AddLight` で明かり付与。

## MinionBossPet (masterペット、カスタムAI+描画)
- Buff: vanity フラグ＋同様のスポーン維持。
- Item: `Item.DefaultToVanitypet(ProjType, BuffType)` で標準設定を一括適用→Master用の `rare=Master`, `Item.master=true`, `value` など上書き。`Shoot` でバフ2tick付与。
- Projectile:
  - Load: 追加テクスチャ `Texture+"_Eye"` を `Asset<Texture2D>` でロード・キャッシュ。
  - Static: `Main.projFrames=6`, `Main.projPet=true`; CharacterPreview は `SimpleLoop(...).WithOffset(...).WithCode(CharacterPreviewCustomization)` で独自プレビュー処理。
  - Defaults: `CloneDefaults(ProjectileID.EyeOfCthulhuPet)` + `aiStyle=-1`（完全カスタムAI）。
  - 描画: `AlphaForVisuals => ai[0]`、`GetAlpha` で白×`AlphaForVisuals`×`Opacity`; `PostDraw` で周囲に10個の目を円軌道に `Main.EntitySpriteDraw`（染色対応）。
  - AI: `CheckActive`（バフ中 `timeLeft=2`）、`Movement`（プレイヤー相対位置+サイン波、速度に応じた回転慣性・復元）、`Animate`（速さでframe速度変更）、`GetAlphaForVisuals`（プレイヤー体力比から可視度）。