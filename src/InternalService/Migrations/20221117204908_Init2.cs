using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InternalService.Migrations
{
    public partial class Init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IObjects_ILogHandles_LogHandleId",
                table: "IObjects");

            migrationBuilder.DropTable(
                name: "ILogMessage");

            migrationBuilder.DropTable(
                name: "ILogHandles");

            migrationBuilder.DropIndex(
                name: "IX_IObjects_LogHandleId",
                table: "IObjects");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ILogHandles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ObjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    AuthorizationDetails = table.Column<string>(type: "text", nullable: true),
                    LocationDetails = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ILogHandles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ILogHandles_IObjects_ObjectId",
                        column: x => x.ObjectId,
                        principalTable: "IObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ILogMessage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ObjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LogHandleId = table.Column<Guid>(type: "uuid", nullable: true),
                    Message = table.Column<string>(type: "text", nullable: false),
                    Severity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ILogMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ILogMessage_ILogHandles_LogHandleId",
                        column: x => x.LogHandleId,
                        principalTable: "ILogHandles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ILogMessage_IObjects_ObjectId",
                        column: x => x.ObjectId,
                        principalTable: "IObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IObjects_LogHandleId",
                table: "IObjects",
                column: "LogHandleId");

            migrationBuilder.CreateIndex(
                name: "IX_ILogHandles_ObjectId",
                table: "ILogHandles",
                column: "ObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ILogMessage_LogHandleId",
                table: "ILogMessage",
                column: "LogHandleId");

            migrationBuilder.CreateIndex(
                name: "IX_ILogMessage_ObjectId",
                table: "ILogMessage",
                column: "ObjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_IObjects_ILogHandles_LogHandleId",
                table: "IObjects",
                column: "LogHandleId",
                principalTable: "ILogHandles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
