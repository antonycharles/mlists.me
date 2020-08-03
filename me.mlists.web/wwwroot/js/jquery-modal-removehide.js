$(document).on('hide.bs.modal', '.modal.remove-hide', function (e) {
    $(e.target).data('modal', null);
    $(e.target).remove();
});