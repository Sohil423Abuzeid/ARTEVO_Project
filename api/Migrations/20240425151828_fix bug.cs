using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class fixbug : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PortoflioMedia_Portoflios_PortoflioId",
                table: "PortoflioMedia");

            migrationBuilder.DropForeignKey(
                name: "FK_Portoflios_Artworks_ArtworkId",
                table: "Portoflios");

            migrationBuilder.DropTable(
                name: "Artworks");

            migrationBuilder.DropIndex(
                name: "IX_Portoflios_ArtworkId",
                table: "Portoflios");

            migrationBuilder.DropColumn(
                name: "ArtworkId",
                table: "Portoflios");

            migrationBuilder.AlterColumn<int>(
                name: "PortoflioId",
                table: "PortoflioMedia",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PortoflioMedia_Portoflios_PortoflioId",
                table: "PortoflioMedia",
                column: "PortoflioId",
                principalTable: "Portoflios",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PortoflioMedia_Portoflios_PortoflioId",
                table: "PortoflioMedia");

            migrationBuilder.AddColumn<int>(
                name: "ArtworkId",
                table: "Portoflios",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PortoflioId",
                table: "PortoflioMedia",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Artworks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artworks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Portoflios_ArtworkId",
                table: "Portoflios",
                column: "ArtworkId");

            migrationBuilder.AddForeignKey(
                name: "FK_PortoflioMedia_Portoflios_PortoflioId",
                table: "PortoflioMedia",
                column: "PortoflioId",
                principalTable: "Portoflios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Portoflios_Artworks_ArtworkId",
                table: "Portoflios",
                column: "ArtworkId",
                principalTable: "Artworks",
                principalColumn: "Id");
        }
    }
}
