using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TranslationManagement.Data.Migrations
{
    public partial class seedStatusData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TranslatorJobStatuses",
                columns: new[] { "Id", "Status", "TranslationJobId" },
                values: new object[,]
                {
                    { 1, "New", null },
                    { 2, "InProgress", null },
                    { 3, "Completed", null }
                });

            migrationBuilder.InsertData(
                table: "TranslatorStatuses",
                columns: new[] { "Id", "Status", "TranslatorId" },
                values: new object[,]
                {
                    { 1, "Applicant", null },
                    { 2, "Certified", null },
                    { 3, "Deleted", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TranslatorJobStatuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TranslatorJobStatuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TranslatorJobStatuses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TranslatorStatuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TranslatorStatuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TranslatorStatuses",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
