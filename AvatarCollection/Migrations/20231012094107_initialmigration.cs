using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AvatarCollection.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Catalogues",
                columns: table => new
                {
                    CatalogueID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalogues", x => x.CatalogueID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthenticationId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Collectables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollectableID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Worth = table.Column<decimal>(type: "DECIMAL(10,2)", nullable: true),
                    Price = table.Column<decimal>(type: "DECIMAL(10,2)", nullable: true),
                    Releasedate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperatingSystem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Platform = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComicEdition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Novel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BlueRay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DVD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PVC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FunkoPop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CatalogueID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collectables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Collectables_Catalogues_CatalogueID",
                        column: x => x.CatalogueID,
                        principalTable: "Catalogues",
                        principalColumn: "CatalogueID");
                });

            migrationBuilder.CreateTable(
                name: "MyCollections",
                columns: table => new
                {
                    MyCollectionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsersUserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyCollections", x => x.MyCollectionID);
                    table.ForeignKey(
                        name: "FK_MyCollections_Users_UsersUserID",
                        column: x => x.UsersUserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "CollectableMyCollection",
                columns: table => new
                {
                    CollectablesId = table.Column<int>(type: "int", nullable: false),
                    MyCollectionsMyCollectionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectableMyCollection", x => new { x.CollectablesId, x.MyCollectionsMyCollectionID });
                    table.ForeignKey(
                        name: "FK_CollectableMyCollection_Collectables_CollectablesId",
                        column: x => x.CollectablesId,
                        principalTable: "Collectables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollectableMyCollection_MyCollections_MyCollectionsMyCollectionID",
                        column: x => x.MyCollectionsMyCollectionID,
                        principalTable: "MyCollections",
                        principalColumn: "MyCollectionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollectableMyCollection_MyCollectionsMyCollectionID",
                table: "CollectableMyCollection",
                column: "MyCollectionsMyCollectionID");

            migrationBuilder.CreateIndex(
                name: "IX_Collectables_CatalogueID",
                table: "Collectables",
                column: "CatalogueID");

            migrationBuilder.CreateIndex(
                name: "IX_MyCollections_UsersUserID",
                table: "MyCollections",
                column: "UsersUserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollectableMyCollection");

            migrationBuilder.DropTable(
                name: "Collectables");

            migrationBuilder.DropTable(
                name: "MyCollections");

            migrationBuilder.DropTable(
                name: "Catalogues");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
