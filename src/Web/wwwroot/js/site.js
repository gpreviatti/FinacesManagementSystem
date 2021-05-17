var FormatValueAsMoney = value => value.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })

var FormatValueAsDate = date => new Date(date).toLocaleDateString()

var barChartConfig = {
    type: "bar",
    options: {
        plugins: {
            legend: { display: false }
        }
    }
}
