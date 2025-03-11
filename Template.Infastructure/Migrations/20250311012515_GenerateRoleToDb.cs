using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Template.Infastructure.Migrations
{
    /// <inheritdoc />
    public partial class GenerateRoleToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1a0514f0-8dae-457a-bf89-2b9ebc5bd0b9", "1a0514f0-8dae-457a-bf89-2b9ebc5bd0b9", "Admin", "ADMIN" },
                    { "8afc3fa6-320a-474d-b767-63c00e769768", "8afc3fa6-320a-474d-b767-63c00e769768", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1a0514f0-8dae-457a-bf89-2b9ebc5bd0b9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8afc3fa6-320a-474d-b767-63c00e769768");
        }
    }
}
