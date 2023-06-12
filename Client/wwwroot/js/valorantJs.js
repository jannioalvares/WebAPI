$.ajax({
    url: "https://valorant-api.com/v1/agents/",
    dataType: "json",
}).done((result) => {
    let table = "";
    let displayNameMap = {}; // Track display names
    

    $.each(result.data, (index, agent) => {
        if (!displayNameMap.hasOwnProperty(agent.displayName)) {
            displayNameMap[agent.displayName] = true; // Add display name to map
            index += 1
            table += "<tr>";
            table += "<td>" + index + "</td>";
            table += "<td><img width='30px' src=" + agent.displayIcon + " >" + agent.displayName + "</td>";

            let agentDetails = JSON.stringify(agent).replace(/'/g, "\\'");
            table += "<td><button class='btn btn-primary' onclick='showAgentDetails(" + agentDetails + ")'>Details</button></td>";
            table += "</tr>";
        }
    });

    console.log(agentDetails);
    $("#valo").html(table);
}).fail((error) => {
    console.log(error);
});


function showAgentDetails(agent) {
    let details = "";
    details += "<img id='imgvalo' src='" + (agent.fullPortrait || agent.displayIcon) + "' class='center' alt='" + agent.displayName + "'>";
    details += "<div class='cnt text-center'>"
    details += "<span class='badge " + getRoleClass(agent.role.displayName) + "'>" + agent.role.displayName + "</span>";
    details += "</div>";
    details += "<p style='text-align:center'><strong>Description</strong>";
    details += "<p class='text-justify'>"+ agent.description + "</p>";
    details += "<p style='text-align:center'><strong>Abillities</strong></p>";
    details += "<div id='abilitiesCarousel' class='carousel slide' data-bs-ride='carousel'>";
    details += "<div class='carousel-inner'>";
    

    $.each(agent.abilities, (index, ability) => {
        let activeClass = index === 0 ? "active" : "";
        details += "<div class='carousel-item " + activeClass + "'>";
        details += "<img src='" + ability.displayIcon + "' alt='" + agent.displayName + "'>";
        details += "<p class='text-justify'>" + ability.description + "</p>";
        details += "<h6>- " + ability.displayName + " -</h6>";
        details += "</div>";
    });

    details += "</div>";
    details += "<button class='carousel-control-prev' type='button' data-bs-target='#abilitiesCarousel' data-bs-slide='prev'>";
    details += "<span class='carousel-control-prev-icon' aria-hidden='true'></span>";
    details += "<span class='visually-hidden'>Previous</span>";
    details += "</button>";
    details += "<button class='carousel-control-next' type='button' data-bs-target='#abilitiesCarousel' data-bs-slide='next'>";
    details += "<span class='carousel-control-next-icon' aria-hidden='true'></span>";
    details += "<span class='visually-hidden'>Next</span>";
    details += "</button>";
    details += "</div>";
    $("#agentDetails").html(details);
    $("#agentModal .modal-title").text(agent.displayName);
    $("#agentModal").modal("show");
}

function getRoleClass(role) {
    let roleClass = "";
    switch (role) {
        case "Duelist":
            roleClass = "badge-primary";
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