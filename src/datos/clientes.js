// Datos de prueba de clientes, con la forma que despues va a devolver la API.

export const CLIENTES = [
	{id: 1, nombre: 'Maria Quiros', telefono: '266-455-1001', direccion: 'Av. delSol 1120, Merlo-San Luis'},
	{id: 2, nombre: 'Juan Miranda', telefono: '266-455-1002', direccion: 'Pta Aguero 770,Merlo-San Luis'},
	{id: 3, nombre: 'Adriana Campos', telefono: '266-455-1003', direccion: 'San Martin 1120,Carpinteria-San Luis'},
	// Sin direccion: los campos opcionales tienen que poder faltar y la pantalla verse bien igual.
	{id: 4, nombre: 'Ricardo Roldan', telefono: '266-455-1004', direccion: null}
];

// Retardo para que el esqueleto de carga se vea, como va a pasar con la API real.
const RETARDO_SIMULADO = 600;

export function obtener_clientes(){
	return new Promise(resolver =>{
		// Resuelve con los datos recien despues del retardo.
		window.setTimeout(() => resolver({clientes: CLIENTES}), RETARDO_SIMULADO);
	});
}