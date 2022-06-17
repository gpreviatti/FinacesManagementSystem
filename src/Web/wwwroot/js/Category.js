let canvasCategories = document.getElementById("canvasCategories")

CreateChart = (canvasId, labels, data) => {
    new Chart(canvasId, {
        ...barChartConfig,
        data: {
            labels,
            datasets: [{
                backgroundColor: ["#000"],
                data
            }]
        }
    });
}

RenderChart = () => {
    let labels = []
    let data = []

    fetch('Categories/Chart').then(response => {

        response.json().then(categories => {

            categories.forEach(category => {
                labels.push(category.name)
                data.push(category.total)
            });

            setTimeout(() => {
                CreateChart(canvasCategories, labels, data)
            }, 1)
        });

    });

}

RenderChart()
