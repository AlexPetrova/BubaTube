$(function () {
    $('#user-activity-filter-button').on('click', function (event) {
        var months = $('.drop-down-element.active').first().data("months");
        if (months === undefined) {
            $('#user-activity-filter-dropdown').effect('bounce');
            return;
        }

        var url = 'filterByLastActivity/' + $('.drop-down-element.active').first().data("months");
        $.get(
            url,
            function (data) {
                $('#user-activity-filter-result').html(data);
            }
        );
    });

    $('.drop-down-element').click(function (e) {
        $('.drop-down-element.active')
            .toArray()
            .forEach(element => element.classList.remove('active'));

        this.classList.add('active');

        $('#user-activity-filter-period-text').first().text(this.textContent.trim())
    });

    $('#manage-users-tab-last-activity').click(function (e) {
        console.log("bau");
    });

    $('#manage-users-tab-period').click(function (e) {
        console.log("bau");
    });

    $('#manage-users-tab-for-more-than').click(function (e) {
        console.log("bau");
    });
});