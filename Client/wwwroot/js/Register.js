$(document).ready(function () {

	$.validator.addMethod("strong_password",
		function (value, element) {
			let password = value;
			if (!(/^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[@#$%&*!])(.{8,20}$)/.test(password))) {
				return false;
			}
			return true;
		});

	$("#inputForm").validate({
		rules: {
			inputUserId: "required",
			firstName: "required",
			lastName: "required",
			inputPhone: "required",
			userStatus: "required",
			username: "required",
			gender: "required",
			inputEmail: {
				required: true,
				email: true
			},
			password: {
				required: true,
				strong_password: true
			}
		},
		errorPlacement: function (error, element) {
		},
		highlight: function (element) {
			$(element).closest('.form-control').addClass('is-invalid');
			$(element).closest('.form-group').addClass('is-invalid');

			var n = $(element).attr("name");
			if (n === "inputUserId") { $(element).attr("placeholder", "Please enter your NIK"); }
			if (n === "firstName") { $(element).attr("placeholder", "Please enter your First Name"); }
			if (n === "lastName") { $(element).attr("placeholder", "Please enter your Last Name"); }
			if (n === "inputPhone") { $(element).attr("placeholder", "Please enter your Phone Correctly"); }
			if (n === "inputEmail") { $(element).attr("placeholder", "Please enter your Email Correctly"); }
			if (n === "username") { $(element).attr("placeholder", "Please enter your username"); }
			if (n === "password") { $(element).attr("placeholder", "Min 1 Huruf Besar, 1 Huruf Kecil, 1 Angka, & 1 karakter @#$%&*!"); }
	
		},
		unhighlight: function (element) {
			$(element).closest('.form-control').removeClass('is-invalid');
			$(element).closest('.form-group').removeClass('is-invalid');
		}
	});


});


function Insert() {
	var obj = {
		"User_Id": $('#inputUserId').val().trim(),
		"FirstName": $('#firstName').val().trim(),
		"LastName": $('#lastName').val().trim(),
		"Email": $('#inputEmail').val().trim(),
		"Phone": $('#inputPhone').val().trim(),
		"Gender": $('#gender').val(),
		"User_Status": $('#userStatus').val().trim(),
		"Username": $('#username').val().trim(),
		"Password": $('#password').val().trim(),
	}
	console.log(obj);

	var user = $('#checkUser').val();	

	if (user == "Candidate") {
		 var link = "/Registers/RegisterEmployee"		
	}
	else if (user == "Trainer") {
		 var link = "/Registers/RegisterTrainer"
	}
	else if (user == "Client") {
		 var link = "/Registers/RegisterClient"
	}
	console.log(user);
	console.log(link);
	$.ajax({
		url: link,
		type: 'POST',
		data: obj,
		dataType: 'JSON',
	})
		.done((result, textStatus, jqXHR) => {
			console.log(result);
			console.log(textStatus);
			console.log(jqXHR);

			if (jqXHR.status = 200) {
				Swal.fire(
					'Good job!',
					'Data Berhasil Dimasukan! ',
					'success'
				);
				resetInput();
			} else {
				Swal.fire({
					icon: 'error',
					title: 'Oops...',
					text: 'Pendaftaran Gagal!',
					footer: '<a>Pastikan data User_Id, Email atau Nomer HP belum pernah digunakan sebelumnya. </a>'
				});
			}

	}).fail((error) => {
		console.log(error);
	});
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

function resetInput() {
	$('#inputUserId').val("");
	$('#firstName').val("");
	$('#lastName').val("");
	$('#inputEmail').val("");
	$('#inputPhone').val("");
	$('#gender').val("");
	$('#userStatus').val("");
	$('#username').val("");
	$('#password').val("");
}


