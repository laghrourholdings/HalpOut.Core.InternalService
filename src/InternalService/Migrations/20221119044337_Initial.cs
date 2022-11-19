using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternalService.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IObjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsSuspended = table.Column<bool>(type: "boolean", nullable: false),
                    SuspendedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LogHandleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Descriptor = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IObjects", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IObjects");
        }
    }
}
