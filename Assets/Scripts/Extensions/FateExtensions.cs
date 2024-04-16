public static class FateExtensions
{
    public static string ConvertToString(this Fate fate)
    {
        return fate switch
        {
            Fate.BurnInHell => "Гореть в аду",
            Fate.NoPurification => "Не заслужил очищение",
            Fate.SlightSinner => "Легкий грешник",
            Fate.DeservePurification => "Заслужил очищение",
            Fate.GoodFellow => "Славный малый",
            Fate.Righteous => "Праведник",
            _ => null
        };
    }
}
