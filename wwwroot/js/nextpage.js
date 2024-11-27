var currentIndex = 0;
var items = document.querySelectorAll('.carousel-item');
var itemCount = items.length;

function showSlide(index) {
    items.forEach(function (item, i) {
        item.classList.remove('active');
        if (i === index) {
            item.classList.add('active');
        }
    });
}

function nextSlide() {
    currentIndex = (currentIndex + 1) % itemCount;
    showSlide(currentIndex);
}

function prevSlide() {
    currentIndex = (currentIndex - 1 + itemCount) % itemCount;
    showSlide(currentIndex);
}
