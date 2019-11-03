using Microsoft.EntityFrameworkCore.Migrations;

namespace DAR.Infrastructure.DiagramMigrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Domains",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Ordered = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Domains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HMLs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Version = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HMLs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Values",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Is = table.Column<string>(nullable: true),
                    From = table.Column<string>(nullable: true),
                    To = table.Column<string>(nullable: true),
                    Num = table.Column<string>(nullable: true),
                    DomainId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Values", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Values_Domains_DomainId",
                        column: x => x.DomainId,
                        principalTable: "Domains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ARDs",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Source = table.Column<string>(nullable: true),
                    Destination = table.Column<string>(nullable: true),
                    HMLId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ARDs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ARDs_HMLs_HMLId",
                        column: x => x.HMLId,
                        principalTable: "HMLs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attributes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    CLB = table.Column<string>(nullable: true),
                    Abbreviation = table.Column<string>(nullable: true),
                    Class = table.Column<string>(nullable: true),
                    Comm = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    HMLId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attributes_HMLs_HMLId",
                        column: x => x.HMLId,
                        principalTable: "HMLs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    HMLId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Properties_HMLs_HMLId",
                        column: x => x.HMLId,
                        principalTable: "HMLs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TPHs",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Source = table.Column<string>(nullable: true),
                    Destination = table.Column<string>(nullable: true),
                    HMLId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TPHs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TPHs_HMLs_HMLId",
                        column: x => x.HMLId,
                        principalTable: "HMLs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DomainId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Base = table.Column<string>(nullable: true),
                    Length = table.Column<string>(nullable: true),
                    Scale = table.Column<string>(nullable: true),
                    HMLId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Types_Domains_DomainId",
                        column: x => x.DomainId,
                        principalTable: "Domains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Types_HMLs_HMLId",
                        column: x => x.HMLId,
                        principalTable: "HMLs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "References",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Destination = table.Column<string>(nullable: true),
                    PropertyId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_References", x => x.Id);
                    table.ForeignKey(
                        name: "FK_References_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ARDs_HMLId",
                table: "ARDs",
                column: "HMLId");

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_HMLId",
                table: "Attributes",
                column: "HMLId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_HMLId",
                table: "Properties",
                column: "HMLId");

            migrationBuilder.CreateIndex(
                name: "IX_References_PropertyId",
                table: "References",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_TPHs_HMLId",
                table: "TPHs",
                column: "HMLId");

            migrationBuilder.CreateIndex(
                name: "IX_Types_DomainId",
                table: "Types",
                column: "DomainId");

            migrationBuilder.CreateIndex(
                name: "IX_Types_HMLId",
                table: "Types",
                column: "HMLId");

            migrationBuilder.CreateIndex(
                name: "IX_Values_DomainId",
                table: "Values",
                column: "DomainId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ARDs");

            migrationBuilder.DropTable(
                name: "Attributes");

            migrationBuilder.DropTable(
                name: "References");

            migrationBuilder.DropTable(
                name: "TPHs");

            migrationBuilder.DropTable(
                name: "Types");

            migrationBuilder.DropTable(
                name: "Values");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "Domains");

            migrationBuilder.DropTable(
                name: "HMLs");
        }
    }
}
