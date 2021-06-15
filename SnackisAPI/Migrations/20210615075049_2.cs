using Microsoft.EntityFrameworkCore.Migrations;

namespace SnackisAPI.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SiteContent",
                keyColumn: "Id",
                keyValue: "2370e4bb-3106-43bd-9f48-af01c7e38f42");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "root-0c0-aa65-4af8-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "4cacea93-3fe0-4e76-8500-53a62088089e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "user-2c0-aa65-4af8-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "e77b1293-c03c-4b77-9398-0d67185d8fc7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "218f1238-d7fb-4e06-bcce-8a7d5497e17c", "AQAAAAEAACcQAAAAEJMDjVZxOqsIkgpd1u3qNmKRsM7f8LUDtlA9JV6vrjPXenBsRHiZfqmVeb36nsoYxg==", "acd2394f-5308-4f4a-ad83-53876574cee6" });

            migrationBuilder.InsertData(
                table: "SiteContent",
                columns: new[] { "Id", "Title" },
                values: new object[] { "cfb54da5-50df-4283-9c52-c614ce11ecdf", "MyTitle" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SiteContent",
                keyColumn: "Id",
                keyValue: "cfb54da5-50df-4283-9c52-c614ce11ecdf");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "root-0c0-aa65-4af8-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "a8dba4ca-0cee-49e1-9036-64ef7461bda3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "user-2c0-aa65-4af8-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "195ed031-7e36-4ae3-a9e5-4ce06702aa2a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6bd3e8c3-c49e-4a17-a4b9-ab2cabad5526", "AQAAAAEAACcQAAAAEGcFokfsIeymPDx14Wchd5uOz1SglBOGWmTu9szNdht5EQdWmtDDDxhTZyTACCmtVg==", "6d303842-db81-4ec4-9fdb-239424f45c71" });

            migrationBuilder.InsertData(
                table: "SiteContent",
                columns: new[] { "Id", "Title" },
                values: new object[] { "2370e4bb-3106-43bd-9f48-af01c7e38f42", "MyTitle" });
        }
    }
}
