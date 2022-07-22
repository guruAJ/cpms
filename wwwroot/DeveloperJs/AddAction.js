$(document).ready(function () {
    $(".sel").change(function () {
        var selValue = $(this).text();
        alert(selValue);
    });
});