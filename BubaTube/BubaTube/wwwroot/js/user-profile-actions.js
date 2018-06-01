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

    $('#container').on('click', 'div form label', function (event) {
        event.preventDefault();
        $('#file').trigger('click');
    });

    //tags
    function getTags() {
        var collection = new Array();

        var tags = $('#tags').each(function () {
            var htmlCollection = this.children;
            for (var i = 0; i < htmlCollection.length; i++) {
                collection.push(htmlCollection[i].textContent);
            }
        });

        return collection;
    }

    $('#container').on('keydown', 'div div input', function (event) {
        if(event.keyCode === 13){
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

    $('#container').on('click', 'div div span', function (event) {
        var liToRemove = $(this).closest('li');
        liToRemove.fadeOut(300, function () { $(this).remove(); });
    });

    //upload form
    $('#container').on('submit', 'div form', function (event) {
        event.preventDefault();
        var data = $('#upload-files').serializeArray();
        var tag = $('#tag-field');
        var arrayOfTags = getTags.call(tags);
        var file = $('#file').get(0).files[0];

        data.push({ name: 'categories', value: arrayOfTags });
        data.push({ name: 'video', value: file });
        
        $.post(
            '/upload/test',
            data,
            function (response) {
                $('#modal').replaceWith(response);
            }
        )
    });
});