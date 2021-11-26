﻿var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    debugger;
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Product/GetAll"
        },
        "columns": [
            { "data": "title", "width": "15%" },
            { "data": "category.name", "width": "15%" },
            { "data": "price", "width": "15%" },
            { "data": "author", "width": "15%" },
          //  { "data": "category.name", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Admin/Product/Upsert/${data}" class="btn btn-success text-white style" style="cursor:pointer">
                                    <i class="far fa-edit">Edit</i> 
                                </a>
                                <a onclick=Delete("/Admin/Product/Delete/${data}") class="btn btn-danger text-white style" style="cursor:pointer">
                                    <i class="far fa-trash-alt">Delete</i>
                                </a>
     
                            </div>
                            `;
                }, "width": "40%"
            }
        ]
    });
}

function Delete(url) {
    swal({
        title: "Are you sure want to delete?",
        text: "You will not be able to restore the data",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}