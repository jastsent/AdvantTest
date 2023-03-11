using AdvantTest.Save;

namespace AdvantTest.Base
{
    public struct BalanceComponent
    {
        public int Balance
        {
            get => _saveData.Balance;
            set => _saveData.Balance = value;
        } 

        private SaveData _saveData;
        
        public void SetSaveData(SaveData saveData)
        {
            _saveData = saveData;
        }
    }
}