---

# ElementParameterExporter2026

 Revitのモデル内でユーザーが選択した要素のパラメータ情報をCSV形式で書き出す 
 これにより、パラメータ情報の一括確認や外部分析が簡単になる
- 選択した要素のすべてのパラメータ名と値を取得
- パラメータの型（数値、文字列、要素IDなど）を判別し適切に出力
- CSVファイルはRevitドキュメントと同じフォルダ、もしくはドキュメントが保存されていない場合はデスクトップに保存
- 文字列内のカンマや改行を自動でエスケープ

---

##  機能概要

| 内容       | 詳細                                   |
|------------|--------------------------------------|
| 対象カテゴリ | Revitの要素（Element）のうち、ユーザーが選択した要素       |
| 識別キー   | `ElementId` （Revit内のID番号）                     |
| 出力方法   | 選択した要素のパラメータ情報をCSVファイルとして出力          |

---

##  表示イメージ

ElementId,ParameterName,ParameterValue

123456,Length,12.34

123456,Diameter,50.00

123456,Comments,"配管メインライン"

789012,Width,100.00

789012,Height,200.00

789012,Mark,"部屋A用ダクト"



---

##  インストール方法

1. このリポジトリをクローンまたはダウンロード  
2. `ModelComparer.dll` を Revitの Addins フォルダに配置  
3. 以下のような `.addin` ファイルを作成して読み込む：

```xml
<?xml version="1.0" encoding="utf-8" standalone="no"?>
<RevitAddIns>
  <AddIn Type="Command">
    <Name>Element Parameter Exporter</Name>
    <Assembly>C:\test\ElementParameterExporter2026\ElementParameterExporter2026\bin\Debug\ElementParameterExporter2026.dll</Assembly>
    <AddInId>F9A7328C-1E89-4F1D-B0F1-2C8B91793E81</AddInId>
    <FullClassName>ElementParameterExporter2026.Command</FullClassName>
    <VendorId>IKST</VendorId>
    <VendorDescription>KengoTanaka</VendorDescription>
  </AddIn>
</RevitAddIns>
```

---

 将来の構想（TODO）

・出力対象の拡張

・多様な出力フォーマット対応

・Revitバージョン対応拡大

・パラメータ編集・一括更新機能の追加

---

 作者

田中 健悟

 BIMエンジニア。Revitアドイン開発を独学で習得。

 Qiitaにて記事公開。
 https://qiita.com/KengoTanaka-BIM/items/4678d144b4deba564bfc

---

 ライセンス & お問い合わせ

ライセンス：MIT（※自由に使ってOK）

質問・案件相談は Issues または GitHub Profile からどうぞ

---

