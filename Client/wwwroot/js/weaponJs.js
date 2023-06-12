/*$.ajax({
    url: "https://valorant-api.com/v1/weapons/",
    dataType: "json",
}).done((result) => {
    let table = "";
    $.each(result.data, (index, weapon) => {
        index += 1
        table += "<tr>";
        table += "<td>" + index + "</td>";
        table += "<td>" + weapon.displayName + "</td>";
        table += "<td><button class='btn btn-primary' onclick='showWeaponDetails(" + JSON.stringify(weapon) + ")'>Details</button></td>";;
        table += "</tr>";
    });
    console.log(table);
    $("#valo").html(table);
}).fail((error) => {
    console.log(error);
});

function showWeaponDetails(weapon) {
    let details = "";
    details += "<img src='" + weapon.displayIcon + "' alt='" + weapon.displayIcon + "'>";
    details += "<p><strong>Category : </strong>" + weapon.shopData.categoryText + "</p>";
    details += "<p><strong>Cost: </strong><span class='badge bg-primary'>" + weapon.shopData.cost + "</span></p>";
    
    $("#agentDetails").html(details);
    $("#agentModal .modal-title").text(weapon.displayName);
    $("#agentModal").modal("show");
}*/
/*
$(document).ready(function () {

    function loadWeaponModal(weapon) {
        $("#weaponImage").attr("src", weapon.displayIcon);
        $("#weaponModalLabel").text(weapon.displayName);
        $("#weaponType").text("Type : " + weapon.shopData.category);
        $("#weaponFireRate").text("Fire Rate : " + weapon.weaponStats.fireRate);
        $("#weaponMagazine").text("Magazine : " + weapon.weaponStats.magazineSize);
        $("#weaponModal").modal("show");
    }

    $.ajax({
        url: "https://valorant-api.com/v1/weapons",
        type: "GET"
    })
        .done(function (response) {
            var weapons = response.data;

            $.each(weapons, function (index, weapon) {
                var row = $("<tr></tr>");
                var numberCell = $("<td></td>").text(index + 1);
                row.append(numberCell);

                var displayNameCell = $("<td></td>").text(weapon.displayName);
                row.append(displayNameCell);

                var modalButtonCell = $("<td></td>");
                var modalButton = $("<button></button>")
                    .text("View")
                    .addClass("btn btn-primary btn-sm")
                    .click((function (w) {
                        return function () {
                            loadWeaponModal(w);
                        };
                    })(weapon));
                modalButtonCell.append(modalButton);
                row.append(modalButtonCell);

                $("#valo").append(row);
            });
        })
        .fail(function () {
            alert("Error retrieving weapon data.");
        });
});*/


$(document).ready(function () {

    function loadWeaponModal(weapon) {
        $("#weaponImage").attr("src", weapon.displayIcon);
        $("#weaponModalLabel").text(weapon.displayName);
        $("#weaponType").text("Type: " + weapon.shopData.category);
        $("#weaponFireRate").text("Fire Rate: " + weapon.weaponStats.fireRate);
        $("#weaponMagazine").text("Magazine: " + weapon.weaponStats.magazineSize);

        $("#weaponChart").remove();
        $("#chartContainer").append('<canvas id="weaponChart"></canvas>');

        const labels = ['Head', 'Body', 'Leg'];
        const datasets = [];

        $.each(weapon.weaponStats.damageRanges, function (range) {
            const rangeData = [];
            $.each(labels, function (label) {
                rangeData.push(range[label]);
            });

            datasets.push({
                label: `Range ${range.rangeStartMeters}-${range.rangeEndMeters}`,
                data: rangeData,
                backgroundColor: 'rgba(0, 123, 255, 0.5)',
                borderColor: 'rgba(0, 123, 255, 1)',
                borderWidth: 2,
                pointBackgroundColor: 'rgba(0, 123, 255, 1)',
                pointBorderColor: '#fff',
                pointRadius: 4
            });
        });

        const ctx = document.getElementById('weaponChart').getContext('2d');
        new Chart(ctx, {
            type: 'radar',
            data: {
                labels: labels,
                datasets: datasets
            },
            options: {
                scale: {
                    ticks: {
                        beginAtZero: true

                    }
                }
            }
        });

        $("#weaponModal").modal("show");
    }

    

    $.ajax({
        url: "https://valorant-api.com/v1/weapons",
        type: "GET"
    })
        .done(function (response) {
            var weapons = response.data;

            $.each(weapons, function (index, weapon) {
                var row = $("<tr></tr>");
                var numberCell = $("<td></td>").text(index + 1);
                row.append(numberCell);

                var displayNameCell = $("<td></td>").text(weapon.displayName);
                row.append(displayNameCell);

                var modalButtonCell = $("<td></td>");
                var modalButton = $("<button></button>")
                    .text("View")
                    .addClass("btn btn-primary btn-sm")
                    .click((function (w) {
                        return function () {
                            loadWeaponModal(w);
                        };
                    })(weapon));
                modalButtonCell.append(modalButton);
                row.append(modalButtonCell);

                $("#valo").append(row);
            });
        })
        .fail(function () {
            alert("Error retrieving weapon data.");
        });
});


