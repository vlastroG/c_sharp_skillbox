using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankSystem.Data.Migrations {
    /// <inheritdoc />
    public partial class AddBankAccounts : Migration {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.CreateTable(
                name: "BankAccountsDeposit",
                columns: table => new {
                    Number = table.Column<string>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    Money = table.Column<decimal>(type: "TEXT", nullable: false),
                    ClientWithGeneralAccountId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClientWithDepositAccountId = table.Column<int>(type: "INTEGER", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_BankAccountsDeposit", x => x.Number);
                    table.ForeignKey(
                        name: "FK_BankAccountsDeposit_Clients_ClientWithDepositAccountId",
                        column: x => x.ClientWithDepositAccountId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    //table.ForeignKey(
                    //    name: "FK_BankAccountsDeposit_Clients_ClientWithGeneralAccountId",
                    //    column: x => x.ClientWithGeneralAccountId,
                    //    principalTable: "Clients",
                    //    principalColumn: "Id",
                    //    onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankAccountsGeneral",
                columns: table => new {
                    Number = table.Column<string>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    Money = table.Column<decimal>(type: "TEXT", nullable: false),
                    ClientWithGeneralAccountId = table.Column<int>(type: "INTEGER", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_BankAccountsGeneral", x => x.Number);
                    table.ForeignKey(
                        name: "FK_BankAccountsGeneral_Clients_ClientWithGeneralAccountId",
                        column: x => x.ClientWithGeneralAccountId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccountsDeposit_ClientWithDepositAccountId",
                table: "BankAccountsDeposit",
                column: "ClientWithDepositAccountId",
                unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_BankAccountsDeposit_ClientWithGeneralAccountId",
            //    table: "BankAccountsDeposit",
            //    column: "ClientWithGeneralAccountId",
            //    unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BankAccountsGeneral_ClientWithGeneralAccountId",
                table: "BankAccountsGeneral",
                column: "ClientWithGeneralAccountId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropTable(
                name: "BankAccountsDeposit");

            migrationBuilder.DropTable(
                name: "BankAccountsGeneral");
        }
    }
}
