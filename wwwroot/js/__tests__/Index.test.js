// Simple test case to check if the correct carousel item is marked as active
test('Check if the correct carousel item is marked as active', () => {
    // Sample HTML content for testing
    document.body.innerHTML = `
      <div class="carousel-item">Item 1</div>
      <div class="carousel-item">Item 2</div>
      <div class="carousel-item">Item 3</div>
    `;
  
    // Define the showSlide function from the provided JavaScript file
    function showSlide(index) {
      const items = document.querySelectorAll('.carousel-item');
      items.forEach((item, i) => {
        item.classList.remove('active');
        if (i === index) {
          item.classList.add('active');
        }
      });
    }
  
    // Select the carousel items
    const items = document.querySelectorAll('.carousel-item');
  
    // Call the showSlide function with index 1
    showSlide(1);
  
    // Check if the correct carousel item is marked as active
    expect(items[0].classList.contains('active')).toBe(false);
    expect(items[1].classList.contains('active')).toBe(true);
    expect(items[2].classList.contains('active')).toBe(false);
  });
  