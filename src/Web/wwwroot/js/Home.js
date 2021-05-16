let divWallets = document.getElementById("divWallets")

//#region ChartJs configs
CreateChart = (canvasId, labels, data) => {
    new Chart(canvasId, {
        ...barChartConfig,
        data: {
            labels,
            datasets: [{
                backgroundColor: ["green", "red"],
                data
            }]
        }
    });
}
//#endregion

//#region Current Values
RenderCurrentValues = (totalExpanses, totalIncomes) => {
    CreateChart(
        "totalIncomesExpansesChart",
        ["Total Income", "Total Expanse"],
        [totalIncomes, totalExpanses]
    )
}
//#endregion

//#region Wallets
RenderWalletsCards = wallets => {
    wallets.forEach(wallet => {
        let id = wallet.id
        divWallets.innerHTML += `<div class="col-lg-6">
            <div class="card mb-3">
                <div class="card-header bg-black">
                    ${wallet.name}
                </div>
                <div class="card-body">
                    <canvas id="wallet-${id}"></canvas>
                </div>
            </div>
        </div>
        `

        setTimeout(() => {
            CreateChart(
                `wallet-${id}`,
                ["Incomes", "Expanses"],
                [wallet.totalIncomes, wallet.totalExpanses]
            )
        }, 1)
    })
}
//#endregion

//#region Load Data
GetData = () => fetch('/GetData').then(response => response.json())

RenderScreen = () => {
    GetData().then(data => {
        RenderCurrentValues(data.totalExpanses, data.totalIncomes)
        RenderWalletsCards(data.walletsValues)
    });
}
//#endregion

window.onload = RenderScreen;
