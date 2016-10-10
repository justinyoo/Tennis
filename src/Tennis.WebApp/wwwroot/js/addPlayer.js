"use strict";

(function ($) {
    $("#add-player")
        .click(function () {
            var $firstNames = $("<input>")
                .addClass("form-control")
                .attr({
                    "name": "FirstNames",
                    "placeholder": "First Name",
                    "style": "display: inline-block !important"
                });

            var $lastNames = $("<input>")
                .addClass("form-control")
                .attr({
                    "name": "LastNames",
                    "placeholder": "Last Name",
                    "style": "display: inline-block !important"
                });

            var $removeTeamMember = $("<span></span>")
                .addClass("glyphicon glyphicon-remove")
                .attr("aria-hidden", true)
                .bind("click",
                    function () {
                        var $parent = $(this).parent().parent();
                        $parent.remove();
                    });

            var $column = $("<div></div>")
                .addClass("col-xs-12 col-sm-offset-2 col-sm-10 col-md-offset-1 col-md-11 col-lg-offset-1 col-lg-11")
                .append($firstNames)
                .append($lastNames)
                .append($removeTeamMember);

            var $formGroup = $("<div></div>")
                .addClass("form-group")
                .append($column);

            $("#players").append($formGroup);
        });
})(jQuery);
