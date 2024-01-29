using Microsoft.AspNetCore.Mvc;

namespace CrudStudent.Views.Shared.Components.ImageUploader
{
    public class ImageUploaderViewComponent : ViewComponent
    {
       public ImageUploaderViewComponent()
        {

        }
        public IViewComponentResult Invoke(string FileName)
        {
            return View("Default", FileName);
        }
    }
}
