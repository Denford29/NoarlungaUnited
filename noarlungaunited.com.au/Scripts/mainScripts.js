(function ($) {

    //set the functions included
    var siteScripts = {
        onReady: function () {
            this.mainMenu();
        },

        //define what each function does
        mainMenu: function () {
            // Navigation Menu
            $('ul.mainmenucontain li').hover(
                function () {
                    $(this).children('div').css('display', 'table')
                },
                function () {
                    $(this).children('div').css('display', 'none')
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

        }

    };

    // on doc ready load the defined main function
    $(document).ready(function () {
        siteScripts.onReady();
    });

})(jQuery);