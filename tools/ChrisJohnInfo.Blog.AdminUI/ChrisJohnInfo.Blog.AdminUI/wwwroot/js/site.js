// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
function focusFormElement() {
    var matches = document.getElementsByClassName('focus-me');
    if (matches === undefined || matches.length == 0) return;

    matches[0].focus();
}

$(() => {

    focusFormElement();
    $('.datepicker').datepicker({
        format:'mm/dd/yyyy'
    });
});

