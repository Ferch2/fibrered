using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fibrered.Api.Migrations
{
    // Etapa 2 — Datos de prueba: cuatro rubros y nueve productos. Los seis insumos son los que la
    // version 2 tenia inventados en src/datos/productos.js; los tres muebles se sumaron despues,
    // porque una carpinteria vende lo que fabrica. Va por migracion y no por un endpoint de siembra
    // porque asi la base queda igual en todas las maquinas con un solo comando.
    // Es la unica migracion escrita a mano: se genera vacia y solo inserta datos, no toca el esquema.
    public partial class DatosDePrueba : Migration
    {
        // Fecha fija: una migracion tiene que producir siempre el mismo resultado en cada maquina.
        private static readonly DateTimeOffset Semilla = new DateTimeOffset(2026, 8, 25, 0, 0, 0, TimeSpan.Zero);

        private static readonly string[] ColumnasCategoria = {
            "id", "nombre", "orden", "activo", "creado_en", "actualizado_en"
        };

        private static readonly string[] ColumnasProducto = {
            "id", "categoria_id", "nombre", "descripcion", "precio", "imagen_url",
            "disponible", "activo", "creado_en", "actualizado_en"
        };

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "categorias",
                columns: ColumnasCategoria,
                values: new object[,] {
                    {1L, "Maderas", 1, true, Semilla, Semilla},
                    {2L, "Placas",  2, true, Semilla, Semilla},
                    {3L, "Insumos", 3, true, Semilla, Semilla},
                    {4L, "Muebles", 4, true, Semilla, Semilla}
                });

            // imagen_url guarda una ruta relativa y nunca el host: guardar el host invalidaria todas
            // las imagenes al mover la API de maquina. Los archivos viven en wwwroot/seed/productos.
            migrationBuilder.InsertData(
                table: "productos",
                columns: ColumnasProducto,
                values: new object[,] {
                    {1L, 1L, "Tirante de pino 2\"x4\"", "Pino seco, largo 3.05 m.", 2800m, "seed/productos/tirante-pino.jpg", true, true, Semilla, Semilla},
                    {2L, 1L, "Tabla de pino cepillada 1\"x6\"", "Cepillada, lista para usar.", 2200m, "seed/productos/tabla-pino.jpg", true, true, Semilla, Semilla},
                    {3L, 1L, "Machimbre de pino 1/2\"", "Para cielorrasos y revestimientos.", 9500m, "seed/productos/machimbre-pino.jpg", true, true, Semilla, Semilla},
                    {4L, 2L, "Placa MDF 18 mm", "Placa cruda de 1.83 x 2.60 m.", 38000m, "seed/productos/placa-mdf.jpg", true, true, Semilla, Semilla},
                    // El unico no disponible: sirve para ver en pantalla que "dado de baja" y "hoy
                    // no hay para entregar" son dos cosas distintas.
                    {5L, 2L, "Fenólico 18 mm", "Placa fenólica para obra.", 52000m, "seed/productos/fenolico-18.jpg", false, true, Semilla, Semilla},
                    {6L, 3L, "Cola vinílica 1 kg", "Adhesivo de secado rápido.", 6500m, "seed/productos/cola-vinilica.jpg", true, true, Semilla, Semilla},
                    {7L, 4L, "Mesa de algarrobo 1,80 m", "Tapa maciza, patas de hierro.", 480000m, "seed/productos/mesa-algarrobo.jpg", true, true, Semilla, Semilla},
                    {8L, 4L, "Juego de cuatro sillas de pino", "Terminación al agua, apilables.", 260000m, "seed/productos/sillas-pino.jpg", true, true, Semilla, Semilla},
                    {9L, 4L, "Biblioteca de pino 5 estantes", "1,80 m de alto, a medida.", 210000m, "seed/productos/biblioteca-pino.jpg", true, true, Semilla, Semilla}
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Primero los productos: la clave foranea no deja borrar una categoria con productos.
            for(long id = 1; id <= 9; id++) migrationBuilder.DeleteData(table: "productos", keyColumn: "id", keyValue: id);
            for(long id = 1; id <= 4; id++) migrationBuilder.DeleteData(table: "categorias", keyColumn: "id", keyValue: id);
        }
    }
}