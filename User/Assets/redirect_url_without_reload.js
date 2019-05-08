// $(document).on('click','.page-sidebar-menu a',function () {
//     var urlPath=$(this).attr('href');
//     var pageTitle = $(this).find('.title').text();
//     $.ajax({
//         url: urlPath + '?v=1',
//         type: 'GET',
//         success: function (data) {
//             $("#main-content").html(data);        
//             document.title = pageTitle;
//             var stateObj = { foo: "bar" };
//             history.pushState(stateObj, "page", urlPath);
         
//         },
//         error: function () {
//             alert('error');
//         }
//     });

  
//     $('.page-sidebar-menu a').each(function (index) {
//         if (urlPath.indexOf($('.page-sidebar-menu a').eq(index).attr('href')) > -1) {
//             $('.page-sidebar-menu li').removeClass('active');
//             $('.page-sidebar-menu a').eq(index).parents('li').addClass('active');
//         }
//     });
//     return false;
//});