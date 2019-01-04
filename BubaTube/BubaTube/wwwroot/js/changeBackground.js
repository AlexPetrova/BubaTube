$('body').addClass('background-gray');
document.addEventListener('unload', function () {
    $('body').removeClass('background-gray')

});
$('document').on('unload', function () {
    $('body').removeClass('background-gray')
});