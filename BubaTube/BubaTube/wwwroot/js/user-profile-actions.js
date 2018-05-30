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
    $('#container').on('click', 'div div button', function (event) {
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

    $('#container').on('click', 'div div span', function (event) {
        var liToRemove = $(this).closest('li');
        liToRemove.fadeOut(300, function () { $(this).remove(); });
    });

    //$('#container').on('submit', 'div form', function (event) {
    //    event.preventDefault();
    //    var form = $(this).serialize();
    //    var arrayOfTags = getTags.call(tags);

    //    $.post(
    //        '/upload/test',
    //        { model: form, categories: arrayOfTags },
    //        function () {

    //        }
    //    )

    //    $.ajax({
    //        type: 'POST',
    //        url: '/upload/test',
    //        data: { model: form, categories: arrayOfTags }
    //    });
    //});

    $('#container').on('submit', 'div form', function (event) {
        event.preventDefault();
    });

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
});