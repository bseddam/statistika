$(function () {
    var owl2 = $('.my-carusel').owlCarousel({
        loop: true,
        margin: 10,
        nav: true,

        autoplay: true,
        autoplayTimeout: 3000,
        autoplayHoverPause: false,

        touchDrag: true,
        mouseDrag: false,
        navText: ["<i class='fa fa-chevron-left'></i>", "<i class='fa fa-chevron-right'></i>"],
        navClass: ['owl-prev', 'owl-next'],
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 1
            },
            1000: {
                items: 1
            }
        }
    });

    $('.owl-item:not(.cloned) .item').each(function (index, item) {
        console.log($(this).attr('data-color'));
        $('.owl-dots .owl-dot').eq(index).find('span').css('background', $(this).attr('data-color'));
    });

    var owl = $('.owl-carousel').owlCarousel({
        loop: true,
        margin: 10,
        nav: true,



        touchDrag: true,
        mouseDrag: false,
        navText: ["<i class='fa fa-chevron-left'></i>", "<i class='fa fa-chevron-right'></i>"],
        navClass: ['owl-prev', 'owl-next'],
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 1
            },
            1000: {
                items: 1
            }
        }
    });

    var pageLoad = true;
    owl.on('changed.owl.carousel', function (event) {
        var maxCount = event.item.count;
        var currentItem = (event.item.index - 2) % maxCount;
        if (currentItem <= 0 || currentItem > maxCount) {
            currentItem = 0;
        }

        if (pageLoad) {
            if (currentItem == 0) {
                pageLoad = false;
            } else {
                currentItem -= 1;
            }

        }
        $('.content-goal').removeClass('active');
        $('.content-goal[data-index="' + currentItem + '"]').addClass('active');

    });


    $('#slider-content .owl-next').html('<i class="fa fa-long-arrow-right" aria-hidden="true"></i>');
    $('#slider-content .owl-prev').html('<i class="fa fa-long-arrow-left" aria-hidden="true"></i>');
    $('#slider-content .owl-dots').remove();



    $('[data-toggle="tooltip"]').tooltip()


});

leftBar = $('#leftbar');
$(document).on('click', '#loader', function () {

    self = $(this);

    // leftBar.attr('data-style-close', leftBar.attr('style'));
    leftBar.attr('data-style-open', 'height:auto');

    if (self.hasClass('down')) {
        leftBar.attr('style', leftBar.attr('data-style-open'));
        self.removeClass('down').addClass('up');
    } else {
        leftBar.attr('style', leftBar.attr('data-style-close'));
        self.removeClass('up').addClass('down');
    }
    //leftBar.css({
    //    'overflow': '',
    //    'height': 'auto'
    //});
    // self.removeClass('down').addClass('up');
});

$(document).on('click', '.content-popup-image img', function () {
    src = $(this).attr('src');
    $.magnificPopup.open({
        items: {
            src: src
        },
        type: 'image'
    });
});

$(document).ready(function () {

    $(".toggle-accordion").on("click", function () {
        var accordionId = $(this).attr("accordion-id"),
          numPanelOpen = $(accordionId + ' .collapse.in').length;

        $(this).toggleClass("active");

        if (numPanelOpen == 0) {
            openAllPanels(accordionId);
        } else {
            closeAllPanels(accordionId);
        }
    })

    openAllPanels = function (aId) {
        console.log("setAllPanelOpen");
        $(aId + ' .panel-collapse:not(".in")').collapse('show');
    }
    closeAllPanels = function (aId) {
        console.log("setAllPanelclose");
        $(aId + ' .panel-collapse.in').collapse('hide');
    }

    //$('.content-popup-image').magnificPopup({
    //    delegate: 'img',
    //    type: 'image',
    //    gallery: {
    //        enabled: false
    //    },
    //    // other options
    //});

    //$('#some-button').magnificPopup({
    //    items: {
    //        src: 'path-to-image-1.jpg'
    //    },
    //    type: 'image' // this is default type
    //});

    $('.page-gallery-item').magnificPopup({
        type: 'image',
        gallery: {
            enabled: true
        },
        // other options
    });


    setTimeout(function () {

        leftBar.css('height', $('#content').height() - 40);
        leftBar.attr('data-style-close', leftBar.attr('style'));
    }, 200);
});

$(document).on('click', '.print-page', function () {
    var prtContent = document.getElementsByClassName("printable")[0];

    if (prtContent != null) {
        var WinPrint = window.open('', '', 'left=0,top=0,width=800,height=900,toolbar=0,scrollbars=0,status=0');
        WinPrint.document.write(prtContent.innerHTML);
        WinPrint.document.getElementsByClassName("share-box")[0].style.display = 'none';
        WinPrint.document.close();
        WinPrint.focus();
        WinPrint.print();
        WinPrint.close();
    } else {
        window.print();
    }
});

$(document).on('click', '.goal-info-tab-container .goal-info-nav li a', function () {
    self = $(this).parents('li');
    $('.goal-info-tab-container .goal-info-nav li').removeClass('active');
    self.addClass('active');

    dataContent = self.attr('data-content');

    $('.goal-info-tab-container .tab-content').removeClass('active');
    $('.goal-info-tab-container .tab-content[data-content="' + dataContent + '"]').addClass('active');
    
});

$(document).on('click', '.arrow-container', function () {
    if ($('.search-form').hasClass('menu-open')) {
        $('.search-form').removeClass('menu-open');
    } else {
        $('.search-form').addClass('menu-open');
    }
});
