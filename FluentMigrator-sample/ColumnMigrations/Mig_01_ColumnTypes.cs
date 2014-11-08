using FluentMigrator;

namespace FluentMigrator_sample.ColumnMigrations
{
    [Migration(2014110901)]
    public class Mig_01_ColumnTypes : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("ColumnTypes")
                // テキスト型
                // いずれの方法も同じ。引数が256以上になると、メモ型になる
                .WithColumn("StringCol").AsString(100)
                .WithColumn("FixedTextCol").AsFixedLengthAnsiString(30)
                .WithColumn("AsFixedTextCol").AsFixedLengthString(40)
                .WithColumn("AnsiStringCol").AsAnsiString()

                // メモ型(引数で256以上を指定)
                .WithColumn("MemoCol").AsString(256)


                // 数値型、フィールドサイズが異なる
                .WithColumn("ByteCol").AsByte()        // バイト型
                .WithColumn("Int16Col").AsInt16()      // 整数型
                .WithColumn("Int32Col").AsInt32()      // 長整数型

                .WithColumn("Int64Col").AsInt64()              // 十進型:精度20, 小数点以下0
                .WithColumn("DecimalCol").AsDecimal()          // 十進型:精度19, 小数点以下5
                .WithColumn("DecimalSizeCol").AsDecimal(10, 2) // 十進型:精度10, 小数点以下2

                .WithColumn("FloatCol").AsFloat()      // 単精度浮動小数点型
                .WithColumn("DoubleCol").AsDouble()    // 倍精度浮動小数点型
                .WithColumn("GuidCol").AsGuid()        // レプリケーション ID型


                // 日付/時刻型(いずれも同じ結果)
                .WithColumn("DateCol").AsDate()
                .WithColumn("DateTimeCol").AsDateTime()
                .WithColumn("TimeCol").AsTime()

                // 通貨型
                .WithColumn("CurrencyCol").AsCurrency()


                // オートナンバー型
                .WithColumn("AutoInt32Col").AsInt32().Identity()    // 長整数型

                // オートナンバーのフィールドサイズでレプリケーションID型が指定できない
                // レプリケーションIDにしようとAsGuidやAsInt64を使うと、以下のエラー
                //.WithColumn("AutoGuid32Col").AsGuid().Identity()
                // System.ArgumentException: Jet Engine only allows identity columns on integer columns
                // チェックしている箇所
                // [fluentmigrator/JetColumn.cs at master · schambers/fluentmigrator · GitHub]
                // (../master/src/FluentMigrator.Runner/Generators/Jet/JetColumn.cs)


                // Yes/No型
                .WithColumn("YesNoCol").AsBoolean()

                // OLEオブジェクト型
                // 引数がないとエラー。引数の中身は特に考慮されないっぽい
                .WithColumn("BinarySizeCol").AsBinary(20)
                //.WithColumn("BinaryCol").AsBinary()

                // ハイパーリンク型・添付ファイル型
                // DDLからは生成できないため、DAOやADOXならOKか
                // [Microsoft Access tips: Field type reference - names and values for DDL, DAO, and ADOX]
                // (http://allenbrowne.com/ser-49.html)


                // その他

                // AsCustom()の使い方はよく分からない
                //.WithColumn("HyperLinkCol").AsCustom("Custom")

                // XML型は存在しないので、エラーになる
                //.WithColumn("XmlCol").AsXml()
                //.WithColumn("XmlSizeCol").AsXml(20)

                .WithColumn("LastCol").AsString();
        }
    }
}
