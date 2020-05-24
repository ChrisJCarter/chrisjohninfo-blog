function focusFormElement() {
    var matches = document.getElementsByClassName('focus-me');
    if (matches === undefined || matches.length == 0) return;

    matches[0].focus();
}

$(() => {

    focusFormElement();
    $('.datepicker').datepicker({
        format: 'mm/dd/yyyy'
    });
});