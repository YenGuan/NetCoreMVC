// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/*modal*/
$(function () {
    // boostrap 4 load modal example from docs
    $('.modal-container').on('show.bs.modal', function (event) {

        var button = $(event.relatedTarget); // Button that triggered the modal

        var url = button.attr("href");

        var modal = $(this);

        // note that this will replace the content of modal-content everytime the modal is opened
        modal.find('.modal-content').load(url);

    });

    $('.modal-container').on('hidden.bs.modal', function () {

        // remove the bs.modal data attribute from it

        $(this).removeData('bs.modal');

        // and empty the modal-content element

        //$('.modal-container .modal-content').empty();

        $(this).find('.modal-content').empty();

    });


    $(".toggle-menu-size-btn").click(function () {
        $("body").toggleClass("mini-side-menu");
    })
});

$(".had-sub-menu").click(function () {
    $(this).toggleClass("active");
});

//Multiple modals overlay z-index fix
$(document).on('show.bs.modal', '.modal', function () {
    var zIndex = 1040 + (10 * $('.modal:visible').length);
    $(this).css('z-index', zIndex);
    setTimeout(function () {
        $('.modal-backdrop').not('.modal-stack').css('z-index', zIndex - 1).addClass('modal-stack');
    }, 0);
});
/*modal end*/
function isHTML(str) {
    var a = document.createElement('div');
    a.innerHTML = str;

    for (var c = a.childNodes, i = c.length; i--;) {
        if (c[i].nodeType == 1) return true;
    }

    return false;
}

