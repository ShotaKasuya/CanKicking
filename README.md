# CanKicking

缶を飛ばして上を目指すゲーム

## 実装要素
* ゲームのメイン
  * 缶を引っ張る
  * 指を離す
  * 飛ぶ
  * 停止する
  * 妨害ブロックに当たると前回停止したポイントから`n`メートル下の場所に落ちる
* ステージセレクト
* タイトル

## コーディング規約

* Domain
    * UseCase
      * 主なゲームの動作を記述する
      * クラス名は最後にCaseをつける
* Installer
  * サービスロケータの立ち回りを行う
  * クラス名は最後にInstallerをつける
* Adapter
* 可変ならMutをインターフェースの名前につける
    * Presenter
      * UseCaseとViewをつなぐ中間層
      * Viewが持つ生のUnity要素をUseCaseが使いやすい形で公開する
      * クラス名は最後にPresenterをつける
    * Repository
      * UseCaseとDataStoreをつなぐ中間層
      * ゲーム中のデータはここで持つ
      * クラス名は最後にRepositoryをつける
* Detail
  * View
    * UnityのMonoBehaviourのコンポーネントをもつ
    * クラス名は最後にViewをつける
  * DataStore
    * データ保存用の構造をもつ
    * クラス名は最後にDataStoreをつける