
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Тестовые сохранения для демо сцены
        // Можно удалить этот код, но тогда удалите и демо (папка Example)
        public int money = 1;                       // Можно задать полям значения по умолчанию
        public string newPlayerName = "Hello!";
        public bool[] openLevels = new bool[3];






        // Ваши сохранения
        public int Coins = 0;

        //улучшения
        public float HP_modificator = 1f;
        public int HP_Upgrade_Count = 0;

        public float Coins_modificator = 1f;
        public int Coins_Upgrade_Count = 0;

        public float Damage_modificator = 1f;
        public int Damage_Upgrade_Count = 0;

        public float Speed_modificator = 1f;
        public int Speed_Upgrade_Count = 0;

        public float Eye_modificator = 1f;
        public int Eye_Upgrade_Count = 0;

        public bool Jerk = false;

        public int lives = 0;

        public bool Double = false;
        /// да, тут ещё и глобальные переменные

        public float volume = 1f;
        public bool Hardmode = false;
        public bool ManualChangedLang = false;




        // ...

        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны
        // Пока выявленное ограничение - это расширение массива


        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            // Допустим, задать значения по умолчанию для отдельных элементов массива

            openLevels[1] = true;

            // Длина массива в проекте должна быть задана один раз!
            // Если после публикации игры изменить длину массива, то после обновления игры у пользователей сохранения могут поломаться
            // Если всё же необходимо увеличить длину массива, сдвиньте данное поле массива в самую нижнюю строку кода
        }
    }
}
