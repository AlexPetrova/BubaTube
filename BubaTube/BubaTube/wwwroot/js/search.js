$(function () {
    $('#search').on('keyup', function (event) {
        if (event.keyCode === 13) {
            var data = $('#search').val();
            var response = searchHelper.search(data);
        }
    });
})

var searchHelper = (function () {
    var searchObj = {};

    searchObj.search = function (data) {
        $.post(
            '/search/search',
            'data=' + data,
            function (response, status) {
                if (status == 200) {
                    return response;
                }
            }
        );
    };

    return searchObj;
})();