using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientManagerServices.Migrations
{
    /// <inheritdoc />
    public partial class NameNormalizedIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_Patients_NameNormalized",
                table: "Patients",
                column: "NameNormalized");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Patients_NameNormalized",
                table: "Patients");
        }
    }
}
