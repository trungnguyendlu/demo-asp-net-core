(function (comdy, $) {
    var settings = {};

    comdy.widgetdetail = comdy.widgetdetail || {};

    comdy.widgetdetail.init = function (options) {
        comdy.widgetdetail.setOptions(options);
        //switch
        $('.switchery').each(function (index, item) {
            var switchery = new Switchery(item);
        });

        //editor
        $('.summernote').summernote({
			lang: 'vi-VN',
            height: 420,
            imageTitle: {
                specificAltField: true,
            },
            toolbar: [
                ['style', ['style', 'bold', 'italic', 'underline', 'clear']],
                ['fontname', ['fontname','fontsize','color']],
                ['para', ['ul', 'ol', 'paragraph']],
                ['height', ['height', 'table']],
                ['insert', ['link', 'video', 'hr']],
                ['view', ['fullscreen', 'codeview']],
                ['help', ['help']]
            ],
            popover: {
                image: [
                    ['imagesize', ['imageSize100', 'imageSize50', 'imageSize25']],
                    ['float', ['floatLeft', 'floatRight', 'floatNone']],
                    ['remove', ['removeMedia']],
                    ['custom', ['imageTitle']]
                ]
            }
        });
    };

    comdy.widgetdetail.setOptions = function (options) {
        settings = $.extend(settings, options);
    };
    
    comdy.widgetdetail.save = function () {
        var html = $('.summernote').summernote('code');
        $(settings.content).val(html);
        var $form = $(settings.saveForm);
        comdy.common.hideValidateMessages($form);
        comdy.common.postForm(settings.saveUrl, settings.saveForm, function (response) {
            if (response.success) {
                if (!settings.isEdit) {
                    comdy.common.gotoUrl(settings.editUrl);
                } else {
                    comdy.common.successNotify(response.message);
                }
            } else {
                comdy.common.showValidateMessages($form, response.message);
            }
        });
    };
}(window.comdy = window.comdy || {}, jQuery));
