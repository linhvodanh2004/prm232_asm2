using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebClient.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorId", "Address", "City", "EmailAddress", "FirstName", "LastName", "Phone", "State", "Zip" },
                values: new object[,]
                {
                    { 1, "123 Main St", "New York", "john.doe@example.com", "John", "Doe", "123-456-7890", "NY", "10001" },
                    { 2, "456 Broad St", "London", "jane.smith@example.com", "Jane", "Smith", "987-654-3210", "LDN", "SW1A" }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "PubId", "City", "Country", "PublisherName", "State" },
                values: new object[,]
                {
                    { 1, "New York", "USA", "Tech Books Publishing", "NY" },
                    { 2, "London", "UK", "Education Press", "LDN" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Advance", "Notes", "Price", "PubId", "PublishedDate", "Royalty", "Title", "Type", "YtdSales" },
                values: new object[,]
                {
                    { 1, 1000m, "Comprehensive guide", 45.99m, 1, new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10m, "Mastering ASP.NET Core 8", "Technology", 500 },
                    { 2, 500m, "Deep dive into EF Core", 39.99m, 1, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 12m, "Entity Framework Core in Action", "Technology", 300 },
                    { 3, 2000m, "Classic computer science book", 55.00m, 2, new DateTime(2022, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 15m, "The Art of Programming", "Education", 1200 }
                });

            migrationBuilder.InsertData(
                table: "BookAuthors",
                columns: new[] { "AuthorId", "BookId", "AuthorOrder", "RoyaltyPercentage" },
                values: new object[,]
                {
                    { 1, 1, 0, 0m },
                    { 1, 2, 0, 0m },
                    { 2, 3, 0, 0m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "PubId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "PubId",
                keyValue: 2);
        }
    }
}
