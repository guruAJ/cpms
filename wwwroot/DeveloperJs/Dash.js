Date.prototype.toDDMMYYYY = function () {
    return this.getDate() + "/" + (this.getMonth() + 1) + "/" + this.getFullYear()
}

function SendData1() {
    $("#dashboard").hide();
    $("#table").removeClass("hide");
    var project = "HRMS";
    var status = "Functional_Units";
    if (status == "Functional_Units") {
        $("#fu").removeClass("hide");
        $("#stat").hide();
    }
    var listdata = "";
    $.ajax({
        type: 'POST',
        url: '/Hrms/SearchTest',
        data: { "Project": project, "Status": status },
        success: function (response) {
            var srl = 1;
            for (var i = 0; i < response.length; i++) {
                listdata += '<tr>';
                listdata += '<td>' + srl + '</td>;'
                listdata += '<td>' + response[i].sus + '</td>;'
                listdata += '<td>' + response[i].unit + '</td>;'
                listdata += '<td>' + new Date(response[i].createdOn.replace(/\//g, ' ')).toDDMMYYYY() + '</td>;'
                listdata += '</tr>';
                srl = srl + 1;
            }
            $('#tbldata').html(listdata);
            $('#table_id').DataTable({
                retrieve: true,
                lengthChange: true,
                ordering: true,
                "order": [[0, 'asc']],
            });
        },
        error: function (response) {
        }
    });
}
function SendData2() {
    $("#dashboard").hide();
    $("#table").removeClass("hide");
    var project = "HRMS";
    var status = "Complaint_Pending";
    if (status == "Complaint_Pending") {
        $("#cp").removeClass("hide");
        $("#stat").hide();
    }
    var listdata = "";
    $.ajax({
        type: 'POST',
        url: '/Hrms/SearchTest',
        data: { "Project": project, "Status": status },
        success: function (response) {
            var srl = 1;
            for (var i = 0; i < response.length; i++) {
                listdata += '<tr>';
                listdata += '<td>' + srl + '</td>;'
                listdata += '<td>' + response[i].sus + '</td>;'
                listdata += '<td>' + response[i].unit + '</td>;'
                listdata += '<td>' + new Date(response[i].createdOn.replace(/\//g, ' ')).toDDMMYYYY() + '</td>;'
                listdata += '</tr>';
                srl = srl + 1;
            }
            $('#tbldata').html(listdata);
            $('#table_id').DataTable({
                retrieve: true,
                lengthChange: true,
                ordering: true,
                "order": [[0, 'asc']],
            });
        },
        error: function (response) {
        }
    });
}
function SendData3() {
    $("#dashboard").hide();
    $("#table").removeClass("hide");
    var project = "HRMS";
    var status = "No_Information";
    if (status == "No_Information") {
        $("#ni").removeClass("hide");
        $("#stat").hide();
    }
    var listdata = "";
    $.ajax({
        type: 'POST',
        url: '/Hrms/SearchTest',
        data: { "Project": project, "Status": status },
        success: function (response) {
            var srl = 1;
            for (var i = 0; i < response.length; i++) {
                listdata += '<tr>';
                listdata += '<td>' + srl + '</td>;'
                listdata += '<td>' + response[i].sus + '</td>;'
                listdata += '<td>' + response[i].unit + '</td>;'
                listdata += '<td>' + new Date(response[i].createdOn.replace(/\//g, ' ')).toDDMMYYYY() + '</td>;'
                listdata += '</tr>';
                srl = srl + 1;
            }
            $('#tbldata').html(listdata);
            $('#table_id').DataTable({
                retrieve: true,
                lengthChange: true,
                ordering: true,
                "order": [[0, 'asc']],
            });
        },
        error: function (response) {
        }
    });
}
function SendData4() {
    $("#dashboard").hide();
    $("#table").removeClass("hide");
    var project = "IQMP";
    var status = "Functional_Units";
    if (status == "Functional_Units") {
        $("#qfu").removeClass("hide");
        $("#stat").hide();
    }
    var listdata = "";
    $.ajax({
        type: 'POST',
        url: '/Hrms/SearchTest',
        data: { "Project": project, "Status": status },
        success: function (response) {
            var srl = 1;
            for (var i = 0; i < response.length; i++) {
                listdata += '<tr>';
                listdata += '<td>' + srl + '</td>;'
                listdata += '<td>' + response[i].sus + '</td>;'
                listdata += '<td>' + response[i].unit + '</td>;'
                listdata += '<td>' + new Date(response[i].createdOn.replace(/\//g, ' ')).toDDMMYYYY() + '</td>;'
                listdata += '</tr>';
                srl = srl + 1;
            }
            $('#tbldata').html(listdata);
            $('#table_id').DataTable({
                retrieve: true,
                lengthChange: true,
                ordering: true,
                "order": [[0, 'asc']],
            });
        },
        error: function (response) {
        }
    });
}
function SendData5() {
    $("#dashboard").hide();
    $("#table").removeClass("hide");
    var project = "IQMP";
    var status = "Complaint_Pending";
    if (status == "Complaint_Pending") {
        $("#qcp").removeClass("hide");
        $("#stat").hide();
    }
    var listdata = "";
    $.ajax({
        type: 'POST',
        url: '/Hrms/SearchTest',
        data: { "Project": project, "Status": status },
        success: function (response) {
            var srl = 1;
            for (var i = 0; i < response.length; i++) {
                listdata += '<tr>';
                listdata += '<td>' + srl + '</td>;'
                listdata += '<td>' + response[i].sus + '</td>;'
                listdata += '<td>' + response[i].unit + '</td>;'
                listdata += '<td>' + new Date(response[i].createdOn.replace(/\//g, ' ')).toDDMMYYYY() + '</td>;'
                listdata += '</tr>';
                srl = srl + 1;
            }
            $('#tbldata').html(listdata);
            $('#table_id').DataTable({
                retrieve: true,
                lengthChange: true,
                ordering: true,
                "order": [[0, 'asc']],
            });
        },
        error: function (response) {
        }
    });
}
function SendData6() {
    $("#dashboard").hide();
    $("#table").removeClass("hide");
    var project = "IQMP";
    var status = "No_Information";
    if (status == "No_Information") {
        $("#qni").removeClass("hide");
        $("#stat").hide();
    }
    var listdata = "";
    $.ajax({
        type: 'POST',
        url: '/Hrms/SearchTest',
        data: { "Project": project, "Status": status },
        success: function (response) {
            var srl = 1;
            for (var i = 0; i < response.length; i++) {
                listdata += '<tr>';
                listdata += '<td>' + srl + '</td>;'
                listdata += '<td>' + response[i].sus + '</td>;'
                listdata += '<td>' + response[i].unit + '</td>;'
                listdata += '<td>' + new Date(response[i].createdOn.replace(/\//g, ' ')).toDDMMYYYY() + '</td>;'
                listdata += '</tr>';
                srl = srl + 1;
            }
            $('#tbldata').html(listdata);
            $('#table_id').DataTable({
                retrieve: true,
                lengthChange: true,
                ordering: true,
                "order": [[0, 'asc']],
            });
        },
        error: function (response) {
        }
    });
}