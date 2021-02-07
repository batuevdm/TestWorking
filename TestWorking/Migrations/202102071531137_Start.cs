namespace TestWorking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Start : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        ContactID = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ContactID);
            
            CreateTable(
                "dbo.PeopleContacts",
                c => new
                    {
                        PeopleID = c.Int(nullable: false),
                        ContactID = c.Int(nullable: false),
                        Value = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.PeopleID, t.ContactID, t.Value })
                .ForeignKey("dbo.Contacts", t => t.ContactID, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.PeopleID, cascadeDelete: true)
                .Index(t => t.PeopleID)
                .Index(t => t.ContactID);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        PeopleID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 255),
                        LastName = c.String(maxLength: 255),
                        MiddleName = c.String(maxLength: 255),
                        Birthday = c.DateTime(),
                        Organization = c.String(maxLength: 255),
                        Post = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.PeopleID);

            Sql("INSERT into [Contacts] VALUES ('Номер телефона'), ('E-Mail'), ('Skype'), ('Другое')");

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PeopleContacts", "PeopleID", "dbo.People");
            DropForeignKey("dbo.PeopleContacts", "ContactID", "dbo.Contacts");
            DropIndex("dbo.PeopleContacts", new[] { "ContactID" });
            DropIndex("dbo.PeopleContacts", new[] { "PeopleID" });
            DropTable("dbo.People");
            DropTable("dbo.PeopleContacts");
            DropTable("dbo.Contacts");
        }
    }
}
