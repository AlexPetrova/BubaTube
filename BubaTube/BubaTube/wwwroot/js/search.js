(function () {

    $('#search').on('click', function () {
        $.post(
            '/search/search'
        )
    });

    $('#search').on('keydown', function (event) {
        if (event.keyCode === 13) {
            var value = $('#search').val();
        }
    });

})