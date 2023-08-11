// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $("#employee-create").click(function () {
        // Panggil fungsi Insert() atau tindakan lain yang Anda inginkan
        Insert();

        // Tutup modal setelah selesai
        $("#employee-modal").modal("hide");
    });
});

function Insert() {
    var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    //ini ngambil value dari tiap inputan di form nya

    obj.firstName = $("#firstname").val();
    obj.lastName = $("#lastname").val();
    obj.birthDate = ($("#birthdate").val());
    obj.gender = parseInt($("#genderSelect").val());
    obj.hiringDate = ($("#hiringdate").val());
    obj.email = $("#emailInput").val();
    obj.phoneNumber = $("#phoneNumber").val();
    console.log(obj)
    //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
    $.ajax({
        url: "https://localhost:7181/api/employees",
        type: "POST",
        data: JSON.stringify(obj), // Konversi object menjadi JSON string
        contentType: "application/json; charset=utf-8", // Set content type
        dataType: "json", // Tipe data yang diharapkan dari server
         //jika terkena 415 unsupported media type (tambahkan headertype Json & JSON.Stringify();)
    }).done((result) => {
        // Buat alert pemberitahuan jika success
        const successAlert = `
            <div class="alert alert-primary" role="alert">
                ${result.message}
            </div>
        `;
        $("#alert-container").html(successAlert);
        Swal.fire({
            icon: 'success',
            title: result.message,
            showConfirmButton: false,
            timer: 1500
        }).then(() => {
            location.reload();
        });
    }).fail((error) => {
        console.log("Error Response:", error); 
    // Buat alert pemberitahuan jika gagal
    const errorAlert = `
            <div class="alert alert-danger" role="alert">
                ${error.responseJSON.message}
            </div>
        `;
        $("#alert-container").html(errorAlert);
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: error.responseJSON.message
        })    
})
}

$(document).ready(function () {
    // Memuat data menggunakan Ajax
    $.ajax({
        url: "https://localhost:7181/api/employees/detail"
    }).done((result) => {
        // Menghitung jumlah gender 0 dan gender 1
        let upiCount = 0;
        let upCount = 0;
        let otherCount =0;

        // Mengiterasi setiap baris data
        result.data.forEach(function (dataDetail) {
            if (dataDetail.universityName === "Universitas Pancasila") {
                upiCount++;
            } else if (dataDetail.universityName === "Universitas Pendidikan Indonesia") {
                upCount++;
            }
            else {
                otherCount++;
            }
        });

        // Menampilkan hasil perhitungan
        console.log("upi:", upiCount);
        console.log("up:", upCount);
        console.log("other:", otherCount);

        // Menginisialisasi Chart.js
        var xValues = ["Universitas Pancasila", "Universitas Pendidikan Indonesia", "Lain-lainnya"];
        var yValues = [upCount, upiCount, otherCount];
        var barColors = [
            "#FF3D3D",
            "#354656",
            "#4a9d9c"
        ];

        new Chart("myChart2", {
            type: "bar",
            data: {
                labels: xValues,
                datasets: [{
                    backgroundColor: barColors,
                    data: yValues
                }]
            },
            options: {
                legend: { display: false },
                title: {
                    display: true,
                    text: "Employee Universities"
                }
            }
        });
    });
});


$(document).ready(function () {
    // Memuat data menggunakan Ajax
    $.ajax({
        url: "https://localhost:7181/api/employees"
    }).done((result) => {
        // Menghitung jumlah gender 0 dan gender 1
        let femaleCount = 0;
        let maleCount = 0;

        // Mengiterasi setiap baris data
        result.data.forEach(function (dataDetail) {
            if (dataDetail.gender === 0) {
                femaleCount++;
            } else if (dataDetail.gender === 1) {
                maleCount++;
            }
        });

        // Menampilkan hasil perhitungan
        console.log("Total Gender 0 (Female):", femaleCount);
        console.log("Total Gender 1 (Male):", maleCount);

        // Menginisialisasi Chart.js
        var xValues = ["Female", "Male"];
        var yValues = [femaleCount, maleCount];
        var barColors = [
            "#b91d47",
            "#00aba9"
        ];

        new Chart("myChart1", {
            type: "pie",
            data: {
                labels: xValues,
                datasets: [{
                    backgroundColor: barColors,
                    data: yValues
                }]
            },
            options: {
                title: {
                    display: true,
                    text: "Employee Gender"
                }
            }
        });
    });
});

