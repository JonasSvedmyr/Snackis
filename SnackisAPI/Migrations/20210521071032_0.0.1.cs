using Microsoft.EntityFrameworkCore.Migrations;

namespace SnackisAPI.Migrations
{
    public partial class _001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SiteContent",
                keyColumn: "Id",
                keyValue: "d7176a3f-0ba9-4dfd-b96a-e578fe2aa281");

            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "Comments",
                newName: "Text");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "root-0c0-aa65-4af8-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "f9b9d977-2736-4b89-a8d6-d7d6f804920a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "user-2c0-aa65-4af8-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "b90129a2-0d70-4477-b566-76b9fcb37289");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "45e7ea68-8b11-4ac7-98fe-c1e3d603d186", "AQAAAAEAACcQAAAAENkWqKMYKHrmppZb5lENyFJFTob/cObww7sYpdtT3ij45+ob9rmMBxpHIafkcHrq/g==", "d95e107e-24b3-409d-b1b6-0d2c5bdf7e15" });

            migrationBuilder.InsertData(
                table: "SiteContent",
                columns: new[] { "Id", "Title" },
                values: new object[] { "ce1c4fa1-d688-426a-b537-a34346d2cb47", "MyTitle" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SiteContent",
                keyColumn: "Id",
                keyValue: "ce1c4fa1-d688-426a-b537-a34346d2cb47");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Comments",
                newName: "Comment");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "root-0c0-aa65-4af8-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "628bd5d3-6015-4190-bf71-a50a08554c13");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "user-2c0-aa65-4af8-bd17-00bd9344e575",
                column: "ConcurrencyStamp",
                value: "3d9c1701-052b-453f-9089-9cdad330826f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dd9ad876-dd24-4d70-b89a-b17056587403", "AQAAAAEAACcQAAAAEEwVSX4WL3niQSACdEpgoduMQLX/PQ0p1L6AGWf18JMsiH36PMuKM5lXYxvWbuqk5g==", "f9dd018e-8d17-4a48-b0ed-bcfdd18473a5" });

            migrationBuilder.InsertData(
                table: "SiteContent",
                columns: new[] { "Id", "Title" },
                values: new object[] { "d7176a3f-0ba9-4dfd-b96a-e578fe2aa281", "MyTitle" });
        }
    }
}
