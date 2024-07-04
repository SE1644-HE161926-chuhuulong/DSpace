using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddThreeFlowsToCommunityGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "canEdit",
                table: "CommunitiesGroups",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "canReview",
                table: "CommunitiesGroups",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "canSubmit",
                table: "CommunitiesGroups",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "canEdit",
                table: "CommunitiesGroups");

            migrationBuilder.DropColumn(
                name: "canReview",
                table: "CommunitiesGroups");

            migrationBuilder.DropColumn(
                name: "canSubmit",
                table: "CommunitiesGroups");
        }
    }
}
