using Microsoft.EntityFrameworkCore.Migrations;

namespace FZBookHouse.DataAccess.Migrations
{
    public partial class AddStoreProcedureForCoverType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROC usp_GetCoverTypes
                                   AS
                                   BEGIN
                                    Select * from dbo.CoverTypes
                                   END");

            migrationBuilder.Sql(@"CREATE PROC usp_GetCoverType
                                   @Id int
                                   AS
                                   BEGIN
                                    Select * from dbo.CoverTypes
                                   WHERE (Id = @Id)
                                   END");

            migrationBuilder.Sql(@"CREATE PROC usp_UpdateCoverType
                                   @Id int,
                                   @Name varchar(100)
                                   AS
                                   BEGIN
                                   Update dbo.CoverTypes
                                   Set Name = @Name
                                   WHERE Id = @Id
                                   END");

            migrationBuilder.Sql(@"CREATE PROC usp_DeleteCoverType
                                    @Id int
                                   AS
                                   BEGIN
                                    Delete from dbo.CoverTypes
                                    WHERE Id = @Id
                                   END");

            migrationBuilder.Sql(@"CREATE PROC usp_CreateCoverType
                                   @Name varchar(100)
                                   AS
                                   BEGIN
                                    INSERT INTO dbo.CoverTypes(Name)
                                    VALUES(@Name)
                                   END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE usp_GetCoverTypes");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_GetCoverType");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_UpdateCoverType");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_DeleteCoverType");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_CreateCoverType");
        }
    }
}
