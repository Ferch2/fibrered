// El tema de la app: una clase en <html> y una clave en localStorage, nada mas.
// RavEat resuelve esto con un store de Pinia porque ya lo traia desde su version 1; aca
// todavia no hay estado compartido que lo justifique, y la regla de esta version es no
// agregar dependencias si el problema se resuelve con lo que ya esta.
const CLAVE_TEMA = 'fibrered_tema';

export function es_tema_oscuro(){
	return localStorage.getItem(CLAVE_TEMA) !== 'claro';
}

// El modo oscuro de Ionic es la clase .ion-palette-dark en <html> (palettes/dark.class.css).
// Ponerla o sacarla es todo lo que hace falta para cambiar el tema entero.
export function aplicar_tema_guardado(){
	document.documentElement.classList.toggle('ion-palette-dark', es_tema_oscuro());
}

export function alternar_tema(){
	localStorage.setItem(CLAVE_TEMA, es_tema_oscuro() ? 'claro' : 'oscuro');
	aplicar_tema_guardado();
}