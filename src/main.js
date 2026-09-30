import { createApp } from 'vue';
import { IonicVue } from '@ionic/vue';
import App from './App.vue';
import router from './router';
import { aplicar_tema_guardado } from './config/tema';
import '@ionic/vue/css/core.css';
import '@ionic/vue/css/normalize.css';
import '@ionic/vue/css/structure.css';
import '@ionic/vue/css/typography.css';
import '@ionic/vue/css/padding.css';
import '@ionic/vue/css/float-elements.css';
import '@ionic/vue/css/text-alignment.css';
import '@ionic/vue/css/text-transformation.css';
import '@ionic/vue/css/flex-utils.css';
import '@ionic/vue/css/display.css';
/* El modo oscuro pasa de dark.system.css a dark.class.css: ya no lo decide el sistema
   operativo, lo decide una clase en <html> que controla el interruptor de Mi cuenta. */
import '@ionic/vue/css/palettes/dark.class.css';
import './theme/variables.css';

const app = createApp(App)
	.use(IonicVue)
	.use(router);

router.isReady().then(function(){
	aplicar_tema_guardado();
	app.mount('#app');
});