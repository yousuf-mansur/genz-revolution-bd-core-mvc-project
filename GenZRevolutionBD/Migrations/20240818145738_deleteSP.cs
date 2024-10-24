using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GenZRevolutionBD.Migrations
{
    /// <inheritdoc />
    public partial class deleteSP : Migration
    {

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE DeleteSuperHero
                                @SuperHeroId Int
                AS
                BEGIN
                        DELETE FROM SuperHeroes
                        WHERE SuperHeroId=@SuperHeroId;
                END;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.Sql("DROP PROCEDURE DeleteSuperHero");
        }
    }
}
