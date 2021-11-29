$(document).ready(function () {

	$.ajax({
		url: '/Interviews/current/' + userID,
		success: function (result, textStatus, jqXHR) {
			//console.log(result)
			//console.log(textStatus);
			//console.log(jqXHR)
			if (result.stats == 'Waiting') {
				RenderWaiting(result);
			} else if (result.stats == 'Accepted') {
				RenderAccept(result);
			}
		}
	})

	$.ajax({
		url: '/Interviews/history/' + userID,
		success: function (result, textStatus, jqXHR) {
			//console.log(result)
			//console.log(textStatus);
			//console.log(jqXHR)
			if (result.length > 0) {
				RenderTable(result);
			}
		}
	})

});

// Date Format

const nameMonths = [
	'Jan',
	'Feb',
	'Mar',
	'Apr',
	'May',
	'Jun',
	'Jul',
	'Aug',
	'Sep',
	'Oct',
	'Nov',
	'Dec'
];

function formatAMPM(date) {
	var hours = date.getHours();
	var minutes = date.getMinutes();
	var ampm = hours >= 12 ? 'PM' : 'AM';
	hours = hours % 12;
	hours = hours ? hours : 12; // the hour '0' should be '12'
	minutes = minutes < 10 ? '0' + minutes : minutes;
	var strTime = hours + ':' + minutes + ' ' + ampm;
	return strTime;
};

// End Date Format

function RenderWaiting(result) {
	let date = new Date(result.accept_Date);
	let year = date.getFullYear();
	let month = nameMonths[date.getMonth()];
	let day = date.getDate();
	let time = formatAMPM(date);
	let project = result.name;
	let stats = result.stats;



	const waitTmp = `
		<!-- Date Card Example -->
	<div class="col-xl-3 col-md-6 mb-4">
		<div class="card h-100">
			<div class="card-body">
				<div class="row align-items-center">
					<div class="col mr-2">
						<div class="text-xs font-weight-bold text-uppercase mb-1">Next Interview</div>
						<div class="h5 mb-0 font-weight-bold text-gray-800">${day} ${month} ${year}</div>
						<div class="mt-2 mb-0 text-muted text-xs">
							<span class="text-primary mr-2"><i class="fa fa-exclamation"></i></span>
							<span>Harap Hadir</span>
						</div>
					</div>
					<div class="col-auto">
						<i class="fas fa-calendar fa-2x text-primary"></i>
					</div>
				</div>
			</div>
		</div>
	</div>
	<!-- Time Card Example -->
	<div class="col-xl-3 col-md-6 mb-4">
		<div class="card h-100">
			<div class="card-body">
				<div class="row no-gutters align-items-center">
					<div class="col mr-2">
						<div class="text-xs font-weight-bold text-uppercase mb-1">Time</div>
						<div class="h5 mb-0 font-weight-bold text-gray-800">${time}</div>
						<div class="mt-2 mb-0 text-muted text-xs">
							<span class="text-success mr-2"><i class="fa fa-exclamation"></i></span>
							<span>Standby - 10 Menit</span>
						</div>
					</div>
					<div class="col-auto">
						<i class="fas fa-clock fa-2x text-success"></i>
					</div>
				</div>
			</div>
		</div>
	</div>
	<!-- Project Card Example -->
	<div class="col-xl-3 col-md-6 mb-4">
		<div class="card h-100">
			<div class="card-body">
				<div class="row no-gutters align-items-center">
					<div class="col mr-2">
						<div class="text-xs font-weight-bold text-uppercase mb-1">Project</div>
						<div class="h5 mb-0 mr-3 font-weight-bold text-gray-800">${project}</div>
					</div>
					<div class="col-auto">
						<i class="fas fa-users fa-2x text-info"></i>
					</div>
				</div>
			</div>
		</div>
	</div>
	<!-- Status Interview Card Example -->
	<div class="col-xl-3 col-md-6 mb-4">
		<div class="card  h-100">
			<div class="card-body">
				<div class="row no-gutters align-items-center">
					<div class="col mr-2">
						<div class="text-xs font-weight-bold text-uppercase mb-1">Status Interview</div>
						<div class="h5 mb-0 font-weight-bold text-gray-800">Waiting Client Decision</div>
					</div>
					<div class="col-auto">
						<i class="fas fa-comments fa-2x text-warning"></i>
					</div>
				</div>
			</div>
		</div>
	</div>
`

	document.getElementById('card-content').innerHTML = waitTmp;
}

function RenderAccept(result) {
	let project = result.name;
	let spec = result.spec;
	let date = new Date(result.accept_Date);
	let year = date.getFullYear();
	let month = nameMonths[date.getMonth()];
	let day = date.getDate();


	const accTmp = `
<!-- Current Project -->
	<div class="col-xl-4 col-md-6 mb-4">
		<div class="card h-100">
			<div class="card-body">
				<div class="row align-items-center">
					<div class="col mr-2">
						<div class="text-xs font-weight-bold text-uppercase mb-1">Current Project</div>
						<div class="h5 mb-0 font-weight-bold text-gray-800">${project}</div>
					</div>
					<div class="col-auto">
						<i class="fas fa-book fa-2x text-primary"></i>
					</div>
				</div>
			</div>
		</div>
	</div>
	<!-- Spesialist Example -->
	<div class="col-xl-4 col-md-6 mb-4">
		<div class="card h-100">
			<div class="card-body">
				<div class="row no-gutters align-items-center">
					<div class="col mr-2">
						<div class="text-xs font-weight-bold text-uppercase mb-1">Specialist</div>
						<div class="h5 mb-0 font-weight-bold text-gray-800">${spec}</div>
					</div>
					<div class="col-auto">
						<i class="fas fa-hammer fa-2x text-success"></i>
					</div>
				</div>
			</div>
		</div>
	</div>
	<!-- Project Card Example -->
	<div class="col-xl-4 col-md-6 mb-4">
		<div class="card h-100">
			<div class="card-body">
				<div class="row no-gutters align-items-center">
					<div class="col mr-2">
						<div class="text-xs font-weight-bold text-uppercase mb-1">Start Date</div>
						<div class="h5 mb-0 mr-3 font-weight-bold text-gray-800">${day} ${month} ${year}</div>
					</div>
					<div class="col-auto">
						<i class="fas fa-calendar-check fa-2x text-info"></i>
					</div>
				</div>
			</div>
		</div>
	</div>
`
	document.getElementById("card-content").innerHTML = accTmp;
}

function RenderTable(result) {
	let tableRes = '';
	let num = 1;
	$.each(result, function (index, value) {
		const date = new Date(value.jadwal);
		const tgl = date.toLocaleDateString('id-ID', { year: 'numeric', month: 'short', day: 'numeric' });
		const jam = date.toLocaleTimeString('en-EN', { hour12: true, timeStyle: 'short' } );
		const status = value.status;		

		tableRes += `
		<tr>
			<td><a>${num++}</a></td>
			<td>${value.name}</td>
			<td>${value.req_skill}</td>
			<td>${tgl}</td>
			<td>${jam}</td>
			<td>${status == 'Waiting' ? '<span class="badge badge-warning">Waiting</span>' : status == 'Rejected' ? '<span class="badge badge-danger">Rejected</span>' : '<span class="badge badge-success">Accpeted</span>' }
			</td>
		</tr>
		`
	})
	document.getElementById('tblResult').innerHTML = tableRes;
}
