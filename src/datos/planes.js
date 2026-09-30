// Datos de prueba de planes. Mientras no exista la API, esta es la fuente.
// Los campos van en snake_case, como los va a devolver el backend: pasar a la API
// despues es cambiar de donde sale el dato y nada mas.

export const PLANES = [
	{id: 1, nombre: 'Internet 300 MB', descripcion: 'Varios dispositivos en simultaneo', precio: 35000, disponible: true},
	{id: 2, nombre: 'Internet 500 MB', descripcion: 'Especial Gamer-Videos 4k.', precio: 45000, disponible: true},
	{id: 3, nombre: 'Internet 1 GB', descripcion: 'Plan ideal para "Pymes".', precio: 55000, disponible: true},
	{id: 4, nombre: 'Internet 300 MB + CATV + Telefono', descripcion: 'Plan para el hogar', precio: 70000, disponible: true},
	// El unico no disponible: sirve para ver que la pantalla contempla ese caso.
	{id: 5, nombre: 'Internet 1 GB-Simetrico', descripcion: 'Plan simetrico para uso profesional (aun en etapa de prueba)', precio: 80000, disponible: false},
	{id: 6, nombre: 'Internet 500 MB + CATV + TV Digital + IPTV (200 canales)', descripcion: 'El mejor plan para los hogares', precio: 80000, disponible: true}
];

// El retardo no es decorativo: sin el, los datos llegan en el mismo tick y el esqueleto de
// carga no se ve nunca. Con la API real la espera existe de verdad.
const RETARDO_SIMULADO = 600;

export function obtener_planes(){
	return new Promise(resolver =>{
		// Resuelve con los datos recien despues del retardo.
		window.setTimeout(() => resolver({planes: PLANES}), RETARDO_SIMULADO);
	});
}