using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class sendEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_M_Account",
                columns: table => new
                {
                    Account_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Account", x => x.Account_Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_Project",
                columns: table => new
                {
                    Project_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Project_Name = table.Column<string>(nullable: true),
                    Capacity = table.Column<int>(nullable: false),
                    Current_Capacity = table.Column<int>(nullable: false),
                    Required_Skill = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Project", x => x.Project_Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_Role",
                columns: table => new
                {
                    Role_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role_Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Role", x => x.Role_Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_Skill",
                columns: table => new
                {
                    Skill_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Skill_Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Skill", x => x.Skill_Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_User",
                columns: table => new
                {
                    User_Id = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: false),
                    User_Status = table.Column<string>(nullable: false),
                    Account_Id = table.Column<int>(nullable: false),
                    Manager_Id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_User", x => x.User_Id);
                    table.ForeignKey(
                        name: "FK_TB_M_User_TB_M_Account_Account_Id",
                        column: x => x.Account_Id,
                        principalTable: "TB_M_Account",
                        principalColumn: "Account_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_T_AccountRole",
                columns: table => new
                {
                    Account_Roles_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Account_Id = table.Column<int>(nullable: false),
                    Role_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_T_AccountRole", x => x.Account_Roles_Id);
                    table.ForeignKey(
                        name: "FK_TB_T_AccountRole_TB_M_Account_Account_Id",
                        column: x => x.Account_Id,
                        principalTable: "TB_M_Account",
                        principalColumn: "Account_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_T_AccountRole_TB_M_Role_Role_Id",
                        column: x => x.Role_Id,
                        principalTable: "TB_M_Role",
                        principalColumn: "Role_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_Interview",
                columns: table => new
                {
                    Interview_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Interview_Date = table.Column<DateTime>(nullable: false),
                    Interview_Result = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ReadBy = table.Column<string>(nullable: true),
                    User_Id = table.Column<string>(nullable: true),
                    Project_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Interview", x => x.Interview_Id);
                    table.ForeignKey(
                        name: "FK_TB_M_Interview_TB_M_Project_Project_Id",
                        column: x => x.Project_Id,
                        principalTable: "TB_M_Project",
                        principalColumn: "Project_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_M_Interview_TB_M_User_User_Id",
                        column: x => x.User_Id,
                        principalTable: "TB_M_User",
                        principalColumn: "User_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_T_Skill_Handler",
                columns: table => new
                {
                    Skill_Handler_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Score = table.Column<int>(nullable: false),
                    User_Id = table.Column<string>(nullable: true),
                    Skill_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_T_Skill_Handler", x => x.Skill_Handler_Id);
                    table.ForeignKey(
                        name: "FK_TB_T_Skill_Handler_TB_M_Skill_Skill_Id",
                        column: x => x.Skill_Id,
                        principalTable: "TB_M_Skill",
                        principalColumn: "Skill_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_T_Skill_Handler_TB_M_User_User_Id",
                        column: x => x.User_Id,
                        principalTable: "TB_M_User",
                        principalColumn: "User_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Interview_Project_Id",
                table: "TB_M_Interview",
                column: "Project_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Interview_User_Id",
                table: "TB_M_Interview",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_User_Account_Id",
                table: "TB_M_User",
                column: "Account_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_T_AccountRole_Account_Id",
                table: "TB_T_AccountRole",
                column: "Account_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TB_T_AccountRole_Role_Id",
                table: "TB_T_AccountRole",
                column: "Role_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TB_T_Skill_Handler_Skill_Id",
                table: "TB_T_Skill_Handler",
                column: "Skill_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TB_T_Skill_Handler_User_Id",
                table: "TB_T_Skill_Handler",
                column: "User_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_M_Interview");

            migrationBuilder.DropTable(
                name: "TB_T_AccountRole");

            migrationBuilder.DropTable(
                name: "TB_T_Skill_Handler");

            migrationBuilder.DropTable(
                name: "TB_M_Project");

            migrationBuilder.DropTable(
                name: "TB_M_Role");

            migrationBuilder.DropTable(
                name: "TB_M_Skill");

            migrationBuilder.DropTable(
                name: "TB_M_User");

            migrationBuilder.DropTable(
                name: "TB_M_Account");
        }
    }
}
