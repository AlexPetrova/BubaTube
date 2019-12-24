$(function () {

    $('#search').on('keyup', function (event) {
        if (event.keyCode === 13) {
            var data = $('#search').val();
            var response = searchHelper.search(data);
            searchHelper.buildSearchResultsBox(response);
        }
    });
})

var searchHelper = (function () {
    var searchHelper = {};

    searchHelper.search = function (data) {
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

    searchHelper.buildSearchResultsBox = function (data) {
        //TODO add the results in the partialView
        $.get(
            '/search/searchresultsbox',
            function (result) {
                $('#search-container').append(result);
            });
    }

    return searchHelper;
})();