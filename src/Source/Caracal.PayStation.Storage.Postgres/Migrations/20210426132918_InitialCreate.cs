using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Caracal.PayStation.Storage.Postgres.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "paystore");

            migrationBuilder.CreateTable(
                name: "withdrawals",
                schema: "paystore",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    account = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    amount = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    status = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_withdrawals", x => x.id);
                });
            
            AddDefaults(migrationBuilder);
        }

        private void AddDefaults(MigrationBuilder migrationBuilder) {
            var rnd = new Random();

            foreach (var i in Enumerable.Range(1, 5)) 
                migrationBuilder.InsertData(
                    "withdrawals", 
                    new string[] {"account", "amount", "status"}, 
                    new[] {$"Savings {i + rnd.Next(100, 900)}", $"R {i}0.44", "Requested"}, 
                    "paystore");
        }
        
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "withdrawals",
                schema: "paystore");
        }
    }
}
