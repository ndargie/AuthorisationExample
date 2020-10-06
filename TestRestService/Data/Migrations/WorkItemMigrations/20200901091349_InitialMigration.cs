using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestRestService.Data.Migrations.WorkItemMigrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 200, nullable: true),
                    OrderNumber = table.Column<string>(maxLength: 200, nullable: false),
                    Quote = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(maxLength: 400, nullable: true),
                    QuoteRaised = table.Column<DateTime>(nullable: true),
                    Started = table.Column<DateTime>(nullable: true),
                    Finished = table.Column<DateTime>(nullable: true),
                    InvoiceRaised = table.Column<DateTime>(nullable: true),
                    InvoicePaid = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkItems", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkItems");
        }
    }
}
