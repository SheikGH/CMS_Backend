using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordKey = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "Email", "FirstName", "LastName", "Password", "PasswordKey", "Phone" },
                values: new object[,]
                {
                    { 1, "123 Main St", "john.doe@example.com", "John", "Doe", "John@123", "John@123", "1234567890" },
                    { 2, "456 Elm St", "jane.smith@example.com", "Jane", "Smith", "Jane@123", "Jane@123", "0987654321" },
                    { 3, "Kulas Light", "Sincere@example.com", "Leanne", "Graham", "Leanne@123", "Leanne@123", "17707368031" },
                    { 4, "Suite 879", "dennis@example.com", "Dennis", "Schulist", "Dennis@123", "Dennis@123", "098-765-4321" },
                    { 5, "Proactive didactic contingency", "glenna@example.com", "Glenna", "Reichert", "Glenna@123", "Glenna@123", "123-456-7890" },
                    { 6, "Wisokyburgh", "ervin@example.com", "Ervin", "Howell", "Ervin@123", "Ervin@123", "0987654321" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Email",
                table: "Customers",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
