using System.Collections.Generic;

namespace Tennis.AppSettings
{
    /// <summary>
    /// This represents the model entity for appsettings.json.
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Gets or sets the <see cref="AuthenticationSettings"/> instance.
        /// </summary>
        public virtual AuthenticationSettings Authentication { get; set; }

        /// <summary>
        /// Gets or sets the list of the <see cref="ConnectionStringSettings"/> instances.
        /// </summary>
        public virtual List<ConnectionStringSettings> ConnectionStrings { get; set; }
    }
}