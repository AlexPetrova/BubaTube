$(function () {
    $('#upload-video').on('click', function (event) {
        $('container').empty();
        event.preventDefault();

        $.get(
            '/upload/uploadVideo',
            function (data) {
                $('#container').html(data);
            }
        )
    });

    $('#container').on('click', '#upload', function (event) {
        event.preventDefault();
        $('#file').trigger('click');
    });

    $('#container').on('change', '#file', function () {
        var filename = $('input[type=file]').val().split('\\').pop();
        $('#name-of-video').html(filename);
    });

    //tags
    function getTags() {
        var collection = new Array();

        var tags = $('#tags').each(function () {
            var htmlCollection = this.children;

            for (var i = 0; i < htmlCollection.length; i++) {
                var text = htmlCollection[i].textContent.substring(1, htmlCollection[i].textContent.length - 1);

                collection.push(text);
            }
        });

        return collection;
    }

    $('#container').on('keydown', '#tag-field', function (event) {
        if (event.keyCode === 13) {
            event.preventDefault();
            var tag = $('#tag-field');

            if (tag.val() !== '') {
                $('#tags').append('<li>#' + tag.val() + '<span class="close-tag" role="button">x</span>' + '</li>');
            }
            else {
                $(tag).effect('bounce', 'slow');
            }

            tag.val('');
        }
    });

    $('#container').on('click', '.close-tag', function (event) {
        var liToRemove = $(this).closest('li');
        liToRemove.fadeOut(300, function () { $(this).remove(); });
    });

    //upload form
    function validateForm() {
        var video = $('#file');
        
        var titleField = $('#title').val();
        if (titleField.length < 5 || titleField.length > 200) {
            var msg ='The title of the video cannot be more than 200 and less than 5 symbols.';
            $('#title-validation').html(msg);
        }

        var descriptionField = $('#description').val();
        if (descriptionField.length === 0) {
            var msg = 'Put description in there.';
            $('#description-validation').html(msg);
        }

        var tagsCount = $('#tags').children().length;
        if (tagsCount === 0) {
            var msg = 'No tags ? :(';
            $('#tag-section-validation').html(msg);
        }
    }

    $('#container').on('submit', '#upload-files', function (event) {
        event.preventDefault();

        validateForm();

        var arrayOfTags = getTags();
        var file = $('#file').get(0).files[0];

        var data = new FormData(this);
        data.append('video', file);
        data.append('categories', arrayOfTags);

        $.ajax({
            url: '/upload/post',
            type: 'POST',
            success: function (response) {
                // your code after succes
            },
            data: data,
            cache: false,
            contentType: false,
            processData: false
        });
    });
});