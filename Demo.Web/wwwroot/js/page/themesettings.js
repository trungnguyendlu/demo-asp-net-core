(function (comdy, $) {
    var settings = {};

    comdy.themesettings = comdy.themesettings || {};

    comdy.themesettings.init = function (options) {
        comdy.themesettings.setOptions(options);

        // Init nicescroll when sidebar affixed
        $('.sidebar-detached').on('affix.bs.affix', function () {
            initScroll();
            resizeDetached();
        });

        // When effixed top, remove scrollbar and its data
        $('.sidebar-detached').on('affix-top.bs.affix', function () {
            removeScroll();

            $(".sidebar-detached .sidebar").removeAttr('style').removeAttr('tabindex');
        });

        // Attach BS affix component to the sidebar
        $('.sidebar-detached').affix({
            offset: {
                top: $('.sidebar-detached').offset().top - 20 // top offset - computed line height
            }
        });


        // Remove affix and scrollbar on mobile
        $(window).on('resize', function () {
            setTimeout(function () {
                if ($(window).width() <= 768) {

                    // Remove nicescroll on mobiles
                    removeScroll();

                    // Remove affix on mobile
                    $(window).off('.affix')
                    $('.sidebar-detached').removeData('affix').removeClass('affix affix-top affix-bottom');
                }
            }, 100);
        }).resize();
    };

    comdy.themesettings.setOptions = function (options) {
        settings = $.extend(settings, options);
    };

    comdy.themesettings.save = function () {
        var $form = $(settings.saveForm);
        comdy.common.hideValidateMessages($form);
        comdy.common.postForm(settings.saveUrl, settings.saveForm, function (response) {
            if (response.success) {
                comdy.common.successNotify(response.message);
            } else {
                comdy.common.showValidateMessages($form, response.message);
            }
        });
    };


    comdy.themesettings.showMediaPopup = function (callback) {
        comdy.common.get(settings.getMediaPopupUrl, {}, function (response) {
            comdy.common.showMediaModal(response, '<button id="btn-media-selected" type="button" class="btn btn-primary"><i class="fa fa-save"></i> OK</button>');

            $('#load-more-media button').click(function () {
                comdy.themesettings.loadMoreMedia();
            });

            $('#btn-media-selected').click(function () {
                callback();
            })
            $(".thumbnail").click(function () {
                $('.thumbnail-selected').removeClass('thumbnail-selected'); // removes the previous selected class
                $(this).addClass('thumbnail-selected'); // adds the class to the clicked image
            });

            $("#dropzone").dropzone({
                paramName: "file",
                maxFilesize: 2,
                maxFiles: 1,
                acceptedFiles: 'image/*',
                dictDefaultMessage: 'Kéo file vào đây để upload <span>hoặc CLICK</span>',
                init: function () {
                    this.on("complete", function (file) {
                        if (this.getUploadingFiles().length === 0 && this.getQueuedFiles().length === 0) {
                            comdy.themesettings.searchMedia(1);
                            $('.nav-tabs a[href="#tab-media"]').tab('show');
                        }
                    });
                    this.on("error", function (file, response) {
                        comdy.common.errorNotify(response);
                    });
                }
            });
        });
    };

    comdy.themesettings.searchMedia = function (page) {
        $("#Paging_PageNumber").val(page || 1);
        var data = {
            "Name": $("#Name").val(),
            "Paging": {
                "PageSize": $("#Paging_PageSize").val(),
                "PageNumber": $("#Paging_PageNumber").val()
            }
        };

        comdy.common.post(settings.searchMediaUrl, data, function (response) {
            if (page === 1) {
                $("#media-result").html(response);
            } else {
                console.log(response);
                if (response !== null && response !== "") {
                    $("#media-result").append(response);
                } else {
                    $("#load-more-media").addClass("hidden");
                }
            }
        });
    };

    comdy.themesettings.loadMoreMedia = function () {
        var page = parseInt($("#Paging_PageNumber").val()) + 1;
        comdy.themesettings.searchMedia(page);
    };

    comdy.themesettings.insertImage = function (element) {
        comdy.themesettings.showMediaPopup(function () {
            var activeTab = $('li.active a.maintab').attr("href");
            var src = null;
            switch (activeTab) {
                case "#tab-internet":
                    src = $("#InternetImageUrl").val();
                    break;
                default:
                    src = $(".thumbnail-selected img").attr("src");
                    break;
            }

            if (src !== null) {
                $(element).val(src);
                comdy.common.hideModal("#media-modal");
            } else {
                comdy.common.errorNotify("Vui lòng chọn hình ảnh");
            }
        });
    };



    // Nice scroll
    // ------------------------------

    // Setup
    function initScroll() {
        $(".sidebar-detached .sidebar").niceScroll({
            mousescrollstep: 100,
            cursorcolor: '#ccc',
            cursorborder: '',
            cursorwidth: 3,
            hidecursordelay: 100,
            autohidemode: 'scroll',
            horizrailenabled: false,
            preservenativescrolling: false,
            railpadding: {
                right: 0.5,
                top: 1.5,
                bottom: 1.5
            }
        });
    }

    // Resize
    function resizeScroll() {
        $('.sidebar-detached .sidebar').getNiceScroll().resize();
    }

    // Remove
    function removeScroll() {
        $(".sidebar-detached .sidebar").getNiceScroll().remove();
        $(".sidebar-detached .sidebar").removeAttr('style').removeAttr('tabindex');
    }


    // Resize sidebar on scroll
    // ------------------------------

    // Resize detached sidebar vertically when bottom reached
    function resizeDetached() {
        $(window).on('load scroll', function () {
            if ($(window).scrollTop() > $(document).height() - $(window).height() - 60) {
                $('.sidebar-detached').addClass('fixed-sidebar-space');
                resizeScroll();
            }
            else {
                $('.sidebar-detached').removeClass('fixed-sidebar-space');
                resizeScroll();
            }
        });
    }

}(window.comdy = window.comdy || {}, jQuery));
