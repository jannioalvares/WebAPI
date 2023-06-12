$.ajax({
    url: "https://swapi.dev/api/people/",
    dataType: "json",
}).done((result) => {
    let table = "<tr><th>No.</th><th>Name</th><th>Height</th><th>Mass</th><th>Hair Color</th><th>Skin Color</th></tr>";
    $.each(result.results, (index, person) => {
        index += 1
        table += "<tr>";
        table += "<td>" + index + "</td>";
        table += "<td>" + person.name + "</td>";
        table += "<td>" + person.height + "</td>";
        table += "<td>" + person.mass + "</td>";
        table += "<td>" + person.hair_color + "</td>";
        table += "<td>" + person.skin_color + "</td>";
        table += "</tr>";
    });
    console.log(table);
    $("#starwar").html(table);
}).fail((error) => {
    console.log(error);
});