    using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class real_mi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Owner",
                columns: table => new
                {
                    IdOwner = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    Address = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: true),
                    Photo = table.Column<byte[]>(type: "image", nullable: true),
                    Birthday = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owner", x => x.IdOwner);
                });

            migrationBuilder.CreateTable(
                name: "Property",
                columns: table => new
                {
                    idProperty = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    Address = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: true),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    CodeInternal = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Year = table.Column<short>(type: "smallint", nullable: true),
                    IdOwner = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Property", x => x.idProperty);
                    table.ForeignKey(
                        name: "FK_Property_Owner",
                        column: x => x.IdOwner,
                        principalTable: "Owner",
                        principalColumn: "IdOwner",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PropertyImage",
                columns: table => new
                {
                    IdPropertyImage = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idProperty = table.Column<int>(type: "int", nullable: false),
                    file = table.Column<byte[]>(type: "image", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyImage", x => new { x.IdPropertyImage, x.idProperty });
                    table.ForeignKey(
                        name: "FK_PropertyImage_Property",
                        column: x => x.idProperty,
                        principalTable: "Property",
                        principalColumn: "idProperty",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PropertyTrace",
                columns: table => new
                {
                    idPropertyTrace = table.Column<int>(type: "int", nullable: false),
                    DateSale = table.Column<DateTime>(type: "datetime", nullable: true),
                    Name = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    Value = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    Tax = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    idProperty = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyTrace", x => x.idPropertyTrace);
                    table.ForeignKey(
                        name: "FK_PropertyTrace_Property",
                        column: x => x.idProperty,
                        principalTable: "Property",
                        principalColumn: "idProperty",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Property_IdOwner",
                table: "Property",
                column: "IdOwner");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyImage_idProperty",
                table: "PropertyImage",
                column: "idProperty");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyTrace_idProperty",
                table: "PropertyTrace",
                column: "idProperty");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyImage");

            migrationBuilder.DropTable(
                name: "PropertyTrace");

            migrationBuilder.DropTable(
                name: "Property");

            migrationBuilder.DropTable(
                name: "Owner");
        }
    }
}
