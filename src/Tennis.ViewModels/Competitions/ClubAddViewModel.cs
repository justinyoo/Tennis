using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Tennis.ViewModels.Competitions
{
    /// <summary>
    /// This represents the view model entity for competition to add.
    /// </summary>
    public class ClubAddViewModel
    {
        /// <summary>
        /// Gets or sets the name of competition.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the manager of the club.
        /// </summary>
        public string Manager { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the club.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the mobile number of the club.
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// Gets or sets the email of the club.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the address 1.
        /// </summary>
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the address 2.
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the suburb.
        /// </summary>
        public string Suburb { get; set; }

        /// <summary>
        /// Gets or sets the list of states.
        /// </summary>
        public List<SelectListItem> States { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the postcode.
        /// </summary>
        public string Postcode { get; set; }

        /// <summary>
        /// Gets or sets the URL of club.
        /// </summary>
        public string Url { get; set; }
    }
}