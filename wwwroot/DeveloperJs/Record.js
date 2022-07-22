﻿$(document).ready(function () {

    if ($('#rdoval').html() == "HRMS") {

        $('#myprojectH').attr('checked', true);
        $('#myprojectI').hide();
        $('#nyTXTIqmp').hide();
    }
    else if ($('#rdoval').html() == "IQMP") {
        $('#myprojectI').attr('checked', true);
        $('#myprojectH').hide();
        $('#nyTXTHrms').hide();
    }
    //else if ($('#rdoval').html() == "CO") {
    //    $('#myprojectH').attr('checked', false);
    //    $('#myprojectI').attr('checked', true);
    //}

    $(':radio[value=1]').click(function () {
        // Might need to adjust the selector here based 
        // on the field you would like to hide
        //window.open("~/index?Id=0&Project=IQMP");
        window.location.replace("/Hrms/Record?Id=0&Project=HRMS");
    });
    $(':radio[value=2]').click(function () {
        // Might need to adjust the selector here based 
        // on the field you would like to hide

        window.location.replace("/Hrms/Record?Id=0&Project=IQMP");

    });


    $('.modalProject').change(function () {
        // Might need to adjust the selector here based 
        // on the field you would like to hide

        var Ids = $(this).closest('tr').children('td:first').text();
        var Statu = $(this).closest('.modalProject').val();
        var Ida = Ids.replace("\n", "");
        var Idb = Ida.replace("\n", "");
        var Id = parseInt(Idb);

        //debugger;
        $.ajax({
            type: 'POST',
            url: '/Hrms/UpdatStatus',
            data: { "Id": Id, "Statu": Statu },

            success: function (response) {
                alert("Status has been Changed Successfully....");

            },

            error: function (response) {
                alert("Status Not Changed Contact System Administrator....");
            }
        });
    });


    $("#btnAddNew").click(function () {
        var sus = $("#checksus").val();
        var unit = $("#myunit").val();
        // var test = '@ViewData["SUSNo"]';

        $.ajax({
            type: 'POST',
            url: '/Hrms/Validate',
            data: { "sus": sus, "units": unit },

            success: function (response) {
                if (response == "Cannot implicitly convert type 'string' to 'CPMS_AUTO.Models.MSUSNo'") {
                    alert("SUS No and Unit Both are not found in System....")
                    var susNo = response.sus
                    $('#modalSUS').val(susNo);
                    $('#mdalProject').val($('#rdoval').html());
                    sus = response.unit
                    $('#modalUnit').val(sus);
                    $('#exampleModal').modal('hide');
                }
                else {
                   
                    var susNo = response.sus
                    $('#modalSUS').val(susNo);
                    $('#mdalProject').val($('#rdoval').html());
                    sus = response.unit
                    $('#modalUnit').val(sus);
                    $('#exampleModal').modal('show');
                }

                ResetData();
            },

            error: function (response) {
                alert("SUS No and Unit Both are not found in System....");
            }
        });
    });
});


Date.prototype.toDDMMYYYY = function () {
    return this.getDate() + "/" + (this.getMonth() + 1) + "/" + this.getFullYear()
}

