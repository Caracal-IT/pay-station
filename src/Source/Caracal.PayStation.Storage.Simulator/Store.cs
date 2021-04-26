using System;
using System.Collections.Generic;
using System.Linq;
using Caracal.PayStation.Storage.Simulator.Model.Withdrawals;

namespace Caracal.PayStation.Storage.Simulator {
    public static class Store {
        public static Dictionary<long, Withdrawal> Withdrawals { get; } = new();
        
        static Store() {
            var rnd = new Random();
            
            foreach (var i in Enumerable.Range(1, 5))
                Withdrawals.Add(i, new Withdrawal(i, $"Savings {i + rnd.Next(100, 900)}", $"R {i}0.44", "Requested"));
        }
    }
}