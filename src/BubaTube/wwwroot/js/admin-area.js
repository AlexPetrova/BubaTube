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

        var url = 'filterByLastActivity/' + months;
        $.get(
            url,
            function (data) {
                $('#user-activity-filter-result').html(data);
            }
        );
    });

    $('#manage-users-filter-container').on('click', '#user-activity-from-to-filter-button', function (_) {
        var from = $('#period-filter-from-dropdown').find('.drop-down-element.active');
        var to = $('#period-filter-to-dropdown').find('.drop-down-element.active');

        if (from.length === 0 && to.length === 0) {
            $('#from-dropdown-container').effect('bounce');
            $('#to-dropdown-container').effect('bounce');
            return;
        }
        if (from.length === 0) {
            $('#from-dropdown-container').effect('bounce');
            return;
        }
        if (to.length === 0) {
            $('#to-dropdown-container').effect('bounce');
            return;
        }

        var url = `filterPerPeriod/${from.data('date')}/${to.data('date')}`;
        $.get(
            url,
            function (data) {
                $('#user-activity-filter-result').html(data);
            }
        );
    });

    $('.video-controls').on('click', '#delete-video', function (_) {
        var id = $(this).data('id');
        $.ajax({
            method: 'delete',
            url: `delete/${id}`
        }).done(function (response) {
            console.log(response);
        });

        $(`#video-${id}`).css('border', '2px solid red');
    });

    $('.video-controls').on('click', '#approve-video', function (_) {
        var id = $(this).data('id');
        $.ajax({
            method: 'put',
            url: `approve/${id}`
        }).done(function (response) {
            console.log(response);
        });

        $(`#video-${id}`).css('border', '2px solid green');
    });
});