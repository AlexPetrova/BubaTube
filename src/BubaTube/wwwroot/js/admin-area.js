$(function () {
    $('#manage-users-tab-last-activity').on('click', function (_) {
        $.get(
            'filterByLastActivity',
            function (data) {
                $('#manage-users-filter-container').html(data);
            }
        );
    });

    $('#manage-users-tab-period').on('click', function (_) {
        $.get(
            'filterByPeriod',
            function (data) {
                $('#manage-users-filter-container').html(data);
            }
        );
    });

    $('#manage-users-filter-container').on('click', '.drop-down-element', function (_) {
        var $this = $(this);
        var dropDownHolder = '#' + $this.closest('div').attr('id');

        var activeItem = $(dropDownHolder + ' ul').find('.drop-down-element.active');
        if (activeItem.length !== 0) {
            activeItem.removeClass('active');
        }

        $this.addClass('active');

        $(dropDownHolder).find('.filter-text').text($this.text().trim());
    });

    $('#manage-users-filter-container').on('click', '#user-activity-filter-button', function (_) {
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

    $('#manage-users-filter-container').on('click', '#period-filter-button', function (_) {

    });
});