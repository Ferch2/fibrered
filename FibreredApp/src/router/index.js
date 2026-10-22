import { createRouter, createWebHistory } from '@ionic/vue-router';
import { navegacion } from '@/config/navegacion';
import main_layout from '@/layouts/main_layout.vue';

// Las rutas ya no se escriben: se derivan de la configuracion. Agregar una pantalla es
// agregar un objeto en navegacion.js, y el router, el menu y los tabs se enteran solos.
const rutas_app = navegacion.map(item => ({
	// '/app/productos' pasa a 'productos': las hijas del layout van con ruta relativa.
	path: item.ruta.replace('/app/', ''),
	name: item.id,
	component: item.componente
}));

const routes = [
	{
		path: '/',
		redirect: '/app/inicio'
	},
	{
		path: '/app',
		component: main_layout,
		children: [
			{
				path: '',
				redirect: '/app/inicio'
			},
			...rutas_app
		]
	},
	{
		path: '/:pathMatch(.*)*',
		redirect: '/app/inicio'
	}
];
const router = createRouter({
	history: createWebHistory(import.meta.env.BASE_URL),
	routes
});

export default router;
