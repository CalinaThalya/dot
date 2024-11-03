using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEndDataTech.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarSenha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "senha",
                table: "tb_cliente",
                type: "NVARCHAR2(2000)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "senha",
                table: "tb_cliente");
        }
    }
}
