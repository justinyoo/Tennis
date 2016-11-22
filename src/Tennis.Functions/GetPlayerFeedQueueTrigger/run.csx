#load "..\Shared\ServiceLocator.csx"

using System;

using Newtonsoft.Json;

using Tournaments.Models;
using Tournaments.Services;

// --- Function

public static async Task Run(string queueItem, IAsyncCollector<string> outputQueues, TraceWriter log)
{
    log.Info("Player queue received.");

    var player = JsonConvert.DeserializeObject<PlayerModel>(queueItem);

    log.Info($"Player: {player.Name}");

    var service = locator.GetService<IFeedService>();

    var feed = await service.GetTournamentFeedByMemberIdAsync(player.MemberId.Value).ConfigureAwait(false);

    foreach (var item in feed.Items)
    {
        var ptfim = new PlayerTournamentFeedItemModel() { PlayerId = player.PlayerId, Item = item };
        var serialised = JsonConvert.SerializeObject(ptfim);
        await outputQueues.AddAsync(serialised);
    }

    log.Info("All feed items queued.");
}