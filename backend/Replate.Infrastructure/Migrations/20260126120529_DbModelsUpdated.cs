using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Replate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DbModelsUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deals_VendorProfiles_VendorProfileId",
                table: "Deals");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Users_UserId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_vendorAddress_VendorProfiles_VendorProfileId",
                table: "vendorAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_VendorProfiles_Users_UserId",
                table: "VendorProfiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_vendorAddress",
                table: "vendorAddress");

            migrationBuilder.DropIndex(
                name: "IX_vendorAddress_Latitude_Longitude",
                table: "vendorAddress");

            migrationBuilder.DropIndex(
                name: "IX_vendorAddress_VendorProfileId",
                table: "vendorAddress");

            migrationBuilder.DropIndex(
                name: "IX_Deals_Category",
                table: "Deals");

            migrationBuilder.DropIndex(
                name: "IX_Deals_ExpiryDate",
                table: "Deals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VendorProfiles",
                table: "VendorProfiles");

            migrationBuilder.DropIndex(
                name: "IX_VendorProfiles_UserId",
                table: "VendorProfiles");

            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "DealItems");

            migrationBuilder.DropColumn(
                name: "BannerImageUrl",
                table: "VendorProfiles");

            migrationBuilder.DropColumn(
                name: "Decscription",
                table: "VendorProfiles");

            migrationBuilder.DropColumn(
                name: "LogoImageUrl",
                table: "VendorProfiles");

            migrationBuilder.RenameTable(
                name: "vendorAddress",
                newName: "VendorAddress");

            migrationBuilder.RenameTable(
                name: "VendorProfiles",
                newName: "VendorProfile");

            migrationBuilder.RenameColumn(
                name: "ReservedAt",
                table: "Reservations",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "QuantityAvailable",
                table: "Deals",
                newName: "DealType");

            migrationBuilder.RenameIndex(
                name: "IX_VendorProfiles_PublicId",
                table: "VendorProfile",
                newName: "IX_VendorProfile_PublicId");

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "VendorAddress",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "VendorAddress",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "PublicId",
                table: "VendorAddress",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "VendorAddress",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirebaseUid",
                table: "Users",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Users",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Reservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PickupInstructions",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PickupTime",
                table: "Reservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Reservations",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Deals",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Deals",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AddColumn<DateTime>(
                name: "AvailableFrom",
                table: "Deals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "AvailableQuantity",
                table: "Deals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "AvailableUntil",
                table: "Deals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Deals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Deals",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "DealItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BusinessName",
                table: "VendorProfile",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AddColumn<string>(
                name: "BusinessHours",
                table: "VendorProfile",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "VendorProfile",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "VendorProfile",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "VendorProfile",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "VendorProfile",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
                table: "VendorProfile",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "VendorProfile",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VendorAddressId",
                table: "VendorProfile",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VendorAddress",
                table: "VendorAddress",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VendorProfile",
                table: "VendorProfile",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_VendorAddress_PublicId",
                table: "VendorAddress",
                column: "PublicId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VendorProfile_UserId",
                table: "VendorProfile",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VendorProfile_VendorAddressId",
                table: "VendorProfile",
                column: "VendorAddressId",
                unique: true,
                filter: "[VendorAddressId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_VendorProfile_VendorProfileId",
                table: "Deals",
                column: "VendorProfileId",
                principalTable: "VendorProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Users_UserId",
                table: "Reservations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VendorProfile_Users_UserId",
                table: "VendorProfile",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VendorProfile_VendorAddress_VendorAddressId",
                table: "VendorProfile",
                column: "VendorAddressId",
                principalTable: "VendorAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deals_VendorProfile_VendorProfileId",
                table: "Deals");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Users_UserId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_VendorProfile_Users_UserId",
                table: "VendorProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_VendorProfile_VendorAddress_VendorAddressId",
                table: "VendorProfile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VendorAddress",
                table: "VendorAddress");

            migrationBuilder.DropIndex(
                name: "IX_VendorAddress_PublicId",
                table: "VendorAddress");

            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VendorProfile",
                table: "VendorProfile");

            migrationBuilder.DropIndex(
                name: "IX_VendorProfile_UserId",
                table: "VendorProfile");

            migrationBuilder.DropIndex(
                name: "IX_VendorProfile_VendorAddressId",
                table: "VendorProfile");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "VendorAddress");

            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "VendorAddress");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "VendorAddress");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "PickupInstructions",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "PickupTime",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "AvailableFrom",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "AvailableQuantity",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "AvailableUntil",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "BusinessHours",
                table: "VendorProfile");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "VendorProfile");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "VendorProfile");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "VendorProfile");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "VendorProfile");

            migrationBuilder.DropColumn(
                name: "LogoUrl",
                table: "VendorProfile");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "VendorProfile");

            migrationBuilder.DropColumn(
                name: "VendorAddressId",
                table: "VendorProfile");

            migrationBuilder.RenameTable(
                name: "VendorAddress",
                newName: "vendorAddress");

            migrationBuilder.RenameTable(
                name: "VendorProfile",
                newName: "VendorProfiles");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Reservations",
                newName: "ReservedAt");

            migrationBuilder.RenameColumn(
                name: "DealType",
                table: "Deals",
                newName: "QuantityAvailable");

            migrationBuilder.RenameIndex(
                name: "IX_VendorProfile_PublicId",
                table: "VendorProfiles",
                newName: "IX_VendorProfiles_PublicId");

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "vendorAddress",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "FirebaseUid",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "Users",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Deals",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Deals",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryDate",
                table: "Deals",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "DealItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "DealItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BusinessName",
                table: "VendorProfiles",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<string>(
                name: "BannerImageUrl",
                table: "VendorProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Decscription",
                table: "VendorProfiles",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LogoImageUrl",
                table: "VendorProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_vendorAddress",
                table: "vendorAddress",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VendorProfiles",
                table: "VendorProfiles",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_vendorAddress_Latitude_Longitude",
                table: "vendorAddress",
                columns: new[] { "Latitude", "Longitude" });

            migrationBuilder.CreateIndex(
                name: "IX_vendorAddress_VendorProfileId",
                table: "vendorAddress",
                column: "VendorProfileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deals_Category",
                table: "Deals",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_Deals_ExpiryDate",
                table: "Deals",
                column: "ExpiryDate");

            migrationBuilder.CreateIndex(
                name: "IX_VendorProfiles_UserId",
                table: "VendorProfiles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_VendorProfiles_VendorProfileId",
                table: "Deals",
                column: "VendorProfileId",
                principalTable: "VendorProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Users_UserId",
                table: "Reservations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_vendorAddress_VendorProfiles_VendorProfileId",
                table: "vendorAddress",
                column: "VendorProfileId",
                principalTable: "VendorProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VendorProfiles_Users_UserId",
                table: "VendorProfiles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
