using Microsoft.EntityFrameworkCore.Migrations;

namespace Caracal.PayStation.Storage.Postgres.Migrations
{
    public partial class WorkflowColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "workflowUrl",
                schema: "paystore",
                table: "withdrawals",
                type: "character varying(2048)",
                maxLength: 2048,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "workflowUrl",
                schema: "paystore",
                table: "withdrawals");
        }
    }
}
