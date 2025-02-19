using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace crypto.Migrations
{
    /// <inheritdoc />
    public partial class FixIdToGuidProperly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Step 1: Add a new temporary UUID column
            migrationBuilder.AddColumn<Guid>(
                name: "NewId",
                table: "Users",
                nullable: false,
                defaultValueSql: "gen_random_uuid()"); // Generates UUIDs automatically

            // Step 2: Copy data from `int` Id column to `NewId` (if needed)
            migrationBuilder.Sql("UPDATE \"Users\" SET \"NewId\" = gen_random_uuid();");

            // Step 3: Drop the old integer primary key constraint
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            // Step 4: Drop the old `int` Id column
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Users");

            // Step 5: Rename `NewId` to `Id`
            migrationBuilder.RenameColumn(
                name: "NewId",
                table: "Users",
                newName: "Id");

            // Step 6: Set the new `Id` column as the primary key
            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
