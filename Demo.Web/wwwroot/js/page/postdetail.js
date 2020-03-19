(function (comdy, $) {
    var settings = {};

    comdy.postdetail = comdy.postdetail || {};

    comdy.postdetail.init = function (options) {
        comdy.postdetail.setOptions(options);

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

        //switch
        $('.switchery').each(function (index, item) {
            var switchery = new Switchery(item);
        });

        //generate slug
        $(settings.title).focusout(function () {
            var title = $(settings.title).val();
            if (title !== '' && $(settings.slug).val() === '') {
                $(settings.slug).val(comdy.common.slugGenerate(title));
            }
        });
    };

    comdy.postdetail.setOptions = function (options) {
        settings = $.extend(settings, options);
    };

    comdy.postdetail.save = function () {
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
                comdy.common.errorNotify(response.message);
            }
        });
    };

    comdy.postdetail.removeMedia = function (current, element) {
        $(current).addClass("hidden");
        $(element).attr("src", settings.placeholder).attr("alt", "");
        $(element).prev().val(settings.placeholder);
        $(element).prev().prev().val("");
    };

    comdy.postdetail.setPhoto = function (img) {
        comdy.admin.showMediaPopup(6, function () {
            var src = $(".thumbnail-selected img").attr("src");
            if (src !== null) {
                $(img).attr("src", src);
                $(img).prev().val(src);
                comdy.common.hideModal("#media-modal");
            } else {
                comdy.common.errorNotify("Vui lòng chọn hình ảnh");
            }
        });
    };

}(window.comdy = window.comdy || {}, jQuery));
