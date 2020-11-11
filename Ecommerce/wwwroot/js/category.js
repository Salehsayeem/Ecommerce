
var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#table').DataTable({
        "ajax": {
            "url": "/admin/category/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "50%" },
        { "data": "displayOrder", "width": "20%" },
        {
            "data": "id",
            "render": function (data) {
                return `<div class="text-center">
                             <a href="/Admin/category/Upsert/${data}" class='btn btn-success text-white' style='cursor:pointer; width:100px;'>
                             <i class='far fa-edit'></i> Edit
                             </a>
                              &nbsp;

                              <a onclick=Delete("/Admin/category/Delete/${data}") class='btn btn-danger text-white' style='cursor:pointer; width:100px;'>
                             <i class='far fa-trash-alt'></i> Delete
                             </a>
                    `;
            },
            "width": "30%"
        }
        ],
        "language": {
                "emplyTable" : "No records found"
},
"width":"100%"
    });
}

function Delete(url) {
    swal({
        title: "Are you sure ,you want to delete?",
        text: "You won't be able to restore the content",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "red",
        confirmButtonText: "Yes, Delete It",
        closeOnConfirm: true
    },
        function () {
            $.ajax({
                type: 'DELETE',
                url: url,
                success: function (data) {
                    if (data.success) {
                        ShowMessage(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    )
}

function ShowMessage(msg) {

}