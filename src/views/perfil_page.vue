<template>
	<comp-page :titulo="'Mi cuenta'">
		<ion-list>
			<ion-item>
				<ion-label>Modo oscuro</ion-label>
				<ion-toggle slot="end" :checked="tema_oscuro" @ion-change="cambiar_tema" />
			</ion-item>
		</ion-list>
	</comp-page>
</template>

<script>import { IonItem, IonLabel, IonList, IonToggle } from '@ionic/vue';
import comp_page from '@/components/estructura/comp_page.vue';
import { alternar_tema, es_tema_oscuro } from '@/config/tema';

export default {
	name: 'perfil_page',
	components: {
		CompPage: comp_page,
		IonItem,
		IonLabel,
		IonList,
		IonToggle
	},
	data(){
		return {
			tema_oscuro: es_tema_oscuro()
		};
	},
	methods: {
		// El toggle emite en el render inicial: solo se alterna si el valor cambio de verdad.
		cambiar_tema: function(evento){
			var vm = this;
			if(evento.detail.checked === vm.tema_oscuro) return;
			alternar_tema();
			vm.tema_oscuro = es_tema_oscuro();
		}
	}
};</script>