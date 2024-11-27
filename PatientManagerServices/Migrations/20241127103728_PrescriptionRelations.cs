using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientManagerServices.Migrations
{
    /// <inheritdoc />
    public partial class PrescriptionRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Illnesses_IllnessId",
                table: "Prescription");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_MedicalHistories_MedicalHistoryId",
                table: "Prescription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prescription",
                table: "Prescription");

            migrationBuilder.RenameTable(
                name: "Prescription",
                newName: "Prescriptions");

            migrationBuilder.RenameIndex(
                name: "IX_Prescription_MedicalHistoryId",
                table: "Prescriptions",
                newName: "IX_Prescriptions_MedicalHistoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Prescription_IllnessId",
                table: "Prescriptions",
                newName: "IX_Prescriptions_IllnessId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prescriptions",
                table: "Prescriptions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Illnesses_IllnessId",
                table: "Prescriptions",
                column: "IllnessId",
                principalTable: "Illnesses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_MedicalHistories_MedicalHistoryId",
                table: "Prescriptions",
                column: "MedicalHistoryId",
                principalTable: "MedicalHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Illnesses_IllnessId",
                table: "Prescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_MedicalHistories_MedicalHistoryId",
                table: "Prescriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prescriptions",
                table: "Prescriptions");

            migrationBuilder.RenameTable(
                name: "Prescriptions",
                newName: "Prescription");

            migrationBuilder.RenameIndex(
                name: "IX_Prescriptions_MedicalHistoryId",
                table: "Prescription",
                newName: "IX_Prescription_MedicalHistoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Prescriptions_IllnessId",
                table: "Prescription",
                newName: "IX_Prescription_IllnessId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prescription",
                table: "Prescription",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Illnesses_IllnessId",
                table: "Prescription",
                column: "IllnessId",
                principalTable: "Illnesses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_MedicalHistories_MedicalHistoryId",
                table: "Prescription",
                column: "MedicalHistoryId",
                principalTable: "MedicalHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
