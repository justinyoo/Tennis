#load "..\Shared\ServiceLocator.csx"

using System;

using Newtonsoft.Json;

using Tournaments.Services;

// --- Function

public static async Task Run(TimerInfo timer, IAsyncCollector<string> outputQueues, TraceWriter log)
{
    var now = DateTimeOffset.Now;
    log.Info($"C# Timer trigger function executed at: {now}");

    var service = locator.GetService<IPlayerService>();

    var players = await service.GetPlayersAsync().ConfigureAwait(false);

    foreach (var player in players)
    {
        var serialised = JsonConvert.SerializeObject(player);
        await outputQueues.AddAsync(serialised).ConfigureAwait(false);
    }

    log.Info($"All players queued!");
}