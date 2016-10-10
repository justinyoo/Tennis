"use strict";

(function ($) {
    var url = $.validator.format("/clubs/{0}/teams");

    var emptyOption = function (elementId) {
        var $option = $("<option></option>")
            .attr("value", "")
            .text("Select Team");
        $("#" + elementId).empty().append($option);
    };

    var appendTeam = function (elementId, team) {
        var $option = $("<option></option>")
            .attr("value", team.teamId)
            .text(team.name);
        $("#" + elementId).append($option);
    };

    var appendTeams = function (elementId, teams) {
        $.each(teams,
            function (i, team) {
                appendTeam(elementId, team);
            });
    };

    $("#Club")
        .change(function () {
            emptyOption("club-team");

            var clubId = $(this).val();
            if (clubId === "") {
                return;
            }

            $.getJSON(url(clubId),
                function (teams) {
                    appendTeams("club-team", teams);
                });
        });
})(jQuery);
