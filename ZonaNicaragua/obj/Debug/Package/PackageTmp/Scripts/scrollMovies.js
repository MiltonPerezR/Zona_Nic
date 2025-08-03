function interpolateColor(color1, color2, factor) {
    const c1 = color1.match(/\d+/g).map(Number);
    const c2 = color2.match(/\d+/g).map(Number);
    const result = c1.map((v, i) => Math.round(v + factor * (c2[i] - v)));
    return `rgb(${result[0]}, ${result[1]}, ${result[2]})`;
}

window.addEventListener("scroll", () => {
    const navbar = document.querySelector(".navbar");
    const body = document.body;

    if (window.scrollY > 50) {
        navbar.classList.add("bg-dark");
    } else {
        navbar.classList.remove("bg-dark");
    }

    const start = 200, end = 500;
    let scrollY = window.scrollY;
    let factor = scrollY <= start ? 0 : scrollY >= end ? 1 : (scrollY - start) / (end - start);

    const startColor = "rgb(30, 27, 27)";
    const endColor = "rgb(0, 0, 0)";
    const newColor = interpolateColor(startColor, endColor, factor);
    body.style.backgroundColor = newColor;
});

document.body.style.backgroundColor = "rgb(30, 27, 27)";
Sys.Application.add_load(function () {
    var contenido = document.querySelector('.banner-contenido');
    if (contenido) {
        contenido.classList.remove('banner-contenido');
        void contenido.offsetWidth;
        contenido.classList.add('banner-contenido');
    }
});