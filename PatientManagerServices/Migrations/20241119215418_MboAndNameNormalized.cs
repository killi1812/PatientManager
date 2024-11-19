using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientManagerServices.Migrations
{
    /// <inheritdoc />
    public partial class MboAndNameNormalized : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Mbo",
                table: "Patients",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameNormalized",
                table: "Patients",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mbo",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "NameNormalized",
                table: "Patients");
        }
    }
}
