"use strict";

(function ($) {
    var url = $.validator.format("/competitions/{0}/teams/{1}/players");

    var emptyOption = function (elementIdPrefix) {
        var $option = $("<option></option>")
            .attr("value", "")
            .text("Select Player");
        $("select[id^=" + elementIdPrefix + "]").empty().append($option);
    };

    var appendPlayer = function (elementIdPrefix, player) {
        var $option = $("<option></option>")
            .attr("value", player.playerId)
            .text(player.name);
        $("select[id^=" + elementIdPrefix + "]").append($option);
    };

    var appendPlayers = function (elementIdPrefix, players) {
        $.each(players,
            function (i, player) {
                appendPlayer(elementIdPrefix, player);
            });
    };

    $("#HomeTeam")
        .change(function () {
            emptyOption("home-player-");

            var competitionId = $(this).data("competitionId");
            var teamId = $(this).val();
            if (teamId === "") {
                return;
            }

            $.getJSON(url(competitionId, teamId),
                function (players) {
                    appendPlayers("home-player-", players);
                });
        });

    $("#AwayTeam")
        .change(function () {
            emptyOption("away-player-");

            var competitionId = $(this).data("competitionId");
            var teamId = $(this).val();
            if (teamId === "") {
                return;
            }

            $.getJSON(url(competitionId, teamId),
                function (players) {
                    appendPlayers("away-player-", players);
                });
        });
})(jQuery);
