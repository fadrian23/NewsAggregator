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
         * set DateTime.MinValue to startdate if it's not provided
         * set DateTime.MaxValue to enddate if it's not provided
         */
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var startValue = bindingContext.ValueProvider.GetValue("Start").FirstValue;
            var endValue = bindingContext.ValueProvider.GetValue("End").FirstValue;

            var startDateValid = true;
            var endDateValid = true;

            if (!DateTime.TryParse(startValue, out var startDate))
            {
                startDateValid = false;
                if (startValue is not null)
                {
                    bindingContext.ModelState.TryAddModelError(
                        bindingContext.ModelName,
                        "Start value is not correct"
                    );
                    return Task.CompletedTask;
                }
            }

            if (!DateTime.TryParse(endValue, out var endDate))
            {
                endDateValid = false;
                if (endValue is not null)
                {
                    bindingContext.ModelState.TryAddModelError(
                        bindingContext.ModelName,
                        "End value is not correct"
                    );
                    return Task.CompletedTask;
                }
            }

            DateRange model = new DateRange();

            if (!startDateValid)
                model.Start = DateTime.MinValue;
            else
                model.Start = startDate;

            if (!endDateValid)
                model.End = DateTime.MaxValue;
            else
                model.End = endDate;

            bindingContext.Result = ModelBindingResult.Success(model);

            return Task.CompletedTask;
        }
    }
}
