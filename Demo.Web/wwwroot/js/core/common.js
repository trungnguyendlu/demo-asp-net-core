(function (comdy, $) {

    comdy.common = comdy.common || {};

    comdy.common.initEditor = function (element, css, js, fonts) {
        tinymce.init({
            selector: element,
            language: 'vi_VN',
            height: 420,
            plugins: [
                'advlist autolink lists link image charmap print preview anchor textcolor',
                'searchreplace visualblocks code fullscreen',
                'insertdatetime media table paste code help wordcount'
            ],
            toolbar: 'undo redo | formatselect | bold italic strikethrough forecolor backcolor | link image media pageembed | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | removeformat | help',
            font_formats: fonts,
            content_css: css,
            content_style: '.appear-animation { opacity: 1 !important;}',
            //content_js: js
            //setup: function (ed) {
            //    if (js.length > 0) {
            //        for (var i = 0; i < js.length; i++) {
            //            tinymce.ScriptLoader.load(js[i]);
            //        }
            //    }
            //}
        });
    };

    comdy.common.slugGenerate = function (str) {
        str = str.toLowerCase();
        str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
        str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
        str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
        str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
        str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
        str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
        str = str.replace(/đ/g, "d");
        str = str.replace(/!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|\'| |\"|\{|\}|\&|\#|\[|\]|~|$|_/g, "-");
        /* tìm và thay thế các kí tự đặc biệt trong chuỗi sang kí tự - */
        str = str.replace(/-+-/g, "-"); //thay thế 2- thành 1-          
        str = str.replace(/^\-+|\-+$/g, ""); //cắt bỏ ký tự - ở đầu và cuối chuỗi 

        return str;
    };

    comdy.common.applyReturn = function(selector, callback) {
        $(selector).keypress(function(e) {
            if (e.which === 13) {
                callback();
                return false;
            }
        });
    };

    comdy.common.applySort = function (page, paging) {
        $(".datatable-sorting th.sorting").on("click", function () {
            var sort = $(this).hasClass("sorting_desc") ? "desc" : "asc";
            $("#Sort").val($(this).data("column") + " " + sort);
            paging($(page).val());

            if (sort === "desc") {
                sort = "sorting_asc";
            } else {
                sort = "sorting_desc";
            }
            $(".sorting_asc").removeClass("sorting_asc").addClass("sorting");
            $(".sorting_desc").removeClass("sorting_desc").addClass("sorting");
            $(this).removeClass("sorting").addClass(sort);
            //comdy.common.resizeColumns();
        });
    };

    comdy.common.resizeColumns = function () {
        var headers = $("table.table-header > thead > tr:first-child > th");
        var columns = $("table.table-content > thead > tr:first-child > th");
        $.each(columns, function (index, column) {
            $(headers[index]).width($(column).width());
        });
    };

    comdy.common.getMultiSelectValue = function (selector, statusSelector) {
        var status = [];
        $(selector).each(function (i, selected) {
            status[i] = $(selected).val();
        });

        $(statusSelector).val(status);
    };

    comdy.common.initAutoComplete = function(url, data, textField, displaySelector, valueSelector) {
        $(displaySelector).kendoAutoComplete({
            dataSource: {
                transport: {
                    read: {
                        url: url,
                        data: data
                    },
                    prefix: ""
                },
                serverFiltering: true,
                filter: [],
                schema: {
                    "errors": "Errors"
                }
            },
            dataTextField: textField,
            minLength: 3,
            enforceMinLength: true,
            autoBind: false,
            select: function (e) {
                var data = this.dataItem(e.item.index());
                $(valueSelector).val(data.Id);
            }
        });

        $(displaySelector).change(function () {
            if ($(this).val() === "") {
                $(valueSelector).val("");
            }
        });
    }

    // Custom color
    comdy.common.initTooltip = function () {
        $("[data-popup=tooltip-custom]").tooltip({
            template: "<div class=\"tooltip\"><div class=\"bg-teal\"><div class=\"tooltip-arrow\"></div><div class=\"tooltip-inner\"></div></div></div>"
        });
    };

    comdy.common.showModal = function(content, title, modalSize, button) {
        $("#modal-content").html(content);
        //comdy.common.maskInput();

        $("#modal .modal-title").text(title);
        $("#modal .modal-dialog").removeClass("modal-sm modal-lg").addClass(modalSize);
        $("#modal-button").find("button").slice(1).remove();
        $("#modal-button").append(button);
        $("#modal").modal({ backdrop: 'static', keyboard: false });
    };

    comdy.common.showMediaModal = function (content, button) {
        $("#media-modal-content").html(content);
        $("#media-modal-button").find("button").slice(1).remove();
        $("#media-modal-button").append(button);
        $("#media-modal").modal({ backdrop: "static", keyboard: false });
    };

    comdy.common.hideModal = function (modalId) {
        $(modalId).modal("toggle");
        $(modalId + " #modal-content").html("");
    };


	comdy.common.queryStringToJSON = function (query) {
		var vars = {};

		$.each(query.split("&"), function (index, item) {
			var itemSplit = item.split("=");
			vars[itemSplit[0]] = decodeURIComponent(itemSplit[1] || "");
		});

		return vars;
	};

	comdy.common.hasProp = function (obj, prop) {
		return (typeof obj === "object") && obj.hasOwnProperty(prop);
	};

	comdy.common.raiseEvent = function (selector, eventName, eventFunc) {
		$(selector).unbind(eventName).bind(eventName, eventFunc);
	};

	comdy.common.preventReturn = function (e) {
		var charCode = e.which || e.charCode;

		if (charCode === 13) {
			return false;
		}

		return true;
	};

	/* sai functions */
	comdy.common.postToDelete = function (params) {
		var msg = "Are you sure you want to delete?";

		if (!comdy.common.undefined(params["message"])) {
			msg = params.message;
		}

		comdy.common.confirm(msg, function () {
			comdy.common.post(params.deleteUrl, params.deleteData, function (response) {
			    if (response.success) {
			        comdy.common.successNotify(response.message);
				    if ($.isFunction(params.callback)) {
				        params.callback();						
					} else {
						comdy.common.gotoUrl(params.callback);
					}
				} else
			        comdy.common.errorNotify(response.message);
			});
		});
	};

    comdy.common.alert = function(message, callback) {
        swal({
                title: message,
                confirmButtonColor: "#2196F3",
                confirmButtonText: "Đóng"
            },
            function() {
                if (callback) {
                    callback();
                }
            });
    };

    comdy.common.confirm = function (message, callback) {
        swal({
                title: message,
                showCancelButton: true,
                cancelButtonText: "Thoát",
                confirmButtonColor: "#2196F3",
                confirmButtonText: "Đồng ý"
            },
            function (isConfirm) {
                if (isConfirm && callback) {
                    callback();
                }
            });
    };

    comdy.common.errorNotify = function(message) {
        new PNotify({
            title: "Thông Báo",
            text: message,
            icon: "icon-cancel-circle2",
            type: "error"
        });
    };

    comdy.common.successNotify = function(message) {
        new PNotify({
            title: "Thông Báo",
            text: message,
            icon: "icon-checkmark",
            type: "success"
        });
    };

	/* UI functions */


	comdy.common.undefined = function (obj) {
		return typeof (obj) === "undefined";
	};

	comdy.common.setAllCheckBoxes = function (tableSelector, checkboxPrefixSelector, checked) {
		$("input[id^=\"" + checkboxPrefixSelector + "\"]", "#" + tableSelector).each(function (index, item) {
			comdy.common.setCheckBox(item, checked);
		});
	};

	comdy.common.setCheckBox = function (selector, checked) {
		var checkbox = $(selector).removeAttr("checked");
		var span = checkbox.parent().removeClass("checked");

		if (checked) {
			checkbox.attr("checked", "checked");
			span.addClass("checked");
		}
	};

	comdy.common.setReadOnlyFields = function (selector, readonly) {
		if (readonly) {
			$(selector).find(":text, select").attr("readonly", true);
		} else {
			$(selector).find(":text, select").removeAttr("readonly");
		}
	};

	comdy.common.blockUI = function () {
		$.blockUI({ message: "", overlayCSS: { cursor: "default" }, baseZ: 20000 });
	};

	comdy.common.unblockUI = function () {
		$.unblockUI();
	};

	comdy.common.showLoading = function () {
		var $loadingParent = $("#loadingParent");
		$loadingParent.css("left", ($(document).width() / 2 - $loadingParent.width() / 2) + 50);
		$loadingParent.css("top", "0");
		$loadingParent.show();
	};

	comdy.common.hideLoading = function () {
		$("#loadingParent").hide();
	};

	comdy.common.reloadFlash = function (selector) {
		var flashUpload = $(selector);
		var flashUploadHtml = flashUpload.html();

		flashUpload.empty();
		flashUpload.html(flashUploadHtml);
	};

	comdy.common.changeFlashVars = function (selector, vars) {
		var flashUpload = $(selector);

		var embedUpload = flashUpload.find("embed");
		var paramFlashVars = flashUpload.find("param[name=\"FlashVars\"]");

		var flashVars = comdy.common.queryStringToJSON(paramFlashVars.attr("value").slice(1));
		flashVars = $.extend(flashVars, vars);

		var flashVarsQuery = "&" + $.param(flashVars);

		embedUpload.attr("flashvars", flashVarsQuery);
		paramFlashVars.attr("value", flashVarsQuery);

		comdy.common.reloadFlash(selector);
	};

	comdy.common.changeFlashUploadPage = function (selector, url) {
		comdy.common.changeFlashVars(selector, {
			uploadPage: encodeURIComponent(url)
		});
	};

	comdy.common.maskInput = function () {
	    moment.locale("vi");
		comdy.common.maskInputPhone();
		comdy.common.maskInputMonth();
		comdy.common.maskInputDate();
		comdy.common.maskInputDateTime();
		comdy.common.tagsInput();
	    comdy.common.initTooltip();
	};

	comdy.common.centerModal = function (selector) {
		var $this = $(selector);

		if ($this.hasClass("in") === false) {
			$this.show();
		};
		var contentHeight = $(window).height() - 60;
		var headerHeight = $this.find(".modal-header").outerHeight() || 2;
		var footerHeight = $this.find(".modal-footer").outerHeight() || 2;

		$this.find(".modal-content").css({
			'max-height': function () {
				return contentHeight;
			}
		});

		$this.find(".modal-body").css({
			'max-height': function () {
				return contentHeight - (headerHeight + footerHeight);
			}
		});

		$this.find(".modal-dialog").addClass("modal-dialog-center").css({
			'margin-top': function () {
				return -($(this).outerHeight() / 2);
			},
			'margin-left': function () {
				return -($(this).outerWidth() / 2);
			}
		});
		if ($this.hasClass("in") === false) {
			$this.hide();
		};
	}

	comdy.common.maskInputPhone = function () {
		if ($.inputmask) {
			$("input.comdyInputPhone").inputmask("mask", { "mask": "9999-999-9999", "clearMaskOnLostFocus": true });
		}
	};

	comdy.common.tagsInput = function () {
		if (jQuery().tagsInput) {
			$("input.ecTagsInput").tagsInput({
				"width": "auto",
				"height": "auto"
			});
		}
	};

	comdy.common.maskInputDate = function () {
		//var $date = $("input.comdyInputDate");

		//if ($.inputmask) {
		//	$date.inputmask("mask", { "mask": "99/99/9999", "clearMaskOnLostFocus": true });
		//}

		//if ($.datepicker) {
			//$date.datepicker({
			//	format: "dd/mm/yyyy",
			//	autoclose: true,
			//	orientation: "auto",
			//	todayHighlight: true
			//});
	    //}

	    var $date = $("input.comdyInputDate");
	    var data = [];
	    $date.each(function() {
	        data.push({ input: $(this), value: $(this).val() });
	    });

	    $date.daterangepicker({
	        singleDatePicker: true,
	        autoApply: false,
	        locale: {
	            applyLabel: "Chọn",
	            format: "DD/MM/YYYY"
	        }
	    });

	    $.each(data, function(index, item) {
	        $(item.input).val(item.value);
	        $(item.input).keyup(function (e) {
	            if (e.keyCode === 8 || e.keyCode === 46) {
	                $(item.input).val(null);
	            }
	        });
	    });
	};
    
	comdy.common.maskInputMonth = function () {
		var $date = $("input.comdyInputMonth");

		if ($.inputmask) {
			$date.inputmask("mask", { "mask": "99/9999", "clearMaskOnLostFocus": true });
		}

		if ($.datepicker) {
			$date.datepicker({
				format: "mm/yyyy",
				viewMode: "months",
				minViewMode: "months",
				autoclose: true,
				orientation: "auto",
			}).on("changeDate", function (ev) {
				var temp = ev.date.valueOf();
				$(ev.currentTarget).val(ev.date.toString("mm/dd/yyyy"));
			});

		}
	};

	comdy.common.maskInputDateTime = function () {
	    //$("input.comdyInputDateTime").datetimepicker();

	    $("input.comdyInputDateTime").daterangepicker({
	        singleDatePicker: true,
	        timePicker: true,
	        timePicker24Hour: true,
	        autoApply: true,
	        locale: {
	            format: "DD/MM/YYYY HH:mm"
	        }
	    });
	};

	comdy.common.dateValid = function (source) {
		if ($.datepicker) {
			try {
                jQuery.datepicker.parseDate("DD/MM/YYYY", source);
				return true;
			}
			catch (e) {
				return false;
			}
		}
		return false;
	};

	/* POST or GET functions */
	var processAjaxError = false;

	comdy.common.load = function (selector, url, data, callback) {
		processAjaxError = false;
		comdy.common.processUIBeforePost();

		$(selector).load(url, data, function () {
			if (!processAjaxError && callback && $.isFunction(callback)) {
				callback();
			}
		});

		comdy.common.processUIAfterPost();
	};

	comdy.common.postForm = function (url, formId, doneFunc, alwaysFunc) {
		return postOrGet(true, url, $(formId).serialize(), doneFunc, alwaysFunc);
	};
	comdy.common.getForm = function (url, formId, doneFunc, alwaysFunc) {
		return postOrGet(false, url, $(formId).serialize(), doneFunc, alwaysFunc);
	};

	comdy.common.post = function (url, data, doneFunc, alwaysFunc) {
		return postOrGet(true, url, data, doneFunc, alwaysFunc);
	};

	comdy.common.get = function (url, data, doneFunc, alwaysFunc) {
		return postOrGet(false, url, data, doneFunc, alwaysFunc);
	};

	function postOrGet(ispost, url, data, doneFunc, alwaysFunc, lockUi) {
	    processAjaxError = false;
	    comdy.common.processUIBeforePost();
	    if (comdy.common.undefined(data) || data === null) {
			data = {};
		}
		var request = ispost ? $.post(url, data) : $.get(url, data);

		return request.done(function (response) {
			if (!processAjaxError) {
				if (doneFunc && $.isFunction(doneFunc)) {
					doneFunc(response);
				}
			}
		}).always(function () {
		    comdy.common.processUIAfterPost();
			if (alwaysFunc && $.isFunction(alwaysFunc)) {
				alwaysFunc();
			}
		});
	};

    comdy.common.ajax = function (options, doneFunc, alwaysFunc) {
        processAjaxError = false;
        comdy.common.processUIBeforePost();

        return $.ajax(options).done(function (response) {
            if (!processAjaxError) {
                if (doneFunc && $.isFunction(doneFunc)) {
                    doneFunc(response);
                }
            }
        }).always(function () {
            comdy.common.processUIAfterPost();

            if (alwaysFunc && $.isFunction(alwaysFunc)) {
                alwaysFunc();
            }
        });
    };

	comdy.common.submit = function (selector, processUiAfterSubmit) {
		comdy.common.processUIBeforePost();

		var form = $(selector);

		if (!form.valid()) {
			comdy.common.processUIAfterPost();
		}

		form.submit();

		if (!comdy.common.undefined(processUiAfterSubmit) && processUiAfterSubmit) {
			comdy.common.processUIAfterPost();
		}
	};

	comdy.common.submitIgnoreValidate = function (selector, processUiAfterSubmit) {
		comdy.common.processUIBeforePost();

		var form = $(selector);
		var settings = form.validate().settings;

		for (var ruleIndex in settings.rules) {
			delete settings.rules[ruleIndex];
		}

		form.submit();

		if (!comdy.common.undefined(processUiAfterSubmit) && processUiAfterSubmit) {
			comdy.common.processUIAfterPost();
		}
	};

	comdy.common.bindDropDownByAjax = function (options, callback) {
		var opts = $.extend({ dataValueField: "Id", dataTextField: "Name", optionalItem: null }, options);

		var dropDown = $(opts.selector);
		dropDown.empty();

		if (opts.optionalItem) {
			var optionalItems = $.isArray(opts.optionalItem) ? opts.optionalItem : [opts.optionalItem];

			$.each(optionalItems, function (index, item) {
				dropDown.append($("<option/>").attr("value", item[opts.dataValueField]).text(item[opts.dataTextField]));
			});
		}

		comdy.common.get(opts.url, opts.params, function (items) {
			var selectedValue = null;

			$.each(items, function (index, item) {
				dropDown.append($("<option/>").attr("value", item[opts.dataValueField]).text(item[opts.dataTextField]));

				if (!selectedValue) {
					if (comdy.common.hasProp(item, "IsSelected") && item.IsSelected) {
						selectedValue = item[opts.dataValueField];
					}
					else if (comdy.common.hasProp(item, "ExtendProps") && comdy.common.hasProp(item.ExtendProps, "IsDefault") && item.ExtendProps.IsDefault) {
						selectedValue = item[opts.dataValueField];
					}
				}
			});

			if (selectedValue) {
				dropDown.val(selectedValue);
			}

			if (callback && $.isFunction(callback)) {
				callback(items);
			}
		});
	};

	comdy.common.bindDropDown = function (options) {
		var opts = $.extend({ dataValueField: "Id", dataTextField: "Name", optionalItem: null }, options);

		var dropDown = $(opts.selector);
		dropDown.empty();

		if (options.items) {
			$.each(options.items, function (index, item) {
				dropDown.append($("<option/>").attr("value", item[opts.dataValueField]).text(item[opts.dataTextField]));
			});
		}
	};

	comdy.common.gotoUrl = function (url, data) {
		var undefinedData = comdy.common.undefined(data);

		comdy.common.blockUI();
		comdy.common.showLoading();

		url = !undefinedData && data && (typeof data === "object") ? (url + "?" + $.param(data)) : url;

		top.location.href = url;
	};

    comdy.common.resetFormValidator = function (form) {
        var $form = $(form);
        $form.removeData("validator");
        $form.removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse($form);
    };

    comdy.common.processUIBeforePost = function () {
        comdy.common.blockUI();
        comdy.common.showLoading();
    };

    comdy.common.processUIAfterPost = function () {
        comdy.common.unblockUI();
        comdy.common.hideLoading();
    };

	comdy.common.addObjToArray = function (source, obj, funcCheckObj) {
		var exist = false;

		if (funcCheckObj && $.isFunction(funcCheckObj)) {
			for (var i = 0; i < source.length; i++) {
				exist = funcCheckObj(source[i], obj);
				if (exist) {
					break;
				}
			}
		} else {
			exist = $.inArray(obj, source) >= 0;
		}

		if (!exist) {
			source.push(obj);
		}
	};

	comdy.common.removeObjFromArray = function (source, obj, funcCheckObj) {
		return $.grep(source, function (item) {
			return funcCheckObj && $.isFunction(funcCheckObj) ? !funcCheckObj(item, obj) : (item != obj);
		});
	};

	comdy.common.addToArray = function (sources, values) {
		if ($.isArray(sources)) {
			if ($.isArray(values)) {
				for (var i = 0; i < values.length; i++) {
					if ($.inArray(values[i], sources) === -1) {
						sources.push(values[i]);
					}
				}
			} else if ($.inArray(values, sources) === -1) {
				sources.push(values);
			}
		}
	};

	comdy.common.removeFromArray = function (sources, values) {
		if ($.isArray(sources)) {
			var valuesIsArray = $.isArray(values);

			return $.grep(sources, function (val) {
				return valuesIsArray ? ($.inArray(val, values) === -1) : (val != values);
			});
		}

		return null;
	};

    ///<summary>Usage: format("Hi {0}, you are {1}!", "Foo", 100) -> "Hi Foo, you ra 100"</summary>
    ///<summary>Usage: format("Hello {Name} ({Id})", { Id: "test", Name: "test"}) ->"Hello test (test)" </summary>
	comdy.common.format = function () {
	    var source = arguments[0];

	    //by json
	    if (arguments.length === 2 && arguments[1].constructor === {}.constructor) {
	        var json = arguments[1];
	        return source.replace(/{([^{}]*)}/g,
				function (regtext, regvalue) {
				    var text = json[regvalue];
				    return typeof text === "string" || typeof text === "number" ? text : regtext;
				}
			);
	    }

	    //by index
	    for (var i = 0; i < arguments.length - 1; i++) {
	        source = source.replace(new RegExp("\\{" + i + "\\}", "g"), arguments[i + 1]);
	    }

	    return source;
	};

	comdy.common.scrollToError = function () {
		var top = $(".input-validation-error").offset().top - 100;
		if (top < 0) top = 0;
		$("html, body").animate({
			scrollTop: top
		}, 10);
	};

	comdy.common.showValidateSuccessMessages = function (scope, messages) {
	    showValidateMessagesByAlertCss(scope, messages, "note-success");
	};

	comdy.common.showValidateInfoMessages = function (scope, messages) {
	    showValidateMessagesByAlertCss(scope, messages, "note-info");
	};

	comdy.common.showValidateErrorMessages = function (scope, messages) {
	    showValidateMessagesByAlertCss(scope, messages, "note-danger");
	};

	comdy.common.showValidateMessages = function (scope, messages) {
		var $div = scope ? $(scope).find("div[data-valmsg-summary]") : $("div[data-valmsg-summary]");

		if ($div.hasClass("validation-summary-valid")) $div.removeClass("validation-summary-valid");
		if (!$div.hasClass("validation-summary-errors")) $div.addClass("validation-summary-errors");

		var $ul = $div.find("ul");

		var msgs = $.isArray(messages) ? messages : [messages];

		$.each(msgs, function (ind, msg) {
			$ul.append($("<li />").html(msg));
		});

		return $div;
	};

	comdy.common.hideValidateMessages = function (scope) {
		var $div = scope ? $(scope).find("div[data-valmsg-summary]") : $("div[data-valmsg-summary]");

		if (!$div.hasClass("validation-summary-valid")) $div.addClass("validation-summary-valid");
		if ($div.hasClass("validation-summary-errors")) $div.removeClass("validation-summary-errors");

		$div.find("ul").empty();

		clearValidateAlertCss($div);
		$div.addClass("note-danger");

		return $div;
	};

	function showValidateMessagesByAlertCss(scope, messages, alertcss) {
		var $div = comdy.common.showValidateMessages(scope, messages);
		clearValidateAlertCss($div);
		$div.addClass(alertcss);
	}

	function clearValidateAlertCss(selector) {
	    $(selector).removeClass("note-danger").removeClass("note-info").removeClass("note-success");
	}

	comdy.common.checkall = function (id, action) {
	   
	    $("input:checkbox").each(function () {
	        
	        if ($(this).attr("id").indexOf(id) >= 0 && $(this).attr("id").indexOf(action) >= 0) {
	            $(this).prop("checked", true);
	            $(this).parent("span").addClass("checked");
	        }
		});
	};

	comdy.common.uncheckall = function (id, action) {
		$("input:checkbox").each(function () {
		    if ($(this).attr("id").indexOf(id) >= 0 && $(this).attr("id").indexOf(action) >= 0) {
		        $(this).parent("span").removeClass("checked");
			    $(this).prop("checked", false);
			}
		});
	};

	comdy.common.ajaxError = function (msg) {
		processAjaxError = true;
		comdy.common.errorNotify(msg);
	};

	comdy.common.ajaxLostAuthen = function () {
        processAjaxError = true;
        comdy.common.alert("Phiên làm việc của bạn đã kết thúc. Vui lòng đăng nhập lại.", function () {
            comdy.common.gotoUrl(window.authenUrl);
        })
	};

	comdy.common.ajaxAccessDeined = function () {
		processAjaxError = true;
		comdy.common.errorNotify("Bạn không có quyền truy cập tính năng này.");
	};

}(window.comdy = window.comdy || {}, jQuery));

/*  Ajax  */
$.ajaxSetup({
	cache: false,
	ifModified: false,
	timeout: 1800000 /* 30 * 60 * 1000 milisecond */
});

$(document).ajaxError(function (e, jqXhr, settings, exception) {
    if (jqXhr.status === 401) {
        comdy.common.ajaxLostAuthen();
    }
	else if (jqXhr && (jqXhr.responseText.indexOf("comdy.common.ajaxError") || jqXhr.responseText.indexOf("comdy.common.ajaxAccessDeined") || jqXhr.responseText.indexOf("comdy.common.ajaxLostAuthen"))) {
		eval(jqXhr.responseText);

	} else if (!settings.suppressErrors) {
		var msg = "AjaxError: " + (new Date()) + ": " + settings.url + ": error:" + exception + ": " + jqXhr.status + ": " + jqXhr.statusText;
		console.log(msg);

		if (jqXhr.statusText.toLowerCase() !== "abort" && (jqXhr.status !== 0 || jqXhr.statusText === "timeout") /*status = 0 - Cancelled/timeout/firewall*/) {
			jAlert(msg);
		}
	}
});