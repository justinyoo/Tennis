using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;

namespace Tournaments.Services
{
    /// <summary>
    /// This provides interfaces to the <see cref="FeedContext"/> class.
    /// </summary>
    public interface IFeedContext : IDisposable
    {
        /// <summary>
        /// Gets the first name, middle names and last name from the feed title.
        /// </summary>
        /// <param name="title">Feed title.</param>
        /// <returns>Returns the first name, middle names and last name.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="title"/> is <see langword="null" />.</exception>
        Task<List<string>> GetNamesFromFeedAsync(string title);

        /// <summary>
        /// Gets the member Id from the given feed URL.
        /// </summary>
        /// <param name="url">Feed URL at tennis.com.au.</param>
        /// <returns>Returns the member Id from the given feed URL.</returns>
        Task<long> GetMemberIdFromFeedAsync(string url);

        /// <summary>
        /// Gets the tournament key from the given feed URL.
        /// </summary>
        /// <param name="url">Feed URL at tennis.com.au.</param>
        /// <returns>Returns the member Id from the given feed URL.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="url"/> is <see langword="null" />.</exception>
        Task<Guid> GetTournamentKeyFromFeedAsync(string url);

        /// <summary>
        /// Gets the player number from the given feed URL.
        /// </summary>
        /// <param name="url">Feed URL at tennis.com.au.</param>
        /// <returns>Returns the member Id from the given feed URL.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="url"/> is <see langword="null" />.</exception>
        Task<int> GetPlayerNumberFromFeedAsync(string url);

        ///// <summary>
        ///// Loads the feed for the member Id of tennis.com.au
        ///// </summary>
        ///// <param name="url">Feed URL at tennis.com.au.</param>
        ///// <returns>Returns the <see cref="System.ServiceModel.Syndication.SyndicationFeed"/> instance.</returns>
        //Task<SyndicationFeed> LoadFeedAsync(string url);

        /// <summary>
        /// Loads the feed for the member Id of tennis.com.au
        /// </summary>
        /// <param name="memberId">Member Id of tennis.com.au</param>
        /// <returns>Returns the <see cref="System.ServiceModel.Syndication.SyndicationFeed"/> instance.</returns>
        Task<SyndicationFeed> LoadFeedAsync(long memberId);
    }
}