(function (comdy, $) {
    var settings = {};

    comdy.admin = comdy.admin || {};

    comdy.admin.init = function (options) {
        comdy.admin.setOptions(options);
    };

    comdy.admin.setOptions = function (options) {
        settings = $.extend(settings, options);
    };

    comdy.admin.myProfilePopup = function () {
        comdy.common.get(settings.myProfileUrl, {}, function (response) {
            if (response !== null) {
                comdy.common.showModal(response, "Tài khoản của tôi", "modal-sm", "");
            } else {
                comdy.common.errorNotify("Đã có lỗi xảy ra");
            }
        });
    };

    comdy.admin.updateAvatar = function (element) {
        comdy.admin.showMediaPopup(1, function () {
            var src = $(".thumbnail-selected img").attr("src");
            if (src !== null) {
                $(element).attr("src", src);
                $("#navbar-user-avatar").attr("src", src);
                comdy.common.hideModal("#media-modal");
                comdy.common.post(settings.updateAvatarUrl, { avatarUrl: src });
            } else {
                comdy.common.errorNotify("Vui lòng chọn hình ảnh");
            }
        });
    };

    comdy.admin.changePasswordPopup = function () {
        comdy.common.get(settings.changePasswordUrl, {}, function (response) {
            if (response !== null) {
                comdy.common.showModal(response, "Đổi mật khẩu", "modal-sm", '<button type="button" class="btn btn-primary" onclick="comdy.admin.changePassword()"><i class="icon-floppy-disk"></i>&nbsp;Lưu</button>');
            } else {
                comdy.common.errorNotify("Đã có lỗi xảy ra");
            }
        });
    };

    comdy.admin.changePassword = function () {
        var $form = $(settings.changePasswordForm);
        comdy.common.hideValidateMessages($form);
        comdy.common.postForm(settings.changePasswordUrl, settings.changePasswordForm, function (response) {
            if (response.success) {
                comdy.common.successNotify(response.message);
                comdy.common.hideModal('#modal');
            } else {
                comdy.common.showValidateMessages($form, response.message);
            }
        });
    };

    comdy.admin.showMediaPopup = function (type, callback) {
        comdy.common.get(settings.getMediaPopupUrl, {type: type}, function (response) {
            comdy.common.showMediaModal(response, '<button id="btn-media-selected" type="button" class="btn btn-primary"><i class="fa fa-save"></i> OK</button>');

            $('#load-more-media button').click(function () {
                comdy.admin.loadMoreMedia();
            });

            $('#btn-media-selected').click(function () {
                callback();
            });

            $("#formSearchMedia #Type").change(function () {
                $("#dropzone #Entity_Type").val($(this).val());
            });

            $(".thumbnail").click(function () {
                $('.thumbnail-selected').removeClass('thumbnail-selected'); // removes the previous selected class
                $(this).addClass('thumbnail-selected'); // adds the class to the clicked image
            });

            $("#dropzone").dropzone({
                paramName: "file",
                maxFiles: 10,
                maxFilesize: 2,
                acceptedFiles: ".jpeg,.jpg,.png,.gif",
                dictFileTooBig: 'Kích thước file hình ảnh không được vượt quá 2MB',
                dictDefaultMessage: 'Kéo file vào đây để upload <span>hoặc CLICK</span>',
                init: function () {
                    this.on("complete", function (file) {
                        if (this.getUploadingFiles().length === 0 && this.getQueuedFiles().length === 0) {
                            comdy.admin.searchMedia(1);
                            $('#section-upload').addClass("hidden");
                        }
                    });
                    this.on("error", function (file, response) {
                        comdy.common.errorNotify(response);
                    });
                }
            });
        });
    };

    comdy.admin.insertImage = function (type) {
        comdy.admin.showMediaPopup(type, function () {
            var src = $(".thumbnail-selected img").attr("src");

            if (src !== null) {
                var fileName = src.substring(src.lastIndexOf('/') + 1);
                $('.summernote').summernote('insertImage', src, fileName);
                comdy.common.hideModal("#media-modal");
            } else {
                comdy.common.errorNotify("Vui lòng chọn hình ảnh");
            }
        });
    };

    comdy.admin.showUploadSection = function () {
        $('#section-upload').removeClass("hidden");
    };

    comdy.admin.selectImage = function (element) {
        $('.thumbnail-selected').removeClass('thumbnail-selected');
        $(element).addClass('thumbnail-selected');
    };

    comdy.admin.searchMedia = function (page) {
        $("#media-modal #Paging_PageNumber").val(page || 1);
        var data = {
            "Type": $("#formSearchMedia #Type").val(),
            "Name": $("#formSearchMedia #Name").val(),
            "Paging": {
                "PageSize": $("#media-modal #Paging_PageSize").val(),
                "PageNumber": $("#media-modal #Paging_PageNumber").val()
            }
        };

        comdy.common.post(settings.searchMediaUrl, data, function (response) {
            if (page === 1) {
                $("#media-result").html(response);
                $('.thumbnail-selected').removeClass('thumbnail-selected');
                $('#media-result .thumbnail').first().addClass('thumbnail-selected');
                $("#load-more-media").removeClass("hidden");
                $(".thumbnail").click(function () {
                    $('.thumbnail-selected').removeClass('thumbnail-selected'); // removes the previous selected class
                    $(this).addClass('thumbnail-selected'); // adds the class to the clicked image
                });
            } else {
                console.log(response);
                if (response !== null && response !== "") {
                    $("#media-result").append(response);
                    $("#load-more-media").removeClass("hidden");
                    $(".thumbnail").click(function () {
                        $('.thumbnail-selected').removeClass('thumbnail-selected'); // removes the previous selected class
                        $(this).addClass('thumbnail-selected'); // adds the class to the clicked image
                    });
                } else {
                    $("#load-more-media").addClass("hidden");
                }
            }
        });
    };

    comdy.admin.loadMoreMedia = function () {
        var page = parseInt($("#media-modal #Paging_PageNumber").val()) + 1;
        comdy.admin.searchMedia(page);
    };

    comdy.admin.createTicketPopup = function () {
        comdy.common.get(settings.createTicketUrl, {}, function (response) {
            if (response !== null) {
                comdy.common.showModal(response, "Yêu cầu hỗ trợ", "modal-lg", '<button type="button" class="btn btn-primary" onclick="comdy.admin.createTicket()"><i class="fa fa-save"></i>&nbsp;Lưu</button>');
            } else {
                comdy.common.errorNotify("Đã có lỗi xảy ra");
            }
        });
    };

    comdy.admin.createTicket = function () {
        var $form = $(settings.formTicket);
        comdy.common.hideValidateMessages($form);
        comdy.common.postForm(settings.saveTicketUrl, settings.formTicket, function (response) {
            if (response.success) {
                comdy.common.successNotify(response.message);
                comdy.common.hideModal('#modal');
            } else {
                comdy.common.showValidateMessages($form, response.message);
            }
        });
    };
}(window.comdy = window.comdy || {}, jQuery));
