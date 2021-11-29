$(document).ready(function () {
	$.ajax({
		url: '/projects/getdetails/' + projectID,
		method: 'get',
		success: function (result, stats, xhr) {
			if (xhr.status == 200) {
				RenderProject(result);
			}
		}
	})

	$('#tbl-candidate').DataTable({
		ajax: {
			url: '/projects/handler/' + projectID,
			dataSrc: ''
		},
		lengthChange: false,
		searching: false,
		columns: [
			{
				data: null,
				render: function (data, type, full, meta) {
					return meta.row + 1;
				}
			},
			{ data: 'name' },
			{ data: 'grade' },
			{
				data: '',
				render: function (data, type, row, meta) {
					let tanggal = new Date(row['interview_Date']);
					return tanggal.toLocaleDateString('id-ID', { year: 'numeric', month: 'short', day: 'numeric' });
				}
			},
			{
				data: '',
				render: function (data, type, row, meta) {
					let tanggal = new Date(row['interview_Date']);
					return tanggal.toLocaleTimeString('en-EN', { hour12: true, timeStyle: 'short' });;
				}
			},
			{
				data: null,
				className: "dt-center",
				orderable: false,
				render: function (data, type, row) {
					return `
					<button class="btn btn-sm btn-success" id="acc" data-id="${row.interview_Id}" rel="tooltip" data-placement="top" title="Accept Candidate" data-toggle="modal" data-target="#accCan">
						<i class="fa fa-check"></i>
					</button>
					<button class="btn btn-sm btn-danger" id="rjj" data-id="${row.interview_Id}" rel="tooltip" data-placement="top" title="Reject Candidate" data-toggle="modal" data-target="#rjCan">
						<i class="fa fa-times"></i>
					</button>
`
				}
			}
		],
		fnDrawCallback: function (oSetting) {
			$('body').tooltip({ selector: '[rel="tooltip"]' }, { trigger: "hover" });
			ButtonListener()
		}
	})

	$('#accBtn').click(function() {
		let dataID = localStorage.getItem("interviewId");

		$.ajax({
			url: '/interviews/accept',
			method: 'patch',
			data: {
				"KeyInt" : dataID
			},
			success: function (res, stat, xhr) {
				if (xhr.status == 200) {
					$('#accCan').modal("hide");
					$('#tbl-candidate').DataTable().ajax.reload();
				} else {
					$('#accCan').modal("hide");
					Swal.fire({
						title: 'Terjadi kesalahan pada sistem',
						icon: 'error'
					})
				}
			}

		})
	})

	$('#rjBtn').click(function () {
		let dataID = localStorage.getItem("interviewId");

		$.ajax({
			url: '/interviews/reject',
			method: 'patch',
			data: {
				"KeyInt": dataID,
				"KeyStr": $('#alasan').val()
			},
			success: function (res, stat, xhr) {
				if (xhr.status == 200) {
					$('#rjCan').modal("hide");
					$('#tbl-candidate').DataTable().ajax.reload();
				} else {
					$('#rjCan').modal("hide");
					Swal.fire({
						title: 'Terjadi kesalahan pada sistem',
						icon: 'error'
					})
				}
			}

		})
	})

})



function RenderProject(result) {
	const cardTemp = `
<!-- Project Stats -->
	<div class="col-xl-4 col-md-6 mb-4">
		<div class="card h-100">
			<div class="card-body">
				<div class="row align-items-center">
					<div class="col ml-2">
						<div class="text-xs font-weight-bold text-uppercase mb-1">Project Status</div>
						<div class="h4 mb-0 text-primary font-weight-bold text-uppercase">${result.proj.status}</div>
						<div class="mt-2 mb-0 text-muted text-xs">
							<span class="h6 text-primary font-weight-bold mr-2">
									<i class="fa fa-users mr-2"></i> ${result.proj.current_Capacity} / ${result.proj.capacity}
							</span>
						</div>
					</div>
					<div class="col-auto mr-4">
						<i class="fab fa-stack-exchange fa-2x text-primary"></i>
					</div>
				</div>
			</div>
		</div>
	</div>
	<!-- Project Creator -->
	<div class="col-xl-4 col-md-6 mb-4">
		<div class="card h-100">
			<div class="card-body">
				<div class="row align-items-center">
					<div class="col ml-2">
						<div class="text-xs font-weight-bold text-uppercase mb-1">Create By</div>
						<div class="h4 mb-0 font-weight-bold">${result.fullname}</div>
						<div class="mt-2 mb-0 text-muted text-xs">
							<span class="h6 font-weight-bold mr-2"><i class="fa fa-feather-alt text-info mr-2"></i>Client</span>
						</div>
					</div>
					<div class="col-auto mr-4">
						<i class="fa fa-user-cog fa-2x text-info"></i>
					</div>
				</div>
			</div>
		</div>
	</div>
	<!-- Project Skill -->
	<div class="col-xl-4 col-md-6 mb-4">
		<div class="card h-100">
			<div class="card-body">
				<div class="row align-items-center">
					<div class="col ml-2">
						<div class="text-xs font-weight-bold text-uppercase mb-1">Required Skill</div>
						<div class="h4 mb-0 font-weight-bold">${result.proj.required_Skill}</div>
						<div class="mt-2 mb-0 text-muted text-xs">
							<span class="h6 font-weight-bold mr-2"><i class="fa fa-cog text-warning mr-2"></i>Development</span>
						</div>
					</div>
					<div class="col-auto mr-4">
						<i class="fa fa-crown fa-2x text-warning"></i>
					</div>
				</div>
			</div>
		</div>
	</div>
`
	document.getElementById('card-content').innerHTML = cardTemp;
	document.getElementById('projectName').innerText = result.proj.project_Name;

}

function RenderCandidate(result) {
	let list = "";
	let num = 1;

	$.each(result, function (index, value) {
		const date = new Date(value.interview_Date);
		const tgl = date.toLocaleDateString('id-ID', { year: 'numeric', month: 'short', day: 'numeric' });
		const jam = date.toLocaleTimeString('en-EN', { hour12: true, timeStyle: 'short' });
		list += `
		<tr>
			<td>${num++}</td>
			<td>${value.name}</td>
			<td class="font-weight-bold">${value.grade}</td>
			<td>${tgl}</td>
			<td>${jam}</td>
			<td>
				<button class="btn btn-sm btn-success" id="acc" data-id="${value.interview_Id}" rel="tooltip" data-placement="top" title="Accept Candidate" data-toggle="modal" data-target="#accCan">
					<i class="fa fa-check"></i>
				</button>
				<button class="btn btn-sm btn-danger" id="rjj" data-id="${value.interview_Id}" rel="tooltip" data-placement="top" title="Reject Candidate" data-toggle="modal" data-target="#rjCan">
					<i class="fa fa-times"></i>
				</button>
			</td>
		</tr>
		`
	})
	document.getElementById('tblResult').innerHTML = list;

	
}

function ButtonListener() {
	let btn = document.querySelectorAll('#acc, #rjj');
	btn.forEach(el => {
		el.addEventListener('click', function(event) {
			const interviewID = el.getAttribute('data-id')
			localStorage.setItem('interviewId', interviewID);
		})
	})
}