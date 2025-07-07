using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class Stored_Procedure_AddPerson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Corrected MySQL stored procedure syntax
            string sp_AddPerson = @"
                CREATE PROCEDURE AddPerson(
                    IN p_PersonId CHAR(36),
                    IN p_PersonName VARCHAR(50),
                    IN p_Email VARCHAR(50),
                    IN p_DateOfBirth DATETIME,
                    IN p_Gender VARCHAR(10),
                    IN p_CountryId CHAR(36),
                    IN p_Address VARCHAR(50),
                    IN p_ReceiveNewsLetters BOOLEAN
                )
                BEGIN
                    INSERT INTO Persons(PersonId, PersonName, Email, DateOfBirth, Gender, CountryId, Address, ReceiveNewsLetters)
                    VALUES(p_PersonId, p_PersonName, p_Email, p_DateOfBirth, p_Gender, p_CountryId, p_Address, p_ReceiveNewsLetters);
                END;
            ";
            migrationBuilder.Sql(sp_AddPerson);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sp_AddPerson = @"
                DROP PROCEDURE IF EXISTS AddPerson;
            ";
            migrationBuilder.Sql(sp_AddPerson);
        }
    }
}