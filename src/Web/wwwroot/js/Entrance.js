$(document).ready(function () {
    $("#datatableEntrances").DataTable({
        serverSide: true,
        responsive: true,
        processing: true,
        ajax: {
            url: "/Entrances/Datatables",
            type: "POST",
            datatype: "json"
        },
        columns: [
            {
                title: "Description",
                autoWidth: true,
                data: "description"
            },
            {
                title: "Type",
                autoWidth: true,
                data: data => {
                    switch (data.type) {
                        case 1:
                            return `<label class="badge badge-pill badge-success">Income</label>`
                        case 2:
                            return `<label class="badge badge-pill badge-danger">Expanse</label>`
                        default:
                            return `<label class="badge badge-pill badge-secondary">Unknown</label>`
                    }
                }
            },
            {
                title: "Value",
                autoWidth: true,
                data: data => FormatValueAsMoney(data.value)
            },
            {
                title: "Category",
                autoWidth: true,
                data: data => `<label class="badge badge-pill badge-primary">${data.category.name}</label>`,
            },
            {
                title: "Created At",
                autoWidth: true,
                data: data => FormatValueAsDate(data.createdAt)
            },
            {
                orderable: false,
                data: data => `<a href='Entrances/Edit/${data.id}' class='btn btn-primary btn-sm'>Edit</a>`
            },
        ]
    })
})
