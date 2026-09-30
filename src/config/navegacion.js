import {
	homeOutline,
	peopleOutline,
	personCircleOutline,
	pricetagsOutline,
	receiptOutline
} from 'ionicons/icons';

// Fuente de verdad unica de la navegacion: de este archivo salen las rutas del router,
// los items del menu lateral y los tabs de abajo. Agregar una pantalla es agregar un objeto aca.

// Dos grupos: el operativo va sin encabezado y el de ajustes si lo lleva.
export const grupos_menu = [
	{
		id: 'operacion',
		titulo: '',
		orden: 10
	},
	{
		id: 'configuracion',
		titulo: 'Configuración',
		orden: 20
	}
];

export const navegacion = [
	{
		id: 'inicio',
		titulo: 'Inicio',
		ruta: '/app/inicio',
		icono: homeOutline,
		grupo_menu: 'operacion',
		orden: 10,
		componente: () => import('@/views/inicio_page.vue')
	},
	{
		id: 'planes',
		titulo: 'Planes',
		ruta: '/app/planes',
		icono: pricetagsOutline,
		grupo_menu: 'operacion',
		orden: 20,
		componente: () => import('@/views/planes_page.vue')
	},
	{
		id: 'clientes',
		titulo: 'Clientes',
		ruta: '/app/clientes',
		icono: peopleOutline,
		grupo_menu: 'operacion',
		orden: 30,
		componente: () => import('@/views/clientes_page.vue')
	},
	{
		id: 'altas',
		titulo: 'Altas',
		ruta: '/app/altas',
		icono: receiptOutline,
		grupo_menu: 'operacion',
		orden: 40,
		componente: () => import('@/views/altas_page.vue')
	},
	{
		id: 'perfil',
		titulo: 'Mi cuenta',
		ruta: '/app/perfil',
		icono: personCircleOutline,
		grupo_menu: 'configuracion',
		orden: 100,
		componente: () => import('@/views/perfil_page.vue')
	}
];

export function obtener_grupos_menu(){
	// Copia de la lista completa, ordenada por el campo orden (menor primero).
	const items = [...navegacion].sort((item_a, item_b) => item_a.orden - item_b.orden);
	// A cada grupo se le cuelgan sus pantallas: las que lo declaran en grupo_menu.
	let grupos = grupos_menu.map(grupo => ({
		...grupo,
		items: items.filter(item => item.grupo_menu === grupo.id)
	}));
	// Un grupo que quedo sin pantallas no se dibuja.
	grupos = grupos.filter(grupo => grupo.items.length > 0);
	// Los grupos tambien tienen su propio campo orden.
	grupos.sort((grupo_a, grupo_b) => grupo_a.orden - grupo_b.orden);
	return grupos;
}

export function obtener_tabs(){
	// La misma lista completa, ordenada por el campo orden.
	return [...navegacion].sort((item_a, item_b) => item_a.orden - item_b.orden);
}