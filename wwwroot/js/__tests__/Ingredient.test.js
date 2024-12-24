// Simple test case to check if an element with a specific ID exists in the DOM
test('Check if #testElement exists in the DOM', () => {
    // Sample HTML content for testing
    document.body.innerHTML = `
      <div id="testElement">This is a test element</div>
    `;
  
    // Check if the div element with ID 'testElement' exists
    const element = document.getElementById('testElement');
    expect(element).not.toBeNull();
    expect(element.textContent).toBe('This is a test element');
  });
  
  test('Check if ul with specific li items exists in the DOM', () => {
    // Sample HTML content for testing
    document.body.innerHTML = `
      <ul>
        <li>Item 1</li>
        <li>Item 2</li>
        <li>Item 3</li>
      </ul>
    `;
  
    // Check if the ul element exists
    const ul = document.querySelector('ul');
    expect(ul).not.toBeNull();
  
    // Check if the ul contains the correct li items
    const liItems = ul.querySelectorAll('li');
    expect(liItems.length).toBe(3);
    expect(liItems[0].textContent).toBe('Item 1');
    expect(liItems[1].textContent).toBe('Item 2');
    expect(liItems[2].textContent).toBe('Item 3');
  });

  test('Check if input with placeholder exists in the DOM', () => {
    // Sample HTML content for testing
    document.body.innerHTML = `
      <input placeholder="Enter text">
    `;
  
    // Check if the input element exists
    const input = document.querySelector('input');
    expect(input).not.toBeNull();
    expect(input.placeholder).toBe('Enter text');
  });

  
  test('Check if button with text exists in the DOM', () => {
    // Sample HTML content for testing
    document.body.innerHTML = `
      <button>Click Me</button>
    `;
  
    // Check if the button exists
    const button = document.querySelector('button');
    expect(button).not.toBeNull();
    expect(button.textContent).toBe('Click Me');
  });
  
  test('Check if .element-class exists in the DOM', () => {
    // Sample HTML content for testing
    document.body.innerHTML = `
      <div class="element-class">Element with class</div>
    `;
  
    // Check if the element with class 'element-class' exists
    const element = document.querySelector('.element-class');
    expect(element).not.toBeNull();
    expect(element.textContent).toBe('Element with class');
  });

  
  test('Check if #element1 exists in the DOM', () => {
    // Sample HTML content for testing
    document.body.innerHTML = `
      <div id="element1">Element 1</div>
    `;
  
    // Check if the element with ID 'element1' exists
    const element = document.getElementById('element1');
    expect(element).not.toBeNull();
    expect(element.textContent).toBe('Element 1');
  });
  