$(document).ready(function () {
    $('#employee-table2').DataTable({
        dom: 'Bfrtip',
        lengthMenu: [
            [10, 25, 50, -1],
            ['10 rows', '25 rows', '50 rows', 'Show all']
        ],
        buttons: ['colvis',
            { extend: 'copy', exportOptions: { columns: ':visible' } },
            { extend: 'csv', exportOptions: { columns: ':visible' } },
            { extend: 'excel', exportOptions: { columns: ':visible' } },
            { extend: 'pdf', exportOptions: { columns: ':visible' } },
            { extend: 'print', exportOptions: { columns: ':visible' } }
        ]
    });
});


$(document).ready(function () {
    let table = new DataTable('#employee-table', {
        dom: 'Bfrtip',
        buttons: ['colvis',
            {
                extend: 'copy',
                exportOptions: {
                    columns: ':visible',
                }
            },
            {
                extend: 'csv',
                exportOptions: { columns: ':visible' }
            },
            {
                extend: 'excel',
                exportOptions: { columns: ':visible' }
            },
            {
                extend: 'pdf',
                exportOptions: { columns: ':visible' }
            },
            {
                extend: 'print',
                exportOptions: { columns: ':visible' }
            }
        ],
        ajax: {
            url: "https://localhost:7181/api/employees",
            dataSrc: "data",
            dataType: "JSON"
        },
        columns: [
            {
                data: "",
                render: function (data, type, row, meta) {
                    // Mengembalikan nomor urut (indeks + 1) dalam kolom yang kosong
                    return meta.row + 1;
                }
            },
            { data: "nik" },
            { data: "firstName" },
            { data: "lastName" },
            { data: "birthDate" },
            {
                data: "gender",
                render: function (data, type, row) {
                    if (data === 0) {
                        return "Female";
                    }
                    else if (data === 1) {
                        return "Male";
                    }
                    else {
                        return "empty";
                    }
                }
            },
            { data: "hiringDate" },
            { data: "email" },
            { data: "phoneNumber" },
            {
                data: "",
                render: function (data, type, row) {
                    return `
                    <div class="d-flex justify-content-center">
                    <button onclick="('')" data-bs-toggle="modal" data-bs-target="" class="btn btn-light mx-1"><i class="fa-solid fa-pen"></i></button>
                    <button onclick="deleteEmployee('${row.guid}')" class="btn btn-danger mx-1"><i class="fa-solid fa-trash "></i></button>
                    </div>
                    `;
                }
            },
        ],

        initComplete: function () {
            // Menghitung jumlah gender 0 dan gender 1
            let gender0Count = 0;
            let gender1Count = 0;

            // Mengiterasi setiap baris data
            table.rows().data().each(function (row) {
                if (row.gender === 0) {
                    gender0Count++;
                } else if (row.gender === 1) {
                    gender1Count++;
                }
            });

            // Menampilkan hasil perhitungan
            console.log("Total Gender 0 (Female):", gender0Count);
            console.log("Total Gender 1 (Male):", gender1Count);
            var xValues = ["Female", "Male"];
            var yValues = [gender0Count, gender1Count];
            var barColors = [
                "#b91d47",
                "#00aba9"
            ];

            new Chart("myChart", {
                type: "pie",
                data: {
                    labels: xValues,
                    datasets: [{
                        backgroundColor: barColors,
                        data: yValues
                    }]
                },
                options: {
                    title: {
                        display: true,
                        text: "Employee Gender"
                    }
                }
            });
        }
    });
});

function deleteEmployee(guid) {
    $.ajax({
        url: `https://localhost:7181/api/employees?guid=${guid}`,
        type: "DELETE",
        success: function (result) {
            // Refresh atau lakukan tindakan lain setelah berhasil menghapus data
            Swal.fire({
                icon: 'success',
                title: result.message,
                showConfirmButton: false,
                timer: 1500
            }).then(() => {
                location.reload();
            });
        },
        error: function (error) {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: error.responseJSON.message
            })
        }
    });
}

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
    console.log(pokemonDetailURL);
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
    const typeColors = {
        "normal": "#929da3",
        "fighting": "#ce416b",
        "flying": "#8fa9de",
        "poison": "#aa6bc8",
        "ground": "#d97845",
        "rock": "#c5b78c",
        "bug": "#91c12f",
        "ghost": "#5269ad",
        "steel": "#5a8ea2",
        "fire": "#ff9d55",
        "water": "#5090d6",
        "grass": "#63bc5a",
        "electric": "#f4d23c",
        "psychic": "#f4d23c",
        "ice": "#73cec0",
        "dragon": "#0b6dc3",
        "dark": "#5a5465",
        "fairy": "#ec8fe6",
        "physical": "#ea551e",
        "special": "#1f4e94",
        "unknown": "#68a090" // Jika jenis tipe tidak dikenali
    };

    return typeColors[type] || "#68a090"; // Mengembalikan warna default jika tipe tidak dikenali
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

/*
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
*/

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


