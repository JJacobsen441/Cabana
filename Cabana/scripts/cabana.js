$(document).ready(function () {
    $('.genres').click(function () {
        $('.votes').prop("checked", false);
    });
    $('.votes').click(function () {
        $('.genres').prop("checked", false);
    });
});