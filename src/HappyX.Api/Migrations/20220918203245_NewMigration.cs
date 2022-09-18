using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HappyX.Api.Migrations
{
    public partial class NewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "moods",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_moods", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    slack_id = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    subscribed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "records",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    MoodId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_records", x => x.id);
                    table.ForeignKey(
                        name: "moods_fk",
                        column: x => x.MoodId,
                        principalTable: "moods",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "users_fk",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.InsertData(
                table: "moods",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "sad" },
                    { 2, "unhappy" },
                    { 3, "indifferent" },
                    { 4, "happy" },
                    { 5, "joyful" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_records_MoodId",
                table: "records",
                column: "MoodId");

            migrationBuilder.CreateIndex(
                name: "unique_user_date",
                table: "records",
                columns: new[] { "UserId", "creation_date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "unique_slack_id",
                table: "users",
                column: "slack_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "records");

            migrationBuilder.DropTable(
                name: "moods");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
