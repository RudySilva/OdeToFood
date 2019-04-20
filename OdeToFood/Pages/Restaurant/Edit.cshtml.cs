using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Data;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace OdeToFood.Pages.Restaurant
{
    public class EditModel : PageModel
    {
        public readonly IRestaurantData restaurantData;
        private readonly IHtmlHelper htmlHelper;
        

        public IEnumerable<SelectListItem> Cuisines { get; set; }

        [BindProperty]
        public OdeToFood.Core.Restaurant Restaurant { get; set; }
        

        public EditModel(IRestaurantData restaurantData,
                            IHtmlHelper htmlHelper)
        {
            this.restaurantData = restaurantData;
            this.htmlHelper = htmlHelper;
        }
        
        public IActionResult OnGet(int? restaurantId)
        {
            Cuisines = htmlHelper.GetEnumSelectList<CuisineTypes>();

            if (restaurantId.HasValue)
            {
                Restaurant = restaurantData.GetById(restaurantId.Value);
            }
            else
            {
                Restaurant = new OdeToFood.Core.Restaurant();
            }

            if (Restaurant == null)
            {
                return RedirectToPage("./Notfound");
            }
            return Page();

        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                Cuisines = htmlHelper.GetEnumSelectList<CuisineTypes>();
                //return Page();
            }

            if (Restaurant.Id > 0)
                restaurantData.Update(Restaurant);
            else
                restaurantData.Add(Restaurant);
             
             restaurantData.Commit();
            TempData["Message"] = "Restaurant saved!";
            return RedirectToPage("./Detail", new { restaurantId = Restaurant.Id });
        }
    }
}