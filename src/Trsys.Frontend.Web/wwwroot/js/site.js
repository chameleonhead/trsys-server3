// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$("form[data-trsys-ajax=true]").each(function () {
    var $this = $(this);
    var validator = $this.validate({
        errorClass: 'invalid-feedback',
        errorElement: 'div',
        highlight: function (element, errorClass, validClass) {
            $(element).addClass("is-invalid");
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass("is-invalid");
        }
    });
    $this.on('submit', function (e) {
        e.preventDefault();
        //$this.trigger('submit.trsys.ajax');
        $.ajax({
            type: $this.attr('method') || 'get',
            url: $this.attr('action'),
            data: new FormData($this[0]),
            contentType: false,
            processData: false
        }).done(function (data) {
            $this.trigger('success.trsys.ajax', data);
        }).fail(function (err) {
            if (err.status == 400) {
                validator.showErrors(err.responseJSON);
            } else {
                console.error(err);
                $this.trigger('error.trsys.ajax', err.responseJSON);
            }
        });
    });
});

$('.modal').on('show.bs.modal', function () {
    var $this = $(this);
    var $form = $this.find('form');
    $form.each(function () {
        this.reset();
    });
    $form.validate().resetForm();
    $form.find('.is-invalid').removeClass('is-invalid');
});