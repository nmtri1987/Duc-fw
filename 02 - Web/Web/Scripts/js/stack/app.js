!function (n, e, a) {
    "use strict";
    var t = a("html"), i = a("body");
    a(n).on("load", function () {
        a("html").data("textdirection"), setTimeout(function () {
            t.removeClass("loading").addClass("loaded")
        }
    , 1200), a.app.menu.init();
        var n = {
            speed: 300
        }
        ;
        !1 === a.app.nav.initialized && a.app.nav.init(n), Unison.on("change", function (n) {
            a.app.menu.change()
        }
    ), a('[data-toggle="tooltip"]').tooltip({
        container: "body"
    }
    ), a(".navbar-hide-on-scroll").length > 0 && (a(".navbar-hide-on-scroll.navbar-fixed-top").headroom({
        offset: 205, tolerance: 5, classes: {
            initial: "headroom", pinned: "headroom--pinned-top", unpinned: "headroom--unpinned-top"
        }

    }

    ), a(".navbar-hide-on-scroll.navbar-fixed-bottom").headroom({
        offset: 205, tolerance: 5, classes: {
            initial: "headroom", pinned: "headroom--pinned-bottom", unpinned: "headroom--unpinned-bottom"
        }

    }
    )), a('a[data-action="collapse"]').on("click", function (n) {
        n.preventDefault(), a(this).closest(".card").children(".card-body").collapse("toggle"), a(this).closest(".card").find('[data-action="collapse"] i').toggleClass("icon-minus4 icon-plus4")
    }
    ), a('a[data-action="expand"]').on("click", function (n) {
        n.preventDefault(), a(this).closest(".card").find('[data-action="expand"] i').toggleClass("icon-expand2 icon-contract"), a(this).closest(".card").toggleClass("card-fullscreen")
    }
    ), a(".scrollable-container").length > 0 && a(".scrollable-container").perfectScrollbar({
        theme: "dark"
    }
    ), a('a[data-action="reload"]').on("click", function () {
        a(this).closest(".card").block({
            message: '<div class="ft-refresh-cw icon-spin font-medium-2"></div>', timeout: 2e3, overlayCSS: {
                backgroundColor: "#FFF", cursor: "wait"
            }
    , css: {
        border: 0, padding: 0, backgroundColor: "none"
    }

        }
    )
    }
    ), a('a[data-action="close"]').on("click", function () {
        a(this).closest(".card").removeClass().slideUp("fast")
    }
    ), setTimeout(function () {
        a(".row.match-height").each(function () {
            a(this).find(".card").not(".card .card").matchHeight()
        }
    )
    }
    , 500), a('.card .heading-elements a[data-action="collapse"]').on("click", function () {
        var n, e = a(this), t = e.closest(".card");
        console.log(parseInt(t[0].style.height, 10)), parseInt(t[0].style.height, 10) > 0 ? (n = t.css("height"), t.css("height", "").attr("data-height", n)) : t.data("height") && (n = t.data("height"), t.css("height", n).attr("data-height", ""))
    }
    );
        var e = i.data("menu");
        "horizontal-menu" != e && a(".main-menu-content").find("li.active").parents("li").addClass("open"), "horizontal-menu" == e && (a(".main-menu-content").find("li.active").parents("li:not(.nav-item)").addClass("open"), a(".main-menu-content").find("li.active").parents("li").addClass("active")), a(".heading-elements-toggle").on("click", function () {
            a(this).parent().children(".heading-elements").toggleClass("visible")
        }
    );
        var o = a(".chartjs"), s = o.children("canvas").attr("height");
        if (o.css("height", s), a("body").hasClass("boxed-layout") && a("body").hasClass("vertical-overlay-menu")) {
            var l = a(".main-menu").width(), c = a(".app-content").position().left, r = c - l;
            a("body").hasClass("menu-flipped") ? a(".main-menu").css("right", r + "px") : a(".main-menu").css("left", r + "px")
        }
        a(".nav-link-search").on("click", function () {
            var n = (a(this), a(this).siblings(".search-input"));
            n.hasClass("open") ? n.removeClass("open") : n.addClass("open")
        }
        )
    }
), a(e).on("click", ".menu-toggle", function (e) {
    return e.preventDefault(), a.app.menu.toggle(), setTimeout(function () {
        a(n).trigger("resize")
    }
, 100), !1
}
), a(e).on("click", ".open-navbar-container", function (n) {
    var e = Unison.fetch.now();
    a.app.menu.drillDownMenu(e.name)
}
), a(e).on("click", ".main-menu-footer .footer-toggle", function (n) {
    return n.preventDefault(), a(this).find("i").toggleClass("pe-is-i-angle-down pe-is-i-angle-up"), a(".main-menu-footer").toggleClass("footer-close footer-open"), !1
}
), a(".navigation").find("li").has("ul").addClass("has-sub"), a(".carousel").carousel({
    interval: 2e3
}
), a(".nav-link-expand").on("click", function (n) {
    "undefined" != typeof screenfull && screenfull.enabled && screenfull.toggle()
}
), "undefined" != typeof screenfull && screenfull.enabled && a(e).on(screenfull.raw.fullscreenchange, function () {
    screenfull.isFullscreen ? a(".nav-link-expand").find("i").toggleClass("icon-contract icon-expand2") : a(".nav-link-expand").find("i").toggleClass("icon-expand2 icon-contract")
}
), a(e).on("click", ".mega-dropdown-menu", function (n) {
    n.stopPropagation()
}
), a(e).ready(function () {
    a(".step-icon").each(function () {
        var n = a(this);
        n.siblings("span.step").length > 0 && (n.siblings("span.step").empty(), a(this).appendTo(a(this).siblings("span.step")))
    }
)
}
), a(n).resize(function () {
    a.app.menu.manualScroller.updateHeight()
}
), a(".nav.nav-tabs a.dropdown-item").on("click", function () {
    var n = a(this), e = n.attr("href"), t = n.closest(".nav");
    t.find(".nav-link").removeClass("active"), n.closest(".nav-item").find(".nav-link").addClass("active"), n.closest(".dropdown-menu").find(".dropdown-item").removeClass("active"), n.addClass("active"), t.next().find(e).siblings(".tab-pane").removeClass("active in").attr("aria-expanded", !1), a(e).addClass("active in").attr("aria-expanded", "true")
}
), a("#sidebar-page-navigation").on("click", "a.nav-link", function (n) {
    n.preventDefault(), n.stopPropagation();
    var e = a(this), t = e.attr("href"), i = a(t).offset(), o = i.top - 80;
    a("html, body").animate({
        scrollTop: o
    }
, 0), setTimeout(function () {
    e.parent(".nav-item").siblings(".nav-item").children(".nav-link").removeClass("active"), e.addClass("active")
}
, 100)
}
)
}
(window, document, jQuery);
