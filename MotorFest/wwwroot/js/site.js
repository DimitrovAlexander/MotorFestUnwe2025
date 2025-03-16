// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.
// Дефинирайте startFlyInAnimations като глобална функция
window.startFlyInAnimations = function () {
    console.log("Стартиране на Fly In анимацията..."); // Дебъг
    const elements = document.querySelectorAll(".fade-fly-in");
    console.log(`Намерени ${elements.length} елемента с клас fade-fly-in`); // Дебъг

    if (elements.length > 0) {
        gsap.to(elements, {
            duration: 1,
            opacity: 1,
            y: 0,
            stagger: 0.2,
            ease: "power2.out",
            onStart: function () {
                console.log("Fly In анимацията започна!"); // Дебъг
            }
        });
    } else {
        console.error("Няма намерени елементи с клас fade-fly-in!"); // Дебъг
    }
};

// Проверка за зареждане на DOM
document.addEventListener("DOMContentLoaded", function () {
    console.log("DOMContentLoaded event fired!"); // Дебъг

    var loadingScreen = document.getElementById("loading-screen");
    var fadeContainer = document.querySelector(".fade-container");

    if (loadingScreen) {
        console.log("Loading screen е намерен!"); // Дебъг
        setTimeout(function () {
            console.log("Добавяне на fade-out клас към loading screen..."); // Дебъг
            loadingScreen.classList.add("fade-out");
        }, 1500);
    } else {
        console.error("Loading screen не е намерен!"); // Дебъг
    }

    if (fadeContainer) {
        console.log("Fade container е намерен!"); // Дебъг
        fadeContainer.style.opacity = "1";
    } else {
        console.error("Fade container не е намерен!"); // Дебъг
    }
});

// Показване на toast съобщение при първо посещение
document.addEventListener("DOMContentLoaded", function () {
    if (!sessionStorage.getItem("site_visited")) {
        var toastEl = document.getElementById("welcomeToast");
        if (toastEl) {
            console.log("Toast елементът е намерен!"); // Дебъг
            var toast = new bootstrap.Toast(toastEl);
            toast.show();
            sessionStorage.setItem("site_visited", "true");
        } else {
            console.error("Toast елементът не е намерен!"); // Дебъг
        }
    }
});

// Показване на SVG елементите
document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll("svg").forEach(svg => {
        svg.style.visibility = "visible";
    });
});

// Ръчно извикване на startFlyInAnimations за тестване
document.addEventListener("DOMContentLoaded", function () {
    setTimeout(function () {
        console.log("Ръчно извикване на startFlyInAnimations за тестване..."); // Дебъг
        startFlyInAnimations();
    }, 1500); // Изчакайте 5 секунди, за да видите дали анимацията работи
});


/* Button Hover Effect
-------------------------------------------------------------------------- */
class HoverBtn {
    constructor(el) {
        this.bindAll();

        this.btn = el;
        this.txt = this.btn.querySelector(".js-button__text");
        this.hoverTxt = this.btn.querySelector(".js-button__hover");
        this.split1 = new SplitText(this.txt, { type: "chars, words" });
        this.split2 = new SplitText(this.hoverTxt, { type: "chars, words" });
        this.numChars1 = this.split1.chars.length;
        this.numChars2 = this.split2.chars.length;

        this.addListeners();

        for (var i = 0; i < this.numChars2; i++) {
            TweenMax.set(this.split2.chars[i], {
                y: 30 * Math.random()
            });
        }
    }

    bindAll() {
        const methods = ['mouseIn', 'mouseOut']

        for (let i = 0; i < methods.length; i++) {
            const fn = methods[i]
            this[fn] = this[fn].bind(this)
        }
    }

    mouseIn() {
        for (var i = 0; i < this.numChars1; i++) {
            TweenMax.to(this.split1.chars[i], 0.5, {
                y: -30 * Math.random()
            }, 0.01);
        }
        TweenMax.staggerTo(this.split2.chars, 0.5, {
            y: 0
        }, 0.01);
    }

    mouseOut() {
        TweenMax.staggerTo(this.split1.chars, 0.5, {
            y: 0
        }, 0.01);
        for (var i = 0; i < this.numChars2; i++) {
            TweenMax.to(this.split2.chars[i], 0.5, {
                y: 30 * Math.random()
            }, 0.01);
        }
    }

    addListeners() {
        this.btn.addEventListener("mouseenter", this.mouseIn)
        this.btn.addEventListener("mouseleave", this.mouseOut)
    }
}

    document.querySelectorAll('.js-button').forEach(el => {
        const hoverbtn = new HoverBtn(el)
    });
