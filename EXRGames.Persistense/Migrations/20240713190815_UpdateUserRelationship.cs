using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EXRGames.Persistense.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Accepted",
                table: "user_relationships");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "RegistrationDate",
                table: "user_profiles",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Accepted",
                table: "user_relationships",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "RegistrationDate",
                table: "user_profiles",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }
    }
}
