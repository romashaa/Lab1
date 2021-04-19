﻿using Models.Wallets;
using Prism.Mvvm;

namespace BudgetsWPF.Wallets
{
    public class WalletDetailsViewModel : BindableBase
    {
        private Wallet _wallet;

        public string Name
        {
            get { return _wallet.Name; }
            set
            {
                _wallet.Name = value; 
                RaisePropertyChanged(nameof(DisplayName));
            }
        }

        public decimal Balance
        {
            get { return _wallet.Balance; }
            set
            {
                _wallet.Balance = value;
                RaisePropertyChanged(nameof(DisplayName));
            }
        }
        public string DisplayName
        {
            get
            {
                return $"{_wallet.Name} (${_wallet.Balance})";
            }
        }

        public WalletDetailsViewModel(Wallet wallet)
        {
            _wallet = wallet;
        }
    }
}
