using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fibrered.Api.Migrations
{
    // Etapa 3 — Stock: la migracion del ejemplo en vivo. QUEDA SIN APLICAR a proposito.
    //
    // Los comandos de README.v3.md aplican hasta DatosDePrueba y paran ahi; esta se aplica en
    // clase, con la API levantada, para ver las tres cosas que hace una migracion:
    //   1. antes: GET /api/productos/stock corta con "Unknown column 'p.stock'" — el modelo tiene
    //      la propiedad y la base todavia no tiene la columna
    //   2. database update: agrega la columna y carga las cantidades
    //   3. database update DatosDePrueba: la borra y se vuelve al punto 1
    //
    // El AddColumn lo escribio EF Core comparando el modelo contra la migracion anterior; los
    // UpdateData se agregaron a mano, igual que en DatosDePrueba.
    public partial class StockDeProductos : Migration
    {
        // Piezas en deposito de cada producto de DatosDePrueba, en el mismo orden.
        private static readonly int[] Cantidades = {40, 25, 12, 8, 0, 60, 3, 8, 4};

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "stock",
                table: "productos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            // Sin esto la columna existe pero todos quedan en 0: agregar una columna y cargarle los
            // datos son dos cosas distintas, y la segunda no la escribe nadie por vos.
            for(var i = 0; i < Cantidades.Length; i++){
                migrationBuilder.UpdateData(
                    table: "productos",
                    keyColumn: "id",
                    keyValue: (long)(i + 1),
                    column: "stock",
                    value: Cantidades[i]);
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Al borrar la columna se van tambien las cantidades: por eso una migracion de esquema
            // que ademas carga datos no es reversible sin perder nada.
            migrationBuilder.DropColumn(
                name: "stock",
                table: "productos");
        }
    }
}