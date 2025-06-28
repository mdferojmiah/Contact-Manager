using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CountryName = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PersonName = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateOfBirth = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Gender = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CountryId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Address = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReceiveNewsLetters = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryId", "CountryName" },
                values: new object[,]
                {
                    { new Guid("11829d03-f2b6-4d29-8d14-1000b61e72cf"), "Pakistan" },
                    { new Guid("1dc09f1e-c61a-4438-96de-00acd31e93d6"), "Palestine" },
                    { new Guid("4c5308b5-efa1-49ad-85c1-25aeaae52d21"), "Bangladesh" },
                    { new Guid("8609cccd-54a7-4718-8d2a-3ba6f72389e7"), "Iran" },
                    { new Guid("b1a34b9b-ae18-4605-bb16-2a9a3957ab16"), "United Arab Emirates" }
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "PersonId", "Address", "CountryId", "DateOfBirth", "Email", "Gender", "PersonName", "ReceiveNewsLetters" },
                values: new object[,]
                {
                    { new Guid("08384f70-af76-409c-bbdd-96afa3c19738"), "4762 Karstens Avenue", new Guid("1dc09f1e-c61a-4438-96de-00acd31e93d6"), new DateTime(1997, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "nreadhead7@artisteer.com", "Male", "Nicolis Readhead", false },
                    { new Guid("0e32cf46-1b40-4bcd-bdd0-15b6e00c0aee"), "9 Duke Park", new Guid("11829d03-f2b6-4d29-8d14-1000b61e72cf"), new DateTime(1999, 7, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "tcrucec@blogspot.com", "Female", "Ted Cruce", true },
                    { new Guid("13e64f65-0b6c-45b2-b18b-a8871e1af2d8"), "546 Chinook Pass", new Guid("11829d03-f2b6-4d29-8d14-1000b61e72cf"), new DateTime(1998, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "abaddam3@narod.ru", "Female", "Agathe Baddam", true },
                    { new Guid("275a4927-8f67-476c-beb3-95e35ca3f8f7"), "65 Fordem Trail", new Guid("11829d03-f2b6-4d29-8d14-1000b61e72cf"), new DateTime(1997, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "cmcconigala@squarespace.com", "Female", "Caril McConigal", true },
                    { new Guid("2fbd9d9d-4c5d-43ca-9721-d6b11ee6bfc1"), "61 Vidon Hill", new Guid("4c5308b5-efa1-49ad-85c1-25aeaae52d21"), new DateTime(1992, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "dthame0@hp.com", "Female", "Darelle Thame", true },
                    { new Guid("32e660ff-2d44-4a96-9a4c-97c8b5a9fb0e"), "7 Doe Crossing Avenue", new Guid("8609cccd-54a7-4718-8d2a-3ba6f72389e7"), new DateTime(1992, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "sdunlap6@cyberchimps.com", "Male", "Shurlock Dunlap", true },
                    { new Guid("36ea9c3d-a426-4d09-9f46-e2b12e52b0e5"), "0 Bobwhite Hill", new Guid("8609cccd-54a7-4718-8d2a-3ba6f72389e7"), new DateTime(2005, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "dbooij9@ox.ac.uk", "Male", "Dominik Booij", true },
                    { new Guid("4e8dac0d-12ce-44b8-a998-8aeaf245e53a"), "61682 Mifflin Plaza", new Guid("b1a34b9b-ae18-4605-bb16-2a9a3957ab16"), new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "tyeob@ustream.tv", "Female", "Therese Yeo", true },
                    { new Guid("654016ab-9d9b-4658-bd49-2e7babfc6d56"), "87 Florence Center", new Guid("8609cccd-54a7-4718-8d2a-3ba6f72389e7"), new DateTime(2000, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "memberson5@furl.net", "Female", "Merline Emberson", false },
                    { new Guid("69e9b346-e785-423a-ad86-1c92c6457275"), "0179 Vahlen Pass", new Guid("1dc09f1e-c61a-4438-96de-00acd31e93d6"), new DateTime(2006, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "fhorsfield2@pagesperso-orange.fr", "Female", "Faina Horsfield", true },
                    { new Guid("8c5ebc57-c287-4d8b-ab92-aac34a13f3b9"), "5 Amoth Street", new Guid("8609cccd-54a7-4718-8d2a-3ba6f72389e7"), new DateTime(2004, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "speakerd@printfriendly.com", "Female", "Sheba Peaker", false },
                    { new Guid("bbc4f831-e965-4dbe-a112-cbd618f9421a"), "6279 Maywood Point", new Guid("8609cccd-54a7-4718-8d2a-3ba6f72389e7"), new DateTime(1991, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "fgeerdts4@apache.org", "Female", "Frances Geerdts", true },
                    { new Guid("bcc21781-33a6-4356-8875-d294cc26d7f2"), "6202 Porter Road", new Guid("b1a34b9b-ae18-4605-bb16-2a9a3957ab16"), new DateTime(2007, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "pblasik1@live.com", "Female", "Pier Blasik", true },
                    { new Guid("f8eea378-a640-4750-ae73-01941d9e5747"), "22 Homewood Street", new Guid("11829d03-f2b6-4d29-8d14-1000b61e72cf"), new DateTime(1994, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "frhydderche@hibu.com", "Male", "Franchot Rhydderch", false },
                    { new Guid("ff8ab986-3558-4f9b-91fe-31a03f291800"), "89 Oak Point", new Guid("4c5308b5-efa1-49ad-85c1-25aeaae52d21"), new DateTime(2000, 12, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "rla8@psu.edu", "Male", "Roderic La Torre", false }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
