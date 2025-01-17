namespace Game.Test.Interactables
{
    // Класс, представляющий интерактивную полку, наследуемый от класса InteractableObject
    public class InteractableShelf : InteractableObject
    {
        // Метод, вызываемый при старте объекта
        private void Start()
        {
            Init(); // Инициализация объекта при запуске
        }
    }
}