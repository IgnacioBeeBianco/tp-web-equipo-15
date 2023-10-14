// Función para guardar la posición del scroll en sessionStorage
function guardarPosicionScroll() {
    var scrollY = window.scrollY || document.documentElement.scrollTop;
    sessionStorage.setItem("scrollPosition", scrollY);
}

// Función para restaurar la posición del scroll desde sessionStorage
function restaurarPosicionScroll() {
    var scrollY = parseInt(sessionStorage.getItem("scrollPosition"));
    if (!isNaN(scrollY)) {
        window.scrollTo(0, scrollY);
    }
}

// Registra un evento antes de la recarga de la página para guardar la posición del scroll
window.onbeforeunload = function () {
    guardarPosicionScroll();
}

// Registra un evento después de cargar la página para restaurar la posición del scroll
window.onload = function () {
    restaurarPosicionScroll();
}