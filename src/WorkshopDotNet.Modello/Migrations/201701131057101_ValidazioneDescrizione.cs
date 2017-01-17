namespace WorkshopDotNet.Modello.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ValidazioneDescrizione : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.dispositivo", "Descrizione", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.dispositivo", "Descrizione", c => c.String());
        }
    }
}
