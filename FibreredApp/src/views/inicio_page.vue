<template>
	<comp-page :titulo="'Fibrered'">
		<!-- La tarjeta de la version 1, tal cual era. Antes vivia sola en HomePage.vue con su
			propio ion-page y header; ahora eso lo pone comp-page. -->
		<ion-card>
			<ion-card-header>
				<ion-card-title>Conexion de alta velocidad</ion-card-title>
				<ion-card-subtitle>Av.Norte 2000 - Merlo , San Luis</ion-card-subtitle>
			</ion-card-header>
			<ion-card-content>
				Los mejores planes de internet, combinados con TV digital y Telefonia.
			</ion-card-content>
		</ion-card>

		<ion-card>
			<ion-card-header>
				<ion-card-title>Versión 2</ion-card-title>
			</ion-card-header>
			<ion-card-content>
				Las rutas, el menú lateral y los tabs salen todos del mismo archivo:
				<strong>src/config/navegacion.js</strong>. Agregar una pantalla es agregar un objeto ahí.
			</ion-card-content>
		</ion-card>
	</comp-page>
</template>

<script>import {
	IonCard,
	IonCardContent,
	IonCardHeader,
	IonCardSubtitle,
	IonCardTitle
} from '@ionic/vue';
import comp_page from '@/components/estructura/comp_page.vue';

export default {
	name: 'inicio_page',
	components: {
		CompPage: comp_page,
		IonCard,
		IonCardContent,
		IonCardHeader,
		IonCardSubtitle,
		IonCardTitle
	}
};</script>