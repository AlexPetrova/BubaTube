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

    //tags
    $('#container').on('click', 'div form div div button', function (event) {
        event.preventDefault();
        var tag = $('#tag-field');

        if (tag.val() !== '') {
            $('#tags').append('<li>#' + tag.val() + '<span class="close-tag" role="button">x</span>' + '</li>');
        }
        else {
            $(tag).effect('bounce', 'slow');
        }
        tag.val('');
    });

    $('#container').on('click', 'div form div div ul li span', function (event) {
        var liToRemove = $(this).closest('li');
        liToRemove.fadeOut(300, function () { $(this).remove(); });
    });
});