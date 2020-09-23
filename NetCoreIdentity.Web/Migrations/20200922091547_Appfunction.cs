using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCoreIdentity.Web.Migrations
{
    public partial class Appfunction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NetCoreAppFunction",
                columns: table => new
                {
                    AppFunctionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppFunctionName = table.Column<string>(maxLength: 50, nullable: false),
                    SchemaName = table.Column<string>(maxLength: 50, nullable: true),
                    ControllerName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NetCoreAppFunction", x => x.AppFunctionID);
                });

            migrationBuilder.CreateTable(
                name: "NetCoreFunctionRole",
                columns: table => new
                {
                    AppFunctionId = table.Column<int>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NetCoreFunctionRole", x => new { x.AppFunctionId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_NetCoreFunctionRole_NetCoreAppFunction_AppFunctionId",
                        column: x => x.AppFunctionId,
                        principalTable: "NetCoreAppFunction",
                        principalColumn: "AppFunctionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NetCoreFunctionRole_NetCoreRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "NetCoreRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "NetCoreRoles",
                keyColumn: "Id",
                keyValue: "8abac3f2-0b6f-4c77-83a8-428c93d429e7",
                column: "ConcurrencyStamp",
                value: "ffc2f5cb-9fa0-4c8f-86fc-17577448b25e");

            migrationBuilder.UpdateData(
                table: "NetCoreUsers",
                keyColumn: "Id",
                keyValue: "c728ce77-d2b1-418f-9fb8-a7c1e1dba663",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a2e90c2f-7d7c-4ba5-9dde-761961c9381c", "f5bfdfd4-29f0-430c-af53-16a03c825844" });

            migrationBuilder.CreateIndex(
                name: "IX_NetCoreFunctionRole_RoleId",
                table: "NetCoreFunctionRole",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NetCoreFunctionRole");

            migrationBuilder.DropTable(
                name: "NetCoreAppFunction");

            migrationBuilder.UpdateData(
                table: "NetCoreRoles",
                keyColumn: "Id",
                keyValue: "8abac3f2-0b6f-4c77-83a8-428c93d429e7",
                column: "ConcurrencyStamp",
                value: "dc6a7353-50fd-4d6e-8f50-1d2f53d58e52");

            migrationBuilder.UpdateData(
                table: "NetCoreUsers",
                keyColumn: "Id",
                keyValue: "c728ce77-d2b1-418f-9fb8-a7c1e1dba663",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "afed879b-f341-4dcc-a912-65d9823a6601", "fae0f18e-a311-4426-a36a-680c902216c3" });
        }
    }
}
