$(function () {
    var windowUrl = window.location.href.toLowerCase();
    //var windowUrl = window.location.href.toLowerCase().split('.')[0];
    setTimeout(function () {
        $('.page-sidebar-menu li').removeClass('active');
        $('.page-sidebar-menu li').each(function (index) {
            pageUrl = $(this).find('a').attr('href');
            if (windowUrl.indexOf(pageUrl) > -1) {
                $(this).addClass('active');
                if ($(this).parent().hasClass('sub-menu')) {
                    $(this).parent().css('display', 'block');
                    $(this).parent().parent().css('display', 'block');
                    $(this).parent().parent().addClass('open');
                }
            }
        });
      

    }, 100);

});
function getUrlParameter(sParam) {
    var sPageURL = window.location.search.substring(1);
    var sURLVariables = sPageURL.split('&');
    for (var i = 0; i < sURLVariables.length; i++) {
        var sParameterName = sURLVariables[i].split('=');
        if (sParameterName[0] == sParam) {
            return sParameterName[1];
        }
    }
}

function formatJSONDate(jsonDate) {
    dateString = jsonDate.substr(6);
    currentTime = new Date(parseInt(dateString));
    month = currentTime.getMonth() + 1;
    day = currentTime.getDate();
    year = currentTime.getFullYear();
    date = day + "." + month + "." + year;
    return date;

}

$(document).on('click', '.show-filter', function () {
    $('.filter-panel').slideDown();
    return false;
});
$(document).on('click', '.close-panel', function () {
    $('.filter-panel').slideUp();
    return false;
});


$(document).on('click', '.not-exists-in-list', function () {
    data_for = $(this).attr('data-for');
    $('#' + data_for).removeClass('display-none');
});



function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('[data-type="from-fileupload"]').attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }
}

$(document).on('change', '[data-type="fileupload"]', function () {
    readURL(this);

    $('#BtnUpload').click();
});

$(document).on('focusout', '.diplomnostart', function () {
    _calculate_diplomCount();
});
$(document).on('focusout', '.diplomnoend', function () {
    _calculate_diplomCount();
});
function _calculate_diplomCount() {

    _startVal = $('.diplomnostart').val();
    _endVal = $('.diplomnoend').val();


    if (_startVal.toString().trim().length < 1) {
        return;
    }
    if (_endVal.toString().trim().length < 1) {
        return;
    }

    _result = parseInt(_endVal) - parseInt(_startVal) + 1;
    _oldVal = $('[data-type="note"]').val();

    $('[data-type="note"]').val(_oldVal + ' Diplom sayı : ' + _result);

    $('[data-type="diplomcount"]').text('Diplom sayı : ' + _result);
    $('[data-type="diplomcount"]').css('color', 'red');
}
