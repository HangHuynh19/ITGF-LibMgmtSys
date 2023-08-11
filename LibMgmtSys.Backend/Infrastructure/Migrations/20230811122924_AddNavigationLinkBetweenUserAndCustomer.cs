using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNavigationLinkBetweenUserAndCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_customers_user_id",
                table: "customers",
                column: "user_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_customers_users_user_temp_id",
                table: "customers",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_customers_users_user_temp_id",
                table: "customers");

            migrationBuilder.DropIndex(
                name: "ix_customers_user_id",
                table: "customers");
        }
    }
}
