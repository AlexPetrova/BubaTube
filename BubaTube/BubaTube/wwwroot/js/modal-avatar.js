$(function () {
    $('#upload-image').on('submit', function (event) {
        event.preventDefault();

    });
    
    $('#upload-image').on('click', function () {
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

    $('#upload').on('click', function () {
        $('#file').trigger('click');
    });

    function readURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#image_upload_preview').attr('src', e.target.result);
            }

            reader.readAsDataURL(input.files[0]);
        }
    }

    $('#file').on('change', function (label) {
        readURL(this);
    });
});