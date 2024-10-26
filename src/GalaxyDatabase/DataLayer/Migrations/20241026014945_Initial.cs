using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace PTA.GalaxyDatabase.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Sqlite:InitSpatialMetaData", true);

            migrationBuilder.CreateTable(
                name: "StarSystems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false),
                    Position = table.Column<Point>(type: "POINT", nullable: false)
                        .Annotation("Sqlite:Srid", 4979)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StarSystems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StellarClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Class = table.Column<byte>(type: "INTEGER", nullable: false),
                    SubClass = table.Column<byte>(type: "INTEGER", nullable: false),
                    Luminosity = table.Column<byte>(type: "INTEGER", nullable: false),
                    CanBeScooped = table.Column<bool>(type: "INTEGER", nullable: false, computedColumnSql: "Class BETWEEN 0 AND 13")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StellarClasses", x => x.Id);
                    table.UniqueConstraint("AK_StellarClasses_Class_SubClass_Luminosity", x => new { x.Class, x.SubClass, x.Luminosity });
                });

            migrationBuilder.CreateTable(
                name: "Stars",
                columns: table => new
                {
                    SystemId = table.Column<long>(type: "INTEGER", nullable: false),
                    Id = table.Column<long>(type: "INTEGER", nullable: false),
                    ClassId = table.Column<int>(type: "INTEGER", nullable: true),
                    DistanceFromMainStar = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stars", x => new { x.SystemId, x.Id });
                    table.ForeignKey(
                        name: "FK_Stars_StarSystems_SystemId",
                        column: x => x.SystemId,
                        principalTable: "StarSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stars_StellarClasses_ClassId",
                        column: x => x.ClassId,
                        principalTable: "StellarClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stars_ClassId",
                table: "Stars",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_StarSystems_Position",
                table: "StarSystems",
                column: "Position",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stars");

            migrationBuilder.DropTable(
                name: "StarSystems");

            migrationBuilder.DropTable(
                name: "StellarClasses");
        }
    }
}
