using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tournaments.Helpers
{
    /// <summary>
    /// This represents the helper entity for feeds.
    /// </summary>
    public class FeedHelper : IFeedHelper
    {
        private bool _disposed;

        /// <summary>
        /// Gets the first name, middle names and last name from the feed title.
        /// </summary>
        /// <param name="title">Feed title.</param>
        /// <returns>Returns the first name, middle names and last name.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="title"/> is <see langword="null" />.</exception>
        public async Task<List<string>> GetNamesFromTitleAsync(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentNullException(nameof(title));
            }

            var names = await Task.Factory.StartNew(() => GetNames(title)).ConfigureAwait(false);
            return names;
        }

        /// <summary>
        /// Gets the member Id from the feed URL.
        /// </summary>
        /// <param name="url">Feed URL at tennis.com.au</param>
        /// <returns>Returns the member Id.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="url"/> is <see langword="null" />.</exception>
        public async Task<long> GetMemberIdFromUrlAsync(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            var memberId = await Task.Factory.StartNew(() => GetQueryValue<long>("memberid", url)).ConfigureAwait(false);
            return memberId;
        }

        /// <summary>
        /// Gets the tournament key from the link URL.
        /// </summary>
        /// <param name="url">Feed URL at tennis.com.au</param>
        /// <returns>Returns the tournament key.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="url"/> is <see langword="null" />.</exception>
        public Guid GetTournamentKeyFromUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            var tournamentKey = GetQueryValue<Guid>("id", url);
            return tournamentKey;
        }

        /// <summary>
        /// Gets the tournament key from the link URL.
        /// </summary>
        /// <param name="url">Feed URL at tennis.com.au</param>
        /// <returns>Returns the tournament key.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="url"/> is <see langword="null" />.</exception>
        public async Task<Guid> GetTournamentKeyFromUrlAsync(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            var tournamentKey = await Task.Factory.StartNew(() => GetTournamentKeyFromUrl(url)).ConfigureAwait(false);
            return tournamentKey;
        }

        /// <summary>
        /// Gets the player number from the link URL.
        /// </summary>
        /// <param name="url">Feed URL at tennis.com.au</param>
        /// <returns>Returns the player number.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="url"/> is <see langword="null" />.</exception>
        public int GetPlayerNumberFromUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            var playerNumber = GetQueryValue<int>("player", url);
            return playerNumber;
        }

        /// <summary>
        /// Gets the player number from the link URL.
        /// </summary>
        /// <param name="url">Feed URL at tennis.com.au</param>
        /// <returns>Returns the player number.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="url"/> is <see langword="null" />.</exception>
        public async Task<int> GetPlayerNumberFromUrlAsync(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            var playerNumber = await Task.Factory.StartNew(() => GetPlayerNumberFromUrl(url)).ConfigureAwait(false);
            return playerNumber;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this._disposed)
            {
                return;
            }

            this._disposed = true;
        }

        private static List<string> GetNames(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentNullException(nameof(title));
            }

            var segments = title.Replace("Tournaments of ", string.Empty)
                                .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(p => p.Trim('.'))
                                .ToArray();

            var names = new List<string>()
                        {
                            segments.First(),
                            segments.Length <= 2 ? string.Empty : string.Join(" ", segments.Skip(1).Take(segments.Length - 2)),
                            segments.Last(),
                        };

            return names;
        }

        private static T GetQueryValue<T>(string key, string url)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            var uri = new Uri(url);
            var qry = uri.Query.Split(new[] { "&" }, StringSplitOptions.RemoveEmptyEntries)
                         .Select(p => p.Trim('?'))
                         .ToDictionary(p => p.Split(new[] { "=" }, StringSplitOptions.RemoveEmptyEntries)[0],
                                       p => p.Split(new[] { "=" }, StringSplitOptions.RemoveEmptyEntries)[1]);

            if (!qry.ContainsKey(key))
            {
                throw new KeyNotFoundException();
            }

            if (typeof(T) == typeof(Guid))
            {
                return (T) Convert.ChangeType(Guid.Parse(qry[key]), typeof(T));
            }

            return (T) Convert.ChangeType(qry[key], typeof(T));
        }
    }
}

