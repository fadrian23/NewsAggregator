using Microsoft.AspNetCore.Mvc;
using NewsAggregator.WebUI.Binders;
using System;

namespace NewsAggregator.WebUI.Models
{
    [ModelBinder(BinderType = typeof(DateRangeBinder))]
    public class DateRange
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
