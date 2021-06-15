using Microsoft.EntityFrameworkCore.Migrations;

namespace SnackisAPI.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SiteContent",
                keyColumn: "Id",
                keyValue: "cfb54da5-50df-4283-9c52-c614ce11ecdf");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "AspNetUsers",
                newName: "ImageUri");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "root-0c0-aa65-4af8-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "fa06a98c-b6fa-463e-8813-6adfa71eae3f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "user-2c0-aa65-4af8-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "4a17d76f-09ac-43ce-859c-6c7df45de4b1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fa9565d7-462e-4bf7-9508-5be27ad33ff5", "AQAAAAEAACcQAAAAEE1S6r/8TD1iLaA6PBRKsRgsvuYZZbxtBvkbbViFV8fGhjjyBfn2aBAL1Hfs16hWxQ==", "09de9836-3b70-4ce6-af0c-dd8922252cca" });

            migrationBuilder.InsertData(
                table: "SiteContent",
                columns: new[] { "Id", "Title" },
                values: new object[] { "68d88ded-6141-406c-86b0-1933baaf0d2e", "MyTitle" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SiteContent",
                keyColumn: "Id",
                keyValue: "68d88ded-6141-406c-86b0-1933baaf0d2e");

            migrationBuilder.RenameColumn(
                name: "ImageUri",
                table: "AspNetUsers",
                newName: "ImageUrl");

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
    }
}
