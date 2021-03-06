using System;
using System.Collections.Generic;
using System.Text;

namespace lab1_1
{
    public class Wallet
    {
        private static int InstanceCount;
        private int _id;
        private string _name;
        private decimal _initialBalance;
        private string _description;
        private string _currency;
        private int _userId;
        private List<Category> _categories;
        private bool _isShared;
        private List<Transaction> _transactions;


        public int Id
        {
            get
            {
                return _id;
            }
            private set
            {
                _id = value;
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        public decimal InitialBalance
        {
            get
            {
                return _initialBalance;
            }
            set
            {
                _initialBalance = value;
            }
        }
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }
        public string Currency
        {
            get
            {
                return _currency;
            }
            set
            {
                _currency = value;
            }
        }
        public int UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                _userId = value;
            }
        }

        public bool IsShared
        {
            get => _isShared;
            set => _isShared = value;
        }
        public List<Category> Categories
        {
            get => _categories;
            set => _categories = value;
        }
        public List<Transaction> Transactions {
            get => _transactions; 
            set => _transactions = value; 
        }

        public Wallet()
        {
            Categories = new List<Category>();
            Transactions = new List<Transaction>();
            InstanceCount += 1;
            _id = InstanceCount;
        }
        public Wallet(int id, string name, decimal initialBalance, string description, string currency, int userId, List<Category> categories)
        {
            _id = id;
            _name = name;
            _initialBalance = initialBalance;
            _description = description;
            _currency = currency;
            _userId = userId;
            _categories = categories;
        }

        public bool Validate()
        {
            var result = true;

            if (Id <= 0)
                result = false;
            if (String.IsNullOrWhiteSpace(Name))
                result = false;
            if (String.IsNullOrWhiteSpace(Description))
                result = false;
            if (String.IsNullOrWhiteSpace(Currency))
                result = false;
            if (UserId <= 0)
                result = false;

            return result;
        }

        public void addTransaction(Transaction transaction)
        {
            if (_isShared == true)
            {
                _transactions.Add(transaction);
            }
            else if (transaction.UserId == this._userId)
            {
                _transactions.Add(transaction);
            }
            else
            {
                throw new Exception("this wallet can't be shared");
            }
            
        }

        public void deleteTransaction(Transaction transaction)
        {
            _transactions.Remove(transaction);
        }

        public decimal countBalance()
        {
            var result = _initialBalance;
            foreach (var transaction in Transactions)
            {
                //result += transaction.Sum;
                result = Decimal.Add(result, transaction.Sum);
            }
            return result;
         }
      
        public decimal countMonthTransactionsPlus()
        {
            var result = 0.0m;
            DateTime dt = DateTime.Now;
            int monthNow = dt.Month;
            foreach (var transaction in Transactions)
            {
                DateTime tr = transaction.Date;
                int transactionMonth = tr.Month;
                if (monthNow == transactionMonth && transaction.Sum > 0)
                {
                    result = Decimal.Add(result, transaction.Sum);
                    //result += transaction.Sum;
                }
            }
            return result;

        }
        public decimal countMonthTransactionsMinus()
        {
            var result = 0.0m;
            DateTime dt = DateTime.Now;
            int monthNow = dt.Month;
            foreach (var transaction in Transactions)
            {
                DateTime tr = transaction.Date;
                int transactionMonth = tr.Month;
                if (monthNow == transactionMonth && transaction.Sum<0)
                {
                    result = Decimal.Add(result, transaction.Sum);
                    //result += transaction.Sum;
                }
            }
            return result;

        }

        public string showMonthIncome()
        {
           var result = this.countMonthTransactionsPlus();
            return $"Month Income: {result} { _currency}";
        }

        public string showMonthCosts()
        {
            var result = this.countMonthTransactionsMinus() * -1;
            return $"Month costs: {result} { _currency}";
        }
        public string Show()
        {
            return $"Wallet: {_id}, Name: {_name}, initial balance: {_initialBalance}, description: {_description}, currency: {_currency}, user id: {_userId}";
        }


    }
}
