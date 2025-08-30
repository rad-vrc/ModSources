Items 学習 (10-of-10) Misc/Showcase

範囲: Items 直下のショーケース/雑多カテゴリー
対象: ActiveSoundShowcase, CameraModifierShowcase, CustomItemDrawingShowcase, CustomItemSets(+System/Player), ExampleDataItem, ExampleDye, ExampleGolfBall, ExampleHairDye, ExampleInstancedItem, ExampleItem, ExampleOnBuyItem, ExamplePaperAirplane, ExampleQuestFish, ExampleResearchPresent, ExampleResourcePickup, ExampleSignItem, ExampleSoul, ExampleStackableDurabilityItem, ExampleTooltipsItem, HoldStyleShowcase, ShimmerShowcase(2種), UseStyleShowcase

要点まとめ:
- ActiveSoundShowcase: カーソル距離からスタイル(0-5)算出→Projectile ai0に付与; HoldItemでカーソルアイコンテキスト表示。テクスチャはProjectileの既存画像を流用。
- CameraModifierShowcase: UseItemでExampleCameraModifierをMain.instance.CameraModifiersに追加 (300f 持続)。
- CustomItemDrawingShowcase: Inventory/WorldそれぞれのPre/PostDraw実装見本。Front/Back2枚のAsset使用。essScaleでパルス、BackはFrame/Origin計算、BossBag風AfterImage、Rocking回転など。右クリックでモード切替+ローカライズメッセージ。
- CustomItemSets: ReinitializeDuringResizeArraysでNamed ID Set(FlamingWeapon)定義・登録。ModSystem.SetStaticDefaultsで値追加入れ替え、ModPlayer.OnHitAnythingで効果発火。MergeNamedSets例コメントあり。
- ExampleDataItem: inventory更新でtimer減少→0で+100HP回復し自己消去。AddRecipes時にcreateItem.ModItemキャストしてtimer=300設定。ツールチップに残り秒数表示。
- ExampleDye: GameShaders.Armor.BindShader(Item.type, ArmorShaderData) (dedServ避け)。CloneDefaults(GelDye)でdye保持。
- ExampleGolfBall: Item.DefaultToGolfBall(tee時Projectile)。CreativeHelper ItemGroup.Golfへ分類。
- ExampleHairDye: GameShaders.Hair.BindShader(Item.type, LegacyHairShaderData.UseLegacyMethod(...Main.DiscoColor))。飲用型消費アイテム。
- ExampleInstancedItem: インスタンスごとのColor[]を保持; Cloneでディープコピー; OnCreatedで生成; ModifyTooltipsで色付き行; UseAnimationでローテーション; Save/Load TagCompound利用。
- ExampleItem: ResearchUnlockCount=100; OnResearchedで関連4アイテムをCreativeUI.ResearchItem。基本Stack/Value設定。
- ExampleOnBuyItem: OnCreated BuyItemCreationContextを判定し50%でKillMe(LocalizedText)。PostBuyItem(ModPlayer)でも代替可能とコメント。
- ExamplePaperAirplane: DefaultToThrownWeapon+SetWeaponValues。レシピで10個作成。
- ExampleQuestFish: DefaultToQuestFish; IsQuestFish; IsAnglerQuestAvailable=hardmode; AnglerQuestChatで説明・場所をローカライズ。
- ExampleResearchPresent: CreativeUI.SacrificeItem hookでResearch既完了時でも再演出・アクセ全開放/ランダム習得。OnResearchedで全/一部アクセ研究。Item.ResearchUnlockCount=ItemLoader.ItemCountをClamp。
- ExampleResourcePickup: ItemsThatShouldNotBeInInventory/IsAPickup/IgnoresEncumberingStone/ItemSpawnDecaySpeed設定。OnPickupでModPlayer資源回復+音、false返却でインベントリ入れず。GrabRange拡張。
- ExampleSignItem: DefaultToPlaceableTile(ExampleSign)。
- ExampleSoul: RegisterItemAnimation(DrawAnimationVertical) + Sets.AnimatesAsSoul/NoGravity/IconPulse; PostUpdateで光源; GetAlpha白透過。
- ExampleStackableDurabilityItem: stack共通のdurability(0..1)。Save/Load/NetSend/Receive; OnStackで重み平均; inventory PostDrawでバー描画; 乱数生成はRecipe作成時。ツールチップ表示。
- ExampleTooltipsItem: RegisterItemAnimation + Sets.AnimatesAsSoul/NoGravity。ModifyTooltips: 一旦追加してHide/末尾":RemoveMe"をHide; ItemNameにDiscoColor。
- HoldStyleShowcase: Item.holdStyleをAltUseで循環; NetSend/Receiveで同期; ThisIs/Swichingテキスト表示。
- UseStyleShowcase: 同様にItem.useStyleを循環; NetStateChangedで同期。
- ShimmerShowcaseConditions: AddDecraftCondition(InDesert/CorruptWorld/CrimsonWorld)でデクラフト条件差し替え。優先レシピ注意。
- ShimmerShowcaseCustomShimmerResult: DisableDecraft()で1つのレシピをデクラフト対象外にし、別レシピにAddCustomShimmerResultでカスタム返却。

パターン/勘所:
- 描画: Inventory/Worldの位置・origin計算、essScale、GetItemDrawFrame、TextureAssets.Item/Asset.Frame。
- 研究/CreativeAPI: ResearchUnlockCount、OnResearched、CreativeUI.ResearchItem、ItemSacrifice hookによる特殊挙動。
- Instanced/Stackデータ: Clone/OnCreated/TagCompound/Net送受信、OnStack重み平均。
- ActiveSound/カメラ: 既存システムへの登録・更新、HoldItemのカーソルアイコンUI。
- Shimmer: Decraft条件/DisableDecraft/Custom結果、TransformToItemは別途Setsで設定。
- Showcase: useStyle/holdStyleのNet同期+AltFunctionUse。
