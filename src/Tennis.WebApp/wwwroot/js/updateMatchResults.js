"use strict";

(function ($) {
    var aggregate = function (scoreElementClass, totalScoreElementId) {
        var sum = 0;
        var $elements = $("." + scoreElementClass);
        $elements.each(function (i, element) {
            sum += parseInt($(element).val());

            $("#" + totalScoreElementId).val(sum);
        });
    };

    var calculate = function (setNumber) {
        var homeGameScore = $(".home-game-score").filter("[data-set-number='" + setNumber + "']").val();
        var awayGameScore = $(".away-game-score").filter("[data-set-number='" + setNumber + "']").val();

        if (homeGameScore > awayGameScore) {
            $(".home-set-score").filter("[data-set-number='" + setNumber + "']").val(1);
            $(".away-set-score").filter("[data-set-number='" + setNumber + "']").val(0);
        } else if (homeGameScore < awayGameScore) {
            $(".home-set-score").filter("[data-set-number='" + setNumber + "']").val(0);
            $(".away-set-score").filter("[data-set-number='" + setNumber + "']").val(1);
        } else {
            alert("Check the score!");
            return false;
        }

        aggregate("home-game-score", "total-home-game-score");
        aggregate("away-game-score", "total-away-game-score");

        aggregate("home-set-score", "total-home-set-score");
        aggregate("away-set-score", "total-away-set-score");
    };

    $(".home-game-score")
        .change(function () {
            var setNumber = $(this).data("setNumber");
            calculate(setNumber);
        });

    $(".away-game-score")
        .change(function () {
            var setNumber = $(this).data("setNumber");
            calculate(setNumber);
        });
})(jQuery);
