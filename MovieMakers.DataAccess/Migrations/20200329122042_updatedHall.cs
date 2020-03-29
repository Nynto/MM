using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieMakers.DataAccess.Migrations
{
    public partial class updatedHall : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Hall_HallId",
                table: "Events");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hall",
                table: "Hall");

            migrationBuilder.DropColumn(
                name: "Chairs",
                table: "Hall");

            migrationBuilder.DropColumn(
                name: "Rows",
                table: "Hall");

            migrationBuilder.RenameTable(
                name: "Hall",
                newName: "Halls");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Halls",
                table: "Halls",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HallId = table.Column<int>(nullable: false),
                    Row = table.Column<int>(nullable: false),
                    Number = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seats_Halls_HallId",
                        column: x => x.HallId,
                        principalTable: "Halls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Seats_HallId",
                table: "Seats",
                column: "HallId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Halls_HallId",
                table: "Events",
                column: "HallId",
                principalTable: "Halls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Halls_HallId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Halls",
                table: "Halls");

            migrationBuilder.RenameTable(
                name: "Halls",
                newName: "Hall");

            migrationBuilder.AddColumn<string>(
                name: "Chairs",
                table: "Hall",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rows",
                table: "Hall",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hall",
                table: "Hall",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Hall_HallId",
                table: "Events",
                column: "HallId",
                principalTable: "Hall",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
