<template>
	<comp-page :titulo="'Clientes'" :mostrar_actualizar="true" @actualizar="cargar">
		<comp-esqueleto v-if="cargando" />
		<ion-list v-else>
			<ion-item v-for="cliente in clientes" :key="cliente.id">
				<ion-label>
					<h3>{{ cliente.nombre }}</h3>
					<p>{{ cliente.telefono }}</p>
					<p>{{ cliente.direccion || 'Sin dirección' }}</p>
				</ion-label>
			</ion-item>
		</ion-list>
	</comp-page>
</template>

<script>import { IonItem, IonLabel, IonList } from '@ionic/vue';
import comp_esqueleto from '@/components/base/comp_esqueleto.vue';
import comp_page from '@/components/estructura/comp_page.vue';
import { obtener_clientes } from '@/datos/clientes';

export default {
	name: 'clientes_page',
	components: {
		CompEsqueleto: comp_esqueleto,
		CompPage: comp_page,
		IonItem,
		IonLabel,
		IonList
	},
	data(){
		return {
			cargando: true,
			clientes: []
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
			const respuesta = await obtener_clientes();
			vm.clientes = respuesta.clientes;
			vm.cargando = false;
		}
	}
};</script>