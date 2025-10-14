using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Eleves_Classes_ClasseId",
                table: "Eleves");

            migrationBuilder.AddForeignKey(
                name: "FK_Eleves_Classes_ClasseId",
                table: "Eleves",
                column: "ClasseId",
                principalTable: "Classes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Eleves_Classes_ClasseId",
                table: "Eleves");

            migrationBuilder.AddForeignKey(
                name: "FK_Eleves_Classes_ClasseId",
                table: "Eleves",
                column: "ClasseId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
