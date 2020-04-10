namespace Xy.SuperMarket.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Catergory = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Unit = c.String(),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        UserName = c.String(),
                        PassWord = c.String(),
                        Language = c.Int(nullable: false),
                        PaymentMethod = c.Int(nullable: false),
                        EmailAddress = c.String(),
                        HomeAddress_Country = c.String(),
                        HomeAddress_Province = c.String(),
                        HomeAddress_City = c.String(),
                        HomeAddress_Street = c.String(),
                        HomeAddress_StreetNumber = c.String(),
                        HomeAddress_ApartmentNumber = c.String(),
                        HomeAddress_Postcode = c.String(),
                        MailAddress_Country = c.String(),
                        MailAddress_Province = c.String(),
                        MailAddress_City = c.String(),
                        MailAddress_Street = c.String(),
                        MailAddress_StreetNumber = c.String(),
                        MailAddress_ApartmentNumber = c.String(),
                        MailAddress_Postcode = c.String(),
                        Group = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Products");
        }
    }
}
