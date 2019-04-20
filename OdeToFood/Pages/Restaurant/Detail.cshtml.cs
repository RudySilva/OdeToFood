using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Core;

namespace OdeToFood.Pages.Restaurant
{
    public class DetailModel : PageModel
    {

        public readonly OdeToFood.Data.IRestaurantData restaurantData;

        [TempData]
        public string Message { get; set; }

        public OdeToFood.Core.Restaurant Restaurant { get; set; }

        public DetailModel(OdeToFood.Data.IRestaurantData restaurantData)
        {
            this.restaurantData = restaurantData;
        }

        public IActionResult OnGet(int restaurantId)
        {
            Restaurant = restaurantData.GetById(restaurantId);
            if (Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }
    }
}