$(function () {
    $('#user-activity-filter-button').on('click', function (event) {
        var url = 'filter/' + $('.drop-down-element.active').first().data("months");
        
        $.get(
            url,
            function (data) {
                console.log(data);
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
});