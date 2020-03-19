(function (comdy, $) {
    var settings = {};
    var currentElement = null;
    var isDirty = false;
    var isPublish = false;

    comdy.customize = comdy.customize || {};

    comdy.customize.init = function (options) {
        comdy.customize.setOptions(options);
        $('body').toggleClass('sidebar-xs');
        init();
    };

    comdy.customize.setOptions = function (options) {
        settings = $.extend(settings, options);
    };

    comdy.customize.showSeoSettingsPopup = function () {
        comdy.common.get(settings.getSeoSettingsUrl, { id: settings.id, themeId: settings.themeId }, function (response) {
            if (!comdy.common.hasProp(response, "success")) {
                comdy.common.showModal(response, "Cấu hình SEO cho trang", "", '<button type="button" class="btn btn-primary" onclick="comdy.customize.saveSeoSettings()"><i class="fa fa-save"></i>&nbsp;Lưu</button>');
            } else {
                comdy.common.errorNotify(response.message);
            }
        });
    };

    comdy.customize.saveSeoSettings = function () {
        comdy.common.confirm("Bạn có chắc chắn muốn Lưu cấu hình SEO của trang?", function () {
            var $form = $("#formSeoSettings");
            comdy.common.hideValidateMessages($form);
            comdy.common.postForm(settings.saveSeoSettingsUrl, "#formSeoSettings", function (response) {
                if (response.success) {
                    comdy.common.hideModal();
                    comdy.common.successNotify(response.message);
                } else {
                    comdy.common.showValidateMessages($form, response.message);
                }
            });
        });
    };

    comdy.customize.save = function () {
        $("#design-frame").contents().find(".comdyempty").each(function (index, item) {
            $(item).html("");
        });

        $("#design-frame").contents().find("body").children(":not(.comdysection)").each(function (index, item) {
            if (item.tagName.toLowerCase() !== "script") {
                $(item).remove();
            }
        });

        var html = $("#design-frame").contents().find("html").html();
        comdy.common.post(settings.saveUrl, { Id: settings.id, ThemeId: settings.themeId, Html: html }, function (response) {
            if (response.success === true) {
                comdy.common.successNotify(response.message);
                isDirty = false;
                isPublish = true;
                canSave();
                canPublish();
                reload();
            } else {
                comdy.common.errorNotify(response.message);
            }
        });
    };

    comdy.customize.publish = function () {
        comdy.common.confirm("Bạn có chắc chắn muốn Xuất bản trang?", function () {
            $("#design-frame").contents().find(".comdyempty").each(function (index, item) {
                $(item).html("");
            });
            $("#design-frame").contents().find("body").children(":not(.comdysection)").each(function (index, item) {
                $(item).remove();
            });
            $("#design-frame").contents().find(".comdydisablelink").each(function (index, item) {
                $(item).removeClass("comdydisablelink");
            });
            $("#design-frame").contents().find(".comdylink").each(function (index, item) {
                $(item).removeClass("comdylink");
            });
            $("#design-frame").contents().find(".comdytext").each(function (index, item) {
                $(item).removeClass("comdytext");
            });
            $("#design-frame").contents().find(".comdyinput").each(function (index, item) {
                $(item).removeClass("comdyinput");
            });
            $("#design-frame").contents().find(".comdyicon").each(function (index, item) {
                $(item).removeClass("comdyicon");
            });
            $("#design-frame").contents().find(".comdyimage").each(function (index, item) {
                $(item).removeClass("comdyimage");
            });
            $("#design-frame").contents().find(".comdybackground").each(function (index, item) {
                $(item).removeClass("comdybackground");
            });
            $("#design-frame").contents().find(".comdybutton").each(function (index, item) {
                $(item).removeClass("comdybutton");
            });
            $("#design-frame").contents().find(".comdysection").each(function (index, item) {
                $(item).removeClass("comdysection");
            });

            var html = $("#design-frame").contents().find("body").html();
            html = html.replace(/\t/ig, "").replace(/\n/ig, "").replace(/\s\s+/ig, "");
            comdy.common.post(settings.publishUrl, { Id: settings.id, ThemeId: settings.themeId, Html: html }, function (response) {
                if (response.success === true) {
                    comdy.common.successNotify(response.message);
                    isPublish = false;
                    canPublish();
                    reload();
                } else {
                    comdy.common.errorNotify(response.message);
                }
            });
        });
    };

    comdy.customize.deleteElement = function () {
        comdy.common.confirm("Bạn có chắc chắn muốn Xóa phần tử này?", function () {
            $(currentElement).remove();
            isDirty = true;
            canSave();
            comdy.common.hideModal();
        });
    }

    var reload = function () {
        document.getElementById('design-frame').contentWindow.location.reload();
        $("#design-frame").on("load", function () {
            init();
        })
    }

    var init = function () {
        initDisableLink();
        initLink();
        initButton();
        initImage();
        initBackground();
        initInput();
        initText();
    };

    var initDisableLink = function () {
        $("#design-frame").contents().find(".comdydisablelink").bind("click", function (e) {
            e.preventDefault();
            e.stopPropagation();
            return false;
        });
    };


    /* --------------------------------------------------------------------
     * Link Editor 
     * -------------------------------------------------------------------- */

    var initLink = function () {
        $("#design-frame").contents().find(".comdylink").bind("click", function (e) {
            e.preventDefault();
            e.stopPropagation();
            currentElement = $(this);
            comdy.common.post(settings.getLinkEditorUrl, { Href: $(this).attr("href"), Title: $(this).html(), Target: $(this).attr("target") }, function (response) {
                if (response !== "") {
                    comdy.common.showModal(response, "comdy Editor", "", '<button type="button" class="btn btn-danger" onclick="comdy.customize.deleteElement()"><i class="fa fa-remove"></i>&nbsp;Xóa</button><button type="button" class="btn btn-primary" onclick="comdy.customize.applyLink()"><i class="fa fa-save"></i>&nbsp;Cập Nhật</button>');
                } else {
                    comdy.common.errorNotify(response);
                }
            });
            return false;
        });
    };

    comdy.customize.applyLink = function () {
        var $form = $("#formEditor");
        if (!$form.valid()) {
            return;
        }
        comdy.common.hideValidateMessages($form);

        var value = $("#formEditor #Text").val();
        $(currentElement).attr("href", $("#formEditor #Href").val());
        $(currentElement).attr("target", $("#formEditor #Target").val());
        $(currentElement).html($("#formEditor #Title").val());
        comdy.common.hideModal();
        isDirty = true;
        canSave();
    }


    /* --------------------------------------------------------------------
     * Button Editor 
     * -------------------------------------------------------------------- */

    var initButton = function () {
        $("#design-frame").contents().find(".comdybutton").bind("click", function (e) {
            e.preventDefault();
            e.stopPropagation();
            currentElement = $(this);
            comdy.common.get(settings.getButtonEditorUrl, { html: $(this).html().trim() }, function (response) {
                comdy.common.showModal(response, "comdy Editor", "", '<button type="button" class="btn btn-danger" onclick="comdy.customize.deleteElement()"><i class="fa fa-remove"></i>&nbsp;Xóa</button><button type="button" class="btn btn-primary" onclick="comdy.customize.applyButton()"><i class="fa fa-save"></i>&nbsp;Cập Nhật</button>');
            });
            return false;
        });
    };

    comdy.customize.applyButton = function () {
        var $form = $("#formEditor");
        if (!$form.valid()) {
            return;
        }
        comdy.common.hideValidateMessages($form);

        var value = $("#formEditor #Title").val();
        $(currentElement).html(value);
        comdy.common.hideModal();
        isDirty = true;
        canSave();
    }


    /* --------------------------------------------------------------------
     * Image Editor 
     * -------------------------------------------------------------------- */
    var initImage = function () {
        $("#design-frame").contents().find(".comdyimage").bind("click", function (e) {
            e.preventDefault();
            e.stopPropagation();
            currentElement = $(this);
            comdy.common.post(settings.getImageEditorUrl, { Src: $(this).attr("src"), Alt: $(this).attr("alt") }, function (response) {
                if (response !== "") {
                    comdy.common.showModal(response, "comdy Editor", "", '<button type="button" class="btn btn-danger" onclick="comdy.customize.deleteElement()"><i class="fa fa-remove"></i>&nbsp;Xóa</button><button type="button" class="btn btn-primary" onclick="comdy.customize.applyImage()"><i class="fa fa-save"></i>&nbsp;Cập Nhật</button>');
                } else {
                    comdy.common.errorNotify(response);
                }
            });
            return false;
        });
    };

    comdy.customize.selectImage = function () {
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
            $("#fw-thumbnail-image-editor").attr("src", src);
            $("#formEditor #Src").val(src);
            comdy.common.hideModal("#media-modal");
        } else {
            comdy.common.errorNotify("Vui lòng chọn hình ảnh");
        }
    }

    comdy.customize.applyImage = function () {
        var $form = $("#formEditor");
        if (!$form.valid()) {
            return;
        }
        comdy.common.hideValidateMessages($form);

        $(currentElement).attr("src", $("#formEditor #Src").val());
        $(currentElement).attr("alt", $("#formEditor #Alt").val());
        comdy.common.hideModal();
        isDirty = true;
        canSave();
    }



    /* --------------------------------------------------------------------
     * Background Editor 
     * -------------------------------------------------------------------- */
    var initBackground = function () {
        $("#design-frame").contents().find(".comdybackground").bind("click", function (e) {
            e.preventDefault();
            e.stopPropagation();
            currentElement = $(this);
            var style = $(this).attr("style");
            var split = style.split(';');
            var src = "";
            if (split.length > 0) {
                for (var i = 0; i < split.length; i++) {
                    var start = split[i].indexOf("url(");
                    var end = split[i].indexOf(")");
                    if (start > 0 && end > 0) {
                        src = split[i].substring(start + 4, end).trim('"');
                    }
                }
            }
            comdy.common.post(settings.getBackgroundEditorUrl, { Src: src, Style: style }, function (response) {
                if (response !== "") {
                    comdy.common.showModal(response, "comdy Editor", "", '<button type="button" class="btn btn-primary" onclick="comdy.customize.applyBackgroundImage()"><i class="fa fa-save"></i>&nbsp;Cập Nhật</button>');
                } else {
                    comdy.common.errorNotify(response);
                }
            });
            return false;
        });
    };

    comdy.customize.selectBackgroundImage = function () {
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
            $("#fw-thumbnail-image-editor").attr("src", src);
            var style = $("#formEditor #Style").val().replace($("#formEditor #Src").val(), src);
            $("#formEditor #Style").val(style)
            $("#formEditor #Src").val(src);
            comdy.common.hideModal("#media-modal");
        } else {
            comdy.common.errorNotify("Vui lòng chọn hình ảnh");
        }
    }

    comdy.customize.applyBackgroundImage = function () {
        var $form = $("#formEditor");
        if (!$form.valid()) {
            return;
        }
        comdy.common.hideValidateMessages($form);
        $(currentElement).attr("style", $("#formEditor #Style").val());
        comdy.common.hideModal();
        isDirty = true;
        canSave();
    }



    /* --------------------------------------------------------------------
     * Input Editor 
     * -------------------------------------------------------------------- */

    var initInput = function () {
        $("#design-frame").contents().find(".comdyinput").bind("click", function (e) {
            e.preventDefault();
            e.stopPropagation();
            currentElement = $(this);
            comdy.common.get(settings.getInputEditorUrl, { text: $(this).attr("placeholder") }, function (response) {
                comdy.common.showModal(response, "comdy Editor", "", '<button type="button" class="btn btn-danger" onclick="comdy.customize.deleteElement()"><i class="fa fa-remove"></i>&nbsp;Xóa</button><button type="button" class="btn btn-primary" onclick="comdy.customize.applyInput()"><i class="fa fa-save"></i>&nbsp;Cập Nhật</button>');
            });
            return false;
        });
    };

    comdy.customize.applyInput = function () {
        var $form = $("#formEditor");
        if (!$form.valid()) {
            return;
        }
        comdy.common.hideValidateMessages($form);
        $(currentElement).attr("placeholder", $("#formEditor #Text").val());
        comdy.common.hideModal();
        isDirty = true;
        canSave();
    }




    /* --------------------------------------------------------------------
     * Text Editor 
     * -------------------------------------------------------------------- */
    var initText = function () {
        $("#design-frame").contents().find(".comdytext").click(function (e) {
            e.preventDefault();
            e.stopPropagation();
            currentElement = $(this);
            comdy.common.get(settings.getTextEditorUrl, { html: $(this).html().trim() }, function (response) {
                comdy.common.showModal(response, "comdy Editor", "", '<button type="button" class="btn btn-danger" onclick="comdy.customize.deleteElement()"><i class="fa fa-remove"></i>&nbsp;Xóa</button><button type="button" class="btn btn-primary" onclick="comdy.customize.applyText()"><i class="fa fa-save"></i>&nbsp;Cập Nhật</button>');
            });
            return false;
        });
    };

    comdy.customize.applyText = function () {
        var $form = $("#formEditor");
        if (!$form.valid()) {
            return;
        }
        comdy.common.hideValidateMessages($form);
        $(currentElement).html($("#formEditor #Text").val());
        comdy.common.hideModal();
        isDirty = true;
        canSave();
    }


    var canSave = function () {
        if (isDirty) {
            if ($("#btn-save-page").hasClass("disabled")) {
                $("#btn-save-page").removeClass("disabled");
                $("#btn-save-page").removeClass("btn-default");
                $("#btn-save-page").addClass("btn-success");
            }
        } else {
            if (!$("#btn-save-page").hasClass("disabled")) {
                $("#btn-save-page").addClass("disabled");
                $("#btn-save-page").addClass("btn-default");
                $("#btn-save-page").removeClass("btn-success");
            }
        }
    }

    var canPublish = function () {
        if (isPublish) {
            if ($("#btn-publish-page").hasClass("disabled")) {
                $("#btn-publish-page").removeClass("disabled");
                $("#btn-publish-page").removeClass("btn-default");
                $("#btn-publish-page").addClass("btn-primary");
            }
        } else {
            if (!$("#btn-publish-page").hasClass("disabled")) {
                $("#btn-publish-page").addClass("disabled");
                $("#btn-publish-page").addClass("btn-default");
                $("#btn-publish-page").removeClass("btn-primary");
            }
        }
    }

}(window.comdy = window.comdy || {}, jQuery));
