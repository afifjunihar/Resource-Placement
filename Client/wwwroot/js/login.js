$(document).ready(function () {

	$('#loginForm').submit(function (event) {
		event.preventDefault();

		validateEmail();
		validatePassword();

		var obj = {};

		obj.EmailOrUsername = $('#email').val().trim();
		obj.Password = $('#pass').val().trim()

		$.ajax({
			url: "/Login",
			type: "POST",
			data: obj,
			dataType: 'json',
			success: function (response, stat, xhr) {
				console.log(stat)
				console.log(xhr)
				if (response.status == "BadRequest") {
					let input = $('#pass');
					showValidate(input, response.message);
				} else if (response.status == "NotFound") {
					let input = $('#email');
					showValidate(input, response.message);
				} else {
					window.location = "/dashboard"
				}
			},
			error: function (a, b , c) {
				console.log(a)
				console.log(b)
				console.log(c)
			}
		})
	});

	$('#lupaPassForm').submit(function (event) {
		event.preventDefault();
		validateLupaEmail();
		var obj = {
			"KeyStr": $('#forgetemail').val().trim(),
		}
		console.log(obj);
		$.ajax({
			url: "/LupaPassword/LupaPassword",
			type: "POST",
			data: obj,
			dataType: 'json',
			success: function (response) {
				console.log(response);
				if (response == "Bad Request") {
					let input = $('#forgetemail');
					showValidate(input, response.message);
				}

				else if (response == "OK")
				{				
					Swal.fire(
						'Good job!',
						'Email Berhasil Dikirim ! ',
						'success'
					);

				}
			}
		})
	});

	let form = $('.input');
	$(form).each(function () {
		$(this).focus(function () {
			hideValidate(this);
		});
	})
})



function showValidate(input, msg) {
	var thisAlert = $(input).parent();
	$(thisAlert).addClass('alert-validate').attr('data-validate', msg);
}

function hideValidate(input) {
	var thisAlert = $(input).parent();
	$(thisAlert).removeClass('alert-validate').removeAttr('data-validate');
}

function validateEmail(msg) {
	let formEmail = $('#email');
	let emailValue = $('#email').val().trim();

	if (emailValue == '') {
		showValidate(formEmail, 'Field tidak boleh kosong');
	}
}

function validatePassword(msg) {
	let formPass = $('#pass');
	let passValue = $('#pass').val().trim();

	if (passValue == '') {
		showValidate(formPass, 'Field tidak boleh kosong');
	}
}

function validateLupaEmail(msg) {
	let formEmail = $('#forgetemail');
	let emailValue = $('#forgetemail').val().trim();

	if (emailValue == '') {
		showValidate(formEmail, 'Field tidak boleh kosong');
	}
}

function forgetPassword()
{
	$("#lupapasswordModal").modal('show');
}

function tutupmodalpassword() {
	$("#lupapasswordModal").modal('toggle');
}

