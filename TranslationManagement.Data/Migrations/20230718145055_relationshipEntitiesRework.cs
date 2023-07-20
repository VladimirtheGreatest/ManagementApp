using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TranslationManagement.Data.Migrations
{
    public partial class relationshipEntitiesRework : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TranslatorJobStatuses_TranslationJobs_TranslationJobId",
                table: "TranslatorJobStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_TranslatorStatuses_Translators_TranslatorId",
                table: "TranslatorStatuses");

            migrationBuilder.DropIndex(
                name: "IX_TranslatorStatuses_TranslatorId",
                table: "TranslatorStatuses");

            migrationBuilder.DropIndex(
                name: "IX_TranslatorJobStatuses_TranslationJobId",
                table: "TranslatorJobStatuses");

            migrationBuilder.DropColumn(
                name: "TranslatorId",
                table: "TranslatorStatuses");

            migrationBuilder.DropColumn(
                name: "TranslationJobId",
                table: "TranslatorJobStatuses");

            migrationBuilder.AddColumn<int>(
                name: "TranslatorStatusId",
                table: "Translators",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TranslationJobStatusId",
                table: "TranslationJobs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Translators_TranslatorStatusId",
                table: "Translators",
                column: "TranslatorStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TranslationJobs_TranslationJobStatusId",
                table: "TranslationJobs",
                column: "TranslationJobStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_TranslationJobs_TranslatorJobStatuses_TranslationJobStatusId",
                table: "TranslationJobs",
                column: "TranslationJobStatusId",
                principalTable: "TranslatorJobStatuses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Translators_TranslatorStatuses_TranslatorStatusId",
                table: "Translators",
                column: "TranslatorStatusId",
                principalTable: "TranslatorStatuses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TranslationJobs_TranslatorJobStatuses_TranslationJobStatusId",
                table: "TranslationJobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Translators_TranslatorStatuses_TranslatorStatusId",
                table: "Translators");

            migrationBuilder.DropIndex(
                name: "IX_Translators_TranslatorStatusId",
                table: "Translators");

            migrationBuilder.DropIndex(
                name: "IX_TranslationJobs_TranslationJobStatusId",
                table: "TranslationJobs");

            migrationBuilder.DropColumn(
                name: "TranslatorStatusId",
                table: "Translators");

            migrationBuilder.DropColumn(
                name: "TranslationJobStatusId",
                table: "TranslationJobs");

            migrationBuilder.AddColumn<int>(
                name: "TranslatorId",
                table: "TranslatorStatuses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TranslationJobId",
                table: "TranslatorJobStatuses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TranslatorStatuses_TranslatorId",
                table: "TranslatorStatuses",
                column: "TranslatorId",
                unique: true,
                filter: "[TranslatorId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TranslatorJobStatuses_TranslationJobId",
                table: "TranslatorJobStatuses",
                column: "TranslationJobId",
                unique: true,
                filter: "[TranslationJobId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_TranslatorJobStatuses_TranslationJobs_TranslationJobId",
                table: "TranslatorJobStatuses",
                column: "TranslationJobId",
                principalTable: "TranslationJobs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TranslatorStatuses_Translators_TranslatorId",
                table: "TranslatorStatuses",
                column: "TranslatorId",
                principalTable: "Translators",
                principalColumn: "Id");
        }
    }
}
