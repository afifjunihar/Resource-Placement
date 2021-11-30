$(document).ready(function () {
    $('#listProject').DataTable({
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'excelHtml5',
                name: 'excel',
                title: 'Client Projects',
                sheetName: 'Projects',
                text: '',
                className: 'buttonsToHide fa fa-download btn-default',
                filename: 'Data',
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
            'url': "/Projects/ListOpenProject",
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
            },
            {
                "data": "",
                "render": function (data, type, row, meta) {
                    return `<button type="button" class="btn btn-success" onclick="projectId('${row['project_Id']}');" data-toggle="modal" data-target="#formModal"><i class='fas fa-edit'></i></button>`
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

    $('#listPortofolio').DataTable({
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'excelHtml5',
                name: 'excel',
                title: 'Client Projects',
                sheetName: 'Projects',
                text: '',
                className: 'buttonlistPortofolio fa fa-download btn-default',
                filename: 'Data',
                autoFilter: true,
                exportOptions: {
                    columns: [0, 1, 2, 3, 4]
                }
            }
        ],
        drawCallback: function () {
            $('.buttonlistPortofolio')[0].style.visibility = 'hidden'
        },
        'ajax': {
            'url': "/skills/GetListSkill",
            'dataSrc': ''
        },
        'columnDefs': [
            { "className": "dt-center", "targets": "_all", "orderable": false }
        ],
        'columns': [
            {
                "data": "user_Id"
            },
            {
                "data": "fullName",
            },
            {
                "data": "fundamentalC"
            },
            {
                "data": "backEnd",
            },
            {
                "data": "frontEnd",
            },
            {
                "data": "fullStack"
            },
            {
                "data": "score",
                "render": function (data, type, row, meta) {
                    if (data >= '80' ) {
                        return `A`
                    }
                    else if (data < "80") {
                        return `B`
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


    $("#inputForm").validate({
        rules: {
            projectid: "required",
            userid: "required",
            interviewdate: "required",
            deskripsi: "required"
        },
        errorPlacement: function (error, element) {
        },
        highlight: function (element) {
            $(element).closest('.form-control').addClass('is-invalid');
            $(element).closest('.form-group').addClass('is-invalid');
        },
        unhighlight: function (element) {
            $(element).closest('.form-control').removeClass('is-invalid');
            $(element).closest('.form-group').removeClass('is-invalid');
        }
    });
});

function downloadExcel() {
    $('#listProject').DataTable().buttons().trigger();
}

function showCandidate()
{
  
    console.log( $("#showButton").html());
    if ($("#showButton").html() == `<i class="far fa-eye"> Show </i>`) {
        $("#showButton").empty();
        $("#showButton").append(`<i class="far fa-eye-slash"> Closed </i>`);
        $("#listcandidate").attr("hidden", false);
        $('#listProject').DataTable().ajax.reload();
    }
    else
    {
        $("#showButton").empty();
        $("#showButton").append(`<i class="far fa-eye"> Show </i>`);
        $("#listcandidate").attr("hidden", true);
        $('#listProject').DataTable().ajax.reload();
    }

}
function Validate() {
    var ini = $("#inputForm").valid();
    console.log(ini);

    if (ini === true) {
        Insert();
    }
    else {
        Swal.fire(
            'Failed!',
            'Tolong Masukan Semua Data.',
            'error'
        );
    }
};

function projectId(id)
{
    $("#projectid").val(`${id}`);
}

function Insert() {
    var obj = {
        "Project_Id": $('#projectid').val().trim(),
        "User_Id": $('#userid').val().trim(),
        "Interview_Date": $('#interviewdate').val().trim(),
        "Description": $('#deskripsi').val().trim(),
        "ReadBy": $('#readby').val().trim(),
        "Interview_Result": $('#interviewproses').val().trim()
    }
    console.log(obj);

    $.ajax({
        url: `/assignproject/assign`,
        type: 'POST',
        data: obj,
        dataType: 'JSON'
        })
        .done((result, textStatus, jqXHR) =>
        {
            console.log(result);
            console.log(textStatus);
            if (result == "OK") {
                Swal.fire(
                    'Good job!',
                    'Data Berhasil Dimasukan! ',
                    'success'
                );
            }
            else {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Assign Gagal!',
                    footer: '<a>Pastikan data User_Id belum memiliki jadwal interview. </a>'
                });
            }
        }).fail((error) => {
            console.log(error);
        });;
}
