<template>
	<ion-list>
		<ion-item v-for="fila in filas" :key="fila">
			<ion-label>
				<h3><ion-skeleton-text :animated="true" style="width: 55%" /></h3>
				<p><ion-skeleton-text :animated="true" style="width: 80%" /></p>
			</ion-label>
		</ion-item>
	</ion-list>
</template>

<script>import {
	IonItem,
	IonLabel,
	IonList,
	IonSkeletonText
} from '@ionic/vue';

export default {
	name: 'comp_esqueleto',
	components: {
		IonItem,
		IonLabel,
		IonList,
		IonSkeletonText
	},
	props: {
		// Cuantas filas fantasma dibujar. Ocupan el lugar del contenido real para que la
		// pantalla no salte cuando llegan los datos.
		filas: {
			type: Number,
			default: 4
		}
	}
};</script>