using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RenadWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddAllTAble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfProject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalAmount = table.Column<int>(type: "int", nullable: true),
                    RestFromAmount = table.Column<int>(type: "int", nullable: true),
                    Approved = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AmountLate = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProjectBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RatioForEng = table.Column<int>(type: "int", nullable: true),
                    TotalRatioForEng = table.Column<int>(type: "int", nullable: true),
                    TotalOfContract = table.Column<int>(type: "int", nullable: true),
                    TotalTax = table.Column<int>(type: "int", nullable: true),
                    TotalAmount = table.Column<int>(type: "int", nullable: true),
                    RestOfContractShow = table.Column<int>(type: "int", nullable: true),
                    RestOfEngShow = table.Column<int>(type: "int", nullable: true),
                    RestOfTAxShow = table.Column<int>(type: "int", nullable: true),
                    Approved = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AmountLate = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Eng",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EngName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Offices = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Connection = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompOrFree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Meeting = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TimeOfMeeting = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastCommunication = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eng", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FinaicalRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FinaicalNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Titel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinaicalRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Payments = table.Column<int>(type: "int", nullable: true),
                    DateForPayment = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LateTime1 = table.Column<int>(type: "int", nullable: true),
                    RatioForEng = table.Column<int>(type: "int", nullable: true),
                    DueEngRatio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentTax = table.Column<int>(type: "int", nullable: true),
                    DueTax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayMentValue = table.Column<int>(type: "int", nullable: true),
                    Difference = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientContract",
                columns: table => new
                {
                    ContractId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientContract", x => new { x.ContractId, x.ClientId });
                    table.ForeignKey(
                        name: "FK_ClientContract_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClientContract_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EngContract",
                columns: table => new
                {
                    ContractId = table.Column<int>(type: "int", nullable: false),
                    EngId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngContract", x => new { x.ContractId, x.EngId });
                    table.ForeignKey(
                        name: "FK_EngContract_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EngContract_Eng_EngId",
                        column: x => x.EngId,
                        principalTable: "Eng",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ContractFinaical",
                columns: table => new
                {
                    ContractId = table.Column<int>(type: "int", nullable: false),
                    FinaicalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractFinaical", x => new { x.ContractId, x.FinaicalId });
                    table.ForeignKey(
                        name: "FK_ContractFinaical_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ContractFinaical_FinaicalRequests_FinaicalId",
                        column: x => x.FinaicalId,
                        principalTable: "FinaicalRequests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ContractPayment",
                columns: table => new
                {
                    ContractId = table.Column<int>(type: "int", nullable: false),
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractPayment", x => new { x.ContractId, x.PaymentId });
                    table.ForeignKey(
                        name: "FK_ContractPayment_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ContractPayment_Payment_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payment",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientContract_ClientId",
                table: "ClientContract",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractFinaical_FinaicalId",
                table: "ContractFinaical",
                column: "FinaicalId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractPayment_PaymentId",
                table: "ContractPayment",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_EngContract_EngId",
                table: "EngContract",
                column: "EngId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientContract");

            migrationBuilder.DropTable(
                name: "ContractFinaical");

            migrationBuilder.DropTable(
                name: "ContractPayment");

            migrationBuilder.DropTable(
                name: "EngContract");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "FinaicalRequests");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "Eng");
        }
    }
}
