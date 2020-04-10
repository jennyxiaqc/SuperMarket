﻿namespace Xy.SuperMarket.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductR1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Catergory", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Unit", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Unit", c => c.String());
            AlterColumn("dbo.Products", "Catergory", c => c.String());
            AlterColumn("dbo.Products", "Name", c => c.String());
        }
    }
}
