using PolishVehicleRecords.Models.Entities;
using System;
using System.Collections.Generic;

namespace PolishVehicleRecords.Models.Builders
{
    class CarsSearchBuilder
    {
        private CarsSearch SearchForm;

        public CarsSearchBuilder()
        {
            SearchForm = new CarsSearch();
        }

        public CarsSearchBuilder SetDateRange(DateTime fromDate, DateTime? toDate = null)
        {
            SearchForm.StartDate = fromDate;
            return this;
        }

        public CarsSearchBuilder OnlyRegisteredCars(bool registered = true)
        {
            SearchForm.OnlyRegistered = registered;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="limit">Max value is 500</param>
        /// <returns></returns>
        public CarsSearchBuilder SetItemsLimit(ushort limit)
        {
            SearchForm.Limit = limit <= 500 ? limit : (ushort)500;
            return this;
        }

        public CarsSearchBuilder SetVoivodeiships(List<string> voivodeships)
        {
            SearchForm.Voivodeships = voivodeships;
            return this;
        }

        public CarsSearchBuilder SetFields(List<string> fields)
        {
            SearchForm.Fields = fields;
            return this;
        }

        public CarsSearchBuilder SetCarTypes(List<string> types)
        {
            SearchForm.Types = types;
            return this;
        }

        public static implicit operator CarsSearch(CarsSearchBuilder pb)
        {
            return pb.SearchForm;
        }
    }
}
