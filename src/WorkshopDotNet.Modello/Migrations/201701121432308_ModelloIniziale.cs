namespace WorkshopDotNet.Modello.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelloIniziale : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.telemetria",
                c => new
                    {
                        IdTelemetria = c.Int(nullable: false, identity: true),
                        Temperatura = c.Double(nullable: false),
                        Umidita = c.Int(nullable: false),
                        DataEvento = c.DateTime(nullable: false),
                        DataSalvataggio = c.DateTime(nullable: false),
                        IdDispositivo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdTelemetria)
                .ForeignKey("dbo.dispositivo", t => t.IdDispositivo, cascadeDelete: true)
                .Index(t => t.IdDispositivo);
            
            CreateTable(
                "dbo.dispositivo",
                c => new
                    {
                        IdDispositivo = c.Int(nullable: false, identity: true),
                        Descrizione = c.String(),
                        DataInstallazione = c.DateTime(),
                    })
                .PrimaryKey(t => t.IdDispositivo);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.telemetria", "IdDispositivo", "dbo.dispositivo");
            DropIndex("dbo.telemetria", new[] { "IdDispositivo" });
            DropTable("dbo.dispositivo");
            DropTable("dbo.telemetria");
        }
    }
}
