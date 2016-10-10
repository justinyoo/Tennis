"use strict";

(function ($) {
    $("#add-team-player")
        .click(function () {
            var $playerOrders = $("<input>")
                .addClass("form-control")
                .attr({
                    "name": "PlayerOrders",
                    "placeholder": "Order",
                    "style": "display: inline-block !important"
                });

            var $players = $("<select></select>")
                .addClass("form-control")
                .attr({
                    "name": "PlayerIds",
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
                .append($playerOrders)
                .append($players)
                .append($removeTeamMember);

            var $formGroup = $("<div></div>")
                .addClass("form-group")
                .append($column);

            $("#players").append($formGroup);
        });
})(jQuery);
