using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherForecaster.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Deviations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ForecastMadeForecastCameTimeDelta = table.Column<int>(nullable: false),
                    Deviation = table.Column<float>(nullable: false),
                    CalculationTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deviations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeatherRecords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsFull = table.Column<bool>(nullable: false),
                    ForecastTime = table.Column<DateTime>(nullable: false),
                    ForecastTemperature = table.Column<float>(nullable: false),
                    ForecastMadeForecastCameTimeDelta = table.Column<int>(nullable: false),
                    ActualTemperature = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherRecords", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deviations");

            migrationBuilder.DropTable(
                name: "WeatherRecords");
        }
    }
}
