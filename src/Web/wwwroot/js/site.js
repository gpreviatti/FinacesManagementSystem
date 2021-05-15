
var FormatValueAsMoney = value => {
    return value
    .toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })
}

var FormatValueAsDate = date => {
    return new Date(date).toLocaleDateString()
}
