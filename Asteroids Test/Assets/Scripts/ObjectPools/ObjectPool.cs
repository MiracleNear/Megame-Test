using System.Collections.Generic;
using UnityEngine;

namespace ObjectPools
{
    public abstract class ObjectPool<T> : MonoBehaviour, IObjectPool<T> where T : Component
    {
         [SerializeField] private int _baseCapacity = 16;
         [SerializeField] private int _additionCapacity = 16;
         [SerializeField] private T _template;

         private Stack<T> _pool;
         
         public T GetFreeElement()
         {
             if (_pool == null)
             {
                 CreatePool(_baseCapacity);
             }
             else if (_pool.Count == 0)
             {
                 CreatePool(_additionCapacity);
             }

             return Release(_pool.Pop(), true);
         }

         public void ReturnToPool(T element)
         {
             _pool.Push(Release(element, false));
         }

         private void CreatePool(int capacity)
         {
             _pool = new Stack<T>(capacity);
             
             Fill(capacity);
         }

         private void Fill(int count)
         {
             for (int i = 0; i < count; i++)
             {
                 _pool.Push(CreateTemplate());
             }
         }

         private T CreateTemplate()
         {
             T template = Instantiate(_template);

             return Release(template, false);
         }

         private T Release(T poolElement, bool isActive)
         {
             poolElement.gameObject.SetActive(isActive);
             poolElement.transform.parent = isActive ? null : transform;

             return poolElement;
         }
    }
}