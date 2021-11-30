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
            'url': "/profiles/GetProfileCandidate",
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
                "data": "phone",
                 "render": function (data, type, row, meta) {
                    if (data[0] == '0') {
                        return "+62" + data.slice(1);
                    }
                    else if (data.slice(0, 3) == '+62') {
                        return data;
                    }
                    else if (data.slice(0, 2) == '62') {
                        return "+" + data;
                    }
                    else {
                        return "+62" + data;
                    }
                }
            },
            {
                "data": "gender",
            },
            {
                "data": "",
                "render": function (data, type, row, meta) {
                    if (row['score_Status'] == "Yes") {
                      return `<button type="button" class="btn btn-info" onclick="scoreUser('${row['user_Id']}');" data-toggle="modal" data-target="#formModal"><i class='fa fa-address-book'></i></button>`
                    }
                    else if (row['score_Status'] == "No") {
                        return `<span class="badge badge-danger">Belum ada Nilai</span>`
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
});

function downloadExcel() {
    $('#listProject').DataTable().buttons().trigger();
}

function scoreUser(id)
{
    $("#userid").val(`${id}`);
    $("#userid").attr("disabled", true);
    $("#btnsubmit").attr("hidden", true);

    $.ajax({
        url: "/skills/Skill/" + id,
        type: 'GET',
        dataType: 'JSON'
    })
        .done((result, textStatus, jqXHR) => {
            console.log(result);
            console.log(textStatus);
            console.log(jqXHR);
            $("#fundamental").val(`${result[3].score}`);
            $("#fundamental").attr("disabled", true);

            $("#backend").val(`${result[0].score}`);
            $("#backend").attr("disabled", true);

            $("#frontend").val(`${result[1].score}`);
            $("#frontend").attr("disabled", true);

            $("#fullstack").val(`${result[2].score}`);
            $("#fullstack").attr("disabled", true);

        }).fail((error) => {
            console.log(error);
        });
}

function ReloadDatatables()
{
    $('#listProject').DataTable().ajax.reload();
}

function InputData() {
    $("#fullstack").attr("disabled", false);
    $("#userid").attr("disabled", false);
    $("#frontend").attr("disabled", false);
    $("#backend").attr("disabled", false);
    $("#fundamental").attr("disabled", false);
    $("#btnsubmit").attr("hidden", false);

    $("#userid").val("");
    $("#fundamental").val("");
    $("#backend").val("");
    $("#frontend").val("");
    $("#fullstack").val("");

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

function Insert()
{

    var obj = {
        "User_Id": $('#userid').val().trim(),
        "fundamentalCScore": $('#fundamental').val().trim(),
        "BackEndScore": $('#backend').val().trim(),
        "FrontEndScore": $('#frontend').val().trim(),
        "FullstackScore": $('#fullstack').val().trim(),
    }

        $.ajax({
            url: "/skills/AddSkill/",
            type: 'POST',
            data: obj,
            dataType: 'JSON'
        }).done((result, textStatus, jqXHR) => {
            console.log(result);
            console.log(textStatus);
            console.log(jqXHR);

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
                    text: 'Pendaftaran Gagal!',
                    footer: '<a>Pastikan data User_Id dan score belum pernah dimasukan sebelumnya. </a>'
                });
            }
        })
          .fail((error) => {
                console.log(error);
            });
}
  