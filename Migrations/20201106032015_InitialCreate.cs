using Microsoft.EntityFrameworkCore.Migrations;

namespace NTest.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LidaData",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageURL = table.Column<string>(nullable: true),
                    SlopeAngle = table.Column<double>(nullable: false),
                    ImageClock = table.Column<string>(nullable: true),
                    LidarClock = table.Column<string>(nullable: true),
                    PrependDist = table.Column<int>(nullable: false),
                    GroudDist = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LidaData", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LidaData");
        }
    }
}
