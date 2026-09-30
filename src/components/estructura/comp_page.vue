<template>
	<ion-page>
		<ion-header>
			<ion-toolbar>
				<ion-buttons slot="start">
					<ion-menu-button />
				</ion-buttons>
				<ion-title>{{ titulo }}</ion-title>
			</ion-toolbar>
		</ion-header>

		<ion-content :fullscreen="true">
			<ion-refresher v-if="mostrar_actualizar" slot="fixed" @ionRefresh="refrescar">
				<ion-refresher-content pulling-text="Desliza para actualizar" refreshing-text="Actualizando..." />
			</ion-refresher>
			<slot />
		</ion-content>
	</ion-page>
</template>

<script>import {
	IonButtons,
	IonContent,
	IonHeader,
	IonMenuButton,
	IonPage,
	IonRefresher,
	IonRefresherContent,
	IonTitle,
	IonToolbar
} from '@ionic/vue';

// El armazon que en la version 1 escribia a mano la unica pantalla: ion-page, header con el
// boton de menu y ion-content. Con cinco pantallas se escribe una sola vez, aca.
export default {
	name: 'comp_page',
	components: {
		IonButtons,
		IonContent,
		IonHeader,
		IonMenuButton,
		IonPage,
		IonRefresher,
		IonRefresherContent,
		IonTitle,
		IonToolbar
	},
	emits: [
		'actualizar'
	],
	props: {
		mostrar_actualizar: {
			type: Boolean,
			default: false
		},
		titulo: {
			type: String,
			required: true
		}
	},
	methods: {
		refrescar: function(evento){
			var vm = this;
			vm.$emit('actualizar');
			// El spinner se cierra a mano: sin datos que esperar, Ionic lo dejaria girando.
			window.setTimeout(() => evento.detail.complete(), 350);
		}
	}
};</script>