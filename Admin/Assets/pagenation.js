
function getLangData(PageValue) {   
    rowCount = $('#tablepagecount').val();
    action = $('#tablepagecount').attr('data-action');
    Metronic.blockUI({
        boxed: true,
        message: jsLangText['loading']
    });

    $.ajax({
        url: action,
        type: 'POST',
        data: {
            RowCount: rowCount,
            PageValue: PageValue
        },
        dataType: 'json',
        success: function (data) {
            window.setTimeout(function () {
                Metronic.unblockUI();
            }, 300);

            var html = '';
            for (var i = 0; i < data.length; i++) {
                html += '<tr>' +
                    '<td>' + ((i + 1) + (currentPage - 1) * rowCount) + '</td>' +
                    '<td>' + data[i].Text + '</td><td class="numeric">' + data[i].Value + '</td>';
                if (jsPermission['isEdit'] == 'True') {
                    html += '<td data-type="edit"><a class="btn green" href="/admin/langs/edit/' + data[i].ID + '"><i class="fa fa-edit "></i> Edit</a></td> ';
                }
                if (jsPermission['isDelete'] == 'True') {
                    html += '<td><a class="btn red" data-toggle="modal" data-action="delete" data-id="' + data[i].ID + '" href="#confirm"><i class="fa fa-minus-circle"></i> Delete</a></td>';
                }
                html += '</tr>';
            }
            $('#table tbody').html(html);

            createLangPagenation(rowCount);

        },
        error: function () {
            window.setTimeout(function () {
                Metronic.unblockUI();
            }, 100);
            alert(jsLangText['globalerror']);
        }
    });
    //window.setTimeout(function () {
    //    clear();
    //}, 70);

}



$(document).on('change', '#tablepagecount', function () {
    currentPage = 1;
    getLangData(currentPage);
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


    getLangData(currentPage);
});

function createLangPagenation(rowCount) {

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

