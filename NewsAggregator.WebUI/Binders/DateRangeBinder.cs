using Microsoft.AspNetCore.Mvc.ModelBinding;
using NewsAggregator.Services.Helpers;
using NewsAggregator.WebUI.Models;
using System;
using System.Threading.Tasks;

namespace NewsAggregator.WebUI.Binders
{
    public class DateRangeBinder : IModelBinder
    {
        /*
         * Binding rules:
         * if both Start and End date isn't provided, then Start date will be first millisecond of today and End date the last millisecond
         * if Start is provided and End isn't, then End date will be DateTime.MaxValue
         * if Start is not provided and End is, then Start date will be DateTime.MinValue
         */
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var startValue = bindingContext.ValueProvider.GetValue("Start").FirstValue;
            var endValue = bindingContext.ValueProvider.GetValue("End").FirstValue;

            if (string.IsNullOrEmpty(startValue) && string.IsNullOrEmpty(endValue))
            {
                var today = DateTime.Today;
                var model = new DateRange
                {
                    Start = today.AbsoluteStart(),
                    End = today.AbsoluteEnd(),
                };

                bindingContext.Result = ModelBindingResult.Success(model);
            }

            if (!string.IsNullOrEmpty(startValue) && string.IsNullOrEmpty(endValue))
            {
                if (DateTime.TryParse(startValue, out DateTime date))
                {
                    var model = new DateRange { Start = date, End = DateTime.MaxValue, };

                    bindingContext.Result = ModelBindingResult.Success(model);
                }
                else
                {
                    bindingContext.ModelState.TryAddModelError(
                        bindingContext.ModelName,
                        "Wrong format of Start DateTime"
                    );
                }
            }

            if (string.IsNullOrEmpty(startValue) && !string.IsNullOrEmpty(endValue))
            {
                if (DateTime.TryParse(endValue, out DateTime date))
                {
                    var model = new DateRange { Start = DateTime.MinValue, End = date, };

                    bindingContext.Result = ModelBindingResult.Success(model);
                }
                else
                {
                    bindingContext.ModelState.TryAddModelError(
                        bindingContext.ModelName,
                        "Wrong format of End DateTime"
                    );
                }
            }

            return Task.CompletedTask;
        }
    }
}
