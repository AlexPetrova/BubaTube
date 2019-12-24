$(function () {
    $('#modal').on('DOMNodeInserted DOMNodeRemoved', function () {
        $('#modal').css('display', 'block');
    });

    $('.close').on('click', function () {
        $('#modal').css('display', 'none');
    });

    $(window).on('click', function (event) {
        var modal = $('#modal');
        if (event.target == modal) {
            $('#modal').css('display', 'none');
        }
    });
});

