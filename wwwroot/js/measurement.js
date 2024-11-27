// measurement.js

document.addEventListener('DOMContentLoaded', function() {
    var addMeasurementModal = new bootstrap.Modal(document.getElementById('addMeasurementModal'));
    var updateMeasurementModal = new bootstrap.Modal(document.getElementById('updateMeasurementModal'));

    document.getElementById('openModalBtn').addEventListener('click', function() {
        var form = document.getElementById('addMeasurementForm');
        form.reset();
        addMeasurementModal.show();
    });

    // Cancel button for Add Modal
    document.getElementById('cancelBtn').addEventListener('click', function() {
        addMeasurementModal.hide();
    });

    document.getElementById('addMeasurementForm').addEventListener('submit', function(event) {
        event.preventDefault();

        var formData = new FormData(this);
        var data = {};
        formData.forEach((value, key) => { data[key] = value; });

        fetch('@Url.Action("AddMeasurement", "Measurements")', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                var newRow = `<tr data-id="${data.measurement.id}">
                                <td><a href="javascript:void(0)" class="measurement-link" data-id="${data.measurement.id}">${data.measurement.measurementName}</a></td>
                                <td><input type="text" class="update-input" value="${data.measurement.weight}"></td>
                                <td><input type="text" class="update-input" value="${data.measurement.oG_L}"></td>
                                <td><input type="text" class="update-input" value="${data.measurement.source}"></td>
                              </tr>`;
                document.getElementById('measurementsTable').innerHTML += newRow;
                addMeasurementModal.hide();
                attachInputChangeHandler(); 
                attachMeasurementLinkClickHandlers(); 
            } else {
                alert(data.message);
            }
        })
        .catch(error => {
            console.error('Error:', error);
        });
    });

    function attachInputChangeHandler() {
        document.querySelectorAll('.update-input').forEach(input => {
            input.addEventListener('change', function() {
                document.getElementById('updateAllBtn').style.display = 'block';
            });
        });
    }

    attachInputChangeHandler(); 

    function attachMeasurementLinkClickHandlers() {
        document.querySelectorAll('.measurement-link').forEach(link => {
            link.addEventListener('click', function(event) {
                var measurementId = this.getAttribute('data-id');

                fetch(`@Url.Action("GetMeasurement", "Measurements")/${measurementId}`)
                .then(response => response.json())
                .then(data => {
                    if (data.success) {
                        var form = document.getElementById('updateMeasurementForm');
                        form.reset();

                        document.getElementById('updateMeasurementId').value = data.measurement.id;
                        document.getElementById('updateMeasurementName').value = data.measurement.measurementName;
                        document.getElementById('updateWeight').value = data.measurement.weight;
                        document.getElementById('updateOg_l').value = data.measurement.oG_L;
                        document.getElementById('updateSource').value = data.measurement.source;

                        updateMeasurementModal.show();
                    } else {
                        alert(data.message);
                    }
                })
                .catch(error => {
                    console.error('Error:', error);
                });
            });
        });
    }

    attachMeasurementLinkClickHandlers(); 

    document.getElementById('updateMeasurementForm').addEventListener('submit', function(event) {
        event.preventDefault();

        var formData = new FormData(this);
        var data = {};
        formData.forEach((value, key) => { data[key] = value; });

        fetch('@Url.Action("UpdateMeasurement", "Measurements")', {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                var row = document.querySelector(`.measurement-link[data-id="${data.measurement.id}"]`).closest('tr');
                row.querySelector('td:nth-child(1)').innerHTML = `<a href="javascript:void(0)" class="measurement-link" data-id="${data.measurement.id}">${data.measurement.measurementName}</a>`;
                row.querySelector('td:nth-child(2) input').value = data.measurement.weight;
                row.querySelector('td:nth-child(3) input').value = data.measurement.oG_L;
                row.querySelector('td:nth-child(4) input').value = data.measurement.source;

                updateMeasurementModal.hide();
                attachMeasurementLinkClickHandlers(); 
            } else {
                alert(data.message);
            }
        })
        .catch(error => {
            console.error('Error:', error);
        });
    });

    document.getElementById('deleteBtn').addEventListener('click', function() {
        var measurementId = document.getElementById('updateMeasurementId').value;

        fetch(`@Url.Action("DeleteMeasurement", "Measurements")/${measurementId}`, {
            method: 'DELETE'
        })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                // Remove the row from the table
                var row = document.querySelector(`.measurement-link[data-id="${measurementId}"]`).closest('tr');
                row.remove();

                updateMeasurementModal.hide();
            } else {
                alert(data.message);
            }
        })
        .catch(error => {
            console.error('Error:', error);
        });
    });

    document.getElementById('updateCancelBtn').addEventListener('click', function() {
        updateMeasurementModal.hide();
    });

    document.getElementById('updateAllBtn').addEventListener('click', function() {
        var updatedData = [];
        document.querySelectorAll('#measurementsTable tr').forEach(row => {
            var id = row.getAttribute('data-id');
            var measurementName = row.querySelector('td:nth-child(1) a').textContent;
            var weight = row.querySelector('td:nth-child(2) input').value;
            var og_l = row.querySelector('td:nth-child(3) input').value;
            var source = row.querySelector('td:nth-child(4) input').value;

            updatedData.push({
                Id: id,
                MeasurementName: measurementName,
                Weight: weight,
                OG_L: og_l,
                Source: source
            });
        });

        fetch('@Url.Action("UpdateAllMeasurements", "Measurements")', {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(updatedData)
        })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                alert('Measurements updated successfully.');
                document.getElementById('updateAllBtn').style.display = 'none';
            } else {
                alert(data.message);
            }
        })
        .catch(error => {
            console.error('Error:', error);
        });
    });
});