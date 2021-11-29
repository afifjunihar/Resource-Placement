using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class update_project_tbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Creator_Id",
                table: "TB_M_Project",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Project_Creator_Id",
                table: "TB_M_Project",
                column: "Creator_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_Project_TB_M_User_Creator_Id",
                table: "TB_M_Project",
                column: "Creator_Id",
                principalTable: "TB_M_User",
                principalColumn: "User_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_Project_TB_M_User_Creator_Id",
                table: "TB_M_Project");

            migrationBuilder.DropIndex(
                name: "IX_TB_M_Project_Creator_Id",
                table: "TB_M_Project");

            migrationBuilder.DropColumn(
                name: "Creator_Id",
                table: "TB_M_Project");
        }
    }
}
