using FluentMigrator;

namespace FluentMigrator_sample.ColumnMigrations
{
    [Migration(2014110902)]
    public class Mig_02_ColumnProperty : AutoReversingMigration
    {
        public override void Up()
        {
            // DDLだけではできることが少ない
            // [Microsoft Access tips: DDL Programming Code Examples]
            // (http://allenbrowne.com/func-DDL.html)

            Create.Table("ColumnProperty")
                // 値要求(=Null不可)プロパティ
                .WithColumn("NotNullCol").AsString().NotNullable()  // 値要求：はい
                .WithColumn("NullableCol").AsString().Nullable()    // 値要求：いいえ

                // 既定値プロパティ(テキスト型、数値型、日付/時刻型)
                .WithColumn("DefalutStringCol").AsString().WithDefaultValue("デフォルト値")
                .WithColumn("DefalutInt32Col").AsInt32().WithDefaultValue(10)
                // *Migration実行時の日時が既定値となり、MS Accessに行追加したときの日時ではない
                .WithColumn("DefalutDateTime").AsDateTime().WithDefaultValue(System.DateTime.Now)

                // インデックスの作成
                // 重複なしで`IX_ColumnProperty_UniqueCol`という名前のインデックスが自動作成
                .WithColumn("UniqueCol").AsString().Unique()

                // MS Accessで使っても変化なかったもの
                .WithColumn("DescriptioCol").AsString().WithColumnDescription("Description")
                .WithDescription("TableDescription")

                // MS Accessで使うとエラーとなるもの
                // FluentMigrationのSystemMethodsがMS Accessで未実装のため使えない
                //.WithColumn("CurrentUser").AsString().WithDefault(SystemMethods.CurrentUser)    

                .WithColumn("LastColumn").AsString();
        }
    }
}
