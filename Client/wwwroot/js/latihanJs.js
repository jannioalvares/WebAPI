const animals = [
    { name: "nemo", species: "fish", class: { name: "invertebrata" } },
    { name: "gary", species: "mouse", class: { name: "mamalia" } },
    { name: "dory", species: "fish", class: { name: "invertebrata" } },
    { name: "tom", species: "mouse", class: { name: "mamalia" } },
    { name: "aji", species: "wibu", class: { name: "mamalia" } }
];

for (let i = 0; i < animals.length; i++) {
    if (animals[i].species !== "mouse") {
        animals[i].class.name = "non mamalia";
    }
}
console.log(animals);

const onlyMouse = animals.filter(animal => animal.species === "mouse");
console.log(onlyMouse);

const mouseAnimals = [];
animals.forEach(animal => {
    if (animal.species !== "mouse") {
        animal.class.name = "non mamalia";
    } else {
        mouseAnimals.push(animal);
    }
});
console.log(animals);
console.log(mouseAnimals);


