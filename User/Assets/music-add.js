$(document).on('submit', '.music-form', function () {
    $('.alert').addClass('display-hide');
    Metronic.blockUI({
        boxed: true,
        target: '.portlet-body',
        message: jsLangText['loading']
    });
    urlPath = $(this).attr('action');

    _categoryID = 0;    _moodID = $('[name="moodID"]').val();
    var _type;
    _url = $('[name="url"]').val();
    _title = $('[name="title"]').val();
    _trackid = $('[name="trackid"]').val();
    _isimagefromurl = $('[name="isimagefromurl"]').parent().hasClass('checked') ? 1 : 0;


    var cssClass = $('.bootstrap-switch').attr('class');
    if (cssClass.indexOf('bootstrap-switch-off') > -1) {
        _type = "from_file";
    } else {

        _type = "from_url";
    }



    var infos = '';

    //for (var i = 1; i <= infoCount; i++) {

    //    infos += $('.music-info' + i).find('select[name="langID"]').val();
    //    infos += '*00*';

    //    infos += $('.music-info' + i).find('.note-editable').html();
    //    infos += '*00*';

    //    infos += '#00#';
    //}

    $('div').each(function (index) {
        type = $('div').eq(index).attr('data-type');

        if (type == 'music-infoText') {

            infos += $('div').eq(index).find('select[name="langID"]').val();
            infos += '*00*';

            infos += $('div').eq(index).find('.note-editable').html();
            infos += '*00*';

            infos += '#00#';
        }

    });


    var formdata = new FormData();

    formdata.append('__RequestVerificationToken', $('input[name="__RequestVerificationToken"]').val());
    formdata.append('categoryID', _categoryID);
    formdata.append('moodID', _moodID);
    formdata.append('infoText', infos);
    formdata.append('url', _url);
    formdata.append('type', _type);
    formdata.append('title', _title);
    formdata.append('trackid', _trackid);
    formdata.append('isimagefromurl', _isimagefromurl);


    var fileInput = document.getElementById('fileInput');
    var fileInput1 = document.getElementById('fileInput1');

    for (i = 0; i < fileInput.files.length; i++) {
        formdata.append(fileInput.files[i].name, fileInput.files[i]);
    }

    for (i = 0; i < fileInput1.files.length; i++) {
        formdata.append(fileInput1.files[i].name, fileInput1.files[i]);
    }

    var xhr = new XMLHttpRequest();
    xhr.open('POST', urlPath);
    xhr.send(formdata);
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            window.setTimeout(function () {
                Metronic.unblockUI('.portlet-body');
            }, 300);

            data = jQuery.parseJSON(xhr.responseText);


            if (data.key == 'ok') {
                $('.alert').removeClass('alert-danger').addClass('alert-success');
            } else {
                $('.alert').removeClass('alert-success').addClass('alert-danger');
            }

            $('.alert').removeClass('display-hide').find('span').text(data.text);
        }
    }
    xhr.onerror = function () {
        window.setTimeout(function () {
            Metronic.unblockUI('.portlet-body');
        }, 100);
        alert(jsLangText['globalerror']);
    };

    return false;

});



$(document).on('click', '[data-type="close"]', function () {
    $(this).parent().parent().remove();
});



