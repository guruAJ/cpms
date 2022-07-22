$(document).ready(function () {
    $("#SSearch").click(function () {
        $("#table_id1").removeClass('hide');
        $("#table_id2").addClass('hide');
        $("#table_id3").addClass('hide');
        $("#hed1").addClass('hide');
        $("#hed2").addClass('hide');
        $("#wrapA").removeClass('wrapHeight');
        var project = $("#proj").val();
        var status = $("#projstatus").val();
        var listdata = "";
        $.ajax({
            type: 'POST',
            //url: '/Hrms/UnitSearch',
            url: '/Hrms/UnitSearchForStatus',
            data: { "status": status, "project": project },

            success: function (response) {
                var srl = 1;
                for (var i = 0; i < response.length; i++) {
                    listdata += '<tr>';
                    listdata += '<td style="text-align:center;">' + srl + '</td>;'
                    listdata += '<td style="text-align:center;">' + response[i].sus + '</td>;'
                    listdata += '<td style="text-align:center;padding-left:0px;padding-right:0px;width:340px;">' + response[i].unit + '</td>;'

                    if (project == "1") {
                        $("#qqqq").hide();
                        $("#hhhh").show();
                        if (response[i].hrms == "1") {
                            listdata += '<td style="width: 332px;text-align:center;">' + "Not Installed" + '</td>;'
                        }
                        else if (response[i].hrms == "2") {
                            listdata += '<td style="width: 332px;text-align:center;">' + "Completely Not Functional" + '</td>;'
                        }
                        else if (response[i].hrms == "3") {
                            listdata += '<td style="width: 332px;text-align:center;">' + "Partially Functional" + '</td>;'
                        }
                        else if (response[i].hrms == "4") {
                            listdata += '<td style="width: 332px;text-align:center;">' + "Fully Functional" + '</td>;'
                        }
                        listdata += '<td style="display:none">' + "" + '</td>;'
                    }
                    else if (project == "2") {
                        $("#hhhh").hide();
                        $("#qqqq").show();
                        if (response[i].iqmp == "1") {
                            listdata += '<td style="width: 468px;text-align:center;">' + "Not Installed" + '</td>;'
                        }
                        else if (response[i].iqmp == "2") {
                            listdata += '<td style="width: 468px;text-align:center;">' + "Completely Not Functional" + '</td>;'
                        }
                        else if (response[i].iqmp == "3") {
                            listdata += '<td style="width: 468px;text-align:center;">' + "Partially Functional" + '</td>;'
                        }
                        else if (response[i].iqmp == "4") {
                            listdata += '<td style="width: 468px;text-align:center;">' + "Fully Functional" + '</td>;'
                        }
                        listdata += '<td style="display:none">' + "" + '</td>;'
                    }

                    //listdata += '<td>' + response[i].hrms + '</td>;'
                    //listdata += '<td>' + response[i].iqmp + '</td>;'
                    /*listdata += '<td><input type="button" value="Edit" class="btnedt btn btn-primary"></td>'*/
                    listdata += '</tr>';
                    srl = srl + 1;
                }
                $('#tbldata1').html(listdata);
                $('#table_id1').DataTable({
                    retrieve: true,
                    lengthChange: true,
                    ordering: true,
                    "order": [[0, 'asc']],
                    "bInfo": true,
                });
            },
            error: function (response) {
            }
        });
    });
});