﻿
$(document).on('change', '#tablepagecount', function () {
    currentPage = 1;
    getData(currentPage);
});
$(document).on('change', '#table_activity', function () {
    currentPage = 1;
    getData(currentPage);
});
$(document).on('click', '.pagination a', function () {
    if ($(this).hasClass('active') || $(this).hasClass('disabled') || $(this).attr('attr') == 'disabled') {
        return false;
    }

    $('.pagination li a').each(function (index) {
        $(this).eq(index).removeClass('active');
    });
    if ($(this).attr('data-val') == 'prev') {
        currentPage -= 1;
    } else if ($(this).attr('data-val') == 'next') {
        currentPage += 1;
    }
    else {
        currentPage = $(this).attr('data-val');
    }


    getData(currentPage);
});

function createPagenation(rowCount) {

    $('.pagination').html('');
    dataLength = $('input[name="rowcount"]').val();
    var pageHtml = '';
    pagenationCount = Math.ceil(dataLength / rowCount);
  
    if (pagenationCount > 1) {
        for (var i = 0; i < pagenationCount; i++) {
            pageHtml += '<li><a href="javascript:void(0)"'
            if (currentPage == (i + 1)) {
                pageHtml += "class='active'";
            }
            pageHtml += ' data-val="' + (i + 1) + '" >' + (i + 1) + '</a></li>';


        }

        $('.pagination').html(pageHtml);

        if (currentPage == 1) {
            $('.pagination li a').eq(0).attr('disabled', 'disabled');
            $('.pagination li a').eq(0).addClass('disabled');
        }


        if (currentPage == pagenationCount) {
            $('.pagination li a').eq(currentPageSize + 1).attr('disabled', 'disabled');
            $('.pagination li a').eq(currentPageSize + 1).addClass('disabled');
        }
    }

}

function getData(PageValue) {
    activity = $('#table_activity').val();
    rowCount = $('#tablepagecount').val();
    action = $('#tablepagecount').attr('data-action');
    Metronic.blockUI({
        boxed: true,
        target: '.portlet-body',
        message: jsLangText['loading']
    });

    $.ajax({
        url: action,
        type: 'POST',
        data: {
            __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val(),
            RowCount: rowCount,
            PageValue: PageValue,
            Activity: activity
        },
        dataType: 'json',
        success: function (data) {
            window.setTimeout(function () {
                Metronic.unblockUI('.portlet-body');
            }, 300);
            var nodata = '<tr><td colspan="8"><center>No data to display</center></td></tr>';
            var html = '';
            if (data == 'error') {
                $('#table tbody').html(nodata);
                return false;
            }
            if (data.length < 1) {
                $('#table tbody').html(nodata);
                return false;
            }
            for (var i = 0; i < data.length; i++) {
                html += '<tr>' +
                    '<td>' + ((i + 1) + (currentPage - 1) * rowCount) + '</td>' +
                    '<td>' + data[i].Mail + '</td>' +
                    '<td>' + data[i].Name + '</td>' +
                    '<td>' + data[i].Surname + '</td>' +
                    '<td>' + formatJSONDate(data[i].LastDt) + '</td>' +
                    '</tr>';
            }
            $('#table tbody').html(html);

            createPagenation(rowCount);

        },
        error: function () {
            window.setTimeout(function () {
                Metronic.unblockUI('.portlet-body');
            }, 100);
            alert(jsLangText['globalerror']);
        }
    });
}

$(document).on('click', 'a[data-action="delete"]', function () {
    $('.user-delete .alert').css('display', 'none');
    $('.user-delete').attr('data-id', $(this).attr('data-id'));
});
$(document).on('click', 'a[data-action="changeactivity"]', function () {
    $('.user-changeactivity .alert').css('display', 'none');
    $('.user-changeactivity').attr('data-id', $(this).attr('data-id'));
    $('.user-changeactivity').attr('data-activity', $(this).attr('data-activity'));
});
$(document).on('submit', '.user-delete', function () {
    Metronic.blockUI({
        boxed: true, target: '.portlet-body',
        message: jsLangText['loading']
    });
    var action = $(this).attr('action');
    $.ajax({
        url: action,
        type: 'POST',
        data: {
            __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val(),
            id: $(this).attr('data-id')
        },
        dataType: 'json',
        success: function (data) {
            window.setTimeout(function () {
                Metronic.unblockUI('.portlet-body');
            }, 300);

            if (data.key == "ok") {
                $('#delete').modal('hide').delay(5000);
                getData(currentPage);

            } else {
                $('.alert').css('display', 'block');
                $('.alert').html(data.text);
            }

        },
        error: function () {
            window.setTimeout(function () {
                Metronic.unblockUI('.portlet-body');
            }, 200);
            $('.alert').css('display', 'block');
            $('.alert').html(jsLangText['globalerror']);
        }
    });


    return false;
});
$(document).on('submit', '.user-changeactivity', function () {
    Metronic.blockUI({
        boxed: true, target: '.portlet-body',
        message: jsLangText['loading']
    });
    var token = $(this).find('input[name="__RequestVerificationToken"]').val();
    var activity = $(this).attr('data-activity');
    var action = $(this).attr('action');
    var dataId = $(this).attr('data-id');
   
    $.ajax({
        url: action,
        type: 'POST',
        data: {
            __RequestVerificationToken: token,
            activity: activity,
            id: dataId
        },
        dataType: 'json',
        success: function (data) {
            window.setTimeout(function () {
                Metronic.unblockUI('.portlet-body');
            }, 300);

            if (data.key == "ok") {
                $('#user-changeactivity').modal('hide').delay(5000);
                getData(currentPage);

            } else {
                $('.alert').css('display', 'block');
                $('.alert').html(data.text);
            }

        },
        error: function () {
            window.setTimeout(function () {
                Metronic.unblockUI('.portlet-body');
            }, 200);
            $('.alert').css('display', 'block');
            $('.alert').html(jsLangText['globalerror']);
        }
    });


    return false;
});
