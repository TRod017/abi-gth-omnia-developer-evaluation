using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    /// <inheritdoc />
    public partial class InsertDefaultUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Username", "Email", "Phone", "Password", "Role", "Status", "CreatedAt", "UpdatedAt" },
                values: new object[]
                {
            Guid.Parse("8c6b8ca2-3ec0-4a33-b826-bfc45ac4fd08"), // pode gerar um novo se quiser
            "joana.miranda",
            "joana@email.com",
            "+5521999991234",
            "$2a$11$b4P0AarhVGt0/ERcLjHC1eWN53lYT2wRjkGEeRClGr7ypH6Wtlfva",
            "Customer", // Customer
            "Active", // Active
            DateTime.UtcNow,
            null
                });
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
