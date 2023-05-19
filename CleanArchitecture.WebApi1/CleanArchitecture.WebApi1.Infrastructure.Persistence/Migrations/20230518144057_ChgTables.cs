using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.WebApi1.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChgTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactInfos_Addresses_AddressId",
                table: "ContactInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_Families_ContactInfos_ContactInfoId",
                table: "Families");

            migrationBuilder.DropForeignKey(
                name: "FK_Families_Insurances_InsuranceId",
                table: "Families");

            migrationBuilder.DropForeignKey(
                name: "FK_Parents_ContactInfos_ContactInfoId",
                table: "Parents");

            migrationBuilder.DropForeignKey(
                name: "FK_Parents_Families_FamilyId",
                table: "Parents");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_ContactInfos_ContactInfoId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Families_FamilyId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_ContactInfoId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Parents_ContactInfoId",
                table: "Parents");

            migrationBuilder.DropIndex(
                name: "IX_Families_ContactInfoId",
                table: "Families");

            migrationBuilder.DropIndex(
                name: "IX_Families_InsuranceId",
                table: "Families");

            migrationBuilder.AlterColumn<int>(
                name: "FamilyId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ContactInfoId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FamilyId",
                table: "Parents",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ContactInfoId",
                table: "Parents",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExternalId",
                table: "Families",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ContactInfoId",
                table: "Families",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "ContactInfos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactInfos_Address",
                table: "ContactInfos",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_Family",
                table: "Parents",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Family",
                table: "Students",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactInfos_Address",
                table: "ContactInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_Parents_Family",
                table: "Parents");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Family",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "FamilyId",
                table: "Students",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ContactInfoId",
                table: "Students",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FamilyId",
                table: "Parents",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ContactInfoId",
                table: "Parents",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ExternalId",
                table: "Families",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ContactInfoId",
                table: "Families",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "ContactInfos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ContactInfoId",
                table: "Students",
                column: "ContactInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Parents_ContactInfoId",
                table: "Parents",
                column: "ContactInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Families_ContactInfoId",
                table: "Families",
                column: "ContactInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Families_InsuranceId",
                table: "Families",
                column: "InsuranceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactInfos_Addresses_AddressId",
                table: "ContactInfos",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Families_ContactInfos_ContactInfoId",
                table: "Families",
                column: "ContactInfoId",
                principalTable: "ContactInfos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Families_Insurances_InsuranceId",
                table: "Families",
                column: "InsuranceId",
                principalTable: "Insurances",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_ContactInfos_ContactInfoId",
                table: "Parents",
                column: "ContactInfoId",
                principalTable: "ContactInfos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_Families_FamilyId",
                table: "Parents",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_ContactInfos_ContactInfoId",
                table: "Students",
                column: "ContactInfoId",
                principalTable: "ContactInfos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Families_FamilyId",
                table: "Students",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "Id");
        }
    }
}
