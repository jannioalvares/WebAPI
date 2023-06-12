let query1 = document.querySelector(".main:first-child");
let query2 = document.querySelector("ul li:nth-child(1)");
let query3 = document.querySelector("ul li:nth-child(2)");

let btn1 = document.getElementById("btn1");
var image = document.getElementById("myImage");

function baris1() {
    query1.innerHTML = "NAH BERUBAH KAN";
    query1.style.backgroundColor = "#00b2b2";
    query1.style.color = "black";
    
}

function baris2() {
    alert("yakin ?");
    query2.innerHTML = "NAH BERUBAH KAN";
    query2.style.backgroundColor = "#006666";
    query2.style.color = "white";
}

function baris3() {
    query3.innerHTML = "NAH BERUBAH KAN";
    query3.style.backgroundColor = "#001919";
    query3.style.color = "white";
}

btn1.addEventListener("dblclick", (event) => {
    query3.innerHTML = "NAH BERUBAH LAGI KAN";
    query1.style.backgroundColor = "grey";
    image.style.display = "block";
});

