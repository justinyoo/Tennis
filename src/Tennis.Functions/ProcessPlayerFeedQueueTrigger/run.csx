#load "..\Shared\ServiceLocator.csx"

using System;

using Newtonsoft.Json;

using Tournaments.Models;
using Tournaments.Services;

// --- Function

public static async Task Run(string queueItem, TraceWriter log)
{
    log.Info("Feed item queue received.");

    var ptfim = JsonConvert.DeserializeObject<PlayerTournamentFeedItemModel>(queueItem);

    var tournamentService = locator.GetService<ITournamentService>();
    var tournamentId = await tournamentService.SaveTournamentAsync(ptfim).ConfigureAwait(false);

    var playerService = locator.GetService<IPlayerService>();
    var ptId = await playerService.SavePlayerTournamentAsync(tournamentId, ptfim).ConfigureAwait(false);

    log.Info($"Feed item queue for {ptfim.PlayerId} processed.");
}