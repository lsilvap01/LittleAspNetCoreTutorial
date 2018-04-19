// Write your JavaScript code.
function addItem() {
 
    $('#add-item-error').hide();
    var newTitle = $('#add-item-title').val();
    var dueAt = $('#add-item-date').val() != "" ? $('#add-item-date').val() : null;
    $.post('/Todo/AddItem', { title: newTitle, dueAt: dueAt }, function () {
        window.location = '/Todo';
    })
    .fail(function (data) {
        if (data && data.responseJSON) {
            var firstError = data.responseJSON[Object.keys(data.responseJSON)[0]];
            $('#add-item-error').text(firstError);
            $('#add-item-error').show();
        }
    });

    return false;
}

function markCompleted(checkbox) {
    checkbox.disabled = true;
    $.post('/Todo/MarkDone', { id: checkbox.name }, function () {
        var row = checkbox.parentElement.parentElement;
        $(row).addClass('done');
    });
}

$(document).ready(function () {
    // Wire up the Add button to send the new item to the server
    $('#todoForm').on('submit', addItem);
    $('.done-checkbox').on('click', function (e) {
        markCompleted(e.target);
    })
});