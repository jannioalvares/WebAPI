$(document).ready(function () {

    function loadAgentModal(agent) {
        
        $("#agentBg").attr("src", agent.background);
        $("#agentImage").attr("src", agent.fullPortrait || agent.displayIcon);
        $("#agentModalLabel").text(agent.displayName);
        $("#agentDesc").text(agent.description).addClass("text-justify");
        $("#agentRole").removeClass().text(agent.role.displayName).addClass("badge "+getRoleClass(agent.role.displayName));

        $("#agentModal").modal("show");
    }

    function loadAbilitiesCarousel(abilities) {
        var carousel = $("#abilitiesCarousel");
        carousel.empty();

        var carouselItems = "";

        $.each(abilities, function (index, ability) {
            var activeClass = index === 0 ? "active" : "";

            var carouselItem = "<div class='carousel-item " + activeClass + "'>";
            carouselItem += "<img src='" + ability.displayIcon + "' alt='" + ability.displayName + "'>";
            carouselItem += "<hr>"
            carouselItem += "<p class='text-justify'>" + ability.description + "</p>";
            carouselItem += "<h6>- " + ability.displayName + " -</h6>";
            carouselItem += "</div>";

            carouselItems += carouselItem;
        });

        var carouselControls = "<button class='carousel-control-prev' type='button' data-bs-target='#abilitiesCarousel' data-bs-slide='prev'>";
        carouselControls += "<span class='carousel-control-prev-icon' aria-hidden='true'></span>";
        carouselControls += "<span class='visually-hidden'>Previous</span>";
        carouselControls += "</button>";
        carouselControls += "<button class='carousel-control-next' type='button' data-bs-target='#abilitiesCarousel' data-bs-slide='next'>";
        carouselControls += "<span class='carousel-control-next-icon' aria-hidden='true'></span>";
        carouselControls += "<span class='visually-hidden'>Next</span>";
        carouselControls += "</button>";

        var carouselInner = "<div class='carousel-inner'>" + carouselItems + "</div>";
        var carouselContent = carouselInner + carouselControls;

        carousel.append(carouselContent);
    }

    function getRoleClass(role) {
        let roleClass = "";
        switch (role) {
            case "Duelist":
                roleClass = "badge-danger";
                break;
            case "Initiator":
                roleClass = "badge-success";
                break;
            case "Controller":
                roleClass = "badge-info";
                break;
            case "Sentinel":
                roleClass = "badge-warning";
                break;
            default:
                roleClass = "badge-secondary";
                break;
        }
        return roleClass;
    }

    function validateForm() {
        var form = document.getElementById("myForm");
        if (form.checkValidity() === false) {
            event.preventDefault();
            event.stopPropagation();
            form.classList.add('was-validated');
            return false;
        }
        return true;
    }

    $("#submitFormBtn").click(function () {
        if (validateForm()) {
            console.log("Form submitted successfully");
            $("#formModal").modal("hide");
            location.reload();
        }
    });

    $.ajax({
        url: "https://valorant-api.com/v1/agents/",
        type: "GET"
    }).done(function (response) {
            var agents = response.data;
            var numb = 0;

            var tableHead = $("<thead></thead>");
            var headRow = $("<tr><th>No</th><th>Icon</th><th>Name</th><th>Detail</th></tr>");
            tableHead.append(headRow);
            $("#valoTb").append(tableHead);

            $.each(agents, function (index, agent) {
                if (agent.isPlayableCharacter == true) {
                    var row = $("<tr></tr>");
                    var numberCell = $("<td></td>").text(numb+1);
                    row.append(numberCell);

                    var imageElement = $("<img>").attr("src", agent.killfeedPortrait).css({
                        width: "auto",
                        height: "30px",
                        "padding-top": "5px",
                        "padding-left": "0px"
                    });
                    var columnCell = $("<td></td>").append(imageElement);
                    row.append(columnCell);

                    var displayNameCell = $("<td></td>").text(agent.displayName);
                    row.append(displayNameCell);

                    var modalButtonCell = $("<td></td>");
                    var modalButton = $("<button></button>")
                        .text("Detail")
                        .addClass("btn btn-primary btn-sm")
                        .click((function (w) {
                            return function () {
                                loadAgentModal(w);
                                loadAbilitiesCarousel(w.abilities);
                            };
                        })(agent));
                    modalButtonCell.append(modalButton);
                    row.append(modalButtonCell);

                    $("#valo").append(row);
                    numb++;
                }
            });

            $("#valoTb").DataTable({
                dom: 'Bfrtip',
                buttons: [
                    'copy', 'csv', 'excel', 'pdf', 'print'
                ]
            });

       }).fail(function () {
            alert("Error retrieving weapon data.");
    });
});