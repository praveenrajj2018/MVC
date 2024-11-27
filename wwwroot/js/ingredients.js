document.addEventListener('DOMContentLoaded', function() {
    var rows = document.querySelectorAll('.clickable-row');
    rows.forEach(row => {
        var cells = row.querySelectorAll('td');
        cells.forEach(cell => {
            if (!cell.querySelector('input[type="checkbox"]')) {
                cell.addEventListener('click', function() {
                    window.location = row.dataset.href;
                });
            }
        });
    });

    // Resizer functionality to adjust column width
    var thElements = document.querySelectorAll('th');
    thElements.forEach(function(th) {
        var resizer = document.createElement('div');
        resizer.classList.add('resizer');
        th.appendChild(resizer);
        resizer.addEventListener('mousedown', initResize);
    });

    function initResize(e) {
        var th = e.target.parentElement;
        var startX = e.clientX;
        var startWidth = th.clientWidth;

        document.addEventListener('mousemove', resizeColumn);
        document.addEventListener('mouseup', stopResize);

        function resizeColumn(e) {
            var newWidth = startWidth + (e.clientX - startX);
            th.style.width = newWidth + 'px';
            th.style.minWidth = newWidth + 'px'; // Ensure table layout consistency
        }

        function stopResize() {
            document.removeEventListener('mousemove', resizeColumn);
            document.removeEventListener('mouseup', stopResize);
        }
    }

    // Sorting functionality
    document.querySelector('th[data-sortable="true"]').addEventListener('click', function() {
        sortTable(0); // Sort by first column (Ingredient Name)
    });

    function sortTable(columnIndex) {
        var table = document.querySelector('.table tbody');
        var rows = Array.from(table.rows);
        var ascending = this.ascending || true;

        rows.sort(function(a, b) {
            var cellA = a.cells[columnIndex].textContent.trim();
            var cellB = b.cells[columnIndex].textContent.trim();

            if (cellA < cellB) return ascending ? -1 : 1;
            if (cellA > cellB) return ascending ? 1 : -1;
            return 0;
        });

        this.ascending = !ascending;

        rows.forEach(function(row) {
            table.appendChild(row);
        });
    }

    // Checkbox functionality
    var selectAllCheckbox = document.getElementById('select-all');
    var rowCheckboxes = document.querySelectorAll('.row-checkbox');
    var actionButtons = document.querySelector('.action-buttons');

    selectAllCheckbox.addEventListener('change', function() {
        rowCheckboxes.forEach(function(checkbox) {
            checkbox.checked = selectAllCheckbox.checked;
        });
        toggleActionButtons();
    });

    rowCheckboxes.forEach(function(checkbox) {
        checkbox.addEventListener('change', function() {
            toggleActionButtons();
        });
    });

    document.getElementById('cancel-button').addEventListener('click', function() {
        rowCheckboxes.forEach(function(checkbox) {
            checkbox.checked = false;
        });
        selectAllCheckbox.checked = false;
        actionButtons.style.display = 'none';
    });

    document.getElementById('delete-button').addEventListener('click', function() {
        var selectedIds = Array.from(rowCheckboxes)
                                .filter(checkbox => checkbox.checked)
                                .map(checkbox => checkbox.value);

        fetch('/Ingredients/DeleteIngredients', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
            },
            body: JSON.stringify(selectedIds)
        })
        .then(response => {
            if (response.ok) {
                selectedIds.forEach(id => {
                    var row = document.querySelector(`tr[data-id='${id}']`);
                    if (row) {
                        row.parentNode.removeChild(row);
                    }
                });
                actionButtons.style.display = 'none';
                selectAllCheckbox.checked = false;
            } else {
                alert('Failed to delete selected items.');
            }
        });
    });

    function toggleActionButtons() {
        var anyChecked = Array.from(rowCheckboxes).some(cb => cb.checked);
        actionButtons.style.display = anyChecked ? 'block' : 'none';
    }
});
