namespace ServerLibraryProject.Services
{
    using ServerLibraryProject.Interfaces;
    using ServerLibraryProject.Models;

    public class MacrosService : IMacrosService
    {
        private readonly IMacrosRepository _macrosRepository;

        public MacrosService(IMacrosRepository macrosRepository)
        {
            this._macrosRepository = macrosRepository;
        }

        public double GetProteinIntake(long userId)
        {
            var macrosList = this._macrosRepository.GetMacrosByUserId(userId);
            return macrosList.Sum(m => m.TotalProtein ?? 0);
        }

        public List<Macros> GetMacrosListByUserId(long userId)
        {
            return this._macrosRepository.GetMacrosByUserId(userId);
        }

        public double GetCarbohydratesIntake(long userId)
        {
            var macrosList = this._macrosRepository.GetMacrosByUserId(userId);
            return macrosList.Sum(m => m.TotalCarbohydrates ?? 0);
        }

        public double GetFatIntake(long userId)
        {
            var macrosList = this._macrosRepository.GetMacrosByUserId(userId);
            return macrosList.Sum(m => m.TotalFat ?? 0);
        }

        public double GetFiberIntake(long userId)
        {
            var macrosList = this._macrosRepository.GetMacrosByUserId(userId);
            return macrosList.Sum(m => m.TotalFiber ?? 0);
        }

        public double GetSugarIntake(long userId)
        {
            var macrosList = this._macrosRepository.GetMacrosByUserId(userId);
            return macrosList.Sum(m => m.TotalSugar ?? 0);
        }
    }
}