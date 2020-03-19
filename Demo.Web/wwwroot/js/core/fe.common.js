(function (corporation, $) {

    Demo.common = Demo.common || {};

	Demo.common.blockUI = function () {
		$.blockUI({ message: "", overlayCSS: { cursor: "default" }, baseZ: 20000 });
	};

	Demo.common.unblockUI = function () {
		$.unblockUI();
	};

	Demo.common.showLoading = function () {
		var $loadingParent = $("#loadingParent");
		$loadingParent.css("left", ($(document).width() / 2 - $loadingParent.width() / 2) + 50);
		$loadingParent.css("top", "0");
		$loadingParent.show();
	};

	Demo.common.hideLoading = function () {
		$("#loadingParent").hide();
    };

    Demo.common.errorNotify = function (message) {
        new PNotify({
            title: "Thông Báo",
            text: message,
            icon: "icon-cancel-circle2",
            type: "error"
        });
    };

    Demo.common.successNotify = function (message) {
        new PNotify({
            title: "Thông Báo",
            text: message,
            icon: "icon-checkmark",
            type: "success"
        });
    };

	/* POST or GET functions */
	var processAjaxError = false;

	Demo.common.load = function (selector, url, data, callback) {
		processAjaxError = false;
		Demo.common.processUIBeforePost();

		$(selector).load(url, data, function () {
			if (!processAjaxError && callback && $.isFunction(callback)) {
				callback();
			}
		});

		Demo.common.processUIAfterPost();
	};

	Demo.common.postForm = function (url, formId, doneFunc, alwaysFunc) {
		return postOrGet(true, url, $(formId).serialize(), doneFunc, alwaysFunc);
	};
	Demo.common.getForm = function (url, formId, doneFunc, alwaysFunc) {
		return postOrGet(false, url, $(formId).serialize(), doneFunc, alwaysFunc);
	};

	Demo.common.post = function (url, data, doneFunc, alwaysFunc) {
		return postOrGet(true, url, data, doneFunc, alwaysFunc);
	};

	Demo.common.get = function (url, data, doneFunc, alwaysFunc) {
		return postOrGet(false, url, data, doneFunc, alwaysFunc);
	};

	function postOrGet(ispost, url, data, doneFunc, alwaysFunc, lockUi) {
        processAjaxError = false;
        Demo.common.processUIBeforePost();
        if (Demo.common.undefined(data) || data === null) {
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
            Demo.common.processUIAfterPost();
			if (alwaysFunc && $.isFunction(alwaysFunc)) {
				alwaysFunc();
			}
		});
	}

	Demo.common.ajax = function (options, doneFunc, alwaysFunc) {
		processAjaxError = false;
		Demo.common.processUIBeforePost();

		return $.ajax(options).done(function (response) {
			if (!processAjaxError) {
				if (doneFunc && $.isFunction(doneFunc)) {
					doneFunc(response);
				}
			}
		}).always(function () {
			Demo.common.processUIAfterPost();

			if (alwaysFunc && $.isFunction(alwaysFunc)) {
				alwaysFunc();
			}
		});
	}

	Demo.common.submit = function (selector, processUiAfterSubmit) {
		Demo.common.processUIBeforePost();

		var form = $(selector);

		if (!form.valid()) {
			Demo.common.processUIAfterPost();
		}

		form.submit();

		if (!Demo.common.undefined(processUiAfterSubmit) && processUiAfterSubmit) {
			Demo.common.processUIAfterPost();
		}
	};

	Demo.common.submitIgnoreValidate = function (selector, processUiAfterSubmit) {
		Demo.common.processUIBeforePost();

		var form = $(selector);
		var settings = form.validate().settings;

		for (var ruleIndex in settings.rules) {
			delete settings.rules[ruleIndex];
		}

		form.submit();

		if (!Demo.common.undefined(processUiAfterSubmit) && processUiAfterSubmit) {
			Demo.common.processUIAfterPost();
		}
	};

	Demo.common.gotoUrl = function (url, data) {
		var undefinedData = Demo.common.undefined(data);

		Demo.common.blockUI();
		Demo.common.showLoading();

		url = !undefinedData && data && (typeof data === "object") ? (url + "?" + $.param(data)) : url;

		top.location.href = url;
	};

    Demo.common.resetFormValidator = function (form) {
        var $form = $(form);
        $form.removeData("validator");
        $form.removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse($form);
    };

    Demo.common.processUIBeforePost = function () {
        Demo.common.blockUI();
        Demo.common.showLoading();
    };

    Demo.common.processUIAfterPost = function () {
        Demo.common.unblockUI();
        Demo.common.hideLoading();
    };

	Demo.common.showValidateSuccessMessages = function (scope, messages) {
	    showValidateMessagesByAlertCss(scope, messages, "note-success");
	};

	Demo.common.showValidateInfoMessages = function (scope, messages) {
	    showValidateMessagesByAlertCss(scope, messages, "note-info");
	};

	Demo.common.showValidateErrorMessages = function (scope, messages) {
	    showValidateMessagesByAlertCss(scope, messages, "note-danger");
	};

	Demo.common.showValidateMessages = function (scope, messages) {
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

	Demo.common.hideValidateMessages = function (scope) {
		var $div = scope ? $(scope).find("div[data-valmsg-summary]") : $("div[data-valmsg-summary]");

		if (!$div.hasClass("validation-summary-valid")) $div.addClass("validation-summary-valid");
		if ($div.hasClass("validation-summary-errors")) $div.removeClass("validation-summary-errors");

		$div.find("ul").empty();

		clearValidateAlertCss($div);
		$div.addClass("note-danger");

		return $div;
	};

	function showValidateMessagesByAlertCss(scope, messages, alertcss) {
		var $div = Demo.common.showValidateMessages(scope, messages);
		clearValidateAlertCss($div);
		$div.addClass(alertcss);
	}

	function clearValidateAlertCss(selector) {
	    $(selector).removeClass("note-danger").removeClass("note-info").removeClass("note-success");
	}

	Demo.common.ajaxError = function (msg) {
		processAjaxError = true;
		Demo.common.errorNotify(msg);
	};
}(window.corporation = window.corporation || {}, jQuery));

/*  Ajax  */
$.ajaxSetup({
	cache: false,
	ifModified: false,
	timeout: 1800000 /* 30 * 60 * 1000 milisecond */
});