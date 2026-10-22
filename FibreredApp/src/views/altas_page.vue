<template>
	<comp-page :titulo="'Altas'" :mostrar_actualizar="true" @actualizar="cargar">
		<comp-esqueleto v-if="cargando" />
		<ion-list v-else>
			<ion-item v-for="alta in altas" :key="alta.id">
				<ion-label>
					<h3>{{ alta.codigo }}</h3>
					<p>{{ alta.cliente_nombre || 'Mostrador' }}</p>
					<p>{{ formatear_importe(alta.total) }} · Pago: {{ etiqueta_estado(alta.estado_pago) }}</p>
				</ion-label>
				<ion-badge slot="end" :color="color_estado(alta.estado)">{{ etiqueta_estado(alta.estado) }}</ion-badge>
			</ion-item>
		</ion-list>
	</comp-page>
</template>

<script>import { IonBadge, IonItem, IonLabel, IonList } from '@ionic/vue';
import comp_esqueleto from '@/components/base/comp_esqueleto.vue';
import comp_page from '@/components/estructura/comp_page.vue';
import { COLOR_ESTADO_ALTAS, etiqueta_estado, obtener_altas } from '@/datos/altas';

export default {
	name: 'altas_page',
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
			altas: []
		};
	},
	mounted(){
		var vm = this;
		vm.cargar();
	},
	methods: {
		cargar: async function(){
			var vm = this;
			vm.cargando = true;
			const respuesta = await obtener_altas();
			vm.altas = respuesta.altas;
			vm.cargando = false;
		},
		// Un estado desconocido no rompe la pantalla: cae en gris en vez de quedar sin color.
		color_estado: function(estado){
			return COLOR_ESTADO_ALTAS[estado] || 'medium';
		},
		// 52600 pasa a '$ 52.600': formato de moneda argentino, sin decimales.
		formatear_importe: function(valor){
			return new Intl.NumberFormat('es-AR', {style: 'currency', currency: 'ARS', maximumFractionDigits: 0}).format(valor || 0);
		},
		etiqueta_estado
	}
};</script>