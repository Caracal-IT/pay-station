using System;
using System.Collections.Generic;
using System.Linq;
using Caracal.PayStation.PaymentModels.Withdrawals;

namespace Caracal.PayStation.Storage.Simulator {
    public static class Store {
        public static List<Withdrawal> Withdrawals { get; } = new();
        
        static Store() {
            var rnd = new Random();
            
            foreach (var i in Enumerable.Range(1, 5))
                Withdrawals.Add(new Withdrawal(i, $"Savings {i + rnd.Next(100, 900)}", $"R {i}0.44", "Requested"));
        }
    }
}