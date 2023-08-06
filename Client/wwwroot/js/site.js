// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon"
}).done((result) => {
    let data = "";

    $.each(result.results, (key, val) => {
        $.ajax({
            url: val.url
        }).done((detailData) => {
            data += `
             <div class="col mb-4 m-auto">
                <div class="card shadow p-3 mb-5 rounded" style="width: 15rem; background-image: url(https://preview.redd.it/c30gynk02rv81.png?width=640&crop=smart&auto=webp&s=5df2e0cce63b46808693e0687695e8010252661a); background-size: cover;">
                    <img src="${detailData.sprites.other['official-artwork'].front_default}" class="card-img-top text-capitalize" alt="${detailData.name}">
                    <div class="card-body" style="background-color: "#f05452">
                        <h5 class="card-title text-center text-capitalize">${detailData.name} </h5>
                        <div class="text-center" id="pokemon-type">${detailData.types.map(type => `
                            <span class="badge mx-auto mb-3 text-center rounded-pill" style="background-color: ${typeBGColor(type.type.name)}">${type.type.name}</span>`).join('')}
                        </div>
                        <button onclick="detailPokemon('${val.url}')" type="button" data-bs-toggle="modal" data-bs-target="#detail-pokemon" class="btn d-grid mx-auto w-100" style="background-color: #f05452; color: #fff;">Detail</button>
                    </div>
                </div>
            </div>
            `;
            $("#pokemon-list").html(data);
        });
    });
});

function detailPokemon(pokemonDetailURL) {
    $.ajax({
        url: pokemonDetailURL,
        success: (pokemonDataResult) => {
            const number = `#${pokemonDataResult.id.toString().padStart(3, '0')}`;
            image = `
            <p class="h4 text-capitalize">${pokemonDataResult.name} <span class="badge bg-light text-dark mt-0 fs-6">${number}</span></p>
            <img src="${pokemonDataResult.sprites.other['official-artwork'].front_default}" class="img-fluid" alt="...">
            `;
            $("#pokemon-image").html(image);

            typePokemon = `
            <p class="h5">Type</p>
            <div class="fs-6" id="pokemon-type">
            ${pokemonDataResult.types.map(type => `
            <span class="badge mx-auto text-center rounded-pill" style="background-color: ${typeBGColor(type.type.name)}">${type.type.name}</span>`).join('')}
            </div>
            `;
            $("#type-pokemon").html(typePokemon);

            characteristics = `
            <p class="h5 mt-3">Characteristics</p>
            <p class="h6">Height : <span class="text-danger">${pokemonDataResult.height}</span></p>
            <p class="h6">Weight : <span class="text-danger">${pokemonDataResult.weight}</span></p>
            <p class="h6">Base Experience : <span class="text-danger">${pokemonDataResult.base_experience}</span></p>
            <p class="h6">Abilities : ${pokemonDataResult.abilities.map(ability => `
            <span class="text-danger">${ability.ability.name}</span>
            `).join(',')}</p>
            `;
            $("#characteristics-pokemon").html(characteristics);

            statsPokemon = `
            <p class="h5 mt-3">Stats</p>
            ${pokemonDataResult.stats.map(stat => `
            <div class="progress mb-2">
              <div class="progress-bar progress-bar-striped bg-${statBGColor(stat.stat.name)}" role="progressbar" style="width: ${stat.base_stat}%" aria-valuenow="${stat.base_stat}" aria-valuemin="0" aria-valuemax="100">${stat.stat.name}</div>
            </div>
            `).join('')}
            `;
            $("#status-pokemon").html(statsPokemon);

        }
    })
}

function statBGColor(stats) {
    if (stats === "hp") {
        return "success";
    }
    else if (stats === "attack") {
        return "danger";
    }
    else if (stats === "defense") {
        return "info";
    }
    else if (stats === "special-attack") {
        return "warning";
    }
    else if (stats === "special-defense") {
        return "secondary";
    }
    else if (stats === "speed") {
        return "primary";
    }
}

