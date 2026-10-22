<template>
	<comp-page :titulo="'Planes'" :mostrar_actualizar="true" @actualizar="cargar">
		<comp-esqueleto v-if="cargando" />
		<ion-list v-else>
			<ion-item v-for="plan in planes" :key="plan.id">
				<ion-label>
					<h3>{{ plan.nombre }}</h3>
					<p>{{ plan.descripcion }}</p>
					<p>{{ formatear_importe(plan.precio) }}</p>
				</ion-label>
				<ion-badge v-if="!plan.disponible" slot="end" color="medium">No disponible</ion-badge>
			</ion-item>
		</ion-list>
	</comp-page>
</template>

<script>import { IonBadge, IonItem, IonLabel, IonList } from '@ionic/vue';
import comp_esqueleto from '@/components/base/comp_esqueleto.vue';
import comp_page from '@/components/estructura/comp_page.vue';
import { obtener_planes } from '@/datos/planes';

export default {
	name: 'planes_page',
	components: {
		CompEsqueleto: comp_esqueleto,
		CompPage: comp_page,
		IonBadge,
		IonItem,
		IonLabel,
		IonList
	},
	data(){
		return {
			cargando: true,
			planes: []
		};
	},
	mounted(){
		var vm = this;
		vm.cargar();
	},
	methods: {
		// Los datos salen de un archivo, pero se piden async y con estado de carga, igual que
		// cuando vengan de la API. Asi la pantalla no cambia al conectarla.
		cargar: async function(){
			var vm = this;
			vm.cargando = true;
			const respuesta = await obtener_planes();
			vm.planes = respuesta.planes;
			vm.cargando = false;
		},
		// 52600 pasa a '$ 52.600': formato de moneda argentino, sin decimales.
		formatear_importe: function(valor){
			return new Intl.NumberFormat('es-AR', {style: 'currency', currency: 'ARS', maximumFractionDigits: 0}).format(valor || 0);
		}
	}
};</script> 