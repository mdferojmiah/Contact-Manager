using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class StoredProcedure_DeletePerson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Corrected MySQL stored procedure syntax
            string sp_DeletePerson = @"
                CREATE PROCEDURE DeletePerson(
                    IN p_PersonId CHAR(36)
                )
                BEGIN
                    DELETE FROM Persons
                    WHERE PersonId = p_PersonId;
                END;
            ";
            migrationBuilder.Sql(sp_DeletePerson);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sp_DeletePerson = @"
                DROP PROCEDURE IF EXISTS DeletePerson;
            ";
            migrationBuilder.Sql(sp_DeletePerson);
        }
    }
}
