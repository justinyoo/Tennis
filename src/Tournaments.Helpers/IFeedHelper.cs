using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tournaments.Helpers
{
    /// <summary>
    /// This provides interfaces to the <see cref="FeedHelper"/> class.
    /// </summary>
    public interface IFeedHelper : IDisposable
    {
        /// <summary>
        /// Gets the first name, middle names and last name from the feed title.
        /// </summary>
        /// <param name="title">Feed title.</param>
        /// <returns>Returns the first name, middle names and last name.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="title"/> is <see langword="null" />.</exception>
        Task<List<string>> GetNamesFromTitleAsync(string title);

        /// <summary>
        /// Gets the member Id from the feed URL.
        /// </summary>
        /// <param name="url">Feed URL at tennis.com.au</param>
        /// <returns>Returns the member Id.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="url"/> is <see langword="null" />.</exception>
        Task<long> GetMemberIdFromUrlAsync(string url);

        /// <summary>
        /// Gets the tournament key from the link URL.
        /// </summary>
        /// <param name="url">Feed URL at tennis.com.au</param>
        /// <returns>Returns the tournament key.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="url"/> is <see langword="null" />.</exception>
        Guid GetTournamentKeyFromUrl(string url);

        /// <summary>
        /// Gets the tournament key from the link URL.
        /// </summary>
        /// <param name="url">Feed URL at tennis.com.au</param>
        /// <returns>Returns the tournament key.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="url"/> is <see langword="null" />.</exception>
        Task<Guid> GetTournamentKeyFromUrlAsync(string url);

        /// <summary>
        /// Gets the player number from the link URL.
        /// </summary>
        /// <param name="url">Feed URL at tennis.com.au</param>
        /// <returns>Returns the player number.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="url"/> is <see langword="null" />.</exception>
        int GetPlayerNumberFromUrl(string url);

        /// <summary>
        /// Gets the player number from the link URL.
        /// </summary>
        /// <param name="url">Feed URL at tennis.com.au</param>
        /// <returns>Returns the player number.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="url"/> is <see langword="null" />.</exception>
        Task<int> GetPlayerNumberFromUrlAsync(string url);
    }
}