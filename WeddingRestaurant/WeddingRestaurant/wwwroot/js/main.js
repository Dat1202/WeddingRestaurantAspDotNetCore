(function ($) {
    "use strict";

    // Spinner
    var spinner = function () {
        setTimeout(function () {
            if ($('#spinner').length > 0) {
                $('#spinner').removeClass('show');
            }
        }, 1);
    };
    spinner(0);


    // Fixed Navbar
    $(window).scroll(function () {
        if ($(window).width() < 400) {
            if ($(this).scrollTop() > 55) {
                $('.fixed-top').addClass('shadow');
            } else {
                $('.fixed-top').removeClass('shadow');
            }
        } else {
            if ($(this).scrollTop() > 55) {
                $('.fixed-top').addClass('shadow').css('top', -55);
            } else {
                $('.fixed-top').removeClass('shadow').css('top', 0);
            }
        }
    });
})(jQuery);
jQuery(document).ready(function () {
    show_1 = jQuery(".thuc-don:nth-child(1)");
    show_2 = jQuery(".thuc-don:nth-child(2)");
    show_1.find(".menu-content").css("display", "block");
    show_2.find(".menu-content").css("display", "block");
    show_1.find(".open-menu").hide(1000);
    show_2.find(".open-menu").hide(1000);
    show_1.find(".close-menu").show(1000);
    show_2.find(".close-menu").show(1000)
})
jQuery(document).ready(function () {
    var format = /[<>{}="']+/;
    input = jQuery(".check-dish");
    order = jQuery("#order").find("table");
    stt = 1;
    tonggia = 0;
    jQuery(".check-all").click(function (e) {
        e.preventDefault();
        target = jQuery(this).parent().parent().find(".check-dish");
        target.each(function () {
            if (jQuery(this).is(":checked")) { } else {
                jQuery(this).trigger("click")
            }
        })
    })
    jQuery(".uncheck-all").click(function (e) {
        e.preventDefault();
        target = jQuery(this).parent().parent().find(".check-dish");
        target.each(function () {
            if (jQuery(this).is(":checked")) {
                jQuery(this).trigger("click")
            } else { }
        })
    })
    jQuery(".check-dish").change(function check_dish() {
        jQuery("#toggle-order").addClass("anirun");
        setTimeout(function () {
            jQuery("#toggle-order").removeClass("anirun")
        }, 500);
        ten = jQuery(this).attr("data-ten");
        gia = jQuery(this).attr("data-gia");
        giafm = new Intl.NumberFormat().format(gia);
        clas = jQuery(this).attr("name");
        add = "<tr class='" + clas + "'><td class='stt'>" + stt + "</td><td>" + ten + "</td><td class='price'>" + giafm + " VND</td><td class='xoa' title='Delete'><i class='far fa-trash-alt'></i></td><input class='hidden-dish' type='hidden' value='" + gia + "' data-name='" + ten + "'></tr>";
        if (jQuery(this).is(":checked")) {
            stt = stt + 1;
            order.append(add);
            tonggia = Number(tonggia) + Number(gia)
        } else {
            tonggia = Number(tonggia) - Number(gia);
            stt = stt - 1;
            xoa = order.find("." + clas);
            xoa.remove();
            tdstt = order.find(".stt");
            for (var i = 0; i < stt; i++) {
                jQuery(tdstt[i]).html(i + 1)
            }
        }
        tonggiafm = tonggia;
        tonggiafm = new Intl.NumberFormat().format(tonggiafm);
        jQuery("#tong-tien span").html(tonggiafm + " VND");
        jQuery("#numb-order").html(stt - 1);
        return stt;
        return tonggia
    })
    jQuery(document).on("click", ".xoa", function () {
        jQuery("#toggle-order").addClass("anirun");
        setTimeout(function () {
            jQuery("#toggle-order").removeClass("anirun")
        }, 500);
        prclass = jQuery(this).parent().attr("class");
        jQuery("#" + prclass).prop('checked', !1);
        gia = jQuery(this).parent().find("input").val();
        jQuery(this).parent().remove();
        stt = stt - 1;
        tdstt = order.find(".stt");
        tonggia = tonggia - gia;
        tonggiafm = tonggia;
        tonggiafm = new Intl.NumberFormat().format(tonggiafm);
        jQuery("#tong-tien span").html(tonggiafm + " VND");
        jQuery("#numb-order").html(stt - 1);
        for (var i = 0; i < stt; i++) {
            jQuery(tdstt[i]).html(i + 1)
        }
    });
 
    jQuery("#toggle-order").click(function () {
        jQuery("#order").toggle()
    })
    jQuery("span.close-order").click(function (e) {
        e.preventDefault();
        jQuery("#order").hide()
    })
    jQuery("#order button").click(function () {
        ml = jQuery(".hidden-dish").length;
        if (ml == 0) {
            alert("Vui lòng chọn món!")
        } else {
            jQuery("#customer-info").css("display", "flex")
        }
    })
    jQuery("#customer-info .popup-bgr").click(function () {
        jQuery("#customer-info").hide()
    })

    jQuery(document).ready(function () {
        jQuery(".open-menu").click(function (e) {
            e.preventDefault();
            jQuery(this).hide(1000);
            parent = jQuery(this).parent().parent();
            jQuery(this).parent().parent().find(".menu-content").show(1000);
            jQuery(parent).find(".close-menu").show(1000)
        })
        jQuery(".close-menu").click(function (e) {
            e.preventDefault();
            jQuery(this).hide(1000);
            parent = jQuery(this).parent().parent();
            jQuery(this).parent().parent().find(".menu-content").hide(1000);
            jQuery(parent).find(".open-menu").show(1000)
        })
    })
    jQuery(document).ready(function () {
        jQuery(".namlbn-mobile-menu .menu-item-has-children").first().addClass("show-sub");
        jQuery(".namlbn-mobile-menu .menu-item-has-children").click(function () {
            jQuery(".show-sub").removeClass("show-sub");
            jQuery(this).addClass("show-sub")
        })
        jQuery("#namlbn-open-menu").click(function () {
            jQuery(".namlbn-mobile-menu").css({
                "transform": "translateX(0%)",
                "transition": ".5s"
            })
        })
        jQuery("#namlbn-close-menu").click(function () {
            jQuery(".namlbn-mobile-menu").css({
                "transform": "translateX(100%)",
                "transition": ".2s"
            })
        });
        jQuery(".namlbn-mobile-menu ul.menu > .menu-item-has-children > a").click(function (e) {
            e.preventDefault()
        })
    });
});
