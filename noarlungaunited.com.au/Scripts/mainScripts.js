(function ($) {

    //set the functions included
    var siteScripts = {
        onReady: function () {
            this.mainMenu();
            this.gallerySlider();
        },

        //define what each function does
        mainMenu: function () {
            // Navigation Menu
            $("ul.mainmenucontain li").hover(
                function () {
                    $(this).children("div").css("display", "table");
                },
                function () {
                    $(this).children("div").css("display", "none");
                }
            );

            // Main Menu mobile
            $("<select />").appendTo(".menurelative");

            // Create default option "Go to..."
            $("<option />", {
                "selected": "selected",
                "value": "",
                "text": "Go to..."
            }).appendTo("nav.subnav select");

            // Populate dropdown with menu items
            $("nav.subnav a[href]").each(function () {
                var el = $(this);
                $("<option />", {
                    "value": el.attr("href"),
                    "text": el.text()
                }).appendTo("nav.subnav select");
            });

            // To make dropdown actually work
            $("nav.subnav select").change(function () {
                window.location = $(this).find("option:selected").val();
            });

        },

        //define what each function does
        gallerySlider: function () {
            $("#borderless-checkbox").on("change", function () {
                var borderless = $(this).is(":checked");
                $("#blueimp-gallery").data("useBootstrapModal", !borderless);
                $("#blueimp-gallery").toggleClass("blueimp-gallery-controls", borderless);
            });

            $("#fullscreen-checkbox").on("change", function () {
                $("#blueimp-gallery").data("fullScreen", $(this).is(":checked"));
            });

            $("#image-gallery-button").on("click", function (event) {
                event.preventDefault();
                blueimp.Gallery($("#links a"), $("#blueimp-gallery").data());
            });
        }

    };

    // on doc ready load the defined main function
    $(document).ready(function () {
        siteScripts.onReady();
    });

})(jQuery);