using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCoreIdentity.Web.Migrations
{
    public partial class AdminSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "NetCoreRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8abac3f2-0b6f-4c77-83a8-428c93d429e7", "dc6a7353-50fd-4d6e-8f50-1d2f53d58e52", "Admin", "Administrator" });

            migrationBuilder.InsertData(
                table: "NetCoreUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "c728ce77-d2b1-418f-9fb8-a7c1e1dba663", 0, "afed879b-f341-4dcc-a912-65d9823a6601", "crusade771022@hotmail.com", true, false, null, "CRUSADE771022@HOTMAIL.COM", "Administrator", null, null, false, "fae0f18e-a311-4426-a36a-680c902216c3", false, "crusade771022@hotmail.com" });

            migrationBuilder.InsertData(
                table: "NetCoreUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "c728ce77-d2b1-418f-9fb8-a7c1e1dba663", "8abac3f2-0b6f-4c77-83a8-428c93d429e7" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "NetCoreUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "c728ce77-d2b1-418f-9fb8-a7c1e1dba663", "8abac3f2-0b6f-4c77-83a8-428c93d429e7" });

            migrationBuilder.DeleteData(
                table: "NetCoreRoles",
                keyColumn: "Id",
                keyValue: "8abac3f2-0b6f-4c77-83a8-428c93d429e7");

            migrationBuilder.DeleteData(
                table: "NetCoreUsers",
                keyColumn: "Id",
                keyValue: "c728ce77-d2b1-418f-9fb8-a7c1e1dba663");
        }
    }
}