var unit = "";
var sus = "";
$("#btnSearch").click(function () {
    sus = $("#checksus").val();
    unit = $("#myunit").val();
    var project = $("input[type='radio']:checked").val();
    var listdata = "";
    $.ajax({
        type: 'POST',
        url: '/Hrms/Search',
        data: { "sus": sus, "unit": unit, "project": project },

        success: function (response) {
            for (var i = 0; i < response.length; i++) {
                listdata += '<tr>';
                listdata += '<td> <span id="spnid" class="d-none">' + response[i].id + '</span>' + response[i].id + '</td>';
                listdata += '<td>' + response[i].sus_No + '</td>;'
                listdata += '<td>' + response[i].unit + '</td>;'
                listdata += '<td>' + response[i].project + '</td>;'
                listdata += '<td>' + response[i].query + '</td>;'
                listdata += '<td>' + new Date(response[i].queryDate.replace(/\//g, ' ')).toDDMMYYYY() + '</td>'
                listdata += '<td>' + response[i].queryRaisedBy + '</td>;'
                listdata += '<td>' + response[i].reply + '</td>;'
                listdata += '<td>' + response[i].status + '</td>;'
                //if (response[i].status == "Open") {
                //    listdata += '<td><select id="stattt"><option value="Open">Open</option><option value="Closed">Close</option></select></td>'
                //}
                //else {
                //    listdata += '<td><select id="stattt"><option value="Closed">Close</option><option value="Open">Open</option></select></td>'
                //}
                /*listdata += '<td><input type="button" value="Edit" class="btnedt btn btn-primary"></td>'*/
                listdata += '</tr>';
            }
            $('#tbldata').html(listdata);
            $('#table_id').DataTable({
                retrieve: true,
                lengthChange: true,
                ordering: true,
                "order": [[0, 'asc']],
            });
            GetUnitStatus();

        },
        error: function (response) {
        }
    });
});

function GetUnitStatus() {
    sus = $("#checksus").val();
    unit = $("#myunit").val();
    var project = $("input[type='radio']:checked").val();
    $.ajax({
        type: 'POST',
        url: '/Hrms/GetRecordStat',
        data: { "unit": unit, "sus": sus, "project": project },

        success: function (response) {
            $("#selectedstatunit").val(response);
        },
        error: function (response) {
        }
    });
}

$(document).ready(function () {
    $('.ustat').click(function () {

        $('#selectedstatunit').attr('disabled', false);
    });
});

$(document).ready(function () {
    $("#selectedstatunit").prop("readonly", true);
});

$(document).ready(function () {
    $("#SaveStatus").click(function () {
        var unitstat = $("#selectedstatunit").val();
        var unit = $("#myunit").val();
        var project = $("input[type='radio']:checked").val();
        var sus = $("#checksus").val();

        $.ajax({
            type: 'POST',
            url: '/Hrms/SaveUnitStatus',
            data: { "unit": unit, "unitstat": unitstat, "project": project, "sus": sus },

            success: function (response) {
                $("#unistatus").val(response.hrms);
                $('#selectedstatunit').attr('disabled', true);
            },
            error: function (response) {
            }
        });
    });
});


$(document).on('change', '#stattt', function () {
    var Ids = $(this).closest("tr").find("#spnid").html();
    //var Ids = $(this).closest('tr').children('td:first').text();
    var Statu = $(this).closest('#stattt').val();
    var Ida = Ids.replace("\n", "");
    var Idb = Ida.replace("\n", "");
    var Id = parseInt(Idb);

   
    $.ajax({
        type: 'POST',
        url: '/Hrms/UpdatStatus',
        data: { "Id": Id, "Statu": Statu },

        success: function (response) {
            alert("Status has been Changed Successfully....");

        },

        error: function (response) {
            alert("Status Not Changed Contact System Administrator....");
        }
    });
});

$(document).ready(function () {
    var Id = "";
    $(document).on('click', '.btnedt', function () {
        Id = $(this).closest("tr").find("#spnid").html();
        //var tr = $(this).closest("tr");
        //var Id = tr.find("td:eq(0)").text();
        //var sus = tr.find("td:eq(1)").text(); //(this).closest('tr').children('td:susno').text()

        //var Id = $("#MyId").val();
        $.ajax({
            type: 'POST',
            url: '/Hrms/GetProjectDataById',
            data: { "Id": Id },
            success: function (response) {
                $('#modalSUS').val(response.sus_No);
                $('#Query').val(response.query);
                $('#QueryRaisedBy').val(response.queryRaisedBy);
                $('#mdalProject').val($('#rdoval').html());
                $('#Telephone').val(response.telephone);
                $('#ModeOfQuery').val(response.modeOfQuery);
                $('#Reply').val(response.reply);
                $('#modalUnit').val(response.unit);
                $('#exampleModal').modal('show');
            },
            error: function (response) {
            }
        });
    });
});

function ResetData() {
    //$("#Sus_No").val("");
    $('#Query').val("");
    $('#QueryRaisedBy').val("");
    //$('#Project').val("");
    $('#ModeOfQuery').val("Telephone");
    //$('#UNIT').val("");
}

$(document).ready(function () {
    //debugger;
    $(document).on("change", "#mysus", function () {
        var susdata = $("#mysus").val();
        console.log(susdata);
        $.ajax({
            type: 'POST',
            url: '/Hrms/ValidateSus',
            data: { "susdata": susdata },
            success: function (response) {
                if (response == "True") {
                    $("#errormsg").html(" ")
                    $("#btnAdd").removeClass("hide");
                }
                if (response == "False") {
                    $("#errormsg").html("Sus is not valid")
                    //debugger;
                    //$("#SaveBtn").removeClass("hide");
                    $("#btnAdd").addClass("hide");
                }
            },
        });
    });
});

$(document).ready(function () {
    $(document).on('click', '.btnstatus', function () {
        swal("Please select the option", {
            buttons: {
                open: {
                    text: "Open",
                    value: "Open",
                    className: 'sweet-warning'
                },
                close: {
                    text: "Close",
                    value: "Close",
                    className: 'sweet-warning'
                },
                cancel: "Same as given"
            },
        })
            .then((value) => {
                switch (value) {

                    case "Open":
                        swal("The case is Open");
                        break;

                    case "Close":
                        swal("The case is Close");
                        break;
                }
                var data = value;
                var tr = $(this).closest("tr");
                var id = tr.find("td:eq(0)").text();
                //var id = $(this).closest("tr").find("#spnid").html();
                var _status = {};
                _status.Status = data;
                _status.Id = id;
                $.ajax({
                    type: 'POST',
                    url: '/Hrms/SubmitStatus',
                    data: { "id": id, "status": data },
                    success: function (response) {
                        //location.reload(true);
                        //  window.location.reload(true);
                        //window.refresh;
                    }
                });

            });
    });
});

