"use strict";

(function ($) {
    var teamsUrl = $.validator.format("/clubs/{0}/teams");
    var fixturesUrl = $.validator.format("/competitions/{0}/teams/{1}/fixtures");
    var fixtureUrl = $.validator.format("/competitions/{0}/fixtures/{1}");

    var pad = function (value, size) {
        var s = String(value);
        while (s.length < (size || 2)) {
            s = "0" + s;
        }

        return s;
    }

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

    var emptyFixture = function (elementId) {
        $("#" + elementId).empty();
    };

    var appendFixture = function (elementId, fixture) {
        var $tr = $("<tr></tr>")
            .append($("<td></td>")
                .append($("<a></a>")
                    .attr("href", fixtureUrl(fixture.competitionId, fixture.fixtureId))
                    .text(pad(fixture.round, 2)))
            )
            .append($("<td></td>").text(moment(fixture.dateScheduled).format("YYYY-MM-DD HH:mm")))
            .append($("<td></td>").html("<strong>" + fixture.club + "</strong> &ndash; " + fixture.venue))
            .append($("<td></td>")
                .append($("<a></a>")
                    .attr("href", fixtureUrl(fixture.competitionId, fixture.fixtureId))
                    .text("View"))
            );
        $("#" + elementId).append($tr);
    };

    var appendFixtures = function (elementId, fixtures) {
        $.each(fixtures,
            function (i, fixture) {
                appendFixture(elementId, fixture);
            });
    };

    $("#Club")
        .change(function () {
            emptyOption("club-team");

            var clubId = $(this).val();
            if (clubId === "") {
                return;
            }

            $.getJSON(teamsUrl(clubId),
                function (teams) {
                    appendTeams("club-team", teams);
                });
        });

    $("#teams")
        .change(function () {
            var competitionId = $(this).data("competitionId");
            var teamId = $(this).val();

            $.getJSON(fixturesUrl(competitionId, teamId),
                function (team) {
                    if (team.teamCode === "" || team.teamCode === undefined) {
                        $("#team-code").val("");
                        $("#fixture-form").attr("style", "display: none !important;");
                    } else {
                        $("#team-code").val(team.teamCode);
                        $("#fixture-form").attr("style", "display: inline-block !important;");
                    }

                    emptyFixture("fixtures");
                    appendFixtures("fixtures", team.fixtures);
                });
        });
})(jQuery);
