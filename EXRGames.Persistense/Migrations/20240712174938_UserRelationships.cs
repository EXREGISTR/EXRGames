using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EXRGames.Persistense.Migrations
{
    /// <inheritdoc />
    public partial class UserRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "friends");

            migrationBuilder.CreateTable(
                name: "user_relationships",
                columns: table => new
                {
                    SenderId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TargetId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Accepted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_relationships", x => new { x.SenderId, x.TargetId });
                    table.ForeignKey(
                        name: "FK_user_relationships_user_profiles_SenderId",
                        column: x => x.SenderId,
                        principalTable: "user_profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_relationships_user_profiles_TargetId",
                        column: x => x.TargetId,
                        principalTable: "user_profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_user_relationships_TargetId",
                table: "user_relationships",
                column: "TargetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_relationships");

            migrationBuilder.CreateTable(
                name: "friends",
                columns: table => new
                {
                    SenderId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TargetId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_friends", x => new { x.SenderId, x.TargetId });
                    table.ForeignKey(
                        name: "FK_friends_user_profiles_SenderId",
                        column: x => x.SenderId,
                        principalTable: "user_profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_friends_user_profiles_TargetId",
                        column: x => x.TargetId,
                        principalTable: "user_profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_friends_TargetId",
                table: "friends",
                column: "TargetId");
        }
    }
}
