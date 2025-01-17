using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Test.Interactables
{
    // Класс, представляющий базовый объект, с которым можно взаимодействовать
    public class ItemBase : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        private RectTransform _schelfRect; // Прямоугольник полки (не используется в текущей реализации)
        private Canvas _canvas; // Ссылка на Canvas, в который вложен объект
        private bool _isDragging = false; // Флаг, указывающий на перетаскивание объекта

        private float _gravityScale = 2.5f; // Коэффициент гравитации объекта

        private float _initialScale = 1; // Начальный масштаб объекта
        private InteractableObject _interactableObject; // Ссылка на объект, с которым можно взаимодействовать

        public Rigidbody2D ItemRb { get; private set; } // Rigidbody2D объекта (для физики)
        public RectTransform ItemRectTransform { get; private set; } // RectTransform объекта (для позиционирования)

        // Инициализация объекта
        private void Start()
        {
            ItemRb = GetComponent<Rigidbody2D>(); // Получение Rigidbody2D объекта
            ItemRectTransform = GetComponent<RectTransform>(); // Получение RectTransform объекта

            _canvas = GetComponentInParent<Canvas>(); // Получение ссылки на Canvas, в который вложен объект

            _initialScale = ItemRectTransform.localScale.x; // Установка начального масштаба объекта

            ResetObjectProperties(); // Сброс свойств объекта
        }

        // Метод для сброса свойств объекта
        private void ResetObjectProperties()
        {
            ItemRb.isKinematic = false; // Убрать объект из кинематического состояния
            ItemRb.gravityScale = 0; // Отключить гравитацию
            ItemRb.velocity = Vector3.zero; // Сбросить скорость объекта

            ItemRectTransform.localScale = Vector3.one * _initialScale; // Установка масштаба объекта
        }

        // Обработка нажатия на объект
        public void OnPointerDown(PointerEventData eventData)
        {
            _isDragging = true; // Установка флага перетаскивания

            ResetObjectProperties(); // Сброс свойств объекта
        }

        // Обработка перетаскивания объекта
        public void OnDrag(PointerEventData eventData)
        {
            if (_isDragging)
            {
                ItemRectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor; // Изменение позиции объекта при перетаскивании
            }
        }

        // Обработка отпускания объекта
        public void OnPointerUp(PointerEventData eventData)
        {
            _isDragging = false; // Сброс флага перетаскивания
            ItemRb.gravityScale = _gravityScale; // Установка гравитации объекта

            if (_interactableObject != null)
                _interactableObject.AddItem(this); // Добавление объекта к другому объекту, если есть ссылка на него
        }

        // Обработка столкновения объекта с другим объектом
        private void OnCollisionEnter2D(Collision2D collision)
        {
            _interactableObject = collision.gameObject.GetComponent<InteractableObject>(); // Получение ссылки на объект, с которым произошло столкновение
        }

        // Обработка окончания столкновения объекта с другим объектом
        private void OnCollisionExit2D(Collision2D collision)
        {
            _interactableObject = null; // Сброс ссылки на объект после окончания столкновения
        }
    }
}