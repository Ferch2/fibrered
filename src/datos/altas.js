// Datos de prueba de pedidos. Los estados van en snake_case, como los serializa un backend:
//   pedido -> confirmado, en_preparacion, listo, entregado, cancelado
//   pago   -> pendiente, pagado, anulado
// La lista cubre varios estados para que se vea que cada uno se pinta distinto.

export const ALTAS = [
	{id: 1, codigo: 'ALT-0001', cliente_nombre: 'Maria Quiros', estado: 'en_factibilidad', estado_pago: 'pendiente', total: 52600},
	{id: 2, codigo: 'ALT-0002', cliente_nombre: 'Juan Miranda', estado: 'para_instalar', estado_pago: 'pagado', total: 36000},
	// Sin cliente: una venta de mostrador no siempre tiene a quien asociarse.
	{id: 3, codigo: 'ALT-0003', cliente_nombre: 'Adriana Campos', estado: 'instalado', estado_pago: 'pagado', total: 6500},
	{id: 4, codigo: 'ALT-0004', cliente_nombre: 'Ricardo Roldan', estado: 'cancelado', estado_pago: 'anulado', total: 9500}
];

// Cada estado con su color de Ionic. Vive con los datos porque lo van a usar varias pantallas.
export const COLOR_ESTADO_ALTAS = {
	confirmado: 'primary',
	en_factibilidad: 'warning',
	para_instalar: 'success',
	instalado: 'success',
	cancelado: 'danger'
};

// De snake_case a texto legible, en un solo lugar.
export function etiqueta_estado(estado){
	if(!estado) return '';
	return estado
		// 'en_preparacion' pasa a 'en preparacion': los guiones bajos se vuelven espacios.
		.replace(/_/g, ' ')
		// 'en preparacion' pasa a 'En preparacion': la primera letra a mayuscula.
		.replace(/^./, letra => letra.toUpperCase());
}

// Retardo para que el esqueleto de carga se vea, como va a pasar con la API real.
const RETARDO_SIMULADO = 600;

export function obtener_altas(){
	return new Promise(resolver =>{
		// Resuelve con los datos recien despues del retardo.
		window.setTimeout(() => resolver({altas: ALTAS}), RETARDO_SIMULADO);
	});
}