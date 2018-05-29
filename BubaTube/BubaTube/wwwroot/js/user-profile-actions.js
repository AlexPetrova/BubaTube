$(function () {
    $('#upload-video').on('click', function (event) {
        $('container').empty(); //not sure if this is working
        event.preventDefault();

        $.get(
            '/upload/uploadVideo',
            function (data) {
                $('#container').html(data);
            }
        )
    });

    $('#container').on('click', 'div form label', function (event) {
        event.preventDefault();
        $('#file').trigger('click');
    });

});