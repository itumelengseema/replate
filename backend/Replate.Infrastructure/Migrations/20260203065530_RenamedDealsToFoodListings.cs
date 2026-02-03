using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Replate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenamedDealsToFoodListings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DealItems_Deals_DealId",
                table: "DealItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Deals_DealId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "Deals");

            migrationBuilder.RenameColumn(
                name: "DealId",
                table: "Reservations",
                newName: "FoodListingId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_DealId",
                table: "Reservations",
                newName: "IX_Reservations_FoodListingId");

            migrationBuilder.RenameColumn(
                name: "DealId",
                table: "DealItems",
                newName: "FoodListingId");

            migrationBuilder.RenameIndex(
                name: "IX_DealItems_DealId",
                table: "DealItems",
                newName: "IX_DealItems_FoodListingId");

            migrationBuilder.CreateTable(
                name: "FoodListings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    OriginalPrice = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    DiscountedPrice = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    AvailableQuantity = table.Column<int>(type: "int", nullable: false),
                    FoodListingType = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    AvailableFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AvailableUntil = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VendorProfileId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodListings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodListings_VendorProfile_VendorProfileId",
                        column: x => x.VendorProfileId,
                        principalTable: "VendorProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodListings_PublicId",
                table: "FoodListings",
                column: "PublicId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FoodListings_VendorProfileId",
                table: "FoodListings",
                column: "VendorProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_DealItems_FoodListings_FoodListingId",
                table: "DealItems",
                column: "FoodListingId",
                principalTable: "FoodListings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_FoodListings_FoodListingId",
                table: "Reservations",
                column: "FoodListingId",
                principalTable: "FoodListings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DealItems_FoodListings_FoodListingId",
                table: "DealItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_FoodListings_FoodListingId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "FoodListings");

            migrationBuilder.RenameColumn(
                name: "FoodListingId",
                table: "Reservations",
                newName: "DealId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_FoodListingId",
                table: "Reservations",
                newName: "IX_Reservations_DealId");

            migrationBuilder.RenameColumn(
                name: "FoodListingId",
                table: "DealItems",
                newName: "DealId");

            migrationBuilder.RenameIndex(
                name: "IX_DealItems_FoodListingId",
                table: "DealItems",
                newName: "IX_DealItems_DealId");

            migrationBuilder.CreateTable(
                name: "Deals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorProfileId = table.Column<int>(type: "int", nullable: false),
                    AvailableFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AvailableQuantity = table.Column<int>(type: "int", nullable: false),
                    AvailableUntil = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DealType = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DiscountedPrice = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    OriginalPrice = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    PublicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deals_VendorProfile_VendorProfileId",
                        column: x => x.VendorProfileId,
                        principalTable: "VendorProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deals_PublicId",
                table: "Deals",
                column: "PublicId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deals_VendorProfileId",
                table: "Deals",
                column: "VendorProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_DealItems_Deals_DealId",
                table: "DealItems",
                column: "DealId",
                principalTable: "Deals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Deals_DealId",
                table: "Reservations",
                column: "DealId",
                principalTable: "Deals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
