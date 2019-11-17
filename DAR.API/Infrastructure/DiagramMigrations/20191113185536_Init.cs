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
                    Id = table.Column<string>(nullable: false),
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
                    Id = table.Column<string>(nullable: false),
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
                    Id = table.Column<string>(nullable: false),
                    Is = table.Column<string>(nullable: true),
                    From = table.Column<string>(nullable: true),
                    To = table.Column<string>(nullable: true),
                    Num = table.Column<string>(nullable: true),
                    DomainId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Values", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Values_Domains_DomainId",
                        column: x => x.DomainId,
                        principalTable: "Domains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attributes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    HMLId = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    CLB = table.Column<string>(nullable: true),
                    Abbreviation = table.Column<string>(nullable: true),
                    Class = table.Column<string>(nullable: true),
                    Comm = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attributes", x => new { x.Id, x.HMLId });
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
                    HMLId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => new { x.Id, x.HMLId });
                    table.ForeignKey(
                        name: "FK_Properties_HMLs_HMLId",
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
                    HMLId = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DomainId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Base = table.Column<string>(nullable: true),
                    Length = table.Column<string>(nullable: true),
                    Scale = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => new { x.Id, x.HMLId });
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
                name: "ARDs",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    HMLId = table.Column<string>(nullable: false),
                    Source = table.Column<string>(nullable: true),
                    Destination = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ARDs", x => new { x.Id, x.HMLId });
                    table.ForeignKey(
                        name: "FK_ARDs_HMLs_HMLId",
                        column: x => x.HMLId,
                        principalTable: "HMLs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ARDs_Properties_Destination_HMLId",
                        columns: x => new { x.Destination, x.HMLId },
                        principalTable: "Properties",
                        principalColumns: new[] { "Id", "HMLId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ARDs_Properties_Source_HMLId",
                        columns: x => new { x.Source, x.HMLId },
                        principalTable: "Properties",
                        principalColumns: new[] { "Id", "HMLId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "References",
                columns: table => new
                {
                    Destination = table.Column<string>(nullable: false),
                    HMLId = table.Column<string>(nullable: false),
                    Source = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_References", x => new { x.Source, x.Destination, x.HMLId });
                    table.ForeignKey(
                        name: "FK_References_Attributes_Destination_HMLId",
                        columns: x => new { x.Destination, x.HMLId },
                        principalTable: "Attributes",
                        principalColumns: new[] { "Id", "HMLId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_References_Properties_Source_HMLId",
                        columns: x => new { x.Source, x.HMLId },
                        principalTable: "Properties",
                        principalColumns: new[] { "Id", "HMLId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TPHs",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    HMLId = table.Column<string>(nullable: false),
                    Source = table.Column<string>(nullable: true),
                    Destination = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TPHs", x => new { x.Id, x.HMLId });
                    table.ForeignKey(
                        name: "FK_TPHs_HMLs_HMLId",
                        column: x => x.HMLId,
                        principalTable: "HMLs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TPHs_Properties_Destination_HMLId",
                        columns: x => new { x.Destination, x.HMLId },
                        principalTable: "Properties",
                        principalColumns: new[] { "Id", "HMLId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TPHs_Properties_Source_HMLId",
                        columns: x => new { x.Source, x.HMLId },
                        principalTable: "Properties",
                        principalColumns: new[] { "Id", "HMLId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ARDs_HMLId",
                table: "ARDs",
                column: "HMLId");

            migrationBuilder.CreateIndex(
                name: "IX_ARDs_Destination_HMLId",
                table: "ARDs",
                columns: new[] { "Destination", "HMLId" });

            migrationBuilder.CreateIndex(
                name: "IX_ARDs_Source_HMLId",
                table: "ARDs",
                columns: new[] { "Source", "HMLId" });

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_HMLId",
                table: "Attributes",
                column: "HMLId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_HMLId",
                table: "Properties",
                column: "HMLId");

            migrationBuilder.CreateIndex(
                name: "IX_References_Destination_HMLId",
                table: "References",
                columns: new[] { "Destination", "HMLId" });

            migrationBuilder.CreateIndex(
                name: "IX_References_Source_HMLId",
                table: "References",
                columns: new[] { "Source", "HMLId" });

            migrationBuilder.CreateIndex(
                name: "IX_TPHs_HMLId",
                table: "TPHs",
                column: "HMLId");

            migrationBuilder.CreateIndex(
                name: "IX_TPHs_Destination_HMLId",
                table: "TPHs",
                columns: new[] { "Destination", "HMLId" });

            migrationBuilder.CreateIndex(
                name: "IX_TPHs_Source_HMLId",
                table: "TPHs",
                columns: new[] { "Source", "HMLId" });

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
                name: "References");

            migrationBuilder.DropTable(
                name: "TPHs");

            migrationBuilder.DropTable(
                name: "Types");

            migrationBuilder.DropTable(
                name: "Values");

            migrationBuilder.DropTable(
                name: "Attributes");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "Domains");

            migrationBuilder.DropTable(
                name: "HMLs");
        }
    }
}
