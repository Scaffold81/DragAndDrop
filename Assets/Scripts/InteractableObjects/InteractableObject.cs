using UnityEngine;

namespace Game.Test.Interactables
{
    // Класс, представляющий объект, с которым можно взаимодействовать
    public class InteractableObject : MonoBehaviour
    {
        private RectTransform _rectTransform; // Прямоугольник объекта
        private float _scaleForShelf = 0.5f; // Масштаб для размещения объекта на полке

        // Инициализация объекта (метод может быть переопределен в наследниках)
        public virtual void Init()
        {
            _rectTransform = GetComponent<RectTransform>(); // Получение RectTransform объекта
        }

        // Добавление объекта к текущему объекту
        public virtual void AddItem(ItemBase item)
        {
            item.ItemRb.isKinematic = true; // Установка объекта в кинематическое состояние
            item.ItemRb.gravityScale = 0; // Отключение гравитации для объекта
            item.ItemRectTransform.localScale = Vector3.one * _scaleForShelf; // Установка масштаба объекта
            item.ItemRectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x, _rectTransform.anchoredPosition.y); // Установка позиции объекта
        }

        // Удаление объекта (метод может быть переопределен в наследниках)
        public virtual void RemoveItem()
        {
            // Метод удаления объекта (в данной реализации не содержит действий)
        }
    }
}