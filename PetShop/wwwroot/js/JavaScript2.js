var citis = document.getElementById("city");
var districts = document.getElementById("district");
var wards = document.getElementById("ward");

var Parameter = {
    url: "https://raw.githubusercontent.com/kenzouno1/DiaGioiHanhChinhVN/master/data.json",
    method: "GET",
    responseType: "json",
};

var promise = axios(Parameter);

promise.then(function (result) {
    renderCity(result.data);
});

function renderCity(data) {
    for (const x of data) {
        citis.options[citis.options.length] = new Option(x.Name, x.Id);
    }

    citis.onchange = function () {
        districts.options.length = 0;
        wards.length = 1;

        if (this.value != "") {
            const result = data.filter((n) => n.Id === this.value);

            for (const k of result[0].Districts) {
                var option = document.createElement("option");
                option.text = k.Name;
                option.value = k.Id;
                districts.add(option);
            }
        }
    };

    districts.onchange = function () {
        wards.length = 1;
        const dataCity = data.filter((n) => n.Id === citis.value);

        if (this.value != "") {
            const dataWards = dataCity[0].Districts.find((n) => n.Id === this.value).Wards;

            for (const w of dataWards) {
                wards.options[wards.options.length] = new Option(w.Name, w.Id);
            }
        }
    };
}