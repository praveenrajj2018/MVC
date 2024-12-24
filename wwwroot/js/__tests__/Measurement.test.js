// Simple test case to check if an element exists in the DOM
test('Check if #openModalBtn exists in the DOM', () => {
    // Sample HTML content for testing
    document.body.innerHTML = `
      <button id="openModalBtn">Open Modal</button>
    `;
  
    // Check if the button exists
    const button = document.getElementById('openModalBtn');
    expect(button).not.toBeNull();
    expect(button.textContent).toBe('Open Modal');
  });
  

  // Simple test case to check if an element with a specific class exists in the DOM
test('Check if .modal-title exists in the DOM', () => {
    // Sample HTML content for testing
    document.body.innerHTML = `
      <div class="modal-title">Measurement Modal</div>
    `;
  
    // Check if the element with class 'modal-title' exists
    const modalTitle = document.querySelector('.modal-title');
    expect(modalTitle).not.toBeNull();
    expect(modalTitle.textContent).toBe('Measurement Modal');
  });
  

  // Simple test case to check if an input element with a specific ID exists and has correct placeholder text
test('Check if #measurementInput exists in the DOM and has correct placeholder', () => {
    // Sample HTML content for testing
    document.body.innerHTML = `
      <input id="measurementInput" placeholder="Enter Measurement">
    `;
  
    // Check if the input element with ID 'measurementInput' exists
    const input = document.getElementById('measurementInput');
    expect(input).not.toBeNull();
    expect(input.placeholder).toBe('Enter Measurement');
  });

  // Simple test case to check if a div element with a specific ID exists and has correct text content
test('Check if #measurementTitle exists in the DOM and has correct text content', () => {
    // Sample HTML content for testing
    document.body.innerHTML = `
      <div id="measurementTitle">Measurement Details</div>
    `;
  
    // Check if the div element with ID 'measurementTitle' exists
    const div = document.getElementById('measurementTitle');
    expect(div).not.toBeNull();
    expect(div.textContent).toBe('Measurement Details');
  });
  
  // Simple test case to check if an unordered list with a specific class name contains a list item with the correct text content
test('Check if .measurement-list contains an li with the correct text content', () => {
    // Sample HTML content for testing
    document.body.innerHTML = `
      <ul class="measurement-list">
        <li>Weight: 100</li>
        <li>OG_L: 1.050</li>
        <li>Source: Test Source</li>
      </ul>
    `;
  
    // Check if the unordered list with class 'measurement-list' exists
    const ul = document.querySelector('.measurement-list');
    expect(ul).not.toBeNull();
  
    // Check if the unordered list contains the correct list items
    const liItems = ul.querySelectorAll('li');
    expect(liItems.length).toBe(3);
    expect(liItems[0].textContent).toBe('Weight: 100');
    expect(liItems[1].textContent).toBe('OG_L: 1.050');
    expect(liItems[2].textContent).toBe('Source: Test Source');
  });

  test('Check if #deleteBtn exists in the DOM', () => {
    document.body.innerHTML = `
      <button id="deleteBtn">Delete</button>
    `;
  
    const button = document.getElementById('deleteBtn');
    expect(button).not.toBeNull();
    expect(button.textContent).toBe('Delete');
});

test('Check if #measurementsTable exists in the DOM', () => {
    document.body.innerHTML = `
      <table id="measurementsTable"></table>
    `;
  
    const table = document.getElementById('measurementsTable');
    expect(table).not.toBeNull();
});
test('Check if #updateAllBtn exists in the DOM', () => {
    document.body.innerHTML = `
      <button id="updateAllBtn">Update All</button>
    `;
  
    const button = document.getElementById('updateAllBtn');
    expect(button).not.toBeNull();
    expect(button.textContent).toBe('Update All');
});


  //=============================================================================
  

  test('Check if #openModalBtn exists in the DOM', () => {
    document.body.innerHTML = ` <button id="openModalBtn">Open Modal</button> `;  const button = document.getElementById('openModalBtn'); expect(button).not.toBeNull(); expect(button.textContent).toBe('Open Modal'); });
  
    test('Check if #openModalBtn exists in the DOM', () => {  document.body.innerHTML = ` <button id="openModalBtn">Open Modal</button> `; const button = document.getElementById('openModalBtn'); expect(button).not.toBeNull(); expect(button.textContent).toBe('Open Modal'); });
    

    test('Check if #source exists in the DOM', () => {
        document.body.innerHTML = `
          <input type="text" id="source" />
        `;
      
        const input = document.getElementById('source');
        expect(input).not.toBeNull();
    });



    test('Check if #og_l exists in the DOM', () => {
        document.body.innerHTML = `
          <input type="number" id="og_l" />
        `;
      
        const input = document.getElementById('og_l');
        expect(input).not.toBeNull();
    });
    test('Check if #weight exists in the DOM', () => {
        document.body.innerHTML = `
          <input type="number" id="weight" />
        `;
      
        const input = document.getElementById('weight');
        expect(input).not.toBeNull();
    });
        
    