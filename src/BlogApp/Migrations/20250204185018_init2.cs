using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogApp.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "B1C2D3E4-F5G6-2345-6789-0ABCDEF56789", "A1B2C3D4-E5F6-1234-5678-90ABCDEF1234" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "B1C2D3E4-F5G6-2345-6789-0ABCDEF56789");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "A1B2C3D4-E5F6-1234-5678-90ABCDEF1234");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "B1C2D3E4-F5G6-2345-6789-0ABCDEF56789", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "A1B2C3D4-E5F6-1234-5678-90ABCDEF1234", 0, "e3e14779-0c5e-4b51-b3b4-0982d06a971d", "admin@blogapi.com", true, false, null, "ADMIN@BLOGAPI.com", "ADMIN", "AQAAAAIAAYagAAAAECUreqhl32VAXDTCryRaRAchjy7LgF8JcOeiFnkveOjYnlKHjvjNvHRsRQeUHB3W6Q==", null, false, "cd900964-c739-4df9-a709-8b8ea9cfcd97", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "B1C2D3E4-F5G6-2345-6789-0ABCDEF56789", "A1B2C3D4-E5F6-1234-5678-90ABCDEF1234" });
        }
    }
}
