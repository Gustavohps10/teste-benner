using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace microwave_benner.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class add_initial_heating_programs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "heatingPrograms",
                columns: new[] { "id", "custom", "food", "heatingChar", "instructions", "name", "power", "time" },
                values: new object[,]
                {
                    { 1, false, "Pipoca (de micro-ondas)", '*', "Observar o barulho de estouros do milho, caso houver um intervalo de mais de 10 segundos entre um estouro e outro, interrompa o aquecimento.", "Pipoca", 7, 180 },
                    { 2, false, "Leite", '#', "Cuidado com aquecimento de líquidos, o choque térmico aliado ao movimento do recipiente pode causar fervura imediata causando risco de queimaduras.", "Leite", 5, 300 },
                    { 3, false, "Carne em pedaço ou fatias", '=', "Interrompa o processo na metade e vire o conteúdo com a parte de baixo para cima para o descongelamento uniforme.", "Carnes de boi", 4, 840 },
                    { 4, false, "Frango (qualquer corte)", '%', "Interrompa o processo na metade e vire o conteúdo com a parte de baixo para cima para o descongelamento uniforme.", "Frango", 7, 480 },
                    { 5, false, "Feijão congelado", '$', "Deixe o recipiente destampado e em casos de plástico, cuidado ao retirar o recipiente pois o mesmo pode perder resistência em altas temperaturas.", "Feijão", 9, 480 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "heatingPrograms",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "heatingPrograms",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "heatingPrograms",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "heatingPrograms",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "heatingPrograms",
                keyColumn: "id",
                keyValue: 5);
        }
    }
}
