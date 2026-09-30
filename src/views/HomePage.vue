<template>
  <ion-page>
    <ion-header :translucent="true">
      <ion-toolbar>
        <ion-title>Fernando Ortiz</ion-title>
        <!-- Botón en la barra superior que dispara el cambio de tema -->
        <ion-buttons slot="end">
          <ion-button @click="toggleTema">
            <ion-icon :icon="contrastOutline"></ion-icon>
          </ion-button>
        </ion-buttons>
      </ion-toolbar>
    </ion-header>

    <ion-content :fullscreen="true" class="ion-padding">
      <ion-header collapse="condense">
        <ion-toolbar>
          <ion-title size="large">Fibrered</ion-title>
        </ion-toolbar>
      </ion-header>

      <!-- Título principal centrado con subtítulo -->
      <div class="header-container">
        <h2 class="main-title">Cooperativa Telefonica M</h2>
        <p class="main-subtitle">Planes de internet a tu medida</p>
      </div>

      <!-- Listado de tarjetas de internet -->
      <ion-card v-for="s in servicios" :key="s.titulo">
        <ion-card-header>
          <ion-card-title>{{ s.titulo }}</ion-card-title>
          <ion-card-subtitle>{{ s.precio }}</ion-card-subtitle>
        </ion-card-header>

        <ion-card-content>
          {{ s.detalle }}
        </ion-card-content>
      </ion-card>
    </ion-content>
  </ion-page>
</template>

<script>
import { contrastOutline } from 'ionicons/icons';

const toggleTema = () => {
  document.documentElement.classList.toggle('ion-palette-dark');
  document.body.classList.toggle('dark');
};

const servicios = [
  {
    titulo: 'Plan Fibra 100 Megas',
    precio: '$30.000 / mes',
    detalle: 'Ideal para navegación básica, redes sociales, clases virtuales y streaming en HD para 2 a 3 dispositivos.'
  },
  {
    titulo: 'Plan Fibra 300 Megas',
    precio: '$40.000 / mes',
    detalle: 'Excelente rendimiento para familias. Permite juegos en línea sin lag, películas en 4K y múltiples dispositivos conectados simultáneamente.'
  },
  {
    titulo: 'Plan Fibra 600 Megas',
    precio: '$50.000 / mes',
    detalle: 'Máxima velocidad simétrica para usuarios exigentes, home office pesado, descarga de archivos grandes y streaming en Ultra HD sin interrupciones.'
  }
];
</script>

<style scoped>
.header-container {
  text-align: center;
  margin-top: 16px;
  margin-bottom: 24px;
}

.main-title {
  font-size: 22px;
  font-weight: bold;
  margin: 0 0 6px 0;
  color: var(--ion-color-dark);
}

.main-subtitle {
  font-size: 14px;
  color: var(--ion-color-medium);
  margin: 0;
}

ion-card {
  margin-bottom: 16px;
}
</style>
