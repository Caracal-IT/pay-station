using System;
using System.Linq;

namespace Caracal.PayStation.Application.UseCases.Withdrawals.AutoAllocate {
    public class Utils {
        public static bool CheckContains(string check, string valList)
        {
            if (String.IsNullOrEmpty(check) || String.IsNullOrEmpty(valList))
                return false;

            var list = valList.Split(',').ToList();
            return list.Contains(check);
        }
    }
}