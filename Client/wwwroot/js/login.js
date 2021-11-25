$(document).ready(function () {

	$('#loginForm').submit(function (event) {
		event.preventDefault();

		validateEmail();
		validatePassword();

		var obj = {
			"EmailOrUsername": $('#email').val().trim(),
			"Password": $('#pass').val().trim()
		}

		$.ajax({
			url: "Login",
			type: "POST",
			data: obj,
			dataType: 'json',
			success: function (response) {
				if (response.status == "BadRequest") {
					let input = $('#pass');
					showValidate(input,response.message);
				} else if (response.status == "NotFound") {
					let input = $('#email');
					showValidate(input,response.message);
				} else {
					window.location = "/Home/Login"
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



