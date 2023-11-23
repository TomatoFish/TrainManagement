using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TrainManagement.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trains",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TrainNumber = table.Column<int>(type: "integer", nullable: false),
                    TrainIndexCombined = table.Column<string>(type: "text", nullable: true),
                    TrainIndex = table.Column<int>(type: "integer", nullable: false),
                    FromStationName = table.Column<string>(type: "text", nullable: false),
                    ToStationName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stops",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StationName = table.Column<string>(type: "text", nullable: false),
                    ParentTrainId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stops_Trains_ParentTrainId",
                        column: x => x.ParentTrainId,
                        principalTable: "Trains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InvoiceNum = table.Column<string>(type: "text", nullable: false),
                    PositionInTrain = table.Column<int>(type: "integer", nullable: false),
                    CarNumber = table.Column<long>(type: "bigint", nullable: false),
                    WhenLastOperation = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastOperationName = table.Column<string>(type: "text", nullable: false),
                    FreightEtsngName = table.Column<string>(type: "text", nullable: false),
                    FreightTotalWeightKg = table.Column<int>(type: "integer", nullable: false),
                    ParentStopId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_Stops_ParentStopId",
                        column: x => x.ParentStopId,
                        principalTable: "Stops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ParentStopId",
                table: "Cars",
                column: "ParentStopId");

            migrationBuilder.CreateIndex(
                name: "IX_Stops_ParentTrainId",
                table: "Stops",
                column: "ParentTrainId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Stops");

            migrationBuilder.DropTable(
                name: "Trains");
        }
    }
}