function typeBGColor(type) {
    if (type === "normal") {
        return "#929da3";
    }
    else if (type === "fighting") {
        return "#ce416b";
    }
    else if (type === "flying") {
        return "#8fa9de";
    }
    else if (type === "poison") {
        return "#aa6bc8";
    }
    else if (type === "ground") {
        return "#d97845";
    }
    else if (type === "rock") {
        return "#c5b78c";
    }
    else if (type === "bug") {
        return "#91c12f";
    }
    else if (type === "ghost") {
        return "#5269ad";
    }
    else if (type === "steel") {
        return "#5a8ea2";
    }
    else if (type === "fire") {
        return "#ff9d55";
    }
    else if (type === "water") {
        return "#5090d6";
    }
    else if (type === "grass") {
        return "#63bc5a";
    }
    else if (type === "electric") {
        return "#f4d23c";
    }
    else if (type === "psychic") {
        return "#f4d23c";
    }
    else if (type === "ice") {
        return "#73cec0";
    }
    else if (type === "dragon") {
        return "#0b6dc3";
    }
    else if (type === "dark") {
        return "#5a5465";
    }
    else if (type === "fairy") {
        return "#ec8fe6";
    }
    else if (type === "physical") {
        return "#ea551e";
    }
    else if (type === "special") {
        return "#1f4e94";
    }
    else {
        return "#68a090";
    }
}


/*
$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon"
}).done((result) => {
    
    $.each(result.results, (key, val) => {
        $.ajax({
            url: val.url
        }).done((detailData) => {
            let data = "";
            $.each(detailData, (key, val) => {
                data += `
                <div class="card" style="width: 18rem;">
                    <img src="${val.sprites.other['official-artwork'].front_default}" class="card-img-top" alt="...">
                    <div class="card-body">
                        <h5 class="card-title text-center">${val.name}</h5>

                        <ul class="list-group list-group-horizontal">
                                ${val.types.map(type => `<li class="list-group-item badge text-bg-dark">${type.type.name}</li>`).join('')}
                        </ul>
                        <a href="#" class="btn btn-primary">detail</a>
                    </div>
                </div>
                `;
            });
            console.log(data)
            $("#pokemon-list").html(data);
        });
    });
});

*/




/*
let a = document.getElementById("btn1");
let b = document.getElementById("btn2");
let c = document.getElementById("btn3");

a.addEventListener('click', () => {
    let d = document.getElementById("col1");
    d.style.backgroundColor = "blue";
})

b.addEventListener('click', () => {
    let d = document.getElementById("col3");
    d.style.backgroundColor = "red";
})

c.addEventListener('click', () => {
    let d = document.getElementById("col4");
    d.style.backgroundColor = "green";
})


//const animals = [
//    { name: "dory", species: "fish", class: { name: "vertebrata" } },
//    { name: "tom", species: "cat", class: { name: "mamalia" } },
//    { name: "nemo", species: "fish", class: { name: "vertebrata" } },
//    { name: "umar", species: "cat", class: { name: "mamalia" } },
//    { name: "gary", species: "fish", class: { name: "human" } },
//];

//let onlyCat = [];
//for (const animal of animals) {
//    if (animal.species = 'cat') {
//        onlyCat.push(animal);
//    }

//    if (animal.species = 'fish') {
//        animal.class.name = 'non-mamalia';
//    }
//}

//console.log('Only Cat :', onlyCat);
//console.log('Fish into non-mamalia', animals);

//asynchronous javascript
$.ajax({
    url: "https://swapi.dev/api/people/"
}).done((result) => {
    let temp = "";


    $.each(result.results, (key, val) => {
        number = key + 1;
        temp += "<tr>";
        temp += "<th>" + number + "</th>";
        temp += "<td>" + val.name + "</td>";
        temp += "<td>" + val.gender + "</td>";
        temp += "<td>" + val.height + "</td>";
        temp += "</tr>";

    })
    $("tbody#table-body").html(temp);
});

*/
