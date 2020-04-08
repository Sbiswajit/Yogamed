using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectYogaMed.Data.Migrations
{
    public partial class thirdmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateTable(
                name: "DiseaseTable",
                columns: table => new
                {
                    DiseaseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiseaseName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiseaseTable", x => x.DiseaseId);
                });

            migrationBuilder.CreateTable(
                name: "UserDetails",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: true),
                    Useremail = table.Column<string>(nullable: true),
                    Usercontact = table.Column<long>(nullable: false),
                    Dob = table.Column<DateTime>(nullable: false),
                    Userpassword = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDetails", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "YogaTable",
                columns: table => new
                {
                    YogaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YogaName = table.Column<string>(nullable: true),
                    YogaStep = table.Column<string>(nullable: true),
                    YdfkId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YogaTable", x => x.YogaId);
                    table.ForeignKey(
                        name: "FK_YogaTable_DiseaseTable_YdfkId",
                        column: x => x.YdfkId,
                        principalTable: "DiseaseTable",
                        principalColumn: "DiseaseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserDisease",
                columns: table => new
                {
                    Udid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserIdFk = table.Column<int>(nullable: true),
                    Disease = table.Column<string>(nullable: true),
                    DiseaseIdfk = table.Column<int>(nullable: true),
                    DiseaseIdfkNavigationDiseaseId = table.Column<int>(nullable: true),
                    UserIdFkNavigationUserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDisease", x => x.Udid);
                    table.ForeignKey(
                        name: "FK_UserDisease_DiseaseTable_DiseaseIdfkNavigationDiseaseId",
                        column: x => x.DiseaseIdfkNavigationDiseaseId,
                        principalTable: "DiseaseTable",
                        principalColumn: "DiseaseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserDisease_UserDetails_UserIdFkNavigationUserId",
                        column: x => x.UserIdFkNavigationUserId,
                        principalTable: "UserDetails",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserDisease_DiseaseIdfkNavigationDiseaseId",
                table: "UserDisease",
                column: "DiseaseIdfkNavigationDiseaseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDisease_UserIdFkNavigationUserId",
                table: "UserDisease",
                column: "UserIdFkNavigationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_YogaTable_YdfkId",
                table: "YogaTable",
                column: "YdfkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDisease");

            migrationBuilder.DropTable(
                name: "YogaTable");

            migrationBuilder.DropTable(
                name: "UserDetails");

            migrationBuilder.DropTable(
                name: "DiseaseTable");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
