using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KontoVerwaltungV4.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "KundenSet",
                table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Vorname = table.Column<string>(nullable: false),
                    Nachname = table.Column<string>(nullable: false),
                    Adresse = table.Column<string>(nullable: false),
                    Plz = table.Column<int>(nullable: false),
                    Ort = table.Column<string>(nullable: false),
                    TelefonNummer = table.Column<string>(nullable: true),
                    DatenschutzErklärung = table.Column<bool>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_KundenSet", x => x.ID); });

            migrationBuilder.CreateTable(
                "KontoSet",
                table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    pin_encrypt = table.Column<string>(nullable: true),
                    KontoNummer = table.Column<string>(nullable: false),
                    Betrag = table.Column<double>(nullable: false),
                    InhaberID = table.Column<int>(nullable: false),
                    konto_type = table.Column<string>(nullable: false),
                    FestgeldKontoZinsSatz = table.Column<double>(nullable: true),
                    GiroKontoZinsSatz = table.Column<double>(nullable: true),
                    SparKontoZinsSatz = table.Column<double>(nullable: true),
                    TagesgeldKontoZinsSatz = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KontoSet", x => x.ID);
                    table.ForeignKey(
                        "FK_KontoSet_KundenSet_InhaberID",
                        x => x.InhaberID,
                        "KundenSet",
                        "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "TransaktionsSet",
                table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Betrag = table.Column<double>(nullable: false),
                    Empfaenger = table.Column<string>(nullable: true),
                    Beschreibung = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    IdNummer = table.Column<Guid>(nullable: false),
                    KontoID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransaktionsSet", x => x.ID);
                    table.ForeignKey(
                        "FK_TransaktionsSet_KontoSet_KontoID",
                        x => x.KontoID,
                        "KontoSet",
                        "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_KontoSet_InhaberID",
                "KontoSet",
                "InhaberID");

            migrationBuilder.CreateIndex(
                "IX_TransaktionsSet_KontoID",
                "TransaktionsSet",
                "KontoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "TransaktionsSet");

            migrationBuilder.DropTable(
                "KontoSet");

            migrationBuilder.DropTable(
                "KundenSet");
        }
    }
}