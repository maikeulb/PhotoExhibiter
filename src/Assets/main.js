import _ from 'lodash';
window.lodash = _;
window._ = _;

import $ from 'jquery';
window.jQuery = $;
window.$ = $;

import 'bootstrap';
import 'ekko-lightbox';

import bootbox from 'bootbox';
window.bootbox = bootbox;

import moment from 'moment';
window.moment = moment;

import Rellax from 'rellax';
window.Rellax = Rellax;

import datepicker from 'bootstrap-datepicker';
window.datepicker = datepicker;

import dt from 'datatables.net';
window.dt = dt;

import Noty from 'noty';
window.Noty = Noty;

// require("datatables.net-bs")(window, $);
// require("datatables.net-responsive")(window, $);
// require("datatables.net-responsive-bs")(window, $);

// require("!style-loader!css-loader!datatables.net-bs/css/dataTables.bootstrap.css");
// require("!style-loader!css-loader!datatables.net-responsive-bs/css/responsive.bootstrap.css");

import logoSmall from './images/logo.png';
var logoImg = document.getElementById('logo_sm');
logoImg.src = logoSmall;

import banner from './images/banner.jpg';
var bannerImg = document.getElementById('banner');
bannerImg.src =  banner;

var highlightFields = function (response) {

    $('.form-group').removeClass('has-error');

    $.each(response, function (propName, val) {
        var nameSelector = '[name = "' + propName.replace(/(:|\.|\[|\])/g, "\\$1") + '"]',
            idSelector = '#' + propName.replace(/(:|\.|\[|\])/g, "\\$1");
        var $el = $(nameSelector) || $(idSelector);

        if (val.Errors.length > 0) {
            $el.closest('.form-group').addClass('has-error');
        }
    });
};

var highlightErrors = function (xhr) {
    try {
        var data = JSON.parse(xhr.responseText);
        highlightFields(data);
        showSummary(data);
        window.scrollTo(0, 0);
    } catch (e) {
    }
};

var showSummary = function (response) {
    $('#validationSummary').empty().removeClass('hidden');

    var verboseErrors = _.flatten(_.map(response, 'Errors')),
        errors = [];

    var nonNullErrors = _.reject(verboseErrors, function (error) {
        return error.ErrorMessage.indexOf('must not be empty') > -1;
    });

    _.each(nonNullErrors, function (error) {
        errors.push(error.ErrorMessage);
    });

    if (nonNullErrors.length !== verboseErrors.length) {
        errors.push('The highlighted fields are required to submit this form.');
    }

    var $ul = $('#validationSummary').append('<ul></ul>');

    _.each(errors, function (error) {
        var $li = $('<li></li>').text(error);
        $li.appendTo($ul);
    });
};

var redirect = function (data) {
    if (data.redirect) {
        window.location = data.redirect;
    } else {
        window.scrollTo(0, 0);
        window.location.reload();
    }
};

$('form[method=post]').not('.no-ajax').on('submit', function () {
    var submitBtn = $(this).find('[type="submit"]');

    submitBtn.prop('disabled', true);
    $(window).unbind();

    var $this = $(this),
        formData = $this.serialize();

    $this.find('div').removeClass('has-error');

    $.ajax({
        url: $this.attr('action'),
        type: 'post',
        data: formData,
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        dataType: 'json',
        statusCode: {
            200: redirect
        },
        complete: function () {
            submitBtn.prop('disabled', false);
        }
    }).error(highlightErrors);

    return false;
});

import './main.scss';
