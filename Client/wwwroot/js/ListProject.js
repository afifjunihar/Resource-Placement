$(document).ready(function () {

    $.fn.dataTable.ext.classes.sPageButton = 'button primary_button';

    $('#listProject').DataTable({
        dom: 'Bfrtip',
        pagingType: "simple_numbers",
        buttons: [
            {
                extend: 'excelHtml5',
                name: 'excel',
                title: 'Client Projects',
                sheetName: 'Projects',
                text: '',
                className: 'buttonsToHide fa fa-download btn-default',
                filename: 'All Project Data',
                autoFilter: true,
                exportOptions: {
                    columns: [0, 1, 2, 3, 4]
                }
            }
        ],
        drawCallback: function () {
            $('.buttonsToHide')[0].style.visibility = 'hidden'
        },
        'ajax': {
            'url': "/Projects/ListProject",
            'dataSrc': ''
        },
        'columnDefs': [
            { "className": "dt-center", "targets": "_all", "orderable": false }
        ],
        'columns': [
            {
                "data": "project_Id"
            },
            {
                "data": "creator_Id",
            },
            {
                "data": "project_Name"
            },
            {
                "data": "capacity",
            },
            {
                "data": "current_Capacity",
            },
            {
                "data": "required_Skill"
            },
            {
                "data": "status",
                "render": function (data, type, row, meta) {
                    if (data == "Open") {
                        return `<span class="badge badge-success">Open</span>`
                    }
                    else if (data == "Closed") {
                        return `<span class="badge badge-danger">Closed</span>`
                    }
                }
            }
        ],
        language: {
            paginate: {
                next: `<i class="fa fa-arrow-right">`,
                previous: `<i class="fa fa-arrow-left">`
            }
        }       
    })
    $('#listProject').DataTable.ext.classes.sPageButton = 'button primary_button';
});

function downloadExcel() {
    $('#listProject').DataTable().buttons().trigger();
}