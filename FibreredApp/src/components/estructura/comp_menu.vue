<template>
	<ion-menu :content-id="content_id" type="overlay">
		<ion-header>
			<ion-toolbar>
				<ion-title>Cooperativa Telefonica M</ion-title>
			</ion-toolbar>
		</ion-header>

		<ion-content>
			<ion-list v-for="grupo in grupos_menu" :key="grupo.id">
				<ion-list-header v-if="grupo.titulo">{{ grupo.titulo }}</ion-list-header>
				<ion-menu-toggle v-for="item in grupo.items" :key="item.id" :auto-hide="false">
					<ion-item button :detail="false" :router-link="item.ruta" router-direction="root">
						<ion-icon slot="start" :icon="item.icono" />
						<ion-label>{{ item.titulo }}</ion-label>
					</ion-item>
				</ion-menu-toggle>
			</ion-list>
		</ion-content>
	</ion-menu>
</template>

<script>import {
	IonContent,
	IonHeader,
	IonIcon,
	IonItem,
	IonLabel,
	IonList,
	IonListHeader,
	IonMenu,
	IonMenuToggle,
	IonTitle,
	IonToolbar
} from '@ionic/vue';
import { obtener_grupos_menu } from '@/config/navegacion';

export default {
	name: 'comp_menu',
	components: {
		IonContent,
		IonHeader,
		IonIcon,
		IonItem,
		IonLabel,
		IonList,
		IonListHeader,
		IonMenu,
		IonMenuToggle,
		IonTitle,
		IonToolbar
	},
	props: {
		content_id: {
			type: String,
			required: true
		}
	},
	computed: {
		grupos_menu(){
			return obtener_grupos_menu();
		}
	}
};</script>