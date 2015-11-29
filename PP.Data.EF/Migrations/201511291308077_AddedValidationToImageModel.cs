namespace PP.Data.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedValidationToImageModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Images", "Description", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Images", "Description", c => c.String());
        }
    }
}