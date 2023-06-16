using Microsoft.AspNetCore.Mvc;

namespace HarborView_Inn.Components
{
    public class AboutUsSummary : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            object data = "Over the last 25 years, the Glory Hotels organisation has been known " +
                "for dependably providing the best Indian hospitality experience. It combines " +
                "modern style and comfort with the warmth of Old World hospitality. With more " +
                "than 50 hotels and resorts across the world, it is one of the world's largest " +
                "hotel chains. We believe in the values of Indian hospitality, and our crew is " +
                "our most valuable asset, providing passionate and memorable hospitality to everyone " +
                "we meet.";
            return View(data);
        }
    }
}
