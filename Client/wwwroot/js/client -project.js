$(document).ready(function () {
	$('#dataTableHover').DataTable({
		ajax: {
			url: '/projects/getall',
			dataSrc: ''
		},
		lengthChange: false,
		columns: [
			{
				data: null,
				render: function (data, type, full, meta) {
					return meta.row + 1;
				}
			},
			{ data: 'project_Name' },
			{ data: 'required_Skill' },
			{
				data: '',
				orderable: false,
				render: function (data, type, row) {
					return `${row.current_Capacity} / ${row.capacity}`
				}
			},
			{
				data: null,
				className: "dt-center",
				orderable: false,
				render: function (data, type, row) {
					return `<button type="button" class="btn btn-warning" id="editBtn" data-toggle="modal" data-target="#editProject" data-id="${row.project_Id}" rel="tooltip" data-placement="top" title="Edit Project" >
											<i class="far fa-edit"></i>
									</button>
									<a href="projects/details/${row.project_Id}" class="btn btn-info" rel="tooltip" data-placement="top" title="Detail Project"><i class="fa fa-arrow-right"></i></a>`
				}
			}
		],
		fnDrawCallback: function (oSetting) {
			$('body').tooltip({ selector: '[rel="tooltip"]' }, { trigger: "hover" });

			let btn = document.querySelectorAll('#editBtn');

			btn.forEach(el => el.addEventListener('click', function (event) {
				event.preventDefault();
				let dataId = el.getAttribute('data-id');

				$.ajax({
					url: '/projects/get/' + dataId,
					method: 'get',
					success: function (result, status, xhr) {
						console.log(result)
						if (xhr.status == 200) {
							$('#editName').val(result.project_Name);
							$('#editCapacity').val(result.capacity).attr('min', result.current_Capacity);
							$('#editSkill').val(result.required_Skill).prop('readonly', true);
							$(`#editStatus option[value="${result.status}"]`).prop('selected', true);

							let temId = {
								capacity: result.capacity,
								creator_Id: result.creator_Id,
								current_Capacity: result.current_Capacity,
								project_Id: result.project_Id,
								project_Name: result.project_Name,
								required_Skill: result.required_Skill,
								status: result.status,
							}

							localStorage.setItem('tempData', JSON.stringify(temId));
						}
					}
				})
			}));
		}
	})
});




// Create Project
(function () {
	'use strict';
	window.addEventListener('load', function () {
		// Fetch all the forms we want to apply custom Bootstrap validation styles to
		var forms = document.getElementsByClassName('needs-validation');
		// Loop over them and prevent submission
		var validation = Array.prototype.filter.call(forms, function (form) {
			form.addEventListener('submit', function (event) {
				if (form.checkValidity() === false) {
					event.preventDefault();
					event.stopPropagation();
					form.classList.add('was-validated');
				} else {
					createProject(event);
					form.classList.remove('was-validated');
				}

			}, false);
		});
	}, false);
})();

// Update Project
(function () {
	'use strict';
	window.addEventListener('load', function () {
		// Fetch all the forms we want to apply custom Bootstrap validation styles to
		var forms = document.getElementsByClassName('needs-validation-two');
		// Loop over them and prevent submission
		var validation = Array.prototype.filter.call(forms, function (form) {
			form.addEventListener('submit', function (event) {
				if (form.checkValidity() === false) {
					event.preventDefault();
					event.stopPropagation();
					form.classList.add('was-validated');
				} else {
					updateProject(event);
					form.classList.remove('was-validated');
					localStorage.clear();
				}

			}, false);
		});
	}, false);
})();

function createProject(e) {
	e.preventDefault();
	e.stopPropagation();

	let formData = {
		"project_Name": $('#projetName').val(),
		"capacity": $('#capacity').val(),
		"current_Capacity": 0,
		"required_Skill": $('#reqSkill').val(),
		"status": "Open",
		"creator_Id": $('#creatorID').val()
	}

	$.ajax({
		url: '/projects/post',
		method: 'post',
		dataType: 'json',
		data: formData,
		success: function (response) {
			if (response == "OK") {
				Swal.fire({
					title: 'Berhasil membuat project',
					icon: 'success'
				});
				$('#dataTableHover').DataTable().ajax.reload();
				resetModal();
			} else {
				Swal.fire({
					title: 'Gagal membuat project',
					icon: 'error'
				});
				resetModal()
			}
			console.log(response)
		}
	})
}

function updateProject(e) {
	e.preventDefault();
	e.stopPropagation();

	let formData = {
		"project_Name": $('#editName').val(),
		"capacity": $('#editCapacity').val(),
		"status": $('#status').val(),
	}

	let tempdata = JSON.parse(localStorage.getItem("tempData"));
	if (formData.capacity == tempdata.current_Capacity) {
		formData.status = "Closed";
	} else if (formData.capacity > tempdata.capacity) {
		formData.status = "Open";
	}
	formData.project_Id = tempdata.project_Id;
	formData.current_Capacity = tempdata.current_Capacity;
	formData.required_Skill = tempdata.required_Skill;
	formData.creator_Id = tempdata.creator_Id;

	$.ajax({
		url: '/projects/put/' + tempdata.project_Id,
		method: 'put',
		dataType: 'json',
		data: formData,
		success: function (response, code, xhr) {
			console.log(response)
			console.log(code)
			console.log(xhr)
			if (response == "OK") {
				Swal.fire({
					title: 'Berhasil mengupdate project',
					icon: 'success'
				});
				$('#dataTableHover').DataTable().ajax.reload();
				resetModal();
			} else {
				Swal.fire({
					title: 'Gagal update project',
					icon: 'error'
				});
				resetModal()
			}
			console.log(response)
		}
	})
}

function resetModal() {
	$('#addProject').modal('hide');
	$('#editProject').modal('hide');
	$('#projetName').val("");
	$('#capacity').val(1);
	$('#reqSkill').prop('selectedIndex', 0);
	$('#invalidCheck').prop('checked', false);
	$('#editCheck').prop('checked', false);
}