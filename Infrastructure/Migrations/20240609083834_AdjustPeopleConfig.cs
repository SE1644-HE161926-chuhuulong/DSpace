using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdjustPeopleConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "LastModifiedBy",
                table: "Peoples",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "Peoples",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Peoples_CreatedBy",
                table: "Peoples",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Peoples_Peoples_CreatedBy",
                table: "Peoples",
                column: "CreatedBy",
                principalTable: "Peoples",
                principalColumn: "PeopleId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Peoples_Peoples_CreatedBy",
                table: "Peoples");

            migrationBuilder.DropIndex(
                name: "IX_Peoples_CreatedBy",
                table: "Peoples");

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                table: "Peoples",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Peoples",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
