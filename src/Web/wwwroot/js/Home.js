var tbodyEntrances = document.getElementById("tbodyEntrances")
var divWalletw = document.getElementById("divWallets")

RenderChart = (canvasId, type, xValues, yValues, barColors) => {
    // let xValues = ["Total Income", "Total Expanse"];
    // let yValues = [55, 49];
    // let barColors = ["black", "black"];
    new Chart(canvasId, {
        type: type,
        data: {
            labels: xValues,
            datasets: [{
                backgroundColor: barColors,
                data: yValues
            }]
        },
        options: {
            legend: { display: false },
            title: { display: false }
        }
    });
}

//#region Current Values
RenderCurrentValues = (totalExpanses, totalIncomes) => { 
    RenderChart(
        "totalIncomesExpansesChart",
        "bar",
        ["Total Income", "Total Expanse"],
        [totalExpanses, totalIncomes],
        ["black", "black"]
    )
}
//#endregion

//#region Wallets
RenderWallets = wallets => {
    console.log(wallets)
}

//#endregion

//#region Entrance
RenderEntrancesTable = entrances => {
    entrances.forEach(entrance => {
        tbodyEntrances.innerHTML += `
        <tr>
            <td>${entrance.description}</td>
            <td>${GetBadgeType(entrance.type)}</td>
            <td>${FormatValueAsMoney(entrance.value)}</td>
            <td>${GetCategoryNameBadge(entrance.category.name)}</td>
            <td>${FormatValueAsDate(entrance.createdAt)}</td>
            <td>${GetEditEntranceButton(entrance.id)}</td>
        </tr>
        `
    });
}

GetBadgeType = typeBadge => {
    switch (typeBadge) {
        case 1:
            return `<label class="badge badge-pill badge-success">Income</label>`
        case 2:
            return `<label class="badge badge-pill badge-danger">Expanse</label>`
        default:
            return `<label class="badge badge-pill badge-secondary">Unknown</label>`
    }
}

GetCategoryNameBadge = categoryName => `<label class="badge badge-pill badge-primary" for="">${categoryName}</label>`

GetEditEntranceButton = id => `<a class="btn btn-primary btn-sm" href="/Entrances/Edit/${id}">Edit</a>`
//#endregion

//#region Load Screen
GetData = () => fetch('/GetData').then(response => response.json())

RenderScreen = () => {
    GetData().then(data => {
        RenderCurrentValues(data.totalExpanses, data.totalIncomes)
        RenderWallets(data.walletsValues)
        RenderEntrancesTable(data.entrances)
    });
}

RenderScreen()
//#endregion
