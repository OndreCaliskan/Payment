using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payment.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig40 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Contacts_ContactID",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_ContactID",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "ContactID",
                table: "Subjects");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Subjects",
                newName: "SubjectID");

            migrationBuilder.AddColumn<int>(
                name: "SubjectID",
                table: "Contacts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_SubjectID",
                table: "Contacts",
                column: "SubjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Subjects_SubjectID",
                table: "Contacts",
                column: "SubjectID",
                principalTable: "Subjects",
                principalColumn: "SubjectID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Subjects_SubjectID",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_SubjectID",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "SubjectID",
                table: "Contacts");

            migrationBuilder.RenameColumn(
                name: "SubjectID",
                table: "Subjects",
                newName: "ID");

            migrationBuilder.AddColumn<int>(
                name: "ContactID",
                table: "Subjects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_ContactID",
                table: "Subjects",
                column: "ContactID");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Contacts_ContactID",
                table: "Subjects",
                column: "ContactID",
                principalTable: "Contacts",
                principalColumn: "ContactID");
        }
    }
}
