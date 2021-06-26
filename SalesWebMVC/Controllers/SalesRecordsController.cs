using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SallesRecordService _sallesRecordService;

        public SalesRecordsController(SallesRecordService sallesRecordService)
        {
            _sallesRecordService = sallesRecordService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            minDate = SetMinDate(minDate);
            maxDate = SetMaxDate(maxDate);

            ViewData["minDate"] = minDate.Value.ToString("yyyy/MM/dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy/MM/dd");

            var result = await _sallesRecordService.FindByDateAsync(minDate, maxDate);

            return View(result);
        }

        public async Task<IActionResult> GroupingSearch(DateTime? minDate, DateTime? maxDate)
        {
            minDate = SetMinDate(minDate);
            maxDate = SetMaxDate(maxDate);

            ViewData["minDate"] = minDate.Value.ToString("yyyy/MM/dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy/MM/dd");

            var result = await _sallesRecordService.FindByDateGroupingAsync(minDate, maxDate);

            return View(result);
        }

        private DateTime SetMinDate (DateTime? minDate)
        {
            return (DateTime)(minDate.HasValue ? minDate : new DateTime(DateTime.Now.Year, 1, 1));
        }

        private DateTime SetMaxDate(DateTime? maxDate)
        {
            return (DateTime)(maxDate.HasValue ? maxDate : DateTime.Now);
        }

    }
}